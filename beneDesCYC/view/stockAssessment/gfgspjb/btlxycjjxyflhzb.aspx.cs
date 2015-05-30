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

namespace beneDesCYC.view.stockAssessment.gfgspjb
{
    public partial class btlxycjjxyflhzb : beneDesCYC.core.UI.corePage
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
            string path = "../../../static/excel/jdniandu.xls";
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
                    fpset = sql.GetDataSet("OWCBS_REPORT_GFGS.R17_BTLXYCJJXYFLHZB_QK", CommandType.StoredProcedure,
                    new OracleParameter[] { param_cyc, param_out });

                }
                else if (typeid == "pjdy")
                {
                    fpset = sql.GetDataSet("OWCBS_REPORT_GFGS.R17_BTLXYCJJXYFLHZB_PJDY", CommandType.StoredProcedure,
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

                                for (int spancol = 3; spancol < 8; spancol += 2)
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

