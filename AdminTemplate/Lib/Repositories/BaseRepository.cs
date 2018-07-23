using Dapper;
using Lib.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Lib.Repositories
{
    internal class BaseRepository<T> where T : BaseEntity
    {
        private SqlConnection _con = null;

        protected SqlConnection CreateConnection()
        {
            if (_con == null && ConfigurationManager.ConnectionStrings.Count > 0)
            {
                _con = new SqlConnection(ConfigurationManager.ConnectionStrings[0].ToString());
            }
            return _con;
        }

        public T Insert(T entity)
        {
            return entity;
        }

        public T Update(T entity)
        {
            return entity;
        }

        public void Delete(T entity)
        {
        }

        public T GetById(int id)
        {
            return null;
        }

        public List<T> Get(int skip = 0, int take = 0, bool includeBigField = false)
        {
            return null;
        }

    }



}