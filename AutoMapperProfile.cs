using AutoMapper;
using Motivator.DB.Models;
using Motivator.Pages.Todos;

namespace Motivator
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Todo, CreateModel>()
                .ReverseMap();
        }
    }
}
