using App.Domain.Attributes;

namespace App.Domain.Entities
{
    public class AdminUser : BaseEntity
    {
        [FieldToSql(FieldType.ForeignKey)]
        public int ProfileId { get; set; }

        [FieldToSql]
        public string Name { get; set; }

        [FieldToSql]
        public string Email { get; set; }

        [FieldToSql(FieldType.Base64Field)]
        public string Photo { get; set; }

        [FieldToSql]
        public bool Enabled { get; set; }

        [FieldToSql]
        public string Password { get; set; }

        public virtual Profile Profile { get; set; }
    }
}