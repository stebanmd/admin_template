using Lib.Utils.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lib.Utils.Attributes
{
    public class MenuAdminAttribute : DescriptionAttribute
    {
        public string Icon { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public MenuAdminGroup Group { get; set; }

        public MenuAdminAttribute()
            : base()
        {
        }

        public MenuAdminAttribute(string description)
            : base(description)
        {
        }

        public MenuAdminAttribute(string description, MenuAdminGroup group)
            : base(description)
        {
            Group = group;
        }
    }
}
