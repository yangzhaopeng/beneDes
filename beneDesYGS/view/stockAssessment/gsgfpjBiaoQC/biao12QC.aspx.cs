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

namespace beneDesYGS.view.stockAssessment.gsgfpjBiaoQC
{
    public partial class biao12QC : beneDesYGS.core.UI.corePage
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
            this.FpSpread1.Sheets[0].OpenExcel(path, "表12-油井销售收入与成本费用表");

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
            this.FpSpread1.Sheets[0].OpenExcel(path, "表13-区块销售收入与成本费用表");

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
            
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            
            string typeid = _getParam("targetType");

            string[] cyc_ids = _getParamArr("CYC");
            string as_cqc = cyc_ids[0];

            DataSet fpset = new DataSet();
            var param_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc.Value = as_cqc;
            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;

            try
            {
                fpset = sql.GetDataSet("OWCBS_REPORT_GFGS_Q.R12_XSSRYCBFYB_QJ", CommandType.StoredProcedure,
                    new OracleParameter[] { param_cyc, param_out });


                //此处用于绑定数据            
                #region
                int rcount = fpset.Tables[0].Rows.Count;
                int ccount = fpset.Tables[0].Columns.Count - 2;  //数据集前两列不显示
                int hcount = 4;


                FpSpread1.Sheets[0].AddRows(hcount, rcount);
                for (int i = 0; i < rcount; i++)
                {
                    for (int j = 0; j < ccount; j++)
                    {
                        //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                        if (isNotFloat.Isdouble(fpset.Tables[0].Rows[i][j + 2].ToString()) && fpset.Tables[0].Rows[i][j + 2].ToString().Trim() != "0")
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables[0].Rows[i][j + 2].ToString()).ToString("0.0000");
                        }
                        else
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables[0].Rows[i][j + 2].ToString();
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
            
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            
            string typeid = _getParam("targetType");

            string[] cyc_ids = _getParamArr("CYC");
            string as_cqc = cyc_ids[0];
            DataSet fpset = new DataSet();
            var param_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc.Value = as_cqc;
            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;

            try
            {
                if (typeid == "qk")
                {
                    fpset = sql.GetDataSet("OWCBS_REPORT_GFGS_Q.R12_XSSRYCBFYB_QK", CommandType.StoredProcedure,
                    new OracleParameter[] { param_cyc, param_out });

                }
                else
                {
                    fpset = sql.GetDataSet("OWCBS_REPORT_GFGS_Q.R12_XSSRYCBFYB_PJDY", CommandType.StoredProcedure,
                    new OracleParameter[] { param_cyc, param_out });

                }

                #region
                int rcount = fpset.Tables[0].Rows.Count;
                int ccount = fpset.Tables[0].Columns.Count;
                int hcount = 4;

                FpSpread1.Sheets[0].AddRows(hcount, rcount);
                for (int i = 0; i < rcount; i++)
                {
                    for (int j = 0; j < ccount - 1; j++)
                    {
                        FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables[0].Rows[i][j + 1].ToString();
                        FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        FpSpread1.Sheets[0].Cells[i + hcount, 1].Value = i + 1;
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
                //重新写序号
                k = 1;
                w = hcount;
                for (int i = w; i < FpSpread1.Sheets[0].Rows.Count; i++)
                {
                    if (FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() != "合计" && FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() != "总计")
                    {
                        FpSpread1.Sheets[0].Cells[i, 1].Value = k;

                        k++;
                        FpSpread1.Sheets[0].Cells[i, 1].Font.Size = 9;
                    }
                    else if (FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() == "合计")
                    {
                        FpSpread1.ActiveSheetView.AddSpanCell(i, 1, 1, 3);
                        FpSpread1.Sheets[0].Cells[i, 1].Value = "合计";
                    }
                    else if (FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() == "总计")
                    {
                        FpSpread1.ActiveSheetView.AddSpanCell(i, 0, 1, 4);
                        FpSpread1.Sheets[0].Cells[i, 0].Value = "总计";
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
            string ss = "select bny,eny from jdstat_djsj where bny='" + bny + "' and eny='" + eny + "'" + " and qk not in(" + qkmc + ")";
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
            //this.FpSpread1.Sheets[0].Cells[0, 0].Text = "您点击了‘导出按钮’";
            string typeid = _getParam("targetType");
            if (typeid == "yt")
            { FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao12.xls"); }
            else
            { FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao13.xls"); }
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;
        }

    }
}
