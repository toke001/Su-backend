CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
INSERT INTO public."Universal_Refferences"
("Id", "Code", "Type", "BusinessDecription", "NameRu", "NameKk", "IsDel", "Description")
VALUES(gen_random_uuid(), 'gov', 'owner', '��������������� 100%', '��������������� 100%', '��������������� 100%', false, '');


INSERT INTO public."Business_Dictionary"
("Id", "Code", "Type", "BusinessDecription", "NameRu", "NameKk", "IsDel", "Description")
VALUES(gen_random_uuid(), 'astsu', 'sem', '{BIN = 12124214}', '������ ���������', '������ ���������', false, '');