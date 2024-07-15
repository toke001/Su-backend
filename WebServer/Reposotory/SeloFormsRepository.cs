using Microsoft.EntityFrameworkCore;
using WebServer.Data;
using WebServer.Interfaces;
using WebServer.Models;

namespace WebServer.Reposotory
{
    public class SeloFormsRepository : ISeloForms
    {
        private readonly WaterDbContext _context;
        private readonly DbSet<SeloForms> _dbSetForm;
        private readonly DbSet<SeloDocument> _dbSetDoc;
        private readonly DbSet<SeloWaterDisposal> _dbSetDisposal;
        private readonly DbSet<SeloWaterSupply> _dbSetSupply;
        private readonly DbSet<SeloTariff> _dbSetTarif;
        private readonly DbSet<SeloNetworkLength> _dbSetNetwork;
        private readonly DbSet<Ref_Kato> _dbSetKato;
        //private readonly DbSet<ResponseCode> _dbSetResponse;

        public SeloFormsRepository(WaterDbContext context)
        {
            _context = context;
            _dbSetForm = _context.Set<SeloForms>();
            _dbSetDoc = _context.Set<SeloDocument>();
            _dbSetDisposal = _context.Set<SeloWaterDisposal>();
            _dbSetSupply = _context.Set<SeloWaterSupply>();
            _dbSetTarif = _context.Set<SeloTariff>();
            _dbSetNetwork = _context.Set<SeloNetworkLength>();
            _dbSetKato = _context.Set<Ref_Kato>();
            //_dbSetResponse = _context.Set<ResponseCode>();
        }

        public async Task<object> GetSeloDocument(string katoKod)
        {
            if (!_dbSetKato.Any(x => x.Code.ToString() == katoKod)) throw new Exception("NotFound");
            var isReportable = await _dbSetKato.Where(x => x.Code.ToString() == katoKod).Select(x=>x.IsReportable).FirstOrDefaultAsync();
            if (!isReportable) return "NotReporting";
            return await _dbSetDoc.Where(x => x.KodNaselPunk == katoKod).ToListAsync();
        }

        public async Task<SeloDocument> AddSeloDocument(SeloDocument seloDoument)
        {
            if (await _dbSetDoc.AnyAsync(x => x.Year == seloDoument.Year && x.KodNaselPunk == seloDoument.KodNaselPunk))
                throw new Exception("За указанный год и насленный пункт уже имеется отчет!");
            await _dbSetDoc.AddAsync(seloDoument);
            await _context.SaveChangesAsync();
            return seloDoument;
        }

        public async Task<object> GetSeloFormsByKodYear(string kodNaselPunk, int year)
        {
            if (!_dbSetKato.Any(x => x.Code.ToString() == kodNaselPunk)) throw new Exception("NotFound");
            var isReportable = await _dbSetKato.Where(x => x.Code.ToString() == kodNaselPunk).Select(x => x.IsReportable).FirstOrDefaultAsync();
            if (!isReportable) return "NotReporting";

            return await _dbSetDoc.Where(x=>x.KodNaselPunk==kodNaselPunk&&x.Year==year)
                .Select(x=>x.SeloForm).FirstOrDefaultAsync();
        }

        public async Task<SeloForms> AddSeloForms(Guid idDoc, SeloForms seloForms)
        {
            var doc = await _dbSetDoc.FindAsync(idDoc);
            if (doc == null) throw new Exception("Документа с таким Id не существует");

            if (doc.SeloFormId != null) throw new Exception("У данного документа уже есть форма");

            await _dbSetForm.AddAsync(seloForms);
            await _context.SaveChangesAsync();

            doc.SeloFormId = seloForms.Id;

            await _context.SaveChangesAsync();

            return seloForms;
        }

        public async Task<SeloWaterSupply> GetWaterSupply(Guid idForm)
        {
            return await _dbSetSupply.FirstOrDefaultAsync(x => x.IdForm == idForm);
        }

        public async Task<SeloWaterSupply> AddWaterSupply(Guid idForm, SeloWaterSupply waterSupplyInfo)
        {
            var seloForm = await _dbSetForm.FindAsync(idForm);
            if (seloForm == null) throw new Exception("Форма не найдена");
            waterSupplyInfo.IdForm = idForm;
            _dbSetSupply.Add(waterSupplyInfo);
            await _context.SaveChangesAsync();
            return waterSupplyInfo;
        }

        public async Task<SeloWaterSupply> UpdateWaterSupply(SeloWaterSupply waterSupplyInfo)
        {
            _dbSetSupply.Update(waterSupplyInfo);
            await _context.SaveChangesAsync();
            return waterSupplyInfo;
        }

        public async Task<SeloWaterDisposal> GetWaterDisposal(Guid idForm)
        {
            return await _dbSetDisposal.FirstOrDefaultAsync(x => x.IdForm == idForm);
        }

        public async Task<SeloWaterDisposal> AddWaterDisposal(Guid idForm, SeloWaterDisposal waterDisposalInfo)
        {
            var seloForm = await _dbSetForm.FindAsync(idForm);
            if (seloForm == null) throw new Exception("Форма не найдена");
            waterDisposalInfo.IdForm = idForm;
            _dbSetDisposal.Add(waterDisposalInfo);
            await _context.SaveChangesAsync();
            return waterDisposalInfo;
        }

        public async Task<SeloWaterDisposal> UpdateWaterDisposal(SeloWaterDisposal waterDisposalInfo)
        {
            _dbSetDisposal.Update(waterDisposalInfo);
            await _context.SaveChangesAsync();
            return waterDisposalInfo;
        }


        public async Task<SeloTariff> GetTarifInfo(Guid idForm)
        {
            return await _dbSetTarif.FirstOrDefaultAsync(x => x.IdForm == idForm);
        }

        public async Task<SeloTariff> AddTarifInfo(Guid idForm, SeloTariff tariffInfo)
        {
            var seloForm = await _dbSetForm.FindAsync(idForm);
            if (seloForm == null) throw new Exception("Форма не найдена");
            tariffInfo.IdForm = idForm;
            _dbSetTarif.Add(tariffInfo);
            await _context.SaveChangesAsync();
            return tariffInfo;
        }

        public async Task<SeloTariff> UpdateTariffInfo(SeloTariff tariffInfo)
        {
            _dbSetTarif.Update(tariffInfo);
            await _context.SaveChangesAsync();
            return tariffInfo;
        }

        public async Task<SeloNetworkLength> GetNetworkLength(Guid idForm)
        {
            return await _dbSetNetwork.FirstOrDefaultAsync(x => x.IdForm == idForm);
        }

        public async Task<SeloNetworkLength> AddNetworkLength(Guid idForm, SeloNetworkLength networkLengthInfo)
        {
            var seloForm = await _dbSetForm.FindAsync(idForm);
            if (seloForm == null) throw new Exception("Форма не найдена");
            networkLengthInfo.IdForm = idForm;
            _dbSetNetwork.Add(networkLengthInfo);
            await _context.SaveChangesAsync();
            return networkLengthInfo;
        }

        public async Task<SeloNetworkLength> UpdateNetworkLength(SeloNetworkLength networkLengthInfo)
        {
            _dbSetNetwork.Update(networkLengthInfo);
            await _context.SaveChangesAsync();
            return networkLengthInfo;
        }
    }
}
