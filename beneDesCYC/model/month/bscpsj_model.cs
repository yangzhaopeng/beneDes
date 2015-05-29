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
    public class bscpsj_model
    {
        /// <summary>
        /// 获取某采油厂下所有的伴生产品数据信息列表
        /// </summary>
        /// <param name="cyc_id"></param>
        public static DataTable getAllList(string cyc_id, string month, string DEP_TYPE)
        {
            string sql = string.Format("select NY,  s.BSCPDM as BSCPDM, CL,SPL,JG,XSSJ, BSCPMC                            from BSCPSJ s, BSCPLX_INFO y  where s.BSCPDM=y.BSCPDM   and s.CYC_ID='{0}'  and s.NY='{1}' and dep_type='{2}' order by NY desc", cyc_id, month, DEP_TYPE);

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 获取某一条具体的伴生产品数据信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static DataTable getOneBSCPSJ(string ny, string bscpdm, string cyc_id)
        {
            string sql = "select * from BSCPSJ where NY='" + ny + "'  and BSCPDM='" + bscpdm + "' and CYC_ID='" + cyc_id + "'";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 添加一条伴生产品数据信息

        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="sc"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool add(string ny, string bscpdm, string cl, string spl, string jg, string xssj, string cyc_id)
        {
            string sql = "insert into BSCPSJ (NY,BSCPDM,CL,SPL,JG,XSSJ,CYC_ID) values ('" +
                         ny + "','" +

                         bscpdm + "'," +
                         cl + "," +
                         spl + "," +
                         jg + "," +
                         xssj + ",'" +
                         cyc_id + "')";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 编辑某一条伴生产品数据信息

        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="sc"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool edit(string ny, string bscpdm, string cl, string spl, string jg, string xssj, string cyc_id)
        {
            string sql = "update BSCPSJ  set CL=" + cl + ", SPL=" + spl + ", JG=" + jg + ", XSSJ=" + xssj + "  where NY='" + ny + "'  and BSCPDM='" + bscpdm + "' and CYC_ID='" + cyc_id + "'";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 删除某一条伴生产品数据信息

        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool delete(string ny, string bscpdm, string cyc_id)
        {
            string sql = "delete from BSCPSJ  where NY='" + ny + "'  and BSCPDM='" + bscpdm + "' and CYC_ID='" + cyc_id + "'";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }
    }
}
