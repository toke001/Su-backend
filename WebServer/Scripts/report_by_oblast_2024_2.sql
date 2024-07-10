select distinct
	"Ref_Katos"."NameRu" as "2",
	"Ref_Katos"."Code" as "3",
	'городов в области единиц' as "4",
	'домохозяйств (кв, ИЖД)' as "5",
	'проживающих в городских населенных пунктах (человек)' as "6",
	get_suppliers_by_report_forms_id("Report_Forms"."Id") as "7",
	get_suppliers_by_report_forms_id("Report_Forms"."Id") as "8",
	get_val_by_repid_colid("Report_Forms"."Id",'9c78191d-cc63-4c4c-b262-d00526fdae90') as "9",
	'Водоотвед село форма 2 50' as "10",
	'Водоотвед село форма 2 51' as "11",
	'Водоотвед село форма 2 52' as "12",
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

--select * from "ApprovedFormItemColumns" where "NameRu" like '%централизован%'

--select * from "ApprovedFormItemColumns" where "NameRu" like '%абонентов%'
--"9c78191d-cc63-4c4c-b262-d00526fdae90"
--select * from "Data"