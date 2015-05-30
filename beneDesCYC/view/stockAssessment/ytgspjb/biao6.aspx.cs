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

namespace beneDesCYC.view.stockAssessment.ytgspjb
{
    public partial class biao6 : beneDesCYC.core.UI.corePage
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
            this.FpSpread1.Sheets[0].OpenExcel(path, "表6-区块操作成本基础数据表");

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
            this.FpSpread1.Sheets[0].OpenExcel(path, "表6-区块操作成本基础数据表");

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

            string cycid = Session["cyc_id"].ToString();
            //OracleConnection connfp = DB.CreatConnection();
            SqlHelper conn = new SqlHelper();
            OracleConnection connfp = conn.GetConn();

            string fpselect = "";
            string fpselect2 = "";
            if (typeid == "pjdy")
            {
                //fpselect += "SELECT gsxylb_info.xymc, SSYT, PJDYMC, nvl(ZJCLF,0), nvl(ZJRLF,0), nvl(ZJDLF,0),  nvl(QYWZRF,0), nvl(JXZYF,0), nvl(CJSJF,0), nvl(WHXLF,0), nvl(cyrcf,0), nvl(YQCLF,0), nvl(QTHSF,0), nvl(YSF,0), nvl(QTZJF,0), nvl(CKGLF,0),nvl(ZJRYF,0), nvl(ZYYQCP,0), nvl(yxczCB,0), nvl(CZCB,0) ";
                //fpselect += "FROM VIEW_DTBIAO16,gsxylb_info where gsxylb_info.xyjb = gsxyjb order by gsxyjb,od ";

                fpselect += " Select   ";
                fpselect += " 0 as xuhao,sdy.mc as pjdymc,  ";
                fpselect += " sdy.ssyt as ssyt, ";
                fpselect += " xy.xymc as xyjb,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else sum(zjclf)/sum(yqspl) end),2) As zjclf,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjrlf)/sum(yqspl) end),2) As zjrlf,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjdlf)/sum(yqspl) end),2) As zjdlf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjryf)/sum(yqspl) end),2) As zjryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qywzrf)/sum(yqspl) end),2) As qywzrf, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(qywzrf_ryf),0)/sum(yqspl) end),2) As qywzrf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(jxzyf)/sum(yqspl) end),2) As jxzyf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(cjsjf)/sum(yqspl) end),2) As cjsjf, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(cjsjf_ryf),0)/sum(yqspl) end),2) As cjsjf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(whxlf)/sum(yqspl) end),2) As whxlf, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(whxlf_ryf),0)/sum(yqspl) end),2) As whxlf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(yqclf)/sum(yqspl) end),2) As yqclf, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(yqclf_ryf),0)/sum(yqspl) end),2) As yqclf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ysf)/sum(yqspl) end),2) As ysf, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(ysf),0)/sum(yqspl) end),2) As ysf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qtzjf)/sum(yqspl) end),2) As qtzjf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ckglf)/sum(yqspl) end),2) As ckglf, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else (nvl(Sum(zjryf),0)+nvl(sum(qywzrf_ryf),0)+nvl(sum(cjsjf_ryf),0)+nvl(sum(whxlf_ryf),0)+nvl(sum(yqclf_ryf),0)+ nvl(sum(ysf_ryf),0))/sum(yqspl) end),2) As ckglf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zyyqcp)/sum(yqspl) end),2) As zyyqcp, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(yxczcb)/sum(yqspl) end),2) As yxczcb, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(czcb)/sum(yqspl) end),2) As czcb, ";
                fpselect += " sdy.gsxyjb ";
                fpselect += " From jdstat_pjdysj sdy,qkxyjb xy ";
                fpselect += " Where sfpj = 1  ";
                fpselect += " and sdy.gsxyjb = xy.xyjb  ";
                fpselect += " and sdy.cyc_id = '" + cycid + "' ";
                fpselect += " Group By xy.xymc ,sdy.ssyt,sdy.mc,sdy.gsxyjb ";

                fpselect += "union ";
                fpselect += " Select 1  as xuhao,  ";
                fpselect += "  ' ' as pjdymc, ";
                fpselect += "  ' ' as ssyt, ";
                fpselect += "  '合计' as xyjb, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else sum(zjclf)/sum(yqspl) end),2) As zjclf,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjrlf)/sum(yqspl) end),2) As zjrlf,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjdlf)/sum(yqspl) end),2) As zjdlf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjryf)/sum(yqspl) end),2) As zjryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qywzrf)/sum(yqspl) end),2) As qywzrf, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(qywzrf_ryf),0)/sum(yqspl) end),2) As qywzrf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(jxzyf)/sum(yqspl) end),2) As jxzyf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(cjsjf)/sum(yqspl) end),2) As cjsjf, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(cjsjf_ryf),0)/sum(yqspl) end),2) As cjsjf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(whxlf)/sum(yqspl) end),2) As whxlf, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(whxlf_ryf),0)/sum(yqspl) end),2) As whxlf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(yqclf)/sum(yqspl) end),2) As yqclf, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(yqclf_ryf),0)/sum(yqspl) end),2) As yqclf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ysf)/sum(yqspl) end),2) As ysf, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(ysf_ryf),0)/sum(yqspl) end),2) As ysf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qtzjf)/sum(yqspl) end),2) As qtzjf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ckglf)/sum(yqspl) end),2) As ckglf, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else (nvl(Sum(zjryf),0)+nvl(sum(qywzrf_ryf),0)+nvl(sum(cjsjf_ryf),0)+nvl(sum(whxlf_ryf),0)+nvl(sum(yqclf_ryf),0)+ nvl(sum(ysf_ryf),0))/sum(yqspl) end),2) As ckglf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zyyqcp)/sum(yqspl) end),2) As zyyqcp, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(yxczcb)/sum(yqspl) end),2) As yxczcb, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(czcb)/sum(yqspl) end),2) As czcb, ";
                fpselect += " 80000 as gsxyjb ";
                fpselect += " From jdstat_pjdysj sdy,qkxyjb xy ";
                fpselect += " Where sfpj = 1  ";
                fpselect += " and sdy.gsxyjb = xy.xyjb  ";
                fpselect += " and sdy.cyc_id = '" + cycid + "' ";
                fpselect += " order by gsxyjb ";



            }
            else if (typeid == "qk")
            {
                //fpselect += "SELECT gsxylb_info.xymc, SSYT, PJDYMC, nvl(ZJCLF,0), nvl(ZJRLF,0), nvl(ZJDLF,0),  nvl(QYWZRF,0), nvl(JXZYF,0), nvl(CJSJF,0), nvl(WHXLF,0), nvl(cyrcf,0), nvl(YQCLF,0), nvl(QTHSF,0), nvl(YSF,0), nvl(QTZJF,0), nvl(CKGLF,0),nvl(ZJRYF,0), nvl(ZYYQCP,0), nvl(yxczCB,0), nvl(CZCB,0) ";
                //fpselect += "FROM VIEW_DTBIAO16_1,gsxylb_info where gsxylb_info.xyjb = gsxyjb order by gsxyjb,od ";
                fpselect += " Select  ";
                fpselect += " 0 as xuhao, sdy.mc as pjdymc, ";
                fpselect += " sdy.ssyt as ssyt, ";
                fpselect += "  xy.xymc as xyjb,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else sum(zjclf)/sum(yqspl) end),2) As zjclf,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjrlf)/sum(yqspl) end),2) As zjrlf,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjdlf)/sum(yqspl) end),2) As zjdlf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjryf)/sum(yqspl) end),2) As zjryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qywzrf)/sum(yqspl) end),2) As qywzrf, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(qywzrf_ryf),0)/sum(yqspl) end),2) As qywzrf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(jxzyf)/sum(yqspl) end),2) As jxzyf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(cjsjf)/sum(yqspl) end),2) As cjsjf, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(cjsjf_ryf),0)/sum(yqspl) end),2) As cjsjf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(whxlf)/sum(yqspl) end),2) As whxlf, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(whxlf_ryf),0)/sum(yqspl) end),2) As whxlf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(yqclf)/sum(yqspl) end),2) As yqclf, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(yqclf_ryf),0)/sum(yqspl) end),2) As yqclf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ysf)/sum(yqspl) end),2) As ysf, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(ysf_ryf),0)/sum(yqspl) end),2) As ysf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qtzjf)/sum(yqspl) end),2) As qtzjf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ckglf)/sum(yqspl) end),2) As ckglf, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else (nvl(Sum(zjryf),0)+nvl(sum(qywzrf_ryf),0)+nvl(sum(cjsjf_ryf),0)+nvl(sum(whxlf_ryf),0)+nvl(sum(yqclf_ryf),0)+ nvl(sum(ysf_ryf),0))/sum(yqspl) end),2) As ckglf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zyyqcp)/sum(yqspl) end),2) As zyyqcp, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(yxczcb)/sum(yqspl) end),2) As yxczcb, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(czcb)/sum(yqspl) end),2) As czcb, ";
                fpselect += " sdy.gsxyjb ";
                fpselect += " From jdstat_qksj sdy,qkxyjb xy ";
                fpselect += " Where sfpj = 1  ";
                fpselect += " and sdy.gsxyjb = xy.xyjb  ";
                fpselect += " and sdy.cyc_id = '" + cycid + "' ";
                fpselect += " Group By xy.xymc ,sdy.ssyt,sdy.mc,sdy.gsxyjb ";

                fpselect += " union ";
                fpselect += " Select 1  as xuhao,  ";
                fpselect += "  ' ' as pjdymc, ";
                fpselect += "  ' ' as ssyt, ";
                fpselect += "  '合计' as xyjb, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else sum(zjclf)/sum(yqspl) end),2) As zjclf,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjrlf)/sum(yqspl) end),2) As zjrlf,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjdlf)/sum(yqspl) end),2) As zjdlf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjryf)/sum(yqspl) end),2) As zjryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qywzrf)/sum(yqspl) end),2) As qywzrf, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(qywzrf_ryf),0)/sum(yqspl) end),2) As qywzrf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(jxzyf)/sum(yqspl) end),2) As jxzyf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(cjsjf)/sum(yqspl) end),2) As cjsjf, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(cjsjf_ryf),0)/sum(yqspl) end),2) As cjsjf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(whxlf)/sum(yqspl) end),2) As whxlf, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(whxlf_ryf),0)/sum(yqspl) end),2) As whxlf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(yqclf)/sum(yqspl) end),2) As yqclf, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(yqclf_ryf),0)/sum(yqspl) end),2) As yqclf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ysf)/sum(yqspl) end),2) As ysf, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(ysf_ryf),0)/sum(yqspl) end),2) As ysf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qtzjf)/sum(yqspl) end),2) As qtzjf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ckglf)/sum(yqspl) end),2) As ckglf, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else (nvl(Sum(zjryf),0)+nvl(sum(qywzrf_ryf),0)+nvl(sum(cjsjf_ryf),0)+nvl(sum(whxlf_ryf),0)+nvl(sum(yqclf_ryf),0)+ nvl(sum(ysf_ryf),0))/sum(yqspl) end),2) As ckglf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zyyqcp)/sum(yqspl) end),2) As zyyqcp, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(yxczcb)/sum(yqspl) end),2) As yxczcb, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(czcb)/sum(yqspl) end),2) As czcb, ";
                fpselect += " 80000 as gsxyjb ";
                fpselect += " From jdstat_qksj sdy,qkxyjb xy ";
                fpselect += " Where sfpj = 1  ";
                fpselect += " and sdy.gsxyjb = xy.xyjb  ";
                fpselect += " and sdy.cyc_id = '" + cycid + "' ";
                fpselect += " order by gsxyjb ";

            }
            else if (typeid == "zyq")
            {
                fpselect += " Select   ";
                fpselect += " 0 as xuhao,  ";
                fpselect += " sdy.ssyt as ssyt, ";
                fpselect += " d.dep_name as pjdymc,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else sum(zjclf)/sum(yqspl) end),2) As zjclf,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjrlf)/sum(yqspl) end),2) As zjrlf,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjdlf)/sum(yqspl) end),2) As zjdlf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjryf)/sum(yqspl) end),2) As zjryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qywzrf)/sum(yqspl) end),2) As qywzrf, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(qywzrf_ryf),0)/sum(yqspl) end),2) As qywzrf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(jxzyf)/sum(yqspl) end),2) As jxzyf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(cjsjf)/sum(yqspl) end),2) As cjsjf, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(cjsjf_ryf),0)/sum(yqspl) end),2) As cjsjf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(whxlf)/sum(yqspl) end),2) As whxlf, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(whxlf_ryf),0)/sum(yqspl) end),2) As whxlf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(yqclf)/sum(yqspl) end),2) As yqclf, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(yqclf_ryf),0)/sum(yqspl) end),2) As yqclf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ysf)/sum(yqspl) end),2) As ysf, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(ysf_ryf),0)/sum(yqspl) end),2) As ysf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qtzjf)/sum(yqspl) end),2) As qtzjf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ckglf)/sum(yqspl) end),2) As ckglf, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else (nvl(Sum(zjryf),0)+nvl(sum(qywzrf_ryf),0)+nvl(sum(cjsjf_ryf),0)+nvl(sum(whxlf_ryf),0)+nvl(sum(yqclf_ryf),0)+ nvl(sum(ysf_ryf),0))/sum(yqspl) end),2) As ckglf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zyyqcp)/sum(yqspl) end),2) As zyyqcp, ";
                //fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(yxczcb)/sum(yqspl) end),2) As yxczcb, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(czcb)/sum(yqspl) end),2) As czcb, ";
                fpselect += " sdy.zyqdm as zyqdm ";

                fpselect += " From view_jdstat_zyqsj sdy, department d  ";
                fpselect += " where   sdy.cyc_id = '" + cycid + "' ";
                fpselect += " and  sdy.cyc_id = d.parent_id and sdy.zyqdm = d.dep_id  ";
                fpselect += " group by sdy.zyqdm, d.dep_name,sdy.ssyt ";

                fpselect += " union ";
                fpselect += " Select 1  as xuhao,  ";
                fpselect += "  ' ' as ssyt, ";
                fpselect += "  '合计 ' as pjdymc, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else sum(zjclf)/sum(yqspl) end),2) As zjclf,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjrlf)/sum(yqspl) end),2) As zjrlf,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjdlf)/sum(yqspl) end),2) As zjdlf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjryf)/sum(yqspl) end),2) As zjryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qywzrf)/sum(yqspl) end),2) As qywzrf, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(qywzrf_ryf),0)/sum(yqspl) end),2) As qywzrf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(jxzyf)/sum(yqspl) end),2) As jxzyf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(cjsjf)/sum(yqspl) end),2) As cjsjf, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(cjsjf_ryf),0)/sum(yqspl) end),2) As cjsjf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(whxlf)/sum(yqspl) end),2) As whxlf, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(whxlf_ryf),0)/sum(yqspl) end),2) As whxlf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(yqclf)/sum(yqspl) end),2) As yqclf, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(yqclf_ryf),0)/sum(yqspl) end),2) As yqclf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ysf)/sum(yqspl) end),2) As ysf, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(ysf_ryf),0)/sum(yqspl) end),2) As ysf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qtzjf)/sum(yqspl) end),2) As qtzjf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ckglf)/sum(yqspl) end),2) As ckglf, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else (nvl(Sum(zjryf),0)+nvl(sum(qywzrf_ryf),0)+nvl(sum(cjsjf_ryf),0)+nvl(sum(whxlf_ryf),0)+nvl(sum(yqclf_ryf),0)+ nvl(sum(ysf_ryf),0))/sum(yqspl) end),2) As ckglf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zyyqcp)/sum(yqspl) end),2) As zyyqcp, ";
                //fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(yxczcb)/sum(yqspl) end),2) As yxczcb, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(czcb)/sum(yqspl) end),2) As czcb, ";
                fpselect += " 'zz ' as zyqdm ";
                fpselect += " From view_jdstat_zyqsj sdy  ";
                fpselect += " where   sdy.cyc_id = '" + cycid + "' ";
                fpselect += " order by zyqdm ";

            }
            try
            {
                connfp.Open();
                OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                //OracleDataAdapter da2 = new OracleDataAdapter(fpselect2, connfp);
                DataSet fpset = new DataSet();
                da.Fill(fpset, "fpdata");
                //da2.Fill(fpset, "fpdata2");
                #region
                //if (dt.Rows.Count > 0)
                //{
                //    //计算合计
                //    
                //    DataRow dr = dt.NewRow();
                //    dr[0] = "合计";
                //    //dr[1] = "";
                //    //初始化各列
                //    for (int i = 2; i < dt.Columns.Count; i++)
                //    {
                //        dr[i] = 0;
                //    }
                //    //计算
                //    for (int k = 0; k < dt.Rows.Count; k++)//循环行
                //    {
                //        for (int n = 2; n < dt.Columns.Count; n++)//循环计算列
                //        {
                //            dr[n] = float.Parse(dr[n].ToString()) + float.Parse(dt.Rows[k][n].ToString());
                //        }
                //    }
                //    dr[8] = float.Parse(dr[8].ToString()) / dt.Rows.Count;

                //    //计算精度
                //    for (int m = 2; m < dt.Columns.Count; m++)
                //    {
                //        dr[m] = Math.Round(float.Parse(dr[m].ToString()), 4);
                //    }
                //    //把行加入表
                //    dt.Rows.Add(dr);
                //}
                //   
                #endregion
                //此处用于绑定数据    

                #region
                int rcount = fpset.Tables["fpdata"].Rows.Count;
                int ccount = fpset.Tables["fpdata"].Columns.Count;
                int hcount = 4;
                //FpSpread1.ColumnHeader.Visible = false;
                //FpSpread1.RowHeader.Visible = false;
                //if (FpSpread1.Sheets[0].Rows.Count == hcount)
                //{
                if (typeid == "zyq")
                {
                    string path = "../../../static/excel/zuoyequ1.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表6-区块操作成本基础数据表");

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
                            FpSpread1.Sheets[0].Cells[i + hcount, 0].Value = i + 1;
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
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表6-区块操作成本基础数据表");

                     FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;

               //     FpSpread1.Sheets[0].Cells[1, 0].Value = tbdw;      ////////09.5.13  3-3
                    FpSpread1.Sheets[0].Cells[1, 0].Font.Size = 9;     ////////09.5.13  3-3

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount-1; j++)
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            FpSpread1.Sheets[0].Cells[i + hcount, 0].Value = i + 1;
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
            FarpointGridChange.FarPointChange(FpSpread1, "biao6.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;


        }


    }
}
