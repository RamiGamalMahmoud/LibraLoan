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
    public class ViewModelCreateTests(ServicesConfiguration servicesConfiguration) : TestBase(servicesConfiguration)
    {
        [Fact]
        public void HasChanges_ShouldBeFalse_AfterCreation()
        {
            ViewModelCreate viewModel = _services.Provider.GetRequiredService<ViewModelCreate>();
            viewModel.HasChanges.Should().BeFalse();
        }

        [Fact]
        public void SaveCommandCanExecute_ShouldBeFalse_AfterCreation()
        {
            ViewModelCreate viewModel = _services.Provider.GetRequiredService<ViewModelCreate>();
            
            viewModel.SaveCommand.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void SaveCommandExecute_ShouldBeFalse_WhenBookTitleNotSet()
        {
            ViewModelCreate viewModel = _services.Provider.GetRequiredService<ViewModelCreate>();

            
            viewModel.ISBN = "test isbn";
            viewModel.PublishDate = DateTime.Now;
            viewModel.Version = 1;
            viewModel.Copies = 1;
            
            viewModel.SelectedPublisher = new LibraLoan.Core.Models.Publisher();

            viewModel.SaveCommand.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void SaveCommandExecute_ShouldBeFalse_WhenISBNNotSet()
        {
            ViewModelCreate viewModel = _services.Provider.GetRequiredService<ViewModelCreate>();


            viewModel.BookTitle = "test title";
            viewModel.PublishDate = DateTime.Now;
            viewModel.Version = 1;
            viewModel.Copies = 1;

            viewModel.SelectedPublisher = new LibraLoan.Core.Models.Publisher();

            viewModel.SaveCommand.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void SaveCommandExecute_ShouldBeFalse_WhenDatePublishedNotSet()
        {
            ViewModelCreate viewModel = _services.Provider.GetRequiredService<ViewModelCreate>();


            viewModel.BookTitle = "test title";
            viewModel.ISBN = "test isbn";
            viewModel.Version = 1;
            viewModel.Copies = 1;

            viewModel.SelectedPublisher = new LibraLoan.Core.Models.Publisher();

            viewModel.SaveCommand.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void SaveCommandExecute_ShouldBeFalse_WhenVersionNotSet()
        {
            ViewModelCreate viewModel = _services.Provider.GetRequiredService<ViewModelCreate>();


            viewModel.BookTitle = "test title";
            viewModel.ISBN = "test isbn";
            viewModel.PublishDate = DateTime.Now;
            viewModel.Version = null;
            viewModel.Copies = 1;

            viewModel.SelectedPublisher = new LibraLoan.Core.Models.Publisher();

            viewModel.SaveCommand.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void SaveCommandExecute_ShouldBeFalse_CopiesNotSet()
        {
            ViewModelCreate viewModel = _services.Provider.GetRequiredService<ViewModelCreate>();


            viewModel.BookTitle = "test title";
            viewModel.ISBN = "test isbn";
            viewModel.PublishDate = DateTime.Now;
            viewModel.Version = 1;
            viewModel.Copies = null;

            viewModel.SelectedPublisher = new LibraLoan.Core.Models.Publisher();

            viewModel.SaveCommand.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void SaveCommandExecute_ShouldBeFalse_WhenPublisherNotSet()
        {
            ViewModelCreate viewModel = _services.Provider.GetRequiredService<ViewModelCreate>();


            viewModel.BookTitle = "test title";
            viewModel.ISBN = "test isbn";
            viewModel.PublishDate = DateTime.Now;
            viewModel.Version = 1;
            viewModel.Copies = 1;

            viewModel.SaveCommand.CanExecute(null).Should().BeFalse();
        }

        [Fact]
        public void SaveCommandExecute_ShouldBeTrue_AfterAllPropertiesSet()
        {
            ViewModelCreate viewModel = _services.Provider.GetRequiredService<ViewModelCreate>();

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
            viewModel.SaveCommand.CanExecute(null).Should().BeTrue();
        }
    }
}
