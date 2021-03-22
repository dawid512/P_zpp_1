using P_ZPP_1.AppDatabase;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace P_ZPP_1
{
    /// <summary>
    /// Logika interakcji dla klasy HistoryOfQuerry.xaml
    /// </summary>
    public partial class HistoryOfQuerry : Window
    {
        public HistoryOfQuerry()
        {
            InitializeComponent();

            var querry = GetQuerry();


            combox.ItemsSource = querry;


          

        }
        private List<string> GetQuerry()
        {
            List<QueryInfo> qurery = new List<QueryInfo>();
            List<string> listOfString = new List<string>();
            using (var db = new AllegroAppContext())
            {
                var id = db.Items.Where(x => x.PageNumber == 1).Select(x => x.Query_Id).FirstOrDefault();

                qurery = db.QueryInfo.Where(x=>x.Id ==id ).ToList();
                foreach (var item in qurery)
                {
                    listOfString.Add(item.Querry);
                }

            }
            // var id = qurery[0].Id; 
           // var QueryString = qurery[1].Querry;

            return listOfString;

        }


    }



}
