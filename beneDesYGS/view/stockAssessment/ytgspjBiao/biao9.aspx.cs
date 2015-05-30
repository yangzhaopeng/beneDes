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

namespace beneDesYGS.view.stockAssessment.ytgspjBiao
{
    public partial class biao9 : beneDesYGS.core.UI.corePage
    {
        static int page = 0;
        static int rcount = 0;
        static int ccount = 0;
        static int hcount = 4;
        static DataSet fpset = new DataSet();
        static bool but1click = false;
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
            //string path = "../../../static/excel/gufenbiao.xls";
            //path = Page.MapPath(path);
            //this.FpSpread1.Sheets[0].OpenExcel(path, "表9-特高成本井汇总表");

            string path = Page.MapPath("~/static/excel/gufenbiao.xls");
            this.FpSpread1.Sheets[0].OpenExcel(path, "表9-特高成本井汇总表 ");

            //string path = "../../../static/excel/gufenbiao.xls";
            //path = Page.MapPath(path);
            //this.FpSpread1.Sheets[0].OpenExcel(path, "表5-操作成本构成表");

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
            //string path = "../../../static/excel/gufenbiao.xls";
            //path = Page.MapPath(path);
            //this.FpSpread1.Sheets[0].OpenExcel(path, "表9-特高成本井汇总表");
            string path = Page.MapPath("~/static/excel/gufenbiao.xls");
            this.FpSpread1.Sheets[0].OpenExcel(path, "表9-特高成本井汇总表 ");

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
            SqlHelper conn = new SqlHelper();
            OracleConnection connfp = conn.GetConn();
            //////////////////搜出表中的特高成本井
            connfp.Open();
            string tgse = "select num from csdy_info where name = '特高成本井' and ny = '" + Session["month"].ToString() + "'";
            OracleCommand tgcom = new OracleCommand(tgse, connfp);
            OracleDataReader tgr = tgcom.ExecuteReader();
            tgr.Read();
            Int64 tgcb = Int64.Parse(tgr[0].ToString());
            connfp.Close();
            /////////////////////////////////////
            string fpselect = " ";

            fpselect += " select  distinct   sdy.jh ,sdy.qk ,djsj.tcrq ,sdy.ssyt ,sdy.scsj , ";
            fpselect += " nvl(round(sdy.hscyl,2),0) as hscyl , nvl(round(sdy.hs ,2),0) as hs, nvl(round(sdy.yqspl ,2),0) as yqspl, nvl(round(sdy.rcy ,2),0) as rcy, ";
            fpselect += " (case when nvl(sdy.yqspl,0) = 0  then 0 else round(nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl,2) end) as dyqczcb, ";
            fpselect += " '直接材料费'as feen1, nvl(sdy.zjclf,0) as fee1, ";
            fpselect += "  (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.zjclf/sdy.czcb)*100,2)  end )as feebl1, ";
            fpselect += " '直接动力费' as feen2,nvl(sdy.zjdlf,0) as fee2 , ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.zjdlf/sdy.czcb)*100,2)  end )  as feebl2, ";
            fpselect += " '驱油物注入费' as feen3, nvl(sdy.qywzrf,0) as fee3 , ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.qywzrf/sdy.czcb)*100,2)  end )  as feebl3, ";
            fpselect += " '维护性井下作业费' as feen4, nvl(sdy.whxjxzyf,0) as fee4, ";
            fpselect += "  (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.whxlf/sdy.czcb)*100,2)  end )  as feebl4, ";
            fpselect += " '油气处理费' as feen5, nvl(sdy.yqclf,0) as fee5 , ";
            fpselect += "  (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.yqclf/sdy.czcb)*100,2)  end )  as feebl5, ";
            fpselect += " sdy.gsxyjb,	sdy.bny, sdy.eny ";
            fpselect += "  from jdstat_djsj sdy ,djsj  ";
            fpselect += " where ";//sdy.jh  = djsj.jh and djsj.ny=sdy.bny and sdy.jh = kfsj.jh  and ";
            fpselect += "   djsj.dep_id = sdy.dep_id and djsj.ny <= sdy.eny and djsj.ny>=sdy.bny ";

            //fpselect += "  kfsj.ny <= sdy.eny and kfsj.ny >= sdy.bny   ";
            if (list == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            }
            fpselect += " and sdy.jh in ";
            fpselect += " (";
            fpselect += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else nvl(nvl(sdy.zjclf,0)+nvl(sdy.zjdlf,0)+nvl(sdy.qywzrf,0)+nvl(sdy.yqclf,0)+nvl(sdy.whxjxzyf,0),0)/sdy.yqspl end),0) as dwcb from jdstat_djsj sdy where  sdy.djisopen = '1'";
            if (list == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
            }
            fpselect += "  ) where dwcb > '" + tgcb + "' ";
            fpselect += " ) ";
            fpselect += " and sdy.dj_id = djsj.dj_id";
            //fpselect += " select * ";
            //fpselect += " from view_dtbiao8 dtb8 ";
            //fpselect += " where dtb8.jh in (select jh from view_dtbiao5_dwcb where dwcb > '1000') ";  //5是特高成本

            //if (Dropdl.SelectedItem.Value == "quan")
            //{
            //    fpselect += "";
            //}
            //else
            //{
            //    //fpselect += " select * ";
            //    //fpselect += " from view_dtbiao8 dtb8 ";
            //    //fpselect += " where dtb8.jh in (select jh from view_dtbiao5_dwcb where dwcb > '1000') ";  //5是特高成本
            //    fpselect += " and sdy.qk = '"+Dropdl .SelectedItem .Value +"'"; 
            //}

            try
            {
                connfp.Open();

                OracleCommand myComm = new OracleCommand(fpselect, connfp);
                OracleDataReader myReader = myComm.ExecuteReader();
                //DataTable Fptable用来输出数据
                DataTable Fptable = new DataTable("fpdata");
                Fptable.Columns.Add("xuhao", typeof(string));
                Fptable.Columns.Add("jh", typeof(string));
                //Fptable.Columns.Add("ny", typeof(string));
                Fptable.Columns.Add("qk", typeof(string));
                Fptable.Columns.Add("tcrq", typeof(string));
                Fptable.Columns.Add("ssyt", typeof(string));
                Fptable.Columns.Add("scsj", typeof(float));
                Fptable.Columns.Add("hscyl", typeof(float));
                Fptable.Columns.Add("hs", typeof(float));
                Fptable.Columns.Add("yqspl", typeof(float));
                Fptable.Columns.Add("rcy", typeof(float));

                //Fptable.Columns.Add("zjyxczcb", typeof(float));
                Fptable.Columns.Add("dyqczcb", typeof(float));

                Fptable.Columns.Add("czcb1", typeof(string));
                Fptable.Columns.Add("czcbbl1", typeof(float));

                Fptable.Columns.Add("czcb2", typeof(string));
                Fptable.Columns.Add("czcbbl2", typeof(float));

                Fptable.Columns.Add("czcb3", typeof(string));
                Fptable.Columns.Add("czcbbl3", typeof(float));

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
                    //fprow[11] = myReader[10];
                    //fprow[12] = myReader[11];


                    czcb[0, 0] = 10;// float.Parse(myReader[12].ToString());
                    czcb[0, 1] = float.Parse(myReader[11].ToString());
                    czcb[0, 2] = float.Parse(myReader[12].ToString());

                    czcb[1, 0] = 13;// float.Parse(myReader[15].ToString());
                    czcb[1, 1] = float.Parse(myReader[14].ToString());
                    czcb[1, 2] = float.Parse(myReader[15].ToString());

                    czcb[2, 0] = 16;// float.Parse(myReader[17].ToString());
                    czcb[2, 1] = float.Parse(myReader[17].ToString());
                    czcb[2, 2] = float.Parse(myReader[18].ToString());

                    czcb[3, 0] = 19;// float.Parse(myReader[17].ToString());
                    czcb[3, 1] = float.Parse(myReader[20].ToString());
                    czcb[3, 2] = float.Parse(myReader[21].ToString());

                    czcb[4, 0] = 22;// float.Parse(myReader[17].ToString());
                    czcb[4, 1] = float.Parse(myReader[23].ToString());
                    czcb[4, 2] = float.Parse(myReader[24].ToString());




                    paixu(czcb);
                    int n1, n2, n3;
                    n1 = int.Parse(czcb[0, 0].ToString());
                    n2 = int.Parse(czcb[1, 0].ToString());
                    n3 = int.Parse(czcb[2, 0].ToString());

                    fprow[11] = myReader[n1].ToString();
                    fprow[12] = czcb[0, 2];

                    fprow[13] = myReader[n2].ToString();
                    fprow[14] = czcb[1, 2];

                    fprow[15] = myReader[n3].ToString();
                    fprow[16] = czcb[2, 2];

                    Fptable.Rows.Add(fprow);

                }
                connfp.Close();
                //此处用于绑定数据             
                #region
                fpset.Tables.Clear();
                fpset.Tables.Add(Fptable);
                //if (fpset.Tables["fpdata"].Rows.Count == 0)
                //    Response.Write("<script>alert('结果为空！')</script>");

                rcount = fpset.Tables["fpdata"].Rows.Count;
                ccount = fpset.Tables["fpdata"].Columns.Count;
                hcount = 4;

                page = 0;
                Fp_DataBound(rcount, ccount, hcount, fpset, page);

                #endregion

            }
            catch (OracleException error)
            {
                string CuoWu = "错误: " + error.Message.ToString();
                Response.Write(CuoWu);
            }
        }

        protected void paixu(float[,] aa)
        {
            //aa[，]的大小写固定值，5*3
            //本函数实现5*3的二维数组排序
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
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");

            string path = Page.MapPath("~/static/excel/gufenbiao.xls");
            this.FpSpread1.Sheets[0].OpenExcel(path, "表9-特高成本井汇总表 ");

            this.FpSpread2.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
            this.FpSpread2.Sheets[0].ColumnHeader.Visible = false;
            this.FpSpread2.Sheets[0].RowHeader.Visible = false;
            if (but1click == true)
            {
                //把数据先放入FpSpread2
                int rcount = fpset.Tables["fpdata"].Rows.Count;
                int ccount = fpset.Tables["fpdata"].Columns.Count;
                int hcount = 4;

                FpSpread2.Sheets[0].AddRows(hcount, rcount);
                for (int i = 0; i < rcount; i++)
                {
                    for (int j = 0; j < ccount; j++)
                    {
                        //FpSpread2.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                        if (j != 3 && j != 5 && j != 6 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                        {
                            FpSpread2.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");
                        }
                        else
                        {
                            FpSpread2.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                        }
                        FpSpread2.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        FpSpread2.Sheets[0].Cells[i + hcount, 0].Value = i + 1;
                    }
                }
            }
              
            //导出FpSpread2中的数据
            FpSpread2.Sheets[0].RowHeader.Visible = true;
            FpSpread2.Sheets[0].ColumnHeader.Visible = true;
            //FpSpread2.SaveExcelToResponse("biao9.xls");
            FarpointGridChange.FarPointChange(FpSpread2, "biao9.xls");
            FpSpread2.Sheets[0].RowHeader.Visible = false;
            FpSpread2.Sheets[0].ColumnHeader.Visible = false;


        }

        protected void QIAN_Click(object sender, EventArgs e)
        {
            //string typeid = _getParam("targetType");

            //if (typeid == "yt")
            //{
            int.TryParse(this.currentpage.Text, out page);
            page = page - 1;
            if (page >= 0)
            {
                Fp_DataBound(rcount, ccount, hcount, fpset, page);
                this.currentpage.Text = page.ToString();
            }
            //}
        }
        protected void HOU_Click(object sender, EventArgs e)
        {
            //string typeid = _getParam("targetType");

            //if (typeid == "yt")
            //{
            
                int.TryParse(this.currentpage.Text, out page);
                if (page >= 1)
                { page = 0; }
                page = page + 1;
                Fp_DataBound(rcount, ccount, hcount, fpset, page);
                this.currentpage.Text = page.ToString();
            
            //}
        }

        protected void Fp_DataBound(int rcount, int ccount, int hcount, DataSet fpset, int page)
        {

            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");

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
                string path = Page.MapPath("~/static/excel/gufenbiao.xls");
                this.FpSpread1.Sheets[0].OpenExcel(path, "表9-特高成本井汇总表 ");

                this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                this.FpSpread1.Sheets[0].RowHeader.Visible = false;

                if (FpSpread1.Sheets[0].Rows.Count == hcount)
                    FpSpread1.Sheets[0].AddRows(hcount, count);
                for (int i = 0; i < count; i++)
                {

                    for (int j = 0; j < ccount; j++)
                    {
                        //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i + page * pagenumber][j].ToString();
                        if (j != 3 && j != 5 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i + page * pagenumber][j].ToString()) && fpset.Tables["fpdata"].Rows[i + page * pagenumber][j].ToString().Trim() != "0")
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i + page * pagenumber][j].ToString()).ToString("0.00");
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
            }
            catch
            { }
        }



    }
}
