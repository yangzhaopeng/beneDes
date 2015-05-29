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
/// str_validate 的摘要说明
/// </summary>
public class str_validate
{
	
	//判断一个字符串是否合乎要求，符合要求返回1，长度不是6返回2，包含非数字字符返回3
    //年份或月份不合法返回4.月份到13月
    public static int validate(String str)
    {
        String year, month;
        int ny, nm;
        if (str.Length != 6) return 2;
        
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] > '9' || str[i] < '0') return 3;
        }

        year = str.Substring(0, 4);
        month = str.Substring(4, 2);
        ny = Convert.ToInt32(year);
        nm = Convert.ToInt32(month);
        if (ny < 1945 || nm < 0 || nm > 13)
            return 4;
        else
            return 1;
    }
	
}
