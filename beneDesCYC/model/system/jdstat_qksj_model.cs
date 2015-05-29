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
    public class jdstat_qksj_model
    {
      /*  public static DataTable getCYCList()
        {
            string sql = "select * from DEPARTMENT where DEP_TYPE='CYC' order by DEP_ID";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }
*/
        public static DataTable getQKList(string cyc_id)
        {
            string sql = "select distinct mc as QKMC, mc as QKID from jdstat_qksj sdy where sdy.cyc_id = '" + cyc_id + "'";
            //string sql = "select * from DEPARTMENT where DEP_TYPE='ZYQ' and PARENT_ID='" + cyc_id + "' order by DEP_ID";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }
    }
}

