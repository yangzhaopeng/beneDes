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
    public partial class pjdyqk : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _type = _getParam("_type");
            switch (_type)
            {
                case "getAllList":
                    getAllList();
                    break;
                case "getUnSelectQkList":
                    getUnSelectQkList();
                    break;
                case "add":
                    add();
                    break;
                case "delete":
                    delete();
                    break;
                case "calc":
                    calc();
                    break;
            }
        }

        //获取未分配给当前评价单元的区块
        private void getUnSelectQkList()
        {
            DataTable dt = null;
            try
            {
                dt = pjdyqk_model.getUnSelectQkList(Convert.ToString(Session["cyc_id"]), _getParam("pjdy"), _getParam("month"));
            }
            catch
            {
                dt = null;
            }
            _return(true, "", dt);
        }


        public void getAllList()
        {
            DataTable dt = null;
            try
            {
                dt = pjdyqk_model.getAllList(Convert.ToString(Session["cyc_id"]), _getParam("pjdy"), _getParam("month"));
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
            string PJDY = _getParam("PJDYMC", true);
            string QKS = _getParam("QKS", true);

            string CYC_ID = Session["cyc_id"].ToString();

            //if (pjdyqk_model.delete(NY, PJDY, CYC_ID))
            //{
            if (pjdyqk_model.add(NY, PJDY, QKS, CYC_ID))
            {
                _return(true, "添加成功！", "null");
            }
            else
            {
                _return(false, "添加失败！", "null");
            }
            //}
        }


        /// <summary>
        /// 删除
        /// </summary>
        public void deleteAll()
        {
            string NY = _getParam("NY", true);
            string PJDY = _getParam("PJDY", true);

            string CYC_ID = Session["cyc_id"].ToString();

            if (pjdyqk_model.deleteAll(NY, PJDY, CYC_ID))
            {
                _return(true, "删除评价单元信息成功！", "null");
            }
            else
            {
                _return(false, "删除评价单元信息失败！", "null");
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        public void delete()
        {
            string NY = _getParam("NY", true);
            string PJDY = _getParam("PJDYMC", true);
            string QKS = _getParam("QKS", true);
            string CYC_ID = Session["cyc_id"].ToString();

            if (pjdyqk_model.delete(NY, PJDY,QKS, CYC_ID))
            {
                _return(true, "删除评价单元信息成功！", "null");
            }
            else
            {
                _return(false, "删除评价单元区块失败！", "null");
            }
        }
        /// <summary>
        /// 计算出评价单元信息
        /// </summary>
        public void calc()
        {
            string NY = _getParam("NY", true);
            string PJDY = _getParam("PJDYMC", true);
            string CYC_ID = Session["cyc_id"].ToString();

            if (pjdyqk_model.calc(NY, PJDY, CYC_ID))
            {
                _return(true, "计算成功！", "null");
            }
            else
            {
                _return(false, "计算失败！", "null");
            }
        }
    }
}

