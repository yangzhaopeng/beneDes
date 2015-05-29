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
using beneDesCYC.core;

namespace beneDesCYC.api.system
{
    public partial class jdstat_djsj1 : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string dwType = _getParam("dwType");

            switch (dwType)
            {
                case "zyq":
                    getZYQList();
                    break;
                case "pjdy":
                    getPJDYList();
                    break;
                case "qk":
                    getQKList();
                    break;
            }
        }

        /// <summary>
        /// 获取所有的作业区下拉列表
        /// </summary>
        public void getZYQList()
        {
            string bny = _getParam("BNY");
            string eny = _getParam("ENY");
            string cycid = Session["cyc_id"].ToString();
            DataTable dt = jdstat_djsj1_model.getZYQList(cycid);
            _return(true, "", dt);
        }

        /// <summary>
        /// 获取所有评价单元列表
        /// </summary>
        public void getPJDYList()
        {
            string bny = _getParam("BNY");
            string eny = _getParam("ENY");
            string cycid = Session["cyc_id"].ToString();
            DataTable dt = jdstat_djsj1_model.getPJDYList(bny, eny, cycid);
            _return(true, "", dt);
        }
        /// <summary>
        /// 获取所有区块列表
        /// </summary>

        public void getQKList()
        {
            string bny = _getParam("BNY");
            string eny = _getParam("ENY");
            string cycid = Session["cyc_id"].ToString();
            DataTable dt = jdstat_djsj1_model.getQKList(bny, eny, cycid);
            _return(true, "", dt);
        }
    }
}
