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
    public partial class uploadfilepjdy : beneDesCYC.core.UI.corePage
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
                string sNewFileName = "PJDY" + DateTime.Now.ToString("yyyymmdd") + Session["userName"].ToString();
                postedFile.SaveAs(savePath + @"/" + sNewFileName + sExtension);

                string address = savePath + @"/" + sNewFileName + sExtension;
                GetSheet getsheetx = new GetSheet();
                string StyleSheet = getsheetx.GetExcelSheetNames(address);

                string msg = string.Empty;
                bool trueOrfalse = LoadData(StyleSheet, address, out msg);
                _return(trueOrfalse, msg, "null"); ;
            }
        }

        public bool LoadData(string StyleSheet, string address, out string errorMessage)
        {

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
            errorMessage = string.Empty;
            int insertCount = 0;
            StringBuilder builder = new StringBuilder();
            try
            {
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
                        errorMessage += string.Format("第{0}行年月、评价单元不能为空！", j);
                        //Response.Write("<script>alert('第:" + j + "行年月、评价单元名称不能为空')</script>");
                    }
                }

                string cyc = Session["cyc_id"].ToString();
                for (int j = 0; j < DT.Rows.Count; j++)
                {
                    string NY = DT.Rows[j][1].ToString();
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
                    string PJDYMC = DT.Rows[j][0].ToString();
                    PJDYMC = CommonFunctions.ConvertString(PJDYMC);
                    //if (string.IsNullOrEmpty(PJDYMC))
                    //{
                    //    errorMessage += string.Format("数据错误——第{0}行数据评价单元名称与系统不一致！\n", j + 1);
                    //    continue;
                    //}
                    //if (CommonObject.PJDY_ID_Table[PJDYMC] == null)
                    //{
                    //    errorMessage += string.Format("数据异常——第{0}行数据评价单元名称与系统不一致！\n", j + 1);
                    //}

                    string chksql = "select * from pjdysj where ny='" + NY + "' and pjdymc='" + PJDYMC + "' and cyc_id='" + cyc + "'";
                    OracleCommand chkcmd = new OracleCommand(chksql, conn);
                    OracleDataReader chkread = chkcmd.ExecuteReader();
                    while (chkread.Read())
                    {
                        OracleCommand mycom = new OracleCommand("delete pjdysj where ny='" + NY + "' and pjdymc='" + PJDYMC + "' and cyc_id='" + cyc + "'", conn);
                        mycom.ExecuteNonQuery();
                    }


                    //double DTB =  DT.Rows[j][2]);
                    string DYHYMJ = DT.Rows[j][2].ToString();
                    string DYDZCL = DT.Rows[j][3].ToString();
                    string DYKCCL = DT.Rows[j][4].ToString();
                    string YCZS = DT.Rows[j][5].ToString();
                    string PJSTL = DT.Rows[j][6].ToString();
                    string DXYYND = DT.Rows[j][7].ToString();
                    string YJZJS = DT.Rows[j][8].ToString();
                    string SJZJS = DT.Rows[j][9].ToString();
                    string SJKJS = DT.Rows[j][10].ToString();



                    string ZSJ = DT.Rows[j][11].ToString();
                    string LJZSL = DT.Rows[j][12].ToString();
                    string ZQJZJS = DT.Rows[j][13].ToString();
                    string ZQJKJZ = DT.Rows[j][14].ToString();
                    string ZQL = DT.Rows[j][15].ToString();
                    string LJZQL = DT.Rows[j][16].ToString();
                    string CYOUL = DT.Rows[j][17].ToString();
                    string LJCYOUL = DT.Rows[j][18].ToString();
                    string CYL = DT.Rows[j][19].ToString();
                    string LJCYL = DT.Rows[j][20].ToString();
                    string CQL = DT.Rows[j][21].ToString();
                    string LJCQL = DT.Rows[j][22].ToString();
                    string YCLX = DT.Rows[j][23].ToString();
                    YCLX = CommonFunctions.ConvertString(YCLX);
                    if (CommonObject.GetYCLX(cyc) == null || CommonObject.GetYCLX(cyc)[YCLX] == null)
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据油藏类型错误！\n", j + 1);
                        continue;
                    }
                    string XSYP = DT.Rows[j][24].ToString();
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
                    string YJKJS = DT.Rows[j][25].ToString();

                    string DJPJRCY = DT.Rows[j][26].ToString();
                    string NJZHHS = DT.Rows[j][27].ToString();
                    string PJTTZQ = DT.Rows[j][28].ToString();
                    string JDYQB = DT.Rows[j][29].ToString();
                    string KCCLCCCD = DT.Rows[j][30].ToString();
                    string SYKCCL = DT.Rows[j][31].ToString();
                    string DJKZSYKCCL = DT.Rows[j][32].ToString();
                    string JWMD = DT.Rows[j][33].ToString();
                    string SFPJ = DT.Rows[j][34].ToString(); ;

                    string SSYT = "";


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

                    string lhqhl = ""; string nxyhl = ""; string nxyjb = ""; string hljb = ""; string ysdcyl = ""; string mqdcyl = ""; string kfjd = ""; string kffs = ""; string qjzjs = ""; string qjkjs = ""; string cclx = ""; string pjkxd = ""; string djpjrcq = ""; string rcqjb = "";

                    string strSql = " insert into pjdysj values('" + NY + "','" + PJDYMC + "','" + DYHYMJ + "', '" + DYDZCL + "',";
                    strSql += "'" + DYKCCL + "', '" + YCZS + "','" + PJSTL + "','" + DXYYND + "',";
                    strSql += "'" + YJZJS + "','" + SJZJS + "','" + YJKJS + "','" + SJKJS + "',";
                    strSql += "'" + ZSJ + "','" + LJZSL + "','" + ZQJZJS + "','" + ZQJKJZ + "',";
                    strSql += "'" + ZQL + "','" + LJZQL + "','" + CYOUL + "','" + LJCYOUL + "',";
                    strSql += "'" + CYL + "','" + LJCYL + "','" + CQL + "','" + LJCQL + "',";
                    strSql += "'" + DJPJRCY + "','" + NJZHHS + "','" + PJTTZQ + "','" + JDYQB + "',";
                    strSql += "'" + KCCLCCCD + "', '" + SYKCCL + "', '" + DJKZSYKCCL + "','" + JWMD + "',";
                    strSql += "'" + SSYT + "','" + YCLX + "','" + XSYP + "','" + ZJCLF + "',";
                    strSql += "'" + ZJRLF + "','" + ZJDLF + "','" + ZJRYF + "','" + QYWZRF + "',";
                    strSql += "'" + JXZYF + "','" + WHXJXZYF + "','" + CJSJF + "', '" + WHXLF + "',";
                    strSql += "'" + CYRCF + "','" + YQCLF + "','" + QTHSF + "', '" + TRQJHF + "',";
                    strSql += "'" + YSF + "','" + LYF + "','" + QTZJF + "','" + CKGLF + "','" + ZYYQCP + "','" + ZJZH + "','" + QJF + "','" + KTF + "','" + CZCB + "','" + CZCB_MY + "','" + SCCB + "','" + SCCB_MY + "','" + YYCB + "','" + YYCB_MY + "','" + YYXSSR + "', '" + YYXSSJ + "','" + TRQXSSR + "', '" + TRQXSSJ + "', '" + BSCPXSSR + "', '" + BSCPXSSJ + "','" + BSCPSHSR + "', '" + XSSR + "', '" + XSSJ + "','" + YYZYS + "','" + TRQZYS + "', '" + ZYS + "', '" + SHSR + "', '" + LR + "','" + LR_MY + "','" + GSXYJB + "', '" + YQL + "', '" + YQSPL + "','" + DTL + "', '" + YYSPL + "', '" + TRQSPL + "', '" + BSCPCL + "','" + BSCPSPL + "', '" + CZCBJB + "', '" + RCYJB + "', '" + HSJB + "','" + YQBJB + "','" + KCCLCCCDJB + "', '" + SYKCCLCCSD + "', '" + CZCBDLJB + "','" + SFPJ + "','" + QYWZRF_RYF + "','" + YQCLF_RYF + "','" + YXCZCB + "','" + ZJYXCZCB + "','" + GSXYJB_1 + "','" + YXCZCB_MY + "','" + ZJYXCZCB_MY + "','" + cyc + "','" + CJSJF_RYF + "','" + WHXLF_RYF + "','" + YSF_RYF + "','" + TBSYJ + "','" + ZJCLF_1 + "','" + ZJDLF_1 + "','" + WHXZYLWF + "','" + lhqhl + "','" + nxyhl + "','" + nxyjb + "','" + hljb + "','" + ysdcyl + "','" + mqdcyl + "','" + kfjd + "','" + kffs + "','" + qjzjs + "','" + qjkjs + "','" + cclx + "','" + pjkxd + "','" + djpjrcq + "','" + rcqjb + "')";
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
            bool result = false;
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
