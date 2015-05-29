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
using System.Collections.Generic;
using beneDesCYC.core;
using System.Data.OracleClient;

namespace beneDesCYC.api.month
{
    public partial class dataClearOil : beneDesCYC.core.UI.corePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string month = Convert.ToString(Session["month"]);
            string cyc_id = Convert.ToString(Session["cyc_id"]);
            List<string> listName = new List<string>();
            listName.Add("djsj");
            listName.Add("kfsj");
            listName.Add("qksj");
            listName.Add("pjdysj");
            listName.Add("djfy");

            List<string> listNameGGFY = new List<string>();
            listNameGGFY.Add("CYC"); 
            listNameGGFY.Add("CQC");
            listNameGGFY.Add("ZYQ");
            listNameGGFY.Add("QK");
            listNameGGFY.Add("ZXZ");
            listNameGGFY.Add("ZRZ");
            listNameGGFY.Add("JQZ");
            listNameGGFY.Add("JQZZ");
            listNameGGFY.Add("JHC");

            bool result = true;
            string msg = "";
            int num = 0;
            for (int item = 0; item < listName.Count; item++)
            {
                string code = _getParam(listName[item]);

                if (int.TryParse(code, out num))
                {
                    result = dataClear(listName[item], out msg);
                    if (listName[item] == "djfy")
                        result = dataClear("fee_cross", out msg);
                }
            }
            for (int item = 0; item < listNameGGFY.Count; item++)
            {
                string code = _getParam(listNameGGFY[item]);
                if (int.TryParse(code, out num))
                {
                    result = deleteGgfyFee(listNameGGFY[item], out msg);
                }
            }
            _return(result, msg, "");

        }
        public bool deleteGgfyFee(string fname, out string msg)
        {
            SqlHelper sqlhelper = new SqlHelper();
            msg = "";
            string fselect = string.Format("delete from ggfy where ny='{0}' and  ft_type=(select ft_type from FT_TYPE where FT_TYPE='{1}') and cyc_id='{2}'", Convert.ToString(Session["month"]), fname, Convert.ToString(Session["cyc_id"]));
            int delCount = sqlhelper.ExcuteSql(fselect);
            if (delCount >= 0)
            {
                msg += "删除" + delCount + "条数据！";
                return true;
            }
            else
            {
                msg += "清空" + fname + "失败!";
                return false;
            }

        }
        private bool dataClear(string tableName, out string msg)
        {
            SqlHelper sql = new SqlHelper();
            msg = "";
            var as_date = new OracleParameter("as_date", OracleType.VarChar);
            as_date.Value = Session["month"].ToString(); ;
            var as_table = new OracleParameter("as_table", OracleType.VarChar);
            as_table.Value = tableName;
            var as_cyc = new OracleParameter("as_cyc", OracleType.VarChar);
            as_cyc.Value = Session["cyc_id"].ToString();

            int delCount = sql.ExecuteTranErrorCount("owcbspkg_ydsj.up_deleteydsj", CommandType.StoredProcedure,
                new OracleParameter[] { as_date, as_table, as_cyc });
            if (delCount >= 0)
            {
                msg += "删除" + delCount + "条数据！";
                return true;
            }
            else
            {
                msg = "清空" + tableName + "失败！";
                return false;
            }
        }

    }
}

