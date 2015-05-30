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
    public class jdsjcx_model
    {
        public static DataTable getJDSJList(string cyc, string xzType)
        {
            if (xzType == "yqlxmc")
            {
                string sql = "select distinct yqlxmc as SJ_ID, yqlxmc as SJ_NAME from yqlx_info,JDSTAT_DJSJ_all where JDSTAT_DJSJ_all.yqlx=yqlx_info.yqlxdm and dep_id='" + cyc + "' ";
                SqlHelper sqlhelper = new SqlHelper();
                return sqlhelper.GetDataSet(sql).Tables[0];
            }
            else if (xzType == "xsypmc")
            {
                string sql = "select distinct xsypmc as SJ_ID, yqlxmc as SJ_NAME from xsyp_info,JDSTAT_DJSJ_all where JDSTAT_DJSJ_all.xsyp=xsyp_info.xsypdm and dep_id='" + cyc + "' ";
                SqlHelper sqlhelper = new SqlHelper();
                return sqlhelper.GetDataSet(sql).Tables[0];
            }
            else if (xzType == "xymc")
            {
                string sql = "select distinct g.xymc as SJ_ID, g.xymc as SJ_NAME from gsxylb_info g,JDSTAT_DJSJ_all j where j.gsxyjb=g.xyjb and j.dep_id='" + cyc + "' order by g.xymc desc ";
                SqlHelper sqlhelper = new SqlHelper();
                return sqlhelper.GetDataSet(sql).Tables[0];
            }
            else if (xzType == "jbname")
            {
                string sql = "select jbname as SJ_ID, jbname as SJ_NAME from jb_info ";
                SqlHelper sqlhelper = new SqlHelper();
                return sqlhelper.GetDataSet(sql).Tables[0];
            }
            else
            {
                string sql = "select distinct " + xzType + " as SJ_ID, " + xzType + " as SJ_NAME from JDSTAT_DJSJ_all where dep_id='" + cyc + "'  order by " + xzType + " desc";
                SqlHelper sqlhelper = new SqlHelper();
                return sqlhelper.GetDataSet(sql).Tables[0];
            }

        }
    }
}
