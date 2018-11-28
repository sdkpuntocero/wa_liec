
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace wa_liec
{
    public partial class inf_web : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //string url = String.Format("http://web.liecsa.com/");
            //WebClient webClient = new WebClient();
            //webClient.Encoding = System.Text.Encoding.UTF8;
            //string result = webClient.DownloadString(url);

            var html = @"http://www.franser.mx/contacto/";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");



            Label1.Text = node.OuterHtml.Replace( "<title>","");

        }
        
       
    }
}