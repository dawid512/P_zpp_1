using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace P_ZPP_1
{
    /// <summary>
    /// Contains method responsible for connecting to web. 
    /// </summary>
    class WebConnection
    {
        /// <summary>
        /// Connects to <see href="http://allegro.pl">Allegro</see> website, 
        /// downloads photos by <see cref="PhotoDownloader.DownloadPhotos(HtmlDocument, WebClient, string, int)">DownloadPhotos</see> 
        /// and information by <see cref="Parser.Parse">Parse</see> method.
        /// </summary>
        /// <param name="query">Query from textbox.</param>
        /// <param name="pageNumber">Number of page.</param>
        public int GetHtml(string query, int pageNumber)
        {
            WebClient client = new WebClient();
            PhotoDownloader pd = new PhotoDownloader();
            Parser parser = new Parser();
            
            string url = "https://allegro.pl/listing?string=" + query + "&bmatch=cl-e2101-d3681-c3682-ele-1-1-0304&p=" + pageNumber;
            Uri uri = new Uri(url);
            
            client.Headers.Add("Accept: text/html, application/xhtml+xml, /");
            client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
            
            var data = client.DownloadData(uri);
            var html = HttpUtility.HtmlDecode(Encoding.UTF8.GetString(data));
            WebUtility.HtmlDecode(html);
            var htmlDoc = new HtmlDocument();
            
            htmlDoc.LoadHtml(html);
            List<string> listOfPaths = pd.DownloadPhotos(htmlDoc, client, query, pageNumber);
            client.Dispose();
            if (htmlDoc == null)
                return 0;
            else
            {
                HtmlNode numberOfAllPages = htmlDoc.DocumentNode
                .SelectNodes(".//span[@class='_1h7wt _1fkm6 _g1gnj _3db39_3i0GV _3db39_XEsAE']")
                .FirstOrDefault();
                int intNumberOfAllPages = Int32.Parse(numberOfAllPages.InnerText);
                try
                { 
                    HtmlNode[] nodes = htmlDoc.DocumentNode.SelectNodes("//article").ToArray();
                    parser.Parse(nodes, listOfPaths, pageNumber, query);
                    
                }
                catch (System.ArgumentNullException)
                {
                    return -1;
                }
                return intNumberOfAllPages;
            }
        }
    }
}



