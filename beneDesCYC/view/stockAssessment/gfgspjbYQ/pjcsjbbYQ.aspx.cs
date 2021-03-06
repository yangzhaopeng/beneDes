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


namespace beneDesCYC.view.stockAssessment.gfgspjbYQ
{
    public partial class pjcsjbbYQ : beneDesCYC.core.UI.corePage
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
            string path = "../../../static/excel/jdnianduYQ.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表1-单井评价基本参数表");

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
            string path = "../../../static/excel/jdnianduYQ.xls";
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

            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);
            Int64 ny = (Convert.ToInt64(eny)) - (Convert.ToInt64(bny)) + 1;
            //string fpselect = " Select 1 as xuhao, yqlx_info.yqlxmc as yqlxmc,round((Case When Sum(hscyl) = 0 Then 0 Else Sum(yyspl)/sum(hscyl) End )*100,2) As yyspl,";
            string fpselect = " Select yqlx_info.yqlxmc as yqlxmc,round((Case When Sum(hscyl) = 0 Then 0 Else Sum(yyspl)/sum(hscyl) End )*100,2) As yyspl,";
            fpselect += " round((Case When Sum(yyspl) = 0 Then 0 Else Sum(yyxssr)/Sum(yyspl) End),2) As yyjg, ";
            fpselect += " 0 As trqjg, ";
            fpselect += " round((Case When Sum(yyspl) = 0 Then 0 Else Sum(yyxssj)/Sum(yyspl) End),2) As yysj, ";
            fpselect += " 0 As trqsj";
            fpselect += " From jdstat_djsj ,yqlx_info  ";
            fpselect += " Where  jdstat_djsj.bny =" + bny + " and jdstat_djsj.eny = " + eny + " and qkxyjb <> 99 and pjdyxyjb <> 99 and yyspl>0 and hscyl>0 ";
            try
            {
                if (Session["cyc"] == null)
                {
                    int temp = 0;
                }
            }
            catch
            {
                return;
            }

            fpselect += " and yqlx = yqlx_info.yqlxdm 　and cyc_id='" + Session["cyc_id"].ToString() + "'";
            fpselect += " Group By yqlx,yqlx_info.yqlxmc ";

            fpselect += " union ";
            //fpselect += " Select 2 as xuhao,yqlx_info.yqlxmc as yqlxmc ,round((Case When Sum(hscql) = 0 Then 0 Else Sum(trqspl)/sum(hscql) End )*100,2) As yyspl, ";
            fpselect += " Select yqlx_info.yqlxmc as yqlxmc ,round((Case When Sum(hscql) = 0 Then 0 Else Sum(trqspl)/sum(hscql) End )*100,2) As yyspl, ";
            fpselect += " 0 As yyjg, ";
            fpselect += " round((Case When Sum(trqspl) = 0 Then 0 Else Sum(trqxssr)/Sum(trqspl)/10 End),2) As trqjg, ";
            fpselect += " 0 As yysj, ";
            fpselect += " round((Case When Sum(trqspl) = 0 Then 0 Else Sum(trqxssj)/Sum(trqspl)/10 End),2) As trqsj  ";
            fpselect += " From jdstat_djsj,yqlx_info ";
            fpselect += " Where  jdstat_djsj.bny = " + bny + " and jdstat_djsj.eny = " + eny + "  and hscql>0  and  trqspl >0 and trqxssr>0 and qkxyjb <> 99 and pjdyxyjb <> 99  ";
            fpselect += " and yqlx = yqlx_info.yqlxdm and cyc_id='" + Session["cyc_id"].ToString() + "'";
            fpselect += " Group By yqlx_info.yqlxmc ";//order by xuhao";
            //string tbsyj = " select round(sum(tbsyj_info.tbsyj)/" + ny + ",2) from tbsyj_info where tbsyj_info.ny>='" + bny + "' and tbsyj_info.ny<='" + eny + "' ";


            try
            {
                connfp.Open();

                OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                DataSet fpset = new DataSet();
                da.Fill(fpset, "fpdata");
                //OracleDataAdapter tbsy = new OracleDataAdapter(tbsyj, connfp);
                DataSet tbsyjds = new DataSet();
                //tbsy.Fill(tbsyjds, "tbsyj");

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
                            //FpSpread1.Sheets[0].Cells[i + hcount, 0].Value = i + 1;
                        }
                        //if (FpSpread1.Sheets[0].Cells[i + hcount, 1].Value.ToString().Trim() != "天然气")
                        //    FpSpread1.Sheets[0].Cells[i + hcount, 7].Value = tbsyjds.Tables["tbsyj"].Rows[0][0].ToString();
                        //else
                        //    FpSpread1.Sheets[0].Cells[i + hcount, 7].Value = 0;
                    }
                }
                else//不为空
                {
                    string path = Page.MapPath("../../../static/excel/jdnianduYQ.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "表1-单井评价基本参数表");

                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;
                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            //FpSpread1.Sheets[0].Cells[i + hcount, 0].Value = i + 1;
                        }
                        //if (FpSpread1.Sheets[0].Cells[i + hcount, 1].Value.ToString().Trim() != "天然气")
                        //    FpSpread1.Sheets[0].Cells[i + hcount, 7].Value = tbsyjds.Tables["tbsyj"].Rows[0][0].ToString();
                        //else
                        //    FpSpread1.Sheets[0].Cells[i + hcount, 7].Value = 0;
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


        protected void sj2()
        {
            //JObject obj = new JObject();
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            //string list = _getParam("CYC");
            string as_cyc = Session["cyc_id"].ToString();
            string as_cqc = Session["cqc_id"].ToString();
            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);
            Int64 ny = (Convert.ToInt64(eny.Trim())) - (Convert.ToInt64(bny.Trim())) + 1;
            //string fpselect = " Select 1 as xuhao,mc As mc,";
            //string fpselect = " Select mc As mc,";
            //fpselect += " xsyp_info.xsypmc, ";
            //fpselect += " round((Case When sum(cyoul) = 0 Then 0 Else (sum(yyspl)/sum(cyoul)*100) End),2) As spl, ";
            //fpselect += " round((Case When sum(yyspl) = 0 Then 0 Else (sum(yyxssr)/sum(yyspl)) End),2) As yyjg, ";
            //fpselect += " 0 As trqjg, ";
            //fpselect += " round((Case When sum(yyspl) =0 Then 0 Else (sum(yyxssj)/sum(yyspl)) End ),2) As yysj, ";
            //fpselect += " 0 As trqsj ";
            //fpselect += " From jdstat_pjdysj,xsyp_info ";
            ////fpselect += " Where xsyp <>'X006' And bny = " + bny.Text + " and eny=" + eny.Text + " and sfpj = 1 ";
            //fpselect += " Where bny = " + bny +" and eny=" + eny + " and sfpj = 1 ";

            //fpselect += " and xsyp = xsyp_info.xsypdm and cyc_id='" + Session["cyc_id"].ToString() + "'";
            //fpselect += " group by xsyp,xsyp_info.xsypmc,mc ";

            //fpselect += " union ";
            ////fpselect += " Select 2 as xuhao ,mc ,";
            //fpselect += " Select mc ,";
            ////fpselect += " 'X006' As xsyp, ";
            //fpselect += " xsyp_info.xsypmc, ";
            //fpselect += " round((Case When sum(cql) = 0 Then 0 Else (sum(trqspl)/sum(cql*10000)*100) End),2) As spl, ";
            ////fpselect += " , ";
            //fpselect += " 0 As yyjg, ";
            //fpselect += " round((Case When sum(trqspl) = 0 Then 0 Else (sum(trqxssr)/sum(trqspl)) End),2) As trqjg,  ";
            //fpselect += " 0 As yysj, ";
            //fpselect += " round((Case When sum(trqspl) =0 Then 0 Else (sum(trqxssj)/sum(trqspl)) End ),2) As trqsj  ";
            //fpselect += " From jdstat_pjdysj ,xsyp_info";
            //fpselect += " Where bny = " + bny + " and eny=" + eny + " and cql >0 and trqspl>0 and trqspl >0 and trqxssr>0  and sfpj = 1 and cyc_id='" + Session["cyc_id"].ToString() + "'";
            //fpselect += " group by mc ";
            //fpselect += " order by mc";//,xuhao ";
            string fpselect = "   Select 1 as xuhao,pjdy.mc as pjdymc, ";
            fpselect += "   (case when (xsyp='X008' or xsyp='X009' or xsyp='X006') then '凝析油' else yp.xsypmc end) as xsypmc, ";
            fpselect += "   to_char((Case When Sum(cyoul) = 0 Then 0 Else Sum(yyspl)/sum(cyoul)/10000 End )*100,'9999990.00') As yyspl,   ";
            fpselect += "  to_char((Case When sum(yyspl) = 0 Then 0 Else (sum(yyxssr)/sum(yyspl)) End),'9999990.00') As yyjg,   ";
            fpselect += "   to_char((Case When sum(trqspl) = 0 Then 0 Else (sum(trqxssr)/sum(trqspl)) End),'9999990.00') As trqjg  , ";
            fpselect += "   to_char((Case When sum(yyspl) =0 Then 0 Else (sum(yyxssj)/sum(yyspl)) End ),'9999990.00') As yysj  , ";
            fpselect += "   to_char((Case When sum(trqspl) =0 Then 0 Else (sum(trqxssj)/sum(trqspl)) End ),'9999990.00') As trqsj  ";
            fpselect += "   From jdstat_pjdysj pjdy,xsyp_info yp Where bny = '" + bny + "' and eny='" + eny + "' and sfpj = 1 ";
            fpselect += "   and pjdy.xsyp=yp.xsypdm ";
            fpselect += "   and (regexp_like(pjdy.cyc_id,'" + as_cyc + "') or regexp_like(pjdy.cyc_id,'" + as_cqc + "'))   ";
            fpselect += "    group by pjdy.mc,yp.xsypdm,yp.xsypmc,xsyp ";
            try
            {
                connfp.Open();

                OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                DataSet fpset = new DataSet();
                da.Fill(fpset, "fpdata");
                //OracleDataAdapter tbsy = new OracleDataAdapter(tbsyj, connfp);
                //DataSet tbsyjds = new DataSet();
                //tbsy.Fill(tbsyjds, "tbsyj");

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
                            if (isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = Math.Round(double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()), 2).ToString("0.00");
                            else
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            //FpSpread1.Sheets[0].Cells[i + hcount, 0].Value = i + 1;
                        }
                        FpSpread1.Sheets[0].Cells[i + hcount, 0].Value = i;
                    }
                }
                //if (FpSpread1.Sheets[0].Rows.Count == hcount)
                //{
                //    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                //    for (int i = 0; i < rcount; i++)
                //    {
                //        for (int j = 1; j < ccount; j++)
                //        {
                //            FpSpread1.Sheets[0].Cells[i + hcount, 0].Value = i + 1;
                //            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                //            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                //        }
                //    }
                //}
                else//不为空
                {
                    string path = Page.MapPath("~/excel/jdnianduQC.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "表1-评价单元评价基本参数表");


                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            //FpSpread1.Sheets[0].Cells[i + hcount, 0].Value = i + 1;
                            if (isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = Math.Round(double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()), 2).ToString("0.00");
                            else
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            //FpSpread1.Sheets[0].Cells[i + hcount, 0].Value = i + 1;
                        }
                        //if (FpSpread1.Sheets[0].Cells[i + hcount, 2].Value.ToString().Trim() != "天然气")
                        //    FpSpread1.Sheets[0].Cells[i + hcount, 8].Value = tbsyjds.Tables["tbsyj"].Rows[0][0].ToString();
                        //else
                        //    FpSpread1.Sheets[0].Cells[i + hcount, 8].Value = 0;
                    }
                    //for (int i = 0; i < rcount; i++)
                    //{
                    //    for (int j = 1; j < ccount; j++)
                    //    {
                    //        FpSpread1.Sheets[0].Cells[i + hcount, 0].Value = i + 1;
                    //        FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                    //        FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                    //    }
                    //}
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
            string ss = "select bny,eny from jdstat_djsj where bny='" + bny + "' and eny='" + eny + "'" + " and cyc_id='" + Session["cyc_id"].ToString() + "'";
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
