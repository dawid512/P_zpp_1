using P_ZPP_1.AppDatabase;
using P_ZPP_1.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace P_ZPP_1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {

            InitializeComponent();
            var db = new AppDatabase.AllegroAppContext();

            db.Database.CreateIfNotExists();
            var testybazydanych = new Tests_1();
            testybazydanych.RunDatabaseTests();

            var querry = GetQuerry();


            combox.ItemsSource = querry.Distinct();
            poprzednia_strona.IsEnabled = false;
            następna_strona.IsEnabled = false;
            historyButton.IsEnabled = false;



        }


        /// <summary>
        /// Gets info about Query from <see cref="QueryInfo"/> table.
        /// </summary>
        /// <returns>List of Items from <see cref="QueryInfo"/>table.</returns>
        private List<string> GetQuerry()
        {
            List<QueryInfo> qurery = new List<QueryInfo>();
            List<string> listOfString = new List<string>();
            using (var db = new AllegroAppContext())
            {
                var id = db.QueryInfo.Select(X => X.Querry).ToList();

                var tmpitems = db.Items.Where(x => x.PageNumber == 1).Select(x => x.Query_Id).ToList();
                var queryinfolist = new List<QueryInfo>();

                foreach (var item in tmpitems)
                {
                    queryinfolist.Add(db.QueryInfo.Where(x => x.Id == item).FirstOrDefault());
                }

                return queryinfolist.Select(x => x.Querry).ToList();
            }

        }
        /// <summary>
        /// Gets the items from <see cref="Items"/> table.
        /// </summary>
        /// <param name="QuerryID">Id of query.</param>
        /// <param name="page">Page number.</param>
        /// <returns>List of items from <see cref="Items"/> table.</returns>
        private List<Items> GetItems(int QuerryID, int page)
        {
            List<Items> qurery = new List<Items>();

            using (var db = new AllegroAppContext())
            {
                List<Items> tmp = db.Items.Where(x => x.Query_Id == QuerryID && x.PageNumber == page).ToList();
                qurery = tmp.Skip(2).Take(tmp.Count() - 4).ToList();
            }
            return qurery;
        }
        /// <summary>
        /// Gets the info about item parameters from <see cref="ItemParams"/> table.
        /// </summary>
        /// <param name="itemID">Id of an item.</param>
        /// <returns>List of item parameters.</returns>
        private List<ItemParams> GetItemParams(int itemID)
        {
            using (var db = new AllegroAppContext())
            {
                return db.ItemParams.Where(x => x.Item_id == itemID).ToList();
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            następna_strona.Visibility = Visibility.Hidden;
            poprzednia_strona.Visibility = Visibility.Hidden;
            textboxStrona.Visibility = Visibility.Hidden;
            Hello.Visibility = Visibility.Hidden;
            MyScrollViewer.Visibility = Visibility.Hidden;
            SpinningWheel.Visibility = Visibility.Visible;
            Dead.Visibility = Visibility.Hidden;
            PagesLoadedMemory.currentQuery = combox.SelectedItem.ToString();
            bool dead = false;
            if (PagesLoadedMemory.currentQuery.Length > 0)
            {

                await Task.Run(() =>
                {
                    do
                    {
                    } while (PagesLoadedMemory.loading == 1);
                    PagesLoadedMemory.loading = 1;
                    PagesLoadedMemory.ClearInfo();
                    PagesLoadedMemory.SetCurrentPage(1);


                    if (PagesLoadedMemory.maxPage == -1)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            MyScrollViewer.Visibility = Visibility.Hidden;
                            Dead.Visibility = Visibility.Visible;
                            dead = true;
                        });
                        return;
                    }


                    using (var db = new AllegroAppContext())
                    {
                        var id = db.QueryInfo.Where(x => x.Querry == PagesLoadedMemory.currentQuery).Select(x => x.Id).FirstOrDefault();

                        var items = GetItems(id, 1);
                        var listItemId = items.Where(x => x.Query_Id == id).Select(x => x.Id).ToList();

                        var tmpParserList = new List<P_ZPP_1.Classes.ParsingToWpf>();

                        string myTmp = "";
                        foreach (var item in items)
                        {

                            foreach (var itempar in GetItemParams(item.Id).ToList())
                            {

                                myTmp += itempar.Property_Name + ": " + itempar.Property_Value + " \n";


                            }

                            tmpParserList.Add(new P_ZPP_1.Classes.ParsingToWpf(item, myTmp));

                            myTmp = "";
                        }


                        if (tmpParserList.Count > 0)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                ProductList.ItemsSource = tmpParserList;
                            });
                        }

                    }
                    PagesLoadedMemory.loading = 0;
                });
                if (dead)
                    MyScrollViewer.Visibility = Visibility.Hidden;
                else
                    MyScrollViewer.Visibility = Visibility.Visible;
                SpinningWheel.Visibility = Visibility.Hidden;


            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e) //przycisk wyszukaj
        {
            WebConnection parser = new WebConnection();
            PagesLoadedMemory.currentQuery = PoleSzukaj.Text;
            bool dead = false;
            if (PagesLoadedMemory.currentQuery.Length > 0)
            {

                textboxStrona.Visibility = Visibility.Visible;
                następna_strona.Visibility = Visibility.Visible;
                poprzednia_strona.Visibility = Visibility.Visible;
                Hello.Visibility = Visibility.Hidden;
                MyScrollViewer.Visibility = Visibility.Hidden;
                SpinningWheel.Visibility = Visibility.Visible;
                Dead.Visibility = Visibility.Hidden;
                await Task.Run(() =>
                {
                    do
                    {
                    } while (PagesLoadedMemory.loading == 1);
                    PagesLoadedMemory.loading = 1;

                    PagesLoadedMemory.ClearInfo();
                    PagesLoadedMemory.SetCurrentPage(1);
                    Dispatcher.Invoke(() =>
                    {
                        następna_strona.IsEnabled = false;
                    });

                    PagesLoadedMemory.maxPage = parser.GetHtml(PagesLoadedMemory.currentQuery, 1);

                    if (PagesLoadedMemory.maxPage == -1)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            MyScrollViewer.Visibility = Visibility.Hidden;
                            Dead.Visibility = Visibility.Visible;
                            PagesLoadedMemory.loading = 0;
                            dead = true;
                        });
                        return;
                    }


                    using (var db = new AllegroAppContext())
                    {
                        var id = db.QueryInfo.Where(x => x.Querry == PagesLoadedMemory.currentQuery).Select(x => x.Id).FirstOrDefault();

                        var items = GetItems(id, 1);
                        var listItemId = items.Where(x => x.Query_Id == id).Select(x => x.Id).ToList();

                        var tmpParserList = new List<P_ZPP_1.Classes.ParsingToWpf>();

                        string myTmp = "";
                        foreach (var item in items)
                        {
                            
                            foreach (var itempar in GetItemParams(item.Id).ToList())
                            {
                                
                                    myTmp += itempar.Property_Name + ": " + itempar.Property_Value + " \n";

                                
                            }

                            tmpParserList.Add(new P_ZPP_1.Classes.ParsingToWpf(item, myTmp));

                            myTmp = "";
                        }


                        if (tmpParserList.Count > 0)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                ProductList.ItemsSource = tmpParserList;
                                string aktualnaStrona = PagesLoadedMemory.GetCurrentPage().ToString();
                                textboxStrona.Text = aktualnaStrona;
                            });
                        }


                    }
                    PagesLoadedMemory.loading = 0;
                });
                if (dead)
                    MyScrollViewer.Visibility = Visibility.Hidden;
                else
                    MyScrollViewer.Visibility = Visibility.Visible;
                SpinningWheel.Visibility = Visibility.Hidden;

                var qr = new QueryRemover();
                qr.QueryRemower_Work();
                combox.ItemsSource = GetQuerry().Distinct();

                // textbox.Text = PagesLoadedMemory.GetCurrentPage().ToString();


                //var qr = new QueryRemover();
                //qr.QueryRemower_Work();
                if (dead)
                    return;
                await Task.Run(() =>
                {
                    do
                    {
                    } while (PagesLoadedMemory.loading == 1);
                    PagesLoadedMemory.loading = 1;
                    string usedQuery = PagesLoadedMemory.currentQuery;
                    for (int i = 2; i <= PagesLoadedMemory.maxPage && i <= PagesLoadedMemory.GetCurrentPage() + 6; i++)
                    {
                        if (usedQuery != PagesLoadedMemory.currentQuery)
                        {
                            PagesLoadedMemory.loading = 0;
                            return;
                        }

                        parser.GetHtml(usedQuery, i);
                        PagesLoadedMemory.maxLoadedPage = i;
                        Dispatcher.Invoke(() =>
                        {
                            if (PagesLoadedMemory.GetCurrentPage() >= PagesLoadedMemory.maxLoadedPage)
                                następna_strona.IsEnabled = false;
                            else
                                następna_strona.IsEnabled = true;
                        });
                        do
                        {
                            if (usedQuery != PagesLoadedMemory.currentQuery)
                            {
                                PagesLoadedMemory.loading = 0;
                                return;
                            }
                        } while (i == PagesLoadedMemory.GetCurrentPage() + 5);

                    }
                    PagesLoadedMemory.loading = 0;
                });
            }
            else
            {
                MessageBox.Show("Błąd, Pole wyszukiwania jest puste");
            }


            /*using (var db = new AllegroAppContext())
            {
                var id = db.QueryInfo.Where(x => x.Querry == PagesLoadedMemory.currentQuery).Select(x => x.Id).FirstOrDefault();

                var items = GetItems(id, 1);
                var listItemId = items.Where(x => x.Query_Id == id).Select(x => x.Id).ToList();
                if (items.Count > 0)
                    ProductList.ItemsSource = items;
               

                //var propertyname = paramiters.Select(x => x.Property_Name ).ToList();
                //var propvalue = paramiters.Select(x => x.Property_Value).ToList();

                //if (paramiters.Count > 0)

            */
        }

        private async void Poprzednia_strona_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                PagesLoadedMemory.SetCurrentPage(PagesLoadedMemory.GetCurrentPage() - 1);
                Dispatcher.Invoke(() =>
                {
                    if (PagesLoadedMemory.GetCurrentPage() >= PagesLoadedMemory.maxLoadedPage)
                        następna_strona.IsEnabled = false;
                    else
                        następna_strona.IsEnabled = true;
                    if (PagesLoadedMemory.GetCurrentPage() == 1)
                        poprzednia_strona.IsEnabled = false;
                });

                using (var db = new AllegroAppContext())
                {
                    var id = db.QueryInfo.Where(x => x.Querry == PagesLoadedMemory.currentQuery).Select(x => x.Id).FirstOrDefault();

                    var nextID = id + PagesLoadedMemory.GetCurrentPage() - 1;
                    var nextpage = PagesLoadedMemory.GetCurrentPage();
                    var items = GetItems(nextID, nextpage);
                    var listItemId = items.Where(x => x.Query_Id == id).Select(x => x.Id).ToList();
                    if (items.Count > 0)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            ProductList.ItemsSource = items;
                            string aktualnaStrona = PagesLoadedMemory.GetCurrentPage().ToString();
                            textboxStrona.Text = aktualnaStrona;
                        });
                    }

                    var tmpParserList = new List<P_ZPP_1.Classes.ParsingToWpf>();

                    string myTmp = "";
                    foreach (var item in items)
                    {

                        foreach (var itempar in GetItemParams(item.Id).ToList())
                        {

                            myTmp += itempar.Property_Name + ": " + itempar.Property_Value + " \n";


                        }

                        tmpParserList.Add(new P_ZPP_1.Classes.ParsingToWpf(item, myTmp));

                        myTmp = "";
                    }


                    if (tmpParserList.Count > 0)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            ProductList.ItemsSource = tmpParserList;
                            string aktualnaStrona = PagesLoadedMemory.GetCurrentPage().ToString();
                            textboxStrona.Text = aktualnaStrona;
                        });
                    }


                }
                


                
            });

        }
    
        private async void Następna_strona_Click(object sender, RoutedEventArgs e)
        {
            PagesLoadedMemory.SetCurrentPage(PagesLoadedMemory.GetCurrentPage() + 1);
            Dispatcher.Invoke(() =>
            {
                if (PagesLoadedMemory.GetCurrentPage() >= PagesLoadedMemory.maxLoadedPage)
                    następna_strona.IsEnabled = false;
                else
                    następna_strona.IsEnabled = true;
                if (PagesLoadedMemory.GetCurrentPage() != 1)
                    poprzednia_strona.IsEnabled = true;
            });

            await Task.Run(() =>
            {


                if (PagesLoadedMemory.maxLoadedPage < PagesLoadedMemory.GetCurrentPage())
                {
                    PagesLoadedMemory.SetCurrentPage(PagesLoadedMemory.GetCurrentPage() - 1);

                    return;
                }
                else
                {
                    using (var db = new AllegroAppContext())
                    {
                        var id = db.QueryInfo.Where(x => x.Querry == PagesLoadedMemory.currentQuery).Select(x => x.Id).FirstOrDefault();
                        var nextID = id + PagesLoadedMemory.GetCurrentPage() - 1;
                        var nextpage = PagesLoadedMemory.GetCurrentPage();
                        var items = GetItems(nextID, nextpage);
                        var listItemId = items.Where(x => x.Query_Id == id).Select(x => x.Id).ToList();
                        if (items.Count > 0)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                ProductList.ItemsSource = items;
                                string aktualnaStrona = PagesLoadedMemory.GetCurrentPage().ToString();
                                textboxStrona.Text = aktualnaStrona;
                            });
                        }
                        var tmpParserList = new List<P_ZPP_1.Classes.ParsingToWpf>();

                        string myTmp = "";
                        foreach (var item in items)
                        {

                            foreach (var itempar in GetItemParams(item.Id).ToList())
                            {

                                myTmp += itempar.Property_Name + ": " + itempar.Property_Value + " \n";


                            }

                            tmpParserList.Add(new P_ZPP_1.Classes.ParsingToWpf(item, myTmp));

                            myTmp = "";
                        }


                        if (tmpParserList.Count > 0)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                ProductList.ItemsSource = tmpParserList;
                                string aktualnaStrona = PagesLoadedMemory.GetCurrentPage().ToString();
                                textboxStrona.Text = aktualnaStrona;
                            });
                        }

                    }
                }


                /*string usedQuery = PagesLoadedMemory.currentQuery;
                int currentPage = PagesLoadedMemory.GetCurrentPage();
                for (int i = PagesLoadedMemory.maxLoadedPage; i <= PagesLoadedMemory.GetCurrentPage() + 5; i++)
                {
                    Dispatcher.Invoke(() =>
                    {
                        if (PagesLoadedMemory.GetCurrentPage() >= PagesLoadedMemory.maxLoadedPage)
                            następna_strona.IsEnabled = false;
                        else
                            następna_strona.IsEnabled = true;
                    });

                    if (usedQuery != PagesLoadedMemory.currentQuery || PagesLoadedMemory.GetCurrentPage() != currentPage)
                    {
                        PagesLoadedMemory.loading = 0;
                        return;
                    }
                }
                PagesLoadedMemory.loading = 0;*/
            });
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var hyperlink = (sender as Button).Tag;
            Process.Start(hyperlink.ToString());
        }

        private void combox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (combox.SelectedItem != null)
                historyButton.IsEnabled = true;
        }

        private void Allegrobutton_Click(object sender, RoutedEventArgs e)
        {
            Hello.Visibility = Visibility.Visible;
            MyScrollViewer.Visibility = Visibility.Hidden;
            następna_strona.Visibility = Visibility.Hidden;
            poprzednia_strona.Visibility = Visibility.Hidden;
            textboxStrona.Visibility = Visibility.Hidden;
            Dead.Visibility = Visibility.Hidden;
            PoleSzukaj.Text = String.Empty;
            PagesLoadedMemory.currentQuery = "";
        }
    }
}
