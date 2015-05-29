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
using beneDesCYC.model.month;
using beneDesCYC.model.system;

namespace beneDesCYC.api.month
{
    public partial class zyqRelateY : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _type = _getParam("_type");

            switch (_type)
            {
                case "getAllList":
                    getAllList();
                    break;
                case "add":
                    add();
                    break;
                case "edit":
                    edit();
                    break;
                case "delete":
                    delete();
                    break;
            }
        }


        public void getAllList()
        {
            DataTable dt = zyqRelateY_model.getAllList(Session["cyc_id"].ToString(), Session["month"].ToString());
            _return(true, "", dt);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public void add()
        {
            string NY = _getParam("NY", true);
            string DEP_ID = _getParam("DEP_ID", true);
            string XSYPDM = _getParam("XSYPDM", true);
            string SC = _getParam("SC", true);

            string CYC_ID = Session["cyc_id"].ToString();

            DataTable dt = zyqRelateY_model.getOneSC(NY, DEP_ID, XSYPDM, CYC_ID);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该输差已存在，您可以进行修改！", "null");
            }

            if (zyqRelateY_model.add(NY, DEP_ID, XSYPDM, SC, CYC_ID))
            {
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加输差失败！", "null");
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public void edit()
        {
            string NY = _getParam("NY", true);
            string DEP_ID = _getParam("DEP_ID", true);
            string XSYPDM = _getParam("XSYPDM", true);
            string SC = _getParam("SC", true);

            string CYC_ID = Session["cyc_id"].ToString();

            if (zyqRelateY_model.edit(NY, DEP_ID, XSYPDM, SC, CYC_ID))
            {
                _return(true, "编辑输差成功！", "null");
            }
            else
            {
                _return(false, "编辑输差失败！", "null");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void delete()
        {
            string NY = _getParam("NY", true);
            string DEP_ID = _getParam("DEP_ID", true);
            string XSYPDM = _getParam("XSYPDM", true);

            string CYC_ID = Session["cyc_id"].ToString();

            if (zyqRelateY_model.delete(NY, DEP_ID, XSYPDM, CYC_ID))
            {
                _return(true, "删除输差成功！", "null");
            }
            else
            {
                _return(false, "删除输差失败！", "null");
            }
        }
    }
}
