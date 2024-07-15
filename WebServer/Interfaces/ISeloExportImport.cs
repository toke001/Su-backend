using WebServer.Models;

namespace WebServer.Interfaces
{
    public interface ISeloExportImport
    {
        Task<SeloForms> GetFormsAsync(string kato, int year);
        byte[] GenerateExcelFile(SeloForms form);
    }
}
