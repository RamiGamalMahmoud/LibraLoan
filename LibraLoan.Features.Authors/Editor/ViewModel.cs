using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LibraLoan.Features.Authors.Editor
{
    internal abstract partial class ViewModel : EditorViewModelBase<Author>
    {
        protected ViewModel(IMediator mediator, IMessenger messenger): base()
        {
            _mediator = mediator;
            _messenger = messenger;
            _notifyPropertiesNames = [nameof(Name)];
        }
        [ObservableProperty]
        [Required(ErrorMessage = "يجب ادخال اسم المؤلف")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _name;

        protected IMediator _mediator;
        protected IMessenger _messenger;
    }
}
