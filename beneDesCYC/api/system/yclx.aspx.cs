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
    public partial class yclx : beneDesCYC.core.UI.corePage
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
        /// 获取所有油藏类型的下拉列表
        /// </summary>
        public void getAllList()
        {
            string _targetType = _getParam("_targetType");
            string cyc_id = Session["cyc_id"].ToString();
            //string cqc_id = Session["cqc_id"].ToString();
            DataTable dt = null;
            //if (_targetType == "qc")
            //    dt = yclx_model.getQCList(cqc_id);
            //else
            dt = yclx_model.getAllList(cyc_id);

            _return(true, "", dt);
        }
    }
}
