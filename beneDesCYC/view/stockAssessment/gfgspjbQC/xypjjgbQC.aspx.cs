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
    public partial class xypjjgbQC : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
        }

        /// <summary>
        /// 初始化spread
        /// </summary>
        protected void initSpread()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/jdnianduQC.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表4-气井效益评价结果表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

            //this.FpSpread1.Sheets[0].Cells[3, 1].Text = _getParam("targetType");
            //this.FpSpread1.Sheets[0].Cells[4, 1].Text = _getParam("startMonth") + " -- " + _getParam("endMonth");

            if (isempty() == 0)
            { Response.Write("<script>alert('无数据')</script>"); }
            else
            { sj(); }
        }

        //}
        //else
        //{
        protected void initSpread2()
        {
            string path = "../../../static/excel/jdnianduQC.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表5-区块效益评价结果表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

            if (isempty() == 0)
            { Response.Write("<script>alert('无数据')</script>"); }
            else
            { sj2(); }

            //}
        }

        /// <summary>
        /// 导出按钮点击触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DC_Click(object sender, EventArgs e)
        {
            //this.FpSpread1.Sheets[0].Cells[0, 0].Text = "您点击了‘导出按钮’";
            string typeid = _getParam("targetType");
            if (typeid == "yt")
            { FarpointGridChange.FarPointChange(FpSpread1, "jdniaodubiao2.xls"); }
            else
            { FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao3.xls"); }
        }

        protected void sj()
        {
            //JObject obj = new JObject();
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            //string list = _getParam("CYC");

            DataSet fpset = new DataSet();
            var param_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc.Value = Session["cqc_id"].ToString();
            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;
            try
            {
                fpset = sql.GetDataSet("OWCBS_REPORT_GFGS_Q.R4_XYPJJGB_QJ", CommandType.StoredProcedure,
                     new OracleParameter[] { param_cyc, param_out });


                //此处用于绑定数据
                #region
                int rcount = fpset.Tables[0].Rows.Count;
                int ccount = fpset.Tables[0].Columns.Count - 1;
                int hcount = 4;

                FpSpread1.Sheets[0].AddRows(hcount, rcount);
                for (int i = 0; i < rcount; i++)
                {
                    for (int j = 0; j < ccount; j++)
                    {
                        FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables[0].Rows[i][j + 1].ToString();
                        FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        
                        //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                        //if ((j == 3 || j == (ccount - 1)) && j != 2 && isNotFloat.Isdouble(fpset.Tables[0].Rows[i][j + 1].ToString()) && fpset.Tables[0].Rows[i][j + 1].ToString().Trim() != "0")
                        //{

                        //    FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables[0].Rows[i][j + 1].ToString()).ToString("0.00");

                        //}
                        //else if (j != 2 && isNotFloat.Isdouble(fpset.Tables[0].Rows[i][j + 1].ToString()) && fpset.Tables[0].Rows[i][j + 1].ToString().Trim() != "0")
                        //{
                        //    FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables[0].Rows[i][j + 1].ToString()).ToString("0.0000");
                        //}
                        //else
                        //{
                        //    FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables[0].Rows[i][j + 1].ToString();
                        //}
                    }
                }
                for (int i = hcount; i < rcount + hcount; i++)
                {
                    if (FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 1].Value.ToString().Trim() != "" && FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 1].Value.ToString().Trim() != "0")
                    {
                        try
                        {
                            FpSpread1.Sheets[0].Cells[i, 2].Value = Math.Round(((float.Parse(FpSpread1.Sheets[0].Cells[i, 1].Value.ToString())) / (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 1].Value.ToString())) * 100), 2).ToString("0.00");
                        }
                        catch { }
                    }
                    if (FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 3].Value.ToString().Trim() != "" && FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 3].Value.ToString().Trim() != "0")
                    {
                        try
                        {
                            FpSpread1.Sheets[0].Cells[i, 4].Value = Math.Round(((float.Parse(FpSpread1.Sheets[0].Cells[i, 3].Value.ToString())) / (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 3].Value.ToString())) * 100), 2).ToString("0.00");

                        }
                        catch { }
                    }
                    if (FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 5].Value.ToString().Trim() != "" && FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 5].Value.ToString().Trim() != "0")
                    {
                        try
                        {
                        FpSpread1.Sheets[0].Cells[i, 6].Value = Math.Round(((float.Parse(FpSpread1.Sheets[0].Cells[i, 5].Value.ToString())) / (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 5].Value.ToString())) * 100), 2).ToString("0.00");
                        }
                        catch {}
                    }
                }
                
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
            string cycid = Session["cyc_id"].ToString();
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
                    fpset = sql.GetDataSet("OWCBS_REPORT_GFGS_Q.R4_XYPJJGB_QK", CommandType.StoredProcedure,
                     new OracleParameter[] { param_cyc, param_out });

                }
                else
                {
                    fpset = sql.GetDataSet("OWCBS_REPORT_GFGS_Q.R4_XYPJJGB_PJDY", CommandType.StoredProcedure,
                     new OracleParameter[] { param_cyc, param_out });

                }

                //此处用于绑定数据            
                #region
                int rcount = fpset.Tables[0].Rows.Count;
                int ccount = fpset.Tables[0].Columns.Count - 1;  //显示的集合比数据集少1列，数据集的第2列给表第0列，表第一列为i++,其他列为数据集列减1
                int hcount = 4;
                
                
                FpSpread1.Sheets[0].AddRows(hcount, rcount);
                for (int i = 0; i < rcount; i++)
                {
                    for (int j = 0; j < ccount; j++)
                    {
                        FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables[0].Rows[i][j + 1].ToString();
                        
                        FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;

                    }
                }
                for (int i = hcount; i < rcount + hcount; i++)
                {
                    if (FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 1].ToString().Trim() != "" && FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 1].ToString().Trim() != "0")
                    {
                        try
                        {
                            FpSpread1.Sheets[0].Cells[i, 2].Value = Math.Round(((float.Parse(FpSpread1.Sheets[0].Cells[i, 1].Value.ToString())) / (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 1].Value.ToString())) * 100), 2).ToString("0.00");
                        }
                        catch { }
                    }
                    if (FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 3].ToString().Trim() != "" && FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 3].ToString().Trim() != "0")
                    {
                        try
                        {
                            FpSpread1.Sheets[0].Cells[i, 4].Value = Math.Round(((float.Parse(FpSpread1.Sheets[0].Cells[i, 3].Value.ToString())) / (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 3].Value.ToString())) * 100), 2).ToString("0.00");

                        }
                        catch { }
                    }
                    if (FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 5].ToString().Trim() != "" && FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 5].ToString().Trim() != "0")
                    {
                        try
                        {
                            FpSpread1.Sheets[0].Cells[i, 6].Value = Math.Round(((float.Parse(FpSpread1.Sheets[0].Cells[i, 5].Value.ToString())) / (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 5].Value.ToString())) * 100), 2).ToString("0.00");
                        }
                        catch { }
                    }
                    if (FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 7].ToString().Trim() != "" && FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 7].ToString().Trim() != "0")
                    {
                        try
                        {
                            FpSpread1.Sheets[0].Cells[i, 8].Value = Math.Round(((float.Parse(FpSpread1.Sheets[0].Cells[i, 7].Value.ToString())) / (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 7].Value.ToString())) * 100), 2).ToString("0.00");
                        }
                        catch { }
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
            string ss = "select bny,eny from jdstat_djsj where bny='" + bny + "' and eny='" + eny + "'" + " and cyc_id='" + Session["cyc_id"].ToString()+"'";
            OracleDataAdapter da = new OracleDataAdapter(ss, con1);
            DataSet ds = new DataSet();
            da.Fill(ds, "time");
            con1.Close();
            if (ds.Tables["time"].Rows.Count == 0)
                return 0;
            else
                return 1;

        }



    }
}
