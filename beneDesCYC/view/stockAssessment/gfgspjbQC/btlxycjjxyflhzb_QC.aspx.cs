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

namespace beneDesCYC.view.stockAssessment.gfgspjb_QC
{
    public partial class btlxycjjxyflhzb_QC : beneDesCYC.core.UI.corePage
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
            string path = "../../../static/excel/jdnianduQC.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表17-不同类型油藏经济效益分类汇总表");

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
            //string list = _getParam("CYC");
            string typeid = _getParam("targetType");

            DataSet fpset = new DataSet();
            var param_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc.Value = Session["cqc_id"].ToString();
            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;

            try
            {
                if (typeid == "qk")
                {
                    fpset = sql.GetDataSet("OWCBS_REPORT_GFGS_Q.R17_BTLXYCJJXYFLHZB_QK", CommandType.StoredProcedure,
                    new OracleParameter[] { param_cyc, param_out });

                }
                else if (typeid == "pjdy")
                {
                    fpset = sql.GetDataSet("OWCBS_REPORT_GFGS_Q.R17_BTLXYCJJXYFLHZB_PJDY", CommandType.StoredProcedure,
                    new OracleParameter[] { param_cyc, param_out });
                }


                
                //此处用于绑定数据            
                #region
                int rcount = fpset.Tables[0].Rows.Count;
                int ccount = fpset.Tables[0].Columns.Count;
                int hcount = 4;
                //FpSpread1.ColumnHeader.Visible = false;
                //FpSpread1.RowHeader.Visible = false;
                if (FpSpread1.Sheets[0].Rows.Count != hcount)
                {
                    string path = "../../../static/excel/jdniandu.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表17-不同类型油藏经济效益分类汇总表");

                    this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + "  " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    this.FpSpread1.Sheets[0].RowHeader.Visible = false;

                }
                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        FpSpread1.Sheets[0].Cells[i + hcount, 0].Value = fpset.Tables[0].Rows[i][1].ToString();
                        FpSpread1.Sheets[0].Cells[i + hcount, 1].Value = fpset.Tables[0].Rows[i][3].ToString();
                        FpSpread1.Sheets[0].Cells[i + hcount, 2].Value = fpset.Tables[0].Rows[i][5].ToString();
                        FpSpread1.Sheets[0].Cells[i + hcount, 4].Value = fpset.Tables[0].Rows[i][6].ToString();
                        FpSpread1.Sheets[0].Cells[i + hcount, 6].Value = fpset.Tables[0].Rows[i][7].ToString();
                        FpSpread1.Sheets[0].Cells[i + hcount, 8].Value = fpset.Tables[0].Rows[i][8].ToString();
                        FpSpread1.Sheets[0].Cells[i + hcount, 10].Value = fpset.Tables[0].Rows[i][9].ToString();
                        FpSpread1.Sheets[0].Rows[i].Font.Size = 9;
                        
                    }
                    int k = 1;  //统计重复单元格
                    int w = hcount;  //记录起始位置
                    int jjj = 0;
                    //Response.Write("hello");
                    for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                    {
                        if (i == (FpSpread1.Sheets[0].Rows.Count - 1))
                        {
                            FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k + 1, 1);
                            //Response.Write("hello");

                            for (int spannum = i - 4; spannum < i + 1; spannum++)
                            {

                                for (int spancol = 3; spancol < 10; spancol += 2)
                                {
                                    string cl = FpSpread1.Sheets[0].Cells[spannum, spancol - 1].Value.ToString();
                                    string hj = FpSpread1.Sheets[0].Cells[rcount + hcount - 1, spancol - 1].Value.ToString();

                                    //string hj = FpSpread1.Sheets[0].Cells[rcount + hcount -1, spancol - 1].Value.ToString();
                                    if (cl != "" && cl != "0")
                                        FpSpread1.Sheets[0].Cells[spannum, spancol].Value = "100.00";
                                    //if (cl != "" && hj.ToString() != "" && hj.ToString() != "0")
                                    //FpSpread1.Sheets[0].Cells[spannum, spancol].Value = Math.Round(float.Parse(cl) / float.Parse(hj) * 100, 2);
                                    else
                                        FpSpread1.Sheets[0].Cells[spannum, spancol].Text = "";

                                }


                            }
                        }
                        else if (FpSpread1.Sheets[0].Cells[i, 0].Value.ToString().Trim() == FpSpread1.Sheets[0].Cells[i - 1, 0].Value.ToString().Trim())
                        {
                            k++;
                        }

                        else
                        {

                            FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);


                            if (w < (rcount + hcount - 5))
                            {
                                for (int spannum = w; spannum < w + k; spannum++)
                                {

                                    for (int spancol = 3; spancol < 8; spancol += 2)
                                    {

                                        string cl = FpSpread1.Sheets[0].Cells[spannum, spancol - 1].Value.ToString();
                                        string hj = FpSpread1.Sheets[0].Cells[rcount + hcount - k + jjj, spancol - 1].Value.ToString();

                                        //string hj = FpSpread1.Sheets[0].Cells[rcount + hcount -1, spancol - 1].Value.ToString();
                                        if (cl != "" && hj.ToString() != "" && hj.ToString() != "0")
                                            FpSpread1.Sheets[0].Cells[spannum, spancol].Value = Math.Round(float.Parse(cl) / float.Parse(hj) * 100, 2).ToString("0.00");
                                        else
                                            FpSpread1.Sheets[0].Cells[spannum, spancol].Text = "";

                                    }
                                    jjj++;
                                }
                                w = i;
                                k = 1;
                                jjj = 0;
                            }
                        }
                    }

                
                if (typeid == "pjdy")
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text.Replace("区块", "评价单元");
                }
                else if (typeid == "qk")
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text.Replace("评价单元", "区块");
                }
                #endregion

            }


            //OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);
            //string fpselect = "";
            //if (typeid == "qk")
            //{
            //    fpselect = "select 1 as od, ycg.yclx,ycg.xyjb,ycg.xymc as xymc_jb,";
            //    fpselect += "gsmc.xymc,gsmc.gs,gsmc.sykccl,gsmc.cyoul,gsmc.czcb ";
            //    fpselect += "from";
            //    fpselect += "(";
            //    fpselect += "select qclx.qclx as yclx, gsx.xyjb,gsx.xymc from qclx,qkxyjb gsx ";
            //    fpselect += " where gsx.xyjb<> 99 and gsx.xyjb<>90000";
            //    fpselect += ")ycg";
            //    fpselect += " left outer join";
            //    fpselect += "(";
            //    fpselect += "select * from ";
            //    fpselect += "(";
            //    fpselect += " Select 1 As od, pj.yclx,qkxyjb.xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " gsxyjb ";
            //    fpselect += " From jdstat_qksj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and PJ.GSXYJB = qkxyjb.xyjb and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group By pj.yclx,pj.gsxyjb,qkxyjb.xymc ";

            //    fpselect += " Union ";
            //    fpselect += " Select 1 As od, pj.yclx, '合计' as xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " 80000 as gsxyjb ";
            //    fpselect += " From jdstat_qksj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and qkxyjb.xyjb='80000' and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group by pj.yclx ";

            //    fpselect += " Union ";
            //    fpselect += " Select 3 As od, '总计' As yclx,qkxyjb.xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " gsxyjb ";
            //    fpselect += " From jdstat_qksj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and PJ.GSXYJB = qkxyjb.xyjb  and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group By pj.gsxyjb,qkxyjb.xymc ";

            //    fpselect += " Union ";
            //    fpselect += " Select 4 As od, '总计' as yclx, '合计' as xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " 80000 as gsxyjb ";
            //    fpselect += " From jdstat_qksj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and qkxyjb.xyjb='90000' and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group by xymc ";
            //    // fpselect += " order by od,yclx,gsxyjb ";

            //    fpselect += ")gsm";
            //    fpselect += " where od=1";
            //    fpselect += " )gsmc";
            //    fpselect += " on(ycg.yclx=GSMC.yclx and ycg.xyjb=gsmc.gsxyjb)";

            //    //////////////////////////在连接处
            //    fpselect += " union";

            //    fpselect += " select 3 as od,'总计'as yclx, ycg.xyjb,ycg.xymc as xymc_jb,gsmd.xymc,gsmd.gs,gsmd.sykccl,gsmd.cyoul,gsmd.czcb";
            //    fpselect += " from";
            //    fpselect += " qkxyjb ycg";
            //    fpselect += " left outer join ";
            //    fpselect += "(";
            //    fpselect += "select od,yclx,gsxyjb,xymc,gs,sykccl,cyoul,czcb";
            //    fpselect += " from";
            //    fpselect += "(";
            //    fpselect += " Select 1 As od, pj.yclx,qkxyjb.xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " gsxyjb ";
            //    fpselect += " From jdstat_qksj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and PJ.GSXYJB = qkxyjb.xyjb and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group By pj.yclx,pj.gsxyjb,qkxyjb.xymc ";

            //    fpselect += " Union ";
            //    fpselect += " Select 1 As od, pj.yclx, '合计' as xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " 80000 as gsxyjb ";
            //    fpselect += " From jdstat_qksj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and qkxyjb.xyjb='80000' and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group by pj.yclx ";

            //    fpselect += " Union ";
            //    fpselect += " Select 3 As od, '总计' As yclx,qkxyjb.xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " gsxyjb ";
            //    fpselect += " From jdstat_qksj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and PJ.GSXYJB = qkxyjb.xyjb  and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group By pj.gsxyjb,qkxyjb.xymc ";

            //    fpselect += " Union ";
            //    fpselect += " Select 4 As od, '总计' as yclx, '合计' as xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " 80000 as gsxyjb ";
            //    fpselect += " From jdstat_qksj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and qkxyjb.xyjb='90000' and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group by xymc ";
            //    // fpselect += " order by od,yclx,gsxyjb ";
            //    fpselect += " )gsm";
            //    fpselect += "  where od=3 ";
            //    fpselect += " )gsmd ";
            //    fpselect += "  on ycg.xyjb=gsmd.gsxyjb ";
            //    fpselect += "  where ycg.xyjb<>99 and ycg.xyjb<>90000 and ycg.xyjb<>80000 ";

            //    ////////////////////////////大连接处
            //    fpselect += " union";
            //    fpselect += " select od, '总计' as yclx,90000 as xyjb,'合计' as xymc_jb,xymc,gs,sykccl,cyoul,czcb";
            //    fpselect += " from";
            //    fpselect += " ( ";
            //    fpselect += " Select 1 As od, pj.yclx,qkxyjb.xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " gsxyjb ";
            //    fpselect += " From jdstat_qksj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and PJ.GSXYJB = qkxyjb.xyjb and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group By pj.yclx,pj.gsxyjb,qkxyjb.xymc ";

            //    fpselect += " Union ";
            //    fpselect += " Select 1 As od, pj.yclx, '合计' as xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " 80000 as gsxyjb ";
            //    fpselect += " From jdstat_qksj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and qkxyjb.xyjb='80000' and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group by pj.yclx ";

            //    fpselect += " Union ";
            //    fpselect += " Select 3 As od, '总计' As yclx,qkxyjb.xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " gsxyjb ";
            //    fpselect += " From jdstat_qksj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and PJ.GSXYJB = qkxyjb.xyjb  and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group By pj.gsxyjb,qkxyjb.xymc ";

            //    fpselect += " Union ";
            //    fpselect += " Select 4 As od, '总计' as yclx, '合计' as xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " 80000 as gsxyjb ";
            //    fpselect += " From jdstat_qksj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and qkxyjb.xyjb='90000' and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group by xymc ";
            //    // fpselect += " order by od,yclx,gsxyjb ";
            //    fpselect += " )gsm";
            //    fpselect += " where od=4 ";

            //    ///////////////////////////

            //}
            //else if (typeid == "pjdy")
            //{
            //    //fpselect = " Select 1 As od, pj.yclx,qkxyjb.xymc, ";
            //    //fpselect += " round(Count(mc),4) As gs, ";
            //    //fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    //fpselect += " round(Sum(cyoul)/10000,4) As cyoul, ";
            //    //fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    //fpselect += " gsxyjb ";
            //    //fpselect += " From jdstat_pjdysj pj,qkxyjb ";
            //    //fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    //fpselect += " and PJ.GSXYJB = qkxyjb.xyjb and pj.cyc_id = '" + Session["cyc"].ToString() + "' ";
            //    //fpselect += " Group By pj.yclx,pj.gsxyjb,qkxyjb.xymc ";

            //    //fpselect += " Union ";
            //    //fpselect += " Select 1 As od, pj.yclx,'合计' as xymc, ";
            //    //fpselect += " round(Count(mc),4) As gs, ";
            //    //fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    //fpselect += " round(Sum(cyoul)/10000,4) As cyoul, ";
            //    //fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    //fpselect += " 80000 as gsxyjb ";
            //    //fpselect += " From jdstat_pjdysj pj,qkxyjb ";
            //    //fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    //fpselect += " and qkxyjb.xyjb='80000' and pj.cyc_id = '" + Session["cyc"].ToString() + "' ";
            //    //fpselect += " Group By pj.yclx ";

            //    //fpselect += " Union ";
            //    //fpselect += " Select 3 As od, '总计' As yclx,qkxyjb.xymc, ";
            //    //fpselect += " round(Count(mc),4) As gs, ";
            //    //fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    //fpselect += " round(Sum(cyoul)/10000,4) As cyoul, ";
            //    //fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    //fpselect += " gsxyjb ";
            //    //fpselect += " From jdstat_pjdysj pj,qkxyjb ";
            //    //fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    //fpselect += " and PJ.GSXYJB = qkxyjb.xyjb  and pj.cyc_id = '" + Session["cyc"].ToString() + "' ";
            //    //fpselect += " Group By pj.gsxyjb,qkxyjb.xymc ";

            //    //fpselect += " Union ";
            //    //fpselect += " Select 4 As od, '总计' as yclx,'合计' as xymc, ";
            //    //fpselect += " round(Count(mc),4) As gs, ";
            //    //fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    //fpselect += " round(Sum(cyoul)/10000,4) As cyoul, ";
            //    //fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    //fpselect += " 80000 as gsxyjb ";
            //    //fpselect += " From jdstat_pjdysj pj,qkxyjb ";
            //    //fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    //fpselect += " and qkxyjb.xyjb='90000' and pj.cyc_id = '" + Session["cyc"].ToString() + "' ";
            //    //fpselect += " Group By xymc ";
            //    //fpselect += " order by od,yclx,gsxyjb ";

            //    fpselect = "select 1 as od, ycg.yclx,ycg.xyjb,ycg.xymc as xymc_jb,";
            //    fpselect += "gsmc.xymc,gsmc.gs,gsmc.sykccl,gsmc.cyoul,gsmc.czcb ";
            //    fpselect += "from";
            //    fpselect += "(";
            //    fpselect += "select qclx.qclx as yclx, gsx.xyjb,gsx.xymc from qclx,qkxyjb gsx ";
            //    fpselect += " where gsx.xyjb<> 99 and gsx.xyjb<>90000";
            //    fpselect += ")ycg";
            //    fpselect += " left outer join";
            //    fpselect += "(";
            //    fpselect += "select * from ";
            //    fpselect += "(";
            //    fpselect += " Select 1 As od, pj.yclx,qkxyjb.xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " gsxyjb ";
            //    fpselect += " From jdstat_pjdysj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and PJ.GSXYJB = qkxyjb.xyjb and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group By pj.yclx,pj.gsxyjb,qkxyjb.xymc ";

            //    fpselect += " Union ";
            //    fpselect += " Select 1 As od, pj.yclx, '合计' as xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " 80000 as gsxyjb ";
            //    fpselect += " From jdstat_pjdysj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and qkxyjb.xyjb='80000' and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group by pj.yclx ";

            //    fpselect += " Union ";
            //    fpselect += " Select 3 As od, '总计' As yclx,qkxyjb.xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " gsxyjb ";
            //    fpselect += " From jdstat_pjdysj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and PJ.GSXYJB = qkxyjb.xyjb  and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group By pj.gsxyjb,qkxyjb.xymc ";

            //    fpselect += " Union ";
            //    fpselect += " Select 4 As od, '总计' as yclx, '合计' as xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " 80000 as gsxyjb ";
            //    fpselect += " From jdstat_pjdysj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and qkxyjb.xyjb='90000' and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group by xymc ";
            //    // fpselect += " order by od,yclx,gsxyjb ";

            //    fpselect += ")gsm";
            //    fpselect += " where od=1";
            //    fpselect += " )gsmc";
            //    fpselect += " on(ycg.yclx=GSMC.yclx and ycg.xyjb=gsmc.gsxyjb)";

            //    //////////////////////////在连接处
            //    fpselect += " union";

            //    fpselect += " select 3 as od,'总计'as yclx, ycg.xyjb,ycg.xymc as xymc_jb,gsmd.xymc,gsmd.gs,gsmd.sykccl,gsmd.cyoul,gsmd.czcb";
            //    fpselect += " from";
            //    fpselect += " qkxyjb ycg";
            //    fpselect += " left outer join ";
            //    fpselect += "(";
            //    fpselect += "select od,yclx,gsxyjb,xymc,gs,sykccl,cyoul,czcb";
            //    fpselect += " from";
            //    fpselect += "(";
            //    fpselect += " Select 1 As od, pj.yclx,qkxyjb.xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " gsxyjb ";
            //    fpselect += " From jdstat_pjdysj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and PJ.GSXYJB = qkxyjb.xyjb and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group By pj.yclx,pj.gsxyjb,qkxyjb.xymc ";

            //    fpselect += " Union ";
            //    fpselect += " Select 1 As od, pj.yclx, '合计' as xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " 80000 as gsxyjb ";
            //    fpselect += " From jdstat_pjdysj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and qkxyjb.xyjb='80000' and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group by pj.yclx ";

            //    fpselect += " Union ";
            //    fpselect += " Select 3 As od, '总计' As yclx,qkxyjb.xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " gsxyjb ";
            //    fpselect += " From jdstat_pjdysj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and PJ.GSXYJB = qkxyjb.xyjb  and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group By pj.gsxyjb,qkxyjb.xymc ";

            //    fpselect += " Union ";
            //    fpselect += " Select 4 As od, '总计' as yclx, '合计' as xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " 80000 as gsxyjb ";
            //    fpselect += " From jdstat_pjdysj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and qkxyjb.xyjb='90000' and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group by xymc ";
            //    // fpselect += " order by od,yclx,gsxyjb ";
            //    fpselect += " )gsm";
            //    fpselect += "  where od=3 ";
            //    fpselect += " )gsmd ";
            //    fpselect += "  on ycg.xyjb=gsmd.gsxyjb ";
            //    fpselect += "  where ycg.xyjb<>99 and ycg.xyjb<>90000 and ycg.xyjb<>80000";
            //    //fpselect += "order by yclx, xyjb";

            //    ////////////////////////////大连接处
            //    fpselect += " union";
            //    fpselect += " select od, '总计' as yclx,90000 as xyjb,'合计' as xymc_jb,xymc,gs,sykccl,cyoul,czcb";
            //    fpselect += " from";
            //    fpselect += " ( ";
            //    fpselect += " Select 1 As od, pj.yclx,qkxyjb.xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " gsxyjb ";
            //    fpselect += " From jdstat_pjdysj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and PJ.GSXYJB = qkxyjb.xyjb and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group By pj.yclx,pj.gsxyjb,qkxyjb.xymc ";

            //    fpselect += " Union ";
            //    fpselect += " Select 1 As od, pj.yclx, '合计' as xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " 80000 as gsxyjb ";
            //    fpselect += " From jdstat_pjdysj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and qkxyjb.xyjb='80000' and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group by pj.yclx ";

            //    fpselect += " Union ";
            //    fpselect += " Select 3 As od, '总计' As yclx,qkxyjb.xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " gsxyjb ";
            //    fpselect += " From jdstat_pjdysj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and PJ.GSXYJB = qkxyjb.xyjb  and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group By pj.gsxyjb,qkxyjb.xymc ";

            //    fpselect += " Union ";
            //    fpselect += " Select 4 As od, '总计' as yclx, '合计' as xymc, ";
            //    fpselect += " round(Count(mc),4) As gs, ";
            //    fpselect += " nvl(round(Sum(sykccl),4),0) As sykccl, ";
            //    fpselect += " round(Sum(cql)/10000,4) As cyoul, ";
            //    fpselect += " round((Case When Sum(trqspl) =0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),2) As czcb, ";
            //    fpselect += " 80000 as gsxyjb ";
            //    fpselect += " From jdstat_pjdysj pj,qkxyjb ";
            //    fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and qkxyjb.xyjb='90000' and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //    fpselect += " Group by xymc ";
            //    // fpselect += " order by od,yclx,gsxyjb ";
            //    fpselect += " )gsm";
            //    fpselect += " where od=4 ";
            //    fpselect += " order by yclx, xyjb ";
            //}

            //try
            //{
            //    connfp.Open();

            //    OracleCommand myComm = new OracleCommand(fpselect, connfp);
            //    OracleDataReader myReader = myComm.ExecuteReader();
            //    //DataTable Fptable用来输出数据
            //    DataTable Fptable = new DataTable("fpdata");
            //    Fptable.Columns.Add("yclx", typeof(string));
            //    Fptable.Columns.Add("gsxyjb", typeof(System.String));
            //    Fptable.Columns.Add("gs", typeof(string));
            //    Fptable.Columns.Add("bl1", typeof(string));
            //    Fptable.Columns.Add("sykccl", typeof(string));
            //    Fptable.Columns.Add("bl2", typeof(string));
            //    Fptable.Columns.Add("cyoul", typeof(string));
            //    Fptable.Columns.Add("bl3", typeof(string));
            //    Fptable.Columns.Add("czcb", typeof(string));

            //    DataRow Fprow;
            //    while (myReader.Read())
            //    {
            //        //Fprow = Fptable.NewRow();

            //        //Fprow[0] = myReader[1];
            //        //Fprow[1] = myReader[2];
            //        //Fprow[2] = myReader.GetValue(3);
            //        //Fprow[4] = myReader.GetValue(4);
            //        //Fprow[6] = myReader.GetValue(5);
            //        //Fprow[8] = myReader.GetValue(6);
            //        //Fptable.Rows.Add(Fprow);
            //        Fprow = Fptable.NewRow();

            //        Fprow[0] = myReader[1];
            //        Fprow[1] = myReader[3];
            //        if (myReader[5].ToString().Equals("") || myReader[5].ToString().Equals(null))
            //        { Fprow[2] = ""; }
            //        else { Fprow[2] = myReader.GetValue(5).ToString(); }
            //        //gs
            //        if (myReader[6].ToString().Equals("") || myReader[6].ToString().Equals(null))
            //        { Fprow[4] = ""; }
            //        else { Fprow[4] = myReader.GetValue(6).ToString(); }
            //        //Fprow[4] = myReader.GetValue(4);//skyccl
            //        if (myReader[7].ToString().Equals("") || myReader[7].ToString().Equals(null))
            //        { Fprow[6] = ""; }
            //        else { Fprow[6] = myReader.GetValue(7).ToString(); }
            //        //Fprow[6] = myReader.GetValue(5);//cyoul
            //        // Fprow[8] = myReader.GetValue(6);//czcb
            //        if (myReader[8].ToString().Equals("") || myReader[8].ToString().Equals(null))
            //        { Fprow[8] = ""; }
            //        else { Fprow[8] = myReader.GetValue(8).ToString(); }
            //        Fptable.Rows.Add(Fprow);
            //    }
            //    myReader.Close();
            //    myComm.Clone();

            //    DataSet fpset = new DataSet();
            //    fpset.Tables.Add(Fptable);
            //    //此处用于绑定数据            
            //    #region
            //    int rcount = fpset.Tables["fpdata"].Rows.Count;
            //    int ccount = fpset.Tables["fpdata"].Columns.Count;
            //    int hcount = 4;
            //    //FpSpread1.ColumnHeader.Visible = false;
            //    //FpSpread1.RowHeader.Visible = false;
            //    if (FpSpread1.Sheets[0].Rows.Count == hcount)
            //    {
            //        FpSpread1.Sheets[0].AddRows(hcount, rcount);
            //        for (int i = 0; i < rcount; i++)
            //        {
            //            for (int j = 0; j < ccount; j++)
            //            {
            //                //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
            //                if (fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
            //                {
            //                    if ((j == 4 || j == 6) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()))
            //                        FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
            //                    else if (j == 8 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()))
            //                        FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
            //                    else

            //                        FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();

            //                }
            //                else
            //                    FpSpread1.Sheets[0].Cells[i + hcount, j].Value = "";

            //                FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
            //            }
            //        }
            //        int k = 1;  //统计重复单元格
            //        int w = hcount;  //记录起始位置
            //        int jjj = 0;
            //        //Response.Write("hello");
            //        for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
            //        {
            //            if (i == (FpSpread1.Sheets[0].Rows.Count - 1))
            //            {
            //                FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k + 1, 1);
            //                //Response.Write("hello");

            //                for (int spannum = i - 4; spannum < i + 1; spannum++)
            //                {

            //                    for (int spancol = 3; spancol < 8; spancol += 2)
            //                    {
            //                        string cl = FpSpread1.Sheets[0].Cells[spannum, spancol - 1].Value.ToString();
            //                        string hj = FpSpread1.Sheets[0].Cells[rcount + hcount - 1, spancol - 1].Value.ToString();

            //                        //string hj = FpSpread1.Sheets[0].Cells[rcount + hcount -1, spancol - 1].Value.ToString();
            //                        if (cl != "" && cl != "0")
            //                            FpSpread1.Sheets[0].Cells[spannum, spancol].Value = "100.00";
            //                        //if (cl != "" && hj.ToString() != "" && hj.ToString() != "0")
            //                        //FpSpread1.Sheets[0].Cells[spannum, spancol].Value = Math.Round(float.Parse(cl) / float.Parse(hj) * 100, 2);
            //                        else
            //                            FpSpread1.Sheets[0].Cells[spannum, spancol].Text = "";

            //                    }


            //                }
            //            }
            //            else if (FpSpread1.Sheets[0].Cells[i, 0].Value.ToString().Trim() == FpSpread1.Sheets[0].Cells[i - 1, 0].Value.ToString().Trim())
            //            {
            //                k++;
            //            }

            //            else
            //            {

            //                FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);


            //                if (w < (rcount + hcount - 5))
            //                {
            //                    for (int spannum = w; spannum < w + k; spannum++)
            //                    {

            //                        for (int spancol = 3; spancol < 8; spancol += 2)
            //                        {

            //                            string cl = FpSpread1.Sheets[0].Cells[spannum, spancol - 1].Value.ToString();
            //                            string hj = FpSpread1.Sheets[0].Cells[rcount + hcount - k + jjj, spancol - 1].Value.ToString();

            //                            //string hj = FpSpread1.Sheets[0].Cells[rcount + hcount -1, spancol - 1].Value.ToString();
            //                            if (cl != "" && hj.ToString() != "" && hj.ToString() != "0")
            //                                FpSpread1.Sheets[0].Cells[spannum, spancol].Value = Math.Round(float.Parse(cl) / float.Parse(hj) * 100, 2).ToString("0.00");
            //                            else
            //                                FpSpread1.Sheets[0].Cells[spannum, spancol].Text = "";

            //                        }
            //                        jjj++;
            //                    }
            //                    w = i;
            //                    k = 1;
            //                    jjj = 0;
            //                }
            //            }
            //        }

            //    }
            //    else//不为空
            //    {
            //        int jjj = 0;
            //        string path = "../../../static/excel/jdniandu.xls";
            //        path = Page.MapPath(path);
            //        this.FpSpread1.Sheets[0].OpenExcel(path, "表17-不同类型油藏经济效益分类汇总表");

            //        this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + "  " + FpSpread1.Sheets[0].Cells[0, 0].Text;
            //        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
            //        this.FpSpread1.Sheets[0].RowHeader.Visible = false;

            //        FpSpread1.Sheets[0].AddRows(hcount, rcount);
            //        for (int i = 0; i < rcount; i++)
            //        {
            //            for (int j = 0; j < ccount; j++)
            //            {
            //                if (fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
            //                {
            //                    if ((j == 4 || j == 6) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()))
            //                        FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
            //                    else if (j == 8 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()))
            //                        FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
            //                    else

            //                        FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();

            //                }
            //                else
            //                    FpSpread1.Sheets[0].Cells[i + hcount, j].Value = "";

            //                FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
            //            }
            //        }
            //        int k = 1;  //统计重复单元格
            //        int w = hcount;  //记录起始位置
            //        for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
            //        {
            //            if (i == (FpSpread1.Sheets[0].Rows.Count - 1))
            //            {
            //                FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k + 1, 1);

            //                for (int spannum = i - 4; spannum < i + 1; spannum++)
            //                {

            //                    for (int spancol = 3; spancol < 8; spancol += 2)
            //                    {
            //                        string cl = FpSpread1.Sheets[0].Cells[spannum, spancol - 1].Value.ToString();
            //                        string hj = FpSpread1.Sheets[0].Cells[rcount + hcount - 1, spancol - 1].Value.ToString();

            //                        //string hj = FpSpread1.Sheets[0].Cells[rcount + hcount -1, spancol - 1].Value.ToString();
            //                        if (cl != "" && cl != "0")
            //                            FpSpread1.Sheets[0].Cells[spannum, spancol].Value = "100.00";
            //                        //if (cl != "" && hj.ToString() != "" && hj.ToString() != "0")
            //                        //FpSpread1.Sheets[0].Cells[spannum, spancol].Value = Math.Round(float.Parse(cl) / float.Parse(hj) * 100, 2);
            //                        else
            //                            FpSpread1.Sheets[0].Cells[spannum, spancol].Text = "";

            //                    }


            //                }
            //                //Response.Write("hello");
            //            }
            //            else if (FpSpread1.Sheets[0].Cells[i, 0].Value.ToString().Trim() == FpSpread1.Sheets[0].Cells[i - 1, 0].Value.ToString().Trim())
            //            {
            //                k++;
            //            }
            //            else
            //            {
            //                FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
            //                for (int spannum = w; spannum < w + k; spannum++)
            //                {
            //                    for (int spancol = 3; spancol < 8; spancol += 2)
            //                    {
            //                        string cl = FpSpread1.Sheets[0].Cells[spannum, spancol - 1].Value.ToString();
            //                        //string hj= FpSpread1.Sheets[0].Cells[w + k - 1, spancol - 1].Value.ToString();
            //                        string hj = FpSpread1.Sheets[0].Cells[rcount + hcount - k + jjj, spancol - 1].Value.ToString();
            //                        if (cl != "" && hj != "" && hj != "0")
            //                            FpSpread1.Sheets[0].Cells[spannum, spancol].Value = Math.Round(float.Parse(cl) / float.Parse(hj) * 100, 2).ToString("0.00");
            //                        else
            //                            FpSpread1.Sheets[0].Cells[spannum, spancol].Text = "";
            //                    }
            //                    jjj++;
            //                }
            //                w = i;
            //                k = 1;
            //                jjj = 0;
            //            }
            //        }

            //    }
            //    if (typeid == "pjdy")
            //    {
            //        FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text.Replace("区块", "评价单元");
            //    }
            //    else if (typeid == "qk")
            //    {
            //        FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text.Replace("评价单元", "区块");
            //    }
            //    #endregion

            
            catch (OracleException error)
            {
                string CuoWu = "错误: " + error.Message.ToString();
                Response.Write(CuoWu);

            }

            
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

            FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao17.xls");


        }
    }
}

