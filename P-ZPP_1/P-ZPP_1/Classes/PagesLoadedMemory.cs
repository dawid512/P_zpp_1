using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1.Classes
{
    public static class PagesLoadedMemory
    {
        public static int currentPage;
        public static int maxPage { get; set; }
        public static int minPage { get; set; }
        public static string currentQuery { get; set; }
        public static int maxLoadedPage { get; set; }
        public static int loading { get; set; }

        static PagesLoadedMemory()
        {

        }

        public static void ClearInfo()
        {
            currentPage = 1;
            maxPage = 1;
            minPage = 1;
            maxLoadedPage = 1;
        }


        public static void SetCurrentPage(int numberOfPage)
        {
            if (numberOfPage < minPage || numberOfPage > maxLoadedPage)
                return;
            else
                currentPage = numberOfPage;
        }

        public static int GetCurrentPage()
        {
            return currentPage;
        }
    }
}
