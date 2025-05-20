using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading.Tasks;

namespace LibraLoan.Features.Publishers.Editor
{
    internal class ViewModelUpdate : ViewModel
    {
        public ViewModelUpdate(IMediator mediator, IMessenger messenger, Publisher publisher) : base(mediator, messenger)
        {
            _publisher = publisher;

            Name = _publisher.Name;
            Phone = _publisher.Phone;
            Fax = _publisher.Fax;
            Email = _publisher.Email;
            Website = _publisher.Website;
            HasChanges = false;
        }

        private readonly Publisher _publisher;

        public override string Title => "تعديل بيانات دار نشر";

        protected override async Task Save()
        {
            bool isConfirmed = _messenger.Send(new Core.Messages.Common.ConfigrRequestMessge());
            if (!isConfirmed) return;
            PublisherDto publisherDto = new PublisherDto(_publisher.Id, Name, Phone, Email, Website, Fax,  null);
            bool isUpdated = await _mediator.Send(new Core.Commands.Common.UpdateCommand<PublisherDto>(publisherDto));

            if (isUpdated)
            {
                _messenger.Send(new Core.Messages.Common.SuccessMessage("تم الحفظ بنجاح"));
                _publisher.Name = Name;
                _publisher.Phone = Phone;
                _publisher.Fax = Fax;
                _publisher.Email = Email;
                _publisher.Website = Website;
                HasChanges = false;
            }
            else
            {
                _messenger.Send(new Core.Messages.Common.ErrorMessage("حدث خطأ في الحفظ"));
            }
        }
    }
}
