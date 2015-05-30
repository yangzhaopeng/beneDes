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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using beneDesCYC;

namespace beneDesYGS.api.month
{
    public partial class pjdydy : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _type = _getParam("_type");

            switch (_type)
            {
                case "getPjdyList":
                    getPjdyList();
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

        /// <summary>
        /// 
        /// </summary>
        private void getPjdyList()
        {
            string cyc_id = _getParam("cyc_id");
            string data_type = Convert.ToString(Session["DEP_TYPE"]);
            DataTable dt = null;
            try
            {
                dt = pjdydy_model.getPjdyList(cyc_id, data_type);
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
            string CYC_ID = _getParam("CYC_ID");
            string PJDYMC = Convert.ToString(_getParam("PJDYMC", true));
            DataTable dt = pjdydy_model.getOneDjsj(PJDYMC, CYC_ID);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该评价单元已存在，您可以进行修改！", "null");
            }

            if (pjdydy_model.add(PJDYMC, CYC_ID))
            {
                CommonObject.Rush_PJDYXX();
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加失败！", "null");
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public void edit()
        {
            string PJDYMC = Convert.ToString(_getParam("PJDYMC", true));
            int ID = 0;
            int.TryParse(_getParam("ID", true).ToString(), out ID);
            if (pjdydy_model.edit(ID, PJDYMC))
            {
                CommonObject.Rush_PJDYXX();
                _return(true, "编辑单井基础信息成功！", "null");
            }
            else
            {
                _return(false, "编辑单井基础信息失败！", "null");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void delete()
        {
            int ID = 0;
            int.TryParse(_getParam("ID", true).ToString(), out ID);

            if (pjdydy_model.delete(ID))
            {
                CommonObject.Rush_PJDYXX();
                _return(true, "删除评价单元信息成功！", "null");
            }
            else
            {
                _return(false, "删除评价单元信息失败！", "null");
            }
        }
    }
}
