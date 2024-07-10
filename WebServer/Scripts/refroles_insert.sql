----Ref_Roles
INSERT INTO public."Ref_Roles"
("Code", "TypeName", "NameRu", "IsDel", "Description")
VALUES('USER_ADMIN', 'Базовая роль', 'Пользователь Администрирование', false, '');

INSERT INTO public."Ref_Roles"
("Code", "TypeName", "NameRu", "IsDel", "Description")
VALUES('SUPER_ADMIN', 'Расширенная роль', 'Пользователь Администрирование', false, '');

INSERT INTO public."Ref_Roles"
("Code", "TypeName", "NameRu", "IsDel", "Description")
VALUES('user_akimat_worker', 'Расширенная роль', 'Пользователь Акимата', false, '');

INSERT INTO public."Ref_Roles"
("Code", "TypeName", "NameRu", "IsDel", "Description")
VALUES('user_akimat_chief', 'Базовая роль', 'Пользователь Акимата', false, '');

INSERT INTO public."Ref_Roles"
("Code", "TypeName", "NameRu", "IsDel", "Description")
VALUES('user_all', 'Базовая роль', 'Пользователь Просмотр', false, '');

--------------------Ref_Access-------------------------------------------------

INSERT INTO public."Ref_Access"
("NameRu", "CodeAccessName", "TypeAccessName", "ActionName", "IsDel", "Description")
VALUES('Доступ на модуля администрирование', 'CAN_VIEW_ADMIN_MODULE', 'Экранная форма', 'чтение', false, '');

INSERT INTO public."Ref_Access"
("NameRu", "CodeAccessName", "TypeAccessName", "ActionName", "IsDel", "Description")
VALUES('Доступ на изменение структуры КАТО', 'CAN_VIEW_KATO', 'Сущность ', 'изменение', false, '');

INSERT INTO public."Ref_Access"
("NameRu", "CodeAccessName", "TypeAccessName", "ActionName", "IsDel", "Description")
VALUES('Доступ на редактирование обьекта КАТО', '', '', '', false, '');

---
INSERT INTO public."Ref_Access"
("NameRu", "CodeAccessName", "TypeAccessName", "ActionName", "IsDel", "Description")
VALUES('Блокировка учетки и сброс пароля на почту', '', 'Сущность', 'изменение', false, '');

INSERT INTO public."Ref_Access"
("NameRu", "CodeAccessName", "TypeAccessName", "ActionName", "IsDel", "Description")
VALUES('Создание учетки', '', 'Сущность ', 'изменение', false, '');

INSERT INTO public."Ref_Access"
("NameRu", "CodeAccessName", "TypeAccessName", "ActionName", "IsDel", "Description")
VALUES('Изменение учетки', '', 'Сущность', 'изменение', false, '');

---
INSERT INTO public."Ref_Access"
("NameRu", "CodeAccessName", "TypeAccessName", "ActionName", "IsDel", "Description")
VALUES('Доступ на редактирование обьекта КАТО', '', 'Сущность', 'изменение', false, '');

INSERT INTO public."Ref_Access"
("NameRu", "CodeAccessName", "TypeAccessName", "ActionName", "IsDel", "Description")
VALUES('Доступ на согласование', '', 'Атрибут ', 'согласование', false, '');

INSERT INTO public."Ref_Access"
("NameRu", "CodeAccessName", "TypeAccessName", "ActionName", "IsDel", "Description")
VALUES('Доступ на корректировку  (возврат)', '', 'Сущность', 'изменение', false, '');

---
INSERT INTO public."Ref_Access"
("NameRu", "CodeAccessName", "TypeAccessName", "ActionName", "IsDel", "Description")
VALUES('Забивка форм', '', 'Сущность', 'изменение', false, '');

INSERT INTO public."Ref_Access"
("NameRu", "CodeAccessName", "TypeAccessName", "ActionName", "IsDel", "Description")
VALUES('отправка формы на согласование', '', 'Атрибут ', 'согласование', false, '');

---
INSERT INTO public."Ref_Access"
("NameRu", "CodeAccessName", "TypeAccessName", "ActionName", "IsDel", "Description")
VALUES('Просмотр карты', '', 'Экранная форма', 'чтение', false, '');

INSERT INTO public."Ref_Access"
("NameRu", "CodeAccessName", "TypeAccessName", "ActionName", "IsDel", "Description")
VALUES('Просмотр отчетов', '', 'Экранная форма', 'чтение', false, '');

INSERT INTO public."Ref_Access"
("NameRu", "CodeAccessName", "TypeAccessName", "ActionName", "IsDel", "Description")
VALUES('Просмотр форм', '', 'Экранная форма', 'чтение', false, '');

INSERT INTO public."Ref_Access"
("NameRu", "CodeAccessName", "TypeAccessName", "ActionName", "IsDel", "Description")
VALUES('Просмотр финансов', '', '', '', false, '');

---
INSERT INTO public."Ref_Access"
("NameRu", "CodeAccessName", "TypeAccessName", "ActionName", "IsDel", "Description")
VALUES('Просмотр финансов', '', '', '', false, '');

--------------------Ref_Role_Access--------------------------

INSERT INTO public."Ref_Role_Access"
("RoleId", "AccessId", "NameRu", "NameKk", "IsDel", "Description")
VALUES(1, 1, 'Доступ на модуля администрирование', '', false, '');

INSERT INTO public."Ref_Role_Access"
("RoleId", "AccessId", "NameRu", "NameKk", "IsDel", "Description")
VALUES(1, 2, 'Доступ на изменение структуры КАТО', '', false, '');

INSERT INTO public."Ref_Role_Access"
("RoleId", "AccessId", "NameRu", "NameKk", "IsDel", "Description")
VALUES(1, 3, 'Доступ на редактирование обьекта КАТО', '', false, '');
--
INSERT INTO public."Ref_Role_Access"
("RoleId", "AccessId", "NameRu", "NameKk", "IsDel", "Description")
VALUES(2, 4, 'Блокировка учетки и сброс пароля на почту', '', false, '');

INSERT INTO public."Ref_Role_Access"
("RoleId", "AccessId", "NameRu", "NameKk", "IsDel", "Description")
VALUES(2, 5, 'Создание учетки', '', false, '');

INSERT INTO public."Ref_Role_Access"
("RoleId", "AccessId", "NameRu", "NameKk", "IsDel", "Description")
VALUES(2, 6, 'Изменение учетки', '', false, '');
---
INSERT INTO public."Ref_Role_Access"
("RoleId", "AccessId", "NameRu", "NameKk", "IsDel", "Description")
VALUES(3, 7, 'Доступ на редактирование обьекта КАТО', '', false, '');

INSERT INTO public."Ref_Role_Access"
("RoleId", "AccessId", "NameRu", "NameKk", "IsDel", "Description")
VALUES(3, 8, 'Доступ на согласование', '', false, '');

INSERT INTO public."Ref_Role_Access"
("RoleId", "AccessId", "NameRu", "NameKk", "IsDel", "Description")
VALUES(3, 9, 'Доступ на корректировку  (возврат)', '', false, '');
---
INSERT INTO public."Ref_Role_Access"
("RoleId", "AccessId", "NameRu", "NameKk", "IsDel", "Description")
VALUES(4, 10, 'Забивка форм', '', false, '');

INSERT INTO public."Ref_Role_Access"
("RoleId", "AccessId", "NameRu", "NameKk", "IsDel", "Description")
VALUES(4, 11, 'отправка формы на согласование', '', false, '');
---
INSERT INTO public."Ref_Role_Access"
("RoleId", "AccessId", "NameRu", "NameKk", "IsDel", "Description")
VALUES(5, 12, 'Просмотр карты', '', false, '');

INSERT INTO public."Ref_Role_Access"
("RoleId", "AccessId", "NameRu", "NameKk", "IsDel", "Description")
VALUES(5, 13, 'Просмотр отчетов', '', false, '');

INSERT INTO public."Ref_Role_Access"
("RoleId", "AccessId", "NameRu", "NameKk", "IsDel", "Description")
VALUES(5, 14, 'Просмотр форм', '', false, '');

INSERT INTO public."Ref_Role_Access"
("RoleId", "AccessId", "NameRu", "NameKk", "IsDel", "Description")
VALUES(5, 15, 'Просмотр финансов', '', false, '');

---------------------------------------------------------

