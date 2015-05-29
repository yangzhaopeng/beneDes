using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

/// <summary>
/// GetSheet 的摘要说明
/// 获取工作表的信息
/// </summary>
public class GetSheet
{
    public GetSheet()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public String GetExcelSheetNames(string excelFile)
    {
        OleDbConnection objConn = null;
        System.Data.DataTable dt = null;

        try
        {

            String connString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=" + excelFile + ";Extended Properties=Excel 8.0;";

            if (excelFile.EndsWith("xls", StringComparison.InvariantCultureIgnoreCase))
            {
                connString = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                @"Data Source=" + excelFile + ";" + "Extended Properties=\"Excel 8.0;HDR=No\"";
            }
            else if (excelFile.EndsWith("xlsx", StringComparison.InvariantCultureIgnoreCase))
            {
                connString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                    @"Data Source=" + excelFile + ";" +
                    "Extended Properties=\"Excel 12.0 Xml;HDR=No\"";
            }



            // Create connection object by using the preceding connection string.
            objConn = new OleDbConnection(connString);
            // Open connection with the database.
            objConn.Open();
            // Get the data table containg the schema guid.
            dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
            {
                return null;
            }

            String[] excelSheets = new String[dt.Rows.Count];
            int i = 0;

            // Add the sheet name to the string array.
            foreach (DataRow row in dt.Rows)
            {
                excelSheets[i] = row["TABLE_NAME"].ToString();
                i++;
            }
            string sheetx = excelSheets[0].Replace("'", "");
            sheetx = sheetx.Substring(0, sheetx.Length - 1);
            return sheetx;
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            // Clean up.
            if (objConn != null)
            {
                objConn.Close();
                objConn.Dispose();
            }
            if (dt != null)
            {
                dt.Dispose();
            }
        }
    }
}

