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
    public partial class bjjwxjxyfxb : beneDesCYC.core.UI.corePage
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
            this.FpSpread1.Sheets[0].OpenExcel(path, "表33-边际、无效井效益分析表");

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

            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");

            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);
            connfp.Open();
            string fpselect = "";
            OracleCommand mycomm = new OracleCommand();
            mycomm.Connection = connfp;
            mycomm.CommandType = CommandType.StoredProcedure;
            string year = eny.Substring(0, 4);
           // mycomm.CommandText = "owcbs_analyse.up_bjwxjxyfx('" + year + "')";
            mycomm.CommandText = "owcbs_analyse.up_bjwxjxyfx('" + year + "','" + Session["cyc_id"].ToString() + "')";
            int reflectrows = Convert.ToInt32(mycomm.ExecuteNonQuery().ToString());
            if (reflectrows == 0)
            {
                Response.Write("<script>alert('不存在该项记录!')</script>");
            }
            else
            {
                fpselect = "select xylb,jglb,jg,sxlb,rcy1,rcy2,rcy3,rcy4,rcy5,js,ncy,zdyxf,zdyxf_my from tmp_bjwxjxyfx  where cyc_id='" + Session["cyc_id"].ToString() + "' ";
                fpselect += " union ";
                fpselect += "select 4 as xylb,2 as jglb,0 as jg,4 as sxlb,sum(rcy1) as rcy1,sum(rcy2) as rcy2,sum(rcy3) as rcy3,sum(rcy4) as rcy4,sum(rcy5) as rcy5,sum(js) as js,sum(ncy) as ncy,sum(zdyxf) as zdyxf,sum(zdyxf_my) as rcyzdyxf_my from tmp_bjwxjxyfx where cyc_id='" + Session["cyc_id"].ToString() + "' and xylb='4' and jglb='1'";
                fpselect += " group by xylb,jglb";
                fpselect += " union ";
                fpselect += "select 4 as xylb,5 as jglb,0 as jg,4 as sxlb,sum(rcy1) as rcy1,sum(rcy2) as rcy2,sum(rcy3) as rcy3,sum(rcy4) as rcy4,sum(rcy5) as rcy5,sum(js) as js,sum(ncy) as ncy,sum(zdyxf) as zdyxf,sum(zdyxf_my) as rcyzdyxf_my from tmp_bjwxjxyfx where cyc_id='" + Session["cyc_id"].ToString() + "' and xylb='4' and jglb='4'";
                fpselect += " group by xylb,jglb";
                fpselect += " union ";
                fpselect += "select 5 as xylb,2 as jglb,0 as jg,4 as sxlb,sum(rcy1) as rcy1,sum(rcy2) as rcy2,sum(rcy3) as rcy3,sum(rcy4) as rcy4,sum(rcy5) as rcy5,sum(js) as js,sum(ncy) as ncy,sum(zdyxf) as zdyxf,sum(zdyxf_my) as rcyzdyxf_my from tmp_bjwxjxyfx where cyc_id='" + Session["cyc_id"].ToString() + "' and xylb='5' and jglb='1'";
                fpselect += " group by xylb,jglb";
                fpselect += " union ";
                fpselect += "select 5 as xylb,5 as jglb,0 as jg,4 as sxlb,sum(rcy1) as rcy1,sum(rcy2) as rcy2,sum(rcy3) as rcy3,sum(rcy4) as rcy4,sum(rcy5) as rcy5,sum(js) as js,sum(ncy) as ncy,sum(zdyxf) as zdyxf,sum(zdyxf_my) as rcyzdyxf_my from tmp_bjwxjxyfx where cyc_id='" + Session["cyc_id"].ToString() + "' and xylb='5' and jglb='4'";
                fpselect += " group by xylb,jglb";
                fpselect += " order by xylb,jglb,sxlb";
            }
            try
            {
                OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                DataSet fpset = new DataSet();
                da.Fill(fpset, "fpdata");


                //此处用于绑定数据
                #region
                int rcount = fpset.Tables["fpdata"].Rows.Count;
                int ccount = fpset.Tables["fpdata"].Columns.Count - 4;
                int hcount = 4;
                //FpSpread1.ColumnHeader.Visible = false;
                //FpSpread1.RowHeader.Visible = false;
                if (FpSpread1.Sheets[0].Rows.Count == hcount)
                {
                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j + 4].Value = fpset.Tables["fpdata"].Rows[i][j + 4].ToString();
                            if ((j == 10 || j == 11 || j == 12) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j + 4].ToString()) && fpset.Tables["fpdata"].Rows[i][j + 4].ToString().Trim() != "0")
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j + 4].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j + 4].ToString()).ToString("0.0000");
                            }
                            else
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j + 4].Value = fpset.Tables["fpdata"].Rows[i][j + 4].ToString();
                            }
                        }
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        FpSpread1.Sheets[0].Cells[i + hcount, 2].Value = fpset.Tables["fpdata"].Rows[i][2].ToString();
                    }
                    for (int i = 8; i < 11; i++)
                    {
                        FpSpread1.Sheets[0].Cells[i + hcount, 2].Value = fpset.Tables["fpdata"].Rows[i][2].ToString();
                    }

                }
                else//不为空
                {
                    string path = "../../../static/excel/jdniandu.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表33-边际、无效井效益分析表");

                    this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    this.FpSpread1.Sheets[0].RowHeader.Visible = false;


                    this.FpSpread1.Sheets[0].Cells[1, 0].Value = tbdw;      ////////09.5.13  3-3
                    this.FpSpread1.Sheets[0].Cells[1, 0].Font.Size = 9;     ////////09.5.13  3-3

                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j + 4].Value = fpset.Tables["fpdata"].Rows[i][j + 4].ToString();
                            if ((j == 10 || j == 11 || j == 12) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j + 4].ToString()) && fpset.Tables["fpdata"].Rows[i][j + 4].ToString().Trim() != "0")
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j + 4].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j + 4].ToString()).ToString("0.0000");
                            }
                            else
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j + 4].Value = fpset.Tables["fpdata"].Rows[i][j + 4].ToString();
                            }
                            FpSpread1.Sheets[0].Cells[i + hcount, j + 4].Font.Size = 9;
                        }
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        FpSpread1.Sheets[0].Cells[i + hcount, 2].Value = fpset.Tables["fpdata"].Rows[i][2].ToString();
                    }
                    for (int i = 8; i < 11; i++)
                    {
                        FpSpread1.Sheets[0].Cells[i + hcount, 2].Value = fpset.Tables["fpdata"].Rows[i][2].ToString();
                    }
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

        protected static string tbdw = "";

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

            FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao33.xls");


        }

    }
}

