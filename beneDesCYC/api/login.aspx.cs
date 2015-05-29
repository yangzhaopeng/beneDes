using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using beneDesCYC.model.system;

namespace beneDesCYC.api
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = Request["name"];
            string word = Request["word"];
            string month = Request["month"];

            Hashtable ok = user_model.login(name, word, month);

            if ((bool)ok["ok"])
            {
                Session.Timeout = 99999;
                Session["userId"] = name;
                Session["month"] = month;
                Session["userName"] = ok["userName"].ToString();
                Session["depId"] = ok["depId"].ToString();

                //Session["cycId"] = ok["depId"].ToString();//(Date:14.10.17;Change:wcx)兼容上一版本代码
                //string[] cyc_ids = ok["depId"].ToString().Split(',');
                string[] cyc_ids = ok["cycid"].ToString().Split(',');
                Session["cyc_id"] = cyc_ids[0];
                Session["cqc_id"] = cyc_ids[0];
                if (cyc_ids.Length > 1)
                    Session["cqc_id"] = cyc_ids[1];

                Session["DEP_TYPE"] = ok["DEP_TYPE"].ToString();
                Session["depName"] = ok["depName"].ToString();
                Session["cycName"] = ok["depName"].ToString();
                Session["roleId"] = ok["roleId"].ToString();

                Session["bny"] = ok["bny"].ToString();
                Session["eny"] = ok["eny"].ToString();

                Session["jbny"] = ok["jbny"].ToString();
                Session["jeny"] = ok["jeny"].ToString();
                Response.Write("{\"success\": true, \"msg\": null}");
            }
            else
            {
                Response.Write("{\"success\": false, \"msg\": \"" + ok["msg"] + "\"}");
            }
        }
    }
}
