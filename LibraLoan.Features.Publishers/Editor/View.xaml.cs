﻿using System.Windows;
using System.Windows.Controls;

namespace LibraLoan.Features.Publishers.Editor
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
            if(DataContext is ViewModel viewModel)
            {
                await Dispatcher.Invoke(() => viewModel.LoadDataCommand.ExecuteAsync(false));
            }
        }
    }
}
