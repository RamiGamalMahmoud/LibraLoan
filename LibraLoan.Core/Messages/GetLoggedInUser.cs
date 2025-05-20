using CommunityToolkit.Mvvm.Messaging.Messages;
using LibraLoan.Core.Models;

namespace LibraLoan.Core.Messages
{
    public class GetLoggedInUser : RequestMessage<User>;
}
