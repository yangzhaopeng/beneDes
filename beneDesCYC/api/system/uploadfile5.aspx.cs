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
    public partial class uploadfile5 : beneDesCYC.core.UI.corePage
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
            LoadData(StyleSheet, address);

            if (postedFile != null)
            { _return(true, "上传成功！", "null"); }
            else
            { _return(false, "上传失败！", "null"); }

        }

        public void LoadData(string StyleSheet, string address)
        {
            //打开指定摸版

            string address1 = Server.MapPath("./excel") + "\\zyqfeiyongmoban.xls";



            string strCon1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" + address1 + " ;Extended Properties=Excel 8.0";
            OleDbConnection myConn1 = new OleDbConnection(strCon1);
            myConn1.Open();   //打开数据链接，得到一个数据集     
            DataSet myDataSet1 = new DataSet();   //创建DataSet对象     
            string StrSql1 = "select   *   from   [" + StyleSheet + "$]";
            OleDbDataAdapter myCommand1 = new OleDbDataAdapter(StrSql1, myConn1);
            myCommand1.Fill(myDataSet1, "[" + StyleSheet + "$]");
            myCommand1.Dispose();
            DataTable DT1 = myDataSet1.Tables["[" + StyleSheet + "$]"];
            myConn1.Close();
            myCommand1.Dispose();



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
            string cyc = Session["cyc_id"].ToString();
            conn.Open();
            //导入重复数据时候,先将原来数据删除
            for (int l = 2; l < DT.Columns.Count; l++)
            {
                // l代表费用开始的列
                for (int n = 0; n < DT.Rows.Count; n++)
                {
                    //n代表费用开始的行
                    string source_id1 = DT.Rows[n][1].ToString();
                    string deletesql = "delete from ggfy where ny='" + DT.Rows[n][0] + "' and dep_id='" + source_id1 + "' and source_id='" + source_id1 + "' and fee_class='" + DT1.Rows[0][l] + "' and fee_code='" + DT1.Rows[1][l] + "' and cyc_id='" + cyc + "'";

                    OracleCommand cmd1 = new OracleCommand(deletesql, conn);
                    cmd1.ExecuteNonQuery();
                }
            }
            string ft_type = "ZYQ";
            double zqlc = 0;
            //查找source_id,dj_id
            for (int j = 2; j < DT.Columns.Count; j++)
            {
                //j表示费用开始的列
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    //i表示费用开始的行
                    if ((DT.Rows[i][j]).GetType() == typeof(Double))
                    {
                        if (Convert.ToDouble(DT.Rows[i][j]) != 0)
                        {
                            string source_id = DT.Rows[i][1].ToString();



                            //string sql = "insert into testfee values('" + DT.Rows[i][0] + "','" + DT1.Rows[1][j] + "','" + DT.Rows[i][j] + "','" + DT1.Rows[0][j] + "','" + DT.Rows[i][1] + "','" + DT.Rows[i][2] + "','" + ft_type + "','" + zqlc + "','" + source_id + "')";
                            string sql = "insert into ggfy values('" + DT.Rows[i][0] + "','" + DT.Rows[i][1] + "','" + ft_type + "','" + DT.Rows[i][1] + "','" + DT1.Rows[0][j] + "','" + DT1.Rows[1][j] + "','" + DT.Rows[i][j] + "','" + cyc + "')";
                            OracleCommand comm = new OracleCommand(sql, conn);
                            comm.ExecuteNonQuery();
                        }


                    }

                }
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('导入成功!');</script>");
            conn.Close();



        }
    }
}
