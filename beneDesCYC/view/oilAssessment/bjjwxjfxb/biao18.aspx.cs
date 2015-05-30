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

namespace beneDesCYC.view.oilAssessment.bjjwxjfxb
{
    public partial class biao18 : beneDesCYC.core.UI.corePage
    {
        static int rcount = 0;
        static int ccount = 0;
        //static int hcount = 5;
        static DataSet fpset = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            string zdw = _getParam("zdw");
            if (zdw == "null")
            { initSpread2(); }
            else
            { initSpread(); }

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }

        protected void initSpread()
        {
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            string zdw = _getParam("zdw");

            string path = Page.MapPath("../../../static/excel/dongtai.xls");
            this.FpSpread1.Sheets[0].OpenExcel(path, "表16-边际效益井对比分析表 ");
            FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
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
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            string zdw = _getParam("zdw");

            string path = Page.MapPath("../../../static/excel/dongtai.xls");
            this.FpSpread1.Sheets[0].OpenExcel(path, "表16-边际效益井对比分析表 ");
            FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

            //this.FpSpread1.Sheets[0].Cells[3, 1].Text = _getParam("targetType");
            //this.FpSpread1.Sheets[0].Cells[4, 1].Text = _getParam("startMonth") + " -- " + _getParam("endMonth");
        }


        protected void sj()
        {
            string cycid = Session["cyc_id"].ToString();
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //string list = _getParam("YCLX");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
         //   string list = _getParam("CYC");
            string lbny = _getParam("lstartMonth");
            string leny = _getParam("lendMonth");
            string dwType = _getParam("dwType");
            string zdw = _getParam("zdw");

            if (bny == eny)
                Response.Write("<script>alert('本次评阶开始时间与结束时间不能相同!')</script>");
            else
            {



                OracleConnection connfp3 = DB.CreatConnection();
                int bt = 4;     //表头行数
                int h = FpSpread1.Sheets[0].Rows.Count - bt;  //总行数减去表头和合计
                string fp3 = "";
                string fp4 = "";
                fp3 += " select xm,jh,reason from bjxyjfxdb where bny = '" + bny + "' and eny = '" + eny + "' and lbny = '" + lbny + "' and leny = '" + leny + "' and cyc_id = '" + cycid + "' order by xm  ";

                connfp3.Open();

                OracleDataAdapter da3 = new OracleDataAdapter(fp3, connfp3);
                DataSet ds3 = new DataSet();
                ds3.Clear();
                da3.Fill(ds3, "bjxyjfxdb");
                try
                {
                    OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);
                    connfp.Open();


                    // string fpselect = "select  '本次评价仍是无效益的井' as xm,a.jh,nvl(a.fxyydm,0) as fxyydm from";
                    //if (dwType.Trim() != "zyq")
                    //{
                    //   fpselect += "(select distinct jdstat_djsj_all.jh,fxyydm from kfsj,jdstat_djsj_all ";

                    //}
                    //else
                    //{
                    //    fpselect += " (select distinct jdstat_djsj_all.jh,fxyydm from kfsj,jdstat_djsj_all,VIEW_DTSTAT_ZYQDM v ";
                    //}

                    // fpselect += " where kfsj.cyc_id='" + cycid + "' and  jdstat_djsj_all.dj_id=kfsj.dj_id and jdstat_djsj_all.gsxyjb='5' and jdstat_djsj_all.bny='" + lbny + "' and jdstat_djsj_all.eny='" + leny + "' and jdstat_djsj_all.cyc_id = '" + cycid + "'";
                    #region
                    // if (list == "quan")
                    // {
                    #region
                    //this.ddltxt1.Enabled = false;
                    //this.ddl1.Enabled = false;

                    ///sql语句
                    #region

                    //本月与上月同为无效井，本次评价仍是无效井的井

                    string fpselect = "select  '本次评价仍是边际效益的井' as xm,a.jh,nvl(a.fxyydm,0) as fxyydm from";
                    if (dwType != "zyq")
                    {
                        fpselect += "(select distinct dtstat_djsj_all.jh,fxyydm from kfsj,dtstat_djsj_all ";

                    }
                    else
                    {
                        fpselect += " (select distinct dtstat_djsj_all.jh,fxyydm from kfsj,dtstat_djsj_all,VIEW_DTSTAT_ZYQSJ v ";
                    }

                    fpselect += " where dtstat_djsj_all.dj_id=kfsj.dj_id and dtstat_djsj_all.gsxyjb_1='3' and dtstat_djsj_all.bny='" + lbny + "' and dtstat_djsj_all.eny='" + leny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "'";


                    if (dwType == "all")
                    {
                        fpselect += " ";

                    }
                    else if (dwType == "qk")
                    {
                        fpselect += " and dtstat_djsj_all.qk='" + zdw + "' ";
                    }
                    else if (dwType == "pjdy")
                    {
                        fpselect += "and dtstat_djsj_all.pjdy='" + zdw + "' ";
                    }
                    else
                    {
                        //fpselect += " and jdstat_djsj_all.dj_id=v.dj_id";
                        //fpselect += " and v.zyqdm='" + this.ddltxt1.SelectedValue + "'";
                        fpselect += " and dtstat_djsj_all.dj_id=v.dj_id";
                        fpselect += " and v.zyqdm='" + zdw + "'";

                    }
                    fpselect += " ) a,";
                    /////////////////

                    if (dwType != "zyq")
                    {
                        fpselect += "(select distinct dtstat_djsj_all.jh,fxyydm from kfsj,dtstat_djsj_all ";

                    }
                    else
                    {
                        fpselect += " (select distinct dtstat_djsj_all.jh,fxyydm from kfsj,dtstat_djsj_all,VIEW_DTSTAT_ZYQSJ v";
                    }
                    fpselect += " where  dtstat_djsj_all.dj_id=kfsj.dj_id and dtstat_djsj_all.gsxyjb_1='3' and dtstat_djsj_all.bny='" + bny + "' and dtstat_djsj_all.eny='" + eny + "'  and  dtstat_djsj_all.cyc_id = '" + cycid + "'";
                    if (dwType == "all")
                    {
                        fpselect += " ";

                    }
                    else if (dwType == "qk")
                    {
                        fpselect += " and dtstat_djsj_all.qk='" + zdw + "' ";
                    }
                    else if (dwType == "pjdy")
                    {
                        fpselect += "and dtstat_djsj_all.pjdy='" + zdw + "' ";
                    }
                    else
                    {
                        //fpselect += " and jdstat_djsj_all.dj_id=v.dj_id";
                        //fpselect += " and v.zyqdm='" + this.ddltxt1.SelectedValue + "'";
                        fpselect += " and dtstat_djsj_all.dj_id=v.dj_id";
                        fpselect += " and v.zyqdm='" + zdw + "'";
                    }
                    fpselect += " ) b";
                    fpselect += " where a.jh=b.jh";

                    fpselect += " union";

                    //本次评价新增加的无效井

                    fpselect += " select '本次评价新增加的边际效益井' as xm, b.jh,nvl(b.fxyydm,0) as fxyydm from";
                    //fpselect += " (select distinct jdstat_djsj_all.jh,fxyydm from kfsj,jdstat_djsj_all,djsj";
                    if (dwType != "zyq")
                    {
                        fpselect += "(select distinct dtstat_djsj_all.jh,fxyydm from kfsj,dtstat_djsj_all ";

                    }
                    else
                    {
                        fpselect += " (select distinct dtstat_djsj_all.jh,fxyydm from kfsj,dtstat_djsj_all,VIEW_DTSTAT_ZYQSJ v";
                    }

                    fpselect += "  where dtstat_djsj_all.dj_id=kfsj.dj_id and dtstat_djsj_all.gsxyjb_1='3' and dtstat_djsj_all.bny='" + bny + "' and dtstat_djsj_all.eny='" + eny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "'";
                    if (dwType == "all")
                    {
                        fpselect += " ";

                    }
                    else if (dwType == "qk")
                    {
                        fpselect += " and dtstat_djsj_all.qk='" + zdw + "' ";
                    }
                    else if (dwType == "pjdy")
                    {
                        fpselect += "and dtstat_djsj_all.pjdy='" + zdw + "' ";
                    }
                    else
                    {
                        //fpselect += " and jdstat_djsj_all.dj_id=v.dj_id";
                        //fpselect += " and v.zyqdm='" + this.ddltxt1.SelectedValue + "'";
                        fpselect += " and dtstat_djsj_all.dj_id=v.dj_id";
                        fpselect += " and v.zyqdm='" + zdw + "'";

                    }
                    fpselect += "  ) b";
                    /////////////
                    fpselect += "  where b.jh not in";

                    if (dwType != "zyq")
                    {
                        fpselect += "(select distinct dtstat_djsj_all.jh from kfsj,dtstat_djsj_all ";

                    }
                    else
                    {
                        fpselect += " (select distinct dtstat_djsj_all.jh from kfsj,dtstat_djsj_all,VIEW_DTSTAT_ZYQSJ v";
                    }
                    fpselect += " where dtstat_djsj_all.dj_id=kfsj.dj_id and dtstat_djsj_all.gsxyjb_1='3' and dtstat_djsj_all.bny='" + lbny + "' and dtstat_djsj_all.eny='" + leny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "' ";
                    if (dwType == "all")
                    {
                        fpselect += " ";

                    }
                    else if (dwType == "qk")
                    {
                        fpselect += " and dtstat_djsj_all.qk='" + zdw + "' ";
                    }
                    else if (dwType == "pjdy")
                    {
                        fpselect += "and dtstat_djsj_all.pjdy='" + zdw + "' ";
                    }
                    else
                    {
                        //fpselect += " and jdstat_djsj_all.dj_id=v.dj_id";
                        //fpselect += " and v.zyqdm='" + this.ddltxt1.SelectedValue + "'";
                        fpselect += " and dtstat_djsj_all.dj_id=v.dj_id";
                        fpselect += " and v.zyqdm='" + zdw + "'";
                    }
                    fpselect += " )";
                    fpselect += " union";

                    //上次评价无效井在本次评价没有出现的井
                    fpselect += " select '上次评价边际效益井在本次评价没有出现的井' as xm,a.jh,nvl(a.fxyydm,0) as fxyydm from";
                    //fpselect += " (select distinct jdstat_djsj_all.jh,fxyydm from kfsj,jdstat_djsj_all,djsj";
                    if (dwType != "zyq")
                    {
                        fpselect += "(select distinct dtstat_djsj_all.jh,fxyydm from kfsj,dtstat_djsj_all ";

                    }
                    else
                    {
                        fpselect += " (select distinct dtstat_djsj_all.jh,fxyydm from kfsj,dtstat_djsj_all,VIEW_DTSTAT_ZYQSJ v";
                    }
                    fpselect += " where dtstat_djsj_all.dj_id=kfsj.dj_id and dtstat_djsj_all.gsxyjb_1='3'  and dtstat_djsj_all.bny='" + lbny + "'  and dtstat_djsj_all.eny='" + leny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "'";
                    if (dwType == "all")
                    {
                        fpselect += " ";

                    }
                    else if (dwType == "qk")
                    {
                        fpselect += " and dtstat_djsj_all.qk='" + zdw + "' ";
                    }
                    else if (dwType == "pjdy")
                    {
                        fpselect += "and dtstat_djsj_all.pjdy='" + zdw + "' ";
                    }
                    else
                    {
                        //fpselect += " and jdstat_djsj_all.dj_id=v.dj_id";
                        //fpselect += " and v.zyqdm='" + this.ddltxt1.SelectedValue + "'";
                        fpselect += " and dtstat_djsj_all.dj_id=v.dj_id";
                        fpselect += " and v.zyqdm='" + zdw + "'";
                    }
                    fpselect += " ) a";
                    //////////////////
                    fpselect += " where a.jh not in";

                    if (dwType != "zyq")
                    {
                        fpselect += "(select distinct dtstat_djsj_all.jh from kfsj,dtstat_djsj_all ";

                    }
                    else
                    {
                        fpselect += " (select distinct dtstat_djsj_all.jh  from kfsj,dtstat_djsj_all,VIEW_DTSTAT_ZYQSJ v";
                    }
                    fpselect += " where dtstat_djsj_all.dj_id=kfsj.dj_id and dtstat_djsj_all.gsxyjb_1='3' and dtstat_djsj_all.bny='" + bny + "' and dtstat_djsj_all.eny='" + eny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "'";
                    if (dwType == "all")
                    {
                        fpselect += " ";

                    }
                    else if (dwType == "qk")
                    {
                        fpselect += " and dtstat_djsj_all.qk='" + zdw + "' ";
                    }
                    else if (dwType == "pjdy")
                    {
                        fpselect += "and dtstat_djsj_all.pjdy='" + zdw + "' ";
                    }
                    else
                    {
                        //fpselect += " and jdstat_djsj_all.dj_id=v.dj_id";
                        //fpselect += " and v.zyqdm='" + this.ddltxt1.SelectedValue + "'";
                        fpselect += " and dtstat_djsj_all.dj_id=v.dj_id";
                        fpselect += " and v.zyqdm='" + zdw + "'";
                    }
                    fpselect += " )";
                    fpselect += " order by xm";

                    #endregion
                 //   connfp.Open();
                    OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                    //DataSet fpset = new DataSet();
                   // fpset.Tables.Clear();
                    fpset.Clear();
                    da.Fill(fpset, "fpdata");


                    //此处用于绑定数据            

                    rcount = fpset.Tables["fpdata"].Rows.Count;
                    ccount = fpset.Tables["fpdata"].Rows.Count;
                    if (rcount == 0)
                        Response.Write("<script>alert('结果为空！')</script>");


                    if (FpSpread1.Sheets[0].Rows.Count == 4)
                    {
                        FpSpread1.Sheets[0].AddRows(4, rcount);

                        FpSpread1.ActiveSheetView.AllowPage = false;
                        //FpSpread1.ActiveSheetView.RowCount = 30;
                        //FpSpread1.ActiveSheetView.PageSize = 30;

                        for (int i = 0; i < rcount; i++)
                        {
                            FpSpread1.Sheets[0].Cells[4 + i, 0].Value = fpset.Tables["fpdata"].Rows[i][0].ToString();
                            FpSpread1.Sheets[0].Cells[4 + i, 1].Value = 1 + i;
                            FpSpread1.Sheets[0].Cells[4 + i, 2].Value = fpset.Tables["fpdata"].Rows[i][1].ToString();
                            //////////////////////////////////////////
                            if (ds3.Tables["bjxyjfxdb"].Rows.Count != 0)
                            {
                                for (int j = 0; j < ds3.Tables["bjxyjfxdb"].Rows.Count; j++)   //在数据集里循环匹配井号
                                {

                                    if (FpSpread1.Sheets[0].Cells[i + 4, 0].Value.ToString() == ds3.Tables["bjxyjfxdb"].Rows[j][0].ToString() && FpSpread1.Sheets[0].Cells[i + 4, 2].Value.ToString() == ds3.Tables["bjxyjfxdb"].Rows[j][1].ToString())
                                    {
                                        FpSpread1.Sheets[0].Cells[i + 4, 3].Value = ds3.Tables["bjxyjfxdb"].Rows[j][2].ToString();

                                        continue;
                                    }
                                }
                            }
                            //////////////////////////////////

                        }


                        for (int m = 0; m < 1; m++)  //列
                        {
                            int k = 1;  //统计重复单元格
                            int w = 4;  //记录起始位置
                            for (int i = w + 1; i < FpSpread1.ActiveSheetView.Rows.Count; i++)
                            {
                                if (FpSpread1.ActiveSheetView.Cells[i, m].Value.ToString() == FpSpread1.ActiveSheetView.Cells[i - 1, m].Value.ToString())
                                {
                                    if (i == FpSpread1.ActiveSheetView.Rows.Count - 1)
                                    {
                                        FpSpread1.Sheets[0].Cells[i, 1].Value = k + 1;
                                        FpSpread1.Sheets[0].Cells[i - 1, 1].Value = k;
                                    }
                                    else

                                        FpSpread1.Sheets[0].Cells[i - 1, 1].Value = k;
                                    k++;
                                }
                                else
                                {

                                    FpSpread1.Sheets[0].Cells[i - 1, 1].Value = k;

                                    if (k != 1)
                                    {
                                        FpSpread1.Sheets[0].Cells[i, 1].Value = k;
                                        FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                                    }
                                    else if (k == 1)
                                        FpSpread1.Sheets[0].Cells[i, 1].Value = 1;
                                    w = i;
                                    k = 1;
                                }
                            }
                            FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                        }

                    }
                    else//不为空
                    {
                        string path = Page.MapPath("~/excel/gufenbiao.xls");
                        FpSpread1.Sheets[0].OpenExcel(path, "表16-边际效益井对比分析表 ");
                        FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                        FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                        FpSpread1.Sheets[0].RowHeader.Visible = false;
                        //FpSpread1.Sheets[0].Cells[1, 0].Value = tbdw;      ////////09.5.13  3-3
                        FpSpread1.Sheets[0].Cells[1, 0].Font.Size = 9;     ////////09.5.13  3-3
                        FpSpread1.Sheets[0].AddRows(4, rcount);
                        FpSpread1.ActiveSheetView.AllowPage = false;
                        for (int i = 0; i < rcount; i++)
                        {
                            FpSpread1.Sheets[0].Cells[4 + i, 0].Value = fpset.Tables["fpdata"].Rows[i][0].ToString();
                            FpSpread1.Sheets[0].Cells[4 + i, 1].Value = 1 + i;
                            FpSpread1.Sheets[0].Cells[4 + i, 2].Value = fpset.Tables["fpdata"].Rows[i][1].ToString();
                            /////////////////////////////////////
                            if (ds3.Tables["bjxyjfxdb"].Rows.Count != 0)
                            {
                                for (int j = 0; j < ds3.Tables["bjxyjfxdb"].Rows.Count; j++)   //在数据集里循环匹配井号
                                {

                                    if (FpSpread1.Sheets[0].Cells[i + 4, 0].Value.ToString() == ds3.Tables["bjxyjfxdb"].Rows[j][0].ToString() && FpSpread1.Sheets[0].Cells[i + 4, 2].Value.ToString() == ds3.Tables["bjxyjfxdb"].Rows[j][1].ToString())
                                    {
                                        FpSpread1.Sheets[0].Cells[i + 4, 3].Value = ds3.Tables["bjxyjfxdb"].Rows[j][2].ToString();

                                        continue;
                                    }
                                }
                            }
                            ///////////////////////////////////////

                        }

                        for (int m = 0; m < 1; m++)  //列
                        {
                            int k = 1;  //统计重复单元格
                            int w = 4;  //记录起始位置
                            for (int i = w + 1; i < FpSpread1.ActiveSheetView.Rows.Count; i++)
                            {
                                if (FpSpread1.ActiveSheetView.Cells[i, m].Value.ToString() == FpSpread1.ActiveSheetView.Cells[i - 1, m].Value.ToString())
                                {
                                    if (i == FpSpread1.ActiveSheetView.Rows.Count - 1)
                                    {
                                        FpSpread1.Sheets[0].Cells[i, 1].Value = k + 1;
                                        FpSpread1.Sheets[0].Cells[i - 1, 1].Value = k;
                                    }
                                    else

                                        FpSpread1.Sheets[0].Cells[i - 1, 1].Value = k;
                                    k++;
                                }
                                else
                                {

                                    FpSpread1.Sheets[0].Cells[i - 1, 1].Value = k;

                                    if (k != 1)
                                    {
                                        FpSpread1.Sheets[0].Cells[i, 1].Value = k;
                                        FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                                    }
                                    w = i;
                                    k = 1;
                                }
                            }
                            FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                        }
                    }
                    #endregion




                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                    /*      else//////////如果不选全部
                          {

                              #region
                              //this.ddltxt1.Enabled = true;
                              //this.ddl1.Enabled = true;
                              ///sql语句
                              #region

                              //本月与上月同为无效井，本次评价仍是无效井的井

                              string fpselect = "select  '本次评价仍是无效益的井' as xm,a.jh,nvl(a.fxyydm,0)  as fxyydm,a.dep_name,a.dep_id from";
                              //if (dwType.Trim() != "zyq")
                              //{
                              fpselect += "(select distinct jdstat_djsj_all.jh,fxyydm,jdstat_djsj_all.dep_id ,d.dep_name from kfsj,jdstat_djsj_all,department d ";

                              //}
                              //else
                              //{
                              //    fpselect += " (select distinct jdstat_djsj_all.jh,fxyydm ,jdstat_djsj_all.dep_id, d.dep_name from kfsj,jdstat_djsj_all,VIEW_DTSTAT_ZYQDM v,department d ";
                              //}

                              fpselect += " where  kfsj.dep_id='" + list + "'and  jdstat_djsj_all.dj_id=kfsj.dj_id and jdstat_djsj_all.gsxyjb='5' and jdstat_djsj_all.bny='" + lbny + "' and jdstat_djsj_all.eny='" + leny + "' and jdstat_djsj_all.dep_id = '" + list + "'";


                              if (dwType == "null")
                              {
                                  fpselect += " ";

                              }
                              else if (dwType == "qk")
                              {
                                  fpselect += " and jdstat_djsj_all.qk='" + zdw + "' ";
                              }
                              else if (dwType == "pjdy")
                              {
                                  fpselect += "and jdstat_djsj_all.pjdy='" + zdw + "' ";
                              }
                              else
                              {
                                  fpselect += " and kfsj.zyq='" + zdw + "'  ";


                              }
                              fpselect += "  and d.dep_id=jdstat_djsj_all.dep_id";
                              fpselect += " ) a,";
                              /////////////////

                              //if (dwType.Trim() != "zyq")
                              //{
                              fpselect += "(select distinct jdstat_djsj_all.jh,fxyydm,jdstat_djsj_all.dep_id from kfsj,jdstat_djsj_all ";

                              //}
                              //else
                              //{
                              //    fpselect += " (select distinct jdstat_djsj_all.jh,fxyydm ,jdstat_djsj_all.dep_id from kfsj,jdstat_djsj_all,VIEW_DTSTAT_ZYQDM v";
                              //}
                              fpselect += " where  kfsj.dep_id='" + list + "'and  jdstat_djsj_all.dj_id=kfsj.dj_id and jdstat_djsj_all.gsxyjb='5' and jdstat_djsj_all.bny='" + bny + "' and jdstat_djsj_all.eny='" + eny + "'  and  jdstat_djsj_all.dep_id = '" + list + "'";
                              if (dwType == "null")
                              {
                                  fpselect += " ";

                              }
                              else if (dwType == "qk")
                              {
                                  fpselect += " and jdstat_djsj_all.qk='" + zdw + "' ";
                              }
                              else if (dwType == "pjdy")
                              {
                                  fpselect += "and jdstat_djsj_all.pjdy='" + zdw + "' ";
                              }
                              else
                              {
                                  fpselect += " and kfsj.zyq='" + zdw + "'  ";
                              }
                              fpselect += " ) b";
                              fpselect += " where a.jh=b.jh";

                              fpselect += " union";

                              //本次评价新增加的无效井

                              fpselect += " select '本次评价新增加的无效益井' as xm, b.jh,nvl(b.fxyydm,0) as fxyydm,b.dep_name,b.dep_id  from";
                              //fpselect += " (select distinct jdstat_djsj_all.jh,fxyydm from kfsj,jdstat_djsj_all,djsj";
                              //if (dwType.Trim() != "zyq")
                              //{
                              fpselect += "(select distinct jdstat_djsj_all.jh,fxyydm,jdstat_djsj_all.dep_id,d.dep_name from kfsj,jdstat_djsj_all,department d ";

                              //}
                              //else
                              //{
                              //    fpselect += " (select distinct jdstat_djsj_all.jh,fxyydm,jdstat_djsj_all.dep_id ,d.dep_name from kfsj,jdstat_djsj_all,VIEW_DTSTAT_ZYQDM v,department d";
                              //}

                              fpselect += "  where  kfsj.dep_id='" + list + "'and jdstat_djsj_all.dj_id=kfsj.dj_id and jdstat_djsj_all.gsxyjb='5' and jdstat_djsj_all.bny='" + bny + "' and jdstat_djsj_all.eny='" + eny + "' and jdstat_djsj_all.dep_id = '" + list + "'";
                              if (dwType == "null")
                              {
                                  fpselect += " ";

                              }
                              else if (dwType == "qk")
                              {
                                  fpselect += " and jdstat_djsj_all.qk='" + zdw + "' ";
                              }
                              else if (dwType == "pjdy")
                              {
                                  fpselect += "and jdstat_djsj_all.pjdy='" + zdw + "' ";
                              }
                              else
                              {
                                  fpselect += " and kfsj.zyq='" + zdw + "'  ";
                              }
                              fpselect += "  and d.dep_id=jdstat_djsj_all.dep_id";
                              fpselect += "  ) b";
                              /////////////
                              fpselect += "  where b.jh not in";

                              //if (dwType.Trim() != "zyq")
                              //{
                              fpselect += "(select distinct jdstat_djsj_all.jh from kfsj,jdstat_djsj_all ";

                              //}
                              //else
                              //{
                              //    fpselect += " (select distinct jdstat_djsj_all.jh from kfsj,jdstat_djsj_all,VIEW_DTSTAT_ZYQDM v";
                              //}
                              fpselect += " where  kfsj.dep_id='" + list + "'and  jdstat_djsj_all.dj_id=kfsj.dj_id and jdstat_djsj_all.gsxyjb='5' and jdstat_djsj_all.bny='" + lbny + "' and jdstat_djsj_all.eny='" + leny + "' and jdstat_djsj_all.dep_id = '" + list + "' ";
                              if (dwType == "null")
                              {
                                  fpselect += " ";

                              }
                              else if (dwType == "qk")
                              {
                                  fpselect += " and jdstat_djsj_all.qk='" + zdw + "' ";
                              }
                              else if (dwType == "pjdy")
                              {
                                  fpselect += "and jdstat_djsj_all.pjdy='" + zdw + "' ";
                              }
                              else
                              {
                                  fpselect += " and kfsj.zyq='" + zdw + "'  ";
                              }
                              fpselect += " )";
                              fpselect += " union";

                              //上次评价无效井在本次评价没有出现的井



                              fpselect += " select '上次评价无效益井在本次评价没有出现的井' as xm,a.jh,nvl(a.fxyydm,0) as fxyydm,a.dep_name,a.dep_id  from";
                              //fpselect += " (select distinct jdstat_djsj_all.jh,fxyydm from kfsj,jdstat_djsj_all,djsj";
                              //if (dwType.Trim() != "zyq")
                              //{
                              fpselect += "(select distinct jdstat_djsj_all.jh,fxyydm,jdstat_djsj_all.dep_id,d.dep_name from kfsj,jdstat_djsj_all,department d ";

                              //}
                              //else
                              //{
                              //    fpselect += " (select distinct jdstat_djsj_all.jh,fxyydm,jdstat_djsj_all.dep_id,d.dep_name from kfsj,jdstat_djsj_all,VIEW_DTSTAT_ZYQDM v ,department d";
                              //}
                              fpselect += " where  kfsj.dep_id='" + list + "'and  jdstat_djsj_all.dj_id=kfsj.dj_id and jdstat_djsj_all.gsxyjb='5'  and jdstat_djsj_all.bny='" + lbny + "'  and jdstat_djsj_all.eny='" + leny + "' and jdstat_djsj_all.dep_id = '" + list + "'";
                              if (dwType == "null")
                              {
                                  fpselect += " ";

                              }
                              else if (dwType == "qk")
                              {
                                  fpselect += " and jdstat_djsj_all.qk='" + zdw + "' ";
                              }
                              else if (dwType == "pjdy")
                              {
                                  fpselect += "and jdstat_djsj_all.pjdy='" + zdw + "' ";
                              }
                              else
                              {
                                  fpselect += " and kfsj.zyq='" + zdw + "'  ";
                              }
                              fpselect += "  and d.dep_id=jdstat_djsj_all.dep_id";
                              fpselect += " ) a";
                              //////////////////
                              fpselect += " where a.jh not in";

                              //if (dwType.Trim() != "zyq")
                              //{
                              fpselect += "(select distinct jdstat_djsj_all.jh from kfsj,jdstat_djsj_all ";

                              //}
                              //else
                              //{
                              //    fpselect += " (select distinct jdstat_djsj_all.jh  from kfsj,jdstat_djsj_all,VIEW_DTSTAT_ZYQDM v";
                              //}
                              fpselect += " where  kfsj.dep_id='" + list + "'and  jdstat_djsj_all.dj_id=kfsj.dj_id and jdstat_djsj_all.gsxyjb='5' and jdstat_djsj_all.bny='" + bny + "' and jdstat_djsj_all.eny='" + eny + "' and jdstat_djsj_all.dep_id = '" + list + "'";
                              if (dwType == "null")
                              {
                                  fpselect += " ";

                              }
                              else if (dwType == "qk")
                              {
                                  fpselect += " and jdstat_djsj_all.qk='" + zdw + "' ";
                              }
                              else if (dwType == "pjdy")
                              {
                                  fpselect += "and jdstat_djsj_all.pjdy='" + zdw + "' ";
                              }
                              else
                              {
                                  fpselect += " and kfsj.zyq='" + zdw + "'  ";
                              }
                              fpselect += " )";
                              fpselect += " order by dep_name, xm";
                              #endregion

                              OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                              //DataSet fpset = new DataSet();
                              fpset.Tables.Clear();
                              da.Fill(fpset, "fpdata");


                              //此处用于绑定数据            

                              rcount = fpset.Tables["fpdata"].Rows.Count;
                              ccount = fpset.Tables["fpdata"].Rows.Count;
                              if (rcount == 0)
                                  Response.Write("<script>alert('结果为空！')</script>");


                              if (FpSpread1.Sheets[0].Rows.Count == 4)
                              {
                                  FpSpread1.Sheets[0].AddRows(4, rcount);

                                  FpSpread1.ActiveSheetView.AllowPage = false;
                                  //FpSpread1.ActiveSheetView.RowCount = 30;
                                  //FpSpread1.ActiveSheetView.PageSize = 30;

                                  for (int i = 0; i < rcount; i++)
                                  {
                                      FpSpread1.Sheets[0].Cells[i + 4, 0].Value = fpset.Tables["fpdata"].Rows[i][3].ToString();
                                      FpSpread1.Sheets[0].Cells[4 + i, 1].Value = fpset.Tables["fpdata"].Rows[i][0].ToString();
                                      FpSpread1.Sheets[0].Cells[4 + i, 3].Value = fpset.Tables["fpdata"].Rows[i][1].ToString(); ;
                                      //FpSpread1.Sheets[0].Cells[4 + i, 3].Value = 1 + i;
                                      //FpSpread1.Sheets[0].Cells[4 + i, 4].Value = fpset.Tables["fpdata"].Rows[i][2].ToString();

                                  }
                                  for (int m = 0; m < 1; m++)
                                  {
                                      //int m = 0;
                                      int k = 1;  //统计重复单元格
                                      int w = 4;  //记录起始位置
                                      for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                                      {
                                          if (FpSpread1.Sheets[0].Cells[i, m].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, m].Value.ToString() && FpSpread1.Sheets[0].Cells[i, m].Value.ToString() != "")
                                          {
                                              k++;
                                          }
                                          else
                                          {
                                              if (k != 1)
                                              {
                                                  FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                                              }
                                              w = i;
                                              k = 1;
                                          }
                                      }
                                      FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                                  }

                                  for (int m = 1; m < 2; m++)  //列
                                  {
                                      int k = 1;  //统计重复单元格
                                      int w = 4;  //记录起始位置
                                      for (int i = w + 1; i < FpSpread1.ActiveSheetView.Rows.Count; i++)
                                      {
                                          if (FpSpread1.ActiveSheetView.Cells[i, m].Value.ToString() == FpSpread1.ActiveSheetView.Cells[i - 1, m].Value.ToString())
                                          {
                                              if (i == FpSpread1.ActiveSheetView.Rows.Count - 1)
                                              {
                                                  FpSpread1.Sheets[0].Cells[i, 2].Value = k + 1;
                                                  FpSpread1.Sheets[0].Cells[i - 1, 2].Value = k;
                                              }
                                              else

                                                  FpSpread1.Sheets[0].Cells[i - 1, 2].Value = k;
                                              k++;
                                          }
                                          else
                                          {

                                              FpSpread1.Sheets[0].Cells[i - 1, 2].Value = k;

                                              if (k != 1)
                                              {
                                                  FpSpread1.Sheets[0].Cells[i, 2].Value = k;
                                                  FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                                              }
                                              w = i;
                                              k = 1;
                                          }
                                      }
                                      FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                                  }

                              }
                              else//不为空
                              {
                                  string path = Page.MapPath("../../../static/excel/gufenbiao.xls");
                                  this.FpSpread1.Sheets[0].OpenExcel(path, "表15-无效井对比分析表");
                                  this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                                  FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                                  FpSpread1.Sheets[0].RowHeader.Visible = false;
                                  //FpSpread1.Sheets[0].Cells[1, 0].Value = tbdw;      ////////09.5.13  3-3
                                  FpSpread1.Sheets[0].Cells[1, 0].Font.Size = 9;     ////////09.5.13  3-3

                                  FpSpread1.Sheets[0].AddRows(4, rcount);

                                  FpSpread1.ActiveSheetView.AllowPage = false;
                                  //FpSpread1.ActiveSheetView.RowCount = 30;
                                  //FpSpread1.ActiveSheetView.PageSize = 30;

                                  for (int i = 0; i < rcount; i++)
                                  {
                                      FpSpread1.Sheets[0].Cells[i + 4, 0].Value = fpset.Tables["fpdata"].Rows[i][3].ToString();
                                      FpSpread1.Sheets[0].Cells[4 + i, 1].Value = fpset.Tables["fpdata"].Rows[i][0].ToString();
                                      FpSpread1.Sheets[0].Cells[4 + i, 3].Value = fpset.Tables["fpdata"].Rows[i][1].ToString(); ;
                                      //FpSpread1.Sheets[0].Cells[4 + i, 3].Value = 1 + i;
                                      //FpSpread1.Sheets[0].Cells[4 + i, 4].Value = fpset.Tables["fpdata"].Rows[i][2].ToString();
                                  }
                                  for (int m = 0; m < 1; m++)
                                  {
                                      //int m = 0;
                                      int k = 1;  //统计重复单元格
                                      int w = 4;  //记录起始位置
                                      for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                                      {
                                          if (FpSpread1.Sheets[0].Cells[i, m].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, m].Value.ToString() && FpSpread1.Sheets[0].Cells[i, m].Value.ToString() != "")
                                          {
                                              k++;
                                          }
                                          else
                                          {
                                              if (k != 1)
                                              {
                                                  FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                                              }
                                              w = i;
                                              k = 1;
                                          }
                                      }
                                      FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                                  }

                                  for (int m = 1; m < 2; m++)  //列
                                  {
                                      int k = 1;  //统计重复单元格
                                      int w = 4;  //记录起始位置
                                      for (int i = w + 1; i < FpSpread1.ActiveSheetView.Rows.Count; i++)
                                      {
                                          if (FpSpread1.ActiveSheetView.Cells[i, m].Value.ToString() == FpSpread1.ActiveSheetView.Cells[i - 1, m].Value.ToString())
                                          {
                                              if (i == FpSpread1.ActiveSheetView.Rows.Count - 1)
                                              {
                                                  FpSpread1.Sheets[0].Cells[i, 2].Value = k + 1;
                                                  FpSpread1.Sheets[0].Cells[i - 1, 2].Value = k;
                                              }
                                              else

                                                  FpSpread1.Sheets[0].Cells[i - 1, 2].Value = k;
                                              k++;
                                          }
                                          else
                                          {

                                              FpSpread1.Sheets[0].Cells[i - 1, 2].Value = k;

                                              if (k != 1)
                                              {
                                                  FpSpread1.Sheets[0].Cells[i, 2].Value = k;
                                                  FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                                              }
                                              w = i;
                                              k = 1;
                                          }
                                      }
                                      FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                                  }


                              }
                              #endregion

                          }
                  */
                    if (zdw == "")
                    {
                        FpSpread1.Sheets[0].Cells[2, 1].Value = "全厂";
                    }
                    else
                    {
                        //FpSpread1.Sheets[0].Cells[2, 2].Value = zdw;
                        FpSpread1.Sheets[0].Cells[2, 1].Value = zdw;
                    }

                    #endregion
                    connfp.Close();

                }

                catch (OracleException error)
                {
                    string CuoWu = "错误: " + error.Message.ToString();
                    Response.Write(CuoWu);

                }

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

            FpSpread1.Sheets[0].RowHeader.Visible = true;
            FpSpread1.Sheets[0].ColumnHeader.Visible = true;
            //FpSpread1.SaveExcelToResponse("dt_biao1.xls");
            FarpointGridChange.FarPointChange(FpSpread1, "biao17.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;

        }


    }
}

