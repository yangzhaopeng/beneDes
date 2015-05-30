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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace beneDesYGS.view.oilAssessment
{
    public partial class oilxypj : beneDesYGS.core.UI.corePage
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
            //    Imagebutton1.Attributes.Add("onclick ", "ShowBar() ");
            //    Panel1.Visible = false;
            //    Label5.Visible = false;
            //    //bny = "200607";
            //    //eny = "200707";
            //    string ny = Session["date"].ToString();
            //    ny = ny.Substring(0, 4);
            //    bny = ny + "01";
            //    eny = Session["date"].ToString();
            //    //Tbnyf.Enabled = false;
            //    //Tbnyl.Enabled = false;
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

        protected void confirmClick()
        {
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");

            Session["bny"] = bny;
            Session["eny"] = eny;

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

                int err = 0;  //err= 0 表示没错误
                Panel1.Visible = true;
                //    Label5.Visible = true;
                Labelctime.Visible = false;
                Label4.Visible = false;
                Label5.Text = "正在处理中,请稍后...";
                //DateTime ctime = System.DateTime.Now;
                //string tt = ctime.Year + ctime.Month + ctime.Day + " " + ctime.Hour + ":" + ctime.Minute + ":" + ctime.Second;

                OracleConnection conb = DB.GetConn();
                //OracleConnection conb = DB.CreatConnection();
                conb.Open();

                //命令一,创建单井阶段评价数据
                OracleCommand comb1 = new OracleCommand();
                comb1.Connection = conb;
                comb1.CommandType = CommandType.StoredProcedure;
                comb1.CommandText = "owcbspkg_dttj.up_stat_djjdsj('" + bny + "','" + eny + "')";

                //命令二,汇总计算评价单元阶段数据
                OracleCommand comb2 = new OracleCommand();
                comb2.Connection = conb;
                comb2.CommandType = CommandType.StoredProcedure;
                comb2.CommandText = " owcbspkg_dttj.up_stat_pjdyjdsj('" + bny + "','" + eny + "')";

                //命令三,汇总计算区块阶段数据
                OracleCommand comb3 = new OracleCommand();
                comb3.Connection = conb;
                comb3.CommandType = CommandType.StoredProcedure;
                comb3.CommandText = "owcbspkg_dttj.up_stat_qkjdsj('" + bny + "','" + eny + "')";

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
}
