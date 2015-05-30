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
using System.Drawing;
using System.Windows.Forms;

namespace beneDesCYC.view.oilAssessment
{
    public partial class jb : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //SqlHelper conn = new SqlHelper();
                //OracleConnection con0 = conn.GetConn();
                //con0.Open();
                //string tgse = "select num from csdy_info where name = '特高成本井' and ny = '" + Session["month"].ToString() + "'";
                //OracleCommand tgcom = new OracleCommand(tgse, con0);
                //OracleDataReader tgr = tgcom.ExecuteReader();
                //try
                //{
                //    //tgr.Read();
                //    //if (tgr.HasRows)

                //    if (tgr.Read() || !string.IsNullOrEmpty(_getParam("tgch")))
                //    {
                //        Tbtgcb.Text = tgr[0].ToString();
                //        Tbtgcb.Enabled = false;
                //        Tbtgcb.Visible = false;
                //        Lbcb.Visible = false;
                //    }
                //    else
                //    {
                //        Response.Write("<script>alert('油公司没有设置本月特高成本井数据！请在本页设置!')</script>");
                //        //return;
                //        Tbtgcb.Enabled = true;
                //        Tbtgcb.Focus();
                //        Tbtgcb.Visible = true;
                //        Lbcb.Visible = true;
                //    }
                //    tgr.Close();
                //    con0.Close();
                //}
                //catch (OracleException err)
                //{
                //    Response.Write(err);
                //}

                string list = _getParam("CYC");
                if (list == "null")
                {
                    Label1.Visible = false;
                    HyperLink1.Visible = false;
                    return;
                }
                else
                {
                    Label1.Visible = false;
                    confirmClick();
                }
            }

        }
        protected void paixu(double[,] aa, int n)
        {
            //aa[，] n为数组大小 
            //本函数实现12*3的二维数组排序,第一列存名称
            double tem0, tem1, tem2;
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    if (aa[i, 1] < aa[j, 1])
                    {
                        tem0 = aa[i, 0];
                        tem1 = aa[i, 1];
                        tem2 = aa[i, 2];
                        aa[i, 0] = aa[j, 0];
                        aa[i, 1] = aa[j, 1];
                        aa[i, 2] = aa[j, 2];
                        aa[j, 0] = tem0;
                        aa[j, 1] = tem1;
                        aa[j, 2] = tem2;
                    }
                }
            }

        }

        protected void confirmClick()
        {
            string cycid = Session["cyc_id"].ToString();
            string bny = _getParam("startMonth");
            string eny = _getParam("endMonth");
            string list = _getParam("CYC");
            string lbny = _getParam("lstartMonth");
            string leny = _getParam("lendMonth");
            string tgcb = _getParam("tgcb");

            int n = 342;  //变量个数,共241个变量
            int m = 109;
            string[] p = new string[342];
            string[] cb = new string[109];
            for (int i = 0; i < n; i++)
            {
                p[i] = "0";
            }
            for (int j = 0; j < m; j++)
            {
                cb[j] = "0";
            }

            try
            {
                //建立连接
                SqlHelper conn = new SqlHelper();
                OracleConnection con0 = conn.GetConn();
                //OracleConnection con0 = DB.CreatConnection();
                con0.Open();
                string cycn = "";
                string sql = "select d.dep_name from department d where d.dep_id = '" + cycid + "'";
                OracleCommand comcyc = new OracleCommand(sql, con0);
                OracleDataReader drcyc = comcyc.ExecuteReader();
                //drcyc.Read();
                //if (drcyc.HasRows)
                if (drcyc.Read())
                {
                    cycn = drcyc[0].ToString();
                }
                drcyc.Close();

                //以下给变量赋值
                //p[0]---p[100]不用
                //取评价年、月
                string pjn = eny.Substring(0, 4);
                string ksny = "0";
                string jsny = "0";
                string pjbny = "0";
                string pjeny = "0";
                string bctime = "";
                string sctime = "";
                string qjtime = bny + '-' + eny;
                pjbny = bny.Substring(4, 2);
                pjeny = eny.Substring(4, 2);
                bctime = pjn + '年' + pjbny + '-' + pjeny + '月';
                sctime = leny.Substring(0, 4) + '年' + lbny.Substring(4, 2) + '-' + leny.Substring(4, 2) + '月';


                //String stny = "select bny, eny from DTSTAT_DJSJ  ";
                //OracleCommand comm0 = new OracleCommand(stny, con0);
                //OracleDataReader dr = comm0.ExecuteReader();
                //dr.Read();            
                //if (dr.HasRows)
                //{
                //    ksny = dr[0].ToString();
                //    jsny = dr[1].ToString();
                //    pjbny = ksny.Substring(4, 2);
                //    pjeny = jsny.Substring(4, 2);
                //}
                //dr.Close();
                //取评价年
                p[101] = pjn;
                //取评价年
                p[102] = pjn;
                //取评价最终年月
                p[103] = pjeny;

                //取当前日期
                DateTime ctime = System.DateTime.Now;
                p[104] = ctime.ToString("d");
                //取评价年
                p[105] = pjn;
                //取评价起始年月
                p[106] = pjbny + "-" + pjeny;
                ///////////////////////////---------以上没变---------/////////////////////////////

                //一、评价参数 p[107]-p[122]
                #region

                //取评价年
                p[107] = pjn;
                p[110] = pjn;
                p[116] = pjn;
                p[119] = pjn;
                //取评价起始年月
                p[108] = pjbny + "-" + pjeny;
                p[111] = pjbny + "-" + pjeny;
                p[113] = pjbny + "-" + pjeny;
                p[117] = pjbny + "-" + pjeny;
                p[120] = pjbny + "-" + pjeny;
                //加权平均油价  p[109]
                //油价商品率    p[112]
                //单位税金      p[114]
                //石油特别收益金p[115]
                //天然气平均价格p[118]
                //天然气商品率  p[121]
                //天然气单位税金p[122]
                /////////////////////////////

                string fpselect = " Select round((Case When Sum(hscyl) = 0 Then 0 Else Sum(yyspl)/sum(hscyl) End )*100,2) As yyspl, ";
                fpselect += " round((Case When Sum(yyspl) = 0 Then 0 Else Sum(yyxssr)/Sum(yyspl) End),2) As yyjg, ";
                fpselect += " round((Case When Sum(yyspl) = 0 Then 0 Else Sum(yyxssj)/Sum(yyspl) End),2) As yysj, ";
                fpselect += " round((Case When Sum(hscql) = 0 Then 0 Else Sum(trqspl)/sum(hscql) End )*100,2) As trqspl, ";
                fpselect += " round((Case When Sum(trqspl) = 0 Then 0 Else Sum(trqxssr)/Sum(trqspl) End),2) As trqjg, ";
                fpselect += " round((Case When Sum(trqspl) = 0 Then 0 Else Sum(trqxssj)/Sum(trqspl) End),2) As trqsj ";
                fpselect += " From dtstat_djsj  ";
                fpselect += " Where   dtstat_djsj.cyc_id = '" + cycid + "' ";

                /////////////////////////////

                OracleCommand comm1 = new OracleCommand(fpselect, con0);
                OracleDataReader dr1 = comm1.ExecuteReader();
                //dr1.Read();
                //if (dr1.HasRows)
                if (dr1.Read())
                {
                    //加权平均油价  p[109]
                    //油价商品率    p[112]
                    //单位税金      p[114]
                    //石油特别收益金p[115]
                    //天然气平均价格p[118]
                    //天然气商品率  p[121]
                    //天然气单位税金p[122]
                    p[109] = dr1[1].ToString();
                    p[112] = dr1[0].ToString();
                    //p[115] = dr1[].ToString();
                    p[114] = dr1[2].ToString();
                    p[118] = dr1[4].ToString();
                    p[121] = dr1[3].ToString();
                    p[122] = dr1[5].ToString();

                }
                dr1.Close();

                //石油特别收益金p[115]
                Int64 ny = (Convert.ToInt64(eny)) - (Convert.ToInt64(bny)) + 1;
                string tbsyj = " select round(sum(tbsyj_info.tbsyj)/" + ny + ",2) from tbsyj_info where tbsyj_info.ny>='" + bny + "' and tbsyj_info.ny<='" + eny + "' ";

                comm1 = new OracleCommand(tbsyj, con0);
                dr1 = comm1.ExecuteReader();
                //dr1.Read();//@yzp注
                //if (dr1.HasRows)
                if (dr1.Read())
                {
                    p[115] = dr1[0].ToString();
                }
                dr1.Close();


                #endregion

                //二、评价范围 p[123]-p[168]
                #region
                //取评价年
                p[123] = pjn;
                //取评价起始年月
                p[124] = pjeny;
                //string spjfw = " select * from view_dtjianbao_2 ";
                string spjfw = "";
                spjfw += " select  count(distinct sdy.dj_id) as js, ";
                spjfw += " round(sum(nvl(sdy.hscyl,0))/10000,4) as hscyl, ";
                spjfw += " round(sum(nvl(sdy.yyspl,0))/10000,4) as yyspl, ";
                spjfw += " (case when sum(nvl(sdy.hscyl,0)) = 0 then 0  else   round(sum(nvl(sdy.yyspl,0))/sum(nvl(sdy.hscyl,0))*100,2) end)as spl, ";   //商品率
                spjfw += " round(sum(sdy.hscyl)/10000,4) as yycl  ";
                spjfw += " from dtstat_djsj sdy ";
                spjfw += " where sdy.djisopen = '1' and  sdy.cyc_id =  '" + cycid + "'  ";


                OracleDataAdapter da = new OracleDataAdapter(spjfw, con0);
                DataTable dtt = new DataTable("pjfw");
                da.Fill(dtt);
                if (dtt.Rows.Count > 0)
                {
                    p[126] = dtt.Rows[0][0].ToString();
                    p[125] = dtt.Rows[0][1].ToString();
                    p[128] = dtt.Rows[0][2].ToString();
                    p[127] = dtt.Rows[0][4].ToString();
                }
                ////////////////////新添加的总井数p[326] ，开井数p[327]
                string sqlyjjs = " select sum(nvl(sdy.yjzjs,0)), sum(nvl(sdy.yjkjs,0)) from dtstat_qksj sdy where sdy.cyc_id = '" + cycid + "' ";
                OracleCommand commyjjs = new OracleCommand(sqlyjjs, con0);
                OracleDataReader dryjjs = commyjjs.ExecuteReader();
                //dryjjs.Read();
                //if (dryjjs.HasRows)
                if (dryjjs.Read())
                {
                    p[326] = dryjjs[0].ToString();
                    p[327] = dryjjs[1].ToString();
                }
                dryjjs.Close();
                ////////////////////////////////////////////////////////////

                #endregion

                //三、评价结果
                #region
                //三、评价结果总述
                #region
                //取评价年
                p[169] = pjn;
                //取当前月
                p[170] = pjeny;
                //p[171]---p[177]从表1中取  
                //string spj = "select * from view_dtbiao1 ";
                string spj = "";
                //@yzp  统一 添加 end （case..when..then..else..end）
                spj += " Select     round(case  when sum(czcb) is null then 0 else Sum(czcb)/10000 end,4) As czcb , ";
                spj += " round(case  when sum(zjclf) is null then 0 else Sum(zjclf)/10000 end,4) As zjclf,  ";
                spj += " round(case  when sum(zjclf) is null then 0 else sum(zjclf)/sum(czcb)*100 end,2) bl1, ";
                spj += " round(case  when sum(zjrlf) is null then 0 else Sum(zjrlf   )/10000 end,4) As zjrlf,  ";
                spj += " round(case  when sum(zjrlf) is null then 0 else sum(zjrlf)/sum(czcb)*100 end,2) bl2, ";
                spj += " round(case  when sum(zjrlf) is null then 0 else Sum(zjdlf   )/10000 end,4) As zjdlf,  ";
                spj += " round(case  when sum(zjrlf) is null then 0 else sum(zjdlf)/sum(czcb)*100 end,2) bl3, ";
                spj += "  round(case  when sum(qywzrf) is null then 0 else Sum(qywzrf  )/10000 end,4) As qywzrf, ";
                spj += " round(case  when sum(qywzrf) is null then 0 else sum(qywzrf)/sum(czcb)*100 end,2) bl4, ";
                spj += " round(case  when sum(jxzyf) is null then 0 else Sum(jxzyf   )/10000 end,4) As jxzyf,  ";
                spj += " round(case  when sum(jxzyf) is null then 0 else sum(jxzyf)/sum(czcb)*100 end,2) bl5, ";
                spj += " round(case  when sum(cjsjf) is null then 0 else Sum(cjsjf   )/10000 end,4) As cjsjf,  ";
                spj += " round(case  when sum(cjsjf) is null then 0 else sum(cjsjf)/sum(czcb)*100 end,2) bl6, ";
                spj += " round(case  when sum(whxlf) is null then 0 else Sum(whxlf   )/10000 end,4) As whxlf,  ";
                spj += " round(case  when sum(whxlf) is null then 0 else sum(whxlf)/sum(czcb)*100 end,2) bl7, ";
                spj += " round(case  when sum(yqclf) is null then 0 else Sum(yqclf   )/10000 end,4) As yqclf,  ";
                spj += " round(case  when sum(yqclf) is null then 0 else sum(yqclf)/sum(czcb)*100 end,2) bl8, ";
                spj += " round(case  when sum(ysf) is null then 0 else Sum(ysf     )/10000 end,4) As ysf,  ";
                spj += " round(case  when sum(ysf) is null then 0 else sum(ysf)/sum(czcb)*100 end,2) bl9,";
                spj += " round(case  when sum(qtzjf) is null then 0 else Sum(qtzjf   )/10000 end,4) As qtzjf, ";
                spj += " round(case  when sum(qtzjf) is null then 0 else sum(qtzjf)/sum(czcb)*100 end,2) bl10 ";
                //spj += " round(Sum(ckglf   )/10000,4) As ckglf,  ";
                //spj += " round(sum(ckglf)/sum(czcb)*100,2) bl11, ";
                //spj += " round(Sum(zjryf   )/10000,4) As zjryf,  ";
                //spj += " round(sum(zjryf)/sum(czcb)*100,2) bl12";
                spj += " From  dtstat_djsj sdy ";
                spj += " where sdy.djisopen = '1 '  and sdy.cyc_id = '" + cycid + "' ";


                comm1 = new OracleCommand(spj, con0);
                dr1 = comm1.ExecuteReader();
                //dr1.Read();
                //if (dr1.HasRows)
                if (dr1.Read())
                {
                    double[,] zczcb = new double[10, 3];
                    zczcb[0, 0] = 1;   //直接材料费
                    zczcb[0, 1] = double.Parse(dr1[1].ToString());
                    zczcb[0, 2] = double.Parse(dr1[2].ToString());
                                       
                    zczcb[1, 0] = 2;   //直接燃料费
                    zczcb[1, 1] = double.Parse(dr1[3].ToString());
                    zczcb[1, 2] = double.Parse(dr1[4].ToString());

                    zczcb[2, 0] = 3;   //直接动力费
                    zczcb[2, 1] = double.Parse(dr1[5].ToString());
                    zczcb[2, 2] = double.Parse(dr1[6].ToString());

                    zczcb[3, 0] = 4;   //驱油物注入费
                    zczcb[3, 1] = double.Parse(dr1[7].ToString());
                    zczcb[3, 2] = double.Parse(dr1[8].ToString());

                    zczcb[4, 0] = 5;   //井下作业费
                    zczcb[4, 1] = double.Parse(dr1[9].ToString());
                    zczcb[4, 2] = double.Parse(dr1[10].ToString());

                    zczcb[5, 0] = 6;   //测井试井费
                    zczcb[5, 1] = double.Parse(dr1[11].ToString());
                    zczcb[5, 2] = double.Parse(dr1[12].ToString());

                    zczcb[6, 0] = 7;   //维护及修理费
                    zczcb[6, 1] = double.Parse(dr1[13].ToString());
                    zczcb[6, 2] = double.Parse(dr1[14].ToString());

                    zczcb[7, 0] = 8;   //油气处理
                    zczcb[7, 1] = double.Parse(dr1[15].ToString());
                    zczcb[7, 2] = double.Parse(dr1[16].ToString());

                    zczcb[8, 0] = 9;   //运输费
                    zczcb[8, 1] = double.Parse(dr1[17].ToString());
                    zczcb[8, 2] = double.Parse(dr1[18].ToString());

                    zczcb[9, 0] = 10;   //其它直接费
                    zczcb[9, 1] = double.Parse(dr1[19].ToString());
                    zczcb[9, 2] = double.Parse(dr1[20].ToString());
                    //不包括以下费用
                    //zczcb[10, 0] = 11;   //厂矿管理费
                    //zczcb[10, 1] = hj[20];
                    //zczcb[10, 2] = hj[21];

                    //zczcb[11, 0] = 12;   //直接人员费用
                    //zczcb[11, 1] = hj[22];
                    //zczcb[11, 2] = hj[23];
                    paixu(zczcb, 10);
                    p[171] = dr1[0].ToString();  //累计发生操作成本
                    p[172] = zczcb[0, 1].ToString("0.0000");
                    p[173] = zczcb[0, 2].ToString("0.00");
                    p[174] = zczcb[1, 1].ToString("0.0000");
                    p[175] = zczcb[1, 2].ToString("0.00");
                    p[176] = zczcb[2, 1].ToString("0.0000");
                    p[177] = zczcb[2, 2].ToString("0.00");

                    switch (int.Parse(zczcb[0, 0].ToString()))
                    {
                        case 1: cb[101] = "直接材料费"; break;
                        case 2: cb[101] = "直接燃料费"; break;
                        case 3: cb[101] = "直接动力费"; break;
                        case 4: cb[101] = "驱油物注入费"; break;
                        case 5: cb[101] = "井下作业费"; break;
                        case 6: cb[101] = "测井试井费"; break;
                        case 7: cb[101] = "维护及修理费"; break;
                        case 8: cb[101] = "油气处理"; break;
                        case 9: cb[101] = "运输费"; break;
                        case 10: cb[101] = "其它直接费"; break;
                        default: break;
                    }
                    switch (int.Parse(zczcb[1, 0].ToString()))
                    {
                        case 1: cb[102] = "直接材料费"; break;
                        case 2: cb[102] = "直接燃料费"; break;
                        case 3: cb[102] = "直接动力费"; break;
                        case 4: cb[102] = "驱油物注入费"; break;
                        case 5: cb[102] = "井下作业费"; break;
                        case 6: cb[102] = "测井试井费"; break;
                        case 7: cb[102] = "维护及修理费"; break;
                        case 8: cb[102] = "油气处理"; break;
                        case 9: cb[102] = "运输费"; break;
                        case 10: cb[102] = "其它直接费"; break;
                        default: break;
                    }
                    switch (int.Parse(zczcb[2, 0].ToString()))
                    {
                        case 1: cb[103] = "直接材料费"; break;
                        case 2: cb[103] = "直接燃料费"; break;
                        case 3: cb[103] = "直接动力费"; break;
                        case 4: cb[103] = "驱油物注入费"; break;
                        case 5: cb[103] = "井下作业费"; break;
                        case 6: cb[103] = "测井试井费"; break;
                        case 7: cb[103] = "维护及修理费"; break;
                        case 8: cb[103] = "油气处理"; break;
                        case 9: cb[103] = "运输费"; break;
                        case 10: cb[103] = "其它直接费"; break;
                        default: break;
                    }

                }
                dr1.Close();

                #endregion
                //三、评价结果（-）单井效益状况
                #region
                //p[178]---p[216] 比例最后再算
                //总的数据
                spj = " select nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb)/Sum(yqspl) End),2),0) As dwczcb,";
                spj += " nvl(round(Sum(zjyxczcb)/10000,4),0) As zjyxczcb, ";
                spj += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjyxczcb)/Sum(yqspl) End),2),0) As dwzjyxczcb";
                spj += " from dtstat_djsj sdy where sdy.djisopen = '1'  and sdy.cyc_id = '" + cycid + "' ";
                OracleDataAdapter daz = new OracleDataAdapter(spj, con0);
                DataTable dtz = new DataTable();
                daz.Fill(dtz);
                if (dtz.Rows.Count > 0)
                {
                    p[178] = dtz.Rows[0][0].ToString();
                    p[179] = dtz.Rows[0][1].ToString();
                    p[180] = dtz.Rows[0][2].ToString();
                }
                //高效益井

                spj = "";
                spj += " select sdy.gsxyjb_1, ";
                spj += " (case when sum(djisopen) = 0 then 0 else sum(djisopen) end) As js,  ";
                spj += " 0 as jsbl, ";
                spj += " nvl(round((case when Sum(hscyl) = 0 then 0 else round(Sum(hscyl)/10000,4) end ),4),0) As cyl,  ";
                spj += " nvl(round((case when sum(yyspl) = 0 then 0 else Sum(yyspl)/10000 end ),4),0) as yyspl,  ";
                spj += " nvl(round(Sum(zjyxczcb)/10000,4),0) As zjyxczcb,  ";
                spj += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjyxczcb)/Sum(yqspl) End),2),0) As dwzjyxczcb, ";
                spj += " nvl(round(Sum(czcb)/10000,4),0) As czcb , ";
                spj += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb)/Sum(yqspl) End),2),0) As dwczcb ";
                spj += " from dtstat_djsj sdy  ";
                spj += " where sdy.djisopen = '1' and sdy.gsxyjb_1 = '1' and sdy.cyc_id = '" + cycid + "' ";
                spj += " group by sdy.gsxyjb_1 ";

                OracleDataAdapter da3 = new OracleDataAdapter(spj, con0);
                DataTable dt3 = new DataTable("gxy");
                da3.Fill(dt3);
                if (dt3.Rows.Count > 0)
                {
                    p[181] = dt3.Rows[0][1].ToString();
                    p[182] = dt3.Rows[0][2].ToString();
                    p[183] = dt3.Rows[0][3].ToString();
                    p[184] = dt3.Rows[0][4].ToString();
                    p[185] = dt3.Rows[0][5].ToString();
                    p[186] = dt3.Rows[0][6].ToString();
                    p[187] = dt3.Rows[0][7].ToString();
                    p[188] = dt3.Rows[0][8].ToString();
                }
                //低效益井
                //spj = " select * from view_dtjianbao_3_1 where gsxyjb_1 = '2'";
                spj = "";
                spj += " select sdy.gsxyjb_1, ";
                spj += " (case when sum(djisopen) = 0 then 0 else sum(djisopen) end) As js,  ";
                spj += " 0 as jsbl, ";
                spj += " nvl(round((case when Sum(hscyl) = 0 then 0 else Sum(hscyl)/10000 end ),4),0) As cyl,  ";
                spj += " nvl(round((case when sum(yyspl) = 0 then 0 else Sum(yyspl)/10000 end ),4),0) as yyspl,  ";
                spj += " nvl(round(Sum(zjyxczcb)/10000,4),0) As zjyxczcb,  ";
                spj += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjyxczcb)/Sum(yqspl) End),2),0) As dwzjyxczcb, ";
                spj += " nvl(round(Sum(czcb)/10000,4),0) As czcb , ";
                spj += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb)/Sum(yqspl) End),2),0) As dwczcb ";
                spj += " from dtstat_djsj sdy  ";
                spj += " where sdy.djisopen = '1' and sdy.gsxyjb_1 = '2' and sdy.cyc_id = '" + cycid + "' ";
                spj += " group by sdy.gsxyjb_1 ";
                da3 = new OracleDataAdapter(spj, con0);
                DataTable dt3d = new DataTable("dxy");
                da3.Fill(dt3d);
                if (dt3d.Rows.Count > 0)
                {
                    p[189] = dt3d.Rows[0][1].ToString();
                    p[190] = dt3d.Rows[0][2].ToString();
                    p[191] = dt3d.Rows[0][3].ToString();
                    p[192] = dt3d.Rows[0][4].ToString();
                    p[193] = dt3d.Rows[0][5].ToString();
                    p[194] = dt3d.Rows[0][6].ToString();
                    p[195] = dt3d.Rows[0][7].ToString();
                    p[196] = dt3d.Rows[0][8].ToString();
                }
                //边际效益井
                //spj = " select * from view_dtjianbao_3_1 where gsxyjb_1 = '3'";
                spj = "";
                spj += " select sdy.gsxyjb_1, ";
                spj += " (case when sum(djisopen) = 0 then 0 else sum(djisopen) end) As js,  ";
                spj += " 0 as jsbl, ";
                spj += " nvl(round((case when Sum(hscyl) = 0 then 0 else Sum(hscyl)/10000 end ),4),0) As cyl,  ";
                spj += " nvl(round((case when sum(yyspl) = 0 then 0 else Sum(yyspl)/10000 end ),4),0) as yyspl,  ";
                spj += " nvl(round(Sum(zjyxczcb)/10000,4),0) As zjyxczcb,  ";
                spj += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjyxczcb)/Sum(yqspl) End),2),0) As dwzjyxczcb, ";
                spj += " nvl(round(Sum(czcb)/10000,4),0) As czcb , ";
                spj += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb)/Sum(yqspl) End),2),0) As dwczcb ";
                spj += " from dtstat_djsj sdy  ";
                spj += " where sdy.djisopen = '1' and sdy.gsxyjb_1 = '3' and sdy.cyc_id = '" + cycid + "' ";
                spj += " group by sdy.gsxyjb_1 ";
                da3 = new OracleDataAdapter(spj, con0);
                DataTable dt3b = new DataTable("bjxy");
                da3.Fill(dt3b);
                if (dt3b.Rows.Count > 0)
                {
                    p[197] = dt3b.Rows[0][1].ToString();
                    p[198] = dt3b.Rows[0][2].ToString();
                    p[199] = dt3b.Rows[0][3].ToString();
                    p[200] = dt3b.Rows[0][4].ToString();
                    p[201] = dt3b.Rows[0][5].ToString();
                    p[202] = dt3b.Rows[0][6].ToString();
                    p[203] = dt3b.Rows[0][7].ToString();
                    p[204] = dt3b.Rows[0][8].ToString();
                }

                p[205] = pjn + '年';
                p[206] = pjbny + '-' + pjeny;
                //无效益井
                //spj = " select * from view_dtjianbao_3_1 where gsxyjb_1 = '4'";
                spj = "";
                spj += " select sdy.gsxyjb_1, ";
                spj += " (case when sum(djisopen) = 0 then 0 else sum(djisopen) end) As js,  ";
                spj += " 0 as jsbl, ";
                spj += " nvl(round((case when Sum(hscyl) = 0 then 0 else Sum(hscyl)/10000 end ),4),0) As cyl,  ";
                spj += " nvl(round((case when sum(yyspl) = 0 then 0 else Sum(yyspl)/10000 end ),4),0) as yyspl,  ";
                spj += " nvl(round(Sum(zjyxczcb)/10000,4),0) As zjyxczcb,  ";
                spj += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(zjyxczcb)/Sum(yqspl) End),2),0) As dwzjyxczcb, ";
                spj += " nvl(round(Sum(czcb)/10000,4),0) As czcb , ";
                spj += " nvl(round((Case When sum(yqspl) = 0 Then 0 Else Sum(czcb)/Sum(yqspl) End),2),0) As dwczcb ";
                spj += " from dtstat_djsj sdy  ";
                spj += " where sdy.djisopen = '1' and sdy.gsxyjb_1 = '4' and sdy.cyc_id = '" + cycid + "' ";
                spj += " group by sdy.gsxyjb_1 ";
                da3 = new OracleDataAdapter(spj, con0);
                DataTable dt3w = new DataTable("wxy");
                da3.Fill(dt3w);
                if (dt3w.Rows.Count > 0)
                {
                    p[207] = dt3w.Rows[0][1].ToString();
                    p[208] = dt3w.Rows[0][2].ToString();
                    p[209] = dt3w.Rows[0][3].ToString();
                    p[210] = dt3w.Rows[0][4].ToString();
                    p[211] = dt3w.Rows[0][5].ToString();
                    p[212] = dt3w.Rows[0][6].ToString();
                    p[213] = dt3w.Rows[0][7].ToString();
                    p[214] = dt3w.Rows[0][8].ToString();
                }

                //计算井口比例

                double jsn = double.Parse(p[181].ToString()) + double.Parse(p[189].ToString()) + double.Parse(p[197].ToString()) + double.Parse(p[207].ToString());
                if (jsn != 0)
                {
                    double bl1 = (double.Parse(p[181].ToString()) / jsn) * 100; bl1 = Math.Round(bl1, 2);
                    double bl2 = (double.Parse(p[189].ToString()) / jsn) * 100; bl2 = Math.Round(bl2, 2);
                    double bl3 = (double.Parse(p[197].ToString()) / jsn) * 100; bl3 = Math.Round(bl3, 2);
                    double bl4 = (double.Parse(p[207].ToString()) / jsn) * 100; bl4 = Math.Round(bl4, 2);

                    p[182] = bl1.ToString();
                    p[190] = bl2.ToString();
                    p[198] = bl3.ToString();
                    p[208] = bl4.ToString();
                }


                #endregion

                //三、评价结果（二）单井直接运行成本状况
                #region
                p[215] = pjn + '年';
                p[216] = pjbny + '-' + pjeny;
                //<200

                spj = "select nvl(sum(tem.djisopen),0) as js ,0 as bl1,round(nvl(sum(tem.hscyl)/10000,0),4) as ljcyq, round(nvl(sum(tem.zjyxczcb)/10000,0),4)  as zjyxczcb ";
                spj += " from  ";
                spj += "(select  nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.zjyxczcb,0) as zjyxczcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.djisopen = '1' and sdy.cyc_id = '" + cycid + "'";
                spj += ") tem ";
                spj += " where tem.dwcb <= '200'";
                da3 = new OracleDataAdapter(spj, con0);
                DataTable dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                {
                    p[217] = dt32.Rows[0][0].ToString();
                    p[218] = dt32.Rows[0][1].ToString();
                    p[219] = dt32.Rows[0][2].ToString();
                    p[220] = dt32.Rows[0][3].ToString();
                }
                // 200--400
                spj = "select nvl(sum(tem.djisopen),0) as js ,0 as bl1,round(nvl(sum(tem.hscyl)/10000,0),4) as ljcyq, round(nvl(sum(tem.zjyxczcb)/10000,0),4)  as zjyxczcb ";
                spj += " from  ";
                spj += "(select  nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.zjyxczcb,0) as zjyxczcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.djisopen = '1'  and sdy.cyc_id = '" + cycid + "' ";
                spj += ") tem ";
                spj += " where tem.dwcb < '400'and tem.dwcb >= '200'";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                {
                    p[221] = dt32.Rows[0][0].ToString();
                    p[222] = dt32.Rows[0][1].ToString();
                    p[223] = dt32.Rows[0][2].ToString();
                    p[224] = dt32.Rows[0][3].ToString();
                }

                //400--600
                spj = "select nvl(sum(tem.djisopen),0) as js ,0 as bl1,round(nvl(sum(tem.hscyl)/10000,0),4) as ljcyq, round(nvl(sum(tem.zjyxczcb)/10000,0),4)  as zjyxczcb ";
                spj += " from  ";
                spj += "(select  nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.zjyxczcb,0) as zjyxczcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.djisopen = '1'  and sdy.cyc_id = '" + cycid + "' ";
                spj += ") tem ";
                spj += " where tem.dwcb < '600'and tem.dwcb >= '400'";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                {
                    p[225] = dt32.Rows[0][0].ToString();
                    p[226] = dt32.Rows[0][1].ToString();
                    p[227] = dt32.Rows[0][2].ToString();
                    p[228] = dt32.Rows[0][3].ToString();
                }
                //600---850
                spj = "select nvl(sum(tem.djisopen),0) as js ,0 as bl1,round(nvl(sum(tem.hscyl)/10000,0),4) as ljcyq, round(nvl(sum(tem.zjyxczcb)/10000,0),4)  as zjyxczcb ";
                spj += " from  ";
                spj += "(select  nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.zjyxczcb,0) as zjyxczcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.djisopen = '1' and sdy.cyc_id = '" + cycid + "' ";
                spj += ") tem ";
                spj += "  where tem.dwcb < '850'and tem.dwcb >= '600'";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                {
                    p[229] = dt32.Rows[0][0].ToString();
                    p[230] = dt32.Rows[0][1].ToString();
                    p[231] = dt32.Rows[0][2].ToString();
                    p[232] = dt32.Rows[0][3].ToString();
                }
                //850--1000
                spj = "select nvl(sum(tem.djisopen),0) as js ,0 as bl1,round(nvl(sum(tem.hscyl)/10000,0),4) as ljcyq, round(nvl(sum(tem.zjyxczcb)/10000,0),4)  as zjyxczcb ";
                spj += " from  ";
                spj += "(select  nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl, nvl(sdy.zjyxczcb,0) as zjyxczcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.djisopen = '1'  and sdy.cyc_id = '" + cycid + "'";
                spj += ") tem ";
                spj += "  where tem.dwcb < '1000'and tem.dwcb >= '850'";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                {
                    p[233] = dt32.Rows[0][0].ToString();
                    p[234] = dt32.Rows[0][1].ToString();
                    p[235] = dt32.Rows[0][2].ToString();
                    p[236] = dt32.Rows[0][3].ToString();
                }
                //>1000
                spj = "select nvl(sum(tem.djisopen),0) as js ,  0 as bl1,  round(nvl(sum(tem.ljcyl)/10000,0),4) as ljcyl ,";
                spj += " round(nvl(sum(tem.spl)/10000,0),4) as spl,  round(nvl(sum(tem.zjyxczcb)/10000,0),4) as zjyxczcb,  ";
                spj += "  round(nvl(sum(tem.hscyl)/10000,0),4) as hscyl, round(nvl(sum(tem.czcb)/10000,0),4) as czcb ";
                spj += " from  ";
                spj += "(select  nvl(sdy.djisopen,0) as djisopen, nvl(sdy.hscyl,0) as hscyl,nvl(sdy.yyspl,0) as spl,nvl(sdy.ljcyl,0) as ljcyl, nvl(sdy.zjyxczcb,0) as zjyxczcb,nvl(sdy.czcb,0) as czcb, nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where sdy.djisopen = '1' and sdy.cyc_id = '" + cycid + "' ";
                spj += ") tem ";
                spj += "  where  tem.dwcb > '1000' ";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                {
                    p[237] = dt32.Rows[0][0].ToString();
                    p[238] = dt32.Rows[0][1].ToString();
                    p[239] = dt32.Rows[0][2].ToString();
                    p[240] = dt32.Rows[0][3].ToString();  //商品量
                    p[241] = dt32.Rows[0][4].ToString(); //zjyxczcb
                    double temcb = Math.Round(double.Parse(dt32.Rows[0][4].ToString()) / double.Parse(dt32.Rows[0][5].ToString()), 2);
                    p[242] = temcb.ToString("0.00");  //单位直接运行操作成本;
                    p[243] = dt32.Rows[0][6].ToString();
                    temcb = Math.Round(double.Parse(dt32.Rows[0][6].ToString()) / double.Parse(dt32.Rows[0][3].ToString()), 2);
                    p[244] = temcb.ToString();
                }
                p[245] = pjn + '年';
                p[246] = pjbny + '-' + pjeny;

                //计算井口比例
                jsn = double.Parse(p[217].ToString()) + double.Parse(p[221].ToString()) + double.Parse(p[225].ToString());
                jsn += double.Parse(p[229].ToString()) + double.Parse(p[233].ToString() + double.Parse(p[237].ToString()));
                if (jsn != 0)
                {
                    double bl1 = (double.Parse(p[217].ToString()) / jsn) * 100; bl1 = Math.Round(bl1, 2);
                    double bl2 = (double.Parse(p[221].ToString()) / jsn) * 100; bl2 = Math.Round(bl2, 2);
                    double bl3 = (double.Parse(p[225].ToString()) / jsn) * 100; bl3 = Math.Round(bl3, 2);
                    double bl4 = (double.Parse(p[229].ToString()) / jsn) * 100; bl4 = Math.Round(bl4, 2);
                    double bl5 = (double.Parse(p[233].ToString()) / jsn) * 100; bl5 = Math.Round(bl5, 2);
                    double bl6 = (double.Parse(p[237].ToString()) / jsn) * 100; bl6 = Math.Round(bl6, 2);

                    p[218] = bl1.ToString("0.00");
                    p[222] = bl2.ToString("0.00");
                    p[226] = bl3.ToString("0.00");
                    p[230] = bl4.ToString("0.00");
                    p[234] = bl5.ToString("0.00");
                    p[238] = bl6.ToString("0.00");
                }
                #endregion

                //统计表6中的内容,建立视图VIEW_DTJIANBAO_3_2
                //P[247]--P[264]
                //spj = "select * from view_dtjianbao_3_2 ";

                spj = "";
                spj += " select  nvl(tem0.js,0) as js,  ";
                spj += " nvl(round(tem0.yyspl/10000,4),0) as yyspl, ";
                spj += " nvl(round(tem0.zjyxczcb/10000,4),0) as zjyxczcb, ";
                spj += " round(tem0.zjyxczcb/tem0.yqspl,2) as dwzjyxczcb, ";
                spj += " nvl(tem1.js,0) as js1, ";
                spj += " nvl(round(tem1.yyspl/10000,4),0) as yyspl1,";
                spj += " nvl(round(tem1.zjyxczcb/10000,4),0) as zjyxczcb1, ";
                spj += " round(tem1.zjyxczcb/tem1.yqspl,2) as dwzjyxczcb1, ";
                spj += " nvl(tem2.js,0) as js2,  ";
                spj += " nvl(round(tem2.yyspl/10000,4),0) as yyspl2, ";
                spj += " nvl(round(tem2.zjyxczcb/10000,4),0) as zjyxczcb2,";
                spj += " round(tem2.zjyxczcb/tem2.yqspl,2) as dwzjyxczcb2, ";
                spj += " nvl(tem3.js,0) as js3,  ";
                spj += " nvl(round(tem3.yyspl/10000,4),0) as yyspl3, ";
                spj += " nvl(round(tem3.zjyxczcb/10000,4),0) as zjyxczcb3, ";
                spj += " round(tem3.zjyxczcb/tem3.yqspl,2) as dwzjyxczcb3 ";
                spj += " from ( ";
                spj += " select  ";
                spj += " nvl(sum(sdy.djisopen),0) as js, ";
                spj += " nvl(sum(sdy.yyspl),0) as yyspl, ";
                spj += " nvl(sum(sdy.zjyxczcb),0) as zjyxczcb, ";
                spj += " nvl(sum(sdy.yqspl),0) as yqspl	 ";
                spj += " from dtstat_djsj sdy ";
                spj += " where sdy.gsxyjb_1 = '1'  and sdy.cyc_id = '" + cycid + "' ";
                spj += "  and sdy.jh in  ";
                spj += " (";
                spj += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where  sdy.djisopen = '1' and sdy.cyc_id = '" + cycid + "' ";
                spj += "  ) where dwcb > '" + tgcb + "' )";
                spj += " group by sdy.ssyt ";
                spj += " ) tem0, ";
                spj += " ( ";
                spj += " select ";
                spj += " nvl(sum(sdy.djisopen),0) as js, ";
                spj += " nvl(sum(sdy.yyspl),0) as yyspl, ";
                spj += " nvl(sum(sdy.zjyxczcb),0) as zjyxczcb, ";
                spj += " nvl(sum(sdy.yqspl),0) as yqspl	 ";
                spj += " from dtstat_djsj sdy ";
                spj += " where sdy.gsxyjb_1 = '2' and sdy.cyc_id = '" + cycid + "'  ";
                spj += "  and sdy.jh in  ";
                spj += " (";
                spj += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where  sdy.djisopen = '1' and sdy.cyc_id = '" + cycid + "' ";
                spj += "  ) where dwcb > '" + tgcb + "' )";
                spj += " group by sdy.ssyt ";
                spj += " ) tem1, ";
                spj += " ( ";
                spj += " select   ";
                spj += " nvl(sum(sdy.djisopen),0) as js, ";
                spj += " nvl(sum(sdy.yyspl),0) as yyspl, ";
                spj += " nvl(sum(sdy.zjyxczcb),0) as zjyxczcb, ";
                spj += " nvl(sum(sdy.yqspl),0) as yqspl	 ";
                spj += " from dtstat_djsj sdy ";
                spj += " where sdy.gsxyjb_1 = '3' and sdy.cyc_id = '" + cycid + "'  ";
                spj += "  and sdy.jh in  ";
                spj += " (";
                spj += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where  sdy.djisopen = '1' and sdy.cyc_id = '" + cycid + "' ";
                spj += "  ) where dwcb > '" + tgcb + "' )";
                spj += " group by sdy.ssyt ";
                spj += " ) tem2, ";
                spj += " (";
                spj += " select ";
                spj += " nvl(sum(sdy.djisopen),0) as js, ";
                spj += " nvl(sum(sdy.yyspl),0) as yyspl, ";
                spj += " nvl(sum(sdy.zjyxczcb),0) as zjyxczcb, ";
                spj += " nvl(sum(sdy.yqspl),0) as yqspl	 ";
                spj += " from dtstat_djsj sdy ";
                spj += " where sdy.gsxyjb_1 = '4'  and sdy.cyc_id = '" + cycid + "' ";
                spj += "  and sdy.jh in  ";
                spj += " (";
                spj += " select jh from (select sdy.jh as jh ,nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0) as dwcb from dtstat_djsj sdy where  sdy.djisopen = '1' and sdy.cyc_id = '" + cycid + "' ";
                spj += "  ) where dwcb > '" + tgcb + "' )";
                spj += " group by sdy.ssyt  ";
                spj += " ) tem3 ";


                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                {
                    p[247] = dt32.Rows[0][0].ToString();
                    p[248] = dt32.Rows[0][1].ToString();
                    p[249] = dt32.Rows[0][2].ToString();
                    p[250] = dt32.Rows[0][3].ToString();

                    p[251] = dt32.Rows[0][4].ToString();
                    p[252] = dt32.Rows[0][5].ToString();
                    p[253] = dt32.Rows[0][6].ToString();
                    p[254] = dt32.Rows[0][7].ToString();

                    p[255] = dt32.Rows[0][8].ToString();
                    p[256] = dt32.Rows[0][9].ToString();
                    p[257] = dt32.Rows[0][10].ToString();
                    p[258] = dt32.Rows[0][11].ToString();

                    p[259] = dt32.Rows[0][12].ToString();
                    p[260] = dt32.Rows[0][13].ToString();
                    p[261] = dt32.Rows[0][14].ToString();
                    p[262] = dt32.Rows[0][15].ToString();
                }

                p[263] = pjn + '年';
                p[264] = pjbny + '-' + pjeny;

                #endregion

                //四、边际效益井，无效井，特高成本井影响因素简要分析 表7,8
                #region
                //表7   p[265]---p[282]
                //spj = " select * from view_dtjianbao_4_1 ";
                p[265] = p[197];
                spj = "select count(*) as js  ";
                spj += " from dtstat_djsj ";
                spj += " where gsxyjb_1='3' and hs> (select num from csdy_info where ny='" + Session["month"].ToString() + "' and name='高含水')";
                spj += " and cyc_id = '" + cycid + "' ";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                p[266] = dt32.Rows[0][0].ToString();
                if (double.Parse(p[265].ToString()) > 0)
                    p[267] = (Math.Round(double.Parse(p[266].ToString()) / double.Parse(p[265].ToString()) * 100, 2)).ToString("0.00");
                else
                    p[267] = "0";

                spj = "select count(*) as js  ";
                spj += " from dtstat_djsj ";
                spj += " where gsxyjb_1='3' and hs<= (select num from csdy_info where ny='" + Session["month"].ToString() + "' and name='高含水')";
                spj += " and hscyl< (select num from csdy_info where ny='" + Session["month"].ToString() + "' and name='低产能')";
                spj += " and cyc_id = '" + cycid + "' ";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                p[268] = dt32.Rows[0][0].ToString();
                if (double.Parse(p[265].ToString()) > 0)
                    p[269] = (Math.Round(double.Parse(p[268].ToString()) / double.Parse(p[265].ToString()) * 100, 2)).ToString("0.00");
                else
                    p[269] = "0";

                spj = "select count(*) as js ";
                spj += " from dtstat_djsj ";
                spj += " where gsxyjb_1='3' and hs<= (select num from csdy_info where ny='" + Session["month"].ToString() + "' and name='高含水')";
                spj += " and hscyl>= (select num from csdy_info where ny='" + Session["month"].ToString() + "' and name='低产能')";
                spj += " and (jxzyf-whxjxzyf)>10000 ";
                spj += " and cyc_id = '" + cycid + "' ";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                p[270] = dt32.Rows[0][0].ToString();
                if (double.Parse(p[265].ToString()) > 0)
                    p[271] = (Math.Round(double.Parse(p[270].ToString()) / double.Parse(p[265].ToString()) * 100, 2)).ToString("0.00");
                else
                    p[271] = "0";

                p[272] = (double.Parse(p[265].ToString()) - double.Parse(p[266].ToString()) - double.Parse(p[268].ToString()) - double.Parse(p[270].ToString())).ToString();
                if (double.Parse(p[265].ToString()) > 0)
                    p[273] = (Math.Round(double.Parse(p[272].ToString()) / double.Parse(p[265].ToString()) * 100, 2)).ToString("0.00");
                else
                    p[273] = "0";


                //无效益

                p[274] = p[207];

                spj = "select count(*) as js  ";
                spj += " from dtstat_djsj ";
                spj += " where gsxyjb_1='4' and hs> (select num from csdy_info where ny='" + Session["month"].ToString() + "' and name='高含水')";
                spj += " and cyc_id = '" + cycid + "' ";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                p[275] = dt32.Rows[0][0].ToString();
                if (double.Parse(p[274].ToString()) > 0)
                    p[276] = (Math.Round(double.Parse(p[275].ToString()) / double.Parse(p[274].ToString()) * 100, 2)).ToString("0.00");
                else
                    p[276] = "0";

                spj = "select count(*) as js  ";
                spj += " from dtstat_djsj ";
                spj += " where gsxyjb_1='4' and hs<= (select num from csdy_info where ny='" + Session["month"].ToString() + "' and name='高含水')";
                spj += " and hscyl< (select num from csdy_info where ny='" + Session["month"].ToString() + "' and name='低产能')";
                spj += " and cyc_id = '" + cycid + "' ";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                p[277] = dt32.Rows[0][0].ToString();
                if (double.Parse(p[265].ToString()) > 0)
                    p[278] = (Math.Round(double.Parse(p[277].ToString()) / double.Parse(p[274].ToString()) * 100, 2)).ToString("0.00");
                else
                    p[278] = "0";

                spj = "select count(*) as js  ";
                spj += " from dtstat_djsj ";
                spj += " where gsxyjb_1='4' and hs<= (select num from csdy_info where ny='" + Session["month"].ToString() + "' and name='高含水')";
                spj += " and hscyl>= (select num from csdy_info where ny='" + Session["month"].ToString() + "' and name='低产能')";
                spj += " and (jxzyf-whxjxzyf)>10000 ";
                spj += " and cyc_id = '" + cycid + "' ";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                p[279] = dt32.Rows[0][0].ToString();
                if (double.Parse(p[265].ToString()) > 0)
                    p[280] = (Math.Round(double.Parse(p[279].ToString()) / double.Parse(p[274].ToString()) * 100, 2)).ToString("0.00");
                else
                    p[280] = "0";

                p[281] = (double.Parse(p[274].ToString()) - double.Parse(p[275].ToString()) - double.Parse(p[277].ToString()) - double.Parse(p[279].ToString())).ToString();
                if (double.Parse(p[265].ToString()) > 0)
                    p[282] = (Math.Round(double.Parse(p[281].ToString()) / double.Parse(p[274].ToString()) * 100, 2)).ToString("0.00");
                else
                    p[282] = "0";

                p[283] = pjn + '年';
                p[284] = pjbny + '-' + pjeny;



                spj = " select  sdy.jh , ";
                spj += " '直接材料费'as feen1, nvl(sdy.zjclf,0) as fee1, ";
                spj += " '直接动力费' as feen2,nvl(sdy.zjdlf,0) as fee2 , ";
                spj += " '驱油物注入费' as feen3, nvl(sdy.qywzrf-sdy.qywzrf_ryf,0) as fee3 , ";
                spj += " '维护性井下作业费' as feen4, nvl(sdy.whxjxzyf,0) as fee4, ";
                spj += " '油气处理费' as feen5, nvl(sdy.yqclf-sdy.yqclf_ryf,0) as fee5  ";
                spj += "  from dtstat_djsj sdy  ";
                spj += " where  sdy.cyc_id = '" + cycid + "'  and ";
                spj += " nvl((case  when sdy.yqspl = 0 then 0 else sdy.zjyxczcb/sdy.yqspl end),0)  > '" + tgcb + "' ";


                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                p[285] = p[237];
                if (dt32.Rows.Count > 0)
                {
                    for (int k = 0; k < dt32.Rows.Count; k++)
                    {
                        for (int i = 4; i <= 10; i = i + 2)
                            if (double.Parse(dt32.Rows[k][i].ToString()) > double.Parse(dt32.Rows[k][2].ToString()))
                            {
                                dt32.Rows[k][1] = dt32.Rows[k][i - 1];
                                dt32.Rows[k][2] = dt32.Rows[k][i];
                            }
                        //Response.Write(dt32.Rows[k][1].ToString ());
                    }
                    //Response.Write("\n"+dt32.Rows[9][0].ToString()+dt32.Rows[9][1].ToString());
                    //存费用名称
                    string[] feename = new string[5];
                    feename[0] = "直接材料费";
                    feename[1] = "直接动力费";
                    feename[2] = "驱油物注入费";
                    feename[3] = "维护性井下作业费";
                    feename[4] = "油气处理费";


                    int[] feez = new int[5];
                    for (int i = 0; i < 5; i++)
                    {
                        feez[i] = 0;
                    }
                    //求最大费用中各种费用所占的口数
                    for (int k = 0; k < dt32.Rows.Count; k++)
                    {
                        if (dt32.Rows[k][1].ToString() == feename[0])
                            feez[0]++;
                        else if (dt32.Rows[k][1].ToString() == feename[1])
                            feez[1]++;
                        else if (dt32.Rows[k][1].ToString() == feename[2])
                            feez[2]++;
                        else if (dt32.Rows[k][1].ToString() == feename[3])
                            feez[3]++;
                        else if (dt32.Rows[k][1].ToString() == feename[4])
                            feez[4]++;
                    }

                    //把各井最大费用排序
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = i + 1; j < 5; j++)
                        {
                            if (feez[j] > feez[i])
                            {
                                int h;
                                string tem;
                                h = feez[i];
                                tem = feename[i];

                                feez[i] = feez[j];
                                feename[i] = feename[j];

                                feez[j] = h;
                                feename[j] = tem;
                            }
                        }
                    }





                    p[286] = feez[0].ToString();
                    p[287] = Math.Round((Math.Round(feez[0] / double.Parse(p[285]) * 100, 4)), 2).ToString("0.00");

                    p[288] = feez[1].ToString();
                    p[289] = Math.Round((Math.Round(feez[1] / double.Parse(p[285]) * 100, 4)), 2).ToString("0.00");

                    p[290] = feez[2].ToString();
                    p[291] = Math.Round((Math.Round(feez[2] / double.Parse(p[285]) * 100, 4)), 2).ToString("0.00");

                    p[292] = feez[3].ToString();
                    p[293] = Math.Round((Math.Round(feez[3] / double.Parse(p[285]) * 100, 4)), 2).ToString("0.00");

                    p[294] = feez[4].ToString();
                    p[295] = Math.Round((Math.Round(feez[4] / double.Parse(p[285]) * 100, 4)), 2).ToString("0.00");


                    cb[104] = feename[0];
                    cb[105] = feename[1];
                    cb[106] = feename[2];
                    cb[107] = feename[3];
                    cb[108] = feename[4];

                }

                p[296] = pjn + '年';
                p[297] = pjbny + '-' + pjeny;

                #endregion
                //五、无效益井、边际效益井季度跟踪对比 表9

                //计算日期
                #region
                string dateny = Session["month"].ToString();
                string daten = dateny.Substring(0, 4);
                string datey = dateny.Substring(4, 2);
                int nb = 0;
                switch (datey)
                {
                    case "02": datey = "01"; break;
                    case "03": datey = "02"; break;
                    case "04": datey = "03"; break;
                    case "05": datey = "04"; break;
                    case "06": datey = "05"; break;
                    case "07": datey = "06"; break;
                    case "08": datey = "07"; break;
                    case "09": datey = "08"; break;
                    case "10": datey = "09"; break;
                    case "11": datey = "10"; break;
                    case "12": datey = "11"; break;
                    case "01": datey = "12"; nb = int.Parse(daten.ToString()) - 1; daten = nb.ToString(); break;
                    default: break;
                }
                dateny = daten + datey;

                #endregion
                //无效益井

                #region
                //本季度无效井数
                spj = "select count(*) as js  from dtstat_djsj sdy ";
                spj += "where sdy.gsxyjb_1 = '4' and sdy.cyc_id = '" + cycid + "'";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                    p[298] = dt32.Rows[0][0].ToString();

                //上季度无效井数
                spj = "select count(*) as js  from dtstat_djsj_all sdy ";
                spj += "where sdy.gsxyjb_1 = '4' and sdy.cyc_id = '" + cycid + "' and bny='" + lbny + "' and eny='" + leny + "'";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                int zjs = 0;
                if (dt32.Rows.Count > 0)
                {
                    p[300] = dt32.Rows[0][0].ToString();
                    zjs = 1;
                }

                int xz = int.Parse(p[298].ToString()) - int.Parse(p[300].ToString());
                p[299] = xz.ToString();
                //上季度无效井在本季度仍是无效井的数量

                //本月与上月同为无效井，本次评价仍是无效井的井

                spj = "select count(*) as js from";
                spj += " (select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='4' and bny='" + lbny + "' and eny='" + leny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) a,";
                spj += "(select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='4' and bny='" + bny + "' and eny='" + eny + "'  and  dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) b";
                spj += " where a.jh=b.jh";



                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                {
                    p[301] = dt32.Rows[0][0].ToString();
                    zjs = 2;
                }

                //上季度无效井本季度效益类别发生变化的井数
                spj = "select count(*) as js from";
                spj += " (select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='4' and bny='" + lbny + "' and eny='" + leny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) a,";
                spj += "(select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1<>'4' and bny='" + bny + "' and eny='" + eny + "'  and  dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) b";
                spj += " where a.jh=b.jh";


                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                {
                    p[302] = dt32.Rows[0][0].ToString();
                    zjs = 3;
                }
                if (zjs == 3)
                {
                    xz = int.Parse(p[300].ToString()) - int.Parse(p[301].ToString()) - int.Parse(p[302].ToString());
                    p[303] = xz.ToString();
                }

                //本季度新增加的无效井
                spj = "select count(*) as js from";
                spj += "(select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='4' and bny='" + bny + "' and eny='" + eny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) b";
                spj += " where b.jh not in";
                spj += "(select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='4' and bny='" + lbny + "' and eny='" + leny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "' ";
                spj += " )";

                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                    p[304] = dt32.Rows[0][0].ToString();
                #endregion

                //边际效益井
                #region
                //本季度边际效益井数
                spj = "select count(*) as js  from dtstat_djsj sdy ";
                spj += "where sdy.gsxyjb_1 = '3' and sdy.cyc_id = '" + cycid + "'";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                    p[305] = dt32.Rows[0][0].ToString();

                //上季度边际效益井数
                spj = "select count(*) as js  from dtstat_djsj_all sdy ";
                spj += "where sdy.gsxyjb_1 = '3' and sdy.cyc_id = '" + cycid + "' and bny='" + lbny + "' and eny='" + leny + "'";

                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                int zjs2 = 0;
                if (dt32.Rows.Count > 0)
                {
                    p[307] = dt32.Rows[0][0].ToString();
                    zjs2 = 1;
                }

                int xx = int.Parse(p[305].ToString()) - int.Parse(p[307].ToString());
                p[306] = xx.ToString();
                //上季度边际效益井在本季度仍是边际效益井的数量
                spj = "select count(*) as js from";
                spj += " (select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='3' and bny='" + lbny + "' and eny='" + leny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) a,";
                spj += "(select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='3' and bny='" + bny + "' and eny='" + eny + "'  and  dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) b";
                spj += " where a.jh=b.jh";


                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                {
                    p[308] = dt32.Rows[0][0].ToString();
                    zjs2 = 2;
                }

                //上季度边际效益井本季度效益类别发生变化的井数
                spj = "select count(*) as js from";
                spj += " (select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='3' and bny='" + lbny + "' and eny='" + leny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) a,";
                spj += "(select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1<>'3' and bny='" + bny + "' and eny='" + eny + "'  and  dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) b";
                spj += " where a.jh=b.jh";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                {
                    p[309] = dt32.Rows[0][0].ToString();
                    zjs2 = 3;
                }
                if (zjs2 == 3)
                {
                    xz = int.Parse(p[307].ToString()) - int.Parse(p[308].ToString()) - int.Parse(p[309].ToString());
                    p[310] = xz.ToString();
                }

                //本季度新增加的边际效益井
                spj = "select count(*) as js from";
                spj += "(select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='3' and bny='" + bny + "' and eny='" + eny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) b";
                spj += " where b.jh not in";
                spj += "(select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='3' and bny='" + lbny + "' and eny='" + leny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "' ";
                spj += " )";

                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                    p[311] = dt32.Rows[0][0].ToString();
                #endregion
                ////六、二季度边际、无效益治理措施及其效果

                #region
                p[312] = p[307]; //边际井
                p[313] = p[300];//无效井
                //以下变量未知
                p[314] = "0";
                p[315] = "0";
                p[316] = "0";
                p[317] = "0";
                p[318] = "0";

                //上季度边际井升级为高效益井
                p[319] = p[312];
                spj = "select count(*) as js from";
                spj += " (select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='3' and bny='" + lbny + "' and eny='" + leny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) a,";
                spj += "(select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='1' and bny='" + bny + "' and eny='" + eny + "'  and  dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) b";
                spj += " where a.jh=b.jh";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                    p[320] = dt32.Rows[0][0].ToString();

                //上季度边际井升级为低效益井    
                spj = "select count(*) as js from";
                spj += " (select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='3' and bny='" + lbny + "' and eny='" + leny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) a,";
                spj += "(select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='2' and bny='" + bny + "' and eny='" + eny + "'  and  dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) b";
                spj += " where a.jh=b.jh";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                    p[321] = dt32.Rows[0][0].ToString();

                //上季度无效益井升级为高效益井
                p[322] = p[313];
                spj = "select count(*) as js from";
                spj += " (select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='4' and bny='" + lbny + "' and eny='" + leny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) a,";
                spj += "(select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='1' and bny='" + bny + "' and eny='" + eny + "'  and  dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) b";
                spj += " where a.jh=b.jh";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                    p[323] = dt32.Rows[0][0].ToString();


                //上季度无效益井升级为低效益井
                spj = "select count(*) as js from";
                spj += " (select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='4' and bny='" + lbny + "' and eny='" + leny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) a,";
                spj += "(select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='2' and bny='" + bny + "' and eny='" + eny + "'  and  dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) b";
                spj += " where a.jh=b.jh";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                    p[324] = dt32.Rows[0][0].ToString();

                //上季度无效益井升级为边际效益井
                spj = "select count(*) as js from";
                spj += " (select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='4' and bny='" + lbny + "' and eny='" + leny + "' and dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) a,";
                spj += "(select distinct dtstat_djsj_all.jh from dtstat_djsj_all";
                spj += " where  dtstat_djsj_all.gsxyjb_1='3' and bny='" + bny + "' and eny='" + eny + "'  and  dtstat_djsj_all.cyc_id = '" + cycid + "'";
                spj += " ) b";
                spj += " where a.jh=b.jh";
                da3 = new OracleDataAdapter(spj, con0);
                dt32 = new DataTable();
                da3.Fill(dt32);
                if (dt32.Rows.Count > 0)
                    p[325] = dt32.Rows[0][0].ToString();

                #endregion

                //本次评价时间，上次评价时间
                p[129] = bctime;
                p[130] = sctime;
                p[131] = sctime;
                p[132] = bctime;
                p[133] = bctime;
                p[134] = bctime;
                p[135] = bctime;
                p[136] = sctime;
                p[137] = sctime;
                p[138] = bctime;
                p[139] = bctime;
                p[140] = bctime;
                p[141] = sctime;
                p[142] = sctime;
                p[143] = bctime;
                p[144] = sctime;
                p[145] = sctime;
                p[146] = bctime;
                for (int i = 147; i <= 166; i++)
                {
                    p[i] = qjtime;
                }

                con0.Close();

                //////////////////////////////////////生成简报
                try
                {
                    RichTextBox richTextBox1 = new RichTextBox();
                    richTextBox1.Dock = DockStyle.Fill;


                    string pp = Server.MapPath("~/static/moban/dtjianbao.rtf");
                    richTextBox1.LoadFile(pp);

                    string data = "";
                    string text = "";
                    string cyc = "";
                    for (int i = 101; i <= 166; i++)
                    {
                        data = "string" + i;
                        richTextBox1.Find(data, RichTextBoxFinds.MatchCase);

                        richTextBox1.SelectedText = p[i];
                    }
                    for (int i = 169; i <= 327; i++)
                    {
                        data = "string" + i;
                        richTextBox1.Find(data, RichTextBoxFinds.MatchCase);

                        richTextBox1.SelectedText = p[i];
                    }
                    for (int j = 101; j < 109; j++)
                    {
                        text = "text" + j;
                        richTextBox1.Find(text, RichTextBoxFinds.MatchCase);

                        richTextBox1.SelectedText = cb[j];
                    }
                    for (int k = 101; k <= 131; k++)
                    {
                        cyc = "cyc" + k;
                        richTextBox1.Find(cyc, RichTextBoxFinds.MatchCase);

                        richTextBox1.SelectedText = cycn;
                    }


                    //Response.Write(n.ToString());
                    string name = "~/static/jianbao/cycjianbao.doc";// +p[104] + ".doc";
                    richTextBox1.SaveFile(Server.MapPath(name), RichTextBoxStreamType.RichText);

                    //////////////////////////////////////
                    Response.Write("<script>alert('简报生成成功！')</script>");
                    HyperLink1.Visible = true;
                    HyperLink1.NavigateUrl = name;

                }
                catch (OracleException err)
                {
                    Label1.Text = "错误: " + err.Message.ToString();
                    Label1.Visible = true;
                }
            }
            catch (OracleException error)
            {
                Label1.Text = "错误: " + error.Message.ToString();
                Label1.Visible = true;
            }
        }
    }
}
