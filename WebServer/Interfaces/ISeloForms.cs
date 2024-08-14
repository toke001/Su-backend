using WebServer.Models;

namespace WebServer.Interfaces
{
    public interface ISeloForms
    {
        Task<object> GetSeloDocument(string katoKod);
        Task<List<SeloDocument>> GetSeloDocumentByParams(string? kodOblast, string? kodRaion, int? year);
        Task<object> GetSeloFormsByDocId(Guid idDoc);
        Task<SeloDocument> AddSeloDocument(SeloDocument seloDoument);
        Task<object> GetSeloFormsByKodYear(string kodNaselPunk, int year);
        Task<List<SeloForm>> AddSeloForms(Guid idDoc, List<SeloForm> seloForms);
        Task<SeloForm> GetSeloFormById(Guid id);
        Task<SeloForm> UpdateSeloForm(SeloForm seloForm);
        
    }
}
