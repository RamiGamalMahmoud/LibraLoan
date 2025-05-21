using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading.Tasks;

namespace LibraLoan.Features.Clients.Editor
{
    internal class ViewModelUpdate : ViewModel
    {
        public ViewModelUpdate(IMediator mediator, IMessenger messenger, Client model) : base(mediator, messenger)
        {
            _model = model;
            Name = model.Name;
            Email = model.Email;
            Phone = model.Phone;
            HasChanges = false;
        }

        public override string Title => "تعديل بيانات عميل";

        protected override async Task Save()
        {
            bool isConfirmed = _messenger.Send(new Core.Messages.Common.ConfigrRequestMessge("هل تريد حفظ التعديلات ؟"));
            if (!isConfirmed) return;
            ClientDto clientDto = new ClientDto(_model.Id, Name.Trim(), Email.Trim(), Phone.Trim(), null);
            bool isUpdated = await _mediator.Send(new Core.Commands.Common.UpdateCommand<ClientDto>(clientDto));

            if(isUpdated)
            {
                _model.Name = Name;
                _model.Email = Email;
                _model.Phone = Phone;
                _messenger.Send(new Core.Messages.Common.SuccessMessage("تم التعديل بنجاح"));
                HasChanges = false;
                return;
            }
            _messenger.Send(new Core.Messages.Common.ErrorMessage("خطاء في التعديل"));
        }

        private Client _model;
    }
}
