using WebServer.Dtos;
using WebServer.Models;

namespace WebServer.Interfaces
{
    public interface ISeloExportImport
    {
        Task<List<SeloTotalFormsDto>> GetSeloTotalFormsAsync(string kato, int year);
        byte[] GenerateExcelFile(List<SeloTotalFormsDto> forms);
        byte[] GenerateExcelTemplate();
        Task<int> ImportExcel(IFormFile file);
    }
}
