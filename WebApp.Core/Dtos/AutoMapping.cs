using AutoMapper;
using WebApp.Data.Models.Entities;

namespace WebApp.Core.Dtos
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Project, ProjectResponseData>();
        }
    }
}
