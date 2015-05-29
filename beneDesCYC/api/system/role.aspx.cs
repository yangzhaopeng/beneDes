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

namespace beneDesCYC.api.system
{
    public partial class role : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _type = _getParam("_type");

            switch (_type)
            {
                case "getAllRoles":
                    getAllRoles();
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
                case "saveRight":
                    saveRight();
                    break;
            }
        }

        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        public void getAllRoles()
        {
            DataTable dt = role_model.getAllRoles();
            _return(true, "", dt);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        public void add()
        {
            int role_id = getMaxId() + 1;
            string role_name = _getParam("role_name", true);
            string remark = _getParam("remark", true);

            if (role_model.add(role_id,role_name, remark))
            {
                _return(true, "添加角色成功！", "null");
            }
            else 
            {
                _return(false, "添加角色失败！", "null");
            }
        }
        /// <summary>
        /// 获取Id的最大值
        /// </summary>
        /// <returns></returns>
        private int getMaxId()
        {
            return role_model.getMaxId();
        }
        /// <summary>
        /// 编辑角色的信息
        /// </summary>
        public void edit()
        {
            string role_id = _getParam("role_id", true);
            string role_name = _getParam("role_name", true);
            string remark = _getParam("remark", true);

            if (role_model.edit(role_id, role_name, remark))
            {
                _return(true, "编辑角色成功！", "null");
            }
            else
            {
                _return(false, "编辑角色失败！", "null");
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        public void delete()
        {
            string role_id = _getParam("role_id", true);
            DataTable dt = user_model.getUsersByRole(role_id);

            if (dt.Rows.Count > 0) {
                _return(false, "已有用户属于该角色，角色不能删除！", "null");
            }

            if (role_model.delete(role_id))
            {
                _return(true, "删除角色成功！", "null");
            }
            else
            {
                _return(false, "删除角色失败！", "null");
            }
        }

        /// <summary>
        /// 保存角色的权限
        /// </summary>
        public void saveRight()
        {
            string role_id = _getParam("role_id", true);
            string menu_list = _getParam("menu_list", true);

            if (role_model.saveRight(role_id, menu_list))
            {
                _return(true, "保存角色权限成功！", "null");
            }
            else
            {
                _return(false, "保存角色权限失败！", "null");
            }
        }
    }
}
