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

namespace beneDesCYC.view.stockAssessment.gfgspjbYQ
{
    public partial class xyflhzbYQ : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string typeid = _getParam("targetType");
                initSpread(typeid);
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
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表6-单井效益分类汇总表"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    }
                    break;
                case "qk":
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表6-区块效益分类汇总表"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    }
                    break;
                case "pjdy":
                    if (this.FpSpread1.Sheets[0].OpenExcel(path, "表6-区块效益分类汇总表"))
                    {
                        this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    }
                    break;
                default:
                    break;
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
        }

        protected void initSpread2()
        {
            string path = "../../../static/excel/jdnianduYQ.xls";
            path = Page.MapPath(path);
            if (this.FpSpread1.Sheets[0].OpenExcel(path, "表6-区块效益分类汇总表"))
            {
                this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
            }

        }

        protected void sj(string typeid)
        {
            //string cycid = Session["cyc_id"].ToString();
            //JObject obj = new JObject();
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            //string list = _getParam("CYC");
            //string typeid = _getParam("targetType");

            DataSet ds = new DataSet();
            var param_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc.Value = Session["cyc_id"].ToString();
            var param_cqc = new OracleParameter("as_cqc", OracleType.VarChar);
            param_cqc.Value = Session["cqc_id"].ToString();
            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;


            try
            {
                if (typeid == "dj")
                {
                    ds = sql.GetDataSet("OWCBS_VIEW_XYFLHZB.XYFLHZB_DJ_PROC", CommandType.StoredProcedure,
                       new OracleParameter[] { param_cyc, param_cqc, param_out });
                }
                else if (typeid == "qk")
                {
                    ds = sql.GetDataSet("OWCBS_VIEW_XYFLHZB.XYFLHZB_QK_PROC", CommandType.StoredProcedure,
                             new OracleParameter[] { param_cyc, param_cqc, param_out });
                }
                else if (typeid == "pjdy")
                {
                    ds = sql.GetDataSet("OWCBS_VIEW_XYFLHZB.XYFLHZB_PJDY_PROC", CommandType.StoredProcedure,
                new OracleParameter[] { param_cyc, param_cqc, param_out });
                }

            }
            catch (Exception ex)
            {

            }
            int rcount = ds.Tables[0].Rows.Count;//效益类别最多为5
            int ccount = ds.Tables[0].Columns.Count;
            int hcount = 4;
            if (FpSpread1.Sheets[0].Rows.Count == hcount)
            {
                FpSpread1.Sheets[0].AddRows(hcount, rcount);
                for (int i = 0; i < rcount; i++)
                {
                    for (int j = 0; j < ccount - 1; j++)
                    {
                        string value = ds.Tables[0].Rows[i][j].ToString();
                        if (string.IsNullOrEmpty(value))
                            value = "0";
                        FpSpread1.Sheets[0].Cells[i + hcount, j].Value = value;
                    }
                }
            }
            //计算比例
            if (typeid == "dj")
                SetFormulas();
            else
                SetQKFormulas();
            FpSpread1.Width = Unit.Pixel(1033);
            #region 旧代码

            ////string cycid = Session["cyc"].ToString();
            //OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);
            ////  Int64 ny = (Convert.ToInt64(eny)) - (Convert.ToInt64(bny)) + 1;

            ////油井
            //string fpselect = " Select xy.xymc ,(case when sum(djisopen) = 0 then 0 else sum(djisopen) end) As js,0 as bl1,round(Sum(hscql) / 10000,4) As cyl ,0 as bl2,sdy.gsxyjb";
            //fpselect += " From jdstat_djsj sdy,gsxylb_info xy ";
            //fpselect += " where sdy.gsxyjb <> 99  and sdy.pjdyxyjb <> 99 and sdy.gsxyjb = xy.xyjb and djisopen = '1'";
            //fpselect += " and sdy.cyc_id = '" + cycid + "'";

            //fpselect += " Group By xy.xymc,sdy.gsxyjb  ";
            //fpselect += " order by sdy.gsxyjb ";
            ////区块
            //string fpselect2 = " Select xy.xymc ,count(mc) As js,0 as bl1,nvl(round(Sum(cql),4),0) As cyl ,0 as bl2,sdy.gsxyjb";
            //if (typeid == "qk")
            //{
            //    fpselect2 += " From jdstat_qksj sdy,gsxylb_info xy ";

            //}
            //else
            //{
            //    fpselect2 += " From jdstat_pjdysj sdy,gsxylb_info xy ";

            //}
            //fpselect2 += " where sdy.sfpj= '1' and sdy.gsxyjb = xy.xyjb ";
            //fpselect2 += " and sdy.cyc_id = '" + cycid + "'";

            //fpselect2 += " Group By xy.xymc,sdy.gsxyjb  ";
            //fpselect2 += " order by sdy.gsxyjb ";




            //try
            //{
            //    connfp.Open();
            //    OracleDataAdapter da = new OracleDataAdapter(fpselect, connfp);
            //    OracleDataAdapter da2 = new OracleDataAdapter(fpselect2, connfp);
            //    #region
            //    //OracleCommand myComm = new OracleCommand(fpselect, connfp);
            //    //OracleDataReader myReader = myComm.ExecuteReader();
            //    ////DataTable Fptable用来输出数据
            //    //DataTable Fptable = new DataTable("fpdata");
            //    //Fptable.Columns.Add("xymc", typeof(string));
            //    //Fptable.Columns.Add("js", typeof(float));
            //    //Fptable.Columns.Add("bl1", typeof(float));
            //    //Fptable.Columns.Add("cyl", typeof(float));
            //    //Fptable.Columns.Add("bl2", typeof(float));

            //    //float blh1 = 0, blh2 = 0, hj1 = 0, hj2 = 0;
            //    //DataRow Fprow;
            //    //while (myReader.Read())
            //    //{
            //    //    Fprow = Fptable.NewRow();

            //    //    Fprow[0] = myReader[0];
            //    //    Fprow[1] = myReader[1];
            //    //    hj1 += float.Parse(Fprow[1].ToString());
            //    //    Fprow[3] = myReader.GetValue(2);
            //    //    hj2 += float.Parse(Fprow[3].ToString());
            //    //    Fptable.Rows.Add(Fprow);
            //    //}
            //    ////计算各列比例
            //    //for (int i = 0; i < Fptable.Rows.Count; i++)
            //    //{
            //    //    if (hj1 != 0)
            //    //    {
            //    //        Fptable.Rows[i][2] = Math.Round(float.Parse(Fptable.Rows[i][1].ToString()) / hj1 * 100, 2);
            //    //    }
            //    //    else
            //    //    {
            //    //        Fptable.Rows[i][2] = 0;
            //    //    }
            //    //    ////////////////
            //    //    if (hj2 != 0)
            //    //    {
            //    //        Fptable.Rows[i][4] = Math.Round(float.Parse(Fptable.Rows[i][3].ToString()) / hj2 * 100, 2);
            //    //    }
            //    //    else
            //    //    {
            //    //        Fptable.Rows[i][4] = 0;
            //    //    }
            //    //}
            //    ////添加合计行
            //    //DataRow dr = Fptable.NewRow();
            //    //dr[0] = "合计";

            //    //dr[1] = hj1;
            //    //dr[2] = 100;
            //    //dr[3] = hj2;
            //    //dr[4] = 100;
            //    //Fptable.Rows.Add(dr);
            //    //myComm.Clone();

            //    //DataSet fpset = new DataSet();
            //    //fpset.Tables.Add(Fptable);
            //    #endregion
            //    //合计
            //    DataTable dt = new DataTable("fpdata");
            //    da.Fill(dt);

            //    DataTable dt2 = new DataTable("fpdata2");
            //    da2.Fill(dt2);
            //    if (dt.Rows.Count > 0)
            //    {
            //        DataRow dr = dt.NewRow();

            //        //初始化各列
            //        for (int i = 0; i < dt.Columns.Count; i++)
            //        {
            //            dr[i] = 0;
            //        }
            //        dr[0] = "合计";

            //        //计算
            //        for (int k = 0; k < dt.Rows.Count; k++)//循环计算各行
            //        {

            //            for (int n = 1; n < dt.Columns.Count - 1; n++)//循环计算各列
            //            {
            //                if (dt.Rows[k][n].ToString() != "")
            //                {
            //                    dr[n] = float.Parse(dr[n].ToString()) + float.Parse(dt.Rows[k][n].ToString());
            //                }
            //                else
            //                    dr[n] = float.Parse(dr[n].ToString()) + 0;
            //            }
            //        }

            //        //计算精度
            //        for (int m = 1; m < dt.Columns.Count; m++)
            //        {
            //            dr[m] = Math.Round(float.Parse(dr[m].ToString()), 4);
            //        }
            //        //把行加入表
            //        dt.Rows.Add(dr);
            //    }
            //    //区块
            //    if (dt2.Rows.Count > 0)
            //    {
            //        DataRow dr = dt2.NewRow();

            //        //初始化各列
            //        for (int i = 0; i < dt2.Columns.Count; i++)
            //        {
            //            dr[i] = 0;
            //        }
            //        dr[0] = "合计";

            //        //计算
            //        for (int k = 0; k < dt2.Rows.Count; k++)//循环计算各行
            //        {

            //            for (int n = 1; n < dt2.Columns.Count - 1; n++)//循环计算各列
            //            {
            //                if (dt2.Rows[k][n].ToString() != "")
            //                {
            //                    dr[n] = float.Parse(dr[n].ToString()) + float.Parse(dt2.Rows[k][n].ToString());
            //                }
            //                else
            //                    dr[n] = float.Parse(dr[n].ToString()) + 0;
            //            }
            //        }
            //        //计算精度
            //        for (int m = 1; m < dt2.Columns.Count; m++)
            //        {
            //            dr[m] = Math.Round(float.Parse(dr[m].ToString()), 4);
            //        }
            //        //把行加入表
            //        dt2.Rows.Add(dr);
            //    }

            //    DataSet fpset = new DataSet();
            //    fpset.Tables.Add(dt);
            //    fpset.Tables.Add(dt2);
            //    //此处用于绑定数据
            //    #region
            //    int rcount = fpset.Tables["fpdata"].Rows.Count;
            //    int ccount = fpset.Tables["fpdata"].Columns.Count;
            //    int hcount = 4;
            //    int rcount2 = fpset.Tables["fpdata2"].Rows.Count;
            //    int ccount2 = fpset.Tables["fpdata2"].Columns.Count;

            //    //FpSpread1.ColumnHeader.Visible = false;
            //    //FpSpread1.RowHeader.Visible = false;
            //    if (FpSpread1.Sheets[0].Rows.Count == hcount)
            //    {
            //        #region MyRegion


            //        //FpSpread1.Sheets[0].AddRows(hcount, rcount);
            //        //for (int i = 0; i < rcount; i++)
            //        //{
            //        //    for (int j = 1; j < ccount-1; j++)
            //        //    {
            //        //        if (fpset.Tables["fpdata"].Rows[i][ccount-1].ToString () == "1")
            //        //        {
            //        //            FpSpread1.Sheets[0].Cells[0 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
            //        //        }
            //        //        else if (fpset.Tables["fpdata"].Rows[i][ccount-1].ToString() == "2")
            //        //        {
            //        //            FpSpread1.Sheets[0].Cells[1 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
            //        //        }
            //        //        else if (fpset.Tables["fpdata"].Rows[i][ccount-1].ToString() == "3")
            //        //        {
            //        //            FpSpread1.Sheets[0].Cells[2 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
            //        //        }
            //        //        else if (fpset.Tables["fpdata"].Rows[i][ccount-1].ToString() == "4")
            //        //        {
            //        //            FpSpread1.Sheets[0].Cells[3 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
            //        //        }
            //        //        else if (fpset.Tables["fpdata"].Rows[i][ccount-1].ToString() == "0")
            //        //        {
            //        //            FpSpread1.Sheets[0].Cells[4 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
            //        //        }
            //        //    }
            //        //}
            //        ////区块
            //        //for (int i = 0; i < rcount2; i++)
            //        //{
            //        //    for (int j = 1 + ccount; j < ccount + ccount2 - 1; j++)
            //        //    {
            //        //        if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "1")
            //        //        {
            //        //            FpSpread1.Sheets[0].Cells[0 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
            //        //        }
            //        //        else if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "2")
            //        //        {
            //        //            FpSpread1.Sheets[0].Cells[1 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
            //        //        }
            //        //        else if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "3")
            //        //        {
            //        //            FpSpread1.Sheets[0].Cells[2 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
            //        //        }
            //        //        else if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "4")
            //        //        {
            //        //            FpSpread1.Sheets[0].Cells[3 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
            //        //        }
            //        //        else if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "0")
            //        //        {
            //        //            FpSpread1.Sheets[0].Cells[4 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
            //        //        }
            //        //    }
            //        //}
            //        #endregion
            //    }
            //    else//不为空
            //    {
            //        string path = "../../../static/excel/jdnianduYQ.xls";
            //        path = Page.MapPath(path);
            //        this.FpSpread1.Sheets[0].OpenExcel(path, "表6-单井效益分类汇总表");

            //        this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
            //        if (typeid == "qk")
            //            FpSpread1.Sheets[0].Cells[2, 6].Text = "气田（区块）";
            //        else
            //            FpSpread1.Sheets[0].Cells[2, 6].Text = "气田（评价单元）";
            //        this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
            //        this.FpSpread1.Sheets[0].RowHeader.Visible = false;



            //        //FpSpread1.Sheets[0].AddRows(hcount, rcount);
            //        for (int i = 0; i < rcount; i++)
            //        {
            //            for (int j = 1; j < ccount - 1; j++)
            //            {
            //                if (fpset.Tables["fpdata"].Rows[i][ccount - 1].ToString() == "1")
            //                {
            //                    FpSpread1.Sheets[0].Cells[0 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
            //                }
            //                else if (fpset.Tables["fpdata"].Rows[i][ccount - 1].ToString() == "2")
            //                {
            //                    FpSpread1.Sheets[0].Cells[1 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
            //                }
            //                else if (fpset.Tables["fpdata"].Rows[i][ccount - 1].ToString() == "3")
            //                {
            //                    FpSpread1.Sheets[0].Cells[2 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
            //                }
            //                else if (fpset.Tables["fpdata"].Rows[i][ccount - 1].ToString() == "4")
            //                {
            //                    FpSpread1.Sheets[0].Cells[3 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
            //                }
            //                else if (fpset.Tables["fpdata"].Rows[i][ccount - 1].ToString() == "5")
            //                {
            //                    FpSpread1.Sheets[0].Cells[4 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
            //                }
            //                else if (fpset.Tables["fpdata"].Rows[i][ccount - 1].ToString() == "0" && i == (rcount - 1) && j == 3)
            //                {
            //                    FpSpread1.Sheets[0].Cells[5 + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
            //                }
            //                else if (fpset.Tables["fpdata"].Rows[i][ccount - 1].ToString() == "0")
            //                {
            //                    FpSpread1.Sheets[0].Cells[5 + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
            //                }
            //            }
            //        }

            //        //区块
            //        for (int i = 0; i < rcount2; i++)
            //        {
            //            for (int j = 1 + ccount; j < ccount + ccount2 - 1; j++)
            //            {
            //                if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "1")
            //                {
            //                    FpSpread1.Sheets[0].Cells[0 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
            //                }
            //                else if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "2")
            //                {
            //                    FpSpread1.Sheets[0].Cells[1 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
            //                }
            //                else if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "3")
            //                {
            //                    FpSpread1.Sheets[0].Cells[2 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
            //                }
            //                else if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "4")
            //                {
            //                    FpSpread1.Sheets[0].Cells[3 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
            //                }
            //                else if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "5")
            //                {
            //                    FpSpread1.Sheets[0].Cells[4 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
            //                }
            //                else if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "0" && i == (rcount2 - 1) && j == 8)
            //                {
            //                    FpSpread1.Sheets[0].Cells[5 + hcount, j - 1].Value = double.Parse(fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString()).ToString("0.0000");
            //                }
            //                else if (fpset.Tables["fpdata2"].Rows[i][ccount2 - 1].ToString() == "0")
            //                {
            //                    FpSpread1.Sheets[0].Cells[5 + hcount, j - 1].Value = fpset.Tables["fpdata2"].Rows[i][j - ccount].ToString();
            //                }
            //            }
            //        }
            //        //计算比例
            //        SetFormulas();


            //        //合并单元格
            //        //int k = 1;  //统计重复单元格
            //        //int w = hcount;  //记录起始位置
            //        //for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
            //        //{
            //        //    if (FpSpread1.Sheets[0].Cells[i, 0].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, 0].Value.ToString())
            //        //    {
            //        //        k++;
            //        //    }
            //        //    else
            //        //    {
            //        //        FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
            //        //        w = i;
            //        //        k = 1;
            //        //    }
            //        //}
            //        //FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
            //    }
            //    #endregion
            //}
            //catch (OracleException error)
            //{
            //    string CuoWu = "错误: " + error.Message.ToString();
            //    Response.Write(CuoWu);

            //}
            //connfp.Close();
            #endregion

        }

        public void SetQKFormulas()
        {
            FpSpread1.ActiveSheetView.Cells[4, 2].Formula = "IF(B5+B6+B7+B8+B9=0,0,ROUND(B5/(B5+B6+B7+B8+B9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[5, 2].Formula = "IF(B5+B6+B7+B8+B9=0,0,ROUND(B6/(B5+B6+B7+B8+B9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[6, 2].Formula = "IF(B5+B6+B7+B8+B9=0,0,ROUND(B7/(B5+B6+B7+B8+B9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[7, 2].Formula = "IF(B5+B6+B7+B8+B9=0,0,ROUND(B8/(B5+B6+B7+B8+B9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[8, 2].Formula = "IF(B5+B6+B7+B8+B9=0,0,ROUND(B9/(B5+B6+B7+B8+B9)*100,2))";

            FpSpread1.ActiveSheetView.Cells[4, 4].Formula = "IF(D5+D6+D7+D8+D9=0,0,ROUND(D5/(D5+D6+D7+D8+D9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[5, 4].Formula = "IF(D5+D6+D7+D8+D9=0,0,ROUND(D6/(D5+D6+D7+D8+D9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[6, 4].Formula = "IF(D5+D6+D7+D8+D9=0,0,ROUND(D7/(D5+D6+D7+D8+D9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[7, 4].Formula = "IF(D5+D6+D7+D8+D9=0,0,ROUND(D8/(D5+D6+D7+D8+D9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[8, 4].Formula = "IF(D5+D6+D7+D8+D9=0,0,ROUND(D9/(D5+D6+D7+D8+D9)*100,2))";


            FpSpread1.ActiveSheetView.Cells[4, 6].Formula = "IF(F5+F6+F7+F8+F9=0,0,ROUND(F5/(F5+F6+F7+F8+F9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[5, 6].Formula = "IF(F5+F6+F7+F8+F9=0,0,ROUND(F6/(F5+F6+F7+F8+F9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[6, 6].Formula = "IF(F5+F6+F7+F8+F9=0,0,ROUND(F7/(F5+F6+F7+F8+F9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[7, 6].Formula = "IF(F5+F6+F7+F8+F9=0,0,ROUND(F8/(F5+F6+F7+F8+F9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[8, 6].Formula = "IF(F5+F6+F7+F8+F9=0,0,ROUND(F9/(F5+F6+F7+F8+F9)*100,2))";

            FpSpread1.ActiveSheetView.Cells[4, 8].Formula = "IF(H5+H6+H7+H8+H9=0,0,ROUND(H5/(H5+H6+H7+H8+H9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[5, 8].Formula = "IF(H5+H6+H7+H8+H9=0,0,ROUND(H6/(H5+H6+H7+H8+H9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[6, 8].Formula = "IF(H5+H6+H7+H8+H9=0,0,ROUND(H7/(H5+H6+H7+H8+H9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[7, 8].Formula = "IF(H5+H6+H7+H8+H9=0,0,ROUND(H8/(H5+H6+H7+H8+H9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[8, 8].Formula = "IF(H5+H6+H7+H8+H9=0,0,ROUND(H9/(H5+H6+H7+H8+H9)*100,2))";

            FpSpread1.ActiveSheetView.Cells[4, 10].Formula = "IF(J5+J6+J7+J8+J9=0,0,ROUND(J5/(J5+J6+J7+J8+J9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[5, 10].Formula = "IF(J5+J6+J7+J8+J9=0,0,ROUND(J6/(J5+J6+J7+J8+J9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[6, 10].Formula = "IF(J5+J6+J7+J8+J9=0,0,ROUND(J7/(J5+J6+J7+J8+J9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[7, 10].Formula = "IF(J5+J6+J7+J8+J9=0,0,ROUND(J8/(J5+J6+J7+J8+J9)*100,2))";

        }

        public void SetFormulas()
        {
            //Set up the formulas
            ////Set up celltypes for formula cells
            //FpSpread1.ActiveSheetView.Columns[1].CellType = new CurrencyCellType();
            //FpSpread1.ActiveSheetView.Cells[4, 2, 4, 5].CellType = new CurrencyCellType();

            ////Set row formulas
            //FpSpread1.ActiveSheetView.Cells[8, 1].Formula = "SUM(B5:B8)";
            ////FpSpread1.ActiveSheetView.Cells[8, 2].Formula = "SUM(C5:C8)";
            //FpSpread1.ActiveSheetView.Cells[8, 3].Formula = "SUM(D5:D8)";
            ////FpSpread1.ActiveSheetView.Cells[8, 4].Formula = "SUM(E5:E8)";

            ////Set column formulas
            //FpSpread1.ActiveSheetView.Cells[8, 6].Formula = "SUM(G5:G8)";
            ////FpSpread1.ActiveSheetView.Cells[8, 7].Formula = "SUM(H5:H8)";
            //FpSpread1.ActiveSheetView.Cells[8, 8].Formula = "SUM(I5:I8)";
            ////FpSpread1.ActiveSheetView.Cells[8, 9].Formula = "SUM(J5:J8)";



            FpSpread1.ActiveSheetView.Cells[4, 2].Formula = "IF(B5+B6+B7+B8+B9=0,0,ROUND(B5/(B5+B6+B7+B8+B9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[5, 2].Formula = "IF(B5+B6+B7+B8+B9=0,0,ROUND(B6/(B5+B6+B7+B8+B9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[6, 2].Formula = "IF(B5+B6+B7+B8+B9=0,0,ROUND(B7/(B5+B6+B7+B8+B9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[7, 2].Formula = "IF(B5+B6+B7+B8+B9=0,0,ROUND(B8/(B5+B6+B7+B8+B9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[8, 2].Formula = "IF(B5+B6+B7+B8+B9=0,0,ROUND(B9/(B5+B6+B7+B8+B9)*100,2))";


            FpSpread1.ActiveSheetView.Cells[4, 4].Formula = "IF(B5=0,0,ROUND(D5/B5*100,2))";
            FpSpread1.ActiveSheetView.Cells[5, 4].Formula = "IF(B6=0,0,ROUND(D6/B6*100,2))";
            FpSpread1.ActiveSheetView.Cells[6, 4].Formula = "IF(B7=0,0,ROUND(D7/B7*100,2))";
            FpSpread1.ActiveSheetView.Cells[7, 4].Formula = "IF(B8=0,0,ROUND(D8/B8*100,2))";
            FpSpread1.ActiveSheetView.Cells[8, 4].Formula = "IF(B9=0,0,ROUND(D9/B9*100,2))";

            FpSpread1.ActiveSheetView.Cells[4, 6].Formula = "IF(B5=0,0,ROUND(F5/B5*100,2))";
            FpSpread1.ActiveSheetView.Cells[5, 6].Formula = "IF(B6=0,0,ROUND(F6/B6*100,2))";
            FpSpread1.ActiveSheetView.Cells[6, 6].Formula = "IF(B7=0,0,ROUND(F7/B7*100,2))";
            FpSpread1.ActiveSheetView.Cells[7, 6].Formula = "IF(B8=0,0,ROUND(F8/B8*100,2))";
            FpSpread1.ActiveSheetView.Cells[8, 6].Formula = "IF(B9=0,0,ROUND(F9/B9*100,2))";

            FpSpread1.ActiveSheetView.Cells[4, 8].Formula = "IF(H5+H6+H7+H8+H9=0,0,ROUND(H5/(H5+H6+H7+H8+H9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[5, 8].Formula = "IF(H5+H6+H7+H8+H9=0,0,ROUND(H6/(H5+H6+H7+H8+H9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[6, 8].Formula = "IF(H5+H6+H7+H8+H9=0,0,ROUND(H7/(H5+H6+H7+H8+H9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[7, 8].Formula = "IF(H5+H6+H7+H8+H9=0,0,ROUND(H8/(H5+H6+H7+H8+H9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[8, 8].Formula = "IF(H5+H6+H7+H8+H9=0,0,ROUND(H9/(H5+H6+H7+H8+H9)*100,2))";


            FpSpread1.ActiveSheetView.Cells[4, 10].Formula = "IF(J5+J6+J7+J8+J9=0,0,ROUND(J5/(J5+J6+J7+J8+J9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[5, 10].Formula = "IF(J5+J6+J7+J8+J9=0,0,ROUND(J6/(J5+J6+J7+J8+J9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[6, 10].Formula = "IF(J5+J6+J7+J8+J9=0,0,ROUND(J7/(J5+J6+J7+J8+J9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[7, 10].Formula = "IF(J5+J6+J7+J8+J9=0,0,ROUND(J8/(J5+J6+J7+J8+J9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[8, 10].Formula = "IF(J5+J6+J7+J8+J9=0,0,ROUND(J9/(J5+J6+J7+J8+J9)*100,2))";

            FpSpread1.ActiveSheetView.Cells[4, 12].Formula = "IF(L5+L6+L7+L8+L9=0,0,ROUND(L5/(L5+L6+L7+L8+L9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[5, 12].Formula = "IF(L5+L6+L7+L8+L9=0,0,ROUND(L6/(L5+L6+L7+L8+L9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[6, 12].Formula = "IF(L5+L6+L7+L8+L9=0,0,ROUND(L7/(L5+L6+L7+L8+L9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[7, 12].Formula = "IF(L5+L6+L7+L8+L9=0,0,ROUND(L8/(L5+L6+L7+L8+L9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[8, 12].Formula = "IF(L5+L6+L7+L8+L9=0,0,ROUND(L9/(L5+L6+L7+L8+L9)*100,2))";

            FpSpread1.ActiveSheetView.Cells[4, 14].Formula = "IF(N5+N6+N7+N8+N9=0,0,ROUND(N5/(N5+N6+N7+N8+N9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[5, 14].Formula = "IF(N5+N6+N7+N8+N9=0,0,ROUND(N6/(N5+N6+N7+N8+N9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[6, 14].Formula = "IF(N5+N6+N7+N8+N9=0,0,ROUND(N7/(N5+N6+N7+N8+N9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[7, 14].Formula = "IF(N5+N6+N7+N8+N9=0,0,ROUND(N8/(N5+N6+N7+N8+N9)*100,2))";
            FpSpread1.ActiveSheetView.Cells[8, 14].Formula = "IF(N5+N6+N7+N8+N9=0,0,ROUND(N9/(N5+N6+N7+N8+N9)*100,2))";

            //FpSpread1.ActiveSheetView.Cells[5, 4].Formula = "IF(B5+B6+B7+B8+B9=0,0,B6/(B5+B6+B7+B8+B9)*100)";
            //FpSpread1.ActiveSheetView.Cells[6, 4].Formula = "IF(B5+B6+B7+B8+B9=0,0,B7/(B5+B6+B7+B8+B9)*100)";
            //FpSpread1.ActiveSheetView.Cells[7, 4].Formula = "IF(B5+B6+B7+B8+B9=0,0,B8/(B5+B6+B7+B8+B9)*100)";
            //FpSpread1.ActiveSheetView.Cells[8, 4].Formula = "IF(B5+B6+B7+B8+B9=0,0,B9/(B5+B6+B7+B8+B9)*100)";
            //FpSpread1.ActiveSheetView.Cells[9, 4].Formula = "IF(B5+B6+B7+B8+B9=0,0,B9/(B5+B6+B7+B8+B9)*100)";

            //FpSpread1.ActiveSheetView.Cells[5, 2].Formula = "IF(B6=0,0,(B6/B10)*100)"; //"(B6/B10)*100";
            //FpSpread1.ActiveSheetView.Cells[6, 2].Formula = "IF(B7=0,0,(B7/B10)*100)"; //"(B7/B10)*100";
            //FpSpread1.ActiveSheetView.Cells[7, 2].Formula = "IF(B8=0,0,(B8/B10)*100)"; //"(B8/B10)*100";
            //FpSpread1.ActiveSheetView.Cells[8, 2].Formula = "IF(B9=0,0,(B9/B10)*100)"; //""(B9/B10)*100";
            //FpSpread1.ActiveSheetView.Cells[9, 2].Formula = "IF(B10=0,0,(B10/B10)*100)"; //""(B10/B10)*100";

            //FpSpread1.ActiveSheetView.Cells[4, 4].Formula = "IF(D5=0,0,(D5/D10)*100)";   // "(D5/D10)*100";
            //FpSpread1.ActiveSheetView.Cells[5, 4].Formula = "IF(D6=0,0,(D6/D10)*100)";   // "(D6/D10)*100";
            //FpSpread1.ActiveSheetView.Cells[6, 4].Formula = "IF(D7=0,0,(D7/D10)*100)";   //  "(D7/D10)*100";
            //FpSpread1.ActiveSheetView.Cells[7, 4].Formula = "IF(D8=0,0,(D8/D10)*100)";   // "(D8/D10)*100";
            //FpSpread1.ActiveSheetView.Cells[8, 4].Formula = "IF(D9=0,0,(D9/D10)*100)";   // "(D9/D10)*100";
            //FpSpread1.ActiveSheetView.Cells[9, 4].Formula = "IF(D10=0,0,(D10/D10)*100)";// "(D10/D10)*100";

            //FpSpread1.ActiveSheetView.Cells[4, 7].Formula = "IF(G5=0,0,(G5/G10)*100)";   // "(G5/G10)*100";
            //FpSpread1.ActiveSheetView.Cells[5, 7].Formula = "IF(G6=0,0,(G6/G10)*100)";   // "(G6/G10)*100";
            //FpSpread1.ActiveSheetView.Cells[6, 7].Formula = "IF(G7=0,0,(G7/G10)*100)";   // "(G7/G10)*100";
            //FpSpread1.ActiveSheetView.Cells[7, 7].Formula = "IF(G8=0,0,(G8/G10)*100)";   // "(G8/G10)*100";
            //FpSpread1.ActiveSheetView.Cells[8, 7].Formula = "IF(G9=0,0,(G9/G10)*100)";   // "(G9/G10)*100";
            //FpSpread1.ActiveSheetView.Cells[9, 7].Formula = "IF(G10=0,0,(G10/G10)*100)";// "(G10/G10)*100";

            //FpSpread1.ActiveSheetView.Cells[4, 9].Formula = "IF(I5=0,0,(I5/I10)*100)";   //"(I5/I10)*100";
            //FpSpread1.ActiveSheetView.Cells[5, 9].Formula = "IF(I6=0,0,(I6/I10)*100)";   //"(I6/I10)*100";
            //FpSpread1.ActiveSheetView.Cells[6, 9].Formula = "IF(I7=0,0,(I7/I10)*100)";   //"(I7/I10)*100";
            //FpSpread1.ActiveSheetView.Cells[7, 9].Formula = "IF(I8=0,0,(I8/I10)*100)";   //"(I8/I10)*100";
            //FpSpread1.ActiveSheetView.Cells[8, 9].Formula = "IF(I9=0,0,(I9/I10)*100)";   //"(I9/I10)*100";
            //FpSpread1.ActiveSheetView.Cells[9, 9].Formula = "IF(I10=0,0,(I10/I10)*100)";//"(I10/I10)*100";


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
            //FpSpread1.SaveExcelToResponse("jdnd_biao6.xls");
            FarpointGridChange.FarPointChange(FpSpread1, "jdnd_biao6.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;
        }

        //protected void JS_Click(object sender, EventArgs e)
        //{
        //initSpread();
        //}

    }
}
