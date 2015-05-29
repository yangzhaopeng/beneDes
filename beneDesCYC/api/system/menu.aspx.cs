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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace beneDesCYC.api.system
{
    public partial class menu : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _type = _getParam("_type");

            switch (_type)
            {
                case "getAllMenus":
                    getAllMenus();
                    break;
                case "getUserMenus":
                    getUserMenus();
                    break;
            }
        }

        /// <summary>
        /// 获取所有导航菜单的树形数据
        /// </summary>
        public void getAllMenus()
        {
            DataTable dt = menu_model.getAllMenu();
            JArray obj = formatTreeJson(dt, "0", true);
            _return(true, "", obj);
        }

        /// <summary>
        /// 获取当前登陆用户权限内的导航菜单树形数据
        /// </summary>
        public void getUserMenus()
        {
            DataTable dt = menu_model.getMenuByUser(Session["roleId"].ToString());
            JArray obj = formatTreeJson(dt, "0", false);
            _return(true, "", obj);
        }

        /// <summary>
        /// 将DataTable数据转换为树形JSON结构
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="parentID">父ID</param>
        /// <param name="needChecked">是否需要checked字段</param>
        /// <returns>JArray类型</returns>
        private JArray formatTreeJson(DataTable dt, string parentID, bool needChecked)
        {
            JArray obj = new JArray();

            DataRow[] drs = dt.Select("parentID=" + parentID);

            for (int i = 0; i < drs.Length; i++)
            {
                JObject item = new JObject();
                item["id"] = drs[i]["ID"].ToString();
                item["name"] = drs[i]["name"].ToString();
                item["text"] = drs[i]["name"].ToString();
                item["url"] = drs[i]["url"].ToString();

                if (needChecked) item["checked"] = false;

                if (dt.Select("parentID=" + drs[i]["ID"].ToString()).Length >= 1)
                {
                    item["children"] = formatTreeJson(dt, drs[i]["ID"].ToString(), needChecked);
                }
                else
                {
                    item["leaf"] = true;
                }

                obj.Add(item);
            }

            return obj;
        }
    }
}
