using System;
using System.Windows;

namespace SpaceDoctor.View
{
    /// <summary>
    /// Логика взаимодействия для XSignInWindow.xaml
    /// </summary>
    public partial class XSignInWindow : Window
    {
        Func<Tuple<String, String>> _createNewClient;

        public static Tuple<String,String> CreateSignInWindow(Func<Tuple<String,String>> createNewClient)
        {
            XSignInWindow wnd = new XSignInWindow();
            wnd._createNewClient = createNewClient;



            if (wnd.ShowDialog().GetValueOrDefault())
            {
                if (!String.IsNullOrEmpty(wnd.XTextBoxLogin.Text))
                    return new Tuple<string, string>(wnd.XTextBoxLogin.Text, wnd.XTextBoxPass.Password);
                else
                {
                    MessageBox.Show("Друг, ты забыл ввести логин");
                    return new Tuple<string, string>("", "");
                }
            }
            else
                return null;
        }

        private XSignInWindow()
        {
            InitializeComponent();
        }

        private void ButtonSiGnInClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void ButtonRegisterClick(object sender, RoutedEventArgs e)
        {
           var tuple = _createNewClient.Invoke();
            XTextBoxLogin.Text = tuple.Item1;
            XTextBoxPass.Password = tuple.Item2;         
        }
    }
}
