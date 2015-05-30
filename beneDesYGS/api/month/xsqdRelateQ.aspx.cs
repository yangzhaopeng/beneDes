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
using beneDesYGS.model.month;
using beneDesYGS.model.system;
using System.Collections.Generic;
using beneDesYGS.core;

namespace beneDesYGS.api.month
{
    public partial class xsqdRelateQ : beneDesYGS.core.UI.corePage
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
                case "distribute":
                    distribute();
                    break;
            }
        }


        public void getAllList()
        {
            DataTable dt = null;
            try
            {
                dt = xsqdRelateQ_model.getAllList(Session["DEP_TYPE"].ToString(), Session["month"].ToString());
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
            string DTB = _getParam("DTB", true);
            string JG = _getParam("JG", true);
            string YQSPL = _getParam("YQSPL", true);
            string ZYS = _getParam("ZYS", true);
            string SJ = _getParam("SJ", true);

            string DEP_TYPE = Session["DEP_TYPE"].ToString();

            DataTable dt = xsqdRelateQ_model.getOneXSYP(NY, XSYPDM, DEP_TYPE);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该销售参数已存在，您可以进行修改！", "null");
            }

            if (xsqdRelateQ_model.add(NY, XSYPDM, DTB, JG, SJ, YQSPL, ZYS, DEP_TYPE))
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
            string YQSPL = _getParam("YQSPL", true);
            string ZYS = _getParam("ZYS", true);
            string SJ = _getParam("SJ", true);
            string DEP_TYPE = Session["DEP_TYPE"].ToString();

            if (xsqdRelateQ_model.edit(NY, XSYPDM, DTB, JG, SJ, YQSPL, ZYS, DEP_TYPE))
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

            string DEP_TYPE = Session["DEP_TYPE"].ToString();

            if (xsqdRelateQ_model.delete(NY, XSYPDM, DEP_TYPE))
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

            string DEP_TYPE = Session["DEP_TYPE"].ToString();


            if (xsqdRelateQ_model.deleteMutilRow(nys, xsypdms, DEP_TYPE))
            {
                _return(true, "删除销售参数成功！", "null");
            }
            else
            {
                _return(false, "删除销售参数失败！", "null");
            }
        }
        /// <summary>
        /// 参数分发
        /// </summary>
        public void distribute()
        {
            string NY = _getParam("NY", true);
            string XSYPDM = _getParam("XSYPDM", true);
            Dictionary<bool, List<string>> cycList = xsqdRelateQ_model.distribute(NY, XSYPDM);
            string rMsg = string.Empty;
            string errMsg = "";
            foreach (var item in cycList)
            {
                if (item.Key == true)
                    foreach (string cyc_id in item.Value)
                    {
                        rMsg += cyc_id + " | ";
                    }
                else
                    foreach (string cyc_id in item.Value)
                    {
                        errMsg += cyc_id + " | ";
                    }
            }
            if (!string.IsNullOrEmpty(rMsg))
                rMsg += "参数分发成功；";
            if (!string.IsNullOrEmpty(errMsg))
                rMsg += errMsg + "参数分发失败！";
            _return(true, rMsg, "null");
        }
    }
}
