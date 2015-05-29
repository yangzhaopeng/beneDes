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
    public partial class uploadfile2 : beneDesCYC.core.UI.corePage
    {
        SqlHelper sqlhelper = new SqlHelper();
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
            string sNewFileName = DateTime.Now.ToString("yyyyMMdd") + Session["userName"].ToString();
            postedFile.SaveAs(savePath + @"/" + sNewFileName + sExtension);

            string address = savePath + @"/" + sNewFileName + sExtension;
            GetSheet getsheetx = new GetSheet();
            string StyleSheet = getsheetx.GetExcelSheetNames(address);

            //LoadData(StyleSheet, address, out backMsg);

            //if (postedFile != null)
            //{ _return(true, "上传成功！" + backMsg, "null"); }
            //else
            //{ _return(false, "上传失败！" + backMsg, "null"); }

            string msg = string.Empty;
            bool trueOrfalse = LoadData(StyleSheet, address, out msg);
            _return(trueOrfalse, msg, "null");
        }

        public bool LoadData(string StyleSheet, string address, out string errorMessage)
        {
            bool result = true;
            #region 加载导入摸版
            //string address1 = Server.MapPath("./excel") + "\\djfeiyongmoban.xls";
            string address1 = Server.MapPath("./excel") + "\\feiyongmoban.xls";
            string strCon1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" + address1 + " ;Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
            OleDbConnection myConn1 = new OleDbConnection(strCon1);
            myConn1.Open();   //打开数据链接，得到一个数据集     
            DataSet myDataSet1 = new DataSet();   //创建DataSet对象     
            string StrSql1 = "select   *   from   [Sheet1$]";
            OleDbDataAdapter myCommand1 = new OleDbDataAdapter(StrSql1, myConn1);
            myCommand1.Fill(myDataSet1, "[Sheet1$]");
            myCommand1.Dispose();
            DataTable feeTemplateTable = myDataSet1.Tables["[Sheet1$]"];
            myConn1.Close();
            myCommand1.Dispose();

            #endregion
            errorMessage = "";
            string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" + address + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
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

            //数据验证
            int countin = 0;
            for (int j = 2; j < DT.Rows.Count; j++)
            {
                if (string.IsNullOrEmpty(DT.Rows[j][0].ToString().Trim()))
                    break;
                else
                {
                    if (DT.Rows[j][1].ToString().Trim() != "" && DT.Rows[j][2].ToString().Trim() != "")
                    {
                        countin = countin + 1;
                    }
                    else
                    {
                        j = j + 2;
                        errorMessage += "第:" + j + "行井号、作业区名称不能为空;";
                        result = false;
                        //Response.Write("<script>alert('第:" + j + "行年月、区块名称不能为空')</script>");
                    }
                }
            }
            if (string.IsNullOrEmpty(errorMessage))
            {
                result = FromTableToSql(DT, feeTemplateTable, out errorMessage);
            }
            return result;
        }

        public bool FromTableToSql(DataTable DT, DataTable feeTemplateTable, out string errorMessage)
        {
            string cyc = Session["cyc_id"].ToString();
            errorMessage = "";
            bool result = false;
            //存储导入的年月

            List<string> nylist = new List<string>();
            string FT_TYPE = "DJ";
            //用来记录错误数据
            StringBuilder sbstr = new StringBuilder();
            #region 循环每一行数据进行处理

            int insertCount = 0;
            StringBuilder builder = new StringBuilder();
            for (int rowNum = 2; rowNum < DT.Rows.Count; rowNum++)
            {
                //1、读取到excel，将首行作为列名   
                //2、从第三行开始数据行，找到其中主键，验证是否存在于数据库中，若存在则执行update，拼凑update语句，否则拼凑insert语句
                //3、拼凑过程：取第一行数据，作为列名，取数据行对应列，作为数据。

                string NY = DT.Rows[rowNum][0].ToString();
                NY = CommonFunctions.ConvertString(NY);
                if (string.IsNullOrEmpty(NY))
                {
                    break;
                }
                else if (NY.Length != 6)
                {
                    errorMessage += string.Format("数据错误——第{0}行数据月份错误！\n", rowNum);
                    continue;
                }
                #region 获取 费用行头

                string ZYQ = DT.Rows[rowNum][1].ToString();
                ZYQ = CommonFunctions.ConvertString(ZYQ);
                if (CommonObject.ZYQ_MC_Table[ZYQ] == null)
                {
                    sbstr.Append(string.Format("数据错误——第{0}行数据作业区名称与系统不一致！\n", rowNum));
                    continue;
                }
                //else
                //{
                //    ZYQ = CommonObject.ZYQ_ID_Table[ZYQ].ToString();
                //}
                string JH = DT.Rows[rowNum][2].ToString();
                JH = CommonFunctions.ConvertString(JH);
                if (CommonObject.DJSJ_JH_ID_Table(NY, cyc)[JH] == null)
                {
                    errorMessage += string.Format("数据错误——第{0}行数据井号与系统不一致！\n", rowNum + 1);
                    continue;
                }

                string DEP_ID = ZYQ;
                string SOURCE_ID = ZYQ;
                string DJ_ID = ZYQ + "$" + JH;
                double ZQLC = 0;

                #endregion

                //数据校验  与单井数据校验

                if (!dataTest(NY, ZYQ, JH))
                {
                    sbstr.Append(string.Format("数据错误——第{0}行,{1}{2}{3}单井基础数据未录入系统;\n", rowNum, NY, ZYQ, JH));
                    continue;
                }
                #region 数据入库
                string FEE_CLASS = string.Empty;
                string FEE_CODE = string.Empty;
                string FEE = string.Empty;

                //string strsql = string.Empty;
                List<string> sqlList = new List<string>();

                //循环对每一列进行处理

                for (int col = 3; col < DT.Columns.Count; col++)
                {
                    #region 列数据提取

                    FEE_CODE = DT.Columns[col].ToString();
                    FEE = DT.Rows[rowNum][col].ToString();
                    //对油井周期轮次的特殊处理
                    if (FEE_CODE == "ZQLC")
                    {
                        double.TryParse(FEE, out ZQLC);
                        cyc = Session["cyc_id"].ToString();
                        continue;
                    }
                    if (DT.Rows[rowNum][col] != null && !string.IsNullOrEmpty(DT.Rows[rowNum][col].ToString()))
                    {
                        //获取费用大类 fee_class
                        FEE_CLASS = getFee_Class(FEE_CODE, feeTemplateTable);
                    }
                    else
                    {
                        continue;
                    }
                    #endregion

                    //验证 单井数据是否存在
                    string chksql = "select count(3) from djfy where ny='" + NY + "' and DJ_ID='" + ZYQ + "$" + JH + "' and FEE_CLASS='" + FEE_CLASS + "' and  FEE_CODE='" + FEE_CODE + "' and cyc_id='" + cyc + "'";
                    int rows = int.Parse(sqlhelper.GetExecuteScalar(chksql).ToString());

                    #region 获取更新、插入语句

                    //拼凑update、insert语句
                    if (rows > 0)
                    {
                        StringBuilder updateStr = new StringBuilder();
                        updateStr.Append("update djfy set ");
                        updateStr.Append(" FEE='" + FEE + "' ");
                        updateStr.Append(" where cyc_id='" + cyc + "'and FEE_CLASS='" + FEE_CLASS + "' and FEE_CODE='" + FEE_CODE + "' and FT_TYPE='" + FT_TYPE + "' and NY= '" + NY + "' and DJ_ID='" + ZYQ + "$" + JH + "'");
                        sqlList.Add(updateStr.ToString());
                    }
                    else
                    {
                        StringBuilder insertStr = new StringBuilder();
                        StringBuilder valueStr = new StringBuilder();
                        insertStr.Append("insert into djfy(NY, DEP_ID, FT_TYPE, SOURCE_ID, DJ_ID, ZQLC, FEE_CLASS, FEE_CODE, FEE, CYC_ID)");
                        insertStr.Append(" values(");
                        if (DT.Rows[rowNum][col] != null && !string.IsNullOrEmpty(DT.Rows[rowNum][col].ToString()))
                        {
                            insertStr.Append("'" + NY + "','" + DEP_ID + "','" + FT_TYPE + "','" + SOURCE_ID + "','" + DJ_ID + "','" + ZQLC + "','" + FEE_CLASS + "','" + FEE_CODE + "','" + FEE.Trim() + "','" + cyc + "')");
                            //strsql = insertStr.ToString() + valueStr.ToString();
                            sqlList.Add(insertStr.ToString() + valueStr.ToString());
                        }
                    }
                    #endregion

                    //try
                    //{
                    //    sqlhelper.ExcuteSql(strsql.ToString());
                    //    if (!nylist.Contains(NY))
                    //    {
                    //        nylist.Add(NY);
                    //    }
                    //}
                    //catch (Exception e)
                    //{
                    //    Response.Write("<script>alert('格式有错误!" + e + "')</script>");
                    //}
                }
                try
                {
                    if (sqlhelper.ExecuteTranErrorCount(sqlList) == -1)
                    {
                        if (!nylist.Contains(NY))
                        {
                            nylist.Add(NY);
                        }
                        insertCount++;
                    }
                    else
                    {
                        if (builder.Length > 0)
                            builder.Append(",");
                        builder.Append((rowNum - 1).ToString());
                    }
                }
                catch (Exception e)
                {
                    if (builder.Length > 0)
                        builder.Append(",");
                    builder.Append((rowNum - 1).ToString());
                }
                #endregion
            }
            #endregion

            #region 纵表转横表入库


            //将单井数据整理到fee_cross
            for (int i = 0; i < nylist.Count; i++)
            {
                try
                {
                    sqlhelper.ExcuteSql("begin CROSS_FEE1('" + nylist[i] + "','" + cyc + "','" + FT_TYPE + "');end;");
                }
                catch (Exception ex)
                {
                }
            }

            #endregion

            if (sbstr.Length > 0)
            {
                errorMessage = sbstr.ToString();
            }
            if (insertCount == DT.Rows.Count - 2)
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
                    errorMessage = string.Format("总共{0}条数据，导入{1}条！\n异常信息：\n{2}", DT.Rows.Count - 2, insertCount, errorMessage);
                }
                else
                {
                    errorMessage = string.Format("总共{0}条数据，导入{1}条,第{2}条数据SQL执行异常！\n异常信息：\n{3}", DT.Rows.Count - 2, insertCount, builder.ToString(), errorMessage);
                }

            }
            return result;
        }

        //获取费用大类
        private string getFee_Class(string FEE_CODE, DataTable feeTemplateTable)
        {
            string FEE_CLASS = "";
            for (int i = 1; i < feeTemplateTable.Columns.Count; i++)
            {
                if (feeTemplateTable.Rows[1][i].ToString().ToUpper().Trim() == FEE_CODE.ToUpper().Trim())
                {
                    FEE_CLASS = feeTemplateTable.Rows[0][i].ToString().ToLower();
                    break;
                }
            }
            return FEE_CLASS;
        }
        //进行数据校验 ，插入的数据必须在djsj中存在

        private bool dataTest(string NY, string ZYQ, string JH)
        {
            bool result = true;
            string sqlstring = "select count(3) from djsj where ny='" + NY + "' and zyq = '" + ZYQ + "' and jh='" + JH + "'";
            int rows = Convert.ToInt32(sqlhelper.GetExecuteScalar(sqlstring));
            if (rows <= 0)
                result = false;
            return result;
        }

        #region 旧代码

        //public void LoadData(string StyleSheet, string address)
        //{
        //    //打开指定摸版

        //    string address1 = Server.MapPath("./excel") + "\\djfeiyongmoban.xls";
        //    //string address1 = Server.MapPath("./excel") + "\\fee_test1.xls";


        //    string strCon1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" + address1 + " ;Extended Properties=Excel 8.0";
        //    OleDbConnection myConn1 = new OleDbConnection(strCon1);
        //    myConn1.Open();   //打开数据链接，得到一个数据集     
        //    DataSet myDataSet1 = new DataSet();   //创建DataSet对象     
        //    string StrSql1 = "select   *   from   [Sheet1$]";
        //    OleDbDataAdapter myCommand1 = new OleDbDataAdapter(StrSql1, myConn1);
        //    myCommand1.Fill(myDataSet1, "[Sheet1$]");
        //    myCommand1.Dispose();
        //    DataTable DT1 = myDataSet1.Tables["[Sheet1$]"];
        //    myConn1.Close();
        //    myCommand1.Dispose();

        //    //this.GridView2.DataSource = DT1;
        //    //GridView2.DataBind();
        //    //this.Label1.Text = address;




        //    string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" + address + ";Extended Properties=Excel 8.0";
        //    OleDbConnection myConn = new OleDbConnection(strCon);
        //    myConn.Open();   //打开数据链接，得到一个数据集     
        //    DataSet myDataSet = new DataSet();   //创建DataSet对象     
        //    string StrSql = "select   *   from   [" + StyleSheet + "$]";
        //    OleDbDataAdapter myCommand = new OleDbDataAdapter(StrSql, myConn);
        //    myCommand.Fill(myDataSet, "[" + StyleSheet + "$]");
        //    myCommand.Dispose();
        //    DataTable DT = myDataSet.Tables["[" + StyleSheet + "$]"];
        //    myConn.Close();
        //    myCommand.Dispose();





        //    OracleConnection conn = DB.CreatConnection();
        //    string CYC = Session["cyc_id"].ToString();
        //    conn.Open();
        //    ////导入重复数据时候,先将原来数据删除
        //    //for (int l = 3; l < DT.Columns.Count; l++)
        //    //{
        //    //    for (int n = 0; n < DT.Rows.Count; n++)
        //    //    {
        //    //        string source_id1 = DT.Rows[n][2] + "$" + DT.Rows[n][1];
        //    //        string deletesql = "delete from djfy where ny='" + DT.Rows[n][0] + "' and dj_id='" + source_id1 + "' and fee_class='" + DT1.Rows[0][l] + "' and fee_code='" + DT1.Rows[1][l] + "' and cyc_id='"+cyc+"'";

        //    //        OracleCommand cmd1 = new OracleCommand(deletesql, conn);
        //    //        cmd1.ExecuteNonQuery();
        //    //    }
        //    //}
        //    //string ft_type = "DJ";
        //    //double zqlc = 0;
        //    ////查找source_id,dj_id
        //    //for (int j = 3; j < DT.Columns.Count; j++)
        //    //{
        //    //    for (int i = 0; i < DT.Rows.Count; i++)
        //    //    {
        //    //        if ((DT.Rows[i][j]).GetType() == typeof(Double))
        //    //        {
        //    //            if (Convert.ToDouble(DT.Rows[i][j]) != 0)
        //    //            {
        //    //                string source_id = DT.Rows[i][2] + "$" + DT.Rows[i][1];



        //    //                //string sql = "insert into testfee values('" + DT.Rows[i][0] + "','" + DT1.Rows[1][j] + "','" + DT.Rows[i][j] + "','" + DT1.Rows[0][j] + "','" + DT.Rows[i][1] + "','" + DT.Rows[i][2] + "','" + ft_type + "','" + zqlc + "','" + source_id + "')";
        //    //                string sql = "insert into djfy values('" + DT.Rows[i][0] + "','" + DT.Rows[i][2] + "','" + ft_type + "','" + source_id + "','" + source_id + "','" + zqlc + "','" + DT1.Rows[0][j] + "','" + DT1.Rows[1][j] + "','" + DT.Rows[i][j] + "','"+cyc+"')";
        //    //                OracleCommand comm = new OracleCommand(sql, conn);
        //    //                comm.ExecuteNonQuery();
        //    //            }


        //    //        }

        //    //    }
        //    //}
        //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('导入成功!');</script>");
        //    //conn.Close();
        //    //边敏2013年11月30日修改

        //    StringBuilder builder = new StringBuilder();
        //    int num_update = 0;   //更新条数
        //    int num_insert = 0;   //插入条数
        //    string FT_TYPE = "DJ";
        //    double ZQLC = 0;
        //    try
        //    {
        //        //存储导入的年月

        //        List<string> nylist = new List<string>();
        //        for (int j = 3; j < DT.Columns.Count; j++)
        //        {
        //            for (int i = 0; i < DT.Rows.Count; i++)
        //            {
        //                //if ((DT.Rows[i][j]).GetType() == typeof(Double))
        //                //{
        //                //    if (Convert.ToDouble(DT.Rows[i][j]) != 0)
        //                //    {

        //                OracleCommand cmd = new OracleCommand("SP_INSERT_DJFY", conn);
        //                cmd.CommandType = CommandType.StoredProcedure;


        //                string NY = DT.Rows[i][0].ToString();
        //                if (NY == "")
        //                {
        //                    break;
        //                }
        //                else
        //                {
        //                    if (!nylist.Contains(NY))
        //                    {
        //                        nylist.Add(NY);
        //                    }
        //                }
        //                string DEP_ID = DT.Rows[i][2].ToString();
        //                string SOURCE_ID = DT.Rows[i][2].ToString() + "$" + DT.Rows[i][1].ToString();

        //                string DJ_ID = DT.Rows[i][2].ToString() + "$" + DT.Rows[i][1].ToString();
        //                string FEE_CLASS = DT1.Rows[0][j].ToString();
        //                string FEE_CODE = DT1.Rows[1][j].ToString();
        //                string FEE = DT.Rows[i][j].ToString();

        //                cmd.Parameters.Add(new OracleParameter("vrtn", SqlDbType.Int));
        //                cmd.Parameters["vrtn"].Direction = ParameterDirection.Output;
        //                #region 存储过程添加值

        //                cmd.Parameters.AddWithValue("vNY", NY);
        //                cmd.Parameters.AddWithValue("vDEP_ID", DEP_ID);
        //                cmd.Parameters.AddWithValue("vFT_TYPE", FT_TYPE);
        //                cmd.Parameters.AddWithValue("vSOURCE_ID", SOURCE_ID);
        //                cmd.Parameters.AddWithValue("vDJ_ID", DJ_ID);
        //                cmd.Parameters.AddWithValue("vZQLC", ZQLC);
        //                cmd.Parameters.AddWithValue("vFEE_CLASS", FEE_CLASS);
        //                cmd.Parameters.AddWithValue("vFEE_CODE", FEE_CODE);
        //                cmd.Parameters.AddWithValue("vFEE", FEE);
        //                cmd.Parameters.AddWithValue("vCYC_ID", CYC);

        //                #endregion

        //                cmd.ExecuteNonQuery();
        //                if (cmd.Parameters["vrtn"].Value == null || string.IsNullOrEmpty(cmd.Parameters["vrtn"].Value.ToString()))
        //                    continue;
        //                int result = (int)cmd.Parameters["vrtn"].Value;
        //                if (result == 1)     //result==1为插入操作,result==2为更新操作

        //                    num_insert++;
        //                if (result == 2)
        //                    num_update++;
        //                if (result == 3)
        //                {
        //                    if (builder.Length > 0)
        //                        builder.Append(",");
        //                    builder.Append((j + 1).ToString());

        //                }

        //                //string sql = "insert into djfy values('" + DT.Rows[i][0] + "','" + DT.Rows[i][2] + "','" + ft_type + "','" + source_id + "','" + source_id + "','" + zqlc + "','" + DT1.Rows[0][j] + "','" + DT1.Rows[1][j] + "','" + DT.Rows[i][j] + "','" + cyc + "')";
        //                //OracleCommand comm = new OracleCommand(sql, conn);
        //                //comm.ExecuteNonQuery();



        //            }
        //        }
        //        //将单井数据整理到fee_cross
        //        OracleCommand cmdP = new OracleCommand("CROSS_FEE1", conn);
        //        cmdP.CommandType = CommandType.StoredProcedure;
        //        for (int i = 0; i < nylist.Count; i++)
        //        {
        //            cmdP.Parameters.AddWithValue("as_ny", nylist[i] );
        //            cmdP.Parameters.AddWithValue("as_cyc",  CYC );
        //            cmdP.Parameters.AddWithValue("as_type", "DJ");
        //              cmdP.ExecuteNonQuery();
        //        }
        //    }

        //    catch (OracleException aa)
        //    {
        //        Response.Write(aa.Message.ToString());
        //    }
        //    conn.Close();
        //    string error = "";
        //    if (builder.Length == 0)
        //        error = "0";
        //    else
        //        error = builder.ToString();
        //    Response.Write("<script>alert('导入成功！您导入的数据条数为:" + (num_insert + num_update) + ",第" + error + "条数据错误！')</script>");



        //}


        //private void DataProcess(string ny)
        //{
        //    OracleConnection conn = DB.CreatConnection();

        //}
        #endregion
    }
}
