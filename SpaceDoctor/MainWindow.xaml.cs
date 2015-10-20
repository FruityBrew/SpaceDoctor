using SpaceDoctor.ViewModel;
using System.Windows;

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

            //XExamVM ex = new XExamVM();


            DataContext = _wndVM;
            Model.XParam p = new Model.XParam()
            {
                    //Type = _wndVM.DbContext.
            };

            //_wndVM.Client.ExamsCollection.Add()
        }
    }
}
