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
/// isFloat 的摘要说明
/// </summary>
public static class isNotFloat
{
	
    public static bool Isdouble(string str)
    {
        try
        {
            float ots = float.Parse(str);
            return true;
        }
        catch
        {
            return false;
        }

    }

}
