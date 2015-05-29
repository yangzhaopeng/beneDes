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
    public class zxzfy_model
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
        public static DataTable getWellList(string cyc_id, string month, string searchWord, int end, int begin)
        {
            //string id, type;
            string where = "";

            //if (searchWord != null && searchWord != "")
            //{
            //    where += " and d.JH like '%" + searchWord.Replace("'", "") + "%'";
            //}
            //if (zyq != null && zyq != "")
            //{
            //    where += " and d.ZYQ='" + zyq + "'";
            //}
            //if (zxz != null && zxz != "")
            //{
            //    where += " and d.ZXZ='" + zxz + "'";
            //}
            //if (zrz != null && zrz != "")
            //{
            //    where += " and d.ZRZ='" + zrz + "'";
            //}
            string sql = @"select * from (select a.*, ROWNUM RN FROM(select distinct a.FEE_NAME, round(a.FEE,2) from VIEW_GGFY a,GGFY b   where a.FT_TYPE=b.FT_TYPE and a.NY='" + month + "' and a.SOURCE_ID=b.SOURCE_ID and a.CYC_ID='" + cyc_id + "' order by a.fee_class ) a WHERE ROWNUM <= " + end + ") where RN >= " + begin + "";
            //string sql = @"select * from (select a.*, ROWNUM RN FROM(select FEE_NAME, round(FEE,2) from VIEW_GGFY   where FT_TYPE='" + type + "' and NY='" + month + "' and SOURCE_ID='" + id + "' and CYC_ID='" + cyc_id + "' order by fee_class ) a WHERE ROWNUM <= " + end + ") where RN >= " + begin + "";
            //            string sql = @"select * from (select a.*, ROWNUM RN FROM (select d.NY as NY, d.DJ_ID as DJ_ID, JH, d.ZYQ as ZYQ, dp.DEP_NAME as ZYQ_NAME,
            //                                   ZJCLF, QTCLF, ZJRLF, ZJDLF, QTDLF, 
            //                                   QYWZRF, QYWZRF_RYF, CSZYF, WHXZYLWF, SJZYF, 
            //                                   CJCSF, YCJCF, WHXLF, YQCLF, YQCLF_RYF, 
            //                                   QTHSF, YSF, LYF,QTZJF,CKGLF,ZJRYF,ZYYQCP,ZJZH, CYC_ID
            //                                 
            //                           from FEE_CROSS d, DEPARTMENT dp
            //                           where d.ZYQ=dp.DEP_ID and dp.DEP_TYPE='ZYQ' and dp.PARENT_ID=d.CYC_ID
            //                           
            //                           and d.CYC_ID='" + cyc_id + "' and d.NY='" + month + "' " + where + " order by ZYQ,JH) a WHERE ROWNUM <= " + end + ") where RN >= " + begin + "";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }


        public static DataTable getCount(string cyc_id, string month, string searchWord)
        {
            //  string id, type;
            string where = "";

            //if (searchWord != null && searchWord != "")
            //{
            //    where += " and d.JH like '%" + searchWord.Replace("'", "") + "%'";
            //}
            //if (zyq != null && zyq != "")
            //{
            //    where += " and d.ZYQ='" + zyq + "'";
            //}
            //if (zxz != null && zxz != "")
            //{
            //    where += " and d.ZXZ='" + zxz + "'";
            //}
            //if (zrz != null && zrz != "")
            //{
            //    where += " and d.ZRZ='" + zrz + "'";
            //}
            //  string sql = @"select * from (select a.*, ROWNUM RN FROM(select FEE_NAME, round(FEE,2) from VIEW_GGFY a,GGFY b   where a.FT_TYPE=b.FT_TYPE and NY='" + month + "' and a.SOURCE_ID=b.SOURCE_ID and CYC_ID='" + cyc_id + "' order by fee_class ) a WHERE ROWNUM <= " + end + ") where RN >= " + begin + "";

            string sql = @"select distinct a.FEE_NAME, round(a.FEE,2) from VIEW_GGFY a,GGFY b   where a.FT_TYPE=b.FT_TYPE and a.NY='" + month + "' and a.SOURCE_ID=b.SOURCE_ID and a.CYC_ID='" + cyc_id + "' order by a.fee_class ";
            //            string sql = @"select d.NY as NY, d.DJ_ID as DJ_ID, JH, d.ZYQ as ZYQ, dp.DEP_NAME as ZYQ_NAME,
            //                                  ZJCLF, QTCLF, ZJRLF, ZJDLF, QTDLF, 
            //                                   QYWZRF, QYWZRF_RYF, CSZYF, WHXZYLWF, SJZYF, 
            //                                   CJCSF, YCJCF, WHXLF, YQCLF, YQCLF_RYF, 
            //                                   QTHSF, YSF, LYF,QTZJF,CKGLF,ZJRYF,ZYYQCP,ZJZH, CYC_ID
            //                                 
            //                           from FEE_CROSS d, DEPARTMENT dp
            //                           where d.ZYQ=dp.DEP_ID and dp.DEP_TYPE='ZYQ' and dp.PARENT_ID=d.CYC_ID
            //                           
            //                           and d.CYC_ID='" + cyc_id + "' and d.NY='" + month + "' " + where + " order by ZYQ,JH";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }
        /// <summary>
        /// 获取某一条直接费用数据
        /// </summary>
        public static DataTable getOnecycfy(string ny, string cyc_id, string dep_id)
        {
            string sql = "select * from VIEW_GGFY where NY='" + ny + "' and CYC_ID='" + cyc_id + "' and DEP_ID='" + dep_id + "'";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 添加一条单井直接费用
        /// </summary>
        //public static bool add(JObject obj, string cyc_id)
        //{
        //    string sql = "insert into FEE_CROSS (NY, DJ_ID, JH, ZYQ, ZJCLF, QTCLF, ZJRLF, ZJDLF, QTDLF, QYWZRF, QYWZRF_RYF, CSZYF, WHXZYLWF, SJZYF, CJCSF, YCJCF, WHXLF, YQCLF, YQCLF_RYF,QTHSF, YSF, LYF,QTZJF,CKGLF,ZJRYF,ZYYQCP,ZJZH, CYC_ID) values ('" +
        //                 obj["NY"] + "','" +
        //                 obj["DJ_ID"] + "','" +
        //                 obj["JH"] + "','" +
        //                 obj["ZYQ"] + "','" +
        //        //obj["QK"] + "','" +
        //        //obj["ZXZ"] + "','" +
        //        //obj["ZRZ"] + "','" +
        //                 obj["ZJCLF"] + "','" +
        //                 obj["QTCLF"] + "','" +
        //                 obj["ZJRLF"] + "','" +
        //                 obj["ZJDLF"] + "'," +
        //                 obj["QTDLF"] + "," +
        //                 obj["QYWZRF"] + "," +
        //                 obj["QYWZRF_RYF"] + ",'" +
        //                 obj["CSZYF"] + "','" +
        //                 obj["WHXZYLWF"] + "','" +
        //                 obj["SJZYF"] + "','" +
        //                 obj["CJCSF"] + "','" +
        //                 obj["YCJCF"] + "','" +
        //                 obj["WHXLF"] + "','" +
        //                 obj["YQCLF"] + "','" +
        //                 obj["YQCLF_RYF"] + "','" +
        //                 obj["QTHSF"] + "','" +
        //                 obj["YSF"] + "','" +
        //                 obj["LYF"] + "','" +
        //                 obj["QTZJF"] + "','" +
        //                 obj["CKGLF"] + "','" +
        //                 obj["ZJRYF"] + "','" +
        //                 obj["ZYYQCP"] + "','" +
        //                 obj["ZJZH"] + "','" +
        //                 cyc_id + "')";

        //    SqlHelper sqlhelper = new SqlHelper();

        //    if (sqlhelper.ExcuteSql(sql) > 0) return true;
        //    return false;
        //}

        /// <summary>
        /// 编辑单井直接费用
        /// </summary>
        public static bool edit(JObject obj, string fee_class, string source_id)
        {
            string sql = @"update GGFY set " +
                          "FEE='" + obj["FEE"] + "', " +
                          "where FEE_CLASS='" + fee_class + "' and NY='" + obj["NY"] + "' and SOURCE_ID='" + source_id + "'";

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
            string sql = "delete from FEE_CROSS where CYC_ID='" + cyc_id + "' and NY='" + ny + "' and DJ_ID='" + dj_id + "'";

            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }
    }
}

