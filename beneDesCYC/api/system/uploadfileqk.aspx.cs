using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Data.OleDb;
using beneDesCYC.core;
using System.Data.OracleClient;

namespace beneDesCYC.api.system
{
    public partial class uploadfileqk : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            fileUpload();
        }

        public void fileUpload()
        {
            HttpPostedFile postedFile = Request.Files["UPLOADFILE"];
            if (postedFile == null)
            {
                _return(false, "上传失败！", "null");
            }
            else
            {
                string fileName = postedFile.FileName;
                if (fileName == "")
                { _return(false, "上传失败，文件名为空！", "null"); }
                string tempPath = System.Configuration.ConfigurationManager.AppSettings["NewsFolderPath"];
                string savePath = Server.MapPath(tempPath);
                string sExtension = fileName.Substring(fileName.LastIndexOf('.'));
                if (!Directory.Exists(savePath))
                { Directory.CreateDirectory(savePath); }
                string sNewFileName = "QK" + DateTime.Now.ToString("yyyymmdd") + Session["userName"].ToString();
                postedFile.SaveAs(savePath + @"/" + sNewFileName + sExtension);

                string address = savePath + @"/" + sNewFileName + sExtension;
                GetSheet getsheetx = new GetSheet();
                string StyleSheet = getsheetx.GetExcelSheetNames(address);
                //string msg = LoadData(StyleSheet, address);
                //if (string.IsNullOrEmpty(msg))
                //{
                //    _return(true, msg, "null");
                //}
                //else
                //{
                //    _return(false, msg, "null");
                //}
                string msg = string.Empty;
                bool trueOrfalse = LoadData(StyleSheet, address, out msg);
                _return(trueOrfalse, msg, "null");
            }

        }
        public bool LoadData(string StyleSheet, string address, out string errorMessage)
        {
            errorMessage = "";
            bool result = false;
            string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" + address + ";Extended Properties=Excel 8.0";
            OleDbConnection myConn = new OleDbConnection(strCon);
            myConn.Open();   //打开数据链接，得到一个数据集     
            DataSet myDataSet = new DataSet();   //创建DataSet对象     
            string StrSql = "select   *   from   [" + StyleSheet + "$]";
            OleDbDataAdapter myCommand = new OleDbDataAdapter(StrSql, myConn);
            myCommand.Fill(myDataSet, "[" + StyleSheet + "$]");
            myCommand.Dispose();
            DataTable DT = myDataSet.Tables["[" + StyleSheet + "$]"];
            myConn.Close();
            myCommand.Dispose();

            //string strConn = "Data Source=orcl; User Id=jilincyc; Password=jilincyc";
            //OracleConnection conn = new OracleConnection(strConn);
            OracleConnection conn = DB.CreatConnection();
            conn.Open();
            int countin = 0;
            for (int j = 0; j < DT.Rows.Count; j++)
            {
                if (DT.Rows[j][0].ToString().Trim() != "" && DT.Rows[j][1].ToString().Trim() != "")
                {
                    countin = countin + 1;
                }
                else
                {
                    j = j + 1;
                    errorMessage = string.Format("第{0}行年月、区块名称不能为空", j);
                    //Response.Write("<script>alert('第:" + j + "行年月、区块名称不能为空')</script>");
                }
            }


            string cyc = Session["cyc_id"].ToString();
            int insertCount = 0;
            StringBuilder builder = new StringBuilder();
            try
            {

                for (int j = 0; j < DT.Rows.Count; j++)
                {
                    string NY = DT.Rows[j][0].ToString();
                    NY = CommonFunctions.ConvertString(NY);
                    if (string.IsNullOrEmpty(NY))
                    {
                        break;
                    }
                    else if (NY.Length != 6)
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据月份错误！\n", j + 1);
                        continue;
                    }
                    string QKMC = DT.Rows[j][1].ToString();
                    QKMC = CommonFunctions.ConvertString(QKMC);
                    if (string.IsNullOrEmpty(QKMC))
                    {
                        errorMessage += string.Format("数据异常——第{0}行数据区块不能为空！\n", j + 1);
                        continue;
                    }
                    if (CommonObject.Area_MC_Table[QKMC] == null)
                    {
                        errorMessage += string.Format("数据异常——第{0}行数据区块名称与A2不一致！\n", j + 1);
                    }

                    string chksql = "select * from qksj where ny='" + NY + "' and qkmc='" + QKMC + "' and cyc_id='" + cyc + "'";
                    OracleCommand chkcmd = new OracleCommand(chksql, conn);
                    OracleDataReader chkread = chkcmd.ExecuteReader();
                    while (chkread.Read())
                    {
                        OracleCommand mycom = new OracleCommand("delete qksj where ny='" + NY + "' and qkmc='" + QKMC + "' and cyc_id='" + cyc + "'", conn);
                        mycom.ExecuteNonQuery();
                    }

                    string YCLX = DT.Rows[j][2].ToString();
                    YCLX = CommonFunctions.ConvertString(YCLX);
                    if (CommonObject.GetYCLX(cyc) == null || CommonObject.GetYCLX(cyc)[YCLX] == null)
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据油藏类型错误！\n", j + 1);
                        continue;
                    }
                    string XSYP = DT.Rows[j][3].ToString();
                    XSYP = CommonFunctions.ConvertString(XSYP);
                    if (CommonObject.XSYP_MC_Table[XSYP] == null)
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据销售油品错误！\n", j + 1);
                        continue;
                    }
                    //else
                    //{
                    //    XSYP = CommonObject.XSYP_DM_Table[XSYP].ToString();
                    //}
                    string DYHYMJ = DT.Rows[j][4].ToString();
                    DYHYMJ = CommonFunctions.ConvertString(DYHYMJ);
                    string DYDZCL = DT.Rows[j][5].ToString();
                    DYDZCL = CommonFunctions.ConvertString(DYDZCL);
                    string DYKCCL = DT.Rows[j][6].ToString();
                    DYKCCL = CommonFunctions.ConvertString(DYKCCL);
                    string YCZS = DT.Rows[j][7].ToString();
                    YCZS = CommonFunctions.ConvertString(YCZS);
                    string PJSTL = DT.Rows[j][8].ToString();
                    PJSTL = CommonFunctions.ConvertString(PJSTL);
                    string DXYYND = DT.Rows[j][9].ToString();
                    DXYYND = CommonFunctions.ConvertString(DXYYND);
                    string YJZJS = DT.Rows[j][10].ToString();
                    YJZJS = CommonFunctions.ConvertString(YJZJS);
                    string SJZJS = DT.Rows[j][11].ToString();
                    SJZJS = CommonFunctions.ConvertString(SJZJS);
                    string YJKJS = DT.Rows[j][12].ToString();
                    YJKJS = CommonFunctions.ConvertString(YJKJS);
                    string SJKJS = DT.Rows[j][13].ToString();
                    SJKJS = CommonFunctions.ConvertString(SJKJS);

                    string ZSJ = DT.Rows[j][14].ToString();
                    ZSJ = CommonFunctions.ConvertString(ZSJ);
                    string LJZSL = DT.Rows[j][15].ToString();
                    LJZSL = CommonFunctions.ConvertString(LJZSL);
                    string ZQJZJS = DT.Rows[j][16].ToString();
                    ZQJZJS = CommonFunctions.ConvertString(ZQJZJS);
                    string ZQJKJZ = DT.Rows[j][17].ToString();
                    ZQJKJZ = CommonFunctions.ConvertString(ZQJKJZ);
                    string ZQL = DT.Rows[j][18].ToString();
                    ZQL = CommonFunctions.ConvertString(ZQL);
                    string LJZQL = DT.Rows[j][19].ToString();
                    LJZQL = CommonFunctions.ConvertString(LJZQL);
                    string CYOUL = DT.Rows[j][20].ToString();
                    CYOUL = CommonFunctions.ConvertString(CYOUL);
                    string LJCYOUL = DT.Rows[j][21].ToString();
                    LJCYOUL = CommonFunctions.ConvertString(LJCYOUL);
                    string CYL = DT.Rows[j][22].ToString();
                    CYL = CommonFunctions.ConvertString(CYL);
                    string LJCYL = DT.Rows[j][23].ToString();
                    LJCYL = CommonFunctions.ConvertString(LJCYL);
                    string CQL = DT.Rows[j][24].ToString();
                    CQL = CommonFunctions.ConvertString(CQL);
                    string LJCQL = DT.Rows[j][25].ToString();
                    LJCQL = CommonFunctions.ConvertString(LJCQL);


                    string DJPJRCY = DT.Rows[j][26].ToString();
                    DJPJRCY = CommonFunctions.ConvertString(DJPJRCY);
                    string NJZHHS = DT.Rows[j][27].ToString();
                    NJZHHS = CommonFunctions.ConvertString(NJZHHS);
                    string JDYQB = DT.Rows[j][28].ToString();
                    JDYQB = CommonFunctions.ConvertString(JDYQB);


                    string KCCLCCCD = DT.Rows[j][29].ToString();
                    KCCLCCCD = CommonFunctions.ConvertString(KCCLCCCD);
                    string SYKCCL = DT.Rows[j][30].ToString();
                    SYKCCL = CommonFunctions.ConvertString(SYKCCL);
                    string DJKZSYKCCL = DT.Rows[j][31].ToString();
                    DJKZSYKCCL = CommonFunctions.ConvertString(DJKZSYKCCL);
                    string JWMD = DT.Rows[j][32].ToString();
                    JWMD = CommonFunctions.ConvertString(JWMD);
                    string SFPJ = DT.Rows[j][33].ToString();
                    SFPJ = CommonFunctions.ConvertString(SFPJ);
                    if (SFPJ == "否")
                    {
                        SFPJ = "0";
                    }
                    else
                    {
                        SFPJ = "1";
                    }


                    string SSYT = "";

                    string PJTTZQ = "0"; //平均吞吐周期
                    string ZJCLF = "";
                    string ZJRLF = "";
                    string ZJDLF = "";
                    string ZJRYF = "";
                    string QYWZRF = "";
                    string JXZYF = "";
                    string WHXJXZYF = "";
                    string CJSJF = "";
                    string WHXLF = "";
                    string CYRCF = "";
                    string YQCLF = "";
                    string QTHSF = "";
                    string TRQJHF = "";
                    string YSF = "";
                    string LYF = "";
                    string QTZJF = "";
                    string CKGLF = "";
                    string ZYYQCP = "";
                    string ZJZH = "";
                    string QJF = "";
                    string KTF = "";
                    string CZCB = "";
                    string CZCB_MY = "";
                    string SCCB = "";
                    string SCCB_MY = "";
                    string YYCB = "";
                    string YYCB_MY = "";
                    string YYXSSR = "";
                    string YYXSSJ = "";
                    string TRQXSSR = "";
                    string TRQXSSJ = "";
                    string BSCPXSSR = "";
                    string BSCPXSSJ = "";
                    string BSCPSHSR = "";
                    string XSSR = "";
                    string XSSJ = "";
                    string YYZYS = "";
                    string TRQZYS = "";
                    string ZYS = "";
                    string SHSR = "";
                    string LR = "";
                    string LR_MY = "";
                    string GSXYJB = "";
                    string YQL = "";
                    string YQSPL = "";
                    string DTL = "";
                    string YYSPL = "";
                    string TRQSPL = "";
                    string BSCPCL = "";
                    string BSCPSPL = "";
                    string CZCBJB = "";
                    string RCYJB = "";
                    string HSJB = "";
                    string YQBJB = "";
                    string KCCLCCCDJB = "";
                    string SYKCCLCCSD = "";
                    string CZCBDLJB = "";


                    string QYWZRF_RYF = "";
                    string YQCLF_RYF = "";
                    string YXCZCB = "";
                    string ZJYXCZCB = "";
                    string GSXYJB_1 = "";
                    string YXCZCB_MY = "";
                    string ZJYXCZCB_MY = "";

                    string CJSJF_RYF = "";
                    string WHXLF_RYF = "";
                    string YSF_RYF = "";
                    string TBSYJ = "";
                    string ZJCLF_1 = "";
                    string ZJDLF_1 = "";
                    string WHXZYLWF = "";

                    string lhqhl = ""; string nxyhl = "";
                    string ysdcyl = ""; string mqdcyl = ""; string kfjd = ""; string kffs = ""; string qjzjs = ""; string qjkjs = ""; string cclx = "";
                    string pjkxd = ""; string nxyjb = ""; string hljb = ""; string rcqjb = ""; string nxyzys = ""; string djpjrcq = "";




                    //string strSql = "insert into qksj(NY,QKMC,DYHYMJ,DYDZCL, DYKCCL,YCZS,PJSTL,DXYYND,YJZJS,SJZJS,YJKJS,SJKJS,ZSJ,LJZSL,ZQJZJS,ZQJKJZ,ZQL,LJZQL,CYOUL,LJCYOUL,CYL,LJCYL,CQL,LJCQL,DJPJRCY,NJZHHS,PJTTZQ,JDYQB,KCCLCCCD, SYKCCL,DJKZSYKCCL,JWMD,SSYT,YCLX,XSYP,ZJCLF,ZJRLF, ZJDLF,ZJRYF,QYWZRF,JXZYF,WHXJXZYF,CJSJF,";
                    //StrSql+="WHXLF,CYRCF,YQCLF,QTHSF,TRQJHF,YSF,LYF,QTZJF,CKGLF,ZYYQCP, ZJZH,QJF,KTF,CZCB, CZCB_MY,SCCB,SCCB_MY ,YYCB ,YYCB_MY,YYXSSR,";
                    //StrSql += "YYXSSJ,TRQXSSR,TRQXSSJ, BSCPXSSR,BSCPXSSJ,BSCPSHSR,XSSR,XSSJ,YYZYS,TRQZYS,ZYS,SHSR,LR, LR_MY,GSXYJB,YQL,YQSPL,DTL,YYSPL,TRQSPL,BSCPCL,BSCPSPL,CZCBJB, RCYJB, HSJB,YQBJB,KCCLCCCDJB,SYKCCLCCSD,CZCBDLJB,SFPJ,QYWZRF_RYF,YQCLF_RYF,YXCZCB,ZJYXCZCB,GSXYJB_1) values('" +NY+"',  '" +QKMC+"', '"+DYHYMJ+"', '"+DYDZCL+"', '"+DYKCCL+"',";
                    string strSql = "insert into qksj values('" + NY + "',  '" + QKMC + "', '" + DYHYMJ + "', '" + DYDZCL + "', '" + DYKCCL + "',";
                    strSql += " '" + YCZS + "', '" + PJSTL + "','" + DXYYND + "', '" + YJZJS + "','" + SJZJS + "',";
                    strSql += "'" + YJKJS + "', '" + SJKJS + "', '" + ZSJ + "','" + LJZSL + "','" + ZQJZJS + "',";
                    strSql += "'" + ZQJKJZ + "', '" + ZQL + "','" + LJZQL + "','" + CYOUL + "','" + LJCYOUL + "',";
                    strSql += "'" + CYL + "', '" + LJCYL + "', '" + CQL + "','" + LJCQL + "','" + DJPJRCY + "',";
                    strSql += "'" + NJZHHS + "', '" + PJTTZQ + "','" + JDYQB + "','" + KCCLCCCD + "','" + SYKCCL + "', ";
                    strSql += "'" + DJKZSYKCCL + "','" + JWMD + "', '" + SSYT + "','" + YCLX + "','" + XSYP + "', ";
                    strSql += " '" + ZJCLF + "', '" + ZJRLF + "',  '" + ZJDLF + "','" + ZJRYF + "','" + QYWZRF + "',";
                    strSql += "'" + JXZYF + "', '" + WHXJXZYF + "', '" + CJSJF + "','" + WHXLF + "', '" + CYRCF + "', ";
                    strSql += "'" + YQCLF + "', '" + QTHSF + "', '" + TRQJHF + "','" + YSF + "','" + LYF + "',";
                    strSql += "'" + QTZJF + "','" + CKGLF + "','" + ZYYQCP + "','" + ZJZH + "', '" + QJF + "',";
                    strSql += "'" + KTF + "', '" + CZCB + "','" + CZCB_MY + "','" + SCCB + "','" + SCCB_MY + "',";
                    strSql += "'" + YYCB + "','" + YYCB_MY + "','" + YYXSSR + "','" + YYXSSJ + "','" + TRQXSSR + "',";
                    strSql += "'" + TRQXSSJ + "','" + BSCPXSSR + "','" + BSCPXSSJ + "','" + BSCPSHSR + "','" + XSSR + "',";
                    strSql += "'" + XSSJ + "','" + YYZYS + "','" + TRQZYS + "','" + ZYS + "','" + SHSR + "',";
                    strSql += "'" + LR + "', '" + LR_MY + "','" + GSXYJB + "','" + YQL + "','" + YQSPL + "',";
                    strSql += "'" + DTL + "', '" + YYSPL + "','" + TRQSPL + "','" + BSCPCL + "','" + BSCPSPL + "',";
                    strSql += "'" + CZCBJB + "','" + RCYJB + "','" + HSJB + "','" + YQBJB + "','" + KCCLCCCDJB + "',";
                    strSql += "'" + SYKCCLCCSD + "','" + CZCBDLJB + "', '" + SFPJ + "','" + QYWZRF_RYF + "', '" + YQCLF_RYF + "',";
                    strSql += "'" + YXCZCB + "','" + ZJYXCZCB + "', '" + GSXYJB_1 + "','" + YXCZCB_MY + "','" + ZJYXCZCB_MY + "','" + cyc + "','" + CJSJF_RYF + "','" + WHXLF_RYF + "','" + YSF_RYF + "','" + TBSYJ + "','" + ZJCLF_1 + "','" + ZJDLF_1 + "','" + WHXZYLWF + "','" + lhqhl + "','" + nxyhl + "','" + ysdcyl + "','" + mqdcyl + "','" + kfjd + "','" + kffs + "','" + qjzjs + "','" + qjkjs + "','" + cclx + "','" + pjkxd + "','" + nxyjb + "','" + hljb + "','" + rcqjb + "','" + nxyzys + "','" + djpjrcq + "')";

                    try
                    {
                        OracleCommand comm = new OracleCommand(strSql, conn);

                        if (comm.ExecuteNonQuery() > 0)
                        {
                            insertCount++;
                        }
                        else
                        {
                            if (builder.Length > 0)
                                builder.Append(",");
                            builder.Append((j + 1).ToString());
                        }
                    }
                    catch
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据导入异常！\n", j + 1);
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage += string.Format("系统错误——{0}\n", e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            string error = "";
            if (insertCount == DT.Rows.Count)
            {
                result = true;
                if (string.IsNullOrEmpty(errorMessage))
                {
                    errorMessage = string.Format("数据导入成功，共导入数据{0}条！", insertCount);
                }
                else
                {
                    errorMessage = string.Format("{0}条数据全部导入，但存在异常数据。\n异常信息：\n{1}", insertCount, errorMessage);
                }
            }
            else
            {
                if (builder.Length == 0)
                {
                    errorMessage = string.Format("总共{0}条数据，导入{1}条！\n异常信息：\n{2}", DT.Rows.Count, insertCount, errorMessage);
                }
                else
                {
                    error = builder.ToString();
                    errorMessage = string.Format("总共{0}条数据，导入{1}条,第{2}条数据SQL执行异常！\n异常信息：\n{3}", DT.Rows.Count, insertCount, error, errorMessage);
                }

            }
            return result;
        }

    }
}
