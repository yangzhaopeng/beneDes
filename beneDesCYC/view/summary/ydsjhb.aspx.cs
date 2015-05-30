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

namespace beneDesCYC.view.summary
{
    public partial class ydsjhb : beneDesCYC.core.UI.corePage
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
                string Tbny = _getParam("Date");
                string list = _getParam("CYC");

                //if (list == "null")
                //{
                //    Panel1.Visible = false;
                //    Label5.Visible = false;
                //}
                //else
                //{
                Label5.Visible = false;
                confirmClick();
                //}
            }



        }
        //protected void createjdtime()
        //{

        //    //命令四,建立阶段效益评价时间库
        //    //取得系统时间

        //    string bny = _getParam("month");
        //    string eny = _getParam("endMonth");

        //    Session["jbny"] = bny;
        //    Session["jeny"] = eny;

        //    OracleConnection conb = DB.GetConn();
        //    //OracleConnection conb = DB.CreatConnection();
        //    conb.Open();
        //    string sqlt = "select sysdate time from dual";
        //    OracleCommand commt = new OracleCommand(sqlt, conb);
        //    try
        //    {
        //        OracleDataReader drt = commt.ExecuteReader();
        //        drt.Read();
        //        this.Labelctime.Text = drt.GetOracleDateTime(0).ToString();
        //        drt.Close();
        //    }
        //    catch (OracleException Error)
        //    {
        //        Label4.Text = "取系统时间错误: " + Error.Message.ToString();
        //        Label4.Visible = true;
        //    }

        //    //建立表,首先判断表是否存在
        //    #region
        //    string sqlt2 = "select count(*) from user_tables where table_name = 'JDTIME'";
        //    OracleCommand commt2 = new OracleCommand(sqlt2, conb);
        //    try
        //    {
        //        OracleDataReader drt2 = commt2.ExecuteReader();
        //        drt2.Read();
        //        Label4.Text = drt2[0].ToString();
        //        if (drt2[0].ToString() == "1") //表已经存在,则插入表即可
        //        {
        //            string sqlt3 = "insert into jdtime (ctime,ftime,ltime) values('" + this.Labelctime.Text + "','" + bny + "','" + eny + "')";
        //            OracleCommand commt3 = new OracleCommand(sqlt3, conb);
        //            try
        //            {
        //                commt3.ExecuteNonQuery();
        //            }
        //            catch (OracleException Error)
        //            {
        //                Label4.Text = "表存在,插入时间错误: " + Error.Message.ToString();
        //                Label4.Visible = true;
        //            }

        //        }
        //        else  //表不存在,则建立表,再插入表
        //        {
        //            //创建表
        //            string sqlt4 = "create table jdtime (ctime varchar(30) primary key ,ftime varchar(20) not null, ltime varchar(20) not null )";
        //            OracleCommand commt4 = new OracleCommand(sqlt4, conb);
        //            try
        //            {
        //                commt4.ExecuteNonQuery();
        //            }
        //            catch (OracleException Error)
        //            {
        //                Label4.Text = "表不存在,创建表错误: " + Error.Message.ToString();
        //                Label4.Visible = true;
        //            }
        //            //插入表
        //            string sqlt5 = "insert into jdtime (ctime,ftime,ltime) values('" + this.Labelctime.Text + "','" + bny + "','" + eny + "')";
        //            OracleCommand commt5 = new OracleCommand(sqlt5, conb);
        //            try
        //            {
        //                commt5.ExecuteNonQuery();
        //            }
        //            catch (OracleException Error)
        //            {
        //                Label4.Text = "表不存在,创建后插入错误: " + Error.Message.ToString();
        //                Label4.Visible = true;
        //            }
        //        }
        //        drt2.Close();

        //    }
        //    catch (OracleException Error)
        //    {
        //        Label4.Text = "错误: " + Error.Message.ToString();
        //        Label4.Visible = true;


        //    }
        //    conb.Close();
        //    #endregion

        //}
        protected void confirmClick()
        {
            string Tbny = _getParam("Date");
            // string Tbny = _getParam("startMonth");
            //   string eny = _getParam("endMonth");

            //Session["jbny"] = bny;
            //Session["jeny"] = eny;

            if (Tbny == "")
            {
                Response.Write("<script>alert('年月不能为空!')</script>");
            }
            //else if (int.Parse(bny) > int.Parse(eny))
            //{
            //    Response.Write("<script>alert('开始日期不能大于结束日期!')</script>");
            //}
            //else if (int.Parse(bny) == int.Parse(eny))
            //{
            //    Response.Write("<script>alert('至少选择两个月的时间!')</script>");
            //}
            else
            {
                string cycid = Session["cyc_id"].ToString();
                string suser = Session["userId"].ToString();
                string cqcid = Session["cqc_id"].ToString();

                DateTime ctime = System.DateTime.Now;
                //  string tt = ctime.Year + ctime.Month + ctime.Day + " " + ctime.Hour + ":" + ctime.Minute + ":" + ctime.Second;
                string tt = ctime.ToString();

                ExcuteProcToHUIZONG(cycid, Tbny, suser, tt);
                if (cycid != cqcid)
                    ExcuteProcToHUIZONG(cqcid, Tbny, suser, tt);
            }
        }

        /// <summary>
        /// 进行数据汇总@yzp
        /// </summary>
        /// <param name="cycid"></param>
        /// <param name="Tbny"></param>
        /// <param name="suser"></param>
        /// <param name="tt"></param>
        private void ExcuteProcToHUIZONG(string cycid, string Tbny, string suser, string tt)
        {
            int err = 0;  //err= 0 表示没错误
            Panel1.Visible = true;
            Label5.Visible = true;
            Label5.Text = "正在处理中,请稍后...";

            OracleConnection conb = DB.GetConn();
            //OracleConnection conb = DB.CreatConnection();
            conb.Open();

            //命令一,创建单井阶段评价数据
            OracleCommand comb1 = new OracleCommand();
            comb1.Connection = conb;
            comb1.CommandType = CommandType.StoredProcedure;
            comb1.CommandText = "owcbspkg_kfsj.update_djcomputecolumn_all('" + Tbny + "','" + cycid + "')";


            //命令二,汇总计算评价单元阶段数据
            OracleCommand comb2 = new OracleCommand();
            comb2.Connection = conb;
            comb2.CommandType = CommandType.StoredProcedure;
            comb2.CommandText = " owcbspkg_ydsj.up_stat_cyczqydsj('" + Tbny + "','" + cycid + "')";

            //命令三,汇总计算区块阶段数据
            OracleCommand comb3 = new OracleCommand();
            comb3.Connection = conb;    
            comb3.CommandType = CommandType.StoredProcedure;
            comb3.CommandText = "owcbspkg_ydsj.up_create_djydsj('" + Tbny + "','" + cycid + "')";

            //命令四,汇总评价单元月度数据  Update pjdysj
            OracleCommand comb4 = new OracleCommand();
            comb4.Connection = conb;
            comb4.CommandType = CommandType.StoredProcedure;
            comb4.CommandText = "owcbspkg_ydsj.up_stat_pjdyydsj('" + Tbny + "','" + cycid + "')";

            //命令五,汇总区块月度数据   Update qksj
            OracleCommand comb5 = new OracleCommand();
            comb5.Connection = conb;
            comb5.CommandType = CommandType.StoredProcedure;
            comb5.CommandText = "owcbspkg_ydsj.up_stat_qkydsj('" + Tbny + "','" + cycid + "')";

            //命令六,写日志
            OracleCommand comb6 = new OracleCommand();
            comb6.Connection = conb;
            comb6.CommandType = CommandType.StoredProcedure;
            //comb6.CommandText = "owcbspkg_ydsj.up_log_cychz(" + Tbny.Text + "," + suser + "," + tt + ")"; //错误语句
            comb6.CommandText = "owcbspkg_ydsj.up_log_cychz('" + Tbny + "','" + suser + "','" + tt + "','" + cycid + "')";

            //命令七,汇总计算单井费用  Insert Into stat_djydsj
            OracleCommand comb7 = new OracleCommand();
            comb7.Connection = conb;
            comb7.CommandType = CommandType.StoredProcedure;
            comb7.CommandText = "owcbspkg_ydsj.up_prepare_zyqzqdata('" + Tbny + "','" + cycid + "')";

            //命令八,汇总计算单井成本  Update zyqydsj
            OracleCommand comb8 = new OracleCommand();
            comb8.Connection = conb;
            comb8.CommandType = CommandType.StoredProcedure;
            comb8.CommandText = " owcbspkg_ydsj.up_stat_zyqzqcb('" + Tbny + "','" + cycid + "')";
            try
            {

                //执行命令一,计算开发数据,
                try
                {
                    comb1.ExecuteNonQuery();

                }
                catch (OracleException Error)
                {
                    Label1.Text = "错误: " + Error.Message.ToString();
                    err = 1;
                }
                //执行命令二,汇总开发数据
                try
                {
                    comb2.ExecuteNonQuery();
                    Label1.Text = "汇总计算成功!";

                }
                catch (OracleException Error)
                {
                    Label1.Text = "汇总计算错误: " + Error.Message.ToString();
                    err = 1;
                }
                //执行命令三,创建单井月度数据库
                try
                {
                    comb3.ExecuteNonQuery();
                    Label2.Text = "创建单井月度数据成功!";
                }
                catch (OracleException Error)
                {
                    Label2.Text = "创建单井月度数据错误: " + Error.Message.ToString();
                    err = 1;
                }
                //执行命令四,汇总评价单元月度数据
                try
                {
                    comb4.ExecuteNonQuery();
                    Label3.Text = "汇总评价单元月度数据成功!";
                }
                catch (OracleException Error)
                {
                    Label3.Text = "汇总评价单元月度数据错误: " + Error.Message.ToString();
                    err = 1;
                }
                //执行命令五,汇总区块月度数据
                try
                {
                    comb5.ExecuteNonQuery();
                    Label4.Text = "汇总区块月度数据成功!";
                }
                catch (OracleException Error)
                {
                    Label4.Text = "汇总区块月度数据错误: " + Error.Message.ToString();
                    err = 1;
                }
                //执行命令六,写日志
                try
                {
                    comb6.ExecuteNonQuery();
                    Label6.Visible = false;
                }
                catch (OracleException Error)
                {
                    Label6.Text = "写日志错误: " + Error.Message.ToString();
                    err = 1;
                }
                //执行命令七,汇总计算单井费用
                try
                {
                    comb7.ExecuteNonQuery();
                    Label7.Text = "汇总计算单井费用成功!";
                }
                catch (OracleException Error)
                {
                    Label7.Text = "错误: " + Error.Message.ToString();
                    err = 1;
                }
                //执行命令八,汇总计算单井成本
                try
                {
                    comb8.ExecuteNonQuery();
                    Label8.Text = "汇总计算单井成本成功!";

                }
                catch (OracleException Error)
                {
                    Label8.Text = "错误: " + Error.Message.ToString();
                    err = 1;
                }
                if (err == 0)
                {
                    Label5.Text = "正在处理中,请稍后...  全部成功!";

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
