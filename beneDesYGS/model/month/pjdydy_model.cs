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
    public class pjdydy_model
    {
        /// <summary>
        /// 查询某单位下的所有评价单元
        /// </summary>
        /// <param name="cyc_id">id 或者 [a-zA-Z]匹配所有单位的评价单元</param>
        /// <returns></returns>
        public static DataTable getPjdyList(string as_cyc, string data_type)
        {
            string sql = string.Format("select A.id,A.pjdymc,A.cyc_id,B.Dep_Name from pjdydy A inner join department B  on A.Cyc_Id=B.Dep_Id  where regexp_like(A.cyc_id,'{0}') and B.DEP_TYPE='{1}' order by A.cyc_id", as_cyc, data_type);
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }
        /// <summary>
        /// 根据查询条件获取所有井列表信息
        /// </summary>
        public static DataTable getWellList(string cyc_id, string month, string searchWord, string zyq, string zxz, string zrz)
        {
            string where = "";

            if (searchWord != null && searchWord != "")
            {
                where += " and d.JH like '%" + searchWord.Replace("'", "") + "%'";
            }
            if (zyq != null && zyq != "")
            {
                where += " and d.ZYQ='" + zyq + "'";
            }
            if (zxz != null && zxz != "")
            {
                where += " and d.ZXZ='" + zxz + "'";
            }
            if (zrz != null && zrz != "")
            {
                where += " and d.ZRZ='" + zrz + "'";
            }

            string sql = @"select d.NY as NY, d.DJ_ID as DJ_ID, JH, d.ZYQ as ZYQ, dp.DEP_NAME as ZYQ_NAME,
                                  QK, ZXZ, ZRZ, CYJXH, CYJMPGL,
                                  DJGL, FZL, YQLX, YQLXMC, XSYP,
                                  XSYPMC, TCRQ, JB, JBNAME, DJDB,
                                  SSYT, PJDY, YCLX, CYC_ID
                           from DJSJ d, DEPARTMENT dp, YQLX_INFO y, XSYP_INFO x, JB_INFO j
                           where d.ZYQ=dp.DEP_ID and dp.DEP_TYPE='ZYQ' and dp.PARENT_ID=d.CYC_ID
                           and d.YQLX=y.YQLXDM and d.XSYP=x.XSYPDM and d.JB=j.JBH
                           and d.CYC_ID='" + cyc_id + "' and d.NY='" + month + "' " + where + " order by ZYQ,ZXZ,ZRZ,JH";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 获取某一条单井信息数据
        /// </summary>
        public static DataTable getOneDjsj(string PJDYMC, string cyc_id)
        {
            string sql = "select * from PJDYDY where PJDYMC='" + PJDYMC + "' and CYC_ID='" + cyc_id + "' ";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }
        public static int getMaxId()
        {
            string sql = "select max(id) from PJDYDY";
            SqlHelper sqlhelper = new SqlHelper();
            int ID = 0;
            Object idObj = sqlhelper.GetExecuteScalar(sql);
            if (idObj != null)
                int.TryParse(idObj.ToString(), out ID);
            return ID;
        }
        /// <summary>
        /// 添加一条单井基础信息
        /// </summary>
        public static bool add(string PJDYMC, string cyc_id)
        {
            SqlHelper sqlhelper = new SqlHelper();
            int ID = getMaxId() + 1;
            string sql = "insert into PJDYDY (ID,PJDYMC,CYC_ID) values (" + ID + ",'" + PJDYMC + "','" + cyc_id + "')";
            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 编辑单井基础信息
        /// </summary>
        public static bool edit(int ID, string PJDYMC)
        {
            string sql = "update pjdydy set " +
                         " PJDYMC='" + PJDYMC + "' " +
                         " where ID='" + ID + "'";

            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 删除一条单井基础信息
        /// </summary>
        /// <returns></returns>
        public static bool delete(int ID)
        {
            string sql = "delete PJDYDY where ID=" + ID;

            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }


    }
}
