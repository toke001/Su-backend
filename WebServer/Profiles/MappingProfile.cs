using AutoMapper;
using WebServer.Dtos;
using WebServer.Models;

namespace WebServer.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SeloDoument, SeloDocumentDto>();
            CreateMap<SeloDocumentDto, SeloDoument>();

            CreateMap<SeloForms, SeloFormsDto>();
            CreateMap<SeloFormsDto, SeloForms>();
        }
    }
}
