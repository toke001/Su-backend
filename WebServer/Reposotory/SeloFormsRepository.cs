using Microsoft.EntityFrameworkCore;
using WebServer.Data;
using WebServer.Interfaces;
using WebServer.Models;

namespace WebServer.Reposotory
{
    public class SeloFormsRepository : ISeloForms
    {
        private readonly WaterDbContext _context;
        private readonly DbSet<SeloForm> _dbSetForm;
        private readonly DbSet<SeloDocument> _dbSetDoc;
        private readonly DbSet<Account> _dbSetAccount;
        private readonly DbSet<Ref_Kato> _dbSetKato;
        //private readonly DbSet<ResponseCode> _dbSetResponse;

        public SeloFormsRepository(WaterDbContext context)
        {
            _context = context;
            _dbSetForm = _context.Set<SeloForm>();
            _dbSetDoc = _context.Set<SeloDocument>();
            _dbSetAccount = _context.Set<Account>();
            //_dbSetTarif = _context.Set<SeloTariff>();
            //_dbSetNetwork = _context.Set<SeloNetworkLength>();
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
            //await AddDocumentLink(seloDoument.Id);
            return seloDoument;
        }        

        public async Task<object> GetSeloFormsByKodYear(string kodNaselPunk, int year)
        {
            if (!_dbSetKato.Any(x => x.Code.ToString() == kodNaselPunk)) throw new Exception("NotFound");
            var isReportable = await _dbSetKato.Where(x => x.Code.ToString() == kodNaselPunk).Select(x => x.IsReportable).FirstOrDefaultAsync();
            if (!isReportable) return "NotReporting";

            var docId = await _dbSetDoc.Where(x=>x.KodNaselPunk==kodNaselPunk&&x.Year==year)
                .Select(x=>x.Id).FirstOrDefaultAsync();
            var res = await _dbSetForm.Where(x=>x.DocumentId==docId).ToListAsync();
            return res;
        }
                
        public async Task<List<SeloForm>> AddSeloForms(Guid idDoc, List<SeloForm> seloForms)
        {
            var doc = await _dbSetDoc.FindAsync(idDoc);
            if (doc == null) throw new Exception("Документа с таким Id не существует");
            if (seloForms.Count == 0) throw new Exception("Формы пустые");
            
            await _dbSetForm.AddRangeAsync(seloForms);
            await _context.SaveChangesAsync();
            return seloForms;
        }

        public async Task<SeloForm> GetSeloFormById(Guid id)
        {
            return await _dbSetForm.FindAsync(id);
        }

        public async Task<SeloForm> UpdateSeloForm(SeloForm seloForm)
        {
            _dbSetForm.Update(seloForm);
            await _context.SaveChangesAsync();
            return seloForm;
        }                
    }
}
