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

namespace beneDesYGS.view.oilAssessment.zhiliBiao
{
    public partial class biao18 : beneDesYGS.core.UI.corePage
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

            string path = Page.MapPath("~/static/excel/dongtai.xls");
            this.FpSpread1.Sheets[0].OpenExcel(path, "表18-已采取措施边际效益井措施效果跟踪表");
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
            string path = Page.MapPath("~/static/excel/dongtai.xls");
            this.FpSpread1.Sheets[0].OpenExcel(path, "表18-已采取措施边际效益井措施效果跟踪表");
            FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
        }

        protected void sj()
        {
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            string list = _getParam("CYC");
            string lbny = _getParam("lstartMonth");
            string leny = _getParam("lendMonth");

            SqlHelper conn = new SqlHelper();
            OracleConnection connfp = conn.GetConn();

            string fpselect = "";

            fpselect += "select a.dep_name,a.xuhao, a.jh,a.qk,b.scsj,b.rcy,b.hscyl,b.jkcyl,b.hs,b.yqspl,b.dyqczcb,b.xymc ";
            fpselect += " from ";
            fpselect += " ( ";
            fpselect += " select dept.dep_name, ";
            fpselect += " 0 as xuhao, ";
            fpselect += " sdy.jh, ";
            fpselect += " sdy.qk ";
            fpselect += " from department dept , dtstat_djsj_all sdy ";
            fpselect += " where dept.dep_id = sdy.dep_id and  ";
            fpselect += " sdy.gsxyjb_1 = '3' ";
            //fpselect += " sdy.dep_id = '" + cycid + "'  ";
            if (list == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
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
            fpselect += " xy.jbmc as xymc ";
            fpselect += " from department dept , dtstat_djsj_all sdy , dtxyjb_info xy ";
            fpselect += " where dept.dep_id = sdy.dep_id  ";
            fpselect += " and sdy.gsxyjb_1 = xy.jbid  ";
            //fpselect += " and sdy.gsxyjb_1 = '3' ";
            //fpselect += " sdy.dep_id = '" + cycid + "'  ";
            if (list == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            }
            fpselect += " and sdy.bny='" + bny + "'";
            fpselect += " and sdy.eny='" + eny + "' ";

            fpselect += " ) b";


            fpselect += " where a.jh=b.jh(+) ";
            fpselect += " order by a.qk";


            //fpselect += " select  dept.dep_name, ";
            //fpselect += " 0 as xuhao, ";
            //fpselect += " sdy.jh, ";
            //fpselect += " sdy.qk, ";
            //fpselect += " sdy.scsj, ";

            //fpselect += " nvl(round(sdy.rcy,4),0) as rcy, ";
            //fpselect += " nvl(round(sdy.hscyl,4),0) as hscyl , ";
            //fpselect += " nvl(round(sdy.jkcyl,4),0) as jkcyl, ";
            //fpselect += " nvl(round(sdy.hs ,4),0) as hs, ";
            //fpselect += " nvl(round(sdy.yqspl ,4),0) as yqspl, ";
            //fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.czcb,0)/sdy.yqspl,2) end) as dyqczcb ";

            //fpselect += " from department dept , dtstat_djsj_all sdy ";
            //fpselect += " where dept.dep_id = sdy.dep_id and  ";
            //fpselect += " sdy.gsxyjb_1 = '3'  ";
            //if (list == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_name = '" + list + "') ";
            //}
            //fpselect += " and sdy.bny='" + lbny + "'";
            //fpselect += " and sdy.eny='" + leny + "' ";
            //fpselect += " order by dept.dep_name ";
            try
            {
                connfp.Open();
                OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                DataSet fpset = new DataSet();
                da.Fill(fpset, "fpdata");

                ////////////////////09.5.8更新//////////////////////
                int dtxyjb = 3;
                string fp2 = "";
                fp2 += " select jh,sscs,time,csq_rcye,csq_rcyou,csq_hs,csq_lzy,csh_rcye,csh_rcyou,csh_hs,csh_lzy,bz from dtbiaoqt where bny = '" + lbny + "' and eny = '" + leny + "' and gsxyjb_1 = '" + dtxyjb + "'  ";


                OracleDataAdapter da2 = new OracleDataAdapter(fp2, connfp);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2, "biao18");
                ////////////////////////////////////////////

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
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            ////FpSpread1.Sheets[0].Cells[i + hcount, 1].Value = i + 1;
                            if ((j == 5 || j == 6 || j == 7 || j == 8 || j == 9 || j == 10) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString() != "0")
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
                            else
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;

                        }
                        /////////////////////09.5.8
                        if (ds2.Tables["biao18"].Rows.Count != 0)
                        {
                            for (int j = 0; j < ds2.Tables["biao18"].Rows.Count; j++)   //在数据集里循环匹配井号
                            {

                                if (FpSpread1.Sheets[0].Cells[i + hcount, 2].Value.ToString() == ds2.Tables["biao18"].Rows[j][0].ToString())
                                {
                                    FpSpread1.Sheets[0].Cells[i + hcount, 12].Value = ds2.Tables["biao18"].Rows[j][1].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 13].Value = ds2.Tables["biao18"].Rows[j][2].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 14].Value = ds2.Tables["biao18"].Rows[j][3].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 15].Value = ds2.Tables["biao18"].Rows[j][4].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 16].Value = ds2.Tables["biao18"].Rows[j][5].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 17].Value = ds2.Tables["biao18"].Rows[j][6].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 18].Value = ds2.Tables["biao18"].Rows[j][7].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 19].Value = ds2.Tables["biao18"].Rows[j][8].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 20].Value = ds2.Tables["biao18"].Rows[j][9].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 21].Value = ds2.Tables["biao18"].Rows[j][10].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 22].Value = ds2.Tables["biao18"].Rows[j][11].ToString();
                                    continue;
                                }
                            }
                        }
                        //////////////////////
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
                    string path = Page.MapPath("~/static/excel/dongtai.xls");
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表18-已采取措施边际效益井措施效果跟踪表");

                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;


                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            ////FpSpread1.Sheets[0].Cells[i + hcount, 1].Value = i + 1;
                            if ((j == 5 || j == 6 || j == 7 || j == 8 || j == 9 || j == 10) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString() != "0")
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
                            else
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;

                        }
                        //////////////////////09.5.8
                        if (ds2.Tables["biao18"].Rows.Count != 0)
                        {
                            for (int j = 0; j < ds2.Tables["biao18"].Rows.Count; j++)   //在数据集里循环匹配井号
                            {

                                if (FpSpread1.Sheets[0].Cells[i + hcount, 2].Value.ToString() == ds2.Tables["biao18"].Rows[j][0].ToString())
                                {
                                    FpSpread1.Sheets[0].Cells[i + hcount, 12].Value = ds2.Tables["biao18"].Rows[j][1].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 13].Value = ds2.Tables["biao18"].Rows[j][2].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 14].Value = ds2.Tables["biao18"].Rows[j][3].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 15].Value = ds2.Tables["biao18"].Rows[j][4].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 16].Value = ds2.Tables["biao18"].Rows[j][5].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 17].Value = ds2.Tables["biao18"].Rows[j][6].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 18].Value = ds2.Tables["biao18"].Rows[j][7].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 19].Value = ds2.Tables["biao18"].Rows[j][8].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 20].Value = ds2.Tables["biao18"].Rows[j][9].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 21].Value = ds2.Tables["biao18"].Rows[j][10].ToString();
                                    FpSpread1.Sheets[0].Cells[i + hcount, 22].Value = ds2.Tables["biao18"].Rows[j][11].ToString();
                                    continue;
                                }
                            }
                        }
                        //////////////////////
                    }
                    /////////////////////09.5.30合并单元格
                    for (int m = 0; m < 4; m++)  //列
                    {
                        //int m = 0;
                        int k = 1;  //统计重复单元格
                        int w = hcount;  //记录起始位置
                        for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                        {
                            if (FpSpread1.Sheets[0].Cells[i, m].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, m].Value.ToString() && FpSpread1.Sheets[0].Cells[i, m].Value.ToString() != "")
                            {
                                FpSpread1.Sheets[0].Cells[i - 1, 1].Value = k;
                                k++;
                                FpSpread1.Sheets[0].Cells[i, 1].Value = k;
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
            FarpointGridChange.FarPointChange(FpSpread1, "dt_biao18.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;


        }

    }
}
