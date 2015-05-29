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
    public partial class department : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _type = _getParam("_type");

            switch (_type)
            {
                case "getCYCList":
                    getCYCList();
                    break;
                case "getZYQList":
                    getZYQList();
                    break;
            }
        }

        /// <summary>
        /// 获取所有的采油厂下拉列表
        /// </summary>
        public void getCYCList()
        {
           

            DataTable dt = department_model.getCYCList();
            _return(true, "", dt);
        }

        /// <summary>
        /// 获取某用户的采油厂下的所有作业区列表
        /// </summary>
        public void getZYQList()
        {
            DataTable dt = department_model.getZYQList(Session["depId"].ToString());
            _return(true, "", dt);
        }
    }
}
