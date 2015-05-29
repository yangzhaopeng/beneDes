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

namespace beneDesCYC.api.system
{
    public partial class bjwxy : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getBJList();
        }

        public void getBJList()
        {
          //  string cyc = _getParam("CYC");
            string cycid = Session["cyc_id"].ToString();
            //if (cyc == "quan")
            //{ _return(false, "请选择采油厂！", "null"); }
            //else
            //{
                DataTable dt = bjwxy_model.getBJList(cycid);
                _return(true, "", dt);
            //}

        }

    }
}
