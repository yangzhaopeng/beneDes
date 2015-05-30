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
using beneDesCYC;

namespace beneDesYGS.api.month
{
    public partial class initBaseData : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            CommonFunctions.RefreshDataBase();

        }

        public void getAllList()
        {
            DataTable dt = dtb_model.getAllList(Session["depId"].ToString(), Session["month"].ToString());
            _return(true, "", dt);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public void add()
        {
            string NY = _getParam("NY", true);
            //string YQLXMC = _getParam("YQLXMC", true);
            string DTB = _getParam("DTB", true);
            string XSYP = _getParam("XSYP", true);
            //string BZ = _getParam("BZ", true);


            //string CYC_ID = Session["depId"].ToString();

            DataTable dt = dtb_model.getOneDT(NY, XSYP);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该吨桶比已存在，您可以进行修改！", "null");
            }

            if (dtb_model.add(NY, XSYP, DTB))
            {
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加吨桶比失败！", "null");
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public void edit()
        {
            //string NY = _getParam("NY", true);
            //string DEP_ID = _getParam("DEP_ID", true);
            //string XSYP = _getParam("XSYP", true);
            //string SC = _getParam("SC", true);

            //string CYC_ID = Session["depId"].ToString();

            string NY = _getParam("NY", true);
            string XSYP = _getParam("XSYP", true);
            string DTB = _getParam("DTB", true);
            //string XSYP = _getParam("XSYP", true);
            //string BZ = _getParam("BZ", true);



            if (dtb_model.edit(NY, XSYP, DTB))
            {
                _return(true, "编辑吨桶比成功！", "null");
            }
            else
            {
                _return(false, "编辑吨桶比失败！", "null");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void delete()
        {
            //string NY = _getParam("NY", true);
            //string DEP_ID = _getParam("DEP_ID", true);
            //string XSYP = _getParam("XSYP", true);

            //string CYC_ID = Session["depId"].ToString();
            //JObject obj = new JObject();
            //obj["XSYP"] = _getParam("XSYP", true);

            string NY = _getParam("NY", true);
            string XSYP = _getParam("XSYP", true);
            string DTB = _getParam("DTB", true);
            //string XSYP = _getParam("XSYP", true);

            if (dtb_model.delete(NY, XSYP, DTB))
            {
                _return(true, "删除吨桶比成功！", "null");
            }
            else
            {
                _return(false, "删除吨桶比失败！", "null");
            }
        }

    }
}
