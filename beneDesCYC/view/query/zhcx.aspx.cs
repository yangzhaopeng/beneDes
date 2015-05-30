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
using System.Collections.Generic;

namespace beneDesCYC.view.query
{
    public partial class  zhcx : beneDesCYC.core.UI.corePage
    {
        //string  zdnr = "";
        List<string> heads = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                this.link_tables.SelectedIndex = -1;
            }


        }


        protected void bt_add_Click(object sender, EventArgs e)
        {

            string a = this.link_tables.SelectedItem.Text.ToString() + '.' + this.zd.SelectedItem.Text.ToString();
            string b = this.link_tables.SelectedItem.Value.ToString() + '.' + this.zd.SelectedItem.Value.ToString();

            this.ChoosedJoinTypes.Items.Add(new ListItem("" + a + "", "" + b + ""));

        }






        protected void bt_addzfc_Click(object sender, EventArgs e)
        {
            string typeid = _getParam("targetType");

            // string zd= "";
            string str = "";
            string str1 = "";

            for (int i = 0; i < this.link_tables.Items.Count; i++)
            {
                if (this.link_tables.Items[i].Value == "")
                {
                    return;

                }
                else
                {

                    if (i == 0)
                    {


                        str1 = this.link_tables.Items[i].Value.ToString();


                    }
                    else
                    {

                        if (this.RadioButtonList1.SelectedValue.ToString() == "1")
                        {
                            str1 += " left join " + link_tables.Items[i].Value.ToString() + " on " + link_tables.Items[0].Value.ToString() + ".dj_id " + "=" + link_tables.Items[i].Value.ToString() + ".dj_id";
                        }
                        if (this.RadioButtonList1.SelectedValue.ToString() == "2")
                        {
                            str1 += " left join " + link_tables.Items[i].Value.ToString() + " on " + link_tables.Items[0].Value.ToString() + ".pjdymc" + "=" + link_tables.Items[i].Value.ToString() + ".mc";
                        }
                        if (this.RadioButtonList1.SelectedValue.ToString() == "3")
                        {
                            str1 += " left join " + link_tables.Items[i].Value.ToString() + " on " + link_tables.Items[0].Value.ToString() + ".qkmc" + "=" + link_tables.Items[i].Value.ToString() + ".mc";
                        }
                    }
                }
            }

            //heads.Clear();
            for (int i = 0; i < this.ChoosedJoinTypes.Items.Count; i++)
            {
                if (this.ChoosedJoinTypes.Items[i].Value == "")
                {
                    return;

                }
                else
                {

                    if (i == (this.ChoosedJoinTypes.Items.Count - 1))
                    {
                        string s1 = "";
                        string s = "";

                        s1 = this.ChoosedJoinTypes.Items[i].Text.ToString();
                        s = s1.Substring((s1.IndexOf('.') + 1), (s1.Length - s1.IndexOf('.') - 1));
                        if (s.IndexOf('(') >= 0 && s.Length > 0)
                        {
                            s = s.Substring(0, s.IndexOf('('));
                        }
                        heads.Add(s);
                        str += this.ChoosedJoinTypes.Items[i].Value.ToString() + " as " + s;

                    }
                    else
                    {
                        string s1 = "";
                        string s = "";
                        s1 = this.ChoosedJoinTypes.Items[i].Text.ToString();
                        s = s1.Substring((s1.IndexOf('.') + 1), (s1.Length - s1.IndexOf('.') - 1));
                        if (s.IndexOf('(') >= 0 && s.Length > 0)
                        {
                            s = s.Substring(0, s.IndexOf('('));
                        }
                        heads.Add(s);
                        str += this.ChoosedJoinTypes.Items[i].Value.ToString() + " as " + s + ",";

                    }
                }

            }
            string a = this.link_tables.SelectedValue.ToString() + '.' + this.dp_zd.SelectedValue.ToString();
            if (a == ".")
            {
                return;
            }
            if (zfc.Text == "")
            {

                // zd = "select " + str + "  from  jilincyc.djsj  left join jilincyc.dtstat_djsj on jilincyc.djsj.dj_id = jilincyc.dtstat_djsj.dj_id left join jilincyc.jdstat_djsj on jilincyc.djsj.dj_id = jilincyc.jdstat_djsj.dj_id  where  " + a + " " + dp_relation.SelectedValue.ToString() + " '" + tb_value.Text.ToString() + "' ";

                zfc.Text = "select " + str + "  from  " + str1 + "  where  " + a + " " + dp_relation.SelectedValue.ToString() + " '" + tb_value.Text.ToString() + "'";


            }
            else
            {
                //zd1= this.zfc.Text.Trim();
                zfc.Text += "  " + dp_link.SelectedValue.ToString() + "  " + a + " " + dp_relation.SelectedValue.ToString() + " '" + tb_value.Text.ToString() + "' ";

            }
            //this.zfc.Text= zdnr;
            //for (int i = 0; i <( zd.Length - 1); i++)
            //{
            //    string cc = "";
            //    if ((zd.Length - 1 - i) > 80)
            //    {
            //        cc = zd.Substring(i, 80);
            //        zfc.Text = cc + "\r\n";
            //        i = i + 79;
            //    }
            //    else
            //    {
            //        cc = zd.Substring(i,( zd.Length - 1 - i));
            //       // this.zfc.Rows.Add(cc);
            //        zfc.Text = cc + "\r\n";

            //        i = i + (zd.Length - 1 - i);
            //    }

            //}

        }

        protected void Bt_clear_Click(object sender, EventArgs e)
        {
            this.ChoosedJoinTypes.Items.Clear();
        }
        protected void Bt_clear1_Click(object sender, EventArgs e)
        {
            this.zfc.Text = "";
        }


        protected void link_tables_SelectedIndexChanged(object sender, EventArgs e)
        {
            init();
        }


        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string cyc = Session["cyc"].ToString();
            string typeid = _getParam("targetType");

            OracleConnection con = DB.CreatConnection();
            con.Open();
            try
            {
                this.zd.Items.Clear();

                string sqlbind = "SELECT DISTINCT SJK_CH, SJK_EN,SX FROM DICTIONARY_SEARCH WHERE BS=" + RadioButtonList1.SelectedValue.ToString() + " ORDER BY SX ";
                //string sqlbind = "select distinct " + ddl2.SelectedItem.Value.ToString().Trim() + " from JDSTAT_DJSJ where cyc_id='" + cyc + "' order by " + ddl2.SelectedItem.Value.ToString() + "";
                //string sqlbind = sqlto(RadioButtonList1.SelectedItem.Value.ToString().Trim());
                OracleDataAdapter myda = new OracleDataAdapter(sqlbind, con);
                DataSet myds = new DataSet();
                myda.Fill(myds, "bind");

                link_tables.SelectedIndexChanged -= link_tables_SelectedIndexChanged;

                link_tables.DataSource = myds.Tables["bind"];
                link_tables.DataTextField = "SJK_CH";
                link_tables.DataValueField = "SJK_EN";
                link_tables.DataBind();
                link_tables.SelectedIndexChanged += link_tables_SelectedIndexChanged;
                link_tables.SelectedIndex = myds.Tables["bind"].Rows.Count > 0 ? 0 : -1;
                init();

            }
            catch (Exception a)
            {
                Response.Write("<script>alert('查询条w件有错误+" + a + "')</script>");
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
        protected void searching_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string str = "";

                //for (int i = 0; i < this.zfc1.Items.Count; i++)
                //{
                //    if (this.zfc1.Items.Count == 0)
                //    {
                //        return;

                //    }

                //    else
                //    {
                //        str += this.zfc1.Items[i].Value.ToString();

                //    }

                //}
                if (this.zfc.Text == "")
                {
                    return;

                }

                else
                {
                    str = this.zfc.Text.Trim();

                }

                OracleConnection conn = DB.CreatConnection();
                conn.Open();


                OracleDataAdapter myda = new OracleDataAdapter(str, conn);
                DataSet myds = new DataSet();
                myda.Fill(myds, "jiansuo");
                if (myds.Tables["jiansuo"].Rows.Count == 0)
                {
                    Response.Write("<script>alert('不存在该项记录！')</script>");
                }
                else
                {
                    //this.GridView1.DataSource = myds.Tables["jiansuo"];
                    //GridView1.DataBind();
                    FpSpread1.Visible = true;
                    FpSpread1.DataSource = myds.Tables["jiansuo"];
                    FpSpread1.DataBind();
                    int rcount = myds.Tables["jiansuo"].Rows.Count;
                    //FpSpread1.ActiveSheetView.Columns[3].Width = 137;
                    for (int i = 0; i < rcount; i++)
                    {

                        FpSpread1.ActiveSheetView.Cells[i, 0].HorizontalAlign = HorizontalAlign.Center;

                    }
                    //冻结表头,前3列
                    FpSpread1.ActiveSheetView.FrozenColumnCount = 3;
                    FpSpread1.ActiveSheetView.AllowPage = true;
                }


                conn.Close();
            }
            catch (Exception a)
            {
                Response.Write("<script>alert('请输入查询条件+" + a + "')</script>");
            }
        }


        protected void dp_relation_Click(object sender, EventArgs e)
        {
            OracleConnection con = DB.CreatConnection();
            con.Open();
            try
            {
                string sqlbind = "SELECT  ZDLX FROM DICTIONARY_SEARCH WHERE ZD_EN ='" + dp_zd.SelectedValue.ToString() + "'";
                //string sqlbind = "select distinct " + ddl2.SelectedItem.Value.ToString().Trim() + " from JDSTAT_DJSJ where cyc_id='" + cyc + "' order by " + ddl2.SelectedItem.Value.ToString() + "";
                //string sqlbind = sqlto(RadioButtonList1.SelectedItem.Value.ToString().Trim());
                OracleDataAdapter myda = new OracleDataAdapter(sqlbind, con);
                DataSet myds = new DataSet();
                myda.Fill(myds, "bind");
                if (myds != null && myds.Tables[0].Rows.Count == 1 && myds.Tables[0].Rows[0][0].ToString() == "varchar2")
                {
                    //dp_relation.SelectedValue.ToString()=="=";

                }

            }
            catch (Exception a)
            {
                Response.Write("<script>alert('查询条件有错误+" + a + "')</script>");
            }

            con.Close();
        }
        protected void daochu_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                if (this.zfc.Rows == 0)
                {//注意防止空异常！
                    Response.Write("<script>alert('请选择查询条件')</script>");
                }
                else
                {
                    string str = "";

                    //for (int i = 0; i < this.zfc1.Items.Count; i++)
                    //{
                    //    if (this.zfc1.Items.Count == 0)
                    //    {
                    //        return;

                    //    }

                    //    else
                    //    {
                    //        str += this.zfc1.Items[i].Value.ToString();

                    //    }

                    //}
                    if (this.zfc.Text == "")
                    {
                        return;

                    }

                    else
                    {
                        str = this.zfc.Text.Trim();

                    }

                    OracleConnection conn = DB.CreatConnection();
                    conn.Open();


                    for (int i = 0; i < this.ChoosedJoinTypes.Items.Count; i++)
                    {
                        if (this.ChoosedJoinTypes.Items[i].Value == "")
                        {
                            return;

                        }
                        else
                        {

                            if (i == (this.ChoosedJoinTypes.Items.Count - 1))
                            {
                                string s1 = "";
                                string s = "";
                                s1 = this.ChoosedJoinTypes.Items[i].Text.ToString();
                                s = s1.Substring((s1.IndexOf('.') + 1), (s1.Length - s1.IndexOf('.') - 1));
                                heads.Add(s);
                                //str += this.ChoosedJoinTypes.Items[i].Value.ToString() + " as " + s;

                            }
                            else
                            {
                                string s1 = "";
                                string s = "";
                                s1 = this.ChoosedJoinTypes.Items[i].Text.ToString();
                                s = s1.Substring((s1.IndexOf('.') + 1), (s1.Length - s1.IndexOf('.') - 1));
                                heads.Add(s);
                                //str += this.ChoosedJoinTypes.Items[i].Value.ToString() + " as " + s + ",";

                            }
                        }

                    }
                    //con.Open();
                    //string sql = "select j.bny as 开始年月, j.eny as 结束年月, j.JH as 井号, (case when j.jb=0 then '正常井' when j.jb=1 then '内部捞油井' when j.jb=2 then '外部捞油井' else '停产井' end) as 井别, j.SSYT as 所属油田, j.YCLX as 油藏类型, j.QK as 区块, j.PJDY as 评价单元, y.yqlxmc as 油气类型, x.xsypmc as 销售油品, j.SCSJ as 生产时间, j.JKCYL as 产液量, j.HSCYL as 产油量, j.HSCQL as 产气量, j.ZSL as 注水量, j.ZQL as 注汽量, j.YQL as 油气量, j.NBHSYQL as 内部核实油气量, j.YQSPL as 油气商品量, j.DTL as 油气商品量桶, j.YYSPL as 原油商品量, j.TRQSPL as 天然气商品量, j.LJCYL as 累计产油量, j.LJCQL as 累计产气量, j.LJZQL as 累计注汽量, j.LJZSL as 累计注水量, j.BSCPCL as 伴生产品产量, j.BSCPSPL as 伴生产品商品量, j.RCY as 日产油, j.HS as 含水, j.ZJCLF as 直接材料费, j.ZJRLF as 直接燃料费, j.ZJDLF as 直接动力费, j.ZJRYF as 直接人员费, j.QYWZRF as 驱油物注入费, j.JXZYF as 井下作业费, j.WHXJXZYF as 其中维护性井下作业费, j.CJSJF as 测井试井费, j.WHXLF as 维护及修理费, j.CYRCF as 稠油热采费, j.YQCLF as 油气处理费, j.QTHSF as 轻烃回收费, j.TRQJHF as 天然气净化费, j.YSF as 运输费, j.LYF as 其中拉油费, j.QTZJF as 其它直接费, j.CKGLF as 厂矿管理费, j.ZYYQCP as 自用油气产品, j.ZJZH as 折旧折耗, j.HSCZCB as 核实操作成本, j.CZCB as 操作成本, j.CZCB_MY as 操作成本美元, j.QJF as 期间费用, j.KTF as 勘探费用, j.ZDYXF as 最低运行费, j.ZDYXF_MY as 最低运行费美元, j.SCCB as 生产成本, j.SCCB_MY as 生产成本美元, j.YYCB as 营运成本, j.YYCB_MY as 营运成本美元, j.YYXSSR as 原油销售收入, j.YYXSSJ as 原油销售税金, j.TRQXSSR as 天然气销售收入, j.TRQXSSJ as 天然气销售税金, j.BSCPXSSR as 伴生产品销售收入, j.BSCPXSSJ as 伴生产品销售税金, j.BSCPSHSR as 伴生产品税后收入, j.XSSR as 销售总收入, j.XSSJ as 销售总税金, j.YYZYS as 原油资源税, j.TRQZYS as 天然气资源税, j.ZYS as 资源税, j.SHSR as 税后总收入, j.LR as 利润, j.LR_MY as 利润美元, g.xymc as 效益类别, (case when j.DJISOPEN=1 then '是' else '否' end) as 是否开井";
                    //sql += SQLCHK();
                    OracleDataAdapter da = new OracleDataAdapter(str, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "all");
                    int rcount = ds.Tables["all"].Rows.Count;
                    int ccount = ds.Tables["all"].Columns.Count;
                    int hcount = 1;
                    string path = string.Empty;
                    path = Page.MapPath("~/excel/chaxun.xls");
                    FpSpread2.ActiveSheetView.OpenExcel(path, "综合查询");
                    FpSpread2.ActiveSheetView.Rows.Count = 1;
                    FpSpread2.ActiveSheetView.Columns.Count = heads.Count;
                    for (int i = 0; i < heads.Count; i++)
                    {
                        FpSpread2.ActiveSheetView.Cells[0, i].Value = heads[i];
                        FpSpread2.ActiveSheetView.Cells[0, i].Font.Size = 9;
                        FpSpread2.ActiveSheetView.Columns[i].Width = (heads[i].Length + 1) * 20;
                    }

                    //int rows = FpSpread2.ActiveSheetView.Rows.Count;
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
                    FpSpread2.ActiveSheetView.FrozenColumnCount = 3;
                    conn.Close();
                    FpSpread2.Sheets[0].RowHeader.Visible = true;
                    FpSpread2.Sheets[0].ColumnHeader.Visible = true;
                    FarpointGridChange.FarPointChange(FpSpread2, System.Web.HttpUtility.UrlEncode("综合查询", System.Text.Encoding.UTF8) + ".xls");
                    FpSpread2.Sheets[0].RowHeader.Visible = false;
                    FpSpread2.Sheets[0].ColumnHeader.Visible = false;
                }
            }
            catch (Exception a)
            {
                Response.Write("<script>alert('导出失败+" + a + "')</script>");
            }


        }

        private void init()
        {
            OracleConnection con = null;
            try
            {
                con = DB.CreatConnection();
                con.Open();
                string sqlbind = "SELECT DISTINCT ZD_CH, ZD_EN FROM DICTIONARY_SEARCH WHERE SJK_EN ='" + link_tables.SelectedValue.ToString() + "'";
                //string sqlbind = "select distinct " + ddl2.SelectedItem.Value.ToString().Trim() + " from JDSTAT_DJSJ where cyc_id='" + cyc + "' order by " + ddl2.SelectedItem.Value.ToString() + "";
                //string sqlbind = sqlto(RadioButtonList1.SelectedItem.Value.ToString().Trim());
                OracleDataAdapter myda = new OracleDataAdapter(sqlbind, con);
                DataSet myds = new DataSet();
                myda.Fill(myds, "bind");
                zd.DataSource = myds.Tables["bind"];
                zd.DataTextField = "ZD_CH";
                zd.DataValueField = "ZD_EN";
                zd.DataBind();

                dp_zd.DataSource = myds.Tables["bind"];
                dp_zd.DataTextField = "ZD_CH";
                dp_zd.DataValueField = "ZD_EN";
                dp_zd.DataBind();

            }
            catch (Exception a)
            {
                Response.Write("<script>alert('查询条件有错误+" + a + "')</script>");
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

    }

}
