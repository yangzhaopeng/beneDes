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
    public partial class gcbcltjbYQ : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //initSpread();
            string typeid = _getParam("targetType");
            initSpread(typeid);

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }
        protected void initSpread(string typeid)
        {

            string path = "../../../static/excel/jdnianduYQ.xls";
            path = Page.MapPath(path);
            switch (typeid)
            {
                case "qj":
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表10-高成本产量统计表"))
                    {
                        if (FpSpread1.Sheets[0].Rows.Count != 5)
                        {
                            Response.Write("<script>alert('加载模板有问题，请检查模板文件是否正确！')</script>");
                        }
                        else
                        {
                            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                            if (isempty() == 0)
                            { Response.Write("<script>alert('无数据')</script>"); }
                            else
                            { sj(typeid); }
                        }
                    }
                    break;
                default:
                    break;
            }
            int width = 0;
            for (int Col = 0; Col < FpSpread1.Sheets[0].Columns.Count; Col++)
            {
                width += FpSpread1.Sheets[0].Columns[Col].Width;
            }

            FpSpread1.Width = Unit.Pixel(width + 800);
        }
        protected void sj(string typeid)
        {
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");



            eny = bny.Substring(0, 4);
            bny = (int.Parse(eny) - 1).ToString();

            string tgcb = _getParam("tgcb");
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
            var param_tgcb = new OracleParameter("as_tgcb", OracleType.VarChar);
            param_tgcb.Value = tgcb;
            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;
            if (typeid == "qj")
            {
                OracleParameter[] paras = new OracleParameter[] { param_bny, param_eny, param_cyc, param_cqc, param_tgcb, param_out };
                ds = sql.GetDataSet("OWCBS_VIEW_GCBCLTJB.GCBCLTJB_QK_PROC", CommandType.StoredProcedure, paras);
                FillDJSpread(ds, tgcb);
            }

        }

        private void FillDJSpread(DataSet ds, string tgcb)
        {
            int rcount = ds.Tables[0].Rows.Count;
            int ccount = ds.Tables[0].Columns.Count;
            int hcount = 5;

            if (FpSpread1.Sheets[0].Rows.Count == hcount)
            {
                FpSpread1.Sheets[0].AddRows(hcount, rcount + 1);
                FpSpread1.Sheets[0].Cells[5, 0].Value = "大于" + tgcb;
                FpSpread1.Sheets[0].Cells[5, 1].Value = "小于" + tgcb;
                FpSpread1.Sheets[0].Cells[6, 0].Value = "大于" + tgcb;
                FpSpread1.Sheets[0].Cells[6, 1].Value = "大于" + tgcb;

                for (int i = 0; i < rcount; i++)
                {
                    for (int j = 0; j < ccount; j++)
                    {
                        string value = ds.Tables[0].Rows[i][j].ToString();
                        if (string.IsNullOrEmpty(value))
                            value = "0";
                        FpSpread1.Sheets[0].Cells[i + hcount, j + 2].Value = value;

                    }
                }
            }

            ////合并单元格
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

        protected void initSpread()
        {


            string path = Page.MapPath("~/static/excel/jdnianduYQ.xls");
            this.FpSpread1.Sheets[0].OpenExcel(path, "表10-高成本产量统计表");

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

            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //string list = _getParam("YCLX");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            string list = _getParam("CYC");
            string typeid = _getParam("targetType");

            string cycid = Session["cyc_id"].ToString();
            //  SqlHelper conn = new SqlHelper();
            //    OracleConnection connfp = conn.GetConn();
            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["ygsConnectionString"].ConnectionString);
            //////////////////搜出表中的特高成本井
            connfp.Open();
            string tgse = "select num from csdy_info where name = '特高成本井' and ny = '" + Session["month"].ToString() + "'";
            OracleCommand tgcom = new OracleCommand(tgse, connfp);
            OracleDataReader tgr = tgcom.ExecuteReader();
            Int64 tgcb = 0;
            if (tgr.Read())
            {
                Int64.TryParse(tgr[0].ToString(), out tgcb);
            }
            connfp.Close();
            /////////////////////////////////////
            string fpselect = "";
            if (typeid == "qk")
            {
                fpselect += " select  xy.xymc as xymc,sdy.qk as qk, ";
                fpselect += " nvl(round(sum(sdy.djisopen),4),0) as js,";
                fpselect += " nvl(round(sum(sdy.hscql/100000000),4),0) as hscql, ";
                fpselect += " nvl(round(sum(sdy.hscyl),2),0) as hscyl, ";
                fpselect += " nvl(round(sum(sdy.trqspl/100000000),4),0) as trqspl,";
                fpselect += " nvl(round(sum(sdy.yyspl),2),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb,";
                fpselect += " nvl(case sum(sdy.trqspl) when 0 then 0 else round(sum(sdy.czcb)/sum(sdy.trqspl)*1000,4) end,0) as dwczcb";

                /*
                //fpselect += " select * from view_dtbiao6_1  ";
                fpselect += " select  xy.xymc as xymc,sdy.qk as qk, ";
                fpselect += " nvl(round(sum(sdy.djisopen),4),0) as js,";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl, ";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                //fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                //fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb";
                
                fpselect += " ,nvl(tem1.js,0) as js1, ";
                fpselect += " nvl(tem1.hscyl,0) as hscyl1, ";
                fpselect += " nvl(tem1.yyspl,0) as yyspl1, ";
                //fpselect += " nvl(tem1.zjyxczcb,0) as zjyxczcb1,";
                //fpselect += " nvl(tem1.yxczcb,0) as yxczcb1,";
                fpselect += " nvl(tem1.czcb,0) as czcb1,";

                fpselect += " nvl(tem2.js,0)  as js2, ";
                fpselect += " nvl(tem2.hscyl,0)  as hscyl2, ";
                fpselect += " nvl(tem2.yyspl,0)  as yyspl2, ";
                //fpselect += " nvl(tem2.zjyxczcb,0)  as zjyxczcb2, ";
                //fpselect += " nvl(tem2.yxczcb,0)  as yxczcb2, ";
                fpselect += " nvl(tem2.czcb,0)  as czcb2, ";

                fpselect += " nvl(tem3.js,0)  as js3,";
                fpselect += " nvl(tem3.hscyl,0)  as hscyl3,";
                fpselect += " nvl(tem3.yyspl,0)  as yyspl3, ";
                //fpselect += " nvl(tem3.zjyxczcb,0)  as zjyxczcb3, ";
                //fpselect += " nvl(tem3.yxczcb,0)  as yxczcb3, ";
                fpselect += " nvl(tem3.czcb,0)  as czcb3, ";

                fpselect += " nvl(tem4.js,0)  as js4, ";
                fpselect += " nvl(tem4.hscyl,0)  as hscyl4, ";
                fpselect += " nvl(tem4.yyspl,0)  as yyspl4, ";
                //fpselect += " nvl(tem4.zjyxczcb,0)  as zjyxczcb4, ";
                //fpselect += " nvl(tem4.yxczcb,0)  as yxczcb4, ";
                fpselect += " nvl(tem4.czcb,0)  as czcb4,";

                fpselect += " nvl(tem5.js,0)  as js5, ";
                fpselect += " nvl(tem5.hscyl,0)  as hscyl5, ";
                fpselect += " nvl(tem5.yyspl,0)  as yyspl5, ";
                //fpselect += " nvl(tem4.zjyxczcb,0)  as zjyxczcb4, ";
                //fpselect += " nvl(tem4.yxczcb,0)  as yxczcb4, ";
                fpselect += " nvl(tem5.czcb,0)  as czcb5";
                  */

                fpselect += " from  jdstat_djsj sdy, qkxyjb xy, jdstat_qksj sdyqk, ";
                fpselect += " (";
                fpselect += " select  sdy.qk as qk, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                //fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                //fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from jdstat_djsj sdy ";
                fpselect += " where sdy.gsxyjb = '1' and sdy.cyc_id = '" + cycid + "' and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1') where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.qk ";
                fpselect += " ) tem1, ";
                fpselect += " (";
                fpselect += " select  sdy.qk as qk, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                //fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                //fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from jdstat_djsj sdy ";
                fpselect += " where sdy.gsxyjb = '2' and sdy.cyc_id = '" + cycid + "' and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1') where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.qk ";
                fpselect += " ) tem2, ";
                fpselect += " (";
                fpselect += " select  sdy.qk as qk, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                //fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                //fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from jdstat_djsj sdy ";
                fpselect += " where sdy.gsxyjb = '3' and sdy.cyc_id = '" + cycid + "' and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1') where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.qk ";
                fpselect += " ) tem3, ";
                fpselect += " (";
                fpselect += " select  sdy.qk as qk, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                //fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                //fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from jdstat_djsj sdy ";
                fpselect += " where sdy.gsxyjb = '4' and sdy.cyc_id = '" + cycid + "' and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1') where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.qk ";
                fpselect += " ) tem4, ";
                fpselect += " (";
                fpselect += " select  sdy.qk as qk, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                //fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                //fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from jdstat_djsj sdy ";
                fpselect += " where sdy.gsxyjb = '5' and sdy.cyc_id = '" + cycid + "' and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1') where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.qk ";
                fpselect += " ) tem5 ";
                fpselect += " where";
                fpselect += "     sdyqk.gsxyjb = xy.xyjb  ";
                fpselect += " and sdy.qk =  tem1.qk(+) ";
                fpselect += " and sdy.qk =  tem2.qk(+) ";
                fpselect += " and sdy.qk =  tem3.qk(+)  ";
                fpselect += " and sdy.qk =  tem4.qk(+)  ";
                fpselect += " and sdy.qk =  tem5.qk(+)  ";
                fpselect += " and sdy.qk = sdyqk.mc";
                fpselect += " and sdy.cyc_id = '" + cycid + "'";
                fpselect += " and sdy.djisopen = '1' and sdy.jh in ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1') where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";

                fpselect += " group by sdy.qk,xy.xymc, ";
                fpselect += " tem1.js,tem1.hscyl,tem1.yyspl,tem1.czcb, ";
                fpselect += " tem2.js,tem2.hscyl,tem2.yyspl,tem2.czcb, ";
                fpselect += " tem3.js,tem3.hscyl,tem3.yyspl,tem3.czcb, ";
                fpselect += " tem4.js,tem4.hscyl,tem4.yyspl,tem4.czcb,  ";
                fpselect += " tem5.js,tem5.hscyl,tem5.yyspl,tem5.czcb  ";

            }
            else if (typeid == "pjdy")
            {

                //fpselect += " select  xy.xymc as xymc,sdy.qk as qk, ";//@yzp注
                fpselect += " select  xy.xymc as xymc,sdy.pjdy as qk, ";
                fpselect += " nvl(round(sum(sdy.djisopen),4),0) as js,";
                fpselect += " nvl(round(sum(sdy.hscql/100000000),4),0) as hscql, ";
                fpselect += " nvl(round(sum(sdy.hscyl),4),0) as hscyl, ";
                fpselect += " nvl(round(sum(sdy.trqspl/100000000),4),0) as trqspl,";
                fpselect += " nvl(round(sum(sdy.yyspl),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb,";
                fpselect += " nvl(case sum(sdy.trqspl) when 0 then 0 else round(sum(sdy.czcb)/sum(sdy.trqspl)*1000,4) end,0) as dwczcb";

                /*
                //fpselect += " select * from view_dtbiao6  ";
                fpselect += " select  xy.xymc as xymc,sdy.pjdy as pjdy, ";
                fpselect += " nvl(round(sum(sdy.djisopen),4),0) as js,";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl, ";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                //fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                //fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb";

               
                fpselect += " ,nvl(tem1.js,0) as js1, ";
                fpselect += " nvl(tem1.hscyl,0) as hscyl1, ";
                fpselect += " nvl(tem1.yyspl,0) as yyspl1, ";
                //fpselect += " nvl(tem1.zjyxczcb,0) as zjyxczcb1,";
                //fpselect += " nvl(tem1.yxczcb,0) as yxczcb1,";
                fpselect += " nvl(tem1.czcb,0) as czcb1,";

                fpselect += " nvl(tem2.js,0)  as js2, ";
                fpselect += " nvl(tem2.hscyl,0)  as hscyl2, ";
                fpselect += " nvl(tem2.yyspl,0)  as yyspl2, ";
                //fpselect += " nvl(tem2.zjyxczcb,0)  as zjyxczcb2, ";
                //fpselect += " nvl(tem2.yxczcb,0)  as yxczcb2, ";
                fpselect += " nvl(tem2.czcb,0)  as czcb2, ";

                fpselect += " nvl(tem3.js,0)  as js3,";
                fpselect += " nvl(tem3.hscyl,0)  as hscyl3,";
                fpselect += " nvl(tem3.yyspl,0)  as yyspl3, ";
                //fpselect += " nvl(tem3.zjyxczcb,0)  as zjyxczcb3, ";
                //fpselect += " nvl(tem3.yxczcb,0)  as yxczcb3, ";
                fpselect += " nvl(tem3.czcb,0)  as czcb3, ";

                fpselect += " nvl(tem4.js,0)  as js4, ";
                fpselect += " nvl(tem4.hscyl,0)  as hscyl4, ";
                fpselect += " nvl(tem4.yyspl,0)  as yyspl4, ";
                //fpselect += " nvl(tem4.zjyxczcb,0)  as zjyxczcb4, ";
                //fpselect += " nvl(tem4.yxczcb,0)  as yxczcb4, ";
                fpselect += " nvl(tem4.czcb,0)  as czcb4,";

                fpselect += " nvl(tem5.js,0)  as js5, ";
                fpselect += " nvl(tem5.hscyl,0)  as hscyl5, ";
                fpselect += " nvl(tem5.yyspl,0)  as yyspl5, ";
                //fpselect += " nvl(tem4.zjyxczcb,0)  as zjyxczcb4, ";
                //fpselect += " nvl(tem4.yxczcb,0)  as yxczcb4, ";
                fpselect += " nvl(tem5.czcb,0)  as czcb45 "; 
                 */

                //fpselect += " sdy.gsxyjb ";
                fpselect += " from  jdstat_djsj sdy, qkxyjb xy, jdstat_pjdysj sdypjdy, ";
                fpselect += " (";
                fpselect += " select  sdy.pjdy as pjdy, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                //fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                //fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from jdstat_djsj sdy ";
                fpselect += " where sdy.gsxyjb = '1' and sdy.cyc_id = '" + cycid + "' and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1') where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.pjdy ";
                fpselect += " ) tem1, ";
                fpselect += " (";
                fpselect += " select  sdy.pjdy as pjdy, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                //fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                //fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from jdstat_djsj sdy ";
                fpselect += " where sdy.gsxyjb = '2' and sdy.cyc_id = '" + cycid + "' and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1') where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.pjdy ";
                fpselect += " ) tem2, ";
                fpselect += " (";
                fpselect += " select  sdy.pjdy as pjdy, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                //fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                //fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from jdstat_djsj sdy ";
                fpselect += " where sdy.gsxyjb = '3' and sdy.cyc_id = '" + cycid + "' and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1') where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.pjdy ";
                fpselect += " ) tem3, ";
                fpselect += " (";
                fpselect += " select  sdy.pjdy as pjdy, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                //fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                //fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from jdstat_djsj sdy ";
                fpselect += " where sdy.gsxyjb = '4' and sdy.cyc_id = '" + cycid + "' and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1') where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.pjdy ";
                fpselect += " ) tem4, ";
                fpselect += " (";
                fpselect += " select  sdy.pjdy as pjdy, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                //fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                //fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from jdstat_djsj sdy ";
                fpselect += " where sdy.gsxyjb = '5' and sdy.cyc_id = '" + cycid + "' and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1') where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.pjdy ";
                fpselect += " ) tem5 ";
                fpselect += " where";
                fpselect += "     sdypjdy.gsxyjb = xy.xyjb  ";
                fpselect += " and sdy.pjdy =  tem1.pjdy(+) ";
                fpselect += " and sdy.pjdy =  tem2.pjdy(+) ";
                fpselect += " and sdy.pjdy =  tem3.pjdy(+)  ";
                fpselect += " and sdy.pjdy =  tem4.pjdy(+)  ";
                fpselect += " and sdy.pjdy =  tem5.pjdy(+)  ";
                fpselect += " and sdy.pjdy = sdypjdy.mc";
                fpselect += " and sdy.cyc_id = '" + cycid + "'";
                fpselect += " and sdy.djisopen = '1' and sdy.jh in ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1') where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";

                fpselect += " group by sdy.pjdy,xy.xymc, ";
                fpselect += " tem1.js,tem1.hscyl,tem1.yyspl,tem1.czcb, ";
                fpselect += " tem2.js,tem2.hscyl,tem2.yyspl,tem2.czcb, ";
                fpselect += " tem3.js,tem3.hscyl,tem3.yyspl,tem3.czcb, ";
                fpselect += " tem4.js,tem4.hscyl,tem4.yyspl,tem4.czcb,  ";
                fpselect += " tem5.js,tem5.hscyl,tem5.yyspl,tem5.czcb,xy.xyjb ";
                fpselect += " order by xy.xyjb ";

            }
            else if (typeid == "zyq")
            {
                //fpselect += " select  xy.xymc as xymc,sdy.qk as qk, "; //@yzp注
                fpselect += " select  xy.xymc as xymc,sdy.zyq as qk, ";
                fpselect += " nvl(round(sum(sdy.djisopen),4),0) as js,";
                fpselect += " nvl(round(sum(sdy.hscql/100000000),4),0) as hscql, ";
                fpselect += " nvl(round(sum(sdy.hscyl),4),0) as hscyl, ";
                fpselect += " nvl(round(sum(sdy.trqspl/100000000),4),0) as trqspl,";
                fpselect += " nvl(round(sum(sdy.yyspl),4),0) as yyspl,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb,";
                fpselect += " nvl(case sum(sdy.trqspl) when 0 then 0 else round(sum(sdy.czcb)/sum(sdy.trqspl)*1000,4) end,0) as dwczcb";

                /*
                fpselect += " select  d.dep_name as qk, ";
                fpselect += " nvl(round(sum(sdy.djisopen),4),0) as js,";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl, ";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                //fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                //fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb,";

                fpselect += " sum(nvl(tem1.js,0)) as js1, ";
                fpselect += " sum(nvl(tem1.hscyl,0)) as hscyl1, ";
                fpselect += " sum(nvl(tem1.yyspl,0)) as yyspl1, ";
                //fpselect += " sum(nvl(tem1.zjyxczcb,0)) as zjyxczcb1,";
                //fpselect += " sum(nvl(tem1.yxczcb,0)) as yxczcb1,";
                fpselect += " sum(nvl(tem1.czcb,0)) as czcb1,";

                fpselect += " sum(nvl(tem2.js,0))  as js2, ";
                fpselect += " sum(nvl(tem2.hscyl,0))  as hscyl2, ";
                fpselect += " sum(nvl(tem2.yyspl,0))  as yyspl2, ";
                //fpselect += " sum(nvl(tem2.zjyxczcb,0))  as zjyxczcb2, ";
                //fpselect += " sum(nvl(tem2.yxczcb,0))  as yxczcb2, ";
                fpselect += " sum(nvl(tem2.czcb,0))  as czcb2, ";

                fpselect += " sum(nvl(tem3.js,0))  as js3,";
                fpselect += " sum(nvl(tem3.hscyl,0))  as hscyl3,";
                fpselect += " sum(nvl(tem3.yyspl,0))  as yyspl3, ";
                //fpselect += " sum(nvl(tem3.zjyxczcb,0))  as zjyxczcb3, ";
                //fpselect += " sum(nvl(tem3.yxczcb,0))  as yxczcb3, ";
                fpselect += " sum(nvl(tem3.czcb,0))  as czcb3, ";

                fpselect += " sum(nvl(tem4.js,0))  as js4, ";
                fpselect += " sum(nvl(tem4.hscyl,0))  as hscyl4, ";
                fpselect += " sum(nvl(tem4.yyspl,0))  as yyspl4, ";
                //fpselect += " sum(nvl(tem4.zjyxczcb,0))  as zjyxczcb4, ";
                //fpselect += " sum(nvl(tem4.yxczcb,0))  as yxczcb4, ";
                fpselect += " sum(nvl(tem4.czcb,0))  as czcb4, ";


                fpselect += " sum(nvl(tem5.js,0))  as js5, ";
                fpselect += " sum(nvl(tem5.hscyl,0))  as hscyl5, ";
                fpselect += " sum(nvl(tem5.yyspl,0))  as yyspl5, ";
                //fpselect += " sum(nvl(tem5.zjyxczcb,0))  as zjyxczcb5, ";
                //fpselect += " sum(nvl(tem4.yxczcb,0))  as yxczcb4, ";
                fpselect += " sum(nvl(tem5.czcb,0))  as czcb5, ";
                fpselect += " sdy.zyqdm as zyq ";
                */

                fpselect += " from  view_jdstat_zyqsj sdy, department d, ";
                fpselect += " (";
                fpselect += " select  sdy.dj_id as dj_id, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                //fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                //fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from view_jdstat_zyqsj sdy ";
                fpselect += " where sdy.gsxyjb = '1' and sdy.cyc_id = '" + cycid + "' and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl end),0) as dwcb from view_jdstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1') where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.dj_id ";
                fpselect += " ) tem1, ";
                fpselect += " (";
                fpselect += " select  sdy.dj_id as dj_id, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                //fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                // fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from view_jdstat_zyqsj sdy ";
                fpselect += " where sdy.gsxyjb = '2' and sdy.cyc_id = '" + cycid + "' and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl end),0) as dwcb from view_jdstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1') where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.dj_id ";
                fpselect += " ) tem2, ";
                fpselect += " (";
                fpselect += " select  sdy.dj_id as dj_id, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                //fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                //fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from view_jdstat_zyqsj sdy ";
                fpselect += " where sdy.gsxyjb = '3' and sdy.cyc_id = '" + cycid + "' and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl end),0) as dwcb from view_jdstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1') where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.dj_id ";
                fpselect += " ) tem3, ";
                fpselect += " (";
                fpselect += " select  sdy.dj_id as dj_id, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                //fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                //fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from view_jdstat_zyqsj sdy ";
                fpselect += " where sdy.gsxyjb = '4' and sdy.cyc_id = '" + cycid + "' and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl end),0) as dwcb from view_jdstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1') where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.dj_id ";
                fpselect += " ) tem4, ";
                fpselect += " (";
                fpselect += " select  sdy.dj_id as dj_id, ";
                fpselect += " nvl(sum(sdy.djisopen),0) as js, ";
                fpselect += " nvl(round(sum(sdy.hscyl/10000),4),0) as hscyl,";
                fpselect += " nvl(round(sum(sdy.yyspl/10000),4),0) as yyspl,";
                //fpselect += " nvl(round(sum(sdy.zjyxczcb/10000),4),0) as zjyxczcb,";
                //fpselect += " nvl(round(sum(sdy.yxczcb/10000),4),0) as yxczcb,";
                fpselect += " nvl(round(sum(sdy.czcb/10000),4),0) as czcb ";
                fpselect += " from view_jdstat_zyqsj sdy ";
                fpselect += " where sdy.gsxyjb = '5' and sdy.cyc_id = '" + cycid + "' and sdy.jh in  ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl end),0) as dwcb from view_jdstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1') where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";
                fpselect += " group by sdy.dj_id ";
                fpselect += " ) tem5 ";

                fpselect += " where";
                fpselect += "  sdy.cyc_id = d.parent_id and sdy.zyqdm = d.dep_id  ";
                fpselect += " and sdy.dj_id =  tem1.dj_id(+) ";
                fpselect += " and sdy.dj_id =  tem2.dj_id(+) ";
                fpselect += " and sdy.dj_id =  tem3.dj_id(+)  ";
                fpselect += " and sdy.dj_id =  tem4.dj_id(+)  ";
                fpselect += " and sdy.dj_id =  tem5.dj_id(+)  ";
                fpselect += " and sdy.cyc_id = '" + cycid + "'";
                fpselect += "  ";
                fpselect += " and sdy.djisopen = '1' and sdy.jh in ";
                fpselect += " (";
                fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl end),0) as dwcb from view_jdstat_zyqsj sdy where sdy.cyc_id = '" + cycid + "'and sdy.djisopen = '1') where dwcb > '" + tgcb + "' ";
                fpselect += " ) ";

                fpselect += " group by sdy.zyqdm , d.dep_name ";
                fpselect += " order by sdy.zyqdm ";

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
                        //  dr[0] = "";
                        // dr[1] = "合计";
                        //初始化各列
                        for (int i = 1; i < dt.Columns.Count; i++)
                        {
                            dr[i] = 0;
                        }
                        //计算
                        for (int k = 0; k < dt.Rows.Count; k++)//循环计算各行
                        {

                            for (int n = 1; n < dt.Columns.Count - 1; n++)//循环计算各列
                            {
                                dr[n] = double.Parse(dr[n].ToString()) + double.Parse(dt.Rows[k][n].ToString());
                            }
                        }
                        //计算精度
                        for (int m = 1; m < dt.Columns.Count - 1; m++)
                        {
                            dr[m] = Math.Round(double.Parse(dr[m].ToString()), 4);
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
                        for (int k = 0; k < dt.Rows.Count; k++)//循环计算各行
                        {

                            for (int n = 2; n < dt.Columns.Count; n++)//循环计算各列
                            {
                                dr[n] = double.Parse(dr[n].ToString()) + double.Parse(dt.Rows[k][n].ToString());
                            }
                        }
                        //计算精度
                        for (int m = 2; m < dt.Columns.Count; m++)
                        {
                            dr[m] = Math.Round(double.Parse(dr[m].ToString()), 4);
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
                    string path = Page.MapPath("~/static/excel/jdnianduQC.xls");
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表10-高成本统计表");

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
                            if (j != 2 && j != 6 && j != 10 && j != 14 && j != 18 && j != 22 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString() != "0")
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
                    string path = Page.MapPath("~/static/excel/jdnianduQC.xls");
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表10-高成本统计表");

                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;


                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            if (j != 2 && j != 6 && j != 10 && j != 14 && j != 18 && j != 22 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString() != "0")
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

                /////////////////////////09.4.29更新//////////////////////////////////////
                if (typeid == "yt")
                {
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "单井";
                }
                else if (typeid == "pjdy")
                {
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "评价单元";
                }
                else if (typeid == "qk")
                {
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "区块";
                }
                //else
                //{
                //    FpSpread1.Sheets[0].Cells[2,1].Value = "作业区";
                //}
                /////////////////////////09.4.30更新//////////////////////////////////////
                if (typeid == "yt")
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text + "(油井)";
                }
                else if (typeid == "pjdy")
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
            FarpointGridChange.FarPointChange(FpSpread1, "biao10.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;

        }


    }
}

