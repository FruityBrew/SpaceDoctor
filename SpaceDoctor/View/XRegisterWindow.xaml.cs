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
using System.Windows.Shapes;

namespace SpaceDoctor.View
{
    /// <summary>
    /// Логика взаимодействия для XRegisterWindow.xaml
    /// </summary>
    public partial class XRegisterWindow : Window
    {

        public static Tuple<String,String,String,String> CreateRegisterWindow()
        {
            var wnd = new XRegisterWindow();
            if (wnd.ShowDialog().GetValueOrDefault())
            {
                var result = new Tuple<String, String, String, String>(
                                                                        wnd.XTextBoxName.Text,
                                                                        wnd.XTextBoxLogin.Text,
                                                                        wnd.XTextBoxPass.Password,
                                                                        wnd.XTextBoxEmail.Text);
                return result;
            }
            else
                return null;
        }    

        private XRegisterWindow()
        {
            InitializeComponent();
        }

        private void ButtonOKClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
