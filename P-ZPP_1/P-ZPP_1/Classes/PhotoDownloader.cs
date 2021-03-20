using HtmlAgilityPack;
using P_ZPP_1.AppDatabase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace P_ZPP_1
{
    /// <summary>
    /// Class containg methods responsible for <see cref="GetPhotoScript(HtmlDocument)">getting photo script</see>,  
    /// <see cref="GetAllLinks(string)">gettling all links</see>, 
    /// and <see cref="DownloadPhotosFromList(List{string}, System.Net.WebClient, QueryInfo)">downloading images </see>from list.
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
            }
            return imagelinklist;
        }
        /// <summary>
        /// Takes the <see cref="List{T}"/> of links and downloads them.
        /// </summary>
        /// <param name="list">List of links.</param>
        /// <param name="client">WebClient object responsible for connecting to Website.</param>
        /// <param name="qi"><see cref="QueryInfo"/> object.</param>
        /// <returns><see cref="List{T}"/> of paths to photos.</returns>
        public List<string> DownloadPhotosFromList(List<string> list, System.Net.WebClient client, QueryInfo qi)
        {
            int i = 1;
            var createDirectory = Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), (qi.Querry + "_" + qi.Id.ToString())));
            var path = createDirectory.FullName;
            List<string> photoPaths = new List<string>();
            foreach (var item in list)
            {
                var photopath = path + "\\" + i + ".jpg";
                client.DownloadFile(item.ToString(), photopath);
                photoPaths.Add(photopath);
                i++;
            }
            return photoPaths;
        }
        /// <summary>
        /// Executes <see cref="GetPhotoScript(HtmlDocument)">GetPhotoScript</see>, 
        /// <see cref="GetAllLinks(string)">GetAllLinks</see>, and 
        /// <see cref="DownloadPhotosFromList(List{string}, WebClient, QueryInfo)">DownloadPhotosFromList</see> methods.
        /// </summary>
        /// <param name="doc"><see cref="HtmlDocument"></see> downloaded from <see cref="WebClient"/>.</param>
        /// <param name="client"><see cref="WebClient"></see> object connecting to <see href="http://allegro.pl">allegro.pl</see> site and downloading photos.</param>
        /// <param name="qi"><see cref="QueryInfo"/> object.</param>
        /// <returns><see cref="List{T}">List</see> of paths to photos.</returns>
        public List<string> DownloadPhotos(HtmlDocument doc, System.Net.WebClient client, QueryInfo qi)
        {
            var photoscript = GetPhotoScript(doc);
            var list = GetAllLinks(photoscript);
            var photoList = DownloadPhotosFromList(list, client, qi);

            return photoList;
        }
    }
}
