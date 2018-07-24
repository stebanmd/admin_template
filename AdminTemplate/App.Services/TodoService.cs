using System.Collections.Generic;
using App.Services.Dtos;
using App.Services.Extensions;
using App.Domain.Entities;
using App.Domain.Filters;
using App.Domain.Repositories;

namespace App.Services
{
    internal class TodoService : Interfaces.ITodoService
    {
        private readonly ITodoRepository repository;

        public TodoService(ITodoRepository rep)
        {
            this.repository = rep;
        }

        public TodoDto Create(TodoDto todo)
        {
            return repository.Create(todo.MapTo<Todo>()).MapTo<TodoDto>();
        }

        public bool Delete(int id)
        {
            return repository.Delete(id);
        }

        public TodoDto GetById(int id)
        {
            return repository.GetById(id).MapTo<TodoDto>();
        }

        public IEnumerable<TodoDto> List(TodoFilterDto filter)
        {
            return repository.List(filter.MapTo<TodoFilter>()).EnumerableTo<TodoDto>();
        }

        public bool Update(TodoDto todo)
        {
            return repository.Update(todo.MapTo<Todo>());
        }
    }
}