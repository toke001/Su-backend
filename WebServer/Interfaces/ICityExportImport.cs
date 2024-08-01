using WebServer.Dtos;
using WebServer.Models;

namespace WebServer.Interfaces
{
    public interface ICityExportImport
    {
        Task<List<CityTotalFormsDto>> GetCityTotalFormsAsync(string kato, int year);
        byte[] GenerateExcelFile(List<CityTotalFormsDto> forms);
        byte[] GenerateExcelTemplate();
        Task<int> ImportExcel(IFormFile file, string login, int year);
    }
}
