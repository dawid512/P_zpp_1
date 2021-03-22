using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_ZPP_1.Classes
{
    public static class PagesLoadedMemory
    {
        private static List<int> pagesLoaded = new List<int>();
        public static int currentPage;
        public static int maxPage { get; set; }
        public static string currentQuery { get; set; }

        static PagesLoadedMemory()
        {

        }

        public static bool LoadedPageAdd(int numberOfPage)
        {
            bool exists = CheckIfPageLoaded(numberOfPage);
            if(!exists)
            {
                pagesLoaded.Add(numberOfPage);
                return true;
            }
            return false;
        }

        public static void ClearInfo()
        {
            pagesLoaded.Clear();
        }

        public static bool CheckIfPageLoaded(int numberOfPage)
        {
            if(pagesLoaded.Contains(numberOfPage))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void SetCurrentPage(int numberOfPage)
        {
            currentPage = numberOfPage;
        }

        public static int GetCurrentPage()
        {
            return currentPage;
        }
    }
}
