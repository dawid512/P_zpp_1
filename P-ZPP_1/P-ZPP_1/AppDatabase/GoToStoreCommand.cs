using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace P_ZPP_1.AppDatabase
{
    public class GoToStoreCommand : ICommand
    {
        private readonly MainWindow mainWindow;

        public event EventHandler CanExecuteChanged;

        public GoToStoreCommand(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mainWindow.Store(parameter.ToString());
        }
    }
}
