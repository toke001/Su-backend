using Microsoft.EntityFrameworkCore;
using WebServer.Data;
using WebServer.Dtos;
using WebServer.Interfaces;
using WebServer.Models;

namespace WebServer.Reposotory
{
    public class FormItemColumnRepository : IFormItemColumn
    {
        private readonly WaterDbContext _context;
        private readonly DbSet<ApprovedFormItemColumn> _dbSetForm;
        private readonly DbSet<ColumnLayout> _dbSetFormLayout;
        private readonly IHttpContextAccessor _httpContext;
        public FormItemColumnRepository(WaterDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
            _dbSetForm = _context.Set<ApprovedFormItemColumn>();
            _dbSetFormLayout = _context.Set<ColumnLayout>();
        }

        public async Task<ApprovedFormItemColumnServDto> Add(ApprovedFormItemColumnServDto aForm)
        {
            try
            {
                var col = await _dbSetForm.FirstOrDefaultAsync(x => x.Id == aForm.Id);
            if (col != null)
            {
                return await Update(aForm);
            }
            var newForm = new ApprovedFormItemColumn()
            {
                Id = Guid.NewGuid(),
                ApprovedFormItemId = aForm.ApprovedFormItemId,
                DataType = aForm.DataType,
                DisplayOrder = aForm.DisplayOrder,
                Length = aForm.Length,
                NameKk = aForm.NameKk,
                NameRu = aForm.NameRu,
                Nullable = aForm.Nullable,
                ReportCode = aForm.ReportCode,
            };
            await _dbSetForm.AddAsync(newForm);
            if(aForm.Layout != null)
            {
                await _dbSetFormLayout.AddAsync(new ColumnLayout()
                {
                    Id= Guid.NewGuid(),
                    ApprovedFormItemColumnId = newForm.Id,
                    Height = aForm?.Layout.Height,
                    Width = aForm?.Layout.Width,
                    Position = aForm?.Layout.Position,
                });
            }
            
                await _context.SaveChangesAsync();
                return aForm;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при создании формы"+ex.Message);
            }
        }

        public async Task<ApprovedFormItemColumn> Delete(Guid id)
        {
            var item = await _dbSetForm.FindAsync(id);
            if (item == null)
            {
                throw new Exception("Объект не найден");
            }
            _dbSetForm.Remove(item);
            try
            {
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при удалении объекта");
            }
        }

        public async Task<List<ApprovedFormItemColumnServDto>> GetForms(Guid tabId)
        {
            var forms = await _dbSetForm
                .Where(x => x.ApprovedFormItemId == tabId)
                .Select(x=>new ApprovedFormItemColumnServDto()
                {
                    Id = x.Id,
                    ApprovedFormItemId = x.ApprovedFormItemId,
                    DataType = x.DataType,
                    DisplayOrder = x.DisplayOrder,
                    Name = x.NameRu,
                    Length = x.Length,
                    ReportCode = x.ReportCode,
                    Nullable = x.Nullable,
                    NameRu = x.NameRu,
                    NameKk = x.NameKk,
                })
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();
            if (forms == null)
            {
                return new List<ApprovedFormItemColumnServDto>();
            }
            //TODO load layouts
            //foreach (var form in forms)
            //{
                
            //}
            return forms;
        }

        public async Task<ApprovedFormItemColumnServDto> Update(ApprovedFormItemColumnServDto aForm)
        {
            var form = await _dbSetForm.FindAsync(aForm.Id);
            if (form == null) { throw new Exception("Объект не найден"); }
            form.DataType = aForm.DataType;
            form.NameKk = aForm.NameKk;
            form.NameRu = aForm.NameRu;
            form.Length = aForm.Length;
            form.Nullable = aForm.Nullable;
            form.DisplayOrder = aForm.DisplayOrder;
            form.ReportCode = aForm.ReportCode;
            _context.Entry(form).State = EntityState.Modified;
            
            if(aForm.Layout!=null)
            {
                var layout = await _dbSetFormLayout.FindAsync(aForm.Layout.Id);
                if (layout == null) { throw new Exception("Объект layout не найден"); }
                layout.Height = aForm.Layout.Height;
                layout.Width = aForm.Layout.Width;
                layout.Position = aForm.Layout.Position;
                _context.Entry(layout).State = EntityState.Modified;
            }
            try
            {
                await _context.SaveChangesAsync();
                return aForm;
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при обновлении объекта");
            }
        }
    }
}
