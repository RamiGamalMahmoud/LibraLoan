using System.Windows;

namespace LibraLoan.Features.Management.RolesAndPermissions.Roles.Editor
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
            ViewModel viewModel = DataContext as ViewModel;

            if (viewModel is null) return;
            await Dispatcher.InvokeAsync(() => viewModel.LoadCommand.ExecuteAsync(false));
        }

        private void CheckBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ViewModel viewModel = DataContext as ViewModel;
            if (viewModel is null) return;

        }

        private void Button_Click(object sender, RoutedEventArgs e) => Close();
    }
}
