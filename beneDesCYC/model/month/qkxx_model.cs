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
    public class qkxx_model
    {
        /// <summary>
        /// 获取某采油厂下的所有区块信息
        /// </summary>
        public static DataTable getAllList(string cyc_id, string month)
        {
            string sql = @"select distinct NY ,QKMC,SSYT,YCLX, DYHYMJ, DYDZCL ,DYKCCL, YCZS,PJSTL, DXYYND, YJZJS,YJKJS, SJZJS, SJKJS, ZSJ, LJZSL,ZQJZJS, ZQJKJZ, ZQL, LJZQL, CYOUL, LJCYOUL, CYL, LJCYL,CQL,LJCQL,XSYP,XSYPMC,SFPJ, CYC_ID
                           from  QKSJ s, XSYP_INFO x
                           where s.XSYP=x.XSYPDM
                           and  CYC_ID='" + cyc_id + "' and NY='" + month + "' order by NY desc,QKMC";

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
        /// 获取某一条单井信息数据
        /// </summary>
        public static DataTable getOneQKXX(string ny, string cyc_id, string qkmc)
        {
            string sql = "select * from QKSJ where NY='" + ny + "' and CYC_ID='" + cyc_id + "' and QKMC='" + qkmc + "' ";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 添加一条区块信息
        /// </summary>
        public static bool add(string ny, string qkmc, string ssyt, string yclx, string dyhymj, string dydzcl, string dykccl, string yczs, string pjstl, string dxyynd, string yjzjs, string yjkjs, string sjzjs, string sjkjs, string zsj, string ljzsl, string zqjzjs, string zqjkjz, string zql, string ljzql, string cyoul, string ljcyoul, string cyl, string ljcyl, string cql, string ljcql, string xsyp, string sfpj, string cyc_id)
        {
            string sql = "insert into QKSJ (NY ,QKMC,SSYT,YCLX, DYHYMJ, DYDZCL ,DYKCCL, YCZS,PJSTL, DXYYND, YJZJS,YJKJS, SJZJS, SJKJS, ZSJ, LJZSL,ZQJZJS, ZQJKJZ, ZQL, LJZQL, CYOUL, LJCYOUL, CYL, LJCYL,CQL,LJCQL,XSYP,SFPJ, CYC_ID) values ('" +
                         ny + "','" +
                         qkmc + "','" +
                         ssyt + "','" +
                         yclx + "','" +
                         dyhymj + "','" +
                         dydzcl + "','" +
                         dykccl + "','" +
                         yczs + "','" +
                         pjstl + "','" +
                         dxyynd + "','" +
                         yjzjs + "','" +
                         yjkjs + "','" +
                         sjzjs + "','" +
                         sjkjs + "','" +
                         zsj + "','" +
                         ljzsl + "','" +
                         zqjzjs + "','" +
                         zqjkjz + "','" +
                         zql + "','" +
                         ljzql + "','" +
                         cyoul + "','" +
                         ljcyoul + "','" +
                         cyl + "','" +
                         ljcyl + "','" +
                         cql + "','" +
                         ljcql + "','" +
                         xsyp + "','" +
                         sfpj + "','" +
                         cyc_id + "')";

            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 编辑区块信息
        /// </summary>
        public static bool edit(string ny, string qkmc, string ssyt, string yclx, string dyhymj, string dydzcl, string dykccl, string yczs, string pjstl, string dxyynd, string yjzjs, string yjkjs, string sjzjs, string sjkjs, string zsj, string ljzsl, string zqjzjs, string zqjkjz, string zql, string ljzql, string cyoul, string ljcyoul, string cyl, string ljcyl, string cql, string ljcql, string xsyp, string sfpj, string cyc_id)
        {

            string sql = "update QKSJ set SSYT= '" + ssyt + "', YCLX='" + yclx + "', DYHYMJ='" + dyhymj + "',DYDZCL='" + dydzcl + "',DYKCCL='" + dykccl + "',YCZS='" + yczs + "',PJSTL='" + pjstl + "', DXYYND='" + dxyynd + "',YJZJS='" + yjzjs + "',YJKJS='" + yjkjs + "',SJZJS='" + sjzjs + "',SJKJS='" + sjkjs + "',ZSJ='" + zsj + "',LJZSL='" + ljzsl + "',ZQJZJS='" + zqjzjs + "',ZQJKJZ='" + zqjkjz + "',ZQL='" + zql + "',LJZQL='" + ljzql + "',CYOUL='" + cyoul + "',LJCYOUL='" + ljcyoul + "',CYL='" + cyl + "',LJCYL='" + ljcyl + "',CQL='" + cql + "',LJCQL='" + ljcql + "',XSYP='" + xsyp + "',SFPJ='" + sfpj + "' where  NY='" + ny + "'  and QKMC='" + qkmc + "' and CYC_ID='" + cyc_id + "'";


            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 删除一条区块信息
        /// </summary>
        /// <returns></returns>
        public static bool delete(string ny, string qkmc, string cyc_id)
        {
            string sql = "delete from QKSJ where CYC_ID='" + cyc_id + "' and NY='" + ny + "' and QKMC='" + qkmc + "'";

            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }
        /// <summary>
        /// 获取在评价单元定义中 不包含的区块
        /// </summary>
        /// <param name="month"></param>
        /// <param name="PJDYMC"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static DataTable getSelectPjdyList(string month, string PJDYMC, string cyc_id)
        {
            string sql = @"select distinct NY ,QKMC from  QKSJ s, XSYP_INFO x where s.XSYP=x.XSYPDM and  CYC_ID='" + cyc_id + "' and NY='" + month + "' and s.qkmc not in (select qk from pjdyqk where ny='" + month + "' and cyc_id='" + cyc_id + "' and pjdy='" + PJDYMC + "') order by NY desc,QKMC";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }
    }
}

