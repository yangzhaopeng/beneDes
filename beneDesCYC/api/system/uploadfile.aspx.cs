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
using beneDesCYC;

namespace beneDesCYC.api.system
{
    public partial class uploadfile : beneDesCYC.core.UI.corePage
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
                //_return(false, msg, "null");
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

            OracleConnection conn = DB.CreatConnection();

            conn.Open();
            int num_update = 0;   //更新条数
            int num_insert = 0;   //插入条数
            try
            {

                for (int j = 0; j < DT.Rows.Count; j++)
                {

                    OracleCommand cmd = new OracleCommand("SP_INSERT_DJSJ", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

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
                    string JH = DT.Rows[j][1].ToString();
                    JH = CommonFunctions.ConvertString(JH);
                    //去除井号校验功能
                    //if (CommonObject.JH_ID_Table[JH] == null)
                    //{
                    //    errorMessage += string.Format("数据错误——第{0}行数据井号与A2不一致！\n", j + 1);
                    //    continue;
                    //}

                    string ZYQ = DT.Rows[j][3].ToString();
                    ZYQ = CommonFunctions.ConvertString(ZYQ);
                    //if (CommonObject.ZYQ_ID_Table[ZYQ] == null)
                    //{
                    //    errorMessage += string.Format("数据错误——第{0}行数据作业区名称与A2不一致！\n", j + 1);
                    //    continue;
                    //}
                    //else
                    //{
                    //    ZYQ = CommonObject.ZYQ_ID_Table[ZYQ].ToString();
                    //}

                    if (CommonObject.ZYQ_MC_Table[ZYQ] == null)
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据作业区名称与A2不一致！\n", j + 1);
                        continue;
                    }
                    string DJ = ZYQ + "$" + JH;

                    string QK = DT.Rows[j][2].ToString();
                    QK = CommonFunctions.ConvertString(QK);
                    //去掉区块校验
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

                    string PJDY = DT.Rows[j][18].ToString();
                    PJDY = CommonFunctions.ConvertString(PJDY);
                    //去掉评价单元校验
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
                    string CYC_ID = Session["cyc_id"].ToString();

                    //2013/11/01边敏修改
                    //OracleCommand mycom = new OracleCommand("delete djsj where ny='" + NY + "' and dj_id='" + DJ + "' and zyq='" + ZYQ + "' and zyq='"+QK+"'", conn);

                    // OracleCommand mycom = new OracleCommand("delete djsj where ny='" + NY + "' and dj_id='" + DJ + "' and jh='" + JH + "' and zyq='" + ZYQ + "' and qk='" + QK + "' and cyc_id='" + CYC_ID + "'", conn);
                    // int i= mycom.ExecuteNonQuery();


                    string ZXZ = DT.Rows[j][4].ToString();
                    ZXZ = CommonFunctions.ConvertString(ZXZ);
                    string ZRZ = DT.Rows[j][5].ToString();
                    ZRZ = CommonFunctions.ConvertString(ZRZ);
                    string JXName = DT.Rows[j][6].ToString();
                    int JX = 1;
                    if (JXName == "直井")
                        JX = 1;
                    else
                        JX = 2;
                    string CYJXH = DT.Rows[j][7].ToString();
                    CYJXH = CommonFunctions.ConvertString(CYJXH);
                    string CYJMPGL = DT.Rows[j][8].ToString();
                    CYJMPGL = CommonFunctions.ConvertString(CYJMPGL);
                    if (!string.IsNullOrEmpty(CYJMPGL) && !CommonFunctions.IsNumber(CYJMPGL))
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据抽油机名牌功率错误！\n", j + 1);
                        continue;
                    }
                    string DJGL = DT.Rows[j][9].ToString();
                    DJGL = CommonFunctions.ConvertString(DJGL);
                    if (!string.IsNullOrEmpty(DJGL) && !CommonFunctions.IsNumber(DJGL))
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据电机功率错误！\n", j + 1);
                        continue;
                    }
                    string FZL = DT.Rows[j][10].ToString();
                    FZL = CommonFunctions.ConvertString(FZL);
                    if (!string.IsNullOrEmpty(FZL) && !CommonFunctions.IsNumber(FZL))
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据负载率错误！\n", j + 1);
                        continue;
                    }
                    string YQLX = DT.Rows[j][11].ToString();
                    YQLX = CommonFunctions.ConvertString(YQLX);
                    string XSYP = DT.Rows[j][12].ToString();
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
                    string TCRQ = DT.Rows[j][13].ToString();
                    TCRQ = CommonFunctions.ConvertString(TCRQ);
                    string JB = DT.Rows[j][14].ToString();
                    JB = CommonFunctions.ConvertString(JB);
                    //if (CommonObject.JB_DM_Table[JB] == null)
                    //{
                    //    errorMessage += string.Format("数据错误——第{0}行数据井别错误！\n", j + 1);
                    //    continue;
                    //}
                    //else
                    //{
                    //    JB = CommonObject.JB_DM_Table[JB].ToString();
                    //}

                    if (CommonObject.JB_MC_Table[JB] == null)
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据井别错误！\n", j + 1);
                        continue;
                    }
                    string DJDB = DT.Rows[j][15].ToString();
                    DJDB = CommonFunctions.ConvertString(DJDB);
                    //if (DJDB == "有")
                    //{
                    //    DJDB = "1";
                    //}
                    //else
                    //{
                    //    DJDB = "0";
                    //}
                    string SSYT = DT.Rows[j][16].ToString();
                    SSYT = CommonFunctions.ConvertString(SSYT);
                    string YCLX = DT.Rows[j][17].ToString();
                    YCLX = CommonFunctions.ConvertString(YCLX);
                    if (CommonObject.GetYCLX(CYC_ID) == null || CommonObject.GetYCLX(CYC_ID)[YCLX] == null)
                    {
                        errorMessage += string.Format("数据错误——第{0}行数据油藏类型错误！\n", j + 1);
                        continue;
                    }

                    cmd.Parameters.Add(new OracleParameter("vrtn", SqlDbType.Int));
                    cmd.Parameters["vrtn"].Direction = ParameterDirection.Output;
                    #region 存储过程添加值


                    cmd.Parameters.AddWithValue("vNY", NY);
                    cmd.Parameters.AddWithValue("vDJ_ID", DJ);
                    cmd.Parameters.AddWithValue("vJH", JH);
                    cmd.Parameters.AddWithValue("vZYQ", CommonFunctions.ConvertDBString(ZYQ));
                    cmd.Parameters.AddWithValue("vQK", CommonFunctions.ConvertDBString(QK));
                    cmd.Parameters.AddWithValue("vPJDY", CommonFunctions.ConvertDBString(PJDY));
                    cmd.Parameters.AddWithValue("vZXZ", CommonFunctions.ConvertDBString(ZXZ));
                    cmd.Parameters.AddWithValue("vZRZ", CommonFunctions.ConvertDBString(ZRZ));

                    cmd.Parameters.AddWithValue("vJX", JX);
                    cmd.Parameters.AddWithValue("vCYJXH", CommonFunctions.ConvertDBString(CYJXH));
                    cmd.Parameters.AddWithValue("vCYJMPGL", CommonFunctions.ConvertDBString(CYJMPGL));
                    cmd.Parameters.AddWithValue("vDJGL", CommonFunctions.ConvertDBString(DJGL));
                    cmd.Parameters.AddWithValue("vFZL", CommonFunctions.ConvertDBString(FZL));
                    cmd.Parameters.AddWithValue("vYQLX", CommonFunctions.ConvertDBString(YQLX));
                    cmd.Parameters.AddWithValue("vXSYP", CommonFunctions.ConvertDBString(XSYP));
                    cmd.Parameters.AddWithValue("vTCRQ", CommonFunctions.ConvertDBString(TCRQ));
                    cmd.Parameters.AddWithValue("vJB", CommonFunctions.ConvertDBString(JB));
                    cmd.Parameters.AddWithValue("vDJDB", CommonFunctions.ConvertDBString(DJDB));
                    cmd.Parameters.AddWithValue("vSSYT", CommonFunctions.ConvertDBString(SSYT));
                    cmd.Parameters.AddWithValue("vYCLX", CommonFunctions.ConvertDBString(YCLX));
                    cmd.Parameters.AddWithValue("vCYC_ID", CYC_ID);



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
                            builder.Append((j + 1).ToString());

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
            if (num_insert + num_update == DT.Rows.Count)
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
