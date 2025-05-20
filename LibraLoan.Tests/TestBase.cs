namespace LibraLoan.Tests
{
    public class TestBase : IClassFixture<ServicesConfiguration>
    {
        protected readonly ServicesConfiguration _services;

        protected TestBase(ServicesConfiguration services)
        {
            _services = services;
        }
    }
}
