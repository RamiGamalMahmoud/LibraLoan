using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading.Tasks;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Clients.Editor
{
    internal class ViewModelCreate : ViewModel
    {
        private readonly IAppStateService _appStateService;

        public ViewModelCreate(IMediator mediator, IMessenger messenger, IAppStateService appStateService) : base(mediator, messenger)
        {
            _appStateService = appStateService;
        }

        public override string Title => "اضافة عميل جديد";

        protected override async Task Save()
        {
            ClientDto clientDto = new ClientDto(0, Name, Email, Phone, _appStateService.CurrentUser);
            Client client = await _mediator.Send(new CreateCommand<Client, ClientDto>(clientDto));
            if (client is null)
            {
                _messenger.Send(new Core.Messages.Common.ErrorMessage("لم يتم الاضافة"));
                return;
            }
            _messenger.Send(new Core.Messages.Common.SuccessMessage("تمت الإضافة بنجاح"));
            ClearInputs();
        }
    }
}
