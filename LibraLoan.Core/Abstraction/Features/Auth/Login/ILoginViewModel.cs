namespace LibraLoan.Core.Abstraction.Features.Auth.Login
{
    public interface ILoginViewModel
    {
        string UserName { get; set; }
        string Password { get; set; }
    }
}
