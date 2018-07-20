using Lib.Entities;
using System;
using System.Collections.Generic;

namespace Lib.Repositories
{
    internal class BaseRepository<T> where T : BaseEntity
    {
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