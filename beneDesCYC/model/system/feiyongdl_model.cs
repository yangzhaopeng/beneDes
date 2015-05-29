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
    public class feiyongdl_model
    {
        public static DataTable getFormula()
        {
            string sql = "select formula_code as FL_ID,formula_name as FL_NAME from formula";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        public static DataTable getAllList()
        {
            string sql = "select class_name as DL_NAME,class_code as DL_ID, case fee_type when '0' then '成本' when '1' then '收入' end as FY_TYPE, formula_code as FORMULAS,remark as BZ from fee_class where fee_type='0'";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];

        }

        public static DataTable getOneFY(string DL_ID)
        {
            string sql = "select * from fee_class where class_code= '" + DL_ID + "'";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];

        }

        public static bool add(string DL_NAME, string DL_ID, string FY_TYPE, string formula, string BZ)
        {
            string sql = "insert into fee_class(class_name,class_code, sort_index,has_alert, fee_type,formula_code,remark) values('" + DL_NAME + "','" + DL_ID + "',0,'Y','" + FY_TYPE + "','" + formula + "','" + BZ + "')";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0)
            {
                return true;
            }
            else
            { return false; }
        }

        public static bool edit(string DL_NAME, string DL_ID, string FY_TYPE, string formula, string BZ)
        {
            string sql = "UPDATE fee_class SET class_name='" + DL_NAME + "', formula_code='" + formula + "', remark='" + BZ + "' WHERE class_code ='" + DL_ID + "'and fee_type ='" + FY_TYPE + "'";

            SqlHelper sqlhelper = new SqlHelper();
            if (sqlhelper.ExcuteSql(sql) > 0)
            { return true; }
            else
            { return false; }
        }

        public static bool delete(string DL_ID)
        {
            string sql = "delete from fee_class where class_code='" + DL_ID + "'";
            SqlHelper sqlhelper = new SqlHelper();
            if (sqlhelper.ExcuteSql(sql) > 0)
            { return true; }
            else
            { return false; }
        }

    }
}
