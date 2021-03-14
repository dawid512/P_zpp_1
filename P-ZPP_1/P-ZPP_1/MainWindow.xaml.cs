using P_ZPP_1.AppDatabase;
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
<<<<<<< HEAD
            db.Database.CreateIfNotExists();
            Parser parser = new Parser();
            parser.Parse("laptop");
=======
            //db.Database.CreateIfNotExists();
>>>>>>> main

            //Parser parser = new Parser();
            //parser.Parse("laptop");

            var items = GetItems();
            if (items.Count > 0)
                ProductList.ItemsSource = items;

        }

        private List<Items> GetItems()
        {
            return new List<Items>()
            {
                new Items(1,1,"Produkt 1",300,325,5,true,"/Photos/1.jpg"),
                 new Items(2,2,"Produkt 1",300,325,5,true,"/Photos/smart.png"),
                  new Items(3,4,"Produkt 1",300,325,5,true,"/Photos/1.jpg"),
                   new Items(4,5,"Produkt 1",300,325,5,true,"/Photos/1.jpg")
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
