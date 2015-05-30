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
using System.Collections;

namespace beneDesYGS.model.month
{
    public class nyRelateQ_model
    {
        /// <summary>
        /// 获取某采油厂下所有的油气商品率信息列表
        /// </summary>
        /// <param name="DEP_TYPE"></param>
        public static DataTable getAllList(string DEP_TYPE, string month)
        {
            string sql = @"select hl.NY,  qjfy,ktfy,tjxs.dly,hl.hl
from qjktfy_info qjkt ,trqdly_info tjxs, rbmhl_info  hl
where   hl.ny=qjkt.ny and hl.DEP_TYPE= qjkt.DEP_TYPE
and hl.ny=tjxs.ny and hl.DEP_TYPE= tjxs.DEP_TYPE
and hl.DEP_TYPE='" + DEP_TYPE + "' order by NY desc";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 获取某一条具体的油气商品率信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="DEP_TYPE"></param>
        /// <returns></returns>
        public static DataTable getOneYQSPL(string ny, string DEP_TYPE)
        {
            string sql = "select * from rbmhl_info where NY='" + ny + "'   and DEP_TYPE='" + DEP_TYPE + "'";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 添加一条油气商品率信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="sc"></param>
        /// <param name="DEP_TYPE"></param>
        /// <returns></returns>
        public static bool add(string ny, string hl, string dly, string qjfy, string ktfy, string DEP_TYPE)
        {
            SqlHelper sqlhelper = new SqlHelper();
            List<String> lsSql = new List<string>();
            //汇率
            string RBMHL_INFO = "insert into RBMHL_INFO (NY,HL,DEP_TYPE) values ('" +
                    ny + "','" +
                    hl + "','" +
                    DEP_TYPE + "')";
            lsSql.Add(RBMHL_INFO);
            //体积换算系数
            string TRQDLY_INFO = "insert into TRQDLY_INFO (NY,DLY,DEP_TYPE) values ('" +
                        ny + "','" +
                        dly + "','" +
                        DEP_TYPE + "')";
            lsSql.Add(TRQDLY_INFO);
            //期间勘探费用
            string QJKTFY_INFO = "insert into QJKTFY_INFO (NY,QJFY,KTFY,DEP_TYPE) values ('" +
                       ny + "','" +
                       qjfy + "','" +
                       ktfy + "','" +
                       DEP_TYPE + "')";
            lsSql.Add(QJKTFY_INFO);

            if (sqlhelper.ExecuteTranErrorCount(lsSql) == -1)
                return true;
            else
                //if (sqlhelper.ExcuteSql(sql) > 0) return true;
                return false;
        }

        /// <summary>
        /// 编辑某一条油气商品率信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="sc"></param>
        /// <param name="DEP_TYPE"></param>
        /// <returns></returns>
        public static bool edit(string ny, string hl, string dly, string qjfy, string ktfy, string DEP_TYPE)
        {
            SqlHelper sqlhelper = new SqlHelper();
            List<String> lsSql = new List<string>();

            //汇率
            string RBMHL_INFO = "update RBMHL_INFO  set HL=" + hl + " where NY='" + ny + "'  and DEP_TYPE='" + DEP_TYPE + "'";
            lsSql.Add(RBMHL_INFO);
            //体积换算系数
            string TRQDLY_INFO = "update TRQDLY_INFO  set DLY=" + dly + " where NY='" + ny + "'  and DEP_TYPE='" + DEP_TYPE + "'";
            lsSql.Add(TRQDLY_INFO);
            //期间勘探费用
            string QJKTFY_INFO = "update QJKTFY_INFO  set QJFY=" + qjfy + " ,KTFY=" + ktfy + "  where NY='" + ny + "'  and DEP_TYPE='" + DEP_TYPE + "'";
            lsSql.Add(QJKTFY_INFO);
            if (sqlhelper.ExecuteTranErrorCount(lsSql) == -1)
                return true;
            else
                //if (sqlhelper.ExcuteSql(sql) > 0) return true;
                return false;
        }

        /// <summary>
        /// 删除某一条油气商品率信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="DEP_TYPE"></param>
        /// <returns></returns>
        public static bool delete(string ny, string DEP_TYPE)
        {
            SqlHelper sqlhelper = new SqlHelper();
            List<String> lsSql = new List<string>();
            //汇率
            string RBMHL_INFO = "delete from RBMHL_INFO  where NY='" + ny + "'   and DEP_TYPE='" + DEP_TYPE + "'";
            lsSql.Add(RBMHL_INFO);
            //体积换算系数
            string TRQDLY_INFO = "delete from TRQDLY_INFO  where NY='" + ny + "'   and DEP_TYPE='" + DEP_TYPE + "'";
            lsSql.Add(TRQDLY_INFO);
            //期间勘探费用
            string QJKTFY_INFO = "delete from QJKTFY_INFO  where NY='" + ny + "'   and DEP_TYPE='" + DEP_TYPE + "'";
            lsSql.Add(QJKTFY_INFO);
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
        public static bool deleteMutilRow(List<string> ny, string DEP_TYPE)
        {
            SqlHelper sqlhelper = new SqlHelper();
            List<String> lsSql = new List<string>();
            for (int i = 0; i < ny.Count; i++)
            {
                //汇率
                string RBMHL_INFO = "delete from RBMHL_INFO  where NY='" + ny[i] + "'   and DEP_TYPE='" + DEP_TYPE + "'";
                lsSql.Add(RBMHL_INFO);
                //体积换算系数
                string TRQDLY_INFO = "delete from TRQDLY_INFO  where NY='" + ny[i] + "'   and DEP_TYPE='" + DEP_TYPE + "'";
                lsSql.Add(TRQDLY_INFO);
                //期间勘探费用
                string QJKTFY_INFO = "delete from QJKTFY_INFO  where NY='" + ny[i] + "'   and DEP_TYPE='" + DEP_TYPE + "'";
                lsSql.Add(QJKTFY_INFO);
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
        internal static Dictionary<bool, List<string>> distribute(string NY)
        {
            beneDesCYC.core.SqlHelper cycSqlHelper = new beneDesCYC.core.SqlHelper();
            SqlHelper ygsSqlHelper = new SqlHelper();
            string cycListStr = "select dep_id,dep_name from department where DEP_TYPE='CQC'";
            DataTable dt = ygsSqlHelper.GetDataSet(cycListStr).Tables[0];

            //语句列表
            List<string> lsSql = new List<string>();

            //返回值 相当于二维数组  
            Dictionary<bool, List<string>> cyclist = new Dictionary<bool, List<string>>();

            List<string> trueList = new List<string>();
            List<string> falseList = new List<string>();

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lsSql.Clear();
                    string dep_id = dt.Rows[i][0].ToString();
                    string dep_name = dt.Rows[i][1].ToString();
                    string cycParamsHavStr = @"select count(3) from " + cycSqlHelper.getCycUserId() + ".qjktfy_info qjkt ," + cycSqlHelper.getCycUserId() + ".trqdly_info tjxs, " + cycSqlHelper.getCycUserId() + ".rbmhl_info  hl where hl.ny=qjkt.ny and hl.cyc_id= qjkt.cyc_id and hl.ny=tjxs.ny and hl.cyc_id= tjxs.cyc_id and hl.cyc_id='" + dep_id + "' and hl.NY='" + NY + "' order by hl.NY desc";
                    if (int.Parse(cycSqlHelper.GetExecuteScalar(cycParamsHavStr).ToString()) > 0)
                    {
                        //update
                        //汇率
                        string RBMHL_INFO = "update " + cycSqlHelper.getCycUserId() + ".rbmhl_info A set a.Hl=(SELECT B.HL FROM  " + cycSqlHelper.getYgsUserId() + ".RBMHL_INFO b WHERE ny='" + NY + "' and dep_type='CQC' ) WHERE a.NY=(SELECT ny FROM " + cycSqlHelper.getCycUserId() + ".RBMHL_INFO b WHERE ny='" + NY + "' and cyc_id='" + dep_id + "')";
                        lsSql.Add(RBMHL_INFO);
                        //体积换算系数
                        string TRQDLY_INFO = " update " + cycSqlHelper.getCycUserId() + ".TRQDLY_INFO A set a.dly=(SELECT B.dly FROM " + cycSqlHelper.getYgsUserId() + ".TRQDLY_INFO b WHERE ny='" + NY + "' and  dep_type='CQC') WHERE a.NY=(SELECT ny FROM " + cycSqlHelper.getCycUserId() + ".TRQDLY_INFO b WHERE ny='" + NY + "' and cyc_id='" + dep_id + "')";
                        lsSql.Add(TRQDLY_INFO);
                        //期间勘探费用
                        string QJKTFY_INFO = "update " + cycSqlHelper.getCycUserId() + ".QJKTFY_INFO A set (a.QJFY,A.KTFY)=(SELECT B.QJFY,B.KTFY FROM " + cycSqlHelper.getYgsUserId() + ".QJKTFY_INFO b WHERE ny='" + NY + "' and  dep_type='CQC') WHERE a.NY=(SELECT ny FROM " + cycSqlHelper.getCycUserId() + ".QJKTFY_INFO b WHERE ny='" + NY + "' and cyc_id='" + dep_id + "')";
                        lsSql.Add(QJKTFY_INFO);
                    }
                    else
                    {
                        //汇率
                        string RBMHL_INFO = "insert into " + cycSqlHelper.getCycUserId() + ".RBMHL_INFO (NY,HL,CYC_ID,CYC_ID) select  NY,HL,'" + dep_id + "' as CYC_ID from " + cycSqlHelper.getYgsUserId() + ".RBMHL_INFO where ny='" + NY + "'";
                        lsSql.Add(RBMHL_INFO);
                        //体积换算系数
                        string TRQDLY_INFO = "insert into " + cycSqlHelper.getCycUserId() + ".TRQDLY_INFO (NY,DLY,CYC_ID) select  NY,DLY,'" + dep_id + "' as CYC_ID from " + cycSqlHelper.getYgsUserId() + ".TRQDLY_INFO where ny='" + NY + "'";
                        lsSql.Add(TRQDLY_INFO);
                        //期间勘探费用
                        string QJKTFY_INFO = "insert into " + cycSqlHelper.getCycUserId() + ".QJKTFY_INFO (NY,QJFY,KTFY,CYC_ID) select  NY,QJFY,KTFY,'" + dep_id + "' as CYC_ID from " + cycSqlHelper.getYgsUserId() + ".QJKTFY_INFO  where ny='" + NY + "'";
                        lsSql.Add(QJKTFY_INFO);
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
