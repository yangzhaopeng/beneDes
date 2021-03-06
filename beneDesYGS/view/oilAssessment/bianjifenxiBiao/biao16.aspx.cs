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
using beneDesYGS.model.system;
using beneDesYGS.core;

namespace beneDesYGS.view.oilAssessment.bianjifenxiBiao
{
    public partial class biao16 : beneDesYGS.core.UI.corePage
    {
        //static int page = 0;
        static int rcount = 0;
        static int ccount = 0;
        //static int hcount = 5;
        static DataSet fpset = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            string list = _getParam("CYC");
            if (list == "null")
                initSpread2();
            else
                initSpread();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }

        protected void initSpread()
        {
            
            string path = "../../../static/excel/dongtai.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表15-无效井对比分析表");

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
            this.FpSpread1.Sheets[0].OpenExcel(path, "表15-无效井对比分析表");

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
            string lbny = _getParam("lstartMonth");
            string leny = _getParam("lendMonth");
            string dwType = _getParam("dwType");
            string zdw = _getParam("zdw");

            if (bny == eny)
                Response.Write("<script>alert('本次评阶开始时间与结束时间不能相同!')</script>");
            else
            {



                int bt = 4;     //表头行数
                int h = FpSpread1.Sheets[0].Rows.Count - bt;  //总行数减去表头和合计
                string fp3 = "";
                //string fp4 = "";
                try
                {
                    SqlHelper DB = new SqlHelper();
                    OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["ygsConnectionString"].ConnectionString);
                    OracleConnection connfp3 = DB.GetConn();
                    connfp3.Open();
                    connfp.Open();
                    #region
                    if (list == "quan")
                    {


                        #region
                        //this.ddltxt1.Enabled = false;
                        //this.ddl1.Enabled = false;

                        fp3 += " select d.dep_name,xm,jh,reason from wxjfxdb,department d where cyc_id=d.dep_id and bny = '" + bny + "' and eny = '" + eny + "' and  lbny = '" + lbny + "' and leny = '" + leny + "'  ";
                        //fp3 += " and cyc_id  = '" + list + "' ";
                        fp3 += " order by d.dep_id ,xm";


                        OracleDataAdapter da3 = new OracleDataAdapter(fp3, connfp3);
                        DataSet ds3 = new DataSet();
                        ds3.Tables.Clear();
                        da3.Fill(ds3, "wxjfxdbb");
                        ///sql语句
                        #region

                        //本月与上月同为无效井，本次评价仍是无效井的井

                        string fpselect = "select  '本次评价仍是无效益的井' as xm,a.jh,nvl(a.fxyydm,0) as fxyydm,a.dep_name,a.dep_id  from";
                        fpselect += "(select distinct dtstat_djsj_all.jh,fxyydm,dtstat_djsj_all.dep_id,d.dep_name from kfsj,dtstat_djsj_all,department d ";
                        fpselect += " where dtstat_djsj_all.dj_id=kfsj.dj_id and dtstat_djsj_all.gsxyjb_1='4' and dtstat_djsj_all.bny='" + lbny + "' and dtstat_djsj_all.eny='" + leny + "'";
                        fpselect += "  and d.dep_id=dtstat_djsj_all.dep_id";
                        fpselect += " ) a,";
                        fpselect += "(select distinct dtstat_djsj_all.jh,fxyydm,dtstat_djsj_all.dep_id from kfsj,dtstat_djsj_all ";
                        fpselect += " where  dtstat_djsj_all.dj_id=kfsj.dj_id and dtstat_djsj_all.gsxyjb_1='4' and dtstat_djsj_all.bny='" + bny + "' and dtstat_djsj_all.eny='" + eny + "'  ";
                        fpselect += " ) b";
                        fpselect += " where a.jh=b.jh";

                        fpselect += " union";

                        //本次评价新增加的无效井

                        fpselect += " select '本次评价新增加的无效益井' as xm, b.jh,nvl(b.fxyydm,0) as fxyydm ,b.dep_name,b.dep_id from";
                        fpselect += "(select distinct dtstat_djsj_all.jh,fxyydm,dtstat_djsj_all.dep_id,d.dep_name from kfsj,dtstat_djsj_all,department d ";
                        fpselect += "  where dtstat_djsj_all.dj_id=kfsj.dj_id and dtstat_djsj_all.gsxyjb_1='4' and dtstat_djsj_all.bny='" + bny + "' and dtstat_djsj_all.eny='" + eny + "' ";
                        fpselect += "  and d.dep_id=dtstat_djsj_all.dep_id";
                        fpselect += "  ) b";
                        fpselect += "  where b.jh not in";
                        fpselect += "(select distinct dtstat_djsj_all.jh from kfsj,dtstat_djsj_all ";
                        fpselect += " where dtstat_djsj_all.dj_id=kfsj.dj_id and dtstat_djsj_all.gsxyjb_1='4' and dtstat_djsj_all.bny='" + lbny + "' and dtstat_djsj_all.eny='" + leny + "'  ";
                        fpselect += " )";
                        fpselect += " union";

                        //上次评价无效井在本次评价没有出现的井
                        fpselect += " select '上次评价无效益井在本次评价没有出现的井' as xm,a.jh,nvl(a.fxyydm,0) as fxyydm,a.dep_name,a.dep_id  from";
                        fpselect += "(select distinct dtstat_djsj_all.jh,fxyydm,dtstat_djsj_all.dep_id,d.dep_name from kfsj,dtstat_djsj_all,department d ";
                        fpselect += " where dtstat_djsj_all.dj_id=kfsj.dj_id and dtstat_djsj_all.gsxyjb_1='4'  and dtstat_djsj_all.bny='" + lbny + "'  and dtstat_djsj_all.eny='" + leny + "' ";
                        fpselect += "  and d.dep_id=dtstat_djsj_all.dep_id";
                        fpselect += " ) a";

                        fpselect += " where a.jh not in";
                        fpselect += "(select distinct dtstat_djsj_all.jh from kfsj,dtstat_djsj_all ";
                        fpselect += " where dtstat_djsj_all.dj_id=kfsj.dj_id and dtstat_djsj_all.gsxyjb_1='4' and dtstat_djsj_all.bny='" + bny + "' and dtstat_djsj_all.eny='" + eny + "' ";
                        fpselect += " )";
                        fpselect += " order by dep_id, xm";
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
                                //////////////////////////////////////////
                                if (ds3.Tables["wxjfxdbb"].Rows.Count != 0)
                                {
                                    for (int j = 0; j < ds3.Tables["wxjfxdbb"].Rows.Count; j++)   //在数据集里循环匹配井号
                                    {

                                        if (FpSpread1.Sheets[0].Cells[i + 4, 3].Value.ToString() == ds3.Tables["wxjfxdbb"].Rows[j][2].ToString())
                                        {
                                            FpSpread1.Sheets[0].Cells[i + 4, 4].Value = ds3.Tables["wxjfxdbb"].Rows[j][3].ToString();


                                            continue;
                                        }
                                    }
                                }
                                //////////////////////////////////

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
                            string path = Page.MapPath("~/static/excel/dongtai.xls");
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
                                //////////////////////////////////////////
                                if (ds3.Tables["wxjfxdbb"].Rows.Count != 0)
                                {
                                    for (int j = 0; j < ds3.Tables["wxjfxdbb"].Rows.Count; j++)   //在数据集里循环匹配井号
                                    {

                                        if (FpSpread1.Sheets[0].Cells[i + 4, 3].Value.ToString() == ds3.Tables["wxjfxdbb"].Rows[j][2].ToString())
                                        {
                                            FpSpread1.Sheets[0].Cells[i + 4, 4].Value = ds3.Tables["wxjfxdbb"].Rows[j][3].ToString();


                                            continue;
                                        }
                                    }
                                }
                                //////////////////////////////////
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
                    #endregion

                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                    else//////////如果不选全部
                    {

                        #region
                        //this.ddltxt1.Enabled = true;
                        //this.ddl1.Enabled = true;

                        fp3 += " select d.dep_name,xm,jh,reason from wxjfxdb,department d where cyc_id=d.dep_id and bny = '" + bny + "' and eny = '" + eny + "' and  lbny = '" + lbny + "' and leny = '" + leny + "'  ";

                        fp3 += " and cyc_id  = '" + list + "' ";

                        fp3 += " order by d.dep_id ,xm";


                        OracleDataAdapter da3 = new OracleDataAdapter(fp3, connfp3);
                        DataSet ds3 = new DataSet();
                        ds3.Tables.Clear();
                        da3.Fill(ds3, "wxjfxdbb");
                        ///sql语句
                        #region

                        //本月与上月同为无效井，本次评价仍是无效井的井

                        string fpselect = "select  '本次评价仍是无效益的井' as xm,a.jh,nvl(a.fxyydm,0)  as fxyydm,a.dep_name,a.dep_id from";
                        //if (dwType.Trim() != "zyq")
                        //{
                        fpselect += "(select distinct dtstat_djsj_all.jh,fxyydm,dtstat_djsj_all.dep_id ,d.dep_name from kfsj,dtstat_djsj_all,department d ";

                        //}
                        //else
                        //{
                        //    fpselect += " (select distinct dtstat_djsj_all.jh,fxyydm ,dtstat_djsj_all.dep_id, d.dep_name from kfsj,dtstat_djsj_all,VIEW_DTSTAT_ZYQDM v,department d ";
                        //}

                        fpselect += " where  kfsj.dep_id='" + list + "'and  dtstat_djsj_all.dj_id=kfsj.dj_id and dtstat_djsj_all.gsxyjb_1='4' and dtstat_djsj_all.bny='" + lbny + "' and dtstat_djsj_all.eny='" + leny + "' and dtstat_djsj_all.dep_id = '" + list + "'";


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
                            fpselect += " and kfsj.zyq='" + zdw + "'  ";


                        }
                        fpselect += "  and d.dep_id=dtstat_djsj_all.dep_id";
                        fpselect += " ) a,";
                        /////////////////

                        //if (dwType.Trim() != "zyq")
                        //{
                        fpselect += "(select distinct dtstat_djsj_all.jh,fxyydm,dtstat_djsj_all.dep_id from kfsj,dtstat_djsj_all ";

                        //}
                        //else
                        //{
                        //    fpselect += " (select distinct dtstat_djsj_all.jh,fxyydm ,dtstat_djsj_all.dep_id from kfsj,dtstat_djsj_all,VIEW_DTSTAT_ZYQDM v";
                        //}
                        fpselect += " where  kfsj.dep_id='" + list + "'and  dtstat_djsj_all.dj_id=kfsj.dj_id and dtstat_djsj_all.gsxyjb_1='4' and dtstat_djsj_all.bny='" + bny + "' and dtstat_djsj_all.eny='" + eny + "'  and  dtstat_djsj_all.dep_id = '" + list + "'";
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
                            fpselect += " and kfsj.zyq='" + zdw + "'  ";
                        }
                        fpselect += " ) b";
                        fpselect += " where a.jh=b.jh";

                        fpselect += " union";

                        //本次评价新增加的无效井

                        fpselect += " select '本次评价新增加的无效益井' as xm, b.jh,nvl(b.fxyydm,0) as fxyydm,b.dep_name,b.dep_id  from";
                        //fpselect += " (select distinct dtstat_djsj_all.jh,fxyydm from kfsj,dtstat_djsj_all,djsj";
                        //if (dwType.Trim() != "zyq")
                        //{
                        fpselect += "(select distinct dtstat_djsj_all.jh,fxyydm,dtstat_djsj_all.dep_id,d.dep_name from kfsj,dtstat_djsj_all,department d ";

                        //}
                        //else
                        //{
                        //    fpselect += " (select distinct dtstat_djsj_all.jh,fxyydm,dtstat_djsj_all.dep_id ,d.dep_name from kfsj,dtstat_djsj_all,VIEW_DTSTAT_ZYQDM v,department d";
                        //}

                        fpselect += "  where  kfsj.dep_id='" + list + "'and dtstat_djsj_all.dj_id=kfsj.dj_id and dtstat_djsj_all.gsxyjb_1='4' and dtstat_djsj_all.bny='" + bny + "' and dtstat_djsj_all.eny='" + eny + "' and dtstat_djsj_all.dep_id = '" + list + "'";
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
                            fpselect += " and kfsj.zyq='" + zdw + "'  ";
                        }
                        fpselect += "  and d.dep_id=dtstat_djsj_all.dep_id";
                        fpselect += "  ) b";
                        /////////////
                        fpselect += "  where b.jh not in";

                        //if (dwType.Trim() != "zyq")
                        //{
                        fpselect += "(select distinct dtstat_djsj_all.jh from kfsj,dtstat_djsj_all ";

                        //}
                        //else
                        //{
                        //    fpselect += " (select distinct dtstat_djsj_all.jh from kfsj,dtstat_djsj_all,VIEW_DTSTAT_ZYQDM v";
                        //}
                        fpselect += " where  kfsj.dep_id='" + list + "'and  dtstat_djsj_all.dj_id=kfsj.dj_id and dtstat_djsj_all.gsxyjb_1='4' and dtstat_djsj_all.bny='" + lbny + "' and dtstat_djsj_all.eny='" + leny + "' and dtstat_djsj_all.dep_id = '" + list + "' ";
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
                            fpselect += " and kfsj.zyq='" + zdw + "'  ";
                        }
                        fpselect += " )";
                        fpselect += " union";

                        //上次评价无效井在本次评价没有出现的井



                        fpselect += " select '上次评价无效益井在本次评价没有出现的井' as xm,a.jh,nvl(a.fxyydm,0) as fxyydm,a.dep_name,a.dep_id  from";
                        //fpselect += " (select distinct dtstat_djsj_all.jh,fxyydm from kfsj,dtstat_djsj_all,djsj";
                        //if (dwType.Trim() != "zyq")
                        //{
                        fpselect += "(select distinct dtstat_djsj_all.jh,fxyydm,dtstat_djsj_all.dep_id,d.dep_name from kfsj,dtstat_djsj_all,department d ";

                        //}
                        //else
                        //{
                        //    fpselect += " (select distinct dtstat_djsj_all.jh,fxyydm,dtstat_djsj_all.dep_id,d.dep_name from kfsj,dtstat_djsj_all,VIEW_DTSTAT_ZYQDM v ,department d";
                        //}
                        fpselect += " where  kfsj.dep_id='" + list + "'and  dtstat_djsj_all.dj_id=kfsj.dj_id and dtstat_djsj_all.gsxyjb_1='4'  and dtstat_djsj_all.bny='" + lbny + "'  and dtstat_djsj_all.eny='" + leny + "' and dtstat_djsj_all.dep_id = '" + list + "'";
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
                            fpselect += " and kfsj.zyq='" + zdw + "'  ";
                        }
                        fpselect += "  and d.dep_id=dtstat_djsj_all.dep_id";
                        fpselect += " ) a";
                        //////////////////
                        fpselect += " where a.jh not in";

                        //if (dwType.Trim() != "zyq")
                        //{
                        fpselect += "(select distinct dtstat_djsj_all.jh from kfsj,dtstat_djsj_all ";

                        //}
                        //else
                        //{
                        //    fpselect += " (select distinct dtstat_djsj_all.jh  from kfsj,dtstat_djsj_all,VIEW_DTSTAT_ZYQDM v";
                        //}
                        fpselect += " where  kfsj.dep_id='" + list + "'and  dtstat_djsj_all.dj_id=kfsj.dj_id and dtstat_djsj_all.gsxyjb_1='4' and dtstat_djsj_all.bny='" + bny + "' and dtstat_djsj_all.eny='" + eny + "' and dtstat_djsj_all.dep_id = '" + list + "'";
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
                                //////////////////////////////////////////
                                if (ds3.Tables["wxjfxdbb"].Rows.Count != 0)
                                {
                                    for (int j = 0; j < ds3.Tables["wxjfxdbb"].Rows.Count; j++)   //在数据集里循环匹配井号
                                    {

                                        if (FpSpread1.Sheets[0].Cells[i + 4, 3].Value.ToString() == ds3.Tables["wxjfxdbb"].Rows[j][2].ToString())
                                        {
                                            FpSpread1.Sheets[0].Cells[i + 4, 4].Value = ds3.Tables["wxjfxdbb"].Rows[j][3].ToString();


                                            continue;
                                        }
                                    }
                                }
                                //////////////////////////////////

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
                            string path = Page.MapPath("~/static/excel/dongtai.xls");
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
                                //////////////////////////////////////////
                                if (ds3.Tables["wxjfxdbb"].Rows.Count != 0)
                                {
                                    for (int j = 0; j < ds3.Tables["wxjfxdbb"].Rows.Count; j++)   //在数据集里循环匹配井号
                                    {

                                        if (FpSpread1.Sheets[0].Cells[i + 4, 3].Value.ToString() == ds3.Tables["wxjfxdbb"].Rows[j][2].ToString())
                                        {
                                            FpSpread1.Sheets[0].Cells[i + 4, 4].Value = ds3.Tables["wxjfxdbb"].Rows[j][3].ToString();


                                            continue;
                                        }
                                    }
                                }
                                //////////////////////////////////
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
                    if (zdw == "")
                    {
                        FpSpread1.Sheets[0].Cells[2, 2].Value = "全厂";
                    }
                    else
                    {
                        //FpSpread1.Sheets[0].Cells[2, 2].Value = zdw;
                        FpSpread1.Sheets[0].Cells[2, 2].Value = zdw;
                    }
                    connfp.Close();
                    connfp3.Close();
                }

                catch (OracleException error)
                {
                    string CuoWu = "错误: " + error.Message.ToString();
                    Response.Write(CuoWu);

                }

            }
            #region
            //    try
            //    {
            //        if (ds3.Tables["wxjfxdbb"].Rows.Count == 0)   ////空表则逐行插入，非空更新
            //        {
            //            Response.Write("<script>alert('数据库中无此阶段数据!')</script>");
            //            connfp3.Close();
            //        }
            //        else 
            //        {
            //            string path = Page.MapPath("~/excel/dongtai.xls");
            //            FpSpread1.Sheets[0].OpenExcel(path, "表15-无效井对比分析表");

            //            FpSpread1.Sheets[0].Cells[0, 0].Text = bny.Text + "-" + eny.Text + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
            //            FpSpread1.Sheets[0].ColumnHeader.Visible = false;
            //            FpSpread1.Sheets[0].RowHeader.Visible = false;

            //            int rcounts = ds3.Tables["wxjfxdbb"].Rows.Count;
            //            int ccounts = ds3.Tables["wxjfxdbb"].Columns.Count;
            //            FpSpread1.Sheets[0].AddRows(ccounts, rcounts);

            //            for (int i = 0; i < rcounts; i++)
            //            {

            //                FpSpread1.Sheets[0].Cells[i + 4, 0].Value = ds3.Tables["wxjfxdbb"].Rows[i][0].ToString();
            //                FpSpread1.Sheets[0].Cells[i + 4, 1].Value = ds3.Tables["wxjfxdbb"].Rows[i][1].ToString();
            //                FpSpread1.Sheets[0].Cells[i + 4, 2].Value = i+1;
            //                FpSpread1.Sheets[0].Cells[i + 4, 3].Value = ds3.Tables["wxjfxdbb"].Rows[i][2].ToString();
            //                FpSpread1.Sheets[0].Cells[i + 4, 4].Value = ds3.Tables["wxjfxdbb"].Rows[i][3].ToString();


            //            }
            //            //合并单元格
            //            for (int m = 0; m < 1;m++ )
            //            {
            //                //int m = 0;
            //                int k = 1;  //统计重复单元格
            //                int w = 4;  //记录起始位置
            //                for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
            //                {
            //                    if (FpSpread1.Sheets[0].Cells[i, m].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, m].Value.ToString() && FpSpread1.Sheets[0].Cells[i, m].Value.ToString() != "")
            //                    {
            //                        k++;
            //                    }
            //                    else
            //                    {
            //                        if (k != 1)
            //                        {
            //                            FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
            //                        }
            //                        w = i;
            //                        k = 1;
            //                    }
            //                }
            //                FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);


            //            }
            //            for (int m = 1; m < 2; m++)  //列
            //            {
            //                int k = 1;  //统计重复单元格
            //                int w = 4;  //记录起始位置
            //                for (int i = w + 1; i < FpSpread1.ActiveSheetView.Rows.Count; i++)
            //                {
            //                    if (FpSpread1.ActiveSheetView.Cells[i, m].Value.ToString() == FpSpread1.ActiveSheetView.Cells[i - 1, m].Value.ToString())
            //                    {
            //                        if (i == FpSpread1.ActiveSheetView.Rows.Count - 1)
            //                        {
            //                            FpSpread1.Sheets[0].Cells[i, 2].Value = k + 1;
            //                            FpSpread1.Sheets[0].Cells[i - 1, 2].Value = k;
            //                        }
            //                        else

            //                            FpSpread1.Sheets[0].Cells[i - 1, 2].Value = k;
            //                        k++;
            //                    }
            //                    else
            //                    {

            //                        FpSpread1.Sheets[0].Cells[i - 1, 2].Value = k;

            //                        if (k != 1)
            //                        {
            //                            FpSpread1.Sheets[0].Cells[i, 2].Value = k;
            //                            FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
            //                        }
            //                        w = i;
            //                        k = 1;
            //                    }
            //                }
            //                FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
            //            }


            //        }
            //        ds3.Dispose();
            //    }
            //    catch (OracleException error)
            //    {
            //        string CuoWu = "错误: " + error.Message.ToString();
            //        Response.Write(CuoWu);

            //    }
            //}
            #endregion
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
            FarpointGridChange.FarPointChange(FpSpread1, "dt_biao16.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;

        }

    }
}
