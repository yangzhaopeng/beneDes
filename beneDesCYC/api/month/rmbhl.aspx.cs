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
using beneDesCYC.model.system;

namespace beneDesCYC.api.month
{
    public partial class rmbhl : beneDesCYC.core.UI.corePage
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
                dt = rmbhl_model.getAllList(Session["cyc_id"].ToString(), Session["month"].ToString());
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
            // string YQLXDM = _getParam("YQLXDM", true);
            string HL = _getParam("HL", true);

            string CYC_ID = Session["cyc_id"].ToString();

            DataTable dt = rmbhl_model.getOneHL(NY, CYC_ID);

            if (dt.Rows.Count > 0)
            {
                _return(false, "该人民币汇率已存在，您可以进行修改！", "null");
            }

            if (rmbhl_model.add(NY, HL, CYC_ID))
            {
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加人民币汇率失败！", "null");
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public void edit()
        {
            string NY = _getParam("NY", true);
            // string DEP_ID = _getParam("DEP_ID", true);
            //string YQLXDM = _getParam("YQLXDM", true);
            string HL = _getParam("HL", true);
            //decimal hl = 0.0000M;
            //decimal.TryParse(HL, out hl);
            //if (hl <= 0.0000M || hl > 100.0000M)
            //{
            //    _return(false, "请输入正确的人民币汇率！", "null");
            //}

            //if (HL.Contains('.'))
            //{
            //    int lengthAfterDot = HL.Length - HL.IndexOf('.') - 1;
            //    if (lengthAfterDot > 4 || lengthAfterDot < 0)
            //    {
            //        _return(false, "请输入正确的人民币汇率(允许四位小数)！", "null");
            //    }
            //}


            string CYC_ID = Session["cyc_id"].ToString();

            if (rmbhl_model.edit(NY, HL, CYC_ID))
            {
                _return(true, "编辑人民币汇率成功！", "null");
            }
            else
            {
                _return(false, "编辑人民币汇率失败！", "null");
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
            //  string DLY = _getParam("DLY", true);

            string CYC_ID = Session["cyc_id"].ToString();

            if (rmbhl_model.delete(NY, CYC_ID))
            {
                _return(true, "删除人民币汇率成功！", "null");
            }
            else
            {
                _return(false, "删除人民币汇率失败！", "null");
            }
        }
    }
}

