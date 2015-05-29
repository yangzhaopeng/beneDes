using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient ;
/// <summary>
/// dbconn 的摘要说明
/// </summary>
public class dbconn
{
	public dbconn()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public static OracleConnection oraconn()
    {
        string constr = "data source = orcl1; user = jilincyc; password = jilincyc";
        OracleConnection fpconn = new OracleConnection(constr);
        return fpconn;
    }
}
