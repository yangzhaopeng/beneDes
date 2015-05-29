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
    public partial class xsyp_info : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _type = _getParam("_type");

            switch (_type)
            {
                case "getAllList":
                    getAllList();
                    break;
            }
        }

        /// <summary>
        /// 获取所有销售油品的下拉列表
        /// </summary>
        public void getAllList()
        {
            DataTable dt = xsyp_info_model.getAllList();
            _return(true, "", dt);
        }
    }
}
