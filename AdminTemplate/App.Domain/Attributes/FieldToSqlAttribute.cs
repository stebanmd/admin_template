using System;

namespace App.Domain.Attributes
{
    public class FieldToSqlAttribute : Attribute
    {
        public FieldType ColumnType { get; private set; }

        public FieldToSqlAttribute()
        {
            this.ColumnType = FieldType.Normal;
        }

        public FieldToSqlAttribute(FieldType fieldType)
        {
            this.ColumnType = fieldType;
        }
    }

    public enum FieldType
    {
        Normal,
        PrimaryKey,
        ForeignKey,
        Unchangeble,
        Base64Field
    }
}