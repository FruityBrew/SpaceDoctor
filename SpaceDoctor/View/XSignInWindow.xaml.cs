using System;
using System.Windows;

namespace SpaceDoctor.View
{
    /// <summary>
    /// Логика взаимодействия для XSignInWindow.xaml
    /// </summary>
    public partial class XSignInWindow : Window
    {
        public static Tuple<String,String> CreateSignInWindow()
        {
            XSignInWindow wnd = new XSignInWindow();
            if (wnd.ShowDialog().GetValueOrDefault())
            {
                if (!String.IsNullOrEmpty(wnd.XTextBoxLogin.Text))
                    return new Tuple<string, string>(wnd.XTextBoxLogin.Text, wnd.XTextBoxPass.Password);
                else
                {
                    MessageBox.Show("Друг, ты забыл ввести логин");
                    return null;
                }
            }
            else
                return null;
        }

        public XSignInWindow()
        {
            InitializeComponent();
        }

        private void ButtonSiGnInClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
