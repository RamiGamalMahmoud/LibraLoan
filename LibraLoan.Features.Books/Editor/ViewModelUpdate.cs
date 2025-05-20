using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.Common;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Books.Editor
{
    internal class ViewModelUpdate : ViewModel
    {
        public ViewModelUpdate(IMediator mediator, IMessenger messenger, Book book) : base(mediator, messenger)
        {
            System.ArgumentNullException.ThrowIfNull(book);
            _book = book;

            BookTitle = _book.Title;
            ISBN = _book.Isbn;
            PublishDate = _book.PublishDate;
            Version = _book.Version;
            Copies = _book.Copies;
            Photo = _book.Photo;
            SelectedPublisher = _book.Publisher;
            HasChanges = false;
        }

        private readonly Book _book;

        public override string Title => "تعديل بيانات كتاب";

        protected override async Task LoadDataAsync()
        {
            IEnumerable<Author> authors = await _mediator.Send(new GetAllCommand<Author>());
            Authors = authors.Select(x => new SelectableObject<Author>(x))
                .ToList();

            Authors.ToList().ForEach(x => x.IsSelected = _book.Authors.Any(y => y.Id == x.Value.Id));

        }

        public Book Book => _book;

        protected override async Task Save()
        {
            bool isConfirmed = _messenger.Send(new Core.Messages.Common.ConfigrRequestMessge("هل تريد حفظ التعديلات ؟"));
            if (!isConfirmed) return;
            BookDto bookDto = new BookDto(_book.Id, BookTitle, ISBN, SelectedPublisher, (int)Version, Photo, (System.DateTime)PublishDate, (int)Copies, null, SelectedAuthors);

            bool isUpdated = await _mediator.Send(new UpdateCommand<BookDto>(bookDto));

            if(isUpdated)
            {
                _book.Title = BookTitle;
                _book.Isbn = ISBN;
                _book.PublishDate = (System.DateTime)PublishDate;
                _book.Version = (int)Version;
                _book.Copies = (int)Copies;
                _book.Publisher = SelectedPublisher;
                _book.Photo = Photo;

                _book.Authors.Clear();
                foreach(Author author in SelectedAuthors)
                {
                    _book.Authors.Add(author);
                }

                _messenger.Send(new Core.Messages.Common.SuccessMessage("تم التعديل بنجاح"));
            }
            else
            {
                _messenger.Send(new Core.Messages.Common.ErrorMessage("فشل التحديث"));
            }
        }
    }
}
