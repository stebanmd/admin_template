using App.Domain.Entities;
using App.Domain.Filters;
using System.Collections.Generic;

namespace App.Domain.Repositories
{
    public interface ITodoRepository
    {
        Todo Create(Todo todo);

        IEnumerable<Todo> List(TodoFilter filter);

        Todo GetById(int id);

        bool Update(Todo todo);

        bool Delete(int id);
    }
}