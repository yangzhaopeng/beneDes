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
using System.Collections.Generic;

namespace beneDesCYC.api.month
{
    public partial class xsypRelateY : beneDesCYC.core.UI.corePage
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
                    deleteMultiRow();
                    break;
            }
        }


        public void getAllList()
        {
            DataTable dt = null;
            try
            {
                dt = xsypRelateY_model.getAllList(Session["cyc_id"].ToString(), Session["month"].ToString());
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
            string XSYPDM = _getParam("XSYPDM", true);
            string JG = _getParam("JG", true);
            string SJ = _getParam("SJ", true);
            string DTB = _getParam("DTB", true);
            string YQSPL = _getParam("YQSPL", true);
            string ZYS = _getParam("ZYS", true);

            string CYC_ID = Session["cyc_id"].ToString();

            DataTable dt = xsypRelateY_model.getOneXSYP(NY, XSYPDM, CYC_ID);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该销售参数已存在，您可以进行修改！", "null");
            }

            if (xsypRelateY_model.add(NY, XSYPDM, DTB,JG, SJ,YQSPL,ZYS, CYC_ID))
            {
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加销售参数失败！", "null");
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public void edit()
        {
            string NY = _getParam("NY", true);
            // string DEP_ID = _getParam("DEP_ID", true);
            string XSYPDM = _getParam("XSYPDM", true);
            string DTB = _getParam("DTB", true);
            string JG = _getParam("JG", true);
            string SJ = _getParam("SJ", true);
            string YQSPL = _getParam("YQSPL", true);
            string ZYS = _getParam("ZYS", true);

            string CYC_ID = Session["cyc_id"].ToString();

            if (xsypRelateY_model.edit(NY, XSYPDM,DTB, JG, SJ,YQSPL,ZYS, CYC_ID))
            {
                _return(true, "编辑销售参数成功！", "null");
            }
            else
            {
                _return(false, "编辑销售参数失败！", "null");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void delete()
        {
            string NY = _getParam("NY", true);
            string XSYPDM = _getParam("XSYPDM", true);

            string CYC_ID = Session["cyc_id"].ToString();

            if (xsypRelateY_model.delete(NY, XSYPDM, CYC_ID))
            {
                _return(true, "删除销售参数成功！", "null");
            }
            else
            {
                _return(false, "删除销售参数失败！", "null");
            }
        }
        /// <summary>
        /// 删除多条数据
        /// </summary>
        public void deleteMultiRow()
        {
            string[] NYArray = _getParam("NYS", true).Split(',');
            List<string> nys = new List<string>();
            nys.AddRange(NYArray);

            string[] XSYPDMArray = _getParam("XSYPDMS", true).Split(',');
            List<string> xsypdms = new List<string>();
            xsypdms.AddRange(XSYPDMArray);

            string CYC_ID = Session["cyc_id"].ToString();


            if (xsypRelateY_model.deleteMutilRow(nys, xsypdms, CYC_ID))
            {
                _return(true, "删除销售参数成功！", "null");
            }
            else
            {
                _return(false, "删除销售参数失败！", "null");
            }
        }
    }
}
