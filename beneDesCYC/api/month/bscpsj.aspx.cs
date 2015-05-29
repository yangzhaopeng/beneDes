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
    public partial class bscpsj : beneDesCYC.core.UI.corePage
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
            DataTable dt = null;
            string DEP_TYPE = Convert.ToString( Session["DEP_TYPE"]);
            try
            {
                dt = bscpsj_model.getAllList(Session["cyc_id"].ToString(), Session["month"].ToString(), DEP_TYPE);
            }
            catch
            {
                dt = null;
            }
            _return(true, "", dt);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public void add()
        {
            string NY = _getParam("NY", true);
            string BSCPDM = _getParam("BSCPDM", true);
            string CL = _getParam("CL", true);
            string SPL = _getParam("SPL", true);
            string JG = _getParam("JG", true);
            string XSSJ = _getParam("XSSJ", true);
            string CYC_ID = Session["cyc_id"].ToString();

            DataTable dt = bscpsj_model.getOneBSCPSJ(NY, BSCPDM, CYC_ID);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该伴生产品数据已存在，您可以进行修改！", "null");
            }

            if (bscpsj_model.add(NY, BSCPDM, CL, SPL, JG, XSSJ, CYC_ID))
            {
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加伴生产品数据失败！", "null");
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public void edit()
        {
            string NY = _getParam("NY", true);
            // string DEP_ID = _getParam("DEP_ID", true);
            string BSCPDM = _getParam("BSCPDM", true);
            string CL = _getParam("CL", true);
            string SPL = _getParam("SPL", true);
            string JG = _getParam("JG", true);
            string XSSJ = _getParam("XSSJ", true);
            string CYC_ID = Session["cyc_id"].ToString();

            if (bscpsj_model.edit(NY, BSCPDM, CL, SPL, JG, XSSJ, CYC_ID))
            {
                _return(true, "编辑伴生产品数据成功！", "null");
            }
            else
            {
                _return(false, "编辑伴生产品数据失败！", "null");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void delete()
        {
            string NY = _getParam("NY", true);
            //string YQSPL = _getParam("YQSPL", true);
            // string DEP_ID = _getParam("DEP_ID", true);
            string BSCPDM = _getParam("BSCPDM", true);

            string CYC_ID = Session["cyc_id"].ToString();

            if (bscpsj_model.delete(NY, BSCPDM, CYC_ID))
            {
                _return(true, "删除伴生产品数据成功！", "null");
            }
            else
            {
                _return(false, "删除伴生产品数据失败！", "null");
            }
        }
    }
}
