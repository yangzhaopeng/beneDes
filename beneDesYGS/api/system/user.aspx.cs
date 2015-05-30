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
using beneDesYGS.model.system;
using beneDesYGS.core;

namespace beneDesYGS.api.system
{
    public partial class user : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _type = _getParam("_type");

            switch (_type) 
            {
                case "changePassword":
                    changePassword();
                    break;
                case "getAllUsers":
                    getAllUsers();
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
                case "resetPassword":
                    resetPassword();
                    break;
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public void changePassword()
        {
            string old = _getParam("old");
            string newPass = _getParam("new");
            DataTable dt = user_model.getUserInfoByUserId(Session["userId"].ToString());

            if (dt.Rows[0]["USER_PASS"].ToString() != pwdHelper.Encrypt(old.Trim()))
            {
                _return(false, "旧密码错误！", "null");
            }
            else 
            {
                user_model.changePassword(Session["userId"].ToString(), newPass);
                _return(true, "ok", "null");
            }
        }

        /// <summary>
        /// 获取所有user的列表
        /// </summary>
        public void getAllUsers()
        {
            DataTable dt = user_model.getAllUsers();
            _return(true, "", dt);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        public void add()
        {
            string user_id = _getParam("user_id", true);
            string user_name = _getParam("user_name", true);
            string dep_id = _getParam("dep_id", true);
            string role_id = _getParam("role_id", true);
            string remark = _getParam("remark", true);

            DataTable dt = user_model.getUserInfoByUserId(user_id);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该登陆名已存在！", "null");
            }

            if (user_model.add(user_id, user_name, dep_id, role_id, remark))
            {
                _return(true, "添加用户成功！", "null");
            }
            else
            {
                _return(false, "添加用户失败！", "null");
            }
        }

        /// <summary>
        /// 编辑用户的信息
        /// </summary>
        public void edit()
        {
            string user_id = _getParam("user_id", true);
            string user_name = _getParam("user_name", true);
            string dep_id = _getParam("dep_id", true);
            string role_id = _getParam("role_id", true);
            string remark = _getParam("remark", true);

            if (user_model.edit(user_id, user_name, dep_id, role_id, remark))
            {
                _return(true, "编辑用户成功！", "null");
            }
            else
            {
                _return(false, "编辑用户失败！", "null");
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        public void delete()
        {
            string user_id = _getParam("user_id", true);

            if (user_model.delete(user_id))
            {
                _return(true, "删除用户成功！", "null");
            }
            else
            {
                _return(false, "删除用户失败！", "null");
            }
        }

        /// <summary>
        /// 用户密码重置
        /// </summary>
        public void resetPassword()
        {
            string user_id = _getParam("user_id", true);
            string password = _getParam("password", true);

            user_model.changePassword(user_id, password);
            _return(true, "密码重置成功！", "null");
        }
    }
}
