using Microsoft.EntityFrameworkCore;
using WebServer.Data;
using WebServer.Models;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using WebServer.Interfaces;
using WebServer.Dtos;
using System.Reflection;
using WebServer.Helpers;

namespace WebServer.Reposotory
{
    public class SeloExportImportRepository : ISeloExportImport
    {
        private readonly WaterDbContext _context;
        private readonly DbSet<SeloForm> _dbSetForm;
        private readonly DbSet<SeloDocument> _dbSetDoc;
        private readonly DbSet<Ref_Kato> _dbSetKato;
        private readonly DbSet<Account> _dbSetAccount;

        public SeloExportImportRepository(WaterDbContext context)
        {
            _context = context;
            _dbSetForm = _context.Set<SeloForm>();
            _dbSetDoc = _context.Set<SeloDocument>();
            _dbSetKato = _context.Set<Ref_Kato>();
            _dbSetAccount = _context.Set<Account>();
        }

        //public async Task<SeloTotalFormsDto> GetSeloTotalFormsAsync(string kato, int year)
        //{
        //    //List<SeloTotalFormsDto> forms = new List<SeloTotalFormsDto>();
        //    var form = new SeloTotalFormsDto();
        //    var mainKato = await _dbSetKato.Where(x=>x.Code.ToString() == kato && x.IsReportable == true).FirstOrDefaultAsync();
        //    if (mainKato != null)
        //    {
        //        //var codesOfKatos = await _dbSetKato.Where(x=>x.ParentId==mainKato!.Id).Select(x=>x.Code.ToString()).ToListAsync();//список всех КАТО
        //        //codesOfKatos.Insert(0, mainKato!.Code.ToString()); //Вношу главное КАТО, которое при в параметрах

        //        //foreach (var t in codesOfKatos)
        //        //{
        //            var formId = await _dbSetDoc.Where(x => x.KodNaselPunk == mainKato.Code.ToString() && x.Year == year).Select(x => x.SeloFormId).FirstOrDefaultAsync();
        //            //if (formId == null) throw new Exception("NotFound");
        //            form = (from f in _dbSetForm
        //                        join k in _dbSetKato on mainKato.Code equals k.Code
        //                        where f.Id == formId
        //                        select new SeloTotalFormsDto
        //                        {
        //                            Id = f.Id,
        //                            NameOfPlace = k.NameRu,
        //                            KodKato = mainKato.Code.ToString(),
        //                            StatusOpor = f.StatusOpor,
        //                            StatusSput = f.StatusSput,
        //                            StatusProch = f.StatusProch,
        //                            StatusPrigran = f.StatusPrigran,
        //                            ObshKolSelNasPun = f.ObshKolChelNasPun,
        //                            ObshKolChelNasPun = f.ObshKolChelNasPun,
        //                            ObshKolDomHoz = f.ObshKolDomHoz,
        //                            YearSystVodoSnab = f.YearSystVodoSnab,
        //                            ObslPredpBin = "",
        //                            ObslPredpName = "",
        //                            SobstName = null,
        //                            DosVodoSnabKolPunk = f.DosVodoSnabKolPunk,
        //                            DosVodoSnabKolChel = f.DosVodoSnabKolChel,
        //                            DosVodoSnabPercent = f.DosVodoSnabPercent,
        //                            CentrVodoSnabKolNasPun = f.CentrVodoSnabKolNasPun,
        //                            CentrVodoSnabKolChel = f.CentrVodoSnabKolChel,
        //                            CentrVodoSnabObesKolNasPunk = f.CentrVodoSnabObesKolNasPunk,
        //                            CentrVodoSnabObesKolChel = f.CentrVodoSnabObesKolChel,
        //                            CentrVodoSnabKolAbon = f.CentrVodoSnabKolAbon,
        //                            CentrVodoSnabFizLic = f.CentrVodoSnabFizLic,
        //                            CentrVodoSnabYriLic = f.CentrVodoSnabYriLic,
        //                            CentrVodoSnabBudzhOrg = f.CentrVodoSnabBudzhOrg,
        //                            CentrVodoIndivPriborUchVodyVsego = f.CentrVodoIndivPriborUchVodyVsego,
        //                            CentrVodoIndivPriborUchVodyASYE = f.CentrVodoIndivPriborUchVodyASYE,
        //                            CentrVodoIndivPriborUchVodyOhvat = f.CentrVodoIndivPriborUchVodyOhvat,
        //                            NeCtentrVodoKolSelsNasPunk = f.NeCtentrVodoKolSelsNasPunk,
        //                            KbmKolSelsNasPunk = f.KbmKolSelsNasPunk,
        //                            KbmKolChel = f.KbmKolChel,
        //                            KbmObespNasel = f.KbmObespNasel,
        //                            PrvKolSelsNasPunk = f.PrvKolSelsNasPunk,
        //                            PrvKolChel = f.PrvKolChel,
        //                            PrvObespNasel = f.PrvObespNasel,
        //                            PrivVodaKolSelsNasPunk = f.PrivVodaKolSelsNasPunk,
        //                            PrivVodaKolChel = f.PrivVodaKolChel,
        //                            PrivVodaObespNasel = f.PrivVodaObespNasel,
        //                            SkvazhKolSelsNasPunk = f.SkvazhKolSelsNasPunk,
        //                            SkvazhKolChel = f.SkvazhKolChel,
        //                            SkvazhObespNasel = f.SkvazhObespNasel,
        //                            SkvazhKolSelsNasPunkOtkaz = f.SkvazhKolSelsNasPunkOtkaz,
        //                            SkvazhKolChelOtkaz = f.SkvazhKolChelOtkaz,
        //                            SkvazhDolyaNaselOtkaz = f.SkvazhDolyaNaselOtkaz,
        //                            SkvazhDolyaSelOtkaz = f.SkvazhDolyaSelOtkaz,
        //                            CentrVodOtvedKolSelsNasPunk = f.CentrVodOtvedKolSelsNasPunk,
        //                            CentrVodOtvedKolChel = f.CentrVodOtvedKolChel,
        //                            CentrVodOtvedKolAbonent = f.CentrVodOtvedKolAbonent,
        //                            CentrVodOtvedFizLic = f.CentrVodOtvedFizLic,
        //                            CentrVodOtvedYriLic = f.CentrVodOtvedYriLic,
        //                            CentrVodOtvedBydzhOrg = f.CentrVodOtvedBydzhOrg,
        //                            CentrVodOtvedDostypKolNasPunk = f.CentrVodOtvedDostypKolNasPunk,
        //                            CentrVodOtvedDostypKolChel = f.CentrVodOtvedDostypKolChel,
        //                            CentrVodOtvedNalich = f.CentrVodOtvedNalich,
        //                            CentrVodOtvedNalichMechan = f.CentrVodOtvedNalichMechan,
        //                            CentrVodOtvedNalichMechanBiolog = f.CentrVodOtvedNalichMechanBiolog,
        //                            CentrVodOtvedProizvod = f.CentrVodOtvedProizvod,
        //                            CentrVodOtvedIznos = f.CentrVodOtvedIznos,
        //                            CentrVodOtvedOhvatKolChel = f.CentrVodOtvedOhvatKolChel,
        //                            CentrVodOtvedOhvatNasel = f.CentrVodOtvedOhvatNasel,
        //                            CentrVodOtvedFactPostypStochVod = f.CentrVodOtvedFactPostypStochVod,
        //                            CentrVodOtvedFactPostypStochVod1 = f.CentrVodOtvedFactPostypStochVod1,
        //                            CentrVodOtvedFactPostypStochVod2 = f.CentrVodOtvedFactPostypStochVod2,
        //                            CentrVodOtvedFactPostypStochVod3 = f.CentrVodOtvedFactPostypStochVod3,
        //                            CentrVodOtvedFactPostypStochVod4 = f.CentrVodOtvedFactPostypStochVod4,
        //                            CentrVodOtvedObiemStochVod = f.CentrVodOtvedObiemStochVod,
        //                            CentrVodOtvedUrovenNorm = f.CentrVodOtvedUrovenNorm,
        //                            DecentrVodoOtvedKolSelsNasPunk = f.DecentrVodoOtvedKolSelsNasPunk,
        //                            DecentrVodoOtvedKolChel = f.DecentrVodoOtvedKolChel,
        //                            TarifVodoSnabUsred = f.TarifVodoSnabUsred,
        //                            TarifVodoSnabFizL = f.TarifVodoSnabFizL,
        //                            TarifVodoSnabYriL = f.TarifVodoSnabYriL,
        //                            TarifVodoSnabBudzh = f.TarifVodoSnabBudzh,
        //                            TarifVodoOtvedUsred = f.TarifVodoOtvedUsred,
        //                            TarifVodoOtvedFizL = f.TarifVodoOtvedFizL,
        //                            TarifVodoOtvedYriL = f.TarifVodoOtvedYriL,
        //                            TarifVodoOtvedBudzh = f.TarifVodoOtvedBudzh,
        //                            ProtyzhVodoSeteyObsh = f.ProtyzhVodoSeteyObsh,
        //                            ProtyzhVodoSeteyVtomIznos = f.ProtyzhVodoSeteyVtomIznos,
        //                            ProtyzhVodoSeteyIznos = f.ProtyzhVodoSeteyIznos,
        //                            ProtyzhKanalSeteyObsh = f.ProtyzhKanalSeteyObsh,
        //                            ProtyzhKanalSeteyVtomIznos = f.ProtyzhKanalSeteyVtomIznos,
        //                            ProtyzhKanalSeteyIznos = f.ProtyzhKanalSeteyIznos,
        //                            ProtyzhNewSeteyVodoSnab = f.ProtyzhNewSeteyVodoSnab,
        //                            ProtyzhNewSeteyVodoOtved = f.ProtyzhNewSeteyVodoOtved,
        //                            ProtyzhRekonSeteyVodoSnab = f.ProtyzhRekonSeteyVodoSnab,
        //                            ProtyzhRekonSeteyVodoOtved = f.ProtyzhRekonSeteyVodoOtved,
        //                            ProtyzhRemontSeteyVodoSnab = f.ProtyzhRemontSeteyVodoSnab,
        //                            ProtyzhRemontSeteyVodoOtved = f.ProtyzhRemontSeteyVodoOtved
        //                        }).FirstOrDefault();
        //            //if (form == null) throw new Exception("NotFoundReport");
        //            //if(form != null) forms.Add(form);
        //        }                
        //    //}
        //    return form;
        //}

        public async Task<List<SeloTotalFormsDto>> GetSeloTotalFormsByParentCodAsync(string parentKato, int year)
        {
            List<SeloTotalFormsDto> listForms = new List<SeloTotalFormsDto>();
            var mainKato = await _dbSetKato.Where(x => x.Code.ToString() == parentKato).FirstOrDefaultAsync();
            if (mainKato != null)
            {
                var childrenOfKatos = await FindChildRecordsAsync(mainKato.Id);
                //var codesOfKatos = await _dbSetKato.Where(x => x.ParentId == mainKato!.Id).Select(x => x.Code.ToString()).ToListAsync();//список всех КАТО
                //codesOfKatos.Insert(0, mainKato!.Code.ToString()); //Вношу главное КАТО, которое при в параметрах
                childrenOfKatos.Insert(0, mainKato);

                foreach (var t in childrenOfKatos)
                {
                    var docId = await _dbSetDoc.Where(x => x.KodNaselPunk == t.Code.ToString() && x.Year == year).Select(x => x.Id).FirstOrDefaultAsync();
                    //if (formId == null) throw new Exception("NotFound");
                    var forms = await (from f in _dbSetForm
                                join k in _dbSetKato on t.Code equals k.Code
                                where f.DocumentId == docId
                                select new SeloTotalFormsDto
                                {
                                    Id = f.Id,
                                    NameOfPlace = k.NameRu,
                                    KodKato = t.Code.ToString(),
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
                                    SobstName = null,
                                    DosVodoSnabKolPunk = f.DosVodoSnabKolPunk,
                                    DosVodoSnabKolChel = f.DosVodoSnabKolChel,
                                    DosVodoSnabPercent = f.DosVodoSnabPercent,
                                    CentrVodoSnabKolNasPun = f.CentrVodoSnabKolNasPun,
                                    CentrVodoSnabKolChel = f.CentrVodoSnabKolChel,
                                    CentrVodoSnabObesKolNasPunk = f.CentrVodoSnabObesKolNasPunk,
                                    CentrVodoSnabObesKolChel = f.CentrVodoSnabObesKolChel,
                                    CentrVodoSnabKolAbon = f.CentrVodoSnabKolAbon,
                                    CentrVodoSnabFizLic = f.CentrVodoSnabFizLic,
                                    CentrVodoSnabYriLic = f.CentrVodoSnabYriLic,
                                    CentrVodoSnabBudzhOrg = f.CentrVodoSnabBudzhOrg,
                                    CentrVodoSnabIndivPriborUchVodyVsego = f.CentrVodoSnabIndivPriborUchVodyVsego,
                                    CentrVodoSnabIndivPriborUchVodyASYE = f.CentrVodoSnabIndivPriborUchVodyASYE,
                                    CentrVodoSnabIndivPriborUchVodyOhvat = f.CentrVodoSnabIndivPriborUchVodyOhvat,
                                    NecVodosnabKolSelsNasPunk = f.NecVodosnabKolSelsNasPunk,
                                    NecVodosnabKbmKolSelsNasPunk = f.NecVodosnabKbmKolSelsNasPunk,
                                    NecVodosnabKbmKolChel = f.NecVodosnabKbmKolChel,
                                    NecVodosnabKbmObespNasel = f.NecVodosnabKbmObespNasel,
                                    NecVodosnabPrvKolSelsNasPunk = f.NecVodosnabPrvKolSelsNasPunk,
                                    NecVodosnabPrvKolChel = f.NecVodosnabPrvKolChel,
                                    NecVodosnabPrvObespNasel = f.NecVodosnabPrvObespNasel,
                                    NecVodosnabPrivVodaKolSelsNasPunk = f.NecVodosnabPrivVodaKolSelsNasPunk,
                                    NecVodosnabPrivVodaKolChel = f.NecVodosnabPrivVodaKolChel,
                                    NecVodosnabPrivVodaObespNasel = f.NecVodosnabPrivVodaObespNasel,
                                    NecVodosnabSkvazhKolSelsNasPunk = f.NecVodosnabSkvazhKolSelsNasPunk,
                                    NecVodosnabSkvazhKolChel = f.NecVodosnabSkvazhKolChel,
                                    NecVodosnabSkvazhObespNasel = f.NecVodosnabSkvazhObespNasel,
                                    NecVodosnabSkvazhKolSelsNasPunkOtkaz = f.NecVodosnabSkvazhKolSelsNasPunkOtkaz,
                                    NecVodosnabSkvazhKolChelOtkaz = f.NecVodosnabSkvazhKolChelOtkaz,
                                    NecVodosnabSkvazhDolyaNaselOtkaz = f.NecVodosnabSkvazhDolyaNaselOtkaz,
                                    NecVodosnabSkvazhDolyaSelOtkaz = f.NecVodosnabSkvazhDolyaSelOtkaz,
                                    CentrVodOtvedKolSelsNasPunk = f.CentrVodOtvedKolSelsNasPunk,
                                    CentrVodOtvedKolChel = f.CentrVodOtvedKolChel,
                                    CentrVodOtvedKolAbonent = f.CentrVodOtvedKolAbonent,
                                    CentrVodOtvedFizLic = f.CentrVodOtvedFizLic,
                                    CentrVodOtvedYriLic = f.CentrVodOtvedYriLic,
                                    CentrVodOtvedBydzhOrg = f.CentrVodOtvedBydzhOrg,
                                    CentrVodOtvedDostypKolNasPunk = f.CentrVodOtvedDostypKolNasPunk,
                                    CentrVodOtvedDostypKolChel = f.CentrVodOtvedDostypKolChel,
                                    CentrVodOtvedNalich = f.CentrVodOtvedNalich,
                                    CentrVodOtvedNalichMechan = f.CentrVodOtvedNalichMechan,
                                    CentrVodOtvedNalichMechanBiolog = f.CentrVodOtvedNalichMechanBiolog,
                                    CentrVodOtvedProizvod = f.CentrVodOtvedProizvod,
                                    CentrVodOtvedIznos = f.CentrVodOtvedIznos,
                                    CentrVodOtvedOhvatKolChel = f.CentrVodOtvedOhvatKolChel,
                                    CentrVodOtvedOhvatNasel = f.CentrVodOtvedOhvatNasel,
                                    CentrVodOtvedFactPostypStochVod = f.CentrVodOtvedFactPostypStochVod,
                                    CentrVodOtvedFactPostypStochVod1 = f.CentrVodOtvedFactPostypStochVod1,
                                    CentrVodOtvedFactPostypStochVod2 = f.CentrVodOtvedFactPostypStochVod2,
                                    CentrVodOtvedFactPostypStochVod3 = f.CentrVodOtvedFactPostypStochVod3,
                                    CentrVodOtvedFactPostypStochVod4 = f.CentrVodOtvedFactPostypStochVod4,
                                    CentrVodOtvedObiemStochVod = f.CentrVodOtvedObiemStochVod,
                                    CentrVodOtvedUrovenNorm = f.CentrVodOtvedUrovenNorm,
                                    DecentrVodoOtvedKolSelsNasPunk = f.DecentrVodoOtvedKolSelsNasPunk,
                                    DecentrVodoOtvedKolChel = f.DecentrVodoOtvedKolChel,
                                    TarifVodoSnabUsred = f.TarifVodoSnabUsred,
                                    TarifVodoSnabFizL = f.TarifVodoSnabFizL,
                                    TarifVodoSnabYriL = f.TarifVodoSnabYriL,
                                    TarifVodoSnabBudzh = f.TarifVodoSnabBudzh,
                                    TarifVodoOtvedUsred = f.TarifVodoOtvedUsred,
                                    TarifVodoOtvedFizL = f.TarifVodoOtvedFizL,
                                    TarifVodoOtvedYriL = f.TarifVodoOtvedYriL,
                                    TarifVodoOtvedBudzh = f.TarifVodoOtvedBudzh,
                                    ProtyzhVodoSeteyObsh = f.ProtyzhVodoSeteyObsh,
                                    ProtyzhVodoSeteyVtomIznos = f.ProtyzhVodoSeteyVtomIznos,
                                    ProtyzhVodoSeteyIznos = f.ProtyzhVodoSeteyIznos,
                                    ProtyzhKanalSeteyObsh = f.ProtyzhKanalSeteyObsh,
                                    ProtyzhKanalSeteyVtomIznos = f.ProtyzhKanalSeteyVtomIznos,
                                    ProtyzhKanalSeteyIznos = f.ProtyzhKanalSeteyIznos,
                                    ProtyzhNewSeteyVodoSnab = f.ProtyzhNewSeteyVodoSnab,
                                    ProtyzhNewSeteyVodoOtved = f.ProtyzhNewSeteyVodoOtved,
                                    ProtyzhRekonSeteyVodoSnab = f.ProtyzhRekonSeteyVodoSnab,
                                    ProtyzhRekonSeteyVodoOtved = f.ProtyzhRekonSeteyVodoOtved,
                                    ProtyzhRemontSeteyVodoSnab = f.ProtyzhRemontSeteyVodoSnab,
                                    ProtyzhRemontSeteyVodoOtved = f.ProtyzhRemontSeteyVodoOtved
                                }).ToListAsync();
                    //if (form == null) throw new Exception("NotFoundReport");
                    if (forms != null) listForms.AddRange(forms);
                }
            }
            return listForms;
        }

        public byte[] GenerateExcelFile(List<SeloTotalFormsDto> forms)
        {
            //var doc = _dbSetDoc.Where(x=>x.SeloFormId==form.Id).FirstOrDefault();
            //string? NamePlace = _dbSetKato.Where(x=>x.Code.ToString() == doc.KodNaselPunk).Select(x=>x.NameRu).FirstOrDefault();
            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("SeloForms");
                worksheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Row(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                //worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Row(1).Height = 200;

                //worksheet.Row(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;                
                //worksheet.Row(2).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;                                

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
                
                for(int i = 1; i <= 90; i++)
                {
                    worksheet.Cells[2, i].Value = i;
                }

                int counter = 0;
                foreach (var form in forms)
                {
                    counter = counter + 1;
                    //Значения колонок
                    worksheet.Cells[counter + 2, 1].Value = form.Id;
                    worksheet.Cells[counter + 2, 2].Value = form.NameOfPlace;
                    worksheet.Cells[counter + 2, 3].Value = form.KodKato;                    
                    worksheet.Cells[counter + 2, 4].Value = form.StatusOpor == true ? "Да" : "Нет";
                    worksheet.Cells[counter + 2, 5].Value = form.StatusSput == true ? "да" : "нет";
                    worksheet.Cells[counter + 2, 6].Value = form.StatusProch;
                    worksheet.Cells[counter + 2, 7].Value = form.StatusPrigran == true ? "да" : "нет";
                    worksheet.Cells[counter + 2, 8].Value = form.ObshKolSelNasPun;
                    worksheet.Cells[counter + 2, 9].Value = form.ObshKolChelNasPun;
                    worksheet.Cells[counter + 2, 10].Value = form.ObshKolDomHoz;
                    worksheet.Cells[counter + 2, 11].Value = form.YearSystVodoSnab;
                    //worksheet.Cells[counter + 2, 11].Style.Numberformat.Format = "yyyy-MM-dd";
                    worksheet.Cells[counter + 2, 12].Value = form.ObslPredpBin;
                    worksheet.Cells[counter + 2, 13].Value = form.ObslPredpName;
                    worksheet.Cells[counter + 2, 14].Value = form.SobstName;
                    worksheet.Cells[counter + 2, 15].Value = form.SobstName;
                    worksheet.Cells[counter + 2, 16].Value = form.DosVodoSnabKolPunk;
                    worksheet.Cells[counter + 2, 17].Value = form.DosVodoSnabKolChel;
                    worksheet.Cells[counter + 2, 18].Value = form.DosVodoSnabPercent;
                    worksheet.Cells[counter + 2, 19].Value = form.CentrVodoSnabKolNasPun;
                    worksheet.Cells[counter + 2, 20].Value = form.CentrVodoSnabKolChel;
                    worksheet.Cells[counter + 2, 21].Value = form.CentrVodoSnabObesKolNasPunk;
                    worksheet.Cells[counter + 2, 22].Value = form.CentrVodoSnabObesKolChel;
                    worksheet.Cells[counter + 2, 23].Value = form.CentrVodoSnabKolAbon;
                    worksheet.Cells[counter + 2, 24].Value = form.CentrVodoSnabFizLic;
                    worksheet.Cells[counter + 2, 25].Value = form.CentrVodoSnabYriLic;
                    worksheet.Cells[counter + 2, 26].Value = form.CentrVodoSnabBudzhOrg;
                    worksheet.Cells[counter + 2, 27].Value = form.CentrVodoSnabIndivPriborUchVodyVsego;
                    worksheet.Cells[counter + 2, 28].Value = form.CentrVodoSnabIndivPriborUchVodyASYE;
                    worksheet.Cells[counter + 2, 29].Value = form.CentrVodoSnabIndivPriborUchVodyOhvat;
                    worksheet.Cells[counter + 2, 30].Value = form.NecVodosnabKolSelsNasPunk;
                    worksheet.Cells[counter + 2, 31].Value = form.NecVodosnabKbmKolSelsNasPunk;
                    worksheet.Cells[counter + 2, 32].Value = form.NecVodosnabKbmKolChel;
                    worksheet.Cells[counter + 2, 33].Value = form.NecVodosnabKbmObespNasel;
                    worksheet.Cells[counter + 2, 34].Value = form.NecVodosnabPrvKolSelsNasPunk;
                    worksheet.Cells[counter + 2, 35].Value = form.NecVodosnabPrvKolChel;
                    worksheet.Cells[counter + 2, 36].Value = form.NecVodosnabPrvObespNasel;
                    worksheet.Cells[counter + 2, 37].Value = form.NecVodosnabPrivVodaKolSelsNasPunk;
                    worksheet.Cells[counter + 2, 38].Value = form.NecVodosnabPrivVodaKolChel;
                    worksheet.Cells[counter + 2, 39].Value = form.NecVodosnabPrivVodaObespNasel;
                    worksheet.Cells[counter + 2, 40].Value = form.NecVodosnabSkvazhKolSelsNasPunk;
                    worksheet.Cells[counter + 2, 41].Value = form.NecVodosnabSkvazhKolChel;
                    worksheet.Cells[counter + 2, 42].Value = form.NecVodosnabSkvazhObespNasel;
                    worksheet.Cells[counter + 2, 43].Value = form.NecVodosnabSkvazhKolSelsNasPunkOtkaz;
                    worksheet.Cells[counter + 2, 44].Value = form.NecVodosnabSkvazhKolChelOtkaz;
                    worksheet.Cells[counter + 2, 45].Value = form.NecVodosnabSkvazhDolyaNaselOtkaz;
                    worksheet.Cells[counter + 2, 46].Value = form.NecVodosnabSkvazhDolyaSelOtkaz;
                    worksheet.Cells[counter + 2, 47].Value = form.CentrVodOtvedKolSelsNasPunk;
                    worksheet.Cells[counter + 2, 48].Value = form.CentrVodOtvedKolChel;
                    worksheet.Cells[counter + 2, 49].Value = form.CentrVodOtvedKolAbonent;
                    worksheet.Cells[counter + 2, 50].Value = form.CentrVodOtvedFizLic;
                    worksheet.Cells[counter + 2, 51].Value = form.CentrVodOtvedYriLic;
                    worksheet.Cells[counter + 2, 52].Value = form.CentrVodOtvedBydzhOrg;
                    worksheet.Cells[counter + 2, 53].Value = form.CentrVodOtvedDostypKolNasPunk;
                    worksheet.Cells[counter + 2, 54].Value = form.CentrVodOtvedDostypKolChel;
                    worksheet.Cells[counter + 2, 55].Value = form.CentrVodOtvedNalich;
                    worksheet.Cells[counter + 2, 56].Value = form.CentrVodOtvedNalichMechan;
                    worksheet.Cells[counter + 2, 57].Value = form.CentrVodOtvedNalichMechanBiolog;
                    worksheet.Cells[counter + 2, 58].Value = form.CentrVodOtvedProizvod;
                    worksheet.Cells[counter + 2, 59].Value = form.CentrVodOtvedIznos;
                    worksheet.Cells[counter + 2, 60].Value = form.CentrVodOtvedOhvatKolChel;
                    worksheet.Cells[counter + 2, 61].Value = form.CentrVodOtvedOhvatNasel;
                    worksheet.Cells[counter + 2, 62].Value = form.CentrVodOtvedFactPostypStochVod;
                    worksheet.Cells[counter + 2, 63].Value = form.CentrVodOtvedFactPostypStochVod1;
                    worksheet.Cells[counter + 2, 64].Value = form.CentrVodOtvedFactPostypStochVod2;
                    worksheet.Cells[counter + 2, 65].Value = form.CentrVodOtvedFactPostypStochVod3;
                    worksheet.Cells[counter + 2, 66].Value = form.CentrVodOtvedFactPostypStochVod4;
                    worksheet.Cells[counter + 2, 67].Value = form.CentrVodOtvedObiemStochVod;
                    worksheet.Cells[counter + 2, 68].Value = form.CentrVodOtvedUrovenNorm;
                    worksheet.Cells[counter + 2, 69].Value = form.DecentrVodoOtvedKolSelsNasPunk;
                    worksheet.Cells[counter + 2, 70].Value = form.DecentrVodoOtvedKolChel;
                    worksheet.Cells[counter + 2, 71].Value = form.TarifVodoSnabUsred;
                    worksheet.Cells[counter + 2, 72].Value = form.TarifVodoSnabFizL;
                    worksheet.Cells[counter + 2, 73].Value = form.TarifVodoSnabYriL;
                    worksheet.Cells[counter + 2, 74].Value = form.TarifVodoSnabBudzh;
                    worksheet.Cells[counter + 2, 75].Value = form.TarifVodoOtvedUsred;
                    worksheet.Cells[counter + 2, 76].Value = form.TarifVodoOtvedFizL;
                    worksheet.Cells[counter + 2, 77].Value = form.TarifVodoOtvedYriL;
                    worksheet.Cells[counter + 2, 78].Value = form.TarifVodoOtvedBudzh;
                    worksheet.Cells[counter + 2, 79].Value = form.ProtyzhVodoSeteyObsh;
                    worksheet.Cells[counter + 2, 80].Value = form.ProtyzhVodoSeteyVtomIznos;
                    worksheet.Cells[counter + 2, 81].Value = form.ProtyzhVodoSeteyIznos;
                    worksheet.Cells[counter + 2, 82].Value = form.ProtyzhKanalSeteyObsh;
                    worksheet.Cells[counter + 2, 83].Value = form.ProtyzhKanalSeteyVtomIznos;
                    worksheet.Cells[counter + 2, 84].Value = form.ProtyzhKanalSeteyIznos;
                    worksheet.Cells[counter + 2, 85].Value = form.ProtyzhNewSeteyVodoSnab;
                    worksheet.Cells[counter + 2, 86].Value = form.ProtyzhNewSeteyVodoOtved;
                    worksheet.Cells[counter + 2, 87].Value = form.ProtyzhRekonSeteyVodoSnab;
                    worksheet.Cells[counter + 2, 88].Value = form.ProtyzhRekonSeteyVodoOtved;
                    worksheet.Cells[counter + 2, 89].Value = form.ProtyzhRemontSeteyVodoSnab;
                    worksheet.Cells[counter + 2, 90].Value = form.ProtyzhRemontSeteyVodoOtved;                    
                }

                // Определите диапазон данных
                var range = worksheet.Cells[1, 1, forms.Count + 2, 90];

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

        public byte[] GenerateExcelTemplate()
        {            
            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("SeloForms");
                worksheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Row(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                //worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Row(1).Height = 200;
                
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
                worksheet.Cells[1, 2, 1, 90].Style.WrapText = true;

                for (int i = 1; i <= 90; i++)
                {
                    worksheet.Cells[2, i].Value = i;
                }                                

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

        public async Task<int> ImportExcel(IFormFile file, string login, int year)
        {
            if (_dbSetAccount.Any(x => x.Login != login)) throw new Exception($"Данного логина не существует - {login}");
            var listSeloForms = new List<SeloForm>();

            using(var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using(var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets[0];
                    var rowCount = workSheet.Dimension.Rows;

                    for(int row = 3;  row <= rowCount; row++)
                    {
                        //var seloForm =  await _dbSetForm.Where(x => x.Id.ToString() == workSheet.Cells[row, 1].Text).FirstOrDefaultAsync();
                        var doc = new SeloDocument();
                        var seloForm = new SeloForm();
                        //var supply = new SeloWaterSupply();
                        //var disposal = new SeloWaterDisposal();
                        //var tarif = new SeloTariff();
                        //var network = new SeloNetworkLength();                        

                        if(workSheet.Cells[row, 1].Text != "") 
                            seloForm.Id = Guid.Parse(workSheet.Cells[row, 1].Text);
                        doc.KodNaselPunk = workSheet.Cells[row, 3].Text;
                        doc.Year = year;
                        doc.Login = login;
                        if (_dbSetDoc.Any(x => x.KodNaselPunk == doc.KodNaselPunk && x.Year == year))
                            break;

                        seloForm.StatusOpor = workSheet.Cells[row, 4].Text.ToLower() == "да" ? true : false;
                        seloForm.StatusSput = workSheet.Cells[row, 5].Text.ToLower() == "да" ? true : false;
                        seloForm.StatusProch = workSheet.Cells[row, 6].Text;
                        seloForm.StatusPrigran = workSheet.Cells[row, 7].Text.ToLower() == "да" ? true : false;
                        if(workSheet.Cells[row, 8].Text != "") seloForm.ObshKolSelNasPun = int.Parse(workSheet.Cells[row, 8].Text);
                        if(workSheet.Cells[row, 9].Text != "") seloForm.ObshKolChelNasPun = int.Parse(workSheet.Cells[row, 9].Text);
                        if(workSheet.Cells[row, 10].Text != "") seloForm.ObshKolDomHoz = int.Parse(workSheet.Cells[row, 10].Text);
                        seloForm.YearSystVodoSnab = workSheet.Cells[row, 11].Text;
                        seloForm.ObslPredpId = null; //TODO
                        seloForm.SobstId = null; //TODO
                        if (workSheet.Cells[row, 16].Text != "") seloForm.DosVodoSnabKolPunk = int.Parse(workSheet.Cells[row, 16].Text);
                        if (workSheet.Cells[row, 17].Text != "") seloForm.DosVodoSnabKolChel = int.Parse(workSheet.Cells[row, 17].Text);
                        if (workSheet.Cells[row, 18].Text != "") seloForm.DosVodoSnabPercent = decimal.Parse(workSheet.Cells[row, 18].Text);
                        if (workSheet.Cells[row, 19].Text != "") seloForm.CentrVodoSnabKolNasPun = int.Parse(workSheet.Cells[row, 19].Text);
                        if (workSheet.Cells[row, 20].Text != "") seloForm.CentrVodoSnabKolChel = int.Parse(workSheet.Cells[row, 20].Text);
                        if (workSheet.Cells[row, 21].Text != "") seloForm.CentrVodoSnabObesKolNasPunk = decimal.Parse(workSheet.Cells[row, 21].Text);
                        if (workSheet.Cells[row, 22].Text != "") seloForm.CentrVodoSnabObesKolChel = decimal.Parse(workSheet.Cells[row, 22].Text);
                        if (workSheet.Cells[row, 23].Text != "") seloForm.CentrVodoSnabKolAbon = int.Parse(workSheet.Cells[row, 23].Text);
                        if (workSheet.Cells[row, 24].Text != "") seloForm.CentrVodoSnabFizLic = int.Parse(workSheet.Cells[row, 24].Text);
                        if (workSheet.Cells[row, 25].Text != "") seloForm.CentrVodoSnabYriLic = int.Parse(workSheet.Cells[row, 25].Text);
                        if (workSheet.Cells[row, 26].Text != "") seloForm.CentrVodoSnabBudzhOrg = int.Parse(workSheet.Cells[row, 26].Text);
                        if (workSheet.Cells[row, 27].Text != "") seloForm.CentrVodoSnabIndivPriborUchVodyVsego = int.Parse(workSheet.Cells[row, 27].Text);
                        if (workSheet.Cells[row, 28].Text != "") seloForm.CentrVodoSnabIndivPriborUchVodyASYE = int.Parse(workSheet.Cells[row, 28].Text);
                        if (workSheet.Cells[row, 29].Text != "") seloForm.CentrVodoSnabIndivPriborUchVodyOhvat = decimal.Parse(workSheet.Cells[row, 29].Text);
                        if (workSheet.Cells[row, 30].Text != "") seloForm.NecVodosnabKolSelsNasPunk = int.Parse(workSheet.Cells[row, 30].Text);
                        if (workSheet.Cells[row, 31].Text != "") seloForm.NecVodosnabKbmKolSelsNasPunk = int.Parse(workSheet.Cells[row, 31].Text);
                        if (workSheet.Cells[row, 32].Text != "") seloForm.NecVodosnabKbmKolChel = int.Parse(workSheet.Cells[row, 32].Text);
                        if (workSheet.Cells[row, 33].Text != "") seloForm.NecVodosnabKbmObespNasel = decimal.Parse(workSheet.Cells[row, 33].Text);
                        if (workSheet.Cells[row, 34].Text != "") seloForm.NecVodosnabPrvKolSelsNasPunk = int.Parse(workSheet.Cells[row, 34].Text);
                        if (workSheet.Cells[row, 35].Text != "") seloForm.NecVodosnabPrvKolChel = int.Parse(workSheet.Cells[row, 35].Text);
                        if (workSheet.Cells[row, 36].Text != "") seloForm.NecVodosnabPrvObespNasel = decimal.Parse(workSheet.Cells[row, 36].Text);
                        if (workSheet.Cells[row, 37].Text != "") seloForm.NecVodosnabPrivVodaKolSelsNasPunk = int.Parse(workSheet.Cells[row, 37].Text);
                        if (workSheet.Cells[row, 38].Text != "") seloForm.NecVodosnabPrivVodaKolChel = int.Parse(workSheet.Cells[row, 38].Text);
                        if (workSheet.Cells[row, 39].Text != "") seloForm.NecVodosnabPrivVodaObespNasel = decimal.Parse(workSheet.Cells[row, 39].Text);
                        if (workSheet.Cells[row, 40].Text != "") seloForm.NecVodosnabSkvazhKolSelsNasPunk = int.Parse(workSheet.Cells[row, 40].Text);
                        if (workSheet.Cells[row, 41].Text != "") seloForm.NecVodosnabSkvazhKolChel = int.Parse(workSheet.Cells[row, 41].Text);
                        if (workSheet.Cells[row, 42].Text != "") seloForm.NecVodosnabSkvazhObespNasel = decimal.Parse(workSheet.Cells[row, 42].Text);
                        if (workSheet.Cells[row, 43].Text != "") seloForm.NecVodosnabSkvazhKolSelsNasPunkOtkaz = int.Parse(workSheet.Cells[row, 43].Text);
                        if (workSheet.Cells[row, 44].Text != "") seloForm.NecVodosnabSkvazhKolChelOtkaz = int.Parse(workSheet.Cells[row, 44].Text);
                        if (workSheet.Cells[row, 45].Text != "") seloForm.NecVodosnabSkvazhDolyaNaselOtkaz = decimal.Parse(workSheet.Cells[row, 45].Text);
                        if (workSheet.Cells[row, 46].Text != "") seloForm.NecVodosnabSkvazhDolyaSelOtkaz = decimal.Parse(workSheet.Cells[row, 46].Text);

                        if (workSheet.Cells[row, 47].Text != "") seloForm.CentrVodOtvedKolSelsNasPunk = int.Parse(workSheet.Cells[row, 47].Text);
                        if (workSheet.Cells[row, 48].Text != "") seloForm.CentrVodOtvedKolChel = int.Parse(workSheet.Cells[row, 48].Text);
                        if (workSheet.Cells[row, 49].Text != "") seloForm.CentrVodOtvedKolAbonent = int.Parse(workSheet.Cells[row, 49].Text);
                        if (workSheet.Cells[row, 50].Text != "") seloForm.CentrVodOtvedFizLic = int.Parse(workSheet.Cells[row, 50].Text);
                        if (workSheet.Cells[row, 51].Text != "") seloForm.CentrVodOtvedYriLic = int.Parse(workSheet.Cells[row, 51].Text);
                        if (workSheet.Cells[row, 52].Text != "") seloForm.CentrVodOtvedBydzhOrg = int.Parse(workSheet.Cells[row, 52].Text);
                        if (workSheet.Cells[row, 53].Text != "") seloForm.CentrVodOtvedDostypKolNasPunk = decimal.Parse(workSheet.Cells[row, 53].Text);
                        if (workSheet.Cells[row, 54].Text != "") seloForm.CentrVodOtvedDostypKolChel = decimal.Parse(workSheet.Cells[row, 54].Text);
                        if (workSheet.Cells[row, 55].Text != "") seloForm.CentrVodOtvedNalich = int.Parse(workSheet.Cells[row, 55].Text);
                        if (workSheet.Cells[row, 56].Text != "") seloForm.CentrVodOtvedNalichMechan = int.Parse(workSheet.Cells[row, 56].Text);
                        if (workSheet.Cells[row, 57].Text != "") seloForm.CentrVodOtvedNalichMechanBiolog = int.Parse(workSheet.Cells[row, 57].Text);
                        if (workSheet.Cells[row, 58].Text != "") seloForm.CentrVodOtvedProizvod = int.Parse(workSheet.Cells[row, 58].Text);
                        if (workSheet.Cells[row, 59].Text != "") seloForm.CentrVodOtvedIznos = decimal.Parse(workSheet.Cells[row, 59].Text);
                        if (workSheet.Cells[row, 60].Text != "") seloForm.CentrVodOtvedOhvatKolChel = int.Parse(workSheet.Cells[row, 60].Text);
                        if (workSheet.Cells[row, 61].Text != "") seloForm.CentrVodOtvedOhvatNasel = decimal.Parse(workSheet.Cells[row, 61].Text);
                        if (workSheet.Cells[row, 62].Text != "") seloForm.CentrVodOtvedFactPostypStochVod = int.Parse(workSheet.Cells[row, 62].Text);
                        if (workSheet.Cells[row, 63].Text != "") seloForm.CentrVodOtvedFactPostypStochVod1 = int.Parse(workSheet.Cells[row, 63].Text);
                        if (workSheet.Cells[row, 64].Text != "") seloForm.CentrVodOtvedFactPostypStochVod2 = int.Parse(workSheet.Cells[row, 64].Text);
                        if (workSheet.Cells[row, 65].Text != "") seloForm.CentrVodOtvedFactPostypStochVod3 = int.Parse(workSheet.Cells[row, 65].Text);
                        if (workSheet.Cells[row, 66].Text != "") seloForm.CentrVodOtvedFactPostypStochVod4 = int.Parse(workSheet.Cells[row, 66].Text);
                        if (workSheet.Cells[row, 67].Text != "") seloForm.CentrVodOtvedObiemStochVod = int.Parse(workSheet.Cells[row, 67].Text);
                        if (workSheet.Cells[row, 68].Text != "") seloForm.CentrVodOtvedUrovenNorm = decimal.Parse(workSheet.Cells[row, 68].Text);
                        if (workSheet.Cells[row, 69].Text != "") seloForm.DecentrVodoOtvedKolSelsNasPunk = int.Parse(workSheet.Cells[row, 69].Text);
                        if (workSheet.Cells[row, 70].Text != "") seloForm.DecentrVodoOtvedKolChel = int.Parse(workSheet.Cells[row, 70].Text);

                        if (workSheet.Cells[row, 71].Text != "") seloForm.TarifVodoSnabUsred = int.Parse(workSheet.Cells[row, 71].Text);
                        if (workSheet.Cells[row, 72].Text != "") seloForm.TarifVodoSnabFizL = int.Parse(workSheet.Cells[row, 72].Text);
                        if (workSheet.Cells[row, 73].Text != "") seloForm.TarifVodoSnabYriL = int.Parse(workSheet.Cells[row, 73].Text);
                        if (workSheet.Cells[row, 74].Text != "") seloForm.TarifVodoSnabBudzh = int.Parse(workSheet.Cells[row, 74].Text);
                        if (workSheet.Cells[row, 75].Text != "") seloForm.TarifVodoOtvedUsred = int.Parse(workSheet.Cells[row, 75].Text);
                        if (workSheet.Cells[row, 76].Text != "") seloForm.TarifVodoOtvedFizL = int.Parse(workSheet.Cells[row, 76].Text);
                        if (workSheet.Cells[row, 77].Text != "") seloForm.TarifVodoOtvedYriL = int.Parse(workSheet.Cells[row, 77].Text);
                        if (workSheet.Cells[row, 78].Text != "") seloForm.TarifVodoOtvedBudzh = int.Parse(workSheet.Cells[row, 78].Text);

                        if (workSheet.Cells[row, 79].Text != "") seloForm.ProtyzhVodoSeteyObsh = int.Parse(workSheet.Cells[row, 79].Text);
                        if (workSheet.Cells[row, 80].Text != "") seloForm.ProtyzhVodoSeteyVtomIznos = int.Parse(workSheet.Cells[row, 80].Text);
                        if (workSheet.Cells[row, 81].Text != "") seloForm.ProtyzhVodoSeteyIznos = decimal.Parse(workSheet.Cells[row, 81].Text);
                        if (workSheet.Cells[row, 82].Text != "") seloForm.ProtyzhKanalSeteyObsh = int.Parse(workSheet.Cells[row, 82].Text);
                        if (workSheet.Cells[row, 83].Text != "") seloForm.ProtyzhKanalSeteyVtomIznos = int.Parse(workSheet.Cells[row, 83].Text);
                        if (workSheet.Cells[row, 84].Text != "") seloForm.ProtyzhKanalSeteyIznos = decimal.Parse(workSheet.Cells[row, 84].Text);
                        if (workSheet.Cells[row, 85].Text != "") seloForm.ProtyzhNewSeteyVodoSnab = int.Parse(workSheet.Cells[row, 85].Text);
                        if (workSheet.Cells[row, 86].Text != "") seloForm.ProtyzhNewSeteyVodoOtved = int.Parse(workSheet.Cells[row, 86].Text);
                        if (workSheet.Cells[row, 87].Text != "") seloForm.ProtyzhRekonSeteyVodoSnab = int.Parse(workSheet.Cells[row, 87].Text);
                        if (workSheet.Cells[row, 88].Text != "") seloForm.ProtyzhRekonSeteyVodoOtved = int.Parse(workSheet.Cells[row, 88].Text);
                        if (workSheet.Cells[row, 89].Text != "") seloForm.ProtyzhRemontSeteyVodoSnab = int.Parse(workSheet.Cells[row, 89].Text);
                        if (workSheet.Cells[row, 90].Text != "") seloForm.ProtyzhRemontSeteyVodoOtved = int.Parse(workSheet.Cells[row, 90].Text);
                        
                        
                        
                        var katoRecord = _dbSetKato.Where(x => x.Code.ToString() == doc.KodNaselPunk).FirstOrDefault();
                        if (katoRecord != null && katoRecord.ParentId != 0)
                        {
                            var oblastKato = await FindParentRecordAsync(katoRecord.ParentId, 0);
                            if (oblastKato != null) doc.KodOblast = oblastKato.Code.ToString();
                        }
                        if (katoRecord != null && katoRecord.ParentId != 0)
                        {
                            var raionKato = await FindParentRecordAsync(katoRecord.ParentId, 3);
                            if (raionKato != null) doc.KodRaiona = raionKato.Code.ToString();
                        }

                        await _dbSetDoc.AddAsync(doc);
                        await _context.SaveChangesAsync();
                        seloForm.DocumentId = doc.Id;

                        listSeloForms.Add(seloForm);                       
                    }
                }
            }

            await _dbSetForm.AddRangeAsync(listSeloForms);            
            await _context.SaveChangesAsync();

            return listSeloForms.Count();
        }        

        private async Task<Ref_Kato?> FindParentRecordAsync(int parentId, int katoLevel)
        {
            var currentRecord = await _dbSetKato.Where(x=>x.Id== parentId).FirstOrDefaultAsync();
            if(currentRecord != null && currentRecord.KatoLevel != katoLevel && currentRecord.ParentId != 0)
            {
                return await FindParentRecordAsync(currentRecord.ParentId, katoLevel);
            }
            return currentRecord;
        }

        private async Task<List<Ref_Kato>> FindChildRecordsAsync(int parentId)
        {
            var children = await _dbSetKato.Where(e => e.ParentId == parentId).ToListAsync();

            var allDescendants = new List<Ref_Kato>(children);

            foreach (var child in children)
            {
                allDescendants.AddRange(await FindChildRecordsAsync(child.Id));
            }

            return allDescendants;
        }

    }
}
