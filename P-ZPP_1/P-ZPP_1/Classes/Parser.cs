using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using P_ZPP_1.AppDatabase;
using P_ZPP_1.Properties;

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
        /// 
        public void Parse(int page, string querry)
            // TODO: Przekazać queryInfo
        {
            //klasy znaczników 
            #region tag classes 
            string regularAuctionName = ".//a[@class='_w7z6o _uj8z7 meqh_en mpof_z0 mqu1_16 _9c44d_2vTdY  ']"; //normalna aukcja
            string freeShippingAuctionName = ".//a[@class='_w7z6o _uj8z7 meqh_en mpof_z0 mqu1_16 _9c44d_2vTdY m9qz_yq ']";//aukcja z darmową dostawą
            string priceInfo = ".//span[@class='_1svub _lf05o']"; //informacja o cenie
            string paramList = ".//dl[@class='mp4t_0 m3h2_0 mryx_0 munh_0 mg9e_0 mvrt_0 mj7a_0 mh36_0 meqh_en msa3_z4 _1vx3o']"; //lista parametrów danego produktu - zawsze inna
            #endregion

            //pobieranie i formatowanie HTML
            {
                WebConnection connection = new WebConnection();
                HtmlNode[] nodes = connection.GetHtml(querry, page);

                // TODO: Przenieść w miejsce tworzenia query
                DbQueryInfo queryLoad = new DbQueryInfo();
                queryLoad.Add(querry, DateTime.Now);

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
                        ItemPrice = ItemPrice.Replace("zł", "");
                        var decimalPrice = Decimal.Parse(ItemPrice.Substring(0, ItemPrice.Length));
                        var ParametersNode = item.SelectSingleNode(paramList);

                        string tmp = "";
                        string tmp2 = "";

                        DbItems result = new DbItems();
                        result.Add(queryLoad.queryInfo.Id, ItemName, decimalPrice, true, page);
                        if(ParametersNode != null)
                        {
                            var ParametersList = ParametersNode.ChildNodes;
                            foreach (var item2 in ParametersList)
                            {
                                if (item2.Name == "dt")
                                {
                                    tmp = item2.InnerText;
                                }
                                else
                                {
                                    tmp2 = item2.InnerText;
                                    DbItemParams param = new DbItemParams();
                                    param.Add(result.item.Id, queryLoad.queryInfo.Id, tmp, tmp2);
                                }
                            }
                        }
                    }
                    else if (item.SelectSingleNode(freeShippingAuctionName) != null)
                    {
                        var ItemName = item.SelectSingleNode(freeShippingAuctionName).InnerText;
                        var ItemPrice = item.SelectSingleNode(priceInfo).InnerText;
                        ItemPrice = ItemPrice.Replace(",", ".");
                        ItemPrice = ItemPrice.Replace(" ", "");
                        ItemPrice = ItemPrice.Replace("zł", "");
                        var decimalPrice = Decimal.Parse(ItemPrice.Substring(0, ItemPrice.Length));
                        var ParametersNode = item.SelectSingleNode(paramList);


                        string tmp = "";
                        string tmp2 = "";

                        DbItems result = new DbItems();
                        result.Add(queryLoad.queryInfo.Id, ItemName, decimalPrice, true, page);

                        if(ParametersNode != null)
                        {
                            var ParametersList = ParametersNode.ChildNodes;
                            foreach (var item2 in ParametersList)
                            {
                                if (item2.Name == "dt")
                                {
                                    tmp = item2.InnerText;
                                }
                                else
                                {
                                    tmp2 = item2.InnerText;

                                    DbItemParams param = new DbItemParams();
                                    param.Add(result.item.Id, queryLoad.queryInfo.Id, tmp, tmp2);
                                }
                            }
                        }

                    }
                }
            }
        }
    }
}
