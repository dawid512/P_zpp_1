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
using System.Globalization;

namespace P_ZPP_1
{
    /// <summary>
    /// Class containg <see cref="Parse(HtmlNode[], List{string}, int, string)">Parse</see> method.
    /// </summary>
    class Parser
    {
        /// <summary>
        /// Parses informations from <see cref="HtmlNode"/> array and adds information to <see cref="AllegroAppContext"/> database.
        /// </summary>
        /// <param name="nodes">Array of &lt;article&gt; nodes.</param>
        /// <param name="paths"><see cref="List{T}">List</see> of paths to photos.</param>
        /// <param name="page">Number of page.</param>
        /// <param name="querry">Query from TextBox</param>
        public void Parse(HtmlNode[] nodes, List<string> paths, int page, string querry)
        {
            //klasy znaczników 
            #region tag classes 
            string regularAuctionName = ".//a[@class='_w7z6o _uj8z7 meqh_en mpof_z0 mqu1_16 _9c44d_2vTdY  ']"; //normalna aukcja
            string freeShippingAuctionName = ".//a[@class='_w7z6o _uj8z7 meqh_en mpof_z0 mqu1_16 _9c44d_2vTdY m9qz_yq ']";//aukcja z darmową dostawą
            string priceInfo = ".//span[@class='_1svub _lf05o']"; //informacja o cenie
            string paramList = ".//dl[@class='mp4t_0 m3h2_0 mryx_0 munh_0 mg9e_0 mvrt_0 mj7a_0 mh36_0 meqh_en msa3_z4 _1vx3o']"; //lista parametrów danego produktu - zawsze inna
            string hyperlink = ".//a[@class='msts_9u mg9e_0 mvrt_0 mj7a_0 mh36_0 mpof_ki m389_6m mx4z_6m m7f5_6m mse2_k4 m7er_k4 _9c44d_1ILhl  ']"; //hiperłącze
            string allegroSmart = ".//i[@class='_9c44d_2UYuR _9c44d_1DKTg']"; //allegro smart
            #endregion



            DbQueryInfo queryLoad = new DbQueryInfo();
            queryLoad.Add(querry, DateTime.Now);

            //wyciągnięcie informacji z tych nodów
            int x = 0;
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
                    var decimalPrice = Decimal.Parse(ItemPrice, CultureInfo.InvariantCulture);

                    var link = item.SelectSingleNode(hyperlink).GetAttributeValue("href", "http://allegro.pl");

                    bool isAllegroSmart = (item.SelectSingleNode(allegroSmart) != null);

                    
                    DbItems result = new DbItems();
                    result.Add(queryLoad.queryInfo.Id, ItemName, decimalPrice, isAllegroSmart, page, paths[x], link);

                    var ParametersNode = item.SelectSingleNode(paramList);
                    string tmp = "";
                    string tmp2 = "";
                    
                    if (ParametersNode != null)
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
                    x++;
                }
                else if (item.SelectSingleNode(freeShippingAuctionName) != null)
                {
                    var ItemName = item.SelectSingleNode(freeShippingAuctionName).InnerText;
                    
                    var ItemPrice = item.SelectSingleNode(priceInfo).InnerText;
                    ItemPrice = ItemPrice.Replace(",", ".");
                    ItemPrice = ItemPrice.Replace(" ", "");
                    ItemPrice = ItemPrice.Replace("zł", "");
                    var decimalPrice = Decimal.Parse(ItemPrice, CultureInfo.InvariantCulture);

                    var ParametersNode = item.SelectSingleNode(paramList);
                    
                    var link = item.SelectSingleNode(hyperlink).GetAttributeValue("href", "http://allegro.pl");

                    bool isAllegroSmart = (item.SelectSingleNode(allegroSmart) != null);


                    string tmp = "";
                    string tmp2 = "";

                    DbItems result = new DbItems();
                    result.Add(queryLoad.queryInfo.Id, ItemName, decimalPrice, isAllegroSmart, page, paths[x], link);

                    if (ParametersNode != null)
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
                    x++;
                }
            }
            var qr = new QueryRemover();
            qr.QueryRemower_Work();
        }
    }
}
