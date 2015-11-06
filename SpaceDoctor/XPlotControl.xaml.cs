using SpaceDoctor.ViewModel;
using System.Windows.Controls;


namespace SpaceDoctor
{
    /// <summary>
    /// Логика взаимодействия для XPlotControl.xaml
    /// </summary>
    public partial class XPlotControl : UserControl
    {
        public XPlotControl(XPlotVM plot)
        {
             InitializeComponent();
            //   this.Wnd.Title = title;
            DataContext = plot;
        }
        public XPlotControl() 
        {
            InitializeComponent();
         }
    }
}
