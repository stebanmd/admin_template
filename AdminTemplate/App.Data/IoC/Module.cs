using App.Domain.Entities;
using System;
using System.Collections.Generic;

namespace App.Data.IoC
{
    public static class Module
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dic = new Dictionary<Type, Type>();
            dic.Add(typeof(Domain.Repositories.IBaseRepository<Todo>), typeof(Repositories.TodoRepository));
            dic.Add(typeof(Domain.Repositories.IBaseRepository<Profile>), typeof(Repositories.TodoRepository));
            dic.Add(typeof(Domain.Repositories.IAdminUserRepository), typeof(Repositories.AdminUserRepository));

            return dic;
        }
    }
}