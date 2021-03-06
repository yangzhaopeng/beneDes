﻿using System;
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
    public partial class pjcsjbb : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
        }

        /// <summary>
        /// 初始化spread
        /// </summary>
        protected void initSpread()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/jdniandu.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表1-油井评价基本参数表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

            //this.FpSpread1.Sheets[0].Cells[3, 1].Text = _getParam("targetType");
            //this.FpSpread1.Sheets[0].Cells[4, 1].Text = _getParam("startMonth") + " -- " + _getParam("endMonth");

            if (isempty() == 0)
            { Response.Write("<script>alert('无数据')</script>"); }
            else
            { sj(); }
        }

        //}
        //else
        //{
        protected void initSpread2()
        {
            string path = "../../../static/excel/jdniandu.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表1-评价单元评价基本参数表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

            if (isempty() == 0)
            { Response.Write("<script>alert('无数据')</script>"); }
            else
            { sj2(); }

            //}
        }

        /// <summary>
        /// 导出按钮点击触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DC_Click(object sender, EventArgs e)
        {
            string typeid = _getParam("targetType");
            //this.FpSpread1.Sheets[0].Cells[0, 0].Text = "您点击了‘导出按钮’";
            if (typeid == "yt")
            { FarpointGridChange.FarPointChange(FpSpread1, "jieduanniandu_biao1_yj.xls"); }
            else
            { FarpointGridChange.FarPointChange(FpSpread1, "jieduanniandu_biao1_pjdy.xls"); }
        }

        protected void sj()
        {
            //JObject obj = new JObject();
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            string list = _getParam("CYC");


            DataSet ds = new DataSet();
            var param_bny = new OracleParameter("as_begindate", OracleType.VarChar);
            param_bny.Value = bny;
            var param_eny = new OracleParameter("as_enddate", OracleType.VarChar);
            param_eny.Value = eny;
            var param_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc.Value = Session["cyc_id"].ToString();
            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;


            
            
            
            //OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);
            Int64 ny = (Convert.ToInt64(eny)) - (Convert.ToInt64(bny)) + 1;
            //string fpselect = " Select 1 as xuhao, yqlx_info.yqlxmc as yqlxmc,round((Case When Sum(hscyl) = 0 Then 0 Else Sum(yyspl)/sum(hscyl) End )*100,2) As yyspl,";
            //去掉序号列  @yzp
            
            //string tbsyj = " select round(sum(tbsyj_info.tbsyj)/" + ny + ",2) from tbsyj_info where tbsyj_info.ny>='" + bny + "' and tbsyj_info.ny<='" + eny + "' ";


            try
            {
                ds = sql.GetDataSet("OWCBS_REPORT_GFGS.R1_PJCSJBB_YJ", CommandType.StoredProcedure,
               new OracleParameter[] { param_bny, param_eny, param_cyc, param_out });

                //connfp.Open();

                //OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                //DataSet fpset = new DataSet();
                //da.Fill(fpset, "fpdata");
                //OracleDataAdapter tbsy = new OracleDataAdapter(tbsyj, connfp);
                //DataSet tbsyjds = new DataSet();
                //tbsy.Fill(tbsyjds, "tbsyj");

                //此处用于绑定数据            
                #region
                int rcount = ds.Tables[0].Rows.Count;
                int ccount = ds.Tables[0].Columns.Count;
                int hcount = 4;
                //FpSpread1.ColumnHeader.Visible = false;
                //FpSpread1.RowHeader.Visible = false;
                if (FpSpread1.Sheets[0].Rows.Count != hcount) //如果表格不为空，重新打开表格
                {
                    string path = Page.MapPath("../../../static/excel/jdniandunew.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "3评价基本参数表");

                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;
                }

                FpSpread1.Sheets[0].AddRows(hcount, rcount);
                for (int i = 0; i < rcount; i++)
                {
                    for (int j = 0; j < ccount; j++)
                    {
                        FpSpread1.Sheets[0].Cells[i + hcount, j].Value = ds.Tables[0].Rows[i][j].ToString();
                        FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        //去掉序号
                        //FpSpread1.Sheets[0].Cells[i + hcount, 0].Value = i + 1;
                    }
                    //不显示特别收益金
                    //if (FpSpread1.Sheets[0].Cells[i + hcount, 1].Value.ToString().Trim() != "天然气")
                    //    FpSpread1.Sheets[0].Cells[i + hcount, 7].Value = tbsyjds.Tables["tbsyj"].Rows[0][0].ToString();
                    //else
                    //    FpSpread1.Sheets[0].Cells[i + hcount, 7].Value = 0;
                }
                

                #endregion

            }
            catch (OracleException error)
            {
                string CuoWu = "错误: " + error.Message.ToString();
                Response.Write(CuoWu);

            }

            //connfp.Close();
        }


        protected void sj2()
        {
            //JObject obj = new JObject();
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            string list = _getParam("CYC");

            DataSet ds = new DataSet();
            var param_bny = new OracleParameter("as_begindate", OracleType.VarChar);
            param_bny.Value = bny;
            var param_eny = new OracleParameter("as_enddate", OracleType.VarChar);
            param_eny.Value = eny;
            var param_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc.Value = Session["cyc_id"].ToString();

            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;



           
            Int64 ny = (Convert.ToInt64(eny)) - (Convert.ToInt64(bny)) + 1;
            
            string tbsyj = " select round(sum(tbsyj_info.tbsyj)/" + ny + ",2) from tbsyj_info where tbsyj_info.ny>='" + bny + "' and tbsyj_info.ny<='" + eny + "' ";
            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);
            try
            {

                ds = sql.GetDataSet("OWCBS_REPORT_GFGS.R1_PJCSJBB_PJDY", CommandType.StoredProcedure,
                   new OracleParameter[] { param_bny, param_eny, param_cyc, param_out });


                connfp.Open();

                OracleDataAdapter tbsy = new OracleDataAdapter(tbsyj, connfp);
                DataSet tbsyjds = new DataSet();
                tbsy.Fill(tbsyjds, "tbsyj");
                //此处用于绑定数据            
                #region
                int rcount = ds.Tables[0].Rows.Count;
                int ccount = ds.Tables[0].Columns.Count;
                int hcount = 4;
                //FpSpread1.ColumnHeader.Visible = false;
                //FpSpread1.RowHeader.Visible = false;
                if (FpSpread1.Sheets[0].Rows.Count != hcount)
                {
                    string path = Page.MapPath("../../../static/excel/jdniandunew.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "表1-评价单元评价基本参数表");


                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;

                }
                
                FpSpread1.Sheets[0].AddRows(hcount, rcount);
                for (int i = 0; i < rcount; i++)
                {
                    for (int j = 0; j < ccount; j++)
                    {
                        if (isNotFloat.Isdouble(ds.Tables[0].Rows[i][j].ToString()) && ds.Tables[0].Rows[i][j].ToString().Trim() != "0")
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = Math.Round(double.Parse(ds.Tables[0].Rows[i][j].ToString()), 2).ToString("0.00");
                        else
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = ds.Tables[0].Rows[i][j].ToString();
                        FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        FpSpread1.Sheets[0].Cells[i + hcount, 0].Value = i + 1;
                    }
                    if (FpSpread1.Sheets[0].Cells[i + hcount, 2].Value.ToString().Trim() != "天然气")
                        FpSpread1.Sheets[0].Cells[i + hcount, 8].Value = tbsyjds.Tables["tbsyj"].Rows[0][0].ToString();
                    else
                        FpSpread1.Sheets[0].Cells[i + hcount, 8].Value = 0;
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



    }
}
