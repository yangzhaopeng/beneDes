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
using System.Collections.Generic;
using System.Data.OleDb;

namespace beneDesCYC.view.month
{
    public partial class cycfy1 : System.Web.UI.Page
    {
        OracleConnection Con = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);
        OracleCommand Cmd = new OracleCommand();
        string id, type;

        protected void Page_Load(object sender, EventArgs e)
        {

            //if (Session["user"] == null)
            //{
            //    Response.Redirect("../../web/chaoshi.htm");
            //}
            txtNY.Text = Session["month"].ToString();
            txtID.Text = "";
            if (!IsPostBack)
                InitTree(this.TreeView1.Nodes, "$root");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            //if (Session["shenhe"].ToString() == "1")
            //{
            //    ImgButBc.Visible = false;
            //    ImgButDr.Visible = false;
            //    FileUpload1.Visible = false;
            //}
        }

        public void InitTree(TreeNodeCollection Nds, string parentId)
        {
            OracleConnection Con = DB.CreatConnection();
            Con.Open();
            string query = "select distinct * from view_alldep where ny='" + Session["month"].ToString() + "' and cyc_id='" + Session["cyc_id"].ToString() + "'";
            OracleDataAdapter myAdapter = new OracleDataAdapter(query, Con);
            DataSet data = new DataSet();
            myAdapter.Fill(data, "tree");

            Con.Close();


            TreeNode tNd;
            DataRow[] rows = data.Tables[0].Select("parent_id='" + parentId + "'");
            foreach (DataRow row in rows)
            {
                tNd = new TreeNode();
                tNd.ShowCheckBox = false;
                tNd.Value = row["dep_id"].ToString();
                tNd.Text = row["dep_name"].ToString();
                Nds.Add(tNd);
                //InitTree(tNd.ChildNodes, tNd.Value);
            }
        }
        public void DL_DataBind()
        {
            Con.Open();
            string Sql1, Sql2, Sql3;
            Sql1 = "select FEE_NAME, round(FEE,2) from VIEW_GGFY where FT_TYPE='" + type + "' and NY='" + Session["month"].ToString() + "' and SOURCE_ID='" + id + "' and CYC_ID='" + Session["cyc_id"].ToString() + "' order by fee_class ";
            Sql3 = "select DEP_NAME from VIEW_ALLDEP where NY='" + Session["month"].ToString() + "' and SOURCE_ID='" + id + "'";
            Sql2 = "select distinct FEE_NAME,view_feeinfo.FEE_CLASS from VIEW_FEEINFO left join FEE_TEMPLET_DETAIL";
            Sql2 += " on VIEW_FEEINFO.FEE_CODE=FEE_TEMPLET_DETAIL.FEE_CODE";
            Sql2 += " where FEE_TEMPLET_DETAIL.TEMPLET_CODE='cycfyggmb' and VIEW_FEEINFO.FEE_NAME not in";
            Sql2 += " (select distinct FEE_NAME from VIEW_GGFY where FT_TYPE='" + type + "' and NY='" + Session["month"].ToString() + "' and SOURCE_ID='" + id + "' and VIEW_GGFY.cyc_id='" + Session["cyc_id"].ToString() + "')";
            Sql2 += " order by FEE_CLASS";
            OracleDataAdapter da1 = new OracleDataAdapter(Sql1, Con);
            DataSet ds1 = new DataSet();
            OracleDataAdapter da2 = new OracleDataAdapter(Sql2, Con);
            DataSet ds2 = new DataSet();
            OracleDataAdapter da3 = new OracleDataAdapter(Sql3, Con);
            DataSet ds3 = new DataSet();
            da1.Fill(ds1, "kk");
            da2.Fill(ds2, "dd");
            da3.Fill(ds3, "nn");
            txtID.Text = ds3.Tables["nn"].Rows[0][0].ToString();
            //建一个datatable用于显示数据
            #region
            DataTable tt = new DataTable();
            tt.Columns.Add("FEE_NAME", typeof(System.String));
            tt.Columns.Add("FEE", typeof(System.Double));
            DataRow rr;
            int n = ds1.Tables["kk"].Rows.Count;
            int m = ds2.Tables["dd"].Rows.Count;
            if (n + m == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请先设置费用模板!');</script>");
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    rr = tt.NewRow();
                    rr[0] = ds1.Tables["kk"].Rows[i][0];
                    rr[1] = ds1.Tables["kk"].Rows[i][1];
                    tt.Rows.Add(rr);
                }
                for (int j = 0; j < m; j++)
                {
                    rr = tt.NewRow();
                    rr[0] = ds2.Tables["dd"].Rows[j][0];
                    rr[1] = "0";
                    tt.Rows.Add(rr);
                }
                DataList1.DataSource = tt;
                DataList1.DataBind();
            }
            #endregion
            Con.Close();
        }



        protected void ImgButBc_Click(object sender, ImageClickEventArgs e)
        {
            Con.Open();
            string Sql1, Sql2, Sql3;
            id = Labelid.Text;
            type = Labeltype.Text;
            Sql1 = "select FEE_NAME, round(FEE,2) from VIEW_GGFY where FT_TYPE='" + type + "' and NY='" + Session["month"].ToString() + "' and SOURCE_ID='" + id + "' and CYC_ID='" + Session["cyc_id"].ToString() + "'";
            Sql2 = "select distinct FEE_NAME from VIEW_FEEINFO left join FEE_TEMPLET_DETAIL";
            Sql2 += " on VIEW_FEEINFO.FEE_CODE=FEE_TEMPLET_DETAIL.FEE_CODE";
            Sql2 += " where FEE_TEMPLET_DETAIL.TEMPLET_CODE='cycfyggmb' and VIEW_FEEINFO.FEE_NAME not in";
            Sql2 += " (select distinct FEE_NAME from VIEW_GGFY where FT_TYPE='" + type + "' and NY='" + Session["month"].ToString() + "' and SOURCE_ID='" + id + "' and VIEW_GGFY.cyc_id='" + Session["cyc_id"].ToString() + "')";
            OracleDataAdapter da1 = new OracleDataAdapter(Sql1, Con);
            DataSet ds1 = new DataSet();
            OracleDataAdapter da2 = new OracleDataAdapter(Sql2, Con);
            DataSet ds2 = new DataSet();
            da1.Fill(ds1, "kk");
            da2.Fill(ds2, "dd");
            try
            {
                for (int p = 0; p < DataList1.Items.Count; p++)
                {
                    string Sql;
                    TextBox FEE1 = (TextBox)DataList1.Items[p].FindControl("txtFEE1");
                    Label FN1 = (Label)DataList1.Items[p].FindControl("labFN1");
                    for (int k = 0; k < ds1.Tables["kk"].Rows.Count; k++)
                    {
                        string fnk = ds1.Tables["kk"].Rows[k][0].ToString();
                        if (fnk == FN1.Text)
                        {
                            if (FEE1.Text == "0")
                            {
                                Sql = "delete ggfy where source_id='" + id + "' and ft_type='" + type + "' and ny='" + Session["month"].ToString() + "' and fee_code in (select fee_code from fee_name where fee_name='" + FN1.Text + "') and cyc_id='" + Session["cyc_id"].ToString() + "'";
                                Cmd = new OracleCommand(Sql, Con);
                            }

                            else
                            {
                                Sql = "update GGFY set fee='" + FEE1.Text + "'";
                                Sql += " where source_id='" + id + "' and ft_type='" + type + "' and ny='" + Session["month"].ToString() + "' and fee_code in (select fee_code from fee_name where fee_name='" + FN1.Text + "') and cyc_id='" + Session["cyc_id"].ToString() + "'";
                                Cmd = new OracleCommand(Sql, Con);
                            }

                            try
                            {
                                int reflectrows = Convert.ToInt32(Cmd.ExecuteNonQuery().ToString());
                                if (reflectrows != 0)
                                {
                                    labShow.Text = "数据保存成功！";
                                }

                                else
                                {
                                    labShow.Text = "数据未保存成功，请重新输入！";
                                }

                            }
                            catch (Exception ex)
                            {
                                labShow.Text = "数据无效，请重新输入！";
                            }
                        }
                    }

                    for (int l = 0; l < ds2.Tables["dd"].Rows.Count; l++)
                    {
                        string fnl = ds2.Tables["dd"].Rows[l][0].ToString();
                        if (fnl == FN1.Text && FEE1.Text != "0")
                        {
                            string ZUOYE = "select dep_id from view_alldep where source_id='" + id + "'";
                            string FCL = "select fee_class from fee_name where fee_name='" + FN1.Text + "'";
                            string FCD = "select fee_code from fee_name where fee_name='" + FN1.Text + "'";

                            OracleDataAdapter da5 = new OracleDataAdapter(ZUOYE, Con);
                            DataSet ds5 = new DataSet();
                            da5.Fill(ds5, "tb5");
                            string DPID = ds5.Tables["tb5"].Rows[0][0].ToString();

                            OracleDataAdapter da6 = new OracleDataAdapter(FCL, Con);
                            DataSet ds6 = new DataSet();
                            da6.Fill(ds6, "tb6");
                            string FL = ds6.Tables["tb6"].Rows[0][0].ToString();

                            OracleDataAdapter da7 = new OracleDataAdapter(FCD, Con);
                            DataSet ds7 = new DataSet();
                            da7.Fill(ds7, "tb7");
                            string FD = ds7.Tables["tb7"].Rows[0][0].ToString();

                            Sql = "insert into ggfy(ny,dep_id,ft_type,source_id,fee_class,fee_code,fee,cyc_id)";
                            Sql += " values('" + Session["month"].ToString() + "','" + DPID + "','" + type + "','" + id + "','" + FL + "','" + FD + "','" + FEE1.Text + "','" + Session["cyc_id"].ToString() + "')";
                            Cmd = new OracleCommand(Sql, Con);
                            try
                            {
                                int reflectrows = Convert.ToInt32(Cmd.ExecuteNonQuery().ToString());
                                if (reflectrows != 0)
                                {
                                    labShow.Text = "数据保存成功！";
                                }

                                else
                                {
                                    labShow.Text = "数据未保存成功，请重新输入！";
                                }

                            }
                            catch (Exception ex)
                            {
                                labShow.Text = "数据无效，请重新输入！";
                            }

                        }
                    }
                }
            }

            catch (OracleException Error)
            {
                string CuoWu = "错误!" + Error.Message.ToString();
                Response.Write(CuoWu);
            }
            finally
            {
                Con.Close();
                DL_DataBind();

            }
        }

        public void LoadData(string StyleSheet, string address)
        {
            //打开指定摸版

            string address1 = Server.MapPath("./excel") + "\\cycfeiyongmoban.xls";



            string strCon1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" + address1 + " ;Extended Properties=Excel 8.0";
            OleDbConnection myConn1 = new OleDbConnection(strCon1);
            myConn1.Open();   //打开数据链接，得到一个数据集     
            DataSet myDataSet1 = new DataSet();   //创建DataSet对象     
            string StrSql1 = "select   *   from   [" + StyleSheet + "$]";
            OleDbDataAdapter myCommand1 = new OleDbDataAdapter(StrSql1, myConn1);
            myCommand1.Fill(myDataSet1, "[" + StyleSheet + "$]");
            myCommand1.Dispose();
            DataTable DT1 = myDataSet1.Tables["[" + StyleSheet + "$]"];
            myConn1.Close();
            myCommand1.Dispose();

            //this.GridView2.DataSource = DT1;
            //GridView2.DataBind();
            //this.Label1.Text = address;




            string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" + address + ";Extended Properties=Excel 8.0";
            OleDbConnection myConn = new OleDbConnection(strCon);
            myConn.Open();   //打开数据链接，得到一个数据集     
            DataSet myDataSet = new DataSet();   //创建DataSet对象     
            string StrSql = "select   *   from   [" + StyleSheet + "$]";
            OleDbDataAdapter myCommand = new OleDbDataAdapter(StrSql, myConn);
            myCommand.Fill(myDataSet, "[" + StyleSheet + "$]");
            myCommand.Dispose();
            DataTable DT = myDataSet.Tables["[" + StyleSheet + "$]"];
            myConn.Close();
            myCommand.Dispose();





            //string strConn = "Data Source=orcl; User Id=jilin_xm; Password=jilin_xm";
            //OracleConnection conn = new OracleConnection(strConn);
            OracleConnection conn = DB.CreatConnection();
            conn.Open();
            string cyc = Session["cyc_id"].ToString();
            //导入重复数据时候,先将原来数据删除
            for (int l = 2; l < DT.Columns.Count; l++)
            {
                // l代表费用开始的列
                for (int n = 0; n < DT.Rows.Count; n++)
                {
                    //n代表费用开始的行
                    string source_id1 = DT.Rows[n][1].ToString();
                    string deletesql = "delete from ggfy where ny='" + DT.Rows[n][0] + "' and dep_id='" + source_id1 + "' and source_id='" + source_id1 + "' and fee_class='" + DT1.Rows[0][l] + "' and fee_code='" + DT1.Rows[1][l] + "' and cyc_id='" + cyc + "'";

                    OracleCommand cmd1 = new OracleCommand(deletesql, conn);
                    cmd1.ExecuteNonQuery();
                }
            }
            string ft_type = "CYC";
            double zqlc = 0;
            //查找source_id,dj_id
            for (int j = 2; j < DT.Columns.Count; j++)
            {
                //j表示费用开始的列
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    //i表示费用开始的行
                    if ((DT.Rows[i][j]).GetType() == typeof(Double))
                    {
                        if (Convert.ToDouble(DT.Rows[i][j]) != 0)
                        {
                            string source_id = DT.Rows[i][1].ToString();



                            //string sql = "insert into testfee values('" + DT.Rows[i][0] + "','" + DT1.Rows[1][j] + "','" + DT.Rows[i][j] + "','" + DT1.Rows[0][j] + "','" + DT.Rows[i][1] + "','" + DT.Rows[i][2] + "','" + ft_type + "','" + zqlc + "','" + source_id + "')";
                            string sql = "insert into ggfy values('" + DT.Rows[i][0] + "','" + DT.Rows[i][1] + "','" + ft_type + "','" + DT.Rows[i][1] + "','" + DT1.Rows[0][j] + "','" + DT1.Rows[1][j] + "','" + DT.Rows[i][j] + "','" + cyc + "')";
                            OracleCommand comm = new OracleCommand(sql, conn);
                            comm.ExecuteNonQuery();
                        }


                    }

                }
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('导入成功!');</script>");
            conn.Close();



        }



        protected void ImgButDr_Click(object sender, ImageClickEventArgs e)
        {
            if (FileUpload1.FileName == "")
            {
                Response.Write("<script>alert('请选择要导入的数据！')</script>");
            }
            else
            {
                string a = FileUpload1.FileName;
                string a1 = a.Substring(a.LastIndexOf("."), a.Length - a.LastIndexOf("."));
                if (a1 != ".xls")
                {

                    Response.Write("<script>alert('请选择excel文件！')</script>");
                }
                else
                {
                    //上传到服务器指定文件夹
                    string f_folder = Server.MapPath("./daoruexcel/");
                    string f_name = f_folder + FileUpload1.FileName;
                    FileUpload1.PostedFile.SaveAs(f_name);

                    //从服务器倒入数据
                    //string StyleSheet = "Sheet1";
                    string address = Server.MapPath("./daoruexcel/") + FileUpload1.FileName;
                    //labtest.Text = address;
                    //2009.4.29修改
                    GetSheet getsheetx = new GetSheet();
                    string StyleSheet = getsheetx.GetExcelSheetNames(address);
                    LoadData(StyleSheet, address);
                    //DL_DataBind();
                    //xianshi();
                }


            }
        }
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            string depid = TreeView1.SelectedNode.Value;
            string Sql66 = "select distinct source_id,dep_type from view_alldep where dep_id='" + depid + "' and cyc_id='" + Session["cyc_id"].ToString() + "' and ny='" + Session["month"].ToString() + "'";
            OracleConnection Con = DB.CreatConnection();
            Con.Open();
            OracleDataAdapter da66 = new OracleDataAdapter(Sql66, Con);
            DataSet ds66 = new DataSet();
            da66.Fill(ds66, "tb66");
            if (ds66.Tables["tb66"].Rows.Count == 0)
            {
                id = "";
                type = "";
            }
            else
            {
                id = ds66.Tables["tb66"].Rows[0][0].ToString();
                type = ds66.Tables["tb66"].Rows[0][1].ToString();
                Labelid.Text = id;
                Labeltype.Text = type;
                DL_DataBind();
                Con.Close();
            }
        }
    }

}
