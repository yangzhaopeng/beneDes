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
using System.Drawing;
using System.Web.SessionState;


namespace beneDesCYC.view.month
{

    public partial class zxzft : beneDesCYC.core.UI.corePage
    {
        String source_id;
        String ny;
        String ft_type = "ZXZ";
        //OracleConnection conn = DB.CreatConnection();
        //SqlHelper conn = new SqlHelper();
        //OracleConnection conb = conn.GetConn();
        protected SqlHelper DB = new SqlHelper();

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                //  this.list1.Visible = false;

                string Tbny = _getParam("Date");
                //  txtdate.Text = Session["date"].ToString();//么有第一次pageload，ispostback=false从来没有过
                // this.FpSpread1.Visible = false;
                //  Session["saveFlag"] = false;//设置标记让spread不能保存状态
                //  this.listcyc.Visible = false;
                Btn_Bind();
            }
        }

        public void Btn_Bind()
        {
            string Tbny = _getParam("Date");
          //  this.FT.Visible = true;
            OracleConnection conn = DB.GetConn();
            String sqlstr = "select dep_name, dep_id from department where dep_type='ZYQ' and parent_id='" + Session["cyc_id"].ToString() + "' order by dep_id";
            OracleDataAdapter mydata = new OracleDataAdapter(sqlstr, conn);


            DataSet myds = new DataSet();
            conn.Open();

            mydata.Fill(myds, "dep_namessss");
            this.zyqddl.DataSource = myds;
            zyqddl.DataValueField = myds.Tables[0].Columns[0].ToString();
            //上面两句话效果一样
            //zyqddl.DataValueField = myds.Tables[0].Columns[1].ToString();
            zyqddl.DataBind();
            this.FT.Visible = true;

            String depidsel = "select dep_id from department where dep_name= '" + myds.Tables[0].Rows[0][0].ToString() + "' and parent_id='" + Session["cyc_id"].ToString() + "' ";
            String sqldep = "select distinct dep_name from view_alldep  where parent_id in (" + depidsel + ") and ny='" + Tbny + "' and dep_type='" + ft_type + "' and cyc_id='" + Session["cyc_id"].ToString() + "' ";
            OracleDataAdapter mydata2 = new OracleDataAdapter(sqldep, conn);
            DataSet mydep = new DataSet();

            mydata2.Fill(mydep, "dep_name2");
            this.zxzchk.DataSource = mydep.Tables["dep_name2"];
            zxzchk.DataTextField = "dep_name";//2009.4.29修改

            zxzchk.DataBind();
            conn.Close();

        }


        protected void zyqddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Tbny = _getParam("Date");
           // listzxz.Items.Clear();
            OracleConnection conn = DB.GetConn();
            this.FT.Visible = true;
            // 出现对应的中心站
            String depidsel = "select dep_id from department where dep_name= '" + zyqddl.SelectedValue + "' and parent_id='" + Session["cyc_id"].ToString() + "' ";
            String sqldep = "select distinct dep_name from view_alldep  where parent_id in (" + depidsel + ") and ny='" + Tbny + "' and dep_type='" + ft_type + "' and cyc_id='" + Session["cyc_id"].ToString() + "' ";
            OracleDataAdapter mydata2 = new OracleDataAdapter(sqldep, conn);
            DataSet mydep = new DataSet();
            conn.Open();
            mydata2.Fill(mydep, "dep_name2");
            this.zxzchk.DataSource = mydep.Tables["dep_name2"];
            //zxzchk.DataValueField = "dep_name";
            zxzchk.DataTextField = "dep_name";//2009.4.29修改

            zxzchk.DataBind();
            conn.Close();

        }
        protected void FT_Click(object sender, EventArgs e)
        {
            //    Session["saveFlag"] = false;//这一次不save session
            string Tbny = _getParam("Date");
            if (Tbny == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入年月!');</script>");

            }
            else
            {

                //listcyc.Items.Clear();

                //判断选择的是不是为空
                int Countselect = 0;
                foreach (ListItem l in zxzchk.Items)
                {
                    if (l.Selected)
                        Countselect++;
                }
                if (Countselect == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请选择中心站!');</script>");

                    this.FT.Visible = true;
                }
                else
                {

                    xianshiresult();
                }

             //   this.zyqchklist.Visible = true;
                //对每一个source_id 进行分摊
                // ny = txtdate.Text;
                foreach (ListItem l in this.zxzchk.Items)
                {

                    if (l.Selected)
                    {
                        OracleConnection conn = DB.GetConn();
                      //  listzxz.Items.Add(new ListItem(l.Value));
                        String sqlzxz = "select source_id from view_alldep where ny='" + Tbny + "' and dep_type='" + ft_type + "' and dep_name='" + l.Value + "' and cyc_id='" + Session["cyc_id"].ToString() + "' ";
                        OracleCommand mycomm = new OracleCommand(sqlzxz, conn);
                        conn.Open();
                        OracleDataReader myReader;
                        myReader = mycomm.ExecuteReader();
                        while (myReader.Read())
                        {
                            string cycid = Session["cyc_id"].ToString();
                            source_id = myReader[0].ToString();
                            OracleCommand mycomm2 = new OracleCommand();

                            mycomm2.Connection = conn;
                            mycomm2.CommandType = CommandType.StoredProcedure;
                            mycomm2.CommandText = "owcbspkg_feeft.up_ggfyft('" + Tbny + "','" + ft_type + "','" + source_id + "','" + cycid + "')";
                            mycomm2.ExecuteNonQuery();
                        }
                        conn.Close();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('分摊成功!');</script>");
                    }

                }
            }
        }
        //protected void listcyc_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.listcyc.Visible = true;
        //    //String depname = list1.SelectedItem.Value;
        //    String depname = listcyc.SelectedItem.Value;

        //    //String sqlstr = "select source_id from view_alldep where ny='" + txtdate.Text + "' and dep_name='" + depname + "'";

        //    //OracleCommand mycomm = new OracleCommand(sqlstr, conn);
        //    //conn.Open();
        //    //OracleDataReader myReader;
        //    //myReader = mycomm.ExecuteReader();
        //    //while (myReader.Read())
        //    //{
        //    //    string sql = "select ny as 年月, jh as 井号,  yqlx as 油气类型, fee_class as 费用大类,  class_name as 费用类名,  fee_name as 费用名称,  fee as 费用 from view_djfy where  ny = '" + txtdate.Text + "' and ft_type = 'CYC' and source_id = '" + myReader[0].ToString() + "'";
        //    //    OracleDataAdapter temada = new OracleDataAdapter(sql, conn);
        //    //    DataSet temset = new DataSet();
        //    //    temada.Fill(temset);
        //    //    FpSpread1.DataSource = temset.Tables[0];
        //    //    FpSpread1.DataBind();
        //    //}
        //    //conn.Close();

        //    conn.Open();
        //    OracleCommand mycomm = new OracleCommand();
        //    mycomm.Connection = conn;
        //    mycomm.CommandType = CommandType.StoredProcedure;
        //    mycomm.CommandText = "cross_fee1('" + txtdate.Text + "','" + Session["cyc"].ToString() + "','" + ft_type + "')";
        //    int reflectrows = Convert.ToInt32(mycomm.ExecuteNonQuery().ToString());
        //    if (reflectrows == 0)
        //    {
        //        Response.Write("<script>alert('不存在该项记录!')</script>");
        //    }
        //    else
        //    {

        //        string sql = "select ny as 年月,jh as 井号,zyq as 作业区,qk as 区块,zxz as 中心站,zrz as 自然站,zjclf as 直接材料费元,qtclf as 其它材料费元,zjrlf as 直接燃料费元,zjdlf as 直接动力费元,qtdlf as 其它动力费元,qywzrf as 驱油物注入费元,qywzrf_ryf 其中人员费元,cszyf as 措施作业费元,";
        //        sql = sql + "whxzylwf as 维护性作业劳务费元,sjzyf as 水井作业费元,cjcsf as 测井试井费元,ycjcf as 油藏监测费元,whxlf as 维护及修理费元,yqclf as 油气处理费元,yqclf_ryf as 其中_人员费元,qthsf as 轻烃回收费元,ysf as 运输费_拉油费元,";
        //        sql = sql + "lyf as 拉油费元,qtzjf as 其他直接费元,ckglf as 厂矿管理费元,zjryf as 直接人员费元,zyyqcp as 减_自用产品元,zjzh as 折旧折耗元  from fee_cross  where ny='" + (txtdate.Text).Trim() + "' and cyc_id='" + Session["cyc"].ToString() + "'";
        //        OracleDataAdapter temada = new OracleDataAdapter(sql, conn);
        //        DataSet myds = new DataSet();
        //        temada.Fill(myds, "show");
        //        //FpSpread1.DataSource = temset.Tables[0];
        //        //FpSpread1.DataBind();
        //        int rcount = myds.Tables["show"].Rows.Count;
        //        int ccount = myds.Tables["show"].Columns.Count;
        //        int hcount = 1;

        //        //FpSpread1.ActiveSheetView.AllowPage = false;
        //        string path;
        //        path = Page.MapPath("~/excel/feiyongfentan.xls");

        //        FpSpread1.Sheets[0].OpenExcel(path, "费用");

        //        FpSpread1.Sheets[0].ColumnHeader.Visible = false;
        //        FpSpread1.Sheets[0].RowHeader.Visible = false;
        //        //FpSpread1.Sheets[0].AllowPage = true;

        //        FpSpread1.Sheets[0].AddRows(hcount, rcount);
        //        for (int i = 0; i < rcount; i++)
        //        {
        //            for (int j = 0; j < ccount; j++)
        //            {
        //                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = myds.Tables["show"].Rows[i][j].ToString();
        //                FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
        //            }
        //        }

        //        //冻结表头及前2列
        //        FpSpread1.ActiveSheetView.FrozenRowCount = 1;
        //        FpSpread1.ActiveSheetView.FrozenColumnCount = 2;

        //        //设置显示格式:年月居中,文本居左,数值居右
        //        for (int i = 1; i < rcount + 1; i++)
        //        {
        //            FpSpread1.ActiveSheetView.Cells[i, 0].HorizontalAlign = HorizontalAlign.Center;
        //        }
        //        for (int i = 1; i < rcount + 1; i++)
        //        {
        //            for (int j = 1; j < 6; j++)
        //            {
        //                FpSpread1.ActiveSheetView.Cells[i, j].HorizontalAlign = HorizontalAlign.Left;
        //            }
        //        }
        //        for (int i = 1; i < rcount + 1; i++)
        //        {
        //            for (int j = 6; j < ccount; j++)
        //            {
        //                FpSpread1.ActiveSheetView.Cells[i, j].HorizontalAlign = HorizontalAlign.Right;
        //            }
        //        }
        //        // 
        //    }
        //    conn.Close();
        //}

        private void xianshiresult()
        {
            //    //this.listcyc.Visible = true;
            ////    this.FpSpread1.Visible = true;
            //   // this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

        }
        protected void QX_Click(object sender, EventArgs e)
        {
            foreach (ListItem l in this.zxzchk.Items)
            {
                l.Selected = true;
            }

        }
        //protected void FpSpread1_SaveOrLoadSheetState(object sender, FarPoint.Web.Spread.SheetViewStateEventArgs e)
        //{
        //    if (e.IsSave)//最后save
        //    {
        //        if (bool.Parse(Session["saveFlag"].ToString()) != true)//在分摊、选厂的时候，清空此session.能清空表格最好了！
        //        {
        //            Session.Remove("caiyouchangviewstate");
        //            Session["saveFlag"] = true;
        //            FpSpread1.Sheets[0].Visible = false;
        //        }
        //        else
        //        {
        //            Session["caiyouchangviewstate"] = e.SheetView.SaveViewState();
        //        }
        //    }
        //    else
        //    {
        //        e.SheetView.LoadViewState(Session["caiyouchangviewstate"]);//e.SheetView.SheetName
        //    }
        //    e.Handled = true;
        //}
    }

}
