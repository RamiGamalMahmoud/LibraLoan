using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
//using System;
using System.Linq;
using System.Threading.Tasks;

namespace LibraLoan.Features.Authors.Listing
{
    internal partial class ViewModel : ListingViewModelBase<Author>
    {
        private readonly System.IServiceProvider _serviceProvider;

        public ViewModel(IMediator mediator, IMessenger messenger, IAppStateService appStateService, System.IServiceProvider serviceProvider) : base(mediator, messenger, appStateService)
        {
            _serviceProvider = serviceProvider;
            _messenger.Register<Core.Messages.Common.CloseEditor<Author>>(this, (r, m) =>
            {
                EditorView = null;
            });
        }

        protected override Task SearchAsync()
        {
            Models = _tempModels?.Where(x => x.Name.Contains(SearchText));

            return Task.CompletedTask;
        }

        protected override Task ShowCreate()
        {
            CloseEditor();
            Editor.ViewModelCreate viewModel = _serviceProvider.GetRequiredService<Editor.ViewModelCreate>();
            EditorView = new Editor.View(viewModel);
            return Task.CompletedTask;
        }

        protected override Task ShowUpdate(Author model)
        {
            CloseEditor();
            Editor.ViewModel viewModel = new Editor.ViewModelUpdate(_mediator, _messenger, model);
            EditorView = new Editor.View(viewModel);
            return Task.CompletedTask;
        }

        protected override bool CanCreate()
        {
            return _appStateService.CurrentUser.HasPermission(Resource.AuthorsResource, Action.CreateAction) && base.CanCreate();
        }

        protected override bool CanDelete(Author model)
        {
            return base.CanDelete(model) && _appStateService.CurrentUser.HasPermission(Resource.AuthorsResource, Action.DeleteAction);
        }

        protected override bool CanUpdate(Author model)
        {
            return base.CanUpdate(model) && _appStateService.CurrentUser.HasPermission(Resource.AuthorsResource, Action.UpdateAction);
        }
    }
}
