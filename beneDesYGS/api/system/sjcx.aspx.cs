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
    public partial class sjcx : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getSJList();
        }

        public void getSJList()
        {
            string cyc = _getParam("CYC");
            string yqclx = _getParam("yqclx");

            string cyc_id = cyc;
            string cqc_id = cyc;
            if (cyc.Contains(','))
            {
                cyc_id = cyc.Split(',')[0];
                cqc_id = cyc.Split(',')[1];
            }
            string xzType = _getParam("xzType");
            if (cyc == "[a-zA-Z]")
            { _return(false, "请选择采油厂！", "null"); }
            else
            {

                DataTable dt = sjcx_model.getSJList(cyc_id, cqc_id, xzType);
                _return(true, "", dt);
            }

        }

    }
}
