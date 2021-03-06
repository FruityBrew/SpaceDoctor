﻿using SpaceDoctor.ViewModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

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

        private void XButtonNEWKitSave_Click(object sender, RoutedEventArgs e)
        {
            XDataGridAllDrags.Visibility = Visibility.Hidden;
            XButtonNEWKitSave.Visibility = Visibility.Hidden;
        }

        private void TabItemDragsCreateNewDragKit_Click(object sender, RoutedEventArgs e)
        {
            XDataGridAllDrags.Visibility = Visibility.Visible;
            XButtonNEWKitSave.Visibility = Visibility.Visible;
        }

        private void CreateNewPlotSubWnd_Click(object sender, RoutedEventArgs e)
        {
           XStack.Children.Add(_wndVM.AddWnd());
        }


        private void XDataGridExtypes_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            XDataGridExtypes.CurrentCell = e.AddedCells[0];
        }

        private void ValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                MessageBox.Show(e.Error.ErrorContent.ToString());
            }
        }

        private void DataGridTextColumn_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                MessageBox.Show(e.Error.ErrorContent.ToString());
            }
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            _wndVM.Dal.Dispose();
        }
    }
}
