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
using beneDesYGS.model.base_info;
using beneDesYGS.model.system;

namespace beneDesYGS.api.base_info
{
    public partial class gycs : beneDesYGS.core.UI.corePage
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
            DataTable dt = gycs_model.getAllList(Convert.ToString(Session["depId"]), Convert.ToString(Session["month"]));
            _return(true, "", dt);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public void add()
        {
            string GYCSDM = _getParam("GYCSDM", true);
            string GYCSMC = _getParam("GYCSMC", true);
            string BZ = _getParam("BZ", true);


            //string CYC_ID = Session["depId"].ToString();

            DataTable dt = gycs_model.getOneGY(GYCSDM);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该工艺措施代码已存在，您可以进行修改！", "null");
            }

            if (gycs_model.add(GYCSDM, GYCSMC, BZ))
            {
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加工艺措施失败！", "null");
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

            string GYCSDM = _getParam("GYCSDM", true);
            string GYCSMC = _getParam("GYCSMC", true);
            string BZ = _getParam("BZ", true);


            if (gycs_model.edit(GYCSDM, GYCSMC, BZ))
            {
                _return(true, "编辑工艺措施成功！", "null");
            }
            else
            {
                _return(false, "编辑工艺措施失败！", "null");
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

            string GYCSDM = _getParam("GYCSDM", true);
            string GYCSMC = _getParam("GYCSMC", true);

            if (gycs_model.delete(GYCSDM, GYCSMC))
            {
                _return(true, "删除工艺措施成功！", "null");
            }
            else
            {
                _return(false, "删除工艺措施失败！", "null");
            }
        }
    }
}
