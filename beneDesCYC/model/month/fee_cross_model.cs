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
using System.Text;
using System.Collections.Generic;

namespace beneDesCYC.model.month
{
    public class fee_cross_model
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
        public static DataTable getWellList(string cyc_id, string cqc_id, string month, string searchWord, string zyq, int end, int begin)
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
            //if (zxz != null && zxz != "")
            //{
            //    where += " and d.ZXZ='" + zxz + "'";
            //}
            //if (zrz != null && zrz != "")
            //{
            //    where += " and d.ZRZ='" + zrz + "'";
            //}

            //费用列表不一致，这部分代码是吉林的直接费用列表  @yzp
            //            string sql = @"select * from (select a.*, ROWNUM RN FROM (select d.NY as NY, d.DJ_ID as DJ_ID, JH, d.ZYQ as ZYQ, dp.DEP_NAME as ZYQ_NAME,
            //                                   ZJCLF, QTCLF, ZJRLF, QTDLF, 
            //                                   QYWZRF, QYWZRF_RYF, CSZYF, WHXZYLWF, SJZYF, 
            //                                   CJCSF, YCJCF, WHXLF, YQCLF, YQCLF_RYF, 
            //                                   QTHSF, YSF, LYF,QTZJF,CKGLF,ZJRYF,ZYYQCP,ZJZH, d.CYC_ID
            //                                 
            //                           from FEE_CROSS d, DEPARTMENT dp
            //                           where d.ZYQ=dp.DEP_ID and dp.DEP_TYPE='ZYQ' and dp.PARENT_ID=d.CYC_ID
            //                           
            //                           and (d.CYC_ID='" + cyc_id + "' or d.CYC_ID='" + cqc_id + "') and d.NY='" + month + "' " + where + " order by ZYQ,JH) a WHERE ROWNUM <= " + end + ") where RN >= " + begin + "";
            //青海的直接费用列表  @yzp
            string sql = @"select * from (select a.*, ROWNUM RN FROM (select d.NY as NY, d.DJ_ID as DJ_ID, JH, d.ZYQ as ZYQ, dp.DEP_NAME as ZYQ_NAME,
                                    QTCLF, ZJRLF,  QTDLF, QYWZRF, QYWZRF_RYF, CSXZYLWF, WHXZYLWF, SJZYF, CJCSF, CJSJF_RYF, QTWHJXLF, WHXLF_RYF, YQCLF, YQCLF_RYF, QTHSF, YSF, YSF_RYF, LYF, QTZJF, CKGLF, ZJRYF as RGCB, ZYYQCP, ZJZH, DDF, JJF, QTCSF, QTTCF, QTYSF, SKF, TCF, XJF, XLF, YBCLF
                           from FEE_CROSS d, DEPARTMENT dp
                           where d.ZYQ=dp.DEP_ID  and dp.PARENT_ID=d.CYC_ID
                           
                           and (d.CYC_ID='" + cyc_id + "' or d.CYC_ID='" + cqc_id + "') and d.NY='" + month + "' " + where + " order by ZYQ,JH) a WHERE ROWNUM <= " + end + ") where RN >= " + begin + "";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }


        public static DataTable getCount(string cyc_id, string cqc_id, string month, string searchWord, string zyq)
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
            //if (zxz != null && zxz != "")
            //{
            //    where += " and d.ZXZ='" + zxz + "'";
            //}
            //if (zrz != null && zrz != "")
            //{
            //    where += " and d.ZRZ='" + zrz + "'";
            //}

            string sql = @"select d.NY as NY, d.DJ_ID as DJ_ID, JH, d.ZYQ as ZYQ, dp.DEP_NAME as ZYQ_NAME,
                                  QTCLF, ZJRLF, QTDLF, QYWZRF, QYWZRF_RYF, CSXZYLWF, WHXZYLWF, SJZYF, CJCSF, CJSJF_RYF, YCJCF, QTWHJXLF, WHXLF_RYF, YQCLF, YQCLF_RYF, QTHSF, YSF, YSF_RYF, LYF, QTZJF, CKGLF, ZJRYF, ZYYQCP, ZJZH, DDF, JJF, QTCSF, QTTCF, QTYSF, SKF, TCF, XJF, XLF, YBCLF
                           from FEE_CROSS d, DEPARTMENT dp
                           where d.ZYQ=dp.DEP_ID  and dp.PARENT_ID=d.CYC_ID
                           and (d.CYC_ID='" + cyc_id + "' or d.CYC_ID='" + cqc_id + "')  and d.NY='" + month + "' " + where + " order by ZYQ,JH";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }
        /// <summary>
        /// 获取某一条直接费用数据
        /// </summary>
        public static DataTable getOneZjfybj(string ny, string cyc_id, string dj_id)
        {
            string sql = "select * from fee_cross where NY='" + ny + "' and CYC_ID='" + cyc_id + "' and DJ_ID='" + dj_id + "'";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 添加一条单井直接费用
        /// </summary>
        public static bool add(JObject obj, string cyc_id)
        {
            string sql = "insert into FEE_CROSS (NY, DJ_ID, JH, ZYQ, QTCLF, ZJRLF,  QTDLF, QYWZRF, QYWZRF_RYF, CSXZYLWF, WHXZYLWF, SJZYF, CJCSF, YCJCF, WHXLF, YQCLF, YQCLF_RYF,QTHSF, YSF, LYF,QTZJF,CKGLF,ZJRYF,ZYYQCP,ZJZH, CYC_ID) values ('" +
                         obj["NY"] + "','" +
                         obj["DJ_ID"] + "','" +
                         obj["JH"] + "','" +
                         obj["ZYQ"] + "','" +
                //obj["QK"] + "','" +
                //obj["ZXZ"] + "','" +
                //obj["ZRZ"] + "','" +
                         obj["QTCLF"] + "','" +
                         obj["ZJRLF"] + "','" +
                         obj["QTDLF"] + "," +
                         obj["QYWZRF"] + "," +
                         obj["QYWZRF_RYF"] + ",'" +
                         obj["CSXZYLWF"] + "','" +
                         obj["WHXZYLWF"] + "','" +
                         obj["SJZYF"] + "','" +
                         obj["CJCSF"] + "','" +
                         obj["YCJCF"] + "','" +
                         obj["WHXLF"] + "','" +
                         obj["YQCLF"] + "','" +
                         obj["YQCLF_RYF"] + "','" +
                         obj["QTHSF"] + "','" +
                         obj["YSF"] + "','" +
                         obj["LYF"] + "','" +
                         obj["QTZJF"] + "','" +
                         obj["CKGLF"] + "','" +
                         obj["ZJRYF"] + "','" +
                         obj["ZYYQCP"] + "','" +
                         obj["ZJZH"] + "','" +
                         cyc_id + "')";

            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 编辑单井直接费用
        /// </summary>
        public static bool edit(JObject obj, string cyc_id)
        {
            bool result = false;
            SqlHelper sqlhelper = new SqlHelper();
            string FT_TYPE = "DJ";
            #region 行不通  因为费用项可能是更新也可能是插入 @yzp
            //StringBuilder sb = new StringBuilder();
            //int temp = 0;
            //if (obj["ZJCLF"] != null && int.TryParse(obj["ZJCLF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,ZJCLF='" + obj["ZJCLF"] + "'");
            //if (obj["QTCLF"] != null && int.TryParse(obj["QTCLF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,QTCLF='" + obj["QTCLF"] + "'");
            //if (obj["ZJRLF"] != null && int.TryParse(obj["ZJRLF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,ZJRLF='" + obj["ZJRLF"] + "'");
            //if (obj["ZJDLF"] != null && int.TryParse(obj["ZJDLF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,ZJDLF='" + obj["ZJDLF"] + "'");
            //if (obj["QTDLF"] != null && int.TryParse(obj["QTDLF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,QTDLF='" + obj["QTDLF"] + "'");
            //if (obj["QYWZRF"] != null && int.TryParse(obj["QYWZRF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,QYWZRF='" + obj["QYWZRF"] + "'");
            //if (obj["CSXZYLWF"] != null && int.TryParse(obj["CSXZYLWF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,CSXZYLWF='" + obj["CSXZYLWF"] + "'");
            //if (obj["WHXZYLWF"] != null && int.TryParse(obj["WHXZYLWF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,WHXZYLWF='" + obj["WHXZYLWF"] + "'");
            //if (obj["SJZYF"] != null && int.TryParse(obj["SJZYF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,SJZYF='" + obj["SJZYF"] + "'");
            //if (obj["CJCSF"] != null && int.TryParse(obj["CJCSF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,CJCSF='" + obj["CJCSF"] + "'");
            //if (obj["QTWHJXLF"] != null && int.TryParse(obj["QTWHJXLF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,QTWHJXLF='" + obj["QTWHJXLF"] + "'");
            //if (obj["YQCLF"] != null && int.TryParse(obj["YQCLF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,YQCLF='" + obj["YQCLF"] + "'");
            //if (obj["QTHSF"] != null && int.TryParse(obj["QTHSF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,QTHSF='" + obj["QTHSF"] + "'");
            //if (obj["YSF"] != null && int.TryParse(obj["YSF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,YSF='" + obj["YSF"] + "'");
            //if (obj["LYF"] != null && int.TryParse(obj["LYF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,LYF='" + obj["LYF"] + "'");
            //if (obj["QTZJF"] != null && int.TryParse(obj["QTZJF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,QTZJF='" + obj["QTZJF"] + "'");
            //if (obj["CKGLF"] != null && int.TryParse(obj["CKGLF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,CKGLF='" + obj["CKGLF"] + "'");
            //if (obj["ZJRYF"] != null && int.TryParse(obj["ZJRYF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,ZJRYF='" + obj["ZJRYF"] + "'");
            //if (obj["ZYYQCP"] != null && int.TryParse(obj["ZYYQCP"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,ZYYQCP='" + obj["ZYYQCP"] + "'");
            //if (obj["ZJZH"] != null && int.TryParse(obj["ZJZH"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,ZJZH='" + obj["ZJZH"] + "'");
            //if (obj["DDF"] != null && int.TryParse(obj["DDF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,DDF='" + obj["DDF"] + "'");
            //if (obj["JJF"] != null && int.TryParse(obj["JJF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,JJF='" + obj["JJF"] + "'");
            //if (obj["QTCSF"] != null && int.TryParse(obj["QTCSF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,QTCSF='" + obj["QTCSF"] + "'");
            //if (obj["QTTCF"] != null && int.TryParse(obj["QTTCF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,QTTCF='" + obj["QTTCF"] + "'");
            //if (obj["QTYSF"] != null && int.TryParse(obj["QTYSF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,QTYSF='" + obj["QTYSF"] + "'");
            //if (obj["SKF"] != null && int.TryParse(obj["SKF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,SKF='" + obj["SKF"] + "'");
            //if (obj["TCF"] != null && int.TryParse(obj["TCF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,TCF='" + obj["TCF"] + "'");
            //if (obj["XJF"] != null && int.TryParse(obj["XJF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,XJF='" + obj["XJF"] + "'");
            //if (obj["XLF"] != null && int.TryParse(obj["XLF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,XLF='" + obj["XLF"] + "'");
            //if (obj["YBCLF"] != null && int.TryParse(obj["YBCLF"].ToString(), out temp) && temp != 0)
            //    sb.Append(" ,YBCLF='" + obj["YBCLF"] + "'");
            //sb.Append(" where NY='" + obj["NY"].ToString() + "' and cyc_id='" + cyc_id + "' and dj_id='" + obj["DJ_ID"] + "'");
            #endregion

            #region @yzp 需要更新djfy才能 影响评价过程
            //string sql = "update FEE_CROSS set " +
            //           "DJ_ID='" + obj["DJ_ID_new"] + "', " +
            //           "JH='" + obj["JH"] + "', " +
            //           "ZYQ='" + obj["ZYQ"] + "', " +
            //           "ZJCLF='" + obj["ZJCLF"] + "', " +
            //           "QTCLF='" + obj["QTCLF"] + "', " +
            //           "ZJRLF='" + obj["ZJRLF"] + "', " +
            //           "ZJDLF='" + obj["ZJDLF"] + "', " +
            //           "QTDLF=" + obj["QTDLF"] + ", " +
            //           "QYWZRF=" + obj["QYWZRF"] + ", " +
            //           "QYWZRF_RYF=" + obj["QYWZRF_RYF"] + ", " +
            //           "CSXZYLWF='" + obj["CSXZYLWF"] + "', " +
            //           "WHXZYLWF='" + obj["WHXZYLWF"] + "', " +
            //           "SJZYF='" + obj["SJZYF"] + "', " +
            //           "CJCSF='" + obj["CJCSF"] + "', " +
            //           "YCJCF='" + obj["YCJCF"] + "', " +
            //           "WHXLF='" + obj["WHXLF"] + "', " +
            //           "YQCLF='" + obj["YQCLF"] + "', " +
            //           "YQCLF_RYF='" + obj["YQCLF_RYF"] + "', " +
            //           "QTHSF='" + obj["QTHSF"] + "', " +
            //           "YSF='" + obj["YSF"] + "' ," +
            //           "LYF='" + obj["LYF"] + "', " +
            //           "QTZJF='" + obj["QTZJF"] + "', " +
            //           "CKGLF='" + obj["CKGLF"] + "' ," +
            //           "ZJRYF='" + obj["ZJRYF"] + "', " +
            //           "ZYYQCP='" + obj["ZYYQCP"] + "' ," +
            //           "ZJZH='" + obj["ZJZH"] + "' " +
            //           "where CYC_ID='" + cyc_id + "' and NY='" + obj["NY"] + "' and DJ_ID='" + obj["DJ_ID"] + "'";

            //if (sqlhelper.ExcuteSql(sql) > 0) return true;
            #endregion


            string insertOrupdate = getUpdateString(obj, cyc_id);

            //将单井数据整理到fee_cross
            string month = obj["NY"].ToString();
            try
            {
                if (sqlhelper.ExcuteSql(insertOrupdate) > 0)
                {
                    sqlhelper.ExcuteSql("begin CROSS_FEE1('" + month + "','" + cyc_id + "','" + FT_TYPE + "');end;");
                    result = true;
                }
            }
            catch (Exception ex)
            { }

            return result;
        }
        /// <summary>
        /// 生成 增加、更新语句 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        private static string getUpdateString(JObject obj, string cyc_id)
        {
            string insertOrUpdate = "begin ";
            string ny = obj["NY"].ToString();
            string ft_type = "DJ";
            string dj_id = obj["ZYQ"].ToString() + "$" + obj["JH"].ToString();
            string dep_id = obj["ZYQ"].ToString();
            string source_id = obj["ZYQ"].ToString();

            StringBuilder insertStr = new StringBuilder();
            StringBuilder updateStr = new StringBuilder();


            JEnumerable<JToken> jet = obj.Children();
            List<JToken> jtk = jet.ToList();
            for (int i = 3; i < jtk.Count; i++)
            {
                JProperty jp = (JProperty)jtk[i];
                string fee_code = jp.Name.ToLower();
                string fee_class = getFeeClass(fee_code);
                double fee = 0;
                if (double.TryParse(jp.Value.ToString(), out fee) && fee != 0)
                    if (isFeeExis(ny, dj_id, ft_type, cyc_id, fee_class, fee_code))
                    {
                        updateStr.Append("update djfy set fee='" + fee + "' ");
                        updateStr.Append("where ny='" + ny + "' and ft_type='DJ' and dj_id='" + dj_id + "' and fee_class='" + fee_class + "' and fee_code='" + fee_code + "';\r\n");

                    }
                    else
                    {
                        insertStr.Append("insert into djfy(ny, dep_id, ft_type, source_id, dj_id, zqlc, fee_class, fee_code, fee, cyc_id)");
                        insertStr.Append("values('" + ny + "','" + dep_id + "','" + ft_type + "','" + source_id + "','" + dj_id + "','" + 0 + "','" + fee_class + "','" + fee_code + "','" + fee + "','" + cyc_id + "');\r\n");

                    };
            }
            insertOrUpdate += insertStr.Append(updateStr).ToString().Trim();
            insertOrUpdate += " commit; end;";
            return insertOrUpdate;
        }

        private static string getFeeClass(string fee_code)
        {
            string sql = "select fee_class from fee_name where fee_code='" + fee_code.ToLower() + "'";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetExecuteScalar(sql).ToString();
        }
        private static bool isFeeExis(string ny, string dj_id, string ft_Type, string cyc_id, string fee_class, string fee_code)
        {
            bool result = false;

            string sql = "select count(3) from djfy where NY='" + ny + "' and CYC_ID='" + cyc_id + "' and DJ_ID='" + dj_id + "' and  fee_class='" + fee_class.ToLower() + "' and fee_code='" + fee_code + "'";

            SqlHelper sqlhelper = new SqlHelper();
            int rows = int.Parse(sqlhelper.GetExecuteScalar(sql).ToString());
            if (rows > 0)
                result = true;
            return result;
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

