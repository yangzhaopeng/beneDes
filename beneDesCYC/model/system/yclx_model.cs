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
    public class yclx_model
    {
        public static DataTable getAllList(string cyc_id)
        {
            string sql = "select * from YCLX  where cyc_id='" + cyc_id + "'";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        internal static DataTable getQCList(string cyc_id)
        {
            string sql = "select * from YCLX where substr(YCLX,-2,2)<>'油藏' and cyc_id='" + cyc_id + "'";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }
    }
}
