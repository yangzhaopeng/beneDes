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
    public class menu_model
    {   
        /// <summary>
        /// 获取系统的所有导航菜单
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable getAllMenu()
        {
            string sql = "SELECT * from FRM_MENU ORDER BY ID";
            SqlHelper sqlhelper = new SqlHelper();
            DataSet ds = sqlhelper.GetDataSet(sql);
            return ds.Tables[0];
        }
           
        /// <summary>
        /// 获取某用户有权限的导航菜单
        /// </summary>
        /// <param name="roleId">roleId</param>
        /// <returns>DataTable</returns>
        public static DataTable getMenuByUser(string roleId)
        {
            SqlHelper sqlhelper = new SqlHelper();
            string sql = "select MENU_LIST from FRM_ROLES where ROLE_ID='" + roleId+"'";
            string menuList = sqlhelper.GetDataSet(sql).Tables[0].Rows[0]["MENU_LIST"].ToString();

            sql = "select * from FRM_MENU where ID in (" + menuList + ") order by ID";
            DataSet ds = sqlhelper.GetDataSet(sql);
            return ds.Tables[0];
        }
    }
}
