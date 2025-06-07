using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace LibraLoan.Features.Clients.Listing
{
    internal class ViewModel : ListingViewModelBase<Client>
    {
        public ViewModel(IMediator mediator, IMessenger messenger, IAppStateService appStateService, System.IServiceProvider serviceProvider) : base(mediator, messenger, appStateService)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task SearchAsync()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                Models = _tempModels;
            }
            else
            {
                Models = _tempModels
                    .Where(x => x.Name.Contains(SearchText) || x.Phone.Contains(SearchText) || x.Email is not null && x.Email.Contains(SearchText))
                    .ToList();
            }
            return Task.CompletedTask;
        }

        protected override Task ShowCreate()
        {
            CloseEditor();
            Editor.ViewModelCreate viewModel = _serviceProvider.GetRequiredService<Editor.ViewModelCreate>();
            EditorView = new Editor.View(viewModel);
            return Task.CompletedTask;
        }

        protected override Task ShowUpdate(Client model)
        {
            CloseEditor();
            Editor.ViewModel viewModel = new Editor.ViewModelUpdate(_mediator, _messenger, model);
            EditorView = new Editor.View(viewModel);
            return Task.CompletedTask;
        }

        protected override bool CanCreate()
        {
            return _appStateService.CurrentUser.HasPermission(Resource.ClientsResource, Action.CreateAction) && base.CanCreate();
        }

        protected override bool CanDelete(Client model)
        {
            return base.CanDelete(model) && _appStateService.CurrentUser.HasPermission(Resource.ClientsResource, Action.DeleteAction);
        }

        protected override bool CanUpdate(Client model)
        {
            return base.CanUpdate(model) && _appStateService.CurrentUser.HasPermission(Resource.ClientsResource, Action.UpdateAction);
        }
        private readonly System.IServiceProvider _serviceProvider;
    }
}
