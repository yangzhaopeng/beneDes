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
using beneDesCYC.core;

namespace beneDesCYC.model.system
{
    public class xsyp_info_model
    {
        public static DataTable getAllList()
        {
            string sql = "select * from XSYP_INFO order by XSYPDM";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        internal static string getValue(string colValue)
        {
            string sql = "select xsypdm from XSYP_INFO where XSYPMC='" + colValue + "'";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetExecuteScalar(sql).ToString();
        }
    }
}
