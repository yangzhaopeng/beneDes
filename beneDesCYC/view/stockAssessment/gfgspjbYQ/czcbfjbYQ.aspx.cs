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

namespace beneDesCYC.view.stockAssessment.gfgspjbYQ
{
    public partial class czcbfjbYQ : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string typeid = _getParam("targetType");
            initSpread(typeid);
        }


        protected void initSpread(string typeid)
        {

            string path = "../../../static/excel/jdnianduYQ.xls";
            path = Page.MapPath(path);
            switch (typeid)
            {
                case "yt":
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表15-单井操作成本分级表"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    }
                    break;
                case "qk":
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表15-区块操作成本分级表"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    }
                    break;
                case "pjdy":
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表15-区块操作成本分级表"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    }
                    break;
                default:
                    break;
            }
            //判断模板是否加载
            if (FpSpread1.Sheets[0].Rows.Count != 4)
            {
                Response.Write("<script>alert('加载模板有问题，请检查模板文件是否正确！')</script>");
            }
            else
            {
                if (isempty() == 0)
                { Response.Write("<script>alert('无数据')</script>"); }
                else
                { sj(typeid); }
            }
        }
        protected void sj(string typeid)
        {
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            DataSet ds = new DataSet();
            var param_bny = new OracleParameter("as_bny", OracleType.VarChar);
            param_bny.Value = bny;
            var param_eny = new OracleParameter("as_eny", OracleType.VarChar);
            param_eny.Value = eny;
            var param_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc.Value = Session["cyc_id"].ToString();
            var param_cqc = new OracleParameter("as_cqc", OracleType.VarChar);
            param_cqc.Value = Session["cqc_id"].ToString();
            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;



            if (typeid == "yt")
            {
                ds = sql.GetDataSet("OWCBS_VIEW_CZCBFJB.CZCBFJB_DJ_PROC", CommandType.StoredProcedure,
                   new OracleParameter[] { param_cyc, param_cqc, param_out });
                FillDJSpread(ds);
            }
            else if (typeid == "qk")
            {
                ds = sql.GetDataSet("OWCBS_VIEW_CZCBFJB.CZCBFJB_QK_PROC", CommandType.StoredProcedure,
                         new OracleParameter[] { param_bny, param_eny, param_cyc, param_cqc, param_out });
                FillQKSpread(ds);
            }
            else if (typeid == "pjdy")
            {
                ds = sql.GetDataSet("OWCBS_VIEW_CZCBFJB.CZCBFJB_PJDY_PROC", CommandType.StoredProcedure,
            new OracleParameter[] { param_bny, param_eny, param_cyc, param_cqc, param_out });
                FillQKSpread(ds);
            }
        }

        private void FillDJSpread(DataSet ds)
        {
            int rcount = ds.Tables[0].Rows.Count;
            int ccount = ds.Tables[0].Columns.Count;
            int hcount = 4;

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
            FpSpread1.Width = Unit.Pixel(1133);
        }

        private void FillQKSpread(DataSet ds)
        {
            int rcount = ds.Tables[0].Rows.Count;
            int ccount = ds.Tables[0].Columns.Count;
            int hcount = 4;

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
            FpSpread1.Width = Unit.Pixel(1133);
        }
        protected void initSpread()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/jdnianduQC.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表15-油井操作成本分级表");

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
            string path = "../../../static/excel/jdnianduQC.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表16-油田(区块)操作成本分级表");

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

            string fpselect = " select a.grade_name,c.grade_name,b.js,b.cyoul,b.pjczcb from stat_grade a,stat_grade c, ";
            fpselect += " (Select nvl(sdy.czcbdljb,'80000') as czcbdljb,nvl(sdy.czcbjb,'70000') As czcbjb, ";
            fpselect += " round(Sum(djisopen),2) As js, ";
            fpselect += " round(Sum(hscql)/100000,4) As cyoul, ";
            fpselect += " round((Case When Sum(trqspl)=0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),4) As pjczcb ";
            //fpselect += " round(sum(sum(czcb_my))over(Order BY  sdy.czcbdljb,sdy.czcbjb) / Sum(sum(dtl)) over(Order BY  sdy.czcbdljb,sdy.czcbjb),4) As jqczcb ";
            fpselect += " From jdstat_djsj sdy  ";
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
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].Rows.Count - 1, 1].Value = " ";
                    FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
                }
                else//不为空
                {
                    string path = "../../../static/excel/jdniandu.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表15-油井操作成本分级表");


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

                    this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + "  " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                    /////////////////////

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
                fpselect = " select a.grade_name,c.grade_name,b.gs,b.dydzcl,b.dykccl,b.sykccl,b.cyoul,b.pjczcb from stat_grade a,stat_grade c,  ";
                fpselect += " (Select nvl(pj.czcbdljb,'80000') as czcbdljb,nvl(pj.czcbjb,'70000') As czcbjb, ";
                fpselect += " Count(mc) As gs, ";
                fpselect += " round(Sum(dydzcl),4) As dydzcl, ";
                fpselect += " round(Sum(dykccl),4) As dykccl, ";
                fpselect += " round(Sum(sykccl),4) As sykccl, ";
                fpselect += " round(Sum(cql)/100000,4) As cyoul, ";
                fpselect += " round((Case When Sum(trqspl)=0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),4) As pjczcb ";
                //fpselect += " round(Sum( Sum(czcb_my))over(Order By pj.czcbdljb,pj.czcbjb)/Sum(Sum(dtl))over(Order By pj.czcbdljb,pj.czcbjb),4) As jqczcb ";
                fpselect += " From jdstat_qksj pj ";
                fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1  and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
                fpselect += " group by rollup(pj.czcbdljb,pj.czcbjb)) b ";
                fpselect += " where ";
                fpselect += " b.czcbjb = c.grade ";
                fpselect += " and b.czcbdljb = a.grade  ";
                fpselect += " order by czcbdljb,czcbjb ";
            }
            else
            {
                fpselect = " select a.grade_name,c.grade_name,b.gs,b.dydzcl,b.dykccl,b.sykccl,b.cyoul,b.pjczcb from stat_grade a,stat_grade c,  ";
                fpselect += " (Select nvl(pj.czcbdljb,'80000') as czcbdljb,nvl(pj.czcbjb,'70000') As czcbjb, ";
                fpselect += " Count(mc) As gs, ";
                fpselect += " round(Sum(dydzcl),4) As dydzcl, ";
                fpselect += " round(Sum(dykccl),4) As dykccl, ";
                fpselect += " round(Sum(sykccl),4) As sykccl, ";
                fpselect += " round(Sum(cql)/100000,4) As cyoul, ";
                fpselect += " round((Case When Sum(trqspl)=0 Then 0 Else Sum(czcb)/Sum(trqspl) End ),4) As pjczcb ";
                //fpselect += " round(Sum( Sum(czcb_my))over(Order By pj.czcbdljb,pj.czcbjb)/Sum(Sum(dtl))over(Order By pj.czcbdljb,pj.czcbjb),4) As jqczcb ";
                fpselect += " From jdstat_pjdysj pj ";
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
                    string path = "../../../static/excel/jdniandu.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表16-油田(区块)操作成本分级表");

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

                    this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + "  " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                    /////////////////////

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
            { FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao15.xls"); }
            else
            { FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao16.xls"); }

        }

    }

}
