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
using System.Net.Sockets;


/// <summary>
/// conn 的摘要说明
/// </summary>
public class conn
{
	public conn()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    static string strconn = ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString;
    public static OracleConnection oraconn()
    {
        OracleConnection conn = new OracleConnection(strconn);
        //Oracle.DataAccess.Client.OracleConnection conn = new Oracle.DataAccess.Client.OracleConnection();
        //conn.ConnectionString = "Data Source = orcl; User Id = qhcy1; Password = qhcy1 ";
        
        return conn;
    }
    #region 采用Socket方式，测试服务器连接
    /// <summary> 
    /// 采用Socket方式，测试服务器连接 
    /// </summary> 
    /// <param        <param       <param  
    /// <returns></returns> 
    public static bool TestConnection()
    {
        string host = strconn.Split(';')[0].Split('=')[1].Split('/')[0];
        int port=1521;
        int millisecondsTimeout = 2;
        TcpClient client = new TcpClient();
        try
        {
            var ar = client.BeginConnect(host, port, null, null);
            ar.AsyncWaitHandle.WaitOne(millisecondsTimeout);
            return true;// client.Connected;
        }
        catch (Exception e) 
        { 
            //throw e; 
            return false;
        }
        finally { client.Close(); }
    }
    #endregion
}
