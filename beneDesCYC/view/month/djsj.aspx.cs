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
using beneDesCYC.core;
using beneDesCYC.model.month;

namespace beneDesCYC.view.month
{
    public partial class djsj : beneDesCYC.core.UI.corePage
    {
        FarPoint.Web.Spread.FpSpread Fpspread1 = new FarPoint.Web.Spread.FpSpread();
        protected void Page_Load(object sender, EventArgs e)
        {
            string downLoad = _getParam("downLoad");
            if (downLoad == "true")
            {
                string cyc_id = Convert.ToString(Session["cyc_id"]);
                string zyq = _getParam("zyq");
                string month = Convert.ToString(Session["month"]);
                initExcel();
                expExcel(cyc_id, zyq, month);
            }
        }

        private void initExcel()
        {
            string path = "../../static/excel/6单井基础信息.xls";
            path = Page.MapPath(path);
            Fpspread1.Sheets[0].OpenExcel(path, 0);
            Fpspread1.OpenExcel(path);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="cyc_id"></param>
        /// <param name="zyq"></param>
        /// <param name="month"></param>
        private void expExcel(string cyc_id, string zyq, string month)
        {
            SqlHelper sqlhelper = new SqlHelper();
            if (!string.IsNullOrEmpty("zyq"))
            {
                djsj_model.getWellList(cyc_id, month, zyq);
            }
            else
            {


            }
        }
    }
}
