using Dapper;
using Lib.Entities;
using Lib.Utils;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Lib.Repositories
{
    internal class BaseRepository<T> where T : BaseEntity
    {
        private SqlConnection _con = null;

        protected SqlConnection CreateConnection()
        {
            if (_con == null)
            {
                _con = new SqlConnection();
            }
            return _con;
        }

        public T Insert(T entity)
        {
            string sql = EntityToSqlUtil.GetInsertQuery(entity);
            using (var con = CreateConnection())
            {
                con.Open();
                entity.Id = con.QueryFirst<int>(sql, entity);
                con.Close();
            }
            return entity;
        }

        public bool Update(T entity)
        {
            string sql = EntityToSqlUtil.GetUpdateQuery(entity);
            int affected = 0;
            using (var con = CreateConnection())
            {
                con.Open();
                affected = con.Execute(sql, entity);
                con.Close();
            }
            return affected > 0;
        }

        public bool Delete(T entity)
        {
            string sql = EntityToSqlUtil.GetDeleteQuery<T>();
            int affected = 0;
            using (var con = CreateConnection())
            {
                con.Open();
                affected = con.Execute(sql, entity);
                con.Close();
            }
            return affected > 0;
        }

        public T GetById(int id)
        {
            string sql = EntityToSqlUtil.GetSelectByIdQuery<T>();
            T result;
            using (var con = CreateConnection())
            {
                con.Open();
                result = con.QueryFirstOrDefault<T>(sql, new { Id = id });
                con.Close();
            }
            return result;
        }

        public IEnumerable<T> Get(int skip = 0, int take = -1, bool includeBase64Field = false)
        {
            string sql = EntityToSqlUtil.GetSelectAllQuery<T>(skip, take, includeBase64Field);
            IEnumerable<T> result;
            using (var con = CreateConnection())
            {
                con.Open();
                result = con.Query<T>(sql, new { Skip = skip, Take = take });
                con.Close();
            }
            return result;
        }
    }
}