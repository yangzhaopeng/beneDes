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
using System.Data.OracleClient;

namespace beneDesCYC.model.month
{
    public class setYtParams_model
    {
        /// <summary>
        /// 获取某采油厂下所有的油气商品率信息列表
        /// </summary>
        /// <param name="cyc_id"></param>
        public static DataTable getAllList(string cyc_id, string month)
        {
            //获取列：
            //        参数类型ID（标识参数）、参数名称、单位、油气类型、销售油品、值  
            //        每一个类型可能有多条数据，提供单条添加的功能

            SqlHelper sqlhelper = new SqlHelper();

            DataSet ds = new DataSet();
            var param_cny = new OracleParameter("CNY", OracleType.VarChar);
            param_cny.Value = month;
            var param_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            param_cyc.Value = cyc_id;
            var param_out = new OracleParameter("v", OracleType.Cursor);
            param_out.Direction = ParameterDirection.Output;

            ds = sqlhelper.GetDataSet("SP_GET_PARAMS_Y", CommandType.StoredProcedure,
                     new OracleParameter[] { param_cny, param_cyc, param_out });
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取某一条具体的油气商品率信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static DataTable getOneYQSPL(string ny, string yqlxdm, string cyc_id)
        {
            string sql = "select * from YQSPL_INFO where NY='" + ny + "'  and YQLXDM='" + yqlxdm + "' and CYC_ID='" + cyc_id + "'";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 添加一条油气商品率信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="sc"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool add(string ny, string yqlxdm, string yqspl, string cyc_id)
        {
            string sql = "insert into YQSPL_INFO (NY,YQLXDM,YQSPL,CYC_ID) values ('" +
                         ny + "','" +

                         yqlxdm + "'," +
                         yqspl + ",'" +
                         cyc_id + "')";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 编辑某一条油气商品率信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="sc"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool edit(string ny, string yqlxdm, string yqspl, string cyc_id)
        {
            string sql = "update YQSPL_INFO  set YQSPL=" + yqspl + " where NY='" + ny + "'  and YQLXDM='" + yqlxdm + "' and CYC_ID='" + cyc_id + "'";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }
        /// <summary>
        /// 编辑参数  
        /// </summary>
        /// <param name="NY">年月</param>
        /// <param name="CSLX_ID">参数名称代码</param>
        /// <param name="CSMC">参数名称</param>
        /// <param name="DW">单位</param>
        /// <param name="YQLXDM">油气类型代码</param>
        /// <param name="XSYPDM">销售油品代码</param>
        /// <param name="PARA_VALUE">值</param>
        /// <param name="CQC_ID"></param>
        /// <returns></returns>
        internal static bool edit(string NY, string TABLENAME, string CSDM, string YQLXDM, string XSYPDM, string PARA_VALUE, string CQC_ID)
        {
            bool result = false;
            SqlHelper sqlhelper = new SqlHelper();
            try
            {
                #region  存储过程参数说明
                //vNY  in VARCHAR2:=(6),     --年月
                //vTABLENAME in VARCHAR2:=(60), --表名
                //vCSDM in VARCHAR2:=(10),   --字段名  yqspl  zys  trqdly  rbmhl qjfy  ktfy ...
                //vYQLXDM in VARCHAR2:=(60),  --油气类型代码
                //vXSYPDM in VARCHAR2:=(60),  --销售油品（销售渠道代码）
                //vPara_Value in NUMBER,      --参数值
                //vCYC_ID in VARCHAR2:=(20),  --生产单位
                //vrtn  out  int
                #endregion

                #region 存储过程添加参数值
                var vNY = new OracleParameter("vNY", OracleType.VarChar);
                vNY.Value = NY;
                var vTABLENAME = new OracleParameter("vTABLENAME", OracleType.VarChar);
                vTABLENAME.Value = TABLENAME;
                var vCSDM = new OracleParameter("vCSDM", OracleType.VarChar);
                vCSDM.Value = CSDM;
                var vYQLXDM = new OracleParameter("vYQLXDM", OracleType.VarChar);
                vYQLXDM.Value = YQLXDM;
                var vXSYPDM = new OracleParameter("vXSYPDM", OracleType.VarChar);
                vXSYPDM.Value = XSYPDM;
                var VPARA_VALUE = new OracleParameter("VPARA_VALUE", OracleType.Number);
                VPARA_VALUE.Value = PARA_VALUE;
                var vCYC_ID = new OracleParameter("vCYC_ID", OracleType.VarChar);
                vCYC_ID.Value = CQC_ID;
                var param_out = new OracleParameter("vrtn", OracleType.Int32);
                param_out.Direction = ParameterDirection.Output;
                #endregion

                int vrtn = sqlhelper.ExcuteSql("SP_INSERT_CS", CommandType.StoredProcedure, new OracleParameter[] { vNY, vTABLENAME, vCSDM, vYQLXDM, vXSYPDM, VPARA_VALUE, vCYC_ID, param_out });

                if (vrtn == 1) { result = true; }
                else if (vrtn == 2) { result = true; }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        /// <summary>
        /// 删除某一条油气商品率信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool delete(string ny, string yqlxdm, string cyc_id)
        {
            string sql = "delete from YQSPL_INFO  where NY='" + ny + "'  and YQLXDM='" + yqlxdm + "' and CYC_ID='" + cyc_id + "'";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }
    }
}
