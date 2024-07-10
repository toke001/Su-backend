--области
drop table if exists oblasti;
create temp table oblasti as
select *, 
case when "Ref_Katos"."KatoLevel" = 1 then 1 else 0 end as "isCity"
	from "Ref_Katos" where "Ref_Katos"."KatoLevel" in(0,1) and "Ref_Katos"."IsDel"=false;
-- select * from oblasti;
--отчеты за 2024
drop table if exists reports;
create temp table reports as
select rf.* from "Report_Forms" rf 
left join oblasti o on rf."RefKatoId" = o."Id" 
and rf."ReportYearId"=2024;
-- select * from reports;
--поставщики
drop table if exists suppliers;
create temp table suppliers as
select s.* from "Suppliers" s 
left join "ReportSuppliers" rs on s."Id"=rs."SupplierId" 
left join reports r on rs."Report_FormId"=r."Id";


-- select * from reports;
-- select * from oblasti;
select
	'num' as "1",
	oblasti."NameRu" as "2",
	oblasti."Code" as "3",
	0 as "4",
	0 as "5",
	0 as "6",
	suppliers."Bin" as "7",
	suppliers."FullName" as "8",
	'село ф2' as "9",
	'физических лиц/население (единиц)' as "10",
	'юридических лиц (единиц)' as "11",
	'бюджетных организаций (единиц)' as "12",
from 
oblasti 
	left join suppliers on oblasti."Id" = suppliers."KatoId"
order by oblasti."Code";
-- select * from suppliers;
--формы водоснабжения
-- DROP TABLE IF EXISTS WATERS;

-- CREATE TEMP TABLE WATERS AS
-- SELECT
-- 	*
-- FROM
-- 	"ApprovedFormItems"
-- 	LEFT JOIN "ApprovedForms" ON "ApprovedFormItems"."ApprovedFormId" = "ApprovedForms"."Id"
-- WHERE
-- 	"ApprovedFormItems"."IsDel" = FALSE
-- 	AND "ApprovedFormItems"."ServiceId" = 0
-- 	AND "ApprovedForms"."IsDel" = FALSE;