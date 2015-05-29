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
    public class bjwxy_model
    {
        public static DataTable getBJList(string cycid)
        {
            //string sql = "select distinct jh as JH_ID, jh as JH_NAME from dtstat_djsj sdy where sdy.dep_id = '" + cyc + "'and  sdy.gsxyjb_1 = '3' union ";
            //sql += "select distinct jh as JH_ID, jh as JH_NAME from dtstat_djsj sdy where sdy.dep_id = '" + cyc + "'and  sdy.gsxyjb_1 = '4'";
            string sql = "select distinct jh as JH_ID, jh as JH_NAME from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and ( sdy.gsxyjb_1 = '3' or sdy.gsxyjb_1 = '4' ) ";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];

        }
    }
}
