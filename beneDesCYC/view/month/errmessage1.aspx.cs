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

namespace beneDesCYC.view.month
{
    public partial class errmessage1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
             if (Session["err"] != null)
            { 
              TextBox1.Text = Session["err"].ToString();
              Session["err"] = "";
             }
            else
            { TextBox1.Text = ""; }
        }
    }
}
