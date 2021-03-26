using P_ZPP_1.AppDatabase;
using P_ZPP_1.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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



           

        }

        private List<Items> GetItems(int QuerryID, int page )
        {
            List<Items> qurery = new List<Items>();
            
            using (var db = new AllegroAppContext())
            {
                //var lastTMP = db.QueryInfo.Last<QueryInfo>();
                //var last = lastTMP.Id;
                qurery = db.Items.Where(x=>x.Query_Id== QuerryID).Where(x=>x.PageNumber == page).ToList();
            }
            return qurery;
        }

        private List<ItemParams> GetItemParams(int QuerryID, int itemID)
        {
            List<ItemParams> itemParams = new List<ItemParams>();
            using (var db = new AllegroAppContext())
            {
                itemParams = db.ItemParams.Where(x => x.Querry_id == QuerryID).Where(x => x.Item_id == itemID).ToList();
            }
            return itemParams;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HistoryOfQuerry historyOfQuerry = new HistoryOfQuerry();
            historyOfQuerry.Show();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WebConnection parser = new WebConnection();
            PagesLoadedMemory.currentQuery = PoleSzukaj.Text;
            if (PagesLoadedMemory.currentQuery.Length > 0 )
            {
                await Task.Run(() =>
                {
                    PagesLoadedMemory.ClearInfo();
                    PagesLoadedMemory.SetCurrentPage(1);
                    PagesLoadedMemory.maxPage = parser.GetHtml(PagesLoadedMemory.currentQuery, 1);

                    if (PagesLoadedMemory.maxPage == 0)
                    {
                        MessageBox.Show("Brak ofert dla danego zapytania.");
                        return;
                    }

                    using (var db = new AllegroAppContext())
                    {
                        var id = db.QueryInfo.Where(x => x.Querry == PagesLoadedMemory.currentQuery).Select(x => x.Id).FirstOrDefault();

                        var items = GetItems(id, 1);
                        var listItemId = items.Where(x => x.Query_Id == id).Select(x => x.Id).ToList();
                        if (items.Count > 0)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                ProductList.ItemsSource = items;
                            });
                        }

                        // var paramiters = GetItemParams(id, listItemId);
                    }
                });


                await Task.Run(() =>
                {
                    string usedQuery = PagesLoadedMemory.currentQuery;
                    for (int i = 2; i <= PagesLoadedMemory.maxPage; i++)
                    {
                        //mozliwe ze tymczasowe
                        if (usedQuery != PagesLoadedMemory.currentQuery)
                            return;

                        parser.GetHtml(usedQuery, i);
                        PagesLoadedMemory.maxLoadedPage = i;
                    }
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

                // var paramiters = GetItemParams(id, listItemId);

            }*/

        }


        private async void Poprzednia_strona_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                PagesLoadedMemory.SetCurrentPage(PagesLoadedMemory.GetCurrentPage() - 1);

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
                        });
                    }
                }
            });

        }
        private async void idz_do_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                PagesLoadedMemory.SetCurrentPage(Convert.ToInt32(textbox.Text));
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
                        });
                    }
                }
            });
        }

        private async void następna_strona_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                PagesLoadedMemory.SetCurrentPage(PagesLoadedMemory.GetCurrentPage() + 1);


                using (var db = new AllegroAppContext())
                {
                    var id = db.QueryInfo.Where(x => x.Querry == PagesLoadedMemory.currentQuery).Select(x => x.Id).FirstOrDefault();
                    var nextID = id + PagesLoadedMemory.GetCurrentPage() - 1;
                    var nextpage = PagesLoadedMemory.GetCurrentPage();
                    var items = GetItems(nextID, nextpage);
                    //var listItemId = items.Where(x => x.Query_Id == id).Select(x => x.Id).ToList();
                    if (items.Count > 0)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            ProductList.ItemsSource = items;
                        });
                    }
                }
            });
        }
    }
}
