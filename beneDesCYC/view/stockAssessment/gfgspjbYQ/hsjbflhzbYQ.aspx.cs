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

namespace beneDesCYC.view.stockAssessment.gsgfpjBiao
{
    public partial class hsjbflhzbYQ : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            initSpread();
        }

        protected void initSpread()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/jdniandu.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表27-油井含水级别分类汇总表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

            //this.FpSpread1.Sheets[0].Cells[3, 1].Text = _getParam("targetType");
            //this.FpSpread1.Sheets[0].Cells[4, 1].Text = _getParam("startMonth") + " -- " + _getParam("endMonth");

            if (isempty() == 0)
            {
                Response.Write("<script>alert('无数据')</script>");
                initSpread2();
            }
            else
            { sj(); }
        }

        protected void initSpread2()
        {
            string path = "../../../static/excel/jdniandu.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表27-油井含水级别分类汇总表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
        }

        protected void sj()
        {
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");


            string as_cyc = Session["cyc_id"].ToString();
            string as_cqc = Session["cqc_id"].ToString();

            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);

            string fpselect = " select a.xymc,b.grade_name,xy1.statname,xy1.value, ";
            fpselect += "xy2.value,xy3.value,xy4.value,xy5.value,(xy1.value+xy2.value+xy3.value+xy4.value+xy5.value) as hj  from ";
            fpselect += " gsxylb_info a,stat_grade b, ";
            fpselect += " (select biao.xyjb,biao.item_order,biao.hsjb,biao.statname, biao.statvalue  as value from ";
            fpselect += " (select a.xyjb,a.hsjb,a.rcyjbmc,a.item_order,a.statitem,a.statname,nvl(b.statvalue,0) as statvalue ";
            fpselect += " from rv_stat_yjhsjbhz_frame a, ";
            fpselect += " (Select sdy.gsxyjb,sdy.hsjb,sdy.rcyjb,1 as item_order, ";
            fpselect += " 'js' As statitem, ";
            fpselect += " '井数(口)' As statname, ";
            fpselect += " Sum(djisopen) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And  sdy.pjdyxyjb <> 99 ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "')";


            fpselect += " Group By sdy.gsxyjb,sdy.hsjb,sdy.rcyjb ";
            fpselect += " Union ";
            fpselect += " Select sdy.gsxyjb,sdy.hsjb,sdy.rcyjb,2 as item_order, ";
            fpselect += " 'cyoul' As statitem, ";
            fpselect += " '产油量(万吨)' As statname, ";
            fpselect += " round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And  sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //fpselect += " and cyc_id  = (select dep_id from department where department.dep_name = '" + list + "') ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "')";
            //}

            fpselect += " Group By sdy.gsxyjb,sdy.hsjb,sdy.rcyjb ";
            fpselect += " union ";
            fpselect += " Select sdy.gsxyjb,70000,sdy.rcyjb,1 as item_order, ";
            fpselect += " 'js' As statitem, ";
            fpselect += " '井数(口)' As statname, ";
            fpselect += " Sum(djisopen) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    fpselect += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}

            fpselect += " Group By sdy.gsxyjb,sdy.rcyjb ";
            fpselect += " Union ";
            fpselect += " Select sdy.gsxyjb,70000,sdy.rcyjb,2 as item_order, ";
            fpselect += " 'cyoul' As statitem, ";
            fpselect += " '产油量(万吨)' As statname, ";
            fpselect += " round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And  sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    fpselect += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}

            fpselect += " Group By sdy.gsxyjb,sdy.rcyjb )b ";
            fpselect += " where a.xyjb = b.gsxyjb(+) and a.hsjb = b.hsjb(+) and a.rcyjb = b.rcyjb(+)  ";
            fpselect += " and a.statitem = b.statitem(+) ";
            fpselect += " order by a.xyjb,a.hsjb,a.rcyjb,a.item_order) biao";
            fpselect += " where biao.rcyjbmc = '0≤q<1') xy1, ";

            fpselect += " (select biao.xyjb,biao.item_order,biao.hsjb,biao.statname,biao.statvalue as value from ";
            fpselect += " (select a.xyjb,a.hsjb,a.rcyjbmc,a.item_order,a.statitem,a.statname,nvl(b.statvalue,0) as statvalue  ";
            fpselect += " from rv_stat_yjhsjbhz_frame a, ";
            fpselect += " (Select sdy.gsxyjb,sdy.hsjb,sdy.rcyjb,1 as item_order, ";
            fpselect += " 'js' As statitem, ";
            fpselect += " '井数(口)' As statname, ";
            fpselect += " Sum(djisopen) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    fpselect += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}

            fpselect += " Group By sdy.gsxyjb,sdy.hsjb,sdy.rcyjb ";
            fpselect += " Union ";
            fpselect += " Select sdy.gsxyjb,sdy.hsjb,sdy.rcyjb,2 as item_order, ";
            fpselect += " 'cyoul' As statitem, ";
            fpselect += " '产油量(万吨)' As statname, ";
            fpselect += " round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    fpselect += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}

            fpselect += " Group By sdy.gsxyjb,sdy.hsjb,sdy.rcyjb ";
            fpselect += " union ";
            fpselect += " Select sdy.gsxyjb,70000,sdy.rcyjb,1 as item_order, ";
            fpselect += " 'js' As statitem, ";
            fpselect += " '井数(口)' As statname, ";
            fpselect += " Sum(djisopen) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    fpselect += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}

            fpselect += " Group By sdy.gsxyjb,sdy.rcyjb ";
            fpselect += " Union ";
            fpselect += " Select sdy.gsxyjb,70000,sdy.rcyjb,2 as item_order, ";
            fpselect += " 'cyoul' As statitem, ";
            fpselect += " '产油量(万吨)' As statname, ";
            fpselect += " round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    fpselect += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}

            fpselect += " Group By sdy.gsxyjb,sdy.rcyjb )b ";
            fpselect += " where a.xyjb = b.gsxyjb(+) and a.hsjb = b.hsjb(+) and a.rcyjb = b.rcyjb(+)  ";
            fpselect += " and a.statitem = b.statitem(+) ";
            fpselect += " order by a.xyjb,a.hsjb,a.rcyjb,a.item_order) biao";
            fpselect += " where biao.rcyjbmc = '1≤q<2') xy2, ";

            fpselect += " (select biao.xyjb,biao.item_order,biao.hsjb,biao.statname,biao.statvalue as value from ";
            fpselect += " (select a.xyjb,a.hsjb,a.rcyjbmc,a.item_order,a.statitem,a.statname,nvl(b.statvalue,0) as statvalue  ";
            fpselect += " from rv_stat_yjhsjbhz_frame a, ";
            fpselect += " (Select sdy.gsxyjb,sdy.hsjb,sdy.rcyjb,1 as item_order, ";
            fpselect += " 'js' As statitem, ";
            fpselect += " '井数(口)' As statname, ";
            fpselect += " Sum(djisopen) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    fpselect += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}

            fpselect += " Group By sdy.gsxyjb,sdy.hsjb,sdy.rcyjb ";
            fpselect += " Union ";
            fpselect += " Select sdy.gsxyjb,sdy.hsjb,sdy.rcyjb,2 as item_order, ";
            fpselect += " 'cyoul' As statitem, ";
            fpselect += " '产油量(万吨)' As statname, ";
            fpselect += " round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    fpselect += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}

            fpselect += " Group By sdy.gsxyjb,sdy.hsjb,sdy.rcyjb ";
            fpselect += " union ";
            fpselect += " Select sdy.gsxyjb,70000,sdy.rcyjb,1 as item_order, ";
            fpselect += " 'js' As statitem, ";
            fpselect += " '井数(口)' As statname, ";
            fpselect += " Sum(djisopen) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    fpselect += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}

            fpselect += " Group By sdy.gsxyjb,sdy.rcyjb ";
            fpselect += " Union ";
            fpselect += " Select sdy.gsxyjb,70000,sdy.rcyjb,2 as item_order, ";
            fpselect += " 'cyoul' As statitem, ";
            fpselect += " '产油量(万吨)' As statname, ";
            fpselect += " round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    fpselect += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}

            fpselect += " Group By sdy.gsxyjb,sdy.rcyjb)b  ";
            fpselect += " where a.xyjb = b.gsxyjb(+) and a.hsjb = b.hsjb(+) and a.rcyjb = b.rcyjb(+)  ";
            fpselect += " and a.statitem = b.statitem(+) ";
            fpselect += " order by a.xyjb,a.hsjb,a.rcyjb,a.item_order) biao";
            fpselect += " where biao.rcyjbmc = '2≤q<5') xy3, ";

            fpselect += " (select biao.xyjb,biao.item_order,biao.hsjb,biao.statname,biao.statvalue as value from ";
            fpselect += " (select a.xyjb,a.hsjb,a.rcyjbmc,a.item_order,a.statitem,a.statname,nvl(b.statvalue,0) as statvalue  ";
            fpselect += " from rv_stat_yjhsjbhz_frame a, ";
            fpselect += " (Select sdy.gsxyjb,sdy.hsjb,sdy.rcyjb,1 as item_order, ";
            fpselect += " 'js' As statitem, ";
            fpselect += " '井数(口)' As statname, ";
            fpselect += " Sum(djisopen) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    fpselect += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}

            fpselect += " Group By sdy.gsxyjb,sdy.hsjb,sdy.rcyjb ";
            fpselect += " Union ";
            fpselect += " Select sdy.gsxyjb,sdy.hsjb,sdy.rcyjb,2 as item_order, ";
            fpselect += " 'cyoul' As statitem, ";
            fpselect += " '产油量(万吨)' As statname, ";
            fpselect += " round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    fpselect += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}

            fpselect += " Group By sdy.gsxyjb,sdy.hsjb,sdy.rcyjb ";
            fpselect += " union ";
            fpselect += " Select sdy.gsxyjb,70000,sdy.rcyjb,1 as item_order, ";
            fpselect += " 'js' As statitem, ";
            fpselect += " '井数(口)' As statname, ";
            fpselect += " Sum(djisopen) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    fpselect += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}

            fpselect += " Group By sdy.gsxyjb,sdy.rcyjb ";
            fpselect += " Union ";
            fpselect += " Select sdy.gsxyjb,70000,sdy.rcyjb,2 as item_order, ";
            fpselect += " 'cyoul' As statitem, ";
            fpselect += " '产油量(万吨)' As statname, ";
            fpselect += " round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    fpselect += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}

            fpselect += " Group By sdy.gsxyjb,sdy.rcyjb)b ";
            fpselect += " where a.xyjb = b.gsxyjb(+) and a.hsjb = b.hsjb(+) and a.rcyjb = b.rcyjb(+)  ";
            fpselect += " and a.statitem = b.statitem(+) ";
            fpselect += " order by a.xyjb,a.hsjb,a.rcyjb,a.item_order) biao";
            fpselect += " where biao.rcyjbmc = '5≤q<10') xy4, ";

            fpselect += " (select biao.xyjb,biao.item_order,biao.hsjb,biao.statname,biao.statvalue as value from ";
            fpselect += " (select a.xyjb,a.hsjb,a.rcyjbmc,a.item_order,a.statitem,a.statname,nvl(b.statvalue,0) as statvalue  ";
            fpselect += " from rv_stat_yjhsjbhz_frame a, ";
            fpselect += " (Select sdy.gsxyjb,sdy.hsjb,sdy.rcyjb,1 as item_order, ";
            fpselect += " 'js' As statitem, ";
            fpselect += " '井数(口)' As statname, ";
            fpselect += " Sum(djisopen) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    fpselect += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}

            fpselect += " Group By sdy.gsxyjb,sdy.hsjb,sdy.rcyjb ";
            fpselect += " Union ";
            fpselect += " Select sdy.gsxyjb,sdy.hsjb,sdy.rcyjb,2 as item_order, ";
            fpselect += " 'cyoul' As statitem, ";
            fpselect += " '产油量(万吨)' As statname, ";
            fpselect += " round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    fpselect += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}

            fpselect += " Group By sdy.gsxyjb,sdy.hsjb,sdy.rcyjb ";
            fpselect += " union ";
            fpselect += " Select sdy.gsxyjb,70000,sdy.rcyjb,1 as item_order, ";
            fpselect += " 'js' As statitem, ";
            fpselect += " '井数(口)' As statname, ";
            fpselect += " Sum(djisopen) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    fpselect += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}

            fpselect += " Group By sdy.gsxyjb,sdy.rcyjb ";
            fpselect += " Union ";
            fpselect += " Select sdy.gsxyjb,70000,sdy.rcyjb,2 as item_order, ";
            fpselect += " 'cyoul' As statitem, ";
            fpselect += " '产油量(万吨)' As statname, ";
            fpselect += " round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselect += " From jdstat_djsj sdy ";
            fpselect += " Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    fpselect += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselect += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}

            fpselect += " Group By sdy.gsxyjb,sdy.rcyjb )b ";
            fpselect += " where a.xyjb = b.gsxyjb(+) and a.hsjb = b.hsjb(+) and a.rcyjb = b.rcyjb(+)  ";
            fpselect += " and a.statitem = b.statitem(+) ";
            fpselect += " order by a.xyjb,a.hsjb,a.rcyjb,a.item_order) biao";
            fpselect += " where biao.rcyjbmc = 'q≥10') xy5 ";

            fpselect += " where xy2.xyjb = xy1.xyjb and xy2.hsjb = xy1.hsjb and xy2.item_order = xy1.item_order ";
            fpselect += " and xy3.xyjb = xy1.xyjb and xy3.hsjb = xy1.hsjb and xy3.item_order = xy1.item_order ";
            fpselect += " and xy4.xyjb = xy1.xyjb and xy4.hsjb = xy1.hsjb and xy4.item_order = xy1.item_order ";
            fpselect += " and xy5.xyjb = xy1.xyjb and xy5.hsjb = xy1.hsjb and xy5.item_order = xy1.item_order ";
            fpselect += " and xy1.xyjb = a.xyjb and xy1.hsjb = b.grade ";

            //------------------20090617
            string fpselectunion = "select distinct b.grade_name,xy1.statname,xy1.value as v1,";
            fpselectunion += " xy2.value as v2,xy3.value as v3,xy4.value as v4,xy5.value as v5,(xy1.value+xy2.value+xy3.value+xy4.value+xy5.value) as hj  from";
            fpselectunion += " gsxylb_info a,stat_grade b,";
            fpselectunion += " (select distinct biao.item_order,biao.hsjb,biao.statname, biao.statvalue  as value from ";
            fpselectunion += " (select a.hsjb,a.rcyjbmc,a.item_order,a.statitem,a.statname,nvl(b.statvalue,0) as statvalue ";
            fpselectunion += " from rv_stat_yjhsjbhz_frame a, ";
            fpselectunion += " (Select sdy.hsjb,sdy.rcyjb,1 as item_order, ";
            fpselectunion += " 'js' As statitem, ";
            fpselectunion += " '井数(口)' As statname, ";
            fpselectunion += "  Sum(djisopen) As statvalue ";
            fpselectunion += "  From jdstat_djsj sdy ";
            fpselectunion += " Where djisopen =1 and yqlx <>'Y00202'  And  sdy.pjdyxyjb <> 99  ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += " Group By sdy.hsjb,sdy.rcyjb ";
            fpselectunion += "  Union ";
            fpselectunion += "  Select sdy.hsjb,sdy.rcyjb,2 as item_order, ";
            fpselectunion += " 'cyoul' As statitem, ";
            fpselectunion += " '产油量(万吨)' As statname, ";
            fpselectunion += "  round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselectunion += "  From jdstat_djsj sdy ";
            fpselectunion += "  Where djisopen =1 and yqlx <>'Y00202'  And  sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += "  Group By sdy.hsjb,sdy.rcyjb ";
            fpselectunion += " union ";
            fpselectunion += " Select 70000,sdy.rcyjb,1 as item_order, ";
            fpselectunion += " 'js' As statitem, ";
            fpselectunion += "  '井数(口)' As statname, ";
            fpselectunion += "  Sum(djisopen) As statvalue ";
            fpselectunion += "  From jdstat_djsj sdy ";
            fpselectunion += "  Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += "  Group By sdy.rcyjb ";
            fpselectunion += "  Union ";
            fpselectunion += "  Select 70000,sdy.rcyjb,2 as item_order, ";
            fpselectunion += "  'cyoul' As statitem, ";
            fpselectunion += "  '产油量(万吨)' As statname, ";
            fpselectunion += "  round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselectunion += "  From jdstat_djsj sdy ";
            fpselectunion += "  Where djisopen =1 and yqlx <>'Y00202'  And  sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += "  Group by sdy.rcyjb )b ";
            fpselectunion += "  where  a.hsjb = b.hsjb(+) and a.rcyjb = b.rcyjb(+)  ";
            fpselectunion += "  and a.statitem = b.statitem(+) ";
            fpselectunion += " order by a.hsjb,a.rcyjb,a.item_order) biao";
            fpselectunion += " where biao.rcyjbmc = '0≤q<1') xy1, ";
            fpselectunion += " (select distinct biao.item_order,biao.hsjb,biao.statname,biao.statvalue as value from ";
            fpselectunion += " (select a.hsjb,a.rcyjbmc,a.item_order,a.statitem,a.statname,nvl(b.statvalue,0) as statvalue  ";
            fpselectunion += " from rv_stat_yjhsjbhz_frame a, ";
            fpselectunion += " (Select sdy.hsjb,sdy.rcyjb,1 as item_order, ";
            fpselectunion += "  'js' As statitem, ";
            fpselectunion += "  '井数(口)' As statname, ";
            fpselectunion += "  Sum(djisopen) As statvalue ";
            fpselectunion += " From jdstat_djsj sdy ";
            fpselectunion += "  Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += "  Group By sdy.hsjb,sdy.rcyjb ";
            fpselectunion += "  Union ";
            fpselectunion += "  Select sdy.hsjb,sdy.rcyjb,2 as item_order, ";
            fpselectunion += "  'cyoul' As statitem, ";
            fpselectunion += "  '产油量(万吨)' As statname, ";
            fpselectunion += "  round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselectunion += "  From jdstat_djsj sdy ";
            fpselectunion += "  Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += "  Group By sdy.hsjb,sdy.rcyjb ";
            fpselectunion += " union ";
            fpselectunion += " Select 70000,sdy.rcyjb,1 as item_order, ";
            fpselectunion += "  'js' As statitem, ";
            fpselectunion += "  '井数(口)' As statname, ";
            fpselectunion += "  Sum(djisopen) As statvalue ";
            fpselectunion += "  From jdstat_djsj sdy ";
            fpselectunion += "  Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += "  Group By sdy.rcyjb ";
            fpselectunion += "  Union ";
            fpselectunion += "  Select 70000,sdy.rcyjb,2 as item_order, ";
            fpselectunion += "  'cyoul' As statitem, ";
            fpselectunion += "  '产油量(万吨)' As statname, ";
            fpselectunion += "  round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselectunion += "  From jdstat_djsj sdy ";
            fpselectunion += "  Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += "  Group By sdy.rcyjb )b ";
            fpselectunion += " where  a.hsjb = b.hsjb(+) and a.rcyjb = b.rcyjb(+)  ";
            fpselectunion += " and a.statitem = b.statitem(+) ";
            fpselectunion += " order by a.hsjb,a.rcyjb,a.item_order) biao";
            fpselectunion += " where biao.rcyjbmc = '1≤q<2') xy2, ";
            fpselectunion += " (select distinct biao.item_order,biao.hsjb,biao.statname,biao.statvalue as value from ";
            fpselectunion += " (select a.hsjb,a.rcyjbmc,a.item_order,a.statitem,a.statname,nvl(b.statvalue,0) as statvalue  ";
            fpselectunion += " from rv_stat_yjhsjbhz_frame a, ";
            fpselectunion += " (Select sdy.hsjb,sdy.rcyjb,1 as item_order, ";
            fpselectunion += "  'js' As statitem, ";
            fpselectunion += "  '井数(口)' As statname, ";
            fpselectunion += " Sum(djisopen) As statvalue ";
            fpselectunion += " From jdstat_djsj sdy ";
            fpselectunion += " Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += " Group By sdy.hsjb,sdy.rcyjb ";
            fpselectunion += " Union ";
            fpselectunion += " Select sdy.hsjb,sdy.rcyjb,2 as item_order, ";
            fpselectunion += "  'cyoul' As statitem, ";
            fpselectunion += "  '产油量(万吨)' As statname, ";
            fpselectunion += "  round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselectunion += " From jdstat_djsj sdy ";
            fpselectunion += "  Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += "  Group By sdy.hsjb,sdy.rcyjb ";
            fpselectunion += "  union ";
            fpselectunion += "  Select 70000,sdy.rcyjb,1 as item_order, ";
            fpselectunion += "  'js' As statitem, ";
            fpselectunion += "  '井数(口)' As statname, ";
            fpselectunion += "  Sum(djisopen) As statvalue ";
            fpselectunion += "  From jdstat_djsj sdy ";
            fpselectunion += "  Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += "  Group By sdy.rcyjb ";
            fpselectunion += " Union ";
            fpselectunion += " Select 70000,sdy.rcyjb,2 as item_order, ";
            fpselectunion += " 'cyoul' As statitem, ";
            fpselectunion += " '产油量(万吨)' As statname, ";
            fpselectunion += "  round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselectunion += "  From jdstat_djsj sdy ";
            fpselectunion += "  Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += " Group By sdy.rcyjb)b  ";
            fpselectunion += "  where a.hsjb = b.hsjb(+) and a.rcyjb = b.rcyjb(+)  ";
            fpselectunion += "  and a.statitem = b.statitem(+) ";
            fpselectunion += "  order by a.hsjb,a.rcyjb,a.item_order) biao";
            fpselectunion += "  where biao.rcyjbmc = '2≤q<5') xy3, ";
            fpselectunion += " (select distinct biao.item_order,biao.hsjb,biao.statname,biao.statvalue as value from ";
            fpselectunion += " (select a.hsjb,a.rcyjbmc,a.item_order,a.statitem,a.statname,nvl(b.statvalue,0) as statvalue  ";
            fpselectunion += "  from rv_stat_yjhsjbhz_frame a, ";
            fpselectunion += "  (Select sdy.hsjb,sdy.rcyjb,1 as item_order, ";
            fpselectunion += "  'js' As statitem, ";
            fpselectunion += "  '井数(口)' As statname, ";
            fpselectunion += "  Sum(djisopen) As statvalue ";
            fpselectunion += "  From jdstat_djsj sdy ";
            fpselectunion += "  Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += " Group By sdy.hsjb,sdy.rcyjb ";
            fpselectunion += " Union ";
            fpselectunion += " Select sdy.hsjb,sdy.rcyjb,2 as item_order, ";
            fpselectunion += " 'cyoul' As statitem, ";
            fpselectunion += "  '产油量(万吨)' As statname, ";
            fpselectunion += "  round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselectunion += "  From jdstat_djsj sdy ";
            fpselectunion += "  Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += "  Group By sdy.hsjb,sdy.rcyjb ";
            fpselectunion += "  union ";
            fpselectunion += "  Select 70000,sdy.rcyjb,1 as item_order, ";
            fpselectunion += "  'js' As statitem, ";
            fpselectunion += "  '井数(口)' As statname, ";
            fpselectunion += "  Sum(djisopen) As statvalue ";
            fpselectunion += "  From jdstat_djsj sdy ";
            fpselectunion += "  Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += " Group By sdy.rcyjb ";
            fpselectunion += "  Union ";
            fpselectunion += "  Select 70000,sdy.rcyjb,2 as item_order, ";
            fpselectunion += "  'cyoul' As statitem, ";
            fpselectunion += "  '产油量(万吨)' As statname, ";
            fpselectunion += "  round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselectunion += "  From jdstat_djsj sdy ";
            fpselectunion += "  Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += "  Group By sdy.rcyjb)b ";
            fpselectunion += "  where a.hsjb = b.hsjb(+) and a.rcyjb = b.rcyjb(+)  ";
            fpselectunion += "  and a.statitem = b.statitem(+) ";
            fpselectunion += "  order by a.hsjb,a.rcyjb,a.item_order) biao";
            fpselectunion += " where biao.rcyjbmc = '5≤q<10') xy4, ";
            fpselectunion += " (select distinct biao.item_order,biao.hsjb,biao.statname,biao.statvalue as value from ";
            fpselectunion += " (select a.hsjb,a.rcyjbmc,a.item_order,a.statitem,a.statname,nvl(b.statvalue,0) as statvalue  ";
            fpselectunion += " from rv_stat_yjhsjbhz_frame a, ";
            fpselectunion += " (Select sdy.hsjb,sdy.rcyjb,1 as item_order, ";
            fpselectunion += " 'js' As statitem, ";
            fpselectunion += "  '井数(口)' As statname, ";
            fpselectunion += "  Sum(djisopen) As statvalue ";
            fpselectunion += "  From jdstat_djsj sdy ";
            fpselectunion += "  Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += "  Group By sdy.hsjb,sdy.rcyjb ";
            fpselectunion += "  Union ";
            fpselectunion += " Select sdy.hsjb,sdy.rcyjb,2 as item_order, ";
            fpselectunion += "  'cyoul' As statitem, ";
            fpselectunion += "  '产油量(万吨)' As statname, ";
            fpselectunion += " round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselectunion += "  From jdstat_djsj sdy ";
            fpselectunion += "  Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += "  Group By sdy.hsjb,sdy.rcyjb ";
            fpselectunion += "  union ";
            fpselectunion += "  Select 70000,sdy.rcyjb,1 as item_order, ";
            fpselectunion += "  'js' As statitem, ";
            fpselectunion += "  '井数(口)' As statname,";
            fpselectunion += "  Sum(djisopen) As statvalue ";
            fpselectunion += "  From jdstat_djsj sdy ";
            fpselectunion += "  Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += " Group By sdy.rcyjb";
            fpselectunion += "  Union ";
            fpselectunion += "  Select 70000,sdy.rcyjb,2 as item_order, ";
            fpselectunion += "  'cyoul' As statitem, ";
            fpselectunion += " '产油量(万吨)' As statname, ";
            fpselectunion += "  round(Sum(hscyl) / 10000,4) As statvalue ";
            fpselectunion += "  From jdstat_djsj sdy ";
            fpselectunion += "  Where djisopen =1 and yqlx <>'Y00202'  And sdy.pjdyxyjb <> 99 ";
            //if (list == "quan")
            //{
            //    fpselectunion += "";
            //}
            //else
            //{
            //    fpselectunion += " and cyc_id  = (select cyc_id from department where department.dep_name = '" + list + "') ";
            fpselectunion += " and regexp_like(cyc_id,'" + as_cqc + "') and substr(sdy.yclx,-2,2)='油藏' ";
            //}
            fpselectunion += "  Group By sdy.rcyjb )b ";
            fpselectunion += "  where a.hsjb = b.hsjb(+) and a.rcyjb = b.rcyjb(+)  ";
            fpselectunion += "  and a.statitem = b.statitem(+) ";
            fpselectunion += "  order by a.hsjb,a.rcyjb,a.item_order) biao";
            fpselectunion += "  where biao.rcyjbmc = 'q≥10') xy5 ";
            fpselectunion += "  where  xy2.hsjb = xy1.hsjb and xy2.item_order = xy1.item_order ";
            fpselectunion += "  and  xy3.hsjb = xy1.hsjb and xy3.item_order = xy1.item_order ";
            fpselectunion += "  and  xy4.hsjb = xy1.hsjb and xy4.item_order = xy1.item_order ";
            fpselectunion += "  and  xy5.hsjb = xy1.hsjb and xy5.item_order = xy1.item_order ";
            fpselectunion += " and  xy1.hsjb = b.grade";
            fpselectunion += " order by grade_name,statname";

            try
            {
                connfp.Open();

                OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                DataSet fpset = new DataSet();
                da.Fill(fpset, "fpdata");

                //添加合计行20090710
                OracleDataAdapter daunion = new OracleDataAdapter(fpselectunion, connfp);
                DataSet fpsetunion = new DataSet();
                daunion.Fill(fpsetunion, "fpdataunion");

                DataRow[] hjk = fpset.Tables["fpdata"].Select("xymc='合计'");
                int hjrow = hjk.Length;
                for (int n = fpset.Tables["fpdata"].Rows.Count - hjrow; n < fpset.Tables["fpdata"].Rows.Count; n++)
                {
                    string grdname = fpset.Tables["fpdata"].Rows[n][1].ToString();
                    string stname = fpset.Tables["fpdata"].Rows[n][2].ToString();
                    DataRow[] hjrcy = fpsetunion.Tables["fpdataunion"].Select("grade_name='" + grdname + "' and statname='" + stname + "'");
                    if (hjrcy.Length == 1)
                    {
                        for (int hjcol = 3; hjcol <= 8; hjcol++)
                        {

                            fpset.Tables["fpdata"].Rows[n][hjcol] = hjrcy[0][hjcol - 1];
                        }
                    }
                }

                for (int i = 1; i < fpset.Tables["fpdata"].Rows.Count; i += 2)
                {
                    for (int j = 3; j <= 8; j++)
                    {
                        if (fpset.Tables["fpdata"].Rows[i - 1][8].ToString() == "0")
                        {
                            fpset.Tables["fpdata"].Rows[i][j] = 0;
                        }
                        else
                        {
                            fpset.Tables["fpdata"].Rows[i][j] = Math.Round(double.Parse(fpset.Tables["fpdata"].Rows[i - 1][j].ToString()) / double.Parse(fpset.Tables["fpdata"].Rows[i - 1][8].ToString()) * 100, 2);
                        }
                    }
                }
                //此处用于绑定数据
                #region
                int rcount = fpset.Tables["fpdata"].Rows.Count;
                int ccount = fpset.Tables["fpdata"].Columns.Count;
                int hcount = 4;
                //FpSpread1.ColumnHeader.Visible = false;
                //FpSpread1.RowHeader.Visible = false;
                if (FpSpread1.Sheets[0].Rows.Count == hcount)
                {
                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }
                    //合并单元格
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
                    //合并第二列单元格
                    k = 1;  //统计重复单元格
                    w = hcount;  //记录起始位置
                    for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                    {
                        if (FpSpread1.Sheets[0].Cells[i, 1].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, 1].Value.ToString())
                        {
                            k++;
                        }
                        else
                        {
                            FpSpread1.ActiveSheetView.AddSpanCell(w, 1, k, 1);
                            w = i;
                            k = 1;
                        }
                    }

                    FpSpread1.ActiveSheetView.AddSpanCell(w, 1, k, 1);
                }
                else//不为空
                {
                    string path = "../../../static/excel/jdniandu.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表27-油井含水级别分类汇总表");

                    this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    this.FpSpread1.Sheets[0].RowHeader.Visible = false;

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }

                    }
                    //合并单元格
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
                    //合并第二列单元格
                    k = 1;  //统计重复单元格
                    w = hcount;  //记录起始位置
                    for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                    {
                        if (FpSpread1.Sheets[0].Cells[i, 1].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, 1].Value.ToString())
                        {
                            k++;
                        }
                        else
                        {
                            FpSpread1.ActiveSheetView.AddSpanCell(w, 1, k, 1);
                            w = i;
                            k = 1;
                        }
                    }

                    FpSpread1.ActiveSheetView.AddSpanCell(w, 1, k, 1);
                }
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

            FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao27.xls");


        }
    }
}
