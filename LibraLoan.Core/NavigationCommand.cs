using System;

namespace LibraLoan.Core
{
    public record NavigationCommand(string Name, string Icon = null, Action Action = null, bool IsEnabled = true);
}
