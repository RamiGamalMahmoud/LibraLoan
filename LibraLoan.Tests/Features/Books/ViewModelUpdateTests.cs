using FluentAssertions;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using LibraLoan.Features.Books.Editor;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraLoan.Tests.Features.Books
{
    public class ViewModelUpdateTests(ServicesConfiguration servicesConfiguration) : TestBase(servicesConfiguration)
    {
        [Fact]
        public void HasErrors_ShouldBeFalse_AfterCreation()
        {
            ViewModelUpdate viewModel = _services.Provider.GetRequiredService<ViewModelUpdate>();
            viewModel.HasErrors.Should().BeFalse();
        }

        [Fact]
        public void HasChanges_ShouldBeFalse_AfterCreation()
        {
            ViewModelUpdate viewModel = _services.Provider.GetRequiredService<ViewModelUpdate>();
            viewModel.HasChanges.Should().BeFalse();
        }

        [Fact]
        public void SaveCommandCanExecute_ShouldBeFalse_AfterCreation()
        {
            ViewModelUpdate viewModel = _services.Provider.GetRequiredService<ViewModelUpdate>();

            viewModel.SaveCommand.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void SaveCommandExecute_ShouldBeFalse_WhenBookTitleBeNull()
        {
            ViewModelUpdate viewModel = _services.Provider.GetRequiredService<ViewModelUpdate>();

            viewModel.BookTitle = null;
            viewModel.ISBN = "test isbn";
            viewModel.PublishDate = DateTime.Now;
            viewModel.Version = 1;
            viewModel.Copies = 1;

            viewModel.SelectedPublisher = new LibraLoan.Core.Models.Publisher();

            viewModel.SaveCommand.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void SaveCommandExecute_ShouldBeFalse_WhenISBNBeNull()
        {
            ViewModelUpdate viewModel = _services.Provider.GetRequiredService<ViewModelUpdate>();


            viewModel.BookTitle = "test title";
            viewModel.ISBN = null;
            viewModel.PublishDate = DateTime.Now;
            viewModel.Version = 1;
            viewModel.Copies = 1;

            viewModel.SelectedPublisher = new LibraLoan.Core.Models.Publisher();

            viewModel.SaveCommand.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void SaveCommandExecute_ShouldBeFalse_WhenDatePublishedBeNull()
        {
            ViewModelUpdate viewModel = _services.Provider.GetRequiredService<ViewModelUpdate>();


            viewModel.BookTitle = "test title";
            viewModel.ISBN = "test isbn";
            viewModel.PublishDate = null;
            viewModel.Version = 1;
            viewModel.Copies = 1;

            viewModel.SelectedPublisher = new LibraLoan.Core.Models.Publisher();

            viewModel.SaveCommand.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void SaveCommandExecute_ShouldBeFalse_WhenVersionBeNull()
        {
            ViewModelUpdate viewModel = _services.Provider.GetRequiredService<ViewModelUpdate>();


            viewModel.BookTitle = "test title";
            viewModel.ISBN = "test isbn";
            viewModel.PublishDate = DateTime.Now;
            viewModel.Version = null;
            viewModel.Copies = 1;

            viewModel.SelectedPublisher = new LibraLoan.Core.Models.Publisher();

            viewModel.SaveCommand.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void SaveCommandExecute_ShouldBeFalse_CopiesBeNull()
        {
            ViewModelUpdate viewModel = _services.Provider.GetRequiredService<ViewModelUpdate>();


            viewModel.BookTitle = "test title";
            viewModel.ISBN = "test isbn";
            viewModel.PublishDate = DateTime.Now;
            viewModel.Version = 1;
            viewModel.Copies = null;

            viewModel.SelectedPublisher = new LibraLoan.Core.Models.Publisher();

            viewModel.SaveCommand.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void SaveCommandExecute_ShouldBeFalse_WhenPublisherBeNull()
        {
            ViewModelUpdate viewModel = _services.Provider.GetRequiredService<ViewModelUpdate>();


            viewModel.BookTitle = "test title";
            viewModel.ISBN = "test isbn";
            viewModel.PublishDate = DateTime.Now;
            viewModel.Version = 1;
            viewModel.Copies = 1;
            viewModel.SelectedPublisher = null;

            viewModel.SaveCommand.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void SaveCommandExecute_ShouldBeTrue_AfterAllPropertiesSet()
        {
            ViewModelUpdate viewModel = _services.Provider.GetRequiredService<ViewModelUpdate>();
            viewModel.Authors = new List<SelectableObject<Author>>()
            {
                new SelectableObject<Author>(new Author() { Id = 1, Name = "Test Author 1" }),
                new SelectableObject<Author>(new Author() { Id = 2, Name = "Test Author 2" }),
                new SelectableObject<Author>(new Author() { Id = 3, Name = "Test Author 3" }),
            };


            viewModel.BookTitle = "test title";
            viewModel.ISBN = "test isbn";
            viewModel.PublishDate = DateTime.Now;
            viewModel.Version = 1;
            viewModel.Copies = 1;

            viewModel.SelectedPublisher = new LibraLoan.Core.Models.Publisher();

            viewModel.Authors.First().IsSelected = true;
            viewModel.UpdateAuthorsSelectionCommand.Execute(null);

            viewModel.HasChanges.Should().BeTrue();
            viewModel.CanSave.Should().BeTrue();
            viewModel.SaveCommand.CanExecute(null).Should().BeTrue();
        }
    }
}
