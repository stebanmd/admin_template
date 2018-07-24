using App.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace App.Services.IoC
{
    public static class Module
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dic = new Dictionary<Type, Type>();

            dic.Add(typeof(ITodoService), typeof(TodoService));
            dic.Add(typeof(IAdminUserService), typeof(AdminUserService));

            return dic;
        }
    }
}