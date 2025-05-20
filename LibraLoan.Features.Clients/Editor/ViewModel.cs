using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LibraLoan.Features.Clients.Editor
{
    internal abstract partial class ViewModel : EditorViewModelBase
    {
        protected ViewModel(IMediator mediator, IMessenger messenger) : base()
        {
            _mediator = mediator;
            _messenger = messenger;

            _notifyPropertiesNames = [nameof(Name), nameof(Phone), nameof(Email)];
        }

        [ObservableProperty]
        [Required(ErrorMessage = "يجب ادخال اسم العميل")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _name;

        [ObservableProperty]
        [Required(ErrorMessage = "يجب ادخال رقم الهاتف")]
        [Phone(ErrorMessage = "رقم الهاتف غير صحيح")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _phone;

        [ObservableProperty]
        [EmailAddress(ErrorMessage = "البريد الالكتروني غير صحيح")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _email;

        [RelayCommand]
        protected void ClearInputs()
        {
            Name = null;
            Phone = null;
            Email = null;
            HasChanges = false;
        }

        protected IMediator _mediator;
        protected IMessenger _messenger;

    }
}
