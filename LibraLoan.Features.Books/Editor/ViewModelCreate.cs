using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading.Tasks;

namespace LibraLoan.Features.Books.Editor
{
    internal class ViewModelCreate : ViewModel
    {
        public ViewModelCreate(IMediator mediator, IMessenger messenger, IAppStateService appStateService) : base(mediator, messenger)
        {
            _appStateService = appStateService;
        }

        public override string Title => "إضافة كتاب جديد";

        protected async override Task Save()
        {
            bool isConfirmed = _messenger.Send(new Core.Messages.Common.ConfigrRequestMessge("هل تريد حفظ التعديلات ؟"));
            if (!isConfirmed) return;
            BookDto bookDto = new BookDto(0, BookTitle, ISBN, SelectedPublisher, (int)Version , Photo, (System.DateTime)PublishDate, (int)Copies, _appStateService.CurrentUser, SelectedAuthors);
            Book book = await _mediator.Send(new Core.Commands.Common.CreateCommand<Book, BookDto>(bookDto));

            if (book is null)
            {
                _messenger.Send(new Core.Messages.Common.ErrorMessage("لم يتم الاضافة"));
            }
            else
            {
                _messenger.Send(new Core.Messages.Common.SuccessMessage("تم الاضافة بنجاح"));
            }
        }

        private readonly IAppStateService _appStateService;
    }
}
