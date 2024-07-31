﻿using Microsoft.EntityFrameworkCore;

namespace WebServer.Dtos
{
    public class CityFormDto
    {
        public Guid Id { get; set; }        
        public int? TotalCountCityOblast { get; set; }        
        public int? TotalCountDomHoz { get; set; }        
        public int? TotalCountChel { get; set; }        
        public Guid? ObslPredpId { get; set; }
        public int? KolichAbonent { get; set; }
        public int? KolFizLic { get; set; }
        public int? KolYriLic { get; set; }
        public int? KolBydzhOrg { get; set; }
        public int? KolChelDostyp { get; set; }
        public decimal? ObespechCentrlVodo { get; set; }
        public int? IndivUchetVodyVsego { get; set; }
        public int? IndivUchetVodyDistance { get; set; }
        public decimal? IndivUchetVodyPercent { get; set; }
        public int? ObshePodlezhashKolZdan { get; set; }
        public int? ObsheUstanKolZdan { get; set; }
        public int? ObsheUstanPriborKol { get; set; }
        public int? ObsheUstanDistanceKol { get; set; }
        public decimal? ObsheOhvatPercent { get; set; }
        public int? AutoProccesVodoZabor { get; set; }
        public int? AutoProccesVodoPodgot { get; set; }
        public int? AutoProccesNasosStanc { get; set; }
        public int? AutoProccesSetVodosnab { get; set; }
        public int? KolAbonent { get; set; }
        public int? KolAbonFizLic { get; set; }
        public int? KolAbonYriLic { get; set; }
        public int? KolBydzhetOrg { get; set; }
        public int? KolChelOhvatCentrVodo { get; set; }
        public decimal? DostypCentrVodo { get; set; }
        public int? KolichKanaliz { get; set; }
        public int? KolichKanalizMechan { get; set; }
        public int? KolichKanalizMechanBiolog { get; set; }
        public int? ProizvodKanaliz { get; set; }
        public decimal? IznosKanaliz { get; set; }
        public int? KolChelKanaliz { get; set; }
        public decimal? OhvatChelKanaliz { get; set; }
        public int? FactPostypKanaliz { get; set; }
        public int? FactPostypKanaliz1kv { get; set; }
        public int? FactPostypKanaliz2kv { get; set; }
        public int? FactPostypKanaliz3kv { get; set; }
        public int? FactPostypKanaliz4kv { get; set; }
        public int? ObiemKanalizNormOchist { get; set; }
        public int? UrovenNormOchishVody { get; set; }
        public int? AutoProccesSetKanaliz { get; set; }
        public int? AutoProccesKanalizNasos { get; set; }
        public int? AutoProccesKanalizSooruzh { get; set; }
        public int? VodoSnabUsrednen { get; set; }
        public int? VodoSnabFizLic { get; set; }
        public int? VodoSnabYriLic { get; set; }
        public int? VodoSnabBydzhOrg { get; set; }
        public int? VodoOtvedUsred { get; set; }
        public int? VodoOtvedFizLic { get; set; }
        public int? VodoOtvedYriLic { get; set; }
        public int? VodoOtvedBydzhOrg { get; set; }
        public int? VodoProvodLengthTotal { get; set; }
        public int? VodoProvodLengthIznos { get; set; }
        public decimal? VodoProvodIznosPercent { get; set; }
        public int? KanalizLengthTotal { get; set; }
        public int? KanalizLengthIznos { get; set; }
        public decimal? KanalizIznosPercent { get; set; }
        public int? ObshNewSetiVodo { get; set; }
        public int? ObshNewSetiKanaliz { get; set; }
        public int? ObshZamenSetiVodo { get; set; }
        public int? ObshZamenSetiKanaliz { get; set; }
        public int? ObshRemontSetiVodo { get; set; }
        public int? ObshRemontSetiKanaliz { get; set; }
    }
}
