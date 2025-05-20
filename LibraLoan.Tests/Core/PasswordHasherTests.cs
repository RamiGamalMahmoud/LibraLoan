using FluentAssertions;
using LibraLoan.Core.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace LibraLoan.Tests.Core
{
    public class PasswordHasherTests(ServicesConfiguration servicesConfiguration) : TestBase(servicesConfiguration)
    {
        [Fact]
        public void CanVerifyPassword()
        {
            string password = "admin";
            string hashedPassword = "$2a$11$Sk2NHXsX3PrWVTrgfmEfa.xw2ViWvadOEFR1leLTq2jSUzu4ak5.2";

            IPasswordHasher passwordHasher = _services.Provider.GetRequiredService<IPasswordHasher>();
            
            passwordHasher.VerifyHashedPassword(password, hashedPassword).Should().BeTrue();
        }
    }
}
