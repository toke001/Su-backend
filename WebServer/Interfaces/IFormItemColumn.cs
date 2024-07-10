using WebServer.Dtos;
using WebServer.Models;

namespace WebServer.Interfaces
{
    public interface IFormItemColumn
    {
        public Task<List<ApprovedFormItemColumnServDto>> GetForms(Guid tabId);
        public Task<ApprovedFormItemColumnServDto> Add(ApprovedFormItemColumnServDto aForm);
        public Task<ApprovedFormItemColumnServDto> Update(ApprovedFormItemColumnServDto aForm);
        public Task<ApprovedFormItemColumn> Delete(Guid id);
    }
}
