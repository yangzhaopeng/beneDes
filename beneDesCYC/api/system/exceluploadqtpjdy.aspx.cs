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
    public partial class exceluploadqtpjdy : beneDesCYC.core.UI.corePage
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
            string sNewFileName = "PJDY" + DateTime.Now.ToString("yyyymmdd") + Session["userName"].ToString();
            postedFile.SaveAs(savePath + @"/" + sNewFileName + sExtension);

            string address = savePath + @"/" + sNewFileName + sExtension;
            GetSheet getsheetx = new GetSheet();
            string StyleSheet = getsheetx.GetExcelSheetNames(address);
            string msg = string.Empty;
            bool trueOrfalse = LoadData(StyleSheet, address, out msg);
            _return(trueOrfalse, msg, "null");

            //if (postedFile != null)
            //{ _return(true, "上传成功！", "null"); }
            //else
            //{ _return(false, "上传失败！", "null"); }

        }

        public bool LoadData(string StyleSheet, string address, out string msg)
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
                    msg = string.Format("第{0}行年月、区块名称不能为空！", j);
                }
            }
            string cyc = Session["cqc_id"].ToString();
            return FromTableToSql(DT, "pjdysj", cyc, out msg);
        }

        public bool FromTableToSql(DataTable DT, string tableName, string cyc, out string errorMessage)
        {
            errorMessage = "";
            bool result = false;
            List<int> keyNum = new List<int>();
            //循环获取主键所在列  主键标识：key：NY   key:CYC_ID
            for (int colNum = 0; colNum < DT.Columns.Count; colNum++)
            {
                if (DT.Columns[colNum].ToString().Contains("key:"))
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
                string PJDYMC = DT.Rows[rowNum][1].ToString();

                string chksql = "select count(1) from " + tableName;// +" where ny='" + NY + "' and pjdymc='" + PJDYMC + "' and cyc_id='" + cyc + "'";
                //拼凑本表的条件where语句：主键约束

                string whereSql = string.Empty;
                if (keyNum.Count > 0)
                {
                    whereSql += " where ";
                    for (int num = 0; num < keyNum.Count; num++)
                    {
                        int col = keyNum[num];//列

                        string colName = DT.Columns[col].ToString().Split(':')[1];
                        string colValue = DT.Rows[rowNum][col].ToString();
                        whereSql += colName + " ='" + colValue + "' and ";
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
                                //    updateStr.Append(colName + "='" + colValue + "',");
                                //}
                                //else
                                //{
                                //对特殊字段的处理
                                string[] colText = colName.Split(':');
                                colName = colText[1];
                                //updateStr.Append(colText[1] + "='" + colValue + "',");
                                //updateStr.Append(colText[0] + "='" + yqlx_info_model.getValue(colValue) + "',");
                                //updateStr.Append(colText[1] + "='" + xsyp_info_model.getValue(colValue) + "',");
                            }
                            colName = CommonFunctions.ConvertString(colName);
                            colValue = CommonFunctions.ConvertString(colValue);
                            colName = colName.ToUpper();
                            if (colName == "PJDYMC")
                            {
                                if (string.IsNullOrEmpty(colValue))
                                {
                                    canExcute = false;
                                    errorMessage += string.Format("数据异常——第{0}行评价单元名称为空！\n", rowNum);
                                    break;
                                }
                                //if (CommonObject.PJDY_ID_Table[colValue] == null)
                                //{
                                //    canExcute = false;
                                //    errorMessage += string.Format("数据错误——第{0}行数据评价单元名称与系统不一致！\n", rowNum);
                                //    break;
                                //}
                            }
                            else if (colName == "YCLX")
                            {
                                if (CommonObject.GetYCLX(cyc) == null || CommonObject.GetYCLX(cyc)[colValue] == null)
                                {
                                    canExcute = false;
                                    errorMessage += string.Format("数据错误——第{0}行数据油藏类型错误！\n", rowNum);
                                    break;
                                }
                            }
                            else if (colName == "SFPJ")
                            {
                                if (colValue == "是")
                                    colValue = "1";
                                else if (colValue == "否")
                                    colValue = "0";
                                else
                                {
                                    canExcute = false;
                                    errorMessage += string.Format("数据错误——第{0}行数据 是否评价 错误！\n", rowNum);
                                    break;
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

                            if (colName.Contains(':'))
                            {
                                //}
                                //else
                                //{
                                //对特殊字段的处理
                                string[] colText = colName.Split(':');
                                colName = colText[1];
                                //insertStr.Append(colText[1] + " ,");
                                //valueStr.Append("'" + colValue + "',");
                                //updateStr.Append(colText[0] + "='" + yqlx_info_model.getValue(colValue) + "',");
                                //updateStr.Append(colText[1] + "='" + xsyp_info_model.getValue(colValue) + "',");
                            }
                            colName = CommonFunctions.ConvertString(colName);
                            colValue = CommonFunctions.ConvertString(colValue);
                            colName = colName.ToUpper();
                            if (colName == "PJDYMC")
                            {
                                if (string.IsNullOrEmpty(colValue))
                                {
                                    canExcute = false;
                                    errorMessage += string.Format("数据异常——第{0}行评价单元名称为空！\n", rowNum);
                                    break;
                                }
                                //if (CommonObject.PJDY_ID_Table[colValue] == null)
                                //{
                                //    canExcute = false;
                                //    errorMessage += string.Format("数据错误——第{0}行数据评价单元名称与系统不一致！\n", rowNum);
                                //    break;
                                //}
                            }
                            else if (colName == "YCLX")
                            {
                                if (CommonObject.GetYCLX(cyc) == null || CommonObject.GetYCLX(cyc)[colValue] == null)
                                {
                                    canExcute = false;
                                    errorMessage += string.Format("数据错误——第{0}行数据油藏类型错误！\n", rowNum);
                                    break;
                                }
                            }
                            else if (colName == "SFPJ")
                            {
                                if (colValue == "是")
                                    colValue = "1";
                                else if (colValue == "否")
                                    colValue = "0";
                                else
                                {
                                    canExcute = false;
                                    errorMessage += string.Format("数据错误——第{0}行数据 是否评价 错误！\n", rowNum);
                                    break;
                                }
                            }
                            insertStr.Append(colName + " ,");
                            valueStr.Append("'" + colValue + "',");
                        }
                    }
                    if (!canExcute)
                    {
                        continue;
                    }
                    insertStr.Append("cyc_id)");
                    valueStr.Append("'" + cyc + "')");

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
