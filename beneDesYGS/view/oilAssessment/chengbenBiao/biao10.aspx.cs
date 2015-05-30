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
using System.Data.OracleClient;
using beneDesYGS.model.system;
using beneDesYGS.core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace beneDesYGS.view.oilAssessment.chengbenBiao
{
    public partial class biao10 : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string list = _getParam("CYC");
            if (list == "null")
                initSpread2();
            else
                initSpread();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }

        protected void initSpread()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/zuoyequ.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表10-特高成本井效益情况统计表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

            //this.FpSpread1.Sheets[0].Cells[3, 1].Text = _getParam("targetType");
            //this.FpSpread1.Sheets[0].Cells[4, 1].Text = _getParam("startMonth") + " -- " + _getParam("endMonth");

            if (isempty() == 0)
            { Response.Write("<script>alert('无数据')</script>"); }
            else
            { sj(); }
        }

        protected void initSpread2()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/zuoyequ.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表10-特高成本井效益情况统计表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

        }

        protected void sj()
        {
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //string list = _getParam("YCLX");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            string list = _getParam("CYC");
            string typeid = _getParam("targetType");

            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["ygsConnectionString"].ConnectionString);
            //////////////////搜出表中的特高成本井
            connfp.Open();
            string tgse = "select num from csdy_info where name = '特高成本井' and ny = '" + Session["month"].ToString() + "'";
            OracleCommand tgcom = new OracleCommand(tgse, connfp);
            OracleDataReader tgr = tgcom.ExecuteReader();
            tgr.Read();
            Int64 tgcb = Int64.Parse(tgr[0].ToString());
            connfp.Close();
            /////////////////////////////////////
            string fpselect = "";
            if (typeid == "yt")
            {
                fpselect += " select  '' as jbid,''  as jbmc,d.dep_name, ";
                fpselect += " nvl(round(sum(sdy.djisopen),4),0) as js,";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl, ";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb,";

                fpselect += " sum(nvl(tem1.js,0)) as js1, ";
                fpselect += " sum(nvl(tem1.hscyl,0)) as hscyl1, ";
                fpselect += " sum(nvl(tem1.yyspl,0)) as yyspl1, ";
                fpselect += " sum(nvl(tem1.zjyxczcb,0)) as zjyxczcb1,";
                fpselect += " sum(nvl(tem1.yxczcb,0)) as yxczcb1,";
                fpselect += " sum(nvl(tem1.czcb,0)) as czcb1,";

                fpselect += " sum(nvl(tem2.js,0))  as js2, ";
                fpselect += " sum(nvl(tem2.hscyl,0))  as hscyl2, ";
                fpselect += " sum(nvl(tem2.yyspl,0))  as yyspl2, ";
                fpselect += " sum(nvl(tem2.zjyxczcb,0))  as zjyxczcb2, ";
                fpselect += " sum(nvl(tem2.yxczcb,0))  as yxczcb2, ";
                fpselect += " sum(nvl(tem2.czcb,0))  as czcb2, ";

                fpselect += " sum(nvl(tem3.js,0))  as js3,";
                fpselect += " sum(nvl(tem3.hscyl,0))  as hscyl3,";
                fpselect += " sum(nvl(tem3.yyspl,0))  as yyspl3, ";
                fpselect += " sum(nvl(tem3.zjyxczcb,0))  as zjyxczcb3, ";
                fpselect += " sum(nvl(tem3.yxczcb,0))  as yxczcb3, ";
                fpselect += " sum(nvl(tem3.czcb,0))  as czcb3, ";

                fpselect += " sum(nvl(tem4.js,0))  as js4, ";
                fpselect += " sum(nvl(tem4.hscyl,0))  as hscyl4, ";
                fpselect += " sum(nvl(tem4.yyspl,0))  as yyspl4, ";
                fpselect += " sum(nvl(tem4.zjyxczcb,0))  as zjyxczcb4, ";
                fpselect += " sum(nvl(tem4.yxczcb,0))  as yxczcb4, ";
                fpselect += " sum(nvl(tem4.czcb,0))  as czcb4 ";

                fpselect += " from  view_dtstat_zyqsj sdy, department d, ";

                //tem1
                fpselect += " (";
                fpselect += " select  sdy.dj_id as dj_id, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from view_dtstat_zyqsj sdy ";
                fpselect += " where sdy.gsxyjb_1 = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where  sdy.djisopen = '1'";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.dj_id ";
                fpselect += " ) tem1, ";

                //tem2
                fpselect += " (";
                fpselect += " select  sdy.dj_id as dj_id, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from view_dtstat_zyqsj sdy ";
                fpselect += " where sdy.gsxyjb_1 = '2' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where  sdy.djisopen = '1'";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.dj_id ";
                fpselect += " ) tem2, ";

                //tem3
                fpselect += " (";
                fpselect += " select  sdy.dj_id as dj_id, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from view_dtstat_zyqsj sdy ";
                fpselect += " where sdy.gsxyjb_1 = '3' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.dj_id ";
                fpselect += " ) tem3, ";

                //tem4
                fpselect += " (";
                fpselect += " select  sdy.dj_id as dj_id, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from view_dtstat_zyqsj sdy ";
                fpselect += " where sdy.gsxyjb_1 = '4' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.dj_id ";
                fpselect += " ) tem4 ";



                fpselect += " where sdy.dep_id = d.dep_id and  ";

                fpselect += "  sdy.dj_id =  tem1.dj_id(+) ";
                fpselect += " and sdy.dj_id =  tem2.dj_id(+) ";
                fpselect += " and sdy.dj_id =  tem3.dj_id(+)  ";
                fpselect += " and sdy.dj_id =  tem4.dj_id(+)  ";

                //fpselect += " and sdy.cyc_id = '" + cycid + "'";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " and sdy.djisopen = '1' and sdy.jh in ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";

                fpselect += "group by d.dep_name ";
                fpselect += " order by d.dep_name ";



            }
            else if (typeid == "qk")
            {
                //fpselect += " select * from view_dtbiao6_1  ";
                fpselect += " select xy.jbid, xy.jbmc as jbmc,sdy.qk as qk, ";
                fpselect += " nvl(round(sum(sdy.djisopen),4),0) as js,";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl, ";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb,";

                fpselect += " nvl(tem1.js,0) as js1, ";
                fpselect += " nvl(tem1.hscyl,0) as hscyl1, ";
                fpselect += " nvl(tem1.yyspl,0) as yyspl1, ";
                fpselect += " nvl(tem1.zjyxczcb,0) as zjyxczcb1,";
                fpselect += " nvl(tem1.yxczcb,0) as yxczcb1,";
                fpselect += " nvl(tem1.czcb,0) as czcb1,";

                fpselect += " nvl(tem2.js,0)  as js2, ";
                fpselect += " nvl(tem2.hscyl,0)  as hscyl2, ";
                fpselect += " nvl(tem2.yyspl,0)  as yyspl2, ";
                fpselect += " nvl(tem2.zjyxczcb,0)  as zjyxczcb2, ";
                fpselect += " nvl(tem2.yxczcb,0)  as yxczcb2, ";
                fpselect += " nvl(tem2.czcb,0)  as czcb2, ";

                fpselect += " nvl(tem3.js,0)  as js3,";
                fpselect += " nvl(tem3.hscyl,0)  as hscyl3,";
                fpselect += " nvl(tem3.yyspl,0)  as yyspl3, ";
                fpselect += " nvl(tem3.zjyxczcb,0)  as zjyxczcb3, ";
                fpselect += " nvl(tem3.yxczcb,0)  as yxczcb3, ";
                fpselect += " nvl(tem3.czcb,0)  as czcb3, ";

                fpselect += " nvl(tem4.js,0)  as js4, ";
                fpselect += " nvl(tem4.hscyl,0)  as hscyl4, ";
                fpselect += " nvl(tem4.yyspl,0)  as yyspl4, ";
                fpselect += " nvl(tem4.zjyxczcb,0)  as zjyxczcb4, ";
                fpselect += " nvl(tem4.yxczcb,0)  as yxczcb4, ";
                fpselect += " nvl(tem4.czcb,0)  as czcb4";

                fpselect += " from  dtstat_djsj sdy, dtxyjb_info xy, dtstat_qksj sdyqk, ";
                fpselect += " (";
                fpselect += " select  sdy.qk as qk, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from dtstat_djsj sdy ";
                fpselect += " where sdy.gsxyjb_1 = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where  sdy.djisopen = '1'";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.qk ";
                fpselect += " ) tem1, ";
                fpselect += " (";
                fpselect += " select  sdy.qk as qk, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from dtstat_djsj sdy ";
                fpselect += " where sdy.gsxyjb_1 = '2' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where  sdy.djisopen = '1'";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.qk ";
                fpselect += " ) tem2, ";
                fpselect += " (";
                fpselect += " select  sdy.qk as qk, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from dtstat_djsj sdy ";
                fpselect += " where sdy.gsxyjb_1 = '3' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.qk ";
                fpselect += " ) tem3, ";
                fpselect += " (";
                fpselect += " select  sdy.qk as qk, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from dtstat_djsj sdy ";
                fpselect += " where sdy.gsxyjb_1 = '4' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.qk ";
                fpselect += " ) tem4 ";
                fpselect += " where";
                fpselect += "     sdyqk.gsxyjb_1 = xy.jbid  ";
                fpselect += " and sdy.qk =  tem1.qk(+) ";
                fpselect += " and sdy.qk =  tem2.qk(+) ";
                fpselect += " and sdy.qk =  tem3.qk(+)  ";
                fpselect += " and sdy.qk =  tem4.qk(+)  ";
                fpselect += " and sdy.qk = sdyqk.mc";
                //fpselect += " and sdy.cyc_id = '" + cycid + "'";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " and sdy.djisopen = '1' and sdy.jh in ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";

                fpselect += " group by xy.jbid,xy.jbmc,sdy.qk, ";
                fpselect += " tem1.js,tem1.hscyl,tem1.yyspl,tem1.zjyxczcb,tem1.yxczcb,tem1.czcb, ";
                fpselect += " tem2.js,tem2.hscyl,tem2.yyspl,tem2.zjyxczcb,tem2.yxczcb,tem2.czcb, ";
                fpselect += " tem3.js,tem3.hscyl,tem3.yyspl,tem3.zjyxczcb,tem3.yxczcb,tem3.czcb, ";
                fpselect += " tem4.js,tem4.hscyl,tem4.yyspl,tem4.zjyxczcb,tem4.yxczcb,tem4.czcb  ";
                fpselect += " order by jbid ";
            }
            else if (typeid == "pjdy")
            {
                //fpselect += " select * from view_dtbiao6  ";
                fpselect += " select  xy.jbid,xy.jbmc as jbmc,sdy.pjdy as pjdy, ";
                fpselect += " nvl(round(sum(sdy.djisopen),4),0) as js,";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl, ";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb,";

                fpselect += " nvl(tem1.js,0) as js1, ";
                fpselect += " nvl(tem1.hscyl,0) as hscyl1, ";
                fpselect += " nvl(tem1.yyspl,0) as yyspl1, ";
                fpselect += " nvl(tem1.zjyxczcb,0) as zjyxczcb1,";
                fpselect += " nvl(tem1.yxczcb,0) as yxczcb1,";
                fpselect += " nvl(tem1.czcb,0) as czcb1,";

                fpselect += " nvl(tem2.js,0)  as js2, ";
                fpselect += " nvl(tem2.hscyl,0)  as hscyl2, ";
                fpselect += " nvl(tem2.yyspl,0)  as yyspl2, ";
                fpselect += " nvl(tem2.zjyxczcb,0)  as zjyxczcb2, ";
                fpselect += " nvl(tem2.yxczcb,0)  as yxczcb2, ";
                fpselect += " nvl(tem2.czcb,0)  as czcb2, ";

                fpselect += " nvl(tem3.js,0)  as js3,";
                fpselect += " nvl(tem3.hscyl,0)  as hscyl3,";
                fpselect += " nvl(tem3.yyspl,0)  as yyspl3, ";
                fpselect += " nvl(tem3.zjyxczcb,0)  as zjyxczcb3, ";
                fpselect += " nvl(tem3.yxczcb,0)  as yxczcb3, ";
                fpselect += " nvl(tem3.czcb,0)  as czcb3, ";

                fpselect += " nvl(tem4.js,0)  as js4, ";
                fpselect += " nvl(tem4.hscyl,0)  as hscyl4, ";
                fpselect += " nvl(tem4.yyspl,0)  as yyspl4, ";
                fpselect += " nvl(tem4.zjyxczcb,0)  as zjyxczcb4, ";
                fpselect += " nvl(tem4.yxczcb,0)  as yxczcb4, ";
                fpselect += " nvl(tem4.czcb,0)  as czcb4";

                fpselect += " from  dtstat_djsj sdy, dtxyjb_info xy, dtstat_pjdysj sdypjdy, ";
                fpselect += " (";
                fpselect += " select  sdy.pjdy as pjdy, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from dtstat_djsj sdy ";
                fpselect += " where sdy.gsxyjb_1 = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where  sdy.djisopen = '1'";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.pjdy ";
                fpselect += " ) tem1, ";
                fpselect += " (";
                fpselect += " select  sdy.pjdy as pjdy, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from dtstat_djsj sdy ";
                fpselect += " where sdy.gsxyjb_1 = '2' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where  sdy.djisopen = '1'";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.pjdy ";
                fpselect += " ) tem2, ";
                fpselect += " (";
                fpselect += " select  sdy.pjdy as pjdy, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from dtstat_djsj sdy ";
                fpselect += " where sdy.gsxyjb_1 = '3' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.pjdy ";
                fpselect += " ) tem3, ";
                fpselect += " (";
                fpselect += " select  sdy.pjdy as pjdy, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from dtstat_djsj sdy ";
                fpselect += " where sdy.gsxyjb_1 = '4' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.pjdy ";
                fpselect += " ) tem4 ";
                fpselect += " where";
                fpselect += "     sdypjdy.gsxyjb_1 = xy.jbid  ";
                fpselect += " and sdy.pjdy =  tem1.pjdy(+) ";
                fpselect += " and sdy.pjdy =  tem2.pjdy(+) ";
                fpselect += " and sdy.pjdy =  tem3.pjdy(+)  ";
                fpselect += " and sdy.pjdy =  tem4.pjdy(+)  ";
                fpselect += " and sdy.pjdy = sdypjdy.mc";
                //fpselect += " and sdy.cyc_id = '" + cycid + "'";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " and sdy.djisopen = '1' and sdy.jh in ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";

                fpselect += " group by xy.jbid,xy.jbmc,sdy.pjdy, ";
                fpselect += " tem1.js,tem1.hscyl,tem1.yyspl,tem1.zjyxczcb,tem1.yxczcb,tem1.czcb, ";
                fpselect += " tem2.js,tem2.hscyl,tem2.yyspl,tem2.zjyxczcb,tem2.yxczcb,tem2.czcb, ";
                fpselect += " tem3.js,tem3.hscyl,tem3.yyspl,tem3.zjyxczcb,tem3.yxczcb,tem3.czcb, ";
                fpselect += " tem4.js,tem4.hscyl,tem4.yyspl,tem4.zjyxczcb,tem4.yxczcb,tem4.czcb  ";
                fpselect += " order by jbid ";
            }
            else if (typeid == "zyq")
            {
                fpselect += " select  d.dep_name,sdy.zyq as  qk, ";
                fpselect += " nvl(round(sum(sdy.djisopen),4),0) as js,";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl, ";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb,";

                fpselect += " sum(nvl(tem1.js,0)) as js1, ";
                fpselect += " sum(nvl(tem1.hscyl,0)) as hscyl1, ";
                fpselect += " sum(nvl(tem1.yyspl,0)) as yyspl1, ";
                fpselect += " sum(nvl(tem1.zjyxczcb,0)) as zjyxczcb1,";
                fpselect += " sum(nvl(tem1.yxczcb,0)) as yxczcb1,";
                fpselect += " sum(nvl(tem1.czcb,0)) as czcb1,";

                fpselect += " sum(nvl(tem2.js,0))  as js2, ";
                fpselect += " sum(nvl(tem2.hscyl,0))  as hscyl2, ";
                fpselect += " sum(nvl(tem2.yyspl,0))  as yyspl2, ";
                fpselect += " sum(nvl(tem2.zjyxczcb,0))  as zjyxczcb2, ";
                fpselect += " sum(nvl(tem2.yxczcb,0))  as yxczcb2, ";
                fpselect += " sum(nvl(tem2.czcb,0))  as czcb2, ";

                fpselect += " sum(nvl(tem3.js,0))  as js3,";
                fpselect += " sum(nvl(tem3.hscyl,0))  as hscyl3,";
                fpselect += " sum(nvl(tem3.yyspl,0))  as yyspl3, ";
                fpselect += " sum(nvl(tem3.zjyxczcb,0))  as zjyxczcb3, ";
                fpselect += " sum(nvl(tem3.yxczcb,0))  as yxczcb3, ";
                fpselect += " sum(nvl(tem3.czcb,0))  as czcb3, ";

                fpselect += " sum(nvl(tem4.js,0))  as js4, ";
                fpselect += " sum(nvl(tem4.hscyl,0))  as hscyl4, ";
                fpselect += " sum(nvl(tem4.yyspl,0))  as yyspl4, ";
                fpselect += " sum(nvl(tem4.zjyxczcb,0))  as zjyxczcb4, ";
                fpselect += " sum(nvl(tem4.yxczcb,0))  as yxczcb4, ";
                fpselect += " sum(nvl(tem4.czcb,0))  as czcb4, ";
                fpselect += " sdy.zyqdm as zyq ";

                fpselect += " from  view_dtstat_zyqsj sdy, department d, ";

                //tem1
                fpselect += " (";
                fpselect += " select  sdy.dj_id as dj_id, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from view_dtstat_zyqsj sdy ";
                fpselect += " where sdy.gsxyjb_1 = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where  sdy.djisopen = '1'";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.dj_id ";
                fpselect += " ) tem1, ";

                //tem2
                fpselect += " (";
                fpselect += " select  sdy.dj_id as dj_id, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from view_dtstat_zyqsj sdy ";
                fpselect += " where sdy.gsxyjb_1 = '2' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where  sdy.djisopen = '1'";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.dj_id ";
                fpselect += " ) tem2, ";

                //tem3
                fpselect += " (";
                fpselect += " select  sdy.dj_id as dj_id, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from view_dtstat_zyqsj sdy ";
                fpselect += " where sdy.gsxyjb_1 = '3' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.dj_id ";
                fpselect += " ) tem3, ";

                //tem4
                fpselect += " (";
                fpselect += " select  sdy.dj_id as dj_id, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from view_dtstat_zyqsj sdy ";
                fpselect += " where sdy.gsxyjb_1 = '4' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.dj_id ";
                fpselect += " ) tem4 ";



                fpselect += " where sdy.dep_id = d.dep_id and  ";

                fpselect += "  sdy.dj_id =  tem1.dj_id(+) ";
                fpselect += " and sdy.dj_id =  tem2.dj_id(+) ";
                fpselect += " and sdy.dj_id =  tem3.dj_id(+)  ";
                fpselect += " and sdy.dj_id =  tem4.dj_id(+)  ";

                //fpselect += " and sdy.cyc_id = '" + cycid + "'";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " and sdy.djisopen = '1' and sdy.jh in ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " ) where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";

                fpselect += "group by d.dep_name,sdy.zyqdm,sdy.zyq";
                fpselect += " order by d.dep_name,sdy.zyqdm ";

            }
            try
            {
                connfp.Open();

                OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                //DataSet fpset = new DataSet();
                //da.Fill(fpset, "fpdata");
                //计算合计, 合计在绑定数据时计算           
                #region
                DataTable dt = new DataTable("fpdata"); //为数据表起一个名字,将来插入数据集中调用
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();


                    dr[0] = "";
                    dr[0] = "合计";
                    if (typeid == "zyq")
                    {
                        dr[0] = "";
                        dr[1] = "合计";
                        //初始化各列
                        for (int i = 2; i < dt.Columns.Count; i++)
                        {
                            dr[i] = 0;
                        }
                        //计算
                        for (int k = 0; k < dt.Rows.Count; k++)//循环计算各行
                        {

                            for (int n = 2; n < dt.Columns.Count - 1; n++)//循环计算各列
                            {
                                dr[n] = float.Parse(dr[n].ToString()) + float.Parse(dt.Rows[k][n].ToString());
                            }
                        }
                        //计算精度
                        for (int m = 2; m < dt.Columns.Count - 1; m++)
                        {
                            dr[m] = Math.Round(float.Parse(dr[m].ToString()), 4);
                        }
                        //把行加入表
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        dt.Columns.Remove("jbid");
                        //初始化各列
                        for (int i = 2; i < dt.Columns.Count; i++)
                        {
                            dr[i] = 0;
                        }
                        //计算
                        for (int k = 0; k < dt.Rows.Count; k++)//循环计算各行
                        {

                            for (int n = 2; n < dt.Columns.Count; n++)//循环计算各列
                            {
                                dr[n] = float.Parse(dr[n].ToString()) + float.Parse(dt.Rows[k][n].ToString());
                            }
                        }
                        //计算精度
                        for (int m = 2; m < dt.Columns.Count; m++)
                        {
                            dr[m] = Math.Round(float.Parse(dr[m].ToString()), 4);
                        }
                        //把行加入表
                        dt.Rows.Add(dr);
                    }
                }
                DataSet fpset = new DataSet();
                fpset.Tables.Add(dt);
                #endregion

                //此处用于绑定数据            
                #region
                int rcount = fpset.Tables["fpdata"].Rows.Count;
                int ccount = fpset.Tables["fpdata"].Columns.Count;
                int hcount = 5;
                //FpSpread1.ColumnHeader.Visible = false;
                //FpSpread1.RowHeader.Visible = false;
                if (typeid == "zyq")
                {
                    string path = Page.MapPath("~/static/excel/zuoyequ.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "表10-特高成本井效益情况统计表");

                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;


                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount - 1; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            if (j != 2 && j != 8 && j != 14 && j != 20 && j != 26 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString() != "0")
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                            else FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }
                    /////////////////////09.5.30合并单元格
                    for (int m = 0; m < 1; m++)  //列
                    {
                        int k = 1;  //统计重复单元格
                        int w = hcount;  //记录起始位置
                        for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                        {
                            if (FpSpread1.Sheets[0].Cells[i, m].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, m].Value.ToString())
                            {
                                k++;
                            }
                            else
                            {
                                if (k != 1)
                                {
                                    FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                                }
                                w = i;
                                k = 1;
                            }
                        }
                        FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                    }
                    //////////////////////////////////
                }
                else
                {
                    string path = Page.MapPath("~/static/excel/dongtai.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "表10-特高成本井效益情况统计表");

                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;


                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            if (j != 2 && j != 8 && j != 14 && j != 20 && j != 26 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString() != "0")
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                            else FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }
                    /////////////////////09.5.30合并单元格
                    for (int m = 0; m < 1; m++)  //列
                    {
                        int k = 1;  //统计重复单元格
                        int w = hcount;  //记录起始位置
                        for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                        {
                            if (FpSpread1.Sheets[0].Cells[i, m].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, m].Value.ToString())
                            {
                                k++;
                            }
                            else
                            {
                                if (k != 1)
                                {
                                    FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                                }
                                w = i;
                                k = 1;
                            }
                        }
                        FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                    }
                    //////////////////////////////////
                }
                /////////////////////09.4.29更新//////////////////////////////////

                if (typeid == "pjdy")
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "评价单元";
                else if (typeid == "qk")
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "评价区块";
                else if (typeid == "yt")
                {
                    FpSpread1.Sheets[0].Cells[2, 0].Value = " ";
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "采油厂";

                    FpSpread1.Sheets[0].Cells[2, 0].Value = FpSpread1.Sheets[0].Cells[2, 1].Value;
                    FpSpread1.ActiveSheetView.AddSpanCell(2, 0, 3, 2);

                    for (int i = 5; i < FpSpread1.Sheets[0].RowCount; i++)
                    {

                        FpSpread1.Sheets[0].Cells[i, 0].Value = FpSpread1.Sheets[0].Cells[i, 1].Value;

                        FpSpread1.ActiveSheetView.AddSpanCell(i, 0, 1, 2);
                        FpSpread1.ActiveSheetView.Cells[i, 0].Column.Width = 3;
                    }
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 0].Value = "合计";
                }
                else
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "作业区";
                ////////////////////////////////////////////////////////////////////
                /////////////////////////09.4.30更新//////////////////////////////////////
                if (typeid == "zyq")
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = "各作业区" + FpSpread1.Sheets[0].Cells[0, 0].Text;
                }
                else if (typeid == "pjdy")
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = "各评价单元" + FpSpread1.Sheets[0].Cells[0, 0].Text;
                }
                else if (typeid == "qk")
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = "各区块" + FpSpread1.Sheets[0].Cells[0, 0].Text;
                }
                else if (typeid == "yt")
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = "各采油厂" + FpSpread1.Sheets[0].Cells[0, 0].Text;
                }

                ////////////////////////////////////////////////////////////////////////////
                #endregion


            }
            catch (OracleException error)
            {
                string CuoWu = "错误: " + error.Message.ToString();
                Response.Write(CuoWu);

            }
            connfp.Close();
        }

        protected int isempty()
        {
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");

            SqlHelper conn = new SqlHelper();
            OracleConnection con1 = conn.GetConn();
            con1.Open();
            string ss = "select bny,eny from dtstat_djsj where bny='" + bny + "' and eny='" + eny + "'";
            OracleDataAdapter da = new OracleDataAdapter(ss, con1);
            DataSet ds = new DataSet();
            da.Fill(ds, "time");
            con1.Close();
            if (ds.Tables["time"].Rows.Count == 0)
                return 0;
            else
                return 1;

        }

        protected void DC_Click(object sender, EventArgs e)
        {
            //this.FpSpread1.Sheets[0].Cells[0, 0].Text = "您点击了‘导出按钮’";

            FpSpread1.Sheets[0].RowHeader.Visible = true;
            FpSpread1.Sheets[0].ColumnHeader.Visible = true;
            //FpSpread1.SaveExcelToResponse("dt_biao1.xls");
            FarpointGridChange.FarPointChange(FpSpread1, "dt_biao10.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;


        }

        protected void FpSpread1_SaveOrLoadSheetState(object sender, FarPoint.Web.Spread.SheetViewStateEventArgs e)
        {
            if (e.IsSave)
            {
                Session[e.SheetView.SheetName] = e.SheetView.SaveViewState();
            }
            else
            {
                e.SheetView.LoadViewState(Session[e.SheetView.SheetName]);
            }
            e.Handled = true;
        }



    }
}
