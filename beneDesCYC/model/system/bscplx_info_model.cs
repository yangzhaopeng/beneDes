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
    public class bscplx_info_model
    {
        public static DataTable getAllList()
        {
            string sql = "select * from BSCPLX_INFO order by BSCPDM";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        public static DataTable getAllList(string dep_type)
        {
            string sql = string.Format("select * from BSCPLX_INFO where dep_type='{0}' order by BSCPDM", dep_type);
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }
    }
}

