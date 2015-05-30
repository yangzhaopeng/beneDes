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
    public partial class kfsjtq1 : beneDesCYC.core.UI.corePage
    {
        protected SqlHelper DB = new SqlHelper();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            string list = _getParam("CYC");
            if (list == "null")
            {
                if (Request.QueryString["op"] != null && !Request.QueryString["op"].Equals(""))//del
                {
                    String parm = Request.QueryString["op"];
                    this.delTable(parm);
                }
            }
            else
            {
                confirmClick();
            
            }
            
            if (!IsPostBack)
            {

                if (Request.QueryString["op"] != null && !Request.QueryString["op"].Equals(""))//del
                {
                    String parm = Request.QueryString["op"];
                    this.delTable(parm);
                }
            }



        }

        protected void confirmClick()

        {

            //  string Tbny = _getParam("startMonth");
            string sureny = _getParam("startMonth");
            string cyc = Session["cyc_id"].ToString();

            OracleConnection conn = DB.GetConn();
            string sql = "select ny from kfsj_tq where ny='" + sureny + "' and cyc_id='" + cyc + "'";
            OracleDataAdapter myda1 = new OracleDataAdapter(sql, conn);
            DataSet myds1 = new DataSet();
            myda1.Fill(myds1, "sure");

            if (myds1.Tables["sure"].Rows.Count == 0)
            {
                //提示框没有本月开发数据 kfsj_tq中没有该年月的数据  

                Response.Write("<script>alert('没有本月开发数据')</script>");
            }
            else
            {
                string sql2 = "select ny from kfsj where ny='" + sureny + "' and cyc_id='" + cyc + "'";
                OracleDataAdapter myda2 = new OracleDataAdapter(sql2, conn);
                DataSet myds2 = new DataSet();
                myda2.Fill(myds2, "sure2");
                if (myds2.Tables["sure2"].Rows.Count == 0)
                {
                    //kfsj中没有 ,先从kfsj_tq中取出来直接插入到kfsj中
                    string sqlstr = "select * from kfsj_tq where ny='" + sureny + "' and cyc_id='" + cyc + "'";
                    OracleCommand mycmd1 = new OracleCommand(sqlstr, conn);
                    conn.Open();
                    OracleDataReader myReader;
                    myReader = mycmd1.ExecuteReader();
                    while (myReader.Read())
                    {
                        string dj_id = myReader[2].ToString() + "$" + myReader[1].ToString();
                        string sqlin = "insert into kfsj(NY,DJ_ID,JH,ZYQ  ,QK, ZQLC ,ZQQSBS, CX ,  CYHD  ,ZSL,ZQL ,CYL,CSL , CYAOL ,SCSJ ,JKCYL  ,JKCYOUL ,HSCYL,JKCQL ,HSCQL,HS,LJCYL,LJZSL,LJCQL, LJZQL ,DZCSDM ,GYCSDM  , FXYYDM , BZ,cyc_id) values";
                        sqlin = sqlin + "('" + myReader[0].ToString() + "','" + dj_id + "','" + myReader[1].ToString() + "','" + myReader[2].ToString() + "','" + myReader[3].ToString() + "','" + myReader[6].ToString() + "','" + myReader[7].ToString() + "','" + myReader[8].ToString() + "',";
                        sqlin = sqlin + "'" + myReader[10].ToString() + "','" + myReader[11].ToString() + "','" + myReader[12].ToString() + "','" + myReader[13].ToString() + "','" + myReader[14].ToString() + "','" + myReader[15].ToString() + "','" + myReader[16].ToString() + "',";
                        sqlin = sqlin + "'" + myReader[17].ToString() + "','" + myReader[18].ToString() + "','" + myReader[19].ToString() + "','" + myReader[20].ToString() + "','" + myReader[21].ToString() + "','" + myReader[22].ToString() + "','" + myReader[23].ToString() + "',";
                        sqlin = sqlin + "'" + myReader[24].ToString() + "','" + myReader[25].ToString() + "','" + myReader[26].ToString() + "','" + myReader[27].ToString() + "','" + myReader[28].ToString() + "','" + myReader[28].ToString() + "','" + myReader[30].ToString() + "','" + cyc + "'";
                        sqlin = sqlin + ")";

                        OracleCommand mycmdin = new OracleCommand(sqlin, conn);
                        mycmdin.ExecuteNonQuery();


                    }
                    Response.Write("<script>alert('提取成功')</script>");
                }
                else
                {
                    //弹出框..确定的话,,先做删除,,再做删除.. 
                    //取消什么都不做.
                    Response.Write("<script>if(confirm('kfsj中已经存在对应数据,点击确定将删除原有数据,重新提取')){ document.location.href='kfsjtq.aspx?op=" + sureny + "';}</script>");
                    //call();
                    // Response.Write("}</script>"); 
                }
            }

        }

        private void delTable(string sureny)
        {
            // string sureny = _getParam("startMonth");
            string cyc = Session["cyc_id"].ToString();
            //删除原来数据重新提取...
            OracleConnection conn = DB.GetConn();
            conn.Open();

            string delkfsj = "delete kfsj where ny='" + sureny + "' and cyc_id='" + cyc + "'";
            OracleCommand mykfsj = new OracleCommand(delkfsj, conn);
            mykfsj.ExecuteNonQuery();

            //提取新的数据..进行插入
            string sql2 = "select ny from kfsj where ny='" + sureny + "' and cyc_id='" + cyc + "'";
            OracleDataAdapter myda2 = new OracleDataAdapter(sql2, conn);
            DataSet myds2 = new DataSet();
            myda2.Fill(myds2, "sure2");
            if (myds2.Tables["sure2"].Rows.Count == 0)
            {
                //kfsj中没有 ,先从kfsj_tq中取出来直接插入到kfsj中
                string sqlstr = "select * from kfsj_tq where ny='" + sureny + "' and cyc_id='" + cyc + "'";
                OracleCommand mycmd1 = new OracleCommand(sqlstr, conn);
                //conn.Open();
                OracleDataReader myReader;
                myReader = mycmd1.ExecuteReader();
                while (myReader.Read())
                {
                    string dj_id = myReader[2].ToString() + "$" + myReader[1].ToString();
                    string sqlin = "insert into kfsj(NY,DJ_ID,JH,ZYQ  ,QK, ZQLC ,ZQQSBS, CX ,  CYHD  ,ZSL,ZQL ,CYL,CSL , CYAOL ,SCSJ ,JKCYL  ,JKCYOUL ,HSCYL,JKCQL ,HSCQL,HS,LJCYL,LJZSL,LJCQL, LJZQL ,DZCSDM ,GYCSDM  , FXYYDM , BZ,cyc_id) values";
                    sqlin = sqlin + "('" + myReader[0].ToString() + "','" + dj_id + "','" + myReader[1].ToString() + "','" + myReader[2].ToString() + "','" + myReader[3].ToString() + "','" + myReader[6].ToString() + "','" + myReader[7].ToString() + "','" + myReader[8].ToString() + "',";
                    sqlin = sqlin + "'" + myReader[10].ToString() + "','" + myReader[11].ToString() + "','" + myReader[12].ToString() + "','" + myReader[13].ToString() + "','" + myReader[14].ToString() + "','" + myReader[15].ToString() + "','" + myReader[16].ToString() + "',";
                    sqlin = sqlin + "'" + myReader[17].ToString() + "','" + myReader[18].ToString() + "','" + myReader[19].ToString() + "','" + myReader[20].ToString() + "','" + myReader[21].ToString() + "','" + myReader[22].ToString() + "','" + myReader[23].ToString() + "',";
                    sqlin = sqlin + "'" + myReader[24].ToString() + "','" + myReader[25].ToString() + "','" + myReader[26].ToString() + "','" + myReader[27].ToString() + "','" + myReader[28].ToString() + "','" + myReader[28].ToString() + "','" + myReader[30].ToString() + "','" + cyc + "'";
                    sqlin = sqlin + ")";

                    OracleCommand mycmdin = new OracleCommand(sqlin, conn);
                    mycmdin.ExecuteNonQuery();


                }
                Response.Write("<script>alert('提取成功')</script>");

                // Response.Write("<script>alert('0000000+" + aa + "');document.location.href='kfsjtq.aspx';</script>");
            }
            conn.Close();
        }
    }
}