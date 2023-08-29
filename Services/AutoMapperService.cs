using AutoMapper;
using Portfolio.Areas.Management.Models;
using Portfolio.Data.Entities;

namespace Portfolio.Services
{
    public class AutoMapperService : Profile
    {
        public AutoMapperService()
        {
            CreateMap<Service, ServiceViewModel>().ReverseMap();
            CreateMap<Service, CreateServiceViewModel>().ReverseMap();
            CreateMap<Service, EditServiceViewModel>().ReverseMap();
            CreateMap<ServiceViewModel, EditServiceViewModel>().ReverseMap();
            
            CreateMap<Image, ImageViewModel>().ReverseMap();

            CreateMap<RecentWork, RecentWorkViewModel>().ReverseMap();
            CreateMap<RecentWork, CreateRecentWorkViewModel>().ReverseMap();
            CreateMap<RecentWork, EditRecentWorkViewModel>().ReverseMap();
            CreateMap<RecentWorkViewModel, EditRecentWorkViewModel>().ReverseMap();

            CreateMap<Language, LanguageViewModel>().ReverseMap();
            CreateMap<Language, CreateLanguageViewModel>().ReverseMap();
            CreateMap<Language, EditLanguageViewModel>().ReverseMap();
            CreateMap<LanguageViewModel, EditLanguageViewModel>().ReverseMap();

            CreateMap<TechnicalSkill, TechnicalSkillViewModel>().ReverseMap();

            CreateMap<Image, Portfolio.Models.ImageViewModel>().ReverseMap();
            CreateMap<Service, Portfolio.Models.ServiceViewModel>().ReverseMap();
            CreateMap<RecentWork, Portfolio.Models.RecentWorkViewModel>().ReverseMap();
            CreateMap<Language, Portfolio.Models.LanguageViewModel>().ReverseMap();
            CreateMap<TechnicalSkill, Portfolio.Models.TechnicalSkillViewModel>().ReverseMap();
        }
    }
}