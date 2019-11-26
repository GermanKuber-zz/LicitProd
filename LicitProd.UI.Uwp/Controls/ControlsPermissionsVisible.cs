using System;
using LicitProd.Seguridad;

namespace LicitProd.UI.Uwp.Controls
{
    public class ControlsPermissionsVisible
    {
        private readonly Action _doesNotHavePermissionCallback;
        private string _permission;

        public ControlsPermissionsVisible(Action doesNotHavePermissionCallback)
        {
            _doesNotHavePermissionCallback = doesNotHavePermissionCallback;
        }

        public string Permission
        {
            get => _permission;
            set
            {
                if (string.IsNullOrWhiteSpace(_permission))
                {
                    IdentityServices.Instance.IsLoggued()
                        .Success(x =>
                        {
                            x.Rol.HasAccess(value)
                                .Error(e => _doesNotHavePermissionCallback());
                        });
                }
                _permission = value;
            }
        }
    }
}