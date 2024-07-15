using Microsoft.EntityFrameworkCore;
using WebServer.Data;
using WebServer.Models;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using WebServer.Interfaces;

namespace WebServer.Reposotory
{
    public class SeloExportImportRepository : ISeloExportImport
    {
        private readonly WaterDbContext _context;
        private readonly DbSet<SeloForms> _dbSetForm;
        private readonly DbSet<SeloDocument> _dbSetDoc;
        private readonly DbSet<SeloWaterDisposal> _dbSetDisposal;
        private readonly DbSet<SeloWaterSupply> _dbSetSupply;
        private readonly DbSet<SeloTariff> _dbSetTarif;
        private readonly DbSet<SeloNetworkLength> _dbSetNetwork;

        public SeloExportImportRepository(WaterDbContext context)
        {
            _context = context;
            _dbSetForm = _context.Set<SeloForms>();
            _dbSetDoc = _context.Set<SeloDocument>();
            _dbSetDisposal = _context.Set<SeloWaterDisposal>();
            _dbSetSupply = _context.Set<SeloWaterSupply>();
            _dbSetTarif = _context.Set<SeloTariff>();
            _dbSetNetwork = _context.Set<SeloNetworkLength>();            
        }

        public async Task<SeloForms> GetFormsAsync(string kato, int year)
        {
            var formId = await _dbSetDoc.Where(x=>x.KodNaselPunk == kato && x.Year == year).Select(x=>x.SeloFormId).FirstOrDefaultAsync();
            if (formId == null) throw new Exception("NotFound");

            var form = await _dbSetForm.FindAsync(formId);
            if(form == null) throw new Exception("NotFoundReport");

            return form;
        }

        public byte[] GenerateExcelFile(SeloForms form)
        {
            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("SeloForm");
                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "StatusOpor";
                worksheet.Cells[1, 3].Value = "StatusSput";

                worksheet.Cells[2, 1].Value = form.Id;
                worksheet.Cells[2, 2].Value = form.StatusOpor;
                worksheet.Cells[2, 3].Value = form.StatusSput;

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var content = stream.ToArray();
                return content;
            }
        }
    }
}
