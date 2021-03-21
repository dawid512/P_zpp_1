﻿using P_ZPP_1.AppDatabase;
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


            if (inToParser.Length >0 )
            {



                await Task.Run(() =>
                {

                    Parser parser = new Parser();
                    // TODO - Async downloading and parsing
                    //parser.Parse(1, inToParser);
                });

            }else
            {
                MessageBox.Show("Błąd, Pole wyszukiwania jest puste");
            }



            /* var number = 2;
             var query = "laptop";
             Parser parser = new Parser();
             var loadTasks = new Task[5];
             for (int i = 0; i < 5; i++)
             {
                 if(number + i - 2 >= 0)
                     loadTasks[i] = parser.Parse((number + i - 2).ToString(), query);
             }
             Task.WaitAll(loadTasks);*/
        }

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
