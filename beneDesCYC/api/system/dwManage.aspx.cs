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
    public partial class dwManage : beneDesCYC.core.UI.corePage
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
            DataTable dt = dwManage_model.getAllList();
            _return(true, "", dt);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public void add()
        {
            string DEP_TYPE = _getParam("DEP_TYPE");
            string DEP_ID = _getParam("DEP_ID");
            string DEP_NAME = _getParam("DEP_NAME");
            string BZ = _getParam("REMARK");

            string parentid = "";
            string deplevel = "";
            if (DEP_TYPE == "CYC")
            {
                parentid = "$ROOT";
                deplevel = Convert.ToString("1");
            }
            else  //作业区
            {
                //parentid = "cyc";
                parentid = DEP_ID;
                deplevel = Convert.ToString("2");
            }

            DataTable dt = dwManage_model.getOneDW(DEP_ID);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该部门已存在，您可以进行修改！", "null");
            }

            if (dwManage_model.add(DEP_TYPE, DEP_ID, DEP_NAME, BZ, parentid, deplevel))
            {
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加部门失败！", "null");
            }


        }

        /// <summary>
        /// 编辑信息
        /// </summary>
        public void edit()
        {
            string DEP_TYPE = _getParam("DEP_TYPE");
            string DEP_ID = _getParam("DEP_ID");
            string DEP_NAME = _getParam("DEP_NAME");
            string BZ = _getParam("REMARK");

            if (dwManage_model.edit(DEP_TYPE, DEP_ID, DEP_NAME, BZ))
            {
                _return(true, "编辑部门成功！", "null");
            }
            else
            {
                _return(false, "编辑部门失败！", "null");
            }

        }

        /// <summary>
        /// 删除
        /// </summary>
        public void delete()
        {
            string DEP_ID = _getParam("DEP_ID");

            if (dwManage_model.delete(DEP_ID))
            {
                _return(true, "删除部门成功！", "null");
            }
            else
            {
                _return(false, "删除部门失败！", "null");
            }
        }
    }
}

