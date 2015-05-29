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
    public partial class bscpftcx : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getSJList();
        }

        public void getSJList()
        {
            string cyc = Session["cyc_id"].ToString();
            //string cyc = _getParam("CYC");
            string xzType = _getParam("xzType");
            //if (cyc == "quan")
            //{ _return(false, "请选择采油厂！", "null"); }
            //else
            //{
            DataTable dt = bscpftcx_model.getSJList(cyc, xzType);
            _return(true, "", dt);
            //}

        }

    }
}
