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
using beneDesYGS.model.month;
using beneDesYGS.core;
using System.Data.OracleClient;

namespace beneDesYGS.api.month
{
    public partial class tbsyj_bz : beneDesYGS.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _type = _getParam("_type");
            
            switch (_type)
            {
                case "getAllList":
                    getAllList();
                    break;
                case "edit":
                    edit();
                    break;
                case "saveSJ":
                    saveSJ();
                    break;
                
            }
        }


        public void getAllList()
        {
            SqlHelper DB = new SqlHelper();
            string Sql;
            Sql = "SELECT distinct ny,nvl(tbsyj,0) as tbsyj FROM tbsyj_Info ";
            //where ny='" + Session["month"].ToString() + "'";
            OracleConnection Con = DB.GetConn();
            int rows = 0;
            try
            {
                Con.Open();
                OracleDataAdapter da = new OracleDataAdapter(Sql, Con);
                DataSet ds = new DataSet();
                da.Fill(ds, "tbsyj_info");

                rows = ds.Tables["tbsyj_info"].Rows.Count;
                if (rows == 0)
                {
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('不存在该年月记录!');</script>");
                    _return(false, "不存在该年月记录！", "null");
                }
            
                else
                {
                    DataTable dt = ds.Tables["tbsyj_info"];
                    _return(true, "", dt);
                }

            }
            catch (OracleException Error)
            {
                string CuoWu = "错误!" + Error.Message.ToString();
                Response.Write(CuoWu);
            }
            Con.Close();

        }


        
        protected void edit()
        {
            
            SqlHelper DB = new SqlHelper();

            
                string HLDT = "select distinct rbmhl_info.hl,dtb_info.dtb,xscs_info.jg from rbmhl_info,dtb_info,xscs_info";
                HLDT += " where rbmhl_info.ny='" + Session["month"].ToString() + "' and dtb_info.ny='" + Session["month"].ToString() + "' and xscs_info.ny='" + Session["month"].ToString() + "' and xscs_info.xsypdm='X003' and dtb_info.xsypdm='X003'";
                OracleConnection Con = DB.GetConn();
                Con.Open();
                OracleDataAdapter da1 = new OracleDataAdapter(HLDT, Con);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "hldt");
                string SHL = ds1.Tables["hldt"].Rows[0][0].ToString();
                string SDTB = ds1.Tables["hldt"].Rows[0][1].ToString();
                string SJG = ds1.Tables["hldt"].Rows[0][2].ToString();
                double DHL = System.Convert.ToDouble(SHL);
                double DDTB = System.Convert.ToDouble(SDTB);
                double DJG = System.Convert.ToDouble(SJG);
                double YJ = (DJG / DHL) / DDTB;
                double DSYJ = 0;

                string HLDT1 = "select YYJG1,YYJG2,ZSBL1,SSKC1,YYJG3,YYJG4,ZSBL2,SSKC2,YYJG5,YYJG6,ZSBL3,SSKC3,YYJG7,YYJG8,ZSBL4,SSKC4,YYJGH,ZSBL5,SSKC5 from TBSYJ_BZ ";
                OracleConnection Con1 = DB.GetConn();
                Con1.Open();
                OracleDataAdapter da11 = new OracleDataAdapter(HLDT1, Con1);
                DataSet ds11 = new DataSet();
                da11.Fill(ds11, "hldt1");
                //string S1 = ds11.Tables["hldt1"].Rows[0][0].ToString();
                //string S2 = ds11.Tables["hldt1"].Rows[0][1].ToString();
                string S3 = ds11.Tables["hldt1"].Rows[0][0].ToString();
                string S4 = ds11.Tables["hldt1"].Rows[0][1].ToString();
                string S5 = ds11.Tables["hldt1"].Rows[0][2].ToString();
                string S6 = ds11.Tables["hldt1"].Rows[0][3].ToString();
                string S7 = ds11.Tables["hldt1"].Rows[0][4].ToString();
                string S8 = ds11.Tables["hldt1"].Rows[0][5].ToString();
                string S9 = ds11.Tables["hldt1"].Rows[0][6].ToString();
                string S10 = ds11.Tables["hldt1"].Rows[0][7].ToString();
                string S11 = ds11.Tables["hldt1"].Rows[0][8].ToString();
                string S12 = ds11.Tables["hldt1"].Rows[0][9].ToString();
                string S13 = ds11.Tables["hldt1"].Rows[0][10].ToString();
                string S14 = ds11.Tables["hldt1"].Rows[0][11].ToString();
                string S15 = ds11.Tables["hldt1"].Rows[0][12].ToString();
                string S16 = ds11.Tables["hldt1"].Rows[0][13].ToString();
                string S17 = ds11.Tables["hldt1"].Rows[0][14].ToString();
                string S18 = ds11.Tables["hldt1"].Rows[0][15].ToString();
                string S19 = ds11.Tables["hldt1"].Rows[0][16].ToString();
                string S20 = ds11.Tables["hldt1"].Rows[0][17].ToString();
                string S21 = ds11.Tables["hldt1"].Rows[0][18].ToString();

                //double DS2 = System.Convert.ToDouble(S2);
                double DS3 = System.Convert.ToDouble(S3);
                double DS4 = System.Convert.ToDouble(S4);
                double DS5 = System.Convert.ToDouble(S5);
                double DS6 = System.Convert.ToDouble(S6);
                double DS7 = System.Convert.ToDouble(S7);
                double DS8 = System.Convert.ToDouble(S8);
                double DS9 = System.Convert.ToDouble(S9);
                double DS10 = System.Convert.ToDouble(S10);
                double DS11 = System.Convert.ToDouble(S11);
                double DS12 = System.Convert.ToDouble(S12);
                double DS13 = System.Convert.ToDouble(S13);
                double DS14 = System.Convert.ToDouble(S14);
                double DS15 = System.Convert.ToDouble(S15);
                double DS16 = System.Convert.ToDouble(S16);
                double DS17 = System.Convert.ToDouble(S17);
                double DS18 = System.Convert.ToDouble(S18);
                double DS19 = System.Convert.ToDouble(S19);
                double DS20 = System.Convert.ToDouble(S20);
                double DS21 = System.Convert.ToDouble(S21);



                if (DS3 < YJ && YJ <= DS4)
                {
                    DSYJ = ((YJ - DS3) * DS5 / 100 - DS6) * DHL * DDTB;
                }
                else if (DS7 < YJ && YJ <= DS8)
                {
                    DSYJ = ((YJ - DS3) * DS9 / 100 - DS10) * DHL * DDTB;
                }
                else if (DS11 < YJ && YJ <= DS12)
                {
                    DSYJ = ((YJ - DS3) * DS13 / 100 - DS14) * DHL * DDTB;
                }
                else if (DS15 < YJ && YJ <= DS16)
                {
                    DSYJ = ((YJ - DS3) * DS17 / 100 - DS18) * DHL * DDTB;
                }
                else if (DS19 < YJ)
                {
                    DSYJ = ((YJ - DS3) * DS20 / 100 - DS21) * DHL * DDTB;
                }
                string SSYJ = DSYJ.ToString("f2");

                Con1.Close();
                Con.Close();

                BCSJ(SSYJ);

                
                _return(true, "计算完毕！", "null");
               
            

            
        }


        protected void saveSJ()
        {

            try
            {
                string TB1 = _getParam("TB1");
                string TB2 = _getParam("TB2");
                string TB3 = _getParam("TB3");
                string TB4 = _getParam("TB4");
                string TB5 = _getParam("TB5");
                string TB6 = _getParam("TB6");
                string TB7 = _getParam("TB7");
                string TB8 = _getParam("TB8");
                string TB9 = _getParam("TB9");
                string TB10 = _getParam("TB10");
                string TB11 = _getParam("TB11");
                string TB12 = _getParam("TB12");
                string TB13 = _getParam("TB13");
                string TB14 = _getParam("TB14");
                string TB15 = _getParam("TB15");
                string TB16 = _getParam("TB16");
                string TB17 = _getParam("TB17");
                string TB18 = _getParam("TB18");
                string TB19 = _getParam("TB19");
               

                OracleConnection conn = new OracleConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ygsConnectionString"].ConnectionString;
                string sql = "select COUNT(*)  from ygs.TBSYJ_BZ ";
                conn.Open();
                OracleCommand cmd1 = new OracleCommand(sql, conn);

                OracleDataAdapter da1 = new OracleDataAdapter(sql, conn);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "sql");
                string SHL = ds1.Tables["sql"].Rows[0][0].ToString();
                int m = System.Convert.ToInt32(SHL);


                string sql1 = "";
                //string sql1;

                if (m == 0)
                {
                    sql1 = "insert into TBSYJ_BZ(YYJG1,YYJG2,ZSBL1,SSKC1,YYJG3,YYJG4,ZSBL2,SSKC2,YYJG5,YYJG6,ZSBL3,SSKC3,YYJG7,YYJG8,ZSBL4,SSKC4,YYJGH,ZSBL5,SSKC5) values('" + TB1 + "','" + TB2 + "','" + TB3 + "','" + TB4 + "','" + TB5 + "','" + TB6 + "','" + TB7 + "','" + TB8 + "','" + TB9
                    + "','" + TB10 + "','" + TB11 + "','" + TB12 + "','" + TB13 + "','" + TB14 + "','" + TB15 + "','" + TB16 + "','" + TB17 + "','" + TB18 + "','" + TB19 + "')";
                }
                else
                {
                    sql1 = "update TBSYJ_BZ set YYJG1='" + TB1 + "',YYJG2='" + TB2 + "',ZSBL1='" + TB3 + "',SSKC1='" + TB4 + "',YYJG3='" + TB5 + "',YYJG4='" + TB6 + "',ZSBL2='" + TB7 + "',SSKC2='" + TB8 + "',YYJG5='" + TB9 + "',YYJG6='" + TB10 + "',ZSBL3='" + TB11 + "',SSKC3='" + TB12 + "',YYJG7='" + TB13 + "',YYJG8='" + TB14 + "',ZSBL4='" + TB15 + "',SSKC4='" + TB16 + "',YYJGH='" + TB17 + "',ZSBL5='" + TB18 + "',SSKC5='" + TB19 + "'";
                }

                OracleCommand cmd = new OracleCommand(sql1, conn);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {

                    //Response.Write("<script type='text/javascript'>alert('数据保存成功！')</script>");
                    _return(true, "数据保存成功！", "null");

                }
                else
                {
                    //Response.Write("<script type='text/javascript'>alert('数据保存失败！')</script>");
                    _return(false, "数据保存失败！", "null");
                }

                conn.Close();

            }
            catch
            {
                //Response.Write("<script type='text/javascript'>alert('数据不能为空！')</script>");
                
            }
        }


        protected void BCSJ(string SSYJ)
        {
            SqlHelper DB = new SqlHelper();
            OracleConnection Conn = DB.GetConn();
            string NY = Session["month"].ToString();
            string SYJ = SSYJ;

            string SC = "delete from tbsyj_Info where ny='" + Session["month"].ToString() + "'";
            string BC1 = "insert into tbsyj_info(ny,tbsyj) values('" + NY + "','" + SYJ + "')";
            Conn.Open();
            OracleCommand CmdSC = new OracleCommand(SC, Conn);
            CmdSC.ExecuteNonQuery();
            OracleCommand CmdBC1 = new OracleCommand(BC1, Conn);
            OracleTransaction tran = Conn.BeginTransaction();
            CmdBC1.Transaction = tran;

            try
            {
                int reflectrows = Convert.ToInt32(CmdBC1.ExecuteNonQuery().ToString());
                if (reflectrows == 1)
                {
                    
                    tran.Commit();

                }
                else
                {
                    
                    _return(false, "数据保存失败！", "null");
                }

            }
            catch
            {
                tran.Rollback();
                _return(false, "数据无效！", "null");
            }
            Conn.Close();
                
        }


    }
}
