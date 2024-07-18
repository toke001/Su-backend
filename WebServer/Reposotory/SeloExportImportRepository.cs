using Microsoft.EntityFrameworkCore;
using WebServer.Data;
using WebServer.Models;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using WebServer.Interfaces;
using WebServer.Dtos;

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

        public async Task<List<SeloTotalFormsDto>> GetFormsAsync(string kato, int year)
        {
            List<SeloTotalFormsDto> forms = new List<SeloTotalFormsDto>();
            var mainKato = await _dbSetKato.Where(x=>x.Code.ToString() == kato && x.IsReportable == true).FirstOrDefaultAsync();
            if (mainKato == null) 
            {
                var codesOfKatos = await _dbSetKato.Where(x=>x.ParentId==mainKato!.Id).Select(x=>x.Code.ToString()).ToListAsync();
                codesOfKatos.Insert(0, mainKato!.Code.ToString());                
                
                foreach(var t in codesOfKatos)
                {
                    var formId = await _dbSetDoc.Where(x => x.KodNaselPunk == t && x.Year == year).Select(x => x.SeloFormId).FirstOrDefaultAsync();
                    //if (formId == null) throw new Exception("NotFound");
                    var form = from f in _dbSetForm
                               join ws in _dbSetSupply on f.Id equals ws.IdForm
                               join wd in _dbSetDisposal on f.Id equals wd.IdForm
                               join tr in _dbSetTarif on f.Id equals tr.IdForm
                               join n in _dbSetNetwork on f.Id equals n.IdForm
                               join k in _dbSetKato on t equals k.Code.ToString()
                               where f.Id == formId
                               select new SeloTotalFormsDto
                               {
                                   Id = f.Id,
                                   NameOfPlace = k.NameRu,
                                   KodKato = t,
                                   StatusOpor = f.StatusOpor,
                                   StatusSput = f.StatusSput,
                                   StatusProch = f.StatusProch,
                                   StatusPrigran = f.StatusPrigran,
                                   ObshKolSelNasPun = f.ObshKolChelNasPun,
                                   ObshKolChelNasPun = f.ObshKolChelNasPun,
                                   ObshKolDomHoz = f.ObshKolDomHoz,
                                   YearSystVodoSnab = f.YearSystVodoSnab,
                                   ObslPredpBin = "",
                                   ObslPredpName = "",
                                   SobstId = Guid.Empty,
                               };
                    //if (form == null) throw new Exception("NotFoundReport");
                    

                }                
            }
            return forms;
        }

        public byte[] GenerateExcelFile(SeloForms form)
        {
            var doc = _dbSetDoc.Where(x=>x.SeloFormId==form.Id).FirstOrDefault();
            string? NamePlace = _dbSetKato.Where(x=>x.Code.ToString() == doc.KodNaselPunk).Select(x=>x.NameRu).FirstOrDefault();
            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("SeloForm");
                worksheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Row(1).Height = 200;

                worksheet.Row(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;                
                worksheet.Row(2).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;                                

                //Названия колонок
                worksheet.Cells[1, 1].Value = "Код строк";                
                worksheet.Cells[1, 2].Value = "Наименование области, района, сельского населенного пункта";                                
                worksheet.Cells[1, 3].Value = "Код области, района по классификатору административно-территориальных объектов";                
                worksheet.Cells[1, 4].Value = "Статус села опорное";
                worksheet.Cells[1, 5].Value = "Статус села спутниковое";
                worksheet.Cells[1, 6].Value = "Статус села прочие";                
                worksheet.Cells[1, 7].Value = "Статус села приграничные";                
                worksheet.Cells[1, 8].Value = "Общее количество сельских населенных пунктов в области (единиц)";
                worksheet.Cells[1, 9].Value = "Общая численность населения в сельских населенных пунктах (человек)";                
                worksheet.Cells[1, 10].Value = "Общее количество домохозяйств (квартир, ИЖД)";
                worksheet.Cells[1, 11].Value = "Год постройки системы водоснабжения";
                worksheet.Cells[1, 12].Value = "Обслуживающее предприятие. БИН";
                worksheet.Cells[1, 13].Value = "Обслуживающее предприятие. Наименование";
                worksheet.Cells[1, 14].Value = "в чьей собственности находится. государственная";
                worksheet.Cells[1, 15].Value = "в чьей собственности находится. частная";
                worksheet.Cells[1, 16].Value = "Доступ населения к услугам водоснабжения. Количество сельских населенных пунктов (единиц)";
                worksheet.Cells[1, 17].Value = "Доступ населения к услугам водоснабжения. Численность населения, проживающего в данных сельских населенных пунктах (человек)";
                worksheet.Cells[1, 18].Value = "Доступ населения к услугам водоснабжения. %";
                worksheet.Cells[1, 19].Value = "Централизованное водоснабжение. Кол-во сельских населенных пунктов (единиц)";
                worksheet.Cells[1, 20].Value = "Централизованное водоснабжение. Численность населения, проживающего в данных сельских населенных пунктах (человек)";
                worksheet.Cells[1, 21].Value = "Централизованное водоснабжение. Обеспеченность централизованным водоснабжением по количеству сельских населенных пунктов, % гр.19/гр.8 *100";
                worksheet.Cells[1, 22].Value = "Централизованное водоснабжение. Обеспеченность централизованным водоснабжением по численности населения, % гр.20/гр.9 *100";
                worksheet.Cells[1, 23].Value = "Централизованное водоснабжение. Кол-во абонентов, охваченных централизованным водоснабжением (единиц)";
                worksheet.Cells[1, 24].Value = "Централизованное водоснабжение. в том числе физических лиц/население (единиц)";
                worksheet.Cells[1, 25].Value = "Централизованное водоснабжение. в том числе юридических лиц (единиц)";
                worksheet.Cells[1, 26].Value = "Централизованное водоснабжение. в том числе бюджетных организаций (единиц)";
                worksheet.Cells[1, 27].Value = "Централизованное водоснабжение. Всего установлено индивидуальных приборов учета воды по состоянию на конец отчетного года (единиц)";
                worksheet.Cells[1, 28].Value = "Централизованное водоснабжение. в том числе с дистанционной передачей данных в АСУЭ обслуживающего предприятия (единиц)";
                worksheet.Cells[1, 29].Value = "Централизованное водоснабжение. Охват индивидуальными приборами учета воды, % гр.27/гр. 23*100";
                worksheet.Cells[1, 30].Value = "Нецентрализованное водоснабжение. Количество сельских населенных пунктов (единиц)";
                worksheet.Cells[1, 31].Value = "Нецентрализованное водоснабжение. КБМ. Количество сельских населенных пунктов, где установлено КБМ";
                worksheet.Cells[1, 32].Value = "Нецентрализованное водоснабжение. КБМ. Численность населения, проживающего в сельских населенных пунктах, где установлены КБМ (человек)";
                worksheet.Cells[1, 33].Value = "Нецентрализованное водоснабжение. КБМ. Обеспеченность населения услугами КБМ, % гр.32/гр.9*100";
                worksheet.Cells[1, 34].Value = "Нецентрализованное водоснабжение. ПРВ. Количество сельских населенных пунктов, где установлено ПРВ";
                worksheet.Cells[1, 35].Value = "Нецентрализованное водоснабжение. ПРВ. Численность населения, проживающего в сельских населенных пунктах, где установлены ПРВ (человек)";
                worksheet.Cells[1, 36].Value = "Нецентрализованное водоснабжение. ПРВ. Обеспеченность населения услугами  ПРВ, % гр.35/гр.9*100";
                worksheet.Cells[1, 37].Value = "Нецентрализованное водоснабжение. Привозная вода. Количество сельских населенных пунктов, жители которых используют привозную воду";
                worksheet.Cells[1, 38].Value = "Нецентрализованное водоснабжение. Привозная вода. Численность населения, проживающего в сельских населенных пунктах, где используют привозную воду";
                worksheet.Cells[1, 39].Value = "Нецентрализованное водоснабжение. Привозная вода. Обеспеченность населения привозной водой, % гр.38/гр.9*100";
                worksheet.Cells[1, 40].Value = "Нецентрализованное водоснабжение. Скважины, колодцы.  Количество сельских населенных пунктов, жители которых используют воду из скважин и колодцов";
                worksheet.Cells[1, 41].Value = "Нецентрализованное водоснабжение. Скважины, колодцы. Численность населения, проживающего в сельских населенных пунктах, где используют  воду из скважин и колодцев";
                worksheet.Cells[1, 42].Value = "Нецентрализованное водоснабжение. Скважины, колодцы. Обеспеченность  привозной водой, % гр.41/гр.9*100";
                worksheet.Cells[1, 43].Value = "Нецентрализованное водоснабжение. Скважины, колодцы. Количество сельских населенных пунктов, жители которых отказались от строительства ЦВ, установки КБМ и ПРВ  (наличие протоколов  отказа)";
                worksheet.Cells[1, 44].Value = "Нецентрализованное водоснабжение. Скважины, колодцы. Численность населения, жители которых отказались от строительства ЦВ, установки КБМ и ПРВ  (наличие протоколов  отказа)";
                worksheet.Cells[1, 45].Value = "Нецентрализованное водоснабжение. Скважины, колодцы. Доля населения, жители которых отказались от строительства ЦВ, установки КБМ и ПРВ  (наличие протоколов  отказа), гр.44/гр.9*100";
                worksheet.Cells[1, 46].Value = "Нецентрализованное водоснабжение. Скважины, колодцы. Доля сел, жители которых отказались от  строительства ЦВ, установки КБМ и ПРВ, %, гр.43/гр.8*100";
                worksheet.Cells[1, 47].Value = "Централизованное водоотведение. Кол-во сельских населенных пунктов (единиц)";
                worksheet.Cells[1, 48].Value = "Централизованное водоотведение. Численность населения, проживающего в данных сельских населенных пунктах (человек)";
                worksheet.Cells[1, 49].Value = "Централизованное водоотведение. Кол-во абонентов, проживающих в данных сельских населенных пунктах (единиц)";
                worksheet.Cells[1, 50].Value = "Централизованное водоотведение. в том числе физических лиц/население (единиц)";
                worksheet.Cells[1, 51].Value = "Централизованное водоотведение. в том числе юридических лиц (единиц)";
                worksheet.Cells[1, 52].Value = "Централизованное водоотведение. в том числе бюджетных организаций (единиц)";
                worksheet.Cells[1, 53].Value = "Централизованное водоотведение. Доступ к централизованному водоотведению по количеству сельских населенных пунктов, в % гр.47/гр.8 *100";
                worksheet.Cells[1, 54].Value = "Централизованное водоотведение. Доступ к централизованному водоотведению по численности населения, в % гр.48/гр.9 *100";
                worksheet.Cells[1, 55].Value = "Централизованное водоотведение. Наличие канализационно- очистных сооружений (единиц)";
                worksheet.Cells[1, 56].Value = "Централизованное водоотведение. в том числе только с механичес-кой очисткой (еди-ниц)";
                worksheet.Cells[1, 57].Value = "Централизованное водоотведение. в том числе с механической и биологической очист-кой (еди-ниц)";
                worksheet.Cells[1, 58].Value = "Централизованное водоотведение. Производительность канализационно-очистных сооружений (проектная)";
                worksheet.Cells[1, 59].Value = "Централизованное водоотведение. Износ канализационно- очистных сооружений, в %";
                worksheet.Cells[1, 60].Value = "Централизованное водоотведение. Числен-ность населе-ния, охваченного действующими канализационно- очистными сооружениями (человек)";
                worksheet.Cells[1, 61].Value = "Централизованное водоотведение. Охват населения очисткой сточных вод, в % гр.60/гр.9*100";
                worksheet.Cells[1, 62].Value = "Централизованное водоотведение. Фактически поступило сточных вод в канализационно-очистные сооружения (тыс.м3)";
                worksheet.Cells[1, 63].Value = "Централизованное водоотведение. В том числе За I квартал (тыс.м3)";
                worksheet.Cells[1, 64].Value = "Централизованное водоотведение. В том числе За II квартал (тыс.м3)";
                worksheet.Cells[1, 65].Value = "Централизованное водоотведение. В том числе За III квартал (тыс.м3)";
                worksheet.Cells[1, 66].Value = "Централизованное водоотведение. В том числе За IV квартал (тыс.м3)";
                worksheet.Cells[1, 67].Value = "Централизованное водоотведение. Объем сточных вод, соответствующей нормативной очистке по собственному лабораторному мониторингу за отчетный период (тыс.м3)";
                worksheet.Cells[1, 68].Value = "Централизованное водоотведение. Уровень нормативно- очищенной воды, % гр.67/гр.62 * 100";
                worksheet.Cells[1, 69].Value = "Централизованное водоотведение. Децентрализованное водоотведение. Кол-во сельских населенных пунктов (единиц)";
                worksheet.Cells[1, 70].Value = "Централизованное водоотведение. Децентрализованное водоотведение. Численность населения, проживающего в данных сельских населенных пунктах (человек)";
                worksheet.Cells[1, 71].Value = "Уровень тарифов. водоснабжение. усредненный, тенге/м3";
                worksheet.Cells[1, 72].Value = "Уровень тарифов. водоснабжение. физическим лицам/населению, тенге/м3";
                worksheet.Cells[1, 73].Value = "Уровень тарифов. водоснабжение. юридическим лицам, тенге/м3";
                worksheet.Cells[1, 74].Value = "Уровень тарифов. водоснабжение. бюджетным организациям, тенге/м3";
                worksheet.Cells[1, 75].Value = "Уровень тарифов. водоотведение. усредненный, тенге/м3";
                worksheet.Cells[1, 76].Value = "Уровень тарифов. водоотведение. физическим лицам/населению, тенге/м3";
                worksheet.Cells[1, 77].Value = "Уровень тарифов. водоотведение. юридическим лицам, тенге/м3";
                worksheet.Cells[1, 78].Value = "Уровень тарифов. водоотведение. бюджетным организациям, тенге/м3";
                worksheet.Cells[1, 79].Value = "Протяженность водопроводных сетей, км (по состоянию на конец отчетного года). общая, км";
                worksheet.Cells[1, 80].Value = "Протяженность водопроводных сетей, км (по состоянию на конец отчетного года). в том числе изношенных, км";
                worksheet.Cells[1, 81].Value = "Протяженность водопроводных сетей, км (по состоянию на конец отчетного года). Износ, % гр.80/гр.79";
                worksheet.Cells[1, 82].Value = "Протяженность канализационных сетей, км (по состоянию на конец отчетного года). общая, км";
                worksheet.Cells[1, 83].Value = "Протяженность канализационных сетей, км (по состоянию на конец отчетного года). в том числе изношенных, км";
                worksheet.Cells[1, 84].Value = "Протяженность канализационных сетей, км (по состоянию на конец отчетного года). Износ, % гр.83/гр.82";
                worksheet.Cells[1, 85].Value = "Общая протяженность построенных (новых) сетей в отчетном году, км. водоснабжения, км";
                worksheet.Cells[1, 86].Value = "Общая протяженность построенных (новых) сетей в отчетном году, км. водоотведения, км";
                worksheet.Cells[1, 87].Value = "Общая протяженность реконструированных (замененных) сетей в отчетном году, км. водоснабжения, км";
                worksheet.Cells[1, 88].Value = "Общая протяженность реконструированных (замененных) сетей в отчетном году, км. водоотведения, км";
                worksheet.Cells[1, 89].Value = "Общая протяженность отремонтированных (текущий/капитальный ремонт) сетей в отчетном году, км. водоснабжения, км";
                worksheet.Cells[1, 90].Value = "Общая протяженность отремонтированных (текущий/капитальный ремонт) сетей в отчетном году, км. водоотведения, км";
                worksheet.Cells[1, 2, 1,90].Style.WrapText = true;

                //Значения колонок
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

                // Определите диапазон данных
                var range = worksheet.Cells[1, 1, 2, 90];

                // Добавьте границы к каждой ячейке в диапазоне
                range.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var content = stream.ToArray();
                return content;
            }
        }
    }
}
