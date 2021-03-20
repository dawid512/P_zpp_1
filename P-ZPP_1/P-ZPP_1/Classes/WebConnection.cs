using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace P_ZPP_1
{
    class WebConnection
    {
        public HtmlNode[] GetHtml(string query, int pageNumber)
        {
            WebClient client = new WebClient();
            string url = "https://allegro.pl/listing?string=" + query + "&bmatch=cl-e2101-d3681-c3682-ele-1-1-0304&p=" + pageNumber;
            Uri uri = new Uri(url);
            client.Headers.Add("Accept: text/html, application/xhtml+xml, /");
            client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
            var data = client.DownloadData(uri);
            var html = HttpUtility.HtmlDecode(Encoding.UTF8.GetString(data));
            WebUtility.HtmlDecode(html);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            return htmlDoc.DocumentNode.SelectNodes("//article").ToArray();
        }
    }
}
