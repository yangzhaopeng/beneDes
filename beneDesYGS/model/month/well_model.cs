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
using System.Web.UI.MobileControls;
using System.Collections.Generic;

namespace beneDesYGS.model.month
{
    public class well_model
    {

        /// <summary>
        /// 根据查询条件获取所有井列表信息
        /// </summary>
        public static DataTable getWellList(string qkid)
        {
            DataTable dt = new DataTable();
            string sql = @"select A.ID,A.jh,A.qk,A.pjdy,B.QKMC from pc_well A inner join qkdy  B on A.qk=B.id  where A.qk='" + qkid + "' order by qk";

            SqlHelper sqlhelper = new SqlHelper();
            DataSet ds = sqlhelper.GetDataSet(sql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                dt = ds.Tables[0];
            return dt;
        }
        public static DataTable getPjdyWellList(string pjdyid)
        {
            DataTable dt = new DataTable();
            string sql = @"select A.ID,A.jh,A.qk,B.pjdymc,C.QKMC as QKMC from pc_well A inner join Pjdydy B on A.pjdy=B.id 
inner join qkdy C on A.qk=C.Id  where A.pjdy='" + pjdyid + "' order by A.pjdy";

            SqlHelper sqlhelper = new SqlHelper();
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

        public static bool update(string IDS, string pjdy, string qk)
        {
            SqlHelper sqlhelper = new SqlHelper();
            List<string> sqlStr = new List<string>();
            string[] ids = IDS.Split(',');

            sqlStr.Add(string.Format("update pc_well set pjdy=''  where pjdy={0} and  qk='{1}' ", pjdy, qk));

            for (int i = 0; i < ids.Length; i++)
            {
                string id = ids[i];
                if (!string.IsNullOrEmpty(id) || id != "")
                    sqlStr.Add(string.Format("update pc_well set pjdy={0}  where id='{1}'", pjdy, id));
            }
            if (sqlhelper.ExecuteTranErrorCount(sqlStr) == -1)
            {
                return true;
            }
            else
                return false;

        }
    }
}
