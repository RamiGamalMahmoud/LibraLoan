using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;
using MediatR;
using System.Threading.Tasks;

namespace LibraLoan.Features.Authors.Editor
{
    internal class ViewModelUpdate : ViewModel
    {
        public ViewModelUpdate(IMediator mediator, IMessenger messenger, Author author) : base(mediator, messenger)
        {
            _author = author;
            Name = author.Name;
            HasChanges = false;
        }

        private Author _author;

        public override string Title => "تعديل مؤلف";

        protected override async Task Save()
        {
            bool isConfirmed = _messenger.Send(new Core.Messages.Common.ConfigrRequestMessge());
            if (!isConfirmed) return;
            AuthorDto authorDto = new AuthorDto(_author.Id, Name.Trim(), null);
            bool isUpdated = await _mediator.Send(new Core.Commands.Common.UpdateCommand<AuthorDto>(authorDto));

            if (isUpdated)
            {
                _messenger.Send(new Core.Messages.Common.SuccessMessage("تم الحفظ بنجاح"));
                _author.Name = Name;
                HasChanges = false;
                return;
            }
            _messenger.Send(new Core.Messages.Common.ErrorMessage("حدث خطأ في الحفظ"));
        }
    }
}
