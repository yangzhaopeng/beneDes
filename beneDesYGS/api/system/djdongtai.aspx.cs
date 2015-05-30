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
    public partial class djdongtai : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getJDDTList();
        }

        public void getJDDTList()
        {
            string cyc = _getParam("CYC");
            string xzType = _getParam("xzType");
            if (cyc == "quan")
            { _return(false, "请选择采油厂！", "null"); }
            else
            {
                DataTable dt = djdongtai_model.getJDDTList(cyc, xzType);
                _return(true, "", dt);
            }

        }

    }
}
