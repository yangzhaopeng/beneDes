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

namespace beneDesYGS.view.stockAssessment.ytgspjBiao
{
    public partial class biao7 : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string typeid = _getParam("targetType");
            if (typeid == "zyq")
                initSpread();
            else
                initSpread2();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore(); 
        }

        protected void initSpread()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/zuoyequ1.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表7-单位操作成本区间汇总表");

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
            string path = "../../../static/excel/gufenbiao.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表7-单位操作成本区间汇总表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

            //this.FpSpread1.Sheets[0].Cells[3, 1].Text = _getParam("targetType");
            //this.FpSpread1.Sheets[0].Cells[4, 1].Text = _getParam("startMonth") + " -- " + _getParam("endMonth");

            if (isempty() == 0)
            { Response.Write("<script>alert('无数据')</script>"); }
            else
            { sj(); }
        }

        protected void sj()
        {

            //JObject obj = new JObject();
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //string list = _getParam("YCLX");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            string list = _getParam("CYC");
            string typeid = _getParam("targetType");

            //string cycid = Session["cyc"].ToString();
            SqlHelper conn = new SqlHelper();
            OracleConnection connfp = conn.GetConn();
            string fpselect = "";
            if (typeid == "pjdy")
            {
                fpselect += " select xy.xymc as xymc, sdy.mc as pjdy, ";
                fpselect += " nvl(sdy.yjkjs,0) as js,";
                fpselect += " round(nvl(sdy.cyoul,0),4) as hscyl,";
                fpselect += " round(nvl(sdy.yyspl/10000,0),4) as yyspl,";
                fpselect += " round(nvl(sdy.cql/10000,0),4) as hscql,";
                fpselect += " round(nvl(sdy.trqspl/10000,0),4) as trqspl,";
                fpselect += " round(nvl(sdy.czcb/10000,0),4) as czcb,";
                fpselect += " round((case when sdy.yqspl=0 then 0 else nvl(sdy.czcb/sdy.yqspl,0) end),4) as yqdwczcb,";

                fpselect += " nvl(c1.js,0) as js1,";
                fpselect += " nvl(round(c1.cyoul,4),0) as yql1, ";
                fpselect += " nvl(round(c1.czcb,4),0) as czcb1,";

                fpselect += " nvl(c2.js,0) as js2, ";
                fpselect += " nvl(round(c2.cyoul,4),0) as yql2,";
                fpselect += " nvl(round(c2.czcb,4),0) as czcb2,";

                fpselect += " nvl(c3.js,0) as js3,";
                fpselect += " nvl(round(c3.cyoul,4),0) as yql3,";
                fpselect += " nvl(round(c3.czcb,4),0) as czcb3,";

                fpselect += " nvl(c4.js,0) as js4,";
                fpselect += " nvl(round(c4.cyoul,4),0) as yql4,";
                fpselect += " nvl(round(c4.czcb,4),0) as czcb4,";

                fpselect += " nvl(c5.js,0) as js5,";
                fpselect += " nvl(round(c5.cyoul,4),0) as yql5,";
                fpselect += " nvl(round(c5.czcb,4),0) as czcb5, ";

                fpselect += " nvl(c6.js,0) as js6,";
                fpselect += " nvl(round(c6.cyoul,4),0) as yql6,";
                fpselect += " nvl(round(c6.czcb,4),0) as czcb6, ";

                fpselect += " nvl(c7.js,0) as js7,";
                fpselect += " nvl(round(c7.cyoul,4),0) as yql7,";
                fpselect += " nvl(round(c7.czcb,4),0) as czcb7, ";

                fpselect += " nvl(round(yqspl/10000,4),0) As yqspl";


                fpselect += "  from jdstat_pjdysj sdy,qkxyjb xy,";

                fpselect += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.cyoul)/10000,0) as cyoul, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as cyoul, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " ) tem ";
                fpselect += "  where tem.dwcb <= '200'";
                fpselect += "  group by tem.pjdy ) c1,";

                fpselect += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.cyoul)/10000,0) as cyoul, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as cyoul, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " ) tem ";
                fpselect += "  where tem.dwcb <= '400'and dwcb > '200' ";
                fpselect += "  group by tem.pjdy ) c2, ";

                fpselect += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.cyoul)/10000,0) as cyoul, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as cyoul, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) tem ";
                fpselect += "  where tem.dwcb <= '600' and dwcb > '400'";
                fpselect += "  group by tem.pjdy ) c3,";

                fpselect += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.cyoul)/10000,0) as cyoul, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as cyoul, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " ) tem ";
                fpselect += " where tem.dwcb <= '850' and dwcb > '600'";
                fpselect += "  group by tem.pjdy ) c4 ,";

                fpselect += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.cyoul)/10000,0) as cyoul, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as cyoul, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " ) tem ";
                fpselect += "  where tem.dwcb <= '1000' and dwcb> '850'";
                fpselect += " group by tem.pjdy ) c5, ";

                fpselect += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.cyoul)/10000,0) as cyoul, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as cyoul, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) tem ";
                fpselect += " where tem.dwcb <= '2000' and dwcb > '1000'";
                fpselect += " group by tem.pjdy ) c6, ";

                fpselect += " (select tem.pjdy as pjdy, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.cyoul)/10000,0) as cyoul, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.pjdy as pjdy, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as cyoul, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) tem ";
                fpselect += " where tem.dwcb > '2000'";
                fpselect += "  group by tem.pjdy ) c7";

                fpselect += " where sdy.sfpj = '1' and sdy.mc=c1.pjdy(+) and sdy.mc=c2.pjdy(+) ";
                fpselect += " and sdy.mc=c3.pjdy(+) and sdy.mc=c4.pjdy(+) and sdy.mc=c5.pjdy(+) ";
                fpselect += " and sdy.mc=c6.pjdy(+) and sdy.mc=c7.pjdy(+) ";
                fpselect += " and sdy.gsxyjb = xy.xyjb ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " order by sdy.gsxyjb ";

            }
            else if (typeid == "qk")
            {
                fpselect += " select xy.xymc as xymc, sdy.mc as pjdy, ";
                fpselect += " nvl(sdy.yjkjs,0) as js,";
                fpselect += " round(nvl(sdy.cyoul,0),4) as hscyl,";
                fpselect += " round(nvl(sdy.yyspl/10000,0),4) as yyspl,";
                fpselect += " round(nvl(sdy.cql/10000,0),4) as hscql,";
                fpselect += " round(nvl(sdy.trqspl/10000,0),4) as trqspl,";
                fpselect += " round(nvl(sdy.czcb/10000,0),4) as czcb,";
                fpselect += " round((case when sdy.yqspl=0 then 0 else nvl(sdy.czcb/sdy.yqspl,0) end),4) as yqdwczcb,";

                fpselect += " nvl(c1.js,0) as js1,";
                fpselect += " nvl(round(c1.cyoul,4),0) as yql1, ";
                fpselect += " nvl(round(c1.czcb,4),0) as czcb1,";

                fpselect += " nvl(c2.js,0) as js2, ";
                fpselect += " nvl(round(c2.cyoul,4),0) as yql2,";
                fpselect += " nvl(round(c2.czcb,4),0) as czcb2,";

                fpselect += " nvl(c3.js,0) as js3,";
                fpselect += " nvl(round(c3.cyoul,4),0) as yql3,";
                fpselect += " nvl(round(c3.czcb,4),0) as czcb3,";

                fpselect += " nvl(c4.js,0) as js4,";
                fpselect += " nvl(round(c4.cyoul,4),0) as yql4,";
                fpselect += " nvl(round(c4.czcb,4),0) as czcb4,";

                fpselect += " nvl(c5.js,0) as js5,";
                fpselect += " nvl(round(c5.cyoul,4),0) as yql5,";
                fpselect += " nvl(round(c5.czcb,4),0) as czcb5, ";

                fpselect += " nvl(c6.js,0) as js6,";
                fpselect += " nvl(round(c6.cyoul,4),0) as yql6,";
                fpselect += " nvl(round(c6.czcb,4),0) as czcb6, ";

                fpselect += " nvl(c7.js,0) as js7,";
                fpselect += " nvl(round(c7.cyoul,4),0) as yql7,";
                fpselect += " nvl(round(c7.czcb,4),0) as czcb7, ";

                fpselect += " nvl(round(yqspl/10000,4),0) As yqspl";


                fpselect += "  from jdstat_qksj sdy,qkxyjb xy,";

                fpselect += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.cyoul)/10000,0) as cyoul, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as cyoul, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " ) tem ";
                fpselect += "  where tem.dwcb <= '200'";
                fpselect += "  group by tem.qk ) c1,";

                fpselect += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.cyoul)/10000,0) as cyoul, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as cyoul, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " ) tem ";
                fpselect += "  where tem.dwcb <= '400'and dwcb > '200' ";
                fpselect += "  group by tem.qk ) c2, ";

                fpselect += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.cyoul)/10000,0) as cyoul, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as cyoul, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) tem ";
                fpselect += "  where tem.dwcb <= '600' and dwcb > '400'";
                fpselect += "  group by tem.qk ) c3,";

                fpselect += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.cyoul)/10000,0) as cyoul, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as cyoul, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " ) tem ";
                fpselect += " where tem.dwcb <= '850' and dwcb > '600'";
                fpselect += "  group by tem.qk ) c4 ,";

                fpselect += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.cyoul)/10000,0) as cyoul, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as cyoul, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " ) tem ";
                fpselect += "  where tem.dwcb <= '1000' and dwcb> '850'";
                fpselect += " group by tem.qk ) c5, ";

                fpselect += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.cyoul)/10000,0) as cyoul, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as cyoul, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) tem ";
                fpselect += " where tem.dwcb <= '2000' and dwcb > '1000'";
                fpselect += " group by tem.qk ) c6, ";

                fpselect += " (select tem.qk as qk, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.cyoul)/10000,0) as cyoul, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.qk as qk, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as cyoul, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) tem ";
                fpselect += " where tem.dwcb > '2000'";
                fpselect += "  group by tem.qk ) c7";

                fpselect += " where sdy.sfpj = '1' and sdy.mc=c1.qk(+) and sdy.mc=c2.qk(+) ";
                fpselect += " and sdy.mc=c3.qk(+) and sdy.mc=c4.qk(+) and sdy.mc=c5.qk(+) ";
                fpselect += " and sdy.mc=c6.qk(+) and sdy.mc=c7.qk(+) ";
                fpselect += " and sdy.gsxyjb = xy.xyjb ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
            }
            else if (typeid == "zyq")
            {
                fpselect += " select d.dep_name,sdy.zyq as pjdy, ";
                fpselect += " sum(nvl(sdy.djisopen,0)) as js,";
                fpselect += " sum(round(nvl(sdy.hscyl/10000,0),4)) as hscyl,";
                fpselect += " sum(round(nvl(sdy.yyspl/10000,0),4)) as yyspl,";
                fpselect += " sum(round(nvl(sdy.hscql/10000/10000,0),4)) as hscql,";
                fpselect += " sum(round(nvl(sdy.trqspl/10000/10000,0),4)) as trqspl,";
                fpselect += " sum(round(nvl(sdy.czcb/10000,0),4)) as czcb,";
                fpselect += " round((case when sum(sdy.yqspl)=0 then 0 else nvl(sum(sdy.czcb)/sum(sdy.yqspl),0) end),4) as yqdwczcb,";
                fpselect += " sum(nvl(c1.js,0)) as js1,";
                fpselect += " sum(nvl(round(c1.hscyl,4),0)) as yql1, ";
                fpselect += " sum(nvl(round(c1.czcb,4),0)) as czcb1,";

                fpselect += " sum(nvl(c2.js,0)) as js2, ";
                fpselect += " sum(nvl(round(c2.hscyl,4),0)) as yql2,";
                fpselect += " sum(nvl(round(c2.czcb,4),0)) as czcb2,";

                fpselect += " sum(nvl(c3.js,0)) as js3,";
                fpselect += " sum(nvl(round(c3.hscyl,4),0)) as yql3,";
                fpselect += " sum(nvl(round(c3.czcb,4),0)) as czcb3,";

                fpselect += " sum(nvl(c4.js,0)) as js4,";
                fpselect += " sum(nvl(round(c4.hscyl,4),0)) as yql4,";
                fpselect += " sum(nvl(round(c4.czcb,4),0)) as czcb4,";

                fpselect += " sum(nvl(c5.js,0)) as js5,";
                fpselect += " sum(nvl(round(c5.hscyl,4),0)) as yql5,";
                fpselect += " sum(nvl(round(c5.czcb,4),0)) as czcb5, ";

                fpselect += " sum(nvl(c6.js,0)) as js6,";
                fpselect += " sum(nvl(round(c6.hscyl,4),0)) as yql6,";
                fpselect += " sum(nvl(round(c6.czcb,4),0)) as czcb6, ";

                fpselect += " sum(nvl(c7.js,0)) as js7,";
                fpselect += " sum(nvl(round(c7.hscyl,4),0)) as yql7,";
                fpselect += " sum(nvl(round(c7.czcb,4),0)) as czcb7, ";
                fpselect += " nvl(round(sum(yqspl)/10000,4),0) As yqspl,";
                fpselect += " sdy.zyqdm as zyq ";


                fpselect += "  from view_jdstat_zyqsj sdy,department d,";


                //c1
                fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_jdstat_zyqsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " ) tem ";
                fpselect += "  where tem.dwcb <= '200'";
                fpselect += "  group by tem.dj_id ) c1,";


                //c2
                fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_jdstat_zyqsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " ) tem ";
                fpselect += "  where tem.dwcb <= '400'and dwcb > '200' ";
                fpselect += "  group by tem.dj_id ) c2, ";


                //c3
                fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_jdstat_zyqsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) tem ";
                fpselect += "  where tem.dwcb <= '600' and dwcb > '400'";
                fpselect += "  group by tem.dj_id ) c3,";


                //c4
                fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_jdstat_zyqsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " ) tem ";
                fpselect += " where tem.dwcb <= '850' and dwcb > '600'";
                fpselect += "  group by tem.dj_id ) c4 ,";


                //c5
                fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_jdstat_zyqsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " ) tem ";
                fpselect += "  where tem.dwcb <= '1000' and dwcb> '850'";
                fpselect += " group by tem.dj_id ) c5, ";


                //c6
                fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_jdstat_zyqsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) tem ";
                fpselect += " where tem.dwcb <= '2000' and dwcb > '1000'";
                fpselect += " group by tem.dj_id ) c6, ";


                //c7
                fpselect += " (select tem.dj_id as dj_id, nvl(sum(tem.djisopen),0) as js,  nvl(sum(tem.hscyl)/10000,0) as hscyl, nvl(sum(tem.czcb)/10000,0) as czcb";
                fpselect += "  from ";
                fpselect += " (select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.czcb/sdy.yqspl end),0) as dwcb from view_jdstat_zyqsj sdy where  sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  ) tem ";
                fpselect += " where tem.dwcb > '2000'";
                fpselect += "  group by tem.dj_id ) c7";

                fpselect += " where sdy.dep_id = d.dep_id and sdy.djisopen= '1'  and  sdy.dj_id=c1.dj_id(+) and sdy.dj_id=c2.dj_id(+) ";
                fpselect += " and sdy.dj_id=c3.dj_id(+) and sdy.dj_id=c4.dj_id(+) and sdy.dj_id=c5.dj_id(+) ";
                fpselect += " and sdy.dj_id=c6.dj_id(+) and sdy.dj_id=c7.dj_id(+) ";

                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "group by d.dep_name,sdy.zyqdm,sdy.zyq ";
                fpselect += " order by d.dep_name,sdy.zyqdm ";
            }


            try
            {
                connfp.Open();

                OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);

                DataTable dt = new DataTable("fpdata"); //为数据表起一个名字,将来插入数据集中调用
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //计算合计
                    #region
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
                        for (int k = 0; k < dt.Rows.Count; k++)//循环行
                        {
                            for (int n = 2; n < dt.Columns.Count - 1; n++)//循环计算列
                            {
                                dr[n] = float.Parse(dr[n].ToString()) + float.Parse(dt.Rows[k][n].ToString());
                            }
                        }
                        dr[8] = float.Parse(dr[7].ToString()) / float.Parse(dr["yqspl"].ToString());

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
                        //初始化各列
                        for (int i = 2; i < dt.Columns.Count; i++)
                        {
                            dr[i] = 0;
                        }
                        //计算
                        for (int k = 0; k < dt.Rows.Count; k++)//循环行
                        {
                            for (int n = 2; n < dt.Columns.Count; n++)//循环计算列
                            {
                                dr[n] = float.Parse(dr[n].ToString()) + float.Parse(dt.Rows[k][n].ToString());
                            }
                        }
                        dr[8] = float.Parse(dr[7].ToString()) / float.Parse(dr["yqspl"].ToString());

                        //计算精度
                        for (int m = 2; m < dt.Columns.Count; m++)
                        {
                            dr[m] = Math.Round(float.Parse(dr[m].ToString()), 4);
                        }
                        //把行加入表
                        dt.Rows.Add(dr);
                    }

                }
                    #endregion
                //此处用于绑定数据    
                DataSet fpset = new DataSet();
                fpset.Tables.Add(dt);

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
                    string path = "../../../static/excel/zuoyequ1.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表7-单位操作成本区间汇总表");
                    this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    this.FpSpread1.Sheets[0].RowHeader.Visible = false;

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount - 1; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            if (j == 8 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString() != "0")
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");

                            }
                            else if ((j != 2 && j != 9 && j != 12 && j != 15 && j != 18 && j != 21 && j != 24 && j != 27) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString() != "0")
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                            else
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }
                    /////////////////////
                    int k = 1;  //统计重复单元格
                    int w = hcount;  //记录起始位置
                    for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                    {
                        if (FpSpread1.Sheets[0].Cells[i, 0].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, 0].Value.ToString())
                        {
                            k++;
                        }
                        else
                        {
                            FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
                            w = i;
                            k = 1;
                        }
                    }
                    FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);

                }
                else
                {
                    string path = "../../../static/excel/gufenbiao.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表7-单位操作成本区间汇总表");
                    this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    this.FpSpread1.Sheets[0].RowHeader.Visible = false;

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            if (j == 8 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString() != "0")
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");

                            }
                            else if ((j != 2 && j != 9 && j != 12 && j != 15 && j != 18 && j != 21 && j != 24 && j != 27) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString() != "0")
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                            else
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }

                }
                /////////////////////////09.4.29更新//////////////////////////////////////
                //if (rbyj.Checked == true)
                //{
                //    FpSpread1.Sheets[0].Cells[2, 1].Value = "单井";
                //}
                if (typeid == "pjdy")
                {
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "评价单元";
                }
                else if (typeid == "qk")
                {
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "区块";
                }
                else
                {
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "作业区";
                }
                /////////////////////////09.4.30更新//////////////////////////////////////
                //if (rbyj.Checked == true)
                //{
                //    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text + "(油井)";
                //}
                if (typeid == "pjdy")
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text + "(评价单元)";
                }
                else if (typeid == "qk")
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text + "(区块)";
                }
                else
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text + "(作业区)";
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
            string ss = "select bny,eny from jdstat_djsj where bny='" + bny + "' and eny='" + eny + "'";
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
