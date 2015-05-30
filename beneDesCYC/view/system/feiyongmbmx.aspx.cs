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
using beneDesCYC.core;

namespace beneDesCYC.view.system
{
    public partial class feiyongmbmx : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                DG_DataBind();
                DG.Attributes.Add("BorderColor", "#83B1D3");
                Btn_Bind();

            }
        }

        public void DG_DataBind()
        {
            SqlHelper conn = new SqlHelper();
            OracleConnection con = conn.GetConn();
            String sql = "select templet_code,templet_name,templet_type,use_type,templet_level,templet_tag from fee_templet";
            int rows = 0;
            try
            {
                con.Open();
                OracleDataAdapter da = new OracleDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "fee_templet");

                rows = ds.Tables["fee_templet"].Rows.Count;//行数,记录数
                this.DG.DataSource = ds.Tables["fee_templet"];
                this.DG.DataBind();

            }
            catch (OracleException Error)
            {
                string CuoWu = "错误!" + Error.Message.ToString();
                Response.Write(CuoWu);
            }
            con.Close();
        }

        public void Btn_Bind()
        {
            SqlHelper conn = new SqlHelper();
            OracleConnection con = conn.GetConn();
            con.Open();
            try
            {
                String sql = "select class_name,class_code from fee_class where fee_type='0' ";

                OracleDataAdapter da = new OracleDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "fee_class");
                this.listbox1.DataSource = ds;

                listbox1.DataTextField = "class_name";
                //listbox1.DataValueField = "class_code";
                listbox1.DataValueField = "class_code";
                this.listbox1.DataBind();

                String sqlfeename = "select fee_name, fee_code,fee_class from fee_name";


                OracleDataAdapter myda = new OracleDataAdapter(sqlfeename, con);
                DataSet myds = new DataSet();
                myda.Fill(myds, "name");
                chklist1.DataSource = myds;
                chklist1.DataTextField = "fee_name";
                chklist1.DataValueField = "fee_code";
                chklist1.DataBind();
            }
            catch (OracleException Error)
            {
                string CuoWu = "错误!" + Error.Message.ToString();
                Response.Write(CuoWu);
            }
            con.Close();

        }
        protected void listbox1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            SqlHelper conn = new SqlHelper();
            OracleConnection con = conn.GetConn();
            try
            {
                String feeclass = this.listbox1.SelectedItem.Text;

                String sql1 = "select class_code from fee_class where class_name= '" + feeclass + "' and fee_type='0'";
                String sqlfeename = "select fee_name,fee_code from fee_name where fee_class in(" + sql1 + ")";
                con.Open();
                OracleDataAdapter myda = new OracleDataAdapter(sqlfeename, con);
                DataSet myds = new DataSet();
                myda.Fill(myds, "name");
                chklist1.DataSource = myds;
                chklist1.DataTextField = "fee_name";
                chklist1.DataValueField = "fee_code";
                chklist1.DataBind();

                if (test.Text == "")
                {
                    Response.Write("<script>alert('请点击列表中的模板')</script>");
                }
                else
                {
                    string sqlfee = "select fee_code from fee_templet_detail where templet_code='" + test.Text + "' and fee_class='" + this.listbox1.SelectedItem.Value + "'";
                    OracleDataAdapter feeda = new OracleDataAdapter(sqlfee, con);
                    DataSet feeds = new DataSet();
                    feeda.Fill(feeds, "xianshi");
                    for (int i = 0; i < feeds.Tables["xianshi"].Rows.Count; i++)
                    {
                        foreach (ListItem l in chklist1.Items)
                        {
                            if (l.Value == feeds.Tables["xianshi"].Rows[i][0].ToString())
                                l.Selected = true;
                        }

                    }
                }




            }
            catch (OracleException Error)
            {
                string CuoWu = "错误!" + Error.Message.ToString();
                Response.Write(CuoWu);
            }
            con.Close();

        }
        protected void DG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标离开某行时恢复背景色
                e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='#EBF0F4'");
                //鼠标处于某行时的颜色
                e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#FFCC66'");
                //单击的结果
                e.Row.Attributes.Add("OnClick", "this.style.backgroundColor='#FFCC66'; ClickEvent('" + e.Row.Cells[0].Text + "')");
                //设置鼠标为小手状
                e.Row.Attributes["style"] = "Cursor:hand";

            }
            //如果是绑定数据行 

        }
        //以下是显示用户费用
        protected void xsfeiyong_Click(object sender, EventArgs e)
        {
            SqlHelper conn = new SqlHelper();
            OracleConnection con = conn.GetConn();
            //先将选种的项目清空一下
            foreach (ListItem ll in chklist1.Items)
            {
                ll.Selected = false;
            }

            if (test.Text == "")
            {
                Response.Write("<script>alert('请点击列表中的模板')</script>");
            }
            else
            {
                //显示所有费用明细
                String sqlfeename = "select fee_name, fee_code,fee_class from fee_name";

                OracleDataAdapter myda = new OracleDataAdapter(sqlfeename, con);
                DataSet myds = new DataSet();
                myda.Fill(myds, "name");
                chklist1.DataSource = myds;
                chklist1.DataTextField = "fee_name";
                chklist1.DataValueField = "fee_code";
                chklist1.DataBind();

                //下面显示对应的费用
                string moban = this.test.Text;
                con.Open();
                string show = "select fee_code from fee_templet_detail where templet_code='" + moban + "'";
                OracleDataAdapter showda = new OracleDataAdapter(show, con);
                DataSet showds = new DataSet();
                showda.Fill(showds, "showfee");
                for (int i = 0; i < showds.Tables["showfee"].Rows.Count; i++)
                {
                    //对查询到的每个费用,使cheklist对应项目选中
                    foreach (ListItem l in chklist1.Items)
                    {
                        if (l.Value == showds.Tables["showfee"].Rows[i][0].ToString())
                        {
                            l.Selected = true;
                        }
                    }

                }
            }
            con.Close();
        }
        protected void ImgButBc_Click(object sender, EventArgs e)
        {
            SqlHelper conn = new SqlHelper();
            OracleConnection con = conn.GetConn();
            //string xiaolei;
            //string dalei;
            string moban = test.Text;
            con.Open();
            string sql = "select fee_class,fee_code,fee_name from fee_name";
            OracleDataAdapter myda = new OracleDataAdapter(sql, con);
            DataSet myds = new DataSet();
            myda.Fill(myds, "all");
            if (test.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请选模板!');</script>");

            }
            else
            {
                int count = 0;
                foreach (ListItem lt in listbox1.Items)
                {
                    if (lt.Selected)
                    {
                        count = count + 1;
                    }
                }
                if (count != 0)
                {
                    //只删除对应大类中的项,左侧列表被选中的时候
                    foreach (ListItem lt1 in listbox1.Items)
                    {
                        if (lt1.Selected)
                        {

                            string dellt1 = "delete fee_templet_detail where templet_code='" + moban + "' and fee_class='" + lt1.Value + "'";
                            OracleCommand cmddellt1 = new OracleCommand(dellt1, con);
                            cmddellt1.ExecuteNonQuery();
                            ////同时要在采油厂端更新
                            //SqlHelper cycSql = new SqlHelper();

                            //string cycstring = "Data Source=orcl;User ID=jilincyc;Password=jilincyc;Unicode=True";
                            //OracleConnection concyc = new OracleConnection(cycstring);
                            //concyc.Open();
                            //string delcyc = "delete fee_templet_detail where templet_code='" + moban + "' and fee_class='" + lt1.Value + "'";
                            //OracleCommand cmdcyc = new OracleCommand(delcyc, concyc);
                            //cmdcyc.ExecuteNonQuery();
                            //concyc.Close();
                        }
                    }
                    con.Close();
                    INTO();

                }

                else
                {


                    //先将所有选中的项目删除掉,左侧列表没有被选种的时候.....
                    foreach (ListItem l in chklist1.Items)
                    {
                        if (l.Selected)
                        {
                            string del = "delete fee_templet_detail where templet_code='" + moban + "'";
                            OracleCommand cmddel = new OracleCommand(del, con);
                            cmddel.ExecuteNonQuery();
                            ////在采油厂端删除
                            //string cycstring1 = "Data Source=orcl;User ID=jilincyc;Password=jilincyc;Unicode=True";
                            //OracleConnection concyc1 = new OracleConnection(cycstring1);
                            //concyc1.Open();
                            //string delcyc1 = "delete fee_templet_detail where templet_code='" + moban + "'";
                            //OracleCommand cmdcyc1 = new OracleCommand(delcyc1, concyc1);
                            //cmdcyc1.ExecuteNonQuery();
                            //concyc1.Close();
                        }
                    }
                    con.Close();
                    INTO();
                }
            }


        }

        private void INTO()
        {
            SqlHelper conn = new SqlHelper();
            OracleConnection con = conn.GetConn();
            con.Open();
            string dalei;
            string xiaolei;
            string moban = test.Text;
            string sql = "select fee_class,fee_code,fee_name from fee_name";
            OracleDataAdapter myda = new OracleDataAdapter(sql, con);
            DataSet myds = new DataSet();
            myda.Fill(myds, "all");
            foreach (ListItem l in chklist1.Items)
            {
                if (l.Selected)
                {

                    for (int i = 0; i < myds.Tables["all"].Rows.Count; i++)
                    {
                        if (myds.Tables["all"].Rows[i][1].ToString() == l.Value)
                        {
                            xiaolei = l.Value;
                            dalei = myds.Tables["all"].Rows[i][0].ToString();
                            string into = "insert into fee_templet_detail(templet_code,fee_class,fee_code) values('" + moban + "','" + dalei + "','" + xiaolei + "')";
                            OracleCommand cmdin = new OracleCommand(into, con);
                            cmdin.ExecuteNonQuery();

                            //在采油厂端加入

                            //string cycstring = "Data Source=orcl;User ID=jilincyc;Password=jilincyc;Unicode=True";
                            //OracleConnection concyc = new OracleConnection(cycstring);
                            //concyc.Open();
                            //string delcyc = "insert into fee_templet_detail(templet_code,fee_class,fee_code) values('" + moban + "','" + dalei + "','" + xiaolei + "')";
                            //OracleCommand cmdcyc = new OracleCommand(delcyc, concyc);
                            //cmdcyc.ExecuteNonQuery();
                            //concyc.Close();


                            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('保存成功!');</script>");
                        }
                    }
                }
            }
            con.Close();
        }
    }
}
