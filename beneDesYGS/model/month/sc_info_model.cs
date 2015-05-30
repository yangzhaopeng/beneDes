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

namespace beneDesYGS.model.month
{
    public class sc_info_model
    { 
        /// <summary>
        /// 获取某采油厂下所有的输差信息列表
        /// </summary>
        /// <param name="cyc_id"></param>
        public static DataTable getAllList(string cyc_id, string month)
        {
            string sql = @"select NY, s.DEP_ID as DEP_ID, s.YQLXDM as YQLXDM, SC, DEP_NAME, YQLXMC
                            from SC_INFO s, DEPARTMENT d, YQLX_INFO y
                            where s.DEP_ID=d.DEP_ID
                            and s.YQLXDM=y.YQLXDM
                            and s.CYC_ID='" + cyc_id + "' and d.PARENT_ID='" + cyc_id + "' and s.NY='" + month + "' order by NY desc, s.DEP_ID";
            
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 获取某一条具体的输差信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static DataTable getOneSC(string ny, string dep_id, string yqlxdm, string cyc_id)
        {
            string sql = "select * from SC_INFO where NY='" + ny + "' and DEP_ID='" + dep_id + "' and YQLXDM='" + yqlxdm + "' and CYC_ID='" + cyc_id + "'";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 添加一条输差信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="sc"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool add(string ny, string dep_id, string yqlxdm, string sc, string cyc_id)
        {
            string sql = "insert into SC_INFO (NY,DEP_ID,YQLXDM,SC,CYC_ID) values ('" +
                         ny + "','" +
                         dep_id + "','" +
                         yqlxdm + "'," +
                         sc + ",'" +
                         cyc_id + "')";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 编辑某一条输差信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="sc"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool edit(string ny, string dep_id, string yqlxdm, string sc, string cyc_id)
        {
            string sql = "update SC_INFO set SC=" + sc + " where NY='" + ny + "' and DEP_ID='" + dep_id + "' and YQLXDM='" + yqlxdm + "' and CYC_ID='" + cyc_id + "'";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 删除某一条输差信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool delete(string ny, string dep_id, string yqlxdm, string cyc_id)
        {
            string sql = "delete from SC_INFO where NY='" + ny + "' and DEP_ID='" + dep_id + "' and YQLXDM='" + yqlxdm + "' and CYC_ID='" + cyc_id + "'";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }
    }
}
