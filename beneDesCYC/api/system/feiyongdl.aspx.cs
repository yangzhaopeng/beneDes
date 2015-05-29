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
using beneDesCYC.model.system;
using beneDesCYC.core;

namespace beneDesCYC.api.system
{
    public partial class feiyongdl : beneDesCYC.core.UI.corePage
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

        /// <summary>
        /// 获取所有列表
        /// </summary>
        public void getAllList()
        {
            DataTable dt = feiyongdl_model.getAllList();
            _return(true, "", dt);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public void add()
        {
            string DL_NAME = _getParam("DL_NAME");
            string DL_ID = _getParam("DL_ID");
            string FY_TYPE = _getParam("FY_TYPE");
            string formula = _getParam("FORMULAS");
            string BZ = _getParam("REMARK");



            DataTable dt = feiyongdl_model.getOneFY(DL_ID);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该费用大类已存在，您可以进行修改！", "null");
            }

            if (feiyongdl_model.add(DL_NAME, DL_ID, FY_TYPE, formula, BZ))
            {
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加费用大类失败！", "null");
            }


        }

        /// <summary>
        /// 编辑信息
        /// </summary>
        public void edit()
        {
            string DL_NAME = _getParam("DL_NAME");
            string DL_ID = _getParam("DL_ID");
            string FY_TYPE = _getParam("FY_TYPE");
            string formula = _getParam("FORMULAS");
            string BZ = _getParam("REMARK");

            if (feiyongdl_model.edit(DL_NAME, DL_ID, FY_TYPE, formula, BZ))
            {
                _return(true, "编辑成功！", "null");
            }
            else
            {
                _return(false, "编辑失败！", "null");
            }

        }

        /// <summary>
        /// 删除
        /// </summary>
        public void delete()
        {
            string DL_ID = _getParam("DL_ID");

            if (feiyongdl_model.delete(DL_ID))
            {
                _return(true, "删除成功！", "null");
            }
            else
            {
                _return(false, "删除失败！", "null");
            }
        }

    }
}
