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
    public partial class uploadfile3 : beneDesCYC.core.UI.corePage
    {
        SqlHelper sqlhelper = new SqlHelper();
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

                string msg = string.Empty;
                bool trueOrfalse = LoadData(StyleSheet, address, out msg);
                _return(trueOrfalse, msg, "null");
            }
        }

        public bool LoadData(string StyleSheet, string address,out string errorMessage)
        {
            #region 加载费用分类 模板
            //string address1 = Server.MapPath("./excel") + "\\cycfeiyongmoban.xls";
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
            #region //数据验证 @yzp  校验功能放入单条数据验证中

            //int countin = 0;
            //for (int j = 1; j < DT.Rows.Count; j++)
            //{
            //    if (DT.Rows[j][0].ToString().Trim() != "" && DT.Rows[j][1].ToString().Trim() != "")
            //    {
            //        countin = countin + 1;
            //    }
            //    else
            //    {
            //        j = j + 1;
            //        Response.Write("<script>alert('第:" + j + "行年月、区块名称不能为空')</script>");
            //    }
            //}
            #endregion
            return FromTableToSql(DT, feeTemplateTable, out errorMessage);
        }
        //数据解析入库
        public bool FromTableToSql(DataTable DT, DataTable feeTemplateTable,out string errorMessage)
        {
            string cyctype = _getParam("cyctype");
            string cyc = Session[cyctype].ToString();

            string FT_TYPE = _getParam("FT_TYPE");

            //用来记录错误数据
            StringBuilder builder = new StringBuilder();
            int insertCount = 0;
            bool result = false;
            #region 循环每一行数据进行处理
            for (int rowNum = 2; rowNum < DT.Rows.Count; rowNum++)
            {
                //1、读取到excel，将首行作为列名   
                //2、找到其中主键，验证是否存在于数据库中，若存在则执行update，拼凑update语句，否则拼凑insert语句
                //3、拼凑过程：取第一行数据，作为列名，取数据行对应列，作为数据。


                //****默认首列必须是  年月  字段
                string NY = DT.Rows[rowNum][0].ToString();
                #region 获取 费用行头  并进行数据校验

                string DEP_ID = string.Empty;
                string SOURCE_ID = string.Empty;
                string strErrorLog = GetParameters(DT, rowNum, NY, out SOURCE_ID, out DEP_ID).ToString();
                if (strErrorLog != "")
                {
                    if (builder.Length > 0)
                        builder.Append(",");
                    builder.Append((rowNum + 1).ToString());
                    continue;
                }
                #endregion

                #region 数据入库
                string FEE_CLASS = string.Empty;
                string FEE_CODE = string.Empty;
                string FEE = string.Empty;

                //string strsql = string.Empty;
                List<string> sqlList = new List<string>();

                //循环对每一列费用进行处理

                for (int col = 1; col < DT.Columns.Count; col++)
                {
                    #region 列数据提取

                    FEE_CODE = DT.Columns[col].ToString();
                    FEE = DT.Rows[rowNum][col].ToString();

                    if (DT.Rows[rowNum][col] != null && !string.IsNullOrEmpty(DT.Rows[rowNum][col].ToString()))
                    {
                        //获取费用大类 fee_class
                        FEE_CLASS = getFee_Class(FEE_CODE, feeTemplateTable);
                        if (FEE_CLASS == "")
                            continue;
                    }
                    else
                    {
                        continue;
                    }
                    #endregion

                    //验证 公共费用数据是否存在
                    string chksql = "select count(3) from ggfy where ny='" + NY + "' and SOURCE_ID='" + SOURCE_ID + "' and  cyc_id='" + cyc + "' and FT_TYPE='" + FT_TYPE + "'and FEE_CLASS='" + FEE_CLASS + "' and  FEE_CODE='" + FEE_CODE + "'";
                    int rows = int.Parse(sqlhelper.GetExecuteScalar(chksql).ToString());


                    #region 获取更新、插入语句

                    //拼凑update、insert语句
                    string strsql = string.Empty;
                    if (rows > 0)
                    {
                        StringBuilder updateStr = new StringBuilder();
                        updateStr.Append("update ggfy set ");
                        updateStr.Append(" FEE='" + FEE + "' ");
                        updateStr.Append(" where cyc_id='" + cyc + "' and NY= '" + NY + "' and FT_TYPE='" + FT_TYPE + "' and FEE_CLASS='" + FEE_CLASS + "' and FEE_CODE = '" + FEE_CODE + "' and  source_id='" + SOURCE_ID + "'");
                        //strsql = updateStr.ToString();
                        sqlList.Add(updateStr.ToString());
                    }
                    else
                    {
                        StringBuilder insertStr = new StringBuilder();
                        StringBuilder valueStr = new StringBuilder();
                        insertStr.Append("insert into ggfy(NY, DEP_ID, FT_TYPE, SOURCE_ID, FEE_CLASS, FEE_CODE, FEE, CYC_ID)");
                        insertStr.Append(" values(");
                        if (DT.Rows[rowNum][col] != null && !string.IsNullOrEmpty(DT.Rows[rowNum][col].ToString()))
                        {
                            insertStr.Append("'" + NY + "','" + DEP_ID + "','" + FT_TYPE + "','" + SOURCE_ID + "','" + FEE_CLASS + "','" + FEE_CODE + "','" + FEE.Trim() + "','" + cyc + "')");
                            //strsql = insertStr.ToString() + valueStr.ToString();
                            sqlList.Add(insertStr.ToString() + valueStr.ToString());
                        }
                    }
                    #endregion

                    //try
                    //{
                    //    if (!string.IsNullOrEmpty(strsql) && strsql != "")
                    //        sqlhelper.ExcuteSql(strsql.ToString());
                    //}
                    //catch (Exception e)
                    //{
                    //    Response.Write("<script>alert('格式有错误!" + e + "')</script>");
                    //}
                }
                try
                {
                    if (sqlhelper.ExecuteTranErrorCount(sqlList) != -1)
                    {
                        if (builder.Length > 0)
                            builder.Append(",");
                        builder.Append((rowNum + 1).ToString());
                    }
                    else
                    {
                        insertCount++;
                    }
                }
                catch (Exception e)
                {
                    if (builder.Length > 0)
                        builder.Append(",");
                    builder.Append((rowNum + 1).ToString());
                }
                #endregion
            }

            #endregion
            string error = "";
            if (insertCount == DT.Rows.Count-2)
            {
                result = true;
                errorMessage = string.Format("数据导入成功，共导入数据{0}条！", insertCount);
            }
            else
            {
                if (builder.Length == 0)
                {
                    errorMessage = string.Format("总共{0}条数据，导入{1}条！", DT.Rows.Count, insertCount);
                }
                else
                {
                    error = builder.ToString();
                    errorMessage = string.Format("总共{0}条数据，导入{1}条,第{2}条数据SQL执行异常！", DT.Rows.Count, insertCount, error);
                }
            }
            return result;
        }
        #region 获取费用大类

        //获取费用大类
        private string getFee_Class(string FEE_CODE, DataTable feeTemplateTable)
        {
            string FEE_CLASS = "";
            for (int i = 1; i < feeTemplateTable.Columns.Count; i++)
            {

                if (feeTemplateTable.Rows[1][i].ToString().ToUpper().Trim() == FEE_CODE.ToUpper().Trim())
                {
                    if (FEE_CODE == "rlf")
                    {
                    }
                    FEE_CLASS = feeTemplateTable.Rows[0][i].ToString().ToLower();
                    break;
                }
            }
            return FEE_CLASS;
        }
        #endregion

        #region 数据验证方法
        //进行数据校验 ，插入的数据必须在djsj中存在

        private bool cycTest(string NY, string CYC)
        {
            bool result = true;
            string sqlstring = "select count(3) from djsj where ny='" + NY + "' and cyc_id = '" + CYC + "'";
            int rows = Convert.ToInt32(sqlhelper.GetExecuteScalar(sqlstring));
            if (rows <= 0)
                result = false;
            return result;
        }
        //进行数据校验 ，插入的数据必须在djsj中存在

        private bool dataTest(string NY, string ZYQ)
        {
            bool result = true;
            string sqlstring = "select count(3) from djsj where ny='" + NY + "' and zyq = '" + ZYQ + "'";
            int rows = Convert.ToInt32(sqlhelper.GetExecuteScalar(sqlstring));
            if (rows <= 0)
                result = false;
            return result;
        }
        //进行数据校验 ，插入的数据 必须包含有井
        private bool qkTest(string NY, string QK)
        {
            bool result = true;
            string sqlstring = "select count(3) from djsj where ny='" + NY + "' and qk = '" + QK + "'";
            int rows = Convert.ToInt32(sqlhelper.GetExecuteScalar(sqlstring));
            if (rows <= 0)
                result = false;
            return result;
        }
        //进行数据校验 ，插入的数据必须在djsj中存在

        private bool zxzTest(string NY, string ZXZ)
        {
            bool result = true;
            string sqlstring = "select count(3) from djsj where ny='" + NY + "' and zxz = '" + ZXZ + "'";
            int rows = Convert.ToInt32(sqlhelper.GetExecuteScalar(sqlstring));
            if (rows <= 0)
                result = false;
            return result;
        }
        //进行数据校验 ，插入的数据必须在djsj中存在

        private bool zrzTest(string NY, string ZRZ)
        {
            bool result = true;
            string sqlstring = "select count(3) from djsj where ny='" + NY + "' and zrz = '" + ZRZ + "'";
            int rows = Convert.ToInt32(sqlhelper.GetExecuteScalar(sqlstring));
            if (rows <= 0)
                result = false;
            return result;
        }
        //进行数据校验 ，插入的数据必须在djsj中存在

        private bool jqzTest(string NY, string JQZ)
        {
            bool result = true;
            string sqlstring = "select count(3) from djsj where ny='" + NY + "' and jqz = '" + JQZ + "'";
            int rows = Convert.ToInt32(sqlhelper.GetExecuteScalar(sqlstring));
            if (rows <= 0)
                result = false;
            return result;
        }
        //进行数据校验 ，插入的数据必须在djsj中存在

        private bool jqzzTest(string NY, string JQZZ)
        {
            bool result = true;
            string sqlstring = "select count(3) from djsj where ny='" + NY + "' and jqzz = '" + JQZZ + "'";
            int rows = Convert.ToInt32(sqlhelper.GetExecuteScalar(sqlstring));
            if (rows <= 0)
                result = false;
            return result;
        }
        //进行数据校验 ，插入的数据必须在djsj中存在

        private bool lhzTest(string NY, string LHZ)
        {
            bool result = true;
            string sqlstring = "select count(3) from djsj where ny='" + NY + "' and LHZ = '" + LHZ + "'";
            int rows = Convert.ToInt32(sqlhelper.GetExecuteScalar(sqlstring));
            if (rows <= 0)
                result = false;
            return result;
        }
        //进行数据校验 ，插入的数据必须在djsj中存在

        private bool jhcTest(string NY, string JHC)
        {
            bool result = true;
            string sqlstring = "select count(3) from djsj where ny='" + NY + "' and jhc = '" + JHC + "'";
            int rows = Convert.ToInt32(sqlhelper.GetExecuteScalar(sqlstring));
            if (rows <= 0)
                result = false;
            return result;
        }
        #endregion

        #region 获取行头数据并对数据进行校验

        /// <summary>
        /// 获取行头数据并对数据进行校验
        /// 策略：如果不存在基准数据，则提醒用户。但是数据会录入系统。

        /// </summary>
        /// <param name="DT">数据列表</param>
        /// <param name="rowNum">当前行</param>
        /// <param name="NY">年月</param>
        /// <param name="SOURCE_ID">数据定位行</param>
        /// <param name="DEP_ID">所属单位</param>
        /// <returns>校验后，基准数据为空的数据</returns>
        private StringBuilder GetParameters(DataTable DT, int rowNum, string NY, out string SOURCE_ID, out string DEP_ID)
        {
            StringBuilder sbstr = new StringBuilder();
            SOURCE_ID = "";
            DEP_ID = "";
            for (int col = 0; col < DT.Columns.Count; col++)
            {
                string col_Name = DT.Columns[col].ToString().ToUpper();
                string col_Value = DT.Rows[rowNum][col].ToString();
                switch (col_Name)
                {
                    case "CYC":
                        //数据校验  与单井数据校验  关键字段不能为空
                        //if (string.IsNullOrEmpty(col_Value) || !cycTest(NY, col_Value))
                        //{
                        //    sbstr.Append(NY + " " + col_Value + "采油厂没有单井数据;\n");
                        //}
                        DEP_ID = Session["cyc_id"].ToString();
                        SOURCE_ID = DEP_ID;
                        break;
                    case "CQC":
                        //数据校验  与单井数据校验  关键字段不能为空
                        //if (string.IsNullOrEmpty(col_Value) || !cycTest(NY, col_Value))
                        //{
                        //    sbstr.Append(NY + " " + col_Value + "采气厂没有单井数据;\n");
                        //}
                        DEP_ID = Session["cyc_id"].ToString(); ;
                        SOURCE_ID = DEP_ID;
                        break;
                    case "ZYQ":
                        //数据校验  与单井数据校验  关键字段不能为空
                        col_Value = CommonFunctions.ConvertString(col_Value);
                        //if (CommonObject.ZYQ_MC_Table[col_Value] == null)
                        //{
                        //    sbstr.Append(col_Value + "作业区不存在;\n");
                        //}
                        //else
                        //{
                        //    col_Value = CommonObject.ZYQ_ID_Table[col_Value].ToString();
                        //}
                        if (string.IsNullOrEmpty(col_Value) || !dataTest(NY, col_Value))
                        {
                            sbstr.Append(NY + " " + col_Value + "作业区没有单井数据;\n");
                        }
                        DEP_ID = col_Value;
                        SOURCE_ID = col_Value;
                        break;
                    case "QK": //必须有作业区
                        //数据校验  与单井数据校验

                        if (!qkTest(NY, col_Value))
                        {
                            sbstr.Append("不存在" + NY + "属于" + col_Value + "的井;\n");
                        }
                        SOURCE_ID = SOURCE_ID + "$" + col_Value;
                        break;
                    case "ZXZ":
                        //数据校验  与单井数据校验

                        if (!zxzTest(NY, col_Value))
                        {
                            sbstr.Append("不存在" + NY + "属于" + col_Value + "的井;\n");
                        }
                        SOURCE_ID = SOURCE_ID + "$" + col_Value;
                        break;
                    case "ZRZ":
                        //数据校验  与单井数据校验

                        if (!zrzTest(NY, col_Value))
                        {
                            sbstr.Append("不存在" + NY + "属于" + col_Value + "的井;\n");
                        }
                        SOURCE_ID = SOURCE_ID + "$" + col_Value;
                        break;
                    case "JQZ":
                        //数据校验  与单井数据校验

                        if (string.IsNullOrEmpty(col_Value) || !jqzTest(NY, col_Value))
                        {
                            sbstr.Append("不存在" + NY + "属于" + col_Value + "的井;\n");
                        }
                        DEP_ID = col_Value;
                        SOURCE_ID = col_Value;
                        break;
                    case "JQZZ":
                        //数据校验  与单井数据校验

                        if (string.IsNullOrEmpty(col_Value) || !jqzzTest(NY, col_Value))
                        {
                            sbstr.Append("不存在" + NY + "属于" + col_Value + "的井;\n");
                        }
                        DEP_ID = col_Value;
                        SOURCE_ID = col_Value;
                        break;
                    case "LHZ":
                        //数据校验  与单井数据校验

                        if (string.IsNullOrEmpty(col_Value) && !lhzTest(NY, col_Value))
                        {
                            sbstr.Append("不存在" + NY + "属于" + col_Value + "的井;\n");
                        }
                        DEP_ID = col_Value;
                        SOURCE_ID = col_Value;
                        break;
                    case "JHC":
                        //数据校验  与单井数据校验

                        if (!jhcTest(NY, col_Value))
                        {
                            sbstr.Append("不存在" + NY + "属于" + col_Value + "的井;\n");
                        }
                        DEP_ID = col_Value;
                        SOURCE_ID = col_Value;
                        break;
                    default:
                        break;
                }
            }
            return sbstr;
        }
        #endregion

        #region 旧代码

        //public void LoadData(string StyleSheet, string address)
        //{
        //    //打开指定摸版

        //    string address1 = Server.MapPath("./excel") + "\\cycfeiyongmoban.xls";

        // //   string address = savePath + @"/" + sNewFileName + sExtension;


        //    string strCon1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" + address1 + " ;Extended Properties=Excel 8.0";
        //    OleDbConnection myConn1 = new OleDbConnection(strCon1);
        //    myConn1.Open();   //打开数据链接，得到一个数据集     
        //    DataSet myDataSet1 = new DataSet();   //创建DataSet对象     
        //    string StrSql1 = "select   *   from   [" + StyleSheet + "$]";
        //    OleDbDataAdapter myCommand1 = new OleDbDataAdapter(StrSql1, myConn1);
        //    myCommand1.Fill(myDataSet1, "[" + StyleSheet + "$]");
        //    myCommand1.Dispose();
        //    DataTable DT1 = myDataSet1.Tables["[" + StyleSheet + "$]"];
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





        //    //string strConn = "Data Source=orcl; User Id=jilin_xm; Password=jilin_xm";
        //    //OracleConnection conn = new OracleConnection(strConn);
        //    OracleConnection conn = DB.CreatConnection();
        //    conn.Open();
        //    string cyc = Session["cyc_id"].ToString();
        //    //导入重复数据时候,先将原来数据删除
        //    for (int l = 2; l < DT.Columns.Count; l++)
        //    {
        //        // l代表费用开始的列

        //        for (int n = 0; n < DT.Rows.Count; n++)
        //        {
        //            //n代表费用开始的行

        //            string source_id1 = DT.Rows[n][1].ToString();
        //            string deletesql = "delete from ggfy where ny='" + DT.Rows[n][0] + "' and dep_id='" + source_id1 + "' and source_id='" + source_id1 + "' and fee_class='" + DT1.Rows[0][l] + "' and fee_code='" + DT1.Rows[1][l] + "' and cyc_id='" + cyc + "'";

        //            OracleCommand cmd1 = new OracleCommand(deletesql, conn);
        //            cmd1.ExecuteNonQuery();
        //        }
        //    }
        //    string ft_type = "CYC";
        //    double zqlc = 0;
        //    //查找source_id,dj_id
        //    for (int j = 2; j < DT.Columns.Count; j++)
        //    {
        //        //j表示费用开始的列

        //        for (int i = 0; i < DT.Rows.Count; i++)
        //        {
        //            //i表示费用开始的行

        //            if ((DT.Rows[i][j]).GetType() == typeof(Double))
        //            {
        //                if (Convert.ToDouble(DT.Rows[i][j]) != 0)
        //                {
        //                    string source_id = DT.Rows[i][1].ToString();



        //                    //string sql = "insert into testfee values('" + DT.Rows[i][0] + "','" + DT1.Rows[1][j] + "','" + DT.Rows[i][j] + "','" + DT1.Rows[0][j] + "','" + DT.Rows[i][1] + "','" + DT.Rows[i][2] + "','" + ft_type + "','" + zqlc + "','" + source_id + "')";
        //                    string sql = "insert into ggfy values('" + DT.Rows[i][0] + "','" + DT.Rows[i][1] + "','" + ft_type + "','" + DT.Rows[i][1] + "','" + DT1.Rows[0][j] + "','" + DT1.Rows[1][j] + "','" + DT.Rows[i][j] + "','" + cyc + "')";
        //                    OracleCommand comm = new OracleCommand(sql, conn);
        //                    comm.ExecuteNonQuery();
        //                }


        //            }

        //        }
        //    }
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('导入成功!');</script>");
        //    conn.Close();



        //} 

        #endregion
    }
}
