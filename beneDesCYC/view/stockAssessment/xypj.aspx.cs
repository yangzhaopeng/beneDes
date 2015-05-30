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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace beneDesCYC.view.stockAssessment
{
    public partial class xypj : beneDesCYC.core.UI.corePage
    {
        protected SqlHelper DB = new SqlHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    if (Session["user"] == null)
            //    {
            //        Response.Redirect("../web/chaoshi.htm");
            //    }
            //    Btnqd.Attributes.Add("onclick ", "ShowBar() ");
            //    Panel1.Visible = false;
            //    Label5.Visible = false;
            //    //bny = "200607";
            //    //eny = "200707";
            //    string ny = Session["date"].ToString();
            //    ny = ny.Substring(0, 4);
            //    bny = ny + "01";
            //    eny = Session["date"].ToString();

            //}
            if (!IsPostBack)
            {
                string list = _getParam("CYC");
                if (list == "null")
                {
                    Panel1.Visible = false;
                    Label5.Visible = false;
                }
                else
                {
                    Label5.Visible = false;
                    confirmClick();
                }
            }



        }
        protected void createjdtime()
        {

            //命令四,建立阶段效益评价时间库
            //取得系统时间

            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");

           Session["jbny"] = bny;
           Session["jeny"] = eny;

            OracleConnection conb = DB.GetConn();
            //OracleConnection conb = DB.CreatConnection();
            conb.Open();
            string sqlt = "select sysdate time from dual";
            OracleCommand commt = new OracleCommand(sqlt, conb);
            try
            {
                OracleDataReader drt = commt.ExecuteReader();
                if(drt.Read())
                this.Labelctime.Text = drt.GetOracleDateTime(0).ToString();
                drt.Close();
            }
            catch (OracleException Error)
            {
                Label4.Text = "取系统时间错误: " + Error.Message.ToString();
                Label4.Visible = true;
            }

            //建立表,首先判断表是否存在
            #region
            string sqlt2 = "select count(*) from user_tables where table_name = 'JDTIME'";
            OracleCommand commt2 = new OracleCommand(sqlt2, conb);
            try
            {
                OracleDataReader drt2 = commt2.ExecuteReader();
                if (drt2.Read())
                {
                    Label4.Text = drt2[0].ToString();
                    if (drt2[0].ToString() == "1") //表已经存在,则插入表即可
                    {
                        string sqlt3 = "insert into jdtime (ctime,ftime,ltime) values('" + this.Labelctime.Text + "','" + bny + "','" + eny + "')";
                        OracleCommand commt3 = new OracleCommand(sqlt3, conb);
                        try
                        {
                            commt3.ExecuteNonQuery();
                        }
                        catch (OracleException Error)
                        {
                            Label4.Text = "表存在,插入时间错误: " + Error.Message.ToString();
                            Label4.Visible = true;
                        }

                    }
                    else  //表不存在,则建立表,再插入表
                    {
                        //创建表
                        string sqlt4 = "create table jdtime (ctime varchar(30) primary key ,ftime varchar(20) not null, ltime varchar(20) not null )";
                        OracleCommand commt4 = new OracleCommand(sqlt4, conb);
                        try
                        {
                            commt4.ExecuteNonQuery();
                        }
                        catch (OracleException Error)
                        {
                            Label4.Text = "表不存在,创建表错误: " + Error.Message.ToString();
                            Label4.Visible = true;
                        }
                        //插入表
                        string sqlt5 = "insert into jdtime (ctime,ftime,ltime) values('" + this.Labelctime.Text + "','" + bny + "','" + eny + "')";
                        OracleCommand commt5 = new OracleCommand(sqlt5, conb);
                        try
                        {
                            commt5.ExecuteNonQuery();
                        }
                        catch (OracleException Error)
                        {
                            Label4.Text = "表不存在,创建后插入错误: " + Error.Message.ToString();
                            Label4.Visible = true;
                        }
                    }
                }
                drt2.Close();

            }
            catch (OracleException Error)
            {
                Label4.Text = "错误: " + Error.Message.ToString();
                Label4.Visible = true;


            }
            conb.Close();
            #endregion

        }
        protected void confirmClick()
        {
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");

            Session["jbny"] = bny;
            Session["jeny"] = eny;

            if (bny == "" || eny == "")
            {
                Response.Write("<script>alert('年月不能为空!')</script>");
            }
            else if (int.Parse(bny) > int.Parse(eny))
            {
                Response.Write("<script>alert('开始日期不能大于结束日期!')</script>");
            }
            //else if (int.Parse(bny) == int.Parse(eny))
            //{
            //    Response.Write("<script>alert('至少选择两个月的时间!')</script>");
            //}
            else
            {
                string cycid = Session["cyc_id"].ToString();
                string cqcid = Session["cqc_id"].ToString();
                Panel1.Visible = true;
                //    Label5.Visible = true;              
                DateTime ctime = System.DateTime.Now;
                string tt = ctime.Year + ctime.Month + ctime.Day + " " + ctime.Hour + ":" + ctime.Minute + ":" + ctime.Second;
                
                ExcutePJ(cycid,bny,eny,tt);
                if(cycid!=cqcid)
                    ExcutePJ(cqcid, bny, eny, tt);
            }

        }

        private void ExcutePJ(string cycid, string bny, string eny, string tt)
        {
            int err = 0;  //err= 0 表示没错误
            Labelctime.Visible = false;
            Label4.Visible = false;
            Label5.Text = "正在处理中,请稍后...";
            OracleConnection conb = DB.GetConn();
            //OracleConnection conb = DB.CreatConnection();
            conb.Open();

            //命令一,创建单井阶段评价数据
            OracleCommand comb1 = new OracleCommand();
            comb1.Connection = conb;
            comb1.CommandType = CommandType.StoredProcedure;
            comb1.CommandText = "owcbspkg_jdtj.up_stat_djjdsj('" + bny + "','" + eny + "','" + cycid + "')";

            //命令二,汇总计算评价单元阶段数据
            OracleCommand comb2 = new OracleCommand();
            comb2.Connection = conb;
            comb2.CommandType = CommandType.StoredProcedure;
            comb2.CommandText = " owcbspkg_jdtj.up_stat_pjdyjdsj('" + bny + "','" + eny + "','" + cycid + "')";

            //命令三,汇总计算区块阶段数据
            OracleCommand comb3 = new OracleCommand();
            comb3.Connection = conb;
            comb3.CommandType = CommandType.StoredProcedure;
            comb3.CommandText = "owcbspkg_jdtj.up_stat_qkjdsj('" + bny + "','" + eny + "','" + cycid + "')";

            try
            {

                //执行命令一,创建单井阶段评价数据
                try
                {
                    comb1.ExecuteNonQuery();
                    Label1.Text = "创建单井阶段评价数据成功!";
                }
                catch (OracleException Error)
                {
                    Label1.Text = "错误: " + Error.Message.ToString();
                    err = 1;
                }
                //执行命令二,汇总计算评价单元阶段数据
                try
                {
                    comb2.ExecuteNonQuery();
                    Label2.Text = "汇总计算评价单元阶段数据成功!";

                }
                catch (OracleException Error)
                {
                    Label2.Text = "错误: " + Error.Message.ToString();
                    err = 1;
                }
                //执行命令三,汇总计算区块阶段数据
                try
                {
                    comb3.ExecuteNonQuery();
                    Label3.Text = "汇总计算区块阶段数据成功!";
                }
                catch (OracleException Error)
                {
                    Label3.Text = "错误: " + Error.Message.ToString();
                    err = 1;
                }

                //判断成功执行
                if (err == 0)
                {
                    Label5.Text = "正在处理中,请稍后...  全部成功!";
                    createjdtime();
                }
                else
                {
                    Label5.Text = "正在处理中,请稍后...  有错误!";
                    Response.Write("<script>alert('计算有错误!'); </script>");
                }
            }
            catch (OracleException Error)
            {
                Label5.Text = Error.Message.ToString() + "错误!";
            }
            conb.Close();
        }
    }
}
