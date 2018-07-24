using App.Domain.Attributes;
using System.Collections.Generic;

namespace App.Domain.Entities
{
    public class Profile : BaseEntity
    {
        [FieldToSql]
        public string Name { get; set; }

        [FieldToSql]
        public bool Enabled { get; set; }

        public IEnumerable<MenuAccess> ProfileMenuAccess { get; set; }
    }
}