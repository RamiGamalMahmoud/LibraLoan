using CommunityToolkit.Mvvm.ComponentModel;
using LibraLoan.Core.Common;
using LibraLoan.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace LibraLoan.Features.Management.RolesAndPermissions.Roles.Editor
{
    internal partial class PermissionsGroup : ObservableObject
    {
        public string Resource { get; }
        public IEnumerable<SelectableObject<Permission>> Permissions { get; }

        [ObservableProperty]
        private bool _isSelectedAll;

        public PermissionsGroup(string resource, IEnumerable<SelectableObject<Permission>> permissions)
        {
            Resource = resource;
            Permissions = permissions;
            IsSelectedAll = permissions.All(p => p.IsSelected);
        }

        partial void OnIsSelectedAllChanged(bool oldValue, bool newValue)
        {
            foreach (SelectableObject<Permission> permission in Permissions)
            {
                permission.IsSelected = newValue;
            }
        }
    }
}
