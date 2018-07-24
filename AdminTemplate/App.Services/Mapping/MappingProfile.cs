using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services.Mapping
{
    public class MappingProfile: AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Dtos.TodoDto, Domain.Entities.Todo>().ReverseMap();
            CreateMap<Dtos.TodoFilterDto, Domain.Filters.TodoFilter>().ReverseMap();

        }
    }
}
