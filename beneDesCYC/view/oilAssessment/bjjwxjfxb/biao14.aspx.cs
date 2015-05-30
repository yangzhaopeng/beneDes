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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace beneDesCYC.view.oilAssessment.bjjwxjfxb
{
    public partial class biao14 : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Dropdl = _getParam("Dropdl");
            if (Dropdl == "null")
                initSpread2();
            else
                initSpread();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }

        protected void initSpread()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/dongtai.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表13-边际效益井效益类别动态跟踪表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

            //this.FpSpread1.Sheets[0].Cells[3, 1].Text = _getParam("targetType");
            //this.FpSpread1.Sheets[0].Cells[4, 1].Text = _getParam("startMonth") + " -- " + _getParam("endMonth");

            if (isempty() == 0)
            { Response.Write("<script>alert('无数据')</script>"); }
            else
            { sj(); }
        }

        protected void initSpread2()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/dongtai.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表13-边际效益井效益类别动态跟踪表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

        }

        protected void sj()
        {
            string Dropdl = _getParam("Dropdl");
            string cycid = Session["cyc_id"].ToString();
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //string list = _getParam("YCLX");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
           // string list = _getParam("CYC");
            string typeid = _getParam("targetType");

            SqlHelper conn = new SqlHelper();
            OracleConnection connfp = conn.GetConn();

            //OracleConnection connfp = DB.CreatConnection();

            string fpselect = "select sdy.jh,sdy.dj_id from dtstat_djsj_all sdy where sdy.gsxyjb_1 = '3' and sdy.cyc_id = '" + cycid + "' and sdy.eny ='" + bny + "'";
            if (Dropdl == "quan")
            {
                fpselect += "";
            }
            else
            {
                fpselect += " and sdy.qk = '" + Dropdl + "'";
            }

            connfp.Open();
            OracleCommand myComm = new OracleCommand(fpselect, connfp);
            OracleDataReader myReader = myComm.ExecuteReader();
            //DataTable Fptable用来输出数据
            DataTable Fptable = new DataTable("fpdataa");
            Fptable.Columns.Add("jh", typeof(string));
            Fptable.Columns.Add("dj_id", typeof(string));
            DataRow Fprow;
            while (myReader.Read())
            {
                Fprow = Fptable.NewRow();

                Fprow[0] = myReader[0];
                Fprow[1] = myReader[1];
                Fptable.Rows.Add(Fprow);
            }
            myReader.Close();
            myComm.Clone();

            int y = int.Parse(eny.Substring(4, 2));
            string ny;
            for (int j = 2; j <= y; j++)
            {
                if (j < 10)
                {
                    ny = eny.Substring(0, 4) + "0" + j.ToString();
                }
                else
                {
                    ny = eny.Substring(0, 4) + j.ToString();
                }

                string fpselectunion = "select b.jh,b.dj_id from ";
                fpselectunion += "(select sdy.jh,sdy.dj_id from dtstat_djsj_all sdy where sdy.gsxyjb_1 = '3' and sdy.cyc_id = '" + cycid + "' and sdy.eny ='" + ny + "'";
                if (Dropdl == "quan")
                {
                    fpselectunion += "";
                }
                else
                {
                    fpselectunion += " and sdy.qk = '" + Dropdl + "'";
                }
                fpselectunion += ")b ";
                fpselectunion += "where b.dj_id not in";
                fpselectunion += "(select sdy.dj_id from dtstat_djsj_all sdy where sdy.gsxyjb_1 = '3' and sdy.cyc_id = '" + cycid + "' and sdy.eny <'" + ny + "'";
                if (Dropdl == "quan")
                {
                    fpselectunion += "";
                }
                else
                {
                    fpselectunion += " and sdy.qk = '" + Dropdl + "'";
                }
                fpselectunion += ")";

                OracleCommand myCommunion = new OracleCommand(fpselectunion, connfp);
                OracleDataReader myReaderunion = myCommunion.ExecuteReader();
                DataRow Fprowunion;
                while (myReaderunion.Read())
                {
                    Fprowunion = Fptable.NewRow();

                    Fprowunion[0] = myReaderunion[0];
                    Fprowunion[1] = myReaderunion[1];
                    Fptable.Rows.Add(Fprowunion);
                }
                myReaderunion.Close();
                myCommunion.Clone();

            }

            try
            {
                DataSet fpset = new DataSet();
                fpset.Tables.Add(Fptable);


                //此处用于绑定数据            
                #region
                int rcount = fpset.Tables["fpdataa"].Rows.Count;
                int ccount = fpset.Tables["fpdataa"].Columns.Count - 1;
                int hcount = 4;
                //FpSpread1.ColumnHeader.Visible = false;
                //FpSpread1.RowHeader.Visible = false;
                if (FpSpread1.Sheets[0].Rows.Count == hcount)
                {
                FpSpread1.Sheets[0].AddRows(hcount, rcount);
                for (int i = 0; i < rcount; i++)
                {
                    FpSpread1.Sheets[0].Cells[i + hcount, 0].Value = i + 1;  //序号
                    int yue = int.Parse(eny.Substring(4, 2));
                    string pjny;

                    
                    FpSpread1.Sheets[0].Cells[i + hcount, 1].Value = fpset.Tables["fpdataa"].Rows[i][0].ToString();
                    
                    
                    for (int j = 1; j <= yue; j++)
                    {
                        if (j < 10)
                        {
                            pjny = eny.Substring(0, 4) + "0" + j.ToString();
                        }
                        else
                        {
                            pjny = eny.Substring(0, 4) + j.ToString();
                        }
                        //求前几个月的效益级别
                        string fpselect1 = "select a.jbmc from dtxyjb_info a,dtstat_djsj_all b ";
                        fpselect1 += "where b.dj_id ='" + fpset.Tables ["fpdataa"].Rows[i][1].ToString () + "' and b.eny='" + pjny + "' and a.jbid=b.gsxyjb_1 and b.cyc_id = '" + cycid + "'";

                        OracleDataAdapter da1 = new OracleDataAdapter(fpselect1, connfp);
                        DataSet fpset1 = new DataSet();
                        da1.Fill(fpset1, "fpdata");
                        if (fpset1.Tables["fpdata"].Rows.Count == 0)
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, ccount + j].Value = "";
                        }
                        else
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, ccount + j].Value = fpset1.Tables["fpdata"].Rows[0][0].ToString();
                        }
                    }
                }
            }
                else//不为空
                {
                    string path = Page.MapPath("../../../static/excel/dongtai.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "表13-边际效益井效益类别动态跟踪表");

                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;

                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                    FpSpread1.Sheets[0].Cells[i + hcount, 0].Value = i + 1;  //序号
                    int yue = int.Parse(eny.Substring(4, 2));
                    string pjny;


                    FpSpread1.Sheets[0].Cells[i + hcount, 1].Value = fpset.Tables["fpdataa"].Rows[i][0].ToString();


                    for (int j = 1; j <= yue; j++)
                    {
                        if (j < 10)
                        {
                            pjny = eny.Substring(0, 4) + "0" + j.ToString();
                        }
                        else
                        {
                            pjny = eny.Substring(0, 4) + j.ToString();
                        }
                        //求前几个月的效益级别
                        string fpselect1 = "select a.jbmc from dtxyjb_info a,dtstat_djsj_all b ";
                        fpselect1 += "where b.dj_id ='" + fpset.Tables["fpdataa"].Rows[i][1].ToString() + "' and b.eny='" + pjny + "' and a.jbid=b.gsxyjb_1 and b.cyc_id = '" + cycid + "'";

                        OracleDataAdapter da1 = new OracleDataAdapter(fpselect1, connfp);
                        DataSet fpset1 = new DataSet();
                        da1.Fill(fpset1, "fpdata");
                        if (fpset1.Tables["fpdata"].Rows.Count == 0)
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, ccount + j].Value = "";
                        }
                        else
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, ccount + j].Value = fpset1.Tables["fpdata"].Rows[0][0].ToString();
                        }
                    }
                }
                }

                #endregion

            }
            catch (OracleException error)
            {
                string CuoWu = "错误: " + error.Message.ToString();
                Response.Write(CuoWu);

            }

            connfp.Close();
        }

        protected int isempty()
        {
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");

            SqlHelper conn = new SqlHelper();
            OracleConnection con1 = conn.GetConn();
            con1.Open();
            string ss = "select bny,eny from dtstat_djsj where bny='" + bny + "' and eny='" + eny + "'";
            OracleDataAdapter da = new OracleDataAdapter(ss, con1);
            DataSet ds = new DataSet();
            da.Fill(ds, "time");
            con1.Close();
            if (ds.Tables["time"].Rows.Count == 0)
                return 0;
            else
                return 1;

        }

        protected void DC_Click(object sender, EventArgs e)
        {
            //this.FpSpread1.Sheets[0].Cells[0, 0].Text = "您点击了‘导出按钮’";

            FpSpread1.Sheets[0].RowHeader.Visible = true;
            FpSpread1.Sheets[0].ColumnHeader.Visible = true;
            //FpSpread1.SaveExcelToResponse("biao13.xls");
            FarpointGridChange.FarPointChange(FpSpread1, "biao13.xls");
            FpSpread1.Sheets[0].RowHeader.Visible = false;
            FpSpread1.Sheets[0].ColumnHeader.Visible = false;


        }

    }
}
