using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using ActionModel = LibraLoan.Core.Models.Action;

namespace LibraLoan.Features.Publishers.Listing
{
    internal class ViewModel : ListingViewModelBase<Publisher>
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
                    .Where(
                       x => x.Name.Contains(SearchText) 
                    || x.Phone is not null && x.Phone.Contains(SearchText)
                    || x.Fax is not null && x.Fax.Contains(SearchText) 
                    || x.Email is not null && x.Email.Contains(SearchText) 
                    || x.Website is not null && x.Website.Contains(SearchText))
                    
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

        protected override Task ShowUpdate(Publisher model)
        {
            CloseEditor();
            Editor.ViewModel viewModel = new Editor.ViewModelUpdate(_mediator, _messenger, model);
            EditorView = new Editor.View(viewModel);
            return Task.CompletedTask;
        }

        protected override bool CanCreate()
        {
            return _appStateService.CurrentUser.HasPermission(Resource.PublishersResource, ActionModel.CreateAction) && base.CanCreate();
        }

        protected override bool CanDelete(Publisher model)
        {
            return base.CanDelete(model) && _appStateService.CurrentUser.HasPermission(Resource.PublishersResource, ActionModel.DeleteAction);
        }

        protected override bool CanUpdate(Publisher model)
        {
            return base.CanUpdate(model) && _appStateService.CurrentUser.HasPermission(Resource.PublishersResource, ActionModel.UpdateAction);
        }
        
        private readonly IServiceProvider _serviceProvider;
    }
}
