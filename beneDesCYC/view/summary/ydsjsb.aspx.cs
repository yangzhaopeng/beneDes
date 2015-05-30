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
    public partial class ydsjsb : beneDesCYC.core.UI.corePage
    {
        protected SqlHelper DBHelper = new SqlHelper();

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
                //string list = _getParam("CYC");
                //if (list == "null")
                //{
                //    Panel1.Visible = false;
                //    Label5.Visible = false;
                //}
                //else
                //{
                //    Label5.Visible = false;
                confirmClick();
                //}
            }



        }
        //protected void createjdtime()
        //{

        //    //命令四,建立阶段效益评价时间库
        //    //取得系统时间

        //    string bny = _getParam("startMonth");
        //    string eny = _getParam("endMonth");

        //    Session["jbny"] = bny;
        //    Session["jeny"] = eny;

        //    OracleConnection conb = DBHelper.GetConn();
        //    //OracleConnection conb = DBHelper.CreatConnection();
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
            string cycid = Session["cyc_id"].ToString();
            string cqcid = Session["cqc_id"].ToString();

            shangbao(cycid);
            if (cycid != cqcid)
                shangbao(cqcid);
        }
        private void shangbao(string dep_id)
        {
            string Tbny = _getParam("Date");
            //string Tbny = _getParam("startMonth");
            //先判断是否上报
            //是，提示无法上报
            //否，则上报，更新ygs数据库中shangbao_info信息，二次上报的要更新，初次上报的插入新的信息
            //首先判断有没有信息，，无插入，有（a，查看锁的信息，（1），锁定，（2）没有锁定，更新信息）
            OracleConnection conncyc = DBHelper.GetConn();
            SqlHelper sqlhelper = new SqlHelper();

            //conncyc.Open();
            string sqlcyc = "select dep_id from department where dep_id='" + dep_id + "'";
            //OracleDataAdapter myda = new OracleDataAdapter(sqlcyc, conncyc);
            //DataSet myds = new DataSet();
            //myda.Fill(myds, "cyc_id");
            //conncyc.Close();

            //  string ny = this.Tbny.Text;

            //string sqlcyc = "select cyc_id from department where (cyc_id='" + Session["cyc_id"].ToString() + "' or cyc_id='" + Session["cqc_id"].ToString() + "')";
            DataSet myds = sqlhelper.GetDataSet(sqlcyc);

            //string dep_id = myds.Tables[0].Rows[0][0].ToString();
            string sbtime = System.DateTime.Now.ToLongDateString().ToString();



            //string connectionygs = "Data Source=orcl;User ID=ygs;Password=ygs;Unicode=True";
            //OracleConnection connygs = new OracleConnection(connectionygs);

            OracleConnection connygs = DB.CreatConnectionygs();
            connygs.Open();

            //先查找相应的记录
            string sqlfind = "select * from shenhe_info where ny='" + Tbny + "' and dep_id='" + dep_id + "'";
            OracleDataAdapter dafind = new OracleDataAdapter(sqlfind, connygs);
            DataSet dsfind = new DataSet();
            dafind.Fill(dsfind, "find");
            if (dsfind.Tables["find"].Rows.Count == 0)
            {
                //没有上报过，上报时插入新的记录
                string sqlin = "insert into shenhe_info(ny,dep_id,sbtime,shenhe,has_suo,bz) values('" + Tbny + "','" + dep_id + "','" + sbtime + "',0,0,'')";
                OracleCommand mycmd = new OracleCommand(sqlin, connygs);
                mycmd.ExecuteNonQuery();
                shangbaoku();
            }
            else
            {
                //上报过，更新数据
                if (dsfind.Tables["find"].Rows[0][4].ToString() == "1")
                {
                    Response.Write("<script>alert('数据已经上报，且被锁定，如需重新上报，请联系油公司')</script>");
                }
                else
                {
                    string bznew = dsfind.Tables["find"].Rows[0][2].ToString() + "," + dsfind.Tables["find"].Rows[0][2].ToString();
                    string sqlupdate = "update shenhe_info set sbtime='" + sbtime + "',bz='" + bznew + "' where ny='" + Tbny + "' and dep_id='" + dep_id + "'";
                    OracleCommand upcmd = new OracleCommand(sqlupdate, connygs);
                    upcmd.ExecuteNonQuery();
                    shangbaoku();
                }
            }
            connygs.Close();
        }
        protected void shangbaoku()
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
                string cyc_id = Session["cyc_id"].ToString();
                string cqc_id = Session["cqc_id"].ToString();



                //  string suser = Session["userId"].ToString();
                int err = 0;  //err= 0 表示没错误
                Panel1.Visible = true;
                Label5.Visible = true;
                Label5.Text = "正在处理中,请稍后...";

                OracleConnection conb = DBHelper.GetConn();

                conb.Open();


                //命令一,上报单井月度数据
                OracleCommand comb1 = new OracleCommand();
                comb1.Connection = conb;
                comb1.CommandType = CommandType.StoredProcedure;
                //命令二,上报评价单元月度数据
                OracleCommand comb2 = new OracleCommand();
                comb2.Connection = conb;
                comb2.CommandType = CommandType.StoredProcedure;
                //命令三,上报区块月度数据
                OracleCommand comb3 = new OracleCommand();
                comb3.Connection = conb;
                comb3.CommandType = CommandType.StoredProcedure;
                //命令四,上报月度参数数据
                //OracleCommand comb4 = new OracleCommand();
                //comb4.Connection = conb;
                //comb4.CommandType = CommandType.StoredProcedure;
                //命令六,上报单井数据
                OracleCommand comb6 = new OracleCommand();
                comb6.Connection = conb;
                comb6.CommandType = CommandType.StoredProcedure;
                //命令七,上报开发数据
                OracleCommand comb7 = new OracleCommand();
                comb7.Connection = conb;
                comb7.CommandType = CommandType.StoredProcedure;
                //命令一,上报单井月度数据
                comb1.CommandText = "owcbs_dbtransfer.up_upload_djydsj('" + Tbny + "','" + cqc_id + "')";
                //命令二,上报评价单元月度数据
                comb2.CommandText = " owcbs_dbtransfer.up_upload_pjdyydsj('" + Tbny + "','" + cqc_id + "')";
                //命令三,上报区块月度数据
                comb3.CommandText = "owcbs_dbtransfer.up_upload_qkydsj('" + Tbny + "','" + cqc_id + "')";
                //命令四,上报月度参数数据
                //comb4.CommandText = "owcbs_dbtransfer.up_upload_ydcs('" + Tbny + "','" + cqc_id + "')";
                //命令六,上报单井数据
                comb6.CommandText = "owcbs_dbtransfer.up_upload_djsj('" + Tbny + "','" + cqc_id + "')";
                //命令七,上报开发数据
                comb7.CommandText = "owcbs_dbtransfer.up_upload_kfsj('" + Tbny + "','" + cqc_id + "')";

                //执行命令六,上报单井数据
                try
                {
                    comb6.ExecuteNonQuery();
                    Label6.Text = "上报单井数据成功!";
                }
                catch (OracleException Error)
                {
                    Label6.Text = "错误: " + Error.Message.ToString();
                    err = 1;
                }
                //执行命令七,上报开发数据
                try
                {
                    comb7.ExecuteNonQuery();
                    Label7.Text = "上报开发数据成功!";
                }
                catch (OracleException Error)
                {
                    Label7.Text = "错误: " + Error.Message.ToString();
                    err = 1;
                }

                //执行命令一,上报单井月度数据
                try
                {
                    comb1.ExecuteNonQuery();
                    Label1.Text = "上报单井月度数据成功!";
                }
                catch (OracleException Error)
                {
                    Label1.Text = "错误: " + Error.Message.ToString();
                    err = 1;
                }
                //执行命令二,上报评价单元月度数据
                try
                {
                    comb2.ExecuteNonQuery();
                    Label2.Text = "上报评价单元月度数据成功!";

                }
                catch (OracleException Error)
                {
                    Label2.Text = "错误: " + Error.Message.ToString();
                    err = 1;
                }
                //执行命令三,上报区块月度数据
                try
                {
                    comb3.ExecuteNonQuery();
                    Label3.Text = "上报区块月度数据成功!";
                }
                catch (OracleException Error)
                {
                    Label3.Text = "错误: " + Error.Message.ToString();
                    err = 1;
                }
                //执行命令四,上报月度参数数据
                try
                {
                    //comb4.ExecuteNonQuery();
                    //Label4.Text = "上报月度参数数据成功!";
                }
                catch (OracleException Error)
                {
                    //Label4.Text = "错误: " + Error.Message.ToString();
                    //err = 1;
                }
                if (cyc_id != cqc_id)
                {
                    comb1.CommandText = "owcbs_dbtransfer.up_upload_djydsj('" + Tbny + "','" + cyc_id + "')";
                    comb2.CommandText = " owcbs_dbtransfer.up_upload_pjdyydsj('" + Tbny + "','" + cyc_id + "')";
                    comb3.CommandText = "owcbs_dbtransfer.up_upload_qkydsj('" + Tbny + "','" + cyc_id + "')";
                    //comb4.CommandText = "owcbs_dbtransfer.up_upload_ydcs('" + Tbny + "','" + cyc_id + "')";
                    comb6.CommandText = "owcbs_dbtransfer.up_upload_djsj('" + Tbny + "','" + cyc_id + "')";
                    comb7.CommandText = "owcbs_dbtransfer.up_upload_kfsj('" + Tbny + "','" + cyc_id + "')";
                    //执行命令六,上报单井数据
                    try
                    {
                        comb6.ExecuteNonQuery();
                        Label6.Text = "上报单井数据成功!";
                    }
                    catch (OracleException Error)
                    {
                        Label6.Text = "错误: " + Error.Message.ToString();
                        err = 1;
                    }
                    //执行命令七,上报开发数据
                    try
                    {
                        comb7.ExecuteNonQuery();
                        Label7.Text = "上报开发数据成功!";
                    }
                    catch (OracleException Error)
                    {
                        Label7.Text = "错误: " + Error.Message.ToString();
                        err = 1;
                    }

                    //执行命令一,上报单井月度数据
                    try
                    {
                        comb1.ExecuteNonQuery();
                        Label1.Text = "上报单井月度数据成功!";
                    }
                    catch (OracleException Error)
                    {
                        Label1.Text = "错误: " + Error.Message.ToString();
                        err = 1;
                    }
                    //执行命令二,上报评价单元月度数据
                    try
                    {
                        comb2.ExecuteNonQuery();
                        Label2.Text = "上报评价单元月度数据成功!";

                    }
                    catch (OracleException Error)
                    {
                        Label2.Text = "错误: " + Error.Message.ToString();
                        err = 1;
                    }
                    //执行命令三,上报区块月度数据
                    try
                    {
                        comb3.ExecuteNonQuery();
                        Label3.Text = "上报区块月度数据成功!";
                    }
                    catch (OracleException Error)
                    {
                        Label3.Text = "错误: " + Error.Message.ToString();
                        err = 1;
                    }
                    //执行命令四,上报月度参数数据
                    try
                    {
                        //comb4.ExecuteNonQuery();
                        //Label4.Text = "上报月度参数数据成功!";
                    }
                    catch (OracleException Error)
                    {
                        //Label4.Text = "错误: " + Error.Message.ToString();
                        //err = 1;
                    }
                }


                if (err == 0)
                {
                    Label5.Text = "正在处理中,请稍后...  全部成功!";
                    ////1月7日 修改，，加入上报相关信息,提示上报成功
                    //shangbao();
                }
                else
                {
                    Label5.Text = "正在处理中,请稍后...  有错误!";
                    Response.Write("<script>alert('计算有错误!'); </script>");
                }
                conb.Close();


            }

        }
    }
}
