﻿using System;
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
    public class qtpjdy_model
    {
        /// <summary>
        /// 获取某采油厂下的评价单元信息
        /// </summary>
        public static DataTable getAllList(string cyc_id, string month)
        {
            string sql = @"select distinct NY ,PJDYMC,SSYT,YCLX, DYHYMJ, DYDZCL ,DYKCCL, YCZS,PJSTL, PJKXD, LHQHL,NXYHL, YSDCYL, MQDCYL, YCLX, KFJD,KFFS, QJZJS, QJKJS, CCLX, SSYT, SFPJ, CYC_ID
                           from  PJDYSJ s
                           where  CYC_ID='" + cyc_id + "' and NY='" + month + "' order by NY desc";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /*    /// <summary>
            /// 根据查询条件获取所有区块信息
            /// </summary>
            public static DataTable getWellList(string cyc_id, string month)
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
            */
        /// <summary>
        /// 获取某一条评价单元信息数据
        /// </summary>
        public static DataTable getOnePJDY(string ny, string cyc_id, string pjdymc)
        {
            string sql = "select * from PJDYSJ where NY='" + ny + "' and CYC_ID='" + cyc_id + "' and PJDYMC='" + pjdymc + "' ";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 添加一条评价单元信息
        /// </summary>
        public static bool add(string NY, string PJDYMC, string SSYT, string YCLX, string DYHYMJ, string DYDZCL, string DYKCCL, string YCZS, string PJSTL, string PJKXD, string LHQHL, string NXYHL, string YSDCYL, string MQDCYL, string KFJD, string KFFS, string QJZJS, string QJKJS, string CCLX, string SFPJ, string CYC_ID)
        {
            string sql = "insert into PJDYSJ (NY, PJDYMC, SSYT, YCLX, DYHYMJ, DYDZCL, DYKCCL, YCZS, PJSTL, PJKXD, LHQHL, NXYHL, YSDCYL, MQDCYL, YCLX, KFJD, KFFS, QJZJS, QJKJS, CCLX, SSYT, SFPJ, CYC_ID) values ('" +

                  NY + "','" + PJDYMC + "','" + SSYT + "','" + YCLX + "','" + DYHYMJ + "','" + DYDZCL + "','" + DYKCCL + "','" + YCZS + "','" + PJSTL + "','" + PJKXD + "','" + LHQHL + "','" + NXYHL + "','" + YSDCYL + "','" + MQDCYL + "','" + KFJD + "','" + KFFS + "','" + QJZJS + "','" + QJKJS + "','" + CCLX + "','" + SFPJ + "','" + CYC_ID + "')";


            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 编辑评价单元信息
        /// </summary>
        public static bool edit(string NY, string PJDYMC, string SSYT, string YCLX, string DYHYMJ, string DYDZCL, string DYKCCL, string YCZS, string PJSTL, string PJKXD, string LHQHL, string NXYHL, string YSDCYL, string MQDCYL, string KFJD, string KFFS, string QJZJS, string QJKJS, string CCLX, string SFPJ, string CYC_ID)
        {

            string SQL = "UPDATE PJDYSJ SET SSYT= '" + SSYT +
                "', YCLX='" + YCLX +
                "', DYHYMJ='" + DYHYMJ +
                "', DYDZCL='" + DYDZCL +
                "',DYKCCL='" + DYKCCL +
                "',YCZS='" + YCZS +
                "',PJSTL='" + PJSTL +
                "', PJKXD='" + PJKXD +
                "',LHQHL='" + LHQHL +
                "',NXYHL='" + NXYHL +
                "',YSDCYL='" + YSDCYL +
                "',MQDCYL='" + MQDCYL +
                "',KFJD='" + KFJD +
                "',KFFS='" + KFFS +
                "',QJZJS='" + QJZJS +
                "',QJKJS='" + QJKJS +
                "',CCLX='" + CCLX +
                "',SFPJ='" + SFPJ +
                "' WHERE  NY='" + NY + "'  AND PJDYMC='" + PJDYMC + "' AND CYC_ID='" + CYC_ID + "'";


            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(SQL) > 0) return true;
            return false;
        }

        /// <summary>
        /// 删除一条评价单元信息
        /// </summary>
        /// <returns></returns>
        public static bool delete(string ny, string pjdymc, string cyc_id)
        {
            string sql = "delete from PJDYSJ where CYC_ID='" + cyc_id + "' and NY='" + ny + "' and PJDYMC='" + pjdymc + "'";

            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }
    }
}


