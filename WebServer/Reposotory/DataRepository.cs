using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebServer.Data;
using WebServer.Dtos;
using WebServer.Emuns;
using WebServer.Interfaces;
using WebServer.Models;

namespace WebServer.Reposotory
{
    public class DataRepository : IData
    {
        private readonly WaterDbContext _context;
        private readonly DbSet<ApprovedFormItem> _dbSetForm;
        private readonly DbSet<ApprovedFormItemColumn> _dbSetColumn;
        private readonly DbSet<Models.Data> _dbSetData;
        private readonly IHttpContextAccessor _httpContext;
        public DataRepository(WaterDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
            _dbSetForm = _context.Set<ApprovedFormItem>();
            _dbSetColumn = _context.Set<ApprovedFormItemColumn>();
            _dbSetData = _context.Set<Models.Data>();
        }

        /// <summary>
        /// Получение таблицы для заполнения
        /// </summary>
        /// <param name="id">ИД формы. Таблица ApprovedFormItem</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<DataTableDto>> Get(Guid id)
        {
            var form = await _dbSetForm.FirstOrDefaultAsync(x => x.Id == id && x.IsDel == false);
            if (form == null) throw new Exception("Форма отсутствует");

            var cols = await _dbSetColumn.Where(x => x.ApprovedFormItemId == form.Id).OrderBy(x => x.DisplayOrder).ToListAsync();
            if (cols.Count == 0) throw new Exception("Столбцы отсутствуют");

            var row = new List<DataTableDto>();
            foreach (var col in cols)
            {
                var rowData = await _dbSetData.FirstOrDefaultAsync(x => x.ApproverFormColumnId == col.Id);
                if (rowData != null)
                {
                    row.Add(new DataTableDto()
                    {
                        Id = rowData.Id,
                        ApprovedFormId = rowData.ApprovedFormId,
                        ApprovedFormItemId = rowData.ApprovedFormItemId,
                        ApproverFormColumnId = rowData.ApproverFormColumnId,
                        ValueType = rowData.ValueType,
                        ValueJson = rowData.ValueType == 0 ? col.NameRu : rowData.ValueJson,
                    });
                }
                else
                {
                    row.Add(new DataTableDto()
                    {
                        Id = Guid.NewGuid(),
                        ApprovedFormId = id,
                        ApprovedFormItemId = col.ApprovedFormItemId,
                        ApproverFormColumnId = col.Id,
                        ValueType = (int)col.DataType,
                        ValueJson = col.DataType == 0 ? col.NameRu : GetJsonByDatatype(col.DataType)
                    });

                }
            }
            return row;
        }

        private string GetJsonValue(List<Models.Data> cols, Guid id, int dataType)
        {
            var json = cols.FirstOrDefault(x => x.ApproverFormColumnId == id);
            if (cols.Count == 0 || json == null) return GetJsonByDatatype((Enums.DataTypeEnum)dataType);
            return json.ValueJson;
        }

        private string GetJsonByDatatype(Enums.DataTypeEnum datatype)
        {
            switch (datatype)
            {
                case Enums.DataTypeEnum.Label:
                    return string.Empty;
                case Enums.DataTypeEnum.IntegerType:
                    return "0";
                case Enums.DataTypeEnum.DecimalType:
                    return "0.0";
                case Enums.DataTypeEnum.StringType:
                    return string.Empty;
                case Enums.DataTypeEnum.BooleanType:
                    return "false";
                case Enums.DataTypeEnum.DateType:
                    return DateTime.Now.ToShortDateString();
                default:
                    throw new InvalidOperationException("Invalid ValueType");
            }
        }

        public async Task<string> Update(List<DataTableDto> data)
        {
            foreach (var item in data)
            {
                var existingRow = await _dbSetData.FindAsync(item.Id);
                if (existingRow != null)
                {
                    existingRow.ValueJson = item.ValueJson;
                    existingRow.LastModifiedDate = DateTime.UtcNow;
                    _context.Update(existingRow);
                }
                else
                {
                    await _dbSetData.AddAsync(new Models.Data()
                    {
                        Id = item.Id,
                        ApprovedFormId = item.ApprovedFormId,
                        ApprovedFormItemId = item.ApprovedFormItemId,
                        ApproverFormColumnId = item.ApproverFormColumnId,
                        CreateDate = DateTime.UtcNow,
                        IsDel = false,
                        ValueJson = item.ValueJson,
                        ValueType = (int)item.ValueType,
                    });
                }
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Данные успешно обновлены";
        }
    }
}
