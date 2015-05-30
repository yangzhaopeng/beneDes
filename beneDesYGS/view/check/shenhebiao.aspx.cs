using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using System.Data.OleDb;
using beneDesYGS.core;

namespace beneDesYGS.view.check
{
    public partial class shenhebiao : System.Web.UI.Page
    {
        static SqlHelper DB = new SqlHelper();
        OracleConnection con = DB.GetConn();
        public static string time;
        public static string cycname;
        public static int cycnum;
        public static string cycid;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            this.FpSpread1.Attributes.Add("BorderColor", "#83B1D3");
            if (!IsPostBack)
            {
                             
            }
        }


        private void databind()
        {
            string ny = time;
            string cyc = cycname;
            string item = itemlist.SelectedItem.Value;

            con.Open();
            string sql;
            sql = sqlback(item, ny, cyc);
            OracleDataAdapter da = new OracleDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "all");
            FpSpread1.DataSource = ds.Tables["all"];
            FpSpread1.DataBind();
            con.Close();
        }


        private string sqlback(string aa, string bb, string cc)
        {
            
            string item = aa;
            string ny = bb;
            string cyc = cc;
            string sql;
           
            if (item == "sc_info")
            {
                sql = "select ny as 年月,dep_id as 作业区,yqlxdm as 油气类型代码,round(sc,2) as 输差 from " + item + " where ny='" + ny + "' and cyc_id='" + cyc + "'";
                return sql;

            }
           
            if (item == "yqspl_info")
            {
                sql = "select ny as 年月,yqlxdm as 油气类型代码,round(yqspl,2) as 油气商品率 from " + item + " where ny='" + ny + "' and cyc_id='" + cyc + "'";
                return sql;
            }

            if (item == "djsj")
            {
                sql = "select ny as 年月,jh as 井号,jb as 井别,tcrq as 投产日期,ssyt as 所属油田,pjdy as 评价单元,yclx as 油藏类型,zyq as 作业区,qk as 区块,zxz as 中心站,zrz as 自然站,cyjxh as 抽油机型号,cyjmpgl as 抽油机名牌功率,djgl as 电机功率,round(fzl,2) as 负载率,djdb as 单井电表,yqlx as 油气类型,xsyp as 销售油品 from " + item + " where ny='" + ny + "' and dep_id='" + cyc + "'";
                return sql;
            }
            if (item == "kfsj")
            {
                sql = "select ny as 年月,jh as 井号,zyq as 作业区,qk as 区块,cx as 层系,round(cyhd,2) as 采油厚度,scsj as 生产时间,round(jkcyl,2) as 井口产液量,round(jkcyoul,2) as 井口产油量,round(hscyl,2) as 核实产油量,round(ljcyl,4) as 累计产油量,round(jkcql,4) as 井口产气量,round(hscql,4) as 核实产气量,round(ljcql,4) as 累计产气量,round(hs,2) as 综合含水,round(zsl,4) as 注水量,round(ljzsl,4) as 累计注水量,round(zql,4) as 注汽量,round(ljzql,4) as 累计注汽量,round(cyl,4) as 掺油量,round(csl,4) as 掺水量,round(cyaol,4)  as  掺药量,";
                sql = sql + "dzcsdm as 地质措施代码,gycsdm as 工艺措施代码,bz as 备注 from " + item + " where ny='" + ny + "' and dep_id='" + cyc + "'";
                return sql;
            }
            if (item == "stat_djydsj")
            {//2011.7.21 修改了模板
                //  sql = "select NY as 年月, JH as 井号,round(ZJCLF,2) as 直接材料费, round(ZJRLF,2) as 直接燃料费, round(ZJDLF,2) as 直接动力费, round(ZJRYF,2) as 直接人员费, round(QYWZRF,2) as 驱油物注入费, round(qywzrf_ryf,2) as 其中人员费,round(JXZYF,2) as 井下作业费,  round(WHXJXZYF,2) as 维护性井下作业费, round(CJSJF,2) as 测井试井费, round(WHXLF,2) as 维护及修理费, round(CYRCF,2) as 稠油热采费, round(YQCLF,2) as 油气处理费,round(yqclf_ryf,2) as 其中_人员费, round(QTHSF,2) as 轻烃回收费,  round(YSF,2) as 运输费, round(LYF,2) as 其中拉油费, round(QTZJF,2) as 其它直接费, round(CKGLF,2) as 厂矿管理费, round(ZYYQCP,2) as 自用油气产品, round(ZJZH,2) as 折旧折耗,  round(CZCB,2) as 操作成本  from " + item + " where ny='" + ny + "' and dep_id='" + cyc + "'";
                sql = "select NY as 年月, JH as 井号, QK as 区块,  round(ZJCLF,2) as 直接材料费, round(ZJRLF,2) as 直接燃料费,	round(ZJDLF,2) as 直接动力费, round(QYWZRF,2) as 驱油物注入费,	round(qywzrf_ryf,2) as 其中人员费, round(CJSJF,2) as 测井试井费, round(WHXLF,2) as 维护及修理费,	round(YQCLF,2) as 油气处理费,	round(yqclf_ryf,2) as 其中_人员费, round(QTHSF,2) as 轻烃回收费, round(YSF,2) as 运输费_拉油费, round(LYF,2) as 拉油费, round(QTZJF,2) as 其它直接费, round(CKGLF,2) as 厂矿管理费, round(ZJRYF,2) as 直接人员费, round(ZYYQCP,2) as 自用油气产品, round(ZJZH,2) as 折旧折耗  from " + item + " where ny='" + ny + "' and dep_id='" + cyc + "'";
                return sql;
            }
            if (item == "qksj")
            {
                sql = "select  NY as 年月,  QKMC as 区块, SSYT as 所属油田,  YCLX as 油藏类型, round(DYHYMJ,4) as 动用含油面积,  round(DYDZCL,4) as 动用地质储量,  round(DYKCCL,4) as 动用可采储量,  round(YCZS,2) as 油藏中深,  round(PJSTL,2) as 平均渗透率,  round(DXYYND,2) as 地下原油粘度,  YJZJS as 油井总井数, YJKJS as 油井开井数, SJZJS as 水井总井数,SJKJS as 水井开井数,round(ZSJ,4) as 月注水量,round(LJZSL,4) as 累计注水量,ZQJZJS as 注汽井总井数,ZQJKJZ as 注汽井开井数, round(ZQL,4) as 月注汽量,  round(LJZQL,4) as 累计注汽量,round(CYOUL,4) as 月产油量,  round(LJCYOUL,4) as 累计产油量,round(CYL,4) as 月产液量,round(LJCYL,4) as 累计产液量,round(CQL,4) as 月产气量,round(LJCQL,4) as 累计产气量,    ";
                sql = sql + "XSYP as 销售油品,  SFPJ as 是否评价 from qksj where ny='" + ny + "' and dep_id='" + cyc + "' ";
                return sql;
            }

            if (item == "pjdysj")
            {
                sql = "select  NY as 年月,PJDYMC as 评价单元, SSYT as 所属油田,  YCLX as 油藏类型, round(DYHYMJ,4) as 动用含油面积,  round(DYDZCL,4) as 动用地质储量,  round(DYKCCL,4) as 动用可采储量,  round(YCZS,2) as 油藏中深,  round(PJSTL,2) as 平均渗透率,  round(DXYYND,2) as 地下原油粘度,  YJZJS as 油井总井数, YJKJS as 油井开井数, SJZJS as 水井总井数,SJKJS as 水井开井数,round(ZSJ,4) as 月注水量,round(LJZSL,4) as 累计注水量,ZQJZJS as 注汽井总井数,ZQJKJZ as 注汽井开井数, round(ZQL,4) as 月注汽量,  round(LJZQL,4) as 累计注汽量,round(CYOUL,4) as 月产油量,  round(LJCYOUL,4) as 累计产油量,round(CYL,4) as 月产液量,round(LJCYL,4) as 累计产液量,round(CQL,4) as 月产气量,round(LJCQL,4) as 累计产气量,   ";
                sql = sql + "XSYP as 销售油品, SFPJ as 是否评价 from pjdysj where ny='" + ny + "' and dep_id='" + cyc + "'";
                return sql;

            }
            else
            {
                return null;
            }
        }
       
       
        
      

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ny是数据表里数据的年月，cycid是数据表里数据的采油厂NAME
            //string ny = time;
            string ny = Request["NY"];
            string item = itemlist.SelectedItem.Value.ToString().Trim();

                if (Request["CYCID"].ToString() == null)
                {
                    //没有选择采油厂的时候
                    //cycid = GridView2.Rows[0].Cells[2].Text.ToString().Trim();
                    Response.Write("<script>alert('请选择要审核的条目')</script>");

                }
                else
                {
                    //cycid = cycname;
                    cycid = Request["CYCID"];
                    con.Open();
                    string sql;
                    sql = sqlback(item, ny, cycid);
                    OracleDataAdapter da = new OracleDataAdapter(sql, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "all");
                    if (item == "sc_info")
                    {
                        if (ds.Tables["all"].Rows.Count == 0)
                        {
                            Response.Write("<script>alert('该月数据尚未上报')</script>");
                        }
                        else
                        {
                            FpSpread1.DataSource = ds.Tables["all"];
                            
                            FpSpread1.DataBind();

                            FpSpread1.Sheets[0].ColumnHeaderVisible = true;
                            int rcount = ds.Tables["all"].Rows.Count;
                            int ccount = ds.Tables["all"].Columns.Count;
                            for (int i = 0; i < ccount; i++)
                            {
                                FpSpread1.Sheets[0].ColumnHeader.Columns[i].Font.Size = 9;
                            }
                            for (int i = 0; i < rcount; i++)
                            {

                                FpSpread1.ActiveSheetView.Cells[i, 0].HorizontalAlign = HorizontalAlign.Center;
                                FpSpread1.ActiveSheetView.Rows[i].Font.Size = 9;
                            }

                        }
                    }
                    else if (item == "yqspl_info")
                    {
                        if (ds.Tables["all"].Rows.Count == 0)
                        {
                            Response.Write("<script>alert('该月数据尚未上报')</script>");
                        }
                        else
                        {
                            FpSpread1.DataSource = ds.Tables["all"];
                            FpSpread1.DataBind();
                            FpSpread1.Sheets[0].ColumnHeaderVisible = true;
                            int rcount = ds.Tables["all"].Rows.Count;
                            int ccount = ds.Tables["all"].Columns.Count;
                            for (int i = 0; i < ccount; i++)
                            {
                                FpSpread1.Sheets[0].ColumnHeader.Columns[i].Font.Size = 9;
                            }
                            for (int i = 0; i < rcount; i++)
                            {

                                FpSpread1.ActiveSheetView.Cells[i, 0].HorizontalAlign = HorizontalAlign.Center;
                                FpSpread1.ActiveSheetView.Rows[i].Font.Size = 9;
                            }
                        }
                    }
                    else if (item == "djsj")
                    {
                        if (ds.Tables["all"].Rows.Count == 0)
                        {
                            Response.Write("<script>alert('该月数据尚未上报')</script>");
                        }
                        else
                        {
                            FpSpread1.DataSource = ds.Tables["all"];
                            FpSpread1.DataBind();
                            FpSpread1.Sheets[0].ColumnHeaderVisible = true;
                            
                            int rcount = ds.Tables["all"].Rows.Count;
                            int ccount = ds.Tables["all"].Columns.Count;
                            for (int i = 0; i < ccount; i++)
                            {
                                FpSpread1.Sheets[0].ColumnHeader.Columns[i].Font.Size = 9;
                            }
                            for (int i = 0; i < rcount; i++)
                            {

                                FpSpread1.ActiveSheetView.Cells[i, 0].HorizontalAlign = HorizontalAlign.Center;
                                FpSpread1.ActiveSheetView.Rows[i].Font.Size = 9;
                               
                            }
                            //冻结表头,前2列
                            //FpSpread1.ActiveSheetView.FrozenRowCount = 1;
                            FpSpread1.ActiveSheetView.FrozenColumnCount = 2;
                            FpSpread1.ActiveSheetView.AllowPage = true;

                        }
                    }
                    else if (item == "kfsj")
                    {
                        if (ds.Tables["all"].Rows.Count == 0)
                        {
                            Response.Write("<script>alert('该月数据尚未上报')</script>");
                        }
                        else
                        {
                            FpSpread1.DataSource = ds.Tables["all"];
                            FpSpread1.DataBind();
                            FpSpread1.Sheets[0].ColumnHeaderVisible = true;
                            int rcount = ds.Tables["all"].Rows.Count;
                            int ccount = ds.Tables["all"].Columns.Count;
                            for (int i = 0; i < ccount; i++)
                            {
                                FpSpread1.Sheets[0].ColumnHeader.Columns[i].Font.Size = 9;
                            }
                            for (int i = 0; i < rcount; i++)
                            {

                                FpSpread1.ActiveSheetView.Cells[i, 0].HorizontalAlign = HorizontalAlign.Center;
                                FpSpread1.ActiveSheetView.Rows[i].Font.Size = 9;
                            }
                            //冻结表头,前2列
                            //FpSpread1.ActiveSheetView.FrozenRowCount = 1;
                            FpSpread1.ActiveSheetView.FrozenColumnCount = 2;
                            FpSpread1.ActiveSheetView.AllowPage = true;
                        }
                    }
                    else if (item == "stat_djydsj")
                    {
                        if (ds.Tables["all"].Rows.Count == 0)
                        {
                            Response.Write("<script>alert('该月数据尚未上报')</script>");
                        }
                        else
                        {
                            FpSpread1.DataSource = ds.Tables["all"];
                            FpSpread1.DataBind();
                            FpSpread1.Sheets[0].ColumnHeaderVisible = true;
                            int rcount = ds.Tables["all"].Rows.Count;
                            int ccount = ds.Tables["all"].Columns.Count;
                            for (int i = 0; i < ccount; i++)
                            {
                                FpSpread1.Sheets[0].ColumnHeader.Columns[i].Font.Size = 9;
                            }
                            for (int i = 0; i < rcount; i++)
                            {

                                FpSpread1.ActiveSheetView.Cells[i, 0].HorizontalAlign = HorizontalAlign.Center;
                                FpSpread1.ActiveSheetView.Rows[i].Font.Size = 9;
                            }
                            //冻结表头,前2列
                            //FpSpread1.ActiveSheetView.FrozenRowCount = 1;
                            FpSpread1.ActiveSheetView.FrozenColumnCount = 2;
                            FpSpread1.ActiveSheetView.AllowPage = true;
                        }
                    }
                    else if (item == "qksj")
                    {
                        if (ds.Tables["all"].Rows.Count == 0)
                        {
                            Response.Write("<script>alert('该月数据尚未上报')</script>");
                        }
                        else
                        {
                            FpSpread1.DataSource = ds.Tables["all"];
                            FpSpread1.DataBind();
                            FpSpread1.Sheets[0].ColumnHeaderVisible = true;
                            int rcount = ds.Tables["all"].Rows.Count;
                            int ccount = ds.Tables["all"].Columns.Count;
                            for (int i = 0; i < ccount; i++)
                            {
                                FpSpread1.Sheets[0].ColumnHeader.Columns[i].Font.Size = 9;
                            }
                            for (int i = 0; i < rcount; i++)
                            {

                                FpSpread1.ActiveSheetView.Cells[i, 0].HorizontalAlign = HorizontalAlign.Center;
                                FpSpread1.ActiveSheetView.Rows[i].Font.Size = 9;
                            }
                            //冻结表头,前2列
                            //FpSpread1.ActiveSheetView.FrozenRowCount = 1;
                            FpSpread1.ActiveSheetView.FrozenColumnCount = 2;
                        }
                    }
                    else if (item == "pjdysj")
                    {
                        if (ds.Tables["all"].Rows.Count == 0)
                        {
                            Response.Write("<script>alert('该月数据尚未上报')</script>");
                        }
                        else
                        {
                            FpSpread1.DataSource = ds.Tables["all"];
                            FpSpread1.DataBind();
                            FpSpread1.Sheets[0].ColumnHeaderVisible = true;
                            int rcount = ds.Tables["all"].Rows.Count;
                            int ccount = ds.Tables["all"].Columns.Count;
                            for (int i = 0; i < ccount; i++)
                            {
                                FpSpread1.Sheets[0].ColumnHeader.Columns[i].Font.Size = 9;
                            }
                            for (int i = 0; i < rcount; i++)
                            {

                                FpSpread1.ActiveSheetView.Cells[i, 0].HorizontalAlign = HorizontalAlign.Center;
                                FpSpread1.ActiveSheetView.Rows[i].Font.Size = 9;
                            }
                            //冻结表头,前2列
                            //FpSpread1.ActiveSheetView.FrozenRowCount = 1;
                            FpSpread1.ActiveSheetView.FrozenColumnCount = 2;
                        }
                    }
                    con.Close();
                }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
         
            string ny = time;
            string item = itemlist.SelectedItem.Value.ToString().Trim();


            cycid = cycname;
            con.Open();
            string sql;
            sql = sqlback(item, ny, cycid);
            OracleDataAdapter da = new OracleDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "all");
            int rcount = ds.Tables["all"].Rows.Count;
            int ccount = ds.Tables["all"].Columns.Count;
            int hcount = 1;
            string path;
            path = Page.MapPath("~/excel/shenheshuju.xls");
            if (item == "djsj")
            {
                FpSpread2.ActiveSheetView.OpenExcel(path, "单井基础数据");
            }
            else if (item == "kfsj")
            {
                FpSpread2.ActiveSheetView.OpenExcel(path, "单井开发数据");
            }
            else if (item == "stat_djydsj")
            {
                FpSpread2.ActiveSheetView.OpenExcel(path, "单井成本数据");
            }
            else if (item == "sc_info")
            {
                FpSpread2.ActiveSheetView.OpenExcel(path, "输差信息");
            }
            else if (item == "yqspl_info")
            {
                FpSpread2.ActiveSheetView.OpenExcel(path, "油气商品率");
            }
            else if (item == "qksj")
            {
                FpSpread2.ActiveSheetView.OpenExcel(path, "区块数据");
            }
            else if (item == "pjdysj")
            {
                FpSpread2.ActiveSheetView.OpenExcel(path, "评价单元数据");
            }
            //FpSpread2.ActiveSheetView.ColumnHeader.Visible = false;
            //FpSpread2.ActiveSheetView.RowHeader.Visible = false;

            //FpSpread1.Sheets[0].PageSize = 80;

            FpSpread2.ActiveSheetView.AddRows(hcount, rcount);
            for (int i = 0; i < rcount; i++)
            {
                for (int j = 0; j < ccount; j++)
                {
                    FpSpread2.ActiveSheetView.Cells[i + hcount, j].Value = ds.Tables["all"].Rows[i][j].ToString();
                    FpSpread2.ActiveSheetView.Cells[i + hcount, j].Font.Size = 9;
                }
            }
            //冻结表头,前2列
            FpSpread2.ActiveSheetView.FrozenRowCount = 1;
            FpSpread2.ActiveSheetView.FrozenColumnCount = 2;
            con.Close();
            FpSpread2.Sheets[0].RowHeader.Visible = true;
            FpSpread2.Sheets[0].ColumnHeader.Visible = true;

            FarpointGridChange.FarPointChange(FpSpread2, "shenheshuju.xls");
            FpSpread2.Sheets[0].RowHeader.Visible = false;
            FpSpread2.Sheets[0].ColumnHeader.Visible = false;



        }
    }
}
