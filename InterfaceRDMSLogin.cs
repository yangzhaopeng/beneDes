using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace beneDes
{
    interface IRDMSLogin
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="funcname">系统名称：单井效益分析</param>
        /// <param name="listrw"></param>
        /// <returns></returns>
        //bool CheckFunc(string funcname, out WebApplication1.ServiceReferenceRDMS.RWINFO[] listrw);
        //bool CheckUser(out string username, out string orgname, out string orgid);
        //bool getFuncs(out WebApplication1.ServiceReferenceRDMS.FunInfo[] listfun);
        //bool GetOrgList(string orguid, out int orgcount, out WebApplication1.ServiceReferenceRDMS.AppOrgInfo[] listorg);
       

        //public void RDMSLogin(string uid, string username);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="funcname">系统名称：单井效益分析</param>
        /// <param name="orgname">机构名称</param>
        /// <param name="orgid">机构代码</param>
        /// <param name="rids">权限列表 IPvMzYGHg1	经济评价(油)  IPvMzYGHg2	经济评价(气)</param>
        /// <returns></returns>
        bool GetRights(string funcname, out string orgname, out string orgid, out System.Collections.Generic.List<string> rids);


        //bool TestConn(System.Web.UI.Page page);
    }
}
