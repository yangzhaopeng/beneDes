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
using beneDesCYC.core;

namespace beneDesCYC.api.system
{
    public partial class bscplx_info : beneDesCYC.core.UI.corePage
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
        /// 获取所有伴生产品类型的下拉列表
        /// </summary>
        public void getAllList()
        {
            string dep_type = Convert.ToString(Session["DEP_TYPE"]);
            DataTable dt = bscplx_info_model.getAllList(dep_type);
            _return(true, "", dt);
        }
    }
}
