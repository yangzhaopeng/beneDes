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
    public partial class biao18 : beneDesYGS.core.UI.corePage
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
            string list = _getParam("YCLX");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            string list2 = _getParam("CYC");
            string listN = _getParam("CYC_NAME");
            string typeid = _getParam("targetType");

            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["ygsConnectionString"].ConnectionString);
            string fpselect = "select distinct hsjb,kcljb,hsjbmc,kcljbmc from rv_stat_qkhsjbhz_frame order by hsjb,kcljb";

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
                for (int i = 1; i <= 4; i++)
                {

                    if (list2 == "[a-zA-Z]")
                    {
                        selectvalue = "select distinct round(value1,4),rv_stat_qkhsjbhz_frame.hsjb,rv_stat_qkhsjbhz_frame.kcljb,item from view_kcclxyflhz_all,rv_stat_qkhsjbhz_frame where bny='" + bny + "' and eny='" + eny + "' and yclx='" + list + "' and item<>'cyoulbl'";
                        selectvalue += " and rv_stat_qkhsjbhz_frame.hsjb=view_kcclxyflhz_all.hsjb and rv_stat_qkhsjbhz_frame.kcljb=view_kcclxyflhz_all.kcljb and rv_stat_qkhsjbhz_frame.hsjb='" + Fprow[0].ToString() + "' and rv_stat_qkhsjbhz_frame.kcljb='" + Fprow[1].ToString() + "' and view_kcclxyflhz_all.xyjb='" + i.ToString() + "'";
                        selectvalue += "  order by item";
                    }
                    else
                    {
                        selectvalue = "select distinct round(value1,4),rv_stat_qkhsjbhz_frame.hsjb,rv_stat_qkhsjbhz_frame.kcljb,item from view_kcclxyflhz,rv_stat_qkhsjbhz_frame where bny='" + bny + "' and eny='" + eny + "' and yclx='" + list + "' and item<>'cyoulbl'";
                        selectvalue += " and rv_stat_qkhsjbhz_frame.hsjb=view_kcclxyflhz.hsjb and rv_stat_qkhsjbhz_frame.kcljb=view_kcclxyflhz.kcljb and rv_stat_qkhsjbhz_frame.hsjb='" + Fprow[0].ToString() + "' and rv_stat_qkhsjbhz_frame.kcljb='" + Fprow[1].ToString() + "' and view_kcclxyflhz.xyjb='" + i.ToString() + "'";
                        selectvalue += " and cyc_id = (select dep_id from department where dep_id =  '" + list2 + "') ";
                        selectvalue += "  order by item";
                    }


                    OracleDataAdapter davalue = new OracleDataAdapter(selectvalue, connfp);
                    DataSet valueset = new DataSet();
                    davalue.Fill(valueset, "value1");
                    if (valueset.Tables["value1"].Rows.Count != 0)
                    {
                        Fprow[4 + (i - 1) * 3] = valueset.Tables["value1"].Rows[0][0];
                        Fprow[3 + 3 * i] = valueset.Tables["value1"].Rows[1][0];
                    }
                }
                fptable.Rows.Add(Fprow);
            }
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
                this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + listN + list + "经济效益分类汇总表";
                this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                this.FpSpread1.Sheets[0].RowHeader.Visible = false;


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
            FpSpread1.Sheets[0].RowHeader.Visible = true;
            FpSpread1.Sheets[0].ColumnHeader.Visible = true;
            FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao18.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;

        }

    }
}
