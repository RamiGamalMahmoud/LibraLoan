using FluentAssertions;
using LibraLoan.Features.Management.Users.Editor;
using Microsoft.Extensions.DependencyInjection;

namespace LibraLoan.Tests.Features.Management.Users
{
    public class EditorViewModelTests(ServicesConfiguration servicesConfiguration) : TestBase(servicesConfiguration)
    {
        [Fact]
        public void UpdateViewModelHasChanges_ShouldBeFalse_AfterInitialization()
        {
            ViewModelUpdate viewModel = _services.Provider.GetRequiredService<ViewModelUpdate>();

            viewModel.HasChanges.Should().BeFalse();
        }

        [Fact]
        public void UpdateViewModelHasChanges_ShouldBeTrue_WhenIsActiveChanged()
        {
            ViewModelUpdate viewModel = _services.Provider.GetRequiredService<ViewModelUpdate>();

            viewModel.IsActive = false;

            viewModel.HasChanges.Should().BeTrue();
        }

        [Fact]
        public void UpdateViewModelCanSave_ShouldBeTrue_WhenIsActiveChanged()
        {
            ViewModelUpdate viewModel = _services.Provider.GetRequiredService<ViewModelUpdate>();

            viewModel.IsActive = false;

            var errors = viewModel.GetErrors();
            viewModel.HasChanges.Should().BeTrue();
            viewModel.HasErrors.Should().BeFalse();
            viewModel.SaveCommand.CanExecute(null).Should().BeTrue();
            viewModel.CanSave.Should().BeTrue();
        }
    }
}
