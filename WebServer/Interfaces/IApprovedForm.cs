using WebServer.Models;

namespace WebServer.Interfaces
{
    public interface IApprovedForm
    {
        public Task<List<ApprovedForm>> GetAll();
        public Task<ApprovedForm> Add(ApprovedForm form);
        public Task<ApprovedForm> Deactivate(Guid id);
        public Task<ApprovedForm> Delete(Guid id);
    }
}
