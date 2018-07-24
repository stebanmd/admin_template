namespace App.Services.Mapping
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Dtos.TodoDto, Domain.Entities.Todo>().ReverseMap();
            CreateMap<Dtos.TodoFilterDto, Domain.Filters.TodoFilter>().ReverseMap();

            CreateMap<Dtos.AdminUserDto, Domain.Entities.AdminUser>().ReverseMap();
            CreateMap<Dtos.ProfileDto, Domain.Entities.Profile>().ReverseMap();
            CreateMap<Dtos.MenuAccessDto, Domain.Entities.MenuAccess>().ReverseMap();
        }
    }
}