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

namespace beneDesCYC.view.oilAssessment.jcsjb
{
    public partial class biao3 : beneDesCYC.core.UI.corePage
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
            string path = "../../../static/excel/dongtai.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表3-油井效益评价结果表");

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
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //string list = _getParam("YCLX");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            string list = _getParam("CYC");
            string typeid = _getParam("targetType");

            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);
            string fpselect = "";
            if (typeid == "yt")
            {
                fpselect += " Select dtxyjb_info.jbmc as lb, ";

                fpselect += " (case when sum(djisopen) = 0 then 0 else sum(djisopen) end) As js, 0 as bl0,";
                fpselect += " nvl(round((case when Sum(hscyl) = 0 then 0 else Sum(hscyl)/10000 end ),4),0) As cyl, 0 as bl1,";
                fpselect += " nvl(round((case when sum(yyspl) = 0 then 0 else Sum(yyspl)/10000 end ),4),0) as yyspl, 0 as bl2,";
                fpselect += " nvl(round((case when sum(hscql) = 0 then 0 else Sum(hscql)/10000 end ),4),0) As ncq, 0 as bl3 ,";
                fpselect += " nvl(round((case when sum(trqspl) = 0 then 0 else Sum(trqspl)/10000 end ),4),0) As trqspl, 0 as bl4,";
                fpselect += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjyxczcb)/Sum(yqspl) End),2),0) As zjyxczcb,  ";
                fpselect += " nvl(round((Case When sum(dtl) = 0 Then 0 Else Sum(zjyxczcb_my)/Sum(dtl) End),2),0) As zjyxczcb_my,";
                fpselect += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(yxczcb)/Sum(yqspl) End),2),0) As yxczcb, ";
                fpselect += " nvl(round((Case When sum(dtl) = 0 Then 0 Else Sum(yxczcb_my)/Sum(dtl) End),2),0) As yxczcb_my, ";
                fpselect += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb)/Sum(yqspl) End),2),0) As czcb, ";
                fpselect += " nvl(round((Case When sum(dtl) = 0 Then 0 Else Sum(czcb_my)/Sum(dtl) End),2),0) As czcb_my, ";
                fpselect += " sdy.gsxyjb_1 as gsxyjb_1, ";
                fpselect += " nvl(round(sum(zjyxczcb),4),0) as zjyxhj,";
                fpselect += " nvl(round(sum(zjyxczcb_my),4),0) as zjyxhj_my,";
                fpselect += " nvl(round(sum(yxczcb),4),0) as yxhj,";
                fpselect += " nvl(round(sum(yxczcb_my),4),0) as yxhj_my,";
                fpselect += " nvl(round(sum(czcb),4),0) as czhj,";
                fpselect += " nvl(round(sum(czcb_my),4),0) as czhj_my,";
                fpselect += " nvl(round(sum(yqspl),4),0) as yqsplhj,";
                fpselect += " nvl(round(sum(dtl),4),0) as dtlhj";

                fpselect += " From dtxyjb_info,dtstat_djsj sdy ";
                fpselect += " where sdy.gsxyjb_1 = dtxyjb_info.jbid  and djisopen = '1' ";
                fpselect += " and sdy.cyc_id = '" + cycid + "'";
                fpselect += " Group By dtxyjb_info.jbmc ,sdy.gsxyjb_1 ";
                fpselect += " Order By sdy.gsxyjb_1 ";
            }
            else if (typeid == "pjdy")
            {
                fpselect += " Select dtxyjb_info.jbmc as lb, ";
                fpselect += " nvl(round(count(sdy.mc),4),0) As js, 0 as bl0,";
                fpselect += " nvl(round((case when Sum(cyoul) = 0 then 0 else Sum(cyoul)/10000 end ),4),0) As cyl, 0 as bl1,";
                fpselect += " nvl(round((case when sum(yyspl) = 0 then 0 else Sum(yyspl)/10000 end ),4),0) as yyspl, 0 as bl2,";
                fpselect += " nvl(round((case when sum(cql) = 0 then 0 else Sum(cql) end ),4),0) As ncq, 0 as bl3,";
                fpselect += " nvl(round((case when sum(trqspl) = 0 then 0 else Sum(trqspl)/10000 end ),4),0) As trqspl, 0 as bl4,";
                fpselect += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjyxczcb)/Sum(yqspl) End),2),0) As zjyxczcb, ";
                fpselect += " nvl(round((Case When sum(dtl) = 0 Then 0 Else Sum(zjyxczcb_my)/Sum(dtl) End),2),0) As zjyxczcb_my,";
                fpselect += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(yxczcb)/Sum(yqspl) End),2),0) As yxczcb, ";
                fpselect += " nvl(round((Case When sum(dtl) = 0 Then 0 Else Sum(yxczcb_my)/Sum(dtl) End),2),0) As yxczcb_my, ";
                fpselect += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb)/Sum(yqspl) End),2),0) As czcb, ";
                fpselect += " nvl(round((Case When sum(dtl) = 0 Then 0 Else Sum(czcb_my)/Sum(dtl) End),2),0) As czcb_my, ";
                fpselect += " sdy.gsxyjb_1 as gsxyjb_1, ";
                fpselect += " nvl(round(sum(zjyxczcb),4),0) as zjyxhj,";
                fpselect += " nvl(round(sum(zjyxczcb_my),4),0) as zjyxhj_my,";
                fpselect += " nvl(round(sum(yxczcb),4),0) as yxhj,";
                fpselect += " nvl(round(sum(yxczcb_my),4),0) as yxhj_my,";
                fpselect += " nvl(round(sum(czcb),4),0) as czhj,";
                fpselect += " nvl(round(sum(czcb_my),4),0) as czhj_my,";
                fpselect += " nvl(round(sum(yqspl),4),0) as yqsplhj,";
                fpselect += " nvl(round(sum(dtl),4),0) as dtlhj";

                fpselect += " From dtxyjb_info,dtstat_pjdysj sdy ";
                fpselect += " where sdy.gsxyjb_1 = dtxyjb_info.jbid  and sfpj = '1' ";
                fpselect += " and sdy.cyc_id = '" + cycid + "'";
                fpselect += " Group By dtxyjb_info.jbmc ,sdy.gsxyjb_1 ";
                fpselect += " Order By sdy.gsxyjb_1 ";
            }
            else if (typeid == "qk")
            {
                fpselect += " Select dtxyjb_info.jbmc as lb, ";
                fpselect += " nvl(round(count(sdy.mc),4),0) As js, 0 as bl0,";
                fpselect += " nvl(round((case when Sum(cyoul) = 0 then 0 else Sum(cyoul)/10000 end ),4),0) As cyl, 0 as bl1,";
                fpselect += " nvl(round((case when sum(yyspl) = 0 then 0 else Sum(yyspl)/10000 end ),4),0) as yyspl, 0 as bl2, ";
                fpselect += " nvl(round((case when sum(cql) = 0 then 0 else Sum(cql) end ),4),0) As ncq, 0 as bl3, ";
                fpselect += " nvl(round((case when sum(trqspl) = 0 then 0 else Sum(trqspl)/10000 end ),4),0) As trqspl, 0 as bl4,";
                fpselect += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjyxczcb)/Sum(yqspl) End),2),0) As zjyxczcb, ";
                fpselect += " nvl(round((Case When sum(dtl) = 0 Then 0 Else Sum(zjyxczcb_my)/Sum(dtl) End),2),0) As zjyxczcb_my,";
                fpselect += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(yxczcb)/Sum(yqspl) End),2),0) As yxczcb, ";
                fpselect += " nvl(round((Case When sum(dtl) = 0 Then 0 Else Sum(yxczcb_my)/Sum(dtl) End),2),0) As yxczcb_my, ";
                fpselect += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb)/Sum(yqspl) End),2),0) As czcb, ";
                fpselect += " nvl(round((Case When sum(dtl) = 0 Then 0 Else Sum(czcb_my)/Sum(dtl) End),2),0) As czcb_my, ";
                fpselect += " sdy.gsxyjb_1 as gsxyjb_1, ";
                fpselect += " nvl(round(sum(zjyxczcb),4),0) as zjyxhj,";
                fpselect += " nvl(round(sum(zjyxczcb_my),4),0) as zjyxhj_my,";
                fpselect += " nvl(round(sum(yxczcb),4),0) as yxhj,";
                fpselect += " nvl(round(sum(yxczcb_my),4),0) as yxhj_my,";
                fpselect += " nvl(round(sum(czcb),4),0) as czhj,";
                fpselect += " nvl(round(sum(czcb_my),4),0) as czhj_my,";
                fpselect += " nvl(round(sum(yqspl),4),0) as yqsplhj,";
                fpselect += " nvl(round(sum(dtl),4),0) as dtlhj";

                fpselect += " From dtxyjb_info,dtstat_qksj sdy ";
                fpselect += " where sdy.gsxyjb_1 = dtxyjb_info.jbid  and sfpj = '1' ";
                fpselect += " and sdy.cyc_id = '" + cycid + "'";
                fpselect += " Group By dtxyjb_info.jbmc , sdy.gsxyjb_1 ";
                fpselect += " Order By sdy.gsxyjb_1 ";
            }
            else if (typeid == "zyq")  //作业区禁用
            {
                #region
                //fpselect += " Select dtxyjb_info.jbmc as lb, ";
                //fpselect += " nvl(round(sum(sdy.yjkjs),4),0) As js, ";
                //fpselect += " nvl(round((case when Sum(ljcyl) = 0 then 0 else Sum(ljcyl) end ),4),0) As cyl, ";
                //fpselect += " nvl(round((case when sum(yyspl) = 0 then 0 else Sum(yyspl)/10000 end ),4),0) as yyspl,";
                //fpselect += " nvl(round((case when sum(ljcql) = 0 then 0 else Sum(ljcql)/10000 end ),4),0) As ncq, ";
                //fpselect += " nvl(round((case when sum(trqspl) = 0 then 0 else Sum(trqspl)/10000 end ),4),0) As trqspl, ";
                //fpselect += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjyxczcb)/Sum(yqspl) End),4),0) As zjyxczcb, ";
                //fpselect += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjyxczcb_my)/Sum(yqspl) End),4),0) As zjyxczcb_my,";
                //fpselect += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(yxczcb)/Sum(yqspl) End),4),0) As yxczcb, ";
                //fpselect += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(yxczcb_my)/Sum(yqspl) End),4),0) As yxczcb_my, ";
                //fpselect += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb)/Sum(yqspl) End),4),0) As czcb, ";
                //fpselect += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb_my)/Sum(yqspl) End),4),0) As czcb_my, ";
                //fpselect += " sdy.gsxyjb_1 ";
                //fpselect += " From dtxyjb_info,dtstat_qksj sdy ";
                //fpselect += " where sdy.gsxyjb_1 = dtxyjb_info.jbid  and sfpj = '1' ";
                ////fpselect += " and sdy.cyc_id = '" + cycid + "'";
                //if (list == "quan")
                //{
                //    fpselect += "";
                //}
                //else
                //{
                //    fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_name = '" + list + "') ";
                //}
                //fpselect += " Group By dtxyjb_info.jbmc , sdy.gsxyjb_1 ";
                //fpselect += " Order By sdy.gsxyjb_1 ";
                #endregion
            }
            //string fpselect = " Select distinct lb,js,cyl,yyspl,ncq,trqspl,zjyxczcb,zjyxczcb_my,yxczcb,yxczcb_my,czcb,czcb_my ";
            //fpselect += " from view_dtbiao2  dtb2 order by gsxyjb_1";        

            try
            {
                connfp.Open();

                OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
                //DataSet fpset = new DataSet();
                //da.Fill(fpset, "fpdata");
                //计算合计,  比例在绑定时计算 
                #region
                DataTable dt = new DataTable("fpdata"); //为数据表起一个名字,将来插入数据集中调用
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    DataRow dr = dt.NewRow();
                    if (typeid == "zyq")
                    {
                        #region
                        ////初始化各列
                        //for (int i = 0; i < dt.Columns.Count; i++)
                        //{
                        //    dr[i] = 0;
                        //}
                        //dr[0] = "";
                        //dr[0] = "合计";
                        ////计算
                        //for (int k = 0; k < dt.Rows.Count; k++)//循环计算各行
                        //{

                        //    for (int n = 1; n < dt.Columns.Count - 1; n++)//循环计算各列
                        //    {
                        //        dr[n] = float.Parse(dr[n].ToString()) + float.Parse(dt.Rows[k][n].ToString());
                        //    }
                        //}
                        ////计算精度
                        //for (int m = 1; m < dt.Columns.Count - 1; m++)
                        //{
                        //    dr[m] = Math.Round(float.Parse(dr[m].ToString()), 4);
                        //}
                        ////把行加入表
                        //dt.Rows.Add(dr);
                        #endregion
                    }
                    else
                    {

                        //初始化各列
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            dr[i] = 0;
                        }

                        dr[0] = "合计";
                        //计算
                        for (int k = 0; k < dt.Rows.Count; k++)//循环计算各行
                        {

                            for (int n = 1; n < dt.Columns.Count; n++)//循环计算各列
                            {
                                dr[n] = double.Parse(dr[n].ToString()) + double.Parse(dt.Rows[k][n].ToString());
                            }
                        }
                        //计算元/吨
                        dr[11] = double.Parse(dr["zjyxhj"].ToString()) / double.Parse(dr["yqsplhj"].ToString());
                        dr[12] = double.Parse(dr["zjyxhj_my"].ToString()) / double.Parse(dr["dtlhj"].ToString());
                        dr[13] = double.Parse(dr["yxhj"].ToString()) / double.Parse(dr["yqsplhj"].ToString());
                        dr[14] = double.Parse(dr["yxhj_my"].ToString()) / double.Parse(dr["dtlhj"].ToString());
                        dr[15] = double.Parse(dr["czhj"].ToString()) / double.Parse(dr["yqsplhj"].ToString());
                        dr[16] = double.Parse(dr["czhj_my"].ToString()) / double.Parse(dr["dtlhj"].ToString());
                        //计算精度
                        dr[11] = Math.Round(double.Parse(dr[11].ToString()), 2);
                        dr[12] = Math.Round(double.Parse(dr[12].ToString()), 2);
                        dr[13] = Math.Round(double.Parse(dr[13].ToString()), 2);
                        dr[14] = Math.Round(double.Parse(dr[14].ToString()), 2);
                        dr[15] = Math.Round(double.Parse(dr[15].ToString()), 2);
                        dr[16] = Math.Round(double.Parse(dr[16].ToString()), 2);
                        //把行加入表
                        dt.Rows.Add(dr);
                        for (int m = 1; m < dt.Columns.Count; m++)
                        {
                            dr[m] = Math.Round(double.Parse(dr[m].ToString()), 4);
                        }
                    }
                }
                DataSet fpset = new DataSet();
                fpset.Tables.Add(dt);
                #endregion
                //此处用于绑定数据             
                #region

                int rcount = fpset.Tables["fpdata"].Rows.Count;
                int ccount = fpset.Tables["fpdata"].Columns.Count - 9;
                int hcount = 5;


                if (FpSpread1.Sheets[0].Rows.Count == hcount)
                {
                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            if ((j == 3 || j == 5 || j == 7 || j == 9) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString() != "0")
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                            else if (j != 1 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString() != "0")
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
                            else FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }
                    //计算比例
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 1; j < 10; j += 2)
                        {
                            double hj = double.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, j].Value.ToString());
                            double blx = double.Parse(FpSpread1.Sheets[0].Cells[i + hcount, j].Value.ToString());
                            if (hj != 0)
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j + 1].Value = Math.Round(blx / hj, 4) * 100;
                            }
                            else
                                FpSpread1.Sheets[0].Cells[i + hcount, j + 1].Value = 0;
                        }
                    }
                }
                else//不为空
                {
                    string path = Page.MapPath("/static/excel/dongtai.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "表3-油井效益评价结果表");

                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;

                  //  FpSpread1.Sheets[0].Cells[1, 0].Value = tbdw;      ////////09.5.13  3-3
                    FpSpread1.Sheets[0].Cells[1, 0].Font.Size = 9;     ////////09.5.13  3-3

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                            if ((j == 3 || j == 5 || j == 7 || j == 9) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString() != "0")
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                            else if (j != 1 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString() != "0")
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
                            else FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }
                    //计算比例
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 1; j < 10; j += 2)
                        {
                            double hj = double.Parse(FpSpread1.Sheets[0].Cells[rcount + hcount - 1, j].Value.ToString());
                            double blx = double.Parse(FpSpread1.Sheets[0].Cells[i + hcount, j].Value.ToString());
                            if (hj != 0)
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j + 1].Value = Math.Round(blx / hj, 4) * 100;
                            }
                            else
                                FpSpread1.Sheets[0].Cells[i + hcount, j + 1].Value = 0;
                        }
                    }
                }
                /////////////////////////09.4.29更新//////////////////////////////////////
                if (typeid == "yt")
                {
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "单井";
                }
                else if (typeid == "pjdy")
                {
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "评价单元";
                    FpSpread1.Sheets[0].Cells[3, 1].Value = "数值(个)";
                }
                else
                {
                    FpSpread1.Sheets[0].Cells[2, 1].Value = "区块";
                    FpSpread1.Sheets[0].Cells[3, 1].Value = "数值(个)";
                }
                ///////////////////////////////////////////////////////////////
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
            FarpointGridChange.FarPointChange(FpSpread1, "dt_biao3.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;


        }

    }
}
