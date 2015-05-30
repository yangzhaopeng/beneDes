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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace beneDesYGS.api.month
{
    public partial class pc_well : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _type = _getParam("_type");

            switch (_type)
            {
                case "getWellList":
                    getWellList();
                    break;
                case "getPjdyWellList":
                    getPjdyWellList();
                    break;
                case "add":
                    add();
                    break;
                case "update":
                    update();
                    break;
                case "delete":
                    delete();
                    break;
            }
        }


        /// <summary>
        /// 根据查询条件获取所有井列表信息
        /// </summary>
        public void getWellList()
        {
            string QKID = _getParam("QKID", true);
            DataTable dt = well_model.getWellList(QKID);
            _return(true, "", dt);
        }
        public void getPjdyWellList()
        {
            string pjdy = _getParam("pjdy", true);

            DataTable dt = well_model.getPjdyWellList(pjdy);
            _return(true, "", dt);
        }
        /// <summary>
        /// 添加
        /// </summary>
        public void add()
        {
            //JObject obj = getWellParam();
            //obj["DJ_ID"] = obj["ZYQ"].ToString() + "$" + obj["JH"].ToString();

            //string CYC_ID = Session["depId"].ToString();

            //DataTable dt = djsj_model.getOneDjsj(obj["NY"].ToString(), CYC_ID, obj["DJ_ID"].ToString());

            //if (dt.Rows.Count > 0)
            //{
            //    _return(false, "该井信息已存在，您可以进行修改！", "null");
            //}

            //if (djsj_model.add(obj, CYC_ID))
            //{
            //    _return(true, "添加成功！", "null");
            //}
            //else
            //{
            //    _return(false, "添加失败！", "null");
            //}
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public void update()
        {
            string IDS = _getParam("IDS", true);
            string pjdy = _getParam("PJDY", true);
            string qk = _getParam("QK",true);

            if (well_model.update(IDS, pjdy,qk))
            {
                _return(true, "修改成功", "null");
            }
            else
            {
                _return(true, "修改失败", "null");
            }

        }

        /// <summary>
        /// 删除
        /// </summary>
        public void delete()
        {
            string NY = _getParam("NY", true);
            string DJ_ID = _getParam("DJ_ID", true);
            string CYC_ID = Session["depId"].ToString();

            if (djsj_model.delete(NY, DJ_ID, CYC_ID))
            {
                _return(true, "删除单井基础信息成功！", "null");
            }
            else
            {
                _return(false, "删除单井基础信息失败！", "null");
            }
        }
    }
}
