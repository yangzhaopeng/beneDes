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

namespace beneDesYGS.view.stockAssessment.gfgspjbYQ
{
    public partial class yjxybhfxbYQ : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string list = _getParam("CYC");
                //if (list == "null")
                //{ initSpread2(); }
                //else
                //{ initSpread(); }
                if (!IsPostBack)
                {
                    string typeid = _getParam("targetType");
                    initSpread(typeid);
                }
            }
        }

        protected void initSpread(string typeid)
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/jdnianduYQ.xls";
            path = Page.MapPath(path);
            switch (typeid)
            {
                case "dj":
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表13-单井效益变化分析表"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    }
                    //判断模板是否加载
                    if (FpSpread1.Sheets[0].Rows.Count != 5)
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
                    break;
                case "qk":
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表13-区块效益变化分析表"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                        FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text.Replace("评价单元", "区块");
                        FpSpread1.Sheets[0].Cells[3, 1].Text = FpSpread1.Sheets[0].Cells[3, 1].Text.Replace("评价单元", "区块");
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
                    break;
                case "pjdy":
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表13-区块效益变化分析表"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                        FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text.Replace("区块", "评价单元");
                        FpSpread1.Sheets[0].Cells[3, 1].Text = FpSpread1.Sheets[0].Cells[3, 1].Text.Replace("区块", "评价单元");
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
                    break;
                default:
                    break;
            }

        }

        protected void sj(string typeid)
        {

            string aa = Session["month"].ToString();
            //bny = aa.Substring(0, 4);
            int int_thisyear = int.Parse(aa.Substring(0, 4).ToString());
            int int_lastyear = int_thisyear - 1;
            string thisyear = int_thisyear.ToString();
            string lastyear = int_lastyear.ToString();

            string[] cyc_ids = _getParamArr("CYC");
            string as_cyc = cyc_ids[0];
            string as_cqc = cyc_ids[0];
            if (cyc_ids.Length > 1)
                as_cqc = cyc_ids[1];

            DataSet ds = new DataSet();
            var param_bny = new OracleParameter("lastyear", OracleType.VarChar);
            param_bny.Value = lastyear;
            var param_eny = new OracleParameter("thisyear", OracleType.VarChar);
            param_eny.Value = thisyear;
            var param_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc.Value = as_cyc;
            var param_cqc = new OracleParameter("as_cqc", OracleType.VarChar);
            param_cqc.Value = as_cqc;
            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;


            try
            {
                if (typeid == "dj")
                {
                    ds = sql.GetDataSet("OWCBS_VIEW_XYBHFXB.XYBHFXB_DJ_PROC", CommandType.StoredProcedure,
                       new OracleParameter[] { param_bny, param_eny, param_cyc, param_cqc, param_out });
                }
                else if (typeid == "qk")
                {
                    ds = sql.GetDataSet("OWCBS_VIEW_XYBHFXB.XYBHFXB_QK_PROC", CommandType.StoredProcedure,
                             new OracleParameter[] { param_bny, param_eny, param_cyc, param_cqc, param_out });
                }
                else if (typeid == "pjdy")
                {
                    ds = sql.GetDataSet("OWCBS_VIEW_XYBHFXB.XYBHFXB_PJDY_PROC", CommandType.StoredProcedure,
                new OracleParameter[] { param_bny, param_eny, param_cyc, param_cqc, param_out });
                }

            }
            catch (Exception ex)
            {

            }
            int rcount = ds.Tables[0].Rows.Count;//效益类别最多为5
            int ccount = ds.Tables[0].Columns.Count;

            int hcount = 4;
            if (typeid == "dj")
            {
                hcount = 5;
                if (FpSpread1.Sheets[0].Rows.Count == hcount)
                {
                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 1; j < ccount; j++)
                        {
                            Object obj = ds.Tables[0].Rows[i][j];
                            if (obj != null)
                            {
                                string value = obj.ToString();
                                if (string.IsNullOrEmpty(value))
                                    if (j == 0)
                                        value = "未设置";
                                    else
                                        value = "0";
                                FpSpread1.Sheets[0].Cells[i + hcount, j - 1].Value = value;
                            }
                        }
                    }
                }
            }
            else
            {
                if (FpSpread1.Sheets[0].Rows.Count == hcount)
                {
                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            Object obj = ds.Tables[0].Rows[i][j];
                            if (obj != null)
                            {
                                string value = obj.ToString();
                                if (string.IsNullOrEmpty(value))
                                    if (j == 0)
                                        value = "未设置";
                                    else
                                        value = "0";
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = value;
                            }
                        }
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
            int width = 0;
            for (int Col = 0; Col < FpSpread1.Sheets[0].Columns.Count; Col++)
            {
                width += FpSpread1.Sheets[0].Columns[Col].Width;
            }

            FpSpread1.Width = Unit.Pixel(width + 100);


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
            //JObject obj = new JObject();
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //string list = _getParam("YCLX");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            string list = _getParam("CYC");

            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["ygsConnectionString"].ConnectionString);

            string aa = Session["month"].ToString();
            //bny = aa.Substring(0, 4);
            int int_thisyear = int.Parse(aa.Substring(0, 4).ToString());
            int int_lastyear = int_thisyear - 1;
            string thisyear = int_thisyear.ToString();
            string lastyear = int_lastyear.ToString();
            string tableName = "ndstat_djsj";//取数据的表
            string nyColumnName = "ny";//时间字段的名称

            string bnyString = bny.Trim();
            string enyString = eny.Trim();
            /*    if (bnyString.Substring(4,2)=="13")//开始月份为13月则换表
                {
                    tableName = "jdstat_djsj_all";
                    nyColumnName = "bny";
                    thisyear = thisyear + "13";//如果从jdstat_djsj表取数据则包含月
                    lastyear = lastyear + "13";        
                }
        */
            string fpselect = " select xy1.qnxyjb,xy1.qnxymc,xy1.qnjs,xy1.js,xy1.cyl,xy2.js,xy2.cyl,xy3.js,xy3.cyl,xy4.js,xy4.cyl,xy5.js,xy5.cyl ";
            fpselect += " from (select biao.qnxyjb,biao.qnxymc,biao.qnjs,biao.js,biao.cyl from ";
            fpselect += " (Select f.qnxyjb,f.qnxymc, nvl(d2.qnjs,0) As qnjs,f.bnxyjb,f.bnxymc, nvl(d.js,0) as js, nvl(d.cyl,0) as cyl ";
            fpselect += " From   (Select g1.xyjb As qnxyjb, g1.xymc As qnxymc, g2.xyjb As bnxyjb, ";
            fpselect += " g2.xymc As bnxymc ";
            fpselect += " From   gsxylb_info g1, gsxylb_info g2 ";
            fpselect += " Where  g1.xyjb < 99 ";
            fpselect += " And    g2.xyjb < 99) f, ";
            fpselect += " (Select c.qnxyjb, c.bnxyjb, Count(dj_id) As js, ";
            fpselect += " round(Sum(bnhscyl) ,4) As cyl ";
            fpselect += " From   (Select l.dj_id As dj_id, l.gsxyjb As qnxyjb, ";
            fpselect += " t.gsxyjb As bnxyjb, t.hscyl As bnhscyl ";
            fpselect += " From   (Select dj_id, gsxyjb ";
            fpselect += " From   " + tableName + " ";
            fpselect += " Where  djisopen = 1  ";
            fpselect += " and	 " + nyColumnName + " = " + lastyear.ToString() + " ";
            fpselect += " And    pjdyxyjb <> 99 and " + tableName + ".cyc_id = '" + Session["cyc_id"].ToString() + "' ";

            fpselect += " ) l, ";
            fpselect += " (Select dj_id, gsxyjb, hscql as hscyl ";
            fpselect += " From   " + tableName + " ";
            fpselect += " Where  djisopen = 1 ";
            fpselect += " and	 " + nyColumnName + " = '" + thisyear.ToString() + "' ";
            fpselect += " And    pjdyxyjb <> 99 and " + tableName + ".cyc_id = '" + Session["cyc_id"].ToString() + "' ";

            fpselect += " ) t ";
            fpselect += " Where  l.dj_id = t.dj_id(+)) c ";
            fpselect += " Group  By c.qnxyjb, bnxyjb) d, ";
            fpselect += " (Select s.gsxyjb As qnxyjb, Count(dj_id) As qnjs ";
            fpselect += " From   " + tableName + " s ";
            fpselect += " Where  djisopen = 1  ";

            fpselect += " and " + nyColumnName + " = '" + lastyear.ToString() + "' ";
            fpselect += " And    pjdyxyjb <> 99 and s.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " Group  By s.gsxyjb) d2 ";
            fpselect += " Where  f.qnxyjb = d.qnxyjb(+) ";
            fpselect += " And    f.bnxyjb = d.bnxyjb(+) ";
            fpselect += " And    f.qnxyjb = d2.qnxyjb(+) ";

            fpselect += " ) biao ";
            fpselect += " where bnxyjb = '1') xy1, ";
            fpselect += " (select biao.qnxyjb,biao.qnxymc,biao.qnjs,biao.js,biao.cyl from ";
            fpselect += " (Select f.qnxyjb,f.qnxymc, nvl(d2.qnjs,0) As qnjs,f.bnxyjb,f.bnxymc, nvl(d.js,0) as js, nvl(d.cyl,0) as cyl ";
            fpselect += " From   (Select g1.xyjb As qnxyjb, g1.xymc As qnxymc, g2.xyjb As bnxyjb, ";
            fpselect += " g2.xymc As bnxymc ";
            fpselect += " From   gsxylb_info g1, gsxylb_info g2 ";
            fpselect += " Where  g1.xyjb < 99 ";
            fpselect += " And    g2.xyjb < 99) f, ";
            fpselect += " (Select c.qnxyjb, c.bnxyjb, Count(dj_id) As js, ";
            fpselect += " round(Sum(bnhscyl) ,4) As cyl ";
            fpselect += " From   (Select l.dj_id As dj_id, l.gsxyjb As qnxyjb, ";
            fpselect += " t.gsxyjb As bnxyjb, t.hscyl As bnhscyl ";
            fpselect += " From   (Select dj_id, gsxyjb ";
            fpselect += " From   " + tableName + " ";
            fpselect += " Where  djisopen = 1  ";
            fpselect += " and	 " + nyColumnName + " = " + lastyear.ToString() + " ";
            fpselect += " And    pjdyxyjb <> 99 and " + tableName + ".cyc_id = '" + Session["cyc_id"].ToString() + "' ";

            fpselect += " ) l, ";
            fpselect += " (Select dj_id, gsxyjb, hscql as hscyl ";
            fpselect += " From   " + tableName + " ";
            fpselect += " Where  djisopen = 1 ";
            fpselect += " and	 " + nyColumnName + " = '" + thisyear.ToString() + "' ";
            fpselect += " And    pjdyxyjb <> 99 and " + tableName + ".cyc_id = '" + Session["cyc_id"].ToString() + "' ";

            fpselect += " ) t ";
            fpselect += " Where  l.dj_id = t.dj_id(+)) c ";
            fpselect += " Group  By c.qnxyjb, bnxyjb) d, ";
            fpselect += " (Select s.gsxyjb As qnxyjb, Count(dj_id) As qnjs ";
            fpselect += " From   " + tableName + " s ";
            fpselect += " Where  djisopen = 1  ";

            fpselect += " and " + nyColumnName + " = '" + lastyear.ToString() + "' ";
            fpselect += " And    pjdyxyjb <> 99 and s.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " Group  By s.gsxyjb) d2 ";
            fpselect += " Where  f.qnxyjb = d.qnxyjb(+) ";
            fpselect += " And    f.bnxyjb = d.bnxyjb(+) ";
            fpselect += " And    f.qnxyjb = d2.qnxyjb(+) ";

            fpselect += " ) biao ";
            fpselect += " where bnxyjb = '2') xy2, ";
            fpselect += " (select biao.qnxyjb,biao.qnxymc,biao.qnjs,biao.js,biao.cyl from   ";
            fpselect += " (Select f.qnxyjb,f.qnxymc, nvl(d2.qnjs,0) As qnjs,f.bnxyjb,f.bnxymc, nvl(d.js,0) as js, nvl(d.cyl,0) as cyl ";
            fpselect += " From   (Select g1.xyjb As qnxyjb, g1.xymc As qnxymc, g2.xyjb As bnxyjb, ";
            fpselect += " g2.xymc As bnxymc ";
            fpselect += " From   gsxylb_info g1, gsxylb_info g2 ";
            fpselect += " Where  g1.xyjb < 99 ";
            fpselect += " And    g2.xyjb < 99) f, ";
            fpselect += " (Select c.qnxyjb, c.bnxyjb, Count(dj_id) As js, ";
            fpselect += " round(Sum(bnhscyl) ,4) As cyl ";
            fpselect += " From   (Select l.dj_id As dj_id, l.gsxyjb As qnxyjb, ";
            fpselect += " t.gsxyjb As bnxyjb, t.hscyl As bnhscyl ";
            fpselect += " From   (Select dj_id, gsxyjb ";
            fpselect += " From   " + tableName + " ";
            fpselect += " Where  djisopen = 1  ";
            fpselect += " and	 " + nyColumnName + " = " + lastyear.ToString() + " ";
            fpselect += " And    pjdyxyjb <> 99 and " + tableName + ".cyc_id = '" + Session["cyc_id"].ToString() + "' ";

            fpselect += " ) l, ";
            fpselect += " (Select dj_id, gsxyjb, hscql as hscyl ";
            fpselect += " From   " + tableName + " ";
            fpselect += " Where  djisopen = 1 ";
            fpselect += " and	 " + nyColumnName + " = '" + thisyear.ToString() + "' ";
            fpselect += " And    pjdyxyjb <> 99 ";

            fpselect += " ) t ";
            fpselect += " Where  l.dj_id = t.dj_id(+)) c ";
            fpselect += " Group  By c.qnxyjb, bnxyjb) d, ";
            fpselect += " (Select s.gsxyjb As qnxyjb, Count(dj_id) As qnjs ";
            fpselect += " From   " + tableName + " s ";
            fpselect += " Where  djisopen = 1  ";

            fpselect += " and " + nyColumnName + " = '" + lastyear.ToString() + "' ";
            fpselect += " And    pjdyxyjb <> 99 and s.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " Group  By s.gsxyjb) d2 ";
            fpselect += " Where  f.qnxyjb = d.qnxyjb(+) ";
            fpselect += " And    f.bnxyjb = d.bnxyjb(+) ";
            fpselect += " And    f.qnxyjb = d2.qnxyjb(+) ";
            fpselect += " ) biao ";
            fpselect += " where bnxyjb = '3') xy3, ";
            fpselect += " (select biao.qnxyjb,biao.qnxymc,biao.qnjs,biao.js,biao.cyl from  ";
            fpselect += " (Select f.qnxyjb,f.qnxymc, nvl(d2.qnjs,0) As qnjs,f.bnxyjb,f.bnxymc, nvl(d.js,0) as js, nvl(d.cyl,0) as cyl ";
            fpselect += " From   (Select g1.xyjb As qnxyjb, g1.xymc As qnxymc, g2.xyjb As bnxyjb, ";
            fpselect += " g2.xymc As bnxymc ";
            fpselect += " From   gsxylb_info g1, gsxylb_info g2 ";
            fpselect += " Where  g1.xyjb < 99 ";
            fpselect += " And    g2.xyjb < 99) f, ";
            fpselect += " (Select c.qnxyjb, c.bnxyjb, Count(dj_id) As js, ";
            fpselect += " round(Sum(bnhscyl) ,4) As cyl ";
            fpselect += " From   (Select l.dj_id As dj_id, l.gsxyjb As qnxyjb, ";
            fpselect += " t.gsxyjb As bnxyjb, t.hscyl As bnhscyl ";
            fpselect += " From   (Select dj_id, gsxyjb ";
            fpselect += " From   " + tableName + " ";
            fpselect += " Where  djisopen = 1  ";
            fpselect += " and	 " + nyColumnName + " = " + lastyear.ToString() + " ";
            fpselect += " And    pjdyxyjb <> 99 and " + tableName + ".cyc_id = '" + Session["cyc_id"].ToString() + "' ";

            fpselect += " ) l, ";
            fpselect += " (Select dj_id, gsxyjb, hscql as hscyl ";
            fpselect += " From   " + tableName + " ";
            fpselect += " Where  djisopen = 1 ";
            fpselect += " and	 " + nyColumnName + " = '" + thisyear.ToString() + "' ";
            fpselect += " And    pjdyxyjb <> 99 and " + tableName + ".cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " ) t ";
            fpselect += " Where  l.dj_id = t.dj_id(+)) c ";
            fpselect += " Group  By c.qnxyjb, bnxyjb) d, ";
            fpselect += " (Select s.gsxyjb As qnxyjb, Count(dj_id) As qnjs ";
            fpselect += " From   " + tableName + " s ";
            fpselect += " Where  djisopen = 1 and s.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " and " + nyColumnName + " = '" + lastyear.ToString() + "' ";
            fpselect += " And    pjdyxyjb <> 99 ";
            fpselect += " Group  By s.gsxyjb) d2 ";
            fpselect += " Where  f.qnxyjb = d.qnxyjb(+) ";
            fpselect += " And    f.bnxyjb = d.bnxyjb(+) ";
            fpselect += " And    f.qnxyjb = d2.qnxyjb(+) ";


            fpselect += " ) biao ";
            fpselect += " where bnxyjb = '4') xy4, ";
            fpselect += " (select biao.qnxyjb,biao.qnxymc,biao.qnjs,biao.js,biao.cyl from ";
            fpselect += " (Select f.qnxyjb,f.qnxymc, nvl(d2.qnjs,0) As qnjs,f.bnxyjb,f.bnxymc, nvl(d.js,0) as js, nvl(d.cyl,0) as cyl ";
            fpselect += " From   (Select g1.xyjb As qnxyjb, g1.xymc As qnxymc, g2.xyjb As bnxyjb, ";
            fpselect += " g2.xymc As bnxymc ";
            fpselect += " From   gsxylb_info g1, gsxylb_info g2 ";
            fpselect += " Where  g1.xyjb < 99 ";
            fpselect += " And    g2.xyjb < 99) f, ";
            fpselect += " (Select c.qnxyjb, c.bnxyjb, Count(dj_id) As js, ";
            fpselect += " round(Sum(bnhscyl) ,4) As cyl ";
            fpselect += " From   (Select l.dj_id As dj_id, l.gsxyjb As qnxyjb, ";
            fpselect += " t.gsxyjb As bnxyjb, t.hscyl As bnhscyl ";
            fpselect += " From   (Select dj_id, gsxyjb ";
            fpselect += " From   " + tableName + " ";
            fpselect += " Where  djisopen = 1  ";
            fpselect += " and	 " + nyColumnName + " = " + lastyear.ToString() + " ";
            fpselect += " And    pjdyxyjb <> 99 and " + tableName + ".cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " ) l, ";
            fpselect += " (Select dj_id, gsxyjb, hscql as hscyl ";
            fpselect += " From   " + tableName + " ";
            fpselect += " Where  djisopen = 1 ";
            fpselect += " and	 " + nyColumnName + " = '" + thisyear.ToString() + "' ";
            fpselect += " And    pjdyxyjb <> 99 ";
            fpselect += " ) t ";
            fpselect += " Where  l.dj_id = t.dj_id(+)) c ";
            fpselect += " Group  By c.qnxyjb, bnxyjb) d, ";
            fpselect += " (Select s.gsxyjb As qnxyjb, Count(dj_id) As qnjs ";
            fpselect += " From   " + tableName + " s ";
            fpselect += " Where  djisopen = 1 and s.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            fpselect += " and " + nyColumnName + " = '" + lastyear.ToString() + "' ";
            fpselect += " And    pjdyxyjb <> 99 ";
            fpselect += " Group  By s.gsxyjb) d2 ";
            fpselect += " Where  f.qnxyjb = d.qnxyjb(+) ";
            fpselect += " And    f.bnxyjb = d.bnxyjb(+) ";
            fpselect += " And    f.qnxyjb = d2.qnxyjb(+) ";


            fpselect += " ) biao ";
            fpselect += " where bnxyjb = '5') xy5 ";

            fpselect += " where xy2.qnxymc = xy1.qnxymc and xy3.qnxymc = xy1.qnxymc and xy4.qnxymc = xy1.qnxymc and xy5.qnxymc = xy1.qnxymc ";
            fpselect += " and xy2.qnxyjb = xy1.qnxyjb and xy3.qnxyjb = xy1.qnxyjb and xy4.qnxyjb = xy1.qnxyjb and xy5.qnxyjb = xy1.qnxyjb ";
            fpselect += " order by qnxyjb ";

            try
            {
                connfp.Open();

                OracleCommand myComm = new OracleCommand(fpselect, connfp);
                OracleDataReader myReader = myComm.ExecuteReader();
                //DataTable Fptable用来输出数据
                DataTable Fptable = new DataTable("fpdata");
                Fptable.Columns.Add("qnxymc", typeof(string));
                Fptable.Columns.Add("qnjs", typeof(int));
                Fptable.Columns.Add("js1", typeof(float));
                Fptable.Columns.Add("cyl1", typeof(float));
                Fptable.Columns.Add("js2", typeof(float));
                Fptable.Columns.Add("cyl2", typeof(float));
                Fptable.Columns.Add("js3", typeof(string));
                Fptable.Columns.Add("cyl3", typeof(float));
                Fptable.Columns.Add("js4", typeof(float));
                Fptable.Columns.Add("cyl4", typeof(float));
                Fptable.Columns.Add("js5", typeof(float));
                Fptable.Columns.Add("cyl5", typeof(float));
                Fptable.Columns.Add("tcj", typeof(float));

                DataRow Fprow;
                while (myReader.Read())
                {
                    Fprow = Fptable.NewRow();

                    Fprow[0] = myReader[1];
                    Fprow[1] = myReader.GetValue(2);
                    Fprow[2] = myReader.GetValue(3);
                    Fprow[3] = myReader.GetValue(4);
                    Fprow[4] = myReader.GetValue(5);
                    Fprow[5] = myReader.GetValue(6);
                    Fprow[6] = myReader.GetValue(7);
                    Fprow[7] = myReader.GetValue(8);
                    Fprow[8] = myReader.GetValue(9);
                    Fprow[9] = myReader.GetValue(10);
                    Fprow[10] = myReader.GetValue(11);
                    Fprow[11] = myReader.GetValue(12);
                    //Fprow[12] = myReader.GetValue(13);

                    Fptable.Rows.Add(Fprow);
                }
                myComm.Clone();

                DataSet fpset = new DataSet();
                fpset.Tables.Add(Fptable);


                //此处用于绑定数据            
                #region
                int rcount = fpset.Tables["fpdata"].Rows.Count;
                int ccount = fpset.Tables["fpdata"].Columns.Count;
                int hcount = 5;
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
                            if ((j == 3 || j == 5 || j == 7 || j == 9 || j == 11) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
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
                }
                else//不为空
                {
                    string path = "../../../static/excel/jdnianduQC.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表32-油井效益分析表");

                    this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    this.FpSpread1.Sheets[0].RowHeader.Visible = false;

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            if ((j == 3 || j == 5 || j == 7 || j == 9 || j == 11) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
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

            FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao32.xls");


        }


    }
}

