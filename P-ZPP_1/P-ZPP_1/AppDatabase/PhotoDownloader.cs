using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace P_ZPP_1.AppDatabase
{
    /// <summary>
    /// Class containg methods responsible for <see cref="GetPhotoScript(HtmlDocument)">getting photo script</see>,  
    /// <see cref="GetAllLinks(string)">gettling all links</see>, 
    /// and <see cref="DownloadPhotosFromList(List{string}, System.Net.WebClient)">downloading images </see>from list.
    /// </summary>
    class PhotoDownloader
    {
        /// <summary>
        /// Finds the script in HTML document containing all links to images.
        /// </summary>
        /// <param name="doc">An HTML document in the form of HtmlAgilityPack.HtmlDocument object.</param>
        /// <returns>The script containing all links to photos in encoded version.</returns>
        public string GetPhotoScript(HtmlDocument doc)
        {
            HtmlNode[] photos = doc.DocumentNode.SelectNodes("//script").ToArray();
            string photscript = "";
            foreach (var item in photos)
            {
                if (item.GetAttributeValue("data-serialize-box-id", "") == "q3sWcOVSTx268bHXp9P4Fw==")
                {
                    photscript = item.InnerText;
                }
            }
            return photscript;
        }
        /// <summary>
        /// Gets the script with encoded links.
        /// </summary>
        /// <param name="photoscript">String containing encoded links.</param>
        /// <returns>List of encoded strings.</returns>
        public List<string> GetAllLinks(string photoscript)
        {
            List<string> imagelinklist = new List<string>();
            string pattern = "\\\\\"\\b(mainThumbnail)\\b\\\\\":\\\\\".{1,200}\\,";
            Regex rgx = new Regex(pattern);

            foreach (Match match in rgx.Matches(photoscript))
            {
                var link = match.Value;
                link = link.Replace("\\\"mainThumbnail\\\":\\\"", "");
                link = link.Replace("\\\\", "/"); ;
                link = link.Replace("u002F", "");
                link = link.Replace("\\\",", "");
                imagelinklist.Add(link);
                Console.WriteLine(link);
            }
            return imagelinklist;
        }
        /// <summary>
        /// Takes the list of links and downloads them.
        /// </summary>
        /// <param name="list">List of links.</param>
        /// <param name="client">WebClient object responsible for connecting to Website.</param>
        public void DownloadPhotosFromList(List<string> list, System.Net.WebClient client)
        {
            var name = DateTime.Now.ToString();
            int i = 0;
            foreach (var item in list)
            {
                Console.WriteLine(item);
                client.DownloadFile(item.ToString(), @"C:\Photos\" + i.ToString() + ".jpg");
                i++;
            }
        }
    }
}
