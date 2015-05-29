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
    public class dtstat_djsj_model
    {

        public static DataTable getZYQList(string cycid)
        {

            string sqlback = "select d.dep_id, d.dep_name  from jilincyc.department d ";
            sqlback += " where  d.dep_type='ZYQ' and  d.parent_id='" + cycid + "'order by d.dep_id ";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sqlback).Tables[0];
        }


        public static DataTable getPJDYList(string bny, string eny, string cycid)
        {

            string sqlback = "select distinct pjdy as dep_id, pjdy as dep_name from dtstat_djsj_all where dtstat_djsj_all.gsxyjb_1='4' and bny='" + bny + "' and eny='" + eny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "'";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sqlback).Tables[0];
        }

        public static DataTable getQKList(string bny, string eny, string cycid)
        {

            string sqlback = "select distinct qk as dep_id, qk as dep_name from dtstat_djsj_all where dtstat_djsj_all.gsxyjb_1='4' and bny='" + bny + "' and eny='" + eny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "' ";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sqlback).Tables[0];
        }
    }
}
