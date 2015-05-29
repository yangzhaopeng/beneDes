using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using beneDesCYC.model.month;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Collections.Generic;

namespace beneDesCYC.api.month
{
    public partial class qjsj : beneDesCYC.core.UI.corePage
    {


        protected void Page_Load(object sender, EventArgs e)
        {

            string _type = _getParam("_type");

            switch (_type)
            {
                case "getZRZTree":
                    getZRZTree();
                    break;
                case "getWellList":
                    getWellList();
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

        /// <summary>
        /// 获取精确到井组的树结构数据
        /// </summary>
        public void getZRZTree()
        {
            //string cyc_id = Session["depId"].ToString();
            string cyc_id = Session["cqc_id"].ToString();
            string month = Session["month"].ToString();
            string cyc_name = Session["depName"].ToString();
            DataTable dt = qjsj_model.getAllZRZ(cyc_id, month);

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
                    zyqObj["id"] = "zyq_" + zyq + i;
                    zyqObj["text"] = zyq_name;
                    zyqObj["expanded"] = true;
                    zyqObj["children"] = new JArray();
                    zyqObj["cyc"] = cyc_id;
                    zyqObj["zyq"] = zyq;
                    obj.Add(zyqObj);
                }

                //if (item[zyq][zxz] == null)
                //{
                //    item[zyq][zxz] = new JObject();
                //    zyq_index = obj.Count - 1;
                //    JObject zxzObj = new JObject();
                //    zxzObj["id"] = "zxz_" + zxz + i;
                //    zxzObj["text"] = zxz;
                //    zxzObj["expanded"] = true;
                //    //   zxzObj["children"] = new JArray();
                //    zxzObj["leaf"] = true;
                //    zxzObj["cyc"] = cyc_id;
                //    zxzObj["zyq"] = zyq;
                //    zxzObj["zxz"] = zxz;
                //    ((JArray)obj[zyq_index]["children"]).Add(zxzObj);
                //}
                //zyq_index = obj.Count - 1;
                //zxz_index = ((JArray)obj[zyq_index]["children"]).Count - 1;
                //JObject zrzObj = new JObject();
                //zrzObj["id"] = "zrz_" + zrz + i;
                //zrzObj["text"] = zrz;
                //zrzObj["leaf"] = true;
                //zrzObj["cyc"] = cyc_id;
                //zrzObj["zyq"] = zyq;
                //zrzObj["zxz"] = zxz;
                //zrzObj["zrz"] = zrz;
                //((JArray)((JArray)obj[zyq_index]["children"])[zxz_index]["children"]).Add(zrzObj);
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

        /// <summary>
        /// 根据查询条件获取所有井列表信息
        /// </summary>
        public void getWellList()
        {
            string cyc_id = Session["cqc_id"].ToString();
            //string cyc_id = Session["depId"].ToString();
            string month = Session["month"].ToString();
            string cyc_name = Session["depName"].ToString();



            int limit = Convert.ToInt32(Request.Params["limit"]);
            int start = Convert.ToInt32(Request.Params["start"]);

            int page = start / limit + 1;
            int begin = page * limit - limit + 1;
            int end = page * limit;

            int total = totalCount();

            //DataTable dtCount = qjsj_model.getCount(cyc_id, month, _getParam("searchWord"), _getParam("zyq"), _getParam("zxz"), _getParam("zrz"));

            //int rcount = dtCount.Rows.Count;
            //int pageCount = rcount / 100 + 1;
            //Session["pageCount"] = pageCount;
            //Session["pageNow"] = pageNow;

            DataTable dt = qjsj_model.getWellList(cyc_id, month, _getParam("searchWord"), _getParam("zyq"), end, begin);

            _return(true, "", dt, total);
        }

        public int totalCount()
        {
            string cyc_id = Session["cqc_id"].ToString();
            string month = Session["month"].ToString();
            string cyc_name = Session["depName"].ToString();

            DataTable dtCount = qjsj_model.getCount(cyc_id, month, _getParam("searchWord"), _getParam("zyq"));

            int rcount = dtCount.Rows.Count;

            return rcount;
        }

        public void fileUpload()
        {
            HttpPostedFile postedFile = Request.Files["UPLOADFILE"];
            string fileName = postedFile.FileName;
            string tempPath = System.Configuration.ConfigurationManager.AppSettings["NewsFolderPath"];
            string savePath = Server.MapPath(tempPath);
            string sExtension = fileName.Substring(fileName.LastIndexOf('.'));
            if (!Directory.Exists(savePath))
            { Directory.CreateDirectory(savePath); }
            string sNewFileName = DateTime.Now.ToString("yyyymmdd");
            postedFile.SaveAs(savePath + @"/" + sNewFileName + sExtension);

        }

        public JObject getWellParam()
        {
            JObject obj = new JObject();
            obj["NY"] = _getParam("NY", true);
            obj["JH"] = _getParam("JH", true);
            obj["ZYQ"] = _getParam("ZYQ", true);
            obj["QK"] = _getParam("QK", true);
            obj["ZXZ"] = _getParam("ZXZ", true);
            obj["ZRZ"] = _getParam("ZRZ", true);
            obj["CYJXH"] = _getParam("CYJXH", true);
            obj["CYJMPGL"] = _getParam("CYJMPGL", true);
            obj["DJGL"] = _getParam("DJGL", true);
            obj["FZL"] = _getParam("FZL", true);
            obj["YQLX"] = _getParam("YQLX", true);
            obj["XSYP"] = _getParam("XSYP", true);
            obj["TCRQ"] = _getParam("TCRQ", true);
            obj["JB"] = _getParam("JB", true);
            obj["DJDB"] = _getParam("DJDB", true);
            obj["SSYT"] = _getParam("SSYT", true);
            obj["PJDY"] = _getParam("PJDY", true);
            obj["YCLX"] = _getParam("YCLX", true);

            return obj;
        }

        /// <summary>
        /// 添加
        /// </summary>
        public void add()
        {
            JObject obj = getWellParam();
            obj["DJ_ID"] = obj["ZYQ"].ToString() + "$" + obj["JH"].ToString();

            //string CYC_ID = Session["depId"].ToString();
            string CYC_ID = Session["cqc_id"].ToString();

            DataTable dt = qjsj_model.getOneDjsj(obj["NY"].ToString(), CYC_ID, obj["DJ_ID"].ToString());

            if (dt.Rows.Count > 0)
            {
                _return(false, "该井信息已存在，您可以进行修改！", "null");
            }

            if (qjsj_model.add(obj, CYC_ID))
            {
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加失败！", "null");
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public void edit()
        {
            JObject obj = getWellParam();
            obj["DJ_ID"] = _getParam("DJ_ID", true);
            obj["DJ_ID_new"] = obj["ZYQ"].ToString() + "$" + obj["JH"].ToString();

            //string CYC_ID = Session["depId"].ToString();
            string CYC_ID = Session["cqc_id"].ToString();
            if (obj["DJ_ID_new"].ToString() != obj["DJ_ID"].ToString())
            {
                DataTable dt = qjsj_model.getOneDjsj(obj["NY"].ToString(), CYC_ID, obj["DJ_ID_new"].ToString());

                if (dt.Rows.Count > 0)
                {
                    _return(false, "井号错误：本作业区已经有该井的基础信息！", "null");
                }
            }

            if (qjsj_model.edit(obj, CYC_ID))
            {
                _return(true, "编辑单井基础信息成功！", "null");
            }
            else
            {
                _return(false, "编辑单井基础信息失败！", "null");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void delete()
        {
            string NY = _getParam("NY", true);
            string DJ_ID = _getParam("DJ_ID", true);
            //string CYC_ID = Session["depId"].ToString();
            string CYC_ID = Session["cqc_id"].ToString();
            if (qjsj_model.delete(NY, DJ_ID, CYC_ID))
            {
                _return(true, "删除单井基础信息成功！", "null");
            }
            else
            {
                _return(false, "删除单井基础信息失败！", "null");
            }
        }



    }
}
