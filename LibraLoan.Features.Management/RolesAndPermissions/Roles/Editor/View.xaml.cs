using System.Windows;
using System.Windows.Controls;

namespace LibraLoan.Features.Management.RolesAndPermissions.Roles.Editor
{
    internal partial class View : UserControl
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            Loaded += View_Loaded;
        }

        private async void View_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is not ViewModel viewModel) return;
            await Dispatcher.InvokeAsync(() => viewModel.LoadCommand.ExecuteAsync(false));
        }

        private void CheckBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ViewModel viewModel = DataContext as ViewModel;
            if (viewModel is null) return;

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if(sender is CheckBox permissionCheckPox)
            {
                permissionCheckPox.Command?.Execute(permissionCheckPox.CommandParameter);
            }
        }
    }
}
