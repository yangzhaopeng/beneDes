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
    public partial class trqtj : beneDesYGS.core.UI.corePage
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
            DataTable dt = trqtj_model.getAllList(Session["depId"].ToString(), Session["month"].ToString());
            _return(true, "", dt);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public void add()
        {
            string NY = _getParam("NY", true);
            string DLY = _getParam("DLY", true);
            //string BZ = _getParam("BZ", true);


            //string CYC_ID = Session["depId"].ToString();

            DataTable dt = trqtj_model.getOneTR(NY);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该天然气体积换算系数已存在，您可以进行修改！", "null");
            }

            if (trqtj_model.add(NY, DLY))
            {
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加天然气体积换算系数失败！", "null");
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
            string DLY = _getParam("DLY", true);
            //string BZ = _getParam("BZ", true);


            if (trqtj_model.edit(NY, DLY))
            {
                _return(true, "编辑天然气体积换算系数成功！", "null");
            }
            else
            {
                _return(false, "编辑天然气体积换算系数失败！", "null");
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
            string DLY = _getParam("DLY", true);

            if (trqtj_model.delete(NY, DLY))
            {
                _return(true, "删除天然气体积换算系数成功！", "null");
            }
            else
            {
                _return(false, "删除天然气体积换算系数失败！", "null");
            }
        }

    }
}
