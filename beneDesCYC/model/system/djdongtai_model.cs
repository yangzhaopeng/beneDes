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
    public class djdongtai_model
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
            //string sql = "select distinct " + xzType + " as SJ_ID, " + xzType + " as SJ_NAME from view_djfy where cyc_id='" + cyc + "'  order by " + xzType + " desc";
            //SqlHelper sqlhelper = new SqlHelper();
            //return sqlhelper.GetDataSet(sql).Tables[0];
            //}

            if (xzType == "yqlxmc")
            {
                string sql = "select distinct yqlxmc as SJ_ID, yqlxmc as SJ_NAME from yqlx_info,DTSTAT_DJSJ_all where DTSTAT_DJSJ_all.yqlx=yqlx_info.yqlxdm and cyc_id='" + cyc + "' ";
                SqlHelper sqlhelper = new SqlHelper();
                return sqlhelper.GetDataSet(sql).Tables[0];
            }
            else if (xzType == "xsypmc")
            {
                string sql = "select distinct xsypmc as SJ_ID, xsypmc as SJ_NAME from xsyp_info,DTSTAT_DJSJ_all where DTSTAT_DJSJ_all.xsyp=xsyp_info.xsypdm and cyc_id='" + cyc + "' ";
                SqlHelper sqlhelper = new SqlHelper();
                return sqlhelper.GetDataSet(sql).Tables[0];
            }
            else if (xzType == "jbmc")
            {
                string sql = "select distinct g.jbmc as SJ_ID, g.jbmc as SJ_NAME from dtxyjb_info g,DTSTAT_DJSJ_all j where j.gsxyjb_1=g.jbid and j.cyc_id='" + cyc + "' order by g.jbmc desc ";
                SqlHelper sqlhelper = new SqlHelper();
                return sqlhelper.GetDataSet(sql).Tables[0];
            }
            else
            {
                string sql = "select distinct " + xzType + " as SJ_ID, " + xzType + " as SJ_NAME from DTSTAT_DJSJ_all where cyc_id='" + cyc + "'  order by " + xzType + " desc";
                SqlHelper sqlhelper = new SqlHelper();
                return sqlhelper.GetDataSet(sql).Tables[0];
            }

        }
    }
}
