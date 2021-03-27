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
            //imgBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,C:/Users/ASUS/Photos/wink.png"));
            var db = new AppDatabase.AllegroAppContext();

            db.Database.CreateIfNotExists();


            var querry = GetQuerry();


            combox.ItemsSource = querry;

            //GoToStore = new GoToStoreCommand(this);




        }



        private List<string> GetQuerry()
        {
            List<QueryInfo> qurery = new List<QueryInfo>();
            List<string> listOfString = new List<string>();
            using (var db = new AllegroAppContext())
            {
                //Where(x => x.PageNumber == 1).Select(x => x.Query_Id)
                var id = db.QueryInfo.Select(X => X.Querry).ToList();
                //foreach (var item in id)
                //{
                //qurery = db.QueryInfo.Where(x => x.Id == item).ToList();
                foreach (var items in id)
                {
                    listOfString.Add(items);
                }
                //}


            }
            // var id = qurery[0].Id; 
            // var QueryString = qurery[1].Querry;

            return listOfString;

        }

        



        private List<Items> GetItems(int QuerryID, int page )
        {
            List<Items> qurery = new List<Items>();
            
            using (var db = new AllegroAppContext())
            {
                //var lastTMP = db.QueryInfo.Last<QueryInfo>();
                //var last = lastTMP.Id;
                List < Items > tmp = db.Items.Where(x=>x.Query_Id == QuerryID && x.PageNumber == page).ToList();
                qurery = tmp.Skip(2).Take(tmp.Count()- 4).ToList();
            }
            return qurery;
        }

        private List<ItemParams> GetItemParams(int itemID)
        {
            //List<ItemParams> itemParams = new List<ItemParams>();
            using (var db = new AllegroAppContext())
            {
                return db.ItemParams.Where(x => x.Item_id == itemID).ToList();
                //return itemParams;
            }
            //return itemParams;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //HistoryOfQuerry historyOfQuerry = new HistoryOfQuerry();
            //historyOfQuerry.Show();

            // URUCHOMIC PRZYCISK OD HISTORII OFFLINE 
            Hello.Visibility = Visibility.Hidden;
            MyScrollViewer.Visibility = Visibility.Hidden;
            SpinningWheel.Visibility = Visibility.Visible;
            Dead.Visibility = Visibility.Hidden;
            WebConnection parser = new WebConnection();
            PagesLoadedMemory.currentQuery = combox.SelectedItem.ToString();
            bool dead = false;
            if (PagesLoadedMemory.currentQuery.Length > 0)
            {

                await Task.Run(() =>
                {
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
                            int i = 0;
                            foreach (var itempar in GetItemParams(item.Id).ToList())
                            {
                                if (i % 2 == 0)
                                    myTmp += itempar.Property_Name + ": " + itempar.Property_Value + " ";
                                else
                                    myTmp += itempar.Property_Name + ": " + itempar.Property_Value + " \n";

                                i++;
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
                });
                if (dead)
                    MyScrollViewer.Visibility = Visibility.Hidden;
                else
                    MyScrollViewer.Visibility = Visibility.Visible;
                SpinningWheel.Visibility = Visibility.Hidden;


            }
        }

            private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Hello.Visibility = Visibility.Hidden;
            MyScrollViewer.Visibility = Visibility.Hidden;
            SpinningWheel.Visibility = Visibility.Visible;
            Dead.Visibility = Visibility.Hidden;
            WebConnection parser = new WebConnection();
            PagesLoadedMemory.currentQuery = PoleSzukaj.Text;
            bool dead = false;
            if (PagesLoadedMemory.currentQuery.Length > 0 )
            {
                await Task.Run(() =>
                {
                    PagesLoadedMemory.ClearInfo();
                    PagesLoadedMemory.SetCurrentPage(1);
                    PagesLoadedMemory.maxPage = parser.GetHtml(PagesLoadedMemory.currentQuery, 1);

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
                            int i = 0;
                            foreach (var itempar in GetItemParams(item.Id).ToList())
                            {
                                if(i%2 == 0)
                                    myTmp += itempar.Property_Name + ": " + itempar.Property_Value+ " "; 
                                else
                                    myTmp += itempar.Property_Name + ": " + itempar.Property_Value + " \n";

                                i++;
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
                });
                if (dead)
                    MyScrollViewer.Visibility = Visibility.Hidden;
                else
                    MyScrollViewer.Visibility = Visibility.Visible;
                SpinningWheel.Visibility = Visibility.Hidden;

                var qr = new QueryRemover();
                qr.QueryRemower_Work();

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

        private async void Następna_strona_Click(object sender, RoutedEventArgs e)
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
        /// <summary>
        /// otwiera strone
        /// </summary>
        /// <param name="link"></param>
        //public void Store(string link)
        //{
        //    Process.Start(link);
        //}

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var hyperlink = (sender as Button).Tag;
            Process.Start(hyperlink.ToString());
        }
    }
}
