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
    public partial class zjfybj : beneDesCYC.core.UI.corePage
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
            string cyc_id = Session["cyc_id"].ToString();
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

                //if (item[zyq][zxz] == null)
                //{
                //    item[zyq][zxz] = new JObject();
                //    zyq_index = obj.Count - 1;
                //    JObject zxzObj = new JObject();
                //    zxzObj["id"] = "zxz_" + zxz;
                //    zxzObj["text"] = zxz;
                //    zxzObj["expanded"] = true;
                // //   zxzObj["children"] = new JArray();
                //    zxzObj["leaf"] = true;
                //    zxzObj["cyc"] = cyc_id;
                //    zxzObj["zyq"] = zyq;
                //    zxzObj["zxz"] = zxz;
                //    ((JArray)obj[zyq_index]["children"]).Add(zxzObj);
                //}

                //zyq_index = obj.Count - 1;
                //zxz_index = ((JArray)obj[zyq_index]["children"]).Count - 1;
                //JObject zrzObj = new JObject();
                //zrzObj["id"] = "zrz_" + zrz;
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
            //string cyc_id = Session["depId"].ToString();
            string cyc_id = Session["cyc_id"].ToString();
            string cqc_id = cyc_id;
            string month = Session["month"].ToString();
            string cyc_name = Session["depName"].ToString();

            int limit = Convert.ToInt32(Request.Params["limit"]);
            int start = Convert.ToInt32(Request.Params["start"]);

            int page = start / limit + 1;
            int begin = page * limit - limit + 1;
            int end = page * limit;

            int total = totalCount();

            DataTable dt = fee_cross_model.getWellList(cyc_id, cqc_id, month, _getParam("searchWord"), _getParam("zyq"), end, begin);
            _return(true, "", dt, total);
        }

        public int totalCount()
        {
            //string cyc_id = Session["depId"].ToString();
            string cyc_id = Session["cyc_id"].ToString();
            string cqc_id = cyc_id;
            string month = Session["month"].ToString();
            string cyc_name = Session["depName"].ToString();
            string depId = Session["depId"].ToString();

            DataTable dtCount = fee_cross_model.getCount(cyc_id, cqc_id, month, _getParam("searchWord"), _getParam("zyq"));

            int rcount = dtCount.Rows.Count;

            return rcount;
        }

        public JObject getWellParam()
        {
            //'NY', 'DJ_ID', 'JH', 'ZYQ', 'ZYQ_NAME',
            //    'QTCLF', '', '', '', '', 'QYWZRF_RYF',
            //    '', '', '', '', 'CJSJF_RYF', '', '', 'WHXLF_RYF',
            //    '', 'YQCLF_RYF', '', '', 'YSF_RYF', '', '', '', '',
            //    '', '', '', '', '', '', '', '', '', '',
            //    '', '', ''   

            JObject obj = new JObject();
            obj["NY"] = _getParam("NY", true);
            obj["JH"] = _getParam("JH", true);
            obj["ZYQ"] = _getParam("ZYQ", true);
            obj["QTCLF"] = _getParam("QTCLF", true);
            obj["ZJRLF"] = _getParam("ZJRLF", true);
            obj["QTDLF"] = _getParam("QTDLF", true);
            obj["QYWZRF"] = _getParam("QYWZRF", true);
            obj["CSXZYLWF"] = _getParam("CSXZYLWF", true);
            obj["WHXZYLWF"] = _getParam("WHXZYLWF", true);
            obj["CJCSF"] = _getParam("CJCSF", true);
            obj["QTWHJXLF"] = _getParam("QTWHJXLF", true);
            obj["YQCLF"] = _getParam("YQCLF", true);
            obj["QTHSF"] = _getParam("QTHSF", true);
            obj["YSF"] = _getParam("YSF", true);
            obj["LYF"] = _getParam("LYF", true);
            obj["QTZJF"] = _getParam("QTZJF", true);
            obj["CKGLF"] = _getParam("CKGLF", true);
            obj["RGCB"] = _getParam("RGCB", true);
            obj["ZYYQCP"] = _getParam("ZYYQCP", true);
            obj["ZJZH"] = _getParam("ZJZH", true);
            obj["DDF"] = _getParam("DDF", true);
            obj["JJF"] = _getParam("JJF", true);
            obj["QTCSF"] = _getParam("QTCSF", true);
            obj["QTTCF"] = _getParam("QTTCF", true);
            obj["QTYSF"] = _getParam("QTYSF", true);

            obj["SKF"] = _getParam("SKF", true);
            obj["TCF"] = _getParam("TCF", true);
            obj["XJF"] = _getParam("XJF", true);
            obj["XLF"] = _getParam("XLF", true);
            obj["YBCLF"] = _getParam("YBCLF", true);

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
            string CYC_ID = Session["cyc_id"].ToString();

            DataTable dt = fee_cross_model.getOneZjfybj(obj["NY"].ToString(), CYC_ID, obj["DJ_ID"].ToString());

            if (dt.Rows.Count > 0)
            {
                _return(false, "该费用已存在，您可以进行修改！", "null");
            }

            if (fee_cross_model.add(obj, CYC_ID))
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

            //string CYC_ID = Session["depId"].ToString();
            string CYC_ID = Session["cyc_id"].ToString();
            #region @yzp 不允许修改井号作业区
            //obj["DJ_ID"] = _getParam("DJ_ID", true);
            //obj["DJ_ID_new"] = obj["ZYQ"].ToString() + "$" + obj["JH"].ToString();
            //if (obj["DJ_ID_new"].ToString() != obj["DJ_ID"].ToString())
            //{
            //    DataTable dt = fee_cross_model.getOneZjfybj(obj["NY"].ToString(), CYC_ID, obj["DJ_ID_new"].ToString());

            //    if (dt.Rows.Count > 0)
            //    {
            //        _return(false, "井号错误：本作业区已经有该井的费用信息！", "null");
            //    }
            //}
            #endregion
            if (fee_cross_model.edit(obj, CYC_ID))
            {
                _return(true, "编辑单井费用成功！", "null");
            }
            else
            {
                _return(false, "编辑单井费用失败！", "null");
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
            string CYC_ID = Session["cyc_id"].ToString();

            if (fee_cross_model.delete(NY, DJ_ID, CYC_ID))
            {
                _return(true, "删除单井费用成功！", "null");
            }
            else
            {
                _return(false, "删除单井费用失败！", "null");
            }
        }
    }
}
