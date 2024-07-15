using WebServer.Models;

namespace WebServer.Interfaces
{
    public interface ICityForms
    {
        Task<object> GetCityDocument(string katoKod);
        Task<CityDocument> AddCityDocument(CityDocument seloDoument);
        Task<object> GetCityFormsByKodYear(string kodNaselPunk, int year);
        Task<CityForms> AddCityForms(Guid idDoc, CityForms seloForms);
        Task<CityWaterSupply> GetWaterSupply(Guid idForm);
        Task<CityWaterSupply> AddWaterSupply(Guid idForm, CityWaterSupply waterSupplyInfo);
        Task<CityWaterSupply> UpdateWaterSupply(CityWaterSupply waterSupplyInfo);
        Task<CityWaterDisposal> GetWaterDisposal(Guid idForm);
        Task<CityWaterDisposal> AddWaterDisposal(Guid idForm, CityWaterDisposal waterDisposalInfo);
        Task<CityWaterDisposal> UpdateWaterDisposal(CityWaterDisposal waterDisposalInfo);
        Task<CityTarif> GetTarifInfo(Guid idForm);
        Task<CityTarif> AddTarifInfo(Guid idForm, CityTarif tariffInfo);
        Task<CityTarif> UpdateTariffInfo(CityTarif tariffInfo);
        Task<CityNetworkLength> GetNetworkLength(Guid idForm);
        Task<CityNetworkLength> AddNetworkLength(Guid idForm, CityNetworkLength networkLengthInfo);
        Task<CityNetworkLength> UpdateNetworkLength(CityNetworkLength networkLengthInfo);
    }
}
