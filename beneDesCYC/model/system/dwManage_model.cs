using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using beneDesCYC.core;

namespace beneDesCYC.model.system
{
    public class dwManage_model
    {
        public static DataTable getAllList()
        {
            string sql = "select dep_type,dep_id,dep_name,remark from department";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        public static bool add(string DEP_TYPE, string DEP_ID, string DEP_NAME, string BZ, string parentid, string deplevel)
        {
            string sql = "insert into department(dep_type,dep_id,dep_name,parent_id,dep_level,remark) values('" + DEP_TYPE + "','" + DEP_ID + "','" + DEP_NAME + "','" + parentid + "','" + deplevel + "','" + BZ + "')";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0)
            {
                return true;
            }
            else
            { return false; }

        }

        public static DataTable getOneDW(string DEP_ID)
        {
            string sql = "select * from department where DEP_ID = '" + DEP_ID + "'";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        public static bool edit(string DEP_TYPE, string DEP_ID, string DEP_NAME, string BZ)
        {
            string sql = "update department set dep_type='" + DEP_TYPE + "', dep_name='" + DEP_NAME + "', remark='" + BZ + "' where DEP_ID = '" + DEP_ID + "'";
            SqlHelper sqlhelper = new SqlHelper();
            if (sqlhelper.ExcuteSql(sql) > 0)
            { return true; }
            else
            { return false; }

        }

        public static bool delete(string DEP_ID)
        {
            string sql = "delete from department where DEP_ID='" + DEP_ID + "'";
            SqlHelper sqlhelper = new SqlHelper();
            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

    }
}
