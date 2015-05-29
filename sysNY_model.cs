using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using beneDesCYC.core;

namespace beneDes
{
    public class sysNY_model
    {
        /// <summary>
        ///初始化系统参数
        /// </summary>
        /// <param name="cyc_ids">部门id</param>
        /// <param name="data_type">CYC   or  CQC</param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static Hashtable login(string cyc_id, string data_type, string month)
        {
            SqlHelper sqlhelper = new SqlHelper();
            Hashtable ht = new Hashtable();

            string sql2 = "select distinct bny, eny from dtstat_djsj sdy where sdy.cyc_id = '" + cyc_id + "'";

            DataSet ds2 = sqlhelper.GetDataSet(sql2);

            string sql3 = "select distinct bny, eny from jdstat_djsj where jdstat_djsj.cyc_id = '" + cyc_id + "'";

            DataSet ds3 = sqlhelper.GetDataSet(sql3);

            ht.Add("ok", true);


            if (ds2.Tables[0].Rows.Count > 0 && ds2.Tables[0].Rows[0]["BNY"].ToString() != null)
            {
                ht.Add("bny", ds2.Tables[0].Rows[0]["BNY"].ToString());
                ht.Add("eny", ds2.Tables[0].Rows[0]["ENY"].ToString());
            }
            else
            {
                //重新设置默认时间@yzp
                ht.Add("bny", month);
                ht.Add("eny", month);
            }

            if (ds3.Tables[0].Rows.Count > 0 && ds3.Tables[0].Rows[0]["BNY"].ToString() != null)
            {
                ht.Add("jbny", ds3.Tables[0].Rows[0]["BNY"].ToString());
                ht.Add("jeny", ds3.Tables[0].Rows[0]["ENY"].ToString());
            }
            else
            {
                //ht.Add("jbny", "200101");
                //ht.Add("jeny", "200101");
                //重新设置默认时间@yzp
                ht.Add("jbny", month);
                ht.Add("jeny", month);
            }
            return ht;
        }
    }
}
