using System;
using System.Collections;
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
using beneDesCYC.core;
using System.Data.OracleClient;

namespace beneDesCYC.view.common
{
    public partial class commonSpreadFrame57 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string tgcb = Request.QueryString["action"];
                if (!string.IsNullOrEmpty(tgcb))
                {
                    Response.Clear();
                    Response.Write(getTgcb());
                    Response.End();
                }
            }
        }

        private string getTgcb()
        {
            string result =string.Empty;
            SqlHelper conn = new SqlHelper();
            OracleConnection con0 = conn.GetConn();
            con0.Open();
            string tgse = "select num from csdy_info where name = '特高成本井' and ny = '" + Session["month"].ToString() + "'";
            OracleCommand tgcom = new OracleCommand(tgse, con0);
            OracleDataReader tgr = tgcom.ExecuteReader();
            try
            {
                if (tgr.Read())
                {
                    result = tgr[0].ToString();
                }

            }
            catch (Exception ex)
            { }

            return result;
        }
    }
}
