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
using System.Collections.Generic;
using beneDesCYC.core;

namespace beneDes
{
    public partial class Login1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //控制查看数据的类型

            string data_types = string.Empty;
            string data_type = string.Empty;
            string cyc_id = string.Empty;
            string dep_id = string.Empty;
            string dep_ids = string.Empty;
            int dep_level = 0;
            string depName = string.Empty;
            string ord = string.Empty;
            string userName = string.Empty;
            string roleId = string.Empty;
            //令牌
            string cid = _getParam("cid");
            //登录名

            string un = _getParam("un");
            if (string.IsNullOrEmpty(un) && !string.IsNullOrEmpty(Convert.ToString(Session["un"])))
            {
                data_types = Convert.ToString(Session["data_types"]); //系统中只能使用其中一个，在系统切换时，判断是否有切换权限
                string[] tempTypes = data_types.Split(',');
                if (!string.IsNullOrEmpty(data_types))
                {

                    switch (tempTypes.Length)
                    {
                        case 1:
                            data_type = Convert.ToString(Session["DEP_TYPE"]);
                            cyc_id = Convert.ToString(Session["cyc_id"]);
                            dep_id = Convert.ToString(Session["depId"]);//单位代码
                            dep_level = getdDepLevel(dep_id);//人员所属厂级、公司级单位
                            depName = Convert.ToString(Session["depName"]);  //单位名称
                            ord = Convert.ToString(Session["ord"]);//人员直属部门
                            userName = Convert.ToString(Session["userName"]);  //用户名称
                            break;
                        case 2:
                            tempTypes = data_types.Split(',');
                            data_type = Convert.ToString(Session["DEP_TYPE"]);
                            if (data_type == tempTypes[0])
                            {
                                data_type = tempTypes[1];
                            }
                            else
                            {
                                data_type = tempTypes[0];
                            }

                            cyc_id = Convert.ToString(Session["cyc_id"]);
                            dep_id = Convert.ToString(Session["depId"]);//单位代码
                            dep_level = getdDepLevel(dep_id);//人员所属厂级、公司级单位
                            depName = Convert.ToString(Session["depName"]);  //单位名称
                            ord = Convert.ToString(Session["ord"]);//人员直属部门
                            userName = Convert.ToString(Session["userName"]);  //用户名称
                            //roleId = Convert.ToString(Session["roleId"]);  //系统内部角色id\
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Response.Write("<script> alert('您无权访问本系统')</script>");
                    Response.End();
                }
            }
            else
            {
                Session["un"] = un;
                //权限
                List<string> rids = new List<string>();
                //当前系统编号  webconfig配置
                string systemName = "单井效益分析";
                #region 处理请求  获取令牌  获取dengluming  初始化RdmsInterface
                userName = un;
                if (cid == null || string.IsNullOrEmpty(cid))
                {
                    un = "admin";//ConfigurationManager.AppSettings["un"];// "admin";
                    cid = ConfigurationManager.AppSettings["cid"]; //"664cb276-3840-4159-9756-8e8e7dce8057";
                }
                //输入：令牌  初始化RdmsInterface
                RDMSLogin rdms = new RDMSLogin(cid, un);
                rdms.GetRights(systemName, out depName, out ord, out rids);

                Session["ord"] = ord;//人员直属部门
                if (rids != null && rids.Count > 0)
                {
                    rdms.FindOrg(ord, out dep_id);     //管理单位  ：cqcyc1 cqcyc2  yks  qks
                }
                else
                {
                    Response.Write("<script> alert('您无权访问本系统')</script>");
                    Response.End();
                }
                #endregion

                #region 获取RdmsInterface  的返回值

                #region 获取人员可查看的  数据生产单位

                //输出：dep_id(组织机构)     采油（气）厂id   or  人员单位（油开室、气开室、规划计划科）

                dep_ids = getDepID(dep_id);     //管理单位  ：cqcyc1 cqcyc2  yks  qks 
                Session["dep_ids"] = dep_ids;
                dep_id = dep_ids.Split(',')[0];
                Session["depId"] = dep_id;

                #endregion


                #region 获取人员的权限 使用评价单位类型做区分


                //输出：data_type(部门油气类型)   "CYC" or    CQC     List<string>  判断是否拥有油、气查看权限  存入Session
                data_types = getDataType(rids);
                data_type = data_types.Split(',')[0];
                Session["data_types"] = data_types;


                //解析：dep_level(单位级别)  1(厂级)  or   0(公司级 )    通过单位解析   
                //输入：dep_id   department
                dep_level = getdDepLevel(dep_id);

                #endregion


                #endregion

            }

            #region 根据获取到的信息初始化页面


            initPageSite(data_type, dep_level, userName, dep_id, depName);
            #endregion
        }

        /// <summary>
        /// 解析：dep_level(单位级别)  厂级  or   公司级 
        /// </summary>
        /// <param name="dep_id">例如：qhcy1</param>
        /// <returns>1（厂级）  or   0（公司级）</returns>
        private int getdDepLevel(string dep_id)
        {
            int result = 0;
            SqlHelper sqlhelper = new SqlHelper();
            string sql = @"select count(2) from department where dep_id='" + dep_id + "'";
            int isHav = int.Parse(sqlhelper.GetExecuteScalar(sql).ToString());
            //如果  dep_id 在 厂级部门id中，则返回  1   否则返回 0
            if (isHav > 0)
                result = 1;
            return result;
        }
        //组织结构代码
        private string getDepID(string dep_id)
        {
            //与Department表核对  

            return dep_id;
        }

        private string getDataType(List<string> rids)
        {

            string result = "";
            //IPvMzYGHg1	经济评价(油)
            //IPvMzYGHg2	经济评价(气)
            for (int i = 0; i < rids.Count; i++)
            {
                if (rids[i] == "IPvMzYGHg1")
                    result += "CYC";
                else
                    if (rids[i] == "IPvMzYGHg2")
                        if (result.Length > 0)
                            result += ",CQC";
                        else result += "CQC";


            }
            return result;
        }
        #region 列表权限
        /// <summary>
        /// 厂级 列表权限
        /// </summary>
        enum cjRole
        {
            cyc = 3,
            cqc = 2
        }
        enum gsjRole
        {
            ygs = 1,
            qgs = 2
        }
        #endregion
        /// <summary>
        /// 根据获取到的信息初始化页面

        /// </summary>
        /// <param name="data_type">"CYC" or    "CQC" </param>
        /// <param name="dep_level">1(厂级)  or   0(公司级 )</param>
        /// <param name="userName">用户名</param>
        /// <param name="dep_id">组织机构</param>
        private void initPageSite(string data_type, int dep_level, string userName, string dep_id, string dep_name)
        {
            string ygsHtml = System.Configuration.ConfigurationManager.AppSettings["ygsSite"];
            string cycHtml = System.Configuration.ConfigurationManager.AppSettings["cycSite"];

            string cycSite = "<a href='javascript:void window.open('" + cycHtml + "','Ldocean',' left=0,top=0,width='+ (screen.availWidth - 10) +',height='+ (screen.availHeight-50) +',scrollbars,resizable=yes,toolbar=no')'>Open</a>";
            string ygsSite = "<a href='javascript:void window.open('" + ygsHtml + "','Ldocean',' left=0,top=0,width='+ (screen.availWidth - 10) +',height='+ (screen.availHeight-50) +',scrollbars,resizable=yes,toolbar=no')'>Open</a>";

            if (data_type == "CYC")
            {
                if (dep_level == 1)
                {
                    initSession(userName, dep_id, (int)cjRole.cyc, data_type, dep_name);
                    //加载  采油厂系统
                    Response.Redirect(cycSite, true);
                }
                else
                {
                    initSession(userName, dep_id, (int)gsjRole.ygs, data_type, dep_name);
                    //加载 油公司系统
                    Response.Redirect(ygsSite, true);
                }
            }
            else
            {
                if (dep_level == 1)
                {
                    initSession(userName, dep_id, (int)cjRole.cqc, data_type, dep_name);
                    //加载  采气厂系统
                    Response.Redirect(cycSite, true);
                }
                else
                {
                    initSession(userName, dep_id, (int)gsjRole.qgs, data_type, dep_name);
                    //加载  气公司系统
                    Response.Redirect(ygsSite, true);
                }
            }

        }

        /// <summary>
        /// 初始化系统运行参数

        /// </summary>
        /// <param name="userName"></param>
        /// <param name="cyc_id"></param>
        /// <param name="roleId"></param>
        /// <param name="data_type"></param>
        private void initSession(string userName, string cyc_id, int roleId, string data_type, string dep_name)
        {

            string month = DateTime.Now.ToString("yyyyMM");
            Hashtable ok = sysNY_model.login(cyc_id, data_type, month);
            if ((bool)ok["ok"])
            {
                Session.Timeout = 99999;
                //写日志使用

                Session["userId"] = userName;
                Session["userName"] = userName;

                Session["month"] = month;
                //控制查看数据的类型

                Session["DEP_TYPE"] = data_type;

                //系统中只能使用其中一个，在系统切换时，判断是否有切换权限
                Session["cyc_id"] = cyc_id;
                Session["cqc_id"] = cyc_id;

                Session["depName"] = dep_name;
                Session["roleId"] = roleId;

                Session["bny"] = ok["bny"].ToString();
                Session["eny"] = ok["eny"].ToString();

                Session["jbny"] = ok["jbny"].ToString();
                Session["jeny"] = ok["jeny"].ToString();
            }
        }

        /// <summary>
        /// 获取url中传递的参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string _getParam(string key)
        {
            string value = Request[key];
            return value;
            //return Request[key];//@yzp
        }

    }
}
