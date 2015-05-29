using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// FarpointGridChange 的摘要说明
/// </summary>
public static class FarpointGridChange
{

    public static void  FarPointChange(FarPoint.Web.Spread.FpSpread fpointname,string tablename)
    {
        
        FarPoint.Web.Spread.FpSpread saveEx = new FarPoint.Web.Spread.FpSpread();
        saveEx = fpointname;
        saveEx.Sheets[0].GridLines = GridLines.Both;
        int rows = saveEx.Sheets[0].RowCount;
        int columns = saveEx.Sheets[0].ColumnCount;
        for (int i = 2; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                saveEx.Sheets[0].Cells[i, j].Border.BorderSize = 2;
                saveEx.Sheets[0].Cells[i, j].Border.BorderStyle = BorderStyle.Solid;

            }
        }
        saveEx.SaveExcelToResponse(tablename);
        //saveEx.SaveExcelToResponse("\""+tablename+"\"");
    }
}
