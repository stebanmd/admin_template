using App.Domain.Entities;
using App.Domain.Filters;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace App.Data.Repositories
{
    internal class RepositoryBase<T> where T : BaseEntity
    {
        private const string CONNECTIONSTRING_KEY = "ConnectionString:DefaultConnection";

        protected SqlConnection connection;

        public RepositoryBase(IConfiguration config)
        {
            var connectionString = config.GetSection(CONNECTIONSTRING_KEY);

            if (string.IsNullOrEmpty(connectionString.Value))
                throw new ArgumentNullException("Connection string not found");

            connection = new SqlConnection(connectionString.Value);
        }

        #region IBaseRepository

        public virtual T Create(T entity)
        {
            using (connection)
            {
                string query = EntityToSqlUtil.GetInsertQuery(entity);
                entity.Id = connection.QueryFirst<int>(query, entity);
                connection.Close();
            }

            return entity;
        }

        public virtual bool Update(T entity)
        {
            string query = EntityToSqlUtil.GetUpdateQuery(entity);
            int affected = 0;
            using (connection)
            {
                connection.Open();
                affected = connection.Execute(query, entity);
                connection.Close();
            }
            return affected > 0;
        }

        public virtual bool Delete(T entity)
        {
            string query = EntityToSqlUtil.GetDeleteQuery<T>();
            int affected = 0;
            using (connection)
            {
                connection.Open();
                affected = connection.Execute(query, entity);
                connection.Close();
            }
            return affected > 0;
        }

        public virtual T GetById(int id)
        {
            string query = EntityToSqlUtil.GetSelectByIdQuery<T>();
            T result;
            using (connection)
            {
                connection.Open();
                result = connection.QueryFirstOrDefault<T>(query, new { Id = id });
                connection.Close();
            }
            return result;
        }

        public virtual IEnumerable<T> List(BaseFilter filter)
        {
            string sql = EntityToSqlUtil.GetSelectAllQuery<T>(filter.IncludeBase64Field);
            IEnumerable<T> result;
            using (connection)
            {
                connection.Open();
                result = connection.Query<T>(sql, new { filter });
                connection.Close();
            }
            return result;
        }

        #endregion IBaseRepository
    }
}