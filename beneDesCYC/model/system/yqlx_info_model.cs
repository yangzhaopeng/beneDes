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
    public class yqlx_info_model
    {
        public static DataTable getAllList()
        {
            string sql = "select * from YQLX_INFO order by YQLXDM";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        internal static string getValue(string colValue)
        {
            string sql = "select YQLXDM from YQLX_INFO where YQLXMC='" + colValue + "'";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetExecuteScalar(sql).ToString();
        }
    }
}
