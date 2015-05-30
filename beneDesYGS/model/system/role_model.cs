using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using beneDesYGS.core;

namespace beneDesYGS.model.system
{
    public class role_model
    {
        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        /// <returns></returns>
        public static DataTable getAllRoles()
        {
            string sql = "select * from FRM_ROLES order by ROLE_ID";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }
        public static int getMaxId()
        {
            string sql = "select Max(ROLE_ID) from FRM_ROLES";
            SqlHelper sqlhelper = new SqlHelper();
            return Convert.ToInt32(sqlhelper.GetExecuteScalar(sql));
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role_name"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public static bool add(int role_id, string role_name, string remark)
        {
            string sql = "insert into FRM_ROLES (ROLE_ID,ROLE_NAME, REMARK) values (" + role_id + ",'" + role_name + "','" + remark + "')";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 编辑角色信息
        /// </summary>
        /// <param name="role_id"></param>
        /// <param name="role_name"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public static bool edit(string role_id, string role_name, string remark)
        {
            string sql = "update FRM_ROLES set ROLE_NAME='" + role_name + "', REMARK='" + remark + "' where ROLE_ID='" + role_id+"'";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="role_id"></param>
        /// <returns></returns>
        public static bool delete(string role_id)
        {
            string sql = "delete from FRM_ROLES where ROLE_ID='" + role_id+"'";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 保存角色的权限
        /// </summary>
        /// <param name="role_id"></param>
        /// <param name="menu_list"></param>
        /// <returns></returns>
        public static bool saveRight(string role_id, string menu_list)
        {
            string sql = "update FRM_ROLES set MENU_LIST='" + menu_list + "' where ROLE_ID='" + role_id+"'";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }
    }
}
