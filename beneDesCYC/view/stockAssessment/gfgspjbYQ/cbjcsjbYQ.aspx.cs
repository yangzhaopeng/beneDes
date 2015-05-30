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
    public partial class cbjcsjbYQ : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string typeid = _getParam("targetType");
            initSpread(typeid);
            //if (typeid == "yt")
            //{
            //    initSpread();
            //}
            //else
            //{
            //    initSpread2();
            //}

        }

        private void initSpread(string typeid)
        {
            string path = "../../../static/excel/jdnianduYQ.xls";
            path = Page.MapPath(path);
            switch (typeid)
            {
                case "yt":
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表7-单井成本基础数据表"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    }
                    //判断模板是否加载
                    if (FpSpread1.Sheets[0].Rows.Count != 5)
                    {
                        Response.Write("<script>alert('加载模板有问题，请检查模板文件是否正确！')</script>");
                    };
                    break;
                default:   //区块 与 评价单元
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表7-区块成本基础数据"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    }
                    //判断模板是否加载
                    if (FpSpread1.Sheets[0].Rows.Count != 4)
                    {
                        Response.Write("<script>alert('加载模板有问题，请检查模板文件是否正确！')</script>");
                    };
                    break;
            }

            if (isempty() == 0)
            { Response.Write("<script>alert('无数据')</script>"); }
            else
            { sj(typeid); }

        }

        private void sj(string typeid)
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

                ds = sql.GetDataSet("OWCBS_VIEW_CBJCSJB.CBJCSJB_DJ_PROC", CommandType.StoredProcedure,
                   new OracleParameter[] { param_bny, param_eny, param_cyc, param_cqc, param_out });

                FillDJSpread(ds);
            }
            else if (typeid == "qk")
            {

                ds = sql.GetDataSet("OWCBS_VIEW_CBJCSJB.CBJCSJB_QK_PROC", CommandType.StoredProcedure,
                         new OracleParameter[] { param_bny, param_eny, param_cyc, param_cqc, param_out });
                FillQKSpread(ds, typeid);
            }
            else if (typeid == "pjdy")
            {

                ds = sql.GetDataSet("OWCBS_VIEW_CBJCSJB.CBJCSJB_PJDY_PROC", CommandType.StoredProcedure,
            new OracleParameter[] { param_bny, param_eny, param_cyc, param_cqc, param_out });
                FillQKSpread(ds, typeid);
            }


        }

        private void FillQKSpread(DataSet ds, string typeid)
        {
            int rcount = ds.Tables[0].Rows.Count;
            int ccount = ds.Tables[0].Columns.Count;
            int hcount = 4;

            if (FpSpread1.Sheets[0].Rows.Count == hcount)
            {
                FpSpread1.Sheets[0].AddRows(hcount, rcount);
                for (int i = 0; i < rcount; i++)
                {
                    for (int j = 1; j < ccount; j++)
                    {
                        string value = ds.Tables[0].Rows[i][j].ToString();
                        if (string.IsNullOrEmpty(value))
                            if (j == 4)
                                value = "未知";
                            else
                                value = "0";
                        FpSpread1.Sheets[0].Cells[i + hcount, j - 1].Value = value;
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

            //重新写序号
            k = 1;
            w = hcount;
            for (int i = w; i < FpSpread1.Sheets[0].Rows.Count; i++)
            {
                if (FpSpread1.Sheets[0].Cells[i, 2].Value.ToString().Trim() != "合计" && FpSpread1.Sheets[0].Cells[i, 2].Value.ToString().Trim() != "总计")
                {
                    FpSpread1.Sheets[0].Cells[i, 1].Value = k;
                    k++;
                    FpSpread1.Sheets[0].Cells[i, 1].Font.Size = 9;
                }
                else if (FpSpread1.Sheets[0].Cells[i, 2].Value.ToString().Trim() == "合计")
                {
                    FpSpread1.ActiveSheetView.AddSpanCell(i, 1, 1, 2);
                    FpSpread1.Sheets[0].Cells[i, 1].Value = "合计";
                }
                else if (FpSpread1.Sheets[0].Cells[i, 2].Value.ToString().Trim() == "总计")
                {
                    FpSpread1.ActiveSheetView.AddSpanCell(i, 0, 1, 3);
                    FpSpread1.Sheets[0].Cells[i, 0].Value = "总计";
                }
            }
            if (typeid == "pjdy")
            {
                FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text.Replace("区块", "评价单元");
                FpSpread1.Sheets[0].Cells[2, 3].Text = FpSpread1.Sheets[0].Cells[2, 3].Text.Replace("区块", "评价单元");
            }
            else if (typeid == "qk")
            {
                FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text.Replace("评价单元", "区块");
                FpSpread1.Sheets[0].Cells[2, 3].Text = FpSpread1.Sheets[0].Cells[2, 3].Text.Replace("评价单元", "区块");
            }
            FpSpread1.Width = Unit.Pixel(800);
        }

        private void FillDJSpread(DataSet ds)
        {
            int rcount = ds.Tables[0].Rows.Count;
            int ccount = ds.Tables[0].Columns.Count;
            int hcount = 5;  //定义模板的 头行数

            if (FpSpread1.Sheets[0].Rows.Count == hcount)
            {
                FpSpread1.Sheets[0].AddRows(hcount, rcount);
                for (int i = 0; i < rcount; i++)
                {
                    for (int j = 2; j < ccount; j++)
                    {
                        string value = ds.Tables[0].Rows[i][j].ToString();
                        if (string.IsNullOrEmpty(value))
                            value = "0";
                        FpSpread1.Sheets[0].Cells[i + hcount, j - 2].Value = value;
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
            FpSpread1.Width = Unit.Pixel(1233);
        }

        protected void initSpread()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/jdnianduQC.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表7-油井成本基础数据表");

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
            this.FpSpread1.Sheets[0].OpenExcel(path, "表8-区块成本基础数据表");

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

            string fpselect = " Select sdy.pjdyxyjb,sdy.gsxyjb, qks.xymc, gsx.xymc , ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb)/Sum(yqspl) End),2) As czcb, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjzh)/Sum(yqspl) End),2) As zjzh, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl) End),2) As sccb, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else (Sum(qjf)+sum(tbsyj))/Sum(yqspl) End),2) As qjf, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(ktf)/Sum(yqspl) End),2) As ktf, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl)+(Sum(qjf)+sum(tbsyj))/Sum(yqspl)+Sum(ktf)/Sum(yqspl) End),2) As yycb  ";
            //fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(yycb)/Sum(yqspl) End),2) As yycb ";
            fpselect += " From jdstat_djsj sdy, qkxyjb qks, gsxylb_info gsx ";
            fpselect += " where sdy.bny = " + bny + " and sdy.eny =" + eny + " and djisopen = 1 and qkxyjb <> 99 and pjdyxyjb <> 99 ";
            fpselect += " and sdy.pjdyxyjb = qks.xyjb and sdy.gsxyjb = gsx.xyjb and sdy.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " Group By sdy.pjdyxyjb,sdy.gsxyjb, qks.xymc, gsx.xymc ";

            fpselect += " Union ";
            fpselect += " Select sdy.pjdyxyjb,80000 As gsxyjb, qks.xymc, gsx.xymc, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb)/Sum(yqspl) End),2) As czcb, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjzh)/Sum(yqspl) End),2) As zjzh, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl) End),2) As sccb, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else (Sum(qjf)+sum(tbsyj))/Sum(yqspl) End),2) As qjf, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(ktf)/Sum(yqspl) End),2) As ktf, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl)+(Sum(qjf)+sum(tbsyj))/Sum(yqspl)+Sum(ktf)/Sum(yqspl) End),2) As yycb ";
            //fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(yycb)/Sum(yqspl) End),2) As yycb ";
            fpselect += " From jdstat_djsj sdy, qkxyjb qks, gsxylb_info gsx ";
            fpselect += " where sdy.bny = " + bny + " and sdy.eny =" + eny + " and djisopen = 1 and qkxyjb <> 99 and pjdyxyjb <> 99 ";
            fpselect += " and sdy.pjdyxyjb = qks.xyjb   and gsx.xyjb = '80000' and sdy.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " Group By sdy.pjdyxyjb,qks.xymc, gsx.xymc ";

            fpselect += " union ";
            fpselect += " select 90000 as pjdyxyjb,sdy.gsxyjb,qks.xymc, gsx.xymc, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb)/Sum(yqspl) End),2) As czcb, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjzh)/Sum(yqspl) End),2) As zjzh, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl) End),2) As sccb, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else (Sum(qjf)+sum(tbsyj))/Sum(yqspl) End),2) As qjf, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(ktf)/Sum(yqspl) End),2) As ktf, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl)+(Sum(qjf)+sum(tbsyj))/Sum(yqspl)+Sum(ktf)/Sum(yqspl) End),2) As yycb ";
            //fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(yycb)/Sum(yqspl) End),2) As yycb ";
            fpselect += " From jdstat_djsj sdy,  qkxyjb qks, gsxylb_info gsx ";
            fpselect += " where sdy.bny = " + bny + " and sdy.eny =" + eny + " and djisopen = 1 and qkxyjb <> 99 and pjdyxyjb <> 99 ";
            fpselect += " and sdy.gsxyjb = gsx .xyjb   and qks.xyjb = '90000' and sdy.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " Group By sdy.gsxyjb,qks.xymc, gsx.xymc ";

            fpselect += " union ";
            fpselect += " select 90000 as pjdyxyjb, 80000 As gsxyjb,qks.xymc, gsx.xymc, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb)/Sum(yqspl) End),2) As czcb, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjzh)/Sum(yqspl) End),2) As zjzh, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl) End),2) As sccb, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else (Sum(qjf)+sum(tbsyj))/Sum(yqspl) End),2) As qjf, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(ktf)/Sum(yqspl) End),2) As ktf, ";
            fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl)+(Sum(qjf)+sum(tbsyj))/Sum(yqspl)+Sum(ktf)/Sum(yqspl) End),2) As yycb ";
            //fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(yycb)/Sum(yqspl) End),2) As yycb ";
            fpselect += " From jdstat_djsj sdy,qkxyjb qks, gsxylb_info gsx ";
            fpselect += " where sdy.bny = " + bny + " and sdy.eny =" + eny + " and djisopen = 1 and qkxyjb <> 99 and pjdyxyjb <> 99 ";
            fpselect += " and qks.xyjb = '90000' and gsx.xyjb = '80000' and sdy.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " Group By  qks.xymc, gsx.xymc ";

            try
            {
                connfp.Open();

                OracleCommand myComm = new OracleCommand(fpselect, connfp);
                OracleDataReader myReader = myComm.ExecuteReader();
                //DataTable Fptable用来输出数据
                DataTable Fptable = new DataTable("fpdata");
                Fptable.Columns.Add("pjdyxyjb", typeof(string));
                Fptable.Columns.Add("gsxyjb", typeof(System.String));
                Fptable.Columns.Add("czcb", typeof(float));
                Fptable.Columns.Add("zjzh", typeof(float));
                Fptable.Columns.Add("sccb", typeof(float));
                Fptable.Columns.Add("qjf", typeof(float));
                Fptable.Columns.Add("ktf", typeof(float));
                Fptable.Columns.Add("yycb", typeof(float));
                DataRow Fprow;
                while (myReader.Read())
                {
                    Fprow = Fptable.NewRow();

                    Fprow[0] = myReader[2];
                    Fprow[1] = myReader[3];
                    Fprow[2] = myReader.GetValue(4);
                    Fprow[3] = myReader[5];
                    Fprow[4] = myReader.GetValue(6);
                    Fprow[5] = myReader.GetValue(7);
                    Fprow[6] = myReader.GetValue(8);
                    Fprow[7] = myReader.GetValue(9);
                    Fptable.Rows.Add(Fprow);
                }
                myReader.Close();
                myComm.Clone();
                ////把第一,二列重复的项去掉
                //string tmp1 = Fptable.Rows[0][0].ToString();
                //string tmp2 = Fptable.Rows[0][1].ToString();
                //for (int j = 1; j < Fptable.Rows.Count; j++)
                //{
                //    if (Fptable.Rows[j][0].ToString() == tmp1)
                //        Fptable.Rows[j][0] = "";
                //    else
                //        tmp1 = Fptable.Rows[j][0].ToString();
                //    if (Fptable.Rows[j][1].ToString() == tmp2)
                //        Fptable.Rows[j][1] = "";
                //    else
                //        tmp2 = Fptable.Rows[j][1].ToString();
                //} //把第一,二列重复的项去掉

                DataSet fpset = new DataSet();
                fpset.Tables.Add(Fptable);

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
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            if (isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
                            }
                            else
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            }
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }
                    //去掉重复项
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
                    string path = "../../../static/excel/jdniandu.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表7-油井成本基础数据表");

                    /////////////////////
                    //OracleConnection con0 = DB.CreatConnection();
                    //con0.Open();
                    //string cycid = Session["cyc"].ToString();
                    //string cycname = "";
                    //string scyc = "select dep_id from department where dep_id = '" + cycid + "'";
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

                    this.FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            if (isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
                            }
                            else
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            }
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }
                    //去掉重复项
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

            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);


            string fpselect = "";
            if (typeid == "qk")
            {
                fpselect = " Select 1 As od,pj.gsxyjb,gsx.xymc,pj.ssyt,pj.mc as pjdymc, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb)/Sum(yqspl) End),2) As czcb, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjzh)/Sum(yqspl) End),2) As zjzh, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl) End),2) As sccb, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else (Sum(qjf)+sum(tbsyj))/Sum(yqspl) End),2) As qjf, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(ktf)/Sum(yqspl) End),2) As ktf, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl)+(Sum(qjf)+sum(tbsyj))/Sum(yqspl)+Sum(ktf)/Sum(yqspl) End),2) As yycb ";
                //fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(yycb)/Sum(yqspl) End),2) As yycb ";
                fpselect += " From jdstat_qksj pj, qkxyjb gsx ";
                fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
                fpselect += " and pj.gsxyjb = gsx.xyjb and pj.cyc_id = '" + cycid + "' ";
                fpselect += " Group By pj.gsxyjb,pj.ssyt,pj.mc, gsx.xymc ";

                fpselect += " Union ";
                fpselect += " Select 2 As od,pj.gsxyjb, gsx.xymc,'' As ssyt,'合计' As pjdymc, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb)/Sum(yqspl) End),2) As czcb, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjzh)/Sum(yqspl) End),2) As zjzh, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl) End),2) As sccb, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else (Sum(qjf)+sum(tbsyj))/Sum(yqspl) End),2) As qjf, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(ktf)/Sum(yqspl) End),2) As ktf, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl)+(Sum(qjf)+sum(tbsyj))/Sum(yqspl)+Sum(ktf)/Sum(yqspl) End),2) As yycb ";
                //fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(yycb)/Sum(yqspl) End),2) As yycb ";
                fpselect += " From jdstat_qksj pj, qkxyjb gsx ";
                fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
                fpselect += " and pj.gsxyjb = gsx.xyjb and pj.cyc_id = '" + cycid + "' ";
                fpselect += " Group By pj.gsxyjb,gsx.xymc ";

                fpselect += " Union ";
                fpselect += " Select 3 As od,90000,gsx.xymc,'' As ssyt,'总计' As pjdymc, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb)/Sum(yqspl) End),2) As czcb, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjzh)/Sum(yqspl) End),2) As zjzh, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl) End),2) As sccb, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else (Sum(qjf)+sum(tbsyj))/Sum(yqspl) End),2) As qjf, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(ktf)/Sum(yqspl) End),2) As ktf, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl)+(Sum(qjf)+sum(tbsyj))/Sum(yqspl)+Sum(ktf)/Sum(yqspl) End),2) As yycb ";
                //fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(yycb)/Sum(yqspl) End),2) As yycb ";
                fpselect += " From jdstat_qksj pj,qkxyjb gsx ";
                fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
                fpselect += " and gsx.xyjb = '90000' and pj.cyc_id = '" + cycid + "' ";
                fpselect += " Group By gsx.xymc ";
                fpselect += " order by gsxyjb,od ";
            }
            else
            {
                fpselect = " Select 1 As od,pj.gsxyjb,gsx.xymc,pj.ssyt,pj.mc as pjdymc, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb)/Sum(yqspl) End),2) As czcb, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjzh)/Sum(yqspl) End),2) As zjzh, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl) End),2) As sccb, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else (Sum(qjf)+sum(tbsyj))/Sum(yqspl) End),2) As qjf, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(ktf)/Sum(yqspl) End),2) As ktf, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl)+(Sum(qjf)+sum(tbsyj))/Sum(yqspl)+Sum(ktf)/Sum(yqspl) End),2) As yycb ";
                //fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(yycb)/Sum(yqspl) End),2) As yycb ";
                fpselect += " From jdstat_pjdysj pj, qkxyjb gsx ";
                fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
                fpselect += " and pj.gsxyjb = gsx.xyjb and pj.cyc_id = '" + cycid + "' ";
                fpselect += " Group By pj.gsxyjb,pj.ssyt,pj.mc, gsx.xymc ";

                fpselect += " Union ";
                fpselect += " Select 2 As od,pj.gsxyjb, gsx.xymc,'' As ssyt,'合计' As pjdymc, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb)/Sum(yqspl) End),2) As czcb, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjzh)/Sum(yqspl) End),2) As zjzh, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl) End),2) As sccb, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else (Sum(qjf)+sum(tbsyj))/Sum(yqspl) End),2) As qjf, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(ktf)/Sum(yqspl) End),2) As ktf, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl)+(Sum(qjf)+sum(tbsyj))/Sum(yqspl)+Sum(ktf)/Sum(yqspl) End),2) As yycb ";
                //fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(yycb)/Sum(yqspl) End),2) As yycb ";
                fpselect += " From jdstat_pjdysj pj, qkxyjb gsx ";
                fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
                fpselect += " and pj.gsxyjb = gsx.xyjb and pj.cyc_id = '" + cycid + "' ";
                fpselect += " Group By pj.gsxyjb,gsx.xymc ";

                fpselect += " Union ";
                fpselect += " Select 3 As od,90000,gsx.xymc,'' As ssyt,'总计' As pjdymc, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb)/Sum(yqspl) End),2) As czcb, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjzh)/Sum(yqspl) End),2) As zjzh, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl) End),2) As sccb, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else (Sum(qjf)+sum(tbsyj))/Sum(yqspl) End),2) As qjf, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(ktf)/Sum(yqspl) End),2) As ktf, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(sccb)/Sum(yqspl)+(Sum(qjf)+sum(tbsyj))/Sum(yqspl)+Sum(ktf)/Sum(yqspl) End),2) As yycb ";
                //fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else Sum(yycb)/Sum(yqspl) End),2) As yycb ";
                fpselect += " From jdstat_pjdysj pj,qkxyjb gsx ";
                fpselect += " Where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
                fpselect += " and gsx.xyjb = '90000' and pj.cyc_id = '" + cycid + "' ";
                fpselect += " Group By gsx.xymc ";
                fpselect += " order by gsxyjb,od ";
            }

            try
            {
                connfp.Open();

                OracleCommand myComm = new OracleCommand(fpselect, connfp);
                OracleDataReader myReader = myComm.ExecuteReader();
                //DataTable Fptable用来输出数据
                DataTable Fptable = new DataTable("fpdata");
                Fptable.Columns.Add("gsxyjb", typeof(string));
                Fptable.Columns.Add("xh", typeof(int));
                Fptable.Columns.Add("ssyt", typeof(string));
                Fptable.Columns.Add("pjdymc", typeof(string));
                Fptable.Columns.Add("czcb", typeof(float));
                Fptable.Columns.Add("zjzh", typeof(float));
                Fptable.Columns.Add("sccb", typeof(float));
                Fptable.Columns.Add("qjf", typeof(float));
                Fptable.Columns.Add("ktf", typeof(float));
                Fptable.Columns.Add("yycb", typeof(float));
                DataRow Fprow;
                int n = 1;

                while (myReader.Read())
                {
                    Fprow = Fptable.NewRow();

                    Fprow[0] = myReader[2];
                    Fprow[1] = n++;
                    Fprow[2] = myReader[3];
                    Fprow[3] = myReader.GetValue(4);
                    Fprow[4] = myReader[5];
                    Fprow[5] = myReader.GetValue(6);
                    Fprow[6] = myReader.GetValue(7);
                    Fprow[7] = myReader.GetValue(8);
                    Fprow[8] = myReader.GetValue(9);
                    Fprow[9] = myReader.GetValue(10);
                    Fptable.Rows.Add(Fprow);

                }
                myReader.Close();
                myComm.Clone();
                ////把第一,二列重复的项去掉
                //string tmp1 = Fptable.Rows[0][0].ToString();
                //string tmp2 = Fptable.Rows[0][1].ToString();
                //for (int j = 1; j < Fptable.Rows.Count; j++)
                //{
                //    if (Fptable.Rows[j][0].ToString() == tmp1)
                //        Fptable.Rows[j][0] = "";
                //    else
                //        tmp1 = Fptable.Rows[j][0].ToString();
                //    if (Fptable.Rows[j][1].ToString() == tmp2)
                //        Fptable.Rows[j][1] = "";
                //    else
                //        tmp2 = Fptable.Rows[j][1].ToString();
                //} //把第一,二列重复的项去掉

                DataSet fpset = new DataSet();
                fpset.Tables.Add(Fptable);

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
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            if (j != 1 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
                            else
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
                    FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);

                    //重新写序号
                    k = 1;
                    w = hcount;
                    for (int i = w; i < FpSpread1.Sheets[0].Rows.Count; i++)
                    {
                        if (FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() != "合计" && FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() != "总计")
                        {
                            FpSpread1.Sheets[0].Cells[i, 1].Value = k;

                            k++;
                            FpSpread1.Sheets[0].Cells[i, 1].Font.Size = 9;
                        }
                        else if (FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() == "合计")
                        {
                            FpSpread1.ActiveSheetView.AddSpanCell(i, 1, 1, 3);
                            FpSpread1.Sheets[0].Cells[i, 1].Value = "合计";
                        }
                        else if (FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() == "总计")
                        {
                            FpSpread1.ActiveSheetView.AddSpanCell(i, 0, 1, 4);
                            FpSpread1.Sheets[0].Cells[i, 0].Value = "总计";
                        }
                    }



                }
                else//不为空
                {
                    string path = "../../../static/excel/jdniandu.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表8-区块成本基础数据表");

                    this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    this.FpSpread1.Sheets[0].RowHeader.Visible = false;

                    this.FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            if (j != 1 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
                            else
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
                    FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
                    //重新写序号
                    k = 1;
                    w = hcount;
                    for (int i = w; i < FpSpread1.Sheets[0].Rows.Count; i++)
                    {
                        if (FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() != "合计" && FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() != "总计")
                        {
                            FpSpread1.Sheets[0].Cells[i, 1].Value = k;

                            k++;
                            FpSpread1.Sheets[0].Cells[i, 1].Font.Size = 9;
                        }
                        else if (FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() == "合计")
                        {
                            FpSpread1.ActiveSheetView.AddSpanCell(i, 1, 1, 3);
                            FpSpread1.Sheets[0].Cells[i, 1].Value = "合计";
                        }
                        else if (FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() == "总计")
                        {
                            FpSpread1.ActiveSheetView.AddSpanCell(i, 0, 1, 4);
                            FpSpread1.Sheets[0].Cells[i, 0].Value = "总计";
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
            { FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao7.xls"); }
            else
            { FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao8.xls"); }

        }




    }
}
