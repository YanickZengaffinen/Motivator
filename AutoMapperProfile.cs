using AutoMapper;
using Motivator.DB.Models;
using Motivator.Pages.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Motivator
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Task, CreateModel>()
                .ReverseMap();
        }
    }
}
