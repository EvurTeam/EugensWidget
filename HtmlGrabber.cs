using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace EugensWidget
{
    public static class HtmlGrabber
    {
        public static IHtmlDocument GetDocument(string url)
        {
            var req = WebRequest.Create(url) as HttpWebRequest;
            var res = req.GetResponse();
            var parser = new HtmlParser();
            var doc = parser.ParseDocument(res.GetResponseStream());
            res.Close();
            return doc;
        }
    }
}
