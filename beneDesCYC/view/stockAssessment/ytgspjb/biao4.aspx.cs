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

namespace beneDesCYC.view.stockAssessment.ytgspjb
{
    public partial class biao4 : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            initSpread();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }

        protected void initSpread()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/gufenbiao.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表4-井块结合效益汇总表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

            //this.FpSpread1.Sheets[0].Cells[3, 1].Text = _getParam("targetType");
            //this.FpSpread1.Sheets[0].Cells[4, 1].Text = _getParam("startMonth") + " -- " + _getParam("endMonth");

            if (isempty() == 0)
            { Response.Write("<script>alert('无数据')</script>"); }
            else
            { sj(); }
        }

        protected void sj()
        {
            string cycid = Session["cyc_id"].ToString();
            //JObject obj = new JObject();
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //string list = _getParam("YCLX");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            string list = _getParam("CYC");
            string typeid = _getParam("targetType");

            //string cycid = Session["cyc"].ToString();
            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);
            string fpselect = "";
            if (typeid == "qk")
            {
                fpselect += " select  tem0.qk as qk, tem0.xymc as xymc, ";
                fpselect += " nvl(tem0.js,0) as js, nvl(round(tem0.hscyl/10000,4),0) as hscyl, nvl(round(tem0.czcb/10000,4),0) as czcb, ";
                fpselect += " nvl(tem1.js,0) as js1,0 as bl101, nvl(round(tem1.hscyl/10000,4),0) as hscyl1,0 as bl102, nvl(round(tem1.czcb/10000,4),0) as czcb1, 0 as bl103,";
                fpselect += " nvl(tem2.js,0)  as js2,0 as bl201, nvl(round(tem2.hscyl/10000,4),0)  as hscyl2,0 as bl202, nvl(round(tem2.czcb/10000,4),0)  as czcb2,0 as bl203, ";
                fpselect += " nvl(tem3.js,0)  as js3,0 as bl301, nvl(round(tem3.hscyl/10000,4),0)  as hscyl3,0 as bl302, nvl(round(tem3.czcb/10000,4),0)  as czcb3,0 as bl303, ";
                fpselect += " nvl(tem4.js,0)  as js4,0 as bl401, nvl(round(tem4.hscyl/10000,4),0)  as hscyl4,0 as bl402, nvl(round(tem4.czcb/10000,4),0)  as czcb4 ,0 as bl403, ";
                fpselect += " nvl(tem5.js,0)  as js5,0 as bl501, nvl(round(tem5.hscyl/10000,4),0)  as hscyl5,0 as bl502, nvl(round(tem5.czcb/10000,4),0)  as czcb5 ,0 as bl503";
                fpselect += " from ( ";
                fpselect += " select sdy.mc as qk, xy.xymc as xymc, nvl(sdy.yjkjs,0) as js, ";
                fpselect += " nvl(sum(sdy.cyoul),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb from jdstat_qksj sdy, qkxyjb xy ";
                fpselect += " where sdy.sfpj = '1' and sdy.gsxyjb = xy.xyjb and sdy.cyc_id = '" + cycid + "'	group by sdy.mc,xy.xymc,sdy.yjkjs ";
                fpselect += " ) tem0, ";
                fpselect += " (select  sdy.qk as qk, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from jdstat_djsj sdy where sdy.gsxyjb = '1'  and sdy.cyc_id = '" + cycid + "' group by sdy.qk  ";
                fpselect += " ) tem1, ";
                fpselect += " (select  sdy.qk as qk, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from jdstat_djsj sdy where sdy.gsxyjb = '2' and sdy.cyc_id = '" + cycid + "'  group by sdy.qk  ";
                fpselect += " ) tem2,  ";
                fpselect += " (select  sdy.qk as qk, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from jdstat_djsj sdy where sdy.gsxyjb = '3' and sdy.cyc_id = '" + cycid + "'  group by sdy.qk ";
                fpselect += " ) tem3, ";
                fpselect += " (select  sdy.qk as qk, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from jdstat_djsj sdy where sdy.gsxyjb = '4' and sdy.cyc_id = '" + cycid + "'  group by sdy.qk ";
                fpselect += " ) tem4, ";
                fpselect += " (select  sdy.qk as qk, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from jdstat_djsj sdy where sdy.gsxyjb = '5'  and sdy.cyc_id = '" + cycid + "' group by sdy.qk  ";
                fpselect += " ) tem5 ";
                fpselect += " where	tem0.qk = tem1.qk(+) and tem0.qk = tem2.qk(+) and tem0.qk = tem3.qk(+) and tem0.qk = tem4.qk(+) and tem0.qk = tem5.qk(+)  ";
            }
            else if (typeid == "pjdy")
            {
                fpselect += " select  tem0.pjdy as pjdy, tem0.xymc as xymc, ";
                fpselect += " nvl(tem0.js,0) as js, nvl(round(tem0.hscyl/10000,4),0) as hscyl, nvl(round(tem0.czcb/10000,4),0) as czcb, ";
                fpselect += " nvl(tem1.js,0) as js1,0 as bl101, nvl(round(tem1.hscyl/10000,4),0) as hscyl1,0 as bl102, nvl(round(tem1.czcb/10000,4),0) as czcb1, 0 as bl103,";
                fpselect += " nvl(tem2.js,0)  as js2,0 as bl201, nvl(round(tem2.hscyl/10000,4),0)  as hscyl2,0 as bl202, nvl(round(tem2.czcb/10000,4),0)  as czcb2,0 as bl203, ";
                fpselect += " nvl(tem3.js,0)  as js3,0 as bl301, nvl(round(tem3.hscyl/10000,4),0)  as hscyl3,0 as bl302, nvl(round(tem3.czcb/10000,4),0)  as czcb3,0 as bl303, ";
                fpselect += " nvl(tem4.js,0)  as js4,0 as bl401, nvl(round(tem4.hscyl/10000,4),0)  as hscyl4,0 as bl402, nvl(round(tem4.czcb/10000,4),0)  as czcb4 ,0 as bl403, ";
                fpselect += " nvl(tem5.js,0)  as js5,0 as bl501, nvl(round(tem5.hscyl/10000,4),0)  as hscyl5,0 as bl502, nvl(round(tem5.czcb/10000,4),0)  as czcb5 ,0 as bl503";
                fpselect += " from ( ";
                fpselect += " select sdy.mc as pjdy, xy.xymc as xymc, nvl(sdy.yjkjs,0) as js, ";
                fpselect += " nvl(sum(sdy.cyoul),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb from jdstat_pjdysj sdy, qkxyjb xy ";
                fpselect += " where sdy.sfpj = '1' and sdy.gsxyjb = xy.xyjb and sdy.cyc_id = '" + cycid + "'	group by sdy.mc,xy.xymc,sdy.yjkjs ";
                fpselect += " ) tem0, ";
                fpselect += " (select  sdy.pjdy as pjdy, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from jdstat_djsj sdy where sdy.gsxyjb = '1'  and sdy.cyc_id = '" + cycid + "' group by sdy.pjdy  ";
                fpselect += " ) tem1, ";
                fpselect += " (select  sdy.pjdy as pjdy, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from jdstat_djsj sdy where sdy.gsxyjb = '2' and sdy.cyc_id = '" + cycid + "'  group by sdy.pjdy  ";
                fpselect += " ) tem2,  ";
                fpselect += " (select  sdy.pjdy as pjdy, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from jdstat_djsj sdy where sdy.gsxyjb = '3' and sdy.cyc_id = '" + cycid + "'  group by sdy.pjdy ";
                fpselect += " ) tem3, ";
                fpselect += " (select  sdy.pjdy as pjdy, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from jdstat_djsj sdy where sdy.gsxyjb = '4' and sdy.cyc_id = '" + cycid + "'  group by sdy.pjdy ";
                fpselect += " ) tem4, ";
                fpselect += " (select  sdy.pjdy as pjdy, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from jdstat_djsj sdy where sdy.gsxyjb = '5' and sdy.cyc_id = '" + cycid + "'  group by sdy.pjdy ";
                fpselect += " ) tem5 ";
                fpselect += " where	tem0.pjdy = tem1.pjdy(+) and tem0.pjdy = tem2.pjdy(+) and tem0.pjdy = tem3.pjdy(+) and tem0.pjdy = tem4.pjdy(+) and tem0.pjdy = tem5.pjdy(+)  ";

            }
            //////////此处有问题，少一口井
            else if (typeid == "zyq")
            {
                fpselect += " select  d.dep_name as pjdy, ";
                fpselect += " sum(nvl(tem0.js,0)) as js, round(sum(nvl(tem0.hscyl,0))/10000,4) as hscyl, sum(nvl(round(tem0.czcb/10000,4),0)) as czcb, ";
                fpselect += " sum(nvl(tem1.js,0)) as js1,0 as bl101, round(sum(nvl(tem1.hscyl,0))/10000,4) as hscyl1,0 as bl102, sum(nvl(round(tem1.czcb/10000,4),0)) as czcb1, 0 as bl103,";
                fpselect += " sum(nvl(tem2.js,0))  as js2,0 as bl201, round(sum(nvl(tem2.hscyl,0))/10000,4)  as hscyl2,0 as bl202, sum(nvl(round(tem2.czcb/10000,4),0))  as czcb2,0 as bl203, ";
                fpselect += " sum(nvl(tem3.js,0))  as js3,0 as bl301, round(sum(nvl(tem3.hscyl,0))/10000,4)  as hscyl3,0 as bl302, sum(nvl(round(tem3.czcb/10000,4),0))  as czcb3,0 as bl303, ";
                fpselect += " sum(nvl(tem4.js,0))  as js4,0 as bl401, round(sum(nvl(tem4.hscyl,0))/10000,4)  as hscyl4,0 as bl402, sum(nvl(round(tem4.czcb/10000,4),0))  as czcb4 ,0 as bl403, ";
                fpselect += " sum(nvl(tem5.js,0))  as js5,0 as bl501, round(sum(nvl(tem5.hscyl,0))/10000,4)  as hscyl5,0 as bl502, sum(nvl(round(tem5.czcb/10000,4),0))  as czcb5 ,0 as bl503, ";
                fpselect += " tem0.zyqdm as zyqdm ";
                fpselect += " from department d, ( ";
                fpselect += " select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as js,sdy.eny as eny, sdy.bny as bny,sdy.cyc_id as cyc_id,  ";
                fpselect += " nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb,sdy.zyqdm as zyqdm from view_jdstat_zyqsj sdy ";
                fpselect += " where sdy.cyc_id = '" + cycid + "'	 ";
                fpselect += " ) tem0, ";
                fpselect += " (select  sdy.dj_id as dj_id, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from view_jdstat_zyqsj sdy where sdy.gsxyjb = '1'  and sdy.cyc_id = '" + cycid + "' group by sdy.dj_id  ";
                fpselect += " ) tem1, ";
                fpselect += " (select  sdy.dj_id as dj_id, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from view_jdstat_zyqsj sdy where sdy.gsxyjb = '2' and sdy.cyc_id = '" + cycid + "'  group by sdy.dj_id  ";
                fpselect += " ) tem2,  ";
                fpselect += " (select  sdy.dj_id as dj_id, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from view_jdstat_zyqsj sdy where sdy.gsxyjb = '3' and sdy.cyc_id = '" + cycid + "'  group by sdy.dj_id ";
                fpselect += " ) tem3, ";
                fpselect += " (select  sdy.dj_id as dj_id, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from view_jdstat_zyqsj sdy where sdy.gsxyjb = '4' and sdy.cyc_id = '" + cycid + "'  group by sdy.dj_id ";
                fpselect += " ) tem4, ";
                fpselect += " (select  sdy.dj_id as dj_id, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from view_jdstat_zyqsj sdy where sdy.gsxyjb = '5' and sdy.cyc_id = '" + cycid + "'  group by sdy.dj_id ";
                fpselect += " ) tem5 ";
                fpselect += " where	tem0.dj_id = tem1.dj_id(+) and tem0.dj_id = tem2.dj_id(+) and tem0.dj_id = tem3.dj_id(+) and tem0.dj_id = tem4.dj_id(+) and tem0.dj_id = tem5.dj_id(+)  ";
                fpselect += " and d.dep_id = tem0.zyqdm  ";
                fpselect += " and d.parent_id = '" + cycid + "' ";
                fpselect += " group by d.dep_name,tem0.zyqdm  ";
                fpselect += " order by tem0.zyqdm ";
            }

            try
            {
                connfp.Open();

                OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                //DataSet fpset = new DataSet();
                //da.Fill(fpset, "fpdata");
                //计算合计, 合计在绑定数据时计算           
                #region
                DataTable dt = new DataTable("fpdata"); //为数据表起一个名字,将来插入数据集中调用
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    if (typeid == "zyq")
                    {
                        //初始化各列
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            dr[i] = 0;
                        }
                        dr[0] = "";
                        dr[0] = "合计";
                        //计算
                        for (int k = 0; k < dt.Rows.Count; k++)//循环计算各行
                        {

                            for (int n = 2; n < dt.Columns.Count - 1; n++)//循环计算各列
                            {
                                dr[n] = float.Parse(dr[n].ToString()) + float.Parse(dt.Rows[k][n].ToString());
                            }
                        }
                        //计算精度
                        for (int m = 2; m < dt.Columns.Count - 1; m++)
                        {
                            dr[m] = Math.Round(float.Parse(dr[m].ToString()), 4);
                        }
                        //把行加入表
                        dt.Rows.Add(dr);
                    }
                    else
                    {

                        //初始化各列
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            dr[i] = 0;
                        }
                        dr[0] = "";
                        dr[1] = "合计";
                        //计算
                        for (int k = 0; k < dt.Rows.Count; k++)//循环计算各行
                        {

                            for (int n = 2; n < dt.Columns.Count; n++)//循环计算各列
                            {
                                dr[n] = float.Parse(dr[n].ToString()) + float.Parse(dt.Rows[k][n].ToString());
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
                DataSet fpset = new DataSet();
                fpset.Tables.Add(dt);
                #endregion

                //此处用于绑定数据            
                #region
                int rcount = fpset.Tables["fpdata"].Rows.Count;
                int ccount = fpset.Tables["fpdata"].Columns.Count;
                int hcount = 5;
                //FpSpread1.ColumnHeader.Visible = false;
                //FpSpread1.RowHeader.Visible = false;

                if (typeid == "zyq")
                {
                    string path = Page.MapPath("../../../static/excel/zuoyequ1.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "表4-井块结合效益汇总表");

                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount - 1; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            if ((j == 2 || j == 3 || j == 6 || j == 8 || j == 12 || j == 14 || j == 18 || j == 20 || j == 24 || j == 26 || j == 30 || j == 32) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString() != "0")
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                            else
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }

                    }

                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 4; j < ccount - 1; j += 2)
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
                    /////////////////////
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
                else
                {
                    string path = "../../../static/excel/gufenbiao.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表4-井块结合效益汇总表");

                    this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    this.FpSpread1.Sheets[0].RowHeader.Visible = false;

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            if ((j == 3 || j == 4 || j == 7 || j == 9 || j == 13 || j == 15 || j == 19 || j == 21 || j == 25 || j == 27 || j == 31 || j == 33) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString() != "0")
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                            else
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();

                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }

                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 5; j < ccount; j += 2)
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
                    /////////////////////
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
                /////////////////////////09.4.29更新//////////////////////////////////////
                if (typeid == "yt")
                {
                    FpSpread1.Sheets[0].Cells[2, 0].Value = "单井";
                }
                else if (typeid == "pjdy")
                {
                    FpSpread1.Sheets[0].Cells[2, 0].Value = "评价单元";
                }
                else if (typeid == "qk")
                {
                    FpSpread1.Sheets[0].Cells[2, 0].Value = "区块";
                }
                else
                {
                    FpSpread1.Sheets[0].Cells[2, 0].Value = "作业区";
                }
                /////////////////////////09.4.30更新//////////////////////////////////////
                if (typeid == "yt")
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text + "(油井)";
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
                    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text + "(作业区)";
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
            FpSpread1.Sheets[0].RowHeader.Visible = true;
            FpSpread1.Sheets[0].ColumnHeader.Visible = true;
            //FpSpread1.SaveExcelToResponse("biao4.xls");
            FarpointGridChange.FarPointChange(FpSpread1, "biao4.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;
        }

    }
}
