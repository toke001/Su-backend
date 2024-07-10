using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebServer.Data;
using WebServer.Dtos;
using WebServer.Interfaces;
using WebServer.Models;

namespace WebServer.Reposotory
{
    public class FormRepository : IForms
    {
        private readonly WaterDbContext _context;
        private readonly DbSet<ApprovedForm> _dbSetForm;
        private readonly IHttpContextAccessor _httpContext;
        public FormRepository(WaterDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
            _dbSetForm = _context.Set<ApprovedForm>();
        }

        public async Task<ApprovedForm> Add(ApprovedForm aForm)
        {
            try
            {
                aForm.ApprovalDate = aForm.ApprovalDate.ToUniversalTime();
                await _dbSetForm.AddAsync(aForm);
                await _context.SaveChangesAsync();

                //отключаем остальные утвержденные формы.
                var list = await _dbSetForm.Where(x => !x.CompletionDate.HasValue && x.Id != aForm.Id).ToListAsync();
                foreach (var entity in list)
                {
                    var row = await _dbSetForm.FindAsync(entity.Id);
                    if (row != null)
                    {
                        row.CompletionDate = DateTime.UtcNow;
                        row.Description = $"Завершен автоматически после создания новой формы:{aForm.Id}";
                        _context.Entry(row).State = EntityState.Modified;
                    }
                    await _context.SaveChangesAsync();
                }
                return aForm;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApprovedForm> Delete(Guid id)
        {
            var item = await _dbSetForm.FindAsync(id);
            if (item == null)
            {
                throw new Exception("Объект не найден");
            }
            item.IsDel = true;
            item.CompletionDate = DateTime.UtcNow;
            item.Description = "Удален пользователем";
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<List<ApprovedForm>> GetForms()
        {
            var form = await _dbSetForm.OrderByDescending(x => x.ApprovalDate).ToListAsync();
            if (form == null)
            {
                return new List<ApprovedForm>();
            }
            return form;
        }

        public async Task<ApprovedForm> Update(ApprovedForm aForm)
        {
            var form = await _dbSetForm.FindAsync(aForm.Id);
            if (form == null) { throw new Exception("Объект не найден"); }
            form.ApprovalDate = aForm.ApprovalDate;
            form.Description = aForm.Description;
            form.IsDel = aForm.IsDel;
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
