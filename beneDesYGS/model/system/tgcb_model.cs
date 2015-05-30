using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.OracleClient;

namespace beneDesYGS.model.system
{
    public class tgcb_model
    {
        public static string TGCB(string month)
        {
            string qctgcb = "";
            OracleConnection conx = DB.CreatConnection();
            conx.Open();
            string tgse = "select num from csdy_info where name = '特高成本井' and ny = '" + month + "'";
            OracleCommand tgcom = new OracleCommand(tgse, conx);
            OracleDataReader tgr = tgcom.ExecuteReader();
            tgr.Read();
            if (tgr.HasRows)
            {
                qctgcb = tgr[0].ToString();
            }
            

            tgr.Close();
            conx.Close();
            return qctgcb;

        }
    }
}
