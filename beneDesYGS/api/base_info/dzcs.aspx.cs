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
using beneDesYGS.model.base_info;
using beneDesYGS.model.system;

namespace beneDesYGS.api.base_info
{
    public partial class dzcs : beneDesYGS.core.UI.corePage
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
            DataTable dt = dzcs_model.getAllList(Convert.ToString(Session["cyc_id"]), Convert.ToString(Session["month"]));
            _return(true, "", dt);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public void add()
        {
            string DZCSDM = _getParam("DZCSDM", true);
            string DZCSMC = _getParam("DZCSMC", true);
            string BZ = _getParam("BZ", true);
         

            //string CYC_ID = Session["cyc_id"].ToString();

            DataTable dt = dzcs_model.getOneDZ(DZCSDM);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该地质措施代码已存在，您可以进行修改！", "null");
            }

            if (dzcs_model.add(DZCSDM,DZCSMC,BZ))
            {
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加地质措施失败！", "null");
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public void edit()
        {
            //string NY = _getParam("NY", true);
            //string DEP_ID = _getParam("DEP_ID", true);
            //string YQLXDM = _getParam("YQLXDM", true);
            //string SC = _getParam("SC", true);

            //string CYC_ID = Session["cyc_id"].ToString();

            string DZCSDM = _getParam("DZCSDM", true);
            string DZCSMC = _getParam("DZCSMC", true);
            string BZ = _getParam("BZ", true);


            if (dzcs_model.edit(DZCSDM,DZCSMC,BZ))
            {
                _return(true, "编辑地质措施成功！", "null");
            }
            else
            {
                _return(false, "编辑地质措施失败！", "null");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void delete()
        {
            //string NY = _getParam("NY", true);
            //string DEP_ID = _getParam("DEP_ID", true);
            //string YQLXDM = _getParam("YQLXDM", true);

            //string CYC_ID = Session["cyc_id"].ToString();

            string DZCSDM = _getParam("DZCSDM", true);
            string DZCSMC = _getParam("DZCSMC", true);

            if (dzcs_model.delete(DZCSDM, DZCSMC))
            {
                _return(true, "删除地质措施成功！", "null");
            }
            else
            {
                _return(false, "删除地质措施失败！", "null");
            }
        }


    }
}
