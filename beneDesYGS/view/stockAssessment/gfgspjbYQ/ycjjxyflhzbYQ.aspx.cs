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
using System.Text.RegularExpressions;

namespace beneDesYGS.view.stockAssessment.gfgspjbYQ
{
    public partial class ycjjxyflhzbYQ : beneDesYGS.core.UI.corePage
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
                int width = 0;
                for (int Col = 0; Col < FpSpread1.Sheets[0].Columns.Count; Col++)
                {
                    width += FpSpread1.Sheets[0].Columns[Col].Width;
                }

                FpSpread1.Width = Unit.Pixel(width + 100);
            }

        }

        //加载油藏数据
        protected void loadycsj(string typeid, string as_yqclx)
        {
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            string[] cyc_ids = _getParamArr("CYC");
            string as_cyc = cyc_ids[0];
            string as_cqc = cyc_ids[0];
            if (cyc_ids.Length > 1)
                as_cqc = cyc_ids[1];

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
            string[] cyc_ids = _getParamArr("CYC");
            string as_cyc = cyc_ids[0];
            string as_cqc = cyc_ids[0];
            if (cyc_ids.Length > 1)
                as_cqc = cyc_ids[1];

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
            int width = 0;
            for (int Col = 0; Col < FpSpread1.Sheets[0].Columns.Count; Col++)
            {
                width += FpSpread1.Sheets[0].Columns[Col].Width;
            }

            FpSpread1.Width = Unit.Pixel(width + 100);

        }
        protected void initSpread()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/jdnianduQC.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表18-中高渗透油藏");

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
            string list = _getParam("QCLX");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            string list2 = _getParam("CYC");
            string typeid = _getParam("targetType");

            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["ygsConnectionString"].ConnectionString);
            string fpselect = string.Empty;
            if (list == "null" || list == "凝析气藏")
            {
                fpselect = "select distinct hsjb,kcljb,hsjbmc,kcljbmc from rv_stat_NXYHLjbhz_frame order by hsjb,kcljb";
            }
            else
            {
                fpselect = "select distinct hsjb,kcljb,hsjbmc,kcljbmc from rv_stat_LHHHLjbhz_frame order by hsjb,kcljb";
            }



            DataTable fptable = new DataTable("fpdata");
            fptable.Columns.Add("hsjb", typeof(int));
            fptable.Columns.Add("kcljb", typeof(int));
            fptable.Columns.Add("hsjbmc", typeof(string));
            fptable.Columns.Add("kcljbmc", typeof(string));

            fptable.Columns.Add("xy1cl", typeof(float));
            fptable.Columns.Add("xy1bl", typeof(float));
            fptable.Columns.Add("xy1czcb", typeof(float));

            fptable.Columns.Add("xy2cl", typeof(float));
            fptable.Columns.Add("xy2bl", typeof(float));
            fptable.Columns.Add("xy2czcb", typeof(float));

            fptable.Columns.Add("xy3cl", typeof(float));
            fptable.Columns.Add("xy3bl", typeof(float));
            fptable.Columns.Add("xy3czcb", typeof(float));

            fptable.Columns.Add("xy4cl", typeof(float));
            fptable.Columns.Add("xy4bl", typeof(float));
            fptable.Columns.Add("xy4czcb", typeof(float));

            connfp.Open();
            OracleCommand myComm = new OracleCommand(fpselect, connfp);
            OracleDataReader myReader = myComm.ExecuteReader();
            while (myReader.Read())
            {
                DataRow Fprow = fptable.NewRow();
                Fprow[0] = myReader[0];
                Fprow[1] = myReader[1];
                Fprow[2] = myReader[2];
                Fprow[3] = myReader[3];
                string selectvalue = "";
                if (list == "凝析气藏")
                {
                    for (int i = 1; i <= 4; i++)
                    {

                        selectvalue = "select distinct round(value,4),rv_stat_NXYHLjbhz_frame.hsjb,rv_stat_NXYHLjbhz_frame.kcljb,item from view_hyqckcclxyflhz,rv_stat_NXYHLjbhz_frame where cyc_id='" + Session["cyc_id"].ToString() + "' and bny='" + bny + "' and eny='" + eny + "' and yclx='" + list + "'and item<>'cyoulbl'";
                        selectvalue += " and rv_stat_NXYHLjbhz_frame.hsjb=view_hyqckcclxyflhz.hsjb and rv_stat_NXYHLjbhz_frame.kcljb=view_hyqckcclxyflhz.kcljb and rv_stat_NXYHLjbhz_frame.hsjb='" + Fprow[0].ToString() + "' and rv_stat_NXYHLjbhz_frame.kcljb='" + Fprow[1].ToString() + "' and view_hyqckcclxyflhz.xyjb='" + i.ToString() + "' order by item";

                        OracleDataAdapter davalue = new OracleDataAdapter(selectvalue, connfp);
                        DataSet valueset = new DataSet();
                        davalue.Fill(valueset, "value");
                        if (valueset.Tables["value"].Rows.Count != 0)
                        {
                            Fprow[4 + (i - 1) * 3] = valueset.Tables["value"].Rows[0][0];
                            Fprow[3 + 3 * i] = valueset.Tables["value"].Rows[1][0];
                        }
                    }
                    fptable.Rows.Add(Fprow);
                }
                else
                {
                    for (int i = 1; i <= 4; i++)
                    {

                        selectvalue = "select distinct round(value,4),rv_stat_LHHHLjbhz_frame.hsjb,rv_stat_LHHHLjbhz_frame.kcljb,item from view_hlqckcclxyflhz,rv_stat_LHHHLjbhz_frame where cyc_id='" + Session["cyc_id"].ToString() + "' and bny='" + bny + "' and eny='" + eny + "' and yclx='" + list + "'and item<>'cyoulbl'";
                        selectvalue += " and rv_stat_LHHHLjbhz_frame.hsjb=view_hlqckcclxyflhz.hsjb and rv_stat_LHHHLjbhz_frame.kcljb=view_hlqckcclxyflhz.kcljb and rv_stat_LHHHLjbhz_frame.hsjb='" + Fprow[0].ToString() + "' and rv_stat_LHHHLjbhz_frame.kcljb='" + Fprow[1].ToString() + "' and view_hlqckcclxyflhz.xyjb='" + i.ToString() + "' order by item";

                        OracleDataAdapter davalue = new OracleDataAdapter(selectvalue, connfp);
                        DataSet valueset = new DataSet();
                        davalue.Fill(valueset, "value");
                        if (valueset.Tables["value"].Rows.Count != 0)
                        {
                            Fprow[4 + (i - 1) * 3] = valueset.Tables["value"].Rows[0][0];
                            Fprow[3 + 3 * i] = valueset.Tables["value"].Rows[1][0];
                        }
                    }
                    fptable.Rows.Add(Fprow);
                }

            }
            //计算比例
            for (int blnum = 5; blnum < 15; blnum += 3)
            {
                for (int rows = 0; rows < fptable.Rows.Count; rows++)
                {
                    string ncl = fptable.Rows[rows][blnum - 1].ToString();
                    if (ncl != "")
                        fptable.Rows[rows][blnum] = Math.Round(float.Parse(ncl) / float.Parse(fptable.Rows[fptable.Rows.Count - 1][blnum - 1].ToString()) * 100, 2);
                }
            }
            myReader.Close();

            try
            {
                DataSet fpset = new DataSet();
                fpset.Tables.Add(fptable);

                //此处用于绑定数据
                #region
                int rcount = fpset.Tables["fpdata"].Rows.Count;
                int ccount = fpset.Tables["fpdata"].Columns.Count;
                int hcount = 5;
                //重新写表头
                this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + list2 + list + "经济效益分类汇总表";
                this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                this.FpSpread1.Sheets[0].RowHeader.Visible = false;

                if (FpSpread1.Sheets[0].Rows.Count == hcount)
                {
                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 2; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j - 2].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            //FpSpread1.Sheets[0].Cells[i + hcount, j - 2].Font.Size = 9;
                            if ((j - 1) % 3 == 0 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j - 2].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                            }
                            else if (j % 3 == 0 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j - 2].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
                            }
                            else
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j - 2].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            }
                            FpSpread1.Sheets[0].Cells[i + hcount, j - 2].Font.Size = 9;
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

                }
                else//不为空
                {
                    string path = "../../../static/excel/jdnianduQC.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表18-中高渗透油藏");

                    this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                    this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + list2 + list + "经济效益分类汇总表";

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 2; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j - 2].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            //FpSpread1.Sheets[0].Cells[i + hcount, j - 2].Font.Size = 9;
                            if ((j - 1) % 3 == 0 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j - 2].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                            }
                            else if (j % 3 == 0 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j - 2].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
                            }
                            else
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j - 2].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            }
                            FpSpread1.Sheets[0].Cells[i + hcount, j - 2].Font.Size = 9;
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

            FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao18.xls");


        }

    }
}
