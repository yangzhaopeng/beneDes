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
    public partial class exceluploadqj : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            fileUpload();
        }

        public void fileUpload()
        {
            HttpPostedFile postedFile = Request.Files["UPLOADFILE"];
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

            //LoadData("单井基础信息", address,out errorMessage);

            string msg = string.Empty;
            bool trueOrfalse = LoadData("单井基础信息", address, out msg);
            _return(trueOrfalse, msg, "null");

            //if (postedFile != null)
            //{ _return(true, "上传成功！", "null"); }
            //else
            //{ _return(false, "上传失败！", "null"); }

        }

        public bool LoadData(string StyleSheet, string address, out string errorMessage)
        {
            errorMessage = "";
            bool result = false;

            int count = 0;
            StringBuilder builder = new StringBuilder();
            string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" + address + ";Extended Properties=Excel 8.0";
            if (address.EndsWith("xls", StringComparison.InvariantCultureIgnoreCase))
            {
                strCon = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                @"Data Source=" + address + ";" + "Extended Properties=\"Excel 8.0;HDR=YES\"";
            }
            else if (address.EndsWith("xlsx", StringComparison.InvariantCultureIgnoreCase))
            {
                strCon = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                    @"Data Source=" + address + ";" +
                    "Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
            }

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
            //OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);
            //OracleConnection conn = new OracleConnection(strConn);
            //SqlHelper conn = new SqlHelper();
            //OracleConnection con = conn.GetConn();
            OracleConnection conn = DB.CreatConnection();

            conn.Open();
            int countin = 0;
            //判断主键是否存在
            for (int j = 0; j < DT.Rows.Count; j++)
            {

                if (DT.Rows[j][0].ToString().Trim() != "" && DT.Rows[j][1].ToString().Trim() != "" && DT.Rows[j][2].ToString().Trim() != "")
                {
                    countin = countin + 1;
                }
                else
                {

                }
            }


            int num_update = 0;   //更新条数
            int num_insert = 0;   //插入条数
            try
            {
                //int visupdate = 1; 
                // conn.ConnectionString = ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString;

                //string errorMessage = string.Empty;
                for (int j = 1; j < DT.Rows.Count; j++)
                {

                    OracleCommand cmd = new OracleCommand("SP_INSERT_QJSJ", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    string NY = DT.Rows[j][0].ToString();
                    NY = CommonFunctions.ConvertString(NY);
                    if (string.IsNullOrEmpty(NY))
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据缺少年月信息！\n", j + 1);
                        break;
                    }
                    string JH = DT.Rows[j][1].ToString();
                    JH = CommonFunctions.ConvertString(JH);
                    if (string.IsNullOrEmpty(JH))
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据缺少井号信息！\n", j + 1);
                        break;
                    }
                    //if (CommonObject.JH_ID_Table[JH] == null)
                    //{
                    //    errorMessage += string.Format("数据错误——第{0}行数据井号与A2不一致！\n", j);
                    //    continue;
                    //}

                    string ZYQ = DT.Rows[j][2].ToString();
                    ZYQ = CommonFunctions.ConvertString(ZYQ);
                    if (CommonObject.ZYQ_ID_Table[ZYQ] == null)
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据作业区名称与A2不一致！\n", j + 1);
                        continue;
                    }
                    else
                    {
                        ZYQ = CommonObject.ZYQ_ID_Table[ZYQ].ToString();
                    }
                    string DJ = ZYQ + "$" + JH;

                    string CYC_ID = Session["cqc_id"].ToString();

                    //主键字段为空则不添加  
                    if (NY == "" || ZYQ == "" || JH == "")
                    {
                        break;
                    }


                    string JQZ = DT.Rows[j][3].ToString();
                    JQZ = CommonFunctions.ConvertString(JQZ);
                    string JQZZ = DT.Rows[j][4].ToString();
                    JQZZ = CommonFunctions.ConvertString(JQZZ);
                    string LHZ = "";
                    string JHC = DT.Rows[j][5].ToString();
                    JHC = CommonFunctions.ConvertString(JHC);
                    string QK = DT.Rows[j][6].ToString();
                    QK = CommonFunctions.ConvertString(QK);
                    if (string.IsNullOrEmpty(QK))
                    {
                        //if (CommonObject.Area_MC_Table[(CommonObject.JH_Data_Table[JH] as DataRow)["QK"]] != null)
                        //{
                        //    QK = CommonObject.Area_MC_Table[(CommonObject.JH_Data_Table[JH] as DataRow)["QK"]].ToString();
                        //}
                        errorMessage += string.Format("数据错误——第{0}行数据区块名称为空！\n", j + 1);
                    }
                    else
                    {
                        if (CommonObject.RushLocalAreaInfo(NY, CYC_ID)[QK] == null)
                        {
                            errorMessage += string.Format("数据错误——第{0}行块名称在区块数据表中不存在！\n", j + 1);
                        }
                    }
                    //else
                    //{
                    //    if (CommonObject.Area_ID_Table[QK] == null)
                    //    {
                    //        errorMessage += string.Format("数据异常——第{0}行数据区块名称与A2不一致！\n", j + 1);
                    //    }
                    //}





                    string JXName = DT.Rows[j][7].ToString();
                    JXName = CommonFunctions.ConvertString(JXName);
                    int JX = 1;
                    if (JXName == "直井")
                        JX = 1;
                    else
                        JX = 2;
                    string XSQD = DT.Rows[j][8].ToString();
                    XSQD = CommonFunctions.ConvertString(XSQD);
                    string YQLX = "Y003";
                    string XSYP = string.Empty;

                    XSYP = CommonFunctions.ConvertString(XSQD);
                    if (CommonObject.XSYP_DM_Table[XSYP] == null)
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据销售油品错误！\n", j + 1);
                        continue;
                    }
                    else
                    {
                        XSYP = CommonObject.XSYP_DM_Table[XSYP].ToString();
                    }

                    //string PJDY = DT.Rows[j][9].ToString();
                    string SSYT = DT.Rows[j][9].ToString();
                    SSYT = CommonFunctions.ConvertString(SSYT);
                    string YCLX = DT.Rows[j][10].ToString();
                    YCLX = CommonFunctions.ConvertString(YCLX);

                    string JB = DT.Rows[j][11].ToString();//生产状态
                    JB = CommonFunctions.ConvertString(JB);
                    if (CommonObject.JB_DM_Table[JB] == null)
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据井别错误！\n", j + 1);
                        continue;
                    }
                    else
                    {
                        JB = CommonObject.JB_DM_Table[JB].ToString();
                    }

                    string DJZY = DT.Rows[j][12].ToString();  //是否增压
                    DJZY = CommonFunctions.ConvertString(DJZY);
                    if (DJZY == "是")
                    {
                        DJZY = "1";
                    }
                    else
                    {
                        DJZY = "0";
                    }
                    string SFHZ = DT.Rows[j][13].ToString();   //是否回注
                    SFHZ = CommonFunctions.ConvertString(SFHZ);
                    if (SFHZ == "是")
                    {
                        SFHZ = "1";
                    }
                    else
                    {
                        SFHZ = "0";
                    }
                    string TCRQ = DT.Rows[j][14].ToString();  //投产日期
                    TCRQ = CommonFunctions.ConvertString(TCRQ);
                    //评价单元录入
                    string PJDY = DT.Rows[j][15].ToString();
                    PJDY = CommonFunctions.ConvertString(PJDY);
                    //if (string.IsNullOrEmpty(PJDY))
                    //{
                    //    PJDY = CommonObject.PJDY_MC_Table[(CommonObject.JH_Data_Table[JH] as DataRow)["PJDY"]].ToString();
                    //}
                    //else
                    //{
                    //    if (CommonObject.PJDY_ID_Table[PJDY] == null)
                    //    {
                    //        errorMessage += string.Format("数据错误——第{0}行数据评价单元名称与系统不一致！\n", j + 1);
                    //        continue;
                    //    }
                    //}
                    if (string.IsNullOrEmpty(PJDY))
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据评价单元名称为空！\n", j + 1);
                    }
                    else if (CommonObject.RushLocalPJDYInfo(NY, CYC_ID)[PJDY] == null)
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据评价单元名称与系统不一致！\n", j + 1);
                        continue;
                    }

                    cmd.Parameters.Add(new OracleParameter("vrtn", SqlDbType.Int));
                    cmd.Parameters["vrtn"].Direction = ParameterDirection.Output;
                    #region 存储过程添加值

                    cmd.Parameters.AddWithValue("vNY", NY);
                    cmd.Parameters.AddWithValue("vDJ_ID", DJ);
                    cmd.Parameters.AddWithValue("vJH", JH);
                    cmd.Parameters.AddWithValue("vZYQ", ZYQ);
                    cmd.Parameters.AddWithValue("vQK", CommonFunctions.ConvertDBString(QK));
                    cmd.Parameters.AddWithValue("vJQZ", CommonFunctions.ConvertDBString(JQZ));
                    cmd.Parameters.AddWithValue("vJQZZ", CommonFunctions.ConvertDBString(JQZZ));
                    cmd.Parameters.AddWithValue("vLHZ", CommonFunctions.ConvertDBString(LHZ));
                    cmd.Parameters.AddWithValue("vJHC", CommonFunctions.ConvertDBString(JHC));
                    cmd.Parameters.AddWithValue("vYQLX", CommonFunctions.ConvertDBString(YQLX));
                    cmd.Parameters.AddWithValue("vXSYP", CommonFunctions.ConvertDBString(XSYP));
                    cmd.Parameters.AddWithValue("vJX", JX);
                    cmd.Parameters.AddWithValue("vJB", CommonFunctions.ConvertDBString(JB));
                    cmd.Parameters.AddWithValue("vSSYT", CommonFunctions.ConvertDBString(SSYT));
                    cmd.Parameters.AddWithValue("vYCLX", CommonFunctions.ConvertDBString(YCLX));
                    cmd.Parameters.AddWithValue("vDJZY", CommonFunctions.ConvertDBString(DJZY));
                    cmd.Parameters.AddWithValue("vWSHZ", CommonFunctions.ConvertDBString(SFHZ));
                    cmd.Parameters.AddWithValue("vTCRQ", CommonFunctions.ConvertDBString(TCRQ));
                    cmd.Parameters.AddWithValue("vPJDY", CommonFunctions.ConvertDBString(PJDY));
                    cmd.Parameters.AddWithValue("vCYC_ID", CYC_ID);


                    #endregion

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
                        builder.Append((j + 1).ToString());

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

            if (num_insert + num_update == DT.Rows.Count - 1)
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
                    errorMessage = string.Format("总共{0}条数据，导入{1}条！\n异常信息：\n{2}", DT.Rows.Count - 1, num_insert + num_update, errorMessage);
                }
                else
                {
                    error = builder.ToString();
                    errorMessage = string.Format("总共{0}条数据，导入{1}条,第{2}条数据SQL执行异常！\n异常信息：\n{3}", DT.Rows.Count - 1, num_insert + num_update, error, errorMessage);
                }

            }
            return result;
        }
    }
}
