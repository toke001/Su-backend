using AutoMapper;
using WebServer.Dtos;
using WebServer.Models;

namespace WebServer.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SeloDocument, SeloDocumentDto>();
            CreateMap<SeloDocumentDto, SeloDocument>();

            CreateMap<SeloForms, SeloFormsDto>();
            CreateMap<SeloFormsDto, SeloForms>();
        }
    }
}
