using System;
using System.Collections.Generic;

namespace App.Data.IoC
{
    public static class Module
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dic = new Dictionary<Type, Type>();
            dic.Add(typeof(Domain.Repositories.ITodoRepository), typeof(Repositories.TodoRepository));

            return dic;
        }
    }
}