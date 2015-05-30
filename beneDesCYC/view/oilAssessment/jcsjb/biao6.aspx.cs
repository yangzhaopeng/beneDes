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


namespace beneDesCYC.view.oilAssessment.jcsjb
{
    public partial class biao6 : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            //    fpselect += " Select  '', ";
            //    fpselect += " 0 as xuhao,  ";
            //    fpselect += " ' ' as pjdymc, ";
            //    fpselect += "  d.dep_name as ssyt, ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else sum(zjclf)/sum(yqspl) end),2) As zjclf,  ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjrlf)/sum(yqspl) end),2) As zjrlf,  ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjdlf)/sum(yqspl) end),2) As zjdlf, ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjryf)/sum(yqspl) end),2) As zjryf, ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qywzrf)/sum(yqspl) end),2) As qywzrf, ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(qywzrf_ryf),0)/sum(yqspl) end),2) As qywzrf_ryf, ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(jxzyf)/sum(yqspl) end),2) As jxzyf, ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(cjsjf)/sum(yqspl) end),2) As cjsjf, ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(cjsjf_ryf),0)/sum(yqspl) end),2) As cjsjf_ryf, ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(whxlf)/sum(yqspl) end),2) As whxlf, ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(whxlf_ryf),0)/sum(yqspl) end),2) As whxlf_ryf, ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(yqclf)/sum(yqspl) end),2) As yqclf, ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(yqclf_ryf),0)/sum(yqspl) end),2) As yqclf_ryf, ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ysf)/sum(yqspl) end),2) As ysf, ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(ysf_ryf),0)/sum(yqspl) end),2) As ysf_ryf, ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qtzjf)/sum(yqspl) end),2) As qtzjf, ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ckglf)/sum(yqspl) end),2) As ckglf, ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else (nvl(Sum(zjryf),0)+nvl(sum(qywzrf_ryf),0)+nvl(sum(cjsjf_ryf),0)+nvl(sum(whxlf_ryf),0)+nvl(sum(yqclf_ryf),0)+ nvl(sum(ysf_ryf),0))/sum(yqspl) end),2) As ckglf, ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zyyqcp)/sum(yqspl) end),2) As zyyqcp, ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(yxczcb)/sum(yqspl) end),2) As yxczcb, ";
            //    fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(czcb)/sum(yqspl) end),2) As czcb ";
            //    //fpselect += " sdy.zyqdm as zyq ";

            //    fpselect += " From view_dtstat_zyqsj sdy ,department d";
            //    fpselect += " Where sdy.djisopen= '1' and sdy.dep_id=d.dep_id ";
            //    //fpselect += "  ";
            //    if (list == "quan")
            //    {
            //        fpselect += "";
            //    }
            //    else
            //    {
            //        fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            //    }
            //    fpselect += " Group By d.dep_name ";
            //    fpselect += " order by d.dep_name ";
            //    /////////////////////09.4.30/////////////////////////////
            //    fpselect2 += " Select  '' , ";
            //    fpselect2 += " null as xuhao,  ";
            //    fpselect2 += " null as pjdymc, ";
            //    fpselect2 += " '合计' as ssyt, ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else sum(zjclf)/sum(yqspl) end),2) As zjclf,  ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(zjrlf)/sum(yqspl) end),2) As zjrlf,  ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(zjdlf)/sum(yqspl) end),2) As zjdlf, ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(zjryf)/sum(yqspl) end),2) As zjryf, ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(qywzrf)/sum(yqspl) end),2) As qywzrf, ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(qywzrf_ryf),0)/sum(yqspl) end),2) As qywzrf_ryf, ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(jxzyf)/sum(yqspl) end),2) As jxzyf, ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(cjsjf)/sum(yqspl) end),2) As cjsjf, ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(cjsjf_ryf),0)/sum(yqspl) end),2) As cjsjf_ryf, ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(whxlf)/sum(yqspl) end),2) As whxlf, ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(whxlf_ryf),0)/sum(yqspl) end),2) As whxlf_ryf, ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(yqclf)/sum(yqspl) end),2) As yqclf, ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(yqclf_ryf),0)/sum(yqspl) end),2) As yqclf_ryf, ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(ysf)/sum(yqspl) end),2) As ysf, ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(ysf_ryf),0)/sum(yqspl) end),2) As ysf_ryf, ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(qtzjf)/sum(yqspl) end),2) As qtzjf, ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(ckglf)/sum(yqspl) end),2) As ckglf, ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else (nvl(Sum(zjryf),0)+nvl(sum(qywzrf_ryf),0)+nvl(sum(cjsjf_ryf),0)+nvl(sum(whxlf_ryf),0)+nvl(sum(yqclf_ryf),0)+ nvl(sum(ysf_ryf),0))/sum(yqspl) end),2) As ckglf, ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(zyyqcp)/sum(yqspl) end),2) As zyyqcp, ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(yxczcb)/sum(yqspl) end),2) As yxczcb, ";
            //    fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(czcb)/sum(yqspl) end),2) As czcb ";
            //    //fpselect2 += " null as zyq ";

            //    fpselect2 += " From view_dtstat_zyqsj sdy ";
            //    fpselect2 += " Where sdy.djisopen= '1'  ";
            //    //fpselect2 += "  ";
            //    if (list == "quan")
            //    {
            //        fpselect2 += "";
            //    }
            //    else
            //    {
            //        fpselect2 += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            //    }
            //}
            if (typeid == "pjdy")
            {
                //fpselect += "SELECT dtxyjb_info.jbmc, SSYT, PJDYMC, nvl(ZJCLF,0), nvl(ZJRLF,0), nvl(ZJDLF,0),  nvl(QYWZRF,0), nvl(JXZYF,0), nvl(CJSJF,0), nvl(WHXLF,0), nvl(cyrcf,0), nvl(YQCLF,0), nvl(QTHSF,0), nvl(YSF,0), nvl(QTZJF,0), nvl(CKGLF,0),nvl(ZJRYF,0), nvl(ZYYQCP,0), nvl(yxczCB,0), nvl(CZCB,0) ";
                //fpselect += "FROM VIEW_DTBIAO16,dtxyjb_info where dtxyjb_info.jbid = GSXYJB_1 order by GSXYJB_1,od ";

                fpselect += " Select 0 as xuhao,  ";
                fpselect += "  sdy.mc as pjdymc, ";
                fpselect += " sdy.ssyt as ssyt, ";
                fpselect += "  xy.jbmc as xyjb, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else sum(zjclf)/sum(yqspl) end),2) As zjclf,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjrlf)/sum(yqspl) end),2) As zjrlf,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjdlf)/sum(yqspl) end),2) As zjdlf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjryf)/sum(yqspl) end),2) As zjryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qywzrf)/sum(yqspl) end),2) As qywzrf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(qywzrf_ryf),0)/sum(yqspl) end),2) As qywzrf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(jxzyf)/sum(yqspl) end),2) As jxzyf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(cjsjf)/sum(yqspl) end),2) As cjsjf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(cjsjf_ryf),0)/sum(yqspl) end),2) As cjsjf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(whxlf)/sum(yqspl) end),2) As whxlf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(whxlf_ryf),0)/sum(yqspl) end),2) As whxlf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(yqclf)/sum(yqspl) end),2) As yqclf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(yqclf_ryf),0)/sum(yqspl) end),2) As yqclf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ysf)/sum(yqspl) end),2) As ysf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(ysf_ryf),0)/sum(yqspl) end),2) As ysf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qtzjf)/sum(yqspl) end),2) As qtzjf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ckglf)/sum(yqspl) end),2) As ckglf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else (nvl(Sum(zjryf),0)+nvl(sum(qywzrf_ryf),0)+nvl(sum(cjsjf_ryf),0)+nvl(sum(whxlf_ryf),0)+nvl(sum(yqclf_ryf),0)+ nvl(sum(ysf_ryf),0))/sum(yqspl) end),2) As ckglf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zyyqcp)/sum(yqspl) end),2) As zyyqcp, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(yxczcb)/sum(yqspl) end),2) As yxczcb, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(czcb)/sum(yqspl) end),2) As czcb ";
                fpselect += " From dtstat_pjdysj sdy,dtxyjb_info xy ";
                fpselect += " Where sfpj = 1  ";
                fpselect += " and sdy.gsxyjb_1 = xy.jbid  ";
                fpselect += " and sdy.cyc_id = '" + cycid + "' ";
                fpselect += " Group By xy.jbmc ,sdy.ssyt,sdy.mc ";

                ///////////////////////////////////09.4.30更新
                fpselect2 += " Select null  as xuhao,  ";
                fpselect2 += "  null as pjdymc, ";
                fpselect2 += "  null as ssyt, ";
                fpselect2 += "  '合计' as heji, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else sum(zjclf)/sum(yqspl) end),2) As zjclf,  ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(zjrlf)/sum(yqspl) end),2) As zjrlf,  ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(zjdlf)/sum(yqspl) end),2) As zjdlf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(zjryf)/sum(yqspl) end),2) As zjryf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(qywzrf)/sum(yqspl) end),2) As qywzrf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(qywzrf_ryf),0)/sum(yqspl) end),2) As qywzrf_ryf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(jxzyf)/sum(yqspl) end),2) As jxzyf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(cjsjf)/sum(yqspl) end),2) As cjsjf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(cjsjf_ryf),0)/sum(yqspl) end),2) As cjsjf_ryf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(whxlf)/sum(yqspl) end),2) As whxlf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(whxlf_ryf),0)/sum(yqspl) end),2) As whxlf_ryf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(yqclf)/sum(yqspl) end),2) As yqclf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(yqclf_ryf),0)/sum(yqspl) end),2) As yqclf_ryf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(ysf)/sum(yqspl) end),2) As ysf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(ysf_ryf),0)/sum(yqspl) end),2) As ysf_ryf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(qtzjf)/sum(yqspl) end),2) As qtzjf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(ckglf)/sum(yqspl) end),2) As ckglf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else (nvl(Sum(zjryf),0)+nvl(sum(qywzrf_ryf),0)+nvl(sum(cjsjf_ryf),0)+nvl(sum(whxlf_ryf),0)+nvl(sum(yqclf_ryf),0)+ nvl(sum(ysf_ryf),0))/sum(yqspl) end),2) As ckglf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(zyyqcp)/sum(yqspl) end),2) As zyyqcp, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(yxczcb)/sum(yqspl) end),2) As yxczcb, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(czcb)/sum(yqspl) end),2) As czcb ";
                fpselect2 += " From dtstat_pjdysj sdy,dtxyjb_info xy ";
                fpselect2 += " Where sfpj = 1  ";
                fpselect2 += " and sdy.gsxyjb_1 = xy.jbid  ";
                fpselect2 += " and sdy.cyc_id = '" + cycid + "' ";

                /////////////////////////////////////////////////


            }
            else if (typeid == "qk")
            {
                //fpselect += "SELECT dtxyjb_info.jbmc, SSYT, PJDYMC, nvl(ZJCLF,0), nvl(ZJRLF,0), nvl(ZJDLF,0),  nvl(QYWZRF,0), nvl(JXZYF,0), nvl(CJSJF,0), nvl(WHXLF,0), nvl(cyrcf,0), nvl(YQCLF,0), nvl(QTHSF,0), nvl(YSF,0), nvl(QTZJF,0), nvl(CKGLF,0),nvl(ZJRYF,0), nvl(ZYYQCP,0), nvl(yxczCB,0), nvl(CZCB,0) ";
                //fpselect += "FROM VIEW_DTBIAO16_1,dtxyjb_info where dtxyjb_info.jbid = GSXYJB_1 order by GSXYJB_1,od ";
                fpselect += " Select 0 as xuhao,  ";
                fpselect += "  sdy.mc as pjdymc, ";
                fpselect += " sdy.ssyt as ssyt, ";
                fpselect += "  xy.jbmc as xyjb, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else sum(zjclf)/sum(yqspl) end),2) As zjclf,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjrlf)/sum(yqspl) end),2) As zjrlf,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjdlf)/sum(yqspl) end),2) As zjdlf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjryf)/sum(yqspl) end),2) As zjryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qywzrf)/sum(yqspl) end),2) As qywzrf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(qywzrf_ryf),0)/sum(yqspl) end),2) As qywzrf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(jxzyf)/sum(yqspl) end),2) As jxzyf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(cjsjf)/sum(yqspl) end),2) As cjsjf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(cjsjf_ryf),0)/sum(yqspl) end),2) As cjsjf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(whxlf)/sum(yqspl) end),2) As whxlf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(whxlf_ryf),0)/sum(yqspl) end),2) As whxlf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(yqclf)/sum(yqspl) end),2) As yqclf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(yqclf_ryf),0)/sum(yqspl) end),2) As yqclf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ysf)/sum(yqspl) end),2) As ysf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(ysf_ryf),0)/sum(yqspl) end),2) As ysf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qtzjf)/sum(yqspl) end),2) As qtzjf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ckglf)/sum(yqspl) end),2) As ckglf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else (nvl(Sum(zjryf),0)+nvl(sum(qywzrf_ryf),0)+nvl(sum(cjsjf_ryf),0)+nvl(sum(whxlf_ryf),0)+nvl(sum(yqclf_ryf),0)+ nvl(sum(ysf_ryf),0))/sum(yqspl) end),2) As ckglf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zyyqcp)/sum(yqspl) end),2) As zyyqcp, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(yxczcb)/sum(yqspl) end),2) As yxczcb, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(czcb)/sum(yqspl) end),2) As czcb ";

                fpselect += " From dtstat_qksj sdy,dtxyjb_info xy ";
                fpselect += " Where sfpj = 1  ";
                fpselect += " and sdy.gsxyjb_1 = xy.jbid  ";
                fpselect += " and sdy.cyc_id = '" + cycid + "' ";
                fpselect += " Group By xy.jbmc ,sdy.ssyt,sdy.mc ";

                //////////////09.4.30////////////////////////
                fpselect2 += " Select null as xuhao,  ";
                fpselect2 += "  null as pjdymc, ";
                fpselect2 += "  null as ssyt, ";
                fpselect2 += "  '合计' as xyjb, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else sum(zjclf)/sum(yqspl) end),2) As zjclf,  ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(zjrlf)/sum(yqspl) end),2) As zjrlf,  ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(zjdlf)/sum(yqspl) end),2) As zjdlf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(zjryf)/sum(yqspl) end),2) As zjryf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(qywzrf)/sum(yqspl) end),2) As qywzrf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(qywzrf_ryf),0)/sum(yqspl) end),2) As qywzrf_ryf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(jxzyf)/sum(yqspl) end),2) As jxzyf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(cjsjf)/sum(yqspl) end),2) As cjsjf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(cjsjf_ryf),0)/sum(yqspl) end),2) As cjsjf_ryf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(whxlf)/sum(yqspl) end),2) As whxlf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(whxlf_ryf),0)/sum(yqspl) end),2) As whxlf_ryf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(yqclf)/sum(yqspl) end),2) As yqclf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(yqclf_ryf),0)/sum(yqspl) end),2) As yqclf_ryf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(ysf)/sum(yqspl) end),2) As ysf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(ysf_ryf),0)/sum(yqspl) end),2) As ysf_ryf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(qtzjf)/sum(yqspl) end),2) As qtzjf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(ckglf)/sum(yqspl) end),2) As ckglf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else (nvl(Sum(zjryf),0)+nvl(sum(qywzrf_ryf),0)+nvl(sum(cjsjf_ryf),0)+nvl(sum(whxlf_ryf),0)+nvl(sum(yqclf_ryf),0)+ nvl(sum(ysf_ryf),0))/sum(yqspl) end),2) As ckglf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(zyyqcp)/sum(yqspl) end),2) As zyyqcp, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(yxczcb)/sum(yqspl) end),2) As yxczcb, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(czcb)/sum(yqspl) end),2) As czcb ";

                fpselect2 += " From dtstat_qksj sdy,dtxyjb_info xy ";
                fpselect2 += " Where sfpj = 1  ";
                fpselect2 += " and sdy.gsxyjb_1 = xy.jbid  ";
                fpselect2 += " and sdy.cyc_id = '" + cycid + "' ";

                ///////////////////////////////////
            }
            else if (typeid == "zyq")
            {
                fpselect += " Select 0 as xuhao,  ";
                fpselect += "  d.dep_name as pjdymc, ";
                fpselect += " sdy.ssyt as ssyt, ";

                fpselect += " round((case when sum(yqspl)=0 then 0 else sum(zjclf)/sum(yqspl) end),2) As zjclf,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjrlf)/sum(yqspl) end),2) As zjrlf,  ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjdlf)/sum(yqspl) end),2) As zjdlf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zjryf)/sum(yqspl) end),2) As zjryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qywzrf)/sum(yqspl) end),2) As qywzrf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(qywzrf_ryf),0)/sum(yqspl) end),2) As qywzrf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(jxzyf)/sum(yqspl) end),2) As jxzyf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(cjsjf)/sum(yqspl) end),2) As cjsjf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(cjsjf_ryf),0)/sum(yqspl) end),2) As cjsjf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(whxlf)/sum(yqspl) end),2) As whxlf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(whxlf_ryf),0)/sum(yqspl) end),2) As whxlf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(yqclf)/sum(yqspl) end),2) As yqclf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(yqclf_ryf),0)/sum(yqspl) end),2) As yqclf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ysf)/sum(yqspl) end),2) As ysf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(ysf_ryf),0)/sum(yqspl) end),2) As ysf_ryf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(qtzjf)/sum(yqspl) end),2) As qtzjf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(ckglf)/sum(yqspl) end),2) As ckglf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else (nvl(Sum(zjryf),0)+nvl(sum(qywzrf_ryf),0)+nvl(sum(cjsjf_ryf),0)+nvl(sum(whxlf_ryf),0)+nvl(sum(yqclf_ryf),0)+ nvl(sum(ysf_ryf),0))/sum(yqspl) end),2) As ckglf, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(zyyqcp)/sum(yqspl) end),2) As zyyqcp, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(yxczcb)/sum(yqspl) end),2) As yxczcb, ";
                fpselect += " round((case when sum(yqspl)=0 then 0 else Sum(czcb)/sum(yqspl) end),2) As czcb, ";
                fpselect += " sdy.zyqdm as zyqdm ";

                fpselect += " From view_dtstat_zyqsj sdy, department d  ";
                fpselect += " where   sdy.cyc_id = '" + cycid + "' ";
                fpselect += " and  sdy.cyc_id = d.parent_id and sdy.zyqdm = d.dep_id  ";
                fpselect += " group by sdy.zyqdm, d.dep_name,sdy.ssyt ";
                fpselect += " order by sdy.zyqdm ";

                ////////////////09.4.30/////////////////////
                fpselect2 += " Select null as xuhao,  ";
                fpselect2 += "  null as pjdymc, ";
                fpselect2 += " '合计' as ssyt, ";

                fpselect2 += " round((case when sum(yqspl)=0 then 0 else sum(zjclf)/sum(yqspl) end),2) As zjclf,  ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(zjrlf)/sum(yqspl) end),2) As zjrlf,  ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(zjdlf)/sum(yqspl) end),2) As zjdlf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(zjryf)/sum(yqspl) end),2) As zjryf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(qywzrf)/sum(yqspl) end),2) As qywzrf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(qywzrf_ryf),0)/sum(yqspl) end),2) As qywzrf_ryf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(jxzyf)/sum(yqspl) end),2) As jxzyf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(cjsjf)/sum(yqspl) end),2) As cjsjf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(cjsjf_ryf),0)/sum(yqspl) end),2) As cjsjf_ryf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(whxlf)/sum(yqspl) end),2) As whxlf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(whxlf_ryf),0)/sum(yqspl) end),2) As whxlf_ryf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(yqclf)/sum(yqspl) end),2) As yqclf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(yqclf_ryf),0)/sum(yqspl) end),2) As yqclf_ryf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(ysf)/sum(yqspl) end),2) As ysf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else nvl(Sum(ysf_ryf),0)/sum(yqspl) end),2) As ysf_ryf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(qtzjf)/sum(yqspl) end),2) As qtzjf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(ckglf)/sum(yqspl) end),2) As ckglf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else (nvl(Sum(zjryf),0)+nvl(sum(qywzrf_ryf),0)+nvl(sum(cjsjf_ryf),0)+nvl(sum(whxlf_ryf),0)+nvl(sum(yqclf_ryf),0)+ nvl(sum(ysf_ryf),0))/sum(yqspl) end),2) As ckglf, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(zyyqcp)/sum(yqspl) end),2) As zyyqcp, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(yxczcb)/sum(yqspl) end),2) As yxczcb, ";
                fpselect2 += " round((case when sum(yqspl)=0 then 0 else Sum(czcb)/sum(yqspl) end),2) As czcb, ";
                fpselect2 += " null as zyqdm ";

                fpselect2 += " From view_dtstat_zyqsj sdy, department d  ";
                fpselect2 += " where   sdy.cyc_id = '" + cycid + "' ";
                fpselect2 += " and  sdy.cyc_id = d.parent_id and sdy.zyqdm = d.dep_id  ";

                ////////////////////////////////////
            }
            try
            {
                connfp.Open();
                OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                OracleDataAdapter da2 = new OracleDataAdapter(fpselect2, connfp);
                DataSet fpset = new DataSet();
                da.Fill(fpset, "fpdata");
                da2.Fill(fpset, "fpdata2");
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
                    string path = "../../../static/excel/zuoyequ.xls";
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
                    /////////////09.4.30/////////////////
                    FpSpread1.Sheets[0].AddRows(hcount + rcount, 1);

                    for (int i = 0; i < 1; i++)
                    {
                        for (int j = 0; j < ccount - 1; j++)
                        {
                            FpSpread1.Sheets[0].Cells[hcount + rcount, j].Value = fpset.Tables["fpdata2"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[hcount + rcount, j].Font.Size = 9;

                        }
                    }

                    /////////////////////
                    //int k = 1;  //统计重复单元格
                    //int w = hcount;  //记录起始位置
                    //for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                    //{
                    //    if (FpSpread1.Sheets[0].Cells[i, 0].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, 0].Value.ToString())
                    //    {
                    //        k++;
                    //    }
                    //    else
                    //    {
                    //        FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
                    //        w = i;
                    //        k = 1;
                    //    }
                    //}
                    //FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);

                }
                else
                {
                    string path = Page.MapPath("../../../static/excel/dongtai.xls");
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表6-区块操作成本基础数据表");

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
                            FpSpread1.Sheets[0].Cells[i + hcount, 0].Value = i + 1;
                        }
                    }
                    /////////////09.4.30/////////////////
                    FpSpread1.Sheets[0].AddRows(hcount + rcount, 1);

                    for (int i = 0; i < 1; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            FpSpread1.Sheets[0].Cells[hcount + rcount, j].Value = fpset.Tables["fpdata2"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[hcount + rcount, j].Font.Size = 9;

                        }
                    }

                    /////////////////////////////
                    /////////////////////09.5.30合并单元格
                    ////for (int m = 1; m < 4; m++)  //列
                    //{
                    //    int m = 3;
                    //    int k = 1;  //统计重复单元格
                    //    int w = hcount;  //记录起始位置
                    //    for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                    //    {
                    //        if (FpSpread1.Sheets[0].Cells[i, m].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, m].Value.ToString())
                    //        {
                    //            k++;
                    //        }
                    //        else
                    //        {
                    //            if (k != 1)
                    //            {
                    //                FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                    //            }
                    //            w = i;
                    //            k = 1;
                    //        }
                    //    }
                    //    FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                    //}
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
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "作业区";
                ////////////////////////////////////////////////////////////////////
                /////////////////////////09.4.30更新//////////////////////////////////////
                //if (typeid == "yt")
                //{
                //    FpSpread1.Sheets[0].Cells[2, 1].Value = "采油厂";

                //    FpSpread1.Sheets[0].Cells[2, 0].Value = FpSpread1.Sheets[0].Cells[2, 1].Value;
                //    FpSpread1.ActiveSheetView.AddSpanCell(2, 0, 2, 4);

                //    FpSpread1.ActiveSheetView.Columns[1].Width = 0;
                //    FpSpread1.ActiveSheetView.Columns[2].Width = 0;
                //    FpSpread1.ActiveSheetView.Columns[3].Width = 0;

                //    for (int i = 4; i < FpSpread1.Sheets[0].RowCount; i++)
                //    {

                //        FpSpread1.Sheets[0].Cells[i, 0].Value = FpSpread1.Sheets[0].Cells[i, 3].Value;

                //        FpSpread1.ActiveSheetView.AddSpanCell(i, 0, 1, 4);
                //        FpSpread1.ActiveSheetView.Cells[1, 0].Column.Width = 74;
                //        FpSpread1.ActiveSheetView.Cells[i, 0].Column.Width = 74;
                //        //FpSpread1.ActiveSheetView.Columns[0].Width = 4;
                //        //Response.Write(FpSpread1.ActiveSheetView.Cells[4, 0].Value);

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
            FarpointGridChange.FarPointChange(FpSpread1, "dt_biao6.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;


        }


    }
}
