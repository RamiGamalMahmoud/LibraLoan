using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LibraLoan.Core.Abstraction;
using LibraLoan.Core.Abstraction.Features.Auth.Login;
using LibraLoan.Core.Abstraction.Services;
using LibraLoan.Core.Models;
using LibraLoan.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraLoan.Features.Auth.Login
{
    internal partial class ViewModel : ObservableValidator, ILoginViewModel
    {
        public ViewModel(IAppDbContextFactory dbContextFactory, IPasswordHasher passwordHasher, IMessenger messenger, IAppStateService appStateService)
        {
            _dbContextFactory = dbContextFactory;
            _passwordHasher = passwordHasher;
            _messenger = messenger;
            AppStateService = appStateService;
            ValidateAllProperties();

#if DEBUG
            UserName = "admin";
            Password = "admin";
#endif
        }

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _userName;

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _password;

        [RelayCommand(CanExecute = nameof(CanSave))]
        private async Task Save()
        {
            using(AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                User user = await dbContext
                    .Users
                    .Include(user => user.Role)
                        .ThenInclude(role => role.Permissions)
                        .ThenInclude(permission => permission.Resource)
                    .Include(user => user.Role)
                        .ThenInclude(role => role.Permissions)
                        .ThenInclude(permission => permission.Action)
                    .Where(user => user.IsActive)
                    .FirstOrDefaultAsync(x => x.Username == UserName);
                if(user is null)
                {
                    _messenger.Send(new Core.Messages.LoginFailedMessage());
                    return;
                }
                string hashedPassword = dbContext.Entry(user).Property("Password").CurrentValue.ToString();
                
                bool isValid = _passwordHasher.VerifyHashedPassword(Password, hashedPassword);
                if(!isValid)
                {
                    _messenger.Send(new Core.Messages.LoginFailedMessage());
                    return;
                }

                _messenger.Send(new Core.Messages.UserLoggedInMessage(user));
            }
        }

        private bool CanSave => !HasErrors;

        public IAppStateService AppStateService { get; }

        private readonly IAppDbContextFactory _dbContextFactory;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMessenger _messenger;
    }
}
