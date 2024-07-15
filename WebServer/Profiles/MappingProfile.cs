using AutoMapper;
using WebServer.Dtos;
using WebServer.Models;

namespace WebServer.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Selo
            CreateMap<SeloDocument, SeloDocumentDto>();
            CreateMap<SeloDocumentDto, SeloDocument>();

            CreateMap<SeloForms, SeloFormsDto>();
            CreateMap<SeloFormsDto, SeloForms>();

            CreateMap<SeloWaterSupply, SeloWaterSupplyDto>();
            CreateMap<SeloWaterSupplyDto, SeloWaterSupply>();

            CreateMap<SeloWaterDisposal, SeloWaterDisposalDto>();
            CreateMap<SeloWaterDisposalDto, SeloWaterDisposal>();

            CreateMap<SeloTariff, SeloTariffDto>();
            CreateMap<SeloTariffDto, SeloTariff>();

            CreateMap<SeloNetworkLength, SeloNetworkLengthDto>();
            CreateMap<SeloNetworkLengthDto, SeloNetworkLength>();

            //City
            CreateMap<CityDocumentDto, CityDocument>();
            CreateMap<CityFormsDto, CityForms>();
            CreateMap<CityForms, CityFormsDto>();
            CreateMap<CityWaterDisposalDto, CityWaterDisposal>();
            CreateMap<CityWaterSupplyDto, CityWaterSupply>();
            CreateMap<CityTarifDto, CityTarif>();
            CreateMap<CityNetworkLengthDto, CityNetworkLength>();
        }
    }
}
