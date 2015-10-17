using SpaceDoctor.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpaceDoctor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XMainWndVM _wndVM;
        public MainWindow()
        {
            InitializeComponent();

            _wndVM = new XMainWndVM();

            XExamVM ex = new XExamVM();

            Model.XParam p = new Model.XParam()
            {
                    //Type = _wndVM.DbContext.
             };

            //_wndVM.Client.ExamsCollection.Add()
        }
    }
}
