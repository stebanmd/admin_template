using App.Domain.Entities;
using App.Domain.Filters;
using System.Collections.Generic;

namespace App.Domain.Repositories
{
    public interface IBaseRepository<T> where T: BaseEntity
    {
        T Create(T entity);

        IEnumerable<T> List(BaseFilter filter);

        T GetById(int id);

        bool Update(T entity);

        bool Delete(T entity);
    }
}