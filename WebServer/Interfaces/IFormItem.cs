using WebServer.Models;

namespace WebServer.Interfaces
{
    public interface IFormItem
    {
        public Task<List<ApprovedFormItem>> GetForms(Guid formId);
        public Task<ApprovedFormItem> Add(ApprovedFormItem aForm);
        public Task<ApprovedFormItem> Update(ApprovedFormItem aForm);
        public Task<ApprovedFormItem> Delete(Guid id);
    }
}
