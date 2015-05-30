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
using beneDesCYC.model.system;
using beneDesCYC.core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace beneDesCYC.view.oilAssessment.cbfxb
{
    public partial class biao7 : beneDesCYC.core.UI.corePage
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
            string typeid = _getParam("targetType");
            if (typeid == "zyq")
            {
                string path = Page.MapPath("/static/excel/zuoyequ.xls");
                FpSpread1.Sheets[0].OpenExcel(path, "表7-单位操作成本区间汇总表");
            }
            else
            {
                string path = Page.MapPath("/static/excel/dongtai.xls");
                FpSpread1.Sheets[0].OpenExcel(path, "表7-单位操作成本区间汇总表");
            }
            //string path = "../../../static/excel/zuoyequ.xls";
            //path = Page.MapPath(path);
            //this.FpSpread1.Sheets[0].OpenExcel(path, "表7-单位操作成本区间汇总表");

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
            this.FpSpread1.Sheets[0].OpenExcel(path, "表7-单位操作成本区间汇总表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

        }


        protected void sj()
        {
            string cycid = Session["cyc_id"].ToString();
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //string list = _getParam("YCLX");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            string list = _getParam("CYC");
            string typeid = _getParam("targetType");

            SqlHelper conn = new SqlHelper();
            OracleConnection connfp = conn.GetConn();

            //OracleConnection connfp = DB.CreatConnection();
            string fpselect = "";
            string fpselect2 = "";
            //if (typeid == "yt")
            //{
            //    fpselect += " select '',d.dep_name as pjdy, ";
            //    fpselect += " sum(nvl(sdy.djisopen,0)) as js,";
            //    fpselect += " sum(round(nvl(sdy.hscyl/10000,0),4)) as hscyl,";
            //    fpselect += " sum(round(nvl(sdy.yyspl/10000,0),4)) as yyspl,";
            //    fpselect += " sum(round(nvl(sdy.hscql/10000/10000,0),4)) as hscql,";
            //    fpselect += " sum(round(nvl(sdy.trqspl/10000/10000,0),4)) as trqspl,";
            //    fpselect += " sum(round(nvl(sdy.czcb/10000,0),4)) as czcb,";
            //    fpselect += " round((case when sum(sdy.yqspl)=0 then 0 else nvl(sum(sdy.czcb)/sum(sdy.yqspl),0) end),2) as yqdwczcb,";
            //    fpselect += " sum(nvl(c1.js,0)) as js1,";
            //    fpselect += " sum(nvl(round(c1.hscyl,4),0)) as yql1, ";
            //    fpselect += " sum(nvl(round(c1.czcb,4),0)) as czcb1,";

            //    fpselect += " sum(nvl(c2.js,0)) as js2, ";
            //    fpselect += " sum(nvl(round(c2.hscyl,4),0)) as yql2,";
            //    fpselect += " sum(nvl(round(c2.czcb,4),0)) as czcb2,";

            //    fpselect += " sum(nvl(c3.js,0)) as js3,";
            //    fpselect += " sum(nvl(round(c3.hscyl,4),0)) as yql3,";
            //    fpselect += " sum(nvl(round(c3.czcb,4),0)) as czcb3,";

            //    fpselect += " sum(nvl(c4.js,0)) as js4,";
            //    fpselect += " sum(nvl(round(c4.hscyl,4),0)) as yql4,";
            //    fpselect += " sum(nvl(round(c4.czcb,4),0)) as czcb4,";

            //    fpselect += " sum(nvl(c5.js,0)) as js5,";
            //    fpselect += " sum(nvl(round(c5.hscyl,4),0)) as yql5,";
            //    fpselect += " sum(nvl(round(c5.czcb,4),0)) as czcb5, ";

            //    fpselect += " sum(nvl(c6.js,0)) as js6,";
            //    fpselect += " sum(nvl(round(c6.hscyl,4),0)) as yql6,";
            //    fpselect += " sum(nvl(round(c6.czcb,4),0)) as czcb6, ";

            //    fpselect += " sum(nvl(c7.js,0)) as js7,";
            //    fpselect += " sum(nvl(round(c7.hscyl,4),0)) as yql7,";
            //    fpselect += " sum(nvl(round(c7.czcb,4),0)) as czcb7, ";
            //    fpselect += " nvl(round(sum(yqspl)/10000,4),0) As yqspl";
            //    //fpselect += " sdy.zyq as zyq ";


            //    fpselect += "  from view_dtstat_zyqsj sdy ,department d, ";


            //    //c1
            //    fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
            //    fpselect += "  from ";
            //    fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where  sdy.djisopen = '1' ";
            //    if (list == "quan")
            //    {
            //        fpselect += "";
            //    }
            //    else
            //    {
            //        fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            //    }
            //    fpselect += " ) tem ";
            //    fpselect += "  where tem.dwcb <= '200'";
            //    fpselect += "  group by tem.dj_id ) c1,";


            //    //c2
            //    fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
            //    fpselect += "  from ";
            //    fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where  sdy.djisopen = '1' ";
            //    if (list == "quan")
            //    {
            //        fpselect += "";
            //    }
            //    else
            //    {
            //        fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            //    }
            //    fpselect += " ) tem ";
            //    fpselect += "  where tem.dwcb <= '400'and dwcb > '200' ";
            //    fpselect += "  group by tem.dj_id ) c2, ";


            //    //c3
            //    fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
            //    fpselect += "  from ";
            //    fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where  sdy.djisopen = '1' ";
            //    if (list == "quan")
            //    {
            //        fpselect += "";
            //    }
            //    else
            //    {
            //        fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            //    }
            //    fpselect += "  ) tem ";
            //    fpselect += "  where tem.dwcb <= '600' and dwcb > '400'";
            //    fpselect += "  group by tem.dj_id ) c3,";


            //    //c4
            //    fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
            //    fpselect += "  from ";
            //    fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where  sdy.djisopen = '1' ";
            //    if (list == "quan")
            //    {
            //        fpselect += "";
            //    }
            //    else
            //    {
            //        fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            //    }
            //    fpselect += " ) tem ";
            //    fpselect += " where tem.dwcb <= '850' and dwcb > '600'";
            //    fpselect += "  group by tem.dj_id ) c4 ,";


            //    //c5
            //    fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
            //    fpselect += "  from ";
            //    fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where  sdy.djisopen = '1' ";
            //    if (list == "quan")
            //    {
            //        fpselect += "";
            //    }
            //    else
            //    {
            //        fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            //    }
            //    fpselect += " ) tem ";
            //    fpselect += "  where tem.dwcb <= '1000' and dwcb> '850'";
            //    fpselect += " group by tem.dj_id ) c5, ";


            //    //c6
            //    fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
            //    fpselect += "  from ";
            //    fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where  sdy.djisopen = '1' ";
            //    if (list == "quan")
            //    {
            //        fpselect += "";
            //    }
            //    else
            //    {
            //        fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            //    }
            //    fpselect += "  ) tem ";
            //    fpselect += " where tem.dwcb <= '2000' and dwcb > '1000'";
            //    fpselect += " group by tem.dj_id ) c6, ";


            //    //c7
            //    fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
            //    fpselect += "  from ";
            //    fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where  sdy.djisopen = '1' ";
            //    if (list == "quan")
            //    {
            //        fpselect += "";
            //    }
            //    else
            //    {
            //        fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            //    }
            //    fpselect += "  ) tem ";
            //    fpselect += " where tem.dwcb > '2000'";
            //    fpselect += "  group by tem.dj_id ) c7";

            //    fpselect += " where sdy.dep_id=d.dep_id and sdy.djisopen= '1'  and  sdy.dj_id=c1.dj_id(+) and sdy.dj_id=c2.dj_id(+) ";
            //    fpselect += " and sdy.dj_id=c3.dj_id(+) and sdy.dj_id=c4.dj_id(+) and sdy.dj_id=c5.dj_id(+) ";
            //    fpselect += " and sdy.dj_id=c6.dj_id(+) and sdy.dj_id=c7.dj_id(+) ";
            //    // fpselect += " and bny='" + bny.Trim() + "'and eny='" + eny.Trim() + "'";
            //    if (list == "quan")
            //    {
            //        fpselect += "";
            //    }
            //    else
            //    {
            //        fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            //    }
            //    fpselect += "group by d.dep_name ";
            //    fpselect += " order by d.dep_name ";

            //}
            if (typeid == "pjdy")
            {
                fpselect += " select xy.jbmc as jbmc, sdy.mc as pjdy, ";
                fpselect += " nvl(sum(sdy.yjkjs),0) as js,";
                fpselect += " round(nvl(sum(sdy.cyoul)/10000,0),4) as hscyl,";
                fpselect += " round(nvl(sum(sdy.yyspl)/10000,0),4) as yyspl,";
                fpselect += " round(nvl(sum(sdy.cql)/10000,0),4) as hscql,";
                fpselect += " round(nvl(sum(sdy.trqspl)/10000,0),4) as trqspl,";
                fpselect += " round(nvl(sum(sdy.czcb)/10000,0),4) as czcb,";
                fpselect += " round((case when sum(sdy.yqspl)=0 then 0 else nvl(sum(sdy.czcb)/sum(sdy.yqspl),0) end),2) as yqdwczcb,";

                fpselect += " nvl(sum(c1.js),0) as js1,";
                fpselect += " nvl(round(sum(c1.hscyl),4),0) as yql1, ";
                fpselect += " nvl(round(sum(c1.czcb),4),0) as czcb1,";

                fpselect += " nvl(sum(c2.js),0) as js2, ";
                fpselect += " nvl(round(sum(c2.hscyl),4),0) as yql2,";
                fpselect += " nvl(round(sum(c2.czcb),4),0) as czcb2,";

                fpselect += " nvl(sum(c3.js),0) as js3,";
                fpselect += " nvl(round(sum(c3.hscyl),4),0) as yql3,";
                fpselect += " nvl(round(sum(c3.czcb),4),0) as czcb3,";

                fpselect += " nvl(sum(c4.js),0) as js4,";
                fpselect += " nvl(round(sum(c4.hscyl),4),0) as yql4,";
                fpselect += " nvl(round(sum(c4.czcb),4),0) as czcb4,";

                fpselect += " nvl(sum(c5.js),0) as js5,";
                fpselect += " nvl(round(sum(c5.hscyl),4),0) as yql5,";
                fpselect += " nvl(round(sum(c5.czcb),4),0) as czcb5, ";

                fpselect += " nvl(sum(c6.js),0) as js6,";
                fpselect += " nvl(round(sum(c6.hscyl),4),0) as yql6,";
                fpselect += " nvl(round(sum(c6.czcb),4),0) as czcb6, ";

                fpselect += " nvl(sum(c7.js),0) as js7,";
                fpselect += " nvl(round(sum(c7.hscyl),4),0) as yql7,";
                fpselect += " nvl(round(sum(c7.czcb),4),0) as czcb7, ";

                fpselect += " nvl(round(sum(yqspl)/10000,4),0) As yqspl";


                fpselect += "  from dtstat_pjdysj sdy,dtxyjb_info xy,";

                fpselect += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += "  where tem.dwcb <= '200'";
                fpselect += "  group by tem.pjdy ) c1,";

                fpselect += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += "  where tem.dwcb <= '400'and dwcb > '200' ";
                fpselect += "  group by tem.pjdy ) c2, ";

                fpselect += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += "  where tem.dwcb <= '600' and dwcb > '400'";
                fpselect += "  group by tem.pjdy ) c3,";

                fpselect += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += " where tem.dwcb <= '850' and dwcb > '600'";
                fpselect += "  group by tem.pjdy ) c4 ,";

                fpselect += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += "  where tem.dwcb <= '1000' and dwcb> '850'";
                fpselect += " group by tem.pjdy ) c5, ";

                fpselect += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += " where tem.dwcb <= '2000' and dwcb > '1000'";
                fpselect += " group by tem.pjdy ) c6, ";

                fpselect += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += " where tem.dwcb > '2000'";
                fpselect += "  group by tem.pjdy ) c7";

                fpselect += " where sdy.sfpj = '1' and sdy.mc=c1.pjdy(+) and sdy.mc=c2.pjdy(+) ";
                fpselect += " and sdy.mc=c3.pjdy(+) and sdy.mc=c4.pjdy(+) and sdy.mc=c5.pjdy(+) ";
                fpselect += " and sdy.mc=c6.pjdy(+) and sdy.mc=c7.pjdy(+) ";
                fpselect += " and sdy.gsxyjb_1 = xy.jbid and sdy.cyc_id = '" + cycid + "'";
                fpselect += " group by xy.jbmc , sdy.mc ";
                fpselect += " order by xy.jbmc";
                ///////////////////////////////////////////////09.5.26合计
                fpselect2 += " select null as jbmc, '合计' as pjdy, ";
                fpselect2 += " nvl(sum(sdy.yjkjs),0) as js,";
                fpselect2 += " round(nvl(sum(sdy.cyoul)/10000,0),4) as hscyl,";
                fpselect2 += " round(nvl(sum(sdy.yyspl)/10000,0),4) as yyspl,";
                fpselect2 += " round(nvl(sum(sdy.cql)/10000,0),4) as hscql,";
                fpselect2 += " round(nvl(sum(sdy.trqspl)/10000,0),4) as trqspl,";
                fpselect2 += " round(nvl(sum(sdy.czcb)/10000,0),4) as czcb,";
                fpselect2 += " round((case when sum(sdy.yqspl)=0 then 0 else nvl(sum(sdy.czcb)/sum(sdy.yqspl),0) end),2) as yqdwczcb,";

                fpselect2 += " nvl(sum(c1.js),0) as js1,";
                fpselect2 += " nvl(round(sum(c1.hscyl),4),0) as yql1, ";
                fpselect2 += " nvl(round(sum(c1.czcb),4),0) as czcb1,";

                fpselect2 += " nvl(sum(c2.js),0) as js2, ";
                fpselect2 += " nvl(round(sum(c2.hscyl),4),0) as yql2,";
                fpselect2 += " nvl(round(sum(c2.czcb),4),0) as czcb2,";

                fpselect2 += " nvl(sum(c3.js),0) as js3,";
                fpselect2 += " nvl(round(sum(c3.hscyl),4),0) as yql3,";
                fpselect2 += " nvl(round(sum(c3.czcb),4),0) as czcb3,";

                fpselect2 += " nvl(sum(c4.js),0) as js4,";
                fpselect2 += " nvl(round(sum(c4.hscyl),4),0) as yql4,";
                fpselect2 += " nvl(round(sum(c4.czcb),4),0) as czcb4,";

                fpselect2 += " nvl(sum(c5.js),0) as js5,";
                fpselect2 += " nvl(round(sum(c5.hscyl),4),0) as yql5,";
                fpselect2 += " nvl(round(sum(c5.czcb),4),0) as czcb5, ";

                fpselect2 += " nvl(sum(c6.js),0) as js6,";
                fpselect2 += " nvl(round(sum(c6.hscyl),4),0) as yql6,";
                fpselect2 += " nvl(round(sum(c6.czcb),4),0) as czcb6, ";

                fpselect2 += " nvl(sum(c7.js),0) as js7,";
                fpselect2 += " nvl(round(sum(c7.hscyl),4),0) as yql7,";
                fpselect2 += " nvl(round(sum(c7.czcb),4),0) as czcb7, ";

                fpselect2 += " nvl(round(sum(yqspl)/10000,4),0) As yqspl";


                fpselect2 += "  from dtstat_pjdysj sdy,dtxyjb_info xy,";

                fpselect2 += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += "  where tem.dwcb <= '200'";
                fpselect2 += "  group by tem.pjdy ) c1,";

                fpselect2 += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += "  where tem.dwcb <= '400'and dwcb > '200' ";
                fpselect2 += "  group by tem.pjdy ) c2, ";

                fpselect2 += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += "  where tem.dwcb <= '600' and dwcb > '400'";
                fpselect2 += "  group by tem.pjdy ) c3,";

                fpselect2 += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += " where tem.dwcb <= '850' and dwcb > '600'";
                fpselect2 += "  group by tem.pjdy ) c4 ,";

                fpselect2 += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += "  where tem.dwcb <= '1000' and dwcb> '850'";
                fpselect2 += " group by tem.pjdy ) c5, ";

                fpselect2 += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += " where tem.dwcb <= '2000' and dwcb > '1000'";
                fpselect2 += " group by tem.pjdy ) c6, ";

                fpselect2 += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += " where tem.dwcb > '2000'";
                fpselect2 += "  group by tem.pjdy ) c7";

                fpselect2 += " where sdy.sfpj = '1' and sdy.mc=c1.pjdy(+) and sdy.mc=c2.pjdy(+) ";
                fpselect2 += " and sdy.mc=c3.pjdy(+) and sdy.mc=c4.pjdy(+) and sdy.mc=c5.pjdy(+) ";
                fpselect2 += " and sdy.mc=c6.pjdy(+) and sdy.mc=c7.pjdy(+) ";
                fpselect2 += " and sdy.gsxyjb_1 = xy.jbid and sdy.cyc_id = '" + cycid + "'";

                //////////////////////////////////////////////////////////
            }
            else if (typeid == "qk")
            {
                fpselect += " select xy.jbmc as jbmc, sdy.mc as pjdy, ";
                fpselect += " nvl(sum(sdy.yjkjs),0) as js,";
                fpselect += " round(nvl(sum(sdy.cyoul)/10000,0),4) as hscyl,";
                fpselect += " round(nvl(sum(sdy.yyspl)/10000,0),4) as yyspl,";
                fpselect += " round(nvl(sum(sdy.cql)/10000,0),4) as hscql,";
                fpselect += " round(nvl(sum(sdy.trqspl)/10000,0),4) as trqspl,";
                fpselect += " round(nvl(sum(sdy.czcb)/10000,0),4) as czcb,";
                fpselect += " round((case when sum(sdy.yqspl)=0 then 0 else nvl(sum(sdy.czcb)/sum(sdy.yqspl),0) end),2) as yqdwczcb,";

                fpselect += " nvl(sum(c1.js),0) as js1,";
                fpselect += " nvl(round(sum(c1.hscyl),4),0) as yql1, ";
                fpselect += " nvl(round(sum(c1.czcb),4),0) as czcb1,";

                fpselect += " nvl(sum(c2.js),0) as js2, ";
                fpselect += " nvl(round(sum(c2.hscyl),4),0) as yql2,";
                fpselect += " nvl(round(sum(c2.czcb),4),0) as czcb2,";

                fpselect += " nvl(sum(c3.js),0) as js3,";
                fpselect += " nvl(round(sum(c3.hscyl),4),0) as yql3,";
                fpselect += " nvl(round(sum(c3.czcb),4),0) as czcb3,";

                fpselect += " nvl(sum(c4.js),0) as js4,";
                fpselect += " nvl(round(sum(c4.hscyl),4),0) as yql4,";
                fpselect += " nvl(round(sum(c4.czcb),4),0) as czcb4,";

                fpselect += " nvl(sum(c5.js),0) as js5,";
                fpselect += " nvl(round(sum(c5.hscyl),4),0) as yql5,";
                fpselect += " nvl(round(sum(c5.czcb),4),0) as czcb5, ";

                fpselect += " nvl(sum(c6.js),0) as js6,";
                fpselect += " nvl(round(sum(c6.hscyl),4),0) as yql6,";
                fpselect += " nvl(round(sum(c6.czcb),4),0) as czcb6, ";

                fpselect += " nvl(sum(c7.js),0) as js7,";
                fpselect += " nvl(round(sum(c7.hscyl),4),0) as yql7,";
                fpselect += " nvl(round(sum(c7.czcb),4),0) as czcb7, ";

                fpselect += " nvl(round(sum(yqspl)/10000,4),0) As yqspl";


                fpselect += "  from dtstat_qksj sdy,dtxyjb_info xy,";

                fpselect += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += "  where tem.dwcb <= '200'";
                fpselect += "  group by tem.qk ) c1,";

                fpselect += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += "  where tem.dwcb <= '400'and dwcb > '200' ";
                fpselect += "  group by tem.qk ) c2, ";

                fpselect += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += "  where tem.dwcb <= '600' and dwcb > '400'";
                fpselect += "  group by tem.qk ) c3,";

                fpselect += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += " where tem.dwcb <= '850' and dwcb > '600'";
                fpselect += "  group by tem.qk ) c4 ,";

                fpselect += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += "  where tem.dwcb <= '1000' and dwcb> '850'";
                fpselect += " group by tem.qk ) c5, ";

                fpselect += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += " where tem.dwcb <= '2000' and dwcb > '1000'";
                fpselect += " group by tem.qk ) c6, ";

                fpselect += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += " where tem.dwcb > '2000'";
                fpselect += "  group by tem.qk ) c7";

                fpselect += " where sdy.sfpj = '1' and sdy.mc=c1.qk(+) and sdy.mc=c2.qk(+) ";
                fpselect += " and sdy.mc=c3.qk(+) and sdy.mc=c4.qk(+) and sdy.mc=c5.qk(+) ";
                fpselect += " and sdy.mc=c6.qk(+) and sdy.mc=c7.qk(+) ";
                fpselect += " and sdy.gsxyjb_1 = xy.jbid and sdy.cyc_id = '" + cycid + "'";
                fpselect += " group by xy.jbmc , sdy.mc ";
                fpselect += " order by xy.jbmc";
                ///////////////////////////////////////09.5.26合计
                fpselect2 += " select null as jbmc, '合计' as pjdy, ";
                fpselect2 += " nvl(sum(sdy.yjkjs),0) as js,";
                fpselect2 += " round(nvl(sum(sdy.cyoul)/10000,0),4) as hscyl,";
                fpselect2 += " round(nvl(sum(sdy.yyspl)/10000,0),4) as yyspl,";
                fpselect2 += " round(nvl(sum(sdy.cql)/10000,0),4) as hscql,";
                fpselect2 += " round(nvl(sum(sdy.trqspl)/10000,0),4) as trqspl,";
                fpselect2 += " round(nvl(sum(sdy.czcb)/10000,0),4) as czcb,";
                fpselect2 += " round((case when sum(sdy.yqspl)=0 then 0 else nvl(sum(sdy.czcb)/sum(sdy.yqspl),0) end),2) as yqdwczcb,";

                fpselect2 += " nvl(sum(c1.js),0) as js1,";
                fpselect2 += " nvl(round(sum(c1.hscyl),4),0) as yql1, ";
                fpselect2 += " nvl(round(sum(c1.czcb),4),0) as czcb1,";

                fpselect2 += " nvl(sum(c2.js),0) as js2, ";
                fpselect2 += " nvl(round(sum(c2.hscyl),4),0) as yql2,";
                fpselect2 += " nvl(round(sum(c2.czcb),4),0) as czcb2,";

                fpselect2 += " nvl(sum(c3.js),0) as js3,";
                fpselect2 += " nvl(round(sum(c3.hscyl),4),0) as yql3,";
                fpselect2 += " nvl(round(sum(c3.czcb),4),0) as czcb3,";

                fpselect2 += " nvl(sum(c4.js),0) as js4,";
                fpselect2 += " nvl(round(sum(c4.hscyl),4),0) as yql4,";
                fpselect2 += " nvl(round(sum(c4.czcb),4),0) as czcb4,";

                fpselect2 += " nvl(sum(c5.js),0) as js5,";
                fpselect2 += " nvl(round(sum(c5.hscyl),4),0) as yql5,";
                fpselect2 += " nvl(round(sum(c5.czcb),4),0) as czcb5, ";

                fpselect2 += " nvl(sum(c6.js),0) as js6,";
                fpselect2 += " nvl(round(sum(c6.hscyl),4),0) as yql6,";
                fpselect2 += " nvl(round(sum(c6.czcb),4),0) as czcb6, ";

                fpselect2 += " nvl(sum(c7.js),0) as js7,";
                fpselect2 += " nvl(round(sum(c7.hscyl),4),0) as yql7,";
                fpselect2 += " nvl(round(sum(c7.czcb),4),0) as czcb7, ";

                fpselect2 += " nvl(round(sum(yqspl)/10000,4),0) As yqspl";


                fpselect2 += "  from dtstat_qksj sdy,dtxyjb_info xy,";

                fpselect2 += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += "  where tem.dwcb <= '200'";
                fpselect2 += "  group by tem.qk ) c1,";

                fpselect2 += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += "  where tem.dwcb <= '400'and dwcb > '200' ";
                fpselect2 += "  group by tem.qk ) c2, ";

                fpselect2 += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += "  where tem.dwcb <= '600' and dwcb > '400'";
                fpselect2 += "  group by tem.qk ) c3,";

                fpselect2 += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += " where tem.dwcb <= '850' and dwcb > '600'";
                fpselect2 += "  group by tem.qk ) c4 ,";

                fpselect2 += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += "  where tem.dwcb <= '1000' and dwcb> '850'";
                fpselect2 += " group by tem.qk ) c5, ";

                fpselect2 += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += " where tem.dwcb <= '2000' and dwcb > '1000'";
                fpselect2 += " group by tem.qk ) c6, ";

                fpselect2 += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += " where tem.dwcb > '2000'";
                fpselect2 += "  group by tem.qk ) c7";

                fpselect2 += " where sdy.sfpj = '1' and sdy.mc=c1.qk(+) and sdy.mc=c2.qk(+) ";
                fpselect2 += " and sdy.mc=c3.qk(+) and sdy.mc=c4.qk(+) and sdy.mc=c5.qk(+) ";
                fpselect2 += " and sdy.mc=c6.qk(+) and sdy.mc=c7.qk(+) ";
                fpselect2 += " and sdy.gsxyjb_1 = xy.jbid and sdy.cyc_id = '" + cycid + "'";
                //////////////////////////////////////////////////
            }
            else if (typeid == "zyq")
            {
                fpselect += " select d.dep_name as pjdy, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js,";
                fpselect += " round(nvl(sum(sdy.hscyl)/10000,0),4) as hscyl,";
                fpselect += " round(nvl(sum(sdy.yyspl)/10000,0),4) as yyspl,";
                fpselect += " round(nvl(sum(sdy.hscql)/10000/10000,0),4) as hscql,";
                fpselect += " round(nvl(sum(sdy.trqspl)/10000/10000,0),4) as trqspl,";
                fpselect += " round(nvl(sum(sdy.czcb)/10000,0),4) as czcb,";
                fpselect += " round((case when sum(sdy.yqspl)=0 then 0 else nvl(sum(sdy.czcb)/sum(sdy.yqspl),0) end),2) as yqdwczcb,";

                fpselect += " nvl(sum(c1.js),0) as js1,";
                fpselect += " nvl(round(sum(c1.hscyl),4),0) as yql1, ";
                fpselect += " nvl(round(sum(c1.czcb),4),0) as czcb1,";

                fpselect += " nvl(sum(c2.js),0) as js2, ";
                fpselect += " nvl(round(sum(c2.hscyl),4),0) as yql2,";
                fpselect += " nvl(round(sum(c2.czcb),4),0) as czcb2,";

                fpselect += " nvl(sum(c3.js),0) as js3,";
                fpselect += " nvl(round(sum(c3.hscyl),4),0) as yql3,";
                fpselect += " nvl(round(sum(c3.czcb),4),0) as czcb3,";

                fpselect += " nvl(sum(c4.js),0) as js4,";
                fpselect += " nvl(round(sum(c4.hscyl),4),0) as yql4,";
                fpselect += " nvl(round(sum(c4.czcb),4),0) as czcb4,";

                fpselect += " nvl(sum(c5.js),0) as js5,";
                fpselect += " nvl(round(sum(c5.hscyl),4),0) as yql5,";
                fpselect += " nvl(round(sum(c5.czcb),4),0) as czcb5, ";

                fpselect += " nvl(sum(c6.js),0) as js6,";
                fpselect += " nvl(round(sum(c6.hscyl),4),0) as yql6,";
                fpselect += " nvl(round(sum(c6.czcb),4),0) as czcb6, ";

                fpselect += " nvl(sum(c7.js),0) as js7,";
                fpselect += " nvl(round(sum(c7.hscyl),4),0) as yql7,";
                fpselect += " nvl(round(sum(c7.czcb),4),0) as czcb7, ";

                fpselect += " nvl(round(sum(yqspl)/10000,4),0) As yqspl,";
                fpselect += " sdy.zyqdm as zyq ";

                fpselect += "  from view_dtstat_zyqsj sdy,department d ,";

                fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += "  where tem.dwcb <= '200'";
                fpselect += "  group by tem.dj_id ) c1,";

                fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += "  where tem.dwcb <= '400'and dwcb > '200' ";
                fpselect += "  group by tem.dj_id ) c2, ";

                fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += "  where tem.dwcb <= '600' and dwcb > '400'";
                fpselect += "  group by tem.dj_id ) c3,";

                fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += " where tem.dwcb <= '850' and dwcb > '600'";
                fpselect += "  group by tem.dj_id ) c4 ,";

                fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += "  where tem.dwcb <= '1000' and dwcb> '850'";
                fpselect += " group by tem.dj_id ) c5, ";

                fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += " where tem.dwcb <= '2000' and dwcb > '1000'";
                fpselect += " group by tem.dj_id ) c6, ";

                fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect += " where tem.dwcb > '2000'";
                fpselect += "  group by tem.dj_id ) c7";

                fpselect += " where sdy.dj_id =c1.dj_id(+) and sdy.dj_id=c2.dj_id(+)  ";
                fpselect += " and sdy.dj_id=c3.dj_id(+) and sdy.dj_id=c4.dj_id(+) and sdy.dj_id=c5.dj_id(+) ";
                fpselect += " and sdy.dj_id=c6.dj_id(+) and sdy.dj_id=c7.dj_id(+) ";
                fpselect += " and sdy.cyc_id = '" + cycid + "'";
                fpselect += " and  sdy.cyc_id = d.parent_id and sdy.zyqdm = d.dep_id  ";
                fpselect += " group by sdy.zyqdm, d.dep_name ";
                fpselect += " order by sdy.zyqdm ";

                //////////////////////////////////////09.5.26合计
                fpselect2 += " select   '合计' as pjdy, ";
                fpselect2 += " nvl(sum(sdy.djisopen),0) as js,";
                fpselect2 += " round(nvl(sum(sdy.hscyl)/10000,0),4) as hscyl,";
                fpselect2 += " round(nvl(sum(sdy.yyspl)/10000,0),4) as yyspl,";
                fpselect2 += " round(nvl(sum(sdy.hscql)/10000/10000,0),4) as hscql,";
                fpselect2 += " round(nvl(sum(sdy.trqspl)/10000/10000,0),4) as trqspl,";
                fpselect2 += " round(nvl(sum(sdy.czcb)/10000,0),4) as czcb,";
                fpselect2 += " round((case when sum(sdy.yqspl)=0 then 0 else nvl(sum(sdy.czcb)/sum(sdy.yqspl),0) end),2) as yqdwczcb,";

                fpselect2 += " nvl(sum(c1.js),0) as js1,";
                fpselect2 += " nvl(round(sum(c1.hscyl),4),0) as yql1, ";
                fpselect2 += " nvl(round(sum(c1.czcb),4),0) as czcb1,";

                fpselect2 += " nvl(sum(c2.js),0) as js2, ";
                fpselect2 += " nvl(round(sum(c2.hscyl),4),0) as yql2,";
                fpselect2 += " nvl(round(sum(c2.czcb),4),0) as czcb2,";

                fpselect2 += " nvl(sum(c3.js),0) as js3,";
                fpselect2 += " nvl(round(sum(c3.hscyl),4),0) as yql3,";
                fpselect2 += " nvl(round(sum(c3.czcb),4),0) as czcb3,";

                fpselect2 += " nvl(sum(c4.js),0) as js4,";
                fpselect2 += " nvl(round(sum(c4.hscyl),4),0) as yql4,";
                fpselect2 += " nvl(round(sum(c4.czcb),4),0) as czcb4,";

                fpselect2 += " nvl(sum(c5.js),0) as js5,";
                fpselect2 += " nvl(round(sum(c5.hscyl),4),0) as yql5,";
                fpselect2 += " nvl(round(sum(c5.czcb),4),0) as czcb5, ";

                fpselect2 += " nvl(sum(c6.js),0) as js6,";
                fpselect2 += " nvl(round(sum(c6.hscyl),4),0) as yql6,";
                fpselect2 += " nvl(round(sum(c6.czcb),4),0) as czcb6, ";

                fpselect2 += " nvl(sum(c7.js),0) as js7,";
                fpselect2 += " nvl(round(sum(c7.hscyl),4),0) as yql7,";
                fpselect2 += " nvl(round(sum(c7.czcb),4),0) as czcb7, ";

                fpselect2 += " nvl(round(sum(yqspl)/10000,4),0) As yqspl,";
                fpselect2 += " null as zyq ";

                fpselect2 += "  from view_dtstat_zyqsj sdy,department d ,";

                fpselect2 += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += "  where tem.dwcb <= '200'";
                fpselect2 += "  group by tem.dj_id ) c1,";

                fpselect2 += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += "  where tem.dwcb <= '400'and dwcb > '200' ";
                fpselect2 += "  group by tem.dj_id ) c2, ";

                fpselect2 += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += "  where tem.dwcb <= '600' and dwcb > '400'";
                fpselect2 += "  group by tem.dj_id ) c3,";

                fpselect2 += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += " where tem.dwcb <= '850' and dwcb > '600'";
                fpselect2 += "  group by tem.dj_id ) c4 ,";

                fpselect2 += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += "  where tem.dwcb <= '1000' and dwcb> '850'";
                fpselect2 += " group by tem.dj_id ) c5, ";

                fpselect2 += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += " where tem.dwcb <= '2000' and dwcb > '1000'";
                fpselect2 += " group by tem.dj_id ) c6, ";

                fpselect2 += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect2 += "  from ";
                fpselect2 += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_dtstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1' ) tem ";
                fpselect2 += " where tem.dwcb > '2000'";
                fpselect2 += "  group by tem.dj_id ) c7";

                fpselect2 += " where sdy.dj_id =c1.dj_id(+) and sdy.dj_id=c2.dj_id(+)  ";
                fpselect2 += " and sdy.dj_id=c3.dj_id(+) and sdy.dj_id=c4.dj_id(+) and sdy.dj_id=c5.dj_id(+) ";
                fpselect2 += " and sdy.dj_id=c6.dj_id(+) and sdy.dj_id=c7.dj_id(+) ";
                fpselect2 += " and sdy.cyc_id = '" + cycid + "'";
                fpselect2 += " and  sdy.cyc_id = d.parent_id and sdy.zyqdm = d.dep_id  ";

                ////////////////////////////////////////////////////

            }


            try
            {
                connfp.Open();

                OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                OracleDataAdapter da2 = new OracleDataAdapter(fpselect2, connfp);
                DataSet fpset = new DataSet();
                da.Fill(fpset, "fpdata");
                da2.Fill(fpset, "fpdata2");
                //DataTable dt = new DataTable("fpdata"); //为数据表起一个名字,将来插入数据集中调用
                //da.Fill(dt);
                //if (dt.Rows.Count > 0)
                //{
                //    //计算合计
                //    #region
                //    DataRow dr = dt.NewRow();
                //    dr[0] = "";
                //    dr[0] = "合计";
                //    if (typeid == "zyq")
                //    {
                //        dr[0] = "";
                //        dr[1] = "合计";
                //        //初始化各列
                //        for (int i = 2; i < dt.Columns.Count; i++)
                //        {
                //            dr[i] = 0;
                //        }
                //        //计算
                //        for (int k = 0; k < dt.Rows.Count; k++)//循环行
                //        {
                //            for (int n = 2; n < dt.Columns.Count - 1; n++)//循环计算列
                //            {
                //                dr[n] = double.Parse(dr[n].ToString()) + double.Parse(dt.Rows[k][n].ToString());
                //            }
                //        }
                //        dr[8] = double.Parse(dr[7].ToString()) / double.Parse(dr[4].ToString());

                //        //计算精度
                //        for (int m = 2; m < dt.Columns.Count - 1; m++)
                //        {
                //            dr[m] = Math.Round(double.Parse(dr[m].ToString()), 4);
                //        }
                //        dr[8] = Math.Round(double.Parse(dr[8].ToString()), 2);
                //        //把行加入表
                //        dt.Rows.Add(dr);
                //    }
                //    else
                //    {
                //        //初始化各列
                //        for (int i = 2; i < dt.Columns.Count; i++)
                //        {
                //            dr[i] = 0;
                //        }
                //        //计算
                //        for (int k = 0; k < dt.Rows.Count; k++)//循环行
                //        {
                //            for (int n = 2; n < dt.Columns.Count; n++)//循环计算列
                //            {
                //                dr[n] = double.Parse(dr[n].ToString()) + double.Parse(dt.Rows[k][n].ToString());//一列值计算
                //            }
                //        }
                //        dr[8] = double.Parse(dr[7].ToString()) / double.Parse(dr[4].ToString());

                //        //计算精度
                //        for (int m = 2; m < dt.Columns.Count; m++)
                //        {
                //            dr[m] = Math.Round(double.Parse(dr[m].ToString()), 4);
                //        }
                //        dr[8] = Math.Round(double.Parse(dr[8].ToString()), 2);
                //        //把行加入表
                //        dt.Rows.Add(dr);
                //    }

                //}
                //    #endregion
                ////此处用于绑定数据    
                //DataSet fpset = new DataSet();
                //fpset.Tables.Add(dt);

                //DataSet fpset = new DataSet();
                //da.Fill(fpset, "fpdata");

                #region
                int rcount = fpset.Tables["fpdata"].Rows.Count;
                int ccount = fpset.Tables["fpdata"].Columns.Count - 1;
                int hcount = 5;
                //FpSpread1.ColumnHeader.Visible = false;
                //FpSpread1.RowHeader.Visible = false;
                //if (FpSpread1.Sheets[0].Rows.Count == hcount)
                //{
                if (typeid == "zyq")
                {
                    string path = Page.MapPath("../../../static/excel/zuoyequ.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "表7-单位操作成本区间汇总表");
                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount - 1; j++)
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }

                    FpSpread1.Sheets[0].AddRows(hcount + rcount, 1);
                    for (int i = 0; i < 1; i++)
                    {
                        for (int j = 0; j < ccount - 1; j++)
                        {
                            FpSpread1.Sheets[0].Cells[hcount + rcount, j].Value = fpset.Tables["fpdata2"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[hcount + rcount, j].Font.Size = 9;
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
                    string path = Page.MapPath("../../../static/excel/dongtai.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "表7-单位操作成本区间汇总表");
                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }

                    FpSpread1.Sheets[0].AddRows(hcount + rcount, 1);
                    for (int i = 0; i < 1; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            FpSpread1.Sheets[0].Cells[hcount + rcount, j].Value = fpset.Tables["fpdata2"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[hcount + rcount, j].Font.Size = 9;
                        }
                    }

                    /////////////////////09.5.30合并单元格
                    for (int m = 0; m < 2; m++)  //列
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
                if (typeid == "yj")
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "井数";
                else if (typeid == "pjdy")
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "评价单元";
                else if (typeid == "qk")
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "评价区块";
                else
                    FpSpread1.Sheets[0].Cells[2, 0].Value = "作业区";
                ////////////////////////////////////////////////////////////////////
                /////////////////////////09.4.30更新//////////////////////////////////////
                //if (typeid == "yt")
                //{
                //    FpSpread1.Sheets[0].Cells[2, 1].Value = "采油厂";

                //    FpSpread1.Sheets[0].Cells[2, 0].Value = FpSpread1.Sheets[0].Cells[2, 1].Value;
                //    FpSpread1.ActiveSheetView.AddSpanCell(2, 0, 3, 2);

                //    for (int i = 5; i < FpSpread1.Sheets[0].RowCount; i++)
                //    {

                //        FpSpread1.Sheets[0].Cells[i, 0].Value = FpSpread1.Sheets[0].Cells[i, 1].Value;

                //        FpSpread1.ActiveSheetView.AddSpanCell(i, 0, 1, 2);
                //        FpSpread1.ActiveSheetView.Cells[i, 0].Column.Width = 3;
                //    }
                //    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 0].Value = "合计";
                //}
                if (typeid == "zyq")
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text + "(作业区)";
                }
                else if (typeid == "pjdy")
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text + "(评价单元)";
                }
                else if (typeid == "qk")
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text + "(区块)";
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
            FarpointGridChange.FarPointChange(FpSpread1, "biao7.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;


        }

    }
}
