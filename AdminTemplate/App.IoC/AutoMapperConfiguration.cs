using System;
using System.Collections.Generic;

namespace App.IoC
{
    public static class AutoMapperConfiguration
    {
        public static IEnumerable<Type> GetAutoMapperProfiles()
        {
            var result = new List<Type>();
            result.Add(typeof(App.Services.Mapping.MappingProfile));
            return result;
        }
    }
}