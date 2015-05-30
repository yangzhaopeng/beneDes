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

namespace beneDesYGS.view.oilAssessment.jichuBiao
{
    public partial class biao5 : beneDesYGS.core.UI.corePage
    {
        static int page = 0;
        static int rcount = 0;
        static int ccount = 0;
        static int hcount = 5;
        static DataSet fpset = new DataSet();
        static bool but1click = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            string list = _getParam("CYC");
            if(list == "null")
                initSpread2();
            else
                initSpread();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }

        protected void initSpread()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/dongtai.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表5-操作成本构成表");

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
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/dongtai.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表5-操作成本构成表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

        }

        protected void sj()
        {
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //string list = _getParam("YCLX");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            string list = _getParam("CYC");
            string typeid = _getParam("targetType");

            but1click = true;
            //ButnForward.Enabled = true;
            //ButnBack.Enabled = true;
            //string cycid = Session["cyc"].ToString();
            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["ygsConnectionString"].ConnectionString);
            string fpselect = "";
            if (typeid == "yt")
            {
                fpselect += " Select   0 as xuhao, d.dep_name as dw, ";
                fpselect += " sum(nvl(round(zjclf/10000,4),0)) As zjclf, 0 as bl1,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(zjclf)/sum(yqspl) End),4) as dy1, ";

                fpselect += " sum(nvl(round(zjrlf/10000,4),0)) As zjrlf, 0 as bl2,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(zjrlf)/sum(yqspl) End),4) as dy2, ";

                fpselect += " sum(nvl(round(zjdlf/10000,4),0)) As zjdlf, 0 as bl3, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(zjdlf)/sum(yqspl) End),4) as dy3, ";

                fpselect += " sum(nvl(round(qywzrf/10000,4),0)) As qywzrf, 0 as bl4,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(qywzrf)/sum(yqspl) End),4) as dy4, ";

                fpselect += " sum(nvl(round(jxzyf/10000,4),0)) As jxzyf, 0 as bl5,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(jxzyf)/sum(yqspl) End),4) as dy5, ";

                fpselect += " sum(nvl(round(cjsjf/10000,4),0)) As cjsjf, 0 as bl6,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(cjsjf)/sum(yqspl) End),4) as dy6, ";

                fpselect += " sum(nvl(round(whxlf/10000,4),0)) As whxlf, 0 as bl7,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(whxlf)/sum(yqspl) End),4) as dy7, ";

                fpselect += " sum(nvl(round(yqclf/10000,4),0)) As yqclf, 0 as bl8,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(yqclf)/sum(yqspl) End),4) as dy8, ";

                fpselect += " sum(nvl(round(ysf/10000,4),0)) As ysf,   0 as bl9, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(ysf)/sum(yqspl) End),4) as dy9, ";

                fpselect += " sum(nvl(round(qtzjf/10000,4),0)) As qtzjf, 0 as bl10,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(qtzjf)/sum(yqspl) End),4) as dy10, ";

                fpselect += " sum(nvl(round(ckglf/10000,4),0)) As ckglf, 0 as bl11,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(ckglf)/sum(yqspl) End),4) as dy11, ";

                fpselect += " sum(nvl(round(zjryf/10000,4),0)) As zjryf, 0 as bl12,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(zjryf)/sum(yqspl) End),4) as dy12, ";

                fpselect += " sum(nvl(round(czcb/10000,4),0)) As czcb , 0 as bl13, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(czcb)/sum(yqspl) End),4) as dy13, ";

                fpselect += " nvl(round(sum(yqspl)/10000,4),0) As yqspl";
                //fpselect += " sdy.zyqdm as zyq ";

                fpselect += " From  view_dtstat_zyqsj sdy ,department d";
                fpselect += " where  sdy.djisopen = '1' and sdy.dep_id=d.dep_id  ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and  sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }

                fpselect += "group by d.dep_name ";
                fpselect += " order by d.dep_name ";
            }
            else if (typeid == "qk")
            {
                fpselect += " Select    0 as xuhao, sdy.mc as dw, ";
                fpselect += " nvl(round(zjclf/10000,4),0) As zjclf, 0 as bl1,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else zjclf/yqspl End),4) as dy1, ";

                fpselect += " nvl(round(zjrlf/10000,4),0) As zjrlf, 0 as bl2,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else zjrlf/yqspl End),4) as dy2, ";

                fpselect += " nvl(round(zjdlf/10000,4),0) As zjdlf, 0 as bl3, ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else zjdlf/yqspl End),4) as dy3, ";

                fpselect += " nvl(round(qywzrf/10000,4),0) As qywzrf, 0 as bl4,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else qywzrf/yqspl End),4) as dy4, ";

                fpselect += " nvl(round(jxzyf/10000,4),0) As jxzyf, 0 as bl5,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else jxzyf/yqspl End),4) as dy5, ";

                fpselect += " nvl(round(cjsjf/10000,4),0) As cjsjf, 0 as bl6,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else cjsjf/yqspl End),4) as dy6, ";

                fpselect += " nvl(round(whxlf/10000,4),0) As whxlf, 0 as bl7,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else whxlf/yqspl End),4) as dy7, ";

                fpselect += " nvl(round(yqclf/10000,4),0) As yqclf, 0 as bl8,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else yqclf/yqspl End),4) as dy8, ";

                fpselect += " nvl(round(ysf/10000,4),0) As ysf,   0 as bl9, ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else ysf/yqspl End),4) as dy9, ";

                fpselect += " nvl(round(qtzjf/10000,4),0) As qtzjf, 0 as bl10,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else qtzjf/yqspl End),4) as dy10, ";

                fpselect += " nvl(round(ckglf/10000,4),0) As ckglf, 0 as bl11,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else ckglf/yqspl End),4) as dy11, ";

                fpselect += " nvl(round(zjryf/10000,4),0) As zjryf, 0 as bl12,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else zjryf/yqspl End),4) as dy12, ";

                fpselect += " nvl(round(czcb/10000,4),0) As czcb , 0 as bl13, ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else czcb/yqspl End),4) as dy13, ";

                fpselect += " nvl(round(yqspl/10000,4),0) As yqspl";

                fpselect += " From  dtstat_qksj sdy ";
                fpselect += " where  sdy.sfpj = '1 '  ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                //fpselect += " order By sdy.mc ";

            }
            else if (typeid == "yj")
            {
                fpselect += " Select    0 as xuhao, sdy.jh as dw, ";
                fpselect += " round(zjclf/10000,4) As zjclf, 0 as bl1,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else zjclf/yqspl End),4) as dy1, ";

                fpselect += " round(zjrlf/10000,4) As zjrlf, 0 as bl2,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else zjrlf/yqspl End),4) as dy2, ";

                fpselect += " round(zjdlf/10000,4) As zjdlf, 0 as bl3, ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else zjdlf/yqspl End),4) as dy3, ";

                fpselect += " round(qywzrf/10000,4) As qywzrf, 0 as bl4,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else qywzrf/yqspl End),4) as dy4, ";

                fpselect += " round(jxzyf/10000,4) As jxzyf, 0 as bl5,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else jxzyf/yqspl End),4) as dy5, ";

                fpselect += " round(cjsjf/10000,4) As cjsjf, 0 as bl6,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else cjsjf/yqspl End),4) as dy6, ";

                fpselect += " round(whxlf/10000,4) As whxlf, 0 as bl7,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else whxlf/yqspl End),4) as dy7, ";

                fpselect += " round(yqclf/10000,4) As yqclf, 0 as bl8,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else yqclf/yqspl End),4) as dy8, ";

                fpselect += " round(ysf/10000,4) As ysf,   0 as bl9, ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else ysf/yqspl End),4) as dy9, ";

                fpselect += " round(qtzjf/10000,4) As qtzjf, 0 as bl10,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else qtzjf/yqspl End),4) as dy10, ";

                fpselect += " round(ckglf/10000,4) As ckglf, 0 as bl11,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else ckglf/yqspl End),4) as dy11, ";

                fpselect += " round(zjryf/10000,4) As zjryf, 0 as bl12,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else zjryf/yqspl End),4) as dy12, ";

                fpselect += " round(czcb/10000,4) As czcb , 0 as bl13, ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else czcb/yqspl End),4) as dy13, ";

                fpselect += " nvl(round(yqspl/10000,4),0) As yqspl";

                fpselect += " From  dtstat_djsj sdy ";
                fpselect += " where  sdy.djisopen = '1 '  ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                //fpselect += " order By sdy.jh ";

            }
            else if (typeid == "pjdy")
            {
                fpselect += " Select    0 as xuhao, sdy.mc as dw, ";
                fpselect += " round(zjclf/10000,4) As zjclf, 0 as bl1,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else zjclf/yqspl End),4) as dy1, ";
                fpselect += " round(zjrlf/10000,4) As zjrlf, 0 as bl2,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else zjrlf/yqspl End),4) as dy2, ";
                fpselect += " round(zjdlf/10000,4) As zjdlf, 0 as bl3, ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else zjdlf/yqspl End),4) as dy3, ";
                fpselect += " round(qywzrf/10000,4) As qywzrf, 0 as bl4,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else qywzrf/yqspl End),4) as dy4, ";
                fpselect += " round(jxzyf/10000,4) As jxzyf, 0 as bl5,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else jxzyf/yqspl End),4) as dy5, ";
                fpselect += " round(cjsjf/10000,4) As cjsjf, 0 as bl6,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else cjsjf/yqspl End),4) as dy6, ";
                fpselect += " round(whxlf/10000,4) As whxlf, 0 as bl7,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else whxlf/yqspl End),4) as dy7, ";
                fpselect += " round(yqclf/10000,4) As yqclf, 0 as bl8,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else yqclf/yqspl End),4) as dy8, ";
                fpselect += " round(ysf/10000,4) As ysf,   0 as bl9, ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else ysf/yqspl End),4) as dy9, ";
                fpselect += " round(qtzjf/10000,4) As qtzjf, 0 as bl10,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else qtzjf/yqspl End),4) as dy10, ";
                fpselect += " round(ckglf/10000,4) As ckglf, 0 as bl11,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else ckglf/yqspl End),4) as dy11, ";
                fpselect += " round(zjryf/10000,4) As zjryf, 0 as bl12,  ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else zjryf/yqspl End),4) as dy12, ";
                fpselect += " round(czcb/10000,4) As czcb , 0 as bl13, ";
                fpselect += " round((Case When yqspl = 0 Then 0 Else czcb/yqspl End),4) as dy13, ";

                fpselect += " nvl(round(yqspl/10000,4),0) As yqspl";

                fpselect += " From  dtstat_pjdysj sdy ";
                fpselect += " where  sdy.sfpj = '1 '  ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                //fpselect += " order By sdy.mc ";
            }
            else if (typeid == "zyq")
            {
                fpselect += " Select  d.dep_name,  0 as xuhao, sdy.zyq as dw, ";
                fpselect += " sum(nvl(round(zjclf/10000,4),0)) As zjclf, 0 as bl1,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(zjclf)/sum(yqspl) End),4) as dy1, ";

                fpselect += " sum(nvl(round(zjrlf/10000,4),0)) As zjrlf, 0 as bl2,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(zjrlf)/sum(yqspl) End),4) as dy2, ";

                fpselect += " sum(nvl(round(zjdlf/10000,4),0)) As zjdlf, 0 as bl3, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(zjdlf)/sum(yqspl) End),4) as dy3, ";

                fpselect += " sum(nvl(round(qywzrf/10000,4),0)) As qywzrf, 0 as bl4,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(qywzrf)/sum(yqspl) End),4) as dy4, ";

                fpselect += " sum(nvl(round(jxzyf/10000,4),0)) As jxzyf, 0 as bl5,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(jxzyf)/sum(yqspl) End),4) as dy5, ";

                fpselect += " sum(nvl(round(cjsjf/10000,4),0)) As cjsjf, 0 as bl6,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(cjsjf)/sum(yqspl) End),4) as dy6, ";

                fpselect += " sum(nvl(round(whxlf/10000,4),0)) As whxlf, 0 as bl7,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(whxlf)/sum(yqspl) End),4) as dy7, ";

                fpselect += " sum(nvl(round(yqclf/10000,4),0)) As yqclf, 0 as bl8,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(yqclf)/sum(yqspl) End),4) as dy8, ";

                fpselect += " sum(nvl(round(ysf/10000,4),0)) As ysf,   0 as bl9, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(ysf)/sum(yqspl) End),4) as dy9, ";

                fpselect += " sum(nvl(round(qtzjf/10000,4),0)) As qtzjf, 0 as bl10,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(qtzjf)/sum(yqspl) End),4) as dy10, ";

                fpselect += " sum(nvl(round(ckglf/10000,4),0)) As ckglf, 0 as bl11,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(ckglf)/sum(yqspl) End),4) as dy11, ";

                fpselect += " sum(nvl(round(zjryf/10000,4),0)) As zjryf, 0 as bl12,  ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(zjryf)/sum(yqspl) End),4) as dy12, ";

                fpselect += " sum(nvl(round(czcb/10000,4),0)) As czcb , 0 as bl13, ";
                fpselect += " round((Case When sum(yqspl) = 0 Then 0 Else sum(czcb)/sum(yqspl) End),4) as dy13, ";

                fpselect += " nvl(round(sum(yqspl)/10000,4),0) As yqspl,";
                fpselect += " sdy.zyqdm as zyq ";

                fpselect += " From  view_dtstat_zyqsj sdy ,department d";
                fpselect += " where  sdy.djisopen = '1' and sdy.dep_id=d.dep_id  ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and  sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }

                fpselect += "group by d.dep_name,sdy.zyq,sdy.zyqdm ";
                fpselect += " order by d.dep_name,sdy.zyqdm ";
            }

            try
            {
                connfp.Open();

                OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                //DataSet fpset = new DataSet();
                //da.Fill(fpset, "fpdata");

                //合计
                DataTable dt = new DataTable("fpdata");
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (typeid == "zyq")
                    {
                        DataRow dr = dt.NewRow();

                        //初始化各列
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            dr[i] = 0;
                        }
                        //dr[0] = " ";
                        dr[2] = "合计";
                        //计算
                        for (int k = 0; k < dt.Rows.Count; k++)//循环计算各行
                        {

                            for (int n = 3; n < dt.Columns.Count - 1; n++)//循环计算各列
                            {
                                if (dt.Rows[k][n].ToString() != "")
                                {
                                    dr[n] = float.Parse(dr[n].ToString()) + float.Parse(dt.Rows[k][n].ToString());
                                }
                                else
                                    dr[n] = float.Parse(dr[n].ToString()) + 0;
                            }
                        }
                        //计算精度
                        for (int m = 3; m < dt.Columns.Count - 1; m++)
                        {
                            dr[m] = Math.Round(float.Parse(dr[m].ToString()), 4);
                        }
                        //把行加入表
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();

                        //初始化各列
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            dr[i] = 0;
                        }
                        //dr[0] = " ";
                        dr[1] = "合计";
                        //计算
                        for (int k = 0; k < dt.Rows.Count; k++)//循环计算各行
                        {

                            for (int n = 2; n < dt.Columns.Count; n++)//循环计算各列
                            {
                                if (dt.Rows[k][n].ToString() != "")
                                {
                                    dr[n] = float.Parse(dr[n].ToString()) + float.Parse(dt.Rows[k][n].ToString());
                                }
                                else
                                    dr[n] = float.Parse(dr[n].ToString()) + 0;
                            }
                        }
                        //计算精度
                        for (int m = 2; m < dt.Columns.Count; m++)
                        {
                            dr[m] = Math.Round(float.Parse(dr[m].ToString()), 4);
                        }
                        //把行加入表
                        dt.Rows.Add(dr);
                    }
                }

                //DataSet fpset = new DataSet();
                fpset.Tables.Clear();
                fpset.Tables.Add(dt);
                //此处用于绑定数据            
                #region
                rcount = fpset.Tables["fpdata"].Rows.Count;
                ccount = fpset.Tables["fpdata"].Columns.Count - 1;
                hcount = 5;
                //int xuhao = 1;//序号
                //FpSpread1.ColumnHeader.Visible = false;
                //FpSpread1.RowHeader.Visible = false;

                if (typeid == "zyq")
                {
                    string path = "../../../static/excel/zuoyequ.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表5-操作成本构成表");

                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {

                        for (int j = 0; j < ccount - 1; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            if ((j % 3 == 0) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                            }
                            else if ((j % 3 != 0) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
                            }
                            else
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            }
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            //添加序号
                            FpSpread1.Sheets[0].Cells[i + hcount, 1].Value = i + 1;
                        }
                    }

                    FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 0].Value = "";
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 3; j < ccount - 1; j += 3)
                        {
                            float hj = float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, j].Value.ToString());
                            float blx = float.Parse(FpSpread1.Sheets[0].Cells[i + hcount, j].Value.ToString());
                            if (hj != 0)
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j + 1].Value = Math.Round(blx / hj, 4) * 100;
                            }
                            else
                                FpSpread1.Sheets[0].Cells[i + hcount, j + 1].Value = 0;
                        }
                    }
                    //计算吨油
                    for (int j = 5; j < ccount - 1; j += 3)
                    {
                        float spl = float.Parse(fpset.Tables["fpdata"].Rows[rcount - 1]["yqspl"].ToString());
                        float dy = float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, j - 2].Value.ToString());
                        FpSpread1.Sheets[0].Cells[rcount + hcount - 1, j].Value = Math.Round(dy / spl, 2);
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
                    page = 0;
                    Fp_DataBound(rcount, ccount, hcount, fpset, page);
                }
                /////////////////////09.4.29更新//////////////////////////////////
                if (typeid == "yt")
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "采油厂";
                else if (typeid == "yj")
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "井号";
                else if (typeid == "pjdy")
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "评价单元";
                else if (typeid == "qk")
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "评价区块";
                else
                    FpSpread1.Sheets[0].Cells[2, 2].Value = "作业区";
                ////////////////////////////////////////////////////////////////////
                /////////////////////////09.4.30更新//////////////////////////////////////
                if (typeid == "yt")
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text + "(采油厂)";
                }
                else if (typeid == "zyq")
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text + "(作业区)";
                }
                else if (typeid == "pjdy")
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text + "(评价单元)";
                }
                else if (typeid == "qk")
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text + "(区块)";
                }
                else
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text + "(油井)";
                }
                ////////////////////////////////////////////////////////////////////////////
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
            string ss = "select bny,eny from dtstat_djsj where bny='" + bny + "' and eny='" + eny + "'";
            OracleDataAdapter da = new OracleDataAdapter(ss, con1);
            DataSet ds = new DataSet();
            da.Fill(ds, "time");
            con1.Close();
            if (ds.Tables["time"].Rows.Count == 0)
                return 0;
            else
                return 1;

        }

        protected void QIAN_Click(object sender, EventArgs e)
        {
            string typeid = _getParam("targetType");

            if (typeid == "yj")
          {
            int.TryParse(this.currentpage.Text, out page);
            page = page - 1;
            if (page >= 0)
            {
                Fp_DataBound(rcount, ccount, hcount, fpset, page);
                this.currentpage.Text = page.ToString();
            }
           }
        }
        protected void HOU_Click(object sender, EventArgs e)
        {
            string typeid = _getParam("targetType");

          if (typeid == "yj")
          {
            int.TryParse(this.currentpage.Text, out page);
            //if (page >= 1)
            //{ page = 0; }
            page = page + 1;
            Fp_DataBound(rcount, ccount, hcount, fpset, page);
            this.currentpage.Text = page.ToString();
           }
        }

        protected void Fp_DataBound(int rcount, int ccount, int hcount, DataSet fpset, int page)
        {
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //if (page == 0)
                //ButnBack.Enabled = false;
            //else
                //ButnBack.Enabled = true;
            try
            {
                int count = 80;
                int pagenumber = 80;
                if (rcount < pagenumber)
                    count = rcount;
                if (rcount - page * pagenumber < 80)
                {
                    count = rcount - page * pagenumber;
                    //ButnForward.Enabled = false;
                }
                //else
                    //ButnForward.Enabled = true;
                string path = "../../../static/excel/dongtai.xls";
                path = Page.MapPath(path);
                this.FpSpread1.Sheets[0].OpenExcel(path, "表5-操作成本构成表");

                FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                FpSpread1.Sheets[0].RowHeader.Visible = false;


                FpSpread1.Sheets[0].AddRows(hcount, count);
                for (int i = 0; i < count; i++)
                {

                    for (int j = 0; j < ccount; j++)
                    {
                        //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i + page * pagenumber][j].ToString();
                        if ((j % 3 == 0 || (j - 1) % 3 == 0) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i + page * pagenumber][j].ToString()) && fpset.Tables["fpdata"].Rows[i + page * pagenumber][j].ToString().Trim() != "0")
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i + page * pagenumber][j].ToString()).ToString("0.00");
                        }
                        else if ((j % 3 != 0) && (j - 1) % 3 != 0 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i + page * pagenumber][j].ToString()) && fpset.Tables["fpdata"].Rows[i + page * pagenumber][j].ToString().Trim() != "0")
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i + page * pagenumber][j].ToString()).ToString("0.0000");
                        }
                        else
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i + page * pagenumber][j].ToString();
                        }
                        FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        //添加序号
                        FpSpread1.Sheets[0].Cells[i + hcount, 0].Value = i + page * pagenumber + 1;
                    }
                }

                ////计算比例
                //FpSpread1.Sheets[0].Cells[rcount + hcount - 1, 0].Value = "";
                for (int i = 0; i < count; i++)
                {
                    for (int j = 2; j < ccount; j += 3)
                    {
                        //float hj = float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, j].Value.ToString());
                        float hj = float.Parse(fpset.Tables["fpdata"].Rows[rcount - 1][j].ToString());
                        float blx = float.Parse(FpSpread1.Sheets[0].Cells[i + hcount, j].Value.ToString());
                        if (hj != 0)
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j + 1].Value = Math.Round(blx / hj, 4) * 100;
                        }
                        else
                            FpSpread1.Sheets[0].Cells[i + hcount, j + 1].Value = 0;
                    }
                }
                //计算合计吨油
                for (int j = 4; j < ccount; j += 3)
                {
                    float spl = float.Parse(fpset.Tables["fpdata"].Rows[rcount - 1]["yqspl"].ToString());
                    float dy = float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, j - 2].Value.ToString());
                    FpSpread1.Sheets[0].Cells[rcount + hcount - 1, j].Value = Math.Round(dy / spl, 2);
                    //float dy = float.Parse(fpset.Tables["fpdata"].Rows[rcount - 1][j - 2].ToString());

                }

                ////计算合计吨油
                //for (int j = 4; j < ccount; j += 3)
                //{
                //    float spl = float.Parse(fpset.Tables["fpdata"].Rows[rcount - 1]["yqspl"].ToString());
                //    float dy = float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, j - 2].Value.ToString());
                //    FpSpread1.Sheets[0].Cells[rcount + hcount - 1, j].Value = Math.Round(dy / spl, 4);
                //float dy = float.Parse(fpset.Tables["fpdata"].Rows[rcount - 1][j - 2].ToString());

                //}
            }
            catch
            { }
        }

        protected void DC_Click(object sender, EventArgs e)
        {
            string list = _getParam("CYC");
            if (list == "null")
                return;

            string typeid = _getParam("targetType");
            if (typeid == "yj")
            {
                string path = "../../../static/excel/dongtai.xls";
                path = Page.MapPath(path);
                this.FpSpread1.Sheets[0].OpenExcel(path, "表5-操作成本构成表");

                FpSpread2.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text;
                FpSpread2.Sheets[0].ColumnHeader.Visible = false;
                FpSpread2.Sheets[0].RowHeader.Visible = false;
                if (but1click == true)
                {
                    //把数据先放入FpSpread2
                    int rcount = fpset.Tables["fpdata"].Rows.Count;
                    int ccount = fpset.Tables["fpdata"].Columns.Count - 1;
                    int hcount = 5;

                    FpSpread2.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread2.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            if ((j % 3 == 0 || (j - 1) % 3 == 0) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                            {
                                FpSpread2.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
                            }
                            else if ((j % 3 != 0) && (j - 1) % 3 != 0 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                            {
                                FpSpread2.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                            }
                            else
                            {
                                FpSpread2.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            }
                            FpSpread2.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            FpSpread2.Sheets[0].Cells[i + hcount, 0].Value = i + 1;
                        }
                    }
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 2; j < ccount; j += 3)
                        {
                            float hj = float.Parse(fpset.Tables["fpdata"].Rows[rcount - 1][j].ToString());
                            float blx = float.Parse(FpSpread2.Sheets[0].Cells[i + hcount, j].Value.ToString());
                            if (hj != 0)
                            {
                                FpSpread2.Sheets[0].Cells[i + hcount, j + 1].Value = Math.Round(blx / hj, 4) * 100;
                            }
                            else
                                FpSpread2.Sheets[0].Cells[i + hcount, j + 1].Value = 0;
                        }
                    }
                    //计算合计吨油
                    for (int j = 4; j < ccount; j += 3)
                    {
                        float spl = float.Parse(fpset.Tables["fpdata"].Rows[rcount - 1]["yqspl"].ToString());
                        float dy = float.Parse(FpSpread2.Sheets[0].Cells[rcount + hcount - 1, j - 2].Value.ToString());
                        FpSpread2.Sheets[0].Cells[rcount + hcount - 1, j].Value = Math.Round(dy / spl, 2);
                        //float dy = float.Parse(fpset.Tables["fpdata"].Rows[rcount - 1][j - 2].ToString());

                    }

                }
                //导出FpSpread2中的数据
                FpSpread2.Sheets[0].RowHeader.Visible = true;
                FpSpread2.Sheets[0].ColumnHeader.Visible = true;
                //FpSpread2.SaveExcelToResponse("biao5.xls");
                FarpointGridChange.FarPointChange(FpSpread2, "biao5.xls");
                FpSpread2.Sheets[0].RowHeader.Visible = false;
                FpSpread2.Sheets[0].ColumnHeader.Visible = false;
            }
            else
            {
                FpSpread1.Sheets[0].RowHeader.Visible = true;
                FpSpread1.Sheets[0].ColumnHeader.Visible = true;
                //FpSpread1.SaveExcelToResponse("biao5.xls");
                FarpointGridChange.FarPointChange(FpSpread1, "biao5.xls");
                FpSpread1.Sheets[0].RowHeader.Visible = false;
                FpSpread1.Sheets[0].ColumnHeader.Visible = false;
            }
        }

    }
}
