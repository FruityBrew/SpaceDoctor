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

            //XExamVM ex = new XExamVM();


            DataContext = _wndVM;
            Model.XParam p = new Model.XParam()
            {
                    //Type = _wndVM.DbContext.
            };

            //_wndVM.Client.ExamsCollection.Add()
        }

        private void ButtonRunNow_Click(object sender, RoutedEventArgs e)
        {
            XDataGridExParamTypes.Visibility = Visibility.Collapsed;
           //XDataGridExParamTypes.ItemsSource = _wndVM.ActualExam.ParamCVSView;
            XDataGridExamParams.Visibility = Visibility.Visible;
            XButtonExamSave.IsEnabled = true;
            XButtonRunNow.IsEnabled = false;
            XButtonPlan.IsEnabled = false;
            XButtonReturnToPlaning.IsEnabled = true;
        }

        private void XButtonExamSave_Click(object sender, RoutedEventArgs e)
        {
            XButtonExamSave.IsEnabled = false;
            XButtonReturnToPlaning.IsEnabled = true;
        }

        private void XButtonReturnToPlaning_Click(object sender, RoutedEventArgs e)
        {
            XButtonPlan.IsEnabled = true;
            XButtonReturnToPlaning.IsEnabled = false;
            XButtonRunNow.IsEnabled = true;
            XDataGridExParamTypes.Visibility = Visibility.Visible;
            XDataGridExamParams.Visibility = Visibility.Hidden;
            
        }

        private void XButtonPlan_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Исследование внесено в ваш гениальный план!");

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            XDataGridNEWExParamTypes.Visibility = Visibility.Visible;
            XButtonNEWExamSave.IsEnabled = true;
        }
    }
}
