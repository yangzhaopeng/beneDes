using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;
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

namespace beneDesYGS.view.stockAssessment.gsgfpjBiaoQC
{
    public partial class xylbqjbQC : beneDesYGS.core.UI.corePage
    {
        public static int page = 1;
        static int sumPages = 0;
        static DataSet fpset = new DataSet();
        static string selectItems = string.Empty;
        static string selectedStrs = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string _type = _getParam("_type");
            switch (_type)
            {
                case "querySelect":
                    QS_Click();
                    break;
                case "selectedItem":
                    isSelected();
                    break;
                case "getPages":
                    getPages();
                    break;
            }

            Fp_DataBound(page);
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

        /// <summary>
        /// 修改设置需要查询的字段
        /// </summary>
        protected void QS_Click()
        {
            //前台传入的已选和为选字段的0,1字符串
            int i = 0;
            string queryItems = _getParam("querySelect");
            if (queryItems == null)
            {
                return;
            }

            string[] strs = queryItems.Split(new char[] { ',' });

            //修改XML文件中的数据
            XmlDocument xmlDoc = new XmlDocument();
            string Path = Server.MapPath("/beneDesYGS/gfgspjbQC.xml");
            xmlDoc.Load(Path);
            XmlNodeList nodelist = xmlDoc.SelectSingleNode("tableAttributes").ChildNodes;
            foreach (XmlNode xn in nodelist)
            {
                XmlElement xe = (XmlElement)xn;
                string test = xe.GetAttribute("checked").ToString();
                if (strs[i] != xe.GetAttribute("checked").ToString())
                {
                    xe.SetAttribute("checked", strs[i]);
                    xmlDoc.Save(Path);
                }
                i = i + 1;
            }
            _return(true, "已保存设置！", "null");
        }

        protected void isSelected()
        {
            string isSelectedStrs = string.Empty; //描述是否选中的0，1字符串
            DataSet selectDs = new DataSet();
            selectDs.ReadXml(Server.MapPath("/beneDesYGS/gfgspjbQC.xml"));
            DataTable selectDt = selectDs.Tables[0];
            for (int i = 0; i < selectDt.Rows.Count; i++)
            {
                if (selectDt.Rows[i]["checked"].ToString() == "1")
                {
                    isSelectedStrs += "1,";
                }
                else
                {
                    isSelectedStrs += "0,";
                }
            }
            isSelectedStrs = isSelectedStrs.Substring(0, isSelectedStrs.Length - 1);
            _return(true, isSelectedStrs, "null");
        }

        /// <summary>
        /// 得到总页数和当前页数并返回
        /// </summary>
        protected void getPages()
        {
            string result = page + "," + sumPages;
            _return(true, result, "null");
        }

        protected void DC_Click(object sender, EventArgs e)
        {
            string path = "../../../static/excel/jdnianduQC.xls";
            path = Page.MapPath(path);
            this.FpSpread2.Sheets[0].OpenExcel(path, "表35-气井效益类别全表");

                FpSpread2.Sheets[0].ColumnHeader.Visible = false;
                FpSpread2.Sheets[0].RowHeader.Visible = false;
                
                    //把数据先放入FpSpread2
                    int rcount = fpset.Tables[0].Rows.Count;
                    int ccount = fpset.Tables[0].Columns.Count - 1;
                    int hcount = 3;

                    FpSpread2.Sheets[0].AddRows(hcount, rcount);
                    FpSpread2.Sheets[0].AddColumns(hcount, ccount - 6);
                    //动态生成表头                  
                    string[] sstrs = selectedStrs.Split(new char[] { ',' }); //中文字段的字符串数组
                    if (sstrs.Length > 3)
                    {//当大于3个字段时才动态添加
                        for (int i = 0; i < ccount - hcount; i++)
                        {
                            FpSpread2.Sheets[0].Cells[hcount - 1, i + hcount].Value = sstrs[i + 3];
                            FpSpread2.Sheets[0].Columns[i + hcount].Width = 100;
                            FpSpread2.Sheets[0].Cells[hcount - 1, i + hcount].HorizontalAlign = HorizontalAlign.Center;
                            FpSpread2.Sheets[0].Cells[hcount - 1, i + hcount].VerticalAlign = VerticalAlign.Middle;
                            FpSpread2.Sheets[0].Cells[hcount - 1, i + hcount].Font.Name = "微软雅黑";
                            FpSpread2.Sheets[0].Cells[hcount - 1, i + hcount].Font.Size = 10;
                        }
                    }

                    for (int i = 0; i < rcount; i++)
                    {
                        for (int j = 0; j < ccount; j++)
                        {
                            FpSpread2.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables[0].Rows[i][j].ToString();
                            FpSpread2.Sheets[0].Cells[i + hcount, j].HorizontalAlign = HorizontalAlign.Center;
                            FpSpread2.Sheets[0].Cells[i + hcount, j].VerticalAlign = VerticalAlign.Middle;
                            FpSpread2.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                        }
                    }
                    FpSpread2.ActiveSheetView.AddSpanCell(0, 0, 1, ccount);
                //导出FpSpread2中的数据
                FpSpread2.Sheets[0].RowHeader.Visible = true;
                FpSpread2.Sheets[0].ColumnHeader.Visible = true;
                //FpSpread2.SaveExcelToResponse("biao5.xls");
                FarpointGridChange.FarPointChange(FpSpread2, "jdnianduQC.xls");
                FpSpread2.Sheets[0].RowHeader.Visible = false;
                FpSpread2.Sheets[0].ColumnHeader.Visible = false;          
        }

        protected void QIAN_Click(object sender, EventArgs e)
        {

            //int.TryParse(this.currentpage.Text, out page);
            page = page - 1;
            if (page >= 1)
            {
                Fp_DataBound(page);
                //this.currentpage.Text = page.ToString();
            }
            else
            {
                page = 1;
            }

        }
        protected void HOU_Click(object sender, EventArgs e)
        {

            //int.TryParse(this.currentpage.Text, out page);
            //if (page >= 1)
            //{ page = 0; }
            if (page >= sumPages)
            {
                page = sumPages;
            }
            else
            {
                page = page + 1;
            }
            Fp_DataBound(page);
            //this.currentpage.Text = page.ToString();
        }

        protected void Fp_DataBound(int page)
        {
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            string cyc = _getParam("CYC");
            //if (page == 0)
            //ButnBack.Enabled = false;
            //else
            //ButnBack.Enabled = true;
            try
            {

                string path = "../../../static/excel/jdnianduQC.xls";
                path = Page.MapPath(path);
                this.FpSpread1.Sheets[0].OpenExcel(path, "表35-气井效益类别全表");
        
                FpSpread1.Sheets[0].ColumnHeader.Visible = false;
                FpSpread1.Sheets[0].RowHeader.Visible = false;

                OracleConnection connfp = new OracleConnection(ConfigurationManager.ConnectionStrings["ygsConnectionString"].ConnectionString);
                connfp.Open();
                selectItems = string.Empty;
                selectedStrs = string.Empty;
     
                //从XML文件中读取已勾选的要查询字段
                DataSet selectDs = new DataSet();
                selectDs.ReadXml(Server.MapPath("/beneDesYGS/gfgspjbQC.xml"));
                DataTable selectDt = selectDs.Tables[0];
                for (int i = 0; i < selectDt.Rows.Count; i++)
                {
                    if (selectDt.Rows[i]["checked"].ToString() == "1")
                    {
                        selectItems += selectDt.Rows[i]["ename"].ToString() + ','; //英文字段
                        selectedStrs += selectDt.Rows[i]["cname"].ToString() + ','; //中文字段
                    }
                }
                if (selectItems.Length != 0 && selectedStrs.Length != 0)
                {
                    selectItems = selectItems.Substring(0, selectItems.Length - 1);//英文字段
                    selectedStrs = selectedStrs.Substring(0, selectedStrs.Length - 1);//中文字段
                }
                OracleDataAdapter da;
                fpset = new DataSet();
                selectItems = selectItems.Replace("GSXYJB", "XYMC") + ",GSXYJB"; 
                string fql = @"select {0} from jdstat_djsj sdy, gsxylb_info xy, department d where regexp_like(sdy.dep_id,'{1}') and sdy.dep_id=d.dep_id and d.dep_type='CQC' and sdy.djisopen='1' and sdy.gsxyjb=xy.xyjb order by gsxyjb ";
                fql = string.Format(fql, selectItems, cyc);

                da = new OracleDataAdapter(fql, connfp);
                da.Fill(fpset, "0");

                //此处用于绑定数据            
                #region

                int rcount = fpset.Tables[0].Rows.Count;
                int ccount = fpset.Tables[0].Columns.Count - 1;
                int hcount = 3;

                //得到总页数
                if (rcount % 80 == 0)
                {//余数为0时
                    sumPages = rcount / 80;
                }
                else
                {//余数不为0时
                    sumPages = rcount / 80 + 1;
                }

                int count = 80;
                int pagenumber = 80;
                if (rcount < pagenumber)
                    count = rcount;
                if (rcount - page * pagenumber < 80)
                {
                    count = rcount - page * pagenumber;
                    //ButnForward.Enabled = false;
                }

                FpSpread1.Sheets[0].AddRows(hcount, count);
                FpSpread1.Sheets[0].AddColumns(hcount, ccount - 3);
                //动态生成表头                  
                string[] sstrs = selectedStrs.Split(new char[] { ',' }); //中文字段的字符串数组
                if (sstrs.Length > 3)
                {//当大于3个字段时才动态添加
                    for (int i = 0; i < ccount - hcount; i++)
                    {
                        FpSpread1.Sheets[0].Cells[hcount - 1, i + hcount].Value = sstrs[i + 3];
                        FpSpread1.Sheets[0].Columns[i + hcount].Width = 100;
                        FpSpread1.Sheets[0].Cells[hcount - 1, i + hcount].HorizontalAlign = HorizontalAlign.Center;
                        FpSpread1.Sheets[0].Cells[hcount - 1, i + hcount].VerticalAlign = VerticalAlign.Middle;
                        FpSpread1.Sheets[0].Cells[hcount - 1, i + hcount].Font.Name = "微软雅黑";
                        FpSpread1.Sheets[0].Cells[hcount - 1, i + hcount].Font.Size = 10;
                    }
                }

                for (int i = 0; i < count; i++)
                {
                    for (int j = 0; j < ccount; j++)
                    {
                        FpSpread1.Sheets[0].Cells[i + hcount, j].Value = fpset.Tables[0].Rows[(page-1) * 80 + i][j].ToString();
                        FpSpread1.Sheets[0].Cells[i + hcount, j].HorizontalAlign = HorizontalAlign.Center;
                        FpSpread1.Sheets[0].Cells[i + hcount, j].VerticalAlign = VerticalAlign.Middle;
                        FpSpread1.Sheets[0].Cells[i + hcount, j].Font.Size = 9;
                    }
                }

                FpSpread1.ActiveSheetView.AddSpanCell(0, 0, 1, ccount);
                //FpSpread1.Sheets[0].FrozenColumnCount = 3;
                da.Dispose();
                #endregion

            }
            catch (OracleException error)
            {
                string CuoWu = "错误: " + error.Message.ToString();
                Response.Write(CuoWu);
            }

        }
    }

    
}
