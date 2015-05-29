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
using System.Collections.Generic;

namespace beneDesCYC.model.month
{
    public class xsqdRelateQ_model
    {
        /// <summary>
        /// 获取某采油厂下所有的销售参数信息列表
        /// </summary>
        /// <param name="cyc_id"></param>
        public static DataTable getAllList(string cyc_id, string month)
        {
            string sql = @"select s.NY,  s.XSYPDM as XSYPDM, y.XSYPMC, JG,SJ,yqspl.ZYS,  yqspl.YQSPL,dtb.dtb
from XSCS_INFO s, XSYP_INFO y,dtb_info dtb,yqspl_info yqspl
where s.XSYPDM=y.XSYPDM    
and s.ny=dtb.ny  and s.cyc_id=dtb.cyc_id and s.xsypdm=dtb.xsypdm 
and s.ny=yqspl.ny and s.cyc_id=yqspl.cyc_id and s.XSYPDM=yqspl.XSYPDM
      and s.CYC_ID='" + cyc_id + "' order by NY desc";
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
        public static DataTable getOneXSYP(string ny, string xsypdm, string cyc_id)
        {
            string sql = "select * from XSCS_INFO where NY='" + ny + "'  and XSYPDM='" + xsypdm + "' and CYC_ID='" + cyc_id + "'";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 添加一条销售油品相关信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="sc"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool add(string ny, string xsypdm, string dtb,  string jg, string sj, string yqspl, string zys, string cyc_id)
        {
            SqlHelper sqlhelper = new SqlHelper();
            List<String> lsSql = new List<string>();
            //销售参数
            string XSCS_INFO = "insert into XSCS_INFO (NY,XSYPDM,JG,SJ,CYC_ID) values ('" +
                         ny + "','" +
                         xsypdm + "'," +
                         jg + "," +
                         sj + ",'" +
                         cyc_id + "')";
            lsSql.Add(XSCS_INFO);
          
            //吨桶比 
            string DTB_INFO = "insert into DTB_INFO (NY,XSYPDM,DTB,CYC_ID) values ('" +
                        ny + "','" +

                        xsypdm + "'," +
                        dtb + ",'" +
                        cyc_id + "')";
            lsSql.Add(DTB_INFO);
           //油气商品量
            string YQSPL_INFO = "insert into YQSPL_INFO (NY,XSYPDM,YQSPL,ZYS,CYC_ID) values ('" +
                        ny + "','" +
                        xsypdm + "'," +
                        yqspl + "," +
                        zys + ",'" +
                        cyc_id + "')";
            lsSql.Add(YQSPL_INFO);

            if (sqlhelper.ExecuteTranErrorCount(lsSql) == -1)
                return true;
            else
                //if (sqlhelper.ExcuteSql(sql) > 0) return true;
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
        public static bool edit(string ny, string xsypdm, string dtb,  string jg, string sj, string yqspl, string zys, string cyc_id)
        {
            SqlHelper sqlhelper = new SqlHelper();
            List<String> lsSql = new List<string>();
            //销售参数
            string XSCS_INFO = "update XSCS_INFO  set JG=" + jg + " , SJ=" + sj + " where NY='" + ny + "'  and  XSYPDM='" + xsypdm + "' and CYC_ID='" + cyc_id + "'";
            lsSql.Add(XSCS_INFO);
           
            //吨桶比 
            string DTB_INFO = "update DTB_INFO  set DTB=" + dtb + " where NY='" + ny + "'  and XSYPDM='" + xsypdm + "' and CYC_ID='" + cyc_id + "'";
            lsSql.Add(DTB_INFO);
          //油气商品量
            string YQSPL_INFO = "update YQSPL_INFO  set YQSPL=" + yqspl + " ,ZYS=" + zys + " where NY='" + ny + "'  and XSYPDM='" + xsypdm + "' and CYC_ID='" + cyc_id + "'";
            lsSql.Add(YQSPL_INFO);
            if (sqlhelper.ExecuteTranErrorCount(lsSql) == -1)
                return true;
            else
                //if (sqlhelper.ExcuteSql(sql) > 0) return true;
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
            SqlHelper sqlhelper = new SqlHelper();
            List<String> lsSql = new List<string>();
            string XSCS_INFO = "delete from XSCS_INFO  where NY='" + ny + "'  and XSYPDM='" + xsypdm + "' and CYC_ID='" + cyc_id + "'";
            lsSql.Add(XSCS_INFO);
           
            //吨桶比 
            string DTB_INFO = "delete from DTB_INFO  where NY='" + ny + "'  and XSYPDM='" + xsypdm + "' and CYC_ID='" + cyc_id + "'";
            lsSql.Add(DTB_INFO);
           
            //油气商品率
            string YQSPL_INFO = "delete from YQSPL_INFO  where NY='" + ny + "'  and XSYPDM='" + xsypdm + "' and CYC_ID='" + cyc_id + "'";
            lsSql.Add(YQSPL_INFO);

            if (sqlhelper.ExecuteTranErrorCount(lsSql) == -1)
                return true;
            else
                //if (sqlhelper.ExcuteSql(sql) > 0) return true;
                return false;
        }
        /// <summary>
        /// 删除多条销售参数信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool deleteMutilRow(List<string> ny, List<string> xsypdm, string cyc_id)
        {
            SqlHelper sqlhelper = new SqlHelper();
            List<String> lsSql = new List<string>();
            if (ny.Count == xsypdm.Count)
            {
                for (int i = 0; i < ny.Count; i++)
                {
                    string XSCS_INFO = "delete from XSCS_INFO  where NY='" + ny[i] + "'  and XSYPDM='" + xsypdm[i] + "' and CYC_ID='" + cyc_id + "'";
                    lsSql.Add(XSCS_INFO);
                    //吨桶比 
                    string DTB_INFO = "delete from DTB_INFO  where NY='" + ny[i] + "'  and XSYPDM='" + xsypdm[i] + "' and CYC_ID='" + cyc_id + "'";
                    lsSql.Add(DTB_INFO);
                    //油气商品量
                    string YQSPL_INFO = "delete from YQSPL_INFO  where NY='" + ny[i] + "'  and XSYPDM='" + xsypdm[i] + "' and CYC_ID='" + cyc_id + "'";
                    lsSql.Add(YQSPL_INFO);
                }
            }
            if (sqlhelper.ExecuteTranErrorCount(lsSql) == -1)
                return true;
            else
                //if (sqlhelper.ExcuteSql(sql) > 0) return true;
                return false;
        }
    }
}
