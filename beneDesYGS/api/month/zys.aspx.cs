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
using beneDesYGS.model.system;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace beneDesYGS.api.month
{
    public partial class zys : beneDesYGS.core.UI.corePage
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
            DataTable dt = zys_model.getAllList(Session["depId"].ToString(), Session["month"].ToString());
            _return(true, "", dt);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public void add()
        {
            string NY = _getParam("NY", true);
            //string YQLXMC = _getParam("YQLXMC", true);
            string ZYS = _getParam("ZYS", true);
            string YQLX = _getParam("YQLX", true);
            //string BZ = _getParam("BZ", true);


            //string CYC_ID = Session["depId"].ToString();

            DataTable dt = zys_model.getOneZY(NY, YQLX);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该资源税已存在，您可以进行修改！", "null");
            }

            if (zys_model.add(NY, YQLX, ZYS))
            {
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加资源税失败！", "null");
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public void edit()
        {
            //string NY = _getParam("NY", true);
            //string DEP_ID = _getParam("DEP_ID", true);
            //string YQLX = _getParam("YQLX", true);
            //string SC = _getParam("SC", true);

            //string CYC_ID = Session["depId"].ToString();

            string NY = _getParam("NY", true);
            string YQLX = _getParam("YQLX", true);
            string ZYS = _getParam("ZYS", true);
            //string YQLX = _getParam("YQLX", true);
            //string BZ = _getParam("BZ", true);

            

            if (zys_model.edit(NY, YQLX, ZYS))
            {
                _return(true, "编辑资源税成功！", "null");
            }
            else
            {
                _return(false, "编辑资源税失败！", "null");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void delete()
        {
            //string NY = _getParam("NY", true);
            //string DEP_ID = _getParam("DEP_ID", true);
            //string YQLX = _getParam("YQLX", true);
            
            //string CYC_ID = Session["depId"].ToString();
            //JObject obj = new JObject();
            //obj["YQLX"] = _getParam("YQLX", true);

            string NY = _getParam("NY", true);
            string YQLX = _getParam("YQLX", true);
            string ZYS = _getParam("ZYS", true);
            //string YQLX = _getParam("YQLX", true);

            if (zys_model.delete(NY, YQLX, ZYS))
            {
                _return(true, "删除资源税成功！", "null");
            }
            else
            {
                _return(false, "删除资源税失败！", "null");
            }
        }

    }
}
