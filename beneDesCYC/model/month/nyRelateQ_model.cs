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
using beneDesCYC.core;
using System.Collections.Generic;

namespace beneDesCYC.model.month
{
    public class nyRelateQ_model
    {
        /// <summary>
        /// 获取某采油厂下所有的油气商品率信息列表
        /// </summary>
        /// <param name="cyc_id"></param>
        public static DataTable getAllList(string CYC_ID, string month)
        {
            string sql = @"select hl.NY,  qjfy,ktfy,tjxs.dly,hl.hl
from qjktfy_info qjkt ,trqdly_info tjxs, rbmhl_info  hl
where   hl.ny=qjkt.ny and hl.CYC_ID= qjkt.CYC_ID
and hl.ny=tjxs.ny and hl.CYC_ID= tjxs.CYC_ID
and hl.CYC_ID='" + CYC_ID + "' order by NY desc";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 获取某一条具体的油气商品率信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static DataTable getOneYQSPL(string ny, string CYC_ID)
        {
            string sql = "select * from rbmhl_info where NY='" + ny + "'   and CYC_ID='" + CYC_ID + "'";

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
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool add(string ny, string hl, string dly, string qjfy, string ktfy, string CYC_ID)
        {
            SqlHelper sqlhelper = new SqlHelper();
            List<String> lsSql = new List<string>();
            //汇率
            string RBMHL_INFO = "insert into RBMHL_INFO (NY,HL,CYC_ID) values ('" +
                    ny + "','" +
                    hl + "','" +
                    CYC_ID + "')";
            lsSql.Add(RBMHL_INFO);
            //体积换算系数
            string TRQDLY_INFO = "insert into TRQDLY_INFO (NY,DLY,CYC_ID) values ('" +
                        ny + "','" +
                        dly + "','" +
                        CYC_ID + "')";
            lsSql.Add(TRQDLY_INFO);
            //期间勘探费用
            string QJKTFY_INFO = "insert into QJKTFY_INFO (NY,QJFY,KTFY,CYC_ID) values ('" +
                       ny + "','" +
                       qjfy + "','" +
                       ktfy + "','" +
                       CYC_ID + "')";
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
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool edit(string ny, string hl, string dly, string qjfy, string ktfy, string CYC_ID)
        {
            SqlHelper sqlhelper = new SqlHelper();
            List<String> lsSql = new List<string>();

            //汇率
            string RBMHL_INFO = "update RBMHL_INFO  set HL=" + hl + " where NY='" + ny + "'  and CYC_ID='" + CYC_ID + "'";
            lsSql.Add(RBMHL_INFO);
            //体积换算系数
            string TRQDLY_INFO = "update TRQDLY_INFO  set DLY=" + dly + " where NY='" + ny + "'  and CYC_ID='" + CYC_ID + "'";
            lsSql.Add(TRQDLY_INFO);
            //期间勘探费用
            string QJKTFY_INFO = "update QJKTFY_INFO  set QJFY=" + qjfy + " ,KTFY=" + ktfy + "  where NY='" + ny + "'  and CYC_ID='" + CYC_ID + "'";
            lsSql.Add(QJKTFY_INFO);
            if (sqlhelper.ExecuteTranErrorCount(lsSql) == -1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除某一条油气商品率信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool delete(string ny, string CYC_ID)
        {
            SqlHelper sqlhelper = new SqlHelper();
            List<String> lsSql = new List<string>();
            //汇率
            string RBMHL_INFO = "delete from RBMHL_INFO  where NY='" + ny + "'   and CYC_ID='" + CYC_ID + "'";
            lsSql.Add(RBMHL_INFO);
            //体积换算系数
            string TRQDLY_INFO = "delete from TRQDLY_INFO  where NY='" + ny + "'   and CYC_ID='" + CYC_ID + "'";
            lsSql.Add(TRQDLY_INFO);
            //期间勘探费用
            string QJKTFY_INFO = "delete from QJKTFY_INFO  where NY='" + ny + "'   and CYC_ID='" + CYC_ID + "'";
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
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool deleteMutilRow(List<string> ny, string CYC_ID)
        {
            SqlHelper sqlhelper = new SqlHelper();
            List<String> lsSql = new List<string>();
            for (int i = 0; i < ny.Count; i++)
            {
                //汇率
                string RBMHL_INFO = "delete from RBMHL_INFO  where NY='" + ny[i] + "'   and CYC_ID='" + CYC_ID + "'";
                lsSql.Add(RBMHL_INFO);
                //体积换算系数
                string TRQDLY_INFO = "delete from TRQDLY_INFO  where NY='" + ny[i] + "'   and CYC_ID='" + CYC_ID + "'";
                lsSql.Add(TRQDLY_INFO);
                //期间勘探费用
                string QJKTFY_INFO = "delete from QJKTFY_INFO  where NY='" + ny[i] + "'   and CYC_ID='" + CYC_ID + "'";
                lsSql.Add(QJKTFY_INFO);
            }
            if (sqlhelper.ExecuteTranErrorCount(lsSql) == -1)
                return true;
            else
                //if (sqlhelper.ExcuteSql(sql) > 0) return true;
                return false;
        }
    }
}
