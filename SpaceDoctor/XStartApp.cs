using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
