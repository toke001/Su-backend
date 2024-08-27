using WebServer.Models;

namespace WebServer.Interfaces
{
    public interface ICityForms
    {
        Task<object> GetCityDocument(string katoKod);
        Task<List<CityDocument>> GetCityDocumentByParams(string? kodOblast, string? kodRaion, int? year);
        Task<object> GetCityFormsByDocId(Guid idDoc);
        Task<CityDocument> AddCityDocument(CityDocument cityDocument);
        Task<object> GetCityFormsByKodYear(string kodNaselPunk, int year);
        Task<CityForm> AddCityForms(Guid idDoc, CityForm cityForms);
        Task<CityForm> GetCityFormById(Guid id);
        Task<CityForm> UpdateCityForm(CityForm cityForm);
        //Task<CityWaterSupply> GetWaterSupply(Guid idForm);
        //Task<CityWaterSupply> AddWaterSupply(Guid idForm, CityWaterSupply waterSupplyInfo);
        //Task<CityWaterSupply> UpdateWaterSupply(CityWaterSupply waterSupplyInfo);
        //Task<CityWaterDisposal> GetWaterDisposal(Guid idForm);
        //Task<CityWaterDisposal> AddWaterDisposal(Guid idForm, CityWaterDisposal waterDisposalInfo);
        //Task<CityWaterDisposal> UpdateWaterDisposal(CityWaterDisposal waterDisposalInfo);
        //Task<CityTarif> GetTarifInfo(Guid idForm);
        //Task<CityTarif> AddTarifInfo(Guid idForm, CityTarif tariffInfo);
        //Task<CityTarif> UpdateTariffInfo(CityTarif tariffInfo);
        //Task<CityNetworkLength> GetNetworkLength(Guid idForm);
        //Task<CityNetworkLength> AddNetworkLength(Guid idForm, CityNetworkLength networkLengthInfo);
        //Task<CityNetworkLength> UpdateNetworkLength(CityNetworkLength networkLengthInfo);
    }
}
