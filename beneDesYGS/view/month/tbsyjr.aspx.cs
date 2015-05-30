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

namespace beneDesYGS.view.month
{
    public partial class tbsyjr : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            sj();
        }

        public void sj()
        {
            SqlHelper DB = new SqlHelper();
            string HLDT = "select distinct rbmhl_info.hl,dtb_info.dtb,xscs_info.jg from rbmhl_info,dtb_info,xscs_info";
            HLDT += " where rbmhl_info.ny='" + Session["month"].ToString() + "' and dtb_info.ny='" + Session["month"].ToString() + "' and xscs_info.ny='" + Session["month"].ToString() + "' and xscs_info.xsypdm='X003' and dtb_info.xsypdm='X003'";
            OracleConnection Con = DB.GetConn();
            Con.Open();
            OracleDataAdapter da1 = new OracleDataAdapter(HLDT, Con);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1, "hldt");
            if (ds1.Tables["hldt"].Rows.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请设置该年月汇率、吨桶比及原油价格!');</script>");
            }
            Con.Close();
            if (ds1.Tables["hldt"].Rows.Count > 0)
            {
                string SHL = ds1.Tables["hldt"].Rows[0][0].ToString();
                string SDTB = ds1.Tables["hldt"].Rows[0][1].ToString();
                string SJG = ds1.Tables["hldt"].Rows[0][2].ToString();
                double DHL = System.Convert.ToDouble(SHL);
                double DDTB = System.Convert.ToDouble(SDTB);
                double DJG = System.Convert.ToDouble(SJG);
                double YJ = (DJG / DHL) / DDTB;
                string Number = YJ.ToString("f2");

                string HLDT1 = "select YYJG1,YYJG2,ZSBL1,SSKC1,YYJG3,YYJG4,ZSBL2,SSKC2,YYJG5,YYJG6,ZSBL3,SSKC3,YYJG7,YYJG8,ZSBL4,SSKC4,YYJGH,ZSBL5,SSKC5 from TBSYJ_BZ ";
                OracleConnection Con1 = DB.GetConn();
                Con1.Open();
                OracleDataAdapter da11 = new OracleDataAdapter(HLDT1, Con1);
                OracleCommand ocm = new OracleCommand(HLDT1, Con1);
                OracleDataReader oreader = ocm.ExecuteReader();

                oreader.Read();

                string TB1 = "" + oreader.GetDouble(0);
                string TB2 = "" + oreader.GetDouble(1);
                string TB3 = "" + oreader.GetDouble(2);
                string TB4 = "" + oreader.GetDouble(3);
                string TB5 = "" + oreader.GetDouble(4);
                string TB6 = "" + oreader.GetDouble(5);
                string TB7 = "" + oreader.GetDouble(6);
                string TB8 = "" + oreader.GetDouble(7);
                string TB9 = "" + oreader.GetDouble(8);
                string TB10 = "" + oreader.GetDouble(9);
                string TB11 = "" + oreader.GetDouble(10);
                string TB12 = "" + oreader.GetDouble(11);
                string TB13 = "" + oreader.GetDouble(12);
                string TB14 = "" + oreader.GetDouble(13);
                string TB15 = "" + oreader.GetDouble(14);
                string TB16 = "" + oreader.GetDouble(15);
                string TB17 = "" + oreader.GetDouble(16);
                string TB18 = "" + oreader.GetDouble(17);
                string TB19 = "" + oreader.GetDouble(18);

                //string[] TB = { TB1, TB2, TB3, TB4, TB5, TB6, TB7, TB8, TB9, TB10, TB11, TB12, TB13, TB14, TB15, TB16, TB17, TB18, TB19, Number };

                //return TB;


                Session["TB1"] = TB1;
                Session["TB2"] = TB2;
                Session["TB3"] = TB3;
                Session["TB4"] = TB4;

                Session["TB5"] = TB5;

                Session["TB6"] = TB6;
                Session["TB7"] = TB7;
                Session["TB8"] = TB8;

                Session["TB9"] = TB9;

                Session["TB10"] = TB10;
                Session["TB11"] = TB11;
                Session["TB12"] = TB12;

                Session["TB13"] = TB13;

                Session["TB14"] = TB14;
                Session["TB15"] = TB15;
                Session["TB16"] = TB16;

                Session["TB17"] = TB17;

                Session["TB18"] = TB18;
                Session["TB19"] = TB19;
                Session["Num"] = Number;


            }


        }        

        }

    }
