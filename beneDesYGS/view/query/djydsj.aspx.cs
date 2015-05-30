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
    public partial class djydsj : beneDesYGS.core.UI.corePage
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
            this.FpSpread1.Sheets[0].OpenExcel(path, "单井月度数据");
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
            string cyc = _getParam("CYC");
            string cyc_id = cyc;
            string cqc_id = cyc;
            if (cyc_id.Contains(','))
            {
                cyc_id = cyc.Split(',')[0];
                cqc_id = cyc.Split(',')[1];
            }

            //SqlHelper conn = new SqlHelper();
            //OracleConnection con = conn.GetConn();
            //con.Open();
            string sql = "select s.ny as 年月, s.JH as 井号, s.DEP_ID as 采油厂,(case when s.jb=0 then '正常井' when s.jb=1 then '内部捞油井' when s.jb=2 then '外部捞油井' else '停产进' end) as 井别, s.SSYT as 所属油田, s.YCLX as 油藏类型, s.QK as 区块, s.PJDY as 评价单元, y.yqlxmc as 油气类型, x.xsypmc as 销售油品, s.SCSJ as 生产时间, s.JKCYL as 月产液量, s.HSCYL as 月产油量, s.HSCQL as 月产气量, s.ZSL as 月注水量, s.ZQL as 月注汽量, s.YQL as 月油气量, s.NBHSYQL as 内部核实油气量, s.YQSPL as 油气商品量, s.DTL as 油气商品量桶, s.YYSPL as 原油商品量, s.TRQSPL as 天然气商品量, s.LJCYL as 累计产油量, s.LJCQL as 累计产气量, s.LJZQL as 累计注汽量, s.LJZSL as 累计注水量, s.BSCPCL as 伴生产品产量, s.BSCPSPL as 伴生产品商品量, s.RCY as 日产油, s.HS as 含水, s.ZJCLF as 直接材料费, s.ZJRLF as 直接燃料费, s.ZJDLF as 直接动力费, s.ZJRYF as 直接人员费, s.QYWZRF as 驱油物注入费, s.QYWZRF_RYF as  驱油物注入费人员费, s.JXZYF as 井下作业费, s.WHXJXZYF as 维护性井下作业费, s.CJSJF as 测井试井费, s.CJSJF_RYF as 测井试井费人员费, s.WHXLF as 维护及修理费, s.WHXLF_RYF as 维护及修理费人员费, s.CYRCF as 稠油热采费, s.YQCLF as 油气处理费, s.YQCLF_RYF as 油气处理费人员费, s.QTHSF as 轻烃回收费, s.TRQJHF as 天然气净化费, s.YSF as 运输费, s.YSF_RYF as 运输费人员费, s.LYF as 拉油费, s.QTZJF as 其它直接费, s.CKGLF as 厂矿管理费, s.ZYYQCP as 自用油气产品, s.ZJZH as 折旧折耗, s.HSCZCB as 核实操作成本, s.CZCB as 操作成本, s.CZCB_MY as 操作成本美元, s.QJF as 期间费用, s.KTF as 勘探费用, s.TBSYJ as 特别收益金, s.SCCB as 生产成本, s.SCCB_MY as 生产成本美元, s.YYCB as 营运成本, s.YYCB_MY as 营运成本美元, s.YYXSSR as 原油销售收入, s.YYXSSJ as 原油销售税金, s.TRQXSSR as 天然气销售收入, s.TRQXSSJ as 天然气销售税金, s.BSCPXSSR as 伴生产品销售收入, s.BSCPXSSJ as 伴生产品销售税金, s.BSCPSHSR as 伴生产品税后收入, s.XSSR as 销售总收入, s.XSSJ as 销售总税金, s.YYZYS as 原油资源税, s.TRQZYS as 天然气资源税, s.ZYS as 资源税, s.SHSR as 税后总收入, s.LR as 利润, s.LR_MY as 利润美元, (case when s.DJISOPEN=1 then '是' else '否' end) as 是否开井";

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
            #region 绑定数据到Fpspread

            //if (myds.Tables["jiansuo"].Rows.Count == 0)
            if (myds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<script>alert('不存在该项记录！')</script>");
            }
            else
            {
                //for (int i = 0; i < myds.Tables["jiansuo"].Rows.Count; i++)
                //{

                //    myds.Tables["jiansuo"].Rows[i]["采油厂"] = cyc_id;

                //}

                string path = Page.MapPath("~/static/excel/shujuchaxun.xls");
                this.FpSpread1.Sheets[0].OpenExcel(path, "单井月度数据");

                //FpSpread1.Visible = true;
                this.FpSpread1.Sheets[0].RowHeader.Visible = false;
                this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                FpSpread1.Width = Unit.Pixel(8233);

                //FpSpread1.DataSource = myds.Tables["jiansuo"];
                //FpSpread1.DataBind();
                int hcount = 1;
                int rcount = myds.Tables[0].Rows.Count;
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
                for (int colNum = 11; colNum < ccount; colNum++)
                {
                    FpSpread1.Sheets[0].Columns[colNum].Width = 60;
                }
                FpSpread1.Sheets[0].Columns[4].Width = 135;
                FpSpread1.Sheets[0].Columns[5].Width = 148;
                FpSpread1.Sheets[0].Columns[6].Width = 148;
                FpSpread1.Sheets[0].Columns[7].Width = 135;
                FpSpread1.Sheets[0].Columns[8].Width = 135;

                //FpSpread1.Sheets[0].Cells[

                //int ccount = myds.Tables["jiansuo"].Columns.Count;
                //int rcount = myds.Tables["jiansuo"].Rows.Count;
                //for (int i = 0; i < rcount; i++)
                //{

                //    FpSpread1.ActiveSheetView.Cells[i, 0].HorizontalAlign = HorizontalAlign.Center;

                //}
                //for (int j = 0; j < ccount; j++)
                //{
                //    this.FpSpread1.ActiveSheetView.Columns[j].Width = 140;
                //    this.FpSpread1.ActiveSheetView.Columns[j].Font.Size = 9;
                //}


                //冻结表头,前2列
                //FpSpread1.ActiveSheetView.FrozenColumnCount = 1;
                //FpSpread1.ActiveSheetView.AllowPage = true;

            }

            #endregion

            //_return(true, "", total);
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
            sql += " from STAT_DJYDSJ s,yqlx_info y,xsyp_info x  where  s.yqlx=y.yqlxdm and s.xsyp=x.xsypdm and  ";

            sql += "(regexp_like(dep_id,'" + cyc_id + "') or regexp_like(dep_id,'" + cqc_id + "')) ";

            if (xzType != "null" && xzType2 != "null" && xzType3 != "null" && xzType4 != "null" && xzType5 != "null")
            {
                //string sql = "SELECT NY,DJ_ID,JH, QK,PJDY,YQLX, XSYP, JB, SSYT,YCLX,ZHZQLC,SCSJ,ZJCLF, ZJRLF,ZJDLF,ZJRYF,QYWZRF,JXZYF,WHXJXZYF,CJSJF, WHXLF,CYRCF,YQCLF,QTHSF,TRQJHF,YSF,LYF,QTZJF,CKGLF, ZYYQCP,ZJZH,QJF,KTF,ZDYXF, ZDYXF_MY,HSCZCB,CZCB,CZCB_MY,(Case when YQSPL = 0 then 0 else CZCB / YQSPL end) as DWCZCB, SCCB, SCCB_MY,YYCB,YYCB_MY,YYXSSR,YYXSSJ,TRQXSSR,TRQXSSJ,BSCPXSSR, BSCPXSSJ,BSCPSHSR,XSSR,XSSJ,";
                //sql = sql + "YYZYS,TRQZYS,  ZYS, SHSR,LR, LR_MY, GSXYJB,ZDYXYJB,JKCYL,HSCYL,HSCQL, ZSL,ZQL, YQL,NBHSYQL,YQSPL,DTL,YYSPL,TRQSPL,LJCYL, LJCQL,LJZQL, LJZSL,  BSCPCL,BSCPSPL, RCY, HS, YQB, LJYQB,  CZCBJB,  RCYJB, HSJB,   YQBJB,DJISOPEN,QKXYJB, PJDYXYJB, CZCBDLJB  FROM STAT_DJYDSJ ";
                //return sql;
                sql += " and  " + xzType + "='" + SJCX + "' " + ROAND + " " + xzType2 + " ='" + SJCX2 + "' " + ROAND2 + " " + xzType3 + " ='" + SJCX3 + "' " + ROAND3 + " " + xzType4 + " ='" + SJCX4 + "' " + ROAND4 + " " + xzType5 + " ='" + SJCX5 + "'";
                return sql;
            }
            else
            {
                if (xzType != "null" && xzType2 != "null" && xzType3 != "null" && xzType4 != "null")
                {
                    sql += " and  " + xzType + "='" + SJCX + "' " + ROAND + " " + xzType2 + " ='" + SJCX2 + "' " + ROAND2 + " " + xzType3 + " ='" + SJCX3 + "' " + ROAND3 + " " + xzType4 + " ='" + SJCX4 + "'";
                    return sql;
                }
                else
                {
                    if (xzType != "null" && xzType2 != "null" && xzType3 != "null")
                    {
                        sql += " and   " + xzType + "='" + SJCX + "' " + ROAND + " " + xzType2 + " ='" + SJCX2 + "' " + ROAND2 + " " + xzType3 + " ='" + SJCX3 + "'";
                        return sql;
                    }
                    else
                    {
                        if (xzType != "null" && xzType2 != "null")
                        {
                            sql += " and   " + xzType + "='" + SJCX + "' " + ROAND + " " + xzType2 + " ='" + SJCX2 + "'";
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
                string sql = "select s.ny as 年月, s.JH as 井号, s.DEP_ID as 采油厂,(case when s.jb=0 then '正常井' when s.jb=1 then '内部捞油井' when s.jb=2 then '外部捞油井' else '停产进' end) as 井别, s.SSYT as 所属油田, s.YCLX as 油藏类型, s.QK as 区块, s.PJDY as 评价单元, y.yqlxmc as 油气类型, x.xsypmc as 销售油品, s.SCSJ as 生产时间, s.JKCYL as 月产液量, s.HSCYL as 月产油量, s.HSCQL as 月产气量, s.ZSL as 月注水量, s.ZQL as 月注汽量, s.YQL as 月油气量, s.NBHSYQL as 内部核实油气量, s.YQSPL as 油气商品量, s.DTL as 油气商品量桶, s.YYSPL as 原油商品量, s.TRQSPL as 天然气商品量, s.LJCYL as 累计产油量, s.LJCQL as 累计产气量, s.LJZQL as 累计注汽量, s.LJZSL as 累计注水量, s.BSCPCL as 伴生产品产量, s.BSCPSPL as 伴生产品商品量, s.RCY as 日产油, s.HS as 含水, s.ZJCLF as 直接材料费, s.ZJRLF as 直接燃料费, s.ZJDLF as 直接动力费, s.ZJRYF as 直接人员费, s.QYWZRF as 驱油物注入费, s.QYWZRF_RYF as  驱油物注入费人员费, s.JXZYF as 井下作业费, s.WHXJXZYF as 维护性井下作业费, s.CJSJF as 测井试井费, s.CJSJF_RYF as 测井试井费人员费, s.WHXLF as 维护及修理费, s.WHXLF_RYF as 维护及修理费人员费, s.CYRCF as 稠油热采费, s.YQCLF as 油气处理费, s.YQCLF_RYF as 油气处理费人员费, s.QTHSF as 轻烃回收费, s.TRQJHF as 天然气净化费, s.YSF as 运输费, s.YSF_RYF as 运输费人员费, s.LYF as 拉油费, s.QTZJF as 其它直接费, s.CKGLF as 厂矿管理费, s.ZYYQCP as 自用油气产品, s.ZJZH as 折旧折耗, s.HSCZCB as 核实操作成本, s.CZCB as 操作成本, s.CZCB_MY as 操作成本美元, s.QJF as 期间费用, s.KTF as 勘探费用, s.TBSYJ as 特别收益金, s.SCCB as 生产成本, s.SCCB_MY as 生产成本美元, s.YYCB as 营运成本, s.YYCB_MY as 营运成本美元, s.YYXSSR as 原油销售收入, s.YYXSSJ as 原油销售税金, s.TRQXSSR as 天然气销售收入, s.TRQXSSJ as 天然气销售税金, s.BSCPXSSR as 伴生产品销售收入, s.BSCPXSSJ as 伴生产品销售税金, s.BSCPSHSR as 伴生产品税后收入, s.XSSR as 销售总收入, s.XSSJ as 销售总税金, s.YYZYS as 原油资源税, s.TRQZYS as 天然气资源税, s.ZYS as 资源税, s.SHSR as 税后总收入, s.LR as 利润, s.LR_MY as 利润美元, (case when s.DJISOPEN=1 then '是' else '否' end) as 是否开井";
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
                FpSpread2.ActiveSheetView.OpenExcel(path, "单井月度数据");

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
                FarpointGridChange.FarPointChange(FpSpread2, System.Web.HttpUtility.UrlEncode("单井月度数据", System.Text.Encoding.UTF8) + ".xls");
                FpSpread2.Sheets[0].RowHeader.Visible = false;
                FpSpread2.Sheets[0].ColumnHeader.Visible = false;


            }
            con.Close();

        }

    }
}
