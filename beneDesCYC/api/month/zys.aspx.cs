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
    public partial class zys : beneDesCYC.core.UI.corePage
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
            try
            {
                dt = zys_model.getAllList(Session["cyc_id"].ToString(), Session["month"].ToString());
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
            // string DEP_ID = _getParam("DEP_ID", true);
            string YQLXDM = _getParam("YQLXDM", true);
            string ZYS = _getParam("ZYS", true);

            string CYC_ID = Session["cyc_id"].ToString();

            DataTable dt = zys_model.getOneZYS(NY, YQLXDM, CYC_ID);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该资源税已存在，您可以进行修改！", "null");
            }

            if (zys_model.add(NY, YQLXDM, ZYS, CYC_ID))
            {
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加资源税失败！", "null");
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public void edit()
        {
            string NY = _getParam("NY", true);
            // string DEP_ID = _getParam("DEP_ID", true);
            string YQLXDM = _getParam("YQLXDM", true);
            string ZYS = _getParam("ZYS", true);

            string CYC_ID = Session["cyc_id"].ToString();

            if (zys_model.edit(NY, YQLXDM, ZYS, CYC_ID))
            {
                _return(true, "编辑资源税成功！", "null");
            }
            else
            {
                _return(false, "编辑资源税失败！", "null");
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
            string YQLXDM = _getParam("YQLXDM", true);

            string CYC_ID = Session["cyc_id"].ToString();

            if (yqspl_model.delete(NY, YQLXDM, CYC_ID))
            {
                _return(true, "删除资源税成功！", "null");
            }
            else
            {
                _return(false, "删除资源税失败！", "null");
            }
        }
    }
}

