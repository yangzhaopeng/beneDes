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

namespace beneDesYGS.api.month
{
    public partial class rmbhl : beneDesYGS.core.UI.corePage
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
            DataTable dt = rmbhl_model.getAllList(Session["depId"].ToString(), Session["month"].ToString());
            _return(true, "", dt);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public void add()
        {
            string NY = _getParam("NY", true);
            string HL = _getParam("HL", true);
            //string BZ = _getParam("BZ", true);


            //string CYC_ID = Session["depId"].ToString();

            DataTable dt = rmbhl_model.getOneTR(NY);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该人民币汇率已存在，您可以进行修改！", "null");
            }

            if (rmbhl_model.add(NY, HL))
            {
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加人民币汇率失败！", "null");
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public void edit()
        {
            //string NY = _getParam("NY", true);
            //string DEP_ID = _getParam("DEP_ID", true);
            //string YQLXDM = _getParam("YQLXDM", true);
            //string SC = _getParam("SC", true);

            //string CYC_ID = Session["depId"].ToString();

            string NY = _getParam("NY", true);
            string HL = _getParam("HL", true);
            //string BZ = _getParam("BZ", true);


            if (rmbhl_model.edit(NY, HL))
            {
                _return(true, "编辑人民币汇率成功！", "null");
            }
            else
            {
                _return(false, "编辑人民币汇率失败！", "null");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void delete()
        {
            //string NY = _getParam("NY", true);
            //string DEP_ID = _getParam("DEP_ID", true);
            //string YQLXDM = _getParam("YQLXDM", true);

            //string CYC_ID = Session["depId"].ToString();

            string NY = _getParam("NY", true);
            string HL = _getParam("HL", true);

            if (rmbhl_model.delete(NY, HL))
            {
                _return(true, "删除人民币汇率成功！", "null");
            }
            else
            {
                _return(false, "删除人民币汇率失败！", "null");
            }
        }

    }
}
