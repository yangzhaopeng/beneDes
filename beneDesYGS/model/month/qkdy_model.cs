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
    public class qkdy_model
    {
        /// <summary>
        /// 根据查询条件获取所有井列表信息
        /// </summary>
        public static DataTable getQKList(string cyc_id)
        {
            DataTable dt = new DataTable();
            SqlHelper sqlhelper = new SqlHelper();
            string sql = @"select ID,QKMC from qkdy where DEP_ID='" + cyc_id + "'order by ID";
            DataSet ds = sqlhelper.GetDataSet(sql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                dt = ds.Tables[0];
            return dt;
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
            string sql = "insert into DJSJ (NY, DJ_ID, JH, ZYQ, QK, ZXZ, ZRZ, CYJXH, CYJMPGL, DJGL, FZL, YQLX, XSYP, TCRQ, JB, DJDB, SSYT, PJDY, YCLX, CYC_ID) values ('" +
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
