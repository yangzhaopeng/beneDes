using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;

/// <summary>
/// DB 的摘要说明
/// </summary>
public class DB
{
	public static OracleConnection CreatConnection()
    {
        string StrCon = "";
        StrCon = ConfigurationManager.ConnectionStrings["cycConnectionString"].ToString();//得到WebConfig数据库连接字符串
        OracleConnection Con = new OracleConnection(StrCon);
        return Con;

    }
    public static OracleConnection CreatConnectionygs()
    {
        string StrCon = "";
        StrCon = ConfigurationManager.ConnectionStrings["ygsConnectionString"].ToString();//得到WebConfig数据库连接字符串
        OracleConnection Con = new OracleConnection(StrCon);
        return Con;

    }
   
}
