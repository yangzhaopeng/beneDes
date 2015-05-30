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
using System.Text.RegularExpressions;

namespace beneDesCYC.view.stockAssessment.gfgspjbYQ
{
    public partial class ycjjxyflhzbYQ : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //initSpread();
            //区块或评级单元  取值 qk / pjdy
            string typeid = _getParam("targetType");
            //取值 。。。油藏 /  。。。气藏
            string yqcmc = _getParam("yqcmc");
            initSpread(typeid, yqcmc);
        }
        protected void initSpread(string typeid, string yqclx)
        {

            string path = "../../../static/excel/jdnianduYQ.xls";
            path = Page.MapPath(path);
            Regex reg = new Regex(@"油藏");  //

            if (isempty() == 0)
            { Response.Write("<script>alert('无数据')</script>"); }
            else
            {
                if (reg.IsMatch(yqclx))  //油藏
                {
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表-18油藏经济效益分类汇总表"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                        loadycsj(typeid, yqclx);
                    }
                    else
                    {
                        if (FpSpread1.Sheets[0].Rows.Count != 5)
                            Response.Write("<script>alert('加载模板有问题，请检查模板文件是否正确！')</script>");
                    }
                }
                else
                {

                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表-18气藏经济效益分类汇总表"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                        loadqcsj(typeid, yqclx);
                    }
                    else
                    {
                        if (FpSpread1.Sheets[0].Rows.Count != 5)
                            Response.Write("<script>alert('加载模板有问题，请检查模板文件是否正确！')</script>");
                    }
                }
            }

        }

        //加载油藏数据
        protected void loadycsj(string typeid, string as_yqclx)
        {
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");

            string as_cyc = Session["cyc_id"].ToString();
            string as_cqc = Session["cqc_id"].ToString();


            DataSet ds = new DataSet();
            DataSet QJds = new DataSet();
            var param_bny = new OracleParameter("as_bny", OracleType.VarChar);
            param_bny.Value = bny;
            var param_eny = new OracleParameter("as_eny", OracleType.VarChar);
            param_eny.Value = eny;
            var param_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc.Value = as_cyc;
            var param_cqc = new OracleParameter("as_cqc", OracleType.VarChar);
            param_cqc.Value = as_cyc;
            var param_yqclx = new OracleParameter("as_yqclx", OracleType.VarChar);
            param_yqclx.Value = as_yqclx;
            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;


            if (typeid == "qk")
            {
                ds = sql.GetDataSet("OWCBS_VIEW_YQCJJXYFLHZB.YCJJXYFLHZB_QK_PROC", CommandType.StoredProcedure,
                         new OracleParameter[] { param_bny, param_eny, param_cyc, param_cqc, param_yqclx, param_out });
                FillQKSpread(ds);
            }
            else if (typeid == "pjdy")
            {
                ds = sql.GetDataSet("OWCBS_VIEW_YQCJJXYFLHZB.YCJJXYFLHZB_PJDY_PROC", CommandType.StoredProcedure,
            new OracleParameter[] { param_bny, param_eny, param_cyc, param_cqc, param_yqclx, param_out });
                FillQKSpread(ds);
            }
        }
        //加载气藏数据
        protected void loadqcsj(string typeid, string as_yqclx)
        {
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");

            string as_cyc = Session["cyc_id"].ToString();
            string as_cqc = Session["cqc_id"].ToString();


            DataSet ds = new DataSet();
            DataSet QJds = new DataSet();
            var param_bny = new OracleParameter("as_bny", OracleType.VarChar);
            param_bny.Value = bny;
            var param_eny = new OracleParameter("as_eny", OracleType.VarChar);
            param_eny.Value = eny;
            var param_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc.Value = as_cyc;
            var param_cqc = new OracleParameter("as_cqc", OracleType.VarChar);
            param_cqc.Value = as_cyc;
            var param_yqclx = new OracleParameter("as_yqclx", OracleType.VarChar);
            param_yqclx.Value = as_yqclx;
            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;


            if (typeid == "qk")
            {
                ds = sql.GetDataSet("OWCBS_VIEW_YQCJJXYFLHZB.QCJJXYFLHZB_QK_PROC", CommandType.StoredProcedure,
                         new OracleParameter[] { param_bny, param_eny, param_cyc, param_cqc, param_yqclx, param_out });
                FillQKSpread(ds);
            }
            else if (typeid == "pjdy")
            {
                ds = sql.GetDataSet("OWCBS_VIEW_YQCJJXYFLHZB.QCJJXYFLHZB_PJDY_PROC", CommandType.StoredProcedure,
            new OracleParameter[] { param_bny, param_eny, param_cyc, param_cqc, param_yqclx, param_out });
                FillQKSpread(ds);
            }
        }
        private void FillQKSpread(DataSet ds)
        {
            int rcount = ds.Tables[0].Rows.Count;
            int ccount = ds.Tables[0].Columns.Count;
            int hcount = 5;

            if (FpSpread1.Sheets[0].Rows.Count == hcount)
            {
                FpSpread1.Sheets[0].AddRows(hcount, rcount);
                for (int i = 0; i < rcount; i++)
                {
                    for (int j = 0; j < ccount; j++)
                    {
                        string value = ds.Tables[0].Rows[i][j].ToString();
                        if (string.IsNullOrEmpty(value))
                            value = "0";
                        FpSpread1.Sheets[0].Cells[i + hcount, j].Value = value;
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
                    w = i;
                    k = 1;
                }
            }
            FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
            FpSpread1.Width = Unit.Pixel(1533);

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

            FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao18.xls");


        }

    }
}
