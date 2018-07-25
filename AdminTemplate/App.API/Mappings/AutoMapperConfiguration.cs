using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Initialize()
        {
            AutoMapper.Mapper.Initialize((cfg) =>
            {
                cfg.AddProfiles(IoC.AutoMapperConfiguration.GetAutoMapperProfiles());
            });
        }
    }
}
