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

        public async Task<List<SeloDocument>> GetSeloDocumentByParams(string? kodOblast, string? kodRaion, int? year)
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
            return  result;
        }

        public async Task<object> GetSeloFormsByDocId(Guid idDoc)
        {
            if (!_dbSetDoc.Any(x => x.Id == idDoc)) throw new Exception("NotFound");            
            return await _dbSetForm.Where(x => x.DocumentId == idDoc).ToListAsync();
        }

        public async Task<SeloDocument> AddSeloDocument(SeloDocument seloDoument)
        {
            var loginKatoCode = await _dbSetAccount.Where(x=>x.Login == seloDoument.Login).Select(x=>x.KatoCode).FirstOrDefaultAsync();
            var loginKato = await _dbSetKato.FirstOrDefaultAsync(x => x.Code == loginKatoCode);
            var loginOblast = await FindParentRecordAsync(loginKato.Id, 0);

            var seloDocumentKato = await _dbSetKato.FirstOrDefaultAsync(x => x.Code.ToString() == seloDoument.KodNaselPunk);
            var seloDocumentOblast = await FindParentRecordAsync(seloDocumentKato.Id, 0);

            if (loginOblast.Id != seloDocumentOblast.Id) throw new Exception("Можно создавать документ только в рамках своей области!");

            if (await _dbSetDoc.AnyAsync(x => x.Year == seloDoument.Year && x.KodNaselPunk == seloDoument.KodNaselPunk))
                throw new Exception("За указанный год и насленный пункт уже имеется отчет!");
            await _dbSetDoc.AddAsync(seloDoument);            
            await _context.SaveChangesAsync();
            //await AddDocumentLink(seloDoument.Id);
            return seloDoument;
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
                
        public async Task<List<SeloForm>> AddSeloForms(string login, List<SeloForm> seloForms)
        {
            var loginKatoCode = await _dbSetAccount.Where(x => x.Login == login).Select(x => x.KatoCode).FirstOrDefaultAsync();
            var loginKato = await _dbSetKato.FirstOrDefaultAsync(x => x.Code == loginKatoCode);
            var loginOblast = await FindParentRecordAsync(loginKato.Id, 0);

            var idDoc = seloForms.Select(x => x.DocumentId).FirstOrDefault();
            var seloDocument = await _dbSetDoc.FindAsync(idDoc);
            if (seloDocument == null) throw new Exception("Документа с таким Id не существует");
            var seloDocumentKato = await _dbSetKato.FirstOrDefaultAsync(x => x.Code.ToString() == seloDocument.KodNaselPunk);
            var seloDocumentOblast = await FindParentRecordAsync(seloDocumentKato.Id, 0);

            if (loginOblast.Id != seloDocumentOblast.Id) throw new Exception("Можно создавать форму только в рамках своей области!");                       
            
            if (seloForms.Count == 0) throw new Exception("Формы пустые");
            
            await _dbSetForm.AddRangeAsync(seloForms);
            await _context.SaveChangesAsync();
            return seloForms;
        }

        public async Task<SeloForm> GetSeloFormById(Guid id)
        {
            return await _dbSetForm.FindAsync(id);
        }

        public async Task<SeloForm> UpdateSeloForm(string login, SeloForm seloForm)
        {
            var loginKatoCode = await _dbSetAccount.Where(x => x.Login == login).Select(x => x.KatoCode).FirstOrDefaultAsync();
            var loginKato = await _dbSetKato.FirstOrDefaultAsync(x => x.Code == loginKatoCode);
            var loginOblast = await FindParentRecordAsync(loginKato.Id, 0);

            var idDoc = seloForm.DocumentId;
            var seloDocument = await _dbSetDoc.FindAsync(idDoc);
            if (seloDocument == null) throw new Exception("Документа с таким Id не существует");
            var seloDocumentKato = await _dbSetKato.FirstOrDefaultAsync(x => x.Code.ToString() == seloDocument.KodNaselPunk);
            var seloDocumentOblast = await FindParentRecordAsync(seloDocumentKato.Id, 0);

            if (loginOblast.Id != seloDocumentOblast.Id) throw new Exception("Можно создавать форму только в рамках своей области!");

            _dbSetForm.Update(seloForm);
            await _context.SaveChangesAsync();
            return seloForm;
        }                
    }
}
