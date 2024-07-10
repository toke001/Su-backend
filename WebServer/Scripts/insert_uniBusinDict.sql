CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
INSERT INTO public."Universal_Refferences"
("Id", "Code", "Type", "BusinessDecription", "NameRu", "NameKk", "IsDel", "Description")
VALUES(gen_random_uuid(), 'gov', 'owner', 'государственная 100%', 'государственная 100%', 'государственная 100%', false, '');


INSERT INTO public."Business_Dictionary"
("Id", "Code", "Type", "BusinessDecription", "NameRu", "NameKk", "IsDel", "Description")
VALUES(gen_random_uuid(), 'astsu', 'sem', '{BIN = 12124214}', 'астана водоканал', 'астана водоканал', false, '');