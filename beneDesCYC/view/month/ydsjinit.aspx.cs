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
    public partial class ydsjinit : beneDesCYC.core.UI.corePage
    {
        protected string opn1 = " up_init_djsj ";
        protected string opn2 = " up_init_pjdy ";
        protected string opn3 = " up_init_qksj ";
        protected string opn4 = " up_init_sc ";
        protected string opn5 = " up_init_yqspl ";
        protected string opn6 = " up_init_xscs ";
        protected string opn7 = " up_init_trqdly ";
        protected string opn8 = " up_init_dtb ";
        protected string opn9 = " up_init_rmbhl ";
        protected string opn10 = " up_init_qjktf ";
        //protected string opn11 = " up_init_alertfeeclass ";
        //protected string opn12 = " up_init_alertfee ";
        protected int se = 0; //se=0表示没有选择任何项
        protected int err = 0; //err = 0表示没有错误

        protected SqlHelper DB = new SqlHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            //int.TryParse(this.thisSE.Text, out se);
            if (!IsPostBack)
            {
                // 在此处放置用户代码以初始化页面
                string Tbny = _getParam("Date");

                //Panel1.Visible = false;
                Label1.Visible = false;
                Label2.Visible = false;
                Label3.Visible = false;
                Label4.Visible = false;
                Label5.Visible = false;
                Label6.Visible = false;
                Label7.Visible = false;
                Label8.Visible = false;
                Label9.Visible = false;
                //Label10.Visible = false;
                //Label11.Visible = false;
                //Label12.Visible = false;
                Label13.Visible = false;
                //Tbny.Text = "200809";            
                //Tbny = Session["date"].ToString();
            }
            if (CheckBox1.Checked == true) se = 1;
            if (CheckBox2.Checked == true) se = 1;
            if (CheckBox3.Checked == true) se = 1;
            if (CheckBox4.Checked == true) se = 1;
            if (CheckBox5.Checked == true) se = 1;
            //if (CheckBox6.Checked == true) se = 1;
            if (CheckBox7.Checked == true) se = 1;
            //if (CheckBox8.Checked == true) se = 1;
            //if (CheckBox9.Checked == true) se = 1;
            //if (CheckBox10.Checked == true) se = 1;
            //if (CheckBox11.Checked == true) se = 1;
            //if (CheckBox12.Checked == true) se = 1;
            //Label13.Text = se.ToString();


        }
        protected void CSH_Click(object sender, EventArgs e)
        {
            string Tbny = _getParam("Date");
            if (Tbny == "")
                Response.Write("<script>alert('年月不能为空!')</script>");
            else if (se == 0)
            {
                Response.Write("<script>alert('您没有选择任何步骤,请选择您要进行的操作!')</script>");
            }
            else
            {
                string cycid = Session["cyc_id"].ToString();
                string cqcid = Session["cqc_id"].ToString();
                //Panel1.Visible = true;
                Label13.Visible = true;
                Label13.Text = "正在处理中,请稍后...";

                SqlHelper conn = new SqlHelper();
                OracleConnection conb = conn.GetConn();
                //OracleConnection conb = DB.CreatConnection();
                conb.Open();
                //初始化参数定义维护

                //命令一,初始化月度单井基础信息
                OracleCommand comb1 = new OracleCommand();
                comb1.Connection = conb;
                comb1.CommandType = CommandType.StoredProcedure;
                comb1.CommandText = "owcbspkg_ydsj_manager.up_dynamicinit_ydsj('" + Tbny + "','" + opn1 + "','" + cycid + "')";
                //命令二,初始化月度评价单元信息
                OracleCommand comb2 = new OracleCommand();
                comb2.Connection = conb;
                comb2.CommandType = CommandType.StoredProcedure;
                comb2.CommandText = "owcbspkg_ydsj_manager.up_dynamicinit_ydsj('" + Tbny + "','" + opn2 + "','" + cycid + "')";

                //命令三,初始化月度区块信息
                OracleCommand comb3 = new OracleCommand();
                comb3.Connection = conb;
                comb3.CommandType = CommandType.StoredProcedure;
                comb3.CommandText = "owcbspkg_ydsj_manager.up_dynamicinit_ydsj('" + Tbny + "','" + opn3 + "','" + cycid + "')";
                //命令四,初始化月度输差信息
                OracleCommand comb4 = new OracleCommand();
                comb4.Connection = conb;
                comb4.CommandType = CommandType.StoredProcedure;
                comb4.CommandText = "owcbspkg_ydsj_manager.up_dynamicinit_ydsj('" + Tbny + "','" + opn4 + "','" + cycid + "')";
                //命令五,初始化月度商品量信息
                OracleCommand comb5 = new OracleCommand();
                comb5.Connection = conb;
                comb5.CommandType = CommandType.StoredProcedure;
                comb5.CommandText = "owcbspkg_ydsj_manager.up_dynamicinit_ydsj('" + Tbny + "','" + opn5 + "','" + cycid + "')";

                ////命令六,初始化月度销售参数
                //OracleCommand comb6 = new OracleCommand();
                //comb6.Connection = conb;
                //comb6.CommandType = CommandType.StoredProcedure;
                //comb6.CommandText = "owcbspkg_ydsj_manager.up_dynamicinit_ydsj('" + Tbny.Text + "','" + opn6 + "','" + cycid + "')";

                //命令七,初始化月度天然气当量油
                OracleCommand comb7 = new OracleCommand();
                comb7.Connection = conb;
                comb7.CommandType = CommandType.StoredProcedure;
                comb7.CommandText = "owcbspkg_ydsj_manager.up_dynamicinit_ydsj('" + Tbny + "','" + opn7 + "','" + cycid + "')";

                ////命令十一,初始化月度预警大类信息
                //OracleCommand comb11 = new OracleCommand();
                //comb11.Connection = conb;
                //comb11.CommandType = CommandType.StoredProcedure;
                //comb11.CommandText = "owcbspkg_ydsj_manager.up_dynamicinit_ydsj('" + Tbny + "','" + opn11 + "')";

                ////命令十二,初始化月度预警明细信息
                //OracleCommand comb12 = new OracleCommand();
                //comb12.Connection = conb;
                //comb12.CommandType = CommandType.StoredProcedure;
                //comb12.CommandText = "owcbspkg_ydsj_manager.up_dynamicinit_ydsj('" + Tbny + "','" + opn12 + "')";



                try
                {

                    //执行命令一,初始化月度单井基础信息
                    if (CheckBox1.Checked == true)
                    {

                        Label1.Visible = true;
                        try
                        {
                            comb1.ExecuteNonQuery();
                            Label1.Text = "初始化月度单井基础信息成功!";
                        }
                        catch (OracleException Error)
                        {
                            Label1.Text = "错误: " + Error.Message.ToString();
                            err = 1;
                        }
                    }

                    //执行命令二,初始化月度评价单元信息
                    if (CheckBox2.Checked == true)
                    {

                        Label2.Visible = true;
                        try
                        {
                            comb2.ExecuteNonQuery();
                            Label2.Text = "初始化月度评价单元信息成功!";
                        }
                        catch (OracleException Error)
                        {
                            Label2.Text = "错误: " + Error.Message.ToString();
                            err = 1;
                        }
                    }

                    //执行命令三,初始化月度区块信息
                    if (CheckBox3.Checked == true)
                    {

                        Label3.Visible = true;
                        try
                        {
                            comb3.ExecuteNonQuery();
                            Label3.Text = "初始化月度区块信息成功!";
                        }
                        catch (OracleException Error)
                        {
                            Label3.Text = "错误: " + Error.Message.ToString();
                            err = 1;
                        }
                    }

                    //执行命令四,初始化月度输差信息
                    if (CheckBox4.Checked == true)
                    {

                        Label4.Visible = true;
                        try
                        {
                            comb4.ExecuteNonQuery();
                            Label4.Text = "初始化月度输差信息成功!";
                        }
                        catch (OracleException Error)
                        {
                            Label4.Text = "错误: " + Error.Message.ToString();
                            err = 1;
                        }
                    }

                    //执行命令五,初始化月度商品量信息
                    if (CheckBox5.Checked == true)
                    {

                        Label5.Visible = true;
                        try
                        {
                            comb5.ExecuteNonQuery();
                            Label5.Text = "初始化月度商品率信息成功!";
                        }
                        catch (OracleException Error)
                        {
                            Label5.Text = "错误: " + Error.Message.ToString();
                            err = 1;
                        }
                    }

                    //执行命令六,初始化月度销售参数
                    //if (CheckBox6.Checked == true)
                    //{

                    //    Label6.Visible = true;
                    //    try
                    //    {
                    //        comb6.ExecuteNonQuery();
                    //        Label6.Text = "初始化月度销售参数成功!";
                    //    }
                    //    catch (OracleException Error)
                    //    {
                    //        Label6.Text = "错误: " + Error.Message.ToString();
                    //        err = 1;
                    //    }
                    //}

                    //执行命令七,初始化月度天然气当量油
                    if (CheckBox7.Checked == true)
                    {

                        Label7.Visible = true;
                        try
                        {
                            comb7.ExecuteNonQuery();
                            Label7.Text = "初始化月度天然气当量油成功!";
                        }
                        catch (OracleException Error)
                        {
                            Label7.Text = "错误: " + Error.Message.ToString();
                            err = 1;
                        }
                    }

                    //执行命令八,初始化月度吨桶比信息
                    //if (CheckBox8.Checked == true)
                    //{

                    //    Label8.Visible = true;
                    //    try
                    //    {
                    //        comb8.ExecuteNonQuery();
                    //        Label8.Text = "初始化月度吨桶比信息成功!";
                    //    }
                    //    catch (OracleException Error)
                    //    {
                    //        Label8.Text = "错误: " + Error.Message.ToString();
                    //        err = 1;
                    //    }
                    //}

                    ////执行命令九,初始化月度人民币汇率
                    //if (CheckBox9.Checked == true)
                    //{

                    //    Label9.Visible = true;
                    //    try
                    //    {
                    //        comb9.ExecuteNonQuery();
                    //        Label9.Text = "初始化月度人民币汇率成功!";
                    //    }
                    //    catch (OracleException Error)
                    //    {
                    //        Label9.Text = "错误: " + Error.Message.ToString();
                    //        err = 1;
                    //    }
                    //}

                    ////执行命令十,初始化月度期间勘探费信息
                    //if (CheckBox10.Checked == true)
                    //{

                    //    Label10.Visible = true;
                    //    try
                    //    {
                    //        comb10.ExecuteNonQuery();
                    //        Label10.Text = "初始化月度期间勘探费信息成功!";
                    //    }
                    //    catch (OracleException Error)
                    //    {
                    //        Label10.Text = "错误: " + Error.Message.ToString();
                    //        err = 1;
                    //    }
                    //}

                    ////执行命令十一,初始化月度预警大类信息
                    //if (CheckBox11.Checked == true)
                    //{

                    //    Label11.Visible = true;
                    //    try
                    //    {
                    //        comb11.ExecuteNonQuery();
                    //        Label11.Text = "初始化月度预警大类信息成功!";
                    //    }
                    //    catch (OracleException Error)
                    //    {
                    //        Label11.Text = "错误: " + Error.Message.ToString();
                    //        err = 1;
                    //    }
                    //}

                    ////执行命令十二,初始化月度预警明细信息
                    //if (CheckBox12.Checked == true)
                    //{

                    //    Label12.Visible = true;
                    //    try
                    //    {
                    //        comb12.ExecuteNonQuery();
                    //        Label12.Text = "初始化月度预警明细信息成功!";
                    //    }
                    //    catch (OracleException Error)
                    //    {
                    //        Label12.Text = "错误: " + Error.Message.ToString();
                    //        err = 1;
                    //    }
                    //}


                    if (err == 0)
                    {
                        Label13.Text = "正在处理中,请稍后...  全部成功!";

                    }
                    else
                    {
                        Label13.Text = "正在处理中,请稍后...  有错误!";
                        Response.Write("<script>alert('计算有错误!'); </script>");
                    }
                }
                catch (OracleException Error)
                {
                    Label13.Text = Error.Message.ToString() + "错误!";
                }
                conb.Close();
            }

        }
        protected void QX_Click(object sender, EventArgs e)
        {


            CheckBox1.Checked = true;
            CheckBox2.Checked = true;
            CheckBox3.Checked = true;
            CheckBox4.Checked = true;
            CheckBox5.Checked = true;
            //CheckBox6.Checked = true;
            CheckBox7.Checked = true;
            //CheckBox8.Checked = true;
            //CheckBox9.Checked = true;
            //CheckBox10.Checked = true;
            //CheckBox11.Checked = true;
            //CheckBox12.Checked = true;
            se = 1;

        }

        protected void CX_Click(object sender, EventArgs e)
        {

            CheckBox1.Checked = false;
            CheckBox2.Checked = false;
            CheckBox3.Checked = false;
            CheckBox4.Checked = false;
            CheckBox5.Checked = false;
            //CheckBox6.Checked = false;
            CheckBox7.Checked = false;
            //CheckBox8.Checked = false;
            //CheckBox9.Checked = false;
            //CheckBox10.Checked = false;
            //CheckBox11.Checked = false;
            //CheckBox12.Checked = false;
            se = 0;

        }

        //同步更新采油厂数据
        //protected void UpdateDTB()
        //{
        //    string Tbny = _getParam("Date");

        //    SqlHelper conn = new SqlHelper();
        //    OracleConnection Con = conn.GetConn();
        //    //OracleConnection Con = DB.CreatConnection();
        //    Con.Open();

        //    string sql = "select * from dtb_info where ny='" + Tbny + "'";
        //    OracleDataAdapter da = new OracleDataAdapter(sql, Con);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds, "kk");
        //    DataTable tt = new DataTable();
        //    tt.Columns.Add("NY", typeof(System.String));
        //    tt.Columns.Add("XSYPDM", typeof(System.String));
        //    tt.Columns.Add("DTB", typeof(System.Double));
        //    tt.Columns.Add("CYC_ID", typeof(System.String));
        //    DataRow rr;
        //    int n = ds.Tables["kk"].Rows.Count;

        //    string cyc = "select distinct dep_id from department";
        //    OracleDataAdapter da1 = new OracleDataAdapter(cyc, Con);
        //    DataSet ds1 = new DataSet();
        //    da1.Fill(ds1, "cyc");
        //    int m = ds1.Tables["cyc"].Rows.Count;

        //    for (int s = 0; s < m; s++)
        //    {
        //        string cycid = ds1.Tables["cyc"].Rows[s][0].ToString();
        //        for (int i = 0; i < n; i++)
        //        {
        //            rr = tt.NewRow();
        //            rr[0] = ds.Tables["kk"].Rows[i][0];
        //            rr[1] = ds.Tables["kk"].Rows[i][1];
        //            rr[2] = ds.Tables["kk"].Rows[i][2];
        //            rr[3] = cycid;
        //            tt.Rows.Add(rr);
        //        }
        //    }
        //    for (int j = 0; j < tt.Rows.Count; j++)
        //    {
        //        try
        //        {
        //            string ny = tt.Rows[j][0].ToString();
        //            string xsypdm = tt.Rows[j][1].ToString();
        //            string dtb = tt.Rows[j][2].ToString();
        //            string cyc_id = tt.Rows[j][3].ToString();
        //            string update = "insert into jilincyc.dtb_info(ny,xsypdm,dtb,cyc_id) values('" + ny + "','" + xsypdm + "','" + dtb + "','" + cyc_id + "')";
        //            OracleCommand comm = new OracleCommand(update, Con);

        //            comm.ExecuteNonQuery();
        //        }
        //        catch
        //        { }
        //    }

        //}

        //protected void UpdateRMBHL()
        //{
        //    string Tbny = _getParam("Date");

        //    SqlHelper conn = new SqlHelper();
        //    OracleConnection Con = conn.GetConn();
        //    //OracleConnection Con = DB.CreatConnection();
        //    Con.Open();

        //    string sql = "select * from rbmhl_info where ny='" + Tbny + "'";
        //    OracleDataAdapter da = new OracleDataAdapter(sql, Con);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds, "kk");
        //    DataTable tt = new DataTable();
        //    tt.Columns.Add("NY", typeof(System.String));
        //    tt.Columns.Add("HL", typeof(System.Double));
        //    tt.Columns.Add("CYC_ID", typeof(System.String));
        //    DataRow rr;
        //    int n = ds.Tables["kk"].Rows.Count;

        //    string cyc = "select distinct dep_id from department";
        //    OracleDataAdapter da1 = new OracleDataAdapter(cyc, Con);
        //    DataSet ds1 = new DataSet();
        //    da1.Fill(ds1, "cyc");
        //    int m = ds1.Tables["cyc"].Rows.Count;

        //    for (int s = 0; s < m; s++)
        //    {
        //        string cycid = ds1.Tables["cyc"].Rows[s][0].ToString();
        //        for (int i = 0; i < n; i++)
        //        {
        //            rr = tt.NewRow();
        //            rr[0] = ds.Tables["kk"].Rows[i][0];
        //            rr[1] = ds.Tables["kk"].Rows[i][1];
        //            rr[2] = cycid;
        //            tt.Rows.Add(rr);
        //        }
        //    }
        //    for (int j = 0; j < tt.Rows.Count; j++)
        //    {
        //        try
        //        {
        //            string ny = tt.Rows[j][0].ToString();
        //            string hl = tt.Rows[j][1].ToString();
        //            string cyc_id = tt.Rows[j][2].ToString();
        //            string update = "insert into jilincyc.rbmhl_info(ny,hl,cyc_id) values('" + ny + "','" + hl + "','" + cyc_id + "')";
        //            OracleCommand comm = new OracleCommand(update, Con);

        //            comm.ExecuteNonQuery();
        //        }
        //        catch
        //        { }
        //    }

        //}

        //protected void UpdateQJKTFY()
        //{
        //    string Tbny = _getParam("Date");
        //    SqlHelper conn = new SqlHelper();
        //    OracleConnection Con = conn.GetConn();
        //    //OracleConnection Con = DB.CreatConnection();
        //    Con.Open();

        //    string sql = "select * from qjktfy_info where ny='" + Tbny + "'";
        //    OracleDataAdapter da = new OracleDataAdapter(sql, Con);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds, "kk");
        //    DataTable tt = new DataTable();
        //    tt.Columns.Add("NY", typeof(System.String));
        //    tt.Columns.Add("QJFY", typeof(System.Double));
        //    tt.Columns.Add("KTFY", typeof(System.Double));
        //    tt.Columns.Add("CYC_ID", typeof(System.String));
        //    DataRow rr;
        //    int n = ds.Tables["kk"].Rows.Count;

        //    string cyc = "select distinct dep_id from department";
        //    OracleDataAdapter da1 = new OracleDataAdapter(cyc, Con);
        //    DataSet ds1 = new DataSet();
        //    da1.Fill(ds1, "cyc");
        //    int m = ds1.Tables["cyc"].Rows.Count;

        //    for (int s = 0; s < m; s++)
        //    {
        //        string cycid = ds1.Tables["cyc"].Rows[s][0].ToString();
        //        for (int i = 0; i < n; i++)
        //        {
        //            rr = tt.NewRow();
        //            rr[0] = ds.Tables["kk"].Rows[i][0];
        //            rr[1] = ds.Tables["kk"].Rows[i][1];
        //            rr[2] = ds.Tables["kk"].Rows[i][2];
        //            rr[3] = cycid;
        //            tt.Rows.Add(rr);
        //        }
        //    }
        //    for (int j = 0; j < tt.Rows.Count; j++)
        //    {
        //        try
        //        {
        //            string ny = tt.Rows[j][0].ToString();
        //            string qjfy = tt.Rows[j][1].ToString();
        //            string ktfy = tt.Rows[j][2].ToString();
        //            string cyc_id = tt.Rows[j][3].ToString();
        //            string update = "insert into jilincyc.qjktfy_info(ny,qjfy,ktfy,cyc_id) values('" + ny + "','" + qjfy + "','" + ktfy + "','" + cyc_id + "')";
        //            OracleCommand comm = new OracleCommand(update, Con);

        //            comm.ExecuteNonQuery();
        //        }
        //        catch
        //        { }
        //    }

        //}
        //public void updateZYS()
        //{

        //    SqlHelper conn = new SqlHelper();
        //    OracleConnection Con = conn.GetConn();
        //    //OracleConnection Con = DB.CreatConnection();
        //    Con.Open();

        //    try
        //    {
        //        //临时表ss：（表ygs.zys_info与cyc_id的笛卡尔积）
        //        //update nvl函数第二个参数可以为b.zys
        //        //inserttable not in 后面括号可以直接改成 (select bb.id from ss bb,YGS.YQSPL_INFO cc where bb.ny=cc.ny and bb.yqlxdm=cc.yqlxdm and bb.cyc_id=cc.cyc_id)
        //        string isExistTable = "select count(*)  from all_tables where table_name='SS'and owner='YGS'";//判断临时表SS是否存在，存在则删除
        //        string dropTable = "drop table ss";
        //        string createTempTable = "create table ss as select * from ( select rownum as id,uuu.* from(select ny,yqlxdm,zys,dep_id as cyc_id from ygs.zys_info,(select distinct dep_id from ygs.department)temp)uuu)";
        //        string updateTable = "update ygs.YQSPL_INFO b set zys=nvl((select a.zys from ss a where a.ny=b.ny and a.yqlxdm=b.yqlxdm and a.cyc_id=b.cyc_id),(select b.zys from YGS.YQSPL_INFO c where c.ny=b.ny and c.yqlxdm=b.yqlxdm and c.cyc_id=b.cyc_id))";
        //        string insertTable = "insert into ygs.YQSPL_INFO (ny,yqlxdm,zys,cyc_id) select a.ny,a.yqlxdm,a.zys,a.cyc_id from ss a where a.id not in (select bb.id from ss bb,(select * from (select rownum as id,YGS.YQSPL_INFO.* from YGS.YQSPL_INFO)) cc where bb.ny=cc.ny and bb.yqlxdm=cc.yqlxdm and bb.cyc_id=cc.cyc_id)";
        //        //同步更新jilincyc的yqspl_info表
        //        string updateJinlincyc_yqspl_info = "merge into jilincyc.yqspl_info p using ygs.yqspl_info q on(p.ny=q.ny and p.yqlxdm=q.yqlxdm and p.cyc_id=q.cyc_id) when matched then update set p.zys=q.zys when not matched then insert values(q.ny,q.yqlxdm,q.yqspl,q.zys,q.cyc_id)";
        //        //先判断ss表是否存在，存在则删除
        //        OracleCommand commExist = new OracleCommand(isExistTable, Con);
        //        string isExist = commExist.ExecuteOracleScalar().ToString();
        //        if (isExist == "1")
        //        {
        //            OracleCommand commDrop = new OracleCommand(dropTable, Con);
        //            commDrop.ExecuteNonQuery();
        //        }
        //        OracleCommand comm1 = new OracleCommand(createTempTable, Con);
        //        // comm1.ExecuteReader();    
        //        comm1.ExecuteNonQuery();
        //        OracleCommand comm2 = new OracleCommand(updateTable, Con);
        //        comm2.ExecuteNonQuery();
        //        OracleCommand comm3 = new OracleCommand(insertTable, Con);
        //        comm3.ExecuteNonQuery();
        //        //OracleCommand comm4 = new OracleCommand(dropTempTable, Con);
        //        // comm4.ExecuteNonQuery();
        //        //以上为ygs的yqspl_info表更新。下面保持jilincyc的yqspl_info表同步更新
        //        OracleCommand comm4 = new OracleCommand(updateJinlincyc_yqspl_info, Con);
        //        int count = comm4.ExecuteNonQuery();

        //    }
        //    catch (Exception ex)
        //    {
        //        // Response.Write(ex.Message.ToString());

        //    }
        //}

        //protected void UpdateXSCS()
        //{
        //    string Tbny = _getParam("Date");

        //    SqlHelper conn = new SqlHelper();
        //    OracleConnection Con = conn.GetConn();
        //    //OracleConnection Con = DB.CreatConnection();
        //    Con.Open();

        //    string sql = "select * from xscs_info where ny='" + Tbny + "'";
        //    OracleDataAdapter da = new OracleDataAdapter(sql, Con);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds, "kk");
        //    DataTable tt = new DataTable();
        //    tt.Columns.Add("NY", typeof(System.String));
        //    tt.Columns.Add("XSYPDM", typeof(System.String));
        //    tt.Columns.Add("JG", typeof(System.Double));
        //    tt.Columns.Add("SJ", typeof(System.Double));
        //    tt.Columns.Add("CYC_ID", typeof(System.String));
        //    DataRow rr;
        //    int n = ds.Tables["kk"].Rows.Count;

        //    string cyc = "select distinct dep_id from department";
        //    OracleDataAdapter da1 = new OracleDataAdapter(cyc, Con);
        //    DataSet ds1 = new DataSet();
        //    da1.Fill(ds1, "cyc");
        //    int m = ds1.Tables["cyc"].Rows.Count;

        //    for (int s = 0; s < m; s++)
        //    {
        //        string cycid = ds1.Tables["cyc"].Rows[s][0].ToString();
        //        for (int i = 0; i < n; i++)
        //        {
        //            rr = tt.NewRow();
        //            rr[0] = ds.Tables["kk"].Rows[i][0];
        //            rr[1] = ds.Tables["kk"].Rows[i][1];
        //            rr[2] = ds.Tables["kk"].Rows[i][2];
        //            rr[3] = ds.Tables["kk"].Rows[i][3];
        //            rr[4] = cycid;
        //            tt.Rows.Add(rr);
        //        }
        //    }
        //    for (int j = 0; j < tt.Rows.Count; j++)
        //    {
        //        try
        //        {
        //            string ny = tt.Rows[j][0].ToString();
        //            string xsypdm = tt.Rows[j][1].ToString();
        //            string jg = tt.Rows[j][2].ToString();
        //            string sj = tt.Rows[j][3].ToString();
        //            string cyc_id = tt.Rows[j][4].ToString();
        //            string update = "insert into jilincyc.xscs_info(ny,xsypdm,jg,sj,cyc_id) values('" + ny + "','" + xsypdm + "','" + jg + "','" + sj + "','" + cyc_id + "')";
        //            OracleCommand comm = new OracleCommand(update, Con);

        //            comm.ExecuteNonQuery();
        //        }
        //        catch
        //        { }
        //    }

        //}

        //protected void UpdateTRQDLY()
        //{
        //    string Tbny = _getParam("Date");
        //    SqlHelper conn = new SqlHelper();
        //    OracleConnection Con = conn.GetConn();
        //    //OracleConnection Con = DB.CreatConnection();
        //    Con.Open();

        //    string sql = "select * from trqdly_info where ny='" + Tbny + "'";
        //    OracleDataAdapter da = new OracleDataAdapter(sql, Con);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds, "kk");
        //    DataTable tt = new DataTable();
        //    tt.Columns.Add("NY", typeof(System.String));
        //    tt.Columns.Add("DLY", typeof(System.Double));
        //    tt.Columns.Add("CYC_ID", typeof(System.String));
        //    DataRow rr;
        //    int n = ds.Tables["kk"].Rows.Count;

        //    string cyc = "select distinct dep_id from department";
        //    OracleDataAdapter da1 = new OracleDataAdapter(cyc, Con);
        //    DataSet ds1 = new DataSet();
        //    da1.Fill(ds1, "cyc");
        //    int m = ds1.Tables["cyc"].Rows.Count;

        //    for (int s = 0; s < m; s++)
        //    {
        //        string cycid = ds1.Tables["cyc"].Rows[s][0].ToString();
        //        for (int i = 0; i < n; i++)
        //        {
        //            rr = tt.NewRow();
        //            rr[0] = ds.Tables["kk"].Rows[i][0];
        //            rr[1] = ds.Tables["kk"].Rows[i][1];
        //            rr[2] = cycid;
        //            tt.Rows.Add(rr);
        //        }
        //    }
        //    for (int j = 0; j < tt.Rows.Count; j++)
        //    {
        //        try
        //        {
        //            string ny = tt.Rows[j][0].ToString();
        //            string dly = tt.Rows[j][1].ToString();
        //            string cyc_id = tt.Rows[j][2].ToString();
        //            string update = "insert into jilincyc.trqdly_info(ny,dly,cyc_id) values('" + ny + "','" + dly + "','" + cyc_id + "')";
        //            OracleCommand comm = new OracleCommand(update, Con);

        //            comm.ExecuteNonQuery();
        //        }
        //        catch
        //        { }
        //    }

        //}

        //protected void UpdateCSDY()
        //{
        //    string Tbny = _getParam("Date");
        //    SqlHelper conn = new SqlHelper();
        //    OracleConnection Con = conn.GetConn();
        //    //OracleConnection Con = DB.CreatConnection();
        //    Con.Open();

        //    try
        //    {
        //        string update = "insert into jilincyc.csdy_info(name,num,ny) (select name,num,ny from csdy_info where ny='" + Tbny + "')";
        //        OracleCommand comm = new OracleCommand(update, Con);

        //        comm.ExecuteNonQuery();
        //    }
        //    catch
        //    { }
        //}

        //protected void UpdateTBSYJ()
        //{
        //    string Tbny = _getParam("Date");
        //    SqlHelper conn = new SqlHelper();
        //    OracleConnection Con = conn.GetConn();
        //    //OracleConnection Con = DB.CreatConnection();
        //    Con.Open();

        //    try
        //    {
        //        string update = "insert into jilincyc.tbsyj_info(ny,tbsyj) (select ny,tbsyj from tbsyj_info where ny='" + Tbny + "')";
        //        OracleCommand comm = new OracleCommand(update, Con);

        //        comm.ExecuteNonQuery();
        //    }
        //    catch
        //    { }
        //}
        //protected void Btnall_Click(object sender, ImageClickEventArgs e)
        //{
        //    CheckBox6.Checked = true;
        //    CheckBox7.Checked = true;
        //    CheckBox8.Checked = true;
        //    CheckBox9.Checked = true;
        //    CheckBox10.Checked = true;
        //    CheckBox1.Checked = true;
        //    CheckBox2.Checked = true;
        //    CheckBox3.Checked = true;
        //    se = 1;
        //}
        //protected void Btncanc_Click(object sender, ImageClickEventArgs e)
        //{
        //    CheckBox6.Checked = false;
        //    CheckBox7.Checked = false;
        //    CheckBox8.Checked = false;
        //    CheckBox9.Checked = false;
        //    CheckBox10.Checked = false;
        //    CheckBox1.Checked = false;
        //    CheckBox2.Checked = false;
        //    CheckBox3.Checked = false;
        //    se = 0;
        //}
    }
}
