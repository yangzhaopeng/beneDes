using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using beneDesCYC.model.system;
using beneDesCYC.core;

namespace beneDesCYC.model.system
{
    public class feiyongweihu_model
    {

        public static DataTable getAllList()
        {
            string sql = "select templet_code,templet_name,templet_type,case use_type when 'S' then '公共' when 'P' then '私有' end as u_type, use_type, case templet_level when '1' then '自定义模板' when '0' then '系统模板' end as t_level, templet_level, templet_tag from fee_templet";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];

        }

        public static DataTable getOneWH(string TEMPLET_CODE)
        {
            string sql = "select * from fee_templet where templet_code= '" + TEMPLET_CODE + "'";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];

        }

        public static bool add(string TEMPLET_CODE, string TEMPLET_NAME, string TEMPLET_TYPE, string USE_TYPE, string TEMPLET_LEVEL, string TEMPLET_TAG)
        {
            string sql = "insert into fee_templet(templet_level,templet_type, templet_code,templet_name,use_type,templet_tag ) values('" + TEMPLET_LEVEL + "','" + TEMPLET_TYPE + "','" + TEMPLET_CODE + "','" + TEMPLET_NAME + " ','" + USE_TYPE + "','" + TEMPLET_TAG + "') ";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0)
            {
                return true;
            }
            else
            { return false; }
        }

        public static bool edit(string TEMPLET_CODE, string TEMPLET_NAME, string TEMPLET_TYPE, string USE_TYPE, string TEMPLET_LEVEL, string TEMPLET_TAG)
        {
            string sql = "UPDATE fee_templet SET templet_code='" + TEMPLET_CODE + "', templet_name='" + TEMPLET_NAME + "', templet_type='" + TEMPLET_TYPE + "', use_type='" + USE_TYPE + "',templet_level='" + TEMPLET_LEVEL + "',templet_tag='" + TEMPLET_TAG + "' WHERE templet_code='" + TEMPLET_CODE + "'";
            SqlHelper sqlhelper = new SqlHelper();
            if (sqlhelper.ExcuteSql(sql) > 0)
            { return true; }
            else
            { return false; }
        }

        public static bool delete(string TEMPLET_CODE)
        {
            string sql = "delete from fee_templet where templet_code='" + TEMPLET_CODE + "'";
            SqlHelper sqlhelper = new SqlHelper();
            if (sqlhelper.ExcuteSql(sql) > 0)
            { return true; }
            else
            { return false; }
        }
    }
}
