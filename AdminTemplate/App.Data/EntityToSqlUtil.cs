using App.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Data
{
    internal abstract class EntityToSqlUtil
    {
        public static string GetInsertQuery(object entity)
        {
            var type = entity.GetType();
            string result = $"INSERT INTO {type.Name} (|colunas|) OUTPUT Inserted.|PK| VALUES (|valores|);";
            List<string> columnList = new List<string>();
            List<string> valuesList = new List<string>();


            foreach (var item in type.GetProperties())
            {
                object[] atr = item.GetCustomAttributes(typeof(FieldToSqlAttribute), true);
                if (atr.Length > 0)
                {
                    if ((atr[0] as FieldToSqlAttribute).ColumnType != FieldType.PrimaryKey)
                    {
                        columnList.Add(item.Name);
                    }

                    if (item.PropertyType.Name.Equals("DateTime"))
                    {
                        if ((DateTime)item.GetValue(entity) == DateTime.MinValue)
                        {
                            valuesList.Add(string.Format("{0}", "NULL"));
                        }
                        else
                        {
                            valuesList.Add(string.Format("@{0}", item.Name));
                        }
                    }
                    else if ((atr[0] as FieldToSqlAttribute).ColumnType == FieldType.ForeignKey)
                    {
                        valuesList.Add(string.Format("IIF(@{0} > 0, @{0}, NULL)", item.Name));
                    }
                    else if ((atr[0] as FieldToSqlAttribute).ColumnType == FieldType.PrimaryKey)
                    {
                        result = result.Replace("|PK|", item.Name);
                    }
                    else
                    {
                        valuesList.Add(string.Format("@{0}", item.Name));
                    }
                }
            }
            result = result.Replace("|colunas|", string.Join(", ", columnList));
            result = result.Replace("|valores|", string.Join(", ", valuesList));

            return result;
        }

        public static string GetUpdateQuery(object entity)
        {
            string result = string.Format("UPDATE {0} SET |colunas| WHERE |PK| = @PK;", entity.GetType().Name);
            List<string> columnList = new List<string>();

            foreach (var item in entity.GetType().GetProperties())
            {
                object[] atr = item.GetCustomAttributes(typeof(FieldToSqlAttribute), true);
                if (atr.Length > 0)
                {
                    if ((atr[0] as FieldToSqlAttribute).ColumnType == FieldType.PrimaryKey)
                    {
                        result = result.Replace("|PK|", item.Name);
                    }
                    else if ((atr[0] as FieldToSqlAttribute).ColumnType == FieldType.Unchangeble)
                    {
                        continue;
                    }
                    else if (item.PropertyType.Name.Equals("DateTime") || (item.PropertyType == typeof(DateTime?) && ((DateTime?)item.GetValue(entity)).HasValue))
                    {
                        if ((DateTime)item.GetValue(entity) > DateTime.MinValue)
                        {
                            columnList.Add(string.Format("{0} = @{0}", item.Name));
                        }
                    }
                    else if ((atr[0] as FieldToSqlAttribute).ColumnType == FieldType.ForeignKey)
                    {
                        columnList.Add(string.Format("{0} = IIF(@{0} > 0, @{0}, NULL)", item.Name));
                    }
                    else
                    {
                        columnList.Add(string.Format("{0} = @{0}", item.Name));
                    }
                }
            }
            result = result.Replace("|colunas|", string.Join(", ", columnList));

            return result;
        }

        private static string GetSelectByIdQuery(Type entity)
        {
            string retorno = $"SELECT |colunas| FROM {entity.Name} (NOLOCK) WHERE |PK| = @PK";

            foreach (var item in entity.GetProperties())
            {
                object[] atr = item.GetCustomAttributes(typeof(FieldToSqlAttribute), true);
                if (atr.Length > 0)
                {
                    if ((atr[0] as FieldToSqlAttribute).ColumnType == FieldType.PrimaryKey)
                    {
                        retorno = retorno.Replace("|PK|", item.Name);
                        break;
                    }
                }
            }
            retorno = retorno.Replace("|colunas|", GetColumns(entity));
            return retorno;
        }

        public static string GetSelectAllQuery<T>(bool includeBase64Field)
        {
            var entity = typeof(T);
            var retorno = $"SELECT |colunas| FROM {entity.Name} (NOLOCK)";
            return retorno.Replace("|colunas|", GetColumns(entity, includeBase64Field: includeBase64Field));
        }

        public static string GetSelectByIdQuery<T>()
        {
            return GetSelectByIdQuery(typeof(T));
        }

        public static string GetDeleteQuery<T>()
        {
            Type entityObj = typeof(T);

            string result;
            if (entityObj.GetProperty("Enabled") != null)
            {
                string columnsToModify = "Enabled = 0";

                if (entityObj.GetProperty("ModifiedAt") != null) columnsToModify += ", ModifiedAt = @ModifiedAt";
                if (entityObj.GetProperty("ModifiedBy") != null) columnsToModify += ", ModifiedBy = @ModifiedBy";

                result = $"UPDATE {entityObj.Name} SET {columnsToModify} where |PK| = @PK;";
            }
            else
            {
                result = $"DELETE FROM {entityObj.Name} WHERE |PK| = @PK;";
            }

            foreach (var item in entityObj.GetProperties())
            {
                object[] atr = item.GetCustomAttributes(typeof(FieldToSqlAttribute), true);
                if (atr.Length > 0)
                {
                    if ((atr[0] as FieldToSqlAttribute).ColumnType == FieldType.PrimaryKey)
                    {
                        result = result.Replace("|PK|", item.Name);
                        break;
                    }
                }
            }
            return result;
        }

        private static string GetColumns(Type entity, bool includePrimaryKey = true, bool includeBase64Field = true, string alias = "")
        {
            List<string> colunas = new List<string>();
            if (!string.IsNullOrEmpty(alias)) alias = alias + ".";

            foreach (var item in entity.GetProperties())
            {
                object[] atr = item.GetCustomAttributes(typeof(FieldToSqlAttribute), true);
                if (atr.Length > 0)
                {
                    if ((atr[0] as FieldToSqlAttribute).ColumnType == FieldType.PrimaryKey)
                    {
                        if (includePrimaryKey)
                            colunas.Add(alias + item.Name);
                    }
                    else if ((atr[0] as FieldToSqlAttribute).ColumnType == FieldType.Base64Field)
                    {
                        if (includeBase64Field)
                            colunas.Add(alias + item.Name);
                    }
                    else
                    {
                        colunas.Add(alias + item.Name);
                    }
                }
            }
            return string.Join(", ", colunas);
        }

        public static string GetColumns<T>(bool includePrimaryKey = true, bool includeBase64Field = true, string alias = "")
        {
            return GetColumns(typeof(T), includePrimaryKey, includeBase64Field, alias);
        }
    }
}
