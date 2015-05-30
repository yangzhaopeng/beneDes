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

namespace beneDesYGS.model.month
{
    public class xscs_model
    {
        /// <summary>
        /// 获取某采油厂下所有的地质措施列表
        /// </summary>
        /// <param name="cyc_id"></param>
        public static DataTable getAllList(string cyc_id, string month)
        {
            /*string sql = @"select NY, s.DEP_ID as DEP_ID, s.XSYPDM as XSYPDM, SC, DEP_NAME, XSYPMC
                            from SC_INFO s, DEPARTMENT d, YQLX_INFO y
                            where s.DEP_ID=d.DEP_ID
                            and s.XSYPDM=y.XSYPDM
                            and s.CYC_ID='" + cyc_id + "' and d.PARENT_ID='" + cyc_id + "' and s.NY='" + month + "' order by NY desc, s.DEP_ID";*/
            string sql = "select d.NY, x.XSYPMC, d.XSYPDM, d.JG, d.SJ from XSCS_INFO d, XSYP_INFO x where d.XSYPDM=x.XSYPDM and NY='" + month + "'";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 获取某一条具体的地质措施
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="xsypdm"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static DataTable getOneXS(string ny, string xsypdm)
        {
            string sql = "select * from XSCS_INFO d, XSYP_INFO x where d.XSYPDM=x.XSYPDM and d.XSYPDM='" + xsypdm + "' and NY='" + ny + "'";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        //通过油气类型名称获得油气类型代码
        //public static DataTable getDM(string ny, string yqlxmc

        /// <summary>
        /// 添加一条地质措施
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="xsypdm"></param>
        /// <param name="sc"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool add(string ny, string xsypdm, string jg, string sj)
        {

            string sql = "insert into XSCS_INFO (NY,XSYPDM,JG,SJ) values ('" +
                         ny + "','" +
                         xsypdm + "','" + jg + "','"+sj+"')";
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
        /// <param name="xsypdm"></param>
        /// <param name="sc"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool edit(string ny, string xsypdm, string jg, string sj)
        {

            string sql = "update XSCS_INFO set JG='" + jg + "', SJ='" + sj + "' where NY='" + ny + "' and XSYPDM='" + xsypdm + "'";
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
        /// <param name="xsypdm"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool delete(string ny, string xsypdm, string jg)
        {
            string sql = "delete from XSCS_INFO where NY='" + ny + "' and XSYPDM='" + xsypdm + "' and JG='" + jg + "'";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }
    }
}
