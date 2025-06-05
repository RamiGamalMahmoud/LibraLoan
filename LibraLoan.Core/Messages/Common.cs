using CommunityToolkit.Mvvm.Messaging.Messages;
using System;

namespace LibraLoan.Core.Messages
{
    public static class Common
    {
        public record SuccessMessage(string Message);
        public record ErrorMessage(string Message);

        public record ShowDialogMessage(Type Type);
        public record LogoutMessage();

        public class ConfigrRequestMessge(string message = "هل تريد بالتأكيد حفظ التعديلات؟") : RequestMessage<bool>
        {
            public string Message => message;
        }

        public record CloseEditor<TModel>;
    }
}
