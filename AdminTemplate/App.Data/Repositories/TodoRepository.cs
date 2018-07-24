using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using App.Domain.Entities;
using App.Domain.Filters;
using Dapper;

namespace App.Data.Repositories
{
    internal class TodoRepository : RepositoryBase, Domain.Repositories.ITodoRepository
    {
        public TodoRepository(IConfiguration config) : base(config)
        {
        }

        public Todo Create(Todo todo)
        {
            todo.Id = connection.QueryFirst<int>("exec todo_sp_create @Text, @IsCompleted", todo);
            return todo;
        }

        public bool Delete(int id)
        {
            var affectedRows = connection.Execute("exec todo_sp_delete @Id", new { Id = id });
            return affectedRows > 0;
        }

        public Todo GetById(int id)
        {
            var result = connection.QueryFirstOrDefault<Todo>("exec todo_sp_get @Id", new { Id = id });
            return result;
        }

        public IEnumerable<Todo> List(TodoFilter filter)
        {
            var result = connection.Query<Todo>("exec todo_sp_list @Id, @Text, @IsCompleted", filter);
            return result;
        }

        public bool Update(Todo todo)
        {
            var affectedRows = connection.Execute("exec todo_sp_update @Id, @Text, @IsCompleted", todo);
            return affectedRows > 0;
        }
    }
}
