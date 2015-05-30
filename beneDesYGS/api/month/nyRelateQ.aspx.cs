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

namespace beneDesYGS.api.month
{
    public partial class nyRelateQ : beneDesYGS.core.UI.corePage
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
                case "distribute":
                    distribute();
                    break;
            }
        }


        public void getAllList()
        {
            DataTable dt = null;
            string DEP_TYPE = Session["DEP_TYPE"].ToString();
            try
            {
                dt = nyRelateQ_model.getAllList(DEP_TYPE, Session["month"].ToString());
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
            string DEP_TYPE = Session["DEP_TYPE"].ToString();

            DataTable dt = nyRelateQ_model.getOneYQSPL(NY, DEP_TYPE);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该参数已存在，您可以进行修改！", "null");
            }

            if (nyRelateQ_model.add(NY, HL, DLY, QJFY, KTFY, DEP_TYPE))
            {
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加参数失败！", "null");
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
            string DEP_TYPE = Session["DEP_TYPE"].ToString();
            if (nyRelateQ_model.edit(NY, HL, DLY, QJFY, KTFY, DEP_TYPE))
            {
                _return(true, "编辑油气商品率成功！", "null");
            }
            else
            {
                _return(false, "编辑油气商品率失败！", "null");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void delete()
        {
            string NY = _getParam("NY", true);
            string DEP_TYPE = Session["DEP_TYPE"].ToString();
            if (nyRelateQ_model.delete(NY, DEP_TYPE))
            {
                _return(true, "删除油气商品率成功！", "null");
            }
            else
            {
                _return(false, "删除油气商品率失败！", "null");
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
            string DEP_TYPE = Session["DEP_TYPE"].ToString();
            if (nyRelateQ_model.deleteMutilRow(nys, DEP_TYPE))
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
            Dictionary<bool, List<string>> cycList = nyRelateQ_model.distribute(NY);
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
