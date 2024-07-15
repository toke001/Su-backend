using WebServer.Models;

namespace WebServer.Interfaces
{
    public interface ISeloForms
    {
        Task<object> GetSeloDocument(string katoKod);
        Task<SeloDocument> AddSeloDocument(SeloDocument seloDoument);
        Task<object> GetSeloFormsByKodYear(string kodNaselPunk, int year);
        Task<SeloForms> AddSeloForms(Guid idDoc, SeloForms seloForms);
        Task<SeloWaterSupply> GetWaterSupply(Guid idForm);
        Task<SeloWaterSupply> AddWaterSupply(Guid idForm, SeloWaterSupply waterSupplyInfo);
        Task<SeloWaterSupply> UpdateWaterSupply(SeloWaterSupply waterSupplyInfo);
        Task<SeloWaterDisposal> GetWaterDisposal(Guid idForm);
        Task<SeloWaterDisposal> AddWaterDisposal(Guid idForm, SeloWaterDisposal waterDisposalInfo);
        Task<SeloWaterDisposal> UpdateWaterDisposal(SeloWaterDisposal waterDisposalInfo);
        Task<SeloTariff> GetTarifInfo(Guid idForm);
        Task<SeloTariff> AddTarifInfo(Guid idForm, SeloTariff tariffInfo);
        Task<SeloTariff> UpdateTariffInfo(SeloTariff tariffInfo);
        Task<SeloNetworkLength> GetNetworkLength(Guid idForm);
        Task<SeloNetworkLength> AddNetworkLength(Guid idForm, SeloNetworkLength networkLengthInfo);
        Task<SeloNetworkLength> UpdateNetworkLength(SeloNetworkLength networkLengthInfo);
    }
}
