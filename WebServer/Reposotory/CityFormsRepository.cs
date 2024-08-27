using Microsoft.EntityFrameworkCore;
using WebServer.Data;
using WebServer.Interfaces;
using WebServer.Models;

namespace WebServer.Reposotory
{
    public class CityFormsRepository : ICityForms
    {
        private readonly WaterDbContext _context;
        private readonly DbSet<CityForm> _dbSetForm;
        private readonly DbSet<CityDocument> _dbSetDoc;        
        private readonly DbSet<Ref_Kato> _dbSetKato;
        private readonly DbSet<Account> _dbSetAccount;

        public CityFormsRepository(WaterDbContext context)
        {
            _context = context;
            _dbSetForm = _context.Set<CityForm>();
            _dbSetDoc = _context.Set<CityDocument>();            
            _dbSetKato = _context.Set<Ref_Kato>();
            _dbSetAccount = _context.Set<Account>();
        }

        public async Task<object> GetCityDocument(string katoKod)
        {
            if (!_dbSetKato.Any(x => x.Code.ToString() == katoKod)) throw new Exception("NotFound");
            var isReportable = await _dbSetKato.Where(x => x.Code.ToString() == katoKod).Select(x => x.IsReportable).FirstOrDefaultAsync();
            if (!isReportable) return "NotReporting";

            return await _dbSetDoc.Where(x => x.KodNaselPunk == katoKod).ToListAsync();
        }

        public async Task<List<CityDocument>> GetCityDocumentByParams(string? kodOblast, string? kodRaion, int? year)
        {
            var query = _dbSetDoc.AsQueryable();
            if (!string.IsNullOrEmpty(kodOblast))
            {
                query = query.Where(x => x.KodOblast == kodOblast);
            }
            if (!string.IsNullOrEmpty(kodRaion))
            {
                query = query.Where(x => x.KodRaiona == kodRaion);
            }
            if (year.HasValue)
            {
                query = query.Where(x => x.Year == year);
            }
            var result = await query.ToListAsync();
            return result;
        }

        public async Task<object> GetCityFormsByDocId(Guid idDoc)
        {
            if (!_dbSetDoc.Any(x => x.Id == idDoc)) throw new Exception("NotFound");
            return await _dbSetForm.Where(x => x.DocumentId == idDoc).ToListAsync();
        }

        public async Task<CityDocument> AddCityDocument(CityDocument cityDocument)
        {
            var loginKatoCode = await _dbSetAccount.Where(x => x.Login == cityDocument.Login).Select(x => x.KatoCode).FirstOrDefaultAsync();
            var loginKato = await _dbSetKato.FirstOrDefaultAsync(x => x.Code == loginKatoCode);
            var loginOblast = await FindParentRecordAsync(loginKato?.Id ?? 0, 0);

            var cityDocumentKato = await _dbSetKato.FirstOrDefaultAsync(x => x.Code.ToString() == cityDocument.KodNaselPunk);
            var cityDocumentOblast = await FindParentRecordAsync(cityDocumentKato?.Id ?? 0, 0);

            if (loginOblast?.Id != cityDocumentOblast?.Id) throw new Exception("Можно создавать документ только в рамках своей области!");

            if (await _dbSetDoc.AnyAsync(x => x.Year == cityDocument.Year && x.KodNaselPunk == cityDocument.KodNaselPunk))
                throw new Exception("За указанный год и насленный пункт уже имеется отчет!");
            await _dbSetDoc.AddAsync(cityDocument);
            await _context.SaveChangesAsync();
            return cityDocument;
        }

        public async Task<Ref_Kato?> FindParentRecordAsync(int parentId, int katoLevel)
        {
            var currentRecord = await _dbSetKato.Where(x => x.Id == parentId).FirstOrDefaultAsync();
            if (currentRecord != null && currentRecord.KatoLevel != katoLevel && currentRecord.ParentId != 0)
            {
                return await FindParentRecordAsync(currentRecord.ParentId, katoLevel);
            }
            return currentRecord;
        }

        public async Task<object> GetCityFormsByKodYear(string kodNaselPunk, int year)
        {
            if (!_dbSetKato.Any(x => x.Code.ToString() == kodNaselPunk)) throw new Exception("NotFound");
            var isReportable = await _dbSetKato.Where(x => x.Code.ToString() == kodNaselPunk).Select(x => x.IsReportable).FirstOrDefaultAsync();
            if (!isReportable) return "NotReporting";

            return await _dbSetDoc.Where(x=>x.KodNaselPunk==kodNaselPunk&&x.Year==year)
                .Select(x=>x.CityForm).FirstOrDefaultAsync();
        }

        public async Task<CityForm> AddCityForms(Guid idDoc, CityForm cityForms)
        {
            var doc = await _dbSetDoc.FindAsync(idDoc);
            if (doc == null) throw new Exception("Документа с таким Id не существует");

            if (doc.CityFormId != null) throw new Exception("У данного документа уже есть форма");

            await _dbSetForm.AddAsync(cityForms);
            await _context.SaveChangesAsync();

            doc.CityFormId = cityForms.Id;

            await _context.SaveChangesAsync();

            return cityForms;
        }

        public async Task<CityForm> GetCityFormById(Guid id)
        {
            return await _dbSetForm.FindAsync(id);
        }

        public async Task<CityForm> UpdateCityForm(CityForm cityForm)
        {
            _dbSetForm.Update(cityForm);
            await _context.SaveChangesAsync();
            return cityForm;
        }

        //public async Task<CityWaterSupply> GetWaterSupply(Guid idForm)
        //{
        //    return await _dbSetSupply.FirstOrDefaultAsync(x => x.IdForm == idForm);
        //}

        //public async Task<CityWaterSupply> AddWaterSupply(Guid idForm, CityWaterSupply waterSupplyInfo)
        //{
        //    var cityForm = await _dbSetForm.FindAsync(idForm);
        //    if (cityForm == null) throw new Exception("Форма не найдена");
        //    waterSupplyInfo.IdForm = idForm;
        //    _dbSetSupply.Add(waterSupplyInfo);
        //    await _context.SaveChangesAsync();
        //    return waterSupplyInfo;
        //}

        //public async Task<CityWaterSupply> UpdateWaterSupply(CityWaterSupply waterSupplyInfo)
        //{
        //    _dbSetSupply.Update(waterSupplyInfo);
        //    await _context.SaveChangesAsync();
        //    return waterSupplyInfo;
        //}

        //public async Task<CityWaterDisposal> GetWaterDisposal(Guid idForm)
        //{
        //    return await _dbSetDisposal.FirstOrDefaultAsync(x => x.IdForm == idForm);
        //}

        //public async Task<CityWaterDisposal> AddWaterDisposal(Guid idForm, CityWaterDisposal waterDisposalInfo)
        //{
        //    var cityForm = await _dbSetForm.FindAsync(idForm);
        //    if (cityForm == null) throw new Exception("Форма не найдена");
        //    waterDisposalInfo.IdForm = idForm;
        //    _dbSetDisposal.Add(waterDisposalInfo);
        //    await _context.SaveChangesAsync();
        //    return waterDisposalInfo;
        //}

        //public async Task<CityWaterDisposal> UpdateWaterDisposal(CityWaterDisposal waterDisposalInfo)
        //{
        //    _dbSetDisposal.Update(waterDisposalInfo);
        //    await _context.SaveChangesAsync();
        //    return waterDisposalInfo;
        //}


        //public async Task<CityTarif> GetTarifInfo(Guid idForm)
        //{
        //    return await _dbSetTarif.FirstOrDefaultAsync(x => x.IdForm == idForm);
        //}

        //public async Task<CityTarif> AddTarifInfo(Guid idForm, CityTarif tariffInfo)
        //{
        //    var cityForm = await _dbSetForm.FindAsync(idForm);
        //    if (cityForm == null) throw new Exception("Форма не найдена");
        //    tariffInfo.IdForm = idForm;
        //    _dbSetTarif.Add(tariffInfo);
        //    await _context.SaveChangesAsync();
        //    return tariffInfo;
        //}

        //public async Task<CityTarif> UpdateTariffInfo(CityTarif tariffInfo)
        //{
        //    _dbSetTarif.Update(tariffInfo);
        //    await _context.SaveChangesAsync();
        //    return tariffInfo;
        //}

        //public async Task<CityNetworkLength> GetNetworkLength(Guid idForm)
        //{
        //    return await _dbSetNetwork.FirstOrDefaultAsync(x => x.IdForm == idForm);
        //}

        //public async Task<CityNetworkLength> AddNetworkLength(Guid idForm, CityNetworkLength networkLengthInfo)
        //{
        //    var cityForm = await _dbSetForm.FindAsync(idForm);
        //    if (cityForm == null) throw new Exception("Форма не найдена");
        //    networkLengthInfo.IdForm = idForm;
        //    _dbSetNetwork.Add(networkLengthInfo);
        //    await _context.SaveChangesAsync();
        //    return networkLengthInfo;
        //}

        //public async Task<CityNetworkLength> UpdateNetworkLength(CityNetworkLength networkLengthInfo)
        //{
        //    _dbSetNetwork.Update(networkLengthInfo);
        //    await _context.SaveChangesAsync();
        //    return networkLengthInfo;
        //}
    }
}
