DROP FUNCTION IF EXISTS get_val_by_repid_colid(rid UUID,cid UUID);
CREATE
OR REPLACE FUNCTION get_val_by_repid_colid (rid UUID,cid UUID) 
	returns text
	LANGUAGE PLPGSQL AS $body$
	declare rez text;
BEGIN
	select 
	sum("Data"."ValueJson"::integer)::text into rez
	from "Data"
	where
	"ApproverFormColumnId"=cid and "Data"."ReportFormId"=rid 
    group by "Data"."ValueJson";
return rez;
END;
$body$;
DROP FUNCTION IF EXISTS GET_SUPPLIERS_BY_REPORT_FORMS_ID (RFID UUID);
CREATE
OR REPLACE FUNCTION GET_SUPPLIERS_BY_REPORT_FORMS_ID (RFID UUID) RETURNS TEXT LANGUAGE PLPGSQL AS $body$
	Declare rez TEXT;
BEGIN
	select 
	string_agg("Suppliers"."FullName",'; ')
	into rez
	from "Report_Forms" 
	left join "Ref_Katos" on "Ref_Katos"."Id" = "Report_Forms"."RefKatoId"
	left join "ReportSuppliers" on "Report_Forms"."Id" = "ReportSuppliers"."Report_FormId"
	left join "Suppliers" ON "Suppliers"."Id" = "ReportSuppliers"."SupplierId"
	where "Report_Forms"."Id" = rfid
	group by "Suppliers"."FullName"
	;
return rez;
END;
$body$;
