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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace beneDesCYC.model.month
{
    public class qjsj_model
    {
        /// <summary>
        /// 获取某采油厂下的所有井组列表信息
        /// </summary>
        public static DataTable getAllZRZ(string cyc_id, string month)
        {
            string sql = @"select distinct d.NY as NY, d.CYC_ID, d.ZYQ as ZYQ, dp.DEP_NAME as ZYQ_NAME, ZXZ, ZRZ 
                           from DJSJ d, DEPARTMENT dp 
                           where d.ZYQ=dp.DEP_ID and dp.DEP_TYPE='ZYQ' and dp.PARENT_ID=d.CYC_ID
                           and d.CYC_ID='" + cyc_id + "' and d.NY='" + month + "' order by ZYQ,ZXZ,ZRZ";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 根据查询条件获取所有井列表信息
        /// </summary>
        public static DataTable getWellList(string cyc_id, string month, string searchWord, string zyq, int end, int begin)
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




            string sql = @"select * from (select a.*, ROWNUM RN FROM (select YCLX,d.NY as NY, d.DJ_ID as DJ_ID, JH, d.ZYQ as ZYQ, dp.DEP_NAME as ZYQ_NAME,
                                  QK, JQZ, JQZZ,LHZ, JHC, x.XSYPMC, SSYT, PJDY, DJZY,WSHZ,d.TCRQ,
                                  d.CYC_ID
                           from DJSJ d, DEPARTMENT dp, XSYP_INFO x
                           where d.ZYQ=dp.DEP_ID and dp.DEP_TYPE='ZYQ' and dp.PARENT_ID=d.CYC_ID
                           and d.XSYP=x.XSYPDM
                           and d.CYC_ID='" + cyc_id + "' and d.NY='" + month + "' " + where + " order by ZYQ,ZXZ,ZRZ,JH) a WHERE ROWNUM <= " + end + ") where RN >= " + begin + " ";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        public static DataTable getCount(string cyc_id, string month, string searchWord, string zyq)
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

            string sql = @"select d.NY as NY, d.DJ_ID as DJ_ID, JH, d.ZYQ as ZYQ, dp.DEP_NAME as ZYQ_NAME,
                                  QK, JQZ, JQZZ,LHZ, JHC,  SSYT, PJDY, YCLX,DJZY,WSHZ,
                                  d.CYC_ID
                           from DJSJ d, DEPARTMENT dp,  XSYP_INFO x
                           where d.ZYQ=dp.DEP_ID and dp.DEP_TYPE='ZYQ' and dp.PARENT_ID=d.CYC_ID
                          and d.XSYP=x.XSYPDM 
                           and d.CYC_ID='" + cyc_id + "' and d.NY='" + month + "' " + where + " order by ZYQ,ZXZ,ZRZ,JH";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }


        /// <summary>
        /// 获取某一条单井信息数据
        /// </summary>
        public static DataTable getOneDjsj(string ny, string cyc_id, string dj_id)
        {
            string sql = "select * from DJSJ where NY='" + ny + "' and CYC_ID='" + cyc_id + "' and DJ_ID='" + dj_id + "'";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 添加一条单井基础信息
        /// </summary>
        public static bool add(JObject obj, string cyc_id)
        {
            string sql = "insert into DJSJ(NY,DJ_ID,JH ,ZYQ,QK,JQZ,JQZZ,LHZ,JHC,YQLX,XSYP,PJDY,TCRQ,SSYT,YCLX,DJZY,WSHZ,CYC_ID) values ('" +
                         obj["NY"] + "','" +
                         obj["DJ_ID"] + "','" +
                         obj["JH"] + "','" +
                         obj["ZYQ"] + "','" +
                         obj["QK"] + "','" +
                         obj["ZXZ"] + "','" +
                         obj["ZRZ"] + "','" +
                         obj["CYJXH"] + "'," +
                         obj["CYJMPGL"] + "," +
                         obj["DJGL"] + "," +
                         obj["FZL"] + ",'" +
                         obj["YQLX"] + "','" +
                         obj["XSYP"] + "','" +
                         obj["TCRQ"] + "','" +
                         obj["JB"] + "','" +
                         obj["DJDB"] + "','" +
                         obj["SSYT"] + "','" +
                         obj["PJDY"] + "','" +
                         obj["YCLX"] + "','" +
                         cyc_id + "')";

            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 编辑单井基础信息
        /// </summary>
        public static bool edit(JObject obj, string cyc_id)
        {
            string sql = "update DJSJ set " +
                         "DJ_ID='" + obj["DJ_ID_new"] + "', " +
                         "JH='" + obj["JH"] + "', " +
                         "ZYQ='" + obj["ZYQ"] + "', " +
                         "QK='" + obj["QK"] + "', " +
                         "ZXZ='" + obj["ZXZ"] + "', " +
                         "ZRZ='" + obj["ZRZ"] + "', " +
                         "CYJXH='" + obj["CYJXH"] + "', " +
                         "CYJMPGL=" + obj["CYJMPGL"] + ", " +
                         "DJGL=" + obj["DJGL"] + ", " +
                         "FZL=" + obj["FZL"] + ", " +
                         "YQLX='" + obj["YQLX"] + "', " +
                         "XSYP='" + obj["XSYP"] + "', " +
                         "TCRQ='" + obj["TCRQ"] + "', " +
                         "JB='" + obj["JB"] + "', " +
                         "DJDB='" + obj["DJDB"] + "', " +
                         "SSYT='" + obj["SSYT"] + "', " +
                         "PJDY='" + obj["PJDY"] + "', " +
                         "YCLX='" + obj["YCLX"] + "' " +
                         "where CYC_ID='" + cyc_id + "' and NY='" + obj["NY"] + "' and DJ_ID='" + obj["DJ_ID"] + "'";

            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 删除一条单井基础信息
        /// </summary>
        /// <returns></returns>
        public static bool delete(string ny, string dj_id, string cyc_id)
        {
            string sql = "delete from DJSJ where CYC_ID='" + cyc_id + "' and NY='" + ny + "' and DJ_ID='" + dj_id + "'";

            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }
    }
}
