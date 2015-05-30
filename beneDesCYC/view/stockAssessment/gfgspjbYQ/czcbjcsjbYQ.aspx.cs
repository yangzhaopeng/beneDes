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
    public partial class czcbjcsjbYQ : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string typeid = _getParam("targetType");
            //if (typeid == "yj" || typeid == "qj")
            //{
            initSpread(typeid);
            //}
            //else
            //{
            //    initSpread2();
            //}
        }

        protected void initSpread(string typeid)
        {

            string path = "../../../static/excel/jdnianduYQ.xls";
            path = Page.MapPath(path);
            switch (typeid)
            {
                case "yj":
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表9-油井操作成本基础数据表"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    }
                    break;
                case "qj":
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表9-气井操作成本基础数据表"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    }
                    break;
                default:   //区块 与 评价单元
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表9-区块操作成本基础数据表"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    }
                    break;
            }
            //判断模板是否加载
            if (FpSpread1.Sheets[0].Rows.Count != 3)
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

        protected void initSpread2()
        {
            string path = "../../../static/excel/jdnianduQC.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表10-区块操作成本基础数据表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

            if (isempty() == 0)
            { Response.Write("<script>alert('无数据')</script>"); }
            else
            { sj2(); }

            //}
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


            if (typeid == "yj")
            {

                ds = sql.GetDataSet("OWCBS_VIEW_CZCBJCSJB.CZCBJCSJB_YJ_PROC", CommandType.StoredProcedure,
                   new OracleParameter[] { param_bny, param_eny, param_cyc, param_out });

                FillDJSpread(ds);
            }
            else if (typeid == "qj")
            {

                ds = sql.GetDataSet("OWCBS_VIEW_CZCBJCSJB.CZCBJCSJB_QJ_PROC", CommandType.StoredProcedure,
                         new OracleParameter[] { param_bny, param_eny, param_cqc, param_out });
                FillDJSpread(ds);
            }
            else if (typeid == "qk")
            {

                ds = sql.GetDataSet("OWCBS_VIEW_CZCBJCSJB.CZCBJCSJB_QK_PROC", CommandType.StoredProcedure,
                         new OracleParameter[] { param_bny, param_eny, param_cyc, param_cqc, param_out });
                FillQKSpread(ds, typeid);
            }
            else if (typeid == "pjdy")
            {

                ds = sql.GetDataSet("OWCBS_VIEW_CZCBJCSJB.CZCBJCSJB_PJDY_PROC", CommandType.StoredProcedure,
            new OracleParameter[] { param_bny, param_eny, param_cyc, param_cqc, param_out });
                FillQKSpread(ds, typeid);
            }



            ////JObject obj = new JObject();
            //string bny = _getParam("startMonth");
            //string eny = _getParam("endMonth");
            ////obj["departmentStore"] = _getParam("departmentStore");
            ////string list = obj["departmentStore"].ToString();
            //string list = _getParam("CYC");
            //string typeid = _getParam("targetType");  
            #region
            //OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);

            //string fpselect = " Select sdy.pjdyxyjb as pjdyxyjb,sdy.gsxyjb as gsxyjb,qkxyjb.xymc, gsxylb_info.xymc, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjclf   )/Sum(trqspl) End),2) As zjclf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjrlf   )/Sum(trqspl) End),2) As zjrlf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjdlf   )/Sum(trqspl) End),2) As zjdlf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjryf   )/Sum(trqspl) End),2) As zjryf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qywzrf  )/Sum(trqspl) End),2) As qywzrf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(jxzyf   )/Sum(trqspl) End),2) As jxzyf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cjsjf   )/Sum(trqspl) End),2) As cjsjf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(whxlf   )/Sum(trqspl) End),2) As whxlf, ";
            ////fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cyrcf   )/Sum(trqspl) End),2) As cyrcf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(yqclf   )/Sum(trqspl) End),2) As yqclf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qthsf   )/Sum(trqspl) End),2) As qthsf, ";
            ////fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(trqjhf  )/Sum(trqspl) End),2) As trqjhf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ysf     )/Sum(trqspl) End),2) As ysf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qtzjf   )/Sum(trqspl) End),2) As qtzjf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ckglf   )/Sum(trqspl) End),2) As ckglf, ";
            ////fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zyyqcp  )/Sum(trqspl) End),2) As zyyqcp, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(czcb    )/Sum(trqspl) End),2) As czcb, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(czcb )/Sum(trqspl) End),2) As czcb_my ";
            ////fpselect += " round((Case When sum(dtl) = 0 Then 0 Else Sum(czcb_my )/Sum(dtl) End),2) As czcb_my ";
            //fpselect += " From jdstat_djsj sdy,qkxyjb, gsxylb_info ";
            //fpselect += " where sdy.bny = " + bny + " and sdy.eny=" + eny + " and djisopen = 1  and qkxyjb <> 99 and pjdyxyjb <> 99 ";
            //fpselect += " and sdy.pjdyxyjb = qkxyjb.xyjb and sdy.gsxyjb = gsxylb_info.xyjb and sdy.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //fpselect += " Group By sdy.pjdyxyjb,sdy.gsxyjb,qkxyjb.xymc, gsxylb_info.xymc ";

            //fpselect += " Union ";
            //fpselect += " Select sdy.pjdyxyjb as pjdyxyjb,80000 As gsxyjb,qkxyjb.xymc, gsxylb_info.xymc, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjclf   )/Sum(trqspl) End),2) As zjclf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjrlf   )/Sum(trqspl) End),2) As zjrlf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjdlf   )/Sum(trqspl) End),2) As zjdlf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjryf   )/Sum(trqspl) End),2) As zjryf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qywzrf  )/Sum(trqspl) End),2) As qywzrf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(jxzyf   )/Sum(trqspl) End),2) As jxzyf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cjsjf   )/Sum(trqspl) End),2) As cjsjf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(whxlf   )/Sum(trqspl) End),2) As whxlf, ";
            ////fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cyrcf   )/Sum(trqspl) End),2) As cyrcf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(yqclf   )/Sum(trqspl) End),2) As yqclf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qthsf   )/Sum(trqspl) End),2) As qthsf, ";
            ////fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(trqjhf  )/Sum(trqspl) End),2) As trqjhf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ysf     )/Sum(trqspl) End),2) As ysf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qtzjf   )/Sum(trqspl) End),2) As qtzjf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ckglf   )/Sum(trqspl) End),2) As ckglf, ";
            ////fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zyyqcp  )/Sum(trqspl) End),2) As zyyqcp, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(czcb    )/Sum(trqspl) End),2) As czcb, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(czcb )/Sum(trqspl) End),2) As czcb_my ";
            ////fpselect += " round((Case When sum(dtl) = 0 Then 0 Else Sum(czcb_my )/Sum(dtl) End),2) As czcb_my ";
            //fpselect += " From jdstat_djsj sdy ,qkxyjb, gsxylb_info ";
            //fpselect += " where sdy.bny = " + bny + " and sdy.eny=" + eny + " and djisopen = 1  and qkxyjb <> 99 and pjdyxyjb <> 99 ";
            //fpselect += " and sdy.pjdyxyjb = qkxyjb.xyjb and gsxylb_info.xyjb ='80000' and sdy.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //fpselect += " Group By sdy.pjdyxyjb,qkxyjb.xymc, gsxylb_info.xymc  ";

            //fpselect += " union ";
            //fpselect += " select 90000 as pjdyxyjb,sdy.gsxyjb as gsxyjb,qkxyjb.xymc, gsxylb_info.xymc, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjclf   )/Sum(trqspl) End),2) As zjclf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjrlf   )/Sum(trqspl) End),2) As zjrlf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjdlf   )/Sum(trqspl) End),2) As zjdlf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjryf   )/Sum(trqspl) End),2) As zjryf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qywzrf  )/Sum(trqspl) End),2) As qywzrf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(jxzyf   )/Sum(trqspl) End),2) As jxzyf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cjsjf   )/Sum(trqspl) End),2) As cjsjf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(whxlf   )/Sum(trqspl) End),2) As whxlf, ";
            ////fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cyrcf   )/Sum(trqspl) End),2) As cyrcf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(yqclf   )/Sum(trqspl) End),2) As yqclf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qthsf   )/Sum(trqspl) End),2) As qthsf, ";
            ////fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(trqjhf  )/Sum(trqspl) End),2) As trqjhf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ysf     )/Sum(trqspl) End),2) As ysf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qtzjf   )/Sum(trqspl) End),2) As qtzjf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ckglf   )/Sum(trqspl) End),2) As ckglf, ";
            ////fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zyyqcp  )/Sum(trqspl) End),2) As zyyqcp, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(czcb    )/Sum(trqspl) End),2) As czcb, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(czcb )/Sum(trqspl) End),2) As czcb_my ";
            ////fpselect += " round((Case When sum(dtl) = 0 Then 0 Else Sum(czcb_my )/Sum(dtl) End),2) As czcb_my ";
            //fpselect += " From jdstat_djsj sdy,qkxyjb,gsxylb_info ";
            //fpselect += " where sdy.bny = " + bny + " and sdy.eny=" + eny + " and djisopen = 1  and qkxyjb <> 99 and pjdyxyjb <> 99 ";
            //fpselect += " and  sdy.gsxyjb = gsxylb_info.xyjb  and qkxyjb.xyjb='90000' and sdy.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //fpselect += " Group By sdy.gsxyjb ,qkxyjb.xymc, gsxylb_info.xymc ";

            //fpselect += " union ";
            //fpselect += " select 90000 as pjdyxyjb, 80000 As gsxyjb,qkxyjb.xymc, gsxylb_info.xymc, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjclf   )/Sum(trqspl) End),2) As zjclf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjrlf   )/Sum(trqspl) End),2) As zjrlf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjdlf   )/Sum(trqspl) End),2) As zjdlf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjryf   )/Sum(trqspl) End),2) As zjryf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qywzrf  )/Sum(trqspl) End),2) As qywzrf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(jxzyf   )/Sum(trqspl) End),2) As jxzyf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cjsjf   )/Sum(trqspl) End),2) As cjsjf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(whxlf   )/Sum(trqspl) End),2) As whxlf, ";
            ////fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cyrcf   )/Sum(trqspl) End),2) As cyrcf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(yqclf   )/Sum(trqspl) End),2) As yqclf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qthsf   )/Sum(trqspl) End),2) As qthsf, ";
            ////fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(trqjhf  )/Sum(trqspl) End),2) As trqjhf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ysf     )/Sum(trqspl) End),2) As ysf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qtzjf   )/Sum(trqspl) End),2) As qtzjf, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ckglf   )/Sum(trqspl) End),2) As ckglf, ";
            ////fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zyyqcp  )/Sum(trqspl) End),2) As zyyqcp, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(czcb    )/Sum(trqspl) End),2) As czcb, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(czcb )/Sum(trqspl) End),2) As czcb_my ";
            ////fpselect += " round((Case When sum(dtl) = 0 Then 0 Else Sum(czcb_my )/Sum(dtl) End),2) As czcb_my ";
            //fpselect += " From jdstat_djsj sdy,qkxyjb,gsxylb_info ";
            //fpselect += " where sdy.bny = " + bny + " and sdy.eny=" + eny + " and djisopen = 1  and qkxyjb <> 99 and pjdyxyjb <> 99 ";
            //fpselect += " and   gsxylb_info.xyjb  = '80000' and qkxyjb.xyjb='90000' and sdy.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //fpselect += " Group By qkxyjb.xymc, gsxylb_info.xymc ";

            //try
            //{
            //    connfp.Open();
            //    OracleCommand myComm = new OracleCommand(fpselect, connfp);
            //    OracleDataReader myReader = myComm.ExecuteReader();
            //    //DataTable Fptable用来输出数据
            //    DataTable Fptable = new DataTable("fpdata");
            //    Fptable.Columns.Add("pjdyxyjb", typeof(string));
            //    Fptable.Columns.Add("gsxyjb", typeof(string));
            //    Fptable.Columns.Add("zjclf", typeof(float));
            //    Fptable.Columns.Add("zjrlf", typeof(float));
            //    Fptable.Columns.Add("zjdlf", typeof(float));
            //    Fptable.Columns.Add("zjryf", typeof(float));
            //    Fptable.Columns.Add("qywzrf", typeof(float));
            //    Fptable.Columns.Add("jxzyf", typeof(float));
            //    Fptable.Columns.Add("cjsjf", typeof(string));
            //    Fptable.Columns.Add("whxlf", typeof(float));
            //    //Fptable.Columns.Add("cyrcf", typeof(float));
            //    Fptable.Columns.Add("yqclf", typeof(float));
            //    Fptable.Columns.Add("qthsf", typeof(float));
            //    //Fptable.Columns.Add("trqjhf", typeof(float));
            //    Fptable.Columns.Add("ysf", typeof(float));
            //    Fptable.Columns.Add("qtzjf", typeof(float));
            //    Fptable.Columns.Add("ckglf", typeof(float));
            //    //Fptable.Columns.Add("zyyqcp", typeof(float));
            //    Fptable.Columns.Add("czcb", typeof(float));
            //    Fptable.Columns.Add("czcb_my", typeof(float));
            //    DataRow Fprow;
            //    while (myReader.Read())
            //    {
            //        Fprow = Fptable.NewRow();

            //        Fprow[0] = myReader[2];
            //        Fprow[1] = myReader[3];
            //        Fprow[2] = myReader.GetValue(4);
            //        Fprow[3] = myReader[5];
            //        Fprow[4] = myReader.GetValue(6);
            //        Fprow[5] = myReader.GetValue(7);
            //        Fprow[6] = myReader.GetValue(8);
            //        Fprow[7] = myReader.GetValue(9);
            //        Fprow[8] = myReader[10];
            //        Fprow[9] = myReader[11];
            //        Fprow[10] = myReader.GetValue(12);
            //        Fprow[11] = myReader[13];
            //        Fprow[12] = myReader.GetValue(14);
            //        Fprow[13] = myReader.GetValue(15);
            //        Fprow[14] = myReader.GetValue(16);
            //        Fprow[15] = myReader.GetValue(17);
            //        Fprow[16] = myReader.GetValue(18);
            //        //Fprow[17] = myReader.GetValue(19);
            //        // Fprow[18] = myReader.GetValue(20);
            //        //Fprow[19] = myReader.GetValue(21);


            //        Fptable.Rows.Add(Fprow);
            //    }
            //    myReader.Close();
            //    myComm.Clone();
            //    DataSet fpset = new DataSet();
            //    fpset.Tables.Add(Fptable);


            //    //此处用于绑定数据            
            //    #region
            //    int rcount = fpset.Tables["fpdata"].Rows.Count;
            //    int ccount = fpset.Tables["fpdata"].Columns.Count;
            //    int hcount = 4;
            //    //FpSpread1.ColumnHeader.Visible = false;
            //    //FpSpread1.RowHeader.Visible = false;
            //    if (FpSpread1.Sheets[0].Rows.Count == hcount)
            //    {
            //        FpSpread1.Sheets[0].AddRows(hcount, rcount);
            //        for (int i = 0; i < rcount; i++)
            //        {
            //            for (int j = 0; j < ccount; j++)
            //            {
            //                //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
            //                if (isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
            //                {
            //                    FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
            //                }
            //                else
            //                {
            //                    FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
            //                }
            //                FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
            //            }
            //        }
            //        int k = 1;  //统计重复单元格
            //        int w = hcount;  //记录起始位置
            //        for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
            //        {
            //            if (FpSpread1.Sheets[0].Cells[i, 0].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, 0].Value.ToString())
            //            {
            //                k++;
            //            }
            //            else
            //            {
            //                FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
            //                w = i;
            //                k = 1;
            //            }
            //        }
            //        FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
            //    }
            //    else//不为空
            //    {
            //        string path = "../../../static/excel/jdnianduQC.xls";
            //        path = Page.MapPath(path);
            //        this.FpSpread1.Sheets[0].OpenExcel(path, "表9-油井操作成本基础数据本表");

            //        /////////////////////
            //        //OracleConnection con0 = DB.CreatConnection();
            //        //con0.Open();
            //        //string cycid = Session["cyc"].ToString();
            //        //string cycname = "";
            //        //string scyc = "select dep_id from department where dep_id = '" + cycid + "'";
            //        //OracleCommand comcyc = new OracleCommand(scyc, con0);
            //        //OracleDataReader drcyc = comcyc.ExecuteReader();
            //        //drcyc.Read();
            //        //if (drcyc.HasRows)
            //        //{
            //        //    cycname = drcyc[0].ToString();
            //        //}
            //        //drcyc.Close();
            //        //con0.Close();

            //        this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + "  " + FpSpread1.Sheets[0].Cells[0, 0].Text;
            //        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
            //        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            //        /////////////////////

            //        FpSpread1.Sheets[0].AddRows(hcount, rcount);
            //        for (int i = 0; i < rcount; i++)
            //        {
            //            for (int j = 0; j < ccount; j++)
            //            {
            //                //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
            //                if (isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
            //                {
            //                    FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
            //                }
            //                else
            //                {
            //                    FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
            //                }
            //                FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
            //            }
            //        }
            //        int k = 1;  //统计重复单元格
            //        int w = hcount;  //记录起始位置
            //        for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
            //        {
            //            if (FpSpread1.Sheets[0].Cells[i, 0].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, 0].Value.ToString())
            //            {
            //                k++;
            //            }
            //            else
            //            {
            //                FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
            //                w = i;
            //                k = 1;
            //            }
            //        }
            //        FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
            //    }
            //    #endregion

            //}
            //catch (OracleException error)
            //{
            //    string CuoWu = "错误: " + error.Message.ToString();
            //    Response.Write(CuoWu);

            //}

            //connfp.Close();
            #endregion
        }

        private void FillQKSpread(DataSet ds, string typeid)
        {
            int rcount = ds.Tables[0].Rows.Count;
            int ccount = ds.Tables[0].Columns.Count;
            int hcount = 3;

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
                FpSpread1.Sheets[0].Cells[2, 2].Text = FpSpread1.Sheets[0].Cells[2, 2].Text.Replace("区块", "评价单元");
            }
            else if (typeid == "qk")
            {
                FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text.Replace("评价单元", "区块");
                FpSpread1.Sheets[0].Cells[2, 2].Text = FpSpread1.Sheets[0].Cells[2, 2].Text.Replace("评价单元", "区块");
            }
            FpSpread1.Width = Unit.Pixel(1733);
        }

        private void FillDJSpread(DataSet ds)
        {
            int rcount = ds.Tables[0].Rows.Count;
            int ccount = ds.Tables[0].Columns.Count;
            int hcount = 3;

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
            FpSpread1.Width = Unit.Pixel(1433);
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
                fpselect = " Select qkxyjb.xymc,1 As od,pj.gsxyjb,pj.ssyt,pj.mc as pjdymc, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjclf   )/Sum(trqspl) End),2) As zjclf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjrlf   )/Sum(trqspl) End),2) As zjrlf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjdlf   )/Sum(trqspl) End),2) As zjdlf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjryf   )/Sum(trqspl) End),2) As zjryf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qywzrf  )/Sum(trqspl) End),2) As qywzrf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(jxzyf   )/Sum(trqspl) End),2) As jxzyf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cjsjf   )/Sum(trqspl) End),2) As cjsjf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(whxlf   )/Sum(trqspl) End),2) As whxlf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cyrcf   )/Sum(trqspl) End),2) As cyrcf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(yqclf   )/Sum(trqspl) End),2) As yqclf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qthsf   )/Sum(trqspl) End),2) As qthsf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(trqjhf  )/Sum(trqspl) End),2) As trqjhf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ysf     )/Sum(trqspl) End),2) As ysf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qtzjf   )/Sum(trqspl) End),2) As qtzjf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ckglf   )/Sum(trqspl) End),2) As ckglf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zyyqcp  )/Sum(trqspl) End),2) As zyyqcp, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(czcb    )/Sum(trqspl) End),2) As czcb, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(czcb )/Sum(trqspl) End),2) As czcb_my ";
                //fpselect += " round((Case When sum(dtl) = 0 Then 0 Else Sum(czcb_my )/Sum(dtl) End),2) As czcb_my ";
                fpselect += " From jdstat_qksj pj,qkxyjb ";
                fpselect += " where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
                fpselect += " and pj.gsxyjb = qkxyjb.xyjb  and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
                fpselect += " Group By pj.gsxyjb,pj.ssyt,pj.mc,qkxyjb.xymc ";

                fpselect += " Union ";
                fpselect += " Select qkxyjb.xymc,2 As od,pj.gsxyjb,'' As ssyt,'合计' As pjdymc, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjclf   )/Sum(trqspl) End),2) As zjclf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjrlf   )/Sum(trqspl) End),2) As zjrlf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjdlf   )/Sum(trqspl) End),2) As zjdlf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjryf   )/Sum(trqspl) End),2) As zjryf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qywzrf  )/Sum(trqspl) End),2) As qywzrf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(jxzyf   )/Sum(trqspl) End),2) As jxzyf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cjsjf   )/Sum(trqspl) End),2) As cjsjf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(whxlf   )/Sum(trqspl) End),2) As whxlf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cyrcf   )/Sum(trqspl) End),2) As cyrcf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(yqclf   )/Sum(trqspl) End),2) As yqclf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qthsf   )/Sum(trqspl) End),2) As qthsf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(trqjhf  )/Sum(trqspl) End),2) As trqjhf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ysf     )/Sum(trqspl) End),2) As ysf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qtzjf   )/Sum(trqspl) End),2) As qtzjf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ckglf   )/Sum(trqspl) End),2) As ckglf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zyyqcp  )/Sum(trqspl) End),2) As zyyqcp, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(czcb    )/Sum(trqspl) End),2) As czcb, ";
                fpselect += " round((Case When sum(dtl) = 0 Then 0 Else Sum(czcb_my )/Sum(dtl) End),4) As czcb_my ";
                fpselect += " From jdstat_qksj pj,qkxyjb ";
                fpselect += " where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
                fpselect += " and pj.gsxyjb = qkxyjb.xyjb  and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
                fpselect += " Group By pj.gsxyjb,qkxyjb.xymc  ";

                fpselect += " Union ";
                fpselect += " Select qkxyjb.xymc,3 As od,90000,'' As ssyt,'总计' As pjdymc, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjclf   )/Sum(trqspl) End),2) As zjclf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjrlf   )/Sum(trqspl) End),2) As zjrlf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjdlf   )/Sum(trqspl) End),2) As zjdlf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjryf   )/Sum(trqspl) End),2) As zjryf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qywzrf  )/Sum(trqspl) End),2) As qywzrf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(jxzyf   )/Sum(trqspl) End),2) As jxzyf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cjsjf   )/Sum(trqspl) End),2) As cjsjf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(whxlf   )/Sum(trqspl) End),2) As whxlf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cyrcf   )/Sum(trqspl) End),2) As cyrcf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(yqclf   )/Sum(trqspl) End),2) As yqclf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qthsf   )/Sum(trqspl) End),2) As qthsf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(trqjhf  )/Sum(trqspl) End),2) As trqjhf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ysf     )/Sum(trqspl) End),2) As ysf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qtzjf   )/Sum(trqspl) End),2) As qtzjf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ckglf   )/Sum(trqspl) End),2) As ckglf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zyyqcp  )/Sum(trqspl) End),2) As zyyqcp, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(czcb    )/Sum(trqspl) End),2) As czcb, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(czcb )/Sum(trqspl) End),2) As czcb_my ";
                //fpselect += " round((Case When sum(dtl) = 0 Then 0 Else Sum(czcb_my )/Sum(dtl) End),2) As czcb_my ";
                fpselect += " From jdstat_qksj pj,qkxyjb ";
                fpselect += " where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
                fpselect += " and qkxyjb.xyjb= '90000' and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
                fpselect += " Group By qkxyjb.xymc  ";
                fpselect += " order by gsxyjb,od ";
            }
            else
            {
                fpselect = " Select qkxyjb.xymc,1 As od,pj.gsxyjb,pj.ssyt,pj.mc as pjdymc, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjclf   )/Sum(trqspl) End),2) As zjclf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjrlf   )/Sum(trqspl) End),2) As zjrlf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjdlf   )/Sum(trqspl) End),2) As zjdlf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjryf   )/Sum(trqspl) End),2) As zjryf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qywzrf  )/Sum(trqspl) End),2) As qywzrf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(jxzyf   )/Sum(trqspl) End),2) As jxzyf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cjsjf   )/Sum(trqspl) End),2) As cjsjf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(whxlf   )/Sum(trqspl) End),2) As whxlf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cyrcf   )/Sum(trqspl) End),2) As cyrcf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(yqclf   )/Sum(trqspl) End),2) As yqclf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qthsf   )/Sum(trqspl) End),2) As qthsf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(trqjhf  )/Sum(trqspl) End),2) As trqjhf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ysf     )/Sum(trqspl) End),2) As ysf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qtzjf   )/Sum(trqspl) End),2) As qtzjf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ckglf   )/Sum(trqspl) End),2) As ckglf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zyyqcp  )/Sum(trqspl) End),2) As zyyqcp, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(czcb    )/Sum(trqspl) End),2) As czcb, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(czcb )/Sum(trqspl) End),2) As czcb_my ";
                //fpselect += " round((Case When sum(dtl) = 0 Then 0 Else Sum(czcb_my )/Sum(dtl) End),2) As czcb_my ";
                fpselect += " From jdstat_pjdysj pj,qkxyjb ";
                fpselect += " where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
                fpselect += " and pj.gsxyjb = qkxyjb.xyjb  and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
                fpselect += " Group By pj.gsxyjb,pj.ssyt,pj.mc,qkxyjb.xymc ";

                fpselect += " Union ";
                fpselect += " Select qkxyjb.xymc,2 As od,pj.gsxyjb,'' As ssyt,'合计' As pjdymc, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjclf   )/Sum(trqspl) End),2) As zjclf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjrlf   )/Sum(trqspl) End),2) As zjrlf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjdlf   )/Sum(trqspl) End),2) As zjdlf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjryf   )/Sum(trqspl) End),2) As zjryf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qywzrf  )/Sum(trqspl) End),2) As qywzrf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(jxzyf   )/Sum(trqspl) End),2) As jxzyf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cjsjf   )/Sum(trqspl) End),2) As cjsjf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(whxlf   )/Sum(trqspl) End),2) As whxlf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cyrcf   )/Sum(trqspl) End),2) As cyrcf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(yqclf   )/Sum(trqspl) End),2) As yqclf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qthsf   )/Sum(trqspl) End),2) As qthsf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(trqjhf  )/Sum(trqspl) End),2) As trqjhf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ysf     )/Sum(trqspl) End),2) As ysf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qtzjf   )/Sum(trqspl) End),2) As qtzjf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ckglf   )/Sum(trqspl) End),2) As ckglf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zyyqcp  )/Sum(trqspl) End),2) As zyyqcp, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(czcb    )/Sum(trqspl) End),2) As czcb, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(czcb )/Sum(trqspl) End),2) As czcb_my ";
                //fpselect += " round((Case When sum(dtl) = 0 Then 0 Else Sum(czcb_my )/Sum(dtl) End),2) As czcb_my ";
                fpselect += " From jdstat_pjdysj pj,qkxyjb ";
                fpselect += " where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
                fpselect += " and pj.gsxyjb = qkxyjb.xyjb  and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
                fpselect += " Group By pj.gsxyjb,qkxyjb.xymc  ";

                fpselect += " Union ";
                fpselect += " Select qkxyjb.xymc,3 As od,90000,'' As ssyt,'总计' As pjdymc, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjclf   )/Sum(trqspl) End),2) As zjclf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjrlf   )/Sum(trqspl) End),2) As zjrlf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjdlf   )/Sum(trqspl) End),2) As zjdlf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zjryf   )/Sum(trqspl) End),2) As zjryf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qywzrf  )/Sum(trqspl) End),2) As qywzrf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(jxzyf   )/Sum(trqspl) End),2) As jxzyf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cjsjf   )/Sum(trqspl) End),2) As cjsjf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(whxlf   )/Sum(trqspl) End),2) As whxlf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(cyrcf   )/Sum(trqspl) End),2) As cyrcf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(yqclf   )/Sum(trqspl) End),2) As yqclf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qthsf   )/Sum(trqspl) End),2) As qthsf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(trqjhf  )/Sum(trqspl) End),2) As trqjhf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ysf     )/Sum(trqspl) End),2) As ysf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(qtzjf   )/Sum(trqspl) End),2) As qtzjf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(ckglf   )/Sum(trqspl) End),2) As ckglf, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(zyyqcp  )/Sum(trqspl) End),2) As zyyqcp, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(czcb    )/Sum(trqspl) End),2) As czcb, ";
                fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else Sum(czcb )/Sum(trqspl) End),2) As czcb_my ";
                //fpselect += " round((Case When sum(dtl) = 0 Then 0 Else Sum(czcb_my )/Sum(dtl) End),2) As czcb_my ";
                fpselect += " From jdstat_pjdysj pj,qkxyjb ";
                fpselect += " where pj.bny = " + bny + " and pj.eny = " + eny + " and sfpj = 1 ";
                fpselect += " and qkxyjb.xyjb= '90000' and pj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
                fpselect += " Group By qkxyjb.xymc  ";
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
                Fptable.Columns.Add("ssyt", typeof(System.String));
                Fptable.Columns.Add("pjdymc", typeof(System.String));
                Fptable.Columns.Add("zjclf", typeof(float));
                Fptable.Columns.Add("zjrlf", typeof(float));
                Fptable.Columns.Add("zjdlf", typeof(float));
                Fptable.Columns.Add("zjryf", typeof(float));
                Fptable.Columns.Add("qywzrf", typeof(float));
                Fptable.Columns.Add("jxzyf", typeof(float));
                Fptable.Columns.Add("cjsjf", typeof(string));
                Fptable.Columns.Add("whxlf", typeof(float));
                Fptable.Columns.Add("cyrcf", typeof(float));
                Fptable.Columns.Add("yqclf", typeof(float));
                Fptable.Columns.Add("qthsf", typeof(float));
                Fptable.Columns.Add("trqjhf", typeof(float));
                Fptable.Columns.Add("ysf", typeof(float));
                Fptable.Columns.Add("qtzjf", typeof(float));
                Fptable.Columns.Add("ckglf", typeof(float));
                Fptable.Columns.Add("zyyqcp", typeof(float));
                Fptable.Columns.Add("czcb", typeof(float));
                Fptable.Columns.Add("czcb_my", typeof(float));
                DataRow Fprow;
                int n = 1;
                while (myReader.Read())
                {
                    Fprow = Fptable.NewRow();

                    Fprow[0] = myReader[0];
                    Fprow[1] = n++;
                    Fprow[2] = myReader[3];
                    Fprow[3] = myReader.GetValue(4);
                    Fprow[4] = myReader[5];
                    Fprow[5] = myReader.GetValue(6);
                    Fprow[6] = myReader.GetValue(7);
                    Fprow[7] = myReader.GetValue(8);
                    Fprow[8] = myReader.GetValue(9);
                    Fprow[9] = myReader[10];
                    Fprow[10] = myReader[11];
                    Fprow[11] = myReader.GetValue(12);
                    Fprow[12] = myReader[13];
                    Fprow[13] = myReader.GetValue(14);
                    Fprow[14] = myReader.GetValue(15);
                    Fprow[15] = myReader.GetValue(16);
                    Fprow[16] = myReader.GetValue(17);
                    Fprow[17] = myReader.GetValue(18);
                    Fprow[18] = myReader.GetValue(19);
                    Fprow[19] = myReader.GetValue(20);
                    Fprow[20] = myReader.GetValue(21);
                    Fprow[21] = myReader.GetValue(22);

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
                            if (j != 1 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
                            }
                            else
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            }
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
                    string path = "../../../static/excel/jdnianduQC.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表10-区块操作成本基础数据表");

                    this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    this.FpSpread1.Sheets[0].RowHeader.Visible = false;

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            if (j != 1 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
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
            { FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao9.xls"); }
            else
            { FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao10.xls"); }

        }


    }
}

