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
        Task<List<CityForm>> AddCityForms(string login, List<CityForm> cityForms);
        Task<CityForm> GetCityFormById(Guid id);
        Task<CityForm> UpdateCityForm(string login, CityForm cityForm);
    }
}
