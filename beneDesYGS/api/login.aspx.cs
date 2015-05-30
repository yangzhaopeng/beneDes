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
using beneDesYGS.model.system;

namespace beneDesYGS.api
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = Request["name"];
            string word = Request["word"];
            string month = Request["month"];

            Hashtable ok = user_model.login(name, word,month);
            
            if ((bool)ok["ok"])
            {
                Session.Timeout = 99999;                                                                
                Session["userId"] = name;                                                              
                Session["month"] = month;                                                               
                Session["userName"] = ok["userName"].ToString();
                Session["data_types"] = ok["data_types"].ToString();

                string data_type = ok["data_types"].ToString().Split(',')[0];
                //Session["data_type"] = data_type;
                Session["DEP_TYPE"] = data_type;

                //Session["cyc_id"] = ok["depId"].ToString();                                          
                //Session["cqcId"] = ok["depId"].ToString();                                            
                Session["depName"] = ok["depName"].ToString();                                        
                //Session["cycName"] = ok["depName"].ToString();                                     
                Session["roleId"] = ok["roleId"].ToString();


                                                                                                     
                Session["bny"] = ok["bny"].ToString();                                               
                Session["eny"] = ok["eny"].ToString();                                               
                                                                                                     
                Session["jbny"] = ok["jbny"].ToString();                                             
                Session["jeny"] = ok["jeny"].ToString();                                             
                                                                                                     
                Response.Write("{\"success\": true, \"msg\": null}");                                   
            }                                                                                           
            else                                                                                        
            {                                                                                           
                //Response.Write("{\"success\": false, \"msg\": \"用户名或密码错误！\"}");              
                Response.Write("{\"success\": false, \"msg\": \"" + ok["msg"] + "\"}");
            }
        }
    }
}
