using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using MediatR;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using static LibraLoan.Core.Commands.Common;

namespace LibraLoan.Features.Books.Editor
{
    internal abstract partial class ViewModel : EditorViewModelBase
    {
        protected ViewModel(IMediator mediator, IMessenger messenger) : base()
        {
            _mediator = mediator;
            _messenger = messenger;
            _notifyPropertiesNames = [
                nameof(BookTitle),
                nameof(ISBN),
                nameof(PublishDate),
                nameof(Version),
                nameof(Copies),
                nameof(Photo),
                nameof(SelectedPublisher)
                ];
        }

        [ObservableProperty]
        [Required(ErrorMessage = "يجب ادخال عنوان الكتاب")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _bookTitle;


        [ObservableProperty]
        [Required(ErrorMessage = "يجب ادخال رقم الكتاب")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _iSBN;

        [ObservableProperty]
        [Required(ErrorMessage = "يجب ادخال تاريخ النشر")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private DateTime? _publishDate;

        [ObservableProperty]
        [Required(ErrorMessage = "يجب ادخال رقم الإصدار")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private int? _version = 1;

        [ObservableProperty]
        [Required(ErrorMessage = "يجب ادخال عدد النسخ")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private int? _copies = 1;

        [ObservableProperty]
        [Required(ErrorMessage = "يجب اختيار دار النشر")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private Publisher _selectedPublisher;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private byte[] _photo;

        [ObservableProperty]
        private IEnumerable<Publisher> _publishers;

        [ObservableProperty]
        private IEnumerable<SelectableObject<Author>> _authors = new List<SelectableObject<Author>>();

        [RelayCommand]
        private void SelectBookImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".png";
            openFileDialog.Filter = "Image Files|*.jpg;*.bmp;*.png;*.jpeg;*.gif";
            bool? result = openFileDialog.ShowDialog();
            if (result is true)
            {
                Photo = System.IO.File.ReadAllBytes(openFileDialog.FileName);
            }
        }

        [RelayCommand]
        protected virtual async Task LoadDataAsync()
        {
            Publishers = await _mediator.Send(new GetAllCommand<Publisher>());
            IEnumerable<Author> authors = await _mediator.Send(new GetAllCommand<Author>());
            Authors = authors.Select(x => new SelectableObject<Author>(x)).ToList();
        }

        [RelayCommand]
        private void UpdateAuthorsSelection()
        {
            HasChanges = true;
            SaveCommand.NotifyCanExecuteChanged();
        }

        public override bool CanSave => base.CanSave && HasAuthors;

        public IEnumerable<Author> SelectedAuthors => Authors.Where(x => x.IsSelected).Select(x => x.Value);
        public int SelectedAuthorsCount => SelectedAuthors.Count();

        public bool HasAuthors => SelectedAuthorsCount > 0;

        protected readonly IMediator _mediator;
        protected readonly IMessenger _messenger;
    }
}
