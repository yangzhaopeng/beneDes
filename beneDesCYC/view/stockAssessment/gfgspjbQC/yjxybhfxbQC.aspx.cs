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

namespace beneDesCYC.view.stockAssessment.gfgspjbQC
{
    public partial class yjxybhfxbQC : beneDesCYC.core.UI.corePage
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
            string path = "../../../static/excel/jdnianduQC.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表32-油井效益分析表");

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
            this.FpSpread1.Sheets[0].OpenExcel(path, "表32-油井效益分析表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
        }

        protected void sj()
        {

            string aa = Session["month"].ToString();
            //bny = aa.Substring(0, 4);
            int int_thisyear = int.Parse(aa.Substring(0, 4).ToString());
            int int_lastyear = int_thisyear - 1;
            string thisyear = int_thisyear.ToString();
            string lastyear = int_lastyear.ToString();

            
            /*    if (bnyString.Substring(4,2)=="13")//开始月份为13月则换表
                {
                    tableName = "jdstat_djsj_all";
                    nyColumnName = "bny";
                    thisyear = thisyear + "13";//如果从jdstat_djsj表取数据则包含月
                    lastyear = lastyear + "13";        
                }
        */

            DataSet fpset = new DataSet();
            var param_ny = new OracleParameter("as_ny", OracleType.VarChar);
            param_ny.Value = thisyear;
            var param_lastny = new OracleParameter("as_lastny", OracleType.VarChar);
            param_lastny.Value = lastyear;
            var param_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc.Value = Session["cqc_id"].ToString();
            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;
            try
            {
                fpset = sql.GetDataSet("OWCBS_REPORT_GFGS_Q.R32_YJXYBHFXB_QJ", CommandType.StoredProcedure,
                    new OracleParameter[] { param_ny, param_lastny, param_cyc, param_out });

                //此处用于绑定数据            
                #region
                int rcount = fpset.Tables[0].Rows.Count;
                int ccount = fpset.Tables[0].Columns.Count - 1;  //第1列不显示
                int hcount = 5;
               
                FpSpread1.Sheets[0].AddRows(hcount, rcount);
                for (int i = 0; i < rcount; i++)
                {
                    for (int j = 0; j < ccount; j++)
                    {
                        //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                        if ((j == 3 || j == 5 || j == 7 || j == 9 || j == 11) && isNotFloat.Isdouble(fpset.Tables[0].Rows[i][j + 1].ToString()) && fpset.Tables[0].Rows[i][j + 1].ToString().Trim() != "0")
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables[0].Rows[i][j + 1].ToString()).ToString("0.0000");
                        }
                        else
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables[0].Rows[i][j + 1].ToString();
                        }
                        FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                    }
                }

                #endregion
            }
            catch (OracleException error)
            {
                string CuoWu = "错误: " + error.Message.ToString();
                Response.Write(CuoWu);

            }

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

            FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao32.xls");


        }


    }
}

