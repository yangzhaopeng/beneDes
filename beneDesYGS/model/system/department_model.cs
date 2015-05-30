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
    public class department_model
    {
        public static DataTable getCYCList()
        {
            string sql = "select * from DEPARTMENT where DEP_TYPE='CYC' order by DEP_ID";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }
        public static DataTable getCQCList()
        {
            string sql = "select * from DEPARTMENT where DEP_TYPE='CQC' order by DEP_ID";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }
        //public static DataTable getZYQList(string cyc_id)
        //{
        //string sql = "select * from DEPARTMENT where DEP_TYPE='ZYQ' and PARENT_ID='" + cyc_id + "' order by DEP_ID";
        //SqlHelper sqlhelper = new SqlHelper();
        //return sqlhelper.GetDataSet(sql).Tables[0];
        //}
        /// <summary>
        /// 获取单位列表
        /// </summary>
        /// <param name="dep_type"></param>
        /// <returns></returns>
        public static DataTable getDepList(string dep_type)
        {
            string sql = "select DEP_ID, DEP_NAME from DEPARTMENT where DEP_TYPE='" + dep_type + "' order by DEP_ID";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }
    }
}
