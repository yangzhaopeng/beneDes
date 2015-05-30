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
    public partial class biao12 : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Dropdl = _getParam("Dropdl");
            if (Dropdl == "null")
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
            this.FpSpread1.Sheets[0].OpenExcel(path, "表11-边际效益井汇总表");

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
            this.FpSpread1.Sheets[0].OpenExcel(path, "表11-边际效益井汇总表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

        }

        protected void paixu(float[,] aa)
        {
            //aa[，]的大小写固定值，12*3
            //本函数实现12*3的二维数组排序
            float tem0, tem1, tem2;
            for (int i = 0; i < 10; i++)
            {
                for (int j = i; j < 10; j++)
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
            string Dropdl = _getParam("Dropdl");
            string cycid = Session["cyc_id"].ToString();
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
            //fpselect += " select * ";
            //fpselect += " from view_dtbiao3_1 dtb3 ";  //表4也从view_dtbiao3里取数据
            //fpselect += " where  dtb3.gsxyjb_1 = '3' ";

            fpselect += " select  sdy.jh , ";
            fpselect += " sdy.qk , ";
            fpselect += " djsj.tcrq  , ";
            fpselect += " sdy.ssyt  , ";
            fpselect += " sdy.pjdy , ";
            fpselect += " sdy.scsj , ";
            fpselect += " nvl(round(sdy.rcy ,4),0) as rcy,";
            fpselect += " nvl(round(sdy.hscyl,4),0) as hscyl  , ";
            fpselect += " nvl(round(sdy.jkcyl,4),0) as jkcyl  , ";
            fpselect += " nvl(round(sdy.hs ,2),0) as hs, ";
            fpselect += " nvl(round(sdy.yqspl ,4),0) as yqspl, ";
            fpselect += " round(nvl(sdy.czcb ,0),2) as czcb, ";
            fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(sdy.hsczcb,0)/sdy.yqspl,2) end) as dyqczcb,";

            fpselect += " '直接材料费'as feen1, ";
            fpselect += " nvl(sdy.zjclf,0) as fee1, ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.zjclf/sdy.czcb)*100,2)  end )as feebl1, ";

            fpselect += "  '直接燃料费' as feen2, ";
            fpselect += "  nvl(sdy.zjrlf,0) as fee2, ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.zjrlf/sdy.czcb)*100,2)  end )  as feebl2, ";

            fpselect += " '直接动力费' as feen3, ";
            fpselect += " nvl(sdy.zjdlf,0) as fee3 , ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.zjdlf/sdy.czcb)*100,2)  end )  as feebl3, ";

            fpselect += " '驱油物注入费' as feen4, ";
            fpselect += " nvl(sdy.qywzrf,0) as fee4 , ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.qywzrf/sdy.czcb)*100,2)  end )  as feebl4, ";

            fpselect += "  '井下作业费' as feen5, ";
            fpselect += " nvl(sdy.jxzyf,0) as fee5 , ";
            fpselect += "  (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.jxzyf/sdy.czcb)*100,2)  end )  as feebl5, ";

            fpselect += "  '测井试井费' as feen6, ";
            fpselect += " nvl(sdy.cjsjf,0) as fee6, ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.cjsjf/sdy.czcb)*100,2)  end )  as feebl6, ";

            fpselect += " '维护及修理费' as feen7, ";
            fpselect += "  nvl(sdy.whxlf,0) as fee7, ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.whxlf/sdy.czcb)*100,2)  end )  as feebl7, ";

            fpselect += " '油气处理费' as feen8, ";
            fpselect += " nvl(sdy.yqclf,0) as fee8 , ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.yqclf/sdy.czcb)*100,2)  end )  as feebl8, ";

            fpselect += " '运输费' as feen9, ";
            fpselect += " nvl(sdy.ysf,0) as fee9 , ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.ysf/sdy.czcb)*100,2)  end )  as feebl9, ";

            fpselect += "  '其他直接费' as feen10, ";
            fpselect += " nvl(sdy.qtzjf,0) as fee10 , ";
            fpselect += "  (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.qtzjf/sdy.czcb)*100,2)  end )  as feebl10, ";

            //fpselect += "  '厂矿管理费' as feen11, ";
            //fpselect += "  nvl(sdy.ckglf,0) as fee11 , ";
            //fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.ckglf/sdy.czcb)*100,2)  end )  as feebl11, ";

            //fpselect += "  '直接人员费' as feen12, ";
            //fpselect += " nvl(sdy.zjryf,0) as fee12 , ";
            //fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.zjryf/sdy.czcb)*100,2)  end )  as feebl12, ";

            fpselect += "  sdy.gsxyjb_1, ";
            fpselect += " sdy.bny, ";
            fpselect += " sdy.eny, ";
            fpselect += "  kfsj.ny ";

            fpselect += " from dtstat_djsj sdy ,djsj ,kfsj ";
            fpselect += " where sdy.jh  = djsj.jh(+) and djsj.ny(+)=sdy.bny  and sdy.jh = kfsj.jh(+) ";
            fpselect += " and kfsj.ny(+)=sdy.bny  and sdy.cyc_id = '" + cycid + "' and sdy.gsxyjb_1 = '3'";

            if (Dropdl == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.qk = '" + Dropdl + "'";
            }



            try
            {
                connfp.Open();
                ////////////////////09.5.8更新//////////////////////
                int dtxyjb = 3;
                string fp2 = "";
                fp2 += " select jh,xbcs from dtbiaoqt where bny = '" + bny + "' and eny = '" + eny + "' and gsxyjb_1 = '" + dtxyjb + "' and cyc_id = '" + Session["cyc_id"].ToString() + "'";


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
                float[,] czcb = new float[10, 3];
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

                    czcb[5, 0] = 28;// float.Parse(myReader[17].ToString());
                    czcb[5, 1] = float.Parse(myReader[29].ToString());
                    czcb[5, 2] = float.Parse(myReader[30].ToString());

                    czcb[6, 0] = 31;// float.Parse(myReader[17].ToString());
                    czcb[6, 1] = float.Parse(myReader[32].ToString());
                    czcb[6, 2] = float.Parse(myReader[33].ToString());

                    czcb[7, 0] = 34;// float.Parse(myReader[17].ToString());
                    czcb[7, 1] = float.Parse(myReader[35].ToString());
                    czcb[7, 2] = float.Parse(myReader[36].ToString());

                    czcb[8, 0] = 37;// float.Parse(myReader[17].ToString());
                    czcb[8, 1] = float.Parse(myReader[38].ToString());
                    czcb[8, 2] = float.Parse(myReader[39].ToString());

                    czcb[9, 0] = 40;// float.Parse(myReader[17].ToString());
                    czcb[9, 1] = float.Parse(myReader[41].ToString());
                    czcb[9, 2] = float.Parse(myReader[42].ToString());

                    //czcb[10, 0] = 43;// float.Parse(myReader[17].ToString());
                    //czcb[10, 1] = float.Parse(myReader[44].ToString());
                    //czcb[10, 2] = float.Parse(myReader[45].ToString());

                    //czcb[11, 0] = 46;// float.Parse(myReader[17].ToString());
                    //czcb[11, 1] = float.Parse(myReader[47].ToString());
                    //czcb[11, 2] = float.Parse(myReader[48].ToString());


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

                            if (myReader[0].ToString().Trim() == ds2.Tables["xbcs"].Rows[j][0].ToString().Trim())
                            {
                                fprow[20] = ds2.Tables["xbcs"].Rows[j][1].ToString().Trim();
                                //this.Response.Write("here");
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
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 8].Value = Math.Round(a8, 2).ToString("0.00");
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 9].Value = Math.Round(a9, 2).ToString("0.00");
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 10].Value = a10.ToString();
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 11].Value = Math.Round(a11, 2).ToString("0.00");
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 12].Value = Math.Round(a12, 2).ToString("0.00");
                    }
                    FpSpread1.Sheets[0].Cells[rcount + hcount, 5].Value = "合计";
                    FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value = Math.Round(float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value.ToString()), 2); // / float.Parse(rcount.ToString()), 2); //生产时间
                    FpSpread1.Sheets[0].Cells[rcount + hcount, 7].Value = Math.Round(float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 7].Value.ToString()) / float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value.ToString()), 2).ToString("0.00");
                    FpSpread1.Sheets[0].Cells[rcount + hcount, 10].Value = Math.Round(float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 10].Value.ToString()) / float.Parse(rcount.ToString()), 2);  //含水

                    FpSpread1.Sheets[0].Cells[rcount + hcount, 13].Value = Math.Round(float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 12].Value.ToString()) / float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 11].Value.ToString()), 2);



                    //////////////////////////////////////////
                    /////////////////////09.5.30合并单元格
                    //for (int m = 2; m < 6; m++)  //列
                    //{
                    //    int k = 1;  //统计重复单元格
                    //    int w = hcount;  //记录起始位置
                    //    for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count-1; i++)
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
                    string path = Page.MapPath("../../../static/excel/dongtai.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "表11-边际效益井汇总表");


                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;


                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            //if (j != 0 && j != 3 && (j == 6 || j == 8 || j == 9 || j == 11) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                            //{
                            //    FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                            //}

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
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 8].Value = Math.Round(a8, 2).ToString("0.00");
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 9].Value = Math.Round(a9, 2).ToString();
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 10].Value = a10.ToString("0.00");
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 11].Value = Math.Round(a11, 2).ToString("0.00");
                        FpSpread1.Sheets[0].Cells[rcount + hcount, 12].Value = Math.Round(a12, 2).ToString("0.00");

                    }
                    FpSpread1.Sheets[0].Cells[rcount + hcount, 5].Value = "合计";
                    FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value = Math.Round(float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value.ToString()), 2);// / float.Parse(rcount.ToString()), 2); //生产时间
                    FpSpread1.Sheets[0].Cells[rcount + hcount, 7].Value = Math.Round(float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 7].Value.ToString()) / float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 6].Value.ToString()), 2).ToString("0.00");
                    FpSpread1.Sheets[0].Cells[rcount + hcount, 10].Value = Math.Round(float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 10].Value.ToString()) / float.Parse(rcount.ToString()), 2);  //含水

                    FpSpread1.Sheets[0].Cells[rcount + hcount, 13].Value = Math.Round(float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 12].Value.ToString()) / float.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount, 11].Value.ToString()), 2);


                    //////////////////////////////////////////

                    /////////////////////09.5.30合并单元格
                    //for (int m = 2; m < 6; m++)  //列
                    //{
                    //    int k = 1;  //统计重复单元格
                    //    int w = hcount;  //记录起始位置
                    //    for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count-1; i++)
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
            FarpointGridChange.FarPointChange(FpSpread1, "biao11.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;


        }

    }
}
