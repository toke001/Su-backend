using WebServer.Models;

namespace WebServer.Interfaces
{
    public interface ISeloForms
    {
        Task<List<SeloDocument>> GetSeloDocument(string katoKod);
        Task<SeloDocument> AddSeloDocument(SeloDocument seloDoument);
        Task<SeloForms> GetSeloFormsByKodYear(string kodNaselPunk, int year);
        Task<SeloForms> AddSeloForms(Guid idDoc, SeloForms seloForms);
        Task<WaterSupplyInfo> GetWaterSupply(Guid idForm);
        Task<WaterSupplyInfo> AddWaterSupply(Guid idForm, WaterSupplyInfo waterSupplyInfo);
        Task<WaterSupplyInfo> UpdateWaterSupply(WaterSupplyInfo waterSupplyInfo);
        Task<WaterDisposalInfo> GetWaterDisposal(Guid idForm);
        Task<WaterDisposalInfo> AddWaterDisposal(Guid idForm, WaterDisposalInfo waterDisposalInfo);
        Task<WaterDisposalInfo> UpdateWaterDisposal(WaterDisposalInfo waterDisposalInfo);
        Task<TariffInfo> GetTarifInfo(Guid idForm);
        Task<TariffInfo> AddTarifInfo(Guid idForm, TariffInfo tariffInfo);
        Task<TariffInfo> UpdateTariffInfo(TariffInfo tariffInfo);
        Task<NetworkLengthInfo> GetNetworkLength(Guid idForm);
        Task<NetworkLengthInfo> AddNetworkLength(Guid idForm, NetworkLengthInfo networkLengthInfo);
        Task<NetworkLengthInfo> UpdateNetworkLength(NetworkLengthInfo networkLengthInfo);
    }
}
