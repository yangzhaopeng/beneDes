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

namespace beneDesYGS.view.month
{
    public partial class csdy : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          

            if (!IsPostBack)
            {
                txtNY.Text = Session["month"].ToString();
                tb_DataBound();
                txtNY.Enabled = false;
                txtCBJ.Enabled = false;
                txtGHS.Enabled = false;
                txtDCN.Enabled = false;
            }
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }
        protected void XG_Click(object sender, EventArgs e)
        {
            labShow.Text = "";
            if (txtCBJ.Text != "" && txtGHS.Text != "" && txtDCN.Text != "")
            {
                Label.Text = "修改";
            }
            txtCBJ.Enabled = true;
            txtGHS.Enabled = true;
            txtDCN.Enabled = true;
        }
        protected void BC_Click(object sender, EventArgs e)
        {
            SqlHelper conn = new SqlHelper();
            OracleConnection Con = conn.GetConn();
            //OracleConnection Con = DB.CreatConnection();
            Con.Open();
            OracleCommand Cmd1 = new OracleCommand();
            string CBJ = txtCBJ.Text;
            string NY = txtNY.Text;
            OracleCommand Cmd2 = new OracleCommand();
            OracleCommand Cmd3 = new OracleCommand();
            string Sql1 = "";
            string Sql2 = "";
            string Sql3 = "";

            if (Label.Text == "修改")
            {
                Sql1 = "update csdy_info set num='" + txtCBJ.Text + "' where name='特高成本井' and ny='" + txtNY.Text + "'";
                Sql2 = "update csdy_info set num='" + txtGHS.Text + "' where name='高含水' and ny='" + txtNY.Text + "'";
                Sql3 = "update csdy_info set num='" + txtDCN.Text + "' where name='低产能' and ny='" + txtNY.Text + "'";
            }
            else
            {
                Sql1 = "insert into csdy_info(num,name,ny) values ('" + txtCBJ.Text + "','特高成本井','" + txtNY.Text + "')";
                Sql2 = "insert into csdy_info(num,name,ny) values ('" + txtGHS.Text + "','高含水','" + txtNY.Text + "')";
                Sql3 = "insert into csdy_info(num,name,ny) values ('" + txtDCN.Text + "','低产能','" + txtNY.Text + "')";
            }
            Cmd1 = new OracleCommand(Sql1, Con);
            Cmd2 = new OracleCommand(Sql2, Con);
            Cmd3 = new OracleCommand(Sql3, Con);
            OracleTransaction tran1 = Con.BeginTransaction();
            Cmd1.Transaction = tran1;
            Cmd2.Transaction = tran1;
            Cmd3.Transaction = tran1;


            try
            {
                int reflectrows1 = Convert.ToInt32(Cmd1.ExecuteNonQuery().ToString());
                int reflectrows2 = Convert.ToInt32(Cmd2.ExecuteNonQuery().ToString());
                int reflectrows3 = Convert.ToInt32(Cmd3.ExecuteNonQuery().ToString());
                if (reflectrows1 != 0 && reflectrows2 != 0 && reflectrows3 != 0)
                {
                    labShow.Text = "数据保存成功！";
                    tran1.Commit();

                }

                else
                {
                    labShow.Text = "数据未保存成功，请重新输入！";
                }

            }
            catch
            {
                tran1.Rollback();
                labShow.Text = "数据无效，请重新输入！";

            }
            finally
            {
                Con.Close();
            }
            if (labShow.Text == "数据保存成功！")
            {
                tb_DataBound();

                try
                {
                    string ConnectionXM = "Data Source=orcl;User ID='jilincyc';Password='jilincyc';Unicode=True";
                    OracleConnection conXM = new OracleConnection(ConnectionXM);
                    conXM.Open();
                    string XM = "delete from csdy_Info where ny='" + txtNY.Text + "'";
                    OracleCommand CmdXM = new OracleCommand(XM, conXM);
                    CmdXM.ExecuteNonQuery();
                    string BC1 = "insert into csdy_info(name,num,ny) values('特高成本井','" + txtCBJ.Text + "','" + txtNY.Text + "')";
                    OracleCommand CmdBC1 = new OracleCommand(BC1, conXM);
                    CmdBC1.ExecuteNonQuery();

                    string BC2 = "insert into csdy_info(name,num,ny) values('高含水','" + txtGHS.Text + "','" + txtNY.Text + "')";
                    OracleCommand CmdBC2 = new OracleCommand(BC2, conXM);
                    CmdBC2.ExecuteNonQuery();

                    string BC3 = "insert into csdy_info(name,num,ny) values('低产能','" + txtDCN.Text + "','" + txtNY.Text + "')";
                    OracleCommand CmdBC3 = new OracleCommand(BC3, conXM);
                    CmdBC3.ExecuteNonQuery();
                    conXM.Close();
                }
                catch
                { }
            }
        }

        public void tb_DataBound()
        {
            string Sql1 = "select nvl(num,0) as num from csdy_info where ny='" + txtNY.Text + "' and name='特高成本井'";
            string Sql2 = "select nvl(num,0) as num from csdy_info where ny='" + txtNY.Text + "' and name='高含水'";
            string Sql3 = "select nvl(num,0) as num from csdy_info where ny='" + txtNY.Text + "' and name='低产能'";
            try
            {
                SqlHelper conn = new SqlHelper();
                OracleConnection Con = conn.GetConn();

                //OracleConnection Con = DB.CreatConnection();
                Con.Open();
                OracleDataAdapter da1 = new OracleDataAdapter(Sql1, Con);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "cbj");
                txtCBJ.Text = ds1.Tables["cbj"].Rows[0][0].ToString();

                OracleDataAdapter da2 = new OracleDataAdapter(Sql2, Con);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2, "ghs");
                txtGHS.Text = ds2.Tables["ghs"].Rows[0][0].ToString();

                OracleDataAdapter da3 = new OracleDataAdapter(Sql3, Con);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3, "dcn");
                txtDCN.Text = ds3.Tables["dcn"].Rows[0][0].ToString();
                Con.Close();
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不存在该年月记录!');</script>");
            }
        }
    }
}
