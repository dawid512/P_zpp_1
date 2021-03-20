using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using P_ZPP_1.AppDatabase;
namespace P_ZPP_1
{
    /// <summary>
    /// Class containg <see cref="Parse(int, string)">Parse</see> method.
    /// </summary>
    class Parser
    {
        /// <summary>
        /// Funkcja parsująca HTML z query allegro.
        /// Method parsing the HTML code and then adding it to database
        /// </summary>
        /// <param name="page">Page number.</param>
        /// <param name="querry">Query search from <see href="http://allegro.pl">allegro</see> site.</param>
        public void Parse(int page, string querry)
        {
            //klasy znaczników 
            #region tag classes 
            string regularAuctionName = ".//a[@class='_w7z6o _uj8z7 meqh_en mpof_z0 mqu1_16 _9c44d_2vTdY  ']"; //normalna aukcja
            string freeShippingAuctionName = ".//a[@class='_w7z6o _uj8z7 meqh_en mpof_z0 mqu1_16 _9c44d_2vTdY m9qz_yq ']";//aukcja z darmową dostawą
            string priceInfo = ".//span[@class='_1svub _lf05o']"; //informacja o cenie
            string paramList = ".//dl[@class='mp4t_0 m3h2_0 mryx_0 munh_0 mg9e_0 mvrt_0 mj7a_0 mh36_0 meqh_en msa3_z4 _1vx3o']"; //lista parametrów danego produktu - zawsze inna
            #endregion

            //pobieranie i formatowanie HTML
            //var task = new Task(() =>
            {
                WebClient client = new WebClient();
                string url = "https://allegro.pl/listing?string=" + querry + "&bmatch=cl-e2101-d3681-c3682-ele-1-1-0304&p=" + page;
                Uri uri = new Uri(url);
                client.Headers.Add("Accept: text/html, application/xhtml+xml, /");
                client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
                var data = client.DownloadData(uri);
                var html = HttpUtility.HtmlDecode(Encoding.UTF8.GetString(data));
                WebUtility.HtmlDecode(html);
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);


                HtmlNode[] nodes = htmlDoc.DocumentNode.SelectNodes("//article").ToArray();

                //Dodanie informacji o wykonaniu query
                DbLoader dbLoad = new DbLoader();
                QueryInfo queryInfo = new QueryInfo(querry, DateTime.Now);
                dbLoad.SaveToLogDb(queryInfo);
                //wyciągnięcie informacji z tych nodów

                foreach (HtmlNode item in nodes)
                {
                    var index = Array.IndexOf(nodes, item);
                    if (item.SelectSingleNode(regularAuctionName) != null)
                    {
                        var ItemName = item.SelectSingleNode(regularAuctionName).InnerText;
                        var ItemPrice = item.SelectSingleNode(priceInfo).InnerText;
                        ItemPrice = ItemPrice.Replace(",", ".");
                        ItemPrice = ItemPrice.Replace(" ", "");
                        Console.WriteLine(ItemPrice.Substring(0, ItemPrice.Length - 3));
                        var decimalPrice = Decimal.Parse(ItemPrice.Substring(0, ItemPrice.Length - 3));
                        var ParametersNode = item.SelectSingleNode(paramList);
                        var ParametersList = ParametersNode.ChildNodes;


                        string tmp = "";
                        string tmp2 = "";

                        Items result = new Items(queryInfo.Id, ItemName, decimalPrice, true);
                        dbLoad.SaveToItemDb(result);

                        foreach (var item2 in ParametersList)
                        {
                            if (item2.Name == "dt")
                            {
                                tmp = item2.InnerText;
                            }
                            else
                            {
                                tmp2 = item2.InnerText;
                                ItemParams param = new ItemParams(result.Id, queryInfo.Id, tmp, tmp2);
                                dbLoad.SaveToParamDb(param);
                            }
                        }
                    }
                    else if (item.SelectSingleNode(freeShippingAuctionName) != null)
                    {
                        var ItemName = item.SelectSingleNode(freeShippingAuctionName).InnerText;
                        var ItemPrice = item.SelectSingleNode(priceInfo).InnerText;
                        ItemPrice = ItemPrice.Replace(",", ".");
                        ItemPrice = ItemPrice.Replace(" ", "");
                        var decimalPrice = Decimal.Parse(ItemPrice.Substring(0, ItemPrice.Length - 3));
                        var ParametersNode = item.SelectSingleNode(paramList);
                        var ParametersList = ParametersNode.ChildNodes;

                        string tmp = "";
                        string tmp2 = "";
                        //var techDic = new Dictionary<string, string>();
                        //Console.WriteLine("\n{0}\nCena: {1}", ItemName, ItemPrice);

                        Items result = new Items(queryInfo.Id, ItemName, decimalPrice, true);
                        dbLoad.SaveToItemDb(result);

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
                                ItemParams param = new ItemParams(result.Id, queryInfo.Id, tmp, tmp2);
                                dbLoad.SaveToParamDb(param);
                            }
                        }
                    }
                }
                //var db = new AppDatabase.AllegroAppContext();
                //db.QueryInfo.Add(new AppDatabase.QueryInfo(querry, DateTime.Now));
                //db.SaveChanges();
            }//);
            //task.Start();

            //return task;
        }
    }
}
