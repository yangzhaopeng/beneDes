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
using System.Drawing;
using System.Web.SessionState;

namespace beneDesYGS.view.month
{
    public partial class tbsyj : beneDesYGS.core.UI.corePage
    {
        protected SqlHelper DB = new SqlHelper();
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // 在此处放置用户代码以初始化页面




            //if (Session["user"] == null)
            //{
            //    Response.Redirect("../web/chaoshi.htm");
            //}
            //2013/10/09边敏修改
            txtNY.Text = Session["month"].ToString();
            string HLDT = "select distinct rbmhl_info.hl,dtb_info.dtb,xscs_info.jg from rbmhl_info,dtb_info,xscs_info";
            HLDT += " where rbmhl_info.ny='" + Session["month"].ToString() + "' and dtb_info.ny='" + Session["month"].ToString() + "' and xscs_info.ny='" + Session["month"].ToString() + "' and xscs_info.xsypdm='X003' and dtb_info.xsypdm='X003'";
            OracleConnection Con = DB.GetConn();
            Con.Open();
            OracleDataAdapter da1 = new OracleDataAdapter(HLDT, Con);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1, "hldt");
            if (ds1.Tables["hldt"].Rows.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请设置该年月汇率、吨桶比及原油价格!');</script>");
            }
            Con.Close();

            if (!IsPostBack)
            {
                DG_DataBind();

                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.Cache.SetNoStore();
                try
                {

                    string SHL = ds1.Tables["hldt"].Rows[0][0].ToString();
                    string SDTB = ds1.Tables["hldt"].Rows[0][1].ToString();
                    string SJG = ds1.Tables["hldt"].Rows[0][2].ToString();
                    double DHL = System.Convert.ToDouble(SHL);
                    double DDTB = System.Convert.ToDouble(SDTB);
                    double DJG = System.Convert.ToDouble(SJG);
                    double YJ = (DJG / DHL) / DDTB;
                    Number.Text = YJ.ToString("f2");

                    string HLDT1 = "select YYJG1,YYJG2,ZSBL1,SSKC1,YYJG3,YYJG4,ZSBL2,SSKC2,YYJG5,YYJG6,ZSBL3,SSKC3,YYJG7,YYJG8,ZSBL4,SSKC4,YYJGH,ZSBL5,SSKC5 from TBSYJ_BZ ";
                    OracleConnection Con1 = DB.GetConn();
                    Con1.Open();
                    OracleDataAdapter da11 = new OracleDataAdapter(HLDT1, Con1);
                    OracleCommand ocm = new OracleCommand(HLDT1, Con1);
                    OracleDataReader oreader = ocm.ExecuteReader();

                    if (oreader.Read())
                    {
                        TB1.Text = "" + oreader.GetDouble(0);
                        TB2.Text = "" + oreader.GetDouble(1);
                        TB3.Text = "" + oreader.GetDouble(2);
                        TB4.Text = "" + oreader.GetDouble(3);
                        TB5.Text = "" + oreader.GetDouble(4);
                        TB6.Text = "" + oreader.GetDouble(5);
                        TB7.Text = "" + oreader.GetDouble(6);
                        TB8.Text = "" + oreader.GetDouble(7);
                        TB9.Text = "" + oreader.GetDouble(8);
                        TB10.Text = "" + oreader.GetDouble(9);
                        TB11.Text = "" + oreader.GetDouble(10);
                        TB12.Text = "" + oreader.GetDouble(11);
                        TB13.Text = "" + oreader.GetDouble(12);
                        TB14.Text = "" + oreader.GetDouble(13);
                        TB15.Text = "" + oreader.GetDouble(14);
                        TB16.Text = "" + oreader.GetDouble(15);
                        TB17.Text = "" + oreader.GetDouble(16);
                        TB18.Text = "" + oreader.GetDouble(17);
                        TB19.Text = "" + oreader.GetDouble(18);


                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('数据库无数据！');</script>");
                    }
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('出错：" + ex.Message + "');</script>");
                }
            }


        }


        private void Scroll(int index)
        {
            string s = "<script>function window.onload(){document.getElementById(\"DG\").rows[" + index + "].scrollIntoView();}</script>";
            Page.RegisterStartupScript("", s);
        }


        public void DG_DataBind()
        {
            string Sql;
            Sql = "SELECT distinct ny,nvl(tbsyj,0) as tbsyj FROM tbsyj_Info where ny='" + Session["month"].ToString() + "'";
            OracleConnection Con = DB.GetConn();
            int rows = 0;
            try
            {
                Con.Open();
                OracleDataAdapter da = new OracleDataAdapter(Sql, Con);
                DataSet ds = new DataSet();
                da.Fill(ds, "tbsyj_info");

                rows = ds.Tables["tbsyj_info"].Rows.Count;
                if (rows == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不存在该年月记录!');</script>");
                }
                //2013年10月8日去掉DG注掉
                else
                {
                    this.DG.DataSource = ds.Tables["tbsyj_info"];
                    this.DG.DataBind();
                }

            }
            catch (OracleException Error)
            {
                string CuoWu = "错误!" + Error.Message.ToString();
                Response.Write(CuoWu);
            }
            Con.Close();

        }




        protected void DG_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标移动到每项时颜色交替效果
                e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor='#EBF0F4'");
                //鼠标处于某行时的颜色
                e.Row.Attributes.Add("OnMouseOver", "this.style.backgroundColor='#FFCC66'");
                //单击的结果
                e.Row.Attributes.Add("OnClick", "this.style.backgroundColor='#FFCC66';ClickEvent('" + e.Row.Cells[0].Text + "', '" + e.Row.Cells[1].Text + "')");
                //设置鼠标为小手状
                e.Row.Attributes["style"] = "Cursor:hand";

            }
            //如果是绑定数据行 

        }


        protected void BC_Click(object sender, EventArgs e)
        {
            OracleConnection Con = DB.GetConn();
            string NY = txtNY.Text;
            string SYJ;

            if (txtSYJ.Text == "")
            {
                SYJ = "0";
            }
            else
            {
                SYJ = txtSYJ.Text;
            }

            string SC = "delete from tbsyj_Info where ny='" + Session["month"].ToString() + "'";
            string BC1 = "insert into tbsyj_info(ny,tbsyj) values('" + NY + "','" + SYJ + "')";
            Con.Open();
            OracleCommand CmdSC = new OracleCommand(SC, Con);
            CmdSC.ExecuteNonQuery();
            OracleCommand CmdBC1 = new OracleCommand(BC1, Con);
            OracleTransaction tran = Con.BeginTransaction();
            CmdBC1.Transaction = tran;

            try
            {
                int reflectrows = Convert.ToInt32(CmdBC1.ExecuteNonQuery().ToString());
                if (reflectrows == 1)
                {
                    labShow.Text = "数据保存成功！";
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
                txtNY.Enabled = true;
                txtSYJ.Enabled = true;
            }
            Con.Close();
            if (labShow.Text == "数据保存成功！")
            {
                DG_DataBind();
                try
                {
                    OracleConnection conXM = new OracleConnection();
                    conXM.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionXM"].ConnectionString;
                    //string ConnectionXM = "Data Source=orcl;User ID='jilincyc';Password='jilincyc';Unicode=True";
                    //OracleConnection conXM = new OracleConnection(ConnectionXM);

                    conXM.Open();
                    string XM = "delete from tbsyj_Info where ny='" + Session["month"].ToString() + "'";
                    OracleCommand CmdXM = new OracleCommand(XM, conXM);
                    CmdXM.ExecuteNonQuery();

                    //2013年10月8日去掉DG注掉
                    for (int j = 0; j < DG.Rows.Count; j++)
                    {
                        string BC = "insert into tbsyj_info(ny,tbsyj) values('" + DG.Rows[j].Cells[0].Text + "','" + DG.Rows[j].Cells[1].Text + "')";
                        OracleCommand CmdBC = new OracleCommand(BC, conXM);
                        CmdBC.ExecuteNonQuery();
                    }

                    conXM.Close();

                }
                catch
                {

                }
            }
        }

        //2013/10/23 边敏修改
        protected void JS_Click(object sender, EventArgs e)
        {
            labShow.Text = "";
            try
            {
                string HLDT = "select distinct rbmhl_info.hl,dtb_info.dtb,xscs_info.jg from rbmhl_info,dtb_info,xscs_info";
                HLDT += " where rbmhl_info.ny='" + Session["month"].ToString() + "' and dtb_info.ny='" + Session["month"].ToString() + "' and xscs_info.ny='" + Session["month"].ToString() + "' and xscs_info.xsypdm='X003' and dtb_info.xsypdm='X003'";
                OracleConnection Con = DB.GetConn();
                Con.Open();
                OracleDataAdapter da1 = new OracleDataAdapter(HLDT, Con);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "hldt");
                string SHL = ds1.Tables["hldt"].Rows[0][0].ToString();
                string SDTB = ds1.Tables["hldt"].Rows[0][1].ToString();
                string SJG = ds1.Tables["hldt"].Rows[0][2].ToString();
                double DHL = System.Convert.ToDouble(SHL);
                double DDTB = System.Convert.ToDouble(SDTB);
                double DJG = System.Convert.ToDouble(SJG);
                double YJ = (DJG / DHL) / DDTB;
                double DSYJ = 0;


                //   Number.Text = YJ.ToString("f2");
                //if (40 < YJ && YJ <= 45)
                //{
                //    DSYJ = ((YJ - 40) * 0.2 - 0) * DHL * DDTB;
                //}
                //else if (45 < YJ && YJ <= 50)
                //{
                //    DSYJ = ((YJ - 40) * 0.25 - 0.25) * DHL * DDTB;
                //}
                //else if (50 < YJ && YJ <= 55)
                //{
                //    DSYJ = ((YJ - 40) * 0.3 - 0.75) * DHL * DDTB;
                //}
                //else if (55 < YJ && YJ <= 60)
                //{
                //    DSYJ = ((YJ - 40) * 0.35 - 1.5) * DHL * DDTB;
                //}
                //else if (60 < YJ)
                //{
                //    DSYJ = ((YJ - 40) * 0.4 - 2.5) * DHL * DDTB;
                //}
                string HLDT1 = "select YYJG1,YYJG2,ZSBL1,SSKC1,YYJG3,YYJG4,ZSBL2,SSKC2,YYJG5,YYJG6,ZSBL3,SSKC3,YYJG7,YYJG8,ZSBL4,SSKC4,YYJGH,ZSBL5,SSKC5 from TBSYJ_BZ ";
                OracleConnection Con1 = DB.GetConn();
                Con1.Open();
                OracleDataAdapter da11 = new OracleDataAdapter(HLDT1, Con1);
                DataSet ds11 = new DataSet();
                da11.Fill(ds11, "hldt1");
                //string S1 = ds11.Tables["hldt1"].Rows[0][0].ToString();
                //string S2 = ds11.Tables["hldt1"].Rows[0][1].ToString();
                string S3 = ds11.Tables["hldt1"].Rows[0][0].ToString();
                string S4 = ds11.Tables["hldt1"].Rows[0][1].ToString();
                string S5 = ds11.Tables["hldt1"].Rows[0][2].ToString();
                string S6 = ds11.Tables["hldt1"].Rows[0][3].ToString();
                string S7 = ds11.Tables["hldt1"].Rows[0][4].ToString();
                string S8 = ds11.Tables["hldt1"].Rows[0][5].ToString();
                string S9 = ds11.Tables["hldt1"].Rows[0][6].ToString();
                string S10 = ds11.Tables["hldt1"].Rows[0][7].ToString();
                string S11 = ds11.Tables["hldt1"].Rows[0][8].ToString();
                string S12 = ds11.Tables["hldt1"].Rows[0][9].ToString();
                string S13 = ds11.Tables["hldt1"].Rows[0][10].ToString();
                string S14 = ds11.Tables["hldt1"].Rows[0][11].ToString();
                string S15 = ds11.Tables["hldt1"].Rows[0][12].ToString();
                string S16 = ds11.Tables["hldt1"].Rows[0][13].ToString();
                string S17 = ds11.Tables["hldt1"].Rows[0][14].ToString();
                string S18 = ds11.Tables["hldt1"].Rows[0][15].ToString();
                string S19 = ds11.Tables["hldt1"].Rows[0][16].ToString();
                string S20 = ds11.Tables["hldt1"].Rows[0][17].ToString();
                string S21 = ds11.Tables["hldt1"].Rows[0][18].ToString();

                //double DS2 = System.Convert.ToDouble(S2);
                double DS3 = System.Convert.ToDouble(S3);
                double DS4 = System.Convert.ToDouble(S4);
                double DS5 = System.Convert.ToDouble(S5);
                double DS6 = System.Convert.ToDouble(S6);
                double DS7 = System.Convert.ToDouble(S7);
                double DS8 = System.Convert.ToDouble(S8);
                double DS9 = System.Convert.ToDouble(S9);
                double DS10 = System.Convert.ToDouble(S10);
                double DS11 = System.Convert.ToDouble(S11);
                double DS12 = System.Convert.ToDouble(S12);
                double DS13 = System.Convert.ToDouble(S13);
                double DS14 = System.Convert.ToDouble(S14);
                double DS15 = System.Convert.ToDouble(S15);
                double DS16 = System.Convert.ToDouble(S16);
                double DS17 = System.Convert.ToDouble(S17);
                double DS18 = System.Convert.ToDouble(S18);
                double DS19 = System.Convert.ToDouble(S19);
                double DS20 = System.Convert.ToDouble(S20);
                double DS21 = System.Convert.ToDouble(S21);



                if (DS3 < YJ && YJ <= DS4)
                {
                    DSYJ = ((YJ - DS3) * DS5 / 100 - DS6) * DHL * DDTB;
                }
                else if (DS7 < YJ && YJ <= DS8)
                {
                    DSYJ = ((YJ - DS3) * DS9 / 100 - DS10) * DHL * DDTB;
                }
                else if (DS11 < YJ && YJ <= DS12)
                {
                    DSYJ = ((YJ - DS3) * DS13 / 100 - DS14) * DHL * DDTB;
                }
                else if (DS15 < YJ && YJ <= DS16)
                {
                    DSYJ = ((YJ - DS3) * DS17 / 100 - DS18) * DHL * DDTB;
                }
                else if (DS19 < YJ)
                {
                    DSYJ = ((YJ - DS3) * DS20 / 100 - DS21) * DHL * DDTB;
                }
                string SSYJ = DSYJ.ToString("f2");
                txtSYJ.Text = SSYJ;
                Labeljs.Text = "计算完毕，请保存数据！";

                //OracleConnection conn = DB.CreatConnection();
                //conn.Open();
                //string sql2 = "update TBSYJ_INFO set NY='" + txtNY.Text.Trim() + "',TBSYJ='" + txtSYJ.Text.Trim() + "'where NY='" + txtNY.Text.Trim() + "'";
                //OracleCommand cmd2 = new OracleCommand(sql2, conn);
                //cmd2.ExecuteNonQuery();

                //conn.Close();
                Con1.Close();
                Con.Close();
                // Response.Write("<script type='text/javascript'>alert('数据计算完毕，请保存数据！')</script>");
            }

            catch
            {


            }
        }


        protected void bt_submit_Click(object sender, EventArgs e)
        {

            try
            {
                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ygsConnectionString"].ConnectionString;
                string sql = "select COUNT(*)  from ygs.TBSYJ_BZ ";
                conn.Open();
                OracleCommand cmd1 = new OracleCommand(sql, conn);

                OracleDataAdapter da1 = new OracleDataAdapter(sql, conn);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "sql");
                string SHL = ds1.Tables["sql"].Rows[0][0].ToString();
                int m = System.Convert.ToInt32(SHL);


                string sql1 = "";
                //string sql1;

                if (m == 0)
                {
                    sql1 = "insert into ygs.TBSYJ_BZ(YYJG1,YYJG2,ZSBL1,SSKC1,YYJG3,YYJG4,ZSBL2,SSKC2,YYJG5,YYJG6,ZSBL3,SSKC3,YYJG7,YYJG8,ZSBL4,SSKC4,YYJGH,ZSBL5,SSKC5) values('" + TB1.Text.Trim() + "','" + TB2.Text.Trim() + "','" + TB3.Text.Trim() + "','" + TB4.Text.Trim() + "','" + TB5.Text.Trim() + "','" + TB6.Text.Trim() + "','" + TB7.Text.Trim() + "','" + TB8.Text.Trim() + "','" + TB9.Text.Trim()
                    + "','" + TB10.Text.Trim() + "','" + TB11.Text.Trim() + "','" + TB12.Text.Trim() + "','" + TB13.Text.Trim() + "','" + TB14.Text.Trim() + "','" + TB15.Text.Trim() + "','" + TB16.Text.Trim() + "','" + TB17.Text.Trim() + "','" + TB18.Text.Trim() + "','" + TB19.Text.Trim() + "')";
                }
                else
                {
                    sql1 = "update ygs.TBSYJ_BZ set YYJG1='" + TB1.Text.Trim() + "',YYJG2='" + TB2.Text.Trim() + "',ZSBL1='" + TB3.Text.Trim() + "',SSKC1='" + TB4.Text.Trim() + "',YYJG3='" + TB5.Text.Trim() + "',YYJG4='" + TB6.Text.Trim() + "',ZSBL2='" + TB7.Text.Trim() + "',SSKC2='" + TB8.Text.Trim() + "',YYJG5='" + TB9.Text.Trim() + "',YYJG6='" + TB10.Text.Trim() + "',ZSBL3='" + TB11.Text.Trim() + "',SSKC3='" + TB12.Text.Trim() + "',YYJG7='" + TB13.Text.Trim() + "',YYJG8='" + TB14.Text.Trim() + "',ZSBL4='" + TB15.Text.Trim() + "',SSKC4='" + TB16.Text.Trim() + "',YYJGH='" + TB17.Text.Trim() + "',ZSBL5='" + TB18.Text.Trim() + "',SSKC5='" + TB19.Text.Trim() + "'";
                }



                OracleCommand cmd = new OracleCommand(sql1, conn);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {

                    Response.Write("<script type='text/javascript'>alert('数据保存成功！')</script>");

                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('数据保存失败！')</script>");
                }



                conn.Close();

            }
            catch
            {
                Response.Write("<script type='text/javascript'>alert('数据不能为空！')</script>");

            }
        }

        protected void TB2_TextChanged(object sender, EventArgs e)
        {
            TB5.Text = TB2.Text;
        }
        protected void TB6_TextChanged(object sender, EventArgs e)
        {
            TB9.Text = TB6.Text;
        }

        protected void TB10_TextChanged(object sender, EventArgs e)
        {
            TB13.Text = TB10.Text;
        }
        protected void TB14_TextChanged(object sender, EventArgs e)
        {
            TB17.Text = TB14.Text;
        }

    }
}
