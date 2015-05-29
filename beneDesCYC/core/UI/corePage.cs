using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Microsoft.Win32;
using System.Net.Sockets;

namespace beneDesCYC.core.UI
{
    public class corePage : System.Web.UI.Page
    {
        /// <summary>
        /// 构造函数

        /// </summary>
        public corePage()
        {
            // 添加一个页面load时的事件
            this.Load += new EventHandler(corePage_Load);
        }
        //在页面后台使用

        public SqlHelper sql = new SqlHelper();
        /// <summary>
        /// 判断是否登陆
        /// </summary>
        public void corePage_Load(object sender, EventArgs e)
        {

            try
            {
                string value = GetRegistryValue(@"SOFTWARE\desw", "adddd");
                if ((!string.IsNullOrEmpty(value)) && (value == "2323"))
                {//判断数据库是否可连接
                    if (conn.TestConnection())
                    {
                        if (Session["userName"] == null)
                        {
                            Regex regex = new Regex("^/api");



                            // 调用的API获取数据
                            if (regex.IsMatch(Request.Path))
                            {
                                _return(false, "session is out of date!", "null");
                            }
                            else    // 访问页面
                            {
                                //Response.Write("<script>" +
                                //               "if(top.location==location) {location.href='../login.aspx';} else {top.location.href='../login.aspx';}" +
                                //               "</script>"
                                //);

                            }
                        }

                    }
                    else
                    {
                        Response.Write("<script>" + "alert('数据库服务器连接失败')" + "</script>");
                        Response.End();

                    }
                }
                else
                {
                    Response.Write("<script>" + "alert('版本未注册!')" + "</script>");
                    Response.Write("<script>" +
                                                 "if(top.location==location) {location.href='/login.aspx';} else {top.location.href='/login.aspx';}" +
                                                 "</script>"
                                  );
                    Response.End();

                }
            }
            catch (Exception ex)
            {
                //Response.Write("<script>" + "alert('" + ex.Message + "')" + "</script>");
                //Response.End();
            }


        }



        /// <summary>
        /// 读取注册表指定项
        /// </summary>
        /// <param name="path"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        private string GetRegistryValue(string path, string paramName)
        {
            string value = string.Empty;
            RegistryKey root = Registry.LocalMachine;
            RegistryKey rk = root.OpenSubKey(path, false);

            if (rk != null)
            {
                value = (string)rk.GetValue(paramName, null);
            }
            else
            {
                //RegistryKey software = root.OpenSubKey("SOFTWARE",true);
                //RegistryKey desw = software.CreateSubKey("desw");
                //desw.SetValue("adddd", "2323");
            }
            return value;
        }


        /// <summary>
        /// 获取url中传递的参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string _getParam(string key)
        {
            string value = Request[key];

            return value;
            //return Request[key];//@yzp
        }

        /// <summary>
        /// 获取url中传递的参数, 并将其中的单引号替换为空
        /// </summary>
        /// <param name="key"></param>
        /// <param name="replaceQuote"></param>
        /// <returns></returns>
        public string _getParam(string key, bool replaceQuote)
        {
            string value = Request[key];

            return value;
            //return Request[key].Replace("'", ""); //@yzp
        }
        public Object _getObjParam(string key, bool replaceQuote)
        {
            Object result = DBNull.Value;
            string value = Request[key];
            if (!string.IsNullOrEmpty(value))
                result = value;
            return result;
            //return Request[key].Replace("'", ""); //@yzp
        }
        /// <summary>
        /// API返回数据
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="msg">提示信息</param>
        /// <param name="data">数据</param>
        public void _return(bool success, string msg, string data)
        {
            JObject ret = new JObject(
                new JProperty("success", success),
                new JProperty("msg", msg),
                new JProperty("data", data));

            Response.Write(ret.ToString());
            Response.End();
        }

        /// <summary>
        /// API返回数据
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="msg">提示信息</param>
        /// <param name="dt">DataTable</param>
        public void _return(bool success, string msg, DataTable dt)
        {
            string dtString = JsonConvert.SerializeObject(dt, new DataTableConverter());

            JObject ret = new JObject(
                new JProperty("success", success),
                new JProperty("msg", msg),
                new JProperty("data", JArray.Parse(dtString)));
            Response.Write(ret.ToString());
            Response.End();
        }

        //将数据总数和数据表一起返回

        public void _return(bool success, string msg, DataTable dt, int total)
        {
            string dtString = JsonConvert.SerializeObject(dt, new DataTableConverter());

            JObject ret = new JObject(
                new JProperty("success", success),
                new JProperty("msg", msg),
                new JProperty("totalProperty", total),
                new JProperty("data", JArray.Parse(dtString)));
            Response.Write(ret.ToString());
            Response.End();
        }

        /// <summary>
        /// API返回数据
        /// </summary>
        /// <param name="dt">DataTable</param>
        public void _return(DataTable dt)
        {
            string dtString = JsonConvert.SerializeObject(dt, new DataTableConverter());
            Response.Write(dtString);
            Response.End();
        }

        /// <summary>
        /// API返回数据
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="msg">提示信息</param>
        /// <param name="obj">JObject</param>
        public void _return(bool success, string msg, JObject obj)
        {
            JObject ret = new JObject(
                new JProperty("success", success),
                new JProperty("msg", msg),
                new JProperty("data", obj));

            Response.Write(ret.ToString());
            Response.End();
        }

        /// <summary>
        /// API返回数据
        /// </summary>
        /// <param name="obj">JObject</param>
        public void _return(JObject obj)
        {
            Response.Write(obj.ToString());
            Response.End();
        }

        /// <summary>
        /// API返回数据
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="msg">提示信息</param>
        /// <param name="obj">JArray</param>
        public void _return(bool success, string msg, JArray obj)
        {
            JObject ret = new JObject(
                new JProperty("success", success),
                new JProperty("msg", msg),
                new JProperty("data", obj));

            Response.Write(ret.ToString());
            Response.End();
        }

        /// <summary>
        /// API返回数据
        /// </summary>
        /// <param name="obj">JArray</param>
        public void _return(JArray obj)
        {
            Response.Write(obj.ToString());
            Response.End();
        }

    }
}
