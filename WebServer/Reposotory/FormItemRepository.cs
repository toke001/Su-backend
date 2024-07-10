using Microsoft.EntityFrameworkCore;
using WebServer.Data;
using WebServer.Interfaces;
using WebServer.Models;

namespace WebServer.Reposotory
{
    public class FormItemRepository : IFormItem
    {
        private readonly WaterDbContext _context;
        private readonly DbSet<ApprovedFormItem> _dbSetForm;
        private readonly IHttpContextAccessor _httpContext;
        public FormItemRepository(WaterDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
            _dbSetForm = _context.Set<ApprovedFormItem>();
        }

        public async Task<ApprovedFormItem> Add(ApprovedFormItem aForm)
        {
            await _dbSetForm.AddAsync(aForm);
            try
            {
                await _context.SaveChangesAsync();
                return aForm;
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при создании формы");
            }
        }

        public async Task<ApprovedFormItem> Delete(Guid id)
        {
            var item = await _dbSetForm.FindAsync(id);
            if (item == null)
            {
                throw new Exception("Объект не найден");
            }
            item.IsDel = true;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<List<ApprovedFormItem>> GetForms(Guid formId)
        {
            var form = await _dbSetForm.Where(x=>x.ApprovedFormId == formId && x.IsDel == false).OrderBy(x=>x.DisplayOrder).ToListAsync();
            if (form == null)
            {
                return new List<ApprovedFormItem>();
            }
            return form;
        }

        public async Task<ApprovedFormItem> Update(ApprovedFormItem aForm)
        {
            var form = await _dbSetForm.FindAsync(aForm.Id);
            if (form == null) { throw new Exception("Объект не найден"); }
            form.ServiceId = aForm.ServiceId;
            form.Title = aForm.Title;
            form.IsDel = aForm.IsDel;
            form.DisplayOrder = aForm.DisplayOrder;
            form.IsVillage = aForm.IsVillage;
            _context.Entry(form).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return form;
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при обновлении объекта");
            }
        }
    }
}
