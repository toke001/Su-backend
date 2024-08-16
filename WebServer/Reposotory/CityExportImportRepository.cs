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
    public class CityExportImportRepository : ICityExportImport
    {
        private readonly WaterDbContext _context;
        private readonly DbSet<CityForm> _dbSetForm;
        private readonly DbSet<CityDocument> _dbSetDoc;
        private readonly DbSet<Ref_Kato> _dbSetKato;
        private readonly DbSet<Account> _dbSetAccount;

        public CityExportImportRepository(WaterDbContext context)
        {
            _context = context;
            _dbSetForm = _context.Set<CityForm>();
            _dbSetDoc = _context.Set<CityDocument>();
            _dbSetKato = _context.Set<Ref_Kato>();
            _dbSetAccount = _context.Set<Account>();
        }

        public async Task<List<CityTotalFormsDto>> GetCityTotalFormsAsync(string kato, int year)
        {
            List<CityTotalFormsDto> forms = new List<CityTotalFormsDto>();
            var mainKato = await _dbSetKato.Where(x=>x.Code.ToString() == kato && x.IsReportable == true).FirstOrDefaultAsync();
            if (mainKato != null)
            {
                var codesOfKatos = await _dbSetKato.Where(x=>x.ParentId==mainKato!.Id).Select(x=>x.Code.ToString()).ToListAsync();//список всех КАТО
                codesOfKatos.Insert(0, mainKato!.Code.ToString()); //Вношу главное КАТО, которое при в параметрах

                foreach (var t in codesOfKatos)
                {
                    var formId = await _dbSetDoc.Where(x => x.KodNaselPunk == t && x.Year == year).Select(x => x.CityFormId).FirstOrDefaultAsync();
                    //if (formId == null) throw new Exception("NotFound");
                    var form = (from f in _dbSetForm
                                //from ws in _dbSetSupply.Where(x=>x.IdForm== f.Id).DefaultIfEmpty()
                                //from wd in _dbSetDisposal.Where(x => x.IdForm == f.Id).DefaultIfEmpty()
                                //from tr in _dbSetTarif.Where(x => x.IdForm == f.Id).DefaultIfEmpty()
                                //from n in _dbSetNetwork.Where(x => x.IdForm == f.Id).DefaultIfEmpty()   
                                join k in _dbSetKato on t equals k.Code.ToString()
                                where f.Id == formId
                                select new CityTotalFormsDto
                                {
                                    Id = f.Id,
                                    NameOfPlace = k.NameRu,
                                    KodKato = t,
                                    TotalCountCityOblast = f.TotalCountCityOblast,
                                    TotalCountDomHoz = f.TotalCountDomHoz,
                                    TotalCountChel = f.TotalCountChel,
                                    ObslPredpBin = "",
                                    ObslPredpName = "",
                                    VodoSnabKolichAbonent = f.VodoSnabKolichAbonent,
                                    VodoSnabKolFizLic = f.VodoSnabKolFizLic,
                                    VodoSnabKolYriLic = f.VodoSnabKolYriLic,
                                    VodoSnabKolBydzhOrg = f.VodoSnabKolBydzhOrg,
                                    VodoSnabKolChelDostyp = f.VodoSnabKolChelDostyp,
                                    VodoSnabObespechCentrlVodo = f.VodoSnabObespechCentrlVodo,
                                    VodoSnabIndivUchetVodyVsego = f.VodoSnabIndivUchetVodyVsego,
                                    VodoSnabIndivUchetVodyDistance = f.VodoSnabIndivUchetVodyDistance,
                                    VodoSnabIndivUchetVodyPercent = f.VodoSnabIndivUchetVodyPercent,
                                    VodoSnabObshePodlezhashKolZdan = f.VodoSnabObshePodlezhashKolZdan,
                                    VodoSnabObsheUstanKolZdan = f.VodoSnabObsheUstanKolZdan,
                                    VodoSnabObsheUstanPriborKol = f.VodoSnabObsheUstanPriborKol,
                                    VodoSnabObsheUstanDistanceKol = f.VodoSnabObsheUstanDistanceKol,
                                    VodoSnabObsheOhvatPercent = f.VodoSnabObsheOhvatPercent,
                                    VodoSnabAutoProccesVodoZabor = f.VodoSnabAutoProccesVodoZabor,
                                    VodoSnabAutoProccesVodoPodgot = f.VodoSnabAutoProccesVodoPodgot,
                                    VodoSnabAutoProccesNasosStanc = f.VodoSnabAutoProccesNasosStanc,
                                    VodoSnabAutoProccesSetVodosnab = f.VodoSnabAutoProccesSetVodosnab,
                                    VodoOtvKolAbonent = f.VodoOtvKolAbonent,
                                    VodoOtvKolAbonFizLic = f.VodoOtvKolAbonFizLic,
                                    VodoOtvKolAbonYriLic = f.VodoOtvKolAbonYriLic,
                                    VodoOtvKolBydzhetOrg = f.VodoOtvKolBydzhetOrg,
                                    VodoOtvKolChelOhvatCentrVodo = f.VodoOtvKolChelOhvatCentrVodo,
                                    VodoOtvDostypCentrVodo = f.VodoOtvDostypCentrVodo,
                                    VodoOtvKolichKanaliz = f.VodoOtvKolichKanaliz,
                                    VodoOtvKolichKanalizMechan = f.VodoOtvKolichKanalizMechan,
                                    VodoOtvKolichKanalizMechanBiolog = f.VodoOtvKolichKanalizMechanBiolog,
                                    VodoOtvProizvodKanaliz = f.VodoOtvProizvodKanaliz,
                                    VodoOtvIznosKanaliz = f.VodoOtvIznosKanaliz,
                                    VodoOtvKolChelKanaliz = f.VodoOtvKolChelKanaliz,
                                    VodoOtvOhvatChelKanaliz = f.VodoOtvOhvatChelKanaliz,
                                    VodoOtvFactPostypKanaliz = f.VodoOtvFactPostypKanaliz,
                                    VodoOtvFactPostypKanaliz1kv = f.VodoOtvFactPostypKanaliz1kv,
                                    VodoOtvFactPostypKanaliz2kv = f.VodoOtvFactPostypKanaliz2kv,
                                    VodoOtvFactPostypKanaliz3kv = f.VodoOtvFactPostypKanaliz3kv,
                                    VodoOtvFactPostypKanaliz4kv = f.VodoOtvFactPostypKanaliz4kv,
                                    VodoOtvObiemKanalizNormOchist = f.VodoOtvObiemKanalizNormOchist,
                                    VodoOtvUrovenNormOchishVody = f.VodoOtvUrovenNormOchishVody,
                                    VodoOtvAutoProccesSetKanaliz = f.VodoOtvAutoProccesSetKanaliz,
                                    VodoOtvAutoProccesKanalizNasos = f.VodoOtvAutoProccesKanalizNasos,
                                    VodoOtvAutoProccesKanalizSooruzh = f.VodoOtvAutoProccesKanalizSooruzh,
                                    UrTarVodoSnabUsrednen = f.UrTarVodoSnabUsrednen,
                                    UrTarVodoSnabFizLic = f.UrTarVodoSnabFizLic,
                                    UrTarVodoSnabYriLic = f.UrTarVodoSnabYriLic,
                                    UrTarVodoSnabBydzhOrg = f.UrTarVodoSnabBydzhOrg,
                                    UrTarVodoOtvedUsred = f.UrTarVodoOtvedUsred,
                                    UrTarVodoOtvedFizLic = f.UrTarVodoOtvedFizLic,
                                    UrTarVodoOtvedYriLic = f.UrTarVodoOtvedYriLic,
                                    UrTarVodoOtvedBydzhOrg = f.UrTarVodoOtvedBydzhOrg,
                                    LengthVodoProvodTotal = f.LengthVodoProvodTotal,
                                    LengthVodoProvodIznos = f.LengthVodoProvodIznos,
                                    LengthVodoProvodIznosPercent = f.LengthVodoProvodIznosPercent,
                                    LengthKanalizTotal = f.LengthKanalizTotal,
                                    LengthKanalizIznos = f.LengthKanalizIznos,
                                    LengthKanalizIznosPercent = f.LengthKanalizIznosPercent,
                                    LengthObshNewSetiVodo = f.LengthObshNewSetiVodo,
                                    LengthObshNewSetiKanaliz = f.LengthObshNewSetiKanaliz,
                                    LengthObshZamenSetiVodo = f.LengthObshZamenSetiVodo,
                                    LengthObshZamenSetiKanaliz = f.LengthObshZamenSetiKanaliz,
                                    LengthObshRemontSetiVodo = f.LengthObshRemontSetiVodo,
                                    LengthObshRemontSetiKanaliz = f.LengthObshRemontSetiKanaliz
                                }).FirstOrDefault();
                    //if (form == null) throw new Exception("NotFoundReport");
                    if(form != null) forms.Add(form);
                }                
            }
            return forms;
        }

        public byte[] GenerateExcelFile(List<CityTotalFormsDto> forms)
        {
            //var doc = _dbSetDoc.Where(x=>x.SeloFormId==form.Id).FirstOrDefault();
            //string? NamePlace = _dbSetKato.Where(x=>x.Code.ToString() == doc.KodNaselPunk).Select(x=>x.NameRu).FirstOrDefault();
            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("CityForm");
                worksheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Row(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                //worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Row(1).Height = 200;

                //worksheet.Row(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;                
                //worksheet.Row(2).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;                                

                //Названия колонок
                worksheet.Cells[1, 1].Value = "Код строк";                
                worksheet.Cells[1, 2].Value = "Наименование области, города";                                
                worksheet.Cells[1, 3].Value = "Код области, города по классификатору административно-территориальных объектов";                
                worksheet.Cells[1, 4].Value = "Общее количество городов в области (единиц)";
                worksheet.Cells[1, 5].Value = "Общее количество домохозяйств (кв, ИЖД)";
                worksheet.Cells[1, 6].Value = "Общее количество проживающих в городских населенных пунктах (человек)";                
                worksheet.Cells[1, 7].Value = "Обслуживающее предприятие БИН";                
                worksheet.Cells[1, 8].Value = "Обслуживающее предприятие Наименование";
                worksheet.Cells[1, 9].Value = "Водоснабжение. Количество абонентов, охваченных централизованным водоснабжением (единиц)";                
                worksheet.Cells[1, 10].Value = "Водоснабжение. физических лиц/население (единиц)";
                worksheet.Cells[1, 11].Value = "Водоснабжение.юридических лиц (единиц)";
                worksheet.Cells[1, 12].Value = "Водоснабжение. бюджетных организаций (единиц)";
                worksheet.Cells[1, 13].Value = "Водоснабжение. Количество населения имеющих доступ к  централизованному водоснабжению (человек)";
                worksheet.Cells[1, 14].Value = "Водоснабжение. Обеспеченность централизованным водоснабжением, в % гр.13/гр.6 *100";
                worksheet.Cells[1, 15].Value = "Водоснабжение. Охват индивидуальными приборами учета воды по состоянию на конец отчетного года - всего с нарастающим (единиц)";
                worksheet.Cells[1, 16].Value = "Водоснабжение. Охват индивидуальными приборами учета воды по состоянию на конец отчетного года - в том числе с дистанционной передачей данных в АСУЭ обслуживающего предприятия (единиц)";
                worksheet.Cells[1, 17].Value = "Водоснабжение. Охват индивидуальными приборами учета воды по состоянию на конец отчетного года - охват в %, гр.15/гр. 9*100";
                worksheet.Cells[1, 18].Value = "Водоснабжение. Охват общедомовыми приборами учета воды по состоянию на конец отчетного года - Количество зданий и сооружений, подлежащих к установке общедомовых приборов учета (единиц)";
                worksheet.Cells[1, 19].Value = "Водоснабжение. Охват общедомовыми приборами учета воды по состоянию на конец отчетного года - Количество зданий и сооружений с установленными общедомовыми приборами учета (единиц)";
                worksheet.Cells[1, 20].Value = "Водоснабжение. Охват общедомовыми приборами учета воды по состоянию на конец отчетного года Количество зданий и сооружений с установленными общедомовыми приборами учета (единиц)";
                worksheet.Cells[1, 21].Value = "Водоснабжение. Охват общедомовыми приборами учета воды по состоянию на конец отчетного года в том числе с дистанционной передачей данных в АСУЭ обслуживающего предприятия (единиц)";
                worksheet.Cells[1, 22].Value = "Водоснабжение. Охват общедомовыми приборами учета воды по состоянию на конец отчетного года охват в %, гр.19/гр. 18*100";
                worksheet.Cells[1, 23].Value = "Водоснабжение. Автоматизация производственных процессов водоснабжения и наличие централизованной системы контроля и управления (SCADA) Водозабор (0 или 1)";
                worksheet.Cells[1, 24].Value = "Водоснабжение. Автоматизация производственных процессов водоснабжения и наличие централизованной системы контроля и управления (SCADA) Водоподготовка (0 или 1)";
                worksheet.Cells[1, 25].Value = "Водоснабжение. Автоматизация производственных процессов водоснабжения и наличие централизованной системы контроля и управления (SCADA) Насосные станции (0 или 1)";
                worksheet.Cells[1, 26].Value = "Водоснабжение. Автоматизация производственных процессов водоснабжения и наличие централизованной системы контроля и управления (SCADA) Сети водоснабжения (0 или 1)";
                worksheet.Cells[1, 27].Value = "Водоотведение. Кол-во абонентов, охваченных централизованным водоотведением (единиц)";
                worksheet.Cells[1, 28].Value = "в том числе физических лиц/население (единиц)";
                worksheet.Cells[1, 29].Value = "в том числе юридических лиц (единиц)";
                worksheet.Cells[1, 30].Value = "в том числе бюджетных организаций (единиц)";
                worksheet.Cells[1, 31].Value = "Численность населения, охваченного централизованным водоотведением, (человек)";
                worksheet.Cells[1, 32].Value = "Доступ к централизованному водоотведению, в % гр.31/гр.6*100";
                worksheet.Cells[1, 33].Value = "Наличие канализационно-очистных сооружений, (единиц)";
                worksheet.Cells[1, 34].Value = "в том числе только с механичес-кой очисткой (еди-ниц)";
                worksheet.Cells[1, 35].Value = "в том числе с механической и биологической очист-кой (еди-ниц)";
                worksheet.Cells[1, 36].Value = "Производительность канализационно-очистных сооружений (проектная)";
                worksheet.Cells[1, 37].Value = "Износ канализационно-очистных сооружений, в %";
                worksheet.Cells[1, 38].Value = "Численность населения, охваченного действующими канализационно-очистными сооружениями, (человек)";
                worksheet.Cells[1, 39].Value = "Охват населения очисткой сточных вод, в % гр.38/гр.6*100";
                worksheet.Cells[1, 40].Value = "Фактически поступило сточных вод в канализационно-очистные сооружения (тыс.м3)";
                worksheet.Cells[1, 41].Value = "В том числе За I квартал (тыс.м3)";
                worksheet.Cells[1, 42].Value = "В том числе За II квартал (тыс.м3)";
                worksheet.Cells[1, 43].Value = "В том числе За III квартал (тыс.м3)";
                worksheet.Cells[1, 44].Value = "В том числе За IV квартал (тыс.м3)";
                worksheet.Cells[1, 45].Value = "Объем сточных вод, соответствующей нормативной очистке по собственному лабораторному мониторингу за отчетный период (тыс.м3)";
                worksheet.Cells[1, 46].Value = "Уровень нормативно- очищенной воды, % гр.45/гр.40 * 100";
                worksheet.Cells[1, 47].Value = "Автоматизация производственных процессов водоотведения и наличие централизованной системы контроля и управления (SCADA) Сети канализации (0 или 1)";
                worksheet.Cells[1, 48].Value = "Автоматизация производственных процессов водоотведения и наличие централизованной системы контроля и управления (SCADA) Канализационные насосные станции (0 или 1)";
                worksheet.Cells[1, 49].Value = "Автоматизация производственных процессов водоотведения и наличие централизованной системы контроля и управления (SCADA) Канализационно-очистные сооружения (0 или 1)";
                worksheet.Cells[1, 50].Value = "Уровень тарифов водоснабжение усредненный, тенге/м3";
                worksheet.Cells[1, 51].Value = "водоснабжение. физическим лицам/населению, тенге/м3";
                worksheet.Cells[1, 52].Value = "водоснабжение юридическим лицам, тенге/м3";
                worksheet.Cells[1, 53].Value = "водоснабжение бюджетным организациям, тенге/м3";
                worksheet.Cells[1, 54].Value = "водоотведение. усредненный, тенге/м3";
                worksheet.Cells[1, 55].Value = "водоотведение физическим лицам/населению, тенге/м3";
                worksheet.Cells[1, 56].Value = "водоотведение юридическим лицам, тенге/м3";
                worksheet.Cells[1, 57].Value = "водоотведение бюджетным организациям, тенге/м3";
                worksheet.Cells[1, 58].Value = "Протяженность водопроводных сетей, км (по состоянию на конец отчетного года). общая, км";
                worksheet.Cells[1, 59].Value = "Протяженность водопроводных сетей, км (по состоянию на конец отчетного года). в том числе изношенных, км";
                worksheet.Cells[1, 60].Value = "Протяженность водопроводных сетей, км (по состоянию на конец отчетного года). Износ, % гр.59/гр.58";
                worksheet.Cells[1, 61].Value = "Протяженность канализационных сетей, км (по состоянию на конец отчетного года). общая, км";
                worksheet.Cells[1, 62].Value = "Протяженность канализационных сетей, км (по состоянию на конец отчетного года). в том числе изношенных, км";
                worksheet.Cells[1, 63].Value = "Протяженность канализационных сетей, км (по состоянию на конец отчетного года). Износ, % гр.62/гр.61";
                worksheet.Cells[1, 64].Value = "Общая протяженность построенных (новых) сетей в отчетном году, кмводоснабжения, км";
                worksheet.Cells[1, 65].Value = "Общая протяженность построенных (новых) сетей в отчетном году, км. водоотведения, км";
                worksheet.Cells[1, 66].Value = "Общая протяженность реконструированных (замененных) сетей в отчетном году, км. водоснабжения, км";
                worksheet.Cells[1, 67].Value = "Общая протяженность реконструированных (замененных) сетей в отчетном году, км. водоотведения, км";                
                worksheet.Cells[1, 68].Value = "Общая протяженность отремонтированных (текущий/капитальный ремонт) сетей в отчетном году, км. водоснабжения, км";
                worksheet.Cells[1, 69].Value = "Общая протяженность отремонтированных (текущий/капитальный ремонт) сетей в отчетном году, км. водоотведения, км";
                worksheet.Cells[1, 2, 1,69].Style.WrapText = true;
                
                for(int i = 1; i <= 69; i++)
                {
                    worksheet.Cells[2, i].Value = i;
                }


                foreach(var form in forms)
                {
                    int counter = 0; counter =+ 1;
                    //Значения колонок
                    worksheet.Cells[counter + 2, 1].Value = form.Id;
                    worksheet.Cells[counter + 2, 2].Value = form.NameOfPlace;
                    worksheet.Cells[counter + 2, 3].Value = form.KodKato;                    
                    worksheet.Cells[counter + 2, 4].Value = form.TotalCountCityOblast;
                    worksheet.Cells[counter + 2, 5].Value = form.TotalCountDomHoz;
                    worksheet.Cells[counter + 2, 6].Value = form.TotalCountChel;
                    worksheet.Cells[counter + 2, 7].Value = form.ObslPredpBin;
                    worksheet.Cells[counter + 2, 8].Value = form.ObslPredpName;
                    worksheet.Cells[counter + 2, 9].Value = form.VodoSnabKolichAbonent;
                    worksheet.Cells[counter + 2, 10].Value = form.VodoSnabKolFizLic;
                    worksheet.Cells[counter + 2, 11].Value = form.VodoSnabKolYriLic;
                    worksheet.Cells[counter + 2, 12].Value = form.VodoSnabKolBydzhOrg;
                    worksheet.Cells[counter + 2, 13].Value = form.VodoSnabKolChelDostyp;
                    worksheet.Cells[counter + 2, 14].Value = form.VodoSnabObespechCentrlVodo;
                    worksheet.Cells[counter + 2, 15].Value = form.VodoSnabIndivUchetVodyVsego;
                    worksheet.Cells[counter + 2, 16].Value = form.VodoSnabIndivUchetVodyDistance;
                    worksheet.Cells[counter + 2, 17].Value = form.VodoSnabIndivUchetVodyPercent;
                    worksheet.Cells[counter + 2, 18].Value = form.VodoSnabObshePodlezhashKolZdan;
                    worksheet.Cells[counter + 2, 19].Value = form.VodoSnabObsheUstanKolZdan;
                    worksheet.Cells[counter + 2, 20].Value = form.VodoSnabObsheUstanPriborKol;
                    worksheet.Cells[counter + 2, 21].Value = form.VodoSnabObsheUstanDistanceKol;
                    worksheet.Cells[counter + 2, 22].Value = form.VodoSnabObsheOhvatPercent;
                    worksheet.Cells[counter + 2, 23].Value = form.VodoSnabAutoProccesVodoZabor;
                    worksheet.Cells[counter + 2, 24].Value = form.VodoSnabAutoProccesVodoPodgot;
                    worksheet.Cells[counter + 2, 25].Value = form.VodoSnabAutoProccesNasosStanc;
                    worksheet.Cells[counter + 2, 26].Value = form.VodoSnabAutoProccesSetVodosnab;
                    worksheet.Cells[counter + 2, 27].Value = form.VodoOtvKolAbonent;
                    worksheet.Cells[counter + 2, 28].Value = form.VodoOtvKolAbonFizLic;
                    worksheet.Cells[counter + 2, 29].Value = form.VodoOtvKolAbonYriLic;
                    worksheet.Cells[counter + 2, 30].Value = form.VodoOtvKolBydzhetOrg;
                    worksheet.Cells[counter + 2, 31].Value = form.VodoOtvKolChelOhvatCentrVodo;
                    worksheet.Cells[counter + 2, 32].Value = form.VodoOtvDostypCentrVodo;
                    worksheet.Cells[counter + 2, 33].Value = form.VodoOtvKolichKanaliz;
                    worksheet.Cells[counter + 2, 34].Value = form.VodoOtvKolichKanalizMechan;
                    worksheet.Cells[counter + 2, 35].Value = form.VodoOtvKolichKanalizMechanBiolog;
                    worksheet.Cells[counter + 2, 36].Value = form.VodoOtvProizvodKanaliz;
                    worksheet.Cells[counter + 2, 37].Value = form.VodoOtvIznosKanaliz;
                    worksheet.Cells[counter + 2, 38].Value = form.VodoOtvKolChelKanaliz;
                    worksheet.Cells[counter + 2, 39].Value = form.VodoOtvOhvatChelKanaliz;
                    worksheet.Cells[counter + 2, 40].Value = form.VodoOtvFactPostypKanaliz;
                    worksheet.Cells[counter + 2, 41].Value = form.VodoOtvFactPostypKanaliz1kv;
                    worksheet.Cells[counter + 2, 42].Value = form.VodoOtvFactPostypKanaliz2kv;
                    worksheet.Cells[counter + 2, 43].Value = form.VodoOtvFactPostypKanaliz3kv;
                    worksheet.Cells[counter + 2, 44].Value = form.VodoOtvFactPostypKanaliz4kv;
                    worksheet.Cells[counter + 2, 45].Value = form.VodoOtvObiemKanalizNormOchist;
                    worksheet.Cells[counter + 2, 46].Value = form.VodoOtvUrovenNormOchishVody;
                    worksheet.Cells[counter + 2, 47].Value = form.VodoOtvAutoProccesSetKanaliz;
                    worksheet.Cells[counter + 2, 48].Value = form.VodoOtvAutoProccesKanalizNasos;
                    worksheet.Cells[counter + 2, 49].Value = form.VodoOtvAutoProccesKanalizSooruzh;
                    worksheet.Cells[counter + 2, 50].Value = form.UrTarVodoSnabUsrednen;
                    worksheet.Cells[counter + 2, 51].Value = form.UrTarVodoSnabFizLic;
                    worksheet.Cells[counter + 2, 52].Value = form.UrTarVodoSnabYriLic;
                    worksheet.Cells[counter + 2, 53].Value = form.UrTarVodoSnabBydzhOrg;
                    worksheet.Cells[counter + 2, 54].Value = form.UrTarVodoOtvedUsred;
                    worksheet.Cells[counter + 2, 55].Value = form.UrTarVodoOtvedFizLic;
                    worksheet.Cells[counter + 2, 56].Value = form.UrTarVodoOtvedYriLic;
                    worksheet.Cells[counter + 2, 57].Value = form.UrTarVodoOtvedBydzhOrg;
                    worksheet.Cells[counter + 2, 58].Value = form.LengthVodoProvodTotal;
                    worksheet.Cells[counter + 2, 59].Value = form.LengthVodoProvodIznos;
                    worksheet.Cells[counter + 2, 60].Value = form.LengthVodoProvodIznosPercent;
                    worksheet.Cells[counter + 2, 61].Value = form.LengthKanalizTotal;
                    worksheet.Cells[counter + 2, 62].Value = form.LengthKanalizIznos;
                    worksheet.Cells[counter + 2, 63].Value = form.LengthKanalizIznosPercent;
                    worksheet.Cells[counter + 2, 64].Value = form.LengthObshNewSetiVodo;
                    worksheet.Cells[counter + 2, 65].Value = form.LengthObshNewSetiKanaliz;
                    worksheet.Cells[counter + 2, 66].Value = form.LengthObshZamenSetiVodo;
                    worksheet.Cells[counter + 2, 67].Value = form.LengthObshZamenSetiKanaliz;
                    worksheet.Cells[counter + 2, 68].Value = form.LengthObshRemontSetiVodo;
                    worksheet.Cells[counter + 2, 69].Value = form.LengthObshRemontSetiKanaliz;                    
                }

                // Определите диапазон данных
                var range = worksheet.Cells[1, 1, forms.Count + 2, 69];

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
                var worksheet = package.Workbook.Worksheets.Add("CityForm");
                worksheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Row(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                //worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Row(1).Height = 200;

                //Названия колонок
                worksheet.Cells[1, 1].Value = "Код строк";
                worksheet.Cells[1, 2].Value = "Наименование области, города";
                worksheet.Cells[1, 3].Value = "Код области, города по классификатору административно-территориальных объектов";
                worksheet.Cells[1, 4].Value = "Общее количество городов в области (единиц)";
                worksheet.Cells[1, 5].Value = "Общее количество домохозяйств (кв, ИЖД)";
                worksheet.Cells[1, 6].Value = "Общее количество проживающих в городских населенных пунктах (человек)";
                worksheet.Cells[1, 7].Value = "Обслуживающее предприятие БИН";
                worksheet.Cells[1, 8].Value = "Обслуживающее предприятие Наименование";
                worksheet.Cells[1, 9].Value = "Водоснабжение. Количество абонентов, охваченных централизованным водоснабжением (единиц)";
                worksheet.Cells[1, 10].Value = "Водоснабжение. физических лиц/население (единиц)";
                worksheet.Cells[1, 11].Value = "Водоснабжение.юридических лиц (единиц)";
                worksheet.Cells[1, 12].Value = "Водоснабжение. бюджетных организаций (единиц)";
                worksheet.Cells[1, 13].Value = "Водоснабжение. Количество населения имеющих доступ к  централизованному водоснабжению (человек)";
                worksheet.Cells[1, 14].Value = "Водоснабжение. Обеспеченность централизованным водоснабжением, в % гр.13/гр.6 *100";
                worksheet.Cells[1, 15].Value = "Водоснабжение. Охват индивидуальными приборами учета воды по состоянию на конец отчетного года - всего с нарастающим (единиц)";
                worksheet.Cells[1, 16].Value = "Водоснабжение. Охват индивидуальными приборами учета воды по состоянию на конец отчетного года - в том числе с дистанционной передачей данных в АСУЭ обслуживающего предприятия (единиц)";
                worksheet.Cells[1, 17].Value = "Водоснабжение. Охват индивидуальными приборами учета воды по состоянию на конец отчетного года - охват в %, гр.15/гр. 9*100";
                worksheet.Cells[1, 18].Value = "Водоснабжение. Охват общедомовыми приборами учета воды по состоянию на конец отчетного года - Количество зданий и сооружений, подлежащих к установке общедомовых приборов учета (единиц)";
                worksheet.Cells[1, 19].Value = "Водоснабжение. Охват общедомовыми приборами учета воды по состоянию на конец отчетного года - Количество зданий и сооружений с установленными общедомовыми приборами учета (единиц)";
                worksheet.Cells[1, 20].Value = "Водоснабжение. Охват общедомовыми приборами учета воды по состоянию на конец отчетного года Количество зданий и сооружений с установленными общедомовыми приборами учета (единиц)";
                worksheet.Cells[1, 21].Value = "Водоснабжение. Охват общедомовыми приборами учета воды по состоянию на конец отчетного года в том числе с дистанционной передачей данных в АСУЭ обслуживающего предприятия (единиц)";
                worksheet.Cells[1, 22].Value = "Водоснабжение. Охват общедомовыми приборами учета воды по состоянию на конец отчетного года охват в %, гр.19/гр. 18*100";
                worksheet.Cells[1, 23].Value = "Водоснабжение. Автоматизация производственных процессов водоснабжения и наличие централизованной системы контроля и управления (SCADA) Водозабор (0 или 1)";
                worksheet.Cells[1, 24].Value = "Водоснабжение. Автоматизация производственных процессов водоснабжения и наличие централизованной системы контроля и управления (SCADA) Водоподготовка (0 или 1)";
                worksheet.Cells[1, 25].Value = "Водоснабжение. Автоматизация производственных процессов водоснабжения и наличие централизованной системы контроля и управления (SCADA) Насосные станции (0 или 1)";
                worksheet.Cells[1, 26].Value = "Водоснабжение. Автоматизация производственных процессов водоснабжения и наличие централизованной системы контроля и управления (SCADA) Сети водоснабжения (0 или 1)";
                worksheet.Cells[1, 27].Value = "Водоотведение. Кол-во абонентов, охваченных централизованным водоотведением (единиц)";
                worksheet.Cells[1, 28].Value = "в том числе физических лиц/население (единиц)";
                worksheet.Cells[1, 29].Value = "в том числе юридических лиц (единиц)";
                worksheet.Cells[1, 30].Value = "в том числе бюджетных организаций (единиц)";
                worksheet.Cells[1, 31].Value = "Численность населения, охваченного централизованным водоотведением, (человек)";
                worksheet.Cells[1, 32].Value = "Доступ к централизованному водоотведению, в % гр.31/гр.6*100";
                worksheet.Cells[1, 33].Value = "Наличие канализационно-очистных сооружений, (единиц)";
                worksheet.Cells[1, 34].Value = "в том числе только с механичес-кой очисткой (еди-ниц)";
                worksheet.Cells[1, 35].Value = "в том числе с механической и биологической очист-кой (еди-ниц)";
                worksheet.Cells[1, 36].Value = "Производительность канализационно-очистных сооружений (проектная)";
                worksheet.Cells[1, 37].Value = "Износ канализационно-очистных сооружений, в %";
                worksheet.Cells[1, 38].Value = "Численность населения, охваченного действующими канализационно-очистными сооружениями, (человек)";
                worksheet.Cells[1, 39].Value = "Охват населения очисткой сточных вод, в % гр.38/гр.6*100";
                worksheet.Cells[1, 40].Value = "Фактически поступило сточных вод в канализационно-очистные сооружения (тыс.м3)";
                worksheet.Cells[1, 41].Value = "В том числе За I квартал (тыс.м3)";
                worksheet.Cells[1, 42].Value = "В том числе За II квартал (тыс.м3)";
                worksheet.Cells[1, 43].Value = "В том числе За III квартал (тыс.м3)";
                worksheet.Cells[1, 44].Value = "В том числе За IV квартал (тыс.м3)";
                worksheet.Cells[1, 45].Value = "Объем сточных вод, соответствующей нормативной очистке по собственному лабораторному мониторингу за отчетный период (тыс.м3)";
                worksheet.Cells[1, 46].Value = "Уровень нормативно- очищенной воды, % гр.45/гр.40 * 100";
                worksheet.Cells[1, 47].Value = "Автоматизация производственных процессов водоотведения и наличие централизованной системы контроля и управления (SCADA) Сети канализации (0 или 1)";
                worksheet.Cells[1, 48].Value = "Автоматизация производственных процессов водоотведения и наличие централизованной системы контроля и управления (SCADA) Канализационные насосные станции (0 или 1)";
                worksheet.Cells[1, 49].Value = "Автоматизация производственных процессов водоотведения и наличие централизованной системы контроля и управления (SCADA) Канализационно-очистные сооружения (0 или 1)";
                worksheet.Cells[1, 50].Value = "Уровень тарифов водоснабжение усредненный, тенге/м3";
                worksheet.Cells[1, 51].Value = "водоснабжение. физическим лицам/населению, тенге/м3";
                worksheet.Cells[1, 52].Value = "водоснабжение юридическим лицам, тенге/м3";
                worksheet.Cells[1, 53].Value = "водоснабжение бюджетным организациям, тенге/м3";
                worksheet.Cells[1, 54].Value = "водоотведение. усредненный, тенге/м3";
                worksheet.Cells[1, 55].Value = "водоотведение физическим лицам/населению, тенге/м3";
                worksheet.Cells[1, 56].Value = "водоотведение юридическим лицам, тенге/м3";
                worksheet.Cells[1, 57].Value = "водоотведение бюджетным организациям, тенге/м3";
                worksheet.Cells[1, 58].Value = "Протяженность водопроводных сетей, км (по состоянию на конец отчетного года). общая, км";
                worksheet.Cells[1, 59].Value = "Протяженность водопроводных сетей, км (по состоянию на конец отчетного года). в том числе изношенных, км";
                worksheet.Cells[1, 60].Value = "Протяженность водопроводных сетей, км (по состоянию на конец отчетного года). Износ, % гр.59/гр.58";
                worksheet.Cells[1, 61].Value = "Протяженность канализационных сетей, км (по состоянию на конец отчетного года). общая, км";
                worksheet.Cells[1, 62].Value = "Протяженность канализационных сетей, км (по состоянию на конец отчетного года). в том числе изношенных, км";
                worksheet.Cells[1, 63].Value = "Протяженность канализационных сетей, км (по состоянию на конец отчетного года). Износ, % гр.62/гр.61";
                worksheet.Cells[1, 64].Value = "Общая протяженность построенных (новых) сетей в отчетном году, кмводоснабжения, км";
                worksheet.Cells[1, 65].Value = "Общая протяженность построенных (новых) сетей в отчетном году, км. водоотведения, км";
                worksheet.Cells[1, 66].Value = "Общая протяженность реконструированных (замененных) сетей в отчетном году, км. водоснабжения, км";
                worksheet.Cells[1, 67].Value = "Общая протяженность реконструированных (замененных) сетей в отчетном году, км. водоотведения, км";
                worksheet.Cells[1, 68].Value = "Общая протяженность отремонтированных (текущий/капитальный ремонт) сетей в отчетном году, км. водоснабжения, км";
                worksheet.Cells[1, 69].Value = "Общая протяженность отремонтированных (текущий/капитальный ремонт) сетей в отчетном году, км. водоотведения, км";
                worksheet.Cells[1, 2, 1, 69].Style.WrapText = true;

                for (int i = 1; i <= 69; i++)
                {
                    worksheet.Cells[2, i].Value = i;
                }                                

                // Определите диапазон данных
                var range = worksheet.Cells[1, 1, 2, 69];

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
            var listCityDocs = new List<CityDocument>();

            using(var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using(var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets[0];
                    var rowCount = workSheet.Dimension.Rows;

                    for(int row = 3;  row <= rowCount; row++)
                    {                        
                        var doc = new CityDocument();
                        var cityForm = new CityForm();

                        if(workSheet.Cells[row, 1].Text != "")
                            cityForm.Id = Guid.Parse(workSheet.Cells[row, 1].Text);
                        doc.KodNaselPunk = workSheet.Cells[row, 3].Text;
                        doc.Year = year;
                        doc.Login = login;
                        if (_dbSetDoc.Any(x => x.KodNaselPunk == doc.KodNaselPunk && x.Year == year))
                            break;
                        if (workSheet.Cells[row, 4].Text != "") cityForm.TotalCountCityOblast = int.Parse(workSheet.Cells[row, 4].Text);
                        if (workSheet.Cells[row, 5].Text != "") cityForm.TotalCountDomHoz = int.Parse(workSheet.Cells[row, 5].Text);
                        if (workSheet.Cells[row, 6].Text != "") cityForm.TotalCountChel = int.Parse(workSheet.Cells[row, 6].Text);
                        cityForm.ObslPredpId = null; //TODO                        
                        if (workSheet.Cells[row, 9].Text != "") cityForm.VodoSnabKolichAbonent = int.Parse(workSheet.Cells[row, 9].Text);
                        if (workSheet.Cells[row, 10].Text != "") cityForm.VodoSnabKolFizLic = int.Parse(workSheet.Cells[row, 10].Text);
                        if (workSheet.Cells[row, 11].Text != "") cityForm.VodoSnabKolYriLic = int.Parse(workSheet.Cells[row, 11].Text);
                        if (workSheet.Cells[row, 12].Text != "") cityForm.VodoSnabKolBydzhOrg = int.Parse(workSheet.Cells[row, 12].Text);
                        if (workSheet.Cells[row, 13].Text != "") cityForm.VodoSnabKolChelDostyp = int.Parse(workSheet.Cells[row, 13].Text);
                        if (workSheet.Cells[row, 14].Text != "") cityForm.VodoSnabObespechCentrlVodo = decimal.Parse(workSheet.Cells[row, 14].Text);
                        if (workSheet.Cells[row, 15].Text != "") cityForm.VodoSnabIndivUchetVodyVsego = int.Parse(workSheet.Cells[row, 15].Text);
                        if (workSheet.Cells[row, 16].Text != "") cityForm.VodoSnabIndivUchetVodyDistance = int.Parse(workSheet.Cells[row, 16].Text);
                        if (workSheet.Cells[row, 17].Text != "") cityForm.VodoSnabIndivUchetVodyPercent = decimal.Parse(workSheet.Cells[row, 17].Text);
                        if (workSheet.Cells[row, 18].Text != "") cityForm.VodoSnabObshePodlezhashKolZdan = int.Parse(workSheet.Cells[row, 18].Text);
                        if (workSheet.Cells[row, 19].Text != "") cityForm.VodoSnabObsheUstanKolZdan = int.Parse(workSheet.Cells[row, 19].Text);
                        if (workSheet.Cells[row, 20].Text != "") cityForm.VodoSnabObsheUstanPriborKol = int.Parse(workSheet.Cells[row, 20].Text);
                        if (workSheet.Cells[row, 21].Text != "") cityForm.VodoSnabObsheUstanDistanceKol = int.Parse(workSheet.Cells[row, 21].Text);
                        if (workSheet.Cells[row, 22].Text != "") cityForm.VodoSnabObsheOhvatPercent = decimal.Parse(workSheet.Cells[row, 22].Text);
                        if (workSheet.Cells[row, 23].Text != "") cityForm.VodoSnabAutoProccesVodoZabor = int.Parse(workSheet.Cells[row, 23].Text);
                        if (workSheet.Cells[row, 24].Text != "") cityForm.VodoSnabAutoProccesVodoPodgot = int.Parse(workSheet.Cells[row, 24].Text);
                        if (workSheet.Cells[row, 25].Text != "") cityForm.VodoSnabAutoProccesNasosStanc = int.Parse(workSheet.Cells[row, 25].Text);
                        if (workSheet.Cells[row, 26].Text != "") cityForm.VodoSnabAutoProccesSetVodosnab = int.Parse(workSheet.Cells[row, 26].Text);
                        if (workSheet.Cells[row, 27].Text != "") cityForm.VodoOtvKolAbonent = int.Parse(workSheet.Cells[row, 27].Text);
                        if (workSheet.Cells[row, 28].Text != "") cityForm.VodoOtvKolAbonFizLic = int.Parse(workSheet.Cells[row, 28].Text);
                        if (workSheet.Cells[row, 29].Text != "") cityForm.VodoOtvKolAbonYriLic = int.Parse(workSheet.Cells[row, 29].Text);
                        if (workSheet.Cells[row, 30].Text != "") cityForm.VodoOtvKolBydzhetOrg = int.Parse(workSheet.Cells[row, 30].Text);
                        if (workSheet.Cells[row, 31].Text != "") cityForm.VodoOtvKolChelOhvatCentrVodo = int.Parse(workSheet.Cells[row, 31].Text);
                        if (workSheet.Cells[row, 32].Text != "") cityForm.VodoOtvDostypCentrVodo = decimal.Parse(workSheet.Cells[row, 32].Text);
                        if (workSheet.Cells[row, 33].Text != "") cityForm.VodoOtvKolichKanaliz = int.Parse(workSheet.Cells[row, 33].Text);
                        if (workSheet.Cells[row, 34].Text != "") cityForm.VodoOtvKolichKanalizMechan = int.Parse(workSheet.Cells[row, 34].Text);
                        if (workSheet.Cells[row, 35].Text != "") cityForm.VodoOtvKolichKanalizMechanBiolog = int.Parse(workSheet.Cells[row, 35].Text);
                        if (workSheet.Cells[row, 36].Text != "") cityForm.VodoOtvProizvodKanaliz = int.Parse(workSheet.Cells[row, 36].Text);
                        if (workSheet.Cells[row, 37].Text != "") cityForm.VodoOtvIznosKanaliz = decimal.Parse(workSheet.Cells[row, 37].Text);
                        if (workSheet.Cells[row, 38].Text != "") cityForm.VodoOtvKolChelKanaliz = int.Parse(workSheet.Cells[row, 38].Text);
                        if (workSheet.Cells[row, 39].Text != "") cityForm.VodoOtvOhvatChelKanaliz = decimal.Parse(workSheet.Cells[row, 39].Text);
                        if (workSheet.Cells[row, 40].Text != "") cityForm.VodoOtvFactPostypKanaliz = int.Parse(workSheet.Cells[row, 40].Text);
                        if (workSheet.Cells[row, 41].Text != "") cityForm.VodoOtvFactPostypKanaliz1kv = int.Parse(workSheet.Cells[row, 41].Text);
                        if (workSheet.Cells[row, 42].Text != "") cityForm.VodoOtvFactPostypKanaliz2kv = int.Parse(workSheet.Cells[row, 42].Text);
                        if (workSheet.Cells[row, 43].Text != "") cityForm.VodoOtvFactPostypKanaliz3kv = int.Parse(workSheet.Cells[row, 43].Text);
                        if (workSheet.Cells[row, 44].Text != "") cityForm.VodoOtvFactPostypKanaliz4kv = int.Parse(workSheet.Cells[row, 44].Text);
                        if (workSheet.Cells[row, 45].Text != "") cityForm.VodoOtvObiemKanalizNormOchist = int.Parse(workSheet.Cells[row, 45].Text);
                        if (workSheet.Cells[row, 46].Text != "") cityForm.VodoOtvUrovenNormOchishVody = int.Parse(workSheet.Cells[row, 46].Text);

                        if (workSheet.Cells[row, 47].Text != "") cityForm.VodoOtvAutoProccesSetKanaliz = int.Parse(workSheet.Cells[row, 47].Text);
                        if (workSheet.Cells[row, 48].Text != "") cityForm.VodoOtvAutoProccesKanalizNasos = int.Parse(workSheet.Cells[row, 48].Text);
                        if (workSheet.Cells[row, 49].Text != "") cityForm.VodoOtvAutoProccesKanalizSooruzh = int.Parse(workSheet.Cells[row, 49].Text);
                        if (workSheet.Cells[row, 50].Text != "") cityForm.UrTarVodoSnabUsrednen = int.Parse(workSheet.Cells[row, 50].Text);
                        if (workSheet.Cells[row, 51].Text != "") cityForm.UrTarVodoSnabFizLic = int.Parse(workSheet.Cells[row, 51].Text);
                        if (workSheet.Cells[row, 52].Text != "") cityForm.UrTarVodoSnabYriLic = int.Parse(workSheet.Cells[row, 52].Text);
                        if (workSheet.Cells[row, 53].Text != "") cityForm.UrTarVodoSnabBydzhOrg = int.Parse(workSheet.Cells[row, 53].Text);
                        if (workSheet.Cells[row, 54].Text != "") cityForm.UrTarVodoOtvedUsred = int.Parse(workSheet.Cells[row, 54].Text);
                        if (workSheet.Cells[row, 55].Text != "") cityForm.UrTarVodoOtvedFizLic = int.Parse(workSheet.Cells[row, 55].Text);
                        if (workSheet.Cells[row, 56].Text != "") cityForm.UrTarVodoOtvedYriLic = int.Parse(workSheet.Cells[row, 56].Text);
                        if (workSheet.Cells[row, 57].Text != "") cityForm.UrTarVodoOtvedBydzhOrg = int.Parse(workSheet.Cells[row, 57].Text);
                        if (workSheet.Cells[row, 58].Text != "") cityForm.LengthVodoProvodTotal = int.Parse(workSheet.Cells[row, 58].Text);
                        if (workSheet.Cells[row, 59].Text != "") cityForm.LengthVodoProvodIznos = int.Parse(workSheet.Cells[row, 59].Text);
                        if (workSheet.Cells[row, 60].Text != "") cityForm.LengthVodoProvodIznosPercent = decimal.Parse(workSheet.Cells[row, 60].Text);
                        if (workSheet.Cells[row, 61].Text != "") cityForm.LengthKanalizTotal = int.Parse(workSheet.Cells[row, 61].Text);
                        if (workSheet.Cells[row, 62].Text != "") cityForm.LengthKanalizIznos = int.Parse(workSheet.Cells[row, 62].Text);
                        if (workSheet.Cells[row, 63].Text != "") cityForm.LengthKanalizIznosPercent = decimal.Parse(workSheet.Cells[row, 63].Text);
                        if (workSheet.Cells[row, 64].Text != "") cityForm.LengthObshNewSetiVodo = int.Parse(workSheet.Cells[row, 64].Text);
                        if (workSheet.Cells[row, 65].Text != "") cityForm.LengthObshNewSetiKanaliz = int.Parse(workSheet.Cells[row, 65].Text);
                        if (workSheet.Cells[row, 66].Text != "") cityForm.LengthObshZamenSetiVodo = int.Parse(workSheet.Cells[row, 66].Text);
                        if (workSheet.Cells[row, 67].Text != "") cityForm.LengthObshZamenSetiKanaliz = int.Parse(workSheet.Cells[row, 67].Text);
                        if (workSheet.Cells[row, 68].Text != "") cityForm.LengthObshRemontSetiVodo = int.Parse(workSheet.Cells[row, 68].Text);
                        if (workSheet.Cells[row, 69].Text != "") cityForm.LengthObshRemontSetiKanaliz = int.Parse(workSheet.Cells[row, 69].Text);

                        await _dbSetForm.AddAsync(cityForm);
                        await _context.SaveChangesAsync();
                        doc.CityFormId = cityForm.Id;
                        
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

                        listCityDocs.Add(doc);                        
                    }
                }
            }

            await _dbSetDoc.AddRangeAsync(listCityDocs);            
            await _context.SaveChangesAsync();

            return listCityDocs.Count();
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
    }
}
