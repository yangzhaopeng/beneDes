﻿using System;
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
using beneDesCYC.model.system;

namespace beneDesCYC.api.month
{
    public partial class dtb : beneDesCYC.core.UI.corePage
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


        public void getAllList()
        {
            DataTable dt = null;
            try
            {
                dt = dtb_model.getAllList(Session["cyc_id"].ToString(), Session["month"].ToString());
            }
            catch
            {
                dt = null;
            }
            _return(true, "", dt);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public void add()
        {
            string NY = _getParam("NY", true);
            // string DEP_ID = _getParam("DEP_ID", true);
            string XSYPDM = _getParam("XSYPDM", true);
            string DTB = _getParam("DTB", true);

            string CYC_ID = Session["cyc_id"].ToString();

            DataTable dt = dtb_model.getOneDTB(NY, XSYPDM, CYC_ID);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该吨桶比已存在，您可以进行修改！", "null");
            }

            if (dtb_model.add(NY, XSYPDM, DTB, CYC_ID))
            {
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加吨桶比失败！", "null");
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public void edit()
        {
            string NY = _getParam("NY", true);
            // string DEP_ID = _getParam("DEP_ID", true);
            string XSYPDM = _getParam("XSYPDM", true);
            string DTB  = _getParam("DTB", true);

            string CYC_ID = Session["cyc_id"].ToString();

            if (dtb_model.edit(NY, XSYPDM, DTB, CYC_ID))
            {
                _return(true, "编辑吨桶比成功！", "null");
            }
            else
            {
                _return(false, "编辑吨桶比失败！", "null");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void delete()
        {
            string NY = _getParam("NY", true);
            //string YQSPL = _getParam("YQSPL", true);
            // string DEP_ID = _getParam("DEP_ID", true);
            string XSYPDM = _getParam("XSYPDM", true);

            string CYC_ID = Session["cyc_id"].ToString();

            if (dtb_model.delete(NY, XSYPDM, CYC_ID))
            {
                _return(true, "删除吨桶比成功！", "null");
            }
            else
            {
                _return(false, "删除吨桶比失败！", "null");
            }
        }
    }
}

