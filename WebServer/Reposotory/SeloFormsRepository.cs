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
        private readonly DbSet<WaterDisposalInfo> _dbSetDisposal;
        private readonly DbSet<WaterSupplyInfo> _dbSetSupply;
        private readonly DbSet<TariffInfo> _dbSetTarif;
        private readonly DbSet<NetworkLengthInfo> _dbSetNetwork;

        public SeloFormsRepository(WaterDbContext context)
        {
            _context = context;
            _dbSetForm = _context.Set<SeloForms>();
            _dbSetDoc = _context.Set<SeloDocument>();
            _dbSetDisposal = _context.Set<WaterDisposalInfo>();
            _dbSetSupply = _context.Set<WaterSupplyInfo>();
            _dbSetTarif = _context.Set<TariffInfo>();
            _dbSetNetwork = _context.Set<NetworkLengthInfo>();
        }

        public async Task<SeloDocument> GetSeloDocument(string katoKod)
        {
            return await _dbSetDoc.FirstOrDefaultAsync(x => x.KodNaselPunk == katoKod);
        }

        public async Task<SeloDocument> AddSeloDocument(SeloDocument seloDoument)
        {
            if (await _dbSetDoc.AnyAsync(x => x.Year == seloDoument.Year && x.KodNaselPunk == seloDoument.KodNaselPunk))
                throw new Exception("За указанный год и насленный пункт уже имеется отчет!");
            await _dbSetDoc.AddAsync(seloDoument);
            await _context.SaveChangesAsync();
            return seloDoument;
        }

        public async Task<SeloForms> GetSeloFormsByKodYear(string kodNaselPunk, int year)
        {
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

        public async Task<WaterSupplyInfo> GetWaterSupply(Guid idForm)
        {
            return await _dbSetSupply.FirstOrDefaultAsync(x => x.IdForm == idForm);
        }

        public async Task<WaterSupplyInfo> AddWaterSupply(Guid idForm, WaterSupplyInfo waterSupplyInfo)
        {
            var seloForm = await _dbSetForm.FindAsync(idForm);
            if (seloForm == null) throw new Exception("Форма не найдена");
            waterSupplyInfo.IdForm = idForm;
            _dbSetSupply.Add(waterSupplyInfo);
            await _context.SaveChangesAsync();
            return waterSupplyInfo;
        }

        public async Task<WaterSupplyInfo> UpdateWaterSupply(WaterSupplyInfo waterSupplyInfo)
        {
            _dbSetSupply.Update(waterSupplyInfo);
            await _context.SaveChangesAsync();
            return waterSupplyInfo;
        }

        public async Task<WaterDisposalInfo> GetWaterDisposal(Guid idForm)
        {
            return await _dbSetDisposal.FirstOrDefaultAsync(x => x.IdForm == idForm);
        }

        public async Task<WaterDisposalInfo> AddWaterDisposal(Guid idForm, WaterDisposalInfo waterDisposalInfo)
        {
            var seloForm = await _dbSetForm.FindAsync(idForm);
            if (seloForm == null) throw new Exception("Форма не найдена");
            waterDisposalInfo.IdForm = idForm;
            _dbSetDisposal.Add(waterDisposalInfo);
            await _context.SaveChangesAsync();
            return waterDisposalInfo;
        }

        public async Task<WaterDisposalInfo> UpdateWaterDisposal(WaterDisposalInfo waterDisposalInfo)
        {
            _dbSetDisposal.Update(waterDisposalInfo);
            await _context.SaveChangesAsync();
            return waterDisposalInfo;
        }


        public async Task<TariffInfo> GetTarifInfo(Guid idForm)
        {
            return await _dbSetTarif.FirstOrDefaultAsync(x => x.IdForm == idForm);
        }

        public async Task<TariffInfo> AddTarifInfo(Guid idForm, TariffInfo tariffInfo)
        {
            var seloForm = await _dbSetForm.FindAsync(idForm);
            if (seloForm == null) throw new Exception("Форма не найдена");
            tariffInfo.IdForm = idForm;
            _dbSetTarif.Add(tariffInfo);
            await _context.SaveChangesAsync();
            return tariffInfo;
        }

        public async Task<TariffInfo> UpdateTariffInfo(TariffInfo tariffInfo)
        {
            _dbSetTarif.Update(tariffInfo);
            await _context.SaveChangesAsync();
            return tariffInfo;
        }

        public async Task<NetworkLengthInfo> GetNetworkLength(Guid idForm)
        {
            return await _dbSetNetwork.FirstOrDefaultAsync(x => x.IdForm == idForm);
        }

        public async Task<NetworkLengthInfo> AddNetworkLength(Guid idForm, NetworkLengthInfo networkLengthInfo)
        {
            var seloForm = await _dbSetForm.FindAsync(idForm);
            if (seloForm == null) throw new Exception("Форма не найдена");
            networkLengthInfo.IdForm = idForm;
            _dbSetNetwork.Add(networkLengthInfo);
            await _context.SaveChangesAsync();
            return networkLengthInfo;
        }

        public async Task<NetworkLengthInfo> UpdateNetworkLength(NetworkLengthInfo networkLengthInfo)
        {
            _dbSetNetwork.Update(networkLengthInfo);
            await _context.SaveChangesAsync();
            return networkLengthInfo;
        }
    }
}
