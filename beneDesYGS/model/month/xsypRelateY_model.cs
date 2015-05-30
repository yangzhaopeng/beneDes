using System;
using System.Data;
using System.Configuration;
using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using beneDesYGS.core;
using System.Collections.Generic;

namespace beneDesYGS.model.month
{
    public class xsypRelateY_model
    {
        /// <summary>
        /// 获取某采油厂下所有的销售参数信息列表

        /// </summary>
        /// <param name="DEP_TYPE"></param>
        public static DataTable getAllList(string DEP_TYPE, string month)
        {
            string sql = @"select s.NY,  s.XSYPDM as XSYPDM, y.XSYPMC, JG,SJ,yqspl.ZYS,  yqspl.YQSPL,dtb.dtb 
from XSCS_INFO s, XSYP_INFO y,dtb_info dtb,yqspl_info yqspl
where s.XSYPDM=y.XSYPDM    
and s.ny=dtb.ny  and s.DEP_TYPE=dtb.DEP_TYPE and s.xsypdm=dtb.xsypdm 
and s.ny=yqspl.ny and s.DEP_TYPE=yqspl.DEP_TYPE and s.XSYPDM=yqspl.XSYPDM
and s.XSYPDM=yqspl.XSYPDM 
      and s.DEP_TYPE='" + DEP_TYPE + "' order by NY desc";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 获取某一条具体的销售参数信息

        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="DEP_TYPE"></param>
        /// <returns></returns>
        public static DataTable getOneXSYP(string ny, string xsypdm, string DEP_TYPE)
        {
            string sql = "select * from XSCS_INFO where NY='" + ny + "'  and XSYPDM='" + xsypdm + "' and DEP_TYPE='" + DEP_TYPE + "'";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 添加一条销售油品相关信息

        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="sc"></param>
        /// <param name="DEP_TYPE"></param>
        /// <returns></returns>
        public static bool add(string ny, string xsypdm, string dtb, string jg, string sj, string yqspl, string zys, string DEP_TYPE)
        {
            SqlHelper sqlhelper = new SqlHelper();
            List<String> lsSql = new List<string>();
            //销售参数

            string XSCS_INFO = "insert into XSCS_INFO (NY,XSYPDM,JG,SJ,DEP_TYPE) values ('" +
                         ny + "','" +
                         xsypdm + "'," +
                         jg + "," +
                         sj + ",'" +
                         DEP_TYPE + "')";
            lsSql.Add(XSCS_INFO);

            //吨桶比 
            string DTB_INFO = "insert into DTB_INFO (NY,XSYPDM,DTB,DEP_TYPE) values ('" +
                        ny + "','" +

                        xsypdm + "'," +
                        dtb + ",'" +
                        DEP_TYPE + "')";
            lsSql.Add(DTB_INFO);
            //油气商品量

            string YQSPL_INFO = "insert into YQSPL_INFO (NY,XSYPDM,YQSPL,ZYS,DEP_TYPE) values ('" +
                        ny + "','" +
                        xsypdm + "'," +
                        yqspl + "," +
                        zys + ",'" +
                        DEP_TYPE + "')";
            lsSql.Add(YQSPL_INFO);
            if (sqlhelper.ExecuteTranErrorCount(lsSql) == -1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 编辑某一条销售参数信息

        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="sc"></param>
        /// <param name="DEP_TYPE"></param>
        /// <returns></returns>
        public static bool edit(string ny, string xsypdm, string dtb, string jg, string sj, string yqspl, string zys, string DEP_TYPE)
        {
            SqlHelper sqlhelper = new SqlHelper();
            List<String> lsSql = new List<string>();
            //销售参数

            string XSCS_INFO = "update XSCS_INFO  set JG=" + jg + " , SJ=" + sj + " where NY='" + ny + "'  and  XSYPDM='" + xsypdm + "' and DEP_TYPE='" + DEP_TYPE + "'";
            lsSql.Add(XSCS_INFO);

            //吨桶比 
            string DTB_INFO = "update DTB_INFO  set DTB=" + dtb + " where NY='" + ny + "'  and XSYPDM='" + xsypdm + "' and DEP_TYPE='" + DEP_TYPE + "'";
            lsSql.Add(DTB_INFO);
            //油气商品量

            string YQSPL_INFO = "update YQSPL_INFO  set YQSPL=" + yqspl + " ,ZYS=" + zys + " where NY='" + ny + "'  and XSYPDM='" + xsypdm + "' and DEP_TYPE='" + DEP_TYPE + "'";
            lsSql.Add(YQSPL_INFO);


            if (sqlhelper.ExecuteTranErrorCount(lsSql) == -1)
                return true;
            else
                //if (sqlhelper.ExcuteSql(sql) > 0) return true;
                return false;
        }

        /// <summary>
        /// 删除某一条销售参数信息

        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="DEP_TYPE"></param>
        /// <returns></returns>
        public static bool delete(string ny, string xsypdm, string DEP_TYPE)
        {
            SqlHelper sqlhelper = new SqlHelper();
            List<String> lsSql = new List<string>();
            string XSCS_INFO = "delete from XSCS_INFO  where NY='" + ny + "'  and XSYPDM='" + xsypdm + "' and DEP_TYPE='" + DEP_TYPE + "'";
            lsSql.Add(XSCS_INFO);

            //吨桶比 
            string DTB_INFO = "delete from DTB_INFO  where NY='" + ny + "'  and XSYPDM='" + xsypdm + "' and DEP_TYPE='" + DEP_TYPE + "'";
            lsSql.Add(DTB_INFO);

            //油气商品率

            string YQSPL_INFO = "delete from YQSPL_INFO  where NY='" + ny + "'  and XSYPDM='" + xsypdm + "' and DEP_TYPE='" + DEP_TYPE + "'";
            lsSql.Add(YQSPL_INFO);

            //输差
            string SC_INFO = "delete from SC_INFO  where NY='" + ny + "'  and XSYPDM='" + xsypdm + "'";
            lsSql.Add(SC_INFO);

            if (sqlhelper.ExecuteTranErrorCount(lsSql) == -1)
                return true;
            else
                //if (sqlhelper.ExcuteSql(sql) > 0) return true;
                return false;
        }
        /// <summary>
        /// 删除多条销售参数信息

        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="DEP_TYPE"></param>
        /// <returns></returns>
        public static bool deleteMutilRow(List<string> ny, List<string> xsypdm, string DEP_TYPE)
        {
            SqlHelper sqlhelper = new SqlHelper();
            List<String> lsSql = new List<string>();
            if (ny.Count == xsypdm.Count)
            {
                for (int i = 0; i < ny.Count; i++)
                {
                    string XSCS_INFO = "delete from XSCS_INFO  where NY='" + ny[i] + "'  and XSYPDM='" + xsypdm[i] + "' and DEP_TYPE='" + DEP_TYPE + "'";
                    lsSql.Add(XSCS_INFO);
                    //吨桶比 
                    string DTB_INFO = "delete from DTB_INFO  where NY='" + ny[i] + "'  and XSYPDM='" + xsypdm[i] + "' and DEP_TYPE='" + DEP_TYPE + "'";
                    lsSql.Add(DTB_INFO);
                    //油气商品量

                    string YQSPL_INFO = "delete from YQSPL_INFO  where NY='" + ny[i] + "'  and XSYPDM='" + xsypdm[i] + "' and DEP_TYPE='" + DEP_TYPE + "'";
                    lsSql.Add(YQSPL_INFO);

                    //输差
                    string SC_INFO = "delete from SC_INFO  where NY='" + ny + "'  and XSYPDM='" + xsypdm + "'";
                    lsSql.Add(SC_INFO);

                }
            }
            if (sqlhelper.ExecuteTranErrorCount(lsSql) == -1)
                return true;
            else
                //if (sqlhelper.ExcuteSql(sql) > 0) return true;
                return false;
        }
        /// <summary>
        /// 分发参数到采气厂
        /// </summary>
        /// <param name="NY"></param>
        /// <returns>键值对（是否录入：单位名称）</returns>
        internal static Dictionary<bool, List<string>> distribute(string NY, string XSYPDM)
        {
            beneDesCYC.core.SqlHelper cycSqlHelper = new beneDesCYC.core.SqlHelper();
            SqlHelper ygsSqlHelper = new SqlHelper();
            string cycListStr = "select dep_id,dep_name from department where DEP_TYPE='CYC'";
            DataTable dt = ygsSqlHelper.GetDataSet(cycListStr).Tables[0];

            //语句列表
            List<string> lsSql = new List<string>();

            //返回值 相当于二维数组  
            Dictionary<bool, List<string>> cyclist = new Dictionary<bool, List<string>>();

            List<string> trueList = new List<string>();
            List<string> falseList = new List<string>();
            //判断是否存在采油厂

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lsSql.Clear();
                    string dep_id = dt.Rows[i][0].ToString();
                    string dep_name = dt.Rows[i][1].ToString();
                    string cycParamsHavStr = @"select count(3) from XSCS_INFO s, XSYP_INFO y,dtb_info dtb,yqspl_info yqspl where s.XSYPDM=y.XSYPDM and s.ny=dtb.ny  and s.cyc_id=dtb.cyc_id and s.xsypdm=dtb.xsypdm and s.ny=yqspl.ny and s.cyc_id=yqspl.cyc_id and s.XSYPDM=yqspl.XSYPDM  and s.XSYPDM=yqspl.XSYPDM  and s.cyc_id='" + dep_id + "' and s.NY='" + NY + "' and s.XSYPDM='" + XSYPDM + "' order by s.NY desc";

                    //判断是否存在当月数据
                    if (int.Parse(cycSqlHelper.GetExecuteScalar(cycParamsHavStr).ToString()) > 0)
                    {
                        //销售参数

                        string XSCS_INFO = "update " + cycSqlHelper.getCycUserId() + ".xscs_info A  set (A.jg,A.sj)=(select  B.jg,B.sj from " + cycSqlHelper.getYgsUserId() + ".xscs_info B where B.ny='" + NY + "' and B.xsypdm='" + XSYPDM + "' and DEP_TYPE='CYC') where A.ny='" + NY + "' and A.Xsypdm='" + XSYPDM + "' and A.Cyc_Id='" + dep_id + "'";
                        lsSql.Add(XSCS_INFO);
                        //吨桶比 
                        string DTB_INFO = "update " + cycSqlHelper.getCycUserId() + ".dtb_info A  set A.dtb=(select  B.dtb from " + cycSqlHelper.getYgsUserId() + ".dtb_info B where B.ny='" + NY + "' and B.xsypdm='" + XSYPDM + "' and DEP_TYPE='CYC') where A.ny='" + NY + "' and A.Xsypdm='" + XSYPDM + "' and A.Cyc_Id='" + dep_id + "'";
                        lsSql.Add(DTB_INFO);
                        //油气商品量

                        string YQSPL_INFO = "update " + cycSqlHelper.getCycUserId() + ".YQSPL_INFO A  set (A.YQSPL,A.ZYS)=(select  B.YQSPL,B.ZYS from    " + cycSqlHelper.getYgsUserId() + ".YQSPL_INFO B where B.ny='" + NY + "' and B.xsypdm='" + XSYPDM + "' and DEP_TYPE='CYC') where A.ny='" + NY + "' and A.Xsypdm='" + XSYPDM + "' and A.Cyc_Id='" + dep_id + "'";
                        lsSql.Add(YQSPL_INFO);
                    }
                    else
                    {
                        //销售参数

                        string XSCS_INFO = "insert into " + cycSqlHelper.getCycUserId() + ".xscs_info(ny, xsypdm, jg, sj, cyc_id) select ny, xsypdm, jg, sj, '" + dep_id + "' as cyc_id from " + cycSqlHelper.getYgsUserId() + ".xscs_info where ny='" + NY + "' and dep_type='CYC' and xsypdm='" + XSYPDM + "'";
                        lsSql.Add(XSCS_INFO);

                        //吨桶比 
                        string DTB_INFO = "insert into " + cycSqlHelper.getCycUserId() + ". DTB_INFO(NY,XSYPDM,DTB,cyc_id) select NY,XSYPDM,DTB, '" + dep_id + "' as cyc_id from " + cycSqlHelper.getYgsUserId() + ".DTB_INFO  where ny='" + NY + "' and dep_type='CYC' and xsypdm='" + XSYPDM + "'";
                        lsSql.Add(DTB_INFO);
                        //油气商品量

                        string YQSPL_INFO = "insert into " + cycSqlHelper.getCycUserId() + ".YQSPL_INFO(NY,XSYPDM,YQSPL,ZYS,cyc_id) select NY,XSYPDM,YQSPL,ZYS, '" + dep_id + "' as cyc_id from " + cycSqlHelper.getYgsUserId() + ".YQSPL_INFO  where ny='" + NY + "' and dep_type='CYC' and xsypdm='" + XSYPDM + "'";
                        lsSql.Add(YQSPL_INFO);
                    }
                    if (cycSqlHelper.ExecuteTranErrorCount(lsSql) == -1)
                    {
                        trueList.Add(dep_name);
                    }
                    else
                    {
                        falseList.Add(dep_name);
                    }
                }
                cyclist.Add(true, trueList);
                cyclist.Add(false, falseList);
            }

            return cyclist;
        }
    }
}
