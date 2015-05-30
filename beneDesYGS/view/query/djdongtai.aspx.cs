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
using Newtonsoft.Json.Linq;

namespace beneDesYGS.view.query
{
    public partial class djdongtai : beneDesYGS.core.UI.corePage
    {
        SqlHelper sqlhelper = new SqlHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            string cyc = _getParam("CYC");
            string type = _getParam("type");
            if (type == "total")
            {
                JObject item = new JObject();
                item.Add("total", getCount());
                _return(true, "", item);
            }

            if (cyc == "[a-zA-Z]")
            { initSpread(); }
            else
            { jiansuo_Click(); }
        }
        private int getCount()
        {
            int total = 0;
            string SJCX = _getParam("SJCX");
            string SJCX2 = _getParam("SJCX2");
            string SJCX3 = _getParam("SJCX3");
            string SJCX4 = _getParam("SJCX4");
            string SJCX5 = _getParam("SJCX5");
            //this.labshow.Text = this.xzType;

            string cyc = _getParam("CYC");
            string cyc_id = cyc;
            string cqc_id = cyc;
            if (cyc_id.Contains(','))
            {
                cyc_id = cyc.Split(',')[0];
                cqc_id = cyc.Split(',')[1];
            }
            string sql = "select count(1) ";
            sql += SQLCHK();
            total = Convert.ToInt32(sqlhelper.GetExecuteScalar(sql));
            return total;
        }
        protected void initSpread()
        {
            string path = Page.MapPath("~/static/excel/shujuchaxun.xls");
            this.FpSpread1.Sheets[0].OpenExcel(path, "单井油田评价");
            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
            FpSpread1.Width = Unit.Pixel(8233);
        }

        protected void jiansuo_Click()
        {

            string SJCX = _getParam("SJCX");
            string SJCX2 = _getParam("SJCX2");
            string SJCX3 = _getParam("SJCX3");
            string SJCX4 = _getParam("SJCX4");
            string SJCX5 = _getParam("SJCX5");
            //this.labshow.Text = this.xzType;
            if (SJCX == "null" && SJCX2 == "null" && SJCX3 == "null" && SJCX4 == "null")
            {
                Response.Write("<script>alert('请选择查询条件')</script>");
            }
            else
            {
                SJJS();

            }


        }

        private void SJJS()
        {
            //string cyc = _getParam("CYC");
            //SqlHelper conn = new SqlHelper();
            //OracleConnection con = conn.GetConn();
            //con.Open();

            string sql = "select d.bny as 开始年月, d.eny as 结束年月, d.JH as 井号, d.DEP_ID as 采油厂,(case when d.jb=0 then '正常井' when d.jb=1 then '内部捞油井' when d.jb=2 then '外部捞油井' else '停产井' end) as 井别, d.SSYT as 所属油田, d.YCLX as 油藏类型, d.QK as 区块, d.PJDY as 评价单元, y.yqlxmc as 油气类型, x.xsypmc as 销售油品, d.SCSJ as 生产时间, d.JKCYL as 产液量, d.HSCYL as 产油量, d.HSCQL as 产气量, d.ZSL as 注水量, d.ZQL as 注汽量, d.YQL as 油气量, d.NBHSYQL as 内部核实油气量, d.YQSPL as 油气商品量, d.DTL as 油气商品量桶, d.YYSPL as 原油商品量, d.TRQSPL as 天然气商品量, d.LJCYL as 累计产油量, d.LJCQL as 累计产气量, d.LJZQL as 累计注汽量, d.LJZSL as 累计注水量, d.BSCPCL as 伴生产品产量, d.BSCPSPL as 伴生产品商品量, d.RCY as 日产油, d.HS as 含水, d.ZJCLF as 直接材料费, d.ZJRLF as 直接燃料费, d.ZJDLF as 直接动力费, d.ZJRYF as 直接人员费, d.QYWZRF as 驱油物注入费, d.QYWZRF_RYF as 驱油物注入费人员费, d.JXZYF as 井下作业费, d.WHXJXZYF as 维护性井下作业费, d.CJSJF as 测井试井费, d.CJSJF_RYF as 测井试井费人员费, d.WHXLF as 维护及修理费, d.WHXLF_RYF as 维护及修理费_人员费, d.CYRCF as 稠油热采费, d.YQCLF as 油气处理费, d.YQCLF_RYF as 油气处理费人员费, d.QTHSF as 轻烃回收费, d.TRQJHF as 天然气净化费, d.YSF as 运输费, d.YSF_RYF as 运输费人员费, d.LYF as 拉油费, d.QTZJF as 其它直接费, d.CKGLF as 厂矿管理费, d.ZYYQCP as 自用油气产品, d.ZJZH as 折旧折耗, d.HSCZCB as 核实操作成本, d.CZCB as 操作成本, d.CZCB_MY as 操作成本美元, d.YXCZCB as 运行操作成本, d.ZJYXCZCB as 直接运行操作成本, d.YXCZCB_MY as 运行操作成本美元, d.ZJYXCZCB_MY as 直接运行操作成本美元, d.QJF as 期间费用, d.KTF as 勘探费用, d.TBSYJ as 特别收益金, d.ZDYXF as 最低运行费, d.ZDYXF_MY as 最低运行费美元, d.SCCB as 生产成本, d.SCCB_MY as 生产成本美元, d.YYCB as 营运成本, d.YYCB_MY as 营运成本美元, d.YYXSSR as 原油销售收入, d.YYXSSJ as 原油销售税金, d.TRQXSSR as 天然气销售收入, d.TRQXSSJ as 天然气销售税金, d.BSCPXSSR as 伴生产品销售收入, d.BSCPXSSJ as 伴生产品销售税金, d.BSCPSHSR as 伴生产品税后收入, d.XSSR as 销售总收入, d.XSSJ as 销售总税金, d.YYZYS as 原油资源税, d.TRQZYS as 天然气资源税, d.ZYS as 资源税, d.SHSR as 税后总收入, d.LR as 利润, d.LR_MY as 利润美元, g.jbmc as 效益类别,(case when d.DJISOPEN=1 then '是' else '否' end) as 是否开井";
            sql += SQLCHK();
            #region 添加分页
            int limit = Convert.ToInt32(_getParam("limit"));
            int start = Convert.ToInt32(_getParam("start"));
            int page = start / limit + 1;
            int begin = page * limit - limit + 1;
            int end = page * limit;
            int total = Convert.ToInt32(sqlhelper.GetExecuteScalar("select count(3) " + SQLCHK()));

            string sqlstr = "select * from (select a.*, ROWNUM RN FROM (" + sql + ") a WHERE ROWNUM <= " + end + ") where RN >= " + begin + "";

            #endregion
            //OracleDataAdapter myda = new OracleDataAdapter(sql, con);
            //DataSet myds = new DataSet();
            //myda.Fill(myds, "jiansuo");

            DataSet myds = sqlhelper.GetDataSet(sqlstr);
            if (myds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<script>alert('不存在该项记录！')</script>");
            }
            else
            {
                //for (int i = 0; i < myds.Tables["jiansuo"].Rows.Count; i++)
                //{

                //    myds.Tables["jiansuo"].Rows[i]["采油厂"] = cyc;

                //}
                //this.GridView1.DataSource = myds.Tables["jiansuo"];
                //GridView1.DataBind();
                string path = Page.MapPath("~/static/excel/shujuchaxun.xls");
                this.FpSpread1.Sheets[0].OpenExcel(path, "单井油田评价");

                //FpSpread1.ActiveSheetView.DataSource = myds;
                //FpSpread1.ActiveSheetView.DataMember = "jiansuo";

                //FpSpread1.Visible = true;
                this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                FpSpread1.Width = Unit.Pixel(8233);
                int hcount = 1;
                int rcount = myds.Tables[0].Rows.Count - 1;
                //去掉分页列
                int ccount = myds.Tables[0].Columns.Count - 1;
                if (FpSpread1.Sheets[0].Rows.Count == hcount)
                {
                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int rowNum = 0; rowNum < rcount; rowNum++)
                    {
                        for (int colNum = 0; colNum < ccount; colNum++)
                        {
                            FpSpread1.Sheets[0].Cells[rowNum + hcount, colNum].Value = myds.Tables[0].Rows[rowNum][colNum].ToString();
                            FpSpread1.Sheets[0].Cells[rowNum + hcount, colNum].Font.Size = 9;
                            FpSpread1.Sheets[0].Cells[rowNum + hcount, colNum].Font.Name = "微软雅黑";
                        }
                    }
                }
                for (int i = 0; i < rcount; i++)
                {
                    this.FpSpread1.ActiveSheetView.Cells[i, 0].HorizontalAlign = HorizontalAlign.Center;
                }
                for (int j = 0; j < ccount; j++)
                {
                    this.FpSpread1.ActiveSheetView.Columns[j].Width = 140;
                    this.FpSpread1.ActiveSheetView.Columns[j].Font.Size = 9;
                }


                //冻结表头,前2列
                FpSpread1.ActiveSheetView.FrozenColumnCount = 2;
                FpSpread1.ActiveSheetView.AllowPage = true;

            }
            //con.Close();
        }

        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    //base.VerifyRenderingInServerForm(control);
        //}

        private string SQLCHK()
        {
            string xzType = _getParam("xzType");
            string xzType2 = _getParam("xzType2");
            string xzType3 = _getParam("xzType3");
            string xzType4 = _getParam("xzType4");
            string xzType5 = _getParam("xzType5");

            string SJCX = _getParam("SJCX");
            string SJCX2 = _getParam("SJCX2");
            string SJCX3 = _getParam("SJCX3");
            string SJCX4 = _getParam("SJCX4");
            string SJCX5 = _getParam("SJCX5");

            string ROAND = _getParam("ROAND");
            string ROAND2 = _getParam("ROAND2");
            string ROAND3 = _getParam("ROAND3");
            string ROAND4 = _getParam("ROAND4");
            //modified by lz ,2011-1-6 
            string sql = "";
            string cyc = _getParam("CYC");
            string cyc_id = cyc;
            string cqc_id = cyc;
            if (cyc_id.Contains(','))
            {
                cyc_id = cyc.Split(',')[0];
                cqc_id = cyc.Split(',')[1];
            }
            sql += " from DTSTAT_DJSJ_all d,yqlx_info y,xsyp_info x ,dtxyjb_info g  where g.jbid=d.gsxyjb_1 and  d.yqlx=y.yqlxdm and d.xsyp=x.xsypdm and ";
            sql += "(regexp_like(dep_id,'" + cyc_id + "') or regexp_like(dep_id,'" + cqc_id + "')) ";
            //if (xzType != "null" && xzType2 != "null" && xzType3 != "null" && xzType4 != "null" && xzType5 != "null")
            //{
            //    //string sql = "SELECT NY,DJ_ID,JH, QK,PJDY,YQLX, XSYP, JB, SSYT,YCLX,ZHZQLC,SCSJ,ZJCLF, ZJRLF,ZJDLF,ZJRYF,QYWZRF,JXZYF,WHXJXZYF,CJSJF, WHXLF,CYRCF,YQCLF,QTHSF,TRQJHF,YSF,LYF,QTZJF,CKGLF, ZYYQCP,ZJZH,QJF,KTF,ZDYXF, ZDYXF_MY,HSCZCB,CZCB,CZCB_MY,(Case when YQSPL = 0 then 0 else CZCB / YQSPL end) as DWCZCB, SCCB, SCCB_MY,YYCB,YYCB_MY,YYXSSR,YYXSSJ,TRQXSSR,TRQXSSJ,BSCPXSSR, BSCPXSSJ,BSCPSHSR,XSSR,XSSJ,";
            //    //sql = sql + "YYZYS,TRQZYS,  ZYS, SHSR,LR, LR_MY, GSXYJB,ZDYXYJB,JKCYL,HSCYL,HSCQL, ZSL,ZQL, YQL,NBHSYQL,YQSPL,DTL,YYSPL,TRQSPL,LJCYL, LJCQL,LJZQL, LJZSL,  BSCPCL,BSCPSPL, RCY, HS, YQB, LJYQB,  CZCBJB,  RCYJB, HSJB,   YQBJB,DJISOPEN,QKXYJB, PJDYXYJB, CZCBDLJB  FROM STAT_DJYDSJ ";
            //    //return sql;
            //    sql += cyc + "' and  " + xzType + "='" + SJCX + "' " + ROAND + " " + xzType2 + " ='" + SJCX2 + "' " + ROAND2 + " " + xzType3 + " ='" + SJCX3 + "' " + ROAND3 + " " + xzType4 + " ='" + SJCX4 + "' " + ROAND4 + " " + xzType5 + " ='" + SJCX5 + "'";

            //}
            //else
            //{
            if (xzType == "null" && xzType2 != "null" && xzType3 != "null")
            {
                sql +=  " and " + xzType2 + " ='" + SJCX2 + "' " + ROAND2 + " " + xzType3 + "='" + SJCX3 + "'  ";
                return sql;
            }

            if (xzType == "null" && xzType2 == "null" && xzType3 != "null")
            {
                sql += " and  " + xzType3 + "='" + SJCX3 + "'  ";
                return sql;
            }
            if (xzType == "null" && xzType2 != "null" && xzType3 == "null")
            {
                sql += " and " + xzType2 + " ='" + SJCX2 + "' ";
                return sql;
            }
            if (xzType != "null" && xzType2 != "null" && xzType3 != "null" && xzType4 != "null")
            {
                sql +=  " and  " + xzType + "='" + SJCX + "' " + ROAND + " " + xzType2 + " ='" + SJCX2 + "' " + ROAND2 + " " + xzType3 + " ='" + SJCX3 + "' " + ROAND3 + " " + xzType4 + " ='" + SJCX4 + "'";
                return sql;
            }
            else
            {
                if (xzType != "null" && xzType2 != "null" && xzType3 != "null")
                {
                    sql +=  " and  " + xzType + "='" + SJCX + "' " + ROAND + " " + xzType2 + " ='" + SJCX2 + "' " + ROAND2 + " " + xzType3 + " ='" + SJCX3 + "'";
                    return sql;
                }
                else
                {
                    if (xzType != "null" && xzType2 != "null")
                    {
                        sql += " and  " + xzType + "='" + SJCX + "' " + ROAND + " " + xzType2 + " ='" + SJCX2 + "'";
                        return sql;
                    }
                    else
                    {
                        sql += " and   " + xzType + "='" + SJCX + "'";
                        return sql;
                    }
                }
            }

        }


        protected void DC_Click(object sender, EventArgs e)
        {

            string SJCX = _getParam("SJCX");
            string SJCX2 = _getParam("SJCX2");
            string SJCX3 = _getParam("SJCX3");
            string SJCX4 = _getParam("SJCX4");
            string SJCX5 = _getParam("SJCX5");

            string cyc = _getParam("CYC");

            SqlHelper conn = new SqlHelper();
            OracleConnection con = conn.GetConn();

            if (SJCX == "null" && SJCX2 == "null" && SJCX3 == "null" && SJCX4 == "null")
            {//注意防止空异常！
                Response.Write("<script>alert('请选择查询条件')</script>");
            }
            else
            {
                con.Open();
                string sql = "select d.bny as 开始年月, d.eny as 结束年月, d.JH as 井号, d.DEP_ID as 采油厂,(case when d.jb=0 then '正常井' when d.jb=1 then '内部捞油井' when d.jb=2 then '外部捞油井' else '停产井' end) as 井别, d.SSYT as 所属油田, d.YCLX as 油藏类型, d.QK as 区块, d.PJDY as 评价单元, y.yqlxmc as 油气类型, x.xsypmc as 销售油品, d.SCSJ as 生产时间, d.JKCYL as 产液量, d.HSCYL as 产油量, d.HSCQL as 产气量, d.ZSL as 注水量, d.ZQL as 注汽量, d.YQL as 油气量, d.NBHSYQL as 内部核实油气量, d.YQSPL as 油气商品量, d.DTL as 油气商品量桶, d.YYSPL as 原油商品量, d.TRQSPL as 天然气商品量, d.LJCYL as 累计产油量, d.LJCQL as 累计产气量, d.LJZQL as 累计注汽量, d.LJZSL as 累计注水量, d.BSCPCL as 伴生产品产量, d.BSCPSPL as 伴生产品商品量, d.RCY as 日产油, d.HS as 含水, d.ZJCLF as 直接材料费, d.ZJRLF as 直接燃料费, d.ZJDLF as 直接动力费, d.ZJRYF as 直接人员费, d.QYWZRF as 驱油物注入费, d.QYWZRF_RYF as 驱油物注入费人员费, d.JXZYF as 井下作业费, d.WHXJXZYF as 维护性井下作业费, d.CJSJF as 测井试井费, d.CJSJF_RYF as 测井试井费人员费, d.WHXLF as 维护及修理费, d.WHXLF_RYF as 维护及修理费_人员费, d.CYRCF as 稠油热采费, d.YQCLF as 油气处理费, d.YQCLF_RYF as 油气处理费人员费, d.QTHSF as 轻烃回收费, d.TRQJHF as 天然气净化费, d.YSF as 运输费, d.YSF_RYF as 运输费人员费, d.LYF as 拉油费, d.QTZJF as 其它直接费, d.CKGLF as 厂矿管理费, d.ZYYQCP as 自用油气产品, d.ZJZH as 折旧折耗, d.HSCZCB as 核实操作成本, d.CZCB as 操作成本, d.CZCB_MY as 操作成本美元, d.YXCZCB as 运行操作成本, d.ZJYXCZCB as 直接运行操作成本, d.YXCZCB_MY as 运行操作成本美元, d.ZJYXCZCB_MY as 直接运行操作成本美元, d.QJF as 期间费用, d.KTF as 勘探费用, d.TBSYJ as 特别收益金, d.ZDYXF as 最低运行费, d.ZDYXF_MY as 最低运行费美元, d.SCCB as 生产成本, d.SCCB_MY as 生产成本美元, d.YYCB as 营运成本, d.YYCB_MY as 营运成本美元, d.YYXSSR as 原油销售收入, d.YYXSSJ as 原油销售税金, d.TRQXSSR as 天然气销售收入, d.TRQXSSJ as 天然气销售税金, d.BSCPXSSR as 伴生产品销售收入, d.BSCPXSSJ as 伴生产品销售税金, d.BSCPSHSR as 伴生产品税后收入, d.XSSR as 销售总收入, d.XSSJ as 销售总税金, d.YYZYS as 原油资源税, d.TRQZYS as 天然气资源税, d.ZYS as 资源税, d.SHSR as 税后总收入, d.LR as 利润, d.LR_MY as 利润美元, g.jbmc as 效益类别,(case when d.DJISOPEN=1 then '是' else '否' end) as 是否开井";
                sql += SQLCHK();
                OracleDataAdapter da = new OracleDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "all");
                for (int i = 0; i < ds.Tables["all"].Rows.Count; i++)
                {
                    if (ds.Tables["all"].Rows[i]["采油厂"].ToString() == cyc)
                    {
                        ds.Tables["all"].Rows[i]["采油厂"] = cyc;
                    }
                }
                int rcount = ds.Tables["all"].Rows.Count;
                int ccount = ds.Tables["all"].Columns.Count;
                int hcount = 1;
                string path = string.Empty;
                path = Page.MapPath("~/static/excel/shujuchaxun.xls");
                FpSpread2.ActiveSheetView.OpenExcel(path, "单井油田评价");

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
                FarpointGridChange.FarPointChange(FpSpread2, System.Web.HttpUtility.UrlEncode("单井股份评价", System.Text.Encoding.UTF8) + ".xls");
                FpSpread2.Sheets[0].RowHeader.Visible = false;
                FpSpread2.Sheets[0].ColumnHeader.Visible = false;


            }
            con.Close();

        }

        protected void FpSpread1_SaveOrLoadSheetState(object sender, FarPoint.Web.Spread.SheetViewStateEventArgs e)
        {
            if (e.IsSave)
            {
                Session[e.SheetView.SheetName] = e.SheetView.SaveViewState();
            }
            else
            {
                e.SheetView.LoadViewState(Session[e.SheetView.SheetName]);
            }
            e.Handled = true;
        }
    }
}
