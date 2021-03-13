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
            var QuerryResult = GetQuerryResult();

            if (QuerryResult.Count > 0)
                ListViewProducts.ItemsSource = QuerryResult;

        }

        public List<QuerryResult> GetQuerryResult()
        {

            return new List<QuerryResult>()
      {
        new QuerryResult(1,1,"Product 1", 205, 225,6,true, "/1.jpg"),
        new QuerryResult(1,1,"Product 2", 105, 125,6,true, "/1.jpg"),
        new QuerryResult(1,1,"Product 3", 305, 325,6,false, "/1.jpg"),
        new QuerryResult(1,2,"Product 3", 305, 325,6,false, "/1.jpg"),
        new QuerryResult(1,2,"Product 3", 305, 325,6,false, "/1.jpg"),

      };
            

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}  


