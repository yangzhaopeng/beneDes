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
    public class jdstat_model
    {
        public static DataTable getJDList(string list)
        {
            //string sql = "select * from DEPARTMENT where DEP_TYPE='CYC' order by DEP_ID";
            //SqlHelper sqlhelper = new SqlHelper();
            string sql = "select d.dep_id, d.dep_name  from jilincyc.department d ";
            sql += " where  d.dep_type='ZYQ' and  d.parent_id='" + list + "'";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        public static DataTable getJD2List(string list,string bny,string eny)
        {
            //string sql = "select * from DEPARTMENT where DEP_TYPE='CYC' order by DEP_ID";
            string sql = "select distinct pjdy as dep_name, pjdy as dep_id from jdstat_djsj_all where jdstat_djsj_all.gsxyjb='5' and bny='" + bny + "' and eny='" + eny + "' and jdstat_djsj_all.dep_id = '" + list + "'";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        public static DataTable getJD3List(string list,string bny,string eny)
        {
            //string sql = "select * from DEPARTMENT where DEP_TYPE='CYC' order by DEP_ID";
            string sql = "select distinct qk as dep_name, qk as dep_id from jdstat_djsj_all where jdstat_djsj_all.gsxyjb='5' and bny='" + bny + "' and eny='" + eny + "' and jdstat_djsj_all.dep_id = '" + list + "'";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }
    }
}
