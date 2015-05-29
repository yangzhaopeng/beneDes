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
    public class djkfsj_model
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
                where += " and kfsj.JH like '%" + searchWord.Replace("'", "") + "%'";
            }
            if (zyq != null && zyq != "")
            {
                where += " and kfsj.ZYQ='" + zyq + "'";
            }
         /*   if (zxz != null && zxz != "")
            {
                where += " and d.ZXZ='" + zxz + "'";
            }
            if (zrz != null && zrz != "")
            {
                where += " and d.ZRZ='" + zrz + "'";
            }
        */
   /*         string sql = @"select d.NY as NY, d.DJ_ID as DJ_ID, JH, d.ZYQ as ZYQ, dp.DEP_NAME as ZYQ_NAME,
                                  d.QK,d.CX, d.CYHD, d.SCSJ,d.JKCYL,d.JKCYOUL,d.HSCYL,d.LJCYL,
                                  d.JKCQL,d.HSCQL,d.LJCQL,d.HS,d.ZSL,d.LJZSL,d.ZQL,d.LJZQL,d.CYL,
                                  d.CSL,d.CYAOL,d.DZCSDM,y.DZCSMC,d.GYCSDM,x.GYCSMC,d.BZ,CYC_ID                                  
                           from KFSJ d, DEPARTMENT dp, DZCS_INFO y, GYCS_INFO x
                           where d.ZYQ=dp.DEP_ID and dp.DEP_TYPE='ZYQ' and dp.PARENT_ID=d.CYC_ID
                           and d.DZCSDM=y.DZCSDM and d.GYCSDM=x.GYCSDM 
                           and d.CYC_ID='" + cyc_id + "' and d.NY='" + month + "' " + where + " order by ZYQ,JH";
*/
            string sql = @"select * from (select a.*, ROWNUM RN FROM (SELECT distinct kfsj.ny,kfsj.jh,department.dep_name,kfsj.qk,kfsj.cx,
            kfsj.cyhd,kfsj.zql,kfsj.zsl,kfsj.cyl,kfsj.csl,kfsj.cyaol,kfsj.scsj, kfsj.dj_id,
            kfsj.jkcyl,kfsj.jkcyoul,kfsj.hscyl,kfsj.jkcql,kfsj.hscql,kfsj.hs,
            kfsj.ljcyl,kfsj.ljcql,kfsj.ljzsl,kfsj.ljzql,kfsj.dzcsdm,kfsj.gycsdm,kfsj.bz 
            FROM kfsj left join department on kfsj.zyq=department.dep_id";
            sql += " WHERE ny='" + month + "' and kfsj.cyc_id='" + cyc_id + "' and department.parent_id='" + cyc_id + "'" + where + " order by kfsj.JH ) a WHERE ROWNUM <= " + end + ") where RN >= " + begin + "";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }


        public static DataTable getCount(string cyc_id, string month, string searchWord, string zyq)
        {
            string where = "";

            if (searchWord != null && searchWord != "")
            {
                where += " and kfsj.JH like '%" + searchWord.Replace("'", "") + "%'";
            }
            if (zyq != null && zyq != "")
            {
                where += " and kfsj.ZYQ='" + zyq + "'";
            }
            /*   if (zxz != null && zxz != "")
               {
                   where += " and d.ZXZ='" + zxz + "'";
               }
               if (zrz != null && zrz != "")
               {
                   where += " and d.ZRZ='" + zrz + "'";
               }
           */
            /*         string sql = @"select d.NY as NY, d.DJ_ID as DJ_ID, JH, d.ZYQ as ZYQ, dp.DEP_NAME as ZYQ_NAME,
                                           d.QK,d.CX, d.CYHD, d.SCSJ,d.JKCYL,d.JKCYOUL,d.HSCYL,d.LJCYL,
                                           d.JKCQL,d.HSCQL,d.LJCQL,d.HS,d.ZSL,d.LJZSL,d.ZQL,d.LJZQL,d.CYL,
                                           d.CSL,d.CYAOL,d.DZCSDM,y.DZCSMC,d.GYCSDM,x.GYCSMC,d.BZ,CYC_ID                                  
                                    from KFSJ d, DEPARTMENT dp, DZCS_INFO y, GYCS_INFO x
                                    where d.ZYQ=dp.DEP_ID and dp.DEP_TYPE='ZYQ' and dp.PARENT_ID=d.CYC_ID
                                    and d.DZCSDM=y.DZCSDM and d.GYCSDM=x.GYCSDM 
                                    and d.CYC_ID='" + cyc_id + "' and d.NY='" + month + "' " + where + " order by ZYQ,JH";
         */
            string sql = @"SELECT distinct kfsj.ny,kfsj.jh,department.dep_name,kfsj.qk,kfsj.cx,
            kfsj.cyhd,kfsj.zql,kfsj.zsl,kfsj.cyl,kfsj.csl,kfsj.cyaol,kfsj.scsj,
            kfsj.jkcyl,kfsj.jkcyoul,kfsj.hscyl,kfsj.jkcql,kfsj.hscql,kfsj.hs,
            kfsj.ljcyl,kfsj.ljcql,kfsj.ljzsl,kfsj.ljzql,kfsj.dzcsdm,kfsj.gycsdm,kfsj.bz 
            FROM kfsj left join department on kfsj.zyq=department.dep_id";
            sql += " WHERE ny='" + month + "' and kfsj.cyc_id='" + cyc_id + "' and department.parent_id='" + cyc_id + "'" + where + " order by kfsj.JH ";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }
        /// <summary>
        /// 获取某一条单井信息数据
        /// </summary>
        public static DataTable getOneKFSJ(string ny, string cyc_id, string dj_id)
        {
            string sql = "select * from KFSJ where NY='" + ny + "' and CYC_ID='" + cyc_id + "' and DJ_ID='" + dj_id + "'";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 添加一条单井基础信息
        /// </summary>
        public static bool add(JObject obj, string cyc_id)
        {
            string sql = "insert into KFSJ (NY, DJ_ID, JH, ZYQ, QK,CX, CYHD, SCSJ, JKCYL,JKCYOUL, HSCYL, LJCYL, JKCQL, HSCQL,LJCQL, HS, ZSL, LJZSL, ZQL,LJZQL, CYL, CSL, CYAOL,DZCSDM,GYCSDM,BZ,CYC_ID) values ('" +
                         obj["NY"] + "','" +
                         obj["DJ_ID"] + "','" +
                         obj["JH"] + "','" +
                         obj["ZYQ"] + "','" +
                         obj["QK"] + "','" +
                         obj["CX"] + "','" +
                         obj["CYHD"] + "','" +
                         obj["SCSJ"] + "'," +
                         obj["JKCYL"] + "," +
                         obj["JKCYOUL"] + "," +
                         obj["HSCYL"] + ",'" +
                         obj["LJCYL"] + "','" +
                         obj["JKCQL"] + "','" +
                         obj["HSCQL"] + "','" +
                         obj["LJCQL"] + "','" +
                         obj["HS"] + "','" +
                         obj["ZSL"] + "','" +
                         obj["LJZSL"] + "','" +
                         obj["ZQL"] + "','" +
                         obj["LJZQL"] + "','" +
                         obj["CYL"] + "','" +
                         obj["CSL"] + "','" +
                         obj["CYAOL"] + "','" +
                         obj["DZCS"] + "','" +
                         obj["GYCS"] + "','" +
                         obj["BZ"] + "','" +
                         cyc_id + "')";

            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 编辑单井开发数据信息
        /// </summary>
        public static bool edit(JObject obj, string cyc_id)
        {
            string sql = "update KFSJ set " +
                         "DJ_ID='" + obj["DJ_ID_new"] + "', " +
                         "JH='" + obj["JH"] + "', " +
                         "ZYQ='" + obj["ZYQ"] + "', " +
                         "QK='" + obj["QK"] + "', " +
                         "CX='" + obj["CX"] + "', " +
                         "CYHD='" + obj["CYHD"] + "', " +
                         "SCSJ='" + obj["SCSJ"] + "', " +
                         "JKCYL=" + obj["JKCYL"] + ", " +
                         "JKCYOUL=" + obj["JKCYOUL"] + ", " +
                         "HSCYL=" + obj["HSCYL"] + ", " +
                         "LJCYL='" + obj["LJCYL"] + "', " +
                         "JKCQL='" + obj["JKCQL"] + "', " +
                         "HSCQL='" + obj["HSCQL"] + "', " +
                         "LJCQL='" + obj["LJCQL"] + "', " +
                         "HS='" + obj["HS"] + "', " +
                         "ZSL='" + obj["ZSL"] + "', " +
                         "LJZSL='" + obj["LJZSL"] + "', " +
                         "ZQL='" + obj["ZQL"] + "', " +
                         "LJZQL='" + obj["LJZQL"] + "', " +
                         "CYL='" + obj["CYL"] + "', " +
                         "CSL='" + obj["CSL"] + "', " +
                         "CYAOL='" + obj["CYAOL"] + "', " +
                         "DZCSDM='" + obj["DZCSDM"] + "', " +
                         "GYCSDM='" + obj["GYCSDM"] + "', " +
                         "BZ='" + obj["BZ"] + "' " +
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
            string sql = "delete from KFSJ where CYC_ID='" + cyc_id + "' and NY='" + ny + "' and DJ_ID='" + dj_id + "'";

            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }
    }
}
