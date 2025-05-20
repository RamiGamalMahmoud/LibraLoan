using FluentAssertions;
using LibraLoan.Features.Loans.Editor;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LibraLoan.Tests.Features.Loans
{
    public class ViewModelCreateTests(ServicesConfiguration servicesConfiguration) : TestBase(servicesConfiguration)
    {
        [Fact]
        public void HasErrors_ShouldBeTrue_WhenViewModelIsCreated()
        {
            ViewModelCreate viewModel = _services.Provider.GetRequiredService<ViewModelCreate>();
            viewModel.HasErrors.Should().BeTrue();
        }

        [Fact]
        public void SaveCommand_CanNotBeExecuted_WhenViewModelIsCreated()
        {
            ViewModelCreate viewModel = _services.Provider.GetRequiredService<ViewModelCreate>();
            viewModel.SaveCommand.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void SaveCommand_CanNotBeExecuted_WhenExpectedReturnDateIsBeforeLoanDate()
        {
            ViewModelCreate viewModel = _services.Provider.GetRequiredService<ViewModelCreate>();

            viewModel.SelectedBook = new LibraLoan.Core.Models.Book();
            viewModel.SelectedClient = new LibraLoan.Core.Models.Client();
            viewModel.ExpectedReturnDate = DateTime.Now - TimeSpan.FromDays(1);
            viewModel.LoanDate = DateTime.Now;

            viewModel.SaveCommand.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void SaveCommand_CanNotBeExecuted_WhenActualReturnDateIsBeforeLoanDate()
        {
            ViewModelCreate viewModel = _services.Provider.GetRequiredService<ViewModelCreate>();

            viewModel.SelectedBook = new LibraLoan.Core.Models.Book();
            viewModel.SelectedClient = new LibraLoan.Core.Models.Client();
            viewModel.ExpectedReturnDate = DateTime.Now;
            viewModel.ActualReturnDate = DateTime.Now - TimeSpan.FromDays(1);
            viewModel.LoanDate = DateTime.Now;

            viewModel.SaveCommand.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void SaveCommand_CanBeExecuted_WhenIsValid()
        {
            ViewModelCreate viewModel = _services.Provider.GetRequiredService<ViewModelCreate>();

            viewModel.SelectedBook = new LibraLoan.Core.Models.Book();
            viewModel.SelectedClient = new LibraLoan.Core.Models.Client();
            viewModel.ExpectedReturnDate = DateTime.Now;
            viewModel.LoanDate = DateTime.Now;

            viewModel.SaveCommand.CanExecute(null).Should().BeTrue();
        }
    }
}
