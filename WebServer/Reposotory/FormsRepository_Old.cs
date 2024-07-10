using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using WebServer.Data;
using WebServer.Dtos;
using WebServer.Helpers;
using WebServer.Interfaces;
using WebServer.Models;

namespace WebServer.Reposotory
{
#if LEGACY_COD
    public class FormsRepository_Old
    {
        private readonly WaterDbContext _context;
        private readonly DbSet<Ref_Kato> _dbSetKato;
        private readonly DbSet<Report_Form> _dbSetForm;

        private readonly DbSet<Supply_City_Form1> _dbSetForm1;
        private readonly DbSet<Supply_City_Form2> _dbSetForm2;
        private readonly DbSet<Supply_City_Form3> _dbSetForm3;
        private readonly DbSet<Supply_City_Form4> _dbSetForm4;
        private readonly DbSet<Supply_City_Form5> _dbSetForm5;


        private readonly DbSet<Waste_City_Form1> _dbSetWForm1;
        private readonly DbSet<Waste_City_Form2> _dbSetWForm2;
        private readonly DbSet<Waste_City_Form3> _dbSetWForm3;

        private readonly DbSet<Ref_Street> _dbSetRefStreet;
        private readonly DbSet<Ref_Building> _dbSetRefBuilding;
        private readonly DbSet<SettingsValue> _dbSetSettings;
        private readonly IHttpContextAccessor _httpContext;

        public FormsRepository_Old(WaterDbContext context
            , IHttpContextAccessor httpContext
            )
        {
            _context = context;
            _dbSetKato = _context.Set<Ref_Kato>();
            _dbSetForm = _context.Set<Report_Form>();

            _dbSetForm1 = _context.Set<Supply_City_Form1>();
            _dbSetForm2 = _context.Set<Supply_City_Form2>();
            _dbSetForm3 = _context.Set<Supply_City_Form3>();
            _dbSetForm4 = _context.Set<Supply_City_Form4>();
            _dbSetForm5 = _context.Set<Supply_City_Form5>();

            _dbSetWForm1 = _context.Set<Waste_City_Form1>();
            _dbSetWForm2 = _context.Set<Waste_City_Form2>();
            _dbSetWForm3 = _context.Set<Waste_City_Form3>();

            _dbSetRefStreet = _context.Set<Ref_Street>();
            _dbSetRefBuilding = _context.Set<Ref_Building>();
            _dbSetSettings = _context.Set<SettingsValue>();
            _httpContext = httpContext;
        }

        /// <summary>
        /// Получение улиц и домов по КАТО
        /// </summary>
        /// <param name="id">ИД Формы</param>
        /// <returns></returns>
        private async Task<List<Ref_Building>> GetStreetBuildingByFormId(Guid id)
        {
            var form = await _dbSetForm.FirstOrDefaultAsync(x => x.Id == id);
            if (form == null)
            {
                return new List<Ref_Building>();
            }
            var result = new List<SupplyCityForm1TableDto>();
            return await _dbSetRefBuilding.Include(x => x.RefStreet).Where(x => x.IsDel == false && x.RefStreet.RefKatoId == form.RefKatoId).ToListAsync();
        }

        /// <summary>
        /// включать улицы и дома в отчет
        /// </summary>
        /// <returns>false:не включать/true:включать</returns>
        private async Task<Boolean> IsStreetLevel()
        {
            var isStreetLevel = await _dbSetSettings.FirstOrDefaultAsync(x => x.Key.ToUpper().Contains("IsStreetLevel".ToUpper()));
            if (isStreetLevel != null)
            {
                Boolean.TryParse(isStreetLevel.Value, out var _isStreetLevel);
                return _isStreetLevel;
            }
            return false;
        }

        private async Task<bool> IsVillage(Guid FormID)
        {
            var form = await _dbSetForm.Include(x => x.RefKato).FirstOrDefaultAsync(x => x.Id == FormID);
            if (form == null) return false;
            if (form.RefKato == null || !form.RefKato.KatoLevel.HasValue) return false;

            return form.RefKato.KatoLevel.Value == 1 ? false : true;
        }

        public async Task<List<FormDto>> GetFormsByKatoId(int id)
        {
            try
            {
                var culture = new CultureInfo("ru-RU");
                var list = await _dbSetForm
                        .Where(x => x.RefKatoId == id)
                        .OrderByDescending(x => x.ReportYearId)
                        .OrderByDescending(x => x.ReportMonthId)
                        //.Skip((query.PageNumber - 1) * query.PageSize)
                        //.Take(query.PageSize)
                        .Select(x => new FormDto()
                        {
                            Id = x.Id,
                            Year = x.ReportYearId,
                            MonthId = x.ReportMonthId,
                            MonthName = culture.DateTimeFormat.GetMonthName(x.ReportMonthId),
                            Status = x.RefStatus != null ? x.RefStatus.NameRu : ""
                        })
                        .ToListAsync();
                //return new PageResultDto<FormDto>(list.Count, list, query.PageNumber, query.PageSize, query.Filter);
                return list;
            }
            catch (Exception)
            {
                //return new PageResultDto<FormDto>(0, [], query.PageNumber, query.PageSize, query.Filter);
                return new List<FormDto>();
            }
        }

        public async Task<FormDto> AddForm(FormsAddDto row)
        {
            var form = await _dbSetForm
                .FirstOrDefaultAsync(x =>
                x.ReportMonthId == row.ReportMonthId &&
                x.ReportYearId == row.ReportYearId &&
                x.RefKatoId == row.RefKatoId &&
                x.IsDel == false);

            if (form != null) { throw new Exception("Отчет за данный период уже имеется"); }
            var newRow = await _dbSetForm.AddAsync(new Report_Form()
            {
                CreateDate = DateTime.UtcNow,
                Description = !String.IsNullOrEmpty(row.Description) ? row.Description : "",
                IsDel = false,
                RefKatoId = row.RefKatoId,
                RefStatusId = 1,
                SupplierId = row.SupplierId,
                ReportYearId = row.ReportYearId,
                ReportMonthId = row.ReportMonthId
            });

            await _context.SaveChangesAsync();

            var culture = new CultureInfo("ru-RU");
            return new FormDto()
            {
                Id = newRow.Entity.Id,
                Year = newRow.Entity.ReportYearId,
                MonthId = newRow.Entity.ReportMonthId,
                MonthName = culture.DateTimeFormat.GetMonthName(newRow.Entity.ReportMonthId),
                Status = "Новый",
            };
        }

        #region Водоснабжение
        #region Город
        public async Task<List<SupplyCityForm1TableDto>> SupplyCityGetForm1(Guid id)
        {
            PageQueryDto query = new PageQueryDto();
            try
            {
                var result = new List<SupplyCityForm1TableDto>();

                if (await IsStreetLevel() == true)
                {
                    var StreetBuildinsList = await GetStreetBuildingByFormId(id);

                    foreach (var item in StreetBuildinsList)
                    {
                        var row = await _dbSetForm1
                            .Include(x => x.RefBuilding)
                            .Include(x => x.RefStreet)
                            .FirstOrDefaultAsync(
                                x => x.FormId == id &&
                                x.RefStreetId == item.RefStreetId &&
                                x.RefBuildingId == item.Id &&
                                x.IsDel == false);
                        if (row != null)
                        {
                            result.Add(new SupplyCityForm1TableDto()
                            {
                                Id = row.Id,
                                FormId = row.FormId,
                                RefStreetId = row.RefStreetId,
                                RefBuildingId = row.RefBuildingId,
                                HomeAddress = row.RefBuilding?.NameRu,
                                KatoId = row.RefStreet != null ? row.RefStreet.RefKatoId : 0,
                                Street = row.RefStreet?.NameRu,
                                Volume = row.Volume,
                                HasStreets = true
                            });
                        }
                        else
                        {
                            result.Add(new SupplyCityForm1TableDto()
                            {
                                Id = Guid.NewGuid(),
                                FormId = id,
                                RefStreetId = item.RefStreetId,
                                RefBuildingId = item.Id,
                                HomeAddress = item.NameRu,
                                KatoId = item.RefStreet != null ? item.RefStreet.RefKatoId : 0,
                                Street = item.RefStreet?.NameRu,
                                Volume = 0,
                                HasStreets = true,
                            });
                        }
                    }
                }
                else
                {
                    var row = await _dbSetForm1
                            .FirstOrDefaultAsync(
                                x => x.FormId == id &&
                                x.IsDel == false);
                    if (row != null)
                    {
                        result.Add(new SupplyCityForm1TableDto()
                        {
                            Id = row.Id,
                            FormId = row.FormId,
                            RefStreetId = null,
                            RefBuildingId = null,
                            HomeAddress = string.Empty,
                            KatoId = 0,
                            Street = string.Empty,
                            Volume = row.Volume,
                            HasStreets = false,
                        });
                    }
                    else
                    {
                        result.Add(new SupplyCityForm1TableDto()
                        {
                            Id = Guid.NewGuid(),
                            FormId = id,
                            RefStreetId = null,
                            RefBuildingId = null,
                            HomeAddress = string.Empty,
                            KatoId = 0, //katoid добавить на фронте
                            Street = string.Empty,
                            Volume = 0,
                            HasStreets = false,
                        });
                    }
                }

                return result;
            }
            catch (Exception)
            {
                //return new PageResultDto<Form1TableDto>(0, [], query.PageNumber, query.PageSize, query.Filter);
                return new List<SupplyCityForm1TableDto>();
            }
        }
        public async Task<List<SupplyCityForm1TableDto>> SupplyCityUpdateForm1(List<SupplyCityForm1TableDto> list, Guid id)
        {
            if (list == null) throw new Exception("Данные не могут быть пустыми");
            if (id == Guid.Empty) throw new Exception("ИД формы не может быть пустым");

            foreach (var entity in list)
            {
                var row = await _dbSetForm1.FindAsync(entity.Id);
                if (row != null)
                {
                    row.Volume = entity.Volume;
                    row.LastModifiedDate = DateTime.UtcNow;
                    _context.Entry(row).State = EntityState.Modified;
                }
                else
                {
                    await _dbSetForm1.AddAsync(new Supply_City_Form1()
                    {
                        Id = entity.Id,
                        Volume = entity.Volume,
                        LastModifiedDate = DateTime.UtcNow,
                        CreateDate = DateTime.UtcNow,
                        IsDel = false,
                        RefBuildingId = entity.RefBuildingId.HasValue ? entity.RefBuildingId.Value : 0,
                        RefStreetId = entity.RefStreetId.HasValue ? entity.RefStreetId.Value : 0,
                        FormId = entity.FormId
                    });
                }
                await _context.SaveChangesAsync();
            }
            return list;
        }
        public async Task<List<SupplyCityForm2TableDto>> SupplyCityGetForm2(Guid id)
        {
            PageQueryDto query = new PageQueryDto();
            try
            {
                var isVill = await IsVillage(id);
                var result = new List<SupplyCityForm2TableDto>();
                if (await IsStreetLevel() == true)
                {
                    var StreetBuildinsList = await GetStreetBuildingByFormId(id);
                    foreach (var item in StreetBuildinsList)
                    {
                        var row = await _dbSetForm2
                            .Include(x => x.RefBuilding)
                            .Include(x => x.RefStreet)
                            .FirstOrDefaultAsync(
                                x => x.FormId == id &&
                                x.RefStreetId == item.RefStreetId &&
                                x.RefBuildingId == item.Id &&
                                x.IsDel == false);
                        if (row != null)
                        {

                            result.Add(new SupplyCityForm2TableDto()
                            {
                                Id = row.Id,
                                FormId = row.FormId,
                                RefStreetId = row.RefStreetId,
                                RefBuildingId = row.RefBuildingId,
                                HomeAddress = row.RefBuilding?.NameRu,
                                KatoId = row.RefStreet != null ? row.RefStreet.RefKatoId : 0,
                                Street = row.RefStreet?.NameRu,
                                IsVillage = isVill,
                                //city
                                CoverageWater = !isVill ? row.CoverageWater : null,
                                CentralizedWaterNumber = !isVill ? row.CentralizedWaterNumber : null,
                                //village
                                RuralPopulation = isVill ? row.RuralPopulation : null,
                                CentralWaterSupplySubscribers = isVill ? row.CentralWaterSupplySubscribers : null,
                                IndividualWaterMetersInstalled = isVill ? row.IndividualWaterMetersInstalled : null,
                                RemoteDataTransmissionMeters = isVill ? row.RemoteDataTransmissionMeters : null,
                            });
                        }
                        else
                        {
                            result.Add(new SupplyCityForm2TableDto()
                            {
                                Id = Guid.NewGuid(),
                                FormId = id,
                                RefStreetId = item.RefStreetId,
                                RefBuildingId = item.Id,
                                HomeAddress = item.NameRu,
                                KatoId = item.RefStreet != null ? item.RefStreet.RefKatoId : 0,
                                Street = item.RefStreet?.NameRu,
                                IsVillage = isVill,
                                //city
                                CoverageWater = !isVill ? false : null,
                                CentralizedWaterNumber = !isVill ? 0 : null,
                                //village
                                RuralPopulation = isVill ? 0 : null,
                                CentralWaterSupplySubscribers = isVill ? 0 : null,
                                IndividualWaterMetersInstalled = isVill ? 0 : null,
                                RemoteDataTransmissionMeters = isVill ? 0 : null,
                            });
                        }
                    }
                }
                else
                {
                    var row = await _dbSetForm2
                            .FirstOrDefaultAsync(
                                x => x.FormId == id &&
                                x.IsDel == false);
                    if (row != null)
                    {
                        result.Add(new SupplyCityForm2TableDto()
                        {
                            Id = row.Id,
                            FormId = row.FormId,
                            RefStreetId = null,
                            RefBuildingId = null,
                            HomeAddress = string.Empty,
                            KatoId = 0,
                            Street = string.Empty,
                            IsVillage = isVill,
                            //city
                            CoverageWater = !isVill ? row.CoverageWater : null,
                            CentralizedWaterNumber = !isVill ? row.CentralizedWaterNumber : null,
                            //village
                            RuralPopulation = isVill ? row.RuralPopulation : null,
                            CentralWaterSupplySubscribers = isVill ? row.CentralWaterSupplySubscribers : null,
                            IndividualWaterMetersInstalled = isVill ? row.IndividualWaterMetersInstalled : null,
                            RemoteDataTransmissionMeters = isVill ? row.RemoteDataTransmissionMeters : null,
                        });
                    }
                    else
                    {
                        result.Add(new SupplyCityForm2TableDto()
                        {
                            Id = Guid.NewGuid(),
                            FormId = id,
                            RefStreetId = null,
                            RefBuildingId = null,
                            HomeAddress = string.Empty,
                            KatoId = 0,
                            Street = string.Empty,
                            IsVillage = isVill,
                            //city
                            CoverageWater = !isVill ? false : null,
                            CentralizedWaterNumber = !isVill ? 0 : null,
                            //village
                            RuralPopulation = isVill ? 0 : null,
                            CentralWaterSupplySubscribers = isVill ? 0 : null,
                            IndividualWaterMetersInstalled = isVill ? 0 : null,
                            RemoteDataTransmissionMeters = isVill ? 0 : null,
                        });
                    }
                }

                return result;
            }
            catch (Exception)
            {
                return new List<SupplyCityForm2TableDto>();
            }
        }
        public async Task<List<SupplyCityForm2TableDto>> SupplyCityUpdateForm2(List<SupplyCityForm2TableDto> list, Guid id)
        {
            if (list == null || list.Count == 0) throw new Exception("Данные не могут быть пустыми");
            if (id == Guid.Empty) throw new Exception("ИД формы не может быть пустым");

            foreach (var entity in list)
            {
                var row = await _dbSetForm2.FindAsync(entity.Id);
                if (row != null)
                {
                    //city
                    row.CoverageWater = row.IsVillage ? null : entity.CoverageWater;
                    row.CentralizedWaterNumber = row.IsVillage ? null : entity.CentralizedWaterNumber;
                    //village
                    row.RuralPopulation = row.IsVillage ? entity.RuralPopulation : null;
                    row.CentralWaterSupplySubscribers = row.IsVillage ? entity.CentralWaterSupplySubscribers : null;
                    row.IndividualWaterMetersInstalled = row.IsVillage ? entity.IndividualWaterMetersInstalled : null;
                    row.RemoteDataTransmissionMeters = row.IsVillage ? entity.RemoteDataTransmissionMeters : null;
                    row.LastModifiedDate = DateTime.UtcNow;
                    _context.Entry(row).State = EntityState.Modified;
                }
                else
                {
                    await _dbSetForm2.AddAsync(new Supply_City_Form2()
                    {
                        Id = entity.Id,
                        LastModifiedDate = DateTime.UtcNow,
                        CreateDate = DateTime.UtcNow,
                        IsDel = false,
                        RefBuildingId = entity.RefBuildingId,
                        RefStreetId = entity.RefStreetId,
                        FormId = entity.FormId,
                        IsVillage = entity.IsVillage,
                        //city
                        CoverageWater = entity.IsVillage ? null : entity.CoverageWater,
                        CentralizedWaterNumber = entity.IsVillage ? null : entity.CentralizedWaterNumber,
                        //village
                        RuralPopulation = entity.IsVillage ? entity.RuralPopulation : null,
                        CentralWaterSupplySubscribers = entity.IsVillage ? entity.CentralWaterSupplySubscribers : null,
                        IndividualWaterMetersInstalled = entity.IsVillage ? entity.IndividualWaterMetersInstalled : null,
                        RemoteDataTransmissionMeters = entity.IsVillage ? entity.RemoteDataTransmissionMeters : null,
                        //Form = new Report_Form() { RefKato = new Ref_Kato(), RefStatus = new Ref_Status() },
                    });
                }
                await _context.SaveChangesAsync();
            }
            return list;
        }
        public async Task<List<SupplyCityForm3TableDto>> SupplyCityGetForm3(Guid id)
        {
            PageQueryDto query = new PageQueryDto();
            try
            {
                var isVill = await IsVillage(id);
                var result = new List<SupplyCityForm3TableDto>();
                if (await IsStreetLevel() == true)
                {
                    var StreetBuildinsList = await GetStreetBuildingByFormId(id);
                    foreach (var item in StreetBuildinsList)
                    {
                        var row = await _dbSetForm3
                            .Include(x => x.RefBuilding)
                            .Include(x => x.RefStreet)
                            .FirstOrDefaultAsync(
                                x => x.FormId == id &&
                                x.RefStreetId == item.RefStreetId &&
                                x.RefBuildingId == item.Id &&
                                x.IsDel == false);
                        if (row != null)
                        {
                            result.Add(new SupplyCityForm3TableDto()
                            {
                                Id = row.Id,
                                FormId = row.FormId,
                                RefStreetId = row.RefStreetId,
                                RefBuildingId = row.RefBuildingId,
                                HomeAddress = row.RefBuilding?.NameRu,
                                KatoId = row.RefStreet != null ? row.RefStreet.RefKatoId : 0,
                                Street = row.RefStreet?.NameRu,
                                IsVillage = isVill,
                                //city
                                CoverageMetersTotalCumulative = isVill ? null : row.CoverageMetersTotalCumulative,
                                CoverageMetersRemoteData = isVill ? null : row.CoverageMetersRemoteData,
                                //village
                                RuralPopulation = isVill ? row.RuralPopulation : null,
                                RuralSettlementsCount = isVill ? row.RuralSettlementsCount : null,
                                PopulationWithKBM = isVill ? row.PopulationWithKBM : null,
                                PopulationWithPRV = isVill ? row.PopulationWithPRV : null,
                                PopulationUsingDeliveredWater = isVill ? row.PopulationUsingDeliveredWater : null,
                                PopulationUsingWellsAndBoreholes = isVill ? row.PopulationUsingWellsAndBoreholes : null,
                                RuralSettlementsWithConstructionRefusalProtocols = isVill ? row.RuralSettlementsWithConstructionRefusalProtocols : null,
                                PopulationWithConstructionRefusalProtocols = isVill ? row.PopulationWithConstructionRefusalProtocols : null,
                            });
                        }
                        else
                        {
                            result.Add(new SupplyCityForm3TableDto()
                            {
                                Id = Guid.NewGuid(),
                                FormId = id,
                                RefStreetId = item.RefStreetId,
                                RefBuildingId = item.Id,
                                HomeAddress = item.NameRu,
                                KatoId = item.RefStreet != null ? item.RefStreet.RefKatoId : 0,
                                Street = item.RefStreet?.NameRu,
                                //city
                                CoverageMetersTotalCumulative = isVill ? null : 0,
                                CoverageMetersRemoteData = isVill ? null : 0,
                                //village
                                RuralPopulation = isVill ? 0 : null,
                                RuralSettlementsCount = isVill ? 0 : null,
                                PopulationWithKBM = isVill ? 0 : null,
                                PopulationWithPRV = isVill ? 0 : null,
                                PopulationUsingDeliveredWater = isVill ? 0 : null,
                                PopulationUsingWellsAndBoreholes = isVill ? 0 : null,
                                RuralSettlementsWithConstructionRefusalProtocols = isVill ? 0 : null,
                                PopulationWithConstructionRefusalProtocols = isVill ? 0 : null,
                            });
                        }
                    }
                }
                else
                {
                    var row = await _dbSetForm3
                    .FirstOrDefaultAsync(
                        x => x.FormId == id &&
                        x.IsDel == false);
                    if (row != null)
                    {
                        result.Add(new SupplyCityForm3TableDto()
                        {
                            Id = row.Id,
                            FormId = row.FormId,
                            RefStreetId = null,
                            RefBuildingId = null,
                            HomeAddress = string.Empty,
                            KatoId = 0,
                            Street = string.Empty,
                            IsVillage = isVill,
                            //city
                            CoverageMetersTotalCumulative = isVill ? null : row.CoverageMetersTotalCumulative,
                            CoverageMetersRemoteData = isVill ? null : row.CoverageMetersRemoteData,
                            //village
                            RuralPopulation = isVill ? row.RuralPopulation : null,
                            RuralSettlementsCount = isVill ? row.RuralSettlementsCount : null,
                            PopulationWithKBM = isVill ? row.PopulationWithKBM : null,
                            PopulationWithPRV = isVill ? row.PopulationWithPRV : null,
                            PopulationUsingDeliveredWater = isVill ? row.PopulationUsingDeliveredWater : null,
                            PopulationUsingWellsAndBoreholes = isVill ? row.PopulationUsingWellsAndBoreholes : null,
                            RuralSettlementsWithConstructionRefusalProtocols = isVill ? row.RuralSettlementsWithConstructionRefusalProtocols : null,
                            PopulationWithConstructionRefusalProtocols = isVill ? row.PopulationWithConstructionRefusalProtocols : null,
                        });
                    }
                    else
                    {
                        result.Add(new SupplyCityForm3TableDto()
                        {
                            Id = Guid.NewGuid(),
                            FormId = id,
                            RefStreetId = null,
                            RefBuildingId = null,
                            HomeAddress = string.Empty,
                            KatoId = 0,
                            Street = string.Empty,
                            IsVillage = isVill,
                            //city
                            CoverageMetersTotalCumulative = isVill ? null : 0,
                            CoverageMetersRemoteData = isVill ? null : 0,
                            //village
                            RuralPopulation = isVill ? 0 : null,
                            RuralSettlementsCount = isVill ? 0 : null,
                            PopulationWithKBM = isVill ? 0 : null,
                            PopulationWithPRV = isVill ? 0 : null,
                            PopulationUsingDeliveredWater = isVill ? 0 : null,
                            PopulationUsingWellsAndBoreholes = isVill ? 0 : null,
                            RuralSettlementsWithConstructionRefusalProtocols = isVill ? 0 : null,
                            PopulationWithConstructionRefusalProtocols = isVill ? 0 : null,
                        });
                    }
                }


                return result;
            }
            catch (Exception)
            {
                //return new PageResultDto<Form1TableDto>(0, [], query.PageNumber, query.PageSize, query.Filter);
                return new List<SupplyCityForm3TableDto>();
            }
        }
        public async Task<List<SupplyCityForm3TableDto>> SupplyCityUpdateForm3(List<SupplyCityForm3TableDto> list, Guid id)
        {
            if (list == null || list.Count == 0) throw new Exception("Данные не могут быть пустыми");
            if (id == Guid.Empty) throw new Exception("ИД формы не может быть пустым");

            foreach (var entity in list)
            {
                var row = await _dbSetForm3.FindAsync(entity.Id);
                if (row != null)
                {
                    row.LastModifiedDate = DateTime.UtcNow;
                    //city
                    row.CoverageMetersTotalCumulative = entity.IsVillage ? null : entity.CoverageMetersTotalCumulative;
                    row.CoverageMetersRemoteData = entity.IsVillage ? null : entity.CoverageMetersRemoteData;
                    //village
                    row.PopulationUsingDeliveredWater = entity.IsVillage ? entity.PopulationUsingDeliveredWater : null;
                    row.PopulationUsingWellsAndBoreholes = entity.IsVillage ? entity.PopulationUsingWellsAndBoreholes : null;
                    row.PopulationWithConstructionRefusalProtocols = entity.IsVillage ? entity.PopulationWithConstructionRefusalProtocols : null;
                    row.PopulationWithPRV = entity.IsVillage ? entity.PopulationWithPRV : null;
                    row.RuralSettlementsWithConstructionRefusalProtocols = entity.IsVillage ? entity.RuralSettlementsWithConstructionRefusalProtocols : null;
                    row.PopulationWithKBM = entity.IsVillage ? entity.PopulationWithKBM : null;
                    row.RuralPopulation = entity.IsVillage ? entity.RuralPopulation : null;
                    row.RuralSettlementsCount = entity.IsVillage ? entity.RuralSettlementsCount : null;
                    _context.Entry(row).State = EntityState.Modified;
                }
                else
                {
                    await _dbSetForm3.AddAsync(new Supply_City_Form3()
                    {
                        Id = entity.Id,
                        //city
                        CoverageMetersTotalCumulative = entity.IsVillage ? null : entity.CoverageMetersTotalCumulative,
                        CoverageMetersRemoteData = entity.IsVillage ? null : entity.CoverageMetersRemoteData,
                        LastModifiedDate = DateTime.UtcNow,
                        CreateDate = DateTime.UtcNow,
                        IsDel = false,
                        RefBuildingId = entity.RefBuildingId ?? 0,
                        RefStreetId = entity.RefStreetId ?? 0,
                        FormId = entity.FormId,
                        //village
                        PopulationUsingDeliveredWater = entity.IsVillage ? entity.PopulationUsingDeliveredWater : null,
                        PopulationUsingWellsAndBoreholes = entity.IsVillage ? entity.PopulationUsingWellsAndBoreholes : null,
                        PopulationWithConstructionRefusalProtocols = entity.IsVillage ? entity.PopulationWithConstructionRefusalProtocols : null,
                        PopulationWithPRV = entity.IsVillage ? entity.PopulationWithPRV : null,
                        RuralSettlementsWithConstructionRefusalProtocols = entity.IsVillage ? entity.RuralSettlementsWithConstructionRefusalProtocols : null,
                        PopulationWithKBM = entity.IsVillage ? entity.PopulationWithKBM : null,
                        RuralPopulation = entity.IsVillage ? entity.RuralPopulation : null,
                        RuralSettlementsCount = entity.IsVillage ? entity.RuralSettlementsCount : null
                    });
                }
                await _context.SaveChangesAsync();
            }
            return list;
        }
        public async Task<List<SupplyCityForm4TableDto>> SupplyCityGetForm4(Guid id)
        {
            PageQueryDto query = new PageQueryDto();
            try
            {
                var result = new List<SupplyCityForm4TableDto>();
                if (await IsStreetLevel() == true)
                {
                    var StreetBuildinsList = await GetStreetBuildingByFormId(id);
                    foreach (var item in StreetBuildinsList)
                    {
                        var row = await _dbSetForm4
                            .Include(x => x.RefBuilding)
                            .Include(x => x.RefStreet)
                            .FirstOrDefaultAsync(
                                x => x.FormId == id &&
                                x.RefStreetId == item.RefStreetId &&
                                x.RefBuildingId == item.Id &&
                                x.IsDel == false);
                        if (row != null)
                        {
                            result.Add(new SupplyCityForm4TableDto()
                            {
                                Id = row.Id,
                                FormId = row.FormId,
                                RefStreetId = row.RefStreetId,
                                RefBuildingId = row.RefBuildingId,
                                HomeAddress = row.RefBuilding.NameRu,
                                KatoId = row.RefStreet.RefKatoId,
                                Street = row.RefStreet.NameRu,
                                CoverageHouseholdNeedNumberBuildings = row.CoverageHouseholdNeedNumberBuildings,
                                CoverageHouseholdInstalledBuildings = row.CoverageHouseholdInstalledBuildings,
                                CoverageHouseholdInstalledCount = row.CoverageHouseholdInstalledCount,
                                CoverageHouseholdRemoteData = row.CoverageHouseholdRemoteData
                            });
                        }
                        else
                        {
                            result.Add(new SupplyCityForm4TableDto()
                            {
                                Id = Guid.NewGuid(),
                                FormId = id,
                                RefStreetId = item.RefStreetId,
                                RefBuildingId = item.Id,
                                HomeAddress = item.NameRu,
                                KatoId = item.RefStreet != null ? item.RefStreet.RefKatoId : 0,
                                Street = item.RefStreet?.NameRu,
                                CoverageHouseholdNeedNumberBuildings = 0,
                                CoverageHouseholdInstalledBuildings = 0,
                                CoverageHouseholdInstalledCount = 0,
                                CoverageHouseholdRemoteData = 0,
                            });
                        }
                    }
                }
                else
                {
                    var row = await _dbSetForm4.FirstOrDefaultAsync(x => x.FormId == id && x.IsDel == false);
                    if (row != null)
                    {
                        result.Add(new SupplyCityForm4TableDto()
                        {
                            Id = row.Id,
                            FormId = row.FormId,
                            RefStreetId = null,
                            RefBuildingId = null,
                            HomeAddress = string.Empty,
                            KatoId = 0,
                            Street = string.Empty,
                            CoverageHouseholdNeedNumberBuildings = row.CoverageHouseholdNeedNumberBuildings,
                            CoverageHouseholdInstalledBuildings = row.CoverageHouseholdInstalledBuildings,
                            CoverageHouseholdInstalledCount = row.CoverageHouseholdInstalledCount,
                            CoverageHouseholdRemoteData = row.CoverageHouseholdRemoteData
                        });
                    }
                    else
                    {
                        result.Add(new SupplyCityForm4TableDto()
                        {
                            Id = Guid.NewGuid(),
                            FormId = id,
                            RefStreetId = null,
                            RefBuildingId = null,
                            HomeAddress = string.Empty,
                            KatoId = 0,
                            Street = string.Empty,
                            CoverageHouseholdNeedNumberBuildings = 0,
                            CoverageHouseholdInstalledBuildings = 0,
                            CoverageHouseholdInstalledCount = 0,
                            CoverageHouseholdRemoteData = 0,
                        });
                    }
                }

                return result;
            }
            catch (Exception)
            {
                //return new PageResultDto<Form1TableDto>(0, [], query.PageNumber, query.PageSize, query.Filter);
                return new List<SupplyCityForm4TableDto>();
            }
        }
        public async Task<List<SupplyCityForm4TableDto>> SupplyCityUpdateForm4(List<SupplyCityForm4TableDto> list, Guid id)
        {
            if (list == null || list.Count == 0) throw new Exception("Данные не могут быть пустыми");
            if (id == Guid.Empty) throw new Exception("ИД формы не может быть пустым");

            foreach (var entity in list)
            {
                var row = await _dbSetForm4.FindAsync(entity.Id);
                if (row != null)
                {
                    row.CoverageHouseholdNeedNumberBuildings = entity.CoverageHouseholdNeedNumberBuildings;
                    row.CoverageHouseholdInstalledBuildings = entity.CoverageHouseholdInstalledBuildings;
                    row.CoverageHouseholdInstalledCount = entity.CoverageHouseholdInstalledCount;
                    row.CoverageHouseholdRemoteData = entity.CoverageHouseholdRemoteData;
                    row.LastModifiedDate = DateTime.UtcNow;
                    _context.Entry(row).State = EntityState.Modified;
                }
                else
                {
                    await _dbSetForm4.AddAsync(new Supply_City_Form4()
                    {
                        Id = entity.Id,
                        CoverageHouseholdNeedNumberBuildings = entity.CoverageHouseholdNeedNumberBuildings,
                        CoverageHouseholdInstalledBuildings = entity.CoverageHouseholdInstalledBuildings,
                        CoverageHouseholdInstalledCount = entity.CoverageHouseholdInstalledCount,
                        CoverageHouseholdRemoteData = entity.CoverageHouseholdRemoteData,
                        LastModifiedDate = DateTime.UtcNow,
                        CreateDate = DateTime.UtcNow,
                        IsDel = false,
                        RefBuildingId = entity.RefBuildingId != null ? entity.RefBuildingId.Value : 0,
                        RefStreetId = entity.RefStreetId != null ? entity.RefStreetId.Value : 0,
                        FormId = entity.FormId,
                    });
                }
                await _context.SaveChangesAsync();
            }
            return list;
        }
        public async Task<List<SupplyCityForm5TableDto>> SupplyCityGetForm5(Guid id)
        {
            PageQueryDto query = new PageQueryDto();
            try
            {
                var result = new List<SupplyCityForm5TableDto>();
                if (await IsStreetLevel() == true)
                {
                    var StreetBuildinsList = await GetStreetBuildingByFormId(id);
                    foreach (var item in StreetBuildinsList)
                    {
                        var row = await _dbSetForm5
                            .Include(x => x.RefBuilding)
                            .Include(x => x.RefStreet)
                            .FirstOrDefaultAsync(
                                x => x.FormId == id &&
                                x.RefStreetId == item.RefStreetId &&
                                x.RefBuildingId == item.Id &&
                                x.IsDel == false);
                        if (row != null)
                        {
                            result.Add(new SupplyCityForm5TableDto()
                            {
                                Id = row.Id,
                                FormId = row.FormId,
                                RefStreetId = row.RefStreetId,
                                RefBuildingId = row.RefBuildingId,
                                HomeAddress = row.RefBuilding?.NameRu,
                                KatoId = row.RefStreet != null ? row.RefStreet.RefKatoId : 0,
                                Street = row.RefStreet?.NameRu,
                                ScadaWaterIntake = row.ScadaWaterIntake,
                                ScadaWaterTreatment = row.ScadaWaterTreatment,
                                ScadaStations = row.ScadaStations,
                                ScadaSupplyNetworks = row.ScadaSupplyNetworks
                            });
                        }
                        else
                        {
                            result.Add(new SupplyCityForm5TableDto()
                            {
                                Id = Guid.NewGuid(),
                                FormId = id,
                                RefStreetId = item.RefStreetId,
                                RefBuildingId = item.Id,
                                HomeAddress = item.NameRu,
                                KatoId = row.RefStreet != null ? row.RefStreet.RefKatoId : 0,
                                Street = item.RefStreet?.NameRu,
                                ScadaWaterIntake = false,
                                ScadaWaterTreatment = false,
                                ScadaStations = false,
                                ScadaSupplyNetworks = false,
                            });
                        }
                    }
                }
                else
                {
                    var row = await _dbSetForm5.FirstOrDefaultAsync(x => x.FormId == id && x.IsDel == false);
                    if (row != null)
                    {
                        result.Add(new SupplyCityForm5TableDto()
                        {
                            Id = row.Id,
                            FormId = row.FormId,
                            RefStreetId = null,
                            RefBuildingId = null,
                            HomeAddress = string.Empty,
                            KatoId = 0,
                            Street = string.Empty,
                            ScadaWaterIntake = row.ScadaWaterIntake,
                            ScadaWaterTreatment = row.ScadaWaterTreatment,
                            ScadaStations = row.ScadaStations,
                            ScadaSupplyNetworks = row.ScadaSupplyNetworks
                        });
                    }
                    else
                    {
                        result.Add(new SupplyCityForm5TableDto()
                        {
                            Id = Guid.NewGuid(),
                            FormId = id,
                            RefStreetId = null,
                            RefBuildingId = null,
                            HomeAddress = string.Empty,
                            KatoId = 0,
                            Street = string.Empty,
                            ScadaWaterIntake = false,
                            ScadaWaterTreatment = false,
                            ScadaStations = false,
                            ScadaSupplyNetworks = false,
                        });
                    }
                }


                return result;
            }
            catch (Exception)
            {
                //return new PageResultDto<Form1TableDto>(0, [], query.PageNumber, query.PageSize, query.Filter);
                return new List<SupplyCityForm5TableDto>();
            }
        }
        public async Task<List<SupplyCityForm5TableDto>> SupplyCityUpdateForm5(List<SupplyCityForm5TableDto> list, Guid id)
        {
            if (list == null || list.Count == 0) throw new Exception("Данные не могут быть пустыми");
            if (id == Guid.Empty) throw new Exception("ИД формы не может быть пустым");

            foreach (var entity in list)
            {
                var row = await _dbSetForm5.FindAsync(entity.Id);
                if (row != null)
                {
                    row.ScadaWaterIntake = entity.ScadaWaterIntake;
                    row.ScadaWaterTreatment = entity.ScadaWaterTreatment;
                    row.ScadaStations = entity.ScadaStations;
                    row.ScadaSupplyNetworks = entity.ScadaSupplyNetworks;
                    row.LastModifiedDate = DateTime.UtcNow;
                    _context.Entry(row).State = EntityState.Modified;
                }
                else
                {
                    await _dbSetForm5.AddAsync(new Supply_City_Form5()
                    {
                        Id = entity.Id,
                        ScadaWaterIntake = entity.ScadaWaterIntake,
                        ScadaWaterTreatment = entity.ScadaWaterTreatment,
                        ScadaStations = entity.ScadaStations,
                        ScadaSupplyNetworks = entity.ScadaSupplyNetworks,
                        LastModifiedDate = DateTime.UtcNow,
                        CreateDate = DateTime.UtcNow,
                        IsDel = false,
                        RefBuildingId = entity.RefBuildingId != null ? entity.RefBuildingId.Value : 0,
                        RefStreetId = entity.RefStreetId != null ? entity.RefStreetId.Value : 0,
                        FormId = entity.FormId,
                    });
                }
                await _context.SaveChangesAsync();
            }
            return list;
        }
        #endregion
        #region Село
        public async Task<List<SupplyCityForm1TableDto>> SupplyVillageGetForm1(Guid id)
        {
            PageQueryDto query = new PageQueryDto();
            try
            {
                var result = new List<SupplyCityForm1TableDto>(); //потому что структуры с городом одинаковы, без наличия улиц
                var form = await _dbSetForm.FirstOrDefaultAsync(x => x.Id == id);
                var curForm = await _dbSetForm1.FirstOrDefaultAsync(x => x.FormId == id && x.IsDel == false); //_dbSetFormVill1.FirstOrDefaultAsync(x => x.FormId == id && x.IsDel == false);

                if (form == null) throw new Exception("Форма не найдена");

                if (curForm == null)
                {
                    result.Add(new SupplyCityForm1TableDto()
                    {
                        FormId = id,
                        KatoId = form.RefKatoId,
                        Volume = 0,
                    });
                    return result;
                }
                return new List<SupplyCityForm1TableDto>()
                {
                    new SupplyCityForm1TableDto()
                    {
                        FormId = curForm.FormId,
                        KatoId = form.RefKatoId,
                        Volume = curForm.Volume,
                    }
                };

            }
            catch (Exception)
            {
                //return new PageResultDto<Form1TableDto>(0, [], query.PageNumber, query.PageSize, query.Filter);
                return new List<SupplyCityForm1TableDto>();
            }
        }
        public async Task<List<SupplyCityForm1TableDto>> SupplyVillageUpdateForm1(List<SupplyCityForm1TableDto> list, Guid id)
        {
            if (list == null || list.Count == 0) throw new Exception("Данные не могут быть пустыми");
            if (id == Guid.Empty) throw new Exception("ИД формы не может быть пустым");

            foreach (var entity in list)
            {
                var row = await _dbSetForm1.FindAsync(entity.Id);
                if (row != null)
                {
                    row.Volume = entity.Volume;
                    row.LastModifiedDate = DateTime.UtcNow;
                    _context.Entry(row).State = EntityState.Modified;
                }
                else
                {
                    await _dbSetForm1.AddAsync(new Supply_City_Form1()
                    {
                        Id = entity.Id,
                        Volume = entity.Volume,
                        CreateDate = DateTime.UtcNow,
                        IsDel = false,
                        FormId = entity.FormId,
                    });
                }
                await _context.SaveChangesAsync();
            }
            return list;
        }
        public async Task<List<SupplyCityForm2TableDto>> SupplyVillageGetForm2(Guid id)
        {
            PageQueryDto query = new PageQueryDto();
            try
            {
                var result = new List<SupplyCityForm2TableDto>(); //потому что структуры с городом одинаковы, без наличия улиц SupplyVillageForm2TableDto
                var form = await _dbSetForm.FirstOrDefaultAsync(x => x.Id == id);
                var curForm = await _dbSetForm2.FirstOrDefaultAsync(x => x.FormId == id && x.IsDel == false);//_dbSetFormVill2

                if (form == null) throw new Exception("Форма не найдена");

                if (curForm == null)
                {
                    result.Add(new SupplyCityForm2TableDto()//SupplyVillageForm2TableDto
                    {
                        FormId = id,
                        KatoId = form.RefKatoId,
                        RuralPopulation = 0,
                        CentralWaterSupplySubscribers = 0,
                        IndividualWaterMetersInstalled = 0,
                        RemoteDataTransmissionMeters = 0,
                        Id = Guid.NewGuid(),
                    });
                    return result;
                }
                return new List<SupplyCityForm2TableDto>()//SupplyVillageForm2TableDto
                {
                    new SupplyCityForm2TableDto()
                    {
                        FormId = curForm.FormId,
                        KatoId = form.RefKatoId,
                        RuralPopulation = curForm.RuralPopulation,
                        CentralWaterSupplySubscribers = curForm.CentralWaterSupplySubscribers,
                        IndividualWaterMetersInstalled = curForm.IndividualWaterMetersInstalled,
                        RemoteDataTransmissionMeters = curForm.RemoteDataTransmissionMeters,
                        Id = curForm.Id,
                    }
                };

            }
            catch (Exception)
            {
                //return new PageResultDto<Form1TableDto>(0, [], query.PageNumber, query.PageSize, query.Filter);
                return new List<SupplyCityForm2TableDto>();
            }
        }
        public async Task<List<SupplyCityForm2TableDto>> SupplyVillageUpdateForm2(List<SupplyCityForm2TableDto> list, Guid id)
        {
            if (list == null || list.Count == 0) throw new Exception("Данные не могут быть пустыми");
            if (id == Guid.Empty) throw new Exception("ИД формы не может быть пустым");

            foreach (var entity in list)
            {
                var row = await _dbSetForm2.FindAsync(entity.Id);//_dbSetFormVill2
                if (row != null)
                {
                    row.RuralPopulation = entity.RuralPopulation;
                    row.CentralWaterSupplySubscribers = entity.CentralWaterSupplySubscribers;
                    row.IndividualWaterMetersInstalled = entity.IndividualWaterMetersInstalled;
                    row.RemoteDataTransmissionMeters = entity.RemoteDataTransmissionMeters;
                    row.LastModifiedDate = DateTime.UtcNow;
                    _context.Entry(row).State = EntityState.Modified;
                }
                else
                {
                    await _dbSetForm2.AddAsync(new Supply_City_Form2()//Supply_Village_Form2
                    {
                        Id = Guid.NewGuid(),
                        RuralPopulation = entity.RuralPopulation,
                        CentralWaterSupplySubscribers = entity.CentralWaterSupplySubscribers,
                        IndividualWaterMetersInstalled = entity.IndividualWaterMetersInstalled,
                        RemoteDataTransmissionMeters = entity.RemoteDataTransmissionMeters,
                        CreateDate = DateTime.UtcNow,
                        IsDel = false,
                        FormId = entity.FormId,
                    });
                }
                await _context.SaveChangesAsync();
            }
            return list;
        }
        public async Task<List<SupplyCityForm3TableDto>> SupplyVillageGetForm3(Guid id)//SupplyVillageForm3TableDto
        {
            PageQueryDto query = new PageQueryDto();
            try
            {
                var result = new List<SupplyCityForm3TableDto>(); //потому что структуры с городом одинаковы, без наличия улиц
                var form = await _dbSetForm.FirstOrDefaultAsync(x => x.Id == id);
                var curForm = await _dbSetForm3.FirstOrDefaultAsync(x => x.FormId == id && x.IsDel == false);//_dbSetFormVill3

                if (form == null) throw new Exception("Форма не найдена");

                if (curForm == null)
                {
                    result.Add(new SupplyCityForm3TableDto()
                    {
                        FormId = id,
                        KatoId = form.RefKatoId,
                        RuralPopulation = 0,
                        RuralSettlementsCount = 0,
                        PopulationWithKBM = 0,
                        PopulationWithPRV = 0,
                        PopulationUsingDeliveredWater = 0,
                        PopulationUsingWellsAndBoreholes = 0,
                        RuralSettlementsWithConstructionRefusalProtocols = 0,
                        PopulationWithConstructionRefusalProtocols = 0,
                        Id = Guid.NewGuid(),
                    });
                    return result;
                }
                else
                {
                    return new List<SupplyCityForm3TableDto>()
                    {
                        new SupplyCityForm3TableDto()
                        {
                            Id = curForm.Id,
                            FormId = curForm.FormId,
                            KatoId = form.RefKatoId,
                            RuralPopulation = curForm.RuralPopulation,
                            RuralSettlementsCount = curForm.RuralSettlementsCount,
                            PopulationWithKBM = curForm.PopulationWithKBM,
                            PopulationWithPRV = curForm.PopulationWithPRV,
                            PopulationUsingDeliveredWater = curForm.PopulationUsingDeliveredWater,
                            PopulationUsingWellsAndBoreholes = curForm.PopulationUsingWellsAndBoreholes,
                            RuralSettlementsWithConstructionRefusalProtocols = curForm.RuralSettlementsWithConstructionRefusalProtocols,
                            PopulationWithConstructionRefusalProtocols = curForm.PopulationWithConstructionRefusalProtocols,
                        }
                    };

                }

            }
            catch (Exception)
            {
                //return new PageResultDto<Form1TableDto>(0, [], query.PageNumber, query.PageSize, query.Filter);
                return new List<SupplyCityForm3TableDto>();
            }
        }
        public async Task<List<SupplyCityForm3TableDto>> SupplyVillageUpdateForm3(List<SupplyCityForm3TableDto> list, Guid id)
        {
            if (list == null || list.Count == 0) throw new Exception("Данные не могут быть пустыми");
            if (id == Guid.Empty) throw new Exception("ИД формы не может быть пустым");

            foreach (var entity in list)
            {
                var row = await _dbSetForm3.FindAsync(entity.Id);//_dbSetFormVill3
                if (row != null)
                {
                    row.RuralPopulation = entity.RuralPopulation;
                    row.RuralSettlementsCount = entity.RuralSettlementsCount;
                    row.PopulationWithKBM = entity.PopulationWithKBM;
                    row.PopulationWithPRV = entity.PopulationWithPRV;
                    row.PopulationUsingDeliveredWater = entity.PopulationUsingDeliveredWater;
                    row.PopulationUsingWellsAndBoreholes = entity.PopulationUsingWellsAndBoreholes;
                    row.RuralSettlementsWithConstructionRefusalProtocols = entity.RuralSettlementsWithConstructionRefusalProtocols;
                    row.PopulationWithConstructionRefusalProtocols = entity.PopulationWithConstructionRefusalProtocols;
                    row.LastModifiedDate = DateTime.UtcNow;
                    _context.Entry(row).State = EntityState.Modified;
                }
                else
                {
                    await _dbSetForm3.AddAsync(new Supply_City_Form3()//Supply_Village_Form3
                    {
                        Id = Guid.NewGuid(),
                        RuralPopulation = entity.RuralPopulation,
                        RuralSettlementsCount = entity.RuralSettlementsCount,
                        PopulationWithKBM = entity.PopulationWithKBM,
                        PopulationWithPRV = entity.PopulationWithPRV,
                        PopulationUsingDeliveredWater = entity.PopulationUsingDeliveredWater,
                        PopulationUsingWellsAndBoreholes = entity.PopulationUsingWellsAndBoreholes,
                        RuralSettlementsWithConstructionRefusalProtocols = entity.RuralSettlementsWithConstructionRefusalProtocols,
                        PopulationWithConstructionRefusalProtocols = entity.PopulationWithConstructionRefusalProtocols,
                        CreateDate = DateTime.UtcNow,
                        IsDel = false,
                        FormId = entity.FormId,
                    });
                }
                await _context.SaveChangesAsync();
            }
            return list;
        }
        #endregion
        #endregion
        #region Водоотведение
        #region Город 1
        #region Форма 1
        public async Task<List<WasteCityForm1TableDto>> WasteCityGetForm1(Guid id)
        {
            PageQueryDto query = new PageQueryDto();
            try
            {
                var result = new List<WasteCityForm1TableDto>();
                var StreetBuildinsList = await GetStreetBuildingByFormId(id);
                foreach (var item in StreetBuildinsList)
                {
                    var row = await _dbSetWForm1
                        .Include(x => x.RefBuilding)
                        .Include(x => x.RefStreet)
                        .FirstOrDefaultAsync(
                            x => x.FormId == id &&
                            x.RefStreetId == item.RefStreetId &&
                            x.RefBuildingId == item.Id &&
                            x.IsDel == false);
                    if (row != null)
                    {
                        result.Add(new WasteCityForm1TableDto()
                        {
                            Id = row.Id,
                            FormId = row.FormId,
                            RefStreetId = row.RefStreetId,
                            RefBuildingId = row.RefBuildingId,
                            HomeAddress = row.RefBuilding.NameRu,
                            KatoId = row.RefStreet.RefKatoId,
                            Street = row.RefStreet.NameRu,
                            WaterVolume = row.WaterVolume,
                        });
                    }
                    else
                    {
                        result.Add(new WasteCityForm1TableDto()
                        {
                            Id = Guid.NewGuid(),
                            FormId = id,
                            RefStreetId = item.RefStreetId,
                            RefBuildingId = item.Id,
                            HomeAddress = item.NameRu,
                            KatoId = item.RefStreet.RefKatoId,
                            Street = item.RefStreet.NameRu,
                            WaterVolume = 0,
                        });
                    }
                }

                return result;
            }
            catch (Exception)
            {
                //return new PageResultDto<Form1TableDto>(0, [], query.PageNumber, query.PageSize, query.Filter);
                return new List<WasteCityForm1TableDto>();
            }
        }
        public async Task<List<WasteCityForm1TableDto>> WasteCityUpdateForm1(List<WasteCityForm1TableDto> list, Guid id)
        {
            if (list == null || list.Count == 0) throw new Exception("Данные не могут быть пустыми");
            if (id == Guid.Empty) throw new Exception("ИД формы не может быть пустым");

            foreach (var entity in list)
            {
                var row = await _dbSetWForm1.FindAsync(entity.Id);
                if (row != null)
                {
                    row.WaterVolume = entity.WaterVolume;
                    row.LastModifiedDate = DateTime.UtcNow;
                    _context.Entry(row).State = EntityState.Modified;
                }
                else
                {
                    await _dbSetWForm1.AddAsync(new Waste_City_Form1()
                    {
                        Id = entity.Id,
                        WaterVolume = entity.WaterVolume,
                        LastModifiedDate = DateTime.UtcNow,
                        CreateDate = DateTime.UtcNow,
                        IsDel = false,
                        RefBuildingId = entity.RefBuildingId.Value,
                        RefStreetId = entity.RefStreetId.Value,
                        FormId = entity.FormId.Value,
                    });
                }
                await _context.SaveChangesAsync();
            }
            return list;
        }
        #endregion
        #region Форма 2
        public async Task<List<WasteCityForm2TableDto>> WasteCityGetForm2(Guid id)
        {
            PageQueryDto query = new PageQueryDto();
            try
            {
                var result = new List<WasteCityForm2TableDto>();
                var StreetBuildinsList = await GetStreetBuildingByFormId(id);
                foreach (var item in StreetBuildinsList)
                {
                    var row = await _dbSetWForm2
                        .Include(x => x.RefBuilding)
                        .Include(x => x.RefStreet)
                        .FirstOrDefaultAsync(
                            x => x.FormId == id &&
                            x.RefStreetId == item.RefStreetId &&
                            x.RefBuildingId == item.Id &&
                            x.IsDel == false);
                    if (row != null)
                    {
                        result.Add(new WasteCityForm2TableDto()
                        {
                            Id = row.Id,
                            FormId = row.FormId,
                            RefStreetId = row.RefStreetId,
                            RefBuildingId = row.RefBuildingId,
                            HomeAddress = row.RefBuilding.NameRu,
                            KatoId = row.RefStreet.RefKatoId,
                            Street = row.RefStreet.NameRu,
                            IsConnectedToCentralizedWastewaterSystem = row.IsConnectedToCentralizedWastewaterSystem,
                            HasSewageTreatmentFacilities = row.HasSewageTreatmentFacilities,
                            HasMechanicalTreatment = row.HasMechanicalTreatment,
                            HasMechanicalAndBiologicalTreatment = row.HasMechanicalAndBiologicalTreatment,
                            PopulationCoveredByCentralizedWastewater = row.PopulationCoveredByCentralizedWastewater,
                        });
                    }
                    else
                    {
                        result.Add(new WasteCityForm2TableDto()
                        {
                            Id = Guid.NewGuid(),
                            FormId = id,
                            RefStreetId = item.RefStreetId,
                            RefBuildingId = item.Id,
                            HomeAddress = item.NameRu,
                            KatoId = item.RefStreet.RefKatoId,
                            Street = item.RefStreet.NameRu,
                            IsConnectedToCentralizedWastewaterSystem = false,
                            HasSewageTreatmentFacilities = false,
                            HasMechanicalTreatment = false,
                            HasMechanicalAndBiologicalTreatment = false,
                            PopulationCoveredByCentralizedWastewater = 0,
                        });
                    }
                }

                return result;
            }
            catch (Exception)
            {
                //return new PageResultDto<Form1TableDto>(0, [], query.PageNumber, query.PageSize, query.Filter);
                return new List<WasteCityForm2TableDto>();
            }
        }
        public async Task<List<WasteCityForm2TableDto>> WasteCityUpdateForm2(List<WasteCityForm2TableDto> list, Guid id)
        {
            if (list == null || list.Count == 0) throw new Exception("Данные не могут быть пустыми");
            if (id == Guid.Empty) throw new Exception("ИД формы не может быть пустым");

            foreach (var entity in list)
            {
                var row = await _dbSetWForm2.FindAsync(entity.Id);
                if (row != null)
                {
                    row.IsConnectedToCentralizedWastewaterSystem = entity.IsConnectedToCentralizedWastewaterSystem;
                    row.HasSewageTreatmentFacilities = entity.HasSewageTreatmentFacilities;
                    row.HasMechanicalTreatment = entity.HasMechanicalTreatment;
                    row.HasMechanicalAndBiologicalTreatment = entity.HasMechanicalAndBiologicalTreatment;
                    row.PopulationCoveredByCentralizedWastewater = entity.PopulationCoveredByCentralizedWastewater;
                    row.LastModifiedDate = DateTime.UtcNow;
                    _context.Entry(row).State = EntityState.Modified;
                }
                else
                {
                    await _dbSetWForm2.AddAsync(new Waste_City_Form2()
                    {
                        Id = entity.Id,
                        IsConnectedToCentralizedWastewaterSystem = entity.IsConnectedToCentralizedWastewaterSystem,
                        HasSewageTreatmentFacilities = entity.HasSewageTreatmentFacilities,
                        HasMechanicalTreatment = entity.HasMechanicalTreatment,
                        HasMechanicalAndBiologicalTreatment = entity.HasMechanicalAndBiologicalTreatment,
                        PopulationCoveredByCentralizedWastewater = entity.PopulationCoveredByCentralizedWastewater,
                        LastModifiedDate = DateTime.UtcNow,
                        CreateDate = DateTime.UtcNow,
                        IsDel = false,
                        RefBuildingId = entity.RefBuildingId ?? 0,
                        RefStreetId = entity.RefStreetId ?? 0,
                        FormId = entity.FormId.Value,
                    });
                }
                await _context.SaveChangesAsync();
            }
            return list;
        }
        #endregion
        #region Форма 3
        public async Task<List<WasteCityForm3TableDto>> WasteCityGetForm3(Guid id)
        {
            PageQueryDto query = new PageQueryDto();
            try
            {
                var result = new List<WasteCityForm3TableDto>();
                var form = await _dbSetForm.FirstOrDefaultAsync(x => x.Id == id);
                if (form == null)
                {
                    throw new Exception("Форма отсутствует");
                }
                var StreetBuildinsList = await _dbSetRefStreet.Where(x => x.IsDel == false && x.RefKatoId == form.RefKatoId).ToListAsync();
                foreach (var item in StreetBuildinsList)
                {
                    var row = await _dbSetWForm3
                        .Include(x => x.RefStreet)
                        .FirstOrDefaultAsync(
                            x => x.FormId == id &&
                            x.RefStreetId == item.Id &&
                            x.IsDel == false);
                    if (row != null)
                    {
                        result.Add(new WasteCityForm3TableDto()
                        {
                            Id = row.Id,
                            FormId = row.FormId,
                            RefStreetId = row.RefStreetId,
                            KatoId = row.RefStreet.RefKatoId,
                            Street = row.RefStreet.NameRu,
                            HasSewerNetworks = row.HasSewerNetworks,
                            HasSewagePumpingStations = row.HasSewagePumpingStations,
                            HasSewageTreatmentPlants = row.HasSewageTreatmentPlants,
                        });
                    }
                    else
                    {
                        result.Add(new WasteCityForm3TableDto()
                        {
                            Id = Guid.NewGuid(),
                            FormId = id,
                            RefStreetId = item.Id,
                            KatoId = item.RefKatoId,
                            Street = item.NameRu,
                            HasSewerNetworks = false,
                            HasSewagePumpingStations = false,
                            HasSewageTreatmentPlants = false,
                        });
                    }
                }

                return result;
            }
            catch (Exception)
            {
                //return new PageResultDto<Form1TableDto>(0, [], query.PageNumber, query.PageSize, query.Filter);
                return new List<WasteCityForm3TableDto>();
            }
        }
        public async Task<List<WasteCityForm3TableDto>> WasteCityUpdateForm3(List<WasteCityForm3TableDto> list, Guid id)
        {
            if (list == null || list.Count == 0) throw new Exception("Данные не могут быть пустыми");
            if (id == Guid.Empty) throw new Exception("ИД формы не может быть пустым");

            foreach (var entity in list)
            {
                var row = await _dbSetWForm3.FindAsync(entity.Id);
                if (row != null)
                {
                    row.HasSewerNetworks = entity.HasSewerNetworks;
                    row.HasSewagePumpingStations = entity.HasSewagePumpingStations;
                    row.HasSewageTreatmentPlants = entity.HasSewageTreatmentPlants;
                    row.LastModifiedDate = DateTime.UtcNow;
                    _context.Entry(row).State = EntityState.Modified;
                }
                else
                {
                    await _dbSetWForm3.AddAsync(new Waste_City_Form3()
                    {
                        Id = entity.Id,
                        HasSewerNetworks = entity.HasSewerNetworks,
                        HasSewagePumpingStations = entity.HasSewagePumpingStations,
                        HasSewageTreatmentPlants = entity.HasSewageTreatmentPlants,
                        LastModifiedDate = DateTime.UtcNow,
                        CreateDate = DateTime.UtcNow,
                        IsDel = false,
                        RefStreetId = entity.RefStreetId ?? 0,
                        FormId = entity.FormId ?? Guid.Empty
                    });
                }
                await _context.SaveChangesAsync();
            }
            return list;
        }

        public Task<List<ApprovedForm>> GetForms()
        {
            throw new NotImplementedException();
        }

        public Task<ApprovedForm> Add(ApprovedForm aForm)
        {
            throw new NotImplementedException();
        }

        public Task<ApprovedForm> Update(ApprovedForm aForm)
        {
            throw new NotImplementedException();
        }

        public Task<ApprovedForm> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion
        #endregion

    }
#endif
}