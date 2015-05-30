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

namespace beneDesYGS.api.month
{
    public partial class qkdy : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _type = _getParam("_type");

            switch (_type)
            {
                case "getQKList":
                    getQKList();
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
        /// 根据查询条件获取所有井列表信息
        /// </summary>
        public void getQKList()
        {
            string cyc_id = _getParam("cyc_id");
            DataTable dt = qkdy_model.getQKList(cyc_id);
            _return(true, "", dt);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public void add()
        {

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
        public void edit()
        {

            //string CYC_ID = Session["depId"].ToString();

            //if (obj["DJ_ID_new"].ToString() != obj["DJ_ID"].ToString())
            //{
            //    DataTable dt = djsj_model.getOneDjsj(obj["NY"].ToString(), CYC_ID, obj["DJ_ID_new"].ToString());

            //    if (dt.Rows.Count > 0)
            //    { 
            //        _return(false, "井号错误：本作业区已经有该井的基础信息！", "null");
            //    }
            //}

            //if (djsj_model.edit(obj, CYC_ID))
            //{
            //    _return(true, "编辑单井基础信息成功！", "null");
            //}
            //else
            //{
            //    _return(false, "编辑单井基础信息失败！", "null");
            //}
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void delete()
        {
            string NY = _getParam("NY", true);
            string DJ_ID = _getParam("DJ_ID", true);
            string CYC_ID = Session["depId"].ToString();

            if (qkdy_model.delete(NY, DJ_ID, CYC_ID))
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
