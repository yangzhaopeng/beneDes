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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace beneDesYGS.model.base_info
{
    public class fxlx_model
    {
        /// <summary>
        /// 获取某采油厂下所有的地质措施列表
        /// </summary>
        /// <param name="cyc_id"></param>
        public static DataTable getAllList(string cyc_id, string month)
        {
            /*string sql = @"select NY, s.DEP_ID as DEP_ID, s.YQLXDM as YQLXDM, SC, DEP_NAME, YQLXMC
                            from SC_INFO s, DEPARTMENT d, YQLX_INFO y
                            where s.DEP_ID=d.DEP_ID
                            and s.YQLXDM=y.YQLXDM
                            and s.CYC_ID='" + cyc_id + "' and d.PARENT_ID='" + cyc_id + "' and s.NY='" + month + "' order by NY desc, s.DEP_ID";*/
            string sql = "select FXYYDM, FXYYMC, BZ from FXYY_INFO";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 获取某一条具体的地质措施
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static DataTable getOneFX(string fxyydm)
        {
            string sql = "select * from FXYY_INFO where FXYYDM='" + fxyydm + "'";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 添加一条地质措施
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="sc"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool add(string fxyydm, string fxyymc, string bz)
        {

            string sql = "insert into FXYY_INFO (FXYYDM,FXYYMC,BZ) values ('" +
                         fxyydm + "','" +
                         fxyymc + "','" +
                         bz + "')";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0)
            {
                return true;
            }
            else
            { return false; }
        }

        /// <summary>
        /// 编辑某一条地质措施
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="sc"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool edit(string fxyydm, string fxyymc, string bz)
        {

            string sql = "update FXYY_INFO set FXYYDM='" + fxyydm + "', FXYYMC='" + fxyymc + "', BZ='" + bz + "'where FXYYDM='" + fxyydm + "'";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0)
            { return true; }
            else
            { return false; }
        }

        /// <summary>
        /// 删除某一条输差信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool delete(string fxyydm, string fxyymc)
        {
            string sql = "delete from FXYY_INFO where FXYYDM='" + fxyydm + "' and FXYYMC='" + fxyymc + "'";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }
    }
}
