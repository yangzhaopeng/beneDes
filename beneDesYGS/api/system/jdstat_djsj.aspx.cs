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
    public partial class jdstat_djsj : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string dwType = _getParam("dwType");

            switch (dwType)
            {
                case "zyq":
                    getJDList();
                    break;
                case "pjdy":
                    getJD2List();
                    break;
                case "qk":
                    getJD3List();
                    break;
            }
        }

        /// <summary>
        /// 获取所有的下拉列表
        /// </summary>
        public void getJDList()
        {
            string list = _getParam("CYC");
            string bny = _getParam("BNY");
            string eny = _getParam("ENY");

            DataTable dt = jdstat_model.getJDList(list);
            _return(true, "", dt);
        }

        public void getJD2List()
        {

            string list = _getParam("CYC");
            string bny = _getParam("BNY");
            string eny = _getParam("ENY");

            DataTable dt = jdstat_model.getJD2List(list,bny,eny);
            _return(true, "", dt);
        }

        public void getJD3List()
        {

            string list = _getParam("CYC");
            string bny = _getParam("BNY");
            string eny = _getParam("ENY");

            DataTable dt = jdstat_model.getJD3List(list, bny, eny);
            _return(true, "", dt);
        }
    }
}
