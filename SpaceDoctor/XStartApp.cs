using System;

namespace SpaceDoctor
{
    class XStartApp
    {
        [STAThread()]
        public static void Main()
        {
            App a = new App();
            a.InitializeComponent();

            View.MainWindow wnd = new View.MainWindow();

            a.Run(wnd);
        }

    }
}
