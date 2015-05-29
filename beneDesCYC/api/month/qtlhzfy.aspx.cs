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
using beneDesCYC.core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.OracleClient;

namespace beneDesCYC.api.month
{
    public partial class qtlhzfy : beneDesCYC.core.UI.corePage
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
                //case "add":
                //    add();
                //    break;
                case "edit":
                    edit();
                    break;
                //case "delete":
                //    delete();
                //    break;
            }
        }

        /// <summary>
        /// 获取精确到井组的树结构数据
        /// </summary>
        public void getZRZTree()
        {
            string cyc_id = Session["cqc_id"].ToString();
            string month = Session["month"].ToString();
            string cyc_name = Session["depName"].ToString();
            DataTable dt = djsj_model.getZYQList(cyc_id, month);

            JObject item = new JObject();
            JArray obj = new JArray();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string zyq = dt.Rows[i]["ZYQ"].ToString();
                string zyq_name = dt.Rows[i]["ZYQ_NAME"].ToString();
                string lhz = dt.Rows[i]["LHZ"].ToString();
                if (item[zyq] == null)
                {
                    item[zyq] = new JObject();
                    JObject zyqObj = new JObject();
                    zyqObj["id"] = "zyq_" + zyq;
                    zyqObj["text"] = zyq_name;
                    zyqObj["expanded"] = false;
                    zyqObj["children"] = new JArray();
                    //  zyqObj["leaf"] = true;
                    zyqObj["cyc"] = cyc_id;
                    zyqObj["zyq"] = zyq;
                    obj.Add(zyqObj);
                }
                int zyq_index;
                if (item[zyq][lhz] == null)
                {
                    item[zyq][lhz] = new JObject();
                    zyq_index = obj.Count - 1;
                    JObject objTemp = new JObject();
                    objTemp["id"] = "lhz_" + lhz;
                    objTemp["text"] = lhz;
                    objTemp["expanded"] = true;
                    objTemp["children"] = new JArray();
                    objTemp["cyc"] = cyc_id;
                    objTemp["zyq"] = zyq;
                    objTemp["lhz"] = lhz;
                    ((JArray)obj[zyq_index]["children"]).Add(objTemp);
                }
                //int zxz_index;
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
            string _type = "";
            string source_id = "";
            string cyc_id = Session["cqc_id"].ToString();
            string month = Session["month"].ToString();
            string cyc_name = Session["depName"].ToString();
            //_getParam("searchWord"), _getParam("zyq"), _getParam("zxz"), _getParam("zrz")
            //if (_getParam("zyq") != null && _getParam("zyq") != "")
            //{
            //    _type = "ZYQ";
            //    source_id = _getParam("zyq");
            //}
            //if (_getParam("zxz") != null && _getParam("zxz") != "")
            //{
            //    _type = "ZXZ";
            //    source_id = _getParam("zyq") + "$" + _getParam("zxz");
            //}
            if (_getParam("lhz") != null && _getParam("lhz") != "")
            {
                _type = "LHZ";
                source_id = _getParam("lhz");
                Session["lhz"] = source_id;
            }
            else if (Session["lhz"] != null && Session["lhz"].ToString() != "")
            {
                _type = "LHZ";
                source_id = Session["lhz"].ToString();
            }
            else
            {
                _type = "LHZ";
                source_id = "cyd001$采油一队$采油一队1#";
            }

            SqlHelper sqlhelper = new SqlHelper();
            OracleConnection Con = sqlhelper.GetConn();
            Con.Open();
            string Sql1, Sql2, Sql3;

            Sql1 = "select FEE_NAME, round(FEE,2) as FEE, FEE_CLASS from VIEW_GGFY where FT_TYPE='" + _type + "' and NY='" + month + "' and SOURCE_ID='" + source_id + "' and CYC_ID='" + cyc_id + "' order by fee_class ";
            Sql3 = "select DEP_NAME from VIEW_ALLDEP where NY='" + month + "' and SOURCE_ID='" + source_id + "'";
            Sql2 = "select distinct FEE_NAME,view_feeinfo.FEE_CLASS as FEE_CLASS from VIEW_FEEINFO left join FEE_TEMPLET_DETAIL";
            Sql2 += " on VIEW_FEEINFO.FEE_CODE=FEE_TEMPLET_DETAIL.FEE_CODE";
            Sql2 += " where FEE_TEMPLET_DETAIL.TEMPLET_CODE='lhzggmb' and VIEW_FEEINFO.FEE_NAME not in";
            Sql2 += " (select distinct FEE_NAME from VIEW_GGFY where FT_TYPE='" + _type + "' and NY='" + month + "' and SOURCE_ID='" + source_id + "' and CYC_ID='" + cyc_id + "')";
            Sql2 += " order by FEE_CLASS";
            OracleDataAdapter da1 = new OracleDataAdapter(Sql1, Con);
            DataSet ds1 = new DataSet();
            OracleDataAdapter da2 = new OracleDataAdapter(Sql2, Con);
            DataSet ds2 = new DataSet();
            OracleDataAdapter da3 = new OracleDataAdapter(Sql3, Con);
            DataSet ds3 = new DataSet();
            da1.Fill(ds1, "kk");
            da2.Fill(ds2, "dd");
            da3.Fill(ds3, "nn");

            //建一个datatable用于显示数据
            #region
            DataTable tt = new DataTable();
            tt.Columns.Add("FEE_NAME", typeof(System.String));
            tt.Columns.Add("FEE", typeof(System.Double));
            tt.Columns.Add("NY", typeof(System.String));
            tt.Columns.Add("DEP_NAME", typeof(System.String));
            tt.Columns.Add("FEE_CLASS", typeof(System.String));
            DataRow rr;
            int n = ds1.Tables["kk"].Rows.Count;
            int m = ds2.Tables["dd"].Rows.Count;
            if (n + m == 0)
            {
                _return(false, "请先设置费用模板！", "null");
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    rr = tt.NewRow();
                    rr[0] = ds1.Tables["kk"].Rows[i][0];
                    rr[1] = ds1.Tables["kk"].Rows[i][1];
                    rr[2] = month;
                    rr[3] = ds3.Tables["nn"].Rows[0][0];
                    rr[4] = ds1.Tables["kk"].Rows[i][2];
                    tt.Rows.Add(rr);
                }
                for (int j = 0; j < m; j++)
                {
                    rr = tt.NewRow();
                    rr[0] = ds2.Tables["dd"].Rows[j][0];
                    rr[1] = "0";
                    rr[2] = month;
                    rr[3] = ds3.Tables["nn"].Rows[0][0];
                    rr[4] = ds2.Tables["dd"].Rows[j][1];
                    tt.Rows.Add(rr);
                }
                //DataList1.DataSource = tt;
                //DataList1.DataBind();
                _return(true, "", tt);
            }
            #endregion
            Con.Close();

            //DataTable dt = cycfy_model.getWellList(cyc_id, month, _getParam("searchWord"));
            //_return(true, "", dt);
        }

        public JObject getWellParam()
        {
            JObject obj = new JObject();
            obj["NY"] = _getParam("NY", true);
            //obj["SOURCE_ID"] = _getParam("SOURCE_ID", true);
            //obj["JH"] = _getParam("JH", true);
            //obj["ZYQ"] = _getParam("ZYQ", true);
            //obj["QK"] = _getParam("QK", true);
            //obj["ZXZ"] = _getParam("ZXZ", true);
            //obj["ZRZ"] = _getParam("ZRZ", true);
            obj["FEE_NAME"] = _getParam("FEE_NAME", true);
            obj["FEE"] = _getParam("FEE", true);
            //obj["ZJRLF"] = _getParam("ZJRLF", true);
            //obj["ZJDLF"] = _getParam("ZJDLF", true);
            //obj["QTDLF"] = _getParam("QTDLF", true);
            //obj["QYWZRF"] = _getParam("QYWZRF", true);
            //obj["QYWZRF_RYF"] = _getParam("QYWZRF_RYF", true);
            //obj["CSZYF"] = _getParam("CSZYF", true);
            //obj["WHXZYLWF"] = _getParam("WHXZYLWF", true);
            //obj["SJZYF"] = _getParam("SJZYF", true);
            //obj["CJCSF"] = _getParam("CJCSF", true);
            //obj["YCJCF"] = _getParam("YCJCF", true);
            //obj["WHXLF"] = _getParam("WHXLF", true);
            //obj["YQCLF"] = _getParam("YQCLF", true);
            //obj["YQCLF_RYF"] = _getParam("YQCLF_RYF", true);
            //obj["QTHSF"] = _getParam("QTHSF", true);
            //obj["YSF"] = _getParam("YSF", true);
            //obj["LYF"] = _getParam("LYF", true);
            //obj["QTZJF"] = _getParam("QTZJF", true);
            //obj["CKGLF"] = _getParam("CKGLF", true);
            //obj["ZJRYF"] = _getParam("ZJRYF", true);
            //obj["ZYYQCP"] = _getParam("ZYYQCP", true);
            //obj["ZJZH"] = _getParam("ZJZH", true);

            return obj;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public void edit1()
        {

            JObject obj = getWellParam();
            string source_id = "";
            string cyc_id = Session["cqc_id"].ToString();
            string FEE_CLASS = _getParam("FEE_CLASS");

            if (_getParam("zyq") != null && _getParam("zyq") != "")
            {
                source_id = _getParam("zyq");
            }
            if (_getParam("zxz") != null && _getParam("zxz") != "")
            {
                source_id = _getParam("zxz");
            }
            if (_getParam("zrz") != null && _getParam("zrz") != "")
            {
                source_id = _getParam("zrz");
            }
            else
            {
                source_id = cyc_id;
            }

            if (zyqfy_model.edit(obj, FEE_CLASS, source_id))
            {
                _return(true, "编辑单井费用成功！", "null");
            }
            else
            {
                _return(false, "编辑单井费用失败！", "null");
            }
        }

        protected void edit()
        {
            OracleCommand Cmd = new OracleCommand();
            string _type = "";
            string source_id = "";
            string cyc_id = Session["cqc_id"].ToString();
            //if (_getParam("zyq") != null && _getParam("zyq") != "")
            //{
            //    _type = "ZYQ";
            //    source_id = _getParam("zyq");
            //}
            //if (_getParam("zxz") != null && _getParam("zxz") != "")
            //{
            //    _type = "ZXZ";
            //    source_id = _getParam("zxz");
            //}
            if (Session["zrz"] != null && Session["zrz"].ToString() != "")
            {
                _type = "ZRZ";
                // source_id = _getParam("zyq") + "$" + _getParam("zxz") + "$" + _getParam("zrz");
                source_id = Session["zrz"].ToString();
            }
            else
            {
                _type = "ZRZ";
                source_id = "cyd001$采油一队$采油一队1#";
            }

            SqlHelper sqlhelper = new SqlHelper();
            OracleConnection Con = sqlhelper.GetConn();
            Con.Open();

            string Sql1, Sql2, Sql3;
            string id = source_id;
            string type = _type;
            //Sql1 = "select FEE_NAME, round(FEE,2) from VIEW_GGFY where FT_TYPE='" + type + "' and NY='" + Session["month"].ToString() + "' and SOURCE_ID='" + id + "' and CYC_ID='" + Session["cyc_id"].ToString() + "'";
            //Sql2 = "select distinct FEE_NAME from VIEW_FEEINFO left join FEE_TEMPLET_DETAIL";
            //Sql2 += " on VIEW_FEEINFO.FEE_CODE=FEE_TEMPLET_DETAIL.FEE_CODE";
            //Sql2 += " where FEE_TEMPLET_DETAIL.TEMPLET_CODE='zyqfyggmb' and VIEW_FEEINFO.FEE_NAME not in";
            //Sql2 += " (select distinct FEE_NAME from VIEW_GGFY where FT_TYPE='" + type + "' and NY='" + Session["month"].ToString() + "' and SOURCE_ID='" + id + "' and CYC_ID='" + Session["cyc_id"].ToString() + "')";


            Sql1 = "select FEE_NAME, round(FEE,2) from VIEW_GGFY where FT_TYPE='" + type + "' and NY='" + Session["month"].ToString() + "' and SOURCE_ID='" + id + "' and cyc_id='" + Session["cqc_id"].ToString() + "'";
            Sql2 = "select distinct FEE_NAME from VIEW_FEEINFO left join FEE_TEMPLET_DETAIL";
            Sql2 += " on VIEW_FEEINFO.FEE_CODE=FEE_TEMPLET_DETAIL.FEE_CODE";
            Sql2 += " where FEE_TEMPLET_DETAIL.TEMPLET_CODE='zrzfyggmb' and VIEW_FEEINFO.FEE_NAME not in";
            Sql2 += " (select distinct FEE_NAME from VIEW_GGFY where FT_TYPE='" + type + "' and NY='" + Session["month"].ToString() + "' and SOURCE_ID='" + id + "' and cyc_id='" + Session["cqc_id"].ToString() + "')";
            OracleDataAdapter da1 = new OracleDataAdapter(Sql1, Con);
            DataSet ds1 = new DataSet();
            OracleDataAdapter da2 = new OracleDataAdapter(Sql2, Con);
            DataSet ds2 = new DataSet();
            da1.Fill(ds1, "kk");
            da2.Fill(ds2, "dd");

            DataTable tt = new DataTable();
            tt.Columns.Add("FEE_NAME", typeof(System.String));
            tt.Columns.Add("FEE", typeof(System.Double));
            DataRow rr;
            int n = ds1.Tables["kk"].Rows.Count;
            int m = ds2.Tables["dd"].Rows.Count;
            if (n + m == 0)
            {
                _return(false, "请先设置费用模板！", "null");
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    rr = tt.NewRow();
                    rr[0] = ds1.Tables["kk"].Rows[i][0];
                    rr[1] = ds1.Tables["kk"].Rows[i][1];
                    tt.Rows.Add(rr);
                }
                for (int j = 0; j < m; j++)
                {
                    rr = tt.NewRow();
                    rr[0] = ds2.Tables["dd"].Rows[j][0];
                    rr[1] = "0";
                    tt.Rows.Add(rr);
                }

                try
                {
                    for (int p = 0; p < tt.Rows.Count; p++)
                    {
                        string Sql;
                        //TextBox FEE1 = (TextBox)DataList1.Items[p].FindControl("txtFEE1");
                        string FEE1 = _getParam("FEE");
                        //Label FN1 = (Label)DataList1.Items[p].FindControl("labFN1");
                        string FN1 = _getParam("FEE_NAME");
                        for (int k = 0; k < ds1.Tables["kk"].Rows.Count; k++)
                        {
                            string fnk = ds1.Tables["kk"].Rows[k][0].ToString();
                            if (fnk == FN1)
                            {
                                if (FEE1 == "0")
                                {
                                    //Sql = "delete ggfy where source_id='" + id + "' and ft_type='" + type + "' and ny='" + Session["month"].ToString() + "' and fee_code in (select fee_code from fee_name where fee_name='" + FN1 + "') and cyc_id='" + Session["cyc_id"].ToString() + "'";
                                    Sql = "delete ggfy where source_id='" + id + "' and ft_type='" + type + "' and ny='" + Session["month"].ToString() + "' and fee_code in (select fee_code from fee_name where fee_name='" + FN1 + "') and cyc_id='" + Session["cqc_id"].ToString() + "'";
                                    Cmd = new OracleCommand(Sql, Con);
                                }

                                else
                                {
                                    Sql = "update GGFY set fee='" + FEE1 + "'";
                                    //Sql += " where source_id='" + id + "' and ft_type='" + type + "' and ny='" + Session["month"].ToString() + "' and fee_code in (select fee_code from fee_name where fee_name='" + FN1 + "') and cyc_id='" + Session["cyc_id"].ToString() + "'";
                                    Sql += " where source_id='" + id + "' and ft_type='" + type + "' and ny='" + Session["month"].ToString() + "' and fee_code in (select fee_code from fee_name where fee_name='" + FN1 + "') and cyc_id='" + Session["cqc_id"].ToString() + "'";
                                    Cmd = new OracleCommand(Sql, Con);
                                }

                                try
                                {
                                    int reflectrows = Convert.ToInt32(Cmd.ExecuteNonQuery().ToString());
                                    if (reflectrows != 0)
                                    {
                                        //labShow.Text = "数据保存成功！";
                                        _return(true, "数据保存成功！", "null");
                                    }

                                    else
                                    {
                                        //labShow.Text = "数据未保存成功，请重新输入！";
                                        _return(false, "数据未保存成功，请重新输入！", "null");
                                    }

                                }
                                catch
                                { }
                            }
                        }

                        for (int l = 0; l < ds2.Tables["dd"].Rows.Count; l++)
                        {
                            string fnl = ds2.Tables["dd"].Rows[l][0].ToString();
                            if (fnl == FN1 && FEE1 != "0")
                            {
                                string ZUOYE = "select dep_id from view_alldep where source_id='" + id + "'";
                                string FCL = "select fee_class from fee_name where fee_name='" + FN1 + "'";
                                string FCD = "select fee_code from fee_name where fee_name='" + FN1 + "'";

                                OracleDataAdapter da5 = new OracleDataAdapter(ZUOYE, Con);
                                DataSet ds5 = new DataSet();
                                da5.Fill(ds5, "tb5");
                                string DPID = ds5.Tables["tb5"].Rows[0][0].ToString();

                                OracleDataAdapter da6 = new OracleDataAdapter(FCL, Con);
                                DataSet ds6 = new DataSet();
                                da6.Fill(ds6, "tb6");
                                string FL = ds6.Tables["tb6"].Rows[0][0].ToString();

                                OracleDataAdapter da7 = new OracleDataAdapter(FCD, Con);
                                DataSet ds7 = new DataSet();
                                da7.Fill(ds7, "tb7");
                                string FD = ds7.Tables["tb7"].Rows[0][0].ToString();

                                Sql = "insert into ggfy(ny,dep_id,ft_type,source_id,fee_class,fee_code,fee,cyc_id)";
                                Sql += " values('" + Session["month"].ToString() + "','" + DPID + "','" + type + "','" + id + "','" + FL + "','" + FD + "','" + FEE1 + "','" + Session["cqc_id"].ToString() + "')";
                                Cmd = new OracleCommand(Sql, Con);
                                try
                                {
                                    int reflectrows = Convert.ToInt32(Cmd.ExecuteNonQuery().ToString());
                                    if (reflectrows != 0)
                                    {
                                        _return(true, "数据保存成功！", "null");
                                    }

                                    else
                                    {
                                        _return(false, "数据未保存成功，请重新输入！", "null");
                                    }

                                }
                                catch
                                { }
                            }
                        }
                    }
                }

                catch (OracleException Error)
                {
                    string CuoWu = "错误!" + Error.Message.ToString();
                    Response.Write(CuoWu);
                }
                finally
                {
                    Con.Close();
                }
            }
        }
    }
}

