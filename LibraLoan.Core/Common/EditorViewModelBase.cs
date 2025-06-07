using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace LibraLoan.Core.Common
{
    public abstract partial class EditorViewModelBase<TModel> : ObservableValidator
    {
        protected EditorViewModelBase()
        {
            ValidateAllProperties();
        }
        public virtual bool CanSave => HasChanges && !HasErrors;
        public abstract string Title { get; }

        protected List<string> _notifyPropertiesNames = new List<string>();

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if(_notifyPropertiesNames.Contains(e.PropertyName))
            {
                HasChanges = true;
            }
        }

        [RelayCommand]
        private void CloseEditor()
        {
            WeakReferenceMessenger.Default.Send(new Core.Messages.Common.CloseEditor<TModel>());
        }

        [RelayCommand(CanExecute = nameof(CanSave))]
        protected abstract Task Save();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private bool _hasChanges;

    }
}
