using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services.IoC
{
    public static class Module
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dic = new Dictionary<Type, Type>();
            dic.Add(typeof(Interfaces.ITodoService), typeof(TodoService));

            return dic;
        }
    }
}
