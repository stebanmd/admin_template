using Lib.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib.Utils.Enums
{
    public enum MenuAdminItem
    {
        [MenuAdmin("Usuários do Admin", MenuAdminGroup.Admin)]
        AdminUser,

        [MenuAdmin("Perfis", MenuAdminGroup.Admin)]
        Profile
    }
}
