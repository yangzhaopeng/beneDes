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
using beneDesYGS.core;

namespace beneDesYGS.view.system
{
    public partial class feiyongdetail : beneDesYGS.core.UI.corePage
    { 
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                Btn_bind();
                DDL_FormulaBind();
                this.Label.Text = "";
                ImgButSc.Attributes["onClick"] = "javascript:return confirm('确定要删除该行吗？');";


            }
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            //ImgButSc.Attributes.Add("onclick", "return confirm('确定要删吗?若确定删除请先点击确定再点击保存按钮');");
        }

        private void DDL_FormulaBind()
        {
            SqlHelper conn = new SqlHelper();
            OracleConnection con = conn.GetConn();
            con.Open();
            //公式代码名字绑定
            string sqlformula = "select formula_code,formula_name from formula";
            OracleDataAdapter daformula = new OracleDataAdapter(sqlformula, con);
            DataSet dsformula = new DataSet();
            daformula.Fill(dsformula, "formula");
            ddlformula.DataSource = dsformula.Tables["formula"];
            ddlformula.DataTextField = "formula_name";
            ddlformula.DataValueField = "formula_code";
            ddlformula.DataBind();
            con.Close();

        }

        protected void listbox_SelectedIndexChanged1(object sender, EventArgs e)
        {
            SqlHelper conn = new SqlHelper();
            OracleConnection con = conn.GetConn();
            Label.Text = " ";
            try
            {

                String selclass = this.listbox.SelectedItem.Text;
                String classin = "select class_code from fee_class where class_name='" + selclass + "'";
                //String sql = "select class_name from fee_class where class_name='" + selclass + "' ";
                //sql += "union";

                string sql = "select fee_class, fee_code,fee_name,fee_level,formula_code,remark,has_cuoshi from fee_name where fee_class in(" + classin + ")";



                //string sqltest = "select fee_class.class_name,fee_name.fee_code,fee_name.fee_name,fee_name.fee_level,fee_name.formula_code,fee_name.remark,fee_name.has_cuoshi from fee_class left join fee_name on fee_class.class_code=fee_name.fee_class and fee_name.fee_name is not null and fee_class.class_name ='" + selclass + "'";

                con.Open();
                OracleDataAdapter da2 = new OracleDataAdapter(sql, con);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2, "fee_name");
                GV.DataSource = ds2;
                GV.DataBind();
                int count = GV.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    if (GV.Rows[i].Cells[3].Text == Convert.ToString(0))
                    {
                        GV.Rows[i].Cells[3].Text = "系统级";
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

        public void Btn_bind()
        {
            SqlHelper conn = new SqlHelper();
            OracleConnection con = conn.GetConn();
            try
            {
                String sql = "select class_name,class_code from fee_class where fee_type='0' ";
                con.Open();
                OracleDataAdapter da = new OracleDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "fee_class");
                this.listbox.DataSource = ds;

                listbox.DataTextField = "class_name";
                listbox.DataValueField = "class_code";
                this.listbox.DataBind();

                ddlclass.DataSource = ds;
                ddlclass.DataTextField = "class_name";
                ddlclass.DataValueField = "class_code";
                ddlclass.DataBind();


                GV_DataBind();
                int count = GV.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    if (GV.Rows[i].Cells[3].Text == Convert.ToString(0))
                    {
                        GV.Rows[i].Cells[3].Text = "系统级";
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
        public void GV_DataBind()
        {
            SqlHelper conn = new SqlHelper();
            OracleConnection con = conn.GetConn();
            String sql = "select fee_class,fee_code,fee_name,fee_level,formula_code,remark,nvl(has_cuoshi,0) as has_cuoshi from fee_name";

            int rows = 0;
            try
            {

                OracleDataAdapter da = new OracleDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "feename");

                //rows = ds.Tables["feeclass"].Rows.Count;//行数,记录数
                this.GV.DataSource = ds.Tables["feename"];
                this.GV.DataBind();
                if (ds.Tables["feename"].Rows.Count == 0)
                {
                    Response.Write("<script>('不存在相关记录')</script>");
                }


            }
            catch (OracleException Error)
            {
                string CuoWu = "错误!" + Error.Message.ToString();
                Response.Write(CuoWu);
            }
            con.Close();

        }



        public void GV2_DataBind()
        {
            SqlHelper conn = new SqlHelper();
            OracleConnection con = conn.GetConn();
            string sqlin = "select class_code from fee_class where class_name='" + this.listbox.SelectedItem.Value + "'";
            string sqlrow = "select fee_class,fee_code,fee_name,fee_level,formula_code,remark,nvl(has_cuoshi,0) as has_cuoshi from fee_name where fee_class in(" + sqlin + ")";
            int rows = 0;
            try
            {
                con.Open();
                OracleDataAdapter da = new OracleDataAdapter(sqlrow, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "feename");

                //rows = ds.Tables["feeclass"].Rows.Count;//行数,记录数
                this.GV.DataSource = ds.Tables["feename"];
                this.GV.DataBind();

            }
            catch (OracleException Error)
            {
                string CuoWu = "错误!" + Error.Message.ToString();
                Response.Write(CuoWu);
            }
            con.Close();

        }


        private void Scroll(int index)
        {
            string s = "<script>function window.onload(){document.getElementById(\"DG\").rows[" + index + "].scrollIntoView();}</script>";
            Page.RegisterStartupScript("", s);
        }


        protected void DG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标离开某行时恢复背景色
                e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='#EBF0F4'");
                //鼠标处于某行时的颜色
                e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#FFCC66'");
                //单击的结果;this.style.color='#8C4510'
                if (e.Row.Cells[6].Text == "&nbsp;")
                {
                    e.Row.Cells[6].Text = "";
                }
                //if (e.Row.Cells[3].Text == "0")
                //{
                //    e.Row.Cells[3].Text = "系统级";
                //}
                //if (e.Row.Cells[3].Text == "1")
                //{
                //    e.Row.Cells[3].Text = "用户级";
                //}

                e.Row.Attributes.Add("OnClick", "this.style.backgroundColor='#FFCC66';ClickEvent('" + e.Row.Cells[0].Text + "', '" + e.Row.Cells[1].Text + "','" + e.Row.Cells[2].Text + "', '" + e.Row.Cells[3].Text + "', '" + e.Row.Cells[4].Text + "', '" + e.Row.Cells[5].Text + "', '" + e.Row.Cells[6].Text + "')");
                //设置鼠标为小手状
                e.Row.Attributes["style"] = "Cursor:hand";

            }
            //如果是绑定数据行 

        }

        protected void ImgButZj_Click(object sender, EventArgs e)
        {
            ddlclass.Enabled = true;
            txtbeizhu.Enabled = true;
            txtcode.Enabled = true;
            ddlformula.Enabled = true;
            ddlgrade.Enabled = true;
            txtname.Enabled = true;
            chk1.Enabled = true;
            txtbeizhu.Text = "";
            txtcode.Text = "";
            txtname.Text = "";
            Label.Text = "增加";
            labShow.Text = "";

        }
        protected void ImgButXg_Click(object sender, EventArgs e)
        {
            ddlclass.Enabled = true;
            txtbeizhu.Enabled = true;
            txtcode.Enabled = false;

            ddlformula.Enabled = true;


            ddlgrade.Enabled = true;
            txtname.Enabled = true;
            chk1.Enabled = true;
            Label.Text = "修改";
            labShow.Text = "";
        }
        protected void ImgButSc_Click(object sender, EventArgs e)
        {
            SqlHelper conn = new SqlHelper();
            OracleConnection con = conn.GetConn();
            if (txtcode.Text == "" || txtname.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请选择要删除的行!');</script>");
            }
            else
            {
                ddlclass.Enabled = false;
                txtbeizhu.Enabled = false;
                txtcode.Enabled = false;
                ddlformula.Enabled = false;

                ddlgrade.Enabled = false;
                txtname.Enabled = false;
                chk1.Enabled = true;
                Label.Text = "删除";
                labShow.Text = "";



                string Code = txtcode.Text;
                string Name = txtname.Text;
                OracleCommand Cmd;
                if (txtcode.Text == "" || ddlgrade.Text == "")
                {
                    labShow.Text = "费用代号、费用级别不可为空";

                }
                //数据库操作
                else
                {
                    //数据库操作

                    try
                    {
                        con.Open();
                        bool judge = true;

                        //string sqlrow = "select fee_class,fee_code,fee_name,fee_level,formula_code,remark,has_cuoshi from fee_name where fee_class in (select class_code from fee_class where class_name='" + this.listbox.SelectedItem.Text + "')";
                        string sqlrow = "select fee_code from fee_name";
                        int rows = 0;
                        try
                        {

                            OracleDataAdapter da = new OracleDataAdapter(sqlrow, con);
                            DataSet ds = new DataSet();
                            da.Fill(ds, "feename");

                            rows = ds.Tables["feename"].Rows.Count;//行数,记录数
                            for (int i = 0; i < rows; i++)
                            {
                                if (ds.Tables["feename"].Rows[i][0].ToString() == Code)
                                {
                                    judge = false;
                                    String Sql = "DELETE fee_name";
                                    Sql += " WHERE fee_code ='" + Code + "'";
                                    Cmd = new OracleCommand(Sql, con);

                                    if (Convert.ToInt32(Cmd.ExecuteNonQuery().ToString()) != 0)
                                    {
                                        labShow.Text = "删除成功！";
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('删除成功!');</script>");

                                        txtbeizhu.Enabled = true;
                                        txtcode.Enabled = true;

                                        ddlclass.Enabled = true;
                                        ddlformula.Enabled = true;
                                        ddlgrade.Enabled = true;
                                        txtname.Enabled = true;
                                        chk1.Enabled = true;
                                        Label.Text = "";
                                        txtbeizhu.Text = "";
                                        txtcode.Text = "";
                                        txtname.Text = "";
                                    }

                                }

                            }
                        }
                        catch (OracleException Error)
                        {
                            string CuoWu = "错误!" + Error.Message.ToString();
                            Response.Write(CuoWu);
                        }



                        if (judge)
                        {
                            this.labShow.Text = "不存在该项记录，不可删除！";


                        }


                    }


                    catch (OracleException Error)
                    {
                        labShow.Text = Error.Message.ToString();
                    }
                    finally
                    {
                        con.Close();
                        GV_DataBind();
                    }
                }

                if (labShow.Text == "删除成功！")
                {
                    GV_DataBind();
                    //string Ccyc = "select dbname from department";
                    //OracleDataAdapter myda = new OracleDataAdapter(Ccyc, con);
                    //DataSet myds = new DataSet();
                    //myda.Fill(myds, "cyc");
                    //for (int i = 0; i < myds.Tables["cyc"].Rows.Count; i++)
                    //{
                    //    string cname = myds.Tables["cyc"].Rows[i][0].ToString();
                    try
                    {
                        //string ConnectionXM = "Data Source=orcl;User ID='" + cname + "';Password='" + cname + "';Unicode=True";
                        string ConnectionXM = "Data Source=orcl;User ID=jilincyc;Password=jilincyc;Unicode=True";
                        OracleConnection conXM = new OracleConnection(ConnectionXM);
                        conXM.Open();
                        string XM = "delete from fee_name";
                        OracleCommand CmdXM = new OracleCommand(XM, conXM);
                        CmdXM.ExecuteNonQuery();
                        for (int j = 0; j < GV.Rows.Count; j++)
                        {

                            string BC = "insert into fee_name(fee_class,fee_code, fee_name,fee_level,formula_code,has_cuoshi,remark)";
                            BC += " values('" + GV.Rows[j].Cells[0].Text + "','" + GV.Rows[j].Cells[1].Text + "','" + GV.Rows[j].Cells[2].Text + "','" + GV.Rows[j].Cells[3].Text + "','" + GV.Rows[j].Cells[4].Text + "','" + GV.Rows[j].Cells[5].Text + "','" + GV.Rows[j].Cells[6].Text + "')";
                            OracleCommand CmdBC = new OracleCommand(BC, conXM);
                            CmdBC.ExecuteNonQuery();
                        }
                        conXM.Close();
                        GV_DataBind();
                    }
                    catch
                    {

                    }

                    //}
                }

            }
        }
        protected void ImgButBc_Click(object sender, EventArgs e)
        {
            SqlHelper conn = new SqlHelper();
            OracleConnection con = conn.GetConn();
            if (Label.Text == "")
            {
                labShow.Text = "请先选择相关操作";

            }

            else
            {
                //定义数据库操作变量

                OracleCommand Cmd = new OracleCommand();
                string sql = "";
                ddlclass.Enabled = false;
                txtbeizhu.Enabled = false;
                txtcode.Enabled = false;

                ddlformula.Enabled = false;

                ddlgrade.Enabled = false;
                txtname.Enabled = false;
                chk1.Enabled = false;

                String Feeclass = ddlclass.SelectedItem.Value;

                string Code = txtcode.Text;
                string Name = txtname.Text;
                //string Formula=txtformula.Text;
                string Formula = ddlformula.SelectedItem.Value;
                //string Grade = ddlgrade.Text;
                string Grade = ddlgrade.SelectedItem.Value;
                if (Grade == "系统级")
                {
                    Grade = "0";
                }
                string Check = (Convert.ToInt32(chk1.Checked)).ToString();
                string Beizhu = txtbeizhu.Text;



                if (Label.Text == "增加")
                {
                    labShow.Text = "";
                    if (txtcode.Text == "" || ddlgrade.Text == "")
                    {

                        labShow.Text = "费用代号、费用级别不可为空";

                    }
                    else
                    {
                        //数据库操作

                        try
                        {
                            con.Open();
                            bool judge = true;

                            //string sqlrow = "select fee_class,fee_code,fee_name,fee_level,formula_code,remark,has_cuoshi from fee_name where fee_class in('" + sqlin + "')";
                            string sqlrow = "select fee_code from fee_name";
                            int rows = 0;
                            try
                            {

                                OracleDataAdapter da = new OracleDataAdapter(sqlrow, con);
                                DataSet ds = new DataSet();
                                da.Fill(ds, "feename");

                                rows = ds.Tables["feename"].Rows.Count;//行数,记录数
                                for (int i = 0; i < rows; i++)
                                {
                                    if (ds.Tables["feename"].Rows[i][0].ToString() == Code)
                                    {
                                        judge = false;
                                        break;
                                    }

                                }
                            }
                            catch (OracleException Error)
                            {
                                string CuoWu = "错误!" + Error.Message.ToString();
                                Response.Write(CuoWu);
                            }

                            if (!judge)
                            {
                                this.labShow.Text = "已经存在该项记录，不可添加";

                            }
                            else
                            {


                                //string Sql = "insert into fee_name(fee_class,fee_code, fee_name,fee_level,formula_code,remark,has_cuoshi ) values('" + Feeclass + "','" + Code+ "','" + Name + "','" +Grade+ "','" + Formula + "','" + Beizhu + "','"+Check+"') ";
                                string Sql1 = "insert into fee_name(fee_class,fee_code, fee_name,fee_level,formula_code,remark,has_cuoshi )";
                                Sql1 += "values('" + Feeclass + "','" + Code + "','" + Name + "','" + Grade + "','" + Formula + "','" + Beizhu + "','" + Check + "')";

                                OracleCommand cmd1 = new OracleCommand(Sql1, con);
                                OracleTransaction tran = con.BeginTransaction();
                                cmd1.Transaction = tran;
                                try
                                {
                                    int reflectrows = cmd1.ExecuteNonQuery();
                                    if (reflectrows == 1)
                                    {
                                        labShow.Text = "数据保存成功！";
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('数据保存成功!');</script>");

                                        txtbeizhu.Enabled = true;
                                        txtcode.Enabled = true;

                                        ddlclass.Enabled = true;
                                        ddlformula.Enabled = true;
                                        ddlgrade.Enabled = true;
                                        txtname.Enabled = true;
                                        chk1.Enabled = true;

                                        Label.Text = "";
                                        tran.Commit();

                                    }

                                    else
                                    {
                                        labShow.Text = "数据未保存成功，请重新输入！";
                                    }

                                }
                                catch
                                {

                                    tran.Rollback();
                                    labShow.Text = "数据无效，请重新输入！";
                                    txtbeizhu.Enabled = true;
                                    txtcode.Enabled = true;
                                    ddlclass.Enabled = true;

                                    //txtformula.Enabled = true;
                                    ddlformula.Enabled = true;
                                    ddlgrade.Enabled = true;
                                    txtname.Enabled = true;
                                    chk1.Enabled = true;
                                }

                            }



                        }

                        catch (OracleException Error)
                        {
                            labShow.Text = Error.Message.ToString();
                        }
                        finally
                        {
                            con.Close();
                        }
                    }

                    if (labShow.Text == "数据保存成功！")
                    {
                        GV_DataBind();
                        //string Ccyc = "select dbname from department";
                        //OracleDataAdapter myda = new OracleDataAdapter(Ccyc, con);
                        //DataSet myds = new DataSet();
                        //myda.Fill(myds, "cyc");
                        //for (int i = 0; i < myds.Tables["cyc"].Rows.Count; i++)
                        //{
                        //    string cname = myds.Tables["cyc"].Rows[i][0].ToString();
                        try
                        {
                            //string ConnectionXM = "Data Source=orcl;User ID='" + cname + "';Password='" + cname + "';Unicode=True";
                            string ConnectionXM = "Data Source=orcl;User ID=jilincyc;Password=jilincyc;Unicode=True";
                            OracleConnection conXM = new OracleConnection(ConnectionXM);
                            conXM.Open();
                            string XM = "delete from fee_name";
                            OracleCommand CmdXM = new OracleCommand(XM, conXM);
                            CmdXM.ExecuteNonQuery();
                            for (int j = 0; j < GV.Rows.Count; j++)
                            {

                                string BC = "insert into fee_name(fee_class,fee_code, fee_name,fee_level,formula_code,has_cuoshi,remark)";
                                BC += " values('" + GV.Rows[j].Cells[0].Text + "','" + GV.Rows[j].Cells[1].Text + "','" + GV.Rows[j].Cells[2].Text + "','" + GV.Rows[j].Cells[3].Text + "','" + GV.Rows[j].Cells[4].Text + "','" + GV.Rows[j].Cells[5].Text + "','" + GV.Rows[j].Cells[6].Text + "')";
                                OracleCommand CmdBC = new OracleCommand(BC, conXM);
                                CmdBC.ExecuteNonQuery();
                            }
                            conXM.Close();
                        }
                        catch
                        {

                        }

                        //}
                    }
                }

                else
                    if (Label.Text == "修改")
                    {

                        if (txtcode.Text == "" || ddlgrade.Text == "")
                        {
                            labShow.Text = "费用代号、费用级别不可为空";

                        }
                        //数据库操作
                        else
                        {
                            //数据库操作

                            try
                            {
                                con.Open();
                                bool judge = true;
                                //string sqlin = "select class_code from fee_class where class_name='" + this.listbox.SelectedItem.Value + "'";
                                //string sqlrow = "select fee_class,fee_code,fee_name,fee_level,formula_code,remark,has_cuoshi from fee_name where fee_class in('" + sqlin + "')";
                                //string sqlrow = "select fee_class,fee_code,fee_name,fee_level,formula_code,remark,has_cuoshi from fee_name where fee_class in (select class_code from fee_class where class_name='" + this.listbox.SelectedItem.Text+ "')";
                                string sqlrow = "select fee_code from fee_name";
                                int rows = 0;
                                try
                                {

                                    OracleDataAdapter da = new OracleDataAdapter(sqlrow, con);
                                    DataSet ds = new DataSet();
                                    da.Fill(ds, "feename");

                                    rows = ds.Tables["feename"].Rows.Count;//行数,记录数
                                    for (int i = 0; i < rows; i++)
                                    {
                                        if (ds.Tables["feename"].Rows[i][0].ToString() == Code)
                                        {
                                            judge = false;
                                            sql = "UPDATE fee_name";
                                            sql += " SET fee_class='" + Feeclass + "', fee_code='" + Code + "', fee_name='" + Name + "', fee_level='" + Grade + "',formula_code='" + Formula + "',remark='" + Beizhu + "',has_cuoshi='" + Check + "'";
                                            sql += " WHERE fee_code='" + Code + "'";
                                            OracleCommand cmd = new OracleCommand(sql, con);

                                            if (Convert.ToInt32(cmd.ExecuteNonQuery().ToString()) != 0)
                                            {
                                                labShow.Text = "修改成功！";
                                                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('修改成功!');</script>");

                                                txtbeizhu.Enabled = true;
                                                txtcode.Enabled = true;

                                                ddlclass.Enabled = true;
                                                ddlformula.Enabled = true;
                                                ddlgrade.Enabled = true;
                                                txtname.Enabled = true;
                                                chk1.Enabled = true;

                                                Label.Text = "";

                                            }

                                        }

                                    }
                                }
                                catch (OracleException Error)
                                {
                                    string CuoWu = "错误!" + Error.Message.ToString();
                                    Response.Write(CuoWu);
                                }



                                if (judge)
                                {
                                    this.labShow.Text = "不存在该项记录，不可修改！";

                                }


                            }


                            catch (OracleException Error)
                            {
                                labShow.Text = Error.Message.ToString();
                            }
                            finally
                            {
                                con.Close();
                                GV_DataBind();
                            }
                        }

                        if (labShow.Text == "修改成功！")
                        {
                            GV_DataBind();
                            //string Ccyc = "select dbname from department";
                            //OracleDataAdapter myda = new OracleDataAdapter(Ccyc, con);
                            //DataSet myds = new DataSet();
                            //myda.Fill(myds, "cyc");
                            //for (int i = 0; i < myds.Tables["cyc"].Rows.Count; i++)
                            //{
                            //    string cname = myds.Tables["cyc"].Rows[i][0].ToString();
                            try
                            {
                                //string ConnectionXM = "Data Source=orcl;User ID='" + cname + "';Password='" + cname + "';Unicode=True";
                                string ConnectionXM = "Data Source=orcl;User ID=jilincyc;Password=jilincyc;Unicode=True";
                                OracleConnection conXM = new OracleConnection(ConnectionXM);
                                conXM.Open();
                                string XM = "delete from fee_name";
                                OracleCommand CmdXM = new OracleCommand(XM, conXM);
                                CmdXM.ExecuteNonQuery();
                                for (int j = 0; j < GV.Rows.Count; j++)
                                {

                                    string BC = "insert into fee_name(fee_class,fee_code, fee_name,fee_level,formula_code,has_cuoshi,remark)";
                                    BC += " values('" + GV.Rows[j].Cells[0].Text + "','" + GV.Rows[j].Cells[1].Text + "','" + GV.Rows[j].Cells[2].Text + "','" + GV.Rows[j].Cells[3].Text + "','" + GV.Rows[j].Cells[4].Text + "','" + GV.Rows[j].Cells[5].Text + "','" + GV.Rows[j].Cells[6].Text + "')";
                                    OracleCommand CmdBC = new OracleCommand(BC, conXM);
                                    CmdBC.ExecuteNonQuery();
                                }
                                conXM.Close();
                                GV_DataBind();
                            }
                            catch
                            {

                            }

                            //}
                        }
                    }


            }

        }
    }
}
