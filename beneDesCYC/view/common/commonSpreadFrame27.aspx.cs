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
using System.Data.OracleClient;
using beneDesCYC.model.system;

namespace beneDesCYC.view.common
{
    public partial class commonSpreadFrame27 : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string TGCB()
        {
            string date = Session["month"].ToString();
            string dt = tgcb_model.TGCB(date);
            return dt;
        }
    }
}
