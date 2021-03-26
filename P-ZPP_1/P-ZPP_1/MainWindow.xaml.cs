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
        public ICommand GoToStore { get; set; }

        public MainWindow()
        {

            InitializeComponent();

            var db = new AppDatabase.AllegroAppContext();

            db.Database.CreateIfNotExists();


            var querry = GetQuerry();


            combox.ItemsSource = querry;

            GoToStore = new GoToStoreCommand(this);




        }

       

        private List<string> GetQuerry()
        {
            List<QueryInfo> qurery = new List<QueryInfo>();
            List<string> listOfString = new List<string>();
            using (var db = new AllegroAppContext())
            {
                var id = db.Items.Where(x => x.PageNumber == 1).Select(x => x.Query_Id).FirstOrDefault();

                qurery = db.QueryInfo.Where(x => x.Id == id).ToList();
                foreach (var item in qurery)
                {
                    listOfString.Add(item.Querry);
                }

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
                qurery = db.Items.Where(x=>x.Query_Id== QuerryID).Where(x=>x.PageNumber == page).ToList();
                

            }
            return qurery;
            


        }

        private List<ItemParams> GetItemParams(int itemID)
        {
            List<ItemParams> itemParams = new List<ItemParams>();
            using (var db = new AllegroAppContext())
            {
                itemParams = db.ItemParams.Where(x => x.Item_id == itemID).ToList();
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
            PagesLoadedMemory.currentQuery = PoleSzukaj.Text;
            bool created;
            if (PagesLoadedMemory.currentQuery.Length > 0 )
            {
                await Task.Run(() =>
                {
                    WebConnection parser = new WebConnection();
                    PagesLoadedMemory.ClearInfo();
                    PagesLoadedMemory.SetCurrentPage(1);

                    PagesLoadedMemory.LoadedPageAdd(1);
                    PagesLoadedMemory.maxPage = parser.GetHtml(PagesLoadedMemory.currentQuery, 1);

                    
                    

                    


                    if (PagesLoadedMemory.maxPage == 0)
                    {
                        MessageBox.Show("Brak ofert dla danego zapytania.");
                        return;
                    }
                    else
                    {
                        

                        for (int i = 2; i <= 3 && i <= PagesLoadedMemory.maxPage; i++)
                        {
                            created = PagesLoadedMemory.LoadedPageAdd(i);
                            if(created)
                                parser.GetHtml(PagesLoadedMemory.currentQuery, i);
                        }



                        


                    }


                });
            }
            else
            {
                MessageBox.Show("Błąd, Pole wyszukiwania jest puste");
            }


            using (var db = new AllegroAppContext())
            {
                var id = db.QueryInfo.Where(x => x.Querry == PagesLoadedMemory.currentQuery).Select(x => x.Id).FirstOrDefault();

                var items = GetItems(id, 1);
               var listItemId = items.Where(x => x.Query_Id == id).Select(x => x.Id).ToList();
                if (items.Count > 0)
                    ProductList.ItemsSource = items;
                foreach (var item in listItemId)
                {
                    var paramiters = GetItemParams(item);
                }

                //var propertyname = paramiters.Select(x => x.Property_Name ).ToList();
                //var propvalue = paramiters.Select(x => x.Property_Value).ToList();

                //if (paramiters.Count > 0)





            }
           
        }


        private async void Poprzednia_strona_Click(object sender, RoutedEventArgs e)
        {
            bool created;
            WebConnection parser = new WebConnection();
            await Task.Run(() =>
            {

                PagesLoadedMemory.SetCurrentPage(PagesLoadedMemory.GetCurrentPage() - 1);
                created = PagesLoadedMemory.LoadedPageAdd(PagesLoadedMemory.GetCurrentPage());
                if (created)
                    parser.GetHtml(PagesLoadedMemory.currentQuery, PagesLoadedMemory.GetCurrentPage());

                for (int i = PagesLoadedMemory.GetCurrentPage(); i >= PagesLoadedMemory.GetCurrentPage() - 2 && i > 0; i--)
                {
                    created = PagesLoadedMemory.LoadedPageAdd(i);
                    if (created)
                        parser.GetHtml(PagesLoadedMemory.currentQuery, i);
                    using (var db = new AllegroAppContext())
                    {
                        var id = db.QueryInfo.Where(x => x.Querry == PagesLoadedMemory.currentQuery).Select(x => x.Id).FirstOrDefault();

                        var items = GetItems(id, i);
                        var listItemId = items.Where(x => x.Query_Id == id).Select(x => x.Id).ToList();
                        if (items.Count > 0)
                            ProductList.ItemsSource = items;
                    }
                }

            });
           
        }
        private async void idz_do_Click(object sender, RoutedEventArgs e)
        {
            bool created;
            WebConnection parser = new WebConnection();
            PagesLoadedMemory.SetCurrentPage(Convert.ToInt32(textbox.Text));
            await Task.Run(() =>
            {
                created = PagesLoadedMemory.LoadedPageAdd(PagesLoadedMemory.GetCurrentPage());
                if(created)
                    parser.GetHtml(PagesLoadedMemory.currentQuery, PagesLoadedMemory.GetCurrentPage());
            });
            await Task.Run(() =>
            {
                for (int i = PagesLoadedMemory.GetCurrentPage(); i <= PagesLoadedMemory.GetCurrentPage() + 2 && i <= PagesLoadedMemory.maxPage; i++)
                {
                    created = PagesLoadedMemory.LoadedPageAdd(i);
                    if (created)
                        parser.GetHtml(PagesLoadedMemory.currentQuery, i);
                }


                for (int i = PagesLoadedMemory.GetCurrentPage(); i >= PagesLoadedMemory.GetCurrentPage() - 2 && i > 0; i--)
                {
                    created = PagesLoadedMemory.LoadedPageAdd(i);
                    if (created)
                        parser.GetHtml(PagesLoadedMemory.currentQuery, i);
                    using (var db = new AllegroAppContext())
                    {
                        var id = db.QueryInfo.Where(x => x.Querry == PagesLoadedMemory.currentQuery).Select(x => x.Id).FirstOrDefault();

                        var items = GetItems(id, i);
                        var listItemId = items.Where(x => x.Query_Id == id).Select(x => x.Id).ToList();
                        if (items.Count > 0)
                            ProductList.ItemsSource = items;
                    }
                }
            });
        }
        private async void następna_strona_Click(object sender, RoutedEventArgs e)
        {
            bool created;
            WebConnection parser = new WebConnection();
            await Task.Run(() =>
            {
                PagesLoadedMemory.SetCurrentPage(PagesLoadedMemory.GetCurrentPage() + 1);
                created = PagesLoadedMemory.LoadedPageAdd(PagesLoadedMemory.GetCurrentPage());
                if(created)
                    parser.GetHtml(PagesLoadedMemory.currentQuery, PagesLoadedMemory.GetCurrentPage());
            });
            await Task.Run(() =>
            {
                for (int i = PagesLoadedMemory.GetCurrentPage(); i <= PagesLoadedMemory.GetCurrentPage() + 2 && i <= PagesLoadedMemory.maxPage; i++)
                {
                    created = PagesLoadedMemory.LoadedPageAdd(i);
                    if (created)
                        parser.GetHtml(PagesLoadedMemory.currentQuery, i);
                   
                }

            });

            using (var db = new AllegroAppContext())
            {
                var id = db.QueryInfo.Where(x => x.Querry == PagesLoadedMemory.currentQuery).Select(x => x.Id).FirstOrDefault();
                var nextID = id + 1;
                var nextpage = PagesLoadedMemory.currentPage + 1;
                var items = GetItems(nextID,  nextpage);
                //var listItemId = items.Where(x => x.Query_Id == id).Select(x => x.Id).ToList();
                if (items.Count > 0)
                    ProductList.ItemsSource = items;
            }

        }

        public void Store(string link)
        {
            Process.Start(link);
        }
    }
}
