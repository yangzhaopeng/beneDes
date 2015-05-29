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
using beneDesCYC.core;

namespace beneDesCYC.model.month
{
    public class xscs_model  
    {
        /// <summary>
        /// 获取某采油厂下所有的销售参数信息列表
        /// </summary>
        /// <param name="cyc_id"></param>
        public static DataTable getAllList(string cyc_id, string month)
        {
            string sql = @"select NY,  s.XSYPDM as XSYPDM, JG,SJ, XSYPMC
                            from XSCS_INFO s, XSYP_INFO y
                            where s.XSYPDM=y.XSYPDM 
                            and s.CYC_ID='" + cyc_id + "'  and s.NY='" + month + "' order by NY desc";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 获取某一条具体的销售参数信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static DataTable getOneXSCS(string ny, string xsypdm, string cyc_id)
        {
            string sql = "select * from XSCS_INFO where NY='" + ny + "'  and XSYPDM='" + xsypdm + "' and CYC_ID='" + cyc_id + "'";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 添加一条销售参数信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="sc"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool add(string ny, string xsypdm, string jg, string sj, string cyc_id)
        {
            string sql = "insert into XSCS_INFO (NY,XSYPDM,JG,SJ,CYC_ID) values ('" +
                         ny + "','" +

                         xsypdm + "'," +
                         jg + "," +
                         sj + ",'" +
                         cyc_id + "')";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 编辑某一条销售参数信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="sc"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool edit(string ny, string xsypdm, string jg, string sj, string cyc_id)
        {
            string sql = "update XSCS_INFO  set JG=" + jg + " , SJ=" + sj +" where NY='" + ny + "'  and  XSYPDM='" + xsypdm + "' and CYC_ID='" + cyc_id + "'";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 删除某一条销售参数信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool delete(string ny, string xsypdm, string cyc_id)
        {
            string sql = "delete from XSCS_INFO  where NY='" + ny + "'  and XSYPDM='" + xsypdm + "' and CYC_ID='" + cyc_id + "'";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }
    }
}
