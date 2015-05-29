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
    public class pjdyqk_model
    {
        /// <summary>
        /// 获取某采油厂下所有的吨桶比信息列表
        /// </summary>
        /// <param name="cyc_id"></param>
        public static DataTable getAllList(string cyc_id, string PJDYMC, string month)
        {
            string sql = @"select ny, pjdy, qk, cyc_id from pjdyqk where  
                              CYC_ID='" + cyc_id + "' and pjdy='" + PJDYMC + "' and NY='" + month + "' order by NY desc";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }
        //获取评价单元没有选择的其他区块
        public static DataTable getUnSelectQkList(string cyc_id, string PJDY, string month)
        {
            string sql = "select qk.ny,qk.qkmc from qksj qk  where not exists(select qk from pjdyqk where  qk.ny=pjdyqk.ny and qk.cyc_id=pjdyqk.cyc_id and qk.qkmc=pjdyqk.qk and pjdy='" + PJDY + "' ) and ny='" + month + "' and cyc_id='" + cyc_id + "'";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 添加区块到评价单元中
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="pjdy"></param>
        /// <param name="qks">以逗号分割的区块列表</param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool add(string ny, string pjdy, string qks, string cyc_id)
        {
            SqlHelper sqlhelper = new SqlHelper();
            string[] qk = qks.Split(',');
            List<string> sqlList = new List<string>();
            for (int i = 0; i < qk.Length; i++)
            {
                string qkmc = qk[i];
                sqlList.Add("insert into pjdyqk (ny, pjdy, qk, cyc_id) values ('" +
                             ny + "','" +
                             pjdy + "','" +
                             qkmc + "','" +
                             cyc_id + "')");
                //sqlList.Add("update djsj set pjdy='" + pjdy + "' where NY='" + ny + "' and QK='" + qkmc + "' and CYC_ID='" + cyc_id + "'");
            }

            if (sqlhelper.ExecuteTranErrorCount(sqlList) == -1) return true;
            return false;
        }



        /// <summary>
        /// 删除某评价单元信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool deleteAll(string ny, string pjdy, string cyc_id)
        {
            //删除pjdyqk表  pjdysj表  和  djsj 表  stat_djydsj 
            SqlHelper sqlhelper = new SqlHelper();
            List<string> sqlList = new List<string>();
            sqlList.Add("delete from pjdyqk  where NY='" + ny + "' and pjdy='" + pjdy + "' and CYC_ID='" + cyc_id + "'");
            sqlList.Add("delete pjdysj where NY='" + ny + "' and pjdymc='" + pjdy + "' and CYC_ID='" + cyc_id + "'");
            sqlList.Add("update djsj set pjdy='' where NY='" + ny + "' and pjdy='" + pjdy + "' and CYC_ID='" + cyc_id + "'");
            sqlList.Add("delete stat_djydsj where NY='" + ny + "' and pjdy='" + pjdy + "' and CYC_ID='" + cyc_id + "'");

            if (sqlhelper.ExecuteTranErrorCount(sqlList) == -1) return true;
            return false;
        }
        /// <summary>
        /// 删除某评价单元信息
        /// </summary>
        /// <param name="ny"></param>
        /// <param name="dep_id"></param>
        /// <param name="yqlxdm"></param>
        /// <param name="cyc_id"></param>
        /// <returns></returns>
        public static bool delete(string ny, string pjdy, string qks, string cyc_id)
        {
            SqlHelper sqlhelper = new SqlHelper();
            string[] qk = qks.Split(',');
            List<string> sqlList = new List<string>();
            for (int i = 0; i < qk.Length; i++)
            {
                string qkmc = qk[i];
                sqlList.Add("delete pjdyqk  where NY='" + ny + "' and pjdy='" + pjdy + "' and qk='" + qkmc + "' and CYC_ID='" + cyc_id + "'");
            }
            if (sqlhelper.ExecuteTranErrorCount(sqlList) == -1) return true;
            return false;
        }


        public static bool calc(string NY, string PJDY, string CYC_ID)
        {
            //更新护具
            //update djsj A set pjdy=(select PJDY from pjdyqk B where A.qk=B.Qk and A.ny=B.Ny and A.Cyc_Id=B.Cyc_Id )
 //where  A.ny='201505' and  A.cyc_id='qhcy3' and A.qk='七个泉Ⅲ层系'
 
            return true;
        }
    }
}


