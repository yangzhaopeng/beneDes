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

namespace beneDesCYC.view.stockAssessment.gfgspjbQC
{
    public partial class xyflhzbQC : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string list = _getParam("CYC");
                if (list == "null")
                { initSpread2(); }
                else
                { initSpread(); }
            }


        }

        protected void initSpread()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/jdnianduQC.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表6-效益分类汇总表-油井");

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
            string path = "../../../static/excel/jdnianduQC.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表6-效益分类汇总表-油井");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
        }

        protected void sj()
        {
            string cycid = Session["cqc_id"].ToString();
            //JObject obj = new JObject();
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            string list = _getParam("CYC");
            string typeid = _getParam("targetType");


            DataSet da = new DataSet();
            DataSet da2 = new DataSet();
            var param_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc.Value = Session["cqc_id"].ToString();
            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;

            var param_cyc1 = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc1.Value = Session["cyc_id"].ToString();
            var param_out1 = new OracleParameter("v", OracleType.Cursor);
            param_out1.Direction = ParameterDirection.Output;
            try
            {
                da = sql.GetDataSet("OWCBS_REPORT_GFGS_Q.R6_XYFLHZB_QJ", CommandType.StoredProcedure,
                    new OracleParameter[] { param_cyc, param_out });

                if (typeid == "qk")
                {
                    da2 = sql.GetDataSet("OWCBS_REPORT_GFGS_Q.R6_XYFLHZB_QK", CommandType.StoredProcedure,
                    new OracleParameter[] { param_cyc1, param_out1 });
                }
                else
                {
                    da2 = sql.GetDataSet("OWCBS_REPORT_GFGS_Q.R6_XYFLHZB_PJDY", CommandType.StoredProcedure,
                    new OracleParameter[] { param_cyc1, param_out1 });
                }


                //合计
                DataTable dt = new DataTable("fpdata");
                dt = da.Tables[0].Copy();

                DataTable dt2 = new DataTable("fpdata2");
                dt2 = da2.Tables[0].Copy();
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


                //此处用于绑定数据
                #region
                int rcount = dt.Rows.Count;
                int ccount = dt.Columns.Count;
                int hcount = 4;
                int rcount2 = dt2.Rows.Count;
                int ccount2 = dt2.Columns.Count;


                for (int i = 0; i < rcount; i++)
                {
                    for (int j = 1; j < ccount - 1; j++)
                    {
                        if (dt.Rows[i][ccount - 1].ToString() == "1")
                        {
                            FpSpread1.Sheets[0].Cells[0 + hcount, j].Value = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Rows[i][ccount - 1].ToString() == "2")
                        {
                            FpSpread1.Sheets[0].Cells[1 + hcount, j].Value = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Rows[i][ccount - 1].ToString() == "3")
                        {
                            FpSpread1.Sheets[0].Cells[2 + hcount, j].Value = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Rows[i][ccount - 1].ToString() == "4")
                        {
                            FpSpread1.Sheets[0].Cells[3 + hcount, j].Value = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Rows[i][ccount - 1].ToString() == "5")
                        {
                            FpSpread1.Sheets[0].Cells[4 + hcount, j].Value = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Rows[i][ccount - 1].ToString() == "0" && i == (rcount - 1) && j == 3)
                        {
                            FpSpread1.Sheets[0].Cells[5 + hcount, j].Value = double.Parse(dt.Rows[i][j].ToString()).ToString("0.0000");
                        }
                        else if (dt.Rows[i][ccount - 1].ToString() == "0")
                        {
                            FpSpread1.Sheets[0].Cells[5 + hcount, j].Value = dt.Rows[i][j].ToString();
                        }
                    }
                }

                //区块
                for (int i = 0; i < rcount2; i++)
                {
                    for (int j = 1 + ccount; j < ccount + ccount2 - 1; j++)
                    {
                        if (dt2.Rows[i][ccount2 - 1].ToString() == "1")
                        {
                            FpSpread1.Sheets[0].Cells[0 + hcount, j - 1].Value = dt2.Rows[i][j - ccount].ToString();
                        }
                        else if (dt2.Rows[i][ccount2 - 1].ToString() == "2")
                        {
                            FpSpread1.Sheets[0].Cells[1 + hcount, j - 1].Value = dt2.Rows[i][j - ccount].ToString();
                        }
                        else if (dt2.Rows[i][ccount2 - 1].ToString() == "3")
                        {
                            FpSpread1.Sheets[0].Cells[2 + hcount, j - 1].Value = dt2.Rows[i][j - ccount].ToString();
                        }
                        else if (dt2.Rows[i][ccount2 - 1].ToString() == "4")
                        {
                            FpSpread1.Sheets[0].Cells[3 + hcount, j - 1].Value = dt2.Rows[i][j - ccount].ToString();
                        }
                        else if (dt2.Rows[i][ccount2 - 1].ToString() == "5")
                        {
                            FpSpread1.Sheets[0].Cells[4 + hcount, j - 1].Value = dt2.Rows[i][j - ccount].ToString();
                        }
                        else if (dt2.Rows[i][ccount2 - 1].ToString() == "0" && i == (rcount2 - 1) && j == 8)
                        {
                            FpSpread1.Sheets[0].Cells[5 + hcount, j - 1].Value = double.Parse(dt2.Rows[i][j - ccount].ToString()).ToString("0.0000");
                        }
                        else if (dt2.Rows[i][ccount2 - 1].ToString() == "0")
                        {
                            FpSpread1.Sheets[0].Cells[5 + hcount, j - 1].Value = dt2.Rows[i][j - ccount].ToString();
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

                #endregion
            }
            catch (OracleException error)
            {
                string CuoWu = "错误: " + error.Message.ToString();
                Response.Write(CuoWu);

            }


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

            FpSpread1.ActiveSheetView.Cells[4, 2].Formula = "IF(B5=0,0,(B5/B10)*100)";
            FpSpread1.ActiveSheetView.Cells[5, 2].Formula = "IF(B6=0,0,(B6/B10)*100)"; //"(B6/B10)*100";
            FpSpread1.ActiveSheetView.Cells[6, 2].Formula = "IF(B7=0,0,(B7/B10)*100)"; //"(B7/B10)*100";
            FpSpread1.ActiveSheetView.Cells[7, 2].Formula = "IF(B8=0,0,(B8/B10)*100)"; //"(B8/B10)*100";
            FpSpread1.ActiveSheetView.Cells[8, 2].Formula = "IF(B9=0,0,(B9/B10)*100)"; //""(B9/B10)*100";
            FpSpread1.ActiveSheetView.Cells[9, 2].Formula = "IF(B10=0,0,(B10/B10)*100)"; //""(B10/B10)*100";

            FpSpread1.ActiveSheetView.Cells[4, 4].Formula ="IF(D5=0,0,(D5/D10)*100)";   // "(D5/D10)*100";
            FpSpread1.ActiveSheetView.Cells[5, 4].Formula ="IF(D6=0,0,(D6/D10)*100)";   // "(D6/D10)*100";
            FpSpread1.ActiveSheetView.Cells[6, 4].Formula ="IF(D7=0,0,(D7/D10)*100)";   //  "(D7/D10)*100";
            FpSpread1.ActiveSheetView.Cells[7, 4].Formula ="IF(D8=0,0,(D8/D10)*100)";   // "(D8/D10)*100";
            FpSpread1.ActiveSheetView.Cells[8, 4].Formula ="IF(D9=0,0,(D9/D10)*100)";   // "(D9/D10)*100";
            FpSpread1.ActiveSheetView.Cells[9, 4].Formula = "IF(D10=0,0,(D10/D10)*100)";// "(D10/D10)*100";

            FpSpread1.ActiveSheetView.Cells[4, 7].Formula = "IF(G5=0,0,(G5/G10)*100)";   // "(G5/G10)*100";
            FpSpread1.ActiveSheetView.Cells[5, 7].Formula = "IF(G6=0,0,(G6/G10)*100)";   // "(G6/G10)*100";
            FpSpread1.ActiveSheetView.Cells[6, 7].Formula = "IF(G7=0,0,(G7/G10)*100)";   // "(G7/G10)*100";
            FpSpread1.ActiveSheetView.Cells[7, 7].Formula = "IF(G8=0,0,(G8/G10)*100)";   // "(G8/G10)*100";
            FpSpread1.ActiveSheetView.Cells[8, 7].Formula = "IF(G9=0,0,(G9/G10)*100)";   // "(G9/G10)*100";
            FpSpread1.ActiveSheetView.Cells[9, 7].Formula = "IF(G10=0,0,(G10/G10)*100)";// "(G10/G10)*100";

            FpSpread1.ActiveSheetView.Cells[4, 9].Formula = "IF(I5=0,0,(I5/I10)*100)";   //"(I5/I10)*100";
            FpSpread1.ActiveSheetView.Cells[5, 9].Formula = "IF(I6=0,0,(I6/I10)*100)";   //"(I6/I10)*100";
            FpSpread1.ActiveSheetView.Cells[6, 9].Formula = "IF(I7=0,0,(I7/I10)*100)";   //"(I7/I10)*100";
            FpSpread1.ActiveSheetView.Cells[7, 9].Formula = "IF(I8=0,0,(I8/I10)*100)";   //"(I8/I10)*100";
            FpSpread1.ActiveSheetView.Cells[8, 9].Formula = "IF(I9=0,0,(I9/I10)*100)";   //"(I9/I10)*100";
            FpSpread1.ActiveSheetView.Cells[9, 9].Formula = "IF(I10=0,0,(I10/I10)*100)";//"(I10/I10)*100";


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
            FpSpread1.Sheets[0].RowHeader.Visible = true;
            FpSpread1.Sheets[0].ColumnHeader.Visible = true;
            //FpSpread1.SaveExcelToResponse("jdnd_biao6.xls");
            FarpointGridChange.FarPointChange(FpSpread1, "jdnd_biao6.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;
        }

        //protected void JS_Click(object sender, EventArgs e)
        //{
        //initSpread();
        //}

    }
}
