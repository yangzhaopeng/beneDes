﻿using System;
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

namespace beneDesYGS.view.oilAssessment.jichuBiao
{
    public partial class biao2 : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string typeid = _getParam("targetType");
            if(typeid == "null")
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
            string path = "../../../static/excel/dongtai.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表2-效益分类汇总表油井");

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
            string path = "../../../static/excel/dongtai.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表2-效益分类汇总表油井");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
        }


        public void SetFormulas()
        {
            //Set up the formulas
            ////Set up celltypes for formula cells
            //FpSpread1.ActiveSheetView.Columns[1].CellType = new CurrencyCellType();
            //FpSpread1.ActiveSheetView.Cells[4, 2, 4, 5].CellType = new CurrencyCellType();

            ////Set row formulas
            //FpSpread1.ActiveSheetView.Cells[8, 1].Formula = "SUM(B5:B8)";
            ////FpSpread1.ActiveSheetView.Cells[8, 2].Formula = "SUM(C5:C8)";
            //FpSpread1.ActiveSheetView.Cells[8, 3].Formula = "SUM(D5:D8)";
            ////FpSpread1.ActiveSheetView.Cells[8, 4].Formula = "SUM(E5:E8)";

            ////Set column formulas
            //FpSpread1.ActiveSheetView.Cells[8, 6].Formula = "SUM(G5:G8)";
            ////FpSpread1.ActiveSheetView.Cells[8, 7].Formula = "SUM(H5:H8)";
            //FpSpread1.ActiveSheetView.Cells[8, 8].Formula = "SUM(I5:I8)";
            ////FpSpread1.ActiveSheetView.Cells[8, 9].Formula = "SUM(J5:J8)";

            FpSpread1.ActiveSheetView.Cells[4, 2].Formula = "(B5/B9)*100";
            FpSpread1.ActiveSheetView.Cells[5, 2].Formula = "(B6/B9)*100";
            FpSpread1.ActiveSheetView.Cells[6, 2].Formula = "(B7/B9)*100";
            FpSpread1.ActiveSheetView.Cells[7, 2].Formula = "(B8/B9)*100";
            FpSpread1.ActiveSheetView.Cells[8, 2].Formula = "(B9/B9)*100";

            FpSpread1.ActiveSheetView.Cells[4, 4].Formula = "(D5/D9)*100";
            FpSpread1.ActiveSheetView.Cells[5, 4].Formula = "(D6/D9)*100";
            FpSpread1.ActiveSheetView.Cells[6, 4].Formula = "(D7/D9)*100";
            FpSpread1.ActiveSheetView.Cells[7, 4].Formula = "(D8/D9)*100";
            FpSpread1.ActiveSheetView.Cells[8, 4].Formula = "(D9/D9)*100";

            FpSpread1.ActiveSheetView.Cells[4, 7].Formula = "(G5/G9)*100";
            FpSpread1.ActiveSheetView.Cells[5, 7].Formula = "(G6/G9)*100";
            FpSpread1.ActiveSheetView.Cells[6, 7].Formula = "(G7/G9)*100";
            FpSpread1.ActiveSheetView.Cells[7, 7].Formula = "(G8/G9)*100";
            FpSpread1.ActiveSheetView.Cells[8, 7].Formula = "(G9/G9)*100";

            FpSpread1.ActiveSheetView.Cells[4, 9].Formula = "(I5/I9)*100";
            FpSpread1.ActiveSheetView.Cells[5, 9].Formula = "(I6/I9)*100";
            FpSpread1.ActiveSheetView.Cells[6, 9].Formula = "(I7/I9)*100";
            FpSpread1.ActiveSheetView.Cells[7, 9].Formula = "(I8/I9)*100";
            FpSpread1.ActiveSheetView.Cells[8, 9].Formula = "(I9/I9)*100";


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
            //string cycid = Session["cyc"].ToString();

            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["ygsConnectionString"].ConnectionString);
            //油井
            string fpselect = " Select xy.jbmc ,sum(djisopen) As js,0 as bl1,round(Sum(hscyl) / 10000,4) As cyl ,0 as bl2,sdy.gsxyjb_1";
            fpselect += " From dtstat_djsj sdy,dtxyjb_info xy ";
            fpselect += " where sdy.gsxyjb_1 <> 99  and sdy.pjdyxyjb <> 99 and sdy.gsxyjb_1 = xy.jbid ";
            //fpselect += " and sdy.cyc_id = '" + cycid + "'";
            if (list == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            }
            fpselect += " Group By xy.jbmc,sdy.gsxyjb_1  ";
            fpselect += " order by sdy.gsxyjb_1 ";
            //区块
            string fpselect2 = " Select xy.jbmc ,count(mc) As js,0 as bl1,nvl(round(Sum(cyoul),4),0) As cyl ,0 as bl2,sdy.gsxyjb_1";
            if (typeid == "qk")
            {
                fpselect2 += " From dtstat_qksj sdy,dtxyjb_info xy ";
            }
            else
            {
                fpselect2 += " From dtstat_pjdysj sdy,dtxyjb_info xy ";
            }
            fpselect2 += " where sdy.sfpj= '1' and sdy.gsxyjb_1 = xy.jbid ";
            //fpselect2 += " and sdy.cyc_id = '" + cycid + "'";
            if (list == "quan")
            {
                fpselect2 += "";
            }
            else
            {
                fpselect2 += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            }
            fpselect2 += " Group By xy.jbmc,sdy.gsxyjb_1  ";
            fpselect2 += " order by sdy.gsxyjb_1 ";

            try
            {
                connfp.Open();
                OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                OracleDataAdapter da2 = new OracleDataAdapter(fpselect2, connfp);
                #region
                //OracleCommand myComm = new OracleCommand(fpselect, connfp);
                //OracleDataReader myReader = myComm.ExecuteReader();
                ////DataTable Fptable用来输出数据
                //DataTable Fptable = new DataTable("fpdata");
                //Fptable.Columns.Add("xymc", typeof(string));
                //Fptable.Columns.Add("js", typeof(float));
                //Fptable.Columns.Add("bl1", typeof(float));
                //Fptable.Columns.Add("cyl", typeof(float));
                //Fptable.Columns.Add("bl2", typeof(float));

                //float blh1 = 0, blh2 = 0, hj1 = 0, hj2 = 0;
                //DataRow Fprow;
                //while (myReader.Read())
                //{
                //    Fprow = Fptable.NewRow();

                //    Fprow[0] = myReader[0];
                //    Fprow[1] = myReader[1];
                //    hj1 += float.Parse(Fprow[1].ToString());
                //    Fprow[3] = myReader.GetValue(2);
                //    hj2 += float.Parse(Fprow[3].ToString());
                //    Fptable.Rows.Add(Fprow);
                //}
                ////计算各列比例
                //for (int i = 0; i < Fptable.Rows.Count; i++)
                //{
                //    if (hj1 != 0)
                //    {
                //        Fptable.Rows[i][2] = Math.Round(float.Parse(Fptable.Rows[i][1].ToString()) / hj1 * 100, 2);
                //    }
                //    else
                //    {
                //        Fptable.Rows[i][2] = 0;
                //    }
                //    ////////////////
                //    if (hj2 != 0)
                //    {
                //        Fptable.Rows[i][4] = Math.Round(float.Parse(Fptable.Rows[i][3].ToString()) / hj2 * 100, 2);
                //    }
                //    else
                //    {
                //        Fptable.Rows[i][4] = 0;
                //    }
                //}
                ////添加合计行
                //DataRow dr = Fptable.NewRow();
                //dr[0] = "合计";

                //dr[1] = hj1;
                //dr[2] = 100;
                //dr[3] = hj2;
                //dr[4] = 100;
                //Fptable.Rows.Add(dr);
                //myComm.Clone();

                //DataSet fpset = new DataSet();
                //fpset.Tables.Add(Fptable);
                #endregion
                //合计
                DataTable dt = new DataTable("fpdata");
                da.Fill(dt);

                DataTable dt2 = new DataTable("fpdata2");
                da2.Fill(dt2);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();

                    //初始化各列
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        dr[i] = 0;
                    }
                    dr[0] = "合计";

                    //计算
                    for (int k = 0; k < dt.Rows.Count; k++)//循环计算各行
                    {

                        for (int n = 1; n < dt.Columns.Count - 1; n++)//循环计算各列
                        {
                            if (dt.Rows[k][n].ToString() != "")
                            {
                                dr[n] = float.Parse(dr[n].ToString()) + float.Parse(dt.Rows[k][n].ToString());
                            }
                            else
                                dr[n] = float.Parse(dr[n].ToString()) + 0;
                        }
                    }

                    //计算精度
                    for (int m = 1; m < dt.Columns.Count; m++)
                    {
                        dr[m] = Math.Round(float.Parse(dr[m].ToString()), 4);
                    }
                    //把行加入表
                    dt.Rows.Add(dr);
                }
                //区块
                if (dt2.Rows.Count > 0)
                {
                    DataRow dr = dt2.NewRow();

                    //初始化各列
                    for (int i = 0; i < dt2.Columns.Count; i++)
                    {
                        dr[i] = 0;
                    }
                    dr[0] = "合计";

                    //计算
                    for (int k = 0; k < dt2.Rows.Count; k++)//循环计算各行
                    {

                        for (int n = 1; n < dt2.Columns.Count - 1; n++)//循环计算各列
                        {
                            if (dt2.Rows[k][n].ToString() != "")
                            {
                                dr[n] = float.Parse(dr[n].ToString()) + float.Parse(dt2.Rows[k][n].ToString());
                            }
                            else
                                dr[n] = float.Parse(dr[n].ToString()) + 0;
                        }
                    }
                    //计算精度
                    for (int m = 1; m < dt2.Columns.Count; m++)
                    {
                        dr[m] = Math.Round(float.Parse(dr[m].ToString()), 4);
                    }
                    //把行加入表
                    dt2.Rows.Add(dr);
                }

                DataSet fpset = new DataSet();
                fpset.Tables.Add(dt);
                fpset.Tables.Add(dt2);
                //此处用于绑定数据
                #region
                int rcount = fpset.Tables["fpdata"].Rows.Count;
                int ccount = fpset.Tables["fpdata"].Columns.Count;
                int hcount = 4;
                int rcount2 = fpset.Tables["fpdata2"].Rows.Count;
                int ccount2 = fpset.Tables["fpdata2"].Columns.Count;

                //FpSpread1.ColumnHeader.Visible = false;
                //FpSpread1.RowHeader.Visible = false;
                if (FpSpread1.Sheets[0].Rows.Count == hcount)
                {
                    //FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    //for (int i = 0; i < rcount; i++)
                    //{
                    //    for (int j = 1; j < ccount-1; j++)
                    //    {
                    //        if (fpset.Tables["fpdata"].Rows[i][ccount-1].ToString () == "1")
                    //        {
                    //            FpSpread1.Sheets[0].Cells[0 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                    //        }
                    //        else if (fpset.Tables["fpdata"].Rows[i][ccount-1].ToString() == "2")
                    //        {
                    //            FpSpread1.Sheets[0].Cells[1 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                    //        }
                    //        else if (fpset.Tables["fpdata"].Rows[i][ccount-1].ToString() == "3")
                    //        {
                    //            FpSpread1.Sheets[0].Cells[2 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                    //        }
                    //        else if (fpset.Tables["fpdata"].Rows[i][ccount-1].ToString() == "4")
                    //        {
                    //            FpSpread1.Sheets[0].Cells[3 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                    //        }
                    //        else if (fpset.Tables["fpdata"].Rows[i][ccount-1].ToString() == "0")
                    //        {
                    //            FpSpread1.Sheets[0].Cells[4 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                    //        }
                    //    }
                    //}
                    ////区块
                    //for (int i = 0; i < rcount2; i++)
                    //{
                    //    for (int j = 1 + ccount; j < ccount + ccount2 - 1; j++)
                    //    {
                    //        if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "1")
                    //        {
                    //            FpSpread1.Sheets[0].Cells[0 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
                    //        }
                    //        else if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "2")
                    //        {
                    //            FpSpread1.Sheets[0].Cells[1 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
                    //        }
                    //        else if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "3")
                    //        {
                    //            FpSpread1.Sheets[0].Cells[2 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
                    //        }
                    //        else if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "4")
                    //        {
                    //            FpSpread1.Sheets[0].Cells[3 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
                    //        }
                    //        else if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "0")
                    //        {
                    //            FpSpread1.Sheets[0].Cells[4 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
                    //        }
                    //    }
                    //}
                }
                else//不为空
                {
                    string path = "../../../static/excel/dongtai.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表2-效益分类汇总表油井");

                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;

                    //FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 1; j < ccount - 1; j++)
                        {
                            if (fpset.Tables["fpdata"].Rows[i][ccount - 1].ToString() == "1")
                            {
                                FpSpread1.Sheets[0].Cells[0 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            }
                            else if (fpset.Tables["fpdata"].Rows[i][ccount - 1].ToString() == "2")
                            {
                                FpSpread1.Sheets[0].Cells[1 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            }
                            else if (fpset.Tables["fpdata"].Rows[i][ccount - 1].ToString() == "3")
                            {
                                FpSpread1.Sheets[0].Cells[2 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            }
                            else if (fpset.Tables["fpdata"].Rows[i][ccount - 1].ToString() == "4")
                            {
                                FpSpread1.Sheets[0].Cells[3 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            }
                            else if (fpset.Tables["fpdata"].Rows[i][ccount - 1].ToString() == "0" && i == (rcount - 1) && j == 3)
                            {
                                FpSpread1.Sheets[0].Cells[4 + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                            }
                            else if (fpset.Tables["fpdata"].Rows[i][ccount - 1].ToString() == "0")
                            {
                                FpSpread1.Sheets[0].Cells[4 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            }
                        }
                    }

                    //区块
                    for (int i = 0; i < rcount2; i++)
                    {
                        for (int j = 1 + ccount; j < ccount + ccount2 - 1; j++)
                        {
                            if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "1")
                            {
                                FpSpread1.Sheets[0].Cells[0 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
                            }
                            else if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "2")
                            {
                                FpSpread1.Sheets[0].Cells[1 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
                            }
                            else if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "3")
                            {
                                FpSpread1.Sheets[0].Cells[2 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
                            }
                            else if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "4")
                            {
                                FpSpread1.Sheets[0].Cells[3 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
                            }
                            else if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "0" && i == (rcount2 - 1) && j == 8)
                            {
                                FpSpread1.Sheets[0].Cells[4 + hcount, j - 1].Value = double.Parse(fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString()).ToString("0.0000");
                            }
                            else if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "0")
                            {
                                FpSpread1.Sheets[0].Cells[4 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
                            }
                        }
                    }
                    //计算比例
                    SetFormulas();

                    //合并单元格
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
                    ////////////////////////////////09.4.29日添加///////////////////////////////////////
                    if (typeid == "pjdy")
                    {
                        FpSpread1.Sheets[0].Cells[2, 6].Value = "评价单元";
                    }
                    else
                    {
                        FpSpread1.Sheets[0].Cells[2, 6].Value = "区块";
                    }
                    ///////////////////////////////////////////////////////////////////////////
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
            FarpointGridChange.FarPointChange(FpSpread1, "dt_biao2.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;


        }
    }
}
