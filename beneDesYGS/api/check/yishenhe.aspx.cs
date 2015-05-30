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

    public partial class yishenhe : beneDesYGS.core.UI.corePage
    {
        protected SqlHelper DB = new SqlHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            string _type = _getParam("_type");

            switch (_type)
            {
                case "getAllList":
                    getAllList();
                    break;
                case "jiesuoClick":
                    jiesuoClick();
                    break;

            }
        }




        public void getAllList()
        {

            string xzny = _getParam("XZNY");
            string Sql;
            Sql = @"select distinct ny,dep_name,case shenhe when 1 then '已审核' when 0 then '未审核' end as shenhe,case has_suo when 1 then '已锁定' when 0 then '未锁定' end as has_suo 
                from shenhe_info join department on shenhe_info.dep_id=department.dep_id where ny='" + xzny + "' and shenhe='1' and has_suo='1' and DEP_TYPE='{0}' order by ny,dep_name";
            string type = Session["DEP_TYPE"].ToString();
            Sql = string.Format(Sql, type);
            SqlHelper DB = new SqlHelper();
            OracleConnection Con = DB.GetConn();
            int rows = 0;
            try
            {
                Con.Open();
                OracleDataAdapter da = new OracleDataAdapter(Sql, Con);
                DataSet ds = new DataSet();
                da.Fill(ds, "dtb_info");
                DataTable dt = ds.Tables["dtb_info"];
                rows = ds.Tables["dtb_info"].Rows.Count;//行数,记录数
                if (rows == 0)
                {
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不存在该年月记录!');</script>");
                    _return(false, "不存在该年月记录!", "null");

                }
                else
                { _return(true, "", dt); }

            }
            catch (OracleException Error)
            {
                string CuoWu = "错误!" + Error.Message.ToString();
                Response.Write(CuoWu);
            }
          

        }

        protected void jiesuoClick()
        {
            string ny = _getParam("NY");
            string dep_name = _getParam("DEP_NAME");
            

            SqlHelper DB = new SqlHelper();
            OracleConnection Con = DB.GetConn();
            //OracleConnection Con = DB.CreatConnection();
            OracleCommand Cmd = new OracleCommand();
            //for (int i = 0; i <= DG.Rows.Count - 1; i++)
            //{
            //    CheckBox cbox = (CheckBox)DG.Rows[i].FindControl("CheckBox1");
            //    if (cbox.Checked == true)
            //    {

                    //string sqlstr = "update shenhe_info set shenhe='0',has_suo='0' where dep_id in (select dep_id from department where dep_name='" + DG.Rows[i].Cells[2].Text + "')";
                    string sqlstr = "update shenhe_info set has_suo='0' where dep_id in (select dep_id from department where dep_name='" + dep_name + "')";
                    sqlstr += " and ny ='" + ny + "'";
                    Cmd = new OracleCommand(sqlstr, Con);
                    Con.Open();
                    if (Convert.ToInt32(Cmd.ExecuteNonQuery().ToString()) != 0)
                    {
                        //labShow.Text = " 解锁成功！";
                        _return(true, "解锁成功！", "null");
                    }
                    else
                    { _return(false, "解锁失败！", "null"); }
                    Con.Close();
                    
                    
                    
            //    }
            //}
          
        }

    }
}
