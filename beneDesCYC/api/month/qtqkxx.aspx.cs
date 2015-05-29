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
using beneDesCYC.model.month;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace beneDesCYC.api.month
{
    public partial class qtqkxx : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _type = _getParam("_type");

            switch (_type)
            {
                case "getAllList":
                    getAllList();
                    break;

                case "add":
                    add();
                    break;
                case "edit":
                    edit();
                    break;
                case "delete":
                    delete();
                    break;
            }
        }

        /*     /// <summary>
             /// 获取精确到井组的树结构数据
             /// </summary>
             public void getZRZTree()
             {
                 string cyc_id = Session["depId"].ToString();
                 string month = Session["month"].ToString();
                 string cyc_name = Session["depName"].ToString();
                 DataTable dt = djsj_model.getAllZRZ(cyc_id, month);

                 JObject item = new JObject();
                 JArray obj = new JArray();

                 for (int i = 0; i < dt.Rows.Count; i++)
                 {
                     string zyq = dt.Rows[i]["ZYQ"].ToString();
                     string zyq_name = dt.Rows[i]["ZYQ_NAME"].ToString();
                     string zxz = dt.Rows[i]["ZXZ"].ToString();
                     string zrz = dt.Rows[i]["ZRZ"].ToString();

                     int zyq_index;
                     int zxz_index;

                     if (item[zyq] == null)
                     {
                         item[zyq] = new JObject();
                         JObject zyqObj = new JObject();
                         zyqObj["id"] = "zyq_" + zyq;
                         zyqObj["text"] = zyq_name;
                         zyqObj["expanded"] = true;
                         zyqObj["children"] = new JArray();
                         zyqObj["cyc"] = cyc_id;
                         zyqObj["zyq"] = zyq;
                         obj.Add(zyqObj);
                     }

                     if (item[zyq][zxz] == null)
                     {
                         item[zyq][zxz] = new JObject();
                         zyq_index = obj.Count - 1;
                         JObject zxzObj = new JObject();
                         zxzObj["id"] = "zxz_" + zxz;
                         zxzObj["text"] = zxz;
                         zxzObj["expanded"] = true;
                         zxzObj["children"] = new JArray();
                         zxzObj["cyc"] = cyc_id;
                         zxzObj["zyq"] = zyq;
                         zxzObj["zxz"] = zxz;
                         ((JArray)obj[zyq_index]["children"]).Add(zxzObj);
                     }

                     zyq_index = obj.Count - 1;
                     zxz_index = ((JArray)obj[zyq_index]["children"]).Count - 1;
                     JObject zrzObj = new JObject();
                     zrzObj["id"] = "zrz_" + zrz;
                     zrzObj["text"] = zrz;
                     zrzObj["leaf"] = true;
                     zrzObj["cyc"] = cyc_id;
                     zrzObj["zyq"] = zyq;
                     zrzObj["zxz"] = zxz;
                     zrzObj["zrz"] = zrz;
                     ((JArray)((JArray)obj[zyq_index]["children"])[zxz_index]["children"]).Add(zrzObj);
                 }

                 JObject ret = new JObject();
                 ret["id"] = "cyc_" + cyc_id;
                 ret["text"] = cyc_name;
                 ret["cyc"] = cyc_id;

                 if (obj.Count > 0)
                 {
                     ret["children"] = obj;
                     ret["expanded"] = true;
                 }
                 else
                 {
                     ret["leaf"] = true;
                 }

                 _return(true, "", ret);
             }
     */
        /// <summary>
        /// 根据查询条件获取所有井列表信息
        /// </summary>
        public void getAllList()
        {
            DataTable dt = null;
            try
            {
                dt = qtqkxx_model.getAllList(Session["cqc_id"].ToString(), Session["month"].ToString());
            }
            catch
            {
                dt = null;
            }
            _return(true, "", dt);
        }
        /*   public void getWellList()
           {
               string cyc_id = Session["depId"].ToString();
               string month = Session["month"].ToString();
               string cyc_name = Session["depName"].ToString();

               DataTable dt = djsj_model.getWellList(cyc_id, month, _getParam("searchWord"), _getParam("zyq"), _getParam("zxz"), _getParam("zrz"));
               _return(true, "", dt);
           }
           */
        public JObject getWellParam()
        {
            JObject obj = new JObject();
            obj["NY"] = _getParam("NY", true);
            obj["QKMC"] = _getParam("QKMC", true);
            obj["SSYT"] = _getParam("SSYT", true);
            obj["YCLX"] = _getParam("YCLX", true);
            obj["DYHYMJ"] = _getParam("DYHYMJ", true);
            obj["DYDZCL"] = _getParam("DYDZCL", true);
            obj["DYKCCL"] = _getParam("DYKCCL", true);
            obj["YCZS"] = _getParam("YCZS", true);
            obj["PJSTL"] = _getParam("PJSTL", true);
            obj["DXYYND"] = _getParam("DXYYND", true);
            obj["YJZJS"] = _getParam("YJZJS", true);
            obj["YJKJS"] = _getParam("YJKJS", true);
            obj["SJZJS"] = _getParam("SJZJS", true);
            obj["SJKJS"] = _getParam("SJKJS", true);
            obj["ZSJ"] = _getParam("ZSJ", true);
            obj["LJZSL"] = _getParam("LJZSL", true);
            obj["ZQJZJS"] = _getParam("ZQJZJS", true);
            obj["ZQJKJZ"] = _getParam("ZQJKJZ", true);
            obj["ZQL"] = _getParam("ZQL", true);
            obj["LJZQL"] = _getParam("LJZQL", true);
            obj["CYOUL"] = _getParam("CYOUL", true);
            obj["LJCYOUL"] = _getParam("LJCYOUL", true);
            obj["CYL"] = _getParam("CYL", true);
            obj["LJCYL"] = _getParam("LJCYL", true);
            obj["CQL"] = _getParam("CQL", true);
            obj["LJCQL"] = _getParam("LJCQL", true);
            obj["XSYP"] = _getParam("XSYP", true);
            obj["SFPJ"] = _getParam("SFPJ", true);
            return obj;
        }

        /// <summary>
        /// 添加
        /// </summary>
        public void add()
        {
            JObject obj = getWellParam();
            string NY = _getParam("NY", true);
            string QKMC = _getParam("QKMC", true);
            string SSYT = _getParam("SSYT", true);
            string YCLX = _getParam("YCLX", true);
            string DYHYMJ = _getParam("DYHYMJ", true);
            string DYDZCL = _getParam("DYDZCL", true);
            string DYKCCL = _getParam("DYKCCL", true);
            string YCZS = _getParam("YCZS", true);
            string PJSTL = _getParam("PJSTL", true);

            string PJKXD = _getParam("PJKXD", true);
            string LHQHL = _getParam("LHQHL", true);
            string NXYHL = _getParam("NXYHL", true);
            string YSDCYL = _getParam("YSDCYL", true);
            string MQDCYL = _getParam("MQDCYL", true);
            string KFJD = _getParam("KFJD", true);
            string KFFS = _getParam("KFFS", true);
            string QJZJS = _getParam("QJZJS", true);
            string QJKJS = _getParam("QJKJS", true);
            string CCLX = _getParam("CCLX", true);
            string SFPJ = _getParam("SFPJ", true);
            string CYC_ID = Session["cqc_id"].ToString();

            DataTable dt = qtqkxx_model.getOneQKXX(NY, QKMC, CYC_ID);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该区块信息已存在，您可以进行修改！", "null");
            }

            if (qtqkxx_model.add(NY, QKMC, SSYT, DYHYMJ, DYDZCL, DYKCCL, YCZS, PJSTL, PJKXD, LHQHL, NXYHL, YSDCYL, MQDCYL, YCLX, KFJD, KFFS, QJZJS, QJKJS, CCLX, SFPJ, CYC_ID))
            {
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加区块信息失败！", "null");
            }
        }
        /*      public void add()
              {
                  JObject obj = getWellParam();
                  obj["DJ_ID"] = obj["ZYQ"].ToString() + "$" + obj["JH"].ToString();

                  string CYC_ID = Session["depId"].ToString();

                  DataTable dt = djsj_model.getOneDjsj(obj["NY"].ToString(), CYC_ID, obj["DJ_ID"].ToString());

                  if (dt.Rows.Count > 0)
                  {
                      _return(false, "该井信息已存在，您可以进行修改！", "null");
                  }

                  if (djsj_model.add(obj, CYC_ID))
                  {
                      _return(true, "添加成功！", "null");
                  }
                  else
                  {
                      _return(false, "添加失败！", "null");
                  }
              }
      */
        /// <summary>
        /// 编辑
        /// </summary>
        public void edit()
        {
            // JObject obj = getWellParam();
            string NY = _getParam("NY", true);
            string QKMC = _getParam("QKMC", true);
            string SSYT = _getParam("SSYT", true);
            string YCLX = _getParam("YCLX", true);
            string DYHYMJ = _getParam("DYHYMJ", true);
            string DYDZCL = _getParam("DYDZCL", true);
            string DYKCCL = _getParam("DYKCCL", true);
            string YCZS = _getParam("YCZS", true);
            string PJSTL = _getParam("PJSTL", true);

            string PJKXD = _getParam("PJKXD", true);
            string LHQHL = _getParam("LHQHL", true);
            string NXYHL = _getParam("NXYHL", true);
            string YSDCYL = _getParam("YSDCYL", true);
            string MQDCYL = _getParam("MQDCYL", true);
            string KFJD = _getParam("KFJD", true);
            string KFFS = _getParam("KFFS", true);
            string QJZJS = _getParam("QJZJS", true);
            string QJKJS = _getParam("QJKJS", true);
            string CCLX = _getParam("CCLX", true);
            string SFPJ = _getParam("SFPJ", true);
            string CYC_ID = Session["cqc_id"].ToString();

            if (qtqkxx_model.edit(NY, QKMC, SSYT, DYHYMJ, DYDZCL, DYKCCL, YCZS, PJSTL, PJKXD, LHQHL, NXYHL, YSDCYL, MQDCYL, YCLX, KFJD, KFFS, QJZJS, QJKJS, CCLX, SFPJ, CYC_ID))
            {
                _return(true, "编辑区块信息成功！", "null");
            }
            else
            {
                _return(false, "编辑区块信息失败！", "null");
            }
        }
        /*      public void edit()
              {
                  JObject obj = getWellParam();
                  obj["DJ_ID"] = _getParam("DJ_ID", true);
                  obj["DJ_ID_new"] = obj["ZYQ"].ToString() + "$" + obj["JH"].ToString();

                  string CYC_ID = Session["depId"].ToString();

                  if (obj["DJ_ID_new"].ToString() != obj["DJ_ID"].ToString())
                  {
                      DataTable dt = djsj_model.getOneDjsj(obj["NY"].ToString(), CYC_ID, obj["DJ_ID_new"].ToString());

                      if (dt.Rows.Count > 0)
                      {
                          _return(false, "井号错误：本作业区已经有该井的基础信息！", "null");
                      }
                  }

                  if (djsj_model.edit(obj, CYC_ID))
                  {
                      _return(true, "编辑单井基础信息成功！", "null");
                  }
                  else
                  {
                      _return(false, "编辑单井基础信息失败！", "null");
                  }
              }
      */
        /// <summary>
        /// 删除
        /// </summary>
        public void delete()
        {
            string NY = _getParam("NY", true);
            string QKMC = _getParam("QKMC", true);
            string CYC_ID = Session["cqc_id"].ToString();

            if (qtqkxx_model.delete(NY, QKMC, CYC_ID))
            {
                _return(true, "删除区块信息成功！", "null");
            }
            else
            {
                _return(false, "删除区块信息失败！", "null");
            }
        }
    }
}
