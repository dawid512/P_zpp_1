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
    class Parser
    {
        /// <summary>
        /// Funkcja parsująca HTML z query allegro.
        /// </summary>
        public void Parse(string querry)
        {
            //klasy znaczników 
            #region tag classes 
            string regularAuctionName = ".//a[@class='_w7z6o _uj8z7 meqh_en mpof_z0 mqu1_16 _9c44d_2vTdY  ']"; //normalna aukcja
            string freeShippingAuctionName = ".//a[@class='_w7z6o _uj8z7 meqh_en mpof_z0 mqu1_16 _9c44d_2vTdY m9qz_yq ']";//aukcja z darmową dostawą
            string priceInfo = ".//span[@class='_1svub _lf05o']"; //informacja o cenie
            string paramList = ".//dl[@class='mp4t_0 m3h2_0 mryx_0 munh_0 mg9e_0 mvrt_0 mj7a_0 mh36_0 meqh_en msa3_z4 _1vx3o']"; //lista parametrów danego produktu - zawsze inna
            #endregion
            //---------------- 

            //----------------

            //pobieranie i formatowanie HTML
            WebClient client = new WebClient();
            string url = "https://allegro.pl/kategoria/laptopy-491?string=" + querry + "&bmatch=cl-e2101-d3681-c3682-ele-1-1-0304";
            Uri uri = new Uri(url);
            client.Headers.Add("Accept: text/html, application/xhtml+xml, /");
            client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
            var data = client.DownloadData(uri);
            var html = HttpUtility.HtmlDecode(Encoding.UTF8.GetString(data));
            WebUtility.HtmlDecode(html);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);


            HtmlNode[] nodes = htmlDoc.DocumentNode.SelectNodes("//article").ToArray();

            foreach (HtmlNode item in nodes)
            {
                var index = Array.IndexOf(nodes, item);

                if (item.SelectSingleNode(regularAuctionName) != null  )
                {
                    var ItemName = item.SelectSingleNode(regularAuctionName).InnerText;
                    var ItemPrice = item.SelectSingleNode(priceInfo).InnerText;
                    var decimalPrice = Decimal.Parse(ItemPrice.Substring(0, ItemPrice.Length - 3));
                    var ParametersNode = item.SelectSingleNode(paramList);
                    var ParametersList = ParametersNode.ChildNodes;
                    string tmp = "";
                    string tmp2 = "";

                    foreach (var item2 in ParametersList)
                    {
                        if (item2.Name == "dt")
                        {
                            tmp = item2.InnerText;
                        }
                        else
                        {
                            tmp2 = item2.InnerText;
                        }
                    }

                    
                }
                else if (item.SelectSingleNode(freeShippingAuctionName) != null)
                {
                    var ItemName = item.SelectSingleNode(freeShippingAuctionName).InnerText;
                    var ItemPrice = item.SelectSingleNode(priceInfo).InnerText;
                    var ParametersNode = item.SelectSingleNode(paramList);
                    var ParametersList = ParametersNode.ChildNodes;
                    string tmp = "";
                    string tmp2 = "";
                    //var techDic = new Dictionary<string, string>();
                    //Console.WriteLine("\n{0}\nCena: {1}", ItemName, ItemPrice);
                    foreach (var item2 in ParametersList)
                    {
                        if (item2.Name == "dt")
                        {
                            tmp = item2.InnerText;
                            //Console.Write("{0}: ", tmp);
                        }
                        else
                        {
                            tmp2 = item2.InnerText;
                            //Console.WriteLine(tmp2);
                        }
                    }
                }
            }
            var db = new AppDatabase.AllegroAppContext();
            db.QueryInfo.Add(new AppDatabase.QueryInfo(querry, DateTime.Now));
            db.SaveChanges();
        }
    }
}
