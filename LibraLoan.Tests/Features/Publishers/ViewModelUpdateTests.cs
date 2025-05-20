using FluentAssertions;
using LibraLoan.Features.Publishers.Editor;
using Microsoft.Extensions.DependencyInjection;

namespace LibraLoan.Tests.Features.Publishers
{
    public class ViewModelUpdateTests(ServicesConfiguration servicesConfiguration) : TestBase(servicesConfiguration)
    {
        [Fact]
        public void HasChanges_ShouldBeFalse_AfterCreation()
        {
            ViewModelUpdate viewModel = _services.Provider.GetRequiredService<ViewModelUpdate>();
            viewModel.HasChanges.Should().BeFalse();
        }

        [Fact]
        public void SaveCommand_ShouldCanExecute_WhenNameChanged()
        {
            ViewModelUpdate viewModel = _services.Provider.GetRequiredService<ViewModelUpdate>();

            viewModel.Name = "New Name";
            viewModel.HasChanges.Should().BeTrue();
            viewModel.HasErrors.Should().BeFalse();
            viewModel.SaveCommand.CanExecute(null).Should().BeTrue();
        }

        [Fact]
        public void SaveCommand_ShouldCanExecute_WhenPhoneChanged()
        {
            ViewModelUpdate viewModel = _services.Provider.GetRequiredService<ViewModelUpdate>();

            viewModel.Phone = "120";
            viewModel.HasChanges.Should().BeTrue();
            viewModel.HasErrors.Should().BeFalse();
            viewModel.SaveCommand.CanExecute(null).Should().BeTrue();
        }
    }
}
