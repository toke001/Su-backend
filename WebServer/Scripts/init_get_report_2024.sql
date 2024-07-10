CREATE
OR REPLACE PROCEDURE GetReport2024 () LANGUAGE PLPGSQL AS $$
BEGIN
    drop table if exists tmp_GetReport2024;
drop table if exists tmp_GetReport2024Hearders;

create temp table tmp_GetReport2024Hearders as
	select 
	'Наименование области, города' as "1",
	'Код области, города по классификатору административно-территориальных объектов' as "2",
	'городов в области (единиц)' as "3",
	'домохозяйств (кв, ИЖД)' as "4",
	'проживающих в городских населенных пунктах (человек)' as "5",
	'БИН' as "6",
	'Наименование' as "7",
	'Количество абонентов, охваченных централизованным водоснабжением (единиц)' as "8",
	'физических лиц/население (единиц)' as "9",
	'юридических лиц (единиц)' as "10",
	'бюджетных организаций (единиц)' as "11",
	'Количество населения имеющих доступ к  централизованному водоснабжению (человек)' as "12"
	;

create temp table tmp_GetReport2024 as 
    select distinct
	"Ref_Katos"."NameRu" as "2",
	"Ref_Katos"."Code" as "3",
	0 as "4",
	0 as "5",
	0 as "6",
	get_suppliers_by_report_forms_id("Report_Forms"."Id") as "7",
	get_suppliers_by_report_forms_id("Report_Forms"."Id") as "8",
	get_val_by_repid_colid("Report_Forms"."Id",'9c78191d-cc63-4c4c-b262-d00526fdae90') as "9",
	0 as "10",
	0 as "11",
	0 as "12",
	get_val_by_repid_colid("Report_Forms"."Id",'12ab9b57-6422-463c-a755-01bcee5f880e') as "13"
	from "Ref_Katos" 
	left join "Report_Forms" on "Ref_Katos"."Id" = "Report_Forms"."RefKatoId"
	left join "ReportSuppliers" on "Report_Forms"."Id" = "ReportSuppliers"."Report_FormId"
	left join "Suppliers" ON "Suppliers"."Id" = "ReportSuppliers"."SupplierId"
	left join "ApprovedForms" on "Report_Forms"."ApprovedFormId" = "ApprovedForms"."Id"
	left join "ApprovedFormItems" on "ApprovedForms"."Id" = "ApprovedFormItems"."ApprovedFormId"
	left join "ApprovedFormItemColumns" on "ApprovedFormItems"."Id" = "ApprovedFormItemColumns"."ApprovedFormItemId"
	left join "Data" on "ApprovedFormItemColumns"."Id" = "Data"."ApproverFormColumnId"
	
	where "Ref_Katos"."KatoLevel" in(1) and "Ref_Katos"."IsDel"=false
order by "Ref_Katos"."Code";
END;
$$;