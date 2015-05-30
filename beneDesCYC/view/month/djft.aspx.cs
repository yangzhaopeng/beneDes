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

    public partial class djft : beneDesCYC.core.UI.corePage
    {
      //  String source_id;
        //String ny;
        //  String ft_type = "QK";
        //OracleConnection conn = DB.CreatConnection();
        //SqlHelper conn = new SqlHelper();
        //OracleConnection conb = conn.GetConn();
        protected SqlHelper DB = new SqlHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
          //  string Tbny = _getParam("Date");

            if (!IsPostBack)
            {
                //  this.list1.Visible = false;
                //  Session["saveFlag"] = false;
              //  string Tbny = _getParam("Date");
                //  txtdate.Text = Session["date"].ToString();//么有第一次pageload，ispostback=false从来没有过
                // this.FpSpread1.Visible = false;
                //  Session["saveFlag"] = false;//设置标记让spread不能保存状态
                //  this.listcyc.Visible = false;
                DDL_Bind();
                DropDownListZYQ.Attributes["onchange"] = String.Format("javascript:document.getElementById('{0}').click()", btnHiddenPostButton.ClientID);
                DropDownListJH.Attributes["onchange"] = String.Format("javascript:document.getElementById('{0}').click()", btnHiddenPostButton.ClientID);
            }
            try
            {
                string Tbny = _getParam("Date");
                //CheckBoxFTJ.Text = DropDownListJH.Text;
                //CheckBoxFTJ.Checked = true;
                //CheckBoxFTJ.Enabled = false;
                OracleConnection conn = DB.GetConn();
                string feef = "select fee from djfy where ny= '" + Tbny + "' and dj_id in (select dj_id from djsj where jh='" + DropDownListJH.SelectedValue + "' and cyc_id = '" + Session["cyc_id"].ToString() + "') and fee_code in (select fee_code from fee_name where fee_name = '" + DropDownListFEE.Text + "') and cyc_id = '" + Session["cyc_id"].ToString() + "'";
                OracleDataAdapter mydatafeef = new OracleDataAdapter(feef, conn);
                DataSet mfeef = new DataSet();
                mydatafeef.Fill(mfeef, "Table_feef");
                LabelFEE.Text = mfeef.Tables["Table_feef"].Rows[0][0].ToString() + "元";
            }
            catch
            { }
        }

        public void DDL_Bind()
        {
            OracleConnection conn = DB.GetConn();
            string Tbny = _getParam("Date");
            //  this.FT.Visible = true;
            try
            {
                //OracleConnection conn = DB.GetConn();
                String sqlZYQ = "select dep_name, dep_id from department where dep_type='ZYQ' and parent_id='" + Session["cyc_id"].ToString() + "' order by dep_id";
                OracleDataAdapter mydata = new OracleDataAdapter(sqlZYQ, conn);
                DataSet myds = new DataSet();
                conn.Open();
                mydata.Fill(myds, "Table_dep_name");

                DropDownListZYQ.DataSource = myds;
                DropDownListZYQ.DataValueField = "dep_name";
                DropDownListZYQ.DataBind();

                DropDownListJH.Items.Clear();
                String sqlJH = "SELECT jh from djsj where ny='" + Tbny + "' and cyc_id='" + Session["cyc_id"].ToString() + "'";
                sqlJH += " and zyq in (select dep_id from department where dep_name='" + DropDownListZYQ.SelectedValue + "')";
                OracleDataAdapter mydata2 = new OracleDataAdapter(sqlJH, conn);
                DataSet myJH = new DataSet();
                mydata2.Fill(myJH, "Table_jh");

                DropDownListJH.DataSource = myJH;
                DropDownListJH.DataValueField = "jh";
                DropDownListJH.DataBind();

                DropDownListFEE.Items.Clear();
                String sqlFEE = "SELECT fee_name from fee_name where fee_code in";
                sqlFEE += " (select fee_code from djfy where ny='" + Tbny + "' and cyc_id='" + Session["cyc_id"].ToString() + "' and dj_id in (select dj_id from djsj where jh='" + DropDownListJH.SelectedValue + "'))";
                OracleDataAdapter mydata3 = new OracleDataAdapter(sqlFEE, conn);
                DataSet myFEE = new DataSet();
                mydata3.Fill(myFEE, "Table_fee");

                DropDownListFEE.DataSource = myFEE;
                DropDownListFEE.DataValueField = "fee_name";
                DropDownListFEE.DataBind();
            }
            catch
            { }
            conn.Close();
        }


        //protected void zyqddl_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string Tbny = _getParam("Date");
        //    // listzxz.Items.Clear();
        //    OracleConnection conn = DB.GetConn();
        //    this.FT.Visible = true;
        //    // 出现对应的中心站
        //    String depzyq = "select dep_id from department where dep_name='" + zyqddl.SelectedValue + "' and cyc_id='" + Session["cyc_id"].ToString() + "' ";
        //    String depzxz = "select dep_name from view_alldep where ny='" + Tbny + "' and dep_type='ZXZ' and  parent_id in(" + depzyq + ") and cyc_id='" + Session["cyc_id"].ToString() + "' ";
        //    String depzrz = "select dep_name from view_alldep where ny='" + Tbny + "' and dep_type='" + ft_type + "' and  parent_id in(" + depzxz + ") and cyc_id='" + Session["cyc_id"].ToString() + "' ";

        //    OracleDataAdapter mydata2 = new OracleDataAdapter(depzrz, conn);
        //    DataSet mydep = new DataSet();
        //    conn.Open();
        //    mydata2.Fill(mydep, "dep_name2");
        //    this.zrzchk.DataSource = mydep.Tables["dep_name2"];
        //    zrzchk.DataTextField = "dep_name";
        //    zrzchk.DataBind();
        //    conn.Close();

        //}
        protected void FT_Click(object sender, EventArgs e)
        
      {
          OracleConnection conn = DB.GetConn();
          string Tbny = _getParam("Date");
          conn.Open();
        try
        {
            getFTJH();
            OracleCommand mycomm = new OracleCommand();
            mycomm.Connection = conn;
            mycomm.CommandType = CommandType.StoredProcedure;
            mycomm.CommandText = "OWCBSPKG_FEEFTDJ.UP_GGFYFT('" + Tbny + "','" + Session["cyc_id"].ToString() + "')";
            int reflectrows = Convert.ToInt32(mycomm.ExecuteNonQuery().ToString());
            if (reflectrows != 0)
            {
                Response.Write("<script>alert('分摊成功!')</script>");
            }

            //FpSpread1_Bind();
            //FpSpread1.Visible = true;
            //cblJH.Visible = false;
            //CheckBoxFTJ.Visible = false;
            //LabelFTJ.Text ="分摊结果：";
        }
        catch
        { 
        }
        conn.Close();
      }

        protected void bc_Click(object sender, EventArgs e)
        {
            string Tbny = _getParam("Date");
            OracleConnection conn = DB.GetConn();
            conn.Open();
            try
            {
                string dltFTJ = "delete from djfy where dj_id in (select dj_id from djsj where jh='" + DropDownListJH.Text + "') and fee_code in (select fee_code from fee_name where fee_name='" + DropDownListFEE.Text + "') and ny='" + Tbny + "' and cyc_id='" + Session["cyc_id"].ToString() + "'";
                OracleCommand CmdXM2 = new OracleCommand(dltFTJ, conn);
                CmdXM2.ExecuteNonQuery();

                string bc = "insert into djfy(select * from djfy_t where cyc_id='" + Session["cyc_id"].ToString() + "')";
                OracleCommand Cmd = new OracleCommand(bc, conn);
                int reflectrows = Convert.ToInt32(Cmd.ExecuteNonQuery().ToString());
                if (reflectrows != 0)
                {
                    Response.Write("<script>alert('保存成功!')</script>");
                }
            }
            catch
            {
            }
            //FpSpread1.Visible = true;
            //cblJH.Visible = false;
            //CheckBoxFTJ.Visible = false;
            //LabelFTJ.Text = "分摊结果：";

        }

        protected void DropDownListZYQ_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleConnection conn = DB.GetConn();
            string Tbny = _getParam("Date");
            DropDownListJH.Items.Clear();
            try
            {
                String sqlJH = "SELECT jh from djsj where ny='" + Tbny + "' and cyc_id='" + Session["cyc_id"].ToString() + "'";
                sqlJH += " and zyq in (select dep_id from department where dep_name='" + DropDownListZYQ.SelectedValue + "')";
                OracleDataAdapter mydata2 = new OracleDataAdapter(sqlJH, conn);
                DataSet myJH = new DataSet();
                conn.Open();
                mydata2.Fill(myJH, "Table_jh");

                DropDownListJH.DataSource = myJH;
                DropDownListJH.DataValueField = "jh";
                DropDownListJH.DataBind();
                conn.Close();
            }
            catch
            { }
        }

        protected void DropDownListJH_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CheckBoxFTJ.Text = DropDownListJH.Text;
            //CheckBoxFTJ.Checked = true;
            OracleConnection conn = DB.GetConn();
            string Tbny = _getParam("Date");
            conn.Open();
            DropDownListFEE.Items.Clear();
            try
            {
                String sqlFEE = "SELECT fee_name from fee_name where fee_code in";
                sqlFEE += " (select fee_code from djfy where ny='" + Tbny + "' and cyc_id='" + Session["cyc_id"].ToString() + "' and dj_id in (select dj_id from djsj where jh='" + DropDownListJH.Text + "'))";
                OracleDataAdapter mydata3 = new OracleDataAdapter(sqlFEE, conn);
                DataSet myFEE = new DataSet();
                mydata3.Fill(myFEE, "Table_feename");

                DropDownListFEE.DataSource = myFEE;
                DropDownListFEE.DataValueField = "fee_name";
                DropDownListFEE.DataBind();
                conn.Close();
            }
            catch
            { }
        }


        protected void getFTJH()
        {
            OracleConnection conn = DB.GetConn();
            string Tbny = _getParam("Date");
            conn.Open();
            try
            {
                string dltDJFTJ = "delete from djftj where cyc_id='" + Session["cyc_id"].ToString() + "'";
                OracleCommand CmdXM = new OracleCommand(dltDJFTJ, conn);
                CmdXM.ExecuteNonQuery();
                //foreach (ListItem J in cblJH.Items)
                //{
                //    if (J.Selected)
                //    {
                //        string jh_table = "insert into djftj(select djsj.ny,djsj.cyc_id,djsj.dj_id,djsj.jh,djsj.zyq,djsj.qk,djsj.zxz,djsj.zrz,djsj.yqlx,djsj.xsyp,djsj.jb,djsj.djdb,djsj.yclx,NVL((djsj.cyjmpgl * djsj.djgl * djsj.fzl * kfsj.scsj),0) as glcs,NVL(kfsj.zqlc,0) As zqlc,NVL(kfsj.zql,0) As zql,NVL(kfsj.zsl,0) As zsl,NVL(kfsj.cyl,0) As cyl,NVL(kfsj.csl,0) As csl,NVL(kfsj.cyaol,0) As cyaol,NVL(kfsj.scsj,0) As scsj,NVL(kfsj.jkcyl,0) As jkcyl,NVL(kfsj.jkcyoul,0) As jkcyoul,NVL(kfsj.jkcql,0) As jkcql,NVL(kfsj.hscyl,0) As hscyl,NVL(kfsj.hscql,0) As hscql,NVL(kfsj.nbhscyl,0) As nbhscyl,NVL(kfsj.nbhscql,0) As nbhscql,NVL(kfsj.yql,0) As yql from djsj,kfsj where djsj.ny = kfsj.ny and djsj.dj_id = kfsj.dj_id and djsj.cyc_id=kfsj.cyc_id and djsj.dj_id='" + J.Value + "' and djsj.ny='" + TextBoxNY.Text + "' and djsj.cyc_id='" + Session["cyc"].ToString() + "')";
                //        OracleCommand mycomm = new OracleCommand(jh_table, conn);
                //        OracleDataReader myReader;
                //        myReader = mycomm.ExecuteReader();
                //    }
                //}
                string jh_table1 = "insert into djftj(select djsj.ny,djsj.cyc_id,djsj.dj_id,djsj.jh,djsj.zyq,djsj.qk,djsj.zxz,djsj.zrz,djsj.yqlx,djsj.xsyp,djsj.jb,djsj.djdb,djsj.yclx,NVL((djsj.cyjmpgl * djsj.djgl * djsj.fzl * kfsj.scsj),0) as glcs,NVL(kfsj.zqlc,0) As zqlc,NVL(kfsj.zql,0) As zql,NVL(kfsj.zsl,0) As zsl,NVL(kfsj.cyl,0) As cyl,NVL(kfsj.csl,0) As csl,NVL(kfsj.cyaol,0) As cyaol,NVL(kfsj.scsj,0) As scsj,NVL(kfsj.jkcyl,0) As jkcyl,NVL(kfsj.jkcyoul,0) As jkcyoul,NVL(kfsj.jkcql,0) As jkcql,NVL(kfsj.hscyl,0) As hscyl,NVL(kfsj.hscql,0) As hscql,NVL(kfsj.nbhscyl,0) As nbhscyl,NVL(kfsj.nbhscql,0) As nbhscql,NVL(kfsj.yql,0) As yql from djsj,kfsj where djsj.ny = kfsj.ny and djsj.dj_id = kfsj.dj_id and djsj.cyc_id=kfsj.cyc_id and djsj.jh='" + DropDownListJH.Text + "' and djsj.ny='" + Tbny + "' and djsj.cyc_id='" + Session["cyc_id"].ToString() + "')";
                OracleCommand mycomm1 = new OracleCommand(jh_table1, conn);
                OracleDataReader myReader1;
                myReader1 = mycomm1.ExecuteReader();

                string dltDJGG = "delete from djfy_gg where cyc_id='" + Session["cyc_id"].ToString() + "'";
                OracleCommand CmdXM3 = new OracleCommand(dltDJGG, conn);
                CmdXM3.ExecuteNonQuery();

                string instDJGG = "insert into djfy_gg(select ny,dep_id,ft_type,source_id,fee_class,fee_code,fee,cyc_id from djfy where dj_id in (select dj_id from djsj where jh='" + DropDownListJH.Text + "') and fee_code in (select fee_code from fee_name where fee_name='" + DropDownListFEE.Text + "') and ny='" + Tbny + "' and cyc_id='" + Session["cyc_id"].ToString() + "')";
                OracleCommand CmdXM1 = new OracleCommand(instDJGG, conn);
                CmdXM1.ExecuteNonQuery();
            }
            catch
            { }
        }

        protected void DropDownListFEE_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleConnection conn = DB.GetConn();
            string Tbny = _getParam("Date");
            try
            {
                string fee = "select fee from djfy where ny= '" + Tbny + "' and dj_id in (select dj_id from djsj where jh='" + DropDownListJH.SelectedValue + "' and cyc_id = '" + Session["cyc_id"].ToString() + "') and fee_code in (select fee_code from fee_name where fee_name = '" + DropDownListFEE.Text + "') and cyc_id = '" + Session["cyc_id"].ToString() + "'";
                OracleDataAdapter mydatafee = new OracleDataAdapter(fee, conn);
                DataSet mfee = new DataSet();
                mydatafee.Fill(mfee, "Table_fee");
                LabelFEE.Text = mfee.Tables["Table_fee"].Rows[0][0].ToString() + "元";
            }
            catch
            { }
        }
        //private void xianshiresult()
        //{
        //    //    //this.listcyc.Visible = true;
        //    ////    this.FpSpread1.Visible = true;
        //    //   // this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

        //}
        //protected void QX_Click(object sender, EventArgs e)
        //{
        //    foreach (ListItem l in bscpchk.Items)
        //    {
        //        l.Selected = true;
        //    }
        //}
      
    }

}
