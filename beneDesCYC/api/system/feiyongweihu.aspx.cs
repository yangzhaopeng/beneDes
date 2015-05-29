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
    public partial class feiyongweihu : beneDesCYC.core.UI.corePage
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
            DataTable dt = feiyongweihu_model.getAllList();
            _return(true, "", dt);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public void add()
        {
            string TEMPLET_CODE = _getParam("TEMPLET_CODE");
            string TEMPLET_NAME = _getParam("TEMPLET_NAME");
            string TEMPLET_TYPE = _getParam("TEMPLET_TYPE");
            string USE_TYPE = _getParam("USE_TYPE");
            string TEMPLET_LEVEL = _getParam("TEMPLET_LEVEL");
            string TEMPLET_TAG = _getParam("TEMPLET_TAG");


            DataTable dt = feiyongweihu_model.getOneWH(TEMPLET_CODE);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该费用大类已存在，您可以进行修改！", "null");
            }

            if (feiyongweihu_model.add(TEMPLET_CODE, TEMPLET_NAME, TEMPLET_TYPE, USE_TYPE, TEMPLET_LEVEL, TEMPLET_TAG))
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
            string TEMPLET_CODE = _getParam("TEMPLET_CODE");
            string TEMPLET_NAME = _getParam("TEMPLET_NAME");
            string TEMPLET_TYPE = _getParam("TEMPLET_TYPE");
            string USE_TYPE = _getParam("USE_TYPE");
            string TEMPLET_LEVEL = _getParam("TEMPLET_LEVEL");
            string TEMPLET_TAG = _getParam("TEMPLET_TAG");

            if (feiyongweihu_model.edit(TEMPLET_CODE, TEMPLET_NAME, TEMPLET_TYPE, USE_TYPE, TEMPLET_LEVEL, TEMPLET_TAG))
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
            string TEMPLET_CODE = _getParam("TEMPLET_CODE");

            if (feiyongweihu_model.delete(TEMPLET_CODE))
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
