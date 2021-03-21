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



            var items = GetItems();
            if (items.Count > 0)
                ProductList.ItemsSource = items;

        }

        private List<Items> GetItems()
        {
            return new List<Items>();
            
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HistoryOfQuerry historyOfQuerry = new HistoryOfQuerry();
            historyOfQuerry.Show();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string inToParser = PoleSzukaj.Text;

            if (inToParser.Length > 0 )
            {
                await Task.Run(() =>
                {
                    WebConnection parser = new WebConnection();
                    PagesLoadedMemory.ClearInfo();
                    PagesLoadedMemory.SetCurrentPage(1);

                    PagesLoadedMemory.LoadedPageAdd(1);
                    PagesLoadedMemory.maxPage = parser.GetHtml(inToParser, 1);

                    if (PagesLoadedMemory.maxPage == 0)
                    {
                        MessageBox.Show("Brak ofert dla danego zapytania.");
                        return;
                    }
                    else
                    {
                        for(int i = 2; i <= 3 || i <= PagesLoadedMemory.maxPage; i++)
                        {
                            PagesLoadedMemory.LoadedPageAdd(i);
                            parser.GetHtml(inToParser, i);
                        }
                    }
                });
            }
            else
            {
                MessageBox.Show("Błąd, Pole wyszukiwania jest puste");
            }
        }

        //Dla inkrementacji stron
        /*

                await Task.Run(() =>
                {
                    WebConnection parser = new WebConnection();
                    PagesLoadedMemory.SetCurrentPage(PagesLoadedMemory.GetCurrentPage()+1);
                    
                    for(int i = PagesLoadedMemory.GetCurrentPage(); i <= PagesLoadedMemory.GetCurrentPage() + 2 || i <= PagesLoadedMemory.maxPage; i++)
                    {
                        PagesLoadedMemory.LoadedPageAdd(i);
                        parser.GetHtml(inToParser, i);
                    }

                }
         //Dla dekrementacji stron
        

                await Task.Run(() =>
                {
                    WebConnection parser = new WebConnection();
                    PagesLoadedMemory.SetCurrentPage(PagesLoadedMemory.GetCurrentPage()-1);
                    
                    for(int i = PagesLoadedMemory.GetCurrentPage(); i => PagesLoadedMemory.GetCurrentPage() - 2 || i > 0; i--)
                    {
                        PagesLoadedMemory.LoadedPageAdd(i);
                        parser.GetHtml(inToParser, i);
                    }

                }

        //Dla idź do
        /*

                await Task.Run(() =>
                {
                    WebConnection parser = new WebConnection();
                    PagesLoadedMemory.SetCurrentPage(ConvertTo.Int32(textbox.Text));
                    
                    for(int i = PagesLoadedMemory.GetCurrentPage(); i <= PagesLoadedMemory.GetCurrentPage() + 2 || i <= PagesLoadedMemory.maxPage; i++)
                    {
                        PagesLoadedMemory.LoadedPageAdd(i);
                        parser.GetHtml(inToParser, i);
                    }


                    for(int i = PagesLoadedMemory.GetCurrentPage(); i => PagesLoadedMemory.GetCurrentPage() - 2 || i > 0; i--)
                    {
                        PagesLoadedMemory.LoadedPageAdd(i);
                        parser.GetHtml(inToParser, i);
                    }
                }

         
        */
        private void Poprzednia_strona_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void idz_do_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void następna_strona_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
