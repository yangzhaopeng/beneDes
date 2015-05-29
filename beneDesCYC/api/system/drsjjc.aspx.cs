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
using System.Text;
using System.Data.OleDb;
using beneDesCYC.core;
using System.Data.OracleClient;
using System.IO;

namespace beneDesCYC.api.system
{
    public partial class drsjjc : beneDesCYC.core.UI.corePage
    {
        //string Dropdl = _getParam("Dropdl");
        //string Tbny = _getParam("month");
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string Dropdl = _getParam("Dropdl");
          //  string Tbny = _getParam("month");
            string Tbny = Session["month"].ToString();
            if (Dropdl == "null")
                initSpread2();
            else
                initSpread();
            //if (!IsPostBack)
            //{

            //    //Tbny.Text = Session["date"].ToString();
            //    string Tbny = _getParam("Date");
            //}
            //fileUpload();
        }
        protected void initSpread2()
        {
        }
        protected void initSpread()
        {
            fileUpload();
        }
        protected void fileUpload()
        {
            string Dropdl = _getParam("Dropdl");
            //string Tbny = _getParam("Date");
            string Tbny = Session["month"].ToString();
            HttpPostedFile postedFile = Request.Files["UPLOADFILE"];
            string fileName = postedFile.FileName;
            if (fileName == "")
            { _return(false, "上传失败，文件名为空！", "null"); }
            string tempPath = System.Configuration.ConfigurationManager.AppSettings["NewsFolderPath"];
            string savePath = Server.MapPath(tempPath);
            string sExtension = fileName.Substring(fileName.LastIndexOf('.'));
            if (!Directory.Exists(savePath))
            { Directory.CreateDirectory(savePath); }
            string sNewFileName = DateTime.Now.ToString("yyyymmdd") + Session["userName"].ToString();
            postedFile.SaveAs(savePath + @"/" + sNewFileName + sExtension);

            string address = savePath + @"/" + sNewFileName + sExtension;
            GetSheet getsheetx = new GetSheet();
            string StyleSheet = getsheetx.GetExcelSheetNames(address);
            //  LoadData(StyleSheet, address);
            if (Dropdl == "1")
            {
                LoadData1(StyleSheet, address);
            }
            else if (Dropdl == "2")
            {
                LoadData2(StyleSheet, address);
            }
            else if (Dropdl == "3")
            {
                LoadData3(StyleSheet, address);
            }

            if (postedFile != null)
            {
                TextBox1.Text = Session["err"].ToString();
                _return(true, "上传成功！", "null");
            }
            else
            { _return(false, "上传失败！", "null"); }
            //if (FileUpload1.FileName == "")
            //{
            //    Response.Write("<script>alert('请选择要导入的数据！')</script>");
            //}
            //else
            //{
            //    string a = FileUpload1.FileName;
            //    string a1 = a.Substring(a.LastIndexOf("."), a.Length - a.LastIndexOf("."));
            //    if (a1 != ".xls")
            //    {

            //        Response.Write("<script>alert('请选择excel文件！')</script>");
            //    }
            //    else
            //    {
            //        //上传到服务器指定文件夹
            //        string f_folder = Server.MapPath("./daoru/");
            //        string f_name = f_folder + FileUpload1.FileName;
            //        FileUpload1.PostedFile.SaveAs(f_name);

            //        //从服务器倒入数据
            //        //string StyleSheet = "Sheet1";//2009.4.29
            //        //string address =FileUpload1.FileName;
            //        string address = Server.MapPath("./daoru/") + FileUpload1.FileName;
            //        //labtest.Text = address;
            //        //2009.4.29修改
            //        GetSheet getsheetx = new GetSheet();
            //        string StyleSheet = getsheetx.GetExcelSheetNames(address);
            //        //int ddltext = Convert.ToInt32(this.DropDownList1.SelectedItem.Value); 
            //        int ddltext = int.Parse(DropDownList1.SelectedItem.Value);
            //        //string ddltext = DropDownList1.SelectedItem.Value.Trim();
            //        if (ddltext == 1)
            //        {
            //            LoadData1(StyleSheet, address);
            //        }
            //        else if (ddltext == 2)
            //        {
            //            LoadData2(StyleSheet, address);
            //        }
            //        else if (ddltext == 3)
            //        {
            //            LoadData3(StyleSheet, address);
            //        }

            //    }
            //}
        }
        public void LoadData1(string StyleSheet, string address)
        {
            string Dropdl = _getParam("Dropdl");
           // string Tbny = _getParam("Date");
            string Tbny = Session["month"].ToString();
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


            ArrayList a1 = new ArrayList();
            ArrayList a2 = new ArrayList();
            ArrayList a3 = new ArrayList();
            ArrayList a4 = new ArrayList();
            ArrayList a5 = new ArrayList();
            ArrayList a6 = new ArrayList();
            ArrayList a7 = new ArrayList();
            ArrayList a8 = new ArrayList();
            ArrayList a9 = new ArrayList();
            ArrayList a10 = new ArrayList();
            string err1 = "";
            string err2 = "";
            string err3 = "";
            string err4 = "";
            string err5 = "";
            string err6 = "";
            string err7 = "";
            string err8 = "";
            string err9 = "";
            string err10 = "";
            string err = "";



            int rowscount = DT.Columns.Count;
            if (rowscount == 18)
            {

                //string strConn = "Data Source=orcl; User Id=jilin_xm; Password=jilin_xm";
                //OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cycConnectionString"].ConnectionString);
                //OracleConnection conn = new OracleConnection(strConn);
                OracleConnection conn = DB.CreatConnection();
                string cyc = Session["cyc_id"].ToString().Trim();
                //string sql="select * from department where parent_id='"+cyc+"'and dep_id='"+cyc+"'";
                string sql = "select dep_id from department where parent_id='" + cyc + "'";
                OracleDataAdapter sda = new OracleDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                //int a=ds.Tables[0].Rows .Count;

                OracleConnection conn1 = DB.CreatConnection();
                //string sql1 = "select* from yqlx_info where yqlxdm='" + YQLX + "'";
                string sql1 = "Select yqlxdm from yqlx_info";
                OracleDataAdapter sda1 = new OracleDataAdapter(sql1, conn1);
                DataSet ds1 = new DataSet();
                sda1.Fill(ds1);
                //int b = ds1.Tables[0].Rows.Count;



                OracleConnection conn2 = DB.CreatConnection();
                //string sql1 = "select* from xsyp_info where yqlxdm='" + XSYP + "'";
                string sql2 = "select xsypdm from xsyp_info";
                OracleDataAdapter sda2 = new OracleDataAdapter(sql2, conn2);
                DataSet ds2 = new DataSet();
                sda2.Fill(ds2);
                //int c = ds2.Tables[0].Rows.Count;

                conn.Open();
                conn1.Open();
                conn2.Open();
                int countin = 0;
                //for (int j = 0; j < DT.Rows.Count; j++)
                //{
                //    if (DT.Rows[j][0].ToString().Trim() != "" && DT.Rows[j][1].ToString().Trim() != "" && DT.Rows[j][2].ToString().Trim() != "" && DT.Rows[j][3].ToString().Trim() != "")
                //    {
                //        countin = countin + 1;
                //    }
                //    else
                //    {
                //        j = j + 1;
                //        Response.Write("<script>alert('第:" + j + "行年月、井号、作业区、区块不能为空')</script>");
                //    }
                //}

                try
                {
                    ///////////////////////////////////////////////////
                    for (int j = 0; j < DT.Rows.Count; j++)
                    {
                        //string NY = DT.Rows[j][0].ToString().Trim();
                        //string JH = DT.Rows[j][1].ToString().Trim();
                        //string cyd = DT.Rows[j][2].ToString().Trim();
                        //string CYJMPGL = DT.Rows[j][7].ToString().Trim();
                        //string DJGL = DT.Rows[j][8].ToString().Trim();
                        //string FZL = DT.Rows[j][9].ToString().Trim();
                        //string YQLX = DT.Rows[j][10].ToString().Trim();
                        //string XSYP = DT.Rows[j][11].ToString().Trim();
                        //string TCRQ = DT.Rows[j][12].ToString().Trim();
                        //string JB = DT.Rows[j][13].ToString().Trim();
                        //2011.7.8 修改为新模板
                        string NY = DT.Rows[j][0].ToString().Trim();
                        string JH = DT.Rows[j][1].ToString().Trim();
                        string cyd = DT.Rows[j][7].ToString().Trim();
                        string CYJMPGL = DT.Rows[j][12].ToString().Trim();
                        string DJGL = DT.Rows[j][12].ToString().Trim();
                        string FZL = DT.Rows[j][14].ToString().Trim();
                        string YQLX = DT.Rows[j][16].ToString().Trim();
                        string XSYP = DT.Rows[j][17].ToString().Trim();
                        string TCRQ = DT.Rows[j][3].ToString().Trim();
                        string JB = DT.Rows[j][2].ToString().Trim();

                        bool cyj = IsNumberic(CYJMPGL);
                        bool djg = IsNumberic(DJGL);
                        bool fz = IsNumberic(FZL);
                        string tbny = Tbny;


                        if (!NY.Equals(tbny) || !NY.Length.Equals(6))
                        { int i = j; i = i + 2; a1.Add(i); }
                        if (JH == "")
                        { int i = j; i = i + 2; a2.Add(i); }
                        if (cyd == "" || ds.Tables[0].Select("dep_id='" + cyd + "'").Length == 0)
                        { int i = j; i = i + 2; a3.Add(i); }

                        if (!cyj)
                        { int i = j; i = i + 2; a4.Add(i); }

                        if (!djg)
                        { int i = j; i = i + 2; a5.Add(i); }
                        if (!fz)
                        { int i = j; i = i + 2; a6.Add(i); }

                        if (ds1.Tables[0].Select("yqlxdm='" + YQLX + "'").Length == 0)
                        { int i = j; i = i + 2; a7.Add(i); }
                        if (ds2.Tables[0].Select("xsypdm='" + XSYP + "'").Length == 0)
                        { int i = j; i = i + 2; a8.Add(i); }
                        if (!TCRQ.Length.Equals(6))
                        { int i = j; i = i + 2; a9.Add(i); }
                        if (JB.Trim() == "")
                        { int i = j; i = i + 2; a10.Add(i); }
                    }



                    // err1 = a1[0].ToString ();
                    for (int j = 0; j < a1.Count; j++)
                    {
                        int i = j;
                        err1 = err1 + "," + a1[i].ToString();
                    }
                    // err2 = a2[0].ToString ();
                    for (int j = 0; j < a2.Count; j++)
                    {
                        int i = j;
                        err2 = err2 + "," + a2[i].ToString();
                    }
                    //err3 = a3[0].ToString ();
                    for (int j = 0; j < a3.Count; j++)
                    {
                        int i = j;
                        err3 = err3 + "," + a3[i].ToString();
                    }
                    //err4 = a4[0].ToString ();
                    for (int j = 0; j < a4.Count; j++)
                    {
                        int i = j;
                        err4 = err4 + "," + a4[i].ToString();
                    }
                    //err5 = a5[0].ToString ();
                    for (int j = 0; j < a5.Count; j++)
                    {
                        int i = j;
                        err5 = err5 + "," + a5[i].ToString();
                    }
                    //err6 = a6[0].ToString();
                    for (int j = 0; j < a6.Count; j++)
                    {
                        int i = j;
                        err6 = err6 + "," + a6[i].ToString();
                    }
                    //err7 = a7[0].ToString();
                    for (int j = 0; j < a7.Count; j++)
                    {
                        int i = j;
                        err7 = err7 + "," + a7[i].ToString();
                    }
                    //err8 = a8[0].ToString();
                    for (int j = 0; j < a8.Count; j++)
                    {
                        int i = j;
                        err8 = err8 + "," + a8[i].ToString();
                    }
                    //err9 = a9[0].ToString();
                    for (int j = 0; j < a9.Count; j++)
                    {
                        int i = j;
                        err9 = err9 + "," + a9[i].ToString();
                    }
                    //err10 = a10[0].ToString();
                    for (int j = 0; j < a10.Count; j++)
                    {
                        int i = j;
                        err10 = err10 + "," + a10[i].ToString();
                    }
                    err = "年月列:(值不对或为空)" + "\n" + err1 + "行\n" + "JH列:(值不为空)" + "\n" + err2 + "行\n" + "作业区列：(值不对或为空)" + "\n" + err3 + "行\n" + "抽油机铭牌功率列：(值不对或为空)" + "\n" + err4 + "行\n" + "电机功率列：(值不对或为空)" + "\n" + err5 + "行\n" + "负载率列：(值不对或为空)" + "\n" + err6 + "行\n" + "油气类型列：(值不对或为空)" + "\n" + err7 + "行\n" + "销售油品列：(值不对或为空)" + "\n" + err8 + "行\n" + "投产日期列：(值不对或为空)" + "\n" + err9 + "行\n" + "井别列：(值不对或为空)" + "\n" + err10 + "行\n";

                    Session["err"] = err;
                    //  Session["a"] = a;

                   // Response.Redirect("errmessage.aspx");
                    TextBox1.Text = Session["err"].ToString();
                }
                catch (OracleException aa)
                {
                    Response.Write(aa.Message.ToString());
                }
                conn.Close();
                conn1.Close();
                conn2.Close();
            }
            else
            {
                //Response.Write("<script>alert('您导入的表非单井基础模板表!!')</script>");
                _return(false,"您导入的表非单井基础模板表","null");
            }


            //Response.Write("<script>alert('导入成功！您导入的数据条数为:" + countin + "')</script>");
        }

        public void LoadData2(string StyleSheet, string address)
        {
            string Dropdl = _getParam("Dropdl");
           // string Tbny = _getParam("Date");
            string Tbny = Session["month"].ToString();
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
            ArrayList a1 = new ArrayList();
            ArrayList a2 = new ArrayList();
            ArrayList a3 = new ArrayList();
            ArrayList a4 = new ArrayList();
            ArrayList a5 = new ArrayList();
            ArrayList a6 = new ArrayList();
            string err1 = "";
            string err2 = "";
            string err3 = "";
            string err4 = "";
            string err5 = "";
            string err6 = "";

            string err = "";

            int rowscount = DT.Columns.Count;
            Label1.Text = rowscount.ToString();
            if (rowscount == 25)
            {
                OracleConnection conn = DB.CreatConnection();
                string cyc = Session["cyc_id"].ToString().Trim();
                //string sql="select * from department where parent_id='"+cyc+"'and dep_id='"+cyc+"'";
                string sql = "select dep_id from department where parent_id='" + cyc + "'";
                OracleDataAdapter sda = new OracleDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                //
                OracleConnection conn1 = DB.CreatConnection();
                //string sql1 = "select* from yqlx_info where yqlxdm='" + YQLX + "'";
                string sql1 = "Select dzcsdm from dzcs_info";
                OracleDataAdapter sda1 = new OracleDataAdapter(sql1, conn1);
                DataSet ds1 = new DataSet();
                sda1.Fill(ds1);
                //int b = ds1.Tables[0].Rows.Count;



                OracleConnection conn2 = DB.CreatConnection();
                //string sql1 = "select* from xsyp_info where yqlxdm='" + XSYP + "'";
                string sql2 = "select gycsdm from gycs_info";
                OracleDataAdapter sda2 = new OracleDataAdapter(sql2, conn2);
                DataSet ds2 = new DataSet();
                sda2.Fill(ds2);
                //int c = ds2.Tables[0].Rows.Count;
                conn.Open();
                conn1.Open();
                conn2.Open();

                try
                {

                    for (int j = 0; j < DT.Rows.Count; j++)
                    {
                        string NY = DT.Rows[j][0].ToString().Trim();
                        string JH = DT.Rows[j][1].ToString().Trim();
                        string cyd = DT.Rows[j][2].ToString().Trim();
                        //string scsj = DT.Rows[j][11].ToString().Trim();
                        string dzcs = DT.Rows[j][22].ToString().Trim();
                        string gycs = DT.Rows[j][23].ToString().Trim();
                        string tbny = Tbny;

                        if (!NY.Equals(tbny) || !NY.Length.Equals(6))
                        { int i = j; i = i + 2; a1.Add(i); }
                        if (JH == "")
                        { int i = j; i = i + 2; a2.Add(i); }
                        if (cyd == "" || ds.Tables[0].Select("dep_id='" + cyd + "'").Length == 0)
                        { int i = j; i = i + 2; a3.Add(i); }
                        if (!dzcs.Equals("") && ds1.Tables[0].Select("dzcsdm='" + dzcs + "'").Length == 0)
                        { int i = j; i = i + 2; a5.Add(i); }
                        if (!gycs.Equals("") && ds2.Tables[0].Select("gycsdm='" + gycs + "'").Length == 0)
                        { int i = j; i = i + 2; a6.Add(i); }

                    }
                    for (int j = 0; j < a1.Count; j++)
                    {
                        int i = j;
                        err1 = err1 + "," + a1[i].ToString();
                    }
                    // err2 = a2[0].ToString ();
                    for (int j = 0; j < a2.Count; j++)
                    {
                        int i = j;
                        err2 = err2 + "," + a2[i].ToString();
                    }
                    //err3 = a3[0].ToString ();
                    for (int j = 0; j < a3.Count; j++)
                    {
                        int i = j;
                        err3 = err3 + "," + a3[i].ToString();
                    }
                    //err4 = a4[0].ToString ();
                    //for (int j = 0; j < a4.Count; j++)
                    //{
                    //    int i = j;
                    //    err4 = err4 + "," + a4[i].ToString();
                    //}
                    //err5 = a5[0].ToString ();
                    for (int j = 0; j < a5.Count; j++)
                    {
                        int i = j;
                        err5 = err5 + "," + a5[i].ToString();
                    }
                    //err6 = a6[0].ToString();
                    for (int j = 0; j < a6.Count; j++)
                    {
                        int i = j;
                        err6 = err6 + "," + a6[i].ToString();
                    }
                    err = "年月列:(值不对或为空)" + "\n" + err1 + "行\n" + "JH列:(值不对或为空)" + "\n" + err2 + "行\n" + "作业区列：(值不对或为空)" + "\n" + err3 + "行\n" + "地质措施列：(值不对或为空)" + "\n" + err5 + "行\n" + "工艺措施列：(值不对或为空)" + "\n" + err6 + "行\n" + "油气类型列：(值不对或为空)" + "行\n";

                    Session["err"] = err;
                    TextBox1.Text = Session["err"].ToString();
                }
                catch (OracleException aa)
                {
                    Response.Write(aa.Message.ToString());
                }
                conn.Close();
                conn1.Close();
                conn2.Close();

                //Response.Write("<script>alert('导入成功！您导入的数据条数为:" + countin + "')</script>");
            }
            else
            {
                Response.Write("<script>alert('您导入的表非单井开发数据模板表!!')</script>");
            }
        }
        public void LoadData3(string StyleSheet, string address)
        {
            string Dropdl = _getParam("Dropdl");
           // string Tbny = _getParam("Date");
            string Tbny = Session["month"].ToString();
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
            ArrayList a1 = new ArrayList();
            ArrayList a2 = new ArrayList();
            ArrayList a3 = new ArrayList();

            string err1 = "";
            string err2 = "";
            string err3 = "";


            string err = "";

            int rowscount = DT.Columns.Count;
            Label1.Text = rowscount.ToString();
            if (rowscount == 26)
            {
                OracleConnection conn = DB.CreatConnection();
                string cyc = Session["cyc_id"].ToString().Trim();
                //string sql="select * from department where parent_id='"+cyc+"'and dep_id='"+cyc+"'";
                string sql = "select dep_id from department where parent_id='" + cyc + "'";
                OracleDataAdapter sda = new OracleDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Open();
                try
                {

                    for (int j = 0; j < DT.Rows.Count; j++)
                    {
                        string NY = DT.Rows[j][0].ToString().Trim();
                        string JH = DT.Rows[j][1].ToString().Trim();
                        string cyd = DT.Rows[j][2].ToString().Trim();
                        string tbny = Tbny;

                        if (!NY.Equals(tbny) || !NY.Length.Equals(6))
                        { int i = j; i = i + 2; a1.Add(i); }
                        if (JH == "")
                        { int i = j; i = i + 2; a2.Add(i); }
                        if (cyd == "" || ds.Tables[0].Select("dep_id='" + cyd + "'").Length == 0)
                        { int i = j; i = i + 2; a3.Add(i); }
                    }



                    for (int j = 0; j < a1.Count; j++)
                    {
                        int i = j;
                        err1 = err1 + "," + a1[i].ToString();
                    }
                    // err2 = a2[0].ToString ();
                    for (int j = 0; j < a2.Count; j++)
                    {
                        int i = j;
                        err2 = err2 + "," + a2[i].ToString();
                    }
                    //err3 = a3[0].ToString ();
                    for (int j = 0; j < a3.Count; j++)
                    {
                        int i = j;
                        err3 = err3 + "," + a3[i].ToString();
                    }


                    err = "年月列:(值不对或为空)" + "\n" + err1 + "行\n" + "tH列:(值不对或为空)" + "\n" + err2 + "行\n" + "作业区列：(值不对或为空)" + "\n" + err3 + "行\n";

                    Session["err"] = err;
                    TextBox1.Text = Session["err"].ToString();

                }

                catch (OracleException aa)
                {
                    Response.Write(aa.Message.ToString());
                }
            }
            else
            {
                Response.Write("<script>alert('您导入的表非单井开发数据模板表!!')</script>");
            }
        }

        public bool IsNumberic(string oText)
        {
            try
            {

                Double var1 = Convert.ToDouble(oText);
                return true;


                //Double  var1 = Convert.ToDouble (oText);
                //float var2= Convert.

                return true;

            }
            catch
            {
                return false;
            }
        }

       
    }
}
