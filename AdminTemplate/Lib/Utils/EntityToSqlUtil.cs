using Lib.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib.Utils
{
    internal abstract class EntityToSqlUtil
    {
        public static string GetInsertQuery(object entity)
        {
            string retorno = string.Format("INSERT INTO {0} (colunas) OUTPUT Inserted.PK VALUES (valores);", entity.GetType().Name);
            List<string> colunas = new List<string>();
            List<string> valores = new List<string>();

            foreach (var item in entity.GetType().GetProperties())
            {
                object[] atr = item.GetCustomAttributes(typeof(FieldToSqlAttribute), true);
                if (atr.Length > 0)
                {
                    if ((atr[0] as FieldToSqlAttribute)._columnType != FieldToSqlAttribute.FieldType.PrimaryKey)
                    {
                        colunas.Add(item.Name);
                    }

                    if (item.PropertyType.Name.Equals("DateTime"))
                    {
                        if ((DateTime)entity.GetPropByName(item.Name) == DateTime.MinValue)
                        {
                            valores.Add(string.Format("{0}", "NULL"));
                        }
                        else
                        {
                            valores.Add(string.Format("@{0}", item.Name));
                        }
                    }
                    else if ((atr[0] as FieldToSqlAttribute)._columnType == FieldToSqlAttribute.FieldType.ForeignKey)
                    {
                        valores.Add(string.Format("IIF(@{0} > 0, @{0}, NULL)", item.Name));
                    }
                    else if ((atr[0] as FieldToSqlAttribute)._columnType == FieldToSqlAttribute.FieldType.PrimaryKey)
                    {
                        retorno = retorno.Replace("PK", item.Name);
                    }
                    else
                    {
                        valores.Add(string.Format("@{0}", item.Name));
                    }
                }
            }
            retorno = retorno.Replace("colunas", string.Join(", ", colunas));
            retorno = retorno.Replace("valores", string.Join(", ", valores));

            return retorno;
        }

        public static string GetUpdateQuery(object entity)
        {
            string retorno = string.Format("UPDATE {0} SET colunas WHERE PK = @PK;", entity.GetType().Name);
            List<string> colunas = new List<string>();

            foreach (var item in entity.GetType().GetProperties())
            {
                object[] atr = item.GetCustomAttributes(typeof(FieldToSqlAttribute), true);
                if (atr.Length > 0)
                {
                    if ((atr[0] as FieldToSqlAttribute)._columnType == FieldToSqlAttribute.FieldType.PrimaryKey)
                    {
                        retorno = retorno.Replace("PK", item.Name);
                    }
                    else if ((atr[0] as FieldToSqlAttribute)._columnType == FieldToSqlAttribute.FieldType.Unchangeble)
                    {
                        continue;
                    }
                    else if (item.PropertyType.Name.Equals("DateTime") || (item.PropertyType == typeof(DateTime?) && ((DateTime?)entity.GetPropByName(item.Name)).HasValue))
                    {
                        if ((DateTime)entity.GetPropByName(item.Name) > DateTime.MinValue)
                        {
                            colunas.Add(string.Format("{0} = @{0}", item.Name));
                        }
                    }
                    else if ((atr[0] as FieldToSqlAttribute)._columnType == FieldToSqlAttribute.FieldType.ForeignKey)
                    {
                        colunas.Add(string.Format("{0} = IIF(@{0} > 0, @{0}, NULL)", item.Name));
                    }
                    else
                    {
                        colunas.Add(string.Format("{0} = @{0}", item.Name));
                    }
                }
            }
            retorno = retorno.Replace("colunas", string.Join(", ", colunas));

            return retorno;
        }

        private static string GetSelectByIdQuery(Type entity)
        {
            string retorno = string.Format("SELECT colunas FROM {0} (NOLOCK) WHERE PK = @PK;", entity.Name);

            foreach (var item in entity.GetProperties())
            {
                object[] atr = item.GetCustomAttributes(typeof(FieldToSqlAttribute), true);
                if (atr.Length > 0)
                {
                    if ((atr[0] as FieldToSqlAttribute)._columnType == FieldToSqlAttribute.FieldType.PrimaryKey)
                    {
                        retorno = retorno.Replace("PK", item.Name);
                        break;
                    }
                }
            }
            retorno = retorno.Replace("colunas", GetColumns(entity));
            return retorno;
        }

        public static string GetSelectAllQuery<T>(int skip, int take, bool includeBase64Field)
        {
            var entity = typeof(T);
            var retorno = string.Format("SELECT colunas FROM {0} (NOLOCK)", entity.Name);
            return retorno.Replace("colunas", GetColumns(entity, includeBase64Field: includeBase64Field));
        }

        public static string GetSelectByIdQuery<T>()
        {
            return GetSelectByIdQuery(typeof(T));
        }

        public static string GetDeleteQuery<T>()
        {
            Type entityObj = typeof(T);

            string retorno;
            if (entityObj.GetProperty("Enabled") != null)
            {
                string columnsToModify = "Enabled = 0";

                if (entityObj.GetProperty("ModifiedAt") != null) columnsToModify += ", ModifiedAt = @ModifiedAt";
                if (entityObj.GetProperty("ModifiedBy") != null) columnsToModify += ", ModifiedBy = @ModifiedBy";

                retorno = $"UPDATE {entityObj.Name} SET {columnsToModify} where PK = @PK;";
            }
            else
            {
                retorno = $"DELETE FROM {entityObj.Name} WHERE PK = @PK;";
            }
             
            foreach (var item in entityObj.GetProperties())
            {
                object[] atr = item.GetCustomAttributes(typeof(FieldToSqlAttribute), true);
                if (atr.Length > 0)
                {
                    if ((atr[0] as FieldToSqlAttribute)._columnType == FieldToSqlAttribute.FieldType.PrimaryKey)
                    {
                        retorno = retorno.Replace("PK", item.Name);
                        break;
                    }
                }
            }
            return retorno;
        }

        private static string GetColumns(Type entity, bool includePrimaryKey = true, bool includeBase64Field = true, string alias = "")
        {
            List<string> colunas = new List<string>();
            string tbl = alias;
            if (!string.IsNullOrEmpty(tbl)) tbl = tbl + ".";

            foreach (var item in entity.GetProperties())
            {
                object[] atr = item.GetCustomAttributes(typeof(FieldToSqlAttribute), true);
                if (atr.Length > 0)
                {
                    if ((atr[0] as FieldToSqlAttribute)._columnType == FieldToSqlAttribute.FieldType.PrimaryKey)
                    {
                        if (includePrimaryKey)
                            colunas.Add(tbl + item.Name);
                    }
                    else if ((atr[0] as FieldToSqlAttribute)._columnType == FieldToSqlAttribute.FieldType.Base64Field)
                    {
                        if (includeBase64Field)
                            colunas.Add(tbl + item.Name);
                    }
                    else
                    {
                        colunas.Add(tbl + item.Name);
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
