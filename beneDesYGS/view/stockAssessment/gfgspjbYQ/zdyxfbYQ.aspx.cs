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

namespace beneDesYGS.view.stockAssessment.gfgspjbYQ
{
    public partial class zdyxfbYQ : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //initSpread();
            string typeid = _getParam("targetType");
            initSpread(typeid);
        }

        protected void initSpread(string typeid)
        {

            string path = "../../../static/excel/jdnianduYQ.xls";
            path = Page.MapPath(path);
            switch (typeid)
            {
                case "yj":
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表11-油井最低运行费表"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    }
                    break;
                case "qj":
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表11-气井最低运行费表"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    }
                    break;
                default:
                    break;
            }
            //判断模板是否加载
            if (FpSpread1.Sheets[0].Rows.Count != 3)
            {
                Response.Write("<script>alert('加载模板有问题，请检查模板文件是否正确！')</script>");
            }
            else
            {
                if (isempty() == 0)
                { Response.Write("<script>alert('无数据')</script>"); }
                else
                { sj(typeid); }
            }
        }


        protected void sj(string typeid)
        {
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            string[] cyc_ids = _getParamArr("CYC");
            string as_cyc = cyc_ids[0];
            string as_cqc = cyc_ids[0];
            if (cyc_ids.Length > 1)
                as_cqc = cyc_ids[1];

            DataSet ds = new DataSet();
            var param_bny = new OracleParameter("as_bny", OracleType.VarChar);
            param_bny.Value = bny;
            var param_eny = new OracleParameter("as_eny", OracleType.VarChar);
            param_eny.Value = eny;
            var param_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc.Value = as_cyc;
            var param_cqc = new OracleParameter("as_cqc", OracleType.VarChar);
            param_cqc.Value = as_cqc;
            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;


            if (typeid == "yj")
            {

                ds = sql.GetDataSet("OWCBS_VIEW_DJZDYXFB.DJZDYXFB_YJ_PROC", CommandType.StoredProcedure,
                   new OracleParameter[] { param_bny, param_eny, param_cyc, param_out });

                FillDJSpread(ds);
            }
            else if (typeid == "qj")
            {

                ds = sql.GetDataSet("OWCBS_VIEW_DJZDYXFB.DJZDYXFB_QJ_PROC", CommandType.StoredProcedure,
                         new OracleParameter[] { param_bny, param_eny, param_cqc, param_out });
                FillDJSpread(ds);
            }
        }

        private void FillDJSpread(DataSet ds)
        {
            int rcount = ds.Tables[0].Rows.Count;
            int ccount = ds.Tables[0].Columns.Count;
            int hcount = 3;

            if (FpSpread1.Sheets[0].Rows.Count == hcount)
            {
                FpSpread1.Sheets[0].AddRows(hcount, rcount);
                for (int i = 0; i < rcount; i++)
                {
                    for (int j = 2; j < ccount; j++)
                    {
                        string value = ds.Tables[0].Rows[i][j].ToString();
                        if (string.IsNullOrEmpty(value))
                            value = "0";
                        FpSpread1.Sheets[0].Cells[i + hcount, j - 2].Value = value;
                        FpSpread1.Sheets[0].Cells[i + hcount, j - 2].HorizontalAlign = HorizontalAlign.Center;
                    }
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
                    FpSpread1.Sheets[0].Cells[w, 0].HorizontalAlign = HorizontalAlign.Center;
                    w = i;
                    k = 1;
                }
            }
            FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
            int width = 0;
            for (int Col = 0; Col < FpSpread1.Sheets[0].Columns.Count; Col++)
            {
                width += FpSpread1.Sheets[0].Columns[Col].Width;
            }

            FpSpread1.Width = Unit.Pixel(width + 300);
        }

        protected void initSpread()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/jdnianduQC.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表11-油井最低运行费表");

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
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            string list = _getParam("CYC");
            string typeid = _getParam("targetType");

            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["ygsConnectionString"].ConnectionString);

            string fpselect = " Select sdy.pjdyxyjb as pjdyxyjb,sdy.gsxyjb as gsxyjb, qks.xymc, gsx.xymc , ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjclf   )/Sum(trqspl) End),2) As zjclf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjrlf   )/Sum(trqspl) End),2) As zjrlf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjdlf   )/Sum(trqspl) End),2) As zjdlf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjryf   )/Sum(trqspl) End),2) As zjryf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qywzrf  )/Sum(trqspl) End),2) As qywzrf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(whxjxzyf   )/Sum(trqspl) End),2) As jxzyf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cyrcf   )/Sum(trqspl) End),2) As cyrcf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(yqclf   )/Sum(trqspl) End),2) As yqclf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(trqjhf  )/Sum(trqspl) End),2) As trqjhf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(lyf     )/Sum(trqspl) End),2) As ysf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zdyxf    )/Sum(trqspl) End),2) As czcb ";
            //fpselect += " ,round((Case When sum(dtl) = 0 Then 0 Else Sum(zdyxf_my )/Sum(dtl) End),2) As czcb_my ";
            fpselect += " From jdstat_djsj sdy,qkxyjb qks, gsxylb_info gsx ";
            fpselect += " where sdy.bny = " + bny + " and sdy.eny=" + eny + " and djisopen = 1  and pjdyxyjb <> 99 ";
            fpselect += " and sdy.pjdyxyjb = qks.xyjb and sdy.gsxyjb = gsx.xyjb  and sdy.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " Group By sdy.pjdyxyjb,sdy.gsxyjb,qks.xymc, gsx.xymc ";

            fpselect += " Union ";
            fpselect += " Select sdy.pjdyxyjb as pjdyxyjb,80000 As gsxyjb,qks.xymc, gsx.xymc, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjclf   )/Sum(trqspl) End),2) As zjclf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjrlf   )/Sum(trqspl) End),2) As zjrlf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjdlf   )/Sum(trqspl) End),2) As zjdlf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjryf   )/Sum(trqspl) End),2) As zjryf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qywzrf  )/Sum(trqspl) End),2) As qywzrf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(whxjxzyf   )/Sum(trqspl) End),2) As jxzyf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cyrcf   )/Sum(trqspl) End),2) As cyrcf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(yqclf   )/Sum(trqspl) End),2) As yqclf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(trqjhf  )/Sum(trqspl) End),2) As trqjhf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(lyf     )/Sum(trqspl) End),2) As ysf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zdyxf    )/Sum(trqspl) End),2) As czcb ";
            //fpselect += " ,round((Case When sum(dtl) = 0 Then 0 Else Sum(zdyxf_my )/Sum(dtl) End),2) As czcb_my ";
            fpselect += " From jdstat_djsj sdy,qkxyjb qks, gsxylb_info gsx ";
            fpselect += " where sdy.bny = " + bny + " and sdy.eny=" + eny + " and djisopen = 1  and pjdyxyjb <> 99 ";
            fpselect += " and sdy.pjdyxyjb = qks.xyjb   and gsx.xyjb = '80000'  and sdy.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " Group By sdy.pjdyxyjb,qks.xymc, gsx.xymc  ";

            fpselect += " union ";
            fpselect += " select 90000 as pjdyxyjb,sdy.gsxyjb as gsxyjb,qks.xymc,gsx.xymc, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjclf   )/Sum(trqspl) End),2) As zjclf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjrlf   )/Sum(trqspl) End),2) As zjrlf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjdlf   )/Sum(trqspl) End),2) As zjdlf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjryf   )/Sum(trqspl) End),2) As zjryf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qywzrf  )/Sum(trqspl) End),2) As qywzrf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(whxjxzyf   )/Sum(trqspl) End),2) As jxzyf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cyrcf   )/Sum(trqspl) End),2) As cyrcf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(yqclf   )/Sum(trqspl) End),2) As yqclf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(trqjhf  )/Sum(trqspl) End),2) As trqjhf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(lyf     )/Sum(trqspl) End),2) As ysf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zdyxf    )/Sum(trqspl) End),2) As czcb ";
            //fpselect += " ,round((Case When sum(dtl) = 0 Then 0 Else Sum(zdyxf_my )/Sum(dtl) End),2) As czcb_my ";
            fpselect += " From jdstat_djsj sdy,qkxyjb qks,gsxylb_info gsx ";
            fpselect += " where sdy.bny = " + bny + " and sdy.eny=" + eny + " and djisopen = 1  and pjdyxyjb <> 99 ";
            fpselect += " and sdy.gsxyjb = gsx .xyjb  and qks.xyjb = '90000'  and sdy.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " Group By sdy.gsxyjb,qks.xymc, gsx.xymc ";

            fpselect += " union ";
            fpselect += " select 90000 as pjdyxyjb, 80000 As gsxyjb,qks.xymc, gsx.xymc, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjclf   )/Sum(trqspl) End),2) As zjclf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjrlf   )/Sum(trqspl) End),2) As zjrlf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjdlf   )/Sum(trqspl) End),2) As zjdlf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjryf   )/Sum(trqspl) End),2) As zjryf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qywzrf  )/Sum(trqspl) End),2) As qywzrf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(whxjxzyf   )/Sum(trqspl) End),2) As jxzyf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cyrcf   )/Sum(trqspl) End),2) As cyrcf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(yqclf   )/Sum(trqspl) End),2) As yqclf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(trqjhf  )/Sum(trqspl) End),2) As trqjhf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(lyf     )/Sum(trqspl) End),2) As ysf, ";
            fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zdyxf    )/Sum(trqspl) End),2) As czcb ";
            //fpselect += " ,round((Case When sum(dtl) = 0 Then 0 Else Sum(zdyxf_my )/Sum(dtl) End),2) As czcb_my ";
            fpselect += " From jdstat_djsj sdy,qkxyjb qks, gsxylb_info gsx ";
            fpselect += " where sdy.bny = " + bny + " and sdy.eny=" + eny + " and djisopen = 1  and pjdyxyjb <> 99 ";
            fpselect += " and qks.xyjb = '90000' and gsx.xyjb = '80000'  and sdy.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " Group By qks.xymc, gsx.xymc ";
            //OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["ygsConnectionString"].ConnectionString);

            //string fpselect = " Select sdy.pjdyxyjb as pjdyxyjb,sdy.gsxyjb as gsxyjb, qks.xymc, gsx.xymc , ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjclf   )/Sum(trqspl) End),2) As zjclf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjrlf   )/Sum(trqspl) End),2) As zjrlf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjdlf   )/Sum(trqspl) End),2) As zjdlf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjryf   )/Sum(trqspl) End),2) As zjryf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qywzrf  )/Sum(trqspl) End),2) As qywzrf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(whxjxzyf   )/Sum(trqspl) End),2) As jxzyf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cyrcf   )/Sum(trqspl) End),2) As cyrcf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(yqclf   )/Sum(trqspl) End),2) As yqclf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(trqjhf  )/Sum(trqspl) End),2) As trqjhf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(lyf     )/Sum(trqspl) End),2) As ysf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zdyxf    )/Sum(trqspl) End),2) As czcb, ";
            //fpselect += " round((Case When sum(dtl) = 0 Then 0 Else Sum(zdyxf_my )/Sum(dtl) End),2) As czcb_my ";
            //fpselect += " From jdstat_djsj sdy,qkxyjb qks, gsxylb_info gsx ";
            //fpselect += " where sdy.bny = " + bny + " and sdy.eny=" + eny + " and djisopen = 1  and pjdyxyjb <> 99 ";
            //fpselect += " and sdy.pjdyxyjb = qks.xyjb and sdy.gsxyjb = gsx.xyjb ";
            //if (list == "quan")
            //{
            //    fpselect += " ";
            //}
            //else
            //{
            //    fpselect += " and sdy.dep_id = (select dep_id from department where dep_id =  '" + list + "') ";
            //}
            //fpselect += " Group By sdy.pjdyxyjb,sdy.gsxyjb,qks.xymc, gsx.xymc ";

            //fpselect += " Union ";
            //fpselect += " Select sdy.pjdyxyjb as pjdyxyjb,80000 As gsxyjb,qks.xymc, gsx.xymc, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjclf   )/Sum(trqspl) End),2) As zjclf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjrlf   )/Sum(trqspl) End),2) As zjrlf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjdlf   )/Sum(trqspl) End),2) As zjdlf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjryf   )/Sum(trqspl) End),2) As zjryf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qywzrf  )/Sum(trqspl) End),2) As qywzrf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(whxjxzyf   )/Sum(trqspl) End),2) As jxzyf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cyrcf   )/Sum(trqspl) End),2) As cyrcf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(yqclf   )/Sum(trqspl) End),2) As yqclf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(trqjhf  )/Sum(trqspl) End),2) As trqjhf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(lyf     )/Sum(trqspl) End),2) As ysf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zdyxf    )/Sum(trqspl) End),2) As czcb, ";
            //fpselect += " round((Case When sum(dtl) = 0 Then 0 Else Sum(zdyxf_my )/Sum(dtl) End),2) As czcb_my ";
            //fpselect += " From jdstat_djsj sdy,qkxyjb qks, gsxylb_info gsx ";
            //fpselect += " where sdy.bny = " + bny + " and sdy.eny=" + eny + " and djisopen = 1  and pjdyxyjb <> 99 ";
            //fpselect += " and sdy.pjdyxyjb = qks.xyjb   and gsx.xyjb = '80000' ";
            //if (list == "quan")
            //{
            //    fpselect += " ";
            //}
            //else
            //{
            //    fpselect += " and sdy.dep_id = (select dep_id from department where dep_id =  '" + list + "') ";
            //}
            //fpselect += " Group By sdy.pjdyxyjb,qks.xymc, gsx.xymc  ";

            //fpselect += " union ";
            //fpselect += " select 90000 as pjdyxyjb,sdy.gsxyjb as gsxyjb,qks.xymc,gsx.xymc, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjclf   )/Sum(trqspl) End),2) As zjclf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjrlf   )/Sum(trqspl) End),2) As zjrlf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjdlf   )/Sum(trqspl) End),2) As zjdlf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjryf   )/Sum(trqspl) End),2) As zjryf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qywzrf  )/Sum(trqspl) End),2) As qywzrf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(whxjxzyf   )/Sum(trqspl) End),2) As jxzyf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cyrcf   )/Sum(trqspl) End),2) As cyrcf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(yqclf   )/Sum(trqspl) End),2) As yqclf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(trqjhf  )/Sum(trqspl) End),2) As trqjhf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(lyf     )/Sum(trqspl) End),2) As ysf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zdyxf    )/Sum(trqspl) End),2) As czcb, ";
            //fpselect += " round((Case When sum(dtl) = 0 Then 0 Else Sum(zdyxf_my )/Sum(dtl) End),2) As czcb_my ";
            //fpselect += " From jdstat_djsj sdy,qkxyjb qks,gsxylb_info gsx ";
            //fpselect += " where sdy.bny = " + bny + " and sdy.eny=" + eny + " and djisopen = 1  and pjdyxyjb <> 99 ";
            //fpselect += " and sdy.gsxyjb = gsx .xyjb  and qks.xyjb = '90000 ";
            //if (list == "quan")
            //{
            //    fpselect += " ";
            //}
            //else
            //{
            //    fpselect += " and sdy.dep_id = (select dep_id from department where dep_id =  '" + list + "') ";
            //}
            //fpselect += " Group By sdy.gsxyjb,qks.xymc, gsx.xymc ";

            //fpselect += " union ";
            //fpselect += " select 90000 as pjdyxyjb, 80000 As gsxyjb,qks.xymc, gsx.xymc, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjclf   )/Sum(trqspl) End),2) As zjclf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjrlf   )/Sum(trqspl) End),2) As zjrlf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjdlf   )/Sum(trqspl) End),2) As zjdlf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjryf   )/Sum(trqspl) End),2) As zjryf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qywzrf  )/Sum(trqspl) End),2) As qywzrf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(whxjxzyf   )/Sum(trqspl) End),2) As jxzyf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cyrcf   )/Sum(trqspl) End),2) As cyrcf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(yqclf   )/Sum(trqspl) End),2) As yqclf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(trqjhf  )/Sum(trqspl) End),2) As trqjhf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(lyf     )/Sum(trqspl) End),4) As ysf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zdyxf    )/Sum(trqspl) End),4) As czcb, ";
            //fpselect += " round((Case When sum(dtl) = 0 Then 0 Else Sum(zdyxf_my )/Sum(dtl) End),4) As czcb_my ";
            //fpselect += " From jdstat_djsj sdy,qkxyjb qks, gsxylb_info gsx ";
            //fpselect += " where sdy.bny = " + bny + " and sdy.eny=" + eny + " and djisopen = 1  and pjdyxyjb <> 99 ";
            //fpselect += " and qks.xyjb = '90000' and gsx.xyjb = '80000' ";
            //if (list == "quan")
            //{
            //    fpselect += " ";
            //}
            //else
            //{
            //    fpselect += " and sdy.dep_id = (select dep_id from department where dep_id =  '" + list + "') ";
            //}
            //fpselect += " Group By qks.xymc, gsx.xymc ";

            try
            {
                connfp.Open();

                OracleCommand myComm = new OracleCommand(fpselect, connfp);
                OracleDataReader myReader = myComm.ExecuteReader();
                //DataTable Fptable用来输出数据
                DataTable Fptable = new DataTable("fpdata");
                Fptable.Columns.Add("pjdyxyjb", typeof(string));
                Fptable.Columns.Add("gsxyjb", typeof(System.String));
                Fptable.Columns.Add("zjclf", typeof(float));
                Fptable.Columns.Add("zjrlf", typeof(float));
                Fptable.Columns.Add("zjdlf", typeof(float));
                Fptable.Columns.Add("zjryf", typeof(float));
                Fptable.Columns.Add("qywzrf", typeof(float));
                Fptable.Columns.Add("jxzyf", typeof(float));
                Fptable.Columns.Add("cyrcf", typeof(float));
                Fptable.Columns.Add("yqclf", typeof(float));
                Fptable.Columns.Add("trqjhf", typeof(float));
                Fptable.Columns.Add("ysf", typeof(float));
                Fptable.Columns.Add("czcb", typeof(float));
                //Fptable.Columns.Add("czcb_my", typeof(float));
                DataRow Fprow;
                while (myReader.Read())
                {
                    Fprow = Fptable.NewRow();

                    Fprow[0] = myReader[2];
                    Fprow[1] = myReader[3];
                    Fprow[2] = myReader.GetValue(4);
                    Fprow[3] = myReader[5];
                    Fprow[4] = myReader.GetValue(6);
                    Fprow[5] = myReader.GetValue(7);
                    Fprow[6] = myReader.GetValue(8);
                    Fprow[7] = myReader.GetValue(9);
                    Fprow[8] = myReader[10];
                    Fprow[9] = myReader.GetValue(11);
                    Fprow[10] = myReader.GetValue(12);
                    Fprow[11] = myReader.GetValue(13);
                    Fprow[12] = myReader.GetValue(14);
                    //Fprow[13] = myReader.GetValue(15);
                    Fptable.Rows.Add(Fprow);
                }
                myReader.Close();
                myComm.Clone();
                ////把第一,二列重复的项去掉
                //string tmp1 = Fptable.Rows[0][0].ToString();
                //string tmp2 = Fptable.Rows[0][1].ToString();
                //for (int j = 1; j < Fptable.Rows.Count; j++)
                //{
                //    if (Fptable.Rows[j][0].ToString() == tmp1)
                //        Fptable.Rows[j][0] = "";
                //    else
                //        tmp1 = Fptable.Rows[j][0].ToString();
                //    if (Fptable.Rows[j][1].ToString() == tmp2)
                //        Fptable.Rows[j][1] = "";
                //    else
                //        tmp2 = Fptable.Rows[j][1].ToString();
                //} //把第一,二列重复的项去掉

                DataSet fpset = new DataSet();
                fpset.Tables.Add(Fptable);

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
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            if (isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
                            }
                            else
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            }
                        }
                    }
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
                else//不为空
                {
                    string path = Page.MapPath("../../excel/表10.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, 0);
                    FpSpread1.Sheets[0].RowHeader.Visible = false;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " 油井最低运行费表 ";

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            if (isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
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

            FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao11.xls");


        }

    }
}
