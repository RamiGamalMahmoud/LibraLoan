﻿using LibraLoan.Core.Abstraction.Features.Publishers;
using System.Windows;
using System.Windows.Controls;

namespace LibraLoan.Features.Publishers.Listing
{
    internal partial class View : UserControl, IPublishersListingView
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
    }
}
