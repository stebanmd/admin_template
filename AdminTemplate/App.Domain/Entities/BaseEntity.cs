using App.Domain.Attributes;
using System;

namespace App.Domain.Entities
{
    public abstract class BaseEntity
    {
        [FieldToSql(FieldType.PrimaryKey)]
        public int Id { get; set; }

        [FieldToSql(FieldType.Unchangeble)]
        public DateTime CreatedAt { get; set; }

        [FieldToSql(FieldType.Unchangeble)]
        public string CreatedBy { get; set; }

        [FieldToSql]
        public DateTime ModifiedAt { get; set; }

        [FieldToSql]
        public string ModifiedBy { get; set; }
    }
}