using WebServer.Dtos;
using WebServer.Models;

namespace WebServer.Interfaces
{
    public interface ISeloExportImport
    {
        //Task<SeloTotalFormsDto> GetSeloTotalFormsAsync(string kato, int year);
        Task<List<SeloTotalFormsDto>> GetSeloTotalFormsByParentCodAsync(string parentKato, int year);
        byte[] GenerateExcelFile(List<SeloTotalFormsDto> forms);
        byte[] GenerateExcelTemplate();
        Task<int> ImportExcel(IFormFile file, string login, int year);
    }
}
