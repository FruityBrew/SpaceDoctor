using SpaceDoctor.ViewModel;
using System.Threading;
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

            DataContext = _wndVM;
        }

        private void ButtonRunNow_Click(object sender, RoutedEventArgs e)
        {
            XTabItemToday.IsSelected = true;
        }


        private void XButtonPlan_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Исследование внесено в ваш гениальный план!");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            XDataGridNEWExParamTypes.Visibility = Visibility.Visible;
            XButtonNEWExamSave.Visibility = Visibility.Visible;
            XButtonPlan.IsEnabled = false;
            XButtonRunNow.IsEnabled = false;
        }

        private void XButtonNEWExamSave_Click(object sender, RoutedEventArgs e)
        {
            XDataGridNEWExParamTypes.Visibility = Visibility.Hidden;
            XButtonNEWExamSave.Visibility = Visibility.Hidden;
            XButtonPlan.IsEnabled = true;
            XButtonRunNow.IsEnabled = true;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void XButtonNEWKitSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
