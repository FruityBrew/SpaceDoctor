using SpaceDoctor.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

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
            XTextBoxName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            XTextBoxCalendarAdress.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.XCheckBox.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
            this.XDatePickerDateBirthday.GetBindingExpression(DatePicker.SelectedDateProperty).UpdateSource();
            this.DialogResult = true;
        }
    }
}
