namespace LibraLoan.Features.Management.RolesAndPermissions
{
    internal class ViewModel
    {
        public ViewModel(Roles.ViewModel rolesViewModel, Permissions.ViewModel permissionsViewModel)
        {
            RolesViewModel = rolesViewModel;
            PermissionsViewModel = permissionsViewModel;
        }

        public Roles.ViewModel RolesViewModel { get; }

        public Permissions.ViewModel PermissionsViewModel { get; }
    }
}
