using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading.Tasks;

namespace LibraLoan.Features.Publishers.Editor
{
    internal class ViewModelCreate : ViewModel
    {
        public ViewModelCreate(IMediator mediator, IMessenger messenger, IAppStateService appStateService) : base(mediator, messenger)
        {
            _appStateService = appStateService;
        }

        public override string Title => "إنشاء دار نشر";

        protected override async Task Save()
        {
            PublisherDto publisherDto = new PublisherDto(0, Name, Phone, Email, Website, Fax, _appStateService.CurrentUser);

            Publisher publisher = await _mediator.Send(new Core.Commands.Common.CreateCommand<Publisher, PublisherDto>(publisherDto));
            if (publisher is null)
            {
                _messenger.Send(new Core.Messages.Common.ErrorMessage("حدث خطأ في الحفظ"));
            }
            _messenger.Send(new Core.Messages.Common.SuccessMessage("تم الحفظ بنجاح"));
            ClearInputs();
            HasChanges = false;
        }

        private readonly IAppStateService _appStateService;
    }
}
