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

namespace beneDesCYC.view.oilAssessment.cbfxb
{
    public partial class biao11 : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
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

        protected void initSpread()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/dongtai.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表20-油井操作成本分级表");

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
            string path = "../../../static/excel/dongtai.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表21-油田(区块)操作成本分级表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

            if (isempty() == 0)
            { Response.Write("<script>alert('无数据')</script>"); }
            else
            { sj2(); }

            //}
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

            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);

            string fpselect = " select a.grade_name,c.grade_name,b.js,b.cyoul,b.pjczcb, b.jqczcb from stat_grade a,stat_grade c, ";
            fpselect += " (Select nvl(sdy.czcbdljb,'80000') as czcbdljb,nvl(sdy.czcbjb,'70000') As czcbjb, ";
            fpselect += " round(Sum(djisopen),2) As js, ";
            fpselect += " round(Sum(hscyl)/10000,4) As cyoul, ";
            fpselect += " round((Case When Sum(dtl)=0 Then 0 Else Sum(czcb_my)/Sum(dtl) End ),4) As pjczcb, ";
            //fpselect += " round(Sum(djisopen),2) As jqczcb ";
            fpselect += " round(sum(sum(czcb_my))over(Order BY  sdy.czcbdljb,sdy.czcbjb) / Sum(sum(dtl)) over(Order BY  sdy.czcbdljb,sdy.czcbjb),4) As jqczcb ";

            fpselect += " From dtstat_djsj sdy  ";
            fpselect += " Where sdy.djisopen = 1  and qkxyjb <> 99 and pjdyxyjb <> 99 and sdy.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " Group By rollup(sdy.czcbdljb,sdy.czcbjb)) b ";
            fpselect += " where ";
            fpselect += " b.czcbjb = c.grade ";
            fpselect += " and b.czcbdljb = a.grade  ";
            fpselect += " order by czcbdljb,czcbjb ";


            try
            {
                connfp.Open();

                OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                DataSet fpset = new DataSet();
                da.Fill(fpset, "fpdata");


                //此处用于绑定数据            
                #region
                int rcount = fpset.Tables["fpdata"].Rows.Count;
                int ccount = fpset.Tables["fpdata"].Columns.Count;
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
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }
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
                    //将合计行小计去掉
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].Rows.Count - 1, 1].Value = " ";
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].Rows.Count - 1, 1].Font.Size = 9;

                    FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
                }
                else//不为空
                {
                    string path = Page.MapPath("/static/excel/dongtai.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "表20-油井操作成本分级表");

                    /////////////////////
                    //OracleConnection con0 = DB.CreatConnection();
                    //con0.Open();
                    //string cycid = Session["cyc"].ToString();
                    //string cycname = "";
                    //string scyc = "select dep_name from department where dep_id = '" + cycid + "'";
                    //OracleCommand comcyc = new OracleCommand(scyc, con0);
                    //OracleDataReader drcyc = comcyc.ExecuteReader();
                    //drcyc.Read();
                    //if (drcyc.HasRows)
                    //{
                    //    cycname = drcyc[0].ToString();
                    //}
                    //drcyc.Close();
                    //con0.Close();

                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + "  " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;
                    /////////////////////
                  //  FpSpread1.Sheets[0].Cells[1, 0].Value = tbdw;      ////////09.5.13  3-3
                    FpSpread1.Sheets[0].Cells[1, 0].Font.Size = 9;     ////////09.5.13  3-3

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }
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
                    //将合计行小计去掉
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].Rows.Count - 1, 1].Value = " ";
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].Rows.Count - 1, 1].Font.Size = 9;

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

        protected void sj2()
        {
            //JObject obj = new JObject();
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            string list = _getParam("CYC");
            string typeid = _getParam("targetType");

            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);

            string fpselect = "";
            if (typeid == "qk")
            {
                fpselect = " select a.grade_name,c.grade_name,b.gs,b.dydzcl,b.dykccl,b.sykccl,b.cyoul,b.pjczcb,b.jqczcb from stat_grade a,stat_grade c,  ";
                fpselect += " (Select nvl(pj.czcbdljb,'80000') as czcbdljb,nvl(pj.czcbjb,'70000') As czcbjb, ";
                fpselect += " Count(mc) As gs, ";
                fpselect += " round(Sum(dydzcl),4) As dydzcl, ";
                fpselect += " round(Sum(dykccl),4) As dykccl, ";
                fpselect += " round(Sum(sykccl),4) As sykccl, ";
                fpselect += " round(Sum(cyoul)/10000,4) As cyoul, ";
                fpselect += " round((Case When Sum(dtl)=0 Then 0 Else Sum(czcb_my)/Sum(dtl) End ),4) As pjczcb ,";
                fpselect += " round(Sum( Sum(czcb_my))over(Order By pj.czcbdljb,pj.czcbjb)/Sum(Sum(dtl))over(Order By pj.czcbdljb,pj.czcbjb),4) As jqczcb ";
                fpselect += " From dtstat_qksj pj ";
                fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1  and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
                fpselect += " group by rollup(pj.czcbdljb,pj.czcbjb)) b ";
                fpselect += " where ";
                fpselect += " b.czcbjb = c.grade ";
                fpselect += " and b.czcbdljb = a.grade  ";
                fpselect += " order by czcbdljb,czcbjb ";
            }
            else
            {
                fpselect = " select a.grade_name,c.grade_name,b.gs,b.dydzcl,b.dykccl,b.sykccl,b.cyoul,b.pjczcb,b.jqczcb from stat_grade a,stat_grade c,  ";
                fpselect += " (Select nvl(pj.czcbdljb,'80000') as czcbdljb,nvl(pj.czcbjb,'70000') As czcbjb, ";
                fpselect += " Count(mc) As gs, ";
                fpselect += " round(Sum(dydzcl),4) As dydzcl, ";
                fpselect += " round(Sum(dykccl),4) As dykccl, ";
                fpselect += " round(Sum(sykccl),4) As sykccl, ";
                fpselect += " round(Sum(cyoul)/10000,4) As cyoul, ";
                fpselect += " round((Case When Sum(dtl)=0 Then 0 Else Sum(czcb_my)/Sum(dtl) End ),4) As pjczcb ,";
                fpselect += " round(Sum( Sum(czcb_my))over(Order By pj.czcbdljb,pj.czcbjb)/Sum(Sum(dtl))over(Order By pj.czcbdljb,pj.czcbjb),4) As jqczcb ";
                fpselect += " From dtstat_pjdysj pj ";
                fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1  and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
                fpselect += " group by rollup(pj.czcbdljb,pj.czcbjb)) b ";
                fpselect += " where ";
                fpselect += " b.czcbjb = c.grade ";
                fpselect += " and b.czcbdljb = a.grade  ";
                fpselect += " order by czcbdljb,czcbjb ";
            }


            try
            {
                connfp.Open();

                OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                DataSet fpset = new DataSet();
                da.Fill(fpset, "fpdata");


                //此处用于绑定数据            
                #region
                int rcount = fpset.Tables["fpdata"].Rows.Count;
                int ccount = fpset.Tables["fpdata"].Columns.Count;
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
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }
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
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].Rows.Count - 1, 1].Value = " ";
                    FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
                }
                else//不为空
                {
                    string path = Page.MapPath("/static/excel/dongtai.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "表21-油田(区块)操作成本分级表");

                    /////////////////////
                    //OracleConnection con0 = DB.CreatConnection();
                    //con0.Open();
                    //string cycid = Session["cyc"].ToString();
                    //string cycname = "";
                    //string scyc = "select dep_name from department where dep_id = '" + cycid + "'";
                    //OracleCommand comcyc = new OracleCommand(scyc, con0);
                    //OracleDataReader drcyc = comcyc.ExecuteReader();
                    //drcyc.Read();
                    //if (drcyc.HasRows)
                    //{
                    //    cycname = drcyc[0].ToString();
                    //}
                    //drcyc.Close();
                    //con0.Close();

                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + "  " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;
                    /////////////////////

                 //   FpSpread1.Sheets[0].Cells[1, 0].Value = tbdw;      ////////09.5.13  3-3
                    FpSpread1.Sheets[0].Cells[1, 0].Font.Size = 9;     ////////09.5.13  3-3

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }
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
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].Rows.Count - 1, 1].Value = " ";
                    FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
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
            string typeid = _getParam("targetType");
            if (typeid == "yt")
            { FarpointGridChange.FarPointChange(FpSpread1, "dongtai_biao20.xls"); }
            else
            { FarpointGridChange.FarPointChange(FpSpread1, "dongtai_biao21.xls"); }

        }

    }

}
