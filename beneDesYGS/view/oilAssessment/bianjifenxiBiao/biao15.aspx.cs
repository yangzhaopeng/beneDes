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

namespace beneDesYGS.view.oilAssessment.bianjifenxiBiao
{
    public partial class biao15 : beneDesYGS.core.UI.corePage
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
            string path = "../../../static/excel/gufenbiao.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表17-边际、无效井产生原因初步分析表");

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
            this.FpSpread1.Sheets[0].OpenExcel(path, "表17-边际、无效井产生原因初步分析表");

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



            string fpselect = "";
            fpselect += "select temp.xymc,temp.fxyymc,temp.jh,temp.qk,temp.ssyt,temp.pjdy,temp.tcrq,temp.scsj,temp.rcy,temp.hscyl,temp.jkcyl,temp.hs,temp.yqspl,temp.dyqczcb,temp.fxyyid ";
            fpselect += " from ";
            #region
            fpselect += "(";
            fpselect += " select temp1.dj_id,temp1.xymc, temp1.fxyymc,temp1.jh,temp1.qk,temp1.ssyt, ";
            fpselect += "temp1.pjdy,d.tcrq,temp1.scsj,temp1.rcy,temp1.hscyl,temp1.jkcyl,temp1.hs,temp1.yqspl,temp1.dyqczcb,temp1.fxyyid";
            fpselect += " from ";

            fpselect += "( ";
            fpselect += " select dj_id,xy.xymc,  ";
            fpselect += "  (case when sdy.hs>ghs.num then '高含水' ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then '低产能' ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then '措施' ";
            fpselect += " else '其它'";
            fpselect += " end ) as fxyymc,sdy.jh, ";
            //fpselect += " fx.fxyymc, ";
            fpselect += " sdy.qk, ";
            fpselect += " sdy.ssyt,  ";
            fpselect += " sdy.pjdy, ";
            //fpselect += " djsj.tcrq, ";
            fpselect += " sdy.scsj,  ";
            fpselect += " nvl(round(sdy.rcy,2),0) as rcy, ";
            fpselect += " nvl(round(sdy.hscyl,2),0) as hscyl , ";
            fpselect += " nvl(round(sdy.jkcyl,2),0) as jkcyl, ";
            fpselect += " nvl(round(sdy.hs ,2),0) as hs, ";
            fpselect += " nvl(round(sdy.yqspl ,2),0) as yqspl, ";
            fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.czcb,0)/sdy.yqspl,2) end) as dyqczcb, ";
            fpselect += " (case when sdy.hs>ghs.num then  1 ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then  2  ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then 3";
            fpselect += "  else  4";
            fpselect += "  end ) as fxyyid ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.yxczcb,0)/sdy.yqspl,2) end) as dyqyxczcb ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.zjyxczcb,0)/sdy.yqspl,2) end) as dyqzjyxczcb ";
            fpselect += " from jdstat_djsj sdy,  gsxylb_info xy, ";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='高含水') ghs,";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='低产能') dhs ";
            fpselect += " where  sdy.gsxyjb = xy.xyjb and	 ";
            fpselect += " sdy.gsxyjb = '4' ";
            if (list == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            }
            fpselect += ")temp1,";
            fpselect += " (select * from djsj where ny='" + eny.Trim() + "')d ";
            fpselect += " where temp1.dj_id= d.dj_id(+)";


            fpselect += " ) temp";
            #endregion

            fpselect += " union";
            fpselect += " (select'边际效益' as xymc,'小计' as fxyymc,' ' as jh,' ' as qk, ' ' as ssyt,' ' as pjdy,' ' as tcrq,sum(temp.scsj) as scsj,";
            fpselect += " sum(temp.rcy) as rcy, sum(temp.hscyl) as hscyl, sum(temp.jkcyl)as jkcyl, (case when Sum(temp.jkcyl)=0 then 0 else round((Sum(temp.jkcyl)-Sum(temp.jkcyl*(1-temp.hs/100)))/Sum(temp.jkcyl),4)*100 end) As hs, round(sum(temp.yqspl),2) as yqspl,";
            fpselect += " (case when sum(nvl(temp.yqspl,0)) = 0  then 0 else round(sum(nvl(temp.czcb,0))/sum(temp.yqspl),2) end) as dyqczcb,fxyyid";
            fpselect += " from";
            #region
            fpselect += "(";
            fpselect += " select temp1.dj_id,temp1.xymc, temp1.fxyymc,temp1.jh,temp1.qk,temp1.ssyt,temp1.czcb, ";
            fpselect += "temp1.pjdy,d.tcrq,temp1.scsj,temp1.rcy,temp1.hscyl,temp1.jkcyl,temp1.hs,temp1.yqspl,temp1.dyqczcb,temp1.fxyyid";
            fpselect += " from ";

            fpselect += "( ";
            fpselect += " select dj_id,xy.xymc,  ";
            fpselect += "  (case when sdy.hs>ghs.num then '高含水' ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then '低产能' ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then '措施' ";
            fpselect += " else '其它'";
            fpselect += " end ) as fxyymc,sdy.jh, ";
            //fpselect += " fx.fxyymc, ";
            fpselect += " sdy.qk, ";
            fpselect += " sdy.ssyt,  ";
            fpselect += " sdy.pjdy, ";
            //fpselect += " djsj.tcrq, ";
            fpselect += " sdy.scsj,  ";
            fpselect += " nvl(round(sdy.rcy,2),0) as rcy, ";
            fpselect += " nvl(round(sdy.hscyl,2),0) as hscyl , ";
            fpselect += " nvl(round(sdy.jkcyl,2),0) as jkcyl, ";
            fpselect += " nvl(round(sdy.hs ,2),0) as hs, ";
            fpselect += "  yqspl,czcb, ";
            fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.czcb,0)/sdy.yqspl,2) end) as dyqczcb, ";
            fpselect += " (case when sdy.hs>ghs.num then  1 ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then  2  ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then 3";
            fpselect += "  else  4";
            fpselect += "  end ) as fxyyid ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.yxczcb,0)/sdy.yqspl,2) end) as dyqyxczcb ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.zjyxczcb,0)/sdy.yqspl,2) end) as dyqzjyxczcb ";
            fpselect += " from jdstat_djsj sdy,  gsxylb_info xy, ";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='高含水') ghs,";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='低产能') dhs ";
            fpselect += " where  sdy.gsxyjb = xy.xyjb and	 ";
            fpselect += " sdy.gsxyjb = '4' ";
            if (list == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            }
            fpselect += ")temp1,";
            fpselect += " (select * from djsj where ny='" + eny.Trim() + "')d ";
            fpselect += " where temp1.dj_id= d.dj_id(+)";


            fpselect += " ) temp";
            #endregion
            fpselect += " where fxyyid=1";
            fpselect += " group by fxyyid)";
            fpselect += " union";
            fpselect += " (select'边际效益' as xymc,'小计' as fxyymc,' ' as jh,' ' as qk, ' ' as ssyt,' ' as pjdy,' ' as tcrq,sum(temp.scsj) as scsj,";
            fpselect += " sum(temp.rcy) as rcy, sum(temp.hscyl) as hscyl, sum(temp.jkcyl)as jkcyl, (case when Sum(temp.jkcyl)=0 then 0 else round((Sum(temp.jkcyl)-Sum(temp.jkcyl*(1-temp.hs/100)))/Sum(temp.jkcyl),4)*100 end) As hs, round(sum(temp.yqspl),2) as yqspl,";
            fpselect += " (case when sum(nvl(temp.yqspl,0)) = 0  then 0 else round(sum(nvl(temp.czcb,0))/sum(temp.yqspl),2) end) as dyqczcb,fxyyid";
            fpselect += " from";
            #region
            fpselect += "(";
            fpselect += " select temp1.dj_id,temp1.xymc, temp1.fxyymc,temp1.jh,temp1.qk,temp1.ssyt,temp1.czcb, ";
            fpselect += "temp1.pjdy,d.tcrq,temp1.scsj,temp1.rcy,temp1.hscyl,temp1.jkcyl,temp1.hs,temp1.yqspl,temp1.dyqczcb,temp1.fxyyid";
            fpselect += " from ";

            fpselect += "( ";
            fpselect += " select dj_id,xy.xymc,  ";
            fpselect += "  (case when sdy.hs>ghs.num then '高含水' ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then '低产能' ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then '措施' ";
            fpselect += " else '其它'";
            fpselect += " end ) as fxyymc,sdy.jh, ";
            //fpselect += " fx.fxyymc, ";
            fpselect += " sdy.qk, ";
            fpselect += " sdy.ssyt,  ";
            fpselect += " sdy.pjdy, ";
            //fpselect += " djsj.tcrq, ";
            fpselect += " sdy.scsj,  ";
            fpselect += " nvl(round(sdy.rcy,2),0) as rcy, ";
            fpselect += " nvl(round(sdy.hscyl,2),0) as hscyl , ";
            fpselect += " nvl(round(sdy.jkcyl,2),0) as jkcyl, ";
            fpselect += " nvl(round(sdy.hs ,2),0) as hs, ";
            fpselect += "  yqspl, czcb,";
            fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.czcb,0)/sdy.yqspl,2) end) as dyqczcb, ";
            fpselect += " (case when sdy.hs>ghs.num then  1 ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then  2  ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then 3";
            fpselect += "  else  4";
            fpselect += "  end ) as fxyyid ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.yxczcb,0)/sdy.yqspl,2) end) as dyqyxczcb ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.zjyxczcb,0)/sdy.yqspl,2) end) as dyqzjyxczcb ";
            fpselect += " from jdstat_djsj sdy,  gsxylb_info xy, ";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='高含水') ghs,";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='低产能') dhs ";
            fpselect += " where  sdy.gsxyjb = xy.xyjb and	 ";
            fpselect += " sdy.gsxyjb = '4' ";
            if (list == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            }
            fpselect += ")temp1,";
            fpselect += " (select * from djsj where ny='" + eny.Trim() + "')d ";
            fpselect += " where temp1.dj_id= d.dj_id(+)";


            fpselect += " ) temp";
            #endregion
            fpselect += " where fxyyid=2";
            fpselect += " group by fxyyid)";
            fpselect += " union";

            fpselect += " (select'边际效益' as xymc,'小计' as fxyymc,' ' as jh,' ' as qk, ' ' as ssyt,' ' as pjdy,' ' as tcrq,sum(temp.scsj) as scsj,";
            fpselect += " sum(temp.rcy) as rcy, sum(temp.hscyl) as hscyl, sum(temp.jkcyl)as jkcyl, (case when Sum(temp.jkcyl)=0 then 0 else round((Sum(temp.jkcyl)-Sum(temp.jkcyl*(1-temp.hs/100)))/Sum(temp.jkcyl),4)*100 end) As hs, round(sum(temp.yqspl),2) as yqspl,";
            fpselect += " (case when sum(nvl(temp.yqspl,0)) = 0  then 0 else round(sum(nvl(temp.czcb,0))/sum(temp.yqspl),2) end) as dyqczcb,fxyyid";
            fpselect += " from";
            #region
            fpselect += "(";
            fpselect += " select temp1.dj_id,temp1.xymc, temp1.fxyymc,temp1.jh,temp1.qk,temp1.ssyt,temp1.czcb, ";
            fpselect += "temp1.pjdy,d.tcrq,temp1.scsj,temp1.rcy,temp1.hscyl,temp1.jkcyl,temp1.hs,temp1.yqspl,temp1.dyqczcb,temp1.fxyyid";
            fpselect += " from ";

            fpselect += "( ";
            fpselect += " select dj_id,xy.xymc,  ";
            fpselect += "  (case when sdy.hs>ghs.num then '高含水' ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then '低产能' ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then '措施' ";
            fpselect += " else '其它'";
            fpselect += " end ) as fxyymc,sdy.jh, ";
            //fpselect += " fx.fxyymc, ";
            fpselect += " sdy.qk, ";
            fpselect += " sdy.ssyt,  ";
            fpselect += " sdy.pjdy, ";
            //fpselect += " djsj.tcrq, ";
            fpselect += " sdy.scsj,  ";
            fpselect += " nvl(round(sdy.rcy,2),0) as rcy, ";
            fpselect += " nvl(round(sdy.hscyl,2),0) as hscyl , ";
            fpselect += " nvl(round(sdy.jkcyl,2),0) as jkcyl, ";
            fpselect += " nvl(round(sdy.hs ,2),0) as hs, ";
            fpselect += "  yqspl, czcb,";
            fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.czcb,0)/sdy.yqspl,2) end) as dyqczcb, ";
            fpselect += " (case when sdy.hs>ghs.num then  1 ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then  2  ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then 3";
            fpselect += "  else  4";
            fpselect += "  end ) as fxyyid ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.yxczcb,0)/sdy.yqspl,2) end) as dyqyxczcb ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.zjyxczcb,0)/sdy.yqspl,2) end) as dyqzjyxczcb ";
            fpselect += " from jdstat_djsj sdy,  gsxylb_info xy, ";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='高含水') ghs,";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='低产能') dhs ";
            fpselect += " where  sdy.gsxyjb = xy.xyjb and	 ";
            fpselect += " sdy.gsxyjb = '4' ";
            if (list == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            }
            fpselect += ")temp1,";
            fpselect += " (select * from djsj where ny='" + eny.Trim() + "')d ";
            fpselect += " where temp1.dj_id= d.dj_id(+)";


            fpselect += " ) temp";
            #endregion
            fpselect += " where fxyyid=3";
            fpselect += " group by fxyyid)";
            fpselect += " union";
            fpselect += " (select'边际效益' as xymc,'小计' as fxyymc,' ' as jh,' ' as qk, ' ' as ssyt,' ' as pjdy,' ' as tcrq,sum(temp.scsj) as scsj,";
            fpselect += " sum(temp.rcy) as rcy, sum(temp.hscyl) as hscyl, sum(temp.jkcyl)as jkcyl, (case when Sum(temp.jkcyl)=0 then 0 else round((Sum(temp.jkcyl)-Sum(temp.jkcyl*(1-temp.hs/100)))/Sum(temp.jkcyl),4)*100 end) As hs, round(sum(temp.yqspl),2) as yqspl,";
            fpselect += " (case when sum(nvl(temp.yqspl,0)) = 0  then 0 else round(sum(nvl(temp.czcb,0))/sum(temp.yqspl),2) end) as dyqczcb,fxyyid";
            fpselect += " from";
            #region
            fpselect += "(";
            fpselect += " select temp1.dj_id,temp1.xymc, temp1.fxyymc,temp1.jh,temp1.qk,temp1.ssyt,temp1.czcb, ";
            fpselect += "temp1.pjdy,d.tcrq,temp1.scsj,temp1.rcy,temp1.hscyl,temp1.jkcyl,temp1.hs,temp1.yqspl,temp1.dyqczcb,temp1.fxyyid";
            fpselect += " from ";

            fpselect += "( ";
            fpselect += " select dj_id,xy.xymc,  ";
            fpselect += "  (case when sdy.hs>ghs.num then '高含水' ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then '低产能' ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then '措施' ";
            fpselect += " else '其它'";
            fpselect += " end ) as fxyymc,sdy.jh, ";
            //fpselect += " fx.fxyymc, ";
            fpselect += " sdy.qk, ";
            fpselect += " sdy.ssyt,  ";
            fpselect += " sdy.pjdy, ";
            //fpselect += " djsj.tcrq, ";
            fpselect += " sdy.scsj,  ";
            fpselect += " nvl(round(sdy.rcy,2),0) as rcy, ";
            fpselect += " nvl(round(sdy.hscyl,2),0) as hscyl , ";
            fpselect += " nvl(round(sdy.jkcyl,2),0) as jkcyl, ";
            fpselect += " nvl(round(sdy.hs ,2),0) as hs, ";
            fpselect += "  yqspl,czcb, ";
            fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.czcb,0)/sdy.yqspl,2) end) as dyqczcb, ";
            fpselect += " (case when sdy.hs>ghs.num then  1 ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then  2  ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then 3";
            fpselect += "  else  4";
            fpselect += "  end ) as fxyyid ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.yxczcb,0)/sdy.yqspl,2) end) as dyqyxczcb ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.zjyxczcb,0)/sdy.yqspl,2) end) as dyqzjyxczcb ";
            fpselect += " from jdstat_djsj sdy,  gsxylb_info xy, ";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='高含水') ghs,";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='低产能') dhs ";
            fpselect += " where  sdy.gsxyjb = xy.xyjb and	 ";
            fpselect += " sdy.gsxyjb = '4' ";
            if (list == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            }
            fpselect += ")temp1,";
            fpselect += " (select * from djsj where ny='" + eny.Trim() + "')d ";
            fpselect += " where temp1.dj_id= d.dj_id(+)";


            fpselect += " ) temp";
            #endregion
            fpselect += " where fxyyid=4";
            fpselect += " group by fxyyid)";
            fpselect += " union";
            fpselect += " (select'合计' as xymc,' ' as fxyymc,' ' as jh,' ' as qk, ' ' as ssyt,' ' as pjdy,' ' as tcrq,sum(temp.scsj) as scsj,";
            fpselect += " sum(temp.rcy) as rcy, sum(temp.hscyl) as hscyl, sum(temp.jkcyl)as jkcyl, (case when Sum(temp.jkcyl)=0 then 0 else round((Sum(temp.jkcyl)-Sum(temp.jkcyl*(1-temp.hs/100)))/Sum(temp.jkcyl),4)*100 end) As hs, round(sum(temp.yqspl),2) as yqspl,";
            fpselect += " (case when sum(nvl(temp.yqspl,0)) = 0  then 0 else round(sum(nvl(temp.czcb,0))/sum(temp.yqspl),2) end) as dyqczcb, 5 as fxyyid";
            fpselect += " from";
            #region
            fpselect += "(";
            fpselect += " select temp1.dj_id,temp1.xymc, temp1.fxyymc,temp1.jh,temp1.qk,temp1.ssyt, temp1.czcb,";
            fpselect += "temp1.pjdy,d.tcrq,temp1.scsj,temp1.rcy,temp1.hscyl,temp1.jkcyl,temp1.hs,temp1.yqspl,temp1.dyqczcb,temp1.fxyyid";
            fpselect += " from ";

            fpselect += "( ";
            fpselect += " select dj_id,xy.xymc,  ";
            fpselect += "  (case when sdy.hs>ghs.num then '高含水' ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then '低产能' ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then '措施' ";
            fpselect += " else '其它'";
            fpselect += " end ) as fxyymc,sdy.jh, ";
            //fpselect += " fx.fxyymc, ";
            fpselect += " sdy.qk, ";
            fpselect += " sdy.ssyt,  ";
            fpselect += " sdy.pjdy, ";
            //fpselect += " djsj.tcrq, ";
            fpselect += " sdy.scsj,  ";
            fpselect += " nvl(round(sdy.rcy,2),0) as rcy, ";
            fpselect += " nvl(round(sdy.hscyl,2),0) as hscyl , ";
            fpselect += " nvl(round(sdy.jkcyl,2),0) as jkcyl, ";
            fpselect += " nvl(round(sdy.hs ,2),0) as hs, ";
            fpselect += "  yqspl,czcb, ";
            fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.czcb,0)/sdy.yqspl,2) end) as dyqczcb, ";
            fpselect += " (case when sdy.hs>ghs.num then  1 ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then  2  ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then 3";
            fpselect += "  else  4";
            fpselect += "  end ) as fxyyid ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.yxczcb,0)/sdy.yqspl,2) end) as dyqyxczcb ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.zjyxczcb,0)/sdy.yqspl,2) end) as dyqzjyxczcb ";
            fpselect += " from jdstat_djsj sdy,  gsxylb_info xy, ";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='高含水') ghs,";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='低产能') dhs ";
            fpselect += " where  sdy.gsxyjb = xy.xyjb and	 ";
            fpselect += " sdy.gsxyjb = '4' ";
            if (list == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            }
            fpselect += ")temp1,";
            fpselect += " (select * from djsj where ny='" + eny.Trim() + "')d ";
            fpselect += " where temp1.dj_id= d.dj_id(+)";


            fpselect += " ) temp";
            #endregion
            fpselect += " )";
            //////////////////////////////////////////////////////无效益
            fpselect += "union ";
            fpselect += "select temp.xymc,temp.fxyymc,temp.jh,temp.qk,temp.ssyt,temp.pjdy,temp.tcrq,temp.scsj,temp.rcy,temp.hscyl,temp.jkcyl,temp.hs,temp.yqspl,temp.dyqczcb,temp.fxyyid ";
            fpselect += " from ";
            #region
            fpselect += "(";
            fpselect += " select temp1.dj_id,temp1.xymc, temp1.fxyymc,temp1.jh,temp1.qk,temp1.ssyt, ";
            fpselect += "temp1.pjdy,d.tcrq,temp1.scsj,temp1.rcy,temp1.hscyl,temp1.jkcyl,temp1.hs,temp1.yqspl,temp1.dyqczcb,temp1.fxyyid";
            fpselect += " from ";

            fpselect += "( ";
            fpselect += " select dj_id,xy.xymc,  ";
            fpselect += "  (case when sdy.hs>ghs.num then '高含水' ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then '低产能' ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then '措施' ";
            fpselect += " else '其它'";
            fpselect += " end ) as fxyymc,sdy.jh, ";
            //fpselect += " fx.fxyymc, ";
            fpselect += " sdy.qk, ";
            fpselect += " sdy.ssyt,  ";
            fpselect += " sdy.pjdy, ";
            //fpselect += " djsj.tcrq, ";
            fpselect += " sdy.scsj,  ";
            fpselect += " nvl(round(sdy.rcy,2),0) as rcy, ";
            fpselect += " nvl(round(sdy.hscyl,2),0) as hscyl , ";
            fpselect += " nvl(round(sdy.jkcyl,2),0) as jkcyl, ";
            fpselect += " nvl(round(sdy.hs ,2),0) as hs, ";
            fpselect += " nvl(round(sdy.yqspl ,2),0) as yqspl, ";
            fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.czcb,0)/sdy.yqspl,2) end) as dyqczcb, ";
            fpselect += " (case when sdy.hs>ghs.num then  6 ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then  7  ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then 8";
            fpselect += "  else  9";
            fpselect += "  end ) as fxyyid ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.yxczcb,0)/sdy.yqspl,2) end) as dyqyxczcb ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.zjyxczcb,0)/sdy.yqspl,2) end) as dyqzjyxczcb ";
            fpselect += " from jdstat_djsj sdy,  gsxylb_info xy, ";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='高含水') ghs,";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='低产能') dhs ";
            fpselect += " where  sdy.gsxyjb = xy.xyjb and	 ";
            fpselect += " sdy.gsxyjb = '5' ";
            if (list == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            }
            fpselect += ")temp1,";
            fpselect += " (select * from djsj where ny='" + eny.Trim() + "')d ";
            fpselect += " where temp1.dj_id= d.dj_id(+)";


            fpselect += " ) temp";
            #endregion
            fpselect += " union";
            fpselect += " (select'无效益' as xymc,'小计' as fxyymc,' ' as jh,' ' as qk, ' ' as ssyt,' ' as pjdy,' ' as tcrq,sum(temp.scsj) as scsj,";
            fpselect += " sum(temp.rcy) as rcy, sum(temp.hscyl) as hscyl, sum(temp.jkcyl)as jkcyl, (case when Sum(temp.jkcyl)=0 then 0 else round((Sum(temp.jkcyl)-Sum(temp.jkcyl*(1-temp.hs/100)))/Sum(temp.jkcyl),4)*100 end) As hs, round(sum(temp.yqspl),2) as yqspl,";
            fpselect += " (case when sum(nvl(temp.yqspl,0)) = 0  then 0 else round(sum(nvl(temp.czcb,0))/sum(temp.yqspl),2) end) as dyqczcb,fxyyid";
            fpselect += " from";
            #region
            fpselect += "(";
            fpselect += " select temp1.dj_id,temp1.xymc, temp1.fxyymc,temp1.jh,temp1.qk,temp1.ssyt, temp1.czcb,";
            fpselect += "temp1.pjdy,d.tcrq,temp1.scsj,temp1.rcy,temp1.hscyl,temp1.jkcyl,temp1.hs,temp1.yqspl,temp1.dyqczcb,temp1.fxyyid";
            fpselect += " from ";

            fpselect += "( ";
            fpselect += " select dj_id,xy.xymc,  ";
            fpselect += "  (case when sdy.hs>ghs.num then '高含水' ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then '低产能' ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then '措施' ";
            fpselect += " else '其它'";
            fpselect += " end ) as fxyymc,sdy.jh, ";
            //fpselect += " fx.fxyymc, ";
            fpselect += " sdy.qk, ";
            fpselect += " sdy.ssyt,  ";
            fpselect += " sdy.pjdy, ";
            //fpselect += " djsj.tcrq, ";
            fpselect += " sdy.scsj,  ";
            fpselect += " nvl(round(sdy.rcy,2),0) as rcy, ";
            fpselect += " nvl(round(sdy.hscyl,2),0) as hscyl , ";
            fpselect += " nvl(round(sdy.jkcyl,2),0) as jkcyl, ";
            fpselect += " nvl(round(sdy.hs ,2),0) as hs, ";
            fpselect += "  yqspl,czcb, ";
            fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.czcb,0)/sdy.yqspl,2) end) as dyqczcb, ";
            fpselect += " (case when sdy.hs>ghs.num then  6 ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then  7  ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then 8";
            fpselect += "  else  9";
            fpselect += "  end ) as fxyyid ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.yxczcb,0)/sdy.yqspl,2) end) as dyqyxczcb ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.zjyxczcb,0)/sdy.yqspl,2) end) as dyqzjyxczcb ";
            fpselect += " from jdstat_djsj sdy,  gsxylb_info xy, ";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='高含水') ghs,";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='低产能') dhs ";
            fpselect += " where  sdy.gsxyjb = xy.xyjb and	 ";
            fpselect += " sdy.gsxyjb = '5' ";
            if (list == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            }
            fpselect += ")temp1,";
            fpselect += " (select * from djsj where ny='" + eny.Trim() + "')d ";
            fpselect += " where temp1.dj_id= d.dj_id(+)";


            fpselect += " ) temp";
            #endregion
            fpselect += " where fxyyid=6";
            fpselect += " group by fxyyid)";
            fpselect += " union";
            fpselect += " (select'无效益' as xymc,'小计' as fxyymc,' ' as jh,' ' as qk, ' ' as ssyt,' ' as pjdy,' ' as tcrq,sum(temp.scsj) as scsj,";
            fpselect += " sum(temp.rcy) as rcy, sum(temp.hscyl) as hscyl, sum(temp.jkcyl)as jkcyl, (case when Sum(temp.jkcyl)=0 then 0 else round((Sum(temp.jkcyl)-Sum(temp.jkcyl*(1-temp.hs/100)))/Sum(temp.jkcyl),4)*100 end) As hs, round(sum(temp.yqspl),2) as yqspl,";
            fpselect += " (case when sum(nvl(temp.yqspl,0)) = 0  then 0 else round(sum(nvl(temp.czcb,0))/sum(temp.yqspl),2) end) as dyqczcb,fxyyid";
            fpselect += " from";
            #region
            fpselect += "(";
            fpselect += " select temp1.dj_id,temp1.xymc, temp1.fxyymc,temp1.jh,temp1.qk,temp1.ssyt,temp1.czcb, ";
            fpselect += "temp1.pjdy,d.tcrq,temp1.scsj,temp1.rcy,temp1.hscyl,temp1.jkcyl,temp1.hs,temp1.yqspl,temp1.dyqczcb,temp1.fxyyid";
            fpselect += " from ";

            fpselect += "( ";
            fpselect += " select dj_id,xy.xymc,  ";
            fpselect += "  (case when sdy.hs>ghs.num then '高含水' ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then '低产能' ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then '措施' ";
            fpselect += " else '其它'";
            fpselect += " end ) as fxyymc,sdy.jh, ";
            //fpselect += " fx.fxyymc, ";
            fpselect += " sdy.qk, ";
            fpselect += " sdy.ssyt,  ";
            fpselect += " sdy.pjdy, ";
            //fpselect += " djsj.tcrq, ";
            fpselect += " sdy.scsj,  ";
            fpselect += " nvl(round(sdy.rcy,2),0) as rcy, ";
            fpselect += " nvl(round(sdy.hscyl,2),0) as hscyl , ";
            fpselect += " nvl(round(sdy.jkcyl,2),0) as jkcyl, ";
            fpselect += " nvl(round(sdy.hs ,2),0) as hs, ";
            fpselect += "  yqspl,czcb, ";
            fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.czcb,0)/sdy.yqspl,2) end) as dyqczcb, ";
            fpselect += " (case when sdy.hs>ghs.num then  6 ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then  7  ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then 8";
            fpselect += "  else  9";
            fpselect += "  end ) as fxyyid ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.yxczcb,0)/sdy.yqspl,2) end) as dyqyxczcb ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.zjyxczcb,0)/sdy.yqspl,2) end) as dyqzjyxczcb ";
            fpselect += " from jdstat_djsj sdy,  gsxylb_info xy, ";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='高含水') ghs,";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='低产能') dhs ";
            fpselect += " where  sdy.gsxyjb = xy.xyjb and	 ";
            fpselect += " sdy.gsxyjb = '5' ";
            if (list == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            }
            fpselect += ")temp1,";
            fpselect += " (select * from djsj where ny='" + eny.Trim() + "')d ";
            fpselect += " where temp1.dj_id= d.dj_id(+)";


            fpselect += " ) temp";
            #endregion
            fpselect += " where fxyyid=7";
            fpselect += " group by fxyyid)";
            fpselect += " union";
            fpselect += " (select'无效益' as xymc,'小计' as fxyymc,' ' as jh,' ' as qk, ' ' as ssyt,' ' as pjdy,' ' as tcrq,sum(temp.scsj) as scsj,";
            fpselect += " sum(temp.rcy) as rcy, sum(temp.hscyl) as hscyl, sum(temp.jkcyl)as jkcyl, (case when Sum(temp.jkcyl)=0 then 0 else round((Sum(temp.jkcyl)-Sum(temp.jkcyl*(1-temp.hs/100)))/Sum(temp.jkcyl),4)*100 end) As hs, round(sum(temp.yqspl),2) as yqspl,";
            fpselect += " (case when sum(nvl(temp.yqspl,0)) = 0  then 0 else round(sum(nvl(temp.czcb,0))/sum(temp.yqspl),2) end) as dyqczcb,fxyyid";
            fpselect += " from";
            #region
            fpselect += "(";
            fpselect += " select temp1.dj_id,temp1.xymc, temp1.fxyymc,temp1.jh,temp1.qk,temp1.ssyt, temp1.czcb,";
            fpselect += "temp1.pjdy,d.tcrq,temp1.scsj,temp1.rcy,temp1.hscyl,temp1.jkcyl,temp1.hs,temp1.yqspl,temp1.dyqczcb,temp1.fxyyid";
            fpselect += " from ";

            fpselect += "( ";
            fpselect += " select dj_id,xy.xymc,  ";
            fpselect += "  (case when sdy.hs>ghs.num then '高含水' ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then '低产能' ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then '措施' ";
            fpselect += " else '其它'";
            fpselect += " end ) as fxyymc,sdy.jh, ";
            //fpselect += " fx.fxyymc, ";
            fpselect += " sdy.qk, ";
            fpselect += " sdy.ssyt,  ";
            fpselect += " sdy.pjdy, ";
            //fpselect += " djsj.tcrq, ";
            fpselect += " sdy.scsj,  ";
            fpselect += " nvl(round(sdy.rcy,2),0) as rcy, ";
            fpselect += " nvl(round(sdy.hscyl,2),0) as hscyl , ";
            fpselect += " nvl(round(sdy.jkcyl,2),0) as jkcyl, ";
            fpselect += " nvl(round(sdy.hs ,2),0) as hs, ";
            fpselect += "  yqspl, czcb,";
            fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.czcb,0)/sdy.yqspl,2) end) as dyqczcb, ";
            fpselect += " (case when sdy.hs>ghs.num then  6 ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then  7  ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then 8";
            fpselect += "  else  9";
            fpselect += "  end ) as fxyyid ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.yxczcb,0)/sdy.yqspl,2) end) as dyqyxczcb ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.zjyxczcb,0)/sdy.yqspl,2) end) as dyqzjyxczcb ";
            fpselect += " from jdstat_djsj sdy,  gsxylb_info xy, ";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='高含水') ghs,";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='低产能') dhs ";
            fpselect += " where  sdy.gsxyjb = xy.xyjb and	 ";
            fpselect += " sdy.gsxyjb = '5' ";
            if (list == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            }
            fpselect += ")temp1,";
            fpselect += " (select * from djsj where ny='" + eny.Trim() + "')d ";
            fpselect += " where temp1.dj_id= d.dj_id(+)";


            fpselect += " ) temp";
            #endregion
            fpselect += " where fxyyid=8";
            fpselect += " group by fxyyid)";
            fpselect += " union";
            fpselect += " (select'无效益' as xymc,'小计' as fxyymc,' ' as jh,' ' as qk, ' ' as ssyt,' ' as pjdy,' ' as tcrq,sum(temp.scsj) as scsj,";
            fpselect += " sum(temp.rcy) as rcy, sum(temp.hscyl) as hscyl, sum(temp.jkcyl)as jkcyl, (case when Sum(temp.jkcyl)=0 then 0 else round((Sum(temp.jkcyl)-Sum(temp.jkcyl*(1-temp.hs/100)))/Sum(temp.jkcyl),4)*100 end) As hs, round(sum(temp.yqspl),2) as yqspl,";
            fpselect += " (case when sum(nvl(temp.yqspl,0)) = 0  then 0 else round(sum(nvl(temp.czcb,0))/sum(temp.yqspl),2) end) as dyqczcb,fxyyid";
            fpselect += " from";
            #region
            fpselect += "(";
            fpselect += " select temp1.dj_id,temp1.xymc, temp1.fxyymc,temp1.jh,temp1.qk,temp1.ssyt,temp1.czcb, ";
            fpselect += "temp1.pjdy,d.tcrq,temp1.scsj,temp1.rcy,temp1.hscyl,temp1.jkcyl,temp1.hs,temp1.yqspl,temp1.dyqczcb,temp1.fxyyid";
            fpselect += " from ";

            fpselect += "( ";
            fpselect += " select dj_id,xy.xymc,  ";
            fpselect += "  (case when sdy.hs>ghs.num then '高含水' ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then '低产能' ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then '措施' ";
            fpselect += " else '其它'";
            fpselect += " end ) as fxyymc,sdy.jh, ";
            //fpselect += " fx.fxyymc, ";
            fpselect += " sdy.qk, ";
            fpselect += " sdy.ssyt,  ";
            fpselect += " sdy.pjdy, ";
            //fpselect += " djsj.tcrq, ";
            fpselect += " sdy.scsj,  ";
            fpselect += " nvl(round(sdy.rcy,2),0) as rcy, ";
            fpselect += " nvl(round(sdy.hscyl,2),0) as hscyl , ";
            fpselect += " nvl(round(sdy.jkcyl,2),0) as jkcyl, ";
            fpselect += " nvl(round(sdy.hs ,2),0) as hs, ";
            fpselect += "  yqspl,czcb, ";
            fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.czcb,0)/sdy.yqspl,2) end) as dyqczcb, ";
            fpselect += " (case when sdy.hs>ghs.num then  6 ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then  7  ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then 8";
            fpselect += "  else  9";
            fpselect += "  end ) as fxyyid ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.yxczcb,0)/sdy.yqspl,2) end) as dyqyxczcb ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.zjyxczcb,0)/sdy.yqspl,2) end) as dyqzjyxczcb ";
            fpselect += " from jdstat_djsj sdy,  gsxylb_info xy, ";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='高含水') ghs,";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='低产能') dhs ";
            fpselect += " where  sdy.gsxyjb = xy.xyjb and	 ";
            fpselect += " sdy.gsxyjb = '5' ";
            if (list == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            }
            fpselect += ")temp1,";
            fpselect += " (select * from djsj where ny='" + eny.Trim() + "')d ";
            fpselect += " where temp1.dj_id= d.dj_id(+)";


            fpselect += " ) temp";
            #endregion
            fpselect += " where fxyyid=9";
            fpselect += " group by fxyyid)";
            fpselect += " union";
            fpselect += " (select'合计' as xymc,' ' as fxyymc,' ' as jh,' ' as qk, ' ' as ssyt,' ' as pjdy,' ' as tcrq,sum(temp.scsj) as scsj,";
            fpselect += " sum(temp.rcy) as rcy, sum(temp.hscyl) as hscyl, sum(temp.jkcyl)as jkcyl, (case when Sum(temp.jkcyl)=0 then 0 else round((Sum(temp.jkcyl)-Sum(temp.jkcyl*(1-temp.hs/100)))/Sum(temp.jkcyl),4)*100 end) As hs, round(sum(temp.yqspl),2) as yqspl,";
            fpselect += " (case when sum(nvl(temp.yqspl,0)) = 0  then 0 else round(sum(nvl(temp.czcb,0))/sum(temp.yqspl),2) end) as dyqczcb,10 as fxyyid";
            fpselect += " from";
            #region
            fpselect += "(";
            fpselect += " select temp1.dj_id,temp1.xymc, temp1.fxyymc,temp1.jh,temp1.qk,temp1.ssyt, temp1.czcb,";
            fpselect += "temp1.pjdy,d.tcrq,temp1.scsj,temp1.rcy,temp1.hscyl,temp1.jkcyl,temp1.hs,temp1.yqspl,temp1.dyqczcb,temp1.fxyyid";
            fpselect += " from ";

            fpselect += "( ";
            fpselect += " select dj_id,xy.xymc,  ";
            fpselect += "  (case when sdy.hs>ghs.num then '高含水' ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then '低产能' ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then '措施' ";
            fpselect += " else '其它'";
            fpselect += " end ) as fxyymc,sdy.jh, ";
            //fpselect += " fx.fxyymc, ";
            fpselect += " sdy.qk, ";
            fpselect += " sdy.ssyt,  ";
            fpselect += " sdy.pjdy, ";
            //fpselect += " djsj.tcrq, ";
            fpselect += " sdy.scsj,  ";
            fpselect += " nvl(round(sdy.rcy,2),0) as rcy, ";
            fpselect += " nvl(round(sdy.hscyl,2),0) as hscyl , ";
            fpselect += " nvl(round(sdy.jkcyl,2),0) as jkcyl, ";
            fpselect += " nvl(round(sdy.hs ,2),0) as hs, ";
            fpselect += "  yqspl,czcb, ";
            fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.czcb,0)/sdy.yqspl,2) end) as dyqczcb, ";
            fpselect += " (case when sdy.hs>ghs.num then  6 ";
            fpselect += " when (sdy.hs<=ghs.num and sdy.hscyl<dhs.num) then  7  ";
            fpselect += " when(sdy.hs<=ghs.num and sdy.hscyl>=dhs.num and (sdy.jxzyf-sdy.whxjxzyf)>10000) then 8";
            fpselect += "  else  9";
            fpselect += "  end ) as fxyyid ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.yxczcb,0)/sdy.yqspl,2) end) as dyqyxczcb ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.zjyxczcb,0)/sdy.yqspl,2) end) as dyqzjyxczcb ";
            fpselect += " from jdstat_djsj sdy,  gsxylb_info xy, ";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='高含水') ghs,";
            fpselect += " (select num from csdy_info where ny='" + eny + "' and name='低产能') dhs ";
            fpselect += " where  sdy.gsxyjb = xy.xyjb and	 ";
            fpselect += " sdy.gsxyjb = '5' ";
            if (list == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            }
            fpselect += ")temp1,";
            fpselect += " (select * from djsj where ny='" + eny.Trim() + "')d ";
            fpselect += " where temp1.dj_id= d.dj_id(+)";


            fpselect += " ) temp";
            #endregion
            fpselect += " )";
            fpselect += " order by fxyyid";

            SqlHelper conn = new SqlHelper();
            OracleConnection connfp = conn.GetConn();

            //OracleConnection connfp = DB.CreatConnection();

            try
            {
                connfp.Open();
                OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                DataSet fpset = new DataSet();
                da.Fill(fpset, "fpdata");

                //此处用于绑定数据             
                #region
                int rcount = fpset.Tables["fpdata"].Rows.Count;
                int ccount = fpset.Tables["fpdata"].Columns.Count - 1;
                int hcount = 6;

                //FpSpread1.ColumnHeader.Visible = false;
                //FpSpread1.RowHeader.Visible = false;
                if (FpSpread1.Sheets[0].Rows.Count == hcount)
                {
                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            if (j != 6 && j != 7 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
                            }
                            else
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            }
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }

                    {
                        int m = 1;
                        int k = 1;  //统计重复单元格
                        int w = hcount;  //记录起始位置
                        for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                        {
                            if (FpSpread1.Sheets[0].Cells[i, m].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, m].Value.ToString() && FpSpread1.Sheets[0].Cells[i, m].Value.ToString() != "")
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
                    {
                        int m = 0;
                        int k = 1;  //统计重复单元格
                        int w = hcount;  //记录起始位置
                        for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                        {
                            if (FpSpread1.Sheets[0].Cells[i, m].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, m].Value.ToString() && FpSpread1.Sheets[0].Cells[i, m].Value.ToString() != "")
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
                }
                else//不为空
                {
                    string path = Page.MapPath("~/static/excel/gufenbiao.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "表17-边际、无效井产生原因初步分析表");


                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;


                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            if (j != 6 && j != 7 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
                            }
                            else
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            }
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }

                    {
                        int m = 1;
                        int k = 1;  //统计重复单元格
                        int w = hcount;  //记录起始位置
                        for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                        {
                            if (FpSpread1.Sheets[0].Cells[i, m].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, m].Value.ToString() && FpSpread1.Sheets[0].Cells[i, m].Value.ToString() != "")
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
                    {
                        int m = 0;
                        int k = 1;  //统计重复单元格
                        int w = hcount;  //记录起始位置
                        for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                        {
                            if (FpSpread1.Sheets[0].Cells[i, m].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, m].Value.ToString() && FpSpread1.Sheets[0].Cells[i, m].Value.ToString() != "")
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
                }

                #endregion

            }
            catch (OracleException error)
            {
                string CuoWu = "错误: " + error.Message.ToString();
                Response.Write(CuoWu);
            }
            finally
            {
                connfp.Close();
            }
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
            FarpointGridChange.FarPointChange(FpSpread1, "dt_biao15.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;


        }

    }
}
