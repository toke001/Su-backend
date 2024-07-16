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
        private readonly DbSet<Ref_Kato> _dbSetKato;

        public SeloExportImportRepository(WaterDbContext context)
        {
            _context = context;
            _dbSetForm = _context.Set<SeloForms>();
            _dbSetDoc = _context.Set<SeloDocument>();
            _dbSetDisposal = _context.Set<SeloWaterDisposal>();
            _dbSetSupply = _context.Set<SeloWaterSupply>();
            _dbSetTarif = _context.Set<SeloTariff>();
            _dbSetNetwork = _context.Set<SeloNetworkLength>();
            _dbSetKato = _context.Set<Ref_Kato>();
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
            var doc = _dbSetDoc.Where(x=>x.SeloFormId==form.Id).FirstOrDefault();
            string? NamePlace = _dbSetKato.Where(x=>x.Code.ToString() == doc.KodNaselPunk).Select(x=>x.NameRu).FirstOrDefault();
            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("SeloForm");
                worksheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Row(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                worksheet.Row(2).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Row(1).Height = 50;
                worksheet.Row(2).Height = 120;

                //SeloForms
                worksheet.Cells[1, 1].Value = "Код строк";
                worksheet.Cells[1, 1, 2, 1].Merge = true;                
                
                worksheet.Cells[1, 2].Value = "Наименование области, района, сельского населенного пункта";
                worksheet.Cells[1, 2, 2, 2].Merge = true;
                worksheet.Cells[1, 2].Style.WrapText = true;         

                worksheet.Cells[1, 3].Value = "Код области, района по классификатору административно-территориальных объектов";
                worksheet.Cells[1, 3, 2, 3].Merge = true;
                worksheet.Cells[1, 3].Style.WrapText = true;

                worksheet.Cells[1, 4].Value = "Статус села";
                worksheet.Cells[1, 4, 1, 7].Merge = true;
                worksheet.Cells[1, 4].Style.WrapText = true;

                worksheet.Cells[2, 4].Value = "опорное";
                worksheet.Cells[2, 4].Style.WrapText = true;
                worksheet.Cells[2, 5].Value = "спутниковое";
                worksheet.Cells[2, 5].Style.WrapText = true;
                worksheet.Cells[2, 6].Value = "прочие";
                worksheet.Cells[2, 6].Style.WrapText = true;
                worksheet.Cells[2, 7].Value = "приграничные";
                worksheet.Cells[2, 7].Style.WrapText = true;
                worksheet.Cells[1, 8].Value = "Общее количество сельских населенных пунктов в области (единиц)";
                worksheet.Cells[1, 8].Style.WrapText = true;
                worksheet.Cells[1, 8, 2, 8].Merge = true;
                worksheet.Cells[1, 8].Value = "Общая численность населения в сельских населенных пунктах (человек)";
                worksheet.Cells[1, 8].Style.WrapText = true;
                worksheet.Cells[1, 8, 2, 8].Merge = true;
                worksheet.Cells[1, 9].Value = "Общее количество домохозяйств (квартир, ИЖД)";
                worksheet.Cells[1, 9].Style.WrapText = true;
                worksheet.Cells[1, 9, 2, 9].Merge = true;

                //worksheet.Cells[1, 4].Value = "Статус села прочие";
                //worksheet.Cells[1, 5].Value = "Статус села приграничные";
                //worksheet.Cells[1, 6].Value = "Общее количество сельских населенных пунктов в области(единиц)";
                //worksheet.Cells[1, 7].Value = "Общая численность населения в сельских населенных пунктах (человек)";
                //worksheet.Cells[1, 8].Value = "Общее количество домохозяйств (квартир, ИЖД)";
                //worksheet.Cells[1, 9].Value = "Год постройки системы водоснабжения";
                //worksheet.Cells[1, 10].Value = "Обслуживающее предприятие";

                worksheet.Cells[3, 1].Value = form.Id;
                worksheet.Cells[3, 2].Value = NamePlace ?? "";
                worksheet.Cells[3, 3].Value = doc?.KodNaselPunk;         
                worksheet.Cells[3, 4].Value = form.StatusOpor == true ? "Да" : "Нет";
                //worksheet.Cells[2, 3].Value = form.StatusSput == true ? "Да" : "Нет";
                //worksheet.Cells[2, 4].Value = form.StatusProch;
                //worksheet.Cells[2, 5].Value = form.StatusPrigran == true ? "Да" : "Нет";
                //worksheet.Cells[2, 6].Value = form.ObshKolSelNasPun;
                //worksheet.Cells[2, 7].Value = form.ObshKolChelNasPun;
                //worksheet.Cells[2, 8].Value = form.ObshKolDomHoz;
                //worksheet.Cells[2, 9].Value = form.YearSystVodoSnab;
                //worksheet.Cells[2, 10].Value = form.ObslPredpId;
                //worksheet.Cells[2, 11].Value = form.SobstId;

                //worksheet.Cells[2, 9].Style.Numberformat.Format = "yyyy-MM-dd";
                worksheet.Column(1).AutoFit();
                worksheet.Column(2).AutoFit();
                worksheet.Column(3).AutoFit();

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var content = stream.ToArray();
                return content;
            }
        }
    }
}
