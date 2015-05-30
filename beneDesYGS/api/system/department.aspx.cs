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
using beneDesYGS.core;

namespace beneDesYGS.api.system
{
    public partial class department : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _type = _getParam("_type");

            switch (_type)
            {
                case "getCYCList":
                    getCYCList();
                    break;
                case "getCQCList":
                    getCQCList();
                    break;
                case "getDepList":
                    getDepList();
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
        public void getCQCList()
        {
            DataTable dt = department_model.getCQCList();
            _return(true, "", dt);
        }
        /// <summary>
        /// 获取油或气类型的厂级单位列表
        /// </summary>
        public void getDepList()
        {
            DataTable dt = department_model.getDepList(Convert.ToString(Session["dep_type"]));
            _return(true, "", dt);
        }

        /// <summary>
        /// 获取某用户的采油厂下的所有作业区列表
        /// </summary>
        //public void getDepList()
        //{
        //DataTable dt = department_model.getZYQList(Session["depId"].ToString());
        //_return(true, "", dt);
        //}
    }
}
