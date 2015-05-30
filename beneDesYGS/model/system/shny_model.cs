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
using beneDesYGS.core;

namespace beneDesYGS.model.system
{
    public class shny_model
    {
        public static DataTable getSHNYList(string type)
        {
            string sql = @"select distinct ny as nyname, ny as nyid from shenhe_info where shenhe='1' and has_suo='1' 
                        and DEP_ID in (select DEP_ID from DEPARTMENT where DEP_TYPE='{0}') order by ny desc";
            sql = string.Format(sql, type);
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

    }
}
