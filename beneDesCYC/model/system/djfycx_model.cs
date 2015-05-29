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
    public class djfycx_model
    {
        public static DataTable getSJList(string cyc, string xzType)
        {
            //if (xzType == "yqlxmc")
            //{
            //    string sql = "select distinct yqlxmc as SJ_ID, yqlxmc as SJ_NAME from yqlx_info,STAT_DJYDSJ where STAT_DJYDSJ.yqlx=yqlx_info.yqlxdm and dep_id='" + cyc + "' ";
            //    SqlHelper sqlhelper = new SqlHelper();
            //    return sqlhelper.GetDataSet(sql).Tables[0];
            //}
            //else if (xzType == "xsypmc")
            //{
            //    string sql = "select distinct xsypmc as SJ_ID, xsypmc as SJ_NAME from xsyp_info,STAT_DJYDSJ where STAT_DJYDSJ.xsyp=xsyp_info.xsypdm and dep_id='" + cyc + "' ";
            //    SqlHelper sqlhelper = new SqlHelper();
            //    return sqlhelper.GetDataSet(sql).Tables[0];
            //}
            //else
            //{
            string sql = "select distinct " + xzType + " as SJ_ID, " + xzType + " as SJ_NAME from view_djfy where cyc_id='" + cyc + "'  order by " + xzType + " desc";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
            //}



        }
    }
}
