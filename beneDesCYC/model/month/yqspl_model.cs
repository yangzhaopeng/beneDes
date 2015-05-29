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

namespace beneDesCYC.model.month
{
    public class yqspl_model
    {
        /// <summary>
        /// 获取某采油厂下所有的油气商品率信息列表
        /// </summary>
        /// <param name="cyc_id"></param>
        public static DataTable getAllList(string cyc_id, string month)
        {
            string sql = @"select NY,  s.YQLXDM as YQLXDM, YQSPL, YQLXMC
                            from YQSPL_INFO s, YQLX_INFO y
                            where s.YQLXDM=y.YQLXDM 
                            and s.CYC_ID='" + cyc_id + "'  and s.NY='" + month + "' order by NY desc";

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
        public static DataTable getOneYQSPL(string ny,  string yqlxdm, string cyc_id)
        {
            string sql = "select * from YQSPL_INFO where NY='" + ny + "'  and YQLXDM='" + yqlxdm + "' and CYC_ID='" + cyc_id + "'";

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
        public static bool add(string ny, string yqlxdm, string yqspl, string cyc_id)
        {
            string sql = "insert into YQSPL_INFO (NY,YQLXDM,YQSPL,CYC_ID) values ('" +
                         ny + "','" +
                        
                         yqlxdm + "'," +
                         yqspl + ",'" +
                         cyc_id + "')";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
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
        public static bool edit(string ny, string yqlxdm, string yqspl, string cyc_id)
        {
            string sql = "update YQSPL_INFO  set YQSPL=" + yqspl + " where NY='" + ny + "'  and YQLXDM='" + yqlxdm + "' and CYC_ID='" + cyc_id + "'";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
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
        public static bool delete(string ny, string yqlxdm, string cyc_id)
        {
            string sql = "delete from YQSPL_INFO  where NY='" + ny + "'  and YQLXDM='" + yqlxdm + "' and CYC_ID='" + cyc_id + "'";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }
    }
}
