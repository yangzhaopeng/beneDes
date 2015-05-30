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

    public partial class bscpft : beneDesCYC.core.UI.corePage
    {
        String source_id;
        String ny;
        //  String ft_type = "QK";
        //OracleConnection conn = DB.CreatConnection();
        //SqlHelper conn = new SqlHelper();
        //OracleConnection conb = conn.GetConn();
        protected SqlHelper DB = new SqlHelper();

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                //  this.list1.Visible = false;
                //  Session["saveFlag"] = false;
                string Tbny = _getParam("Date");
                //  txtdate.Text = Session["date"].ToString();//么有第一次pageload，ispostback=false从来没有过

                // this.FpSpread1.Visible = false;
                //  Session["saveFlag"] = false;//设置标记让spread不能保存状态

                //  this.listcyc.Visible = false;
                Btn_Bind();
            }
        }

        public void Btn_Bind()
        {
            string Tbny = _getParam("Date");
            //  this.FT.Visible = true;
            OracleConnection conn = DB.GetConn();
            String sqlstr = "select bscpdm,bscpmc from bscplx_info where  order by bscpmc";
            OracleDataAdapter mydata = new OracleDataAdapter(sqlstr, conn);


            DataSet myds = new DataSet();
            conn.Open();

            mydata.Fill(myds, "dep_namessss");

            bscpchk.DataSource = myds;
            bscpchk.DataValueField = "bscpdm";
            bscpchk.DataTextField = "bscpmc";
            bscpchk.DataBind();
            conn.Close();
        }


        protected void FT_Click(object sender, EventArgs e)
        {
            //    Session["saveFlag"] = false;//这一次不save session
            string Tbny = _getParam("Date");
            if (Tbny == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请输入年月!');</script>");

            }
            else
            {

                //listcyc.Items.Clear();

                //判断选择的是不是为空
                int Countselect = 0;
                foreach (ListItem l in bscpchk.Items)
                {
                    if (l.Selected)
                        Countselect++;
                }
                if (Countselect == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请选择伴生产品!');</script>");

                    this.FT.Visible = true;
                }
                else
                {
                    //显示结果
                    xianshiresult();

                }

                //   this.zyqchklist.Visible = true;
                //对每一个source_id 进行分摊
                // ny = txtdate.Text;
                foreach (ListItem l in bscpchk.Items)
                {
                    if (l.Selected)
                    {
                        OracleConnection conn = DB.GetConn();
                        string cycid = Session["cyc_id"].ToString();
                        conn.Open();
                        source_id = l.Value;
                        //source_id = l.Text;
                        OracleCommand mycomm2 = new OracleCommand();

                        mycomm2.Connection = conn;
                        mycomm2.CommandType = CommandType.StoredProcedure;
                        mycomm2.CommandText = "owcbspkg_feeft.up_bscpft('" + Tbny + "','" + source_id + "','" + cycid + "')";
                        mycomm2.ExecuteNonQuery();



                        conn.Close();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('分摊成功!');</script>");
                    }
                }
            }
        }

        private void xianshiresult()
        {
            //    //this.listcyc.Visible = true;
            ////    this.FpSpread1.Visible = true;
            //   // this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

        }
        protected void QX_Click(object sender, EventArgs e)
        {
            foreach (ListItem l in bscpchk.Items)
            {
                l.Selected = true;
            }
        }
        //protected void FpSpread1_SaveOrLoadSheetState(object sender, FarPoint.Web.Spread.SheetViewStateEventArgs e)
        //{
        //    if (e.IsSave)//最后save
        //    {
        //        if (bool.Parse(Session["saveFlag"].ToString()) != true)//在分摊、选厂的时候，清空此session.能清空表格最好了！

        //        {
        //            Session.Remove("caiyouchangviewstate");
        //            Session["saveFlag"] = true;
        //            FpSpread1.Sheets[0].Visible = false;
        //        }
        //        else
        //        {
        //            Session["caiyouchangviewstate"] = e.SheetView.SaveViewState();
        //        }
        //    }
        //    else
        //    {
        //        e.SheetView.LoadViewState(Session["caiyouchangviewstate"]);//e.SheetView.SheetName
        //    }
        //    e.Handled = true;
        //}
    }

}
