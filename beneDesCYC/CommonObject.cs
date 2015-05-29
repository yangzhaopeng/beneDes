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
using System.Collections;
using System.Collections.Generic;
using System.Data.OracleClient;

namespace beneDesCYC
{
    public class CommonObject
    {
        //单井基础信息
        private static Hashtable m_JH_ID_Table = null;
        public static Hashtable JH_ID_Table
        {
            get
            {
                if (m_JH_ID_Table == null || m_JH_ID_Table.Count == 0)
                {
                    RushWellInfo();
                }
                return m_JH_ID_Table;
            }
        }

        private static Hashtable m_JH_Data_Table = null;
        public static Hashtable JH_Data_Table
        {
            get
            {
                if (m_JH_Data_Table == null || m_JH_Data_Table.Count == 0)
                {
                    RushWellInfo();
                }
                return m_JH_Data_Table;
            }
        }
        public static void RushWellInfo()
        {
            if (m_JH_ID_Table == null)
            {
                m_JH_ID_Table = new Hashtable();
            }
            else
            {
                m_JH_ID_Table.Clear();
            }
            if (m_JH_Data_Table == null)
            {
                m_JH_Data_Table = new Hashtable();
            }
            else
            {
                m_JH_Data_Table.Clear();
            }
            try
            {
                string sql = "select * from pc_well";
                beneDesYGS.core.SqlHelper myHelper = new beneDesYGS.core.SqlHelper();
                DataSet ds = myHelper.GetDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dr = dt.Rows[i];
                            m_JH_ID_Table[dr["JH"]] = dr["ID"];
                            m_JH_Data_Table[dr["JH"]] = dr;
                        }
                    }
                }
            }
            catch
            {
            }
        }
        #region 与当月单井数据做校验

        //单井数据中已有井号
        private static Hashtable m_DJSJ_JH_ID_Table = null;
        public static Hashtable DJSJ_JH_ID_Table(string month, string cyc_id)
        {
            if (m_DJSJ_JH_ID_Table == null)
            {
                m_DJSJ_JH_ID_Table = new Hashtable();
            }
            else
            {
                m_DJSJ_JH_ID_Table.Clear();
            }
            try
            {
                string sql = string.Format("select * from djsj where ny='{0}' and cyc_id='{1}' ", month, cyc_id);
                beneDesCYC.core.SqlHelper myHelper = new beneDesCYC.core.SqlHelper();
                DataSet ds = myHelper.GetDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dr = dt.Rows[i];
                            m_DJSJ_JH_ID_Table[dr["JH"]] = dr["NY"];
                        }
                    }
                }

            }
            catch
            {
            }
            return m_DJSJ_JH_ID_Table;
        }
        //单井数据  中区块校验
        private static Hashtable m_DJSJ_QK_Table = null;
        public static Hashtable DJSJ_QK_Table(string month)
        {
            if (m_DJSJ_QK_Table == null)
            {
                m_DJSJ_QK_Table = new Hashtable();
            }
            else
            {
                m_DJSJ_QK_Table.Clear();
            }
            try
            {
                string sql = string.Format("select * from djsj where ny={0}", month);
                beneDesCYC.core.SqlHelper myHelper = new beneDesCYC.core.SqlHelper();
                DataSet ds = myHelper.GetDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dr = dt.Rows[i];
                            m_DJSJ_QK_Table[dr["QK"]] = dr["NY"];
                        }
                    }
                }

            }
            catch
            {
            }
            return m_DJSJ_QK_Table;
        }
        //单井数据  中评价单元校验
        private static Hashtable m_DJSJ_PJDY_Table = null;
        public static Hashtable DJSJ_PJDY_Table(string month)
        {
            if (m_DJSJ_PJDY_Table == null)
            {
                m_DJSJ_PJDY_Table = new Hashtable();
            }
            else
            {
                m_DJSJ_PJDY_Table.Clear();
            }
            try
            {
                string sql = string.Format("select * from djsj where ny={0}", month);
                beneDesCYC.core.SqlHelper myHelper = new beneDesCYC.core.SqlHelper();
                DataSet ds = myHelper.GetDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dr = dt.Rows[i];
                            m_DJSJ_PJDY_Table[dr["PJDY"]] = dr["NY"];
                        }
                    }
                }

            }
            catch
            {
            }
            return m_DJSJ_PJDY_Table;
        }

        #endregion
        //评价单元信息
        private static Hashtable m_PJDY_ID_Table = null;
        private static Hashtable m_PJDY_MC_Table = null;
        public static Hashtable PJDY_ID_Table
        {
            get
            {
                if (m_PJDY_ID_Table == null || m_PJDY_ID_Table.Count == 0)
                {
                    Rush_PJDYXX();
                }
                return m_PJDY_ID_Table;
            }
        }
        public static Hashtable PJDY_MC_Table
        {
            get
            {
                if (m_PJDY_MC_Table == null)
                {
                    Rush_PJDYXX();
                }
                return m_PJDY_MC_Table;
            }
        }
        public static void Rush_PJDYXX()
        {
            if (m_PJDY_ID_Table == null)
            {
                m_PJDY_ID_Table = new Hashtable();
            }
            else
            {
                m_PJDY_ID_Table.Clear();
            }
            if (m_PJDY_MC_Table == null)
            {
                m_PJDY_MC_Table = new Hashtable();
            }
            else
            {
                m_PJDY_MC_Table.Clear();
            }
            try
            {
                string sql = "select * from pjdydy";
                beneDesYGS.core.SqlHelper myHelper = new beneDesYGS.core.SqlHelper();
                DataSet ds = myHelper.GetDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dr = dt.Rows[i];
                            m_PJDY_ID_Table[dr["PJDYMC"]] = dr["ID"];
                            m_PJDY_MC_Table[dr["ID"]] = dr["PJDYMC"];
                        }
                    }
                }
            }
            catch
            {
            }
        }
        //获取本地本月区块名称数据与日期的对照
        private static Hashtable m_LocalPJDY_Table = null;
        public static Hashtable RushLocalPJDYInfo(string month, string cyc_id)
        {
            if (m_LocalPJDY_Table == null)
            {
                m_LocalPJDY_Table = new Hashtable();
            }
            else
            {
                m_LocalPJDY_Table.Clear();
            }
            //if (m_Area_MC_Table == null)
            //{
            //    m_Area_MC_Table = new Hashtable();
            //}
            //else
            //{
            //    m_Area_MC_Table.Clear();
            //}
            try
            {
                string sql = string.Format("select * from pjdysj where ny='{0}' and cyc_id='{1}'", month, cyc_id);
                beneDesCYC.core.SqlHelper myHelper = new beneDesCYC.core.SqlHelper();
                DataSet ds = myHelper.GetDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dr = dt.Rows[i];
                            m_LocalPJDY_Table[dr["PJDYMC"]] = dr["NY"];
                        }
                    }
                }
            }
            catch
            {
            }
            return m_LocalPJDY_Table;
        }

        //区块信息
        private static Hashtable m_Area_ID_Table = null;
        public static Hashtable Area_ID_Table
        {
            get
            {
                if (m_Area_ID_Table == null || m_Area_ID_Table.Count == 0)
                {
                    RushAreaInfo();
                }
                return m_Area_ID_Table;
            }
        }
        private static Hashtable m_Area_MC_Table = null;
        public static Hashtable Area_MC_Table
        {
            get
            {
                if (m_Area_MC_Table == null || m_Area_MC_Table.Count == 0)
                {
                    RushAreaInfo();
                }
                return m_Area_MC_Table;
            }
        }
        public static void RushAreaInfo()
        {
            if (m_Area_ID_Table == null)
            {
                m_Area_ID_Table = new Hashtable();
            }
            else
            {
                m_Area_ID_Table.Clear();
            }
            if (m_Area_MC_Table == null)
            {
                m_Area_MC_Table = new Hashtable();
            }
            else
            {
                m_Area_MC_Table.Clear();
            }
            try
            {
                string sql = "select * from qkdy";
                beneDesYGS.core.SqlHelper myHelper = new beneDesYGS.core.SqlHelper();
                DataSet ds = myHelper.GetDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dr = dt.Rows[i];
                            m_Area_ID_Table[dr["QKMC"]] = dr["ID"];
                            m_Area_MC_Table[dr["ID"]] = dr["QKMC"];
                        }
                    }
                }
            }
            catch
            {
            }
        }
        //获取本地本月区块名称数据与日期的对照
        private static Hashtable m_LocalArea_Table = null;
        public static Hashtable RushLocalAreaInfo(string month, string cyc_id)
        {
            if (m_LocalArea_Table == null)
            {
                m_LocalArea_Table = new Hashtable();
            }
            else
            {
                m_LocalArea_Table.Clear();
            }
            //if (m_Area_MC_Table == null)
            //{
            //    m_Area_MC_Table = new Hashtable();
            //}
            //else
            //{
            //    m_Area_MC_Table.Clear();
            //}
            try
            {
                string sql = string.Format("select * from qksj where ny='{0}' and cyc_id='{1}'", month, cyc_id);
                beneDesCYC.core.SqlHelper myHelper = new beneDesCYC.core.SqlHelper();
                DataSet ds = myHelper.GetDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dr = dt.Rows[i];
                            m_LocalArea_Table[dr["QKMC"]] = dr["NY"];
                        }
                    }
                }
            }
            catch
            {
            }
            return m_LocalArea_Table;
        }
        //作业区信息

        private static Hashtable m_ZYQ_ID_Table = null;
        public static Hashtable ZYQ_ID_Table
        {
            get
            {
                if (m_ZYQ_ID_Table == null || m_ZYQ_ID_Table.Count == 0)
                {
                    Rush_ZYQXX();
                }
                return m_ZYQ_ID_Table;
            }
        }

        private static Hashtable m_ZYQ_MC_Table = null;
        public static Hashtable ZYQ_MC_Table
        {
            get
            {
                if (m_ZYQ_MC_Table == null || m_ZYQ_MC_Table.Count == 0)
                {
                    Rush_ZYQXX();
                }
                return m_ZYQ_MC_Table;
            }
        }


        public static void Rush_ZYQXX()
        {
            if (m_ZYQ_ID_Table == null)
            {
                m_ZYQ_ID_Table = new Hashtable();
            }
            else
            {
                m_ZYQ_ID_Table.Clear();
            }
            if (m_ZYQ_MC_Table == null)
            {
                m_ZYQ_MC_Table = new Hashtable();
            }
            else
            {
                m_ZYQ_MC_Table.Clear();
            }
            try
            {
                string sql = "select * from department WHERE DEP_TYPE='ZYQ'";
                beneDesCYC.core.SqlHelper myHelper = new beneDesCYC.core.SqlHelper();
                DataSet ds = myHelper.GetDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dr = dt.Rows[i];
                            m_ZYQ_ID_Table[dr["DEP_NAME"]] = dr["DEP_ID"];
                            m_ZYQ_MC_Table[dr["DEP_ID"]] = dr["DEP_NAME"];
                        }
                    }
                }
            }
            catch
            {
            }
        }

        //油藏类型信息
        private static Hashtable m_CYC_YCLX_Table = null;
        public static Hashtable GetYCLX(string cyc_ID)
        {
            Hashtable result = null;
            try
            {
                if (m_CYC_YCLX_Table == null)
                {
                    m_CYC_YCLX_Table = new Hashtable();
                }
                if (m_CYC_YCLX_Table[cyc_ID] == null)
                {
                    string sql = "select * from yclx where cyc_id='{0}'";
                    sql = string.Format(sql, cyc_ID);
                    beneDesCYC.core.SqlHelper myHelper = new beneDesCYC.core.SqlHelper();
                    DataSet ds = myHelper.GetDataSet(sql);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = null;
                            result = new Hashtable();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                dr = dt.Rows[i];
                                result[dr["yclx"]] = 1;
                            }
                            m_CYC_YCLX_Table[cyc_ID] = result;
                        }
                    }
                }
                else
                {
                    result = m_CYC_YCLX_Table[cyc_ID] as Hashtable;
                }
            }
            catch
            {
            }
            return result;
        }

        //销售油品信息

        private static Hashtable m_XSYP_DM_Table = null;
        private static Hashtable m_XSYP_MC_Table = null;
        public static Hashtable XSYP_DM_Table
        {
            get
            {
                if (m_XSYP_DM_Table == null || m_XSYP_DM_Table.Count == 0)
                {
                    RushXSYPXX();
                }
                return m_XSYP_DM_Table;
            }

        }
        public static Hashtable XSYP_MC_Table
        {
            get
            {
                if (m_XSYP_MC_Table == null || m_XSYP_MC_Table.Count == 0)
                {
                    RushXSYPXX();
                }
                return m_XSYP_MC_Table;
            }
        }
        public static void RushXSYPXX()
        {
            if (m_XSYP_DM_Table == null)
            {
                m_XSYP_DM_Table = new Hashtable();
            }
            else
            {
                m_XSYP_DM_Table.Clear();
            }
            if (m_XSYP_MC_Table == null)
            {
                m_XSYP_MC_Table = new Hashtable();
            }
            else
            {
                m_XSYP_MC_Table.Clear();
            }
            try
            {
                string sql = "select * from XSYP_INFO";
                beneDesCYC.core.SqlHelper myHelper = new beneDesCYC.core.SqlHelper();
                DataSet ds = myHelper.GetDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dr = dt.Rows[i];
                            m_XSYP_DM_Table[dr["XSYPMC"]] = dr["XSYPDM"];
                            m_XSYP_MC_Table[dr["XSYPDM"]] = dr["XSYPMC"];
                        }
                    }
                }
            }
            catch
            {
            }
        }

        //井别
        private static Hashtable m_JB_DM_Table = null;
        public static Hashtable JB_DM_Table
        {
            get
            {
                if (m_JB_DM_Table == null || m_JB_DM_Table.Count == 0)
                {
                    RushJBXX();
                }
                return m_JB_DM_Table;
            }
        }
        private static Hashtable m_JB_MC_Table = null;
        public static Hashtable JB_MC_Table
        {
            get
            {
                if (m_JB_MC_Table == null || m_JB_MC_Table.Count == 0)
                {
                    RushJBXX();
                }
                return m_JB_MC_Table;
            }
        }
        public static void RushJBXX()
        {
            if (m_JB_DM_Table == null)
            {
                m_JB_DM_Table = new Hashtable();
            }
            else
            {
                m_JB_DM_Table.Clear();
            }
            if (m_JB_MC_Table == null)
            {
                m_JB_MC_Table = new Hashtable();
            }
            else
            {
                m_JB_MC_Table.Clear();
            }
            try
            {
                string sql = "select * from JB_INFO";
                beneDesCYC.core.SqlHelper myHelper = new beneDesCYC.core.SqlHelper();
                DataSet ds = myHelper.GetDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dr = dt.Rows[i];
                            m_JB_DM_Table[dr["JBNAME"]] = dr["JBH"];
                            m_JB_MC_Table[dr["JBH"]] = dr["JBNAME"];
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }

    /// <summary>
    /// 通用功能
    /// </summary>
    public class CommonFunctions
    {
        public static string ConvertString(string source)
        {
            source = source.Trim();
            source = source.Replace("\r", "").Replace("\n", "");
            return source;
        }
        public static object ConvertDBString(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return DBNull.Value;
            }
            else
            {
                return source;
            }
        }
        public static bool IsNumber(string souce)
        {
            try
            {
                double value = 0;
                return double.TryParse(souce, out value);
            }
            catch
            {
                return false;
            }
        }
        public static bool RefreshDataBase()
        {
            bool result = true;
            try
            {
                if (!RefreshCYCXX())
                {
                    result = false;
                }
                if (!RefreshZYQXX())
                {
                    result = false;
                }
                if (!RefreshQKXX())
                {
                    result = false;
                }
                if (!RefreshWellInfo())
                {
                    result = false;
                }
                if (result)
                {
                    CommonObject.RushWellInfo();
                    CommonObject.RushAreaInfo();
                    CommonObject.Rush_ZYQXX();
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }
        public static bool RefreshWellInfo()
        {
            bool result = false;
            try
            {
                List<String> sqlList = new List<string>();
                string sql = @"select a.*,c.cyc_id
                        from (
                        select x1.well_id,x1.well_common_name,x3.project_id
                        from cd_well_source x1,cd_site_source x2,  cd_project_source x3                 
                        where x1.site_id=x2.site_id and x2.project_id=x3.project_id)a 
                        left join PC_PROJECT_ASSOC b on a.project_id=b.project_id
                        left join
                        (select org_id,org_id as cyc_id from PC_ORGANIZATION_T where org_level=10 and org_Name like '%厂' 
                        union
                        select b.ORG_ID,b.parent_ID as cyc_id
                        from PC_ORGANIZATION_T a，PC_ORGANIZATION_T b 
                        where a.org_level=10 and a.org_Name like '%厂' 
                        and a.org_id=b.parent_ID
                        union
                        select c.ORG_ID,b.parent_ID as cyc_id
                        from PC_ORGANIZATION_T a，PC_ORGANIZATION_T b ,PC_ORGANIZATION_T c
                        where a.org_level=10 and a.org_Name like '%厂' 
                        and a.org_id=b.parent_ID and b.org_id=c.parent_ID)c on b.org_id=c.org_id";
                SqlHelper_A2 myHelper = new SqlHelper_A2();
                DataSet ds = myHelper.GetDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null)
                    {
                        sqlList.Add("truncate table pc_well");
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dr = dt.Rows[i];
                            sqlList.Add(string.Format("insert into pc_well(id,dj_id,jh,zyq,qk,dep_id) values('{0}',null,'{1}',null,'{2}','{3}')",
                                dr["well_id"], dr["well_common_name"], dr["project_id"], dr["cyc_id"]));
                        }
                        beneDesYGS.core.SqlHelper helper2 = new beneDesYGS.core.SqlHelper();
                        if (helper2.ExecuteTranErrorCount(sqlList) < 0)
                        {
                            result = true;
                        }
                    }
                }
            }
            catch
            {
            }
            return result;
        }
        public static bool RefreshQKXX()
        {
            bool result = false;
            try
            {
                List<String> sqlList = new List<string>();
                string sql = @"select site_id,site_name,b.org_id
                         from (
                        select case when (x9.project_name like '%采油%厂' OR x9.project_name like '%采气%厂') then x9.project_id else 
                        case when (x7.project_name like '%采油%厂' or x7.project_name like '%采气%厂') then x7.project_id else
                        x5.project_id end end as cyc_id,x3.project_name site_name,x3.project_id site_id
                        from cd_site_source x2,  cd_project_source x3,
                              PC_PROJECT_ASSOC x4,  cd_project_source x5,
                              PC_PROJECT_ASSOC x6,  cd_project_source x7,
                              PC_PROJECT_ASSOC x8,  cd_project_source x9
                        where x2.project_id=x3.project_id(+)
                        and x3.project_id=x4.project_id(+) and x4.parent_project_id=x5.project_id(+)
                        and x5.project_id=x6.project_id(+) and x6.parent_project_id=x7.project_id(+)
                        and x7.project_id=x8.project_id(+) and x8.parent_project_id=x9.project_id(+)
                        and ( x9.project_name like '%采油%厂' or x7.project_name like '%采油%厂' or x5.project_name like '%采油%厂' 
                        OR x9.project_name like '%采气%厂' or x7.project_name like '%采气%厂' or x5.project_name like '%采气%厂' ))a
                        ,(select a.project_id,c.org_id from cd_project_source a,PC_PROJECT_ASSOC b,PC_ORGANIZATION_T c
                        where (a.project_name like '%采油%厂' or a.project_name like '%采气%厂')
                        and a.project_id=b.project_id and b.org_id=c.org_id)b where a.cyc_id=b.project_id";
                SqlHelper_A2 myHelper = new SqlHelper_A2();
                DataSet ds = myHelper.GetDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null)
                    {
                        sqlList.Add("truncate table QKDY");
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dr = dt.Rows[i];
                            sqlList.Add(string.Format("insert into QKDY(id,qkmc,dep_id) values('{0}','{1}','{2}')", dr["site_id"], dr["site_name"], dr["org_id"]));
                        }
                        beneDesYGS.core.SqlHelper helper2 = new beneDesYGS.core.SqlHelper();
                        if (helper2.ExecuteTranErrorCount(sqlList) < 0)
                        {
                            result = true;
                        }
                    }
                }
            }
            catch
            {
            }
            return result;
        }
        public static bool RefreshCYCXX()
        {
            bool result = false;
            try
            {
                List<String> sqlList = new List<string>();
                string sql = @"select ORG_ID,ORG_NAME,CASE WHEN ORG_NAME LIKE '%采油%' THEN 'CYC' ELSE 'CQC' END AS ORG_TYPE  
                        from PC_ORGANIZATION_T a where org_level=10 and org_Name like '%厂'";
                SqlHelper_A2 myHelper = new SqlHelper_A2();
                DataSet ds = myHelper.GetDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null)
                    {
                        sqlList.Add("delete from DEPARTMENT where dep_id<>'[a-zA-Z]' and dep_type in('CYC','CQC')");
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dr = dt.Rows[i];
                            sqlList.Add(string.Format("insert into DEPARTMENT(dep_type,dep_id,Dep_Name,Haschild,PARENT_ID,Dep_Level) values('{0}','{1}','{2}','Y','$ROOT',1)",
                                dr["ORG_TYPE"], dr["ORG_ID"], dr["ORG_NAME"]));
                        }
                        beneDesYGS.core.SqlHelper helper2 = new beneDesYGS.core.SqlHelper();
                        helper2.ExecuteTranErrorCount(sqlList);
                        beneDesCYC.core.SqlHelper helper3 = new beneDesCYC.core.SqlHelper();
                        if (helper3.ExecuteTranErrorCount(sqlList) < 0)
                        {
                            result = true;
                        }
                    }
                }
            }
            catch
            {
            }
            return result;
        }
        public static bool RefreshZYQXX()
        {
            bool result = false;
            try
            {
                List<String> sqlList = new List<string>();
                string sql = @"select b.parent_ID,b.ORG_ID,b.ORG_NAME from PC_ORGANIZATION_T a，PC_ORGANIZATION_T b 
                        where a.org_level=10 and a.org_Name like '%厂' and a.org_id=b.parent_ID and b.org_Name like '%区%' order by b.parent_ID,b.ORG_ID";
                SqlHelper_A2 myHelper = new SqlHelper_A2();
                DataSet ds = myHelper.GetDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null)
                    {
                        sqlList.Add("delete from DEPARTMENT where dep_type='ZYQ'");
                        DataRow dr = null;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dr = dt.Rows[i];
                            sqlList.Add(string.Format("insert into DEPARTMENT(dep_type,dep_id,Dep_Name,Haschild,PARENT_ID,Dep_Level) values('ZYQ','{0}','{1}','N','{2}',2)",
                                dr["ORG_ID"], dr["ORG_NAME"], dr["parent_ID"]));
                        }
                        beneDesYGS.core.SqlHelper helper2 = new beneDesYGS.core.SqlHelper();
                        helper2.ExecuteTranErrorCount(sqlList);
                        beneDesCYC.core.SqlHelper helper3 = new beneDesCYC.core.SqlHelper();
                        if (helper3.ExecuteTranErrorCount(sqlList) < 0)
                        {
                            result = false;
                        }
                    }
                }
            }
            catch
            {
            }
            return result;
        }
    }
    public class SqlHelper_A2
    {

        private static string m_StrConn = string.Empty;
        #region GetConn()
        /// <summary>
        /// 获得数据连接串


        /// </summary>
        /// <returns></returns>
        public static OracleConnection GetConn()
        {
            if (string.IsNullOrEmpty(m_StrConn))
            {
                m_StrConn = ConfigurationManager.ConnectionStrings["A2ConnectionString"].ToString();//得到WebConfig数据库连接字符串
            }
            OracleConnection Conn = new OracleConnection(m_StrConn);
            return Conn;
        }

        #endregion



        #region GetDataSet()
        /// <summary>
        /// 获得数据集


        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql, string strTableName)
        {
            DataSet ds = new DataSet();
            OracleConnection conn = GetConn();
            conn.Open();
            OracleCommand cmd = new OracleCommand();

            OracleTransaction concreteDbTrans = conn.BeginTransaction();

            cmd.Connection = conn;
            cmd.Transaction = concreteDbTrans;
            OracleDataAdapter sda = new OracleDataAdapter();
            try
            {
                cmd.CommandText = sql;
                sda.SelectCommand = cmd;
                ((OracleDataAdapter)sda).MissingSchemaAction = MissingSchemaAction.AddWithKey;
                ((OracleDataAdapter)sda).Fill(ds, strTableName);
                concreteDbTrans.Commit();
            }
            catch
            {
                concreteDbTrans.Rollback();
                ds.Clear();
                //throw;
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetDataSet(string CommandText)
        {
            DataSet ds = new DataSet();
            OracleConnection conn = GetConn();
            try
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();

                OracleTransaction concreteDbTrans = conn.BeginTransaction();

                cmd.Connection = conn;
                cmd.Transaction = concreteDbTrans;
                OracleDataAdapter sda = new OracleDataAdapter();

                cmd.CommandText = CommandText;
                sda.SelectCommand = cmd;
                ((OracleDataAdapter)sda).Fill(ds);
            }
            catch (Exception ex)
            {
                ds.Clear();
                //throw;
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        #endregion
    }
}
