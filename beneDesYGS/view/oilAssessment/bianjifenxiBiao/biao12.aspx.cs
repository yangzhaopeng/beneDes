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

namespace beneDesYGS.view.oilAssessment.bianjifenxiBiao
{
    public partial class biao12 : beneDesYGS.core.UI.corePage
    {
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
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/dongtai.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表12-无效益井汇总表 ");

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
            this.FpSpread1.Sheets[0].OpenExcel(path, "表12-无效益井汇总表 ");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

        }

        protected void paixu(float[,] aa)
        {
            //aa[，]的大小写固定值，12*3
            //本函数实现12*3的二维数组排序
            float tem0, tem1, tem2;
            for (int i = 0; i < 5; i++)
            {
                for (int j = i; j < 5; j++)
                {
                    if (aa[i, 1] < aa[j, 1])
                    {
                        tem0 = aa[i, 0];
                        tem1 = aa[i, 1];
                        tem2 = aa[i, 2];
                        aa[i, 0] = aa[j, 0];
                        aa[i, 1] = aa[j, 1];
                        aa[i, 2] = aa[j, 2];
                        aa[j, 0] = tem0;
                        aa[j, 1] = tem1;
                        aa[j, 2] = tem2;
                    }
                }
            }

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
            
            SqlHelper conn = new SqlHelper();
            OracleConnection connfp = conn.GetConn();
            //OracleConnection connfp = DB.CreatConnection();
            string fpselect = "";

            fpselect += " select  sdy.jh ,";
            fpselect += " sdy.qk , ";
            fpselect += " djsj.tcrq , ";
            fpselect += " sdy.ssyt , ";
            fpselect += " sdy.pjdy , ";
            fpselect += " sdy.scsj , ";
            fpselect += " nvl(round(sdy.rcy ,2),0) as rcy,";
            fpselect += " nvl(round(sdy.hscyl,2),0) as hscyl  , ";
            fpselect += " nvl(round(sdy.jkcyl,2),0) as jkcyl  , ";
            fpselect += " nvl(round(sdy.hs ,2),0) as hs, ";
            fpselect += " nvl(round(sdy.yqspl ,2),0) as yqspl, ";
            fpselect += " round(nvl(sdy.czcb ,0),2) as czcb, ";
            fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.hsczcb,0)/sdy.yqspl,2) end) as dyqczcb,";

            fpselect += " '直接材料费'as feen1, ";
            fpselect += " nvl(sdy.zjclf_1,0) as fee1, ";
            fpselect += " (case when nvl(sdy.zjyxczcb,0) = 0 then 0 else round((nvl(sdy.zjclf_1,0)/sdy.zjyxczcb)*100,2)  end )as feebl1, ";

            fpselect += " '直接动力费' as feen2, ";
            fpselect += " nvl(sdy.zjdlf_1,0) as fee2 , ";
            fpselect += " (case when nvl(sdy.zjyxczcb,0) = 0 then 0 else round((nvl(sdy.zjdlf_1,0)/sdy.zjyxczcb)*100,2)  end )  as feebl2, ";

            fpselect += " '维护性井下作业费' as feen3, ";
            fpselect += "  nvl(sdy.whxzylwf,0) as fee3, ";
            fpselect += " (case when nvl(sdy.zjyxczcb,0) = 0 then 0 else round((nvl(sdy.whxzylwf,0)/sdy.zjyxczcb)*100,2)  end )  as feebl3, ";

            fpselect += " '驱油物注入费' as feen4, ";
            fpselect += " nvl(sdy.qywzrf-sdy.qywzrf_ryf,0) as fee4 , ";
            fpselect += " (case when nvl(sdy.zjyxczcb,0) = 0 then 0 else round((nvl(sdy.qywzrf-sdy.qywzrf_ryf,0)/sdy.zjyxczcb)*100,2)  end )  as feebl4, ";

            fpselect += " '油气处理费' as feen5, ";
            fpselect += " nvl(sdy.yqclf-sdy.yqclf_ryf,0) as fee5 , ";
            fpselect += " (case when nvl(sdy.zjyxczcb,0) = 0 then 0 else round((nvl(sdy.yqclf-sdy.yqclf_ryf,0)/sdy.zjyxczcb)*100,2)  end )  as feebl5, ";


            fpselect += "  sdy.gsxyjb_1, ";
            fpselect += " sdy.bny, ";
            fpselect += " sdy.eny, ";
            fpselect += "  kfsj.ny ";

            fpselect += " from dtstat_djsj sdy ,djsj ,kfsj ";
            fpselect += " where sdy.jh  = djsj.jh(+) and djsj.ny(+)=sdy.bny  and sdy.jh = kfsj.jh(+) ";
            fpselect += " and kfsj.ny(+)=sdy.bny  and  sdy.gsxyjb_1 = '4'";
            if (list == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            }



            try
            {
                connfp.Open();
                ////////////////////09.5.8更新//////////////////////
                int dtxyjb = 4;
                string fp2 = "";
                fp2 += " select jh,xbcs from dtbiaoqt where bny = '" + bny + "' and eny = '" + eny + "' and gsxyjb_1 = '" + dtxyjb + "' ";


                OracleDataAdapter da2 = new OracleDataAdapter(fp2, connfp);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2, "xbcs");
                ////////////////////////////////////////////

                OracleCommand myComm = new OracleCommand(fpselect, connfp);
                OracleDataReader myReader = myComm.ExecuteReader();
                //DataTable Fptable用来输出数据
                DataTable Fptable = new DataTable("fpdata");
                Fptable.Columns.Add("xuhao", typeof(string));
                Fptable.Columns.Add("jh", typeof(string));
                Fptable.Columns.Add("qk", typeof(string));
                Fptable.Columns.Add("tcrq", typeof(string));
                Fptable.Columns.Add("ssyt", typeof(string));
                Fptable.Columns.Add("pjdy", typeof(string));
                Fptable.Columns.Add("scsj", typeof(float));
                Fptable.Columns.Add("rcy", typeof(float));
                Fptable.Columns.Add("hscyl", typeof(float));
                Fptable.Columns.Add("jkcyl", typeof(float));
                Fptable.Columns.Add("hs", typeof(float));
                Fptable.Columns.Add("yqspl", typeof(float));
                Fptable.Columns.Add("czcb", typeof(float));
                Fptable.Columns.Add("dyqczcb", typeof(float));

                Fptable.Columns.Add("czcb1", typeof(string));
                Fptable.Columns.Add("czcbbl1", typeof(float));

                Fptable.Columns.Add("czcb2", typeof(string));
                Fptable.Columns.Add("czcbbl2", typeof(float));

                Fptable.Columns.Add("czcb3", typeof(string));
                Fptable.Columns.Add("czcbbl3", typeof(float));
                Fptable.Columns.Add("xbcs", typeof(string));  /////////09.5.更新
                DataRow fprow;
                int n = 1;
                float[,] czcb = new float[5, 3];
                while (myReader.Read())
                {
                    fprow = Fptable.NewRow();
                    fprow[0] = n++;
                    fprow[1] = myReader[0];
                    fprow[2] = myReader[1];
                    fprow[3] = myReader[2];
                    fprow[4] = myReader[3];
                    fprow[5] = myReader[4];
                    fprow[6] = myReader[5];
                    fprow[7] = myReader[6];
                    fprow[8] = myReader[7];
                    fprow[9] = myReader[8];
                    fprow[10] = myReader[9];
                    fprow[11] = myReader[10];
                    fprow[12] = myReader[11];
                    fprow[13] = myReader[12];

                    czcb[0, 0] = 13;// float.Parse(myReader[13].ToString());
                    czcb[0, 1] = float.Parse(myReader[14].ToString());
                    czcb[0, 2] = float.Parse(myReader[15].ToString());

                    czcb[1, 0] = 16;// float.Parse(myReader[16].ToString());
                    czcb[1, 1] = float.Parse(myReader[17].ToString());
                    czcb[1, 2] = float.Parse(myReader[18].ToString());

                    czcb[2, 0] = 19;// float.Parse(myReader[17].ToString());
                    czcb[2, 1] = float.Parse(myReader[20].ToString());
                    czcb[2, 2] = float.Parse(myReader[21].ToString());

                    czcb[3, 0] = 22;// float.Parse(myReader[17].ToString());
                    czcb[3, 1] = float.Parse(myReader[23].ToString());
                    czcb[3, 2] = float.Parse(myReader[24].ToString());

                    czcb[4, 0] = 25;// float.Parse(myReader[17].ToString());
                    czcb[4, 1] = float.Parse(myReader[26].ToString());
                    czcb[4, 2] = float.Parse(myReader[27].ToString());

                    paixu(czcb);
                    int n1, n2, n3;
                    n1 = int.Parse(czcb[0, 0].ToString());
                    n2 = int.Parse(czcb[1, 0].ToString());
                    n3 = int.Parse(czcb[2, 0].ToString());

                    fprow[14] = myReader[n1].ToString();
                    fprow[15] = czcb[0, 2];

                    fprow[16] = myReader[n2].ToString();
                    fprow[17] = czcb[1, 2];

                    fprow[18] = myReader[n3].ToString();
                    fprow[19] = czcb[2, 2];
                    ////////////////////////////////////////////09.5.8

                    if (ds2.Tables["xbcs"].Rows.Count != 0)   //dtbiaoqt不空，则插入
                    {

                        for (int j = 0; j < ds2.Tables["xbcs"].Rows.Count; j++)   //在数据集里循环匹配井号
                        {

                            if (myReader[0].ToString() == ds2.Tables["xbcs"].Rows[j][0].ToString())
                            {
                                fprow[20] = ds2.Tables["xbcs"].Rows[j][1].ToString();
                                continue;
                            }
                        }


                    }
                    ////////////////////////////////////////////
                    Fptable.Rows.Add(fprow);

                }
                connfp.Close();
                //此处用于绑定数据             
                #region
                DataSet fpset = new DataSet();
                fpset.Tables.Add(Fptable);


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
                            if (j != 0 && j != 3 && j != 6 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
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
                    /////////////////////09.4.30//////////////////////添加合计行

                    FpSpread1.Sheets[0].AddRows(hcount + rcount, 1);
                    float a6 = 0;
                    float a7 = 0;
                    float a8 = 0;
                    float a9 = 0;
                    float a10 = 0;
                    float a11 = 0;
                    float a12 = 0;
                    for (int i = 0; i < rcount; i++)
                    {
                        if (fpset.Tables["fpdata"].Rows[i][6].ToString() != "")
                            a6 += float.Parse(fpset.Tables["fpdata"].Rows[i][6].ToString());
                        if (fpset.Tables["fpdata"].Rows[i][7].ToString() != "")
                            a7 += float.Parse(fpset.Tables["fpdata"].Rows[i][7].ToString());
                        if (fpset.Tables["fpdata"].Rows[i][8].ToString() != "")
                            a8 += float.Parse(fpset.Tables["fpdata"].Rows[i][8].ToString());
                        if (fpset.Tables["fpdata"].Rows[i][9].ToString() != "")
                            a9 += float.Parse(fpset.Tables["fpdata"].Rows[i][9].ToString());
                        if (fpset.Tables["fpdata"].Rows[i][10].ToString() != "")
                            a10 += float.Parse(fpset.Tables["fpdata"].Rows[i][10].ToString());
                        if (fpset.Tables["fpdata"].Rows[i][11].ToString() != "")
                            a11 += float.Parse(fpset.Tables["fpdata"].Rows[i][11].ToString());
                        if (fpset.Tables["fpdata"].Rows[i][12].ToString() != "")
                            a12 += float.Parse(fpset.Tables["fpdata"].Rows[i][12].ToString());

                        FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value = a6.ToString();
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 7].Value = a7.ToString();
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 8].Value = a8.ToString();
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 9].Value = a9.ToString();
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 10].Value = a10.ToString();
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 11].Value = a11.ToString();
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 12].Value = Math.Round(float.Parse(a12.ToString()), 2);

                    }
                    //FpSpread1.Sheets[0].Cells[rcount + hcount, 5].Value = "合计";
                    //FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value = Math.Round(float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value.ToString()),2); // / float.Parse(rcount.ToString()), 2); //生产时间
                    //FpSpread1.Sheets[0].Cells[rcount + hcount, 7].Value = Math.Round(float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 7].Value.ToString()) / float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value.ToString()), 4);
                    //FpSpread1.Sheets[0].Cells[rcount + hcount, 10].Value = Math.Round(float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 10].Value.ToString()) / float.Parse(rcount.ToString()), 2);  //含水

                    //FpSpread1.Sheets[0].Cells[rcount + hcount, 13].Value = Math.Round(float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 12].Value.ToString()) / float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 11].Value.ToString()), 2);
                    //////////////////////////////////////////


                    //FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value = float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value.ToString()).ToString("0.00");// / float.Parse(rcount.ToString()), 2); //生产时间
                    //FpSpread1.Sheets[0].Cells[rcount + hcount, 7].Value = (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 7].Value.ToString()) / float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value.ToString())).ToString("0.0000");
                    //FpSpread1.Sheets[0].Cells[rcount + hcount, 10].Value = (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 10].Value.ToString()) / float.Parse(rcount.ToString())).ToString("0.00");  //含水

                    //FpSpread1.Sheets[0].Cells[rcount + hcount, 13].Value = (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 12].Value.ToString()) / float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 11].Value.ToString())).ToString("0.00");
                    //////////////////////////////////////////





                    /////////////////////09.5.30合并单元格
                    //for (int m = 2; m < 6; m++)  //列
                    //{
                    //    int k = 1;  //统计重复单元格
                    //    int w = hcount;  //记录起始位置
                    //    for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count - 1; i++)
                    //    {
                    //        if (FpSpread1.Sheets[0].Cells[i, m].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, m].Value.ToString())
                    //        {
                    //            k++;
                    //        }
                    //        else
                    //        {
                    //            if (k != 1)
                    //            {
                    //                FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                    //            }
                    //            w = i;
                    //            k = 1;
                    //        }
                    //    }
                    //    FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                    //}
                    //////////////////////////////////
                }
                else//不为空
                {
                    string path = Page.MapPath("~/static/excel/dongtai.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "表12-无效益井汇总表 ");


                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;


                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            if (j != 0 && j != 3 && j != 6 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
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
                    /////////////////////09.4.30//////////////////////添加合计行

                    FpSpread1.Sheets[0].AddRows(hcount + rcount, 1);
                    float a6 = 0;
                    float a7 = 0;
                    float a8 = 0;
                    float a9 = 0;
                    float a10 = 0;
                    float a11 = 0;
                    float a12 = 0;
                    for (int i = 0; i < rcount; i++)
                    {
                        if (fpset.Tables["fpdata"].Rows[i][6].ToString() != "")
                            a6 += float.Parse(fpset.Tables["fpdata"].Rows[i][6].ToString());
                        if (fpset.Tables["fpdata"].Rows[i][7].ToString() != "")
                            a7 += float.Parse(fpset.Tables["fpdata"].Rows[i][7].ToString());
                        if (fpset.Tables["fpdata"].Rows[i][8].ToString() != "")
                            a8 += float.Parse(fpset.Tables["fpdata"].Rows[i][8].ToString());
                        if (fpset.Tables["fpdata"].Rows[i][9].ToString() != "")
                            a9 += float.Parse(fpset.Tables["fpdata"].Rows[i][9].ToString());
                        if (fpset.Tables["fpdata"].Rows[i][10].ToString() != "")
                            a10 += float.Parse(fpset.Tables["fpdata"].Rows[i][10].ToString());
                        if (fpset.Tables["fpdata"].Rows[i][11].ToString() != "")
                            a11 += float.Parse(fpset.Tables["fpdata"].Rows[i][11].ToString());
                        if (fpset.Tables["fpdata"].Rows[i][12].ToString() != "")
                            a12 += float.Parse(fpset.Tables["fpdata"].Rows[i][12].ToString());

                        FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value = a6.ToString();
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 7].Value = a7.ToString();
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 8].Value = a8.ToString();
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 9].Value = a9.ToString();
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 10].Value = a10.ToString();
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 11].Value = a11.ToString();
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 12].Value = Math.Round(float.Parse(a12.ToString()), 2);


                    }
                    FpSpread1.Sheets[0].Cells[rcount + hcount, 5].Value = "合计";
                    //FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value = Math.Round(float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value.ToString()),2);// / float.Parse(rcount.ToString()), 2); //生产时间
                    //FpSpread1.Sheets[0].Cells[rcount + hcount, 7].Value = Math.Round(float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 7].Value.ToString()) / float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value.ToString()), 4);
                    //FpSpread1.Sheets[0].Cells[rcount + hcount, 10].Value = Math.Round(float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 10].Value.ToString()) / float.Parse(rcount.ToString()), 2);  //含水

                    //FpSpread1.Sheets[0].Cells[rcount + hcount, 13].Value = Math.Round(float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 12].Value.ToString()) / float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 11].Value.ToString()), 2);


                    FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value = float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value.ToString()).ToString("0.00");// / float.Parse(rcount.ToString()), 2); //生产时间
                    FpSpread1.Sheets[0].Cells[rcount + hcount, 7].Value = (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 7].Value.ToString()) / float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value.ToString())).ToString("0.0000");
                    FpSpread1.Sheets[0].Cells[rcount + hcount, 10].Value = (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 10].Value.ToString()) / float.Parse(rcount.ToString())).ToString("0.00");  //含水

                    FpSpread1.Sheets[0].Cells[rcount + hcount, 13].Value = (float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 12].Value.ToString()) / float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 11].Value.ToString())).ToString("0.00");
                    //////////////////////////////////////////
                    //////////////////////////////////////////
                    /////////////////////09.5.30合并单元格
                    //for (int m = 2; m < 6; m++)  //列
                    //{
                    //    int k = 1;  //统计重复单元格
                    //    int w = hcount;  //记录起始位置
                    //    for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count - 1; i++)
                    //    {
                    //        if (FpSpread1.Sheets[0].Cells[i, m].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, m].Value.ToString())
                    //        {
                    //            k++;
                    //        }
                    //        else
                    //        {
                    //            if (k != 1)
                    //            {
                    //                FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                    //            }
                    //            w = i;
                    //            k = 1;
                    //        }
                    //    }
                    //    FpSpread1.ActiveSheetView.AddSpanCell(w, m, k, 1);
                    //}
                    //////////////////////////////////
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
            FarpointGridChange.FarPointChange(FpSpread1, "dt_biao12.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;


        }

    }
}
