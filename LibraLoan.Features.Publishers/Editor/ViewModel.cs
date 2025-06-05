using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LibraLoan.Features.Publishers.Editor
{
    internal abstract partial class ViewModel : EditorViewModelBase
    {
        protected ViewModel(IMediator mediator, IMessenger messenger) : base()
        {
            _messenger = messenger;
            _mediator = mediator;

            _notifyPropertiesNames = [nameof(Name), nameof(Phone), nameof(Fax), nameof(Email), nameof(Website)];
        }

        [RelayCommand]
        private Task LoadDataAsync()
        {
            return Task.CompletedTask;
        }

        [RelayCommand]
        protected virtual void ClearInputs()
        {
            Name = null;
            Phone = null;
            Fax = null;
            Email = null;
            Website = null;
            HasChanges = false;
        }

        [ObservableProperty]
        [Required(ErrorMessage = "يجب ادخال اسم دار النشر")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _name;

        [ObservableProperty]
        [Phone(ErrorMessage = "يجب ادخال رقم هاتف صحيح")]
        [Required(ErrorMessage = "يجب ادخال رقم الهاتف")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _phone;

        [ObservableProperty]
        [Phone(ErrorMessage = "يجب ادخال رقم فاكس صحيح")]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _fax;

        [ObservableProperty]
        [EmailAddress(ErrorMessage = "يجب ادخال عنوان بريد صحيح")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _email;

        [ObservableProperty]
        [Url(ErrorMessage = "يجب ادخال عنوان موقع صحيح")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _website;

        protected IMediator _mediator;
        protected IMessenger _messenger;

    }
}
