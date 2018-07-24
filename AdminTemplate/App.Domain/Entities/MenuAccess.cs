using App.Domain.Attributes;

namespace App.Domain.Entities
{
    public class MenuAccess : BaseEntity
    {
        [FieldToSql(FieldType.ForeignKey)]
        public int ProfileId { get; set; }

        [FieldToSql]
        public string Menu { get; set; }

        [FieldToSql]
        public bool Allowed { get; set; }
    }
}