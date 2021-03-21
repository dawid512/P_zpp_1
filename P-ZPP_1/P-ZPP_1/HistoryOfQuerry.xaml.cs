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

            List<QueryInfo> queryInfos = new List<QueryInfo>();
            queryInfos.Select(x => x.Querry).ToList();

            

            

        }


        

    }



}
