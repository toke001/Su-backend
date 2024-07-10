using WebServer.Dtos;
using WebServer.Models;

namespace WebServer.Interfaces
{
    public interface IReport
    {
        public Task<List<ReportsDto>> Get(int katoId);
        public Task<ReportsDto> Add(Report_Form form);
        public Task<List<ReportsDto>> Delete(Guid id);
        Task<List<ApprovedFormItemDto>> GetServices();
        public Task<List<ApprovedFormItem>> GetTabsByServiceID(int id);
        public Task<List<ReportByKatoDto>> GetByKato(int? id);
        public Task<int> FillRandomData();

        public Task<Tuple<List<Dictionary<string, string>>, List<Dictionary<string, string>>>> GetReport2024();
    }
}
