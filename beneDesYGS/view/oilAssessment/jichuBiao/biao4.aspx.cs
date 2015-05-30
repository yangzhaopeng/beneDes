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
    public partial class biao4 : beneDesYGS.core.UI.corePage
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
            string path = "../../../static/excel/zuoyequ.xls";
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

            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //string list = _getParam("YCLX");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            string list = _getParam("CYC");
            string typeid = _getParam("targetType");

            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["ygsConnectionString"].ConnectionString);
            string fpselect = "";
            if (typeid == "yt")
            {
                fpselect += " select  '' as dep_id,d.dep_name as pjdy, ";
                fpselect += " sum(nvl(tem0.js,0)) as js, round(sum(nvl(tem0.hscyl,0))/10000,4) as hscyl, sum(nvl(round(tem0.czcb/10000,4),0)) as czcb, ";
                fpselect += " sum(nvl(tem1.js,0)) as js1,0 as bl101, round(sum(nvl(tem1.hscyl,0))/10000,4) as hscyl1,0 as bl102, sum(nvl(round(tem1.czcb/10000,4),0)) as czcb1, 0 as bl103,";
                fpselect += " sum(nvl(tem2.js,0))  as js2,0 as bl201, round(sum(nvl(tem2.hscyl,0))/10000,4)  as hscyl2,0 as bl202, sum(nvl(round(tem2.czcb/10000,4),0))  as czcb2,0 as bl203, ";
                fpselect += " sum(nvl(tem3.js,0))  as js3,0 as bl301, round(sum(nvl(tem3.hscyl,0))/10000,4)  as hscyl3,0 as bl302, sum(nvl(round(tem3.czcb/10000,4),0))  as czcb3,0 as bl303, ";
                fpselect += " sum(nvl(tem4.js,0))  as js4,0 as bl401, round(sum(nvl(tem4.hscyl,0))/10000,4)  as hscyl4,0 as bl402, sum(nvl(round(tem4.czcb/10000,4),0))  as czcb4 ,0 as bl403 ";
                //fpselect += " tem0.zyqdm as zyq ";

                fpselect += " from ( ";
                //tem0
                fpselect += " select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as js,sdy.eny as eny, sdy.bny as bny,sdy.dep_id as dep_id,  ";
                fpselect += " nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb  from view_dtstat_zyqsj sdy ";
                fpselect += " where sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                //fpselect += "  ";
                fpselect += " ) tem0, ";


                //tem1
                fpselect += " (select  sdy.dj_id as dj_id, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from view_dtstat_zyqsj sdy where sdy.gsxyjb_1 = '1' and sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " group by sdy.dj_id  ";
                fpselect += " ) tem1, ";
                //tem2
                fpselect += " (select  sdy.dj_id as dj_id, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from view_dtstat_zyqsj sdy where sdy.gsxyjb_1 = '2' and sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " group by sdy.dj_id ";
                fpselect += " ) tem2,  ";
                //tem3
                fpselect += " (select  sdy.dj_id as dj_id, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from view_dtstat_zyqsj sdy where sdy.gsxyjb_1 = '3'  and sdy.djisopen = '1'  ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  group by sdy.dj_id ";
                fpselect += " ) tem3, ";
                //tem4
                fpselect += " (select  sdy.dj_id as dj_id, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from view_dtstat_zyqsj sdy where sdy.gsxyjb_1 = '4' and sdy.djisopen = '1'  ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  group by sdy.dj_id ";
                fpselect += " ) tem4,department d ";
                fpselect += " where	tem0.dep_id=d.dep_id and tem0.dj_id = tem1.dj_id(+) and tem0.dj_id = tem2.dj_id(+) and tem0.dj_id = tem3.dj_id(+) and tem0.dj_id = tem4.dj_id(+)  ";
                fpselect += "group by d.dep_name ";
                fpselect += " order by d.dep_name ";
            }

            else if (typeid == "qk")
            {
                fpselect += " select  tem0.qk as qk, tem0.jbmc as jbmc, ";
                fpselect += " nvl(tem0.js,0) as js, nvl(round(tem0.hscyl,4),0) as hscyl, nvl(round(tem0.czcb/10000,4),0) as czcb, ";
                fpselect += " nvl(tem1.js,0) as js1,0 as bl101, nvl(round(tem1.hscyl/10000,4),0) as hscyl1,0 as bl102, nvl(round(tem1.czcb/10000,4),0) as czcb1, 0 as bl103,";
                fpselect += " nvl(tem2.js,0)  as js2,0 as bl201, nvl(round(tem2.hscyl/10000,4),0)  as hscyl2,0 as bl202, nvl(round(tem2.czcb/10000,4),0)  as czcb2,0 as bl203, ";
                fpselect += " nvl(tem3.js,0)  as js3,0 as bl301, nvl(round(tem3.hscyl/10000,4),0)  as hscyl3,0 as bl302, nvl(round(tem3.czcb/10000,4),0)  as czcb3,0 as bl303, ";
                fpselect += " nvl(tem4.js,0)  as js4,0 as bl401, nvl(round(tem4.hscyl/10000,4),0)  as hscyl4,0 as bl402, nvl(round(tem4.czcb/10000,4),0)  as czcb4 ,0 as bl4o3";
                fpselect += " from ( ";
                fpselect += " select sdy.mc as qk, xy.jbmc as jbmc, nvl(sdy.yjkjs,0) as js, ";
                fpselect += " nvl(sum(sdy.cyoul),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb from dtstat_qksj sdy, dtxyjb_info xy ";
                fpselect += " where sdy.sfpj = '1' and sdy.gsxyjb_1 = xy.jbid ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " group by sdy.mc,xy.jbmc,sdy.yjkjs ";
                fpselect += " ) tem0, ";
                fpselect += " (select  sdy.qk as qk, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from dtstat_djsj sdy where sdy.gsxyjb_1 = '1'  ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " group by sdy.qk  ";
                fpselect += " ) tem1, ";
                fpselect += " (select  sdy.qk as qk, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from dtstat_djsj sdy where sdy.gsxyjb_1 = '2' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " group by sdy.qk  ";
                fpselect += " ) tem2,  ";
                fpselect += " (select  sdy.qk as qk, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from dtstat_djsj sdy where sdy.gsxyjb_1 = '3' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  group by sdy.qk ";
                fpselect += " ) tem3, ";
                fpselect += " (select  sdy.qk as qk, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from dtstat_djsj sdy where sdy.gsxyjb_1 = '4' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  group by sdy.qk ";
                fpselect += " ) tem4 ";
                fpselect += " where	tem0.qk = tem1.qk(+) and tem0.qk = tem2.qk(+) and tem0.qk = tem3.qk(+) and tem0.qk = tem4.qk(+)  ";
            }
            else if (typeid == "pjdy")
            {
                fpselect += " select  tem0.pjdy as pjdy, tem0.jbmc as jbmc, ";
                fpselect += " nvl(tem0.js,0) as js, nvl(round(tem0.hscyl,4),0) as hscyl, nvl(round(tem0.czcb/10000,4),0) as czcb, ";
                fpselect += " nvl(tem1.js,0) as js1,0 as bl101, nvl(round(tem1.hscyl/10000,4),0) as hscyl1,0 as bl102, nvl(round(tem1.czcb/10000,4),0) as czcb1, 0 as bl103,";
                fpselect += " nvl(tem2.js,0)  as js2,0 as bl201, nvl(round(tem2.hscyl/10000,4),0)  as hscyl2,0 as bl202, nvl(round(tem2.czcb/10000,4),0)  as czcb2,0 as bl203, ";
                fpselect += " nvl(tem3.js,0)  as js3,0 as bl301, nvl(round(tem3.hscyl/10000,4),0)  as hscyl3,0 as bl302, nvl(round(tem3.czcb/10000,4),0)  as czcb3,0 as bl303, ";
                fpselect += " nvl(tem4.js,0)  as js4,0 as bl401, nvl(round(tem4.hscyl/10000,4),0)  as hscyl4,0 as bl402, nvl(round(tem4.czcb/10000,4),0)  as czcb4 ,0 as bl4o3";
                fpselect += " from ( ";
                fpselect += " select sdy.mc as pjdy, xy.jbmc as jbmc, nvl(sdy.yjkjs,0) as js, ";
                fpselect += " nvl(sum(sdy.cyoul),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb from dtstat_pjdysj sdy, dtxyjb_info xy ";
                fpselect += " where sdy.sfpj = '1' and sdy.gsxyjb_1 = xy.jbid ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  group by sdy.mc,xy.jbmc,sdy.yjkjs ";
                fpselect += " ) tem0, ";
                fpselect += " (select  sdy.pjdy as pjdy, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from dtstat_djsj sdy where sdy.gsxyjb_1 = '1'  ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  group by sdy.pjdy  ";
                fpselect += " ) tem1, ";
                fpselect += " (select  sdy.pjdy as pjdy, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from dtstat_djsj sdy where sdy.gsxyjb_1 = '2' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  group by sdy.pjdy  ";
                fpselect += " ) tem2,  ";
                fpselect += " (select  sdy.pjdy as pjdy, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from dtstat_djsj sdy where sdy.gsxyjb_1 = '3' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  group by sdy.pjdy ";
                fpselect += " ) tem3, ";
                fpselect += " (select  sdy.pjdy as pjdy, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from dtstat_djsj sdy where sdy.gsxyjb_1 = '4' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  group by sdy.pjdy ";
                fpselect += " ) tem4 ";
                fpselect += " where	tem0.pjdy = tem1.pjdy(+) and tem0.pjdy = tem2.pjdy(+) and tem0.pjdy = tem3.pjdy(+) and tem0.pjdy = tem4.pjdy(+)  ";

            }
            else if (typeid == "zyq")
            {
                fpselect += " select  d.dep_name as dep_id,tem0.zyq as pjdy, ";
                fpselect += " sum(nvl(tem0.js,0)) as js, round(sum(nvl(tem0.hscyl,0))/10000,4) as hscyl, sum(nvl(round(tem0.czcb/10000,4),0)) as czcb, ";
                fpselect += " sum(nvl(tem1.js,0)) as js1,0 as bl101, round(sum(nvl(tem1.hscyl,0))/10000,4) as hscyl1,0 as bl102, sum(nvl(round(tem1.czcb/10000,4),0)) as czcb1, 0 as bl103,";
                fpselect += " sum(nvl(tem2.js,0))  as js2,0 as bl201, round(sum(nvl(tem2.hscyl,0))/10000,4)  as hscyl2,0 as bl202, sum(nvl(round(tem2.czcb/10000,4),0))  as czcb2,0 as bl203, ";
                fpselect += " sum(nvl(tem3.js,0))  as js3,0 as bl301, round(sum(nvl(tem3.hscyl,0))/10000,4)  as hscyl3,0 as bl302, sum(nvl(round(tem3.czcb/10000,4),0))  as czcb3,0 as bl303, ";
                fpselect += " sum(nvl(tem4.js,0))  as js4,0 as bl401, round(sum(nvl(tem4.hscyl,0))/10000,4)  as hscyl4,0 as bl402, sum(nvl(round(tem4.czcb/10000,4),0))  as czcb4 ,0 as bl403, ";
                fpselect += " tem0.zyqdm as zyq ";

                fpselect += " from ( ";
                //tem0
                fpselect += " select sdy.dj_id as dj_id, nvl(sdy.djisopen,0) as js,sdy.eny as eny, sdy.bny as bny,sdy.dep_id as dep_id,  ";
                fpselect += " nvl(sdy.hscyl,0) as hscyl, nvl(sdy.czcb,0) as czcb,sdy.zyqdm as zyqdm,sdy.zyq as zyq  from view_dtstat_zyqsj sdy ";
                fpselect += " where sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                //fpselect += "  ";
                fpselect += " ) tem0, ";


                //tem1
                fpselect += " (select  sdy.dj_id as dj_id, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from view_dtstat_zyqsj sdy where sdy.gsxyjb_1 = '1' and sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " group by sdy.dj_id  ";
                fpselect += " ) tem1, ";
                //tem2
                fpselect += " (select  sdy.dj_id as dj_id, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from view_dtstat_zyqsj sdy where sdy.gsxyjb_1 = '2' and sdy.djisopen = '1' ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += " group by sdy.dj_id ";
                fpselect += " ) tem2,  ";
                //tem3
                fpselect += " (select  sdy.dj_id as dj_id, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from view_dtstat_zyqsj sdy where sdy.gsxyjb_1 = '3'  and sdy.djisopen = '1'  ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  group by sdy.dj_id ";
                fpselect += " ) tem3, ";
                //tem4
                fpselect += " (select  sdy.dj_id as dj_id, nvl(sum(sdy.djisopen),0) as js, nvl(sum(sdy.hscyl),0) as hscyl, nvl(sum(sdy.czcb),0) as czcb ";
                fpselect += " from view_dtstat_zyqsj sdy where sdy.gsxyjb_1 = '4' and sdy.djisopen = '1'  ";
                if (list == "quan")
                {
                    fpselect += "";
                }
                else
                {
                    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
                }
                fpselect += "  group by sdy.dj_id ";
                fpselect += " ) tem4,department d ";
                fpselect += " where	tem0.dep_id=d.dep_id and tem0.dj_id = tem1.dj_id(+) and tem0.dj_id = tem2.dj_id(+) and tem0.dj_id = tem3.dj_id(+) and tem0.dj_id = tem4.dj_id(+)  ";
                fpselect += "group by d.dep_name,tem0.zyqdm,tem0.zyq ";
                fpselect += " order by d.dep_name,tem0.zyqdm ";
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
                        dr[1] = "合计";
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
                    string path = "../../../static/excel/zuoyequ.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表4-井块结合效益汇总表");

                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount - 1; j++)
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }

                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 5; j < ccount - 1; j += 2)
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
                    string path = "../../../static/excel/zuoyequ.xls";
                    path = Page.MapPath(path);
                    this.FpSpread1.Sheets[0].OpenExcel(path, "表4-井块结合效益汇总表");

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
                ///////////////////////////09.4.29日更新/////////////////////////////////////
                if (typeid == "yt")
                    FpSpread1.Sheets[0].Cells[2, 0].Value = "";
                else if (typeid == "pjdy")
                    FpSpread1.Sheets[0].Cells[2, 0].Value = "评价单元";
                else if (typeid == "qk")
                    FpSpread1.Sheets[0].Cells[2, 0].Value = "评价区块";
                else
                    FpSpread1.Sheets[0].Cells[2, 0].Value = "作业区";
                ///////////////////////////////////////////////////////////////
                /////////////////////////09.4.30更新//////////////////////////////////////
                if (typeid == "yt")
                {
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "采油厂";

                    FpSpread1.Sheets[0].Cells[2, 0].Value = FpSpread1.Sheets[0].Cells[2, 1].Value;
                    FpSpread1.ActiveSheetView.AddSpanCell(2, 0, 3, 2);

                    for (int i = 5; i < FpSpread1.Sheets[0].RowCount; i++)
                    {

                        FpSpread1.Sheets[0].Cells[i, 0].Value = FpSpread1.Sheets[0].Cells[i, 1].Value;

                        FpSpread1.ActiveSheetView.AddSpanCell(i, 0, 1, 2);
                        FpSpread1.ActiveSheetView.Cells[i, 0].Column.Width = 4;
                    }
                    FpSpread1.Sheets[0].Cells[FpSpread1.Sheets[0].RowCount - 1, 0].Value = "合计";
                }
                else if (typeid == "zyq")
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text + "(作业区)";
                }
                else if (typeid == "pjdy")
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text + "(评价单元)";
                }
                else
                {
                    FpSpread1.Sheets[0].Cells[0, 0].Text = FpSpread1.Sheets[0].Cells[0, 0].Text + "(区块)";
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

        protected void DC_Click(object sender, EventArgs e)
        {
            //this.FpSpread1.Sheets[0].Cells[0, 0].Text = "您点击了‘导出按钮’";

            FpSpread1.Sheets[0].RowHeader.Visible = true;
            FpSpread1.Sheets[0].ColumnHeader.Visible = true;
            //FpSpread1.SaveExcelToResponse("dt_biao1.xls");
            FarpointGridChange.FarPointChange(FpSpread1, "dt_biao4.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;


        }

    }
}
