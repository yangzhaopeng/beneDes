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
    public partial class exceluploadQTQK : beneDesCYC.core.UI.corePage
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
            string sNewFileName = "QK" + DateTime.Now.ToString("yyyymmdd") + Session["userName"].ToString();
            postedFile.SaveAs(savePath + @"/" + sNewFileName + sExtension);

            string address = savePath + @"/" + sNewFileName + sExtension;
            GetSheet getsheetx = new GetSheet();
            string StyleSheet = getsheetx.GetExcelSheetNames(address);

            string Msg = string.Empty;
            bool trueOrfalse = LoadData(StyleSheet, address, out Msg);
            _return(trueOrfalse, Msg, "null");

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
            msg = "";
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
                    Response.Write("<script>alert('第:" + j + "行年月、区块名称不能为空')</script>");
                    msg += string.Format("第{0}行年月、区块名称不能为空", j);
                }
            }
            if (string.IsNullOrEmpty(msg))
                return FromTableToSql(DT, out msg);
            else
                return false;
        }

        public bool FromTableToSql(DataTable DT, out string errorMessage)
        {
            string cyc = Session["cqc_id"].ToString();
            errorMessage = string.Empty;
            List<string> sqlList = new List<string>();
            #region 拼接SqlList

            for (int rowNum = 1; rowNum < DT.Rows.Count; rowNum++)
            {
                //1、读取到excel，将首行作为列名   
                //2、从第三行开始数据行，找到其中主键，验证是否存在于数据库中，若存在则执行update，拼凑update语句，否则拼凑insert语句
                //3、拼凑过程：取第一行数据，作为列名，取数据行对应列，作为数据。


                string NY = DT.Rows[rowNum][0].ToString();
                string QKMC = DT.Rows[rowNum][1].ToString();

                string chksql = "select count(3) from qksj where ny='" + NY + "' and qkmc='" + QKMC + "' and cyc_id='" + cyc + "'";
                int rows = int.Parse(sqlhelper.GetExecuteScalar(chksql).ToString());

                bool canExcute = true;
                //拼凑update、insert语句
                if (rows > 0)
                {

                    StringBuilder updateStr = new StringBuilder();
                    updateStr.Append("update qksj set ");
                    //循环对每一列进行处理

                    for (int col = 0; col < DT.Columns.Count; col++)
                    {
                        if (DT.Rows[rowNum][col] != null)
                        {
                            string colName = DT.Columns[col].ToString();
                            string colValue = DT.Rows[rowNum][col].ToString();
                            if (colName.Contains(':'))
                            {
                                //对主键字段的处理
                                string[] colText = colName.Split(':');
                                colName = colText[1];
                            }
                            colName = CommonFunctions.ConvertString(colName);
                            colValue = CommonFunctions.ConvertString(colValue);
                            colName = colName.ToUpper();
                            if (colName == "QKMC")
                            {
                                if (string.IsNullOrEmpty(colValue))
                                {
                                    canExcute = false;
                                    errorMessage += string.Format("数据异常——第{0}行数据区块名称为空！\n", rowNum);
                                    break;
                                }
                                //if (CommonObject.Area_ID_Table[colValue] == null)
                                //{
                                //    errorMessage += string.Format("数据异常——第{0}行数据区块名称与A2不一致！\n", rowNum);
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
                                if (colValue.Trim() == "是")
                                {
                                    colValue = "1";

                                }
                                else if (colValue == "否")
                                {
                                    colValue = "0";
                                }
                                else
                                {
                                    canExcute = false;
                                    errorMessage += string.Format("数据错误——第{0}行数据 是否评价 错误！\n", rowNum);
                                    break;
                                }
                            }
                            if (!colName.Contains(','))
                            {
                                updateStr.Append(colName + "='" + colValue + "',");
                            }
                            else
                            {
                                //对特殊字段的处理
                                string[] colText = colName.Split(',');
                                updateStr.Append(colText[0] + "='" + yqlx_info_model.getValue(colValue) + "',");
                                updateStr.Append(colText[1] + "='" + xsyp_info_model.getValue(colValue) + "',");
                            }
                        }

                    }
                    if (!canExcute)
                    {
                        continue;
                    }
                    updateStr.Append(" cyc_id='" + cyc + "' where cyc_id='" + cyc + "' and NY= '" + NY + "' and qkmc='" + QKMC + "'");
                    sqlList.Add(updateStr.ToString());
                    //try
                    //{
                    //    sqlhelper.ExcuteSql(updateStr.ToString());
                    //}
                    //catch (Exception e)
                    //{
                    //    Response.Write("<script>alert('格式有错误!" + e + "')</script>");
                    //}
                }
                else
                {
                    StringBuilder insertStr = new StringBuilder();
                    StringBuilder valueStr = new StringBuilder();

                    insertStr.Append("insert into qksj( ");
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
                            colName = colName.ToUpper();
                            if (colName == "QKMC")
                            {
                                if (string.IsNullOrEmpty(colValue))
                                {
                                    canExcute = false;
                                    errorMessage += string.Format("数据异常——第{0}行数据区块名称为空！\n", rowNum);
                                    break;
                                }
                                //if (CommonObject.Area_ID_Table[colValue] == null)
                                //{
                                //    errorMessage += string.Format("数据异常——第{0}行数据区块名称与A2不一致！\n", rowNum);
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
                                if (colValue.Trim() == "是")
                                {
                                    colValue = "1";

                                }
                                else if (colValue == "否")
                                {
                                    colValue = "0";
                                }
                                else
                                {
                                    canExcute = false;
                                    errorMessage += string.Format("数据错误——第{0}行数据 是否评价 错误！\n", rowNum);
                                    break;
                                }
                            }
                            if (!colName.Contains(','))
                            {
                                insertStr.Append(colName + " ,");
                                valueStr.Append("'" + colValue + "',");
                            }
                            else
                            {

                            }
                        }
                    }
                    if (!canExcute)
                    {
                        continue;
                    }
                    insertStr.Append("cyc_id)");
                    valueStr.Append("'" + cyc + "')");
                    insertStr.Append(valueStr);
                    sqlList.Add(insertStr.ToString());
                }

            }
            bool result = false;
            #endregion
            try
            {
                if (sqlhelper.ExecuteTranErrorCount(sqlList) == -1)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                errorMessage += string.Format("SQL执行异常——{0}\n", e.Message);
            }
            if (result && sqlList.Count == DT.Rows.Count-1)
            {
                if (string.IsNullOrEmpty(errorMessage))
                {
                    errorMessage = string.Format("数据导入成功，共导入数据{0}条！", sqlList.Count);
                }
                else
                {
                    errorMessage = string.Format("{0}条数据全部导入，但存在异常数据。\n异常信息：\n{1}", sqlList.Count, errorMessage);
                }
            }
            else
            {
                result = false;
                errorMessage = string.Format("总共{0}条数据，导入0条！\n异常信息：\n{2}", DT.Rows.Count - 1, sqlList.Count, errorMessage);
            }
            return result;

        }

    }

}

