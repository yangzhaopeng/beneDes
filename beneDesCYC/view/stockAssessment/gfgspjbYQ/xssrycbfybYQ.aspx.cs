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
    public partial class xssrycbfybYQ : beneDesCYC.core.UI.corePage
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
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表12-单井销售收入与成本费用表"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    }
                    break;
                case "qk":
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表12-区块销售收入与成本费用表"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    }
                    break;
                case "pjdy":
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表12-区块销售收入与成本费用表"))
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

                ds = sql.GetDataSet("OWCBS_VIEW_XSSRYCBFYB.XSSRYCBFYB_DJ_PROC", CommandType.StoredProcedure,
                   new OracleParameter[] { param_bny, param_eny, param_cyc,param_cqc, param_out });

                FillDJSpread(ds);
            }
            else if (typeid == "qk")
            {

                ds = sql.GetDataSet("OWCBS_VIEW_XSSRYCBFYB.XSSRYCBFYB_QK_PROC", CommandType.StoredProcedure,
                         new OracleParameter[] { param_bny, param_eny, param_cyc, param_cqc, param_out });
                FillQKSpread(ds, typeid);
            }
            else if (typeid == "pjdy")
            {

                ds = sql.GetDataSet("OWCBS_VIEW_XSSRYCBFYB.XSSRYCBFYB_PJDY_PROC", CommandType.StoredProcedure,
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
                            if (j == 2)
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
            if (typeid == "pjdy")
            {
                FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text.Replace("区块", "评价单元");
                FpSpread1.Sheets[0].Cells[3, 3].Text = FpSpread1.Sheets[0].Cells[3, 3].Text.Replace("区块", "评价单元");
            }
            else if (typeid == "qk")
            {
                FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text.Replace("评价单元", "区块");
                FpSpread1.Sheets[0].Cells[3, 3].Text = FpSpread1.Sheets[0].Cells[3, 3].Text.Replace("评价单元", "区块");
            }
            FpSpread1.Width = Unit.Pixel(1233);
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
            this.FpSpread1.Sheets[0].OpenExcel(path, "表12-油井销售收入与成本费用表");

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
            this.FpSpread1.Sheets[0].OpenExcel(path, "表13-区块销售收入与成本费用表");

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

            string fpselect = " Select ";
            fpselect += " pjdyxyjb, ";
            fpselect += " gsxyjb, ";
            fpselect += " qkxyjb.xymc, gsxylb_info.xymc, ";
            fpselect += " round(sum(yyspl),4) as    yyspl, ";
            fpselect += " round(sum(trqspl),4) as   trqspl, ";
            fpselect += " round(sum(bscpspl)/10000,4) as  bscpspl, ";
            fpselect += " round(sum(yyxssr)/10000,4) as   yyxssr, ";
            fpselect += " round(sum(trqxssr)/10000,4) as  trqxssr, ";
            fpselect += " round(sum(bscpxssr)/10000,4) as bscpxssr, ";
            fpselect += " round(sum(yyxssr)/10000 + sum(trqxssr)/10000 + sum(bscpxssr)/10000,4) as hj, ";
            fpselect += " round(sum(xssj)/10000,4) as     xssj, ";
            fpselect += " round(sum(zdyxf)/10000,4) as    zdyxf, ";
            fpselect += " round(sum(czcb)/10000,4) as     czcb, ";
            fpselect += " round(sum(zjzh)/10000,4) as     zjzh, ";
            fpselect += " round(sum(sccb)/10000,4) as     sccb, ";
            fpselect += " round((Sum(qjf)+sum(tbsyj))/10000,4) as      qjf, ";
            fpselect += " round(sum(ktf)/10000,4) as      ktf, ";
            fpselect += " round(sum(yycb)/10000,4) as     yycb, ";
            fpselect += " round(sum(lr)/10000,4) as       lr ";
            fpselect += " From jdstat_djsj, qkxyjb, gsxylb_info ";
            fpselect += " Where djisopen =1  and bny = " + bny + " and eny=" + eny + " and qkxyjb <> 99 and pjdyxyjb <> 99 ";
            fpselect += " and jdstat_djsj.pjdyxyjb = qkxyjb.xyjb and jdstat_djsj.gsxyjb = gsxylb_info.xyjb and jdstat_djsj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " Group By pjdyxyjb,gsxyjb,qkxyjb.xymc, gsxylb_info.xymc ";

            fpselect += " Union ";
            fpselect += " Select pjdyxyjb,80000 As gsxyjb, ";
            fpselect += " qkxyjb.xymc, gsxylb_info.xymc, ";
            fpselect += " round(sum(yyspl),4) as    yyspl, ";
            fpselect += " round(sum(trqspl),4) as   trqspl, ";
            fpselect += " round(sum(bscpspl)/10000,4) as  bscpspl, ";
            fpselect += " round(sum(yyxssr)/10000,4) as   yyxssr, ";
            fpselect += " round(sum(trqxssr)/10000,4) as  trqxssr, ";
            fpselect += " round(sum(bscpxssr)/10000,4) as bscpxssr, ";
            fpselect += " round(sum(yyxssr)/10000 + sum(trqxssr)/10000 + sum(bscpxssr)/10000,4) as hj, ";
            fpselect += " round(sum(xssj)/10000,4) as     xssj, ";
            fpselect += " round(sum(zdyxf)/10000,4) as    zdyxf, ";
            fpselect += " round(sum(czcb)/10000,4) as     czcb, ";
            fpselect += " round(sum(zjzh)/10000,4) as     zjzh, ";
            fpselect += " round(sum(sccb)/10000,4) as     sccb, ";
            fpselect += " round((Sum(qjf)+sum(tbsyj))/10000,4) as      qjf, ";
            fpselect += " round(sum(ktf)/10000,4) as      ktf, ";
            fpselect += " round(sum(yycb)/10000,4) as     yycb, ";
            fpselect += " round(sum(lr)/10000,4) as       lr ";
            fpselect += " From jdstat_djsj, qkxyjb, gsxylb_info ";
            fpselect += " Where djisopen =1  and bny = " + bny + " and eny=" + eny + " and qkxyjb <> 99 and pjdyxyjb <> 99 ";
            fpselect += " and jdstat_djsj.pjdyxyjb = qkxyjb.xyjb and gsxylb_info.xyjb = '80000' and jdstat_djsj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " Group By pjdyxyjb,qkxyjb.xymc, gsxylb_info.xymc ";


            fpselect += " Union ";
            fpselect += " Select ";
            fpselect += " 90000 As pjdyxyjb, ";
            fpselect += " gsxyjb,qkxyjb.xymc, gsxylb_info.xymc, ";
            fpselect += " round(sum(yyspl),4) as    yyspl, ";
            fpselect += " round(sum(trqspl),4) as   trqspl, ";
            fpselect += " round(sum(bscpspl)/10000,4) as  bscpspl, ";
            fpselect += " round(sum(yyxssr)/10000,4) as   yyxssr, ";
            fpselect += " round(sum(trqxssr)/10000,4) as  trqxssr, ";
            fpselect += " round(sum(bscpxssr)/10000,4) as bscpxssr, ";
            fpselect += " round(sum(yyxssr)/10000 + sum(trqxssr)/10000 + sum(bscpxssr)/10000,4) as hj, ";
            fpselect += " round(sum(xssj)/10000,4) as     xssj, ";
            fpselect += " round(sum(zdyxf)/10000,4) as    zdyxf, ";
            fpselect += " round(sum(czcb)/10000,4) as     czcb, ";
            fpselect += " round(sum(zjzh)/10000,4) as     zjzh, ";
            fpselect += " round(sum(sccb)/10000,4) as     sccb, ";
            fpselect += " round((Sum(qjf)+sum(tbsyj))/10000,4) as      qjf, ";
            fpselect += " round(sum(ktf)/10000,4) as      ktf, ";
            fpselect += " round(sum(yycb)/10000,4) as     yycb, ";
            fpselect += " round(sum(lr)/10000,4) as       lr ";
            fpselect += " From jdstat_djsj,qkxyjb,gsxylb_info ";
            fpselect += " Where djisopen =1  and bny = " + bny + " and eny=" + eny + " and qkxyjb <> 99 and pjdyxyjb <> 99 ";
            fpselect += " and  jdstat_djsj.gsxyjb = gsxylb_info.xyjb  and qkxyjb.xyjb='90000'  and jdstat_djsj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " Group By gsxyjb,qkxyjb.xymc, gsxylb_info.xymc ";

            fpselect += " Union ";
            fpselect += " Select 90000 as pjdyxyjb,80000 As gsxyjb, ";
            fpselect += " qkxyjb.xymc, gsxylb_info.xymc, ";
            fpselect += " round(sum(yyspl),4) as    yyspl, ";
            fpselect += " round(sum(trqspl),4) as   trqspl, ";
            fpselect += " round(sum(bscpspl)/10000,4) as  bscpspl, ";
            fpselect += " round(sum(yyxssr)/10000,4) as   yyxssr, ";
            fpselect += " round(sum(trqxssr)/10000,4) as  trqxssr, ";
            fpselect += " round(sum(bscpxssr)/10000,4) as bscpxssr, ";
            fpselect += " round(sum(yyxssr)/10000 + sum(trqxssr)/10000 + sum(bscpxssr)/10000,4) as hj, ";
            fpselect += " round(sum(xssj)/10000,4) as     xssj, ";
            fpselect += " round(sum(zdyxf)/10000,4) as    zdyxf, ";
            fpselect += " round(sum(czcb)/10000,4) as     czcb, ";
            fpselect += " round(sum(zjzh)/10000,4) as     zjzh, ";
            fpselect += " round(sum(sccb)/10000,4) as     sccb, ";
            fpselect += " round((Sum(qjf)+sum(tbsyj))/10000,4) as      qjf, ";
            fpselect += " round(sum(ktf)/10000,4) as      ktf, ";
            fpselect += " round(sum(yycb)/10000,4) as     yycb, ";
            fpselect += " round(sum(lr)/10000,4) as       lr ";
            fpselect += " From jdstat_djsj, qkxyjb, gsxylb_info ";
            fpselect += " Where djisopen =1  and bny = " + bny + " and eny=" + eny + " and qkxyjb <> 99 and pjdyxyjb <> 99 ";
            fpselect += " and qkxyjb.xyjb='90000' and gsxylb_info.xyjb = '80000' and jdstat_djsj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " Group By qkxyjb.xymc, gsxylb_info.xymc ";

            try
            {
                connfp.Open();

                OracleCommand myComm = new OracleCommand(fpselect, connfp);
                OracleDataReader myReader = myComm.ExecuteReader();
                //DataTable Fptable用来输出数据
                DataTable Fptable = new DataTable("fpdata");
                Fptable.Columns.Add("pjdyxyjb", typeof(string));
                Fptable.Columns.Add("gsxyjb", typeof(System.String));
                Fptable.Columns.Add("yyspl", typeof(float));
                Fptable.Columns.Add("trqspl", typeof(float));
                Fptable.Columns.Add("bscpspl", typeof(float));
                Fptable.Columns.Add("yyxssr", typeof(float));
                Fptable.Columns.Add("trqxssr", typeof(float));
                Fptable.Columns.Add("bscpxssr", typeof(float));
                Fptable.Columns.Add("hj", typeof(float));
                Fptable.Columns.Add("xssj", typeof(float));
                Fptable.Columns.Add("zdyxf", typeof(float));
                Fptable.Columns.Add("czcb", typeof(float));
                Fptable.Columns.Add("zjzh", typeof(float));
                Fptable.Columns.Add("sccb", typeof(float));
                Fptable.Columns.Add("qjf", typeof(float));
                Fptable.Columns.Add("ktf", typeof(float));
                Fptable.Columns.Add("yycb", typeof(float));
                Fptable.Columns.Add("lr", typeof(float));
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
                    Fprow[8] = myReader[10];
                    Fprow[9] = myReader.GetValue(11);
                    Fprow[10] = myReader[12];
                    Fprow[11] = myReader.GetValue(13);
                    Fprow[12] = myReader.GetValue(14);
                    Fprow[13] = myReader.GetValue(15);
                    Fprow[14] = myReader.GetValue(16);
                    Fprow[15] = myReader.GetValue(17);
                    Fprow[16] = myReader.GetValue(18);
                    Fprow[17] = myReader.GetValue(19);
                    Fptable.Rows.Add(Fprow);
                }
                myReader.Close();
                myComm.Clone();
                //#region//把第一,二列重复的项去掉
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
                //}
                //#endregion//把第一,二列重复的项去掉
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
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                            }
                            else
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            }
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
                }
                else//不为空
                {
                    string path = Page.MapPath("~/excel/jdnianduQC.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "表12-油井销售收入与成本费用表");

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

                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + "  " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;
                    /////////////////////

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            if (isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                            }
                            else
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            }
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
                fpselect = " Select pj.gsxyjb, gsx.xymc,1 as xh, ";
                fpselect += " ssyt, ";
                fpselect += " mc as mc, ";
                fpselect += " round(yyspl,4) as   yyspl, ";
                //fpselect += " round(trqspl,4) as  trqspl, ";
                fpselect += " round(bscpspl/10000,4) as bscpspl, ";
                //fpselect += "'80000' as hj";
                fpselect += " round(yyxssr/10000,4) as  yyxssr, ";
                //fpselect += " round(trqxssr/10000,4) as trqxssr, ";
                fpselect += " round(bscpxssr/10000,4) as bscpxssr, ";
                fpselect += " round(yyxssr/10000 + bscpxssr/10000,4) as hj, ";
                fpselect += " round(xssj/10000,4) as    xssj, ";
                fpselect += " round(czcb/10000,4) as    czcb, ";
                fpselect += " round(sccb/10000,4) as    sccb, ";
                fpselect += " round((qjf+tbsyj)/10000,4) as     qjf, ";
                fpselect += " round(ktf/10000,4) as     ktf, ";
                fpselect += " round((yycb+tbsyj)/10000,4) as    yycb, ";
                fpselect += " round(lr/10000,4) as      lr ";
                fpselect += " From jdstat_qksj pj,qkxyjb gsx ";
                fpselect += " Where bny = " + bny + " and eny=" + eny + " and sfpj = 1 ";
                fpselect += " and pj.GSXYJB = gsx.xyjb  and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
                //fpselect += " group by pj.gsxyjb,ssyt,mc,gsx.xymc ";

                fpselect += " Union ";
                fpselect += " Select pj.gsxyjb,gsx.xymc,2 As xh,   ";
                fpselect += " '' as ssyt, ";
                fpselect += " '合计' as mc, ";
                fpselect += " round(sum(yyspl),4) as   yyspl, ";
                //fpselect += " round(trqspl,4) as  trqspl, ";
                fpselect += " round(sum(bscpspl)/10000,4) as bscpspl, ";
                //fpselect += "'80000' as hj";
                fpselect += " round(sum(yyxssr)/10000,4) as  yyxssr, ";
                //fpselect += " round(trqxssr/10000,4) as trqxssr, ";
                fpselect += " round(sum(bscpxssr)/10000,4) as bscpxssr, ";
                fpselect += " round(sum(yyxssr)/10000 + sum(bscpxssr)/10000,4) as hj, ";
                fpselect += " round(sum(xssj)/10000,4) as    xssj, ";
                fpselect += " round(sum(czcb)/10000,4) as    czcb, ";
                fpselect += " round(sum(sccb)/10000,4) as    sccb, ";
                fpselect += " round((Sum(qjf)+sum(tbsyj))/10000,4) as     qjf, ";
                fpselect += " round(sum(ktf)/10000,4) as     ktf, ";
                fpselect += " round((sum(yycb)+sum(tbsyj))/10000,4) as    yycb, ";
                fpselect += " round(sum(lr)/10000,4) as      lr ";
                fpselect += " From jdstat_qksj pj,qkxyjb gsx";
                fpselect += " Where bny = " + bny + " and eny=" + eny + " and sfpj = 1 ";
                fpselect += " and pj.GSXYJB = gsx.xyjb  and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
                fpselect += " group by pj.gsxyjb,gsx.xymc  ";

                fpselect += " Union ";
                fpselect += " Select 90000,gsx.xymc,3 As xh,'' As ssyt,'总计' As mc, ";
                fpselect += " round(sum(yyspl)/10000,4) as   yyspl, ";
                //fpselect += " round(trqspl,4) as  trqspl, ";
                fpselect += " round(sum(bscpspl)/10000,4) as bscpspl, ";
                //fpselect += "'80000' as hj";
                fpselect += " round(sum(yyxssr),4) as  yyxssr, ";
                //fpselect += " round(trqxssr/10000,4) as trqxssr, ";
                fpselect += " round(sum(bscpxssr)/10000,4) as bscpxssr, ";
                fpselect += " round(sum(yyxssr)/10000 + sum(bscpxssr)/10000,4) as hj, ";
                fpselect += " round(sum(xssj)/10000,4) as    xssj, ";
                fpselect += " round(sum(czcb)/10000,4) as    czcb, ";
                fpselect += " round(sum(sccb)/10000,4) as    sccb, ";
                fpselect += " round((Sum(qjf)+sum(tbsyj))/10000,4) as     qjf, ";
                fpselect += " round(sum(ktf)/10000,4) as     ktf, ";
                fpselect += " round((sum(yycb)+sum(tbsyj))/10000,4) as    yycb, ";
                fpselect += " round(sum(lr)/10000,4) as      lr ";
                fpselect += " From jdstat_qksj pj,qkxyjb gsx";
                fpselect += " Where bny = " + bny + " and eny=" + eny + " and sfpj = 1 ";
                fpselect += " and gsx.xyjb='90000'  and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
                fpselect += " Group By gsx.xymc ";
                fpselect += " order by gsxyjb,xh ";


            }
            else
            {
                fpselect = " Select pj.gsxyjb,qkxyjb.xymc, ";
                fpselect += " 1 as xh, ";
                fpselect += " ssyt, ";
                fpselect += " mc as mc, ";
                fpselect += " round(yyspl,4) as   yyspl, ";
                //fpselect += " round(trqspl,4) as  trqspl, ";
                fpselect += " round(bscpspl/10000,4) as bscpspl, ";
                //fpselect += "'80000' as hj";
                fpselect += " round(yyxssr/10000,4) as  yyxssr, ";
                //fpselect += " round(trqxssr/10000,4) as trqxssr, ";
                fpselect += " round(bscpxssr/10000,4) as bscpxssr, ";
                fpselect += " round(yyxssr/10000 + bscpxssr/10000,4) as hj, ";
                fpselect += " round(xssj/10000,4) as    xssj, ";
                fpselect += " round(czcb/10000,4) as    czcb, ";
                fpselect += " round(sccb/10000,4) as    sccb, ";
                fpselect += " round((qjf+tbsyj)/10000,4) as     qjf, ";
                fpselect += " round(ktf/10000,4) as     ktf, ";
                fpselect += " round((yycb+tbsyj)/10000,4) as    yycb, ";
                fpselect += " round(lr/10000,4) as      lr ";
                fpselect += " From jdstat_pjdysj pj,qkxyjb";
                fpselect += " Where bny = " + bny + " and eny=" + eny + " and sfpj = 1 ";
                fpselect += " and pj.GSXYJB = qkxyjb.xyjb  and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
                //fpselect += " group by pj.gsxyjb,qkxyjb.xymc, ssyt,mc,yyspl,bscpspl, yyxssr,bscpxssr,xssj,czcb,sccb,qjf,ktf,yycb,lr";

                fpselect += " Union ";
                fpselect += " Select pj.gsxyjb,gsx.xymc,2 As xh,   ";
                fpselect += " '' as ssyt, ";
                fpselect += " '合计' as mc, ";
                fpselect += " round(sum(yyspl),4) as   yyspl, ";
                //fpselect += " round(trqspl,4) as  trqspl, ";
                fpselect += " round(sum(bscpspl)/10000,4) as bscpspl, ";
                //fpselect += "'80000' as hj";
                fpselect += " round(sum(yyxssr)/10000,4) as  yyxssr, ";
                //fpselect += " round(trqxssr/10000,4) as trqxssr, ";
                fpselect += " round(sum(bscpxssr)/10000,4) as bscpxssr, ";
                fpselect += " round(sum(yyxssr)/10000 + sum(bscpxssr)/10000,4) as hj, ";
                fpselect += " round(sum(xssj)/10000,4) as    xssj, ";
                fpselect += " round(sum(czcb)/10000,4) as    czcb, ";
                fpselect += " round(sum(sccb)/10000,4) as    sccb, ";
                fpselect += " round((Sum(qjf)+sum(tbsyj))/10000,4) as     qjf, ";
                fpselect += " round(sum(ktf)/10000,4) as     ktf, ";
                fpselect += " round((sum(yycb)+sum(tbsyj))/10000,4) as    yycb, ";
                fpselect += " round(sum(lr)/10000,4) as      lr ";
                fpselect += " From jdstat_pjdysj pj,qkxyjb gsx";
                fpselect += " Where bny = " + bny + " and eny=" + eny + " and sfpj = 1 ";
                fpselect += " and pj.GSXYJB = gsx.xyjb  and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
                fpselect += " group by pj.gsxyjb,gsx.xymc ";

                fpselect += " Union ";
                fpselect += " Select 90000,gsx.xymc,3 As xh,'' As ssyt,'总计' As mc, ";
                fpselect += " round(sum(yyspl),4) as   yyspl, ";
                //fpselect += " round(trqspl,4) as  trqspl, ";
                fpselect += " round(sum(bscpspl)/10000,4) as bscpspl, ";
                //fpselect += "'80000' as hj";
                fpselect += " round(sum(yyxssr)/10000,4) as  yyxssr, ";
                //fpselect += " round(trqxssr/10000,4) as trqxssr, ";
                fpselect += " round(sum(bscpxssr)/10000,4) as bscpxssr, ";
                fpselect += " round(sum(yyxssr)/10000 + sum(bscpxssr)/10000,4) as hj, ";
                fpselect += " round(sum(xssj)/10000,4) as    xssj, ";
                fpselect += " round(sum(czcb)/10000,4) as    czcb, ";
                fpselect += " round(sum(sccb)/10000,4) as    sccb, ";
                fpselect += " round((Sum(qjf)+sum(tbsyj))/10000,4) as     qjf, ";
                fpselect += " round(sum(ktf)/10000,4) as     ktf, ";
                fpselect += " round((sum(yycb)+sum(tbsyj))/10000,4) as    yycb, ";
                fpselect += " round(sum(lr)/10000,4) as      lr ";
                fpselect += " From jdstat_pjdysj pj,qkxyjb gsx";
                fpselect += " Where bny = " + bny + " and eny=" + eny + " and sfpj = 1 ";
                fpselect += " and gsx.xyjb='90000'  and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
                fpselect += " Group By gsx.xymc ";
                fpselect += " order by gsxyjb,xh ";

            }

            try
            {
                connfp.Open();
                OracleDataAdapter adafp = new OracleDataAdapter(fpselect, connfp);
                DataSet fpset = new DataSet();
                adafp.Fill(fpset, "fpdata");
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
                        for (int j = 0; j < ccount - 1; j++)
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j + 1].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            FpSpread1.Sheets[0].Cells[i + hcount, 1].Value = i + 1;
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
                    string path = Page.MapPath("~/excel/jdnianduQC.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "表13-区块销售收入与成本费用表");

                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;


                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount - 1; j++)
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j + 1].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            FpSpread1.Sheets[0].Cells[i + hcount, 1].Value = i + 1;
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
            { FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao12.xls"); }
            else
            { FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao13.xls"); }

        }

    }
}
