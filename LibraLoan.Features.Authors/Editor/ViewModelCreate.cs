using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading.Tasks;

namespace LibraLoan.Features.Authors.Editor
{
    internal class ViewModelCreate : ViewModel
    {
        private readonly IAppStateService _appStateService;

        public ViewModelCreate(IMediator mediator, IMessenger messenger, IAppStateService appStateService) : base(mediator, messenger)
        {
            _appStateService = appStateService;
        }

        public override string Title => "إضافة مؤلف";

        protected override async Task Save()
        {
            AuthorDto authorDto = new AuthorDto(0, Name.Trim(), _appStateService.CurrentUser);
            Author author = await _mediator.Send(new Core.Commands.Common.CreateCommand<Author, AuthorDto>(authorDto));
            if (author is not null)
            {
                _messenger.Send(new Core.Messages.Common.SuccessMessage("تم الحفظ بنجاح"));
                author.CreatedBy = _appStateService.CurrentUser;
                Name = string.Empty;
                return;
            }
            _messenger.Send(new Core.Messages.Common.ErrorMessage("حدث خطأ في الحفظ"));

        }
    }
}
