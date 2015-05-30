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

namespace beneDesCYC.view.query
{
    public partial class bjwxy : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string jh = _getParam("JH");
            //if (jh == "null")
            if (string.IsNullOrEmpty(jh))
            { initSpread2(); }
            else
            { initSpread(); }
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }

        protected void initSpread()
        {

            string path = "/static/excel/bjwxygzfx.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "边际效益无效益井跟踪分析表");

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
            string path = "/static/excel/bjwxygzfx.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "边际效益无效益井跟踪分析表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
        }

        protected void paixu(float[,] aa)
        {
            //aa[，]的大小写固定值，12*3
            //本函数实现12*3的二维数组排序
            float tem0, tem1, tem2;
            for (int i = 0; i < 12; i++)
            {
                for (int j = i; j < 12; j++)
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
            string cycid = Session["cyc_id"].ToString();
           // string cyc = _getParam("CYC");
            string jh1 = _getParam("JH");
            string jh = jh1.Replace(" ", "+");
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //string cyc = Session["cyc"].ToString();
            OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);

            string fpselect = "";
            fpselect += " select round((case when nvl(sdy.scsj,0)=0 then 0 else nvl(sdy.jkcyl,0)/sdy.scsj end),4) as rcye  , ";
            fpselect += " round((case when nvl(sdy.scsj,0)=0 then 0 else nvl(sdy.jkcyoul,0)/sdy.scsj end),4) as rcyou , ";
            fpselect += " round( nvl(sdy.hs,0) ,4) as hs , ";
            fpselect += " round( nvl(sdy.yqspl,0),4) as spl , ";
            fpselect += " round( nvl(sdy.czcb,0),4) as czcb , ";

            fpselect += " '直接材料费'as feen1, ";
            fpselect += " nvl(sdy.zjclf,0) as fee1, ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.zjclf/sdy.czcb)*100,4)  end )as feebl1, ";

            fpselect += "  '直接燃料费' as feen2, ";
            fpselect += "  nvl(sdy.zjrlf,0) as fee2, ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.zjrlf/sdy.czcb)*100,4)  end )  as feebl2, ";

            fpselect += " '直接动力费' as feen3, ";
            fpselect += " nvl(sdy.zjdlf,0) as fee3 , ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.zjdlf/sdy.czcb)*100,4)  end )  as feebl3, ";

            fpselect += " '驱油物注入费' as feen4, ";
            fpselect += " nvl(sdy.qywzrf,0) as fee4 , ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round(((sdy.qywzrf-sdy.qywzrf_ryf)/sdy.czcb)*100,4)  end )  as feebl4, ";

            fpselect += "  '井下作业费' as feen5, ";
            fpselect += " nvl(sdy.jxzyf,0) as fee5 , ";
            fpselect += "  (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.jxzyf/sdy.czcb)*100,4)  end )  as feebl5, ";

            fpselect += "  '测井试井费' as feen6, ";
            fpselect += " nvl(sdy.cjsjf,0) as fee6, ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.cjsjf/sdy.czcb)*100,4)  end )  as feebl6, ";

            fpselect += " '维护及修理费' as feen7, ";
            fpselect += "  nvl(sdy.whxlf,0) as fee7, ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.whxlf/sdy.czcb)*100,4)  end )  as feebl7, ";

            fpselect += " '油气处理费' as feen8, ";
            fpselect += " nvl(sdy.yqclf,0) as fee8 , ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round(((sdy.yqclf-sdy.yqclf_ryf)/sdy.czcb)*100,4)  end )  as feebl8, ";

            fpselect += " '运输费' as feen9, ";
            fpselect += " nvl(sdy.ysf,0) as fee9 , ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.ysf/sdy.czcb)*100,4)  end )  as feebl9, ";

            fpselect += "  '其他直接费' as feen10, ";
            fpselect += " nvl(sdy.qtzjf,0) as fee10 , ";
            fpselect += "  (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.qtzjf/sdy.czcb)*100,4)  end )  as feebl10, ";

            fpselect += "  '厂矿管理费' as feen11, ";
            fpselect += "  nvl(sdy.ckglf,0) as fee11 , ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.ckglf/sdy.czcb)*100,4)  end )  as feebl11, ";

            fpselect += "  '直接人员费' as feen12, ";
            fpselect += " nvl(sdy.zjryf,0) as fee12 , ";
            fpselect += " (case when nvl(sdy.czcb,0) = 0 then 0 else round((sdy.zjryf/sdy.czcb)*100,4)  end )  as feebl12, ";

            fpselect += "  null as bz, ";
            fpselect += " sdy.ny as ny ";

            fpselect += " from kfsj sdy ,dtstat_djsj dt ";
            //fpselect += " where  sdy.ny <= '" + eny.ToString() + "' and sdy.ny >= '" + bny.ToString() + "' and sdy.cyc_id = '" + cycid + "'";
            fpselect += " where  sdy.ny <= dt.eny and sdy.ny>= dt.bny and sdy.jh = dt.jh and sdy.cyc_id = '" + cycid + "'";

            if (jh != "null")
            {
                fpselect += " and sdy.jh = '" + jh + "'";
            }

            try
            {
                connfp.Open();

                OracleCommand myComm = new OracleCommand(fpselect, connfp);
                OracleDataReader myReader = myComm.ExecuteReader();
                //DataTable Fptable用来输出数据
                DataTable Fptable = new DataTable("fpdata");
                Fptable.Columns.Add("rcye", typeof(string));
                Fptable.Columns.Add("rcyou", typeof(string));
                Fptable.Columns.Add("hs", typeof(string));
                Fptable.Columns.Add("spl", typeof(string));
                Fptable.Columns.Add("czcb", typeof(string));

                Fptable.Columns.Add("czcb1", typeof(string));
                //Fptable.Columns.Add("czcbbl1", typeof(float));
                Fptable.Columns.Add("czcb2", typeof(string));
                //Fptable.Columns.Add("czcbbl2", typeof(float));
                Fptable.Columns.Add("czcb3", typeof(string));
                //Fptable.Columns.Add("czcbbl3", typeof(float));

                Fptable.Columns.Add("bz", typeof(string));
                Fptable.Columns.Add("ny", typeof(string));

                DataRow fprow;
                int n = 1;
                float[,] czcb = new float[12, 3];

                while (myReader.Read())
                {
                    if (myReader.HasRows)
                    {
                        fprow = Fptable.NewRow();
                        fprow[0] = myReader[0];
                        fprow[1] = myReader[1];
                        fprow[2] = myReader[2];
                        fprow[3] = myReader[3];
                        fprow[4] = myReader[4];

                        fprow[8] = myReader[41];
                        fprow[9] = myReader[42];


                        czcb[0, 0] = 5;// float.Parse(myReader[13].ToString());
                        czcb[0, 1] = float.Parse(myReader[6].ToString());
                        czcb[0, 2] = float.Parse(myReader[7].ToString());

                        czcb[1, 0] = 8;// float.Parse(myReader[16].ToString());
                        czcb[1, 1] = float.Parse(myReader[9].ToString());
                        czcb[1, 2] = float.Parse(myReader[10].ToString());

                        czcb[2, 0] = 11;// float.Parse(myReader[17].ToString());
                        czcb[2, 1] = float.Parse(myReader[12].ToString());
                        czcb[2, 2] = float.Parse(myReader[13].ToString());

                        czcb[3, 0] = 14;// float.Parse(myReader[17].ToString());
                        czcb[3, 1] = float.Parse(myReader[15].ToString());
                        czcb[3, 2] = float.Parse(myReader[16].ToString());

                        czcb[4, 0] = 17;// float.Parse(myReader[17].ToString());
                        czcb[4, 1] = float.Parse(myReader[18].ToString());
                        czcb[4, 2] = float.Parse(myReader[19].ToString());

                        czcb[5, 0] = 20;// float.Parse(myReader[17].ToString());
                        czcb[5, 1] = float.Parse(myReader[21].ToString());
                        czcb[5, 2] = float.Parse(myReader[22].ToString());

                        czcb[6, 0] = 23;// float.Parse(myReader[17].ToString());
                        czcb[6, 1] = float.Parse(myReader[24].ToString());
                        czcb[6, 2] = float.Parse(myReader[25].ToString());

                        czcb[7, 0] = 26;// float.Parse(myReader[17].ToString());
                        czcb[7, 1] = float.Parse(myReader[27].ToString());
                        czcb[7, 2] = float.Parse(myReader[28].ToString());

                        czcb[8, 0] = 29;// float.Parse(myReader[17].ToString());
                        czcb[8, 1] = float.Parse(myReader[30].ToString());
                        czcb[8, 2] = float.Parse(myReader[31].ToString());

                        czcb[9, 0] = 32;// float.Parse(myReader[17].ToString());
                        czcb[9, 1] = float.Parse(myReader[33].ToString());
                        czcb[9, 2] = float.Parse(myReader[34].ToString());

                        czcb[10, 0] = 35;// float.Parse(myReader[17].ToString());
                        czcb[10, 1] = float.Parse(myReader[36].ToString());
                        czcb[10, 2] = float.Parse(myReader[37].ToString());

                        czcb[11, 0] = 38;// float.Parse(myReader[17].ToString());
                        czcb[11, 1] = float.Parse(myReader[39].ToString());
                        czcb[11, 2] = float.Parse(myReader[40].ToString());


                        paixu(czcb);
                        int n1, n2, n3;
                        n1 = int.Parse(czcb[0, 0].ToString());
                        n2 = int.Parse(czcb[1, 0].ToString());
                        n3 = int.Parse(czcb[2, 0].ToString());

                        fprow[5] = myReader[n1].ToString();
                        fprow[6] = myReader[n2].ToString();
                        fprow[7] = myReader[n3].ToString();

                        Fptable.Rows.Add(fprow);
                    }
                }
                connfp.Close();

                //此处用于绑定数据
                #region
                DataSet fpset = new DataSet();
                fpset.Tables.Add(Fptable);

                int rcount = fpset.Tables["fpdata"].Rows.Count;
                int ccount = fpset.Tables["fpdata"].Columns.Count;
                int hcount = 2;

                if (FpSpread1.Sheets[0].Rows.Count == hcount)
                {
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount - 1; j++)
                        {
                            string lie = fpset.Tables["fpdata"].Rows[i][ccount - 1].ToString().Substring(4, 2);
                            int li = int.Parse(lie);  //求所在年月的的月份
                            FpSpread1.Sheets[0].Cells[j + hcount, 1 + li].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                        }
                    }

                }
                else//不为空
                {
                    string path = Page.MapPath("~/static/excel/bjwxygzfx.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "边际效益无效益井跟踪分析表");
                    //修改表头
                    SqlHelper DB = new SqlHelper();
                    OracleConnection con1 = DB.GetConn();
                    con1.Open();
                  //  String stjb = "select gsxyjb_1 from dtstat_djsj where dep_id = '" + cyc + "' and jh='" + jh + "'";
                    String stjb = "select gsxyjb_1 from dtstat_djsj where cyc_id = '" + cycid + "' and jh='" + jh + "'";
                    OracleCommand comm1 = new OracleCommand(stjb, con1);
                    OracleDataReader dr1 = comm1.ExecuteReader();
                    dr1.Read();
                    if (dr1[0].ToString() == "3")
                    {
                        FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " 边际效益井" + jh + "信息汇总表";
                    }
                    else
                    {
                        FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " 无效益井" + jh + "信息汇总表";
                    }
                    dr1.Close();
                    con1.Close();

                    string yue = bny;
                    Int32 k;
                    for (int i = 1; i <= 12; i++)
                    {
                        FpSpread1.Sheets[0].Cells[1, i].Text = yue;
                        k = Convert.ToInt32(yue) + 1;
                        yue = k.ToString();
                    }

                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;

                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount - 1; j++)
                        {
                            string lie = fpset.Tables["fpdata"].Rows[i][ccount - 1].ToString().Substring(4, 2);
                            int li = int.Parse(lie);  //求所在年月的的月份
                            FpSpread1.Sheets[0].Cells[j + hcount, li].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
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
        }

        protected int isempty()
        {
            string bny = Session["jbny"].ToString();
            string eny = Session["jeny"].ToString();

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
            FarpointGridChange.FarPointChange(FpSpread1, "bianjiwuxiaofenxi.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;


        }

    }
}
