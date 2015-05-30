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
    public partial class biao19 : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string list = _getParam("CYC");
            if (list == "null")
            { initSpread2(); }
            else
            { initSpread(); }

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }

        protected void initSpread()
        {
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");

            string path = Page.MapPath("../../../static/excel/gufenbiao.xls");
            this.FpSpread1.Sheets[0].OpenExcel(path, "表19-已采取措施无效井措施效果跟踪表");
            FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
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
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            string path = Page.MapPath("../../../static/excel/gufenbiao.xls");
            this.FpSpread1.Sheets[0].OpenExcel(path, "表19-已采取措施无效井措施效果跟踪表");
            FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
        }

        protected void sj()
        {
            string cycid = Session["cyc_id"].ToString();
            string Dropdl = _getParam("Dropdl");
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            string list = _getParam("CYC");
            string lbny = _getParam("lstartMonth");
            string leny = _getParam("lendMonth");

            SqlHelper conn = new SqlHelper();
            OracleConnection connfp = conn.GetConn();
            //OracleConnection connfp = DB.CreatConnection();

            string fpselect = "";
            fpselect += "select a.dep_name,a.xuhao, a.jh,a.qk,b.scsj,b.rcy,b.hscyl,b.jkcyl,b.hs,b.yqspl,b.dyqczcb ,b.xymc ";
            fpselect += " from ";
            fpselect += " ( ";
            fpselect += " select dept.dep_name, ";
            fpselect += " 0 as xuhao, ";
            fpselect += " sdy.jh, ";
            fpselect += " sdy.qk ";
            fpselect += " from department dept , jdstat_djsj_all sdy ";
            fpselect += " where dept.dep_id = sdy.cyc_id and  ";
            fpselect += " sdy.gsxyjb = '5' and ";
            fpselect += " sdy.cyc_id = '" + cycid + "'  ";
            if (Dropdl == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.qk = '" + Dropdl + "'";
            }
            fpselect += " and sdy.bny='" + lbny + "'";
            fpselect += " and sdy.eny='" + leny + "' ";
            //fpselect += " order by qk";
            fpselect += ") a, ";
            /////////////////////////
            fpselect += " (";
            fpselect += " select  dept.dep_name, ";
            fpselect += " 0 as xuhao, ";
            fpselect += " sdy.jh, ";
            //fpselect += " sdy.qk, ";
            fpselect += " sdy.scsj, ";

            fpselect += " nvl(round(sdy.rcy,4),0) as rcy, ";
            fpselect += " nvl(round(sdy.hscyl,4),0) as hscyl , ";
            fpselect += " nvl(round(sdy.jkcyl,4),0) as jkcyl, ";
            fpselect += " nvl(round(sdy.hs ,4),0) as hs, ";
            fpselect += " nvl(round(sdy.yqspl ,4),0) as yqspl, ";
            fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.czcb,0)/sdy.yqspl,2) end) as dyqczcb, ";
            fpselect += " xy.xymc as xymc ";
            fpselect += " from department dept , jdstat_djsj_all sdy ,gsxylb_info xy ";
            fpselect += " where dept.dep_id = sdy.cyc_id and  ";
            fpselect += " sdy.gsxyjb = xy.xyjb and ";
            //fpselect += " sdy.gsxyjb = '5' and ";
            fpselect += " sdy.cyc_id = '" + cycid + "'  ";
            if (Dropdl == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.qk = '" + Dropdl + "'";
            }
            fpselect += " and sdy.bny='" + bny + "'";
            fpselect += " and sdy.eny='" + eny + "' ";

            fpselect += " ) b";


            fpselect += " where a.jh=b.jh(+) ";
            fpselect += " order by a.qk";
            try
            {
                connfp.Open();
                OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                DataSet fpset = new DataSet();
                da.Fill(fpset, "fpdata");

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
                            FpSpread1.Sheets[0].Cells[i + hcount, 1].Value = i + 1;
                        }
                    }
                    /////////////////////09.5.30合并单元格
                    for (int m = 0; m < 4; m++)  //列
                    {
                        //int m = 0;
                        int k = 1;  //统计重复单元格
                        int w = hcount;  //记录起始位置
                        for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                        {
                            if (FpSpread1.ActiveSheetView.Cells[i, m].Value.ToString() == FpSpread1.ActiveSheetView.Cells[i - 1, m].Value.ToString())
                            {
                                if (i == FpSpread1.ActiveSheetView.Rows.Count - 1)
                                {
                                    FpSpread1.Sheets[0].Cells[i, 1].Value = k + 1;
                                    FpSpread1.Sheets[0].Cells[i - 1, 1].Value = k;
                                }
                                else

                                    FpSpread1.Sheets[0].Cells[i - 1, 1].Value = k;
                                k++;
                            }
                            else
                            {

                                FpSpread1.Sheets[0].Cells[i - 1, 1].Value = k;

                                if (k != 1)
                                {
                                    FpSpread1.Sheets[0].Cells[i, 1].Value = k;
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
                else//不为空
                {
                    string path = Page.MapPath("~/excel/gufenbiao.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "表19-已采取措施无效井措施效果跟踪表");

                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;


                //    FpSpread1.Sheets[0].Cells[1, 0].Value = tbdw;      ////////09.5.13  3-3
                    FpSpread1.Sheets[0].Cells[1, 0].Font.Size = 9;     ////////09.5.13  3-3

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            FpSpread1.Sheets[0].Cells[i + hcount, 1].Value = i + 1;
                        }
                    }
                    /////////////////////09.5.30合并单元格
                    for (int m = 0; m < 4; m++)  //列
                    {
                        //int m = 0;
                        int k = 1;  //统计重复单元格
                        int w = hcount;  //记录起始位置
                        for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                        {
                            if (FpSpread1.ActiveSheetView.Cells[i, m].Value.ToString() == FpSpread1.ActiveSheetView.Cells[i - 1, m].Value.ToString())
                            {
                                if (i == FpSpread1.ActiveSheetView.Rows.Count - 1)
                                {
                                    FpSpread1.Sheets[0].Cells[i, 1].Value = k + 1;
                                    FpSpread1.Sheets[0].Cells[i - 1, 1].Value = k;
                                }
                                else

                                    FpSpread1.Sheets[0].Cells[i - 1, 1].Value = k;
                                k++;
                            }
                            else
                            {

                                FpSpread1.Sheets[0].Cells[i - 1, 1].Value = k;

                                if (k != 1)
                                {
                                    FpSpread1.Sheets[0].Cells[i, 1].Value = k;
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
            FarpointGridChange.FarPointChange(FpSpread1, "biao20.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;


        }

    }
}
