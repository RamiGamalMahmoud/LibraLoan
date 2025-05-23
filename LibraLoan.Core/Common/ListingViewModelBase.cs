using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraLoan.Core.Common
{
    public abstract partial class ListingViewModelBase<TModel> : ObservableObject where TModel : class
    {
        public ListingViewModelBase(IMediator mediator, IMessenger messenger, IAppStateService appStateService)
        {
            _mediator = mediator;
            _messenger = messenger;
            _appStateService = appStateService;
        }

        protected string _deleteMessage = "هل تريد الحذف؟";

        [ObservableProperty]
        protected IEnumerable<TModel> _models;
        protected IEnumerable<TModel> _tempModels;

        [RelayCommand]
        protected virtual async Task LoadDataAsync(bool reload)
        {
            _tempModels = await _mediator.Send(new Core.Commands.Common.GetAllCommand<TModel>(reload));
            Models = _tempModels;
        }

        [RelayCommand(CanExecute = nameof(CanDelete))]
        protected virtual async Task Delete(TModel model)
        {
            bool isConfirmed = _messenger.Send(new Core.Messages.Common.ConfigrRequestMessge(_deleteMessage));

            if (isConfirmed is true)
            {
                bool isDeleted = await _mediator.Send(new Core.Commands.Common.DeleteCommand<TModel>(model));

                if (isDeleted)
                {
                    _messenger.Send(new Core.Messages.Common.SuccessMessage("تم الحذف بنجاح"));
                    return;
                }

                _messenger.Send(new Core.Messages.Common.ErrorMessage("خطأ : بيانات مستخدمة لا يمكن حذفها"));
            }
        }

        [RelayCommand(CanExecute = nameof(CanUpdate))]
        protected virtual async Task ShowUpdate(TModel model)
        {
            await _mediator.Send(new Core.Commands.Common.ShowUpdateCommand<TModel>(model));
        }

        [RelayCommand(CanExecute = nameof(CanCreate))]
        protected virtual async Task ShowCreate()
        {
            await _mediator.Send(new Core.Commands.Common.ShowCreateCommand<TModel>());
        }

        async partial void OnSearchTextChanged(string oldValue, string newValue)
        {
            await SearchAsync();
        }

        protected virtual Task SearchAsync()
        {
            return Task.CompletedTask;
        }

        [ObservableProperty]
        protected string _searchText;
        protected virtual bool CanDelete(TModel model) => model is not null;
        protected virtual bool CanUpdate(TModel model) => model is not null;
        protected virtual bool CanCreate() => true;
        protected readonly IMediator _mediator;
        protected readonly IMessenger _messenger;
        protected readonly IAppStateService _appStateService;
    }
}
