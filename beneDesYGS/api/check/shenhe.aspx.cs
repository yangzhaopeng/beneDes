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


namespace beneDesYGS.api.check
{
    public partial class shenhe : beneDesYGS.core.UI.corePage
    {
        protected static SqlHelper DB = new SqlHelper();
        protected static OracleConnection con = DB.GetConn();

        protected void Page_Load(object sender, EventArgs e)
        {
            string _type = _getParam("_type");

            switch (_type)
            {
                case "getAllList":
                    getAllList();
                    break;
                case "shenheClick":
                    shenheClick();
                    break;

            }
            //this.txtny.Text = Session["date"].ToString();      
        }


        public void getAllList()
        {
            string cycid = "";
            string ny = "";
            con.Open();
            string type = Session["DEP_TYPE"].ToString();
            string sql = @"select ny,dep_name,shenhe_info.dep_id,sbtime,case shenhe when 1 then '已审核' when 0 then '未审核' end as shenhe,
            case has_suo when 1 then '已锁定' when 0 then '未锁定' end as has_suo, shenheren 
            from shenhe_info join department on shenhe_info.dep_id=department.dep_id 
            where has_suo<>'1' and DEP_TYPE='{0}' order by ny desc";
            sql = string.Format(sql, type);
         
            OracleDataAdapter myda = new OracleDataAdapter(sql, con);
            DataSet myds = new DataSet();
            myda.Fill(myds, "shenhe");           
            con.Close();
            DataTable dt = myds.Tables["shenhe"];
            dt.Columns.Add("REMARK", typeof(System.String));
            for (int i = 0; i < dt.Rows.Count;i++ )
            {
                cycid = dt.Rows[i]["dep_id"].ToString();
                ny = dt.Rows[i]["ny"].ToString();
                dt.Rows[i]["REMARK"] = "<a href='../../view/check/shenhebiao.aspx?NY=" + ny + "&CYCID=" + cycid + "' target='_blank'><img src='../../static/image/look.png'></a>";
            }
            _return(true, "", dt);
        }

        public void shenheClick()
        {
            string sureny = _getParam("NY");
            //出现对话框，则判断。。如果确定的话，进行缩定，否则，跳会原来界面
            string ny1 = _getParam("NY");
            string cyc1 = _getParam("DEP_NAME");

            con.Open();
            string sqlfind = "select * from shenhe_info where ny='" + ny1 + "' and dep_id in (select dep_id from department where dep_name='" + cyc1 + "')";
            OracleDataAdapter dafind1 = new OracleDataAdapter(sqlfind, con);
            DataSet dsfind1 = new DataSet();
            dafind1.Fill(dsfind1, "find");
            if (dsfind1.Tables["find"].Rows.Count == 0)
            {
                //Response.Write("<script>alert('该月数据尚未上报')</script>");
                _return(false, "该月数据尚未上报", "null");
            }
            else
            {  // 此处修改过
                if (dsfind1.Tables["find"].Rows[0][3].ToString() == "1" && dsfind1.Tables["find"].Rows[0][4].ToString() == "1")
                {
                    //Response.Write("<script>alert('该月数据已经被审核过，如果需要重新上报，请点击“解锁”')</script>");
                    _return(false, "该月数据已经被审核过", "null");
                }
                else
                {

                    //Response.Write("<script>if(confirm('确定要提交当前数据')){ document.location.href='/api/check/shenhe.aspx?op=" + sureny + "&op1=" + cyc1 + "';}</script>");
                    
                        String parm = sureny;
                        string parm2 = cyc1;
                        //调用审核函数
                        shenhecz(parm, parm2);

                        _return(true, "审核成功！", "null");

                }
            }
        }

        public void shenhecz(string parm, string parm2)
        {

            string ny = parm;
            string cyc = parm2;

            //con.Open();

            try
            {
                string sqlsh = "update shenhe_info set shenhe='1',has_suo='1' where ny='" + ny + "' and  dep_id in (select dep_id from department where dep_name='" + cyc + "')";
                OracleCommand cmd = new OracleCommand(sqlsh, con);
                cmd.ExecuteNonQuery();

                //Response.Write("<script>alert('审核成功')</script>");
            }
            catch (Exception e)
            {
                Response.Write("<script>alet('" + e + "')</script>");
            }
            con.Close();
            
        }

        
    }
}
