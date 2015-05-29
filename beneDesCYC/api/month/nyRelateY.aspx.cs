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
    public partial class nyRelateY : beneDesCYC.core.UI.corePage
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
                case "extract":
                    extract();
                    break;
            }
        }


        public void getAllList()
        {
            DataTable dt = null;
            try
            {
                dt = nyRelateY_model.getAllList(Session["cyc_id"].ToString(), Session["month"].ToString());
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
            string HL = _getParam("HL", true);
            string DLY = _getParam("DLY", true);
            string QJFY = _getParam("QJFY", true);
            string KTFY = _getParam("KTFY", true);
            string CYC_ID = Session["cyc_id"].ToString();

            DataTable dt = nyRelateY_model.getOneYQSPL(NY, CYC_ID);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该日期相关参数已存在，您可以进行修改！", "null");
            }

            if (nyRelateY_model.add(NY, HL, DLY, QJFY, KTFY, CYC_ID))
            {
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加日期相关参数失败！", "null");
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public void edit()
        {
            string NY = _getParam("NY", true);
            string HL = _getParam("HL", true);
            string DLY = _getParam("DLY", true);
            string QJFY = _getParam("QJFY", true);
            string KTFY = _getParam("KTFY", true);
            string CYC_ID = Session["cyc_id"].ToString();

            if (nyRelateY_model.edit(NY, HL, DLY, QJFY, KTFY, CYC_ID))
            {
                _return(true, "编辑日期相关参数成功！", "null");
            }
            else
            {
                _return(false, "编辑日期相关参数失败！", "null");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void delete()
        {
            string NY = _getParam("NY", true);

            string CYC_ID = Session["cyc_id"].ToString();

            if (nyRelateY_model.delete(NY, CYC_ID))
            {
                _return(true, "删除日期相关参数成功！", "null");
            }
            else
            {
                _return(false, "删除日期相关参数失败！", "null");
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
            string CYC_ID = Session["cyc_id"].ToString();

            if (nyRelateY_model.deleteMutilRow(nys,  CYC_ID))
            {
                _return(true, "删除日期相关参数成功！", "null");
            }
            else
            {
                _return(false, "删除日期相关参数失败！", "null");
            }
        }
        /// <summary>
        /// 提取
        /// </summary>
        public void extract()
        {
            string NY = _getParam("NY", true);

            string CYC_ID = Session["cyc_id"].ToString();

            if (nyRelateY_model.extract(NY, CYC_ID))
            {
                _return(true, "提取日期相关参数成功！", "null");
            }
            else
            {
                _return(false, "提取日期相关参数失败！", "null");
            }
        }
    }
}
