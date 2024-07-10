using Microsoft.EntityFrameworkCore;
using WebServer.Data;
using WebServer.Dtos;
using WebServer.Interfaces;
using WebServer.Models;
using WebServer.Helpers;

namespace WebServer.Reposotory
{
    public class ReportRepository : IReport
    {
        private readonly WaterDbContext _context;
        private readonly DbSet<Report_Form> _dbSetForm;
        private readonly DbSet<Ref_Status> _dbSetRefStatus;
        private readonly IHttpContextAccessor _httpContext;
        private readonly DbSet<ApprovedForm> _dbSetApprovedForm;
        private readonly DbSet<ApprovedFormItem> _dbSetApprovedFormItem;
        private readonly DbSet<ApprovedFormItemColumn> _dbSetApprovedFormItemColumn;
        private readonly DbSet<Ref_Kato> _dbSetRefKato;
        private readonly DbSet<Models.Data> _dbData;
        private readonly DbSet<Models.ReportSupplier> _dbReportSupplier;
        private readonly DbSet<Models.Supplier> _dbSupplier;

        public ReportRepository(WaterDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
            _dbSetForm = _context.Set<Report_Form>();
            _dbSetRefStatus = _context.Set<Ref_Status>();
            _dbSetApprovedForm = _context.Set<ApprovedForm>();
            _dbSetApprovedFormItem = _context.Set<ApprovedFormItem>();
            _dbSetApprovedFormItemColumn = _context.Set<ApprovedFormItemColumn>();
            _dbSetRefKato = _context.Set<Ref_Kato>();
            _dbData = _context.Set<Models.Data>();
            _dbReportSupplier = _context.Set<ReportSupplier>();
            _dbSupplier = _context.Set<Supplier>();
        }
        public async Task<ReportsDto> Add(Report_Form form)
        {
            var existForm = await _dbSetForm
                .FirstOrDefaultAsync(x => x.IsDel == false &&
                x.ApprovedFormId == form.ApprovedFormId &&
                x.RefKatoId == form.RefKatoId
                && x.ReportYearId == form.ReportYearId &&
                x.ReportMonthId == form.ReportMonthId);
            if (existForm != null)
            {
                throw new DuplicateWaitObjectException("Отчет за данный период уже добавлен");
            }
            await _dbSetForm.AddAsync(form);
            try
            {
                await _context.SaveChangesAsync();
                return new ReportsDto()
                {
                    Id = form.Id,
                    HasStreets = false,
                    Description = form.Description,
                    AuthorId = form.AuthorId,
                    CreateDate = form.CreateDate,
                    IsDel = form.IsDel,
                    LastModifiedDate = form.LastModifiedDate,
                    RefKatoId = form.RefKatoId,
                    RefStatusId = form.RefStatusId,
                    RefStatusLabel = GetStatusLabelById(form.RefStatusId),
                    ReportMonthId = 0,
                    ReportYearId = form.ReportYearId,
                };
            }
            catch (Exception)
            {
                throw new Exception("Что то пошло не так, попробуйте позже!");
            }
        }

        public async Task<List<ReportsDto>> Delete(Guid id)
        {
            var item = await _dbSetForm.FindAsync(id);
            if (item == null)
            {
                throw new Exception("Объект не найден");
            }
            item.IsDel = true;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await Get(item.RefKatoId);
        }

        public async Task<List<ReportsDto>> Get(int katoId)
        {
            return await _dbSetForm.Include(x => x.RefStatus)
                    .Where(x => x.IsDel == false && x.RefKatoId == katoId)
                    .Select(x => new ReportsDto()
                    {
                        Id = x.Id,
                        HasStreets = false,
                        Description = x.Description,
                        AuthorId = x.AuthorId,
                        CreateDate = x.CreateDate,
                        IsDel = x.IsDel,
                        LastModifiedDate = x.LastModifiedDate,
                        RefKatoId = x.RefKatoId,
                        RefStatusId = x.RefStatusId,
                        RefStatusLabel = x.RefStatus != null ? x.RefStatus.NameRu : "статус не определен",
                        ReportMonthId = 0,
                        ReportYearId = x.ReportYearId,
                    })
                    .ToListAsync();
        }

        private string GetStatusLabelById(int id)
        {
            var label = _dbSetRefStatus.FirstOrDefault(x => x.Id == id);
            if (label == null) return "";
            return label.NameRu;
        }

        public async Task<List<ApprovedFormItemDto>> GetServices()
        {
            return await _dbSetApprovedFormItem.Where(x => x.IsDel == false)
                .Select(x => new ApprovedFormItemDto()
                {
                    Id = x.Id,
                    ApprovedFormId = x.ApprovedFormId,
                    ServiceId = x.ServiceId,
                    ServiceName = GetServiceName(x.ServiceId),
                    Title = x.Title,
                    DisplayOrder = x.DisplayOrder,
                    Url = $"/forms?id={x.Id}"
                })
                .ToListAsync();
        }

        public async Task<List<ApprovedFormItemColumnServDto>> GetApprovedFormItemColumnsServId(Guid Id)
        {
            return await _dbSetApprovedFormItemColumn.Where(x => x.ApprovedFormItemId == Id)
                .Select(x => new ApprovedFormItemColumnServDto()
                {
                    Id = x.Id,
                    ApprovedFormItemId = x.ApprovedFormItemId,
                    DataType = x.DataType,
                    NameRu = x.NameRu,
                    NameKk = x.NameKk,
                    Length = x.Length,
                    Nullable = x.Nullable,
                    DisplayOrder = x.DisplayOrder
                })
                .ToListAsync();
        }

        private static string GetServiceName(int serviceId)
        {
            return serviceId switch
            {
                0 => "водоснабжение",
                1 => "водоотведение",
                _ => "водопровод"
            };
        }

        public async Task<List<ApprovedFormItemColumnTableDto>> GetApprovedFormItemColumnTablesById(Guid Id)
        {
            return await _dbSetApprovedFormItemColumn.Where(x => x.Id == Id)
                .Select(x => new ApprovedFormItemColumnTableDto()
                {
                    Id = x.Id,
                    DataType = (int)x.DataType,
                    Name = x.NameRu,
                    DisplayOrder = x.DisplayOrder,
                    //Data = x.DataJson
                })
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();
        }

        public async Task<List<ApprovedFormItem>> GetTabsByServiceID(int id)
        {
            var result = new List<ApprovedFormItem>();
            //получаем утвержденную форму
            var form = await _dbSetApprovedForm.FirstOrDefaultAsync(x => !x.CompletionDate.HasValue);
            if (form == null) throw new Exception("Отсутствуют актвные утвержденные формы");
            //получаем вкладки утвержденной формы
            var tabs = await _dbSetApprovedFormItem.Where(x => x.ApprovedFormId == form.Id && x.ServiceId == id).ToListAsync();
            if (!tabs.Any() || tabs == null) throw new Exception("Отсутствуют формы");
            return tabs;
        }

        public async Task<List<ReportByKatoDto>> GetByKato(int? id)
        {
            var result = new List<ReportByKatoDto>();
            if (!id.HasValue)
            {
                var reps = await _dbSetApprovedForm.Include(x => x.Items).Where(x => x.IsDel == false).ToListAsync();
                foreach (var item in reps)
                {
                    foreach (var item2 in item.Items)
                    {
                        var cols = await _dbSetApprovedFormItemColumn.Where(x => x.ApprovedFormItemId == item2.ApprovedFormId).ToListAsync();
                        if (cols == null) continue;
                        result.Add(new ReportByKatoDto()
                        {

                        });
                    }
                }
            }
            return new List<ReportByKatoDto>();
        }

        public async Task<int> FillRandomData()
        {
            try
            {
                var cnt = 0;
                var data = await _dbData.Where(x => x.IsDel == false).ToListAsync();
                if (data.Count > 10) return 0;
                var forms = await _dbSetApprovedForm.Include(x => x.Items).FirstOrDefaultAsync(x => x.IsDel == false);
                var formCols = new List<ApprovedFormItemColumn>();
                foreach (var tabs in forms.Items.Where(x => x.ServiceId == 0 || x.ServiceId == 1))
                {
                    var cols = await _dbSetApprovedFormItemColumn.Include(x => x.ApprovedFormItem).Where(x => x.ApprovedFormItemId == tabs.Id).ToListAsync();
                    formCols.AddRange(cols);
                }
                var cities = await _dbSetRefKato.Where(x => x.KatoLevel == 1 && x.Code.ToString().StartsWith("7")).ToListAsync();

                foreach (var item in cities)
                {
                    await Add(new Report_Form()
                    {
                        Id = Guid.NewGuid(),
                        CreateDate = DateTime.UtcNow,
                        Description = "тестовый отчет за 2024",
                        HasStreets = false,
                        IsDel = false,
                        RefKatoId = item.Id,
                        RefStatusId = 3,
                        ReportMonthId = 0,
                        ReportYearId = 2024,
                        ApprovedFormId = forms.Id,
                    });
                }
                await _context.SaveChangesAsync();

                var reports_ = await _dbSetForm.ToListAsync();
                //добавление поставщиков 1 город 1 поставщик
                foreach (var city in cities)
                {
                    await _dbSupplier.AddAsync(new Supplier()
                    {
                        Bin = new Random().NextInt64(100000000000, 999999999999).ToString(),
                        FullName = city.NameRu.Trim().Replace("г.", "") + " Водоканал",
                        Id = Guid.NewGuid(),
                        KatoId = city.Id,
                    });
                }
                await _context.SaveChangesAsync();

                //добавление поставщиков к отчетам 1 поставщик на водоснабжение и 1 на водоотведение
                var suppliers = await _dbSupplier.ToListAsync();
                foreach (var item in reports_)
                {
                    await _dbReportSupplier.AddAsync(new ReportSupplier()
                    {
                        Id = Guid.NewGuid(),
                        Report_FormId = item.Id,
                        SupplierId = suppliers.FirstOrDefault(x => x.KatoId == item.RefKatoId).Id,
                        IsDel = false,
                        ServiceId = 0,
                        CreateDate = DateTime.UtcNow,
                        Description = "тестовый поставщик",
                    });
                    await _dbReportSupplier.AddAsync(new ReportSupplier()
                    {
                        Id = Guid.NewGuid(),
                        Report_FormId = item.Id,
                        SupplierId = suppliers.FirstOrDefault(x => x.KatoId == item.RefKatoId).Id,
                        IsDel = false,
                        ServiceId = 1,
                        CreateDate = DateTime.UtcNow,
                        Description = "тестовый поставщик",
                    });
                }
                await _context.SaveChangesAsync();
                var dataList = new List<Models.Data>();


                //добавление данных

                foreach (var report in reports_)
                {
                    foreach (var col in formCols)
                    {
                        await _dbData.AddAsync(new Models.Data()
                        {
                            Id = Guid.NewGuid(),
                            ReportFormId = report.Id,
                            ApprovedFormId = col.ApprovedFormItem.ApprovedFormId,
                            ApprovedFormItemId = col.ApprovedFormItem.Id,
                            ApproverFormColumnId = col.Id,
                            CreateDate = DateTime.UtcNow,
                            Description = $"тестовое заполнение {col.ApprovedFormItem.Title}|{col.NameRu}|{report.Description}",
                            IsDel = false,
                            ValueType = (int)col.DataType,
                            ValueJson = GetRandomValueJson((int)col.DataType),
                        });
                        cnt++;
                    }
                }

                await _context.SaveChangesAsync();
                dataList.Clear();

                /*
                var villages = await _dbSetRefKato.Where(x => x.KatoLevel == 2).ToListAsync();

                foreach (var villageItem in villages)
                {
                    foreach (var col in formCols)
                    {
                        dataList.Add(new Models.Data()
                        {
                            Id = Guid.NewGuid(),
                            ApprovedFormId = col.ApprovedFormItem.ApprovedFormId,
                            ApprovedFormItemId = col.ApprovedFormItem.Id,
                            ApproverFormColumnId = col.Id,
                            CreateDate = DateTime.UtcNow,
                            Description = "тестовое заполнение",
                            IsDel = false,
                            ValueType = (int)col.DataType,
                            ValueJson = GetRandomValueJson((int)col.DataType),
                        });
                    }
                }

                cnt += dataList.Count;
                await _dbData.AddRangeAsync(dataList);
                await _context.SaveChangesAsync();
                */
                return cnt;
            }
            catch (Exception ex)
            {
                var exp = ex.Message;
                throw new Exception("Ошибка при сохранении данных");
            }
        }

        private string GetRandomValueJson(int dataType)
        {
            Random rnd = new Random();
            switch (dataType)
            {
                case 0:
                    return "случайная строка";
                case 1:
                    return rnd.Next(1, 5000).ToString();
                case 2:
                    {
                        double d = rnd.NextDouble();
                        decimal rd = (decimal)d;
                        decimal min = 1.0m;
                        decimal max = 5000.0m;
                        return ((decimal)d * (max - min) + min).ToString();
                    }
                case 3:
                    return "случайная строка";
                case 4:
                    return (rnd.Next(0, 2) == 1) == true ? "true" : "false";
                case 5:
                    {
                        DateTime start = new DateTime(2000, 1, 1);
                        int range = (DateTime.Today - start).Days;
                        DateTime rndDate = start.AddDays(rnd.Next(range));
                        return rndDate.ToShortDateString();
                    }
                default:
                    throw new InvalidOperationException("Invalid ValueType");
            }
        }

        public async Task<Tuple<List<Dictionary<string,string>>,List<Dictionary<string, string>>>> GetReport2024()
        {
            var report = new List<Dictionary<string, object>>();
            var reportHeaders = new List<Dictionary<string, object>>();
            var datatable = new List<Dictionary<string, string>>();
            var datatableHeaders = new List<Dictionary<string, string>>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "CALL GetReport2024();";
                command.CommandType = System.Data.CommandType.Text;

                await _context.Database.OpenConnectionAsync();
                await command.ExecuteNonQueryAsync();

                command.CommandText = "SELECT * FROM tmp_GetReport2024;";
                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        var row = new Dictionary<string, object>();
                        for (global::System.Int32 i = 0; i < result.FieldCount; i++)
                        {
                            row[result.GetName(i)] = result.GetValue(i);
                        }
                        report.Add(row);
                    }
                }
                command.CommandText = "SELECT * FROM tmp_GetReport2024Hearders;";
                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        var row = new Dictionary<string, object>();
                        for (global::System.Int32 i = 0; i < result.FieldCount; i++)
                        {
                            row[result.GetName(i)] = result.GetValue(i);
                        }
                        reportHeaders.Add(row);
                    }
                }
            }
            report.ForEach(x => 
            {
                datatable.Add(DatatableHelper.ConvertToDictionaryString(x));
            });
            reportHeaders.ForEach(x =>
            {
                datatableHeaders.Add(DatatableHelper.ConvertToDictionaryString(x));
            });
            return Tuple.Create(datatableHeaders, datatable);
        }
    }
}
