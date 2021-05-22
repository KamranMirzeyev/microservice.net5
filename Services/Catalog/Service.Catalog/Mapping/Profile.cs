using Service.Catalog.Dtos;
using Service.Catalog.Models;

namespace Service.Catalog.Mapping
{
    public class Profile : AutoMapper.Profile
    {
        public Profile()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

            CreateMap<CourseCreateDto, Course>().ReverseMap();
            CreateMap<CourseUpdateDto, Course>().ReverseMap();
        }
    }
}