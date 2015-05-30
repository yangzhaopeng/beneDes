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
namespace beneDesYGS.api.system
{
    public partial class shny : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getSHNYList();
        }

        public void getSHNYList()
        {
            string type = Session["DEP_TYPE"].ToString();
            DataTable dt = shny_model.getSHNYList(type);
            _return(true, "", dt);
        }

    }
}
