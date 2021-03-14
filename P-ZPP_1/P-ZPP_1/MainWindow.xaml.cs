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
            db.Database.CreateIfNotExists();
<<<<<<< Updated upstream

            Parser parser = new Parser();
            parser.Parse("laptop");

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

=======
            var QuerryResult = GetQuerryResult();

            if (QuerryResult.Count > 0)
                ListViewProducts.ItemsSource = QuerryResult;

        }

        public List<Items> GetQuerryResult()
        {

            return new List<Items>()
      {
        new Items(1,1,"Product 1", 205, 225,6,true, "/1.jpg"),
        new Items(1,1,"Product 2", 105, 125,6,true, "/1.jpg"),
        new Items(1,1,"Product 3", 305, 325,6,false, "/1.jpg"),
        new Items(1,2,"Product 3", 305, 325,6,false, "/1.jpg"),
        new Items(1,2,"Product 3", 305, 325,6,false, "/1.jpg"),

      };
            

        }
>>>>>>> Stashed changes
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}  


