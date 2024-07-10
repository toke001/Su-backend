using Microsoft.EntityFrameworkCore;
using WebServer.Data;
using WebServer.Models;

namespace WebServer.Reposotory
{
    public class SeloFormsRepository
    {
        private readonly WaterDbContext _context;
        private readonly DbSet<SeloForms> _dbSetForm;
        private readonly DbSet<SeloDoument> _dbSetDoc;

        public SeloFormsRepository(WaterDbContext context)
        {
            _context = context;
            _dbSetForm = _context.Set<SeloForms>();
            _dbSetDoc = _context.Set<SeloDoument>();
        }

        public async Task<SeloDoument> AddSeloDocument(SeloDoument seloDoument)
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
                .Select(x=>x.SeloForm).FirstOrDefaultAsync() ?? new SeloForms();
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

        public async Task UpdateSeloForms()
        {

        }
    }
}
