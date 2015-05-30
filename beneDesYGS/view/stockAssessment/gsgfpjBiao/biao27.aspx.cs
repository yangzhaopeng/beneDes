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

namespace beneDesYGS.view.stockAssessment.gsgfpjBiao
{
    public partial class biao27 : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string list = _getParam("CYC");
            if (list == "null")
            { initSpread2(); }
            else
            { initSpread(); }
        }

        protected void initSpread()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/jdniandu.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表27-油井含水级别分类汇总表");

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
            string path = "../../../static/excel/jdniandu.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表27-油井含水级别分类汇总表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
        }

        protected void sj()
        {

            //JObject obj = new JObject();
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //string list = _getParam("YCLX");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            //string list = _getParam("CYC");
            //string typeid = _getParam("targetType");

            string[] cyc_ids = _getParamArr("CYC");
            string as_cyc = cyc_ids[0];
            string as_cqc = cyc_ids[0];
            if (cyc_ids.Length > 1)
                as_cqc = cyc_ids[1];

            DataSet fpset = new DataSet();
            var param_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc.Value = as_cyc;
            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;

            DataSet fpsetunion = new DataSet();
            var param_cyc1 = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc1.Value = as_cyc;
            var param_out1 = new OracleParameter("v", OracleType.Cursor);
            param_out1.Direction = ParameterDirection.Output;

            try
            {
                fpset = sql.GetDataSet("OWCBS_REPORT_GFGS.R27_YJHSJBFLHZB_YJ", CommandType.StoredProcedure,
                    new OracleParameter[] { param_cyc, param_out });

                fpsetunion = sql.GetDataSet("OWCBS_REPORT_GFGS.R27_YJHSJBFLHZB_HJ", CommandType.StoredProcedure,
                    new OracleParameter[] { param_cyc1, param_out1 });


                DataRow[] hjk = fpset.Tables[0].Select("xymc='合计'");
                int hjrow = hjk.Length;
                for (int n = fpset.Tables[0].Rows.Count - hjrow; n < fpset.Tables[0].Rows.Count; n++)
                {
                    string grdname = fpset.Tables[0].Rows[n][1].ToString();
                    string stname = fpset.Tables[0].Rows[n][2].ToString();
                    DataRow[] hjrcy = fpsetunion.Tables[0].Select("grade_name='" + grdname + "' and statname='" + stname + "'");
                    if (hjrcy.Length == 1)
                    {
                        for (int hjcol = 3; hjcol <= 8; hjcol++)
                        {

                            fpset.Tables[0].Rows[n][hjcol] = hjrcy[0][hjcol - 1];
                        }
                    }
                }

                for (int i = 1; i < fpset.Tables[0].Rows.Count; i += 2)
                {
                    for (int j = 3; j <= 8; j++)
                    {
                        if (fpset.Tables[0].Rows[i - 1][8].ToString() == "0")
                        {
                            fpset.Tables[0].Rows[i][j] = 0;
                        }
                        else
                        {
                            fpset.Tables[0].Rows[i][j] = Math.Round(double.Parse(fpset.Tables[0].Rows[i - 1][j].ToString()) / double.Parse(fpset.Tables[0].Rows[i - 1][8].ToString()) * 100, 2);
                        }
                    }
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
                    string path = "/static/excel/jdniandu.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表27-油井含水级别分类汇总表");

                    this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    this.FpSpread1.Sheets[0].RowHeader.Visible = false;

                }
                FpSpread1.Sheets[0].AddRows(hcount, rcount);
                for (int i = 0; i < rcount; i++)
                {
                    for (int j = 0; j < ccount; j++)
                    {
                        FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables[0].Rows[i][j].ToString();
                        FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
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
                        w = i;
                        k = 1;
                    }
                }
                FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
                //合并第二列单元格
                k = 1;  //统计重复单元格
                w = hcount;  //记录起始位置
                for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                {
                    if (FpSpread1.Sheets[0].Cells[i, 1].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, 1].Value.ToString())
                    {
                        k++;
                    }
                    else
                    {
                        FpSpread1.ActiveSheetView.AddSpanCell(w, 1, k, 1);
                        w = i;
                        k = 1;
                    }
                }

                FpSpread1.ActiveSheetView.AddSpanCell(w, 1, k, 1);

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
            FpSpread1.Sheets[0].RowHeader.Visible = true;
            FpSpread1.Sheets[0].ColumnHeader.Visible = true;
            FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao27.xls");

            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;
        }
    }
}
