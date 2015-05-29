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
    public partial class uploadfile1 : beneDesCYC.core.UI.corePage
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
                string sNewFileName = DateTime.Now.ToString("yyyymmdd") + Session["userName"].ToString();
                postedFile.SaveAs(savePath + @"/" + sNewFileName + sExtension);

                string address = savePath + @"/" + sNewFileName + sExtension;
                GetSheet getsheetx = new GetSheet();
                string StyleSheet = getsheetx.GetExcelSheetNames(address);

                //string msg = LoadData(StyleSheet, address);
                //_return(true, msg, "null");

                string msg = string.Empty;
                bool trueOrfalse = LoadData(StyleSheet, address, out msg);
                _return(trueOrfalse, msg, "null");
            }
        }

        public bool LoadData(string StyleSheet, string address, out string errorMessage)
        {
            errorMessage = "";
            bool result = false;
            StringBuilder builder = new StringBuilder();
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

            //string strConn = "Data Source=orcl; User Id=jilin_xm; Password=jilin_xm";
            //OracleConnection conn = new OracleConnection(strConn);
            //OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);
            OracleConnection conn = DB.CreatConnection();

            conn.Open();
            int countin = 0;
            for (int j = 0; j < DT.Rows.Count; j++)
            {
                if (DT.Rows[j][0].ToString().Trim() != "" && DT.Rows[j][1].ToString().Trim() != "" && DT.Rows[j][2].ToString().Trim() != "")
                {
                    countin = countin + 1;
                }
                else
                {
                    //2013/11/01边敏修改
                    // j = j + 1;
                    //if (builder.Length > 0)
                    //    builder.Append(",");
                    //builder.Append(j.ToString());
                    // Response.Write("<script>alert('第:" + j + "行年月、井号、作业区不能为空')</script>");
                }

            }
            //   string error1 = "";
            //if (builder.Length == 0)
            //    error1 = "0";
            //else
            //    error1 = builder.ToString();
            //2013/11/01边敏修改
            int totalCount = 0;
            int num_update = 0;   //更新条数
            int num_insert = 0;   //插入条数
            //string errorMessage = string.Empty;
            try
            {
                //int isupdate = 1;

                // conn.ConnectionString = ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString;
                string CYC_ID = Session["cyc_id"].ToString();

                for (int j = 0; j < DT.Rows.Count; j++)
                {
                    OracleCommand cmd = new OracleCommand("SP_INSERT_KFSJ", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    #region 从Excel文件中初始化存储过程的参数值

                    string NY = DT.Rows[j][0].ToString();
                    NY = CommonFunctions.ConvertString(NY);
                    if (string.IsNullOrEmpty(NY))
                    {
                        break;
                    }
                    else if (NY.Length != 6)
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据月份错误！\n", j + 2);
                        continue;
                    }
                    else
                        totalCount++;
                    string JH = DT.Rows[j][1].ToString();
                    JH = CommonFunctions.ConvertString(JH);
                    if (CommonObject.DJSJ_JH_ID_Table(NY, CYC_ID)[JH] == null)
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据井号与A2不一致！\n", j + 2);
                        continue;
                    }
                    string ZYQ = DT.Rows[j][2].ToString();
                    if (CommonObject.ZYQ_MC_Table[ZYQ] == null)
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据作业区名称与A2不一致！\n", j + 2);
                        continue;
                    }
                    //else
                    //{
                    //    ZYQ = CommonObject.ZYQ_ID_Table[ZYQ].ToString();
                    //}
                    string QK = DT.Rows[j][3].ToString();
                    QK = CommonFunctions.ConvertString(QK);
                    if (CommonObject.DJSJ_QK_Table(Convert.ToString(Session["month"]))[QK] == null)
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据区块与单井数据不一致！\n", j + 2);
                        continue;
                    }
                    //if (string.IsNullOrEmpty(QK))
                    //{
                    //    if (CommonObject.Area_MC_Table[(CommonObject.JH_Data_Table[JH] as DataRow)["QK"]] != null)
                    //    {
                    //        QK = CommonObject.Area_MC_Table[(CommonObject.JH_Data_Table[JH] as DataRow)["QK"]].ToString();
                    //    }
                    //}
                    //else
                    //{
                    //    if (CommonObject.Area_ID_Table[QK] == null)
                    //    {
                    //        //errorMessage += string.Format("数据异常——第{0}行数据区块名称与A2不一致！\n", j + 1);
                    //    }
                    //}
                    string DJ_ID = ZYQ + "$" + JH;

                    int ZQLC = 0;
                    //int.TryParse(DT.Rows[j][2].ToString(), out ZQLC);
                    string CX = DT.Rows[j][4].ToString();
                    CX = CommonFunctions.ConvertString(CX);
                    decimal CYHD = 0.00M;
                    decimal.TryParse(DT.Rows[j][5].ToString(), out CYHD);
                    decimal ZQL = 0.000M;
                    decimal.TryParse(DT.Rows[j][6].ToString(), out ZQL);
                    decimal ZSL = 0.000M;
                    decimal.TryParse(DT.Rows[j][7].ToString(), out ZSL);
                    decimal CYL = 0.000M;
                    decimal.TryParse(DT.Rows[j][8].ToString(), out CYL);
                    decimal CSL = 0.000M;
                    decimal.TryParse(DT.Rows[j][9].ToString(), out CSL);
                    decimal CYAOL = 0.00M;
                    decimal.TryParse(DT.Rows[j][10].ToString(), out CYAOL);
                    decimal SCSJ = 0.00M;
                    decimal.TryParse(DT.Rows[j][11].ToString(), out SCSJ);
                    decimal JKCYL = 0.000M;
                    decimal.TryParse(DT.Rows[j][12].ToString(), out JKCYL);
                    decimal JKCYOUL = 0.000M;
                    decimal.TryParse(DT.Rows[j][13].ToString(), out JKCYOUL);
                    decimal HSCYL = 0.000M;
                    decimal.TryParse(DT.Rows[j][14].ToString(), out HSCYL);
                    decimal HS = 0.0000M;
                    decimal.TryParse(DT.Rows[j][15].ToString(), out HS);
                    decimal LJCYL = 0.0000M;
                    decimal.TryParse(DT.Rows[j][16].ToString(), out LJCYL);
                    decimal LJZSL = 0.000M;
                    decimal.TryParse(DT.Rows[j][17].ToString(), out LJZSL);
                    decimal LJZQL = 0.0000M;
                    decimal.TryParse(DT.Rows[j][18].ToString(), out LJZQL);
                    string BZ = DT.Rows[j][19].ToString();
                    BZ = CommonFunctions.ConvertString(BZ);
                    if (BZ.Length > 100)
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据备注信息太长！\n", j + 1);
                        continue;
                    }

                    //decimal JKCQL = 0.0000M;
                    //decimal.TryParse(DT.Rows[j][15].ToString(), out JKCQL);
                    ////string JKCQL = DT.Rows[j][11].ToString();
                    //decimal HSCQL = 0.0000M;
                    //decimal.TryParse(DT.Rows[j][17].ToString(), out HSCQL);
                    ////string HSCQL = DT.Rows[j][12].ToString();
                    //decimal LJCQL = 0.0000M;
                    //decimal.TryParse(DT.Rows[j][20].ToString(), out LJCQL);
                    ////string LJCQL = DT.Rows[j][13].ToString();




                    //string DZCSDM = DT.Rows[j][23].ToString();
                    //string GYCSDM = DT.Rows[j][24].ToString();

                    #region (Date:13.11.03;Anonation:wj)为空的列
                    //string ZQQSBS = "";
                    //string NBHSCYL = "";
                    //string NBHSCQL = "";
                    //string YYSPL = "";
                    //string TRQSPL = "";
                    //string NYQB = "";
                    //string LJYQB = "";
                    //string DJRCY = "";
                    //string YQL = "";
                    //string NBHSYQL = "";
                    //string YQSPL = "";
                    //string DTL = "";                
                    //string FXYYDM = "";
                    //string ZJCLF = "";
                    //string ZJRLF = "";
                    //string ZJDLF = "";
                    //string ZJRYF = "";
                    //string QYWZRF = "";
                    //string JXZYF = "";
                    //string WHXJXZYF = "";
                    //string CJSJF = "";
                    //string WHXLF = "";
                    //string CYRCF = "";
                    //string YQCLF = "";
                    //string QTHSF = "";
                    //string TRQJHF = "";
                    //string YSF = "";
                    //string LYF = "";
                    //string QTZJF = "";
                    //string CKGLF = "";
                    //string ZYYQCP = "";
                    //string ZJZH = "";
                    //string QJF = "";
                    //string KTF = "";
                    //string ZDYXF = "";
                    //string DYQZDYXF = "";
                    //string HSCZCB = "";
                    //string DYQHSCZCB = "";
                    //string CZCB = "";
                    //string DYQCZCB = "";
                    //string SCCB = "";
                    //string DYQSCCB = "";
                    //string YYCB = "";
                    //string DYQYYCB = "";
                    //string YYXSSR = "";
                    //string YYXSSJ = "";
                    //string TRQXSSR = "";
                    //string TRQXSSJ = "";
                    //string BSCPCL = "";
                    //string BSCPSPL = "";
                    //string BSCPXSSR = "";
                    //string BSCPXSSJ = "";
                    //string BSCPSHSR = "";
                    //string XSSR = "";
                    //string XSSJ = "";
                    //string YYZYS = "";
                    //string TRQZYS = "";
                    //string ZYS = "";
                    //string SHSR = "";
                    //string LR = "";
                    //string GSXYJB = "";
                    //string ZDYXYJB = "";
                    //string QYWZRF_RYF = "";
                    //string YQCLF_RYF = "";
                    //string YXCZCB = "";
                    //string ZJYXCZCB = "";
                    //string GSXYJB_1 = "";
                    #endregion


                    #endregion

                    #region (Date:13.11.03, Add:wj)
                    //string sql = "insert into KFSJ(ny,dj_id,jh,zyq,qk,zqlc,zqqsbs,cx,cyhd,zql,zsl,cyl,csl,cyaol,scsj,jkcyl,jkcyoul,hscyl,jkcql,hscql,hs,nbhscyl,nbhscql,yyspl,trqspl,ljcyl,ljcql,ljzsl,ljzql,nyqb," +
                    //                "ljyqb,djrcy,yql,nbhsyql,yqspl,dtl,dzcsdm,gycsdm,fxyydm,zjclf,zjrlf,zjdlf,zjryf,qywzrf,jxzyf,whxjxzyf,cjsjf,whxlf,cyrcf,yqclf,qthsf,trqjhf,ysf,lyf,qtzjf,ckglf,zyyqcp," +
                    //                "zjzh,qjf,ktf,zdyxf,dyqzdyxf,hsczcb,dyqhsczcb,czcb,dyqczcb,sccb,dyqsccb,yycb,dyqyycb,yyxssr,yyxssj,trqxssr,trqxssj,bscpcl,bscpspl,bscpxssr,bscpxssj,bscpshsr,xssr,xssj," +
                    //                "yyzys,trqzys,zys,shsr,lr,gsxyjb,zdyxyjb,bz,qywzrf_ryf,yqclf_ryf,yxczcb,zjyxczcb,gsxyjb_1,cyc_id) " +
                    //                "values('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}',{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25}," +
                    //                "{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},'{36}','{37}','{38}',{39},{40},{41},{42},{43},{44},{45},{46},{47},{48},{49}," +
                    //                "{50},{51},{52},{53},{54},{55},{56},{57},{58},{59},{60},{61},{62},{63},{64},{65},{66},{67},{68},{69},{70},{71},{72}," +
                    //                "{73},{74},{75},{76},{77},{78},{79},{80},{81},{82},{83},{84},{85},{86},{87},'{88}',{89},{90},{91},{92},{93},'{94}')";
                    //sql = string.Format(sql, NY, DJ_ID, JH, ZYQ, QK, ZQLC, ZQQSBS, CX, CYHD, ZQL, ZSL, CYL, CSL, CYAOL, SCSJ, JKCYL, JKCYOUL, HSCYL, JKCQL, HSCQL, HS, NBHSCYL, NBHSCQL, YYSPL, TRQSPL, LJCYL,
                    //                LJCQL, LJZSL, LJZQL, NYQB,LJYQB,DJRCY,YQL,NBHSYQL,YQSPL, DTL,DZCSDM,GYCSDM,FXYYDM,ZJCLF,ZJRLF,ZJDLF,ZJRYF,QYWZRF,JXZYF,WHXJXZYF,CJSJF,WHXLF,CYRCF,YQCLF, 
                    //                QTHSF,TRQJHF, YSF,LYF,QTZJF,CKGLF,ZYYQCP,ZJZH,QJF,KTF,ZDYXF,DYQZDYXF,HSCZCB,DYQHSCZCB,CZCB,DYQCZCB,SCCB,DYQSCCB,YYCB,DYQYYCB,YYXSSR,YYXSSJ,TRQXSSR,
                    //                TRQXSSJ,BSCPCL,BSCPSPL,BSCPXSSR,BSCPXSSJ,BSCPSHSR,XSSR,XSSJ,YYZYS,TRQZYS,ZYS,SHSR,LR,GSXYJB,ZDYXYJB,BZ,QYWZRF_RYF,YQCLF_RYF,YXCZCB,ZJYXCZCB,GSXYJB_1, CYC_ID);
                    ////string strSql = "insert into kfsj(ny,dj_id,jh,zyq,qk,zqlc,zqqsbs,cx,cyhd,zql,zsl,cyl,csl,cyaol,scsj,jkcyl,jkcyoul,hscyl,jkcql,hscql,hs,nbhscyl,nbhscql,yyspl,trqspl,ljcyl,ljcql,ljzsl,ljzql,nyqb,ljyqb,djrcy,yql,nbhsyql,yqspl,dtl,dzcsdm,gycsdm,fxyydm,zjclf,zjrlf,zjdlf,zjryf,qywzrf,jxzyf,whxjxzyf,cjsjf,whxlf,cyrcf,yqclf,qthsf,trqjhf,ysf,lyf,qtzjf,ckglf,zyyqcp,zjzh,qjf,ktf,zdyxf,dyqzdyxf,hsczcb,dyqhsczcb,czcb,dyqczcb,sccb,dyqsccb,yycb,dyqyycb,yyxssr,yyxssj,trqxssr,trqxssj,bscpcl,bscpspl,bscpxssr,bscpxssj,bscpshsr,xssr,xssj,yyzys,trqzys,zys,shsr,lr,gsxyjb,zdyxyjb,bz,qywzrf_ryf,yqclf_ryf,yxczcb,zjyxczcb,gsxyjb_1,cyc_id)";
                    ////strSql = strSql + " values('" + NY + "','" + DJ_ID + "','" + JH + "','" + ZYQ + "','" + QK + "','" + ZQLC + "','" + ZQQSBS + "','" + CX + "','" + CYHD + "','" + ZQL + "','" + ZSL + "','" + CYL + "','" + CSL + "','" + CYAOL + "','" + SCSJ + "','" + JKCYL + "','" + JKCYOUL + "','" + HSCYL + "','" + JKCQL + "','" + HSCQL + "','" + HS + "','" + NBHSCYL + "','" + NBHSCQL + "','" + YYSPL + "','" + TRQSPL + "','" + LJCYL + "','" + LJCQL + "','" + LJZSL + "','" + LJZQL + "','" + NYQB + "','" + LJYQB + "','" + DJRCY + "','" + YQL + "','" + NBHSYQL + "','" + YQSPL + "','" + DTL + "','" + DZCSDM + "','" + GYCSDM + "','" + FXYYDM + "','" + ZJCLF + "','" + ZJRLF + "','" + ZJDLF + "','" + ZJRYF + "','" + QYWZRF + "','" + JXZYF + "','" + WHXJXZYF + "','" + CJSJF + "','" + WHXLF + "','" + CYRCF + "','" + YQCLF + "','" + QTHSF + "','" + TRQJHF + "','" + YSF + "','" + LYF + "','" + QTZJF + "','" + CKGLF + "','" + ZYYQCP + "','" + ZJZH + "','" + QJF + "','" + KTF + "','" + ZDYXF + "','" + DYQZDYXF + "','" + HSCZCB + "','" + DYQHSCZCB + "','" + CZCB + "','" + DYQCZCB + "','" + SCCB + "','" + DYQSCCB + "','" + YYCB + "','" + DYQYYCB + "','" + YYXSSR + "','" + YYXSSJ + "','" + TRQXSSR + "','" + TRQXSSJ + "','" + BSCPCL + "','" + BSCPSPL + "','" + BSCPXSSR + "','" + BSCPXSSJ + "','" + BSCPSHSR + "','" + XSSR + "','" + XSSJ + "','" + YYZYS + "','" + TRQZYS + "','" + ZYS + "','" + SHSR + "','" + LR + "','" + GSXYJB + "','" + ZDYXYJB + "','" + BZ + "','" + QYWZRF_RYF + "','" + YQCLF_RYF + "','" + YXCZCB + "','" + ZJYXCZCB + "','" + GSXYJB_1 + "','" + CYC_ID + "')";
                    //OracleCommand mycomm = new OracleCommand(sql, conn);
                    //int i = mycomm.ExecuteNonQuery();
                    //i = i + 1;

                    //string sql = "insert into jilincyc.KFSJ(ny,dj_id,jh,zyq,qk,zqlc,cx,cyhd,scsj,jkcyl,jkcyoul,hscyl," +
                    //                "ljcyl,jkcql,hscql,ljcql,hs,zsl,ljzsl,zql,ljzql,cyl,csl,cyaol,dzcsdm,gycsdm,bz,cyc_id) " +
                    //                "values('{0}','{1}','{2}','{3}','{4}',{5},'{6}',{7},{8},{9},{10},{11}," +
                    //                "{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},'{24}','{25}','{26}','{27}')";
                    //sql = string.Format(sql, NY, DJ_ID, JH, ZYQ, QK, ZQLC, CX, CYHD, SCSJ, JKCYL, JKCYOUL, HSCYL,
                    //                    LJCYL, JKCQL, HSCQL, LJCQL, HS, ZSL, LJZSL, ZQL, LJZQL, CYL, CSL, CYAOL, DZCSDM, GYCSDM, BZ, CYC_ID);
                    ////string strSql = "insert into kfsj(ny,dj_id,jh,zyq,qk,zqlc,zqqsbs,cx,cyhd,zql,zsl,cyl,csl,cyaol,scsj,jkcyl,jkcyoul,hscyl,jkcql,hscql,hs,nbhscyl,nbhscql,yyspl,trqspl,ljcyl,ljcql,ljzsl,ljzql,nyqb,ljyqb,djrcy,yql,nbhsyql,yqspl,dtl,dzcsdm,gycsdm,fxyydm,zjclf,zjrlf,zjdlf,zjryf,qywzrf,jxzyf,whxjxzyf,cjsjf,whxlf,cyrcf,yqclf,qthsf,trqjhf,ysf,lyf,qtzjf,ckglf,zyyqcp,zjzh,qjf,ktf,zdyxf,dyqzdyxf,hsczcb,dyqhsczcb,czcb,dyqczcb,sccb,dyqsccb,yycb,dyqyycb,yyxssr,yyxssj,trqxssr,trqxssj,bscpcl,bscpspl,bscpxssr,bscpxssj,bscpshsr,xssr,xssj,yyzys,trqzys,zys,shsr,lr,gsxyjb,zdyxyjb,bz,qywzrf_ryf,yqclf_ryf,yxczcb,zjyxczcb,gsxyjb_1,cyc_id)";
                    ////strSql = strSql + " values('" + NY + "','" + DJ_ID + "','" + JH + "','" + ZYQ + "','" + QK + "','" + ZQLC + "','" + ZQQSBS + "','" + CX + "','" + CYHD + "','" + ZQL + "','" + ZSL + "','" + CYL + "','" + CSL + "','" + CYAOL + "','" + SCSJ + "','" + JKCYL + "','" + JKCYOUL + "','" + HSCYL + "','" + JKCQL + "','" + HSCQL + "','" + HS + "','" + NBHSCYL + "','" + NBHSCQL + "','" + YYSPL + "','" + TRQSPL + "','" + LJCYL + "','" + LJCQL + "','" + LJZSL + "','" + LJZQL + "','" + NYQB + "','" + LJYQB + "','" + DJRCY + "','" + YQL + "','" + NBHSYQL + "','" + YQSPL + "','" + DTL + "','" + DZCSDM + "','" + GYCSDM + "','" + FXYYDM + "','" + ZJCLF + "','" + ZJRLF + "','" + ZJDLF + "','" + ZJRYF + "','" + QYWZRF + "','" + JXZYF + "','" + WHXJXZYF + "','" + CJSJF + "','" + WHXLF + "','" + CYRCF + "','" + YQCLF + "','" + QTHSF + "','" + TRQJHF + "','" + YSF + "','" + LYF + "','" + QTZJF + "','" + CKGLF + "','" + ZYYQCP + "','" + ZJZH + "','" + QJF + "','" + KTF + "','" + ZDYXF + "','" + DYQZDYXF + "','" + HSCZCB + "','" + DYQHSCZCB + "','" + CZCB + "','" + DYQCZCB + "','" + SCCB + "','" + DYQSCCB + "','" + YYCB + "','" + DYQYYCB + "','" + YYXSSR + "','" + YYXSSJ + "','" + TRQXSSR + "','" + TRQXSSJ + "','" + BSCPCL + "','" + BSCPSPL + "','" + BSCPXSSR + "','" + BSCPXSSJ + "','" + BSCPSHSR + "','" + XSSR + "','" + XSSJ + "','" + YYZYS + "','" + TRQZYS + "','" + ZYS + "','" + SHSR + "','" + LR + "','" + GSXYJB + "','" + ZDYXYJB + "','" + BZ + "','" + QYWZRF_RYF + "','" + YQCLF_RYF + "','" + YXCZCB + "','" + ZJYXCZCB + "','" + GSXYJB_1 + "','" + CYC_ID + "')";
                    //OracleCommand mycomm = new OracleCommand(sql, conn);
                    //int i = mycomm.ExecuteNonQuery();
                    //i = i + 1;
                    #endregion
                    //2013/11/01边敏修改
                    //OracleCommand mycom = new OracleCommand("delete kfsj where ny='" + NY + "' and dj_id='" + DJ_ID + "' and jh='" + JH + "' and zyq='" + ZYQ + "' and qk='" + QK + "' and cyc_id='" + CYC + "'", conn);
                    //mycom.ExecuteNonQuery();

                    cmd.Parameters.Add(new OracleParameter("vrtn", SqlDbType.Int));
                    cmd.Parameters["vrtn"].Direction = ParameterDirection.Output;
                    #region 存储过程参数赋值


                    cmd.Parameters.AddWithValue("vNY", NY);
                    cmd.Parameters.AddWithValue("vDJ_ID", DJ_ID);
                    cmd.Parameters.AddWithValue("vJH", JH);
                    cmd.Parameters.AddWithValue("vZYQ", ZYQ);
                    cmd.Parameters.AddWithValue("vQK", CommonFunctions.ConvertDBString(QK));
                    cmd.Parameters.AddWithValue("vZQLC", ZQLC);
                    cmd.Parameters.AddWithValue("vCX", CommonFunctions.ConvertDBString(CX));
                    cmd.Parameters.AddWithValue("vCYHD", CYHD);
                    cmd.Parameters.AddWithValue("vZQL", ZQL);
                    cmd.Parameters.AddWithValue("vZSL", ZSL);
                    cmd.Parameters.AddWithValue("vCYL", CYL);
                    cmd.Parameters.AddWithValue("vCSL", CSL);
                    cmd.Parameters.AddWithValue("vCYAOL", CYAOL);
                    cmd.Parameters.AddWithValue("vSCSJ", SCSJ);
                    cmd.Parameters.AddWithValue("vJKCYL", JKCYL);
                    cmd.Parameters.AddWithValue("vJKCYOUL", JKCYOUL);
                    cmd.Parameters.AddWithValue("vHSCYL", HSCYL);
                    cmd.Parameters.AddWithValue("vHS", HS);
                    cmd.Parameters.AddWithValue("vLJCYL", LJCYL);
                    cmd.Parameters.AddWithValue("vLJZSL", LJZSL);
                    cmd.Parameters.AddWithValue("vLJZQL", LJZQL);
                    //cmd.Parameters.AddWithValue("vDZCSDM", DZCSDM);
                    //cmd.Parameters.AddWithValue("vGYCSDM", GYCSDM);
                    cmd.Parameters.AddWithValue("vBZ", CommonFunctions.ConvertDBString(BZ));
                    cmd.Parameters.AddWithValue("vCYC_ID", CYC_ID);

                    #region (Date:13.11.03;Anonation:wj)为空的列
                    //cmd.Parameters.AddWithValue("vZQQSBS", ZQQSBS);
                    //cmd.Parameters.AddWithValue("vNBHSCYL", NBHSCYL);
                    //cmd.Parameters.AddWithValue("vNBHSCQL", NBHSCQL);
                    //cmd.Parameters.AddWithValue("vNYQB",NYQB );
                    //cmd.Parameters.AddWithValue("vLJYQB",LJYQB );
                    //cmd.Parameters.AddWithValue("vDJRCY",DJRCY );
                    //cmd.Parameters.AddWithValue("vYQL",YQL );
                    //cmd.Parameters.AddWithValue("vNBHSYQL",NBHSYQL );
                    //cmd.Parameters.AddWithValue("vYQSPL",YQSPL );
                    //cmd.Parameters.AddWithValue("vDTL",DTL );
                    //cmd.Parameters.AddWithValue("vFXYYDM",FXYYDM );
                    //cmd.Parameters.AddWithValue("vZJCLF",ZJCLF );
                    //cmd.Parameters.AddWithValue("vZJRLF",ZJRLF);
                    //cmd.Parameters.AddWithValue("vZJDLF",ZJDLF);
                    //cmd.Parameters.AddWithValue("vZJRYF",ZJRYF );
                    //cmd.Parameters.AddWithValue("vQYWZRF",QYWZRF);
                    //cmd.Parameters.AddWithValue("vJXZYF",JXZYF );
                    //cmd.Parameters.AddWithValue("vWHXJXZYF",WHXJXZYF );
                    //cmd.Parameters.AddWithValue("vCJSJF",CJSJF );
                    //cmd.Parameters.AddWithValue("vWHXLF",WHXLF );
                    //cmd.Parameters.AddWithValue("vCYRCF",CYRCF );
                    //cmd.Parameters.AddWithValue("vYQCLF",YQCLF );
                    //cmd.Parameters.AddWithValue("vQTHSF",QTHSF );
                    //cmd.Parameters.AddWithValue("vTRQJHF",TRQJHF );
                    //cmd.Parameters.AddWithValue("vYSF",YSF);
                    //cmd.Parameters.AddWithValue("vLYF",LYF );
                    //cmd.Parameters.AddWithValue("vQTZJF ",QTZJF  );
                    //cmd.Parameters.AddWithValue("vCKGLF",CKGLF);
                    //cmd.Parameters.AddWithValue("vZYYQCP",ZYYQCP);
                    //cmd.Parameters.AddWithValue("vZJZH",ZJZH );
                    //cmd.Parameters.AddWithValue("vQJF",QJF );
                    //cmd.Parameters.AddWithValue("vKTF",KTF );
                    //cmd.Parameters.AddWithValue("vZDYXF",ZDYXF);
                    //cmd.Parameters.AddWithValue("vDYQZDYXF",DYQZDYXF );
                    //cmd.Parameters.AddWithValue("vHSCZCB",HSCZCB );
                    //cmd.Parameters.AddWithValue("vDYQHSCZCB",DYQHSCZCB );
                    //cmd.Parameters.AddWithValue("vCZCB",CZCB );
                    //cmd.Parameters.AddWithValue("vDYQCZCB",DYQCZCB );
                    //cmd.Parameters.AddWithValue("vSCCB",SCCB );
                    //cmd.Parameters.AddWithValue("vDYQSCCB",DYQSCCB );
                    //cmd.Parameters.AddWithValue("vYYCB",YYCB );
                    //cmd.Parameters.AddWithValue("vDYQYYCB",DYQYYCB );
                    //cmd.Parameters.AddWithValue("vYYXSSR ",YYXSSR  );
                    //cmd.Parameters.AddWithValue("vYYXSSJ", YYXSSJ);
                    //cmd.Parameters.AddWithValue("vTRQXSSR",TRQXSSR );
                    //cmd.Parameters.AddWithValue("vTRQXSSJ",TRQXSSJ );
                    //cmd.Parameters.AddWithValue("vBSCPCL",BSCPCL );
                    //cmd.Parameters.AddWithValue("vBSCPSPL",BSCPSPL );
                    //cmd.Parameters.AddWithValue("vBSCPXSSR",BSCPXSSR );
                    //cmd.Parameters.AddWithValue("vBSCPXSSJ",BSCPXSSJ );
                    //cmd.Parameters.AddWithValue("vBSCPSHSR",BSCPSHSR );
                    //cmd.Parameters.AddWithValue("vXSSR",XSSR );
                    //cmd.Parameters.AddWithValue("vXSSJ",XSSJ);
                    //cmd.Parameters.AddWithValue("vYYZYS",YYZYS );
                    //cmd.Parameters.AddWithValue("vTRQZYS",TRQZYS );
                    //cmd.Parameters.AddWithValue("vZYS",ZYS);
                    //cmd.Parameters.AddWithValue("vSHSR",SHSR );
                    //cmd.Parameters.AddWithValue("vLR",LR );
                    //cmd.Parameters.AddWithValue("vGSXYJB",GSXYJB );
                    //cmd.Parameters.AddWithValue("vZDYXYJB",ZDYXYJB );
                    //cmd.Parameters.AddWithValue("vYYSPL", YYSPL);
                    //cmd.Parameters.AddWithValue("vTRQSPL", TRQSPL);	            
                    //cmd.Parameters.AddWithValue("vQYWZRF_RYF",QYWZRF_RYF );
                    //cmd.Parameters.AddWithValue("vYQCLF_RYF", YQCLF_RYF); //cmd.Parameters.AddWithValue("vYQCLF_RFY", YQCLF_RYF);
                    // cmd.Parameters.AddWithValue("vYXCZCB",YXCZCB );
                    //cmd.Parameters.AddWithValue("vZJYXCZCB",ZJYXCZCB);
                    //cmd.Parameters.AddWithValue("vGSXYJB_1",GSXYJB_1);
                    //cmd.Parameters.AddWithValue("@CJSJF_RYF",CJSJF_RYF );
                    //cmd.Parameters.AddWithValue("@WHXLF_RYF",WHXLF_RYF );
                    //cmd.Parameters.AddWithValue("@YSF_RYF",YSF_RYF );
                    //cmd.Parameters.AddWithValue("@TBSYJ",TBSYJ );
                    //cmd.Parameters.AddWithValue("@ZJCLF_1 ",ZJCLF_1  );
                    //cmd.Parameters.AddWithValue("@ZJDLF_1",ZJDLF_1 );
                    //cmd.Parameters.AddWithValue("@WHXZYLWF",WHXZYLWF );
                    #endregion
                    // cmd.Parameters.Add("vIsUpdate", SqlDbType.Int).Value = visupdate;
                    #endregion


                    try
                    {
                        cmd.ExecuteNonQuery();
                        if (cmd.Parameters["vrtn"].Value == null || string.IsNullOrEmpty(cmd.Parameters["vrtn"].Value.ToString()))
                            continue;
                        int vrtn = (int)cmd.Parameters["vrtn"].Value;
                        if (vrtn == 1)     //result==1为插入操作,result==2为更新操作


                            num_insert++;
                        if (vrtn == 2)
                            num_update++;
                        if (vrtn == 3)
                        {
                            if (builder.Length > 0)
                                builder.Append(",");
                            builder.Append("" + (Convert.ToInt32(j.ToString()) + 1));

                        }
                    }
                    catch
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据导入异常！\n", j + 1);
                    }
                }
            }
            catch (OracleException aa)
            {
                errorMessage += string.Format("系统错误——{0}\n", aa.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            string error = "";
            if (num_insert + num_update == totalCount)
            {
                result = true;
                if (string.IsNullOrEmpty(errorMessage))
                {
                    errorMessage = string.Format("数据导入成功，共导入数据{0}条！", num_insert + num_update);
                }
                else
                {
                    errorMessage = string.Format("{0}条数据全部导入，但存在异常数据。\n异常信息：\n{1}", num_insert + num_update, errorMessage);
                }
            }
            else
            {
                if (builder.Length == 0)
                {
                    errorMessage = string.Format("总共{0}条数据，导入{1}条！\n异常信息：\n{2}", DT.Rows.Count, num_insert + num_update, errorMessage);
                }
                else
                {
                    error = builder.ToString();
                    errorMessage = string.Format("总共{0}条数据，导入{1}条,第{2}条数据SQL执行异常！\n异常信息：\n{3}", DT.Rows.Count, num_insert + num_update, error, errorMessage);
                }

            }
            return result;
        }
    }
}
