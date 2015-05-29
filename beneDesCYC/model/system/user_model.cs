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
    public class user_model
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns>返回HashTable</returns>
        public static Hashtable login(string name, string word, string month)
        {
            //string sql = "select USER_PASS, USER_NAME, s.DEP_ID as DEP_ID,CYC_ID, DEP_NAME,PARENT_ID, ROLE_ID from Frm_USERS s, DEPARTMENT d where USER_ID='" + name + "' and s.DEP_ID=d.DEP_ID ";//and DEP_TYPE='CYC'
            string sql = "select USER_PASS, USER_NAME, s.DEP_ID as DEP_ID,s.CYC_ID as CYC_ID,dep_type, DEP_NAME,PARENT_ID, ROLE_ID from Frm_USERS s, DEPARTMENT d where USER_ID='" + name + "' and s.DEP_ID=d.DEP_ID ";
            SqlHelper sqlhelper = new SqlHelper();
            DataSet ds = sqlhelper.GetDataSet(sql);
            Hashtable ht = new Hashtable();

            if (ds.Tables[0] == null || ds.Tables[0].Rows.Count < 1)
            {
                ht.Add("msg", "用户不存在！");
                ht.Add("ok", false);
            }
            else if (ds.Tables[0].Rows[0]["USER_PASS"].ToString().Trim() == pwdHelper.Encrypt(word.Trim()))
            {
                //初始化系统参数
                string[] cyc_ids = ds.Tables[0].Rows[0]["CYC_ID"].ToString().Split(',');
                if (cyc_ids.Length > 0)
                {
                    ht.Add("userName", ds.Tables[0].Rows[0]["USER_NAME"].ToString());
                    ht.Add("depId", ds.Tables[0].Rows[0]["DEP_ID"].ToString());
                    ht.Add("cycid", ds.Tables[0].Rows[0]["CYC_ID"].ToString());
                    ht.Add("roleId", ds.Tables[0].Rows[0]["ROLE_ID"].ToString());
                    ht.Add("depName", ds.Tables[0].Rows[0]["DEP_NAME"].ToString());
                    ht.Add("DEP_TYPE", ds.Tables[0].Rows[0]["DEP_TYPE"].ToString());
                    string sql2 = "select distinct bny, eny from dtstat_djsj sdy where sdy.cyc_id = '" + cyc_ids[0] + "'";
                    string sql3 = "select distinct bny, eny from jdstat_djsj where jdstat_djsj.cyc_id = '" + cyc_ids[0] + "'";

                    DataSet ds2 = sqlhelper.GetDataSet(sql2);
                    DataSet ds3 = sqlhelper.GetDataSet(sql3);

                    if (string.IsNullOrEmpty(month))
                        month = DateTime.Now.ToString("yyyyMM");

                    if (ds2 != null && ds2.Tables[0].Rows.Count > 0 && ds2.Tables[0].Rows[0]["BNY"].ToString() != null)
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

                    if (ds3 != null && ds3.Tables[0].Rows.Count > 0 && ds3.Tables[0].Rows[0]["BNY"].ToString() != null)
                    {
                        ht.Add("jbny", ds3.Tables[0].Rows[0]["BNY"].ToString());
                        ht.Add("jeny", ds3.Tables[0].Rows[0]["ENY"].ToString());
                    }
                    else
                    {
                        //重新设置默认时间@yzp
                        ht.Add("jbny", month);
                        ht.Add("jeny", month);
                    }
                    ht.Add("ok", true);
                }
                else
                {
                    ht.Add("msg", "您无权限登陆系统，请联系管理员！");
                    ht.Add("ok", false);
                }
            }
            else
            {
                ht.Add("msg", "密码错误！");
                ht.Add("ok", false);
            }
            return ht;
        }

        /// <summary>
        /// 通过用户名获取用户的信息
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>DataTable</returns>
        public static DataTable getUserInfoByUserId(string userId)
        {
            string sql = "select * from Frm_USERS where USER_ID='" + userId + "'";
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 修改某用户的登陆密码
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="password">password</param>
        public static void changePassword(string userId, string password)
        {
            password = pwdHelper.Encrypt(password.Trim());
            string sql = "update Frm_USERS set USER_PASS='" + password + "' where USER_ID='" + userId + "'";
            SqlHelper sqlhelper = new SqlHelper();
            sqlhelper.ExcuteSql(sql);
        }

        /// <summary>
        /// 获取某角色id下的所有用户

        /// </summary>
        /// <returns></returns>
        public static DataTable getUsersByRole(string role_id)
        {
            string sql = "select * from Frm_USERS where ROLE_ID=" + role_id;
            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 获取所有user的列表

        /// </summary>
        /// <returns></returns>
        public static DataTable getAllUsers()
        {
            string sql = @"select USER_ID, USER_NAME, u.DEP_ID as DEP_ID, u.REMARK as REMARK, u.ROLE_ID as ROLE_ID, DEP_NAME, ROLE_NAME
                           from Frm_USERS u,  DEPARTMENT d, Frm_ROLES r
                           where u.DEP_ID=d.DEP_ID and u.ROLE_ID=r.ROLE_ID and d.DEP_TYPE='CYC'
                           order by USER_ID";

            SqlHelper sqlhelper = new SqlHelper();
            return sqlhelper.GetDataSet(sql).Tables[0];
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="role_name"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public static bool add(string user_id, string user_name, string dep_id, string role_id, string remark)
        {
            string password = pwdHelper.Encrypt("123456");

            string sql = "insert into Frm_USERS (USER_ID,USER_PASS,USER_NAME,DEP_ID,ROLE_ID,REMARK) values ('" +
                         user_id + "','" +
                         password + "','" +
                         user_name + "','" +
                         dep_id + "'," +
                         role_id + ",'" +
                         remark + "')";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="role_id"></param>
        /// <param name="role_name"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public static bool edit(string user_id, string user_name, string dep_id, string role_id, string remark)
        {
            string sql = "update Frm_USERS set USER_NAME='" +
                         user_name + "', REMARK='" + remark + "', DEP_ID='" + dep_id +
                         "', ROLE_ID=" + role_id + " where USER_ID='" + user_id + "'";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public static bool delete(string user_id)
        {
            string sql = "delete from Frm_USERS where USER_ID='" + user_id + "'";
            SqlHelper sqlhelper = new SqlHelper();

            if (sqlhelper.ExcuteSql(sql) > 0) return true;
            return false;
        }
    }
}
