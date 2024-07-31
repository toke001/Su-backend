using WebServer.Dtos;
using WebServer.Models;

namespace WebServer.Interfaces
{
    public interface ICityExportImport
    {
        Task<List<SeloTotalFormsDto>> GetCityTotalFormsAsync(string kato, int year);
        byte[] GenerateExcelFile(List<SeloTotalFormsDto> forms);
        byte[] GenerateExcelTemplate();
        Task<int> ImportExcel(IFormFile file, string login, int year);
    }
}
