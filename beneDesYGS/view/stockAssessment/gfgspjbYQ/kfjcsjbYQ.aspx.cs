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

namespace beneDesYGS.view.stockAssessment.gfgspjbYQ
{
    public partial class kfjcsjbYQ : beneDesYGS.core.UI.corePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string typeid = _getParam("targetType");
                if (typeid == "dj")
                {
                    initSpread();
                }
                else
                {
                    initSpread2();
                }

            }
        }

        /// <summary>
        /// 初始化spread
        /// </summary>
        protected void initSpread()
        {
            //string typeid = _getParam("targetType");
            //if (typeid == "yt")
            //{
            string path = "../../../static/excel/jdnianduYQ.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表2-单井开发基础数据表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

            //this.FpSpread1.Sheets[0].Cells[3, 1].Text = _getParam("targetType");
            //this.FpSpread1.Sheets[0].Cells[4, 1].Text = _getParam("startMonth") + " -- " + _getParam("endMonth");

            if (isempty() == 0)
            { Response.Write("<script>alert('无数据')</script>"); }
            else
            { sj(); }
        }

        //}
        //else
        //{
        protected void initSpread2()
        {
            string path = "../../../static/excel/jdnianduYQ.xls";
            path = Page.MapPath(path);
            this.FpSpread1.Sheets[0].OpenExcel(path, "表2-区块开发基础数据表");

            this.FpSpread1.Sheets[0].RowHeader.Visible = false;
            this.FpSpread1.Sheets[0].ColumnHeader.Visible = false;

            if (isempty() == 0)
            { Response.Write("<script>alert('无数据')</script>"); }
            else
            { sj2(); }

            //}
        }

        /// <summary>
        /// 导出按钮点击触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DC_Click(object sender, EventArgs e)
        {
            //this.FpSpread1.Sheets[0].Cells[0, 0].Text = "您点击了‘导出按钮’";
            string typeid = _getParam("targetType");
            if (typeid == "dj")
            { FarpointGridChange.FarPointChange(FpSpread1, "jdniaodubiao2.xls"); }
            else
            { FarpointGridChange.FarPointChange(FpSpread1, "jdniandu_biao3.xls"); }
        }

        protected void sj()
        {
            //JObject obj = new JObject();
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            //string list = _getParam("CYC");

            string[] cyc_ids = _getParamArr("CYC");
            string as_cyc = cyc_ids[0];
            string as_cqc = cyc_ids[0];
            if (cyc_ids.Length > 1)
                as_cqc = cyc_ids[1];

            //OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["ygsConnectionString"].ConnectionString);


            #region //原代码
            //string fpselect = " Select jdstat_djsj.pjdyxyjb, jdstat_djsj.gsxyjb, qkxyjb.xymc, gsxylb_info.xymc, ";
            //fpselect += " round(sum(djisopen),4) As kjs, ";
            ////fpselect += " round(sum(rcy)/ Sum(djisopen),2) As djpjrcy, ";
            ////fpselect += " round(Sum(hscyl)/10000,4) As cyoul, ";
            ////fpselect += " round(Sum(jkcyl)/10000,4) As cyl, ";
            //////fpselect += " round(Sum(ljcyl),4) As ljcyoul, ";
            ////fpselect += " round(Sum(hscql)/10000,4) As cql, ";
            ////fpselect += " round((Sum(jkcyl)-Sum(jkcyl*(1-hs/100)))/Sum(jkcyl),4)*100 As hs ";    //round(Sum(hs)/10000,2) As hs
            //fpselect += "  round((Case when Sum(scsj) =0 then 0  else sum(hscql)/Sum(scsj)/ Sum(djisopen) end),2) As djpjrcy,  ";
            //fpselect += " round(Sum(hscql)/100000,4) As cyoul,  ";
            //fpselect += " round((Sum(jkcyl)-Sum(jkcyl*(1-hs/100))),4) As cyl, ";
            //fpselect += " round(sum(hscyl),2) As cql,  ";
            //fpselect += " round(sum(ljcql),2) As hs ";

            //fpselect += " From jdstat_djsj, qkxyjb, gsxylb_info  ";
            //fpselect += " Where djisopen >0 And yqlx <> 'Y00202' and  qkxyjb <> 99 and pjdyxyjb <> 99 and jdstat_djsj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //fpselect += " and jdstat_djsj.pjdyxyjb = qkxyjb.xyjb and jdstat_djsj.gsxyjb = gsxylb_info.xyjb ";
            //fpselect += " Group By jdstat_djsj.pjdyxyjb, jdstat_djsj.gsxyjb ,qkxyjb.xymc, gsxylb_info.xymc ";

            //fpselect += "union";
            //fpselect += " Select pjdyxyjb, 80000 as gsxyjb,qkxyjb.xymc, gsxylb_info.xymc, ";
            //fpselect += " round(sum(djisopen),2) As kjs, ";
            ////fpselect += " round(sum(rcy)/Sum(djisopen),2) As djpjrcy, ";
            ////fpselect += " round(Sum(hscyl)/10000,4) As cyoul, ";
            ////fpselect += " round(Sum(jkcyl)/10000,4) As cyl, ";
            //////fpselect += " round(Sum(ljcyl),4) As ljcyoul, ";
            ////fpselect += " round(Sum(hscql)/10000,4) As cql, ";
            ////fpselect += " round((Sum(jkcyl)-Sum(jkcyl*(1-hs/100)))/Sum(jkcyl),4)*100 As hs ";
            //fpselect += "  round((Case when Sum(scsj) =0 then 0  else sum(hscql)/Sum(scsj)/ Sum(djisopen) end),2) As djpjrcy,  ";
            //fpselect += " round(Sum(hscql)/100000,4) As cyoul,  ";
            //fpselect += " round((Sum(jkcyl)-Sum(jkcyl*(1-hs/100))),4) As cyl, ";
            //fpselect += " round(sum(hscyl),2) As cql,  ";
            //fpselect += " round(sum(ljcql),2) As hs ";

            //fpselect += " From jdstat_djsj,qkxyjb,gsxylb_info  ";
            //fpselect += " Where djisopen >0 And yqlx <> 'Y00202' and qkxyjb <> 99 and pjdyxyjb <> 99 and jdstat_djsj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //fpselect += " and   gsxylb_info.xyjb  = '80000' and qkxyjb.xyjb=jdstat_djsj.pjdyxyjb ";
            //fpselect += " Group By  pjdyxyjb,qkxyjb.xymc, gsxylb_info.xymc ";

            //fpselect += " Union ";
            //fpselect += " Select 90000 As pjdyxyjb,gsxyjb,qkxyjb.xymc, gsxylb_info.xymc, ";
            //fpselect += " round(sum(djisopen),2) As kjs, ";
            ////fpselect += " round(sum(rcy) / Sum(djisopen),2) As djpjrcy, ";
            ////fpselect += " round(Sum(hscyl)/10000,4) As cyoul, ";
            ////fpselect += " round(Sum(jkcyl)/10000,4) As cyl, ";
            //////fpselect += " round(Sum(ljcyl),4) As ljcyoul, ";
            ////fpselect += " round(Sum(hscql)/10000,4) As cql, ";
            ////fpselect += " round((Sum(jkcyl)-Sum(jkcyl*(1-hs/100)))/Sum(jkcyl),4)*100 As hs ";
            //fpselect += "  round((Case when Sum(scsj) =0 then 0  else sum(hscql)/Sum(scsj)/ Sum(djisopen) end),2) As djpjrcy,  ";
            //fpselect += " round(Sum(hscql)/100000,4) As cyoul,  ";
            //fpselect += " round((Sum(jkcyl)-Sum(jkcyl*(1-hs/100))),4) As cyl, ";
            //fpselect += " round(sum(hscyl),2) As cql,  ";
            //fpselect += " round(sum(ljcql),2) As hs ";

            //fpselect += " From jdstat_djsj,qkxyjb,gsxylb_info  ";
            //fpselect += " Where djisopen >0 And yqlx <> 'Y00202' and qkxyjb <> 99 and pjdyxyjb <> 99 and jdstat_djsj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //fpselect += " and  jdstat_djsj.gsxyjb = gsxylb_info.xyjb  and qkxyjb.xyjb='90000' ";
            //fpselect += " Group By jdstat_djsj.gsxyjb ,qkxyjb.xymc, gsxylb_info.xymc ";
            //fpselect += " Union ";

            //fpselect += " Select 90000 As pjdyxyjb, 80000 as gsxyjb,qkxyjb.xymc, gsxylb_info.xymc, ";
            //fpselect += " round(sum(djisopen),2) As kjs, ";
            ////fpselect += " round(sum(rcy)/Sum(djisopen),2) As djpjrcy, ";
            ////fpselect += " round(Sum(hscyl)/10000,4) As cyoul, ";
            ////fpselect += " round(Sum(jkcyl)/10000,4) As cyl, ";
            //////fpselect += " round(Sum(ljcyl),4) As ljcyoul, ";
            ////fpselect += " round(Sum(hscql)/10000,4) As cql, ";
            ////fpselect += " round((Sum(jkcyl)-Sum(jkcyl*(1-hs/100)))/Sum(jkcyl),4)*100 As hs ";
            //fpselect += "  round((Case when Sum(scsj) =0 then 0  else sum(hscql)/Sum(scsj)/ Sum(djisopen) end),2) As djpjrcy,  ";
            //fpselect += " round(Sum(hscql)/100000,4) As cyoul,  ";
            //fpselect += " round((Sum(jkcyl)-Sum(jkcyl*(1-hs/100))),4) As cyl, ";
            //fpselect += " round(sum(hscyl),2) As cql,  ";
            //fpselect += " round(sum(ljcql),2) As hs ";

            //fpselect += " From jdstat_djsj,qkxyjb,gsxylb_info  ";
            //fpselect += " Where djisopen >0 And yqlx <> 'Y00202' and qkxyjb <> 99 and pjdyxyjb <> 99 and jdstat_djsj.cyc_id = '" + Session["cyc_id"].ToString() + "' ";
            //fpselect += " and   gsxylb_info.xyjb  = '80000' and qkxyjb.xyjb='90000' ";
            //fpselect += " Group By  qkxyjb.xymc, gsxylb_info.xymc ";
            #endregion

            DataSet ds = new DataSet();
            //var param_bny = new OracleParameter("as_bny", OracleType.VarChar);
            //param_bny.Value = bny;
            //var param_eny = new OracleParameter("as_eny", OracleType.VarChar);
            //param_eny.Value = eny;
            var param_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc.Value = as_cyc;
            var param_cqc = new OracleParameter("as_cqc", OracleType.VarChar);
            param_cqc.Value = as_cqc;
            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;

            try
            {
                ds = sql.GetDataSet("OWCBS_VIEW_KFJCSJB.KFJCSJB_DJ_PROC", CommandType.StoredProcedure,
                   new OracleParameter[] { param_cyc, param_cqc, param_out });


                //DataTable Fptable用来输出数据
                DataTable Fptable = GetColumnsTable("fpdata", "dj");

                //float hj1 = 0, hj2 = 0, hj3 = 0, hj4 = 0, hj5 = 0;
                //DataRow Fprow;



                DataSet fpset = new DataSet();
                //fpset.Tables.Add(Fptable);
                fpset = ds;
                //此处用于绑定数据
                #region
                int rcount = ds.Tables[0].Rows.Count;
                int ccount = ds.Tables[0].Columns.Count;
                int hcount = 4;
                if (FpSpread1.Sheets[0].Rows.Count == hcount)
                {
                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables[0].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j].HorizontalAlign = HorizontalAlign.Center;
                        }
                    }
                    int k = 1;  //统计重复单元格
                    int w = hcount;  //记录起始位置
                    for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                    {
                        if (FpSpread1.Sheets[0].Cells[i, 0].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, 0].Value.ToString())
                        {
                            k++;
                        }
                        else
                        {
                            FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
                            FpSpread1.Sheets[0].Cells[w, 0].HorizontalAlign = HorizontalAlign.Center;
                            w = i;
                            k = 1;
                        }
                    }
                    FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
                    int width = 0;
                    for (int Col = 0; Col < FpSpread1.Sheets[0].Columns.Count; Col++)
                    {
                        width += FpSpread1.Sheets[0].Columns[Col].Width;
                    }

                    FpSpread1.Width = Unit.Pixel(width + 300);
                }
                else//不为空
                {
                    string path = Page.MapPath("~/static/excel/jdnianduYQ.xls");
                    FpSpread1.Sheets[0].OpenExcel(path, "表2-单井开发基础数据表");

                    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + " " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                    FpSpread1.Sheets[0].RowHeader.Visible = false;


                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            if ((j == 3 || j == (ccount - 1)) && j != 2 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                            {

                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");

                            }
                            else if (j != 2 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = float.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                            }
                            else
                            {
                                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                            }
                            FpSpread1.Sheets[0].Cells[i + hcount, j].HorizontalAlign = HorizontalAlign.Center;
                            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }
                    int k = 1;  //统计重复单元格
                    int w = hcount;  //记录起始位置
                    for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                    {
                        if (FpSpread1.Sheets[0].Cells[i, 0].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, 0].Value.ToString())
                        {
                            k++;
                        }
                        else
                        {
                            FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
                            FpSpread1.Sheets[0].Cells[w, 0].HorizontalAlign = HorizontalAlign.Center;
                            w = i;
                            k = 1;
                        }
                    }
                    FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
                }
                #endregion

            }
            catch (OracleException error)
            {
                string CuoWu = "错误: " + error.Message.ToString();
                Response.Write(CuoWu);

            }
            //connfp.Close();
        }
        /// <summary>
        /// 获取列目录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="tableType"></param>
        /// <returns></returns>
        private DataTable GetColumnsTable(string tableName, string tableType)
        {
            DataTable dt = new DataTable(tableName);
            if (tableType == "dj")
            {
                dt.Columns.Add("pjdyxyjb", typeof(string));
                dt.Columns.Add("gsxyjb", typeof(System.String));
                dt.Columns.Add("kjs", typeof(float));
                dt.Columns.Add("djpjrcy", typeof(float));
                dt.Columns.Add("cyoul", typeof(float));
                dt.Columns.Add("cyl", typeof(float));
                dt.Columns.Add("cql", typeof(float));
                dt.Columns.Add("hs", typeof(float));
            }
            else if (tableType == "qk")
            {

            }
            return dt;
        }


        protected void sj2()
        {
            //string cycid = Session["cyc_id"].ToString();
            //JObject obj = new JObject();
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            //obj["departmentStore"] = _getParam("departmentStore");
            //string list = obj["departmentStore"].ToString();
            //string list = _getParam("CYC");
            string typeid = _getParam("targetType");

            string[] cyc_ids = _getParamArr("CYC");
            string as_cyc = cyc_ids[0];
            string as_cqc = cyc_ids[0];
            if (cyc_ids.Length > 1)
                as_cqc = cyc_ids[1];

            #region //旧代码

            //OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["ygsConnectionString"].ConnectionString);

            //string fpselect = "";
            //if (typeid == "qk")
            //{
            //    fpselect = " SELECT 1 as od,gsxyjb ,qkxyjb.xymc,  ";
            //    fpselect += " JDSTAT_QKSJ.SSYT, ";
            //    fpselect += " JDSTAT_QKSJ.MC, ";
            //    fpselect += " JDSTAT_QKSJ.YCLX,  ";
            //    fpselect += " JDSTAT_QKSJ.DYHYMJ, ";
            //    fpselect += " JDSTAT_QKSJ.DYDZCL, ";
            //    fpselect += " JDSTAT_QKSJ.DYKCCL, ";
            //    fpselect += " round(JDSTAT_QKSJ.YCZS,2),  ";
            //    fpselect += " JDSTAT_QKSJ.PJSTL, ";
            //    fpselect += " JDSTAT_QKSJ.DXYYND, ";
            //    fpselect += " JDSTAT_QKSJ.YJZJS, ";
            //    fpselect += " JDSTAT_QKSJ.SJZJS, ";
            //    fpselect += " JDSTAT_QKSJ.YJKJS, ";
            //    fpselect += " JDSTAT_QKSJ.SJKJS, ";
            //    fpselect += " round(JDSTAT_QKSJ.DJPJRCY,2), ";
            //    fpselect += " nvl(round(JDSTAT_QKSJ.CYOUL/10000,4),0), ";
            //    fpselect += " nvl(JDSTAT_QKSJ.CYL,0), ";
            //    fpselect += " nvl(round(JDSTAT_QKSJ.LJCYOUL,4),0), ";
            //    fpselect += " nvl(round(JDSTAT_QKSJ.CQL,4),0),  ";
            //    fpselect += " nvl(JDSTAT_QKSJ.ZSJ,0), ";
            //    fpselect += " nvl(round(JDSTAT_QKSJ.NJZHHS,2),0), ";
            //    fpselect += " JDSTAT_QKSJ.KCCLCCCD, ";
            //    fpselect += " JDSTAT_QKSJ.SYKCCL, ";
            //    fpselect += " JDSTAT_QKSJ.SYKCCLCCSD, ";
            //    fpselect += " JDSTAT_QKSJ.DJKZSYKCCL, ";
            //    /////////////////////////////////////////////////////////
            //    //fpselect += " JDSTAT_QKSJ.JWMD ";
            //    fpselect += " round((case when nvl(DYHYMJ,0)=0 then 0 else (YJZJS+SJZJS)/DYHYMJ  end),2) as JWMD ";
            //    fpselect += "  FROM JDSTAT_QKSJ,qkxyjb ";
            //    fpselect += " WHERE ( JDSTAT_QKSJ.BNY = " + bny + " ) AND ";
            //    fpselect += " ( JDSTAT_QKSJ.ENY = " + eny + " ) AND ";
            //    fpselect += " ( JDSTAT_QKSJ.YCLX <> '热采稠油油藏' )   ";
            //    fpselect += " and JDSTAT_QKSJ.GSXYJB = qkxyjb.xyjb and jdstat_qksj.cyc_id = '" + cycid + "' ";

            //    fpselect += " Union ";
            //    fpselect += " Select 2 as od,gsxyjb, xymc,'' As ssyt,'合计' As pjdymc, ";
            //    fpselect += "  ' ' as YCLX,  ";
            //    fpselect += " sum(nvl(DYHYMJ,0)) as DYHYMJ, ";
            //    fpselect += " sum(nvl(DYDZCL,0)) as DYDZCL , ";
            //    fpselect += " sum(nvl(DYKCCL,0)) as DYKCCL , ";
            //    fpselect += " 0 as YCZS ,  ";
            //    fpselect += " 0 as PJSTL, ";
            //    fpselect += " 0 as DXYYND, ";
            //    fpselect += " sum(nvl(YJZJS,0)) as YJZJS, ";
            //    fpselect += " sum(nvl(SJZJS,0)) as SJZJS, ";
            //    fpselect += " sum(nvl(YJKJS,0)) as YJKJS, ";
            //    fpselect += " sum(nvl(SJKJS,0)) as SJKJS, ";
            //    fpselect += " round((case when sum(YJKJS)=0 then 0 else sum(DJPJRCY*YJKJS)/sum(YJKJS) end),2) as DJPJRCY, ";
            //    fpselect += " round(sum(nvl(CYOUL,0))/10000,4) as CYOUL, ";
            //    fpselect += " sum(nvl(CYL,0)) as CYL, ";
            //    fpselect += " round(sum(nvl(LJCYOUL,0)),4) as LJCYOUL, ";
            //    fpselect += " sum(nvl(round(CQL,4),0)) as CQL,  ";
            //    fpselect += " sum(nvl(ZSJ,0)) as ZSJ, ";
            //    fpselect += " round((Sum(cyl)-sum(cyl*(1-NJZHHS/100)))/Sum(cyl),4)*100 as NJZHHS, ";
            //    //fpselect += " 0 as KCCLCCCD, ";
            //    //fpselect += " sum(nvl(SYKCCL,0)) as SYKCCL, ";
            //    //fpselect += " 0 as SYKCCLCCSD, ";

            //    fpselect += "  round((case when sum(DYKCCL)=0 then 0 else sum(LJCYOUL)/SUM(DYKCCL)*100 end),4) as KCCLCCCD, ";
            //    fpselect += " sum(nvl(SYKCCL,0)) as SYKCCL, ";
            //    fpselect += " round((case when (sum(DYKCCL)-sum(LJCYOUL)+sum(CYOUL))=0 then 0 else sum(CYOUL)/10000/(sum(DYKCCL)-sum(LJCYOUL)+sum(CYOUL)/10000)*100 end),4) as SYKCCLCCSD, ";


            //    fpselect += " sum(nvl(DJKZSYKCCL,0)) as DJKZSYKCCL, ";
            //    fpselect += " round((case when sum(nvl(DYHYMJ,0))=0 then 0 else sum(YJZJS+SJZJS)/sum(DYHYMJ)  end),2) as JWMD ";
            //    fpselect += " From jdstat_qksj, qkxyjb ";
            //    fpselect += " Where jdstat_qksj.bny = " + bny + " and jdstat_qksj.eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and jdstat_qksj.gsxyjb = qkxyjb.xyjb  and jdstat_qksj.cyc_id = '" + cycid + "' ";
            //    fpselect += " Group By gsxyjb,xymc ";

            //    fpselect += " Union ";
            //    fpselect += " Select 3 as od,90000 as gsxyjb,xymc,'' As ssyt,'总计' As pjdymc, ";
            //    fpselect += "  ' ' as YCLX,  ";
            //    fpselect += " sum(nvl(DYHYMJ,0)) as DYHYMJ, ";
            //    fpselect += " sum(nvl(DYDZCL,0)) as DYDZCL , ";
            //    fpselect += " sum(nvl(DYKCCL,0)) as DYKCCL , ";
            //    fpselect += " 0 as YCZS ,  ";
            //    fpselect += " 0 as PJSTL, ";
            //    fpselect += " 0 as DXYYND, ";
            //    fpselect += " sum(nvl(YJZJS,0)) as YJZJS, ";
            //    fpselect += " sum(nvl(SJZJS,0)) as SJZJS, ";
            //    fpselect += " sum(nvl(YJKJS,0)) as YJKJS, ";
            //    fpselect += " sum(nvl(SJKJS,0)) as SJKJS, ";
            //    fpselect += " round((case when sum(YJKJS)=0 then 0 else sum(DJPJRCY*YJKJS)/sum(YJKJS) end),2) as DJPJRCY, ";
            //    fpselect += " round(sum(nvl(CYOUL,0))/10000,4) as CYOUL, ";
            //    fpselect += " sum(nvl(CYL,0)) as CYL, ";
            //    fpselect += " round(sum(nvl(LJCYOUL,0)),4) as LJCYOUL, ";
            //    fpselect += " sum(nvl(round(CQL,4),0)) as CQL,  ";
            //    fpselect += " sum(nvl(ZSJ,0)) as ZSJ, ";
            //    fpselect += " round((Sum(cyl)-sum(cyl*(1-NJZHHS/100)))/Sum(cyl),4)*100 as NJZHHS, ";
            //    //fpselect += " 0 as KCCLCCCD, ";
            //    //fpselect += " sum(nvl(SYKCCL,0)) as SYKCCL, ";
            //    //fpselect += " 0 as SYKCCLCCSD, ";
            //    fpselect += "  round((case when sum(DYKCCL)=0 then 0 else sum(LJCYOUL)/SUM(DYKCCL)*100 end),4) as KCCLCCCD, ";
            //    fpselect += " sum(nvl(SYKCCL,0)) as SYKCCL, ";
            //    fpselect += " round((case when (sum(DYKCCL)-sum(LJCYOUL)+sum(CYOUL))=0 then 0 else sum(CYOUL)/10000/(sum(DYKCCL)-sum(LJCYOUL)+sum(CYOUL)/10000)*100 end),4) as SYKCCLCCSD, ";
            //    fpselect += " sum(nvl(DJKZSYKCCL,0)) as DJKZSYKCCL, ";
            //    fpselect += " round((case when sum(nvl(DYHYMJ,0))=0 then 0 else sum(YJZJS+SJZJS)/sum(DYHYMJ)  end),2) as JWMD ";
            //    fpselect += " From jdstat_qksj,qkxyjb  ";
            //    fpselect += " Where bny = " + bny + " and eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and xyjb = '90000' and cyc_id = '" + cycid + "' ";
            //    fpselect += " Group By xymc ";
            //    fpselect += " order by gsxyjb ";

            //}
            //else
            //{
            //    fpselect = " SELECT 1 as od,gsxyjb ,qkxyjb.xymc,  ";
            //    fpselect += " JDSTAT_pjdySJ.SSYT, ";
            //    fpselect += " JDSTAT_pjdySJ.MC, ";
            //    fpselect += " JDSTAT_pjdySJ.YCLX,  ";
            //    fpselect += " JDSTAT_pjdySJ.DYHYMJ, ";
            //    fpselect += " JDSTAT_pjdySJ.DYDZCL, ";
            //    fpselect += " JDSTAT_pjdySJ.DYKCCL, ";
            //    fpselect += "round( JDSTAT_pjdySJ.YCZS,2),  ";
            //    fpselect += " JDSTAT_pjdySJ.PJSTL, ";
            //    fpselect += " JDSTAT_pjdySJ.DXYYND, ";
            //    fpselect += " JDSTAT_pjdySJ.YJZJS, ";
            //    fpselect += " JDSTAT_pjdySJ.SJZJS, ";
            //    fpselect += " JDSTAT_pjdySJ.YJKJS, ";
            //    fpselect += " JDSTAT_pjdySJ.SJKJS, ";
            //    fpselect += " round(JDSTAT_pjdySJ.DJPJRCY,2), ";
            //    fpselect += " nvl(round(JDSTAT_pjdySJ.CYOUL/10000,4),0), ";
            //    fpselect += " nvl(JDSTAT_pjdySJ.CYL,0), ";
            //    fpselect += " nvl(round(JDSTAT_pjdySJ.LJCYOUL,4),0), ";
            //    fpselect += " nvl(round(JDSTAT_pjdySJ.CQL,4),0),  ";
            //    fpselect += " nvl(JDSTAT_pjdySJ.ZSJ,0), ";
            //    fpselect += " nvl(round(JDSTAT_pjdySJ.NJZHHS,2),0), ";
            //    fpselect += " JDSTAT_pjdySJ.KCCLCCCD, ";
            //    fpselect += " JDSTAT_pjdySJ.SYKCCL, ";
            //    fpselect += " JDSTAT_pjdySJ.SYKCCLCCSD, ";
            //    fpselect += " JDSTAT_pjdySJ.DJKZSYKCCL, ";
            //    //fpselect += " JDSTAT_pjdySJ.JWMD ";
            //    fpselect += " round((case when nvl(DYHYMJ,0)=0 then 0 else (YJZJS+SJZJS)/DYHYMJ  end),2) as JWMD ";
            //    fpselect += " FROM JDSTAT_pjdySJ,qkxyjb ";
            //    fpselect += " WHERE ( JDSTAT_pjdySJ.BNY = " + bny + " ) AND ";
            //    fpselect += " ( JDSTAT_pjdySJ.ENY = " + eny + " )  ";
            //    //fpselect += " ( JDSTAT_pjdySJ.YCLX <> '热采稠油油藏' )   ";
            //    fpselect += " and JDSTAT_pjdySJ.GSXYJB = qkxyjb.xyjb and jdstat_pjdysj.cyc_id = '" + cycid + "' ";
            //    //fpselect += " order by gsxyjb ";

            //    fpselect += " Union ";
            //    fpselect += " Select 2 as od,gsxyjb, xymc,'' As ssyt,'合计' As pjdymc, ";
            //    fpselect += "  ' ' as YCLX,  ";
            //    fpselect += " sum(nvl(DYHYMJ,0)) as DYHYMJ, ";
            //    fpselect += " sum(nvl(DYDZCL,0)) as DYDZCL , ";
            //    fpselect += " sum(nvl(DYKCCL,0)) as DYKCCL , ";
            //    fpselect += " 0 as YCZS ,  ";
            //    fpselect += " 0 as PJSTL, ";
            //    fpselect += " 0 as DXYYND, ";
            //    fpselect += " sum(nvl(YJZJS,0)) as YJZJS, ";
            //    fpselect += " sum(nvl(SJZJS,0)) as SJZJS, ";
            //    fpselect += " sum(nvl(YJKJS,0)) as YJKJS, ";
            //    fpselect += " sum(nvl(SJKJS,0)) as SJKJS, ";
            //    fpselect += " round((case when sum(YJKJS)=0 then 0 else sum(DJPJRCY*YJKJS)/sum(YJKJS) end),2) as DJPJRCY, ";
            //    fpselect += " round(sum(nvl(CYOUL,0))/10000,4) as CYOUL, ";
            //    fpselect += " sum(nvl(CYL,0)) as CYL, ";
            //    fpselect += " round(sum(nvl(LJCYOUL,0)),4) as LJCYOUL, ";
            //    fpselect += " sum(nvl(ROUND(CQL,4),0)) as CQL,  ";
            //    fpselect += " sum(nvl(ZSJ,0)) as ZSJ, ";
            //    fpselect += " round((Sum(cyl)-sum(cyl*(1-NJZHHS/100)))/Sum(cyl),4)*100 as NJZHHS, ";
            //    fpselect += "  round((case when sum(DYKCCL)=0 then 0 else sum(LJCYOUL)/SUM(DYKCCL)*100 end),4) as KCCLCCCD, ";
            //    fpselect += " sum(nvl(SYKCCL,0)) as SYKCCL, ";
            //    fpselect += " round((case when (sum(DYKCCL)-sum(LJCYOUL)+sum(CYOUL))=0 then 0 else sum(CYOUL)/10000/(sum(DYKCCL)-sum(LJCYOUL)+sum(CYOUL)/10000)*100 end),4) as SYKCCLCCSD, ";
            //    fpselect += " sum(nvl(DJKZSYKCCL,0)) as DJKZSYKCCL, ";
            //    //fpselect += " round((case when sum(nvl(DYHYMJ,0))=0 then 0 else sum(YJZJS+SJZJS)/sum(DYHYMJ) end),2) as JWMD ";
            //    fpselect += " round((case when sum(nvl(DYHYMJ,0))=0 then 0 else sum(YJZJS+SJZJS)/sum(DYHYMJ) end),2) as JWMD ";
            //    fpselect += " From JDSTAT_pjdySJ, qkxyjb ";
            //    fpselect += " Where bny = " + bny + " and eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and gsxyjb = qkxyjb.xyjb  and cyc_id = '" + cycid + "' ";
            //    fpselect += " Group By gsxyjb,xymc ";
            //    //fpselect += " order by gsxyjb ";

            //    fpselect += " Union ";
            //    fpselect += " Select 3 as od,90000 as gsxyjb,xymc,'' As ssyt,'总计' As pjdymc, ";
            //    fpselect += "  ' ' as YCLX,  ";
            //    fpselect += " sum(nvl(DYHYMJ,0)) as DYHYMJ, ";
            //    fpselect += " sum(nvl(DYDZCL,0)) as DYDZCL , ";
            //    fpselect += " sum(nvl(DYKCCL,0)) as DYKCCL , ";
            //    fpselect += " 0 as YCZS ,  ";
            //    fpselect += " 0 as PJSTL, ";
            //    fpselect += " 0 as DXYYND, ";
            //    fpselect += " sum(nvl(YJZJS,0)) as YJZJS, ";
            //    fpselect += " sum(nvl(SJZJS,0)) as SJZJS, ";
            //    fpselect += " sum(nvl(YJKJS,0)) as YJKJS, ";
            //    fpselect += " sum(nvl(SJKJS,0)) as SJKJS, ";
            //    fpselect += " round((case when sum(YJKJS)=0 then 0 else sum(DJPJRCY*YJKJS)/sum(YJKJS) end),2) as DJPJRCY, ";
            //    fpselect += " round(sum(nvl(CYOUL,0))/10000,4) as CYOUL, ";
            //    fpselect += " sum(nvl(CYL,0)) as CYL, ";
            //    fpselect += " round(sum(nvl(LJCYOUL,0)),4) as LJCYOUL, ";
            //    fpselect += " sum(nvl(ROUND(CQL,4),0)) as CQL,  ";
            //    fpselect += " sum(nvl(ZSJ,0)) as ZSJ, ";
            //    fpselect += " round((Sum(cyl)-sum(cyl*(1-NJZHHS/100)))/Sum(cyl),4)*100 as NJZHHS, ";
            //    fpselect += "  round((case when sum(DYKCCL)=0 then 0 else sum(LJCYOUL)/SUM(DYKCCL)*100 end),4) as KCCLCCCD, ";
            //    fpselect += " sum(nvl(SYKCCL,0)) as SYKCCL, ";
            //    fpselect += " round((case when (sum(DYKCCL)-sum(LJCYOUL)+sum(CYOUL))=0 then 0 else sum(CYOUL)/10000/(sum(DYKCCL)-sum(LJCYOUL)+sum(CYOUL)/10000)*100 end),4) as SYKCCLCCSD, ";
            //    fpselect += " sum(nvl(DJKZSYKCCL,0)) as DJKZSYKCCL, ";
            //    //fpselect += " round((case when sum(nvl(DYHYMJ,0))=0 then 0 else sum(YJZJS+SJZJS)/sum(DYHYMJ) end),2) as JWMD ";
            //    fpselect += " round((case when sum(nvl(DYHYMJ,0))=0 then 0 else sum(YJZJS+SJZJS)/sum(DYHYMJ) end),2) as JWMD ";
            //    fpselect += " From JDSTAT_pjdySJ,qkxyjb  ";
            //    fpselect += " Where bny = " + bny + " and eny = " + eny + " and sfpj = 1 ";
            //    fpselect += " and xyjb = '90000' and cyc_id = '" + cycid + "' ";
            //    fpselect += " Group By xymc ";
            //    fpselect += " order by gsxyjb ";

            //}

            #endregion

            DataSet ds = new DataSet();
            var param_bny = new OracleParameter("as_bny", OracleType.VarChar);
            param_bny.Value = bny;
            var param_eny = new OracleParameter("as_eny", OracleType.VarChar);
            param_eny.Value = eny;
            var param_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc.Value = as_cyc;
            var param_cqc = new OracleParameter("as_cqc", OracleType.VarChar);
            param_cqc.Value = as_cqc;
            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;

            try
            {
                if (typeid == "qk")
                {
                    ds = sql.GetDataSet("OWCBS_VIEW_KFJCSJB.KFJCSJB_QK_PROC", CommandType.StoredProcedure,
                        new OracleParameter[] { param_bny, param_eny, param_cyc, param_cqc, param_out });
                }
                else
                {
                    ds = sql.GetDataSet("OWCBS_VIEW_KFJCSJB.KFJCSJB_PJDY_PROC", CommandType.StoredProcedure,
                        new OracleParameter[] { param_bny, param_eny, param_cyc, param_cqc, param_out });
                }


                #region //旧代码
                //connfp.Open();
                //OracleCommand myComm = new OracleCommand(fpselect, connfp);
                //OracleDataReader myReader = myComm.ExecuteReader();
                ////DataTable Fptable用来输出数据
                //int n = 1;
                //DataTable Fptable = new DataTable("fpdata");
                //Fptable.Columns.Add("pjdyxyjb", typeof(string));
                //Fptable.Columns.Add("xuhao", typeof(int));
                //Fptable.Columns.Add("pjdymc", typeof(string));
                //Fptable.Columns.Add("ssyt", typeof(string));
                //Fptable.Columns.Add("YCLX", typeof(string));
                //Fptable.Columns.Add("DYHYMJ", typeof(float));
                //Fptable.Columns.Add("DYDZCL", typeof(float));
                //Fptable.Columns.Add("DYKCCL", typeof(float));
                //Fptable.Columns.Add("YCZS", typeof(float));
                //Fptable.Columns.Add("PJSTL", typeof(float));
                //Fptable.Columns.Add("DXYYND", typeof(float));
                //Fptable.Columns.Add("YJZJS", typeof(float));
                //Fptable.Columns.Add("SJZJS", typeof(float));
                //Fptable.Columns.Add("YJKJS", typeof(float));
                //Fptable.Columns.Add("SJKJS", typeof(float));
                //Fptable.Columns.Add("DJPJRCY", typeof(float));
                //Fptable.Columns.Add("CYOUL", typeof(float));
                //Fptable.Columns.Add("CYL", typeof(float));
                //Fptable.Columns.Add("LJCYOUL", typeof(float));
                //Fptable.Columns.Add("CQL", typeof(float));
                //Fptable.Columns.Add("ZSJ", typeof(float));
                //Fptable.Columns.Add("NJZHHS", typeof(float));
                //Fptable.Columns.Add("KCCLCCCD", typeof(float));
                //Fptable.Columns.Add("SYKCCL", typeof(float));
                //Fptable.Columns.Add("SYKCCLCCSD", typeof(float));
                //Fptable.Columns.Add("DJKZSYKCCL", typeof(float));
                //Fptable.Columns.Add("JWMD", typeof(float));
                //DataRow Fprow;
                //while (myReader.Read())
                //{
                //    Fprow = Fptable.NewRow();

                //    Fprow[0] = myReader[2];
                //    Fprow[1] = n++;
                //    Fprow[2] = myReader.GetValue(3);
                //    Fprow[3] = myReader[4];
                //    Fprow[4] = myReader.GetValue(5);
                //    Fprow[5] = myReader.GetValue(6);
                //    Fprow[6] = myReader.GetValue(7);
                //    Fprow[7] = myReader.GetValue(8);
                //    Fprow[8] = myReader.GetValue(9);
                //    Fprow[9] = myReader.GetValue(10);
                //    Fprow[10] = myReader.GetValue(11);
                //    Fprow[11] = myReader.GetValue(12);
                //    Fprow[12] = myReader.GetValue(13);
                //    Fprow[13] = myReader.GetValue(14);
                //    Fprow[14] = myReader.GetValue(15);
                //    Fprow[15] = myReader.GetValue(16);
                //    Fprow[16] = myReader.GetValue(17);
                //    Fprow[17] = myReader.GetValue(18);
                //    Fprow[18] = myReader.GetValue(19);
                //    Fprow[19] = myReader.GetValue(20);
                //    Fprow[20] = myReader.GetValue(21);
                //    Fprow[21] = myReader.GetValue(22);
                //    Fprow[22] = myReader.GetValue(23);
                //    Fprow[23] = myReader.GetValue(24);
                //    Fprow[24] = myReader.GetValue(25);
                //    Fprow[25] = myReader.GetValue(26);
                //    Fprow[26] = myReader.GetValue(27);
                //    Fptable.Rows.Add(Fprow);
                //}
                //myReader.Close();
                //myComm.Clone();

                #endregion

                DataSet fpset = new DataSet();
                //fpset.Tables.Add(Fptable);
                fpset = ds;
                int rcount = fpset.Tables[0].Rows.Count;
                int ccount = fpset.Tables[0].Columns.Count;
                int hcount = 4;
                if (FpSpread1.Sheets[0].Rows.Count == hcount)
                {
                    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 1; j < ccount; j++)
                        {
                            FpSpread1.Sheets[0].Cells[i + hcount, j - 1].Value = fpset.Tables[0].Rows[i][j].ToString();
                            FpSpread1.Sheets[0].Cells[i + hcount, j - 1].HorizontalAlign = HorizontalAlign.Center;
                        }
                    }
                }

                //此处用于绑定数据            
                #region
                ////int rcount = fpset.Tables["fpdata"].Rows.Count;
                ////int ccount = fpset.Tables["fpdata"].Columns.Count;
                //int rcount = fpset.Tables[0].Rows.Count;
                //int ccount = fpset.Tables[0].Columns.Count;
                //int hcount = 4;
                ////FpSpread1.ColumnHeader.Visible = false;
                ////FpSpread1.RowHeader.Visible = false;
                //if (FpSpread1.Sheets[0].Rows.Count == hcount)
                //{
                //    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                //    for (int i = 0; i < rcount; i++)
                //    {
                //        for (int j = 0; j < ccount; j++)
                //        {
                //            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                //            if ((j == 8 || j == 9 || j == 10 || j == 15 || j == 21 || j == 22 || j == 24) && isNotFloat.Isdouble(fpset.Tables[0].Rows[i][j].ToString()) && fpset.Tables[0].Rows[i][j].ToString().Trim() != "0")

                //                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");

                //            else if (j != 1 && j != 11 && j != 12 && j != 13 && j != 14 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                //                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                //            else
                //                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                //            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;

                //        }
                //    }
                //    int k = 1;  //统计重复单元格
                //    int w = hcount;  //记录起始位置
                //    for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                //    {
                //        if (FpSpread1.Sheets[0].Cells[i, 0].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, 0].Value.ToString())
                //        {
                //            k++;
                //        }
                //        else
                //        {
                //            FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
                //            w = i;
                //            k = 1;
                //        }
                //    }
                //    FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
                //    //重新写序号
                //    k = 1;
                //    w = hcount;
                //    for (int i = w; i < FpSpread1.Sheets[0].Rows.Count; i++)
                //    {
                //        if (FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() != "合计" && FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() != "总计")
                //        {
                //            FpSpread1.Sheets[0].Cells[i, 1].Value = k;

                //            k++;
                //            FpSpread1.Sheets[0].Cells[i, 1].Font.Size = 9;
                //        }
                //        else if (FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() == "合计")
                //        {
                //            FpSpread1.ActiveSheetView.AddSpanCell(i, 1, 1, 3);
                //            FpSpread1.Sheets[0].Cells[i, 1].Value = "合计";
                //        }
                //        else if (FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() == "总计")
                //        {
                //            FpSpread1.ActiveSheetView.AddSpanCell(i, 0, 1, 4);
                //            FpSpread1.Sheets[0].Cells[i, 0].Value = "总计";
                //        }
                //    }
                //}
                //#region 原来的处理
                //else//不为空
                //{
                //    string path = Page.MapPath("../../../static/excel/jdnianduYQ.xls");
                //    FpSpread1.Sheets[0].OpenExcel(path, "表2-区块开发基础数据表");


                //    FpSpread1.Sheets[0].Cells[0, 0].Text = bny + "-" + eny + "  " + FpSpread1.Sheets[0].Cells[0, 0].Text;
                //    FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                //    FpSpread1.Sheets[0].RowHeader.Visible = false;
                //    /////////////////////

                //    FpSpread1.Sheets[0].AddRows(hcount, rcount);
                //    for (int i = 0; i < rcount; i++)
                //    {
                //        for (int j = 0; j < ccount; j++)
                //        {
                //            //FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                //            //FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                //            if ((j == 8 || j == 9 || j == 10 || j == 15 || j == 21 || j == 22 || j == 24) && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")

                //                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.00");

                //            else if (j != 1 && j != 11 && j != 12 && j != 13 && j != 14 && isNotFloat.Isdouble(fpset.Tables["fpdata"].Rows[i][j].ToString()) && fpset.Tables["fpdata"].Rows[i][j].ToString().Trim() != "0")
                //                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = double.Parse(fpset.Tables["fpdata"].Rows[i][j].ToString()).ToString("0.0000");
                //            else
                //                FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables["fpdata"].Rows[i][j].ToString();
                //            FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                //        }
                //    }
                //int k = 1;  //统计重复单元格
                //int w = hcount;  //记录起始位置
                //for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                //{
                //    if (FpSpread1.Sheets[0].Cells[i, 0].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, 0].Value.ToString())
                //    {
                //        k++;
                //    }
                //    else
                //    {
                //        FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
                //        w = i;
                //        k = 1;
                //    }
                //}
                //FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
                ////重新写序号
                //k = 1;
                //w = hcount;
                //for (int i = w; i < FpSpread1.Sheets[0].Rows.Count; i++)
                //{
                //    if (FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() != "合计" && FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() != "总计")
                //    {
                //        FpSpread1.Sheets[0].Cells[i, 1].Value = k;

                //        k++;
                //        FpSpread1.Sheets[0].Cells[i, 1].Font.Size = 9;
                //    }
                //    else if (FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() == "合计")
                //    {
                //        FpSpread1.ActiveSheetView.AddSpanCell(i, 1, 1, 3);
                //        FpSpread1.Sheets[0].Cells[i, 1].Value = "合计";
                //    }
                //    else if (FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() == "总计")
                //    {
                //        FpSpread1.ActiveSheetView.AddSpanCell(i, 0, 1, 4);
                //        FpSpread1.Sheets[0].Cells[i, 0].Value = "总计";
                //    }
                //}
                //}
                //#endregion
                #endregion
                int k = 1;  //统计重复单元格
                int w = hcount;  //记录起始位置
                for (int i = w + 1; i < FpSpread1.Sheets[0].Rows.Count; i++)
                {
                    if (FpSpread1.Sheets[0].Cells[i, 0].Value.ToString() == FpSpread1.Sheets[0].Cells[i - 1, 0].Value.ToString())
                    {
                        k++;
                    }
                    else
                    {
                        FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
                        w = i;
                        k = 1;
                    }
                }
                FpSpread1.ActiveSheetView.AddSpanCell(w, 0, k, 1);
                //重新写序号
                k = 1;
                w = hcount;
                for (int i = w; i < FpSpread1.Sheets[0].Rows.Count; i++)
                {
                    if (FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() != "油合计" && FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() != "油总计" && FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() != "气合计" && FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim() != "气总计")
                    {
                        FpSpread1.Sheets[0].Cells[i, 2].Value = k;
                        FpSpread1.Sheets[0].Cells[i, 2].HorizontalAlign = HorizontalAlign.Center;
                        k++;
                        FpSpread1.Sheets[0].Cells[i, 1].Font.Size = 9;
                    }
                    else if (FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim().Contains("合计"))
                    {
                        FpSpread1.ActiveSheetView.AddSpanCell(i, 2, 1, 3);
                        FpSpread1.Sheets[0].Cells[i, 2].Value = FpSpread1.Sheets[0].Cells[i, 3].Value;
                        FpSpread1.Sheets[0].Cells[i, 2].HorizontalAlign = HorizontalAlign.Center;
                    }
                    else if (FpSpread1.Sheets[0].Cells[i, 3].Value.ToString().Trim().Contains("总计"))
                    {
                        FpSpread1.ActiveSheetView.AddSpanCell(i, 1, 1, 3);
                        FpSpread1.Sheets[0].Cells[i, 2].Value = FpSpread1.Sheets[0].Cells[i, 3].Value;
                        FpSpread1.Sheets[0].Cells[i, 2].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
                if (typeid == "pjdy")
                {
                    FpSpread1.Sheets[0].Cells[0, 2].Text = FpSpread1.Sheets[0].Cells[0, 2].Text.Replace("区块", "评价单元");
                    FpSpread1.Sheets[0].Cells[2, 3].Text = FpSpread1.Sheets[0].Cells[2, 3].Text.Replace("区块", "评价单元");
                }
                else if (typeid == "qk")
                {
                    FpSpread1.Sheets[0].Cells[0, 2].Text = FpSpread1.Sheets[0].Cells[0, 2].Text.Replace("评价单元", "区块");
                    FpSpread1.Sheets[0].Cells[2, 3].Text = FpSpread1.Sheets[0].Cells[2, 3].Text.Replace("评价单元", "区块");
                }
                int width = 0;
                for (int Col = 0; Col < FpSpread1.Sheets[0].Columns.Count; Col++)
                {
                    width += FpSpread1.Sheets[0].Columns[Col].Width;
                }

                FpSpread1.Width = Unit.Pixel(width + 300);

            }
            catch (OracleException error)
            {
                string CuoWu = "错误: " + error.Message.ToString();
                Response.Write(CuoWu);

            }

            //connfp.Close();
        }

        protected int isempty()
        {
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");

            SqlHelper conn = new SqlHelper();
            OracleConnection con1 = conn.GetConn();
            con1.Open();
            string ss = "select bny,eny from jdstat_djsj where bny='" + bny + "' and eny='" + eny + "'";
            OracleDataAdapter da = new OracleDataAdapter(ss, con1);
            DataSet ds = new DataSet();
            da.Fill(ds, "time");
            con1.Close();
            if (ds.Tables["time"].Rows.Count == 0)
                return 0;
            else
                return 1;

        }



    }
}
