using System;
using Gtk;
using semafor.ui.MainWindow;

namespace semafor
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args){
            Application.Init();
            // mainWindow mainWin = mainWindow.Create();
            MainWindow mainWin = MainWindow.Create();
            mainWin.Show();
            Application.Run();
        }
    }
}
