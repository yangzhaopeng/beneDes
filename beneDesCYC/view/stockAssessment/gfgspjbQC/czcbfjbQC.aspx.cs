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
using beneDesCYC.model.system;
using beneDesCYC.core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace beneDesCYC.view.stockAssessment.gfgspjbQC
{
    public partial class czcbfjbQC : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string typeid = _getParam("targetType");
            if (typeid == "yt")
            {
                initSpread();
            }
            else
            {
                initSpread2();
            }
        }

        protected void initSpread()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/jdnianduQC.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表15-油井操作成本分级表");

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
            this.FpSpread1.Sheets[0].OpenExcel(path, "表16-油田(区块)操作成本分级表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

            if (isempty() == 0)
            { Response.Write("<script>alert('无数据')</script>"); }
            else
            { sj2(); }

            //}
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
                fpset = sql.GetDataSet("OWCBS_REPORT_GFGS_Q.R15_CZCBFJB_QJ", CommandType.StoredProcedure,
                    new OracleParameter[] { param_cyc, param_out });


                //此处用于绑定数据            
                #region
                int rcount = fpset.Tables[0].Rows.Count;
                int ccount = fpset.Tables[0].Columns.Count;
                int hcount = 4;
                
                FpSpread1.Sheets[0].AddRows(hcount, rcount);
                for (int i = 0; i < rcount; i++)
                {

                    FpSpread1.Sheets[0].Cells[i + hcount, 0].Value = fpset.Tables[0].Rows[i][0].ToString();
                    FpSpread1.Sheets[0].Cells[i + hcount, 1].Value = fpset.Tables[0].Rows[i][1].ToString();
                    FpSpread1.Sheets[0].Cells[i + hcount, 3].Value = fpset.Tables[0].Rows[i][2].ToString();
                    FpSpread1.Sheets[0].Cells[i + hcount, 5].Value = fpset.Tables[0].Rows[i][3].ToString();
                    FpSpread1.Sheets[0].Cells[i + hcount, 7].Value = fpset.Tables[0].Rows[i][4].ToString();

                    FpSpread1.Sheets[0].Rows[i + hcount].Font.Size = 9;
                }

                for (int i = hcount; i < rcount + hcount; i++)
                {
                    if (FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 1].Value.ToString().Trim() != "" && FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 1].Value.ToString().Trim() != "0")
                    {
                        try
                        {
                            FpSpread1.Sheets[0].Cells[i, 2].Value = Math.Round(((float.Parse(FpSpread1.Sheets[0].Cells[i, 1].Value.ToString())) / (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 1].Value.ToString())) * 100), 2).ToString("0.00");
                        }
                        catch 
                        {
                            FpSpread1.Sheets[0].Cells[i, 2].Value = '0';
                        }
                    }
                    if (FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 3].Value.ToString().Trim() != "" && FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 3].Value.ToString().Trim() != "0")
                    {
                        try
                        {
                            FpSpread1.Sheets[0].Cells[i, 4].Value = Math.Round(((float.Parse(FpSpread1.Sheets[0].Cells[i, 3].Value.ToString())) / (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 3].Value.ToString())) * 100), 2).ToString("0.00");

                        }
                        catch 
                        {
                            FpSpread1.Sheets[0].Cells[i, 4].Value = '0';
                        }
                    }
                    if (FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 5].Value.ToString().Trim() != "" && FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 5].Value.ToString().Trim() != "0")
                    {
                        try
                        {
                            FpSpread1.Sheets[0].Cells[i, 6].Value = Math.Round(((float.Parse(FpSpread1.Sheets[0].Cells[i, 5].Value.ToString())) / (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 5].Value.ToString())) * 100), 2).ToString("0.00");
                        }
                        catch
                        {
                            FpSpread1.Sheets[0].Cells[i, 6].Value = '0';
                        }
                    }
                    if (FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 7].Value.ToString().Trim() != "" && FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 7].Value.ToString().Trim() != "0")
                    {
                        try
                        {
                            FpSpread1.Sheets[0].Cells[i, 8].Value = Math.Round(((float.Parse(FpSpread1.Sheets[0].Cells[i, 7].Value.ToString())) / (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 7].Value.ToString())) * 100), 2).ToString("0.00");
                        }
                        catch 
                        {
                            FpSpread1.Sheets[0].Cells[i, 8].Value = '0';
                        }
                    }
                }

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
                //FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].Rows.Count - 1, 1].Value = " ";
                //FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);

                #endregion

            }
            catch (OracleException error)
            {
                string CuoWu = "错误: " + error.Message.ToString();
                Response.Write(CuoWu);

            }

        }

        protected void sj2()
        {
            //JObject obj = new JObject();
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            string list = _getParam("CYC");
            string typeid = _getParam("targetType");

            DataSet fpset = new DataSet();
            var param_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc.Value = Session["cyc_id"].ToString();
            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;
            try
            {
                if (typeid == "qk")
                {
                    fpset = sql.GetDataSet("OWCBS_REPORT_GFGS_Q.R15_CZCBFJB_QK", CommandType.StoredProcedure,
                    new OracleParameter[] { param_cyc, param_out });

                }
                else
                {
                    fpset = sql.GetDataSet("OWCBS_REPORT_GFGS_Q.R15_CZCBFJB_PJDY", CommandType.StoredProcedure,
                    new OracleParameter[] { param_cyc, param_out });

                }

                //此处用于绑定数据            
                #region
                int rcount = fpset.Tables[0].Rows.Count;
                int ccount = fpset.Tables[0].Columns.Count;
                int hcount = 4;
                
                FpSpread1.Sheets[0].AddRows(hcount, rcount);
                for (int i = 0; i < rcount; i++)
                {

                    FpSpread1.Sheets[0].Cells[i + hcount, 0].Value = fpset.Tables[0].Rows[i][2].ToString();
                    FpSpread1.Sheets[0].Cells[i + hcount, 1].Value = fpset.Tables[0].Rows[i][3].ToString();
                    FpSpread1.Sheets[0].Cells[i + hcount, 2].Value = fpset.Tables[0].Rows[i][4].ToString();
                    FpSpread1.Sheets[0].Cells[i + hcount, 4].Value = fpset.Tables[0].Rows[i][5].ToString();
                    FpSpread1.Sheets[0].Cells[i + hcount, 6].Value = fpset.Tables[0].Rows[i][6].ToString();
                    FpSpread1.Sheets[0].Cells[i + hcount, 8].Value = fpset.Tables[0].Rows[i][7].ToString();
                    FpSpread1.Sheets[0].Cells[i + hcount, 10].Value = fpset.Tables[0].Rows[i][8].ToString();

                    FpSpread1.Sheets[0].Rows[i + hcount].Font.Size = 9;
                }

                for (int i = hcount; i < rcount + hcount; i++)
                {
                    if (FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 2].Value.ToString().Trim() != "" && FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 2].Value.ToString().Trim() != "0")
                    {
                        try
                        {
                            FpSpread1.Sheets[0].Cells[i, 3].Value = Math.Round(((float.Parse(FpSpread1.Sheets[0].Cells[i, 2].Value.ToString())) / (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 2].Value.ToString())) * 100), 2).ToString("0.00");
                        }
                        catch
                        {
                            FpSpread1.Sheets[0].Cells[i, 3].Value = '0';
                        }
                    }
                    if (FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 4].Value.ToString().Trim() != "" && FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 4].Value.ToString().Trim() != "0")
                    {
                        try
                        {
                            FpSpread1.Sheets[0].Cells[i, 5].Value = Math.Round(((float.Parse(FpSpread1.Sheets[0].Cells[i, 4].Value.ToString())) / (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 4].Value.ToString())) * 100), 2).ToString("0.00");

                        }
                        catch
                        {
                            FpSpread1.Sheets[0].Cells[i, 5].Value = '0';
                        }
                    }
                    if (FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 6].Value.ToString().Trim() != "" && FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 6].Value.ToString().Trim() != "0")
                    {
                        try
                        {
                            FpSpread1.Sheets[0].Cells[i, 7].Value = Math.Round(((float.Parse(FpSpread1.Sheets[0].Cells[i, 6].Value.ToString())) / (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 6].Value.ToString())) * 100), 2).ToString("0.00");
                        }
                        catch
                        {
                            FpSpread1.Sheets[0].Cells[i, 7].Value = '0';
                        }
                    }
                    if (FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 8].Value.ToString().Trim() != "" && FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 8].Value.ToString().Trim() != "0")
                    {
                        try
                        {
                            FpSpread1.Sheets[0].Cells[i, 9].Value = Math.Round(((float.Parse(FpSpread1.Sheets[0].Cells[i, 8].Value.ToString())) / (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 8].Value.ToString())) * 100), 2).ToString("0.00");
                        }
                        catch
                        {
                            FpSpread1.Sheets[0].Cells[i, 9].Value = '0';
                        }
                    }
                    if (FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 10].Value.ToString().Trim() != "" && FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 10].Value.ToString().Trim() != "0")
                    {
                        try
                        {
                            FpSpread1.Sheets[0].Cells[i, 11].Value = Math.Round(((float.Parse(FpSpread1.Sheets[0].Cells[i, 10].Value.ToString())) / (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 10].Value.ToString())) * 100), 2).ToString("0.00");
                        }
                        catch
                        {
                            FpSpread1.Sheets[0].Cells[i, 11].Value = '0';
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
                //FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].Rows.Count - 1, 1].Value = " ";
                FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);

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
            string typeid = _getParam("targetType");
            if (typeid == "yt")
            { FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao15.xls"); }
            else
            { FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao16.xls"); }

        }

    }

}
