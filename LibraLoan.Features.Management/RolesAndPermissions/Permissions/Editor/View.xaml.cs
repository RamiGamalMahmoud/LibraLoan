using System;
using System.ComponentModel;
using System.Windows;

namespace LibraLoan.Features.Management.RolesAndPermissions.Permissions.Editor
{
    internal partial class View : Window
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;

            Loaded += View_Loaded;
        }

        private async void View_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModel viewModel)
            {
                await Dispatcher.InvokeAsync(() => viewModel.LoadAsync());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) => Close();
    }
}
