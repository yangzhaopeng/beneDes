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


namespace beneDesYGS.view.stockAssessment.ytgspjBiao
{
    public partial class biao1 : beneDesYGS.core.UI.corePage
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
            this.FpSpread1.Sheets[0].OpenExcel(path, "表1-评价参数表");

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
            //JObject obj = new JObject();
            string bny = Session["jbny"].ToString();
            string eny = Session["jeny"].ToString();
            //string list = _getParam("YCLX");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            string list = _getParam("CYC");

        OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["ygsConnectionString"].ConnectionString);

        Int64 ny = (Convert.ToInt64(eny.Trim())) - (Convert.ToInt64(bny.Trim())) + 1;
        string fpselect = " Select yqlx_info.yqlxmc ,round((Case When Sum(hscyl) = 0 Then 0 Else Sum(yyspl)/sum(hscyl) End )*100,2) As yyspl, ";
        fpselect += " round((Case When Sum(yyspl) = 0 Then 0 Else Sum(yyxssr)/Sum(yyspl) End),2) As yyjg, ";
        fpselect += " 0 As trqjg, ";
        fpselect += " round((Case When Sum(yyspl) = 0 Then 0 Else Sum(yyxssj)/Sum(yyspl) End),2) As yysj, ";
        fpselect += " 0 As trqsj ";
        //fpselect += " sum(qjktfy_info.tbsyj)/"+ny+" ";
        fpselect += " From jdstat_djsj,yqlx_info ";
        fpselect += " Where  jdstat_djsj.yqlx = yqlx_info.yqlxdm ";
        //fpselect += " and qjktfy_info.ny>='"+bny +"' and qjktfy_info.ny<='"+eny +"' ";
        if (list == "quan")
        {
            fpselect += "";
        }
        else
        {
            fpselect += " and jdstat_djsj.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
        }
        fpselect += " Group By yqlx_info.yqlxmc ";
        
        fpselect += " union ";
        fpselect += " Select  '天然气'as yqlxmc ,round((Case When Sum(hscql) = 0 Then 0 Else Sum(trqspl)/sum(hscql) End )*100,2) As yyspl, ";
        fpselect += " 0 As yyjg, ";
        fpselect += " round((Case When Sum(trqspl) = 0 Then 0 Else Sum(trqxssr)/Sum(trqspl)/10 End),2) As trqjg, ";
        fpselect += " 0 As yysj, ";
        fpselect += " round((Case When Sum(trqspl) = 0 Then 0 Else Sum(trqxssj)/Sum(trqspl)/10 End),2) As trqsj ";
        //fpselect += " sum(qjktfy_info.tbsyj)/" + ny + " ";
        fpselect += " From jdstat_djsj,yqlx_info  ";
        fpselect += " Where jdstat_djsj.yqlx = yqlx_info.yqlxdm  and hscql>0  and  trqspl >0 and trqxssr>0  ";
        //fpselect += " and qjktfy_info.ny>='" + bny + "' and qjktfy_info.ny<='" + eny + "' ";
        if (list == "quan")
        {
            fpselect += "";
        }
        else
        {
            fpselect += " and jdstat_djsj.dep_id  = (select dep_id from department where department.dep_id = '" + list + "') ";
        }
        fpselect += " Group By yqlx_info.yqlxmc order by yqlxmc desc";

        string tbsyj = " select round(sum(tbsyj_info.tbsyj)/" + ny + ",2) from tbsyj_info where tbsyj_info.ny>='" + bny + "' and tbsyj_info.ny<='" + eny + "' ";
        try
        {
            connfp.Open();
            OracleDataAdapter adafp = new OracleDataAdapter(fpselect, connfp);
            DataSet fpset = new DataSet();
            adafp.Fill(fpset, "fpdata");
            OracleDataAdapter tbsy=new OracleDataAdapter (tbsyj ,connfp );
            DataSet tbsyjds = new DataSet ();
            tbsy.Fill (tbsyjds,"tbsyj");

            //此处用于绑定数据
            #region
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
                        FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                        
                    }
                    FpSpread1.Sheets[0].Cells[i + hcount, 6].Value = tbsyjds.Tables["tbsyj"].Rows[0][0].ToString();
                }
                //合并单元格
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
            else//不为空
            {
                string path = "../../../static/excel/gufenbiao.xls";
                path = Page.MapPath(path);
                this.FpSpread1.Sheets[0].OpenExcel(path, "表1-评价参数表");

                this.FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                this.FpSpread1.Sheets[0].RowHeader.Visible = false;

                FpSpread1.Sheets[0].AddRows(hcount, rcount);
                for (int i = 0; i < rcount; i++)
                {
                    for (int j = 0; j < ccount; j++)
                    {
                        FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                        FpSpread1.Sheets[0].Cells[i + hcount, 6].Value = tbsyjds.Tables["tbsyj"].Rows[0][0].ToString();
                    }
                }
                //合并单元格
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
            string bny = Session["jbny"].ToString();
            string eny = Session["jeny"].ToString();

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
            FarpointGridChange.FarPointChange(FpSpread1, "biao1.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;


        }
   }

 }
