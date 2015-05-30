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
    public partial class czcbgcfxb : beneDesCYC.core.UI.corePage
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
            string path = "../../../static/excel/jdnianduQC.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表29-操作成本构成分析表");

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
            string cyc_id = Session["cqc_id"].ToString();
            //string list = _getParam("YCLX");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            //string list = _getParam("CYC");
           // string aa = Session["date"].ToString();
            //string aa = Session["date"].ToString();

            //int lasty = int.Parse(aa.Substring(0, 4)) - 1;
            SqlHelper conn = new SqlHelper();
            OracleConnection connfp = conn.GetConn();
            connfp.Open();

            string[] fee_code = new string[15];
            fee_code[0] = "zjclf";
            fee_code[1] = "zjrlf";
            fee_code[2] = "zjdlf";
            fee_code[3] = "zjryf";
            fee_code[4] = "qywzrf";
            fee_code[5] = "jxzyf";
            fee_code[6] = "cjsjf";
            fee_code[7] = "whxlf";
            fee_code[8] = "cyrcf";
            fee_code[9] = "yqclf";
            fee_code[10] = "qthsf";
            fee_code[11] = "trqjhf";
            fee_code[12] = "ysf";
            fee_code[13] = "qtzjf";
            fee_code[14] = "ckglf";
            DataSet fpset1 = new DataSet();
            DataSet fpset2 = new DataSet();
            DataSet fpset3 = new DataSet();
            DataSet fpset4 = new DataSet();
            DataSet fpset5 = new DataSet();
            DataSet fpset6 = new DataSet();
            OracleDataAdapter da1, da2, da3, da4, da5, da6;
            string fpl;

            int t = int.Parse(bny.Substring(0, 4));
            t = t - 1;

            string bny1, eny1;
            bny1 = t.ToString() + bny.Substring(4, 2);
            eny1 = t.ToString() + eny.Substring(4, 2);

            for (int i = 0; i < 15; i++)
            {
                //fpl = "select round(sum(nvl(" + fee_code[i] + ",0))/10000,4) from jdstat_djsj  where cyc_id = '" + Session["cyc_id"].ToString() + "' and djisopen = 1 and qkxyjb <> 99 and pjdyxyjb <> 99 ";
                fpl = "select round(sum(nvl(" + fee_code[i] + ",0))/10000,4) from jdstat_djsj  where cyc_id = '" + cyc_id + "' and djisopen = 1 and qkxyjb <> 99 and pjdyxyjb <> 99 ";
                da1 = new OracleDataAdapter(fpl, connfp);
                da1.Fill(fpset1, i.ToString());
                if (isNotFloat.Isdouble(fpset1.Tables[i.ToString()].Rows[0][0].ToString()) && fpset1.Tables[i.ToString()].Rows[0][0].ToString().Trim() != "0")
                {
                    FpSpread1.Sheets[0].Cells[4 + i, 2].Value = float.Parse(fpset1.Tables[i.ToString()].Rows[0][0].ToString()).ToString("0.0000");
                }
                else
                {
                    FpSpread1.Sheets[0].Cells[4 + i, 2].Value = fpset1.Tables[i.ToString()].Rows[0][0].ToString();
                }
                //FpSpread1.Sheets[0].Cells[4 + i, 2].Value = fpset1.Tables[i.ToString()].Rows[0][0];
                da1.Dispose();


                fpl = "select round(sum(nvl(" + fee_code[i] + ",0))/10000,4) from jdstat_djsj_all  where cyc_id = '" + cyc_id + "'  and bny='" + bny1 + "' and eny='" + eny1 + "' and djisopen = 1 and qkxyjb <> 99 and pjdyxyjb <> 99";
                da2 = new OracleDataAdapter(fpl, connfp);
                da2.Fill(fpset2, i.ToString());
                //FpSpread1.Sheets[0].Cells[4 + i, 3].Value = fpset2.Tables[i.ToString()].Rows[0][0];

                if (isNotFloat.Isdouble(fpset2.Tables[i.ToString()].Rows[0][0].ToString()) && fpset2.Tables[i.ToString()].Rows[0][0].ToString().Trim() != "0")
                {
                    FpSpread1.Sheets[0].Cells[4 + i, 3].Value = float.Parse(fpset2.Tables[i.ToString()].Rows[0][0].ToString()).ToString("0.0000");
                }
                else
                {
                    FpSpread1.Sheets[0].Cells[4 + i, 3].Value = fpset2.Tables[i.ToString()].Rows[0][0].ToString();
                }

                da2.Dispose();


                fpl = "select (case when sum(nvl(hscql,0)) = 0 then 0 else  round(sum(nvl(" + fee_code[i].ToString() + ",0))/sum(hscql),4) end) from jdstat_djsj where cyc_id = '" + cyc_id + "' and djisopen = 1 and qkxyjb <> 99 and pjdyxyjb <> 99 ";

                da3 = new OracleDataAdapter(fpl, connfp);
                da3.Fill(fpset3, i.ToString());
                //FpSpread1.Sheets[0].Cells[4 + i, 4].Value = fpset3.Tables[i.ToString()].Rows[0][0];

                if (isNotFloat.Isdouble(fpset3.Tables[i.ToString()].Rows[0][0].ToString()) && fpset3.Tables[i.ToString()].Rows[0][0].ToString().Trim() != "0")
                {
                    FpSpread1.Sheets[0].Cells[4 + i, 4].Value = float.Parse(fpset3.Tables[i.ToString()].Rows[0][0].ToString()).ToString("0.0000");
                }
                else
                {
                    FpSpread1.Sheets[0].Cells[4 + i, 4].Value = fpset3.Tables[i.ToString()].Rows[0][0].ToString();
                }
                da3.Dispose();


                fpl = "select (case when sum(nvl(hscql,0)) = 0 then 0 else  round(sum(nvl(" + fee_code[i].ToString() + ",0))/sum(hscql),4) end) from jdstat_djsj_all where cyc_id = '" + cyc_id + "'  and bny='" + bny1 + "' and eny='" + eny1 + "' and djisopen = 1 and qkxyjb <> 99 and pjdyxyjb <> 99 ";

                da4 = new OracleDataAdapter(fpl, connfp);
                da4.Fill(fpset4, i.ToString());
                //FpSpread1.Sheets[0].Cells[4 + i, 5].Value = fpset4.Tables[i.ToString()].Rows[0][0];

                if (isNotFloat.Isdouble(fpset4.Tables[i.ToString()].Rows[0][0].ToString()) && fpset4.Tables[i.ToString()].Rows[0][0].ToString().Trim() != "0")
                {
                    FpSpread1.Sheets[0].Cells[4 + i, 5].Value = float.Parse(fpset4.Tables[i.ToString()].Rows[0][0].ToString()).ToString("0.0000");
                }
                else
                {
                    FpSpread1.Sheets[0].Cells[4 + i, 5].Value = fpset4.Tables[i.ToString()].Rows[0][0].ToString();
                }
                da4.Dispose();


                fpl = "select (case when sum(nvl(yqspl,0)) = 0 then 0 else  round(sum(nvl(" + fee_code[i].ToString() + ",0))/sum(yqspl),4) end) from jdstat_djsj where cyc_id = '" + cyc_id + "' and djisopen = 1 and qkxyjb <> 99 and pjdyxyjb <> 99 ";

                da5 = new OracleDataAdapter(fpl, connfp);
                da5.Fill(fpset5, i.ToString());
                //FpSpread1.Sheets[0].Cells[4 + i, 6].Value = fpset5.Tables[i.ToString()].Rows[0][0];

                if (isNotFloat.Isdouble(fpset5.Tables[i.ToString()].Rows[0][0].ToString()) && fpset5.Tables[i.ToString()].Rows[0][0].ToString().Trim() != "0")
                {
                    FpSpread1.Sheets[0].Cells[4 + i, 6].Value = float.Parse(fpset5.Tables[i.ToString()].Rows[0][0].ToString()).ToString("0.0000");
                }
                else
                {
                    FpSpread1.Sheets[0].Cells[4 + i, 6].Value = fpset5.Tables[i.ToString()].Rows[0][0].ToString();
                }
                da5.Dispose();


                fpl = "select (case when sum(nvl(yqspl,0)) = 0 then 0 else  round(sum(nvl(" + fee_code[i].ToString() + ",0))/sum(yqspl),4) end) from jdstat_djsj_all where cyc_id = '" + Session["cyc_id"].ToString() + "'   and bny='" + bny1 + "' and eny='" + eny1 + "' and djisopen = 1 and qkxyjb <> 99 and pjdyxyjb <> 99";

                da6 = new OracleDataAdapter(fpl, connfp);
                da6.Fill(fpset6, i.ToString());
                //FpSpread1.Sheets[0].Cells[4 + i, 7].Value = fpset6.Tables[i.ToString()].Rows[0][0];

                if (isNotFloat.Isdouble(fpset6.Tables[i.ToString()].Rows[0][0].ToString()) && fpset6.Tables[i.ToString()].Rows[0][0].ToString().Trim() != "0")
                {
                    FpSpread1.Sheets[0].Cells[4 + i, 7].Value = float.Parse(fpset6.Tables[i.ToString()].Rows[0][0].ToString()).ToString("0.0000");
                }
                else
                {
                    FpSpread1.Sheets[0].Cells[4 + i, 7].Value = fpset6.Tables[i.ToString()].Rows[0][0].ToString();
                }
                da6.Dispose();
            }
            double[] hj = new double[] { 0, 0, 0, 0, 0, 0 };

            int rcount = FpSpread1.Sheets[0].Rows.Count;
            int ccount = FpSpread1.Sheets[0].Columns.Count;

            for (int j = 2; j < ccount; j++)
            {
                for (int i = 4; i < rcount - 1; i++)
                {
                    if (isNotFloat.Isdouble(FpSpread1.Sheets[0].Cells[i, j].Value.ToString()))

                        hj[j - 2] = hj[j - 2] + double.Parse(FpSpread1.Sheets[0].Cells[i, j].Value.ToString());
                    else
                        hj[j - 2] = hj[j - 2] + 0;
                }
                if ((j == 2 || j == 3) && hj[j - 2] != 0)
                    FpSpread1.Sheets[0].Cells[rcount - 1, j].Value = hj[j - 2].ToString("0.0000");
                else if (hj[j - 2] != 0)
                    FpSpread1.Sheets[0].Cells[rcount - 1, j].Value = hj[j - 2].ToString("0.00");
                else
                    FpSpread1.Sheets[0].Cells[rcount - 1, j].Value = "";
                FpSpread1.Sheets[0].Cells[rcount - 1, j].Font.Size = 9;


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

            FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao29.xls");


        }

    }
}
