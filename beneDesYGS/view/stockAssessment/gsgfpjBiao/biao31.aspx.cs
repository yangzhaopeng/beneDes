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
    public partial class biao31 : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string list = _getParam("CYC");
                if (list == "null")
                { initSpread2(); }
                else
                { initSpread(); }
            }
        }

        protected void initSpread()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/jdniandu.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表31-边际效益、无效益井成因分析表");

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
            this.FpSpread1.Sheets[0].OpenExcel(path, "表31-边际效益、无效益井成因分析表");

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
            string list = _getParam("CYC");
            string[] cyc_ids = _getParamArr("CYC");
            string as_cyc = cyc_ids[0];
            string as_cqc = cyc_ids[0];
            if (cyc_ids.Length > 1)
                as_cqc = cyc_ids[1];

            SqlHelper conn = new SqlHelper();
            OracleConnection connfp = conn.GetConn();
            
            string fpselectsecond = "select distinct xy.xymc,fx.fxyymc,xy.xyjb,fx.fxyydm from fxyy_info fx,gsxylb_info xy where xy.xyjb='4' or xy.xyjb='5' order by xy.xyjb,fx.fxyydm";

            connfp.Open();
            OracleCommand myComm = new OracleCommand(fpselectsecond, connfp);
            OracleDataReader myReader = myComm.ExecuteReader();
            //DataTable Fptable用来输出数据
            DataTable Fptable = new DataTable("fpdata");
            Fptable.Columns.Add("xymc", typeof(string));
            Fptable.Columns.Add("fxyymc", typeof(string));
            Fptable.Columns.Add("js", typeof(int));
            Fptable.Columns.Add("jhb", typeof(float));
            Fptable.Columns.Add("cyl", typeof(float));
            Fptable.Columns.Add("cylb", typeof(float));
            Fptable.Columns.Add("zdyxf", typeof(float));
            Fptable.Columns.Add("zdyxfb", typeof(float));
            DataRow Fprow;
            while (myReader.Read())
            {
                Fprow = Fptable.NewRow();

                Fprow[0] = myReader[0];
                Fprow[1] = myReader[1];
                string dmfxyy = myReader[3].ToString();

                string fpselect = "select nvl(sum(djisopen),0) as js, 0 as jhb, ";
                fpselect += " round(nvl(sum(nd.hscyl),0)/10000,4) as cyl,  0 as cylb, ";
                fpselect += " round(nvl(sum(nd.zdyxf),0)/10000,4) as zdyxf,0 as zdyxfb ";
                fpselect += " from jdstat_djsj nd,kfsj kf,department d ";
                fpselect += " where nd.jh=kf.jh(+)";
                if (dmfxyy == "1")
                    fpselect += " and nd.hs> (select num from csdy_info where ny='" + Session["month"].ToString() + "' and name='高含水')";
                else if (dmfxyy == "2")
                {
                    fpselect += " and nd.hs<= (select num from csdy_info where ny='" + Session["month"].ToString() + "' and name='高含水')";
                    fpselect += " and nd.hscyl< (select num from csdy_info where ny='" + Session["month"].ToString() + "' and name='低产能')";
                }
                else if (dmfxyy == "3")
                {
                    fpselect += " and nd.hs<= (select num from csdy_info where ny='" + Session["month"].ToString() + "' and name='高含水')";
                    fpselect += " and nd.hscyl>=(select num from csdy_info where ny='" + Session["month"].ToString() + "' and name='低产能')";
                    fpselect += " and (nd.jxzyf-nd.whxjxzyf)>10000";
                }
                //else if (dmfxyy == "9")
                //    fpselect += " and kf.fxyydm is null";
                else
                    fpselect += " and kf.fxyydm='" + dmfxyy + "'";
                fpselect += " and nd.gsxyjb = '" + myReader[2].ToString() + "' and nd.eny=kf.ny(+) and ";
                fpselect += " nd.djisopen = '1' ";
                fpselect += " and regexp_like(nd.dep_id,'" + as_cyc + "')  and nd.dep_id = d.dep_id and d.dep_type='CYC' ";

                

                OracleCommand myCommunion = new OracleCommand(fpselect, connfp);
                OracleDataReader myReaderunion = myCommunion.ExecuteReader();
                while (myReaderunion.Read())
                {
                    Fprow[2] = myReaderunion[0];
                    Fprow[3] = myReaderunion[1];
                    Fprow[4] = myReaderunion[2];
                    Fprow[5] = myReaderunion[3];
                    Fprow[6] = myReaderunion[4];
                    Fprow[7] = myReaderunion[5];
                }
                myReaderunion.Close();
                myCommunion.Clone();

                Fptable.Rows.Add(Fprow);
            }
            myReader.Close();
            myComm.Clone();

            try
            {
                DataSet fpset = new DataSet();
                fpset.Tables.Add(Fptable);

                if (fpset.Tables["fpdata"].Rows.Count > 0)
                {
                    //直接在数据集里添加合计行，计算比例
                    double[] he = new double[6];

                    DataRow r;
                    DataRow rw;
                    int w = 0;

                    #region
                    //初始化合计行
                    //he[0] = double.Parse(fpset.Tables["fpdata"].Rows[0][2].ToString());
                    //he[2] = double.Parse(fpset.Tables["fpdata"].Rows[0][4].ToString());
                    //he[4] = double.Parse(fpset.Tables["fpdata"].Rows[0][6].ToString());
                    int fprow = fpset.Tables["fpdata"].Rows.Count;

                    for (int i = 1; i < fprow; i++)
                    {
                        if (fpset.Tables["fpdata"].Rows[i][0].ToString() == fpset.Tables["fpdata"].Rows[i - 1][0].ToString())
                        {
                            //he[0] += double.Parse(fpset.Tables["fpdata"].Rows[i][2].ToString());
                            //he[2] += double.Parse(fpset.Tables["fpdata"].Rows[i][4].ToString());
                            //he[4] += double.Parse(fpset.Tables["fpdata"].Rows[i][6].ToString());
                        }
                        else
                        {
                            string fpselect1 = "select nvl(sum(djisopen),0) as js, 0 as jhb, ";
                            fpselect1 += " round(nvl(sum(nd.hscyl),0)/10000,4) as cyl,  0 as cylb, ";
                            fpselect1 += " round(nvl(sum(nd.zdyxf),0)/10000,4) as zdyxf,0 as zdyxfb ";
                            fpselect1 += " from jdstat_djsj nd";
                            fpselect1 += " where nd.gsxyjb = '4'  ";

                            if (list == "quan")
                            {
                                fpselect1 += " ";
                            }
                            else
                            {
                                fpselect1 += " and nd.dep_id = (select dep_id from department where dep_id =  '" + list + "') ";
                            }
                            OracleCommand myComm2 = new OracleCommand(fpselect1, connfp);
                            OracleDataReader myRead = myComm2.ExecuteReader();
                            myRead.Read();

                            r = fpset.Tables["fpdata"].NewRow();
                            r[0] = fpset.Tables["fpdata"].Rows[i - 1][0];
                            r[1] = "合计";
                            r[2] = myRead[0];
                            r[3] = 100;
                            r[4] = myRead[2];
                            r[5] = 100;
                            r[6] = myRead[4];
                            r[7] = 100;
                            fpset.Tables["fpdata"].Rows.Add(r); myRead.Close();
                            myComm2.Clone();

                            //计算其他原因的个数=总数-各种原因个数
                            fpset.Tables["fpdata"].Rows[i - 1][2] = fpset.Tables["fpdata"].Rows[fprow][2];
                            fpset.Tables["fpdata"].Rows[i - 1][4] = fpset.Tables["fpdata"].Rows[fprow][4];
                            fpset.Tables["fpdata"].Rows[i - 1][6] = fpset.Tables["fpdata"].Rows[fprow][6];
                            for (int t = w; t < i - 1; t++)
                            {
                                fpset.Tables["fpdata"].Rows[i - 1][2] = double.Parse(fpset.Tables["fpdata"].Rows[i - 1][2].ToString()) - double.Parse(fpset.Tables["fpdata"].Rows[t][2].ToString());
                                fpset.Tables["fpdata"].Rows[i - 1][4] = double.Parse(fpset.Tables["fpdata"].Rows[i - 1][4].ToString()) - double.Parse(fpset.Tables["fpdata"].Rows[t][4].ToString());
                                fpset.Tables["fpdata"].Rows[i - 1][6] = double.Parse(fpset.Tables["fpdata"].Rows[i - 1][6].ToString()) - double.Parse(fpset.Tables["fpdata"].Rows[t][6].ToString());
                            }

                            //计算各行比例
                            for (int j = w; j < i; j++)
                            {
                                if (fpset.Tables["fpdata"].Rows[fprow][2].ToString() != "0")
                                    fpset.Tables["fpdata"].Rows[j][3] = Math.Round(double.Parse(fpset.Tables["fpdata"].Rows[j][2].ToString()) / double.Parse(fpset.Tables["fpdata"].Rows[fprow][2].ToString()) * 100, 2);
                                if (fpset.Tables["fpdata"].Rows[fprow][4].ToString() != "0")
                                    fpset.Tables["fpdata"].Rows[j][5] = Math.Round(double.Parse(fpset.Tables["fpdata"].Rows[j][4].ToString()) / double.Parse(fpset.Tables["fpdata"].Rows[fprow][4].ToString()) * 100, 2);
                                if (fpset.Tables["fpdata"].Rows[fprow][6].ToString() != "0")
                                    fpset.Tables["fpdata"].Rows[j][7] = Math.Round(double.Parse(fpset.Tables["fpdata"].Rows[j][6].ToString()) / double.Parse(fpset.Tables["fpdata"].Rows[fprow][6].ToString()) * 100, 2);
                            }
                            w = i;
                        }
                    }
                    ////加无效益合计行
                    //初始化
                    string fpselect2 = "select nvl(sum(djisopen),0) as js, 0 as jhb, ";
                    fpselect2 += " round(nvl(sum(nd.hscyl),0)/10000,4) as cyl,  0 as cylb, ";
                    fpselect2 += " round(nvl(sum(nd.zdyxf),0)/10000,4) as zdyxf,0 as zdyxfb ";
                    fpselect2 += " from jdstat_djsj nd";
                    fpselect2 += " where nd.gsxyjb = '5'  ";
                    if (list == "quan")
                    {
                        fpselect2 += " ";
                    }
                    else
                    {
                        fpselect2 += " and nd.dep_id = (select dep_id from department where dep_id =  '" + list + "') ";
                    }

                    OracleCommand myComm1 = new OracleCommand(fpselect2, connfp);
                    OracleDataReader myRead1 = myComm1.ExecuteReader();
                    myRead1.Read();

                    //he[0] = double.Parse(fpset.Tables["fpdata"].Rows[w][2].ToString());
                    //he[2] = double.Parse(fpset.Tables["fpdata"].Rows[w][4].ToString());
                    //he[4] = double.Parse(fpset.Tables["fpdata"].Rows[w][6].ToString());
                    //for (int m = w + 1; m < fprow; m++)
                    //{
                    //    he[0] += double.Parse(fpset.Tables["fpdata"].Rows[m][2].ToString());
                    //    he[2] += double.Parse(fpset.Tables["fpdata"].Rows[m][4].ToString());
                    //    he[4] += double.Parse(fpset.Tables["fpdata"].Rows[m][6].ToString());
                    //}
                    rw = fpset.Tables["fpdata"].NewRow();
                    rw[0] = fpset.Tables["fpdata"].Rows[fprow - 1][0];
                    rw[1] = "合计";
                    rw[2] = myRead1[0];
                    rw[3] = 100;
                    rw[4] = myRead1[2];
                    rw[5] = 100;
                    rw[6] = myRead1[4];
                    rw[7] = 100;
                    fpset.Tables["fpdata"].Rows.Add(rw);
                    myRead1.Close();
                    myComm1.Clone();

                    //计算其他原因的个数=总数-各种原因个数
                    fpset.Tables["fpdata"].Rows[fprow - 1][2] = fpset.Tables["fpdata"].Rows[fprow + 1][2];
                    fpset.Tables["fpdata"].Rows[fprow - 1][4] = fpset.Tables["fpdata"].Rows[fprow + 1][4];
                    fpset.Tables["fpdata"].Rows[fprow - 1][6] = fpset.Tables["fpdata"].Rows[fprow + 1][6];
                    for (int t = w; t < fprow - 1; t++)
                    {
                        fpset.Tables["fpdata"].Rows[fprow - 1][2] = double.Parse(fpset.Tables["fpdata"].Rows[fprow - 1][2].ToString()) - double.Parse(fpset.Tables["fpdata"].Rows[t][2].ToString());
                        fpset.Tables["fpdata"].Rows[fprow - 1][4] = double.Parse(fpset.Tables["fpdata"].Rows[fprow - 1][4].ToString()) - double.Parse(fpset.Tables["fpdata"].Rows[t][4].ToString());
                        fpset.Tables["fpdata"].Rows[fprow - 1][6] = double.Parse(fpset.Tables["fpdata"].Rows[fprow - 1][6].ToString()) - double.Parse(fpset.Tables["fpdata"].Rows[t][6].ToString());
                    }

                    //计算各行比例
                    for (int n = w; n < fprow; n++)
                    {
                        if (fpset.Tables["fpdata"].Rows[fprow + 1][2].ToString() != "0")
                            fpset.Tables["fpdata"].Rows[n][3] = Math.Round(double.Parse(fpset.Tables["fpdata"].Rows[n][2].ToString()) / double.Parse(fpset.Tables["fpdata"].Rows[fprow + 1][2].ToString()) * 100, 2);
                        if (fpset.Tables["fpdata"].Rows[fprow + 1][4].ToString() != "0")
                            fpset.Tables["fpdata"].Rows[n][5] = Math.Round(double.Parse(fpset.Tables["fpdata"].Rows[n][4].ToString()) / double.Parse(fpset.Tables["fpdata"].Rows[fprow + 1][4].ToString()) * 100, 2);
                        if (fpset.Tables["fpdata"].Rows[fprow + 1][6].ToString() != "0")
                            fpset.Tables["fpdata"].Rows[n][7] = Math.Round(double.Parse(fpset.Tables["fpdata"].Rows[n][6].ToString()) / double.Parse(fpset.Tables["fpdata"].Rows[fprow + 1][6].ToString()) * 100, 2);
                    }
                    #endregion

                    //fpset排序
                    DataTable db = new DataTable();
                    db = fpset.Tables["fpdata"];
                    DataRow[] dr = db.Select("", "xymc");
                    DataTable db1 = db.Clone();
                    db1.Clear();
                    foreach (DataRow row in dr)
                    {
                        db1.ImportRow(row);
                    }
                    db = db1;

                    //此处用于绑定数据            
                    #region
                    int rcount = db.Rows.Count;
                    int ccount = db.Columns.Count;
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
                                //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = db.Rows[i][j].ToString();
                                if ((j == 4 || j == 6) && isNotFloat.Isdouble(db.Rows[i][j].ToString()) && db.Rows[i][j].ToString().Trim() != "0")
                                {
                                    FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(db.Rows[i][j].ToString()).ToString("0.0000");
                                }
                                else if ((j == 3 || j == 5 || j == 7) && isNotFloat.Isdouble(db.Rows[i][j].ToString()) && db.Rows[i][j].ToString().Trim() != "0")
                                {
                                    FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(db.Rows[i][j].ToString()).ToString("0.00");
                                }
                                else
                                {
                                    FpSpread1.Sheets[0].Cells[i + hcount, j].Value = db.Rows[i][j].ToString();
                                }
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            }
                        }
                        //合并单元格
                        int k = 1;  //统计重复单元格
                        w = hcount;  //记录起始位置
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
                        string path = "../../../static/excel/jdniandu.xls";
                        path = Page.MapPath(path);
                        this.FpSpread1.Sheets[0].OpenExcel(path, "表31-边际效益、无效益井成因分析表");

                        this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;

                        FpSpread1.Sheets[0].AddRows(hcount, rcount);
                        for (int i = 0; i < rcount; i++)
                        {
                            for (int j = 0; j < ccount; j++)
                            {
                                //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = db.Rows[i][j].ToString();
                                if ((j == 4 || j == 6) && isNotFloat.Isdouble(db.Rows[i][j].ToString()) && db.Rows[i][j].ToString().Trim() != "0")
                                {
                                    FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(db.Rows[i][j].ToString()).ToString("0.0000");
                                }
                                else if ((j == 3 || j == 5 || j == 7) && isNotFloat.Isdouble(db.Rows[i][j].ToString()) && db.Rows[i][j].ToString().Trim() != "0")
                                {
                                    FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(db.Rows[i][j].ToString()).ToString("0.00");
                                }
                                else
                                {
                                    FpSpread1.Sheets[0].Cells[i + hcount, j].Value = db.Rows[i][j].ToString();
                                }

                                FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            }
                        }
                        //合并单元格
                        int k = 1;  //统计重复单元格
                        w = hcount;  //记录起始位置
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
                else
                {
                    Response.Write("<script>alert('没有相关数据')</script>");
                }
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
            FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao31.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;

        }

    }
}
