using AutoMapper;
using MoviesAPI.Models;
using MoviesAPI.Models.Dtos;

namespace MoviesAPI.Mapper
{
    public class MovieMapper : Profile
    {
        public MovieMapper()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Movie, MovieDto>().ReverseMap();
        }
    }
}
