using LibraLoan.Core.Abstraction.Features.Books;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LibraLoan.Features.Books.Listing
{
    internal partial class View : UserControl, IBooksListingView
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
                await Dispatcher.InvokeAsync(() =>
                {
                    viewModel.SearchText = string.Empty;
                    viewModel.LoadDataCommand.ExecuteAsync(false);
                });
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MaterialDesignThemes.Wpf.PackIcon icon = new MaterialDesignThemes.Wpf.PackIcon() { Kind = MaterialDesignThemes.Wpf.PackIconKind.ChevronDown };

            if (sender is Button button)
            {
                MaterialDesignThemes.Wpf.PackIcon icon = button.Content as MaterialDesignThemes.Wpf.PackIcon;
                DataGridRow row = FindAncestor<DataGridRow>(button);
                if (row != null)
                {
                    if (row.DetailsVisibility == Visibility.Collapsed)
                    {
                        row.DetailsVisibility = Visibility.Visible;
                        icon.Kind = MaterialDesignThemes.Wpf.PackIconKind.ChevronUp;
                    }
                    else
                    {
                        row.DetailsVisibility = Visibility.Collapsed;
                        icon.Kind = MaterialDesignThemes.Wpf.PackIconKind.ChevronDown;
                    }
                }
            }
        }

        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            while (current != null)
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            return null;
        }
    }
}
