using SpaceDoctor.ViewModel;
using System;
using System.Windows;

namespace SpaceDoctor.View
{
    /// <summary>
    /// Логика взаимодействия для XProfileWindow.xaml
    /// </summary>
    public partial class XProfileWindow : Window
    {
        static XClientVM _client;

        public static Boolean CreateXProfileWnd(XClientVM client)
        {
            _client = client;
            XProfileWindow wnd = new XProfileWindow();
            return wnd.ShowDialog().GetValueOrDefault();
         }

        public XProfileWindow()
        {
            InitializeComponent();
            DataContext = _client;
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
