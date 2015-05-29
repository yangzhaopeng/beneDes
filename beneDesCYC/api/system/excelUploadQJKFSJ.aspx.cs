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
using beneDesCYC.model.system;

namespace beneDesCYC.api.system
{
    public partial class excelUploadQJKFSJ : beneDesCYC.core.UI.corePage
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
            string sNewFileName = DateTime.Now.ToString("yyyymmdd") + Session["userName"].ToString();
            postedFile.SaveAs(savePath + @"/" + sNewFileName + sExtension);

            string address = savePath + @"/" + sNewFileName + sExtension;
            GetSheet getsheetx = new GetSheet();
            string StyleSheet = getsheetx.GetExcelSheetNames(address);

            //LoadData(StyleSheet, address);

            string msg = string.Empty;
            bool trueOrfalse = LoadData(StyleSheet, address, out msg);
            _return(trueOrfalse, msg, "null");

            //if (postedFile != null)
            //{ _return(true, "上传成功！", "null"); }
            //else
            //{ _return(false, "上传失败！", "null"); }

        }
        public bool LoadData(string StyleSheet, string address, out string errorMessage)
        {
            errorMessage = "";
            bool result = true;
            string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" + address + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
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
            for (int j = 1; j < DT.Rows.Count; j++)
            {
                if (DT.Rows[j][0].ToString().Trim() != "" && DT.Rows[j][1].ToString().Trim() != "")
                {
                    countin = countin + 1;
                }
                else
                {
                    j = j + 1;
                    //Response.Write("<script>alert('第:" + j + "行年月、区块名称不能为空')</script>");
                    errorMessage = string.Format("第{0}行年月、区块名称不能为空", j);
                    result = false;
                }
            }
            string cyc = Session["cqc_id"].ToString();
            result = FromTableToSql(DT, "kfsj", cyc, out errorMessage);
            return result;
        }

        public bool FromTableToSql(DataTable DT, string tableName, string cyc, out string errorMessage)
        {
            bool result = false;
            errorMessage = "";
            List<int> keyNum = new List<int>();
            //循环获取主键所在列  主键标识：key：NY   key:ZYQ
            for (int colNum = 0; colNum < DT.Columns.Count; colNum++)
            {
                if (DT.Columns[colNum].ToString().ToLower().Contains("key:"))
                {
                    keyNum.Add(colNum);
                }
            }
            int insertCount = 0;
            for (int rowNum = 1; rowNum < DT.Rows.Count; rowNum++)
            {
                //1、读取到excel，将首行作为列名   
                //2、从第三行开始数据行，找到其中主键，验证是否存在于数据库中，若存在则执行update，拼凑update语句，否则拼凑insert语句
                //3、拼凑过程：取第一行数据，作为列名，取数据行对应列，作为数据。


                string NY = DT.Rows[rowNum][0].ToString();
                //string PJDYMC = DT.Rows[rowNum][1].ToString();

                string chksql = "select count(1) from " + tableName;// +" where ny='" + NY + "' and pjdymc='" + PJDYMC + "' and cyc_id='" + cyc + "'";
                //拼凑本表的条件where语句：主键约束
                if (string.IsNullOrEmpty(NY))
                    break;
                string whereSql = string.Empty;
                if (keyNum.Count > 0)
                {
                    whereSql += " where ";
                    for (int num = 0; num < keyNum.Count; num++)
                    {
                        int col = keyNum[num];//列

                        string colName = DT.Columns[col].ToString().Split(':')[1];
                        string colValue = DT.Rows[rowNum][col].ToString();
                        if (colName == "ZYQ")
                        {
                            //string colValue = DT.Rows[rowNum][col].ToString() + "$" + DT.Rows[rowNum][col - 1].ToString();
                            if (CommonObject.ZYQ_ID_Table[colValue] != null)
                            {
                                colValue = CommonObject.ZYQ_ID_Table[colValue].ToString() + "$" + DT.Rows[rowNum][col - 1].ToString();
                                whereSql += "DJ_ID" + " ='" + colValue + "' and ";
                            }
                        }
                        else
                        {
                            whereSql += colName + " ='" + colValue + "' and ";
                        }
                    }
                    whereSql += " cyc_id='" + cyc + "'";
                }
                chksql = chksql + whereSql;
                int rows = int.Parse(sqlhelper.GetExecuteScalar(chksql).ToString());

                bool canExcute = true;
                //拼凑update、insert语句
                if (rows > 0)
                {

                    StringBuilder updateStr = new StringBuilder();
                    updateStr.Append("update " + tableName + " set ");
                    //循环对每一列进行处理

                    for (int col = 0; col < DT.Columns.Count; col++)
                    {
                        if (DT.Rows[rowNum][col] != null)
                        {
                            string colName = DT.Columns[col].ToString();
                            string colValue = DT.Rows[rowNum][col].ToString();
                            if (colName.Contains(':'))
                            {
                                //对主键的处理
                                string[] colText = colName.Split(':');
                                colName = colText[1];
                            }
                            colName = CommonFunctions.ConvertString(colName);
                            colValue = CommonFunctions.ConvertString(colValue);
                            if (colName.ToUpper() == "JH")
                            {
                                //if (CommonObject.JH_ID_Table[colValue] == null)
                                //{
                                //    canExcute = false;
                                //    errorMessage += string.Format("数据错误——第{0}行数据井号与A2不一致！\n", rowNum);
                                //    break;
                                //}
                                if (CommonObject.DJSJ_JH_ID_Table(NY,cyc)[colValue] == null)
                                {
                                    canExcute = false;
                                    errorMessage += string.Format("数据错误——第{0}行数据井号{1}与系统不一致！\n", rowNum, colValue);
                                    break;
                                }
                            }
                            else if (colName.ToUpper() == "ZYQ")
                            {
                                if (CommonObject.ZYQ_ID_Table[colValue] == null)
                                {
                                    canExcute = false;
                                    errorMessage += string.Format("数据错误——第{0}行数据作业区名称与A2不一致！\n", rowNum);
                                    break;
                                }
                                else
                                {
                                    colValue = CommonObject.ZYQ_ID_Table[colValue].ToString();
                                }
                            }
                            else if (colName.ToUpper() == "QK")
                            {
                                //if (CommonObject.Area_ID_Table[colValue] == null)
                                //{
                                //    errorMessage += string.Format("数据异常——第{0}行数据区块名称与A2不一致！\n", rowNum);
                                //}
                                if (CommonObject.RushLocalAreaInfo(NY, cyc)[colValue] == null)
                                {
                                    errorMessage += string.Format("数据错误——第{0}行数据区块名称与系统不一致！\n", rowNum);
                                }
                            }
                            updateStr.Append(colName + "='" + colValue + "',");
                        }

                    }
                    if (!canExcute)
                    {
                        continue;
                    }
                    updateStr.Append(" cyc_id='" + cyc + "'" + whereSql);
                    try
                    {
                        if (sqlhelper.ExcuteSql(updateStr.ToString()) > 0)
                        {
                            insertCount++;
                        }
                    }
                    catch (Exception e)
                    {
                        errorMessage += string.Format("SQL执行异常——{0}\n", e.Message);
                    }
                }
                else
                {
                    StringBuilder insertStr = new StringBuilder();
                    StringBuilder valueStr = new StringBuilder();

                    insertStr.Append("insert into " + tableName + "( ");
                    valueStr.Append("values( ");
                    //循环对每一列进行处理

                    for (int col = 0; col < DT.Columns.Count; col++)
                    {
                        if (DT.Rows[rowNum][col] != null)
                        {
                            string colName = DT.Columns[col].ToString();
                            string colValue = DT.Rows[rowNum][col].ToString();
                            colName = CommonFunctions.ConvertString(colName);
                            colValue = CommonFunctions.ConvertString(colValue);
                            if (colName.Contains(':'))
                            {
                                //对主键字段的处理
                                string[] colText = colName.Split(':');
                                colName = colText[1];
                            }
                            if (colName == "JH")
                            {
                                //if (CommonObject.JH_ID_Table[colValue] == null)
                                //{
                                //    canExcute = false;
                                //    errorMessage += string.Format("数据错误——第{0}行数据井号与A2不一致！\n", rowNum);
                                //    break;
                                //}

                                if (CommonObject.DJSJ_JH_ID_Table(NY,cyc)[colValue] == null)
                                {
                                    canExcute = false;
                                    errorMessage += string.Format("数据错误——第{0}行数据井号{1}与系统不一致！\n", rowNum, colValue);
                                    break;
                                }
                            }
                            else if (colName == "ZYQ")
                            {
                                if (CommonObject.ZYQ_ID_Table[colValue] == null)
                                {
                                    canExcute = false;
                                    errorMessage += string.Format("数据错误——第{0}行数据作业区名称与A2不一致！\n", rowNum);
                                    break;
                                }
                                else
                                {
                                    colValue = CommonObject.ZYQ_ID_Table[colValue].ToString();
                                }
                            }
                            else if (colName == "QK")
                            {
                                //if (CommonObject.Area_ID_Table[colValue] == null)
                                //{
                                //    errorMessage += string.Format("数据异常——第{0}行数据区块名称与A2不一致！\n", rowNum);
                                //}
                                if (CommonObject.RushLocalAreaInfo(NY, cyc)[colValue] == null)
                                {
                                    errorMessage += string.Format("数据错误——第{0}行数据区块名称与系统不一致！\n", rowNum);
                                }

                            }
                            insertStr.Append(colName + " ,");
                            valueStr.Append("'" + colValue + "',");
                            if (colName == "ZYQ")
                            {
                                colValue = colValue + "$" + DT.Rows[rowNum][col - 1].ToString();
                                insertStr.Append("DJ_ID,ZQLC,");
                                valueStr.Append("'" + colValue + "',0,");

                            }
                        }
                    }
                    if (!canExcute)
                    {
                        continue;
                    }
                    insertStr.Append(" cyc_id)");
                    valueStr.Append(" '" + cyc + "')");

                    insertStr.Append(valueStr);
                    try
                    {
                        if (sqlhelper.ExcuteSql(insertStr.ToString()) > 0)
                        {
                            insertCount++;
                        }
                    }
                    catch (Exception e)
                    {
                        errorMessage += string.Format("SQL执行异常——{0}\n", e.Message);
                    }
                }
            }
            if (insertCount == DT.Rows.Count - 1)
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
                errorMessage = string.Format("总共{0}条数据，导入{1}条！\n异常信息：\n{2}", DT.Rows.Count - 1, insertCount, errorMessage);
            }
            return result;
        }
    }
}
