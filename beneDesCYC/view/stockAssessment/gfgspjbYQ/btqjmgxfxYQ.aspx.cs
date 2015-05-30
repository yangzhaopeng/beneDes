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

namespace beneDesCYC.view.stockAssessment.gsgfpjBiao
{
    public partial class btqjmgxfxYQ : beneDesCYC.core.UI.corePage
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
            string path = "../../../static/excel/jdnianduYQ.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表14-不同气价敏感性分析表");

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
            string jgsz = _getParam("jgsz");

            string as_cyc = Session["cyc_id"].ToString();
            string as_cqc = Session["cqc_id"].ToString();


            //OracleConnection con1 = DB.CreatConnection();
            //con1.Open();
            SqlHelper conn = new SqlHelper();
            OracleConnection con1 = conn.GetConn();
            con1.Open();
            //string cycselect = "select dep_id from department where dep_name =  '" + DropDownList1.SelectedItem.Text + "'";
            //OracleDataAdapter dacyc = new OracleDataAdapter(cycselect , con1);
            //DataSet dscyc = new DataSet();
            //dacyc.Fill(dscyc, "cyc");
            //string cycid="";
            //if (dscyc.Tables["cyc"].Rows.Count != 0)
            //    cycid = dscyc.Tables["cyc"].Rows[0][0].ToString();

            OracleCommand com1 = new OracleCommand();
            com1.Connection = con1;
            com1.CommandType = CommandType.StoredProcedure;
            com1.CommandText = " owcbs_analyse.up_mgfx_q('" + jgsz + "', ',' ,'" + bny + "' ,'" + eny + "','" + as_cyc + "','" + as_cqc + "')";
            com1.ExecuteNonQuery();

            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);
            connfp.Open();
            DataTable Fptable = new DataTable("fpdata");

            string[] jg = new string[20];
            string str = jgsz;
            int c = str.IndexOf(',');
            int m = 0;
            while (c > 0)
            {
                jg[m] = str.Substring(0, c);
                str = str.Substring(c + 1, str.Length - c - 1);
                c = (int)str.IndexOf(',');
                m = m + 1;
            }
            jg[m] = str;
            for (int jgindex = 0; jgindex <= m; jgindex++)
            {
                Fptable.Columns.Add(jg[jgindex], typeof(float));
            }
            for (int rowN = 0; rowN < 9; rowN++)
            {
                DataRow Fprow = Fptable.NewRow();
                Fptable.Rows.Add(Fprow);
            }
            string fpselect = "";
            for (int columnN = 0; columnN < Fptable.Columns.Count; columnN++)
            {
                fpselect = " select * from(select lb,gsxylb_info.xyjb,nvl(cl,0) as cl from gsxylb_info,mgxfx  where lb='气井' and jg='" + jg[columnN] + "'  and   cyc_id= '" + as_cqc + "'  and gsxylb_info.xyjb=mgxfx.xylb ";               
                fpselect += " order by lb,xylb)t1";

                fpselect += " union";

                fpselect += " select * from(select lb,qkxyjb.xyjb,nvl(cl,0) as cl from qkxyjb,mgxfx  where lb='气田' and jg='" + jg[columnN] + "' and cyc_id= '" + as_cqc + "' and qkxyjb.xyjb=mgxfx.xylb";
              
                fpselect += " order by lb,xylb)t1";
                OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                DataSet ds = new DataSet();
                da.Fill(ds, "tablecl");
                float hj = 0;
                int tableclRcount = ds.Tables["tablecl"].Rows.Count;
                if (tableclRcount == 9)
                {
                    for (int rowsn = 0; rowsn < tableclRcount; rowsn++)
                    {
                        Fptable.Rows[rowsn][columnN] = ds.Tables["tablecl"].Rows[rowsn][2];
                    }
                }
                else
                {
                    for (int rowsn = 0; rowsn < Fptable.Rows.Count; rowsn++)
                    {
                        Fptable.Rows[rowsn][columnN] = 0;
                    }
                }
            }

            try
            {
                DataSet fpset = new DataSet();
                fpset.Tables.Clear();
                fpset.Tables.Add(Fptable);

                //此处用于绑定数据
                #region
                int rcount = fpset.Tables["fpdata"].Rows.Count;
                int ccount = fpset.Tables["fpdata"].Columns.Count;
                int hcount = 5;
                //FpSpread1.ColumnHeader.Visible = false;
                //FpSpread1.RowHeader.Visible = false;
                if (FpSpread1.Sheets[0].Rows.Count == hcount)
                {
                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int rowjg = 2; rowjg < FpSpread1.Sheets[0].ColumnCount; rowjg++)
                    {
                        FpSpread1.Sheets[0].Cells[hcount - 2, rowjg].Value = "";
                    }
                    for (int j = 0; j < Fptable.Columns.Count; j++)
                    {
                        FpSpread1.Sheets[0].Cells[hcount - 2, j + 2].Value = jg[j];
                        float yjhj = 0, ythj = 0;

                        for (int i = 0; i < rcount; i++)
                        {
                            if (i < 5)
                            {
                                string Fpcellvalue = fpset.Tables["fpdata"].Rows[i][j].ToString();
                                if (Fpcellvalue == "")
                                    fpset.Tables["fpdata"].Rows[i][j] = "0";
                                //FpSpread1.Sheets[0].Cells[i + hcount, j + 2].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                                if (isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                                {
                                    FpSpread1.Sheets[0].Cells[i + hcount, j + 2].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                                }
                                else
                                {
                                    FpSpread1.Sheets[0].Cells[i + hcount, j + 2].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                                }
                                FpSpread1.Sheets[0].Cells[i + hcount, j + 2].Font.Size = 9;
                            }

                            if (4 < i && i < rcount)
                            {
                                string Fpcellvalue = fpset.Tables["fpdata"].Rows[i][j].ToString();
                                if (Fpcellvalue == "")
                                    fpset.Tables["fpdata"].Rows[i][j] = "0";
                                //FpSpread1.Sheets[0].Cells[i + hcount + 1, j + 2].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                                if (isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                                {
                                    FpSpread1.Sheets[0].Cells[i + hcount + 1, j + 2].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                                }
                                else
                                {
                                    FpSpread1.Sheets[0].Cells[i + hcount + 1, j + 2].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                                }
                                FpSpread1.Sheets[0].Cells[i + hcount + 1, j + 2].Font.Size = 9;
                            }
                        }
                        yjhj = float.Parse(fpset.Tables["fpdata"].Rows[0][j].ToString()) + float.Parse(fpset.Tables["fpdata"].Rows[1][j].ToString()) + float.Parse(fpset.Tables["fpdata"].Rows[2][j].ToString()) + float.Parse(fpset.Tables["fpdata"].Rows[3][j].ToString()) + float.Parse(fpset.Tables["fpdata"].Rows[4][j].ToString());
                        FpSpread1.Sheets[0].Cells[5 + hcount, j + 2].Value = yjhj.ToString();
                        FpSpread1.Sheets[0].Cells[5 + hcount, j + 2].Font.Size = 9;

                        ythj = float.Parse(fpset.Tables["fpdata"].Rows[5][j].ToString()) + float.Parse(fpset.Tables["fpdata"].Rows[6][j].ToString()) + float.Parse(fpset.Tables["fpdata"].Rows[7][j].ToString()) + float.Parse(fpset.Tables["fpdata"].Rows[8][j].ToString());
                        FpSpread1.Sheets[0].Cells[10 + hcount, j + 2].Value = ythj.ToString();
                        FpSpread1.Sheets[0].Cells[10 + hcount, j + 2].Font.Size = 9;
                    }
                    //计算精度
                    for (int i = 0; i < rcount + 2; i++)
                        for (int j = 0; j < Fptable.Columns.Count; j++)
                            FpSpread1.Sheets[0].Cells[i + hcount, j + 2].Value = Math.Round(float.Parse(FpSpread1.Sheets[0].Cells[i + hcount, j + 2].Value.ToString()), 4);



                }
                else//不为空
                {
                    string path = "../../../static/excel/jdnianduYQ.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表14-不同气价敏感性分析表");

                    this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    this.FpSpread1.Sheets[0].RowHeader.Visible = false;

                    for (int rowjg = 2; rowjg < FpSpread1.Sheets[0].ColumnCount; rowjg++)
                    {
                        FpSpread1.Sheets[0].Cells[hcount - 2, rowjg].Value = "";
                    }
                    //FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int j = 0; j < Fptable.Columns.Count; j++)
                    {
                        FpSpread1.Sheets[0].Cells[hcount - 2, j + 2].Value = jg[j];
                        float yjhj = 0, ythj = 0;

                        for (int i = 0; i < rcount; i++)
                        {
                            if (i < 5)
                            {
                                string Fpcellvalue = fpset.Tables["fpdata"].Rows[i][j].ToString();
                                if (Fpcellvalue == "")
                                    fpset.Tables["fpdata"].Rows[i][j] = "0";
                                //FpSpread1.Sheets[0].Cells[i + hcount, j + 2].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                                if (isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                                {
                                    FpSpread1.Sheets[0].Cells[i + hcount, j + 2].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                                }
                                else
                                {
                                    FpSpread1.Sheets[0].Cells[i + hcount, j + 2].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                                }
                                FpSpread1.Sheets[0].Cells[i + hcount, j + 2].Font.Size = 9;
                            }

                            if (4 < i && i < rcount)
                            {
                                string Fpcellvalue = fpset.Tables["fpdata"].Rows[i][j].ToString();
                                if (Fpcellvalue == "")
                                    fpset.Tables["fpdata"].Rows[i][j] = "0";
                                //FpSpread1.Sheets[0].Cells[i + hcount + 1, j + 2].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                                if (isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                                {
                                    FpSpread1.Sheets[0].Cells[i + hcount + 1, j + 2].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                                }
                                else
                                {
                                    FpSpread1.Sheets[0].Cells[i + hcount + 1, j + 2].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                                }
                                FpSpread1.Sheets[0].Cells[i + hcount + 1, j + 2].Font.Size = 9;
                            }
                        }
                        yjhj = float.Parse(fpset.Tables["fpdata"].Rows[0][j].ToString()) + float.Parse(fpset.Tables["fpdata"].Rows[1][j].ToString()) + float.Parse(fpset.Tables["fpdata"].Rows[2][j].ToString()) + float.Parse(fpset.Tables["fpdata"].Rows[3][j].ToString()) + float.Parse(fpset.Tables["fpdata"].Rows[4][j].ToString());
                        FpSpread1.Sheets[0].Cells[5 + hcount, j + 2].Value = yjhj.ToString("0.0000");
                        FpSpread1.Sheets[0].Cells[5 + hcount, j + 2].Font.Size = 9;

                        ythj = float.Parse(fpset.Tables["fpdata"].Rows[5][j].ToString()) + float.Parse(fpset.Tables["fpdata"].Rows[6][j].ToString()) + float.Parse(fpset.Tables["fpdata"].Rows[7][j].ToString()) + float.Parse(fpset.Tables["fpdata"].Rows[8][j].ToString());
                        FpSpread1.Sheets[0].Cells[10 + hcount, j + 2].Value = ythj.ToString("0.0000");
                        FpSpread1.Sheets[0].Cells[10 + hcount, j + 2].Font.Size = 9;
                    }
                    //计算精度
                    //for (int i = 0; i < rcount + 2; i++)
                    //{
                    //    for (int j = 0; j < FpSpread1.Sheets[0].Columns.Count-2; j++)
                    //    {
                    //        //if (isNotFloat.Isdouble(FpSpread1.Sheets[0].Cells[i + hcount, j + 2].Value.ToString()) && FpSpread1.Sheets[0].Cells[i + hcount, j + 2].Value.ToString().Trim() != "0")

                    //        FpSpread1.Sheets[0].Cells[i + hcount, j + 2].Value = Math.Round(float.Parse(FpSpread1.Sheets[0].Cells[i + hcount, j + 2].Value.ToString()), 4);
                    //    }
                    //}

                }
                #endregion

            }
            catch (OracleException error)
            {
                //string CuoWu = "错误: " + error.Message.ToString();
                //Response.Write(CuoWu);

            }

            connfp.Close();

            con1.Close();

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

            FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao14.xls");


        }

    }
}
