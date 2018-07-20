using System;
using System.Collections.Generic;
using System.Text;

namespace Lib.Utils.Attributes
{
    public class FieldToSqlAttribute: Attribute
    {
        public FieldType _columnType { get; private set; }

        public enum FieldType
        {
            Normal,
            PrimaryKey,
            ForeignKey,
            Unchangeble,
            Base64Field
        }

        public FieldToSqlAttribute()
        {
            this._columnType = FieldType.Normal;
        }

        public FieldToSqlAttribute(FieldType fieldType)
        {
            this._columnType= fieldType;
        }
    }
}
