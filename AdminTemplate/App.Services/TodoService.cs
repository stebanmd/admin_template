using System.Collections.Generic;
using App.Services.Dtos;
using App.Domain.Entities;
using App.Domain.Filters;
using App.Domain.Repositories;

namespace App.Services
{
    internal class TodoService : Interfaces.ITodoService
    {
        private readonly IBaseRepository<Todo> repository;

        public TodoService(IBaseRepository<Todo> rep)
        {
            this.repository = rep;
        }

        public TodoDto Create(TodoDto dto)
        {
            return repository.Create(dto.MapTo<Todo>()).MapTo<TodoDto>();
        }

        public bool Delete(TodoDto dto)
        {
            return repository.Delete(dto.MapTo<Todo>());
        }

        public TodoDto GetById(int id)
        {
            return repository.GetById(id).MapTo<TodoDto>();
        }

        public IEnumerable<TodoDto> List(TodoFilterDto filter)
        {
            return repository.List(new BaseFilter()).EnumerableTo<TodoDto>();
        }

        public bool Update(TodoDto todo)
        {
            return repository.Update(todo.MapTo<Todo>());
        }
    }
}