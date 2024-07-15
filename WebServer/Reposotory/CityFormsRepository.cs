using Microsoft.EntityFrameworkCore;
using WebServer.Data;
using WebServer.Interfaces;
using WebServer.Models;

namespace WebServer.Reposotory
{
    public class CityFormsRepository : ICityForms
    {
        private readonly WaterDbContext _context;
        private readonly DbSet<CityForms> _dbSetForm;
        private readonly DbSet<CityDocument> _dbSetDoc;
        private readonly DbSet<CityWaterDisposal> _dbSetDisposal;
        private readonly DbSet<CityWaterSupply> _dbSetSupply;
        private readonly DbSet<CityTarif> _dbSetTarif;
        private readonly DbSet<CityNetworkLength> _dbSetNetwork;
        private readonly DbSet<Ref_Kato> _dbSetKato;

        public CityFormsRepository(WaterDbContext context)
        {
            _context = context;
            _dbSetForm = _context.Set<CityForms>();
            _dbSetDoc = _context.Set<CityDocument>();
            _dbSetDisposal = _context.Set<CityWaterDisposal>();
            _dbSetSupply = _context.Set<CityWaterSupply>();
            _dbSetTarif = _context.Set<CityTarif>();
            _dbSetNetwork = _context.Set<CityNetworkLength>();
            _dbSetKato = _context.Set<Ref_Kato>();
        }

        public async Task<object> GetCityDocument(string katoKod)
        {
            if (!_dbSetKato.Any(x => x.Code.ToString() == katoKod)) throw new Exception("NotFound");
            var isReportable = await _dbSetKato.Where(x => x.Code.ToString() == katoKod).Select(x => x.IsReportable).FirstOrDefaultAsync();
            if (!isReportable) return "NotReporting";

            return await _dbSetDoc.Where(x => x.KodNaselPunk == katoKod).ToListAsync();
        }

        public async Task<CityDocument> AddCityDocument(CityDocument cityDoument)
        {
            if (await _dbSetDoc.AnyAsync(x => x.Year == cityDoument.Year && x.KodNaselPunk == cityDoument.KodNaselPunk))
                throw new Exception("За указанный год и насленный пункт уже имеется отчет!");
            await _dbSetDoc.AddAsync(cityDoument);
            await _context.SaveChangesAsync();
            return cityDoument;
        }

        public async Task<object> GetCityFormsByKodYear(string kodNaselPunk, int year)
        {
            if (!_dbSetKato.Any(x => x.Code.ToString() == kodNaselPunk)) throw new Exception("NotFound");
            var isReportable = await _dbSetKato.Where(x => x.Code.ToString() == kodNaselPunk).Select(x => x.IsReportable).FirstOrDefaultAsync();
            if (!isReportable) return "NotReporting";

            return await _dbSetDoc.Where(x=>x.KodNaselPunk==kodNaselPunk&&x.Year==year)
                .Select(x=>x.CityForm).FirstOrDefaultAsync();
        }

        public async Task<CityForms> AddCityForms(Guid idDoc, CityForms cityForms)
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

        public async Task<CityWaterSupply> GetWaterSupply(Guid idForm)
        {
            return await _dbSetSupply.FirstOrDefaultAsync(x => x.IdForm == idForm);
        }

        public async Task<CityWaterSupply> AddWaterSupply(Guid idForm, CityWaterSupply waterSupplyInfo)
        {
            var cityForm = await _dbSetForm.FindAsync(idForm);
            if (cityForm == null) throw new Exception("Форма не найдена");
            waterSupplyInfo.IdForm = idForm;
            _dbSetSupply.Add(waterSupplyInfo);
            await _context.SaveChangesAsync();
            return waterSupplyInfo;
        }

        public async Task<CityWaterSupply> UpdateWaterSupply(CityWaterSupply waterSupplyInfo)
        {
            _dbSetSupply.Update(waterSupplyInfo);
            await _context.SaveChangesAsync();
            return waterSupplyInfo;
        }

        public async Task<CityWaterDisposal> GetWaterDisposal(Guid idForm)
        {
            return await _dbSetDisposal.FirstOrDefaultAsync(x => x.IdForm == idForm);
        }

        public async Task<CityWaterDisposal> AddWaterDisposal(Guid idForm, CityWaterDisposal waterDisposalInfo)
        {
            var cityForm = await _dbSetForm.FindAsync(idForm);
            if (cityForm == null) throw new Exception("Форма не найдена");
            waterDisposalInfo.IdForm = idForm;
            _dbSetDisposal.Add(waterDisposalInfo);
            await _context.SaveChangesAsync();
            return waterDisposalInfo;
        }

        public async Task<CityWaterDisposal> UpdateWaterDisposal(CityWaterDisposal waterDisposalInfo)
        {
            _dbSetDisposal.Update(waterDisposalInfo);
            await _context.SaveChangesAsync();
            return waterDisposalInfo;
        }


        public async Task<CityTarif> GetTarifInfo(Guid idForm)
        {
            return await _dbSetTarif.FirstOrDefaultAsync(x => x.IdForm == idForm);
        }

        public async Task<CityTarif> AddTarifInfo(Guid idForm, CityTarif tariffInfo)
        {
            var cityForm = await _dbSetForm.FindAsync(idForm);
            if (cityForm == null) throw new Exception("Форма не найдена");
            tariffInfo.IdForm = idForm;
            _dbSetTarif.Add(tariffInfo);
            await _context.SaveChangesAsync();
            return tariffInfo;
        }

        public async Task<CityTarif> UpdateTariffInfo(CityTarif tariffInfo)
        {
            _dbSetTarif.Update(tariffInfo);
            await _context.SaveChangesAsync();
            return tariffInfo;
        }

        public async Task<CityNetworkLength> GetNetworkLength(Guid idForm)
        {
            return await _dbSetNetwork.FirstOrDefaultAsync(x => x.IdForm == idForm);
        }

        public async Task<CityNetworkLength> AddNetworkLength(Guid idForm, CityNetworkLength networkLengthInfo)
        {
            var cityForm = await _dbSetForm.FindAsync(idForm);
            if (cityForm == null) throw new Exception("Форма не найдена");
            networkLengthInfo.IdForm = idForm;
            _dbSetNetwork.Add(networkLengthInfo);
            await _context.SaveChangesAsync();
            return networkLengthInfo;
        }

        public async Task<CityNetworkLength> UpdateNetworkLength(CityNetworkLength networkLengthInfo)
        {
            _dbSetNetwork.Update(networkLengthInfo);
            await _context.SaveChangesAsync();
            return networkLengthInfo;
        }
    }
}
