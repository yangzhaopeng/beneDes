using System;
using System.Collections.Generic;
using System.Web.UI;
using beneDes.ServiceReferenceRDMS;

namespace beneDes
{
    public class RDMSLogin : beneDes.IRDMSLogin
    {
        private static string UID;
        private static string UName;
        private static Dictionary<string, string[]> orglist = new Dictionary<string, string[]>();

        RDMSAccountWebServiceSoapClient client;

        public RDMSLogin(string cid, string username)
        {
            UID = cid;
            UName = username;
            client = new ServiceReferenceRDMS.RDMSAccountWebServiceSoapClient();
        }


        #region IRDMSLogin 成员

        public Boolean GetRights(string funcname, out string orgname, out string orgid, out List<string> rids)
        {
            string un;
            ServiceReferenceRDMS.RWINFO[] listrwi;

            List<string> lwi = new List<string>();

            //lwi.Add("1000000002");
            //lwi.Add("29WbbI14r5");
            //lwi.Add("r4iHlh92ph");
            //lwi.Add("IPvMzYGHg1");
            //lwi.Add("IPvMzYGHg2");
            //lwi.Add("IPvMzYGHg3");

            //orgname = "地质研究所";
            //orgid = "1000510580";
            //rids = lwi;
            //return true;

            if (CheckUser(out un, out orgname, out orgid))
            {
                if (CheckFunc(funcname, out listrwi))
                {
                    if (listrwi != null)
                    {
                        foreach (ServiceReferenceRDMS.RWINFO rwi in listrwi)
                        {
                            lwi.Add(rwi.ID.ToString());
                        }

                        if (lwi.Count > 0)
                        {
                            rids = lwi;
                            return true;
                        }
                    }
                }
            }
            rids = null;
            return false;

        }

        public bool TestConn(Page page)
        {
            ServiceReferenceRDMS.TestConnectionRequest req = new ServiceReferenceRDMS.TestConnectionRequest();
            req.requestID = "test";
            req.validationCode = "RDMS_SAFE";
            ServiceReferenceRDMS.TestConnectionResponse res = client.testConnection(req);
            if (res.returnFlag)
            {
                page.Response.Write(res.returnMessage);
                page.Response.Write(res.ExtensionData.ToString());
                page.Response.Write(res.returnCode.ToString());
                //MessageBox.Show("连接服务成功！");
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckUser(out string username, out string orgname, out string orgid)
        {
            try
            {
                ServiceReferenceRDMS.CheckGuidRequest req = new ServiceReferenceRDMS.CheckGuidRequest();
                req.requestID = "test";
                req.validationCode = "RDMS_SAFE";
                req.userGuid = UID;
                req.userName = UName;

                ServiceReferenceRDMS.CheckGuidResponse res = client.checkGuid(req);

                if (res.returnFlag)
                {
                    username = res.userName;
                    orgname = res.orgName;
                    orgid = res.orgID;
                    //MessageBox.Show("用户名：" + res.userName + "\n组织机构：" + res.orgName);
                    return true;
                }
                else
                {
                    username = "";
                    orgname = "";
                    orgid = "";
                    return false;

                }
            }
            catch (System.Exception ex)
            {
                username = "";
                orgname = "";
                orgid = "";
                return false;
            }
        }

        public bool getFuncs(out ServiceReferenceRDMS.FunInfo[] listfun)
        {
            try
            {
                ServiceReferenceRDMS.GetFunsRequest req = new ServiceReferenceRDMS.GetFunsRequest();
                req.userGuid = UID;
                req.validationCode = "RDMS_SAFE";
                req.funID = "1000003745";
                req.requestID = "test";

                ServiceReferenceRDMS.GetFunsResponse res = client.getFuns(req);

                if (res.returnFlag)
                {
                    listfun = res.Funs;
                    //MessageBox.Show("功能数：" + res.Funs.Count().ToString());
                    return true;
                }
                else
                {
                    listfun = null;
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                listfun = null;
                return false;
            }
        }

        public bool CheckFunc(string funcname, out ServiceReferenceRDMS.RWINFO[] listrw)
        {
            ServiceReferenceRDMS.FunInfo[] listfuncs;

            if (getFuncs(out listfuncs))
            {
                foreach (ServiceReferenceRDMS.FunInfo fi in listfuncs)
                {
                    if (fi.Fun_Name.Equals(funcname))
                    {
                        if (fi.IsHave)
                        {
                            listrw = fi.RWS;
                            return true;
                        }
                    }

                }
            }
            listrw = null;
            return false;
        }

        public Boolean GetOrgList(string orguid, out int orgcount, out ServiceReferenceRDMS.AppOrgInfo[] listorg)
        {
            try
            {

                ServiceReferenceRDMS.SearchUserAppOrgListRequest req = new ServiceReferenceRDMS.SearchUserAppOrgListRequest();
                req.validationCode = "RDMS_SAFE";
                req.requestID = "test";
                req.uid = UID;
                req.ouid = orguid;

                ServiceReferenceRDMS.SearchUserAppOrgListResponse rep = client.searchUserAppOrgList(req);

                if (rep.returnFlag)
                {
                    orgcount = rep.appOrgInfoSize;
                    listorg = rep.appOrgInfoList;
                    return true;
                }
                else
                {
                    orgcount = 0;
                    listorg = null;
                    return false;

                }


            }
            catch (System.Exception ex)
            {
                orgcount = 0;
                listorg = null;
                return false;

            }
            listorg = null;
            return false;
        }
        /// <summary>
        /// 获取组织机构列表
        /// </summary>
        /// <returns></returns>
        private Boolean GetOrgList()
        {
            try
            {
                int orgcount;


                ServiceReferenceRDMS.SearchUserAppOrgListRequest req = new ServiceReferenceRDMS.SearchUserAppOrgListRequest();
                req.validationCode = "RDMS_SAFE";
                req.requestID = "test";
                req.uid = UID;
                req.ouid = "";

                ServiceReferenceRDMS.SearchUserAppOrgListResponse rep = client.searchUserAppOrgList(req);

                if (rep.returnFlag)
                {
                    orgcount = rep.appOrgInfoSize;
                    foreach (ServiceReferenceRDMS.AppOrgInfo oi in rep.appOrgInfoList)
                    {
                        string[] item = new string[2];
                        item[0] = oi.appOrgParentID;  //机构父id
                        item[1] = oi.appOrgName;     //机构名称
                        //item[2] = oi.e               //机构  经济评价单位类型

                        orglist.Add(oi.appOrgID, item);
                    }
                    return true;
                }
                else
                {
                    orgcount = 0;
                    return false;

                }


            }
            catch (System.Exception ex)
            {
                return false;
            }
            return false;
        }

        private string Traversal(string oid)
        {
            string pid;
            if (orglist.ContainsKey(oid))
            {
                pid = orglist[oid][0];

                if (pid == "1000000000")
                {
                    return oid;
                }
                else
                {
                    return Traversal(pid);
                }
            }
            return "1000000000";

        }


        public void FindOrg(string oid, out string opid)
        {
            try
            {
                if (orglist == null || orglist.Count == 0)
                {
                    if (!GetOrgList())
                    {
                        opid = "1000000000";
                    }
                }

                opid = Traversal(oid);

            }
            catch (System.Exception ex)
            {
                opid = "1000000000";
            }
        }

        private Boolean GetUserOrg(out ServiceReferenceRDMS.AccountInfo[] listaccount)
        {
            try
            {
                ServiceReferenceRDMS.SearchAccountRequest req = new ServiceReferenceRDMS.SearchAccountRequest();
                ServiceReferenceRDMS.AccountInfo acc = new ServiceReferenceRDMS.AccountInfo();
                acc.uid = UID;
                req.AccountInfo = acc;

                ServiceReferenceRDMS.SearchAccountResponse rep = client.searchAccount(req);
                if (rep.accountInfoSize > 0)
                {
                    listaccount = rep.accountInfoList;
                    return true;
                }
                else
                {
                    listaccount = null;
                    return false;
                }

            }
            catch (System.Exception ex)
            {
                listaccount = null;
                return false;
            }

        }
        #endregion
    }
}
