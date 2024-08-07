using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebServer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Bin = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    FullNameRu = table.Column<string>(type: "text", nullable: true),
                    FullNameKk = table.Column<string>(type: "text", nullable: true),
                    StreetName = table.Column<string>(type: "text", nullable: true),
                    BuildingNumber = table.Column<string>(type: "text", nullable: true),
                    KatoCode = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDel = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false, comment: "Примечания")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApprovedForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false, comment: "Информация о создании группы форм"),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата утверждения"),
                    CompletionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата завершения утвержденной формы"),
                    IsDel = table.Column<bool>(type: "boolean", nullable: false, comment: "Идентификатор удаления"),
                    DeletedById = table.Column<Guid>(type: "uuid", nullable: true, comment: "Идентификатор пользователя удалившего форму")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Business_Dictionary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true, comment: "ключ на ИД (своего типа или стороннего)"),
                    Code = table.Column<string>(type: "text", nullable: false, comment: "Код*"),
                    Type = table.Column<string>(type: "text", nullable: false, comment: "Тип*"),
                    BusinessDecription = table.Column<string>(type: "text", nullable: true, comment: "Бизнес описание"),
                    NameKk = table.Column<string>(type: "text", nullable: true, comment: "Наименование на каз"),
                    NameRu = table.Column<string>(type: "text", nullable: true, comment: "Наименование на рус"),
                    DescriptionKk = table.Column<string>(type: "text", nullable: true, comment: "Пояснение на каз"),
                    DescriptionRu = table.Column<string>(type: "text", nullable: true, comment: "Пояснение на рус"),
                    IsDel = table.Column<bool>(type: "boolean", nullable: false, comment: "Удален")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business_Dictionary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CityForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalCountCityOblast = table.Column<int>(type: "integer", nullable: true, comment: "Общее количество - городов в области (единиц)"),
                    TotalCountDomHoz = table.Column<int>(type: "integer", nullable: true, comment: "Общее количество - домохозяйств (кв, ИЖД)"),
                    TotalCountChel = table.Column<int>(type: "integer", nullable: true, comment: "Общее количество - проживающих в городских населенных пунктах (человек)"),
                    ObslPredpId = table.Column<Guid>(type: "uuid", nullable: true, comment: "Обслуживающее предприятие"),
                    KolichAbonent = table.Column<int>(type: "integer", nullable: true, comment: "Количество абонентов, охваченных централизованным водоснабжением (единиц)"),
                    KolFizLic = table.Column<int>(type: "integer", nullable: true, comment: "физических лиц/население (единиц)"),
                    KolYriLic = table.Column<int>(type: "integer", nullable: true, comment: "юридических лиц (единиц)"),
                    KolBydzhOrg = table.Column<int>(type: "integer", nullable: true, comment: "бюджетных организаций (единиц)"),
                    KolChelDostyp = table.Column<int>(type: "integer", nullable: true, comment: "Количество населения имеющих доступ к  централизованному водоснабжению (человек)"),
                    ObespechCentrlVodo = table.Column<decimal>(type: "numeric", nullable: true, comment: "Обеспеченность централизованным водоснабжением, в % гр.13/гр.6 *100"),
                    IndivUchetVodyVsego = table.Column<int>(type: "integer", nullable: true, comment: "Охват индивидуальными приборами учета воды по состоянию на конец отчетного года - всего с нарастающим (единиц)"),
                    IndivUchetVodyDistance = table.Column<int>(type: "integer", nullable: true, comment: "Охват индивидуальными приборами учета воды по состоянию на конец отчетного года - в том числе с дистанционной передачей данных в АСУЭ обслуживающего предприятия (единиц)"),
                    IndivUchetVodyPercent = table.Column<decimal>(type: "numeric", nullable: true, comment: "Охват индивидуальными приборами учета воды по состоянию на конец отчетного года - охват в %, гр.15/гр. 9*100"),
                    ObshePodlezhashKolZdan = table.Column<int>(type: "integer", nullable: true, comment: "Охват общедомовыми приборами учета воды по состоянию на конец отчетного года - Количество зданий и сооружений, подлежащих к установке общедомовых приборов учета (единиц)"),
                    ObsheUstanKolZdan = table.Column<int>(type: "integer", nullable: true, comment: "Охват общедомовыми приборами учета воды по состоянию на конец отчетного года - Количество зданий и сооружений с установленными общедомовыми приборами учета (единиц)"),
                    ObsheUstanPriborKol = table.Column<int>(type: "integer", nullable: true, comment: "Охват общедомовыми приборами учета воды по состоянию на конец отчетного года - Количество установленных общедомовых приборов учета (единиц)"),
                    ObsheUstanDistanceKol = table.Column<int>(type: "integer", nullable: true, comment: "Охват общедомовыми приборами учета воды по состоянию на конец отчетного года - в том числе с дистанционной передачей данных в АСУЭ обслуживающего предприятия (единиц)"),
                    ObsheOhvatPercent = table.Column<decimal>(type: "numeric", nullable: true, comment: "Охват общедомовыми приборами учета воды по состоянию на конец отчетного года - охват в %, гр.19/гр. 18*100"),
                    AutoProccesVodoZabor = table.Column<int>(type: "integer", nullable: true, comment: "Автоматизация производственных процессов водоснабжения и наличие централизованной системы контроля и управления (SCADA) - Водозабор (0 или 1)"),
                    AutoProccesVodoPodgot = table.Column<int>(type: "integer", nullable: true, comment: "Автоматизация производственных процессов водоснабжения и наличие централизованной системы контроля и управления (SCADA) - Водоподготовка (0 или 1)"),
                    AutoProccesNasosStanc = table.Column<int>(type: "integer", nullable: true, comment: "Автоматизация производственных процессов водоснабжения и наличие централизованной системы контроля и управления (SCADA) - Насосные станции (0 или 1)"),
                    AutoProccesSetVodosnab = table.Column<int>(type: "integer", nullable: true, comment: "Автоматизация производственных процессов водоснабжения и наличие централизованной системы контроля и управления (SCADA) - Сети водоснабжения (0 или 1)"),
                    KolAbonent = table.Column<int>(type: "integer", nullable: true, comment: "Кол-во абонентов, охваченных централизованным водоотведением (единиц)"),
                    KolAbonFizLic = table.Column<int>(type: "integer", nullable: true, comment: "Кол-во абонентов, охваченных централизованным водоотведением (единиц) - физических лиц/население (единиц)"),
                    KolAbonYriLic = table.Column<int>(type: "integer", nullable: true, comment: "Кол-во абонентов, охваченных централизованным водоотведением (единиц) - юридических лиц (единиц)"),
                    KolBydzhetOrg = table.Column<int>(type: "integer", nullable: true, comment: "Кол-во абонентов, охваченных централизованным водоотведением (единиц) - бюджетных организаций (единиц)"),
                    KolChelOhvatCentrVodo = table.Column<int>(type: "integer", nullable: true, comment: "Численность населения, охваченного централизованным водоотведением, (человек)"),
                    DostypCentrVodo = table.Column<decimal>(type: "numeric", nullable: true, comment: "Доступ к централизованному водоотведению, в % гр.31/гр.6*100"),
                    KolichKanaliz = table.Column<int>(type: "integer", nullable: true, comment: "Наличие канализационно-очистных сооружений, (единиц)"),
                    KolichKanalizMechan = table.Column<int>(type: "integer", nullable: true, comment: "Наличие канализационно-очистных сооружений, (единиц) - только с механичес-кой очисткой (еди-ниц)"),
                    KolichKanalizMechanBiolog = table.Column<int>(type: "integer", nullable: true, comment: "Наличие канализационно-очистных сооружений, (единиц) - с механической и биологической очист-кой (еди-ниц)"),
                    ProizvodKanaliz = table.Column<int>(type: "integer", nullable: true, comment: "Производительность канализационно-очистных сооружений (проектная)"),
                    IznosKanaliz = table.Column<decimal>(type: "numeric", nullable: true, comment: "Износ канализационно-очистных сооружений, в %"),
                    KolChelKanaliz = table.Column<int>(type: "integer", nullable: true, comment: "Численность населения, охваченного действующими канализационно-очистными сооружениями, (человек)"),
                    OhvatChelKanaliz = table.Column<decimal>(type: "numeric", nullable: true, comment: "Охват населения очисткой сточных вод, в % гр.38/гр.6*100"),
                    FactPostypKanaliz = table.Column<int>(type: "integer", nullable: true, comment: "Фактически поступило сточных вод в канализационно-очистные сооружения (тыс.м3)"),
                    FactPostypKanaliz1kv = table.Column<int>(type: "integer", nullable: true, comment: "Фактически поступило сточных вод в канализационно-очистные сооружения (тыс.м3) - За I квартал (тыс.м3)"),
                    FactPostypKanaliz2kv = table.Column<int>(type: "integer", nullable: true, comment: "Фактически поступило сточных вод в канализационно-очистные сооружения (тыс.м3) - За II квартал (тыс.м3)"),
                    FactPostypKanaliz3kv = table.Column<int>(type: "integer", nullable: true, comment: "Фактически поступило сточных вод в канализационно-очистные сооружения (тыс.м3) - За III квартал (тыс.м3)"),
                    FactPostypKanaliz4kv = table.Column<int>(type: "integer", nullable: true, comment: "Фактически поступило сточных вод в канализационно-очистные сооружения (тыс.м3) - За IV квартал (тыс.м3)"),
                    ObiemKanalizNormOchist = table.Column<int>(type: "integer", nullable: true, comment: "Объем сточных вод, соответствующей нормативной очистке по собственному лабораторному мониторингу за отчетный период (тыс.м3)"),
                    UrovenNormOchishVody = table.Column<int>(type: "integer", nullable: true, comment: "Уровень нормативно- очищенной воды, % гр.45/гр.40 * 100"),
                    AutoProccesSetKanaliz = table.Column<int>(type: "integer", nullable: true, comment: "Автоматизация производственных процессов водоотведения и наличие централизованной системы контроля и управления (SCADA) - Сети канализации (0 или 1)"),
                    AutoProccesKanalizNasos = table.Column<int>(type: "integer", nullable: true, comment: "Автоматизация производственных процессов водоотведения и наличие централизованной системы контроля и управления (SCADA) - Канализационные насосные станции (0 или 1)"),
                    AutoProccesKanalizSooruzh = table.Column<int>(type: "integer", nullable: true, comment: "Автоматизация производственных процессов водоотведения и наличие централизованной системы контроля и управления (SCADA) - Канализационно-очистные сооружения (0 или 1)"),
                    VodoSnabUsrednen = table.Column<int>(type: "integer", nullable: true, comment: "водоснабжение усредненный, тенге/м3"),
                    VodoSnabFizLic = table.Column<int>(type: "integer", nullable: true, comment: "водоснабжение физическим лицам/населению, тенге/м3"),
                    VodoSnabYriLic = table.Column<int>(type: "integer", nullable: true, comment: "водоснабжение юридическим лицам, тенге/м3"),
                    VodoSnabBydzhOrg = table.Column<int>(type: "integer", nullable: true, comment: "водоснабжение бюджетным организациям, тенге/м3"),
                    VodoOtvedUsred = table.Column<int>(type: "integer", nullable: true, comment: "водоотведение - усредненный, тенге/м3"),
                    VodoOtvedFizLic = table.Column<int>(type: "integer", nullable: true, comment: "водоотведение - физическим лицам/населению, тенге/м3"),
                    VodoOtvedYriLic = table.Column<int>(type: "integer", nullable: true, comment: "водоотведение - юридическим лицам, тенге/м3"),
                    VodoOtvedBydzhOrg = table.Column<int>(type: "integer", nullable: true, comment: "водоотведение - бюджетным организациям, тенге/м3"),
                    VodoProvodLengthTotal = table.Column<int>(type: "integer", nullable: true, comment: "Протяженность водопроводных сетей, км (по состоянию на конец отчетного года) - общая, км"),
                    VodoProvodLengthIznos = table.Column<int>(type: "integer", nullable: true, comment: "Протяженность водопроводных сетей, км (по состоянию на конец отчетного года) - в том числе изношенных, км"),
                    VodoProvodIznosPercent = table.Column<decimal>(type: "numeric", nullable: true, comment: "Протяженность водопроводных сетей, км (по состоянию на конец отчетного года) - Износ, % гр.59/гр.58"),
                    KanalizLengthTotal = table.Column<int>(type: "integer", nullable: true, comment: "Протяженность водопроводных сетей, км (по состоянию на конец отчетного года) - общая, км"),
                    KanalizLengthIznos = table.Column<int>(type: "integer", nullable: true, comment: "Протяженность канализационных сетей, км (по состоянию на конец отчетного года) - в том числе изношенных, км"),
                    KanalizIznosPercent = table.Column<decimal>(type: "numeric", nullable: true, comment: "Протяженность канализационных сетей, км (по состоянию на конец отчетного года) - Износ, % гр.62/гр.61"),
                    ObshNewSetiVodo = table.Column<int>(type: "integer", nullable: true, comment: "Общая протяженность построенных (новых) сетей в отчетном году, км - водоснабжения, км"),
                    ObshNewSetiKanaliz = table.Column<int>(type: "integer", nullable: true, comment: "Общая протяженность построенных (новых) сетей в отчетном году, км - водоотведения, км"),
                    ObshZamenSetiVodo = table.Column<int>(type: "integer", nullable: true, comment: "Общая протяженность реконструированных (замененных) сетей в отчетном году, км - водоснабжения, км"),
                    ObshZamenSetiKanaliz = table.Column<int>(type: "integer", nullable: true, comment: "Общая протяженность реконструированных (замененных) сетей в отчетном году, км - водоотведения, км"),
                    ObshRemontSetiVodo = table.Column<int>(type: "integer", nullable: true, comment: "Общая протяженность отремонтированных (текущий/капитальный ремонт) сетей в отчетном году, км - водоснабжения, км"),
                    ObshRemontSetiKanaliz = table.Column<int>(type: "integer", nullable: true, comment: "Общая протяженность отремонтированных (текущий/капитальный ремонт) сетей в отчетном году, км - водоотведения, км")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Data",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApprovedFormId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApprovedFormItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApproverFormColumnId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReportFormId = table.Column<Guid>(type: "uuid", nullable: false),
                    ValueType = table.Column<int>(type: "integer", nullable: false),
                    ValueJson = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDel = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false, comment: "Примечания")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Data", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ref_Access",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameRu = table.Column<string>(type: "text", nullable: false, comment: "Наименование доступа"),
                    CodeAccessName = table.Column<string>(type: "text", nullable: false, comment: "Код доступа"),
                    TypeAccessName = table.Column<string>(type: "text", nullable: false, comment: "Тип доступа"),
                    ActionName = table.Column<string>(type: "text", nullable: false, comment: "Действие"),
                    NameKk = table.Column<string>(type: "text", nullable: true),
                    IsDel = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_Access", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ref_Katos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<long>(type: "bigint", nullable: false),
                    Latitude = table.Column<decimal>(type: "numeric", nullable: true),
                    Longitude = table.Column<decimal>(type: "numeric", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsReportable = table.Column<bool>(type: "boolean", nullable: false),
                    KatoLevel = table.Column<int>(type: "integer", nullable: true, comment: "Категории населенных пунктов. 0-область,1-город,2-село,3-район"),
                    ParentRaion = table.Column<int>(type: "integer", nullable: true, comment: "Если это район, он смотрит сам на себя, если это село, код района,"),
                    ParentObl = table.Column<int>(type: "integer", nullable: true, comment: "Область Астана,Алматы...сам на себя ссылка, если район код области"),
                    NameRu = table.Column<string>(type: "text", nullable: false),
                    NameKk = table.Column<string>(type: "text", nullable: true),
                    IsDel = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_Katos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ref_Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false, comment: "Код роли"),
                    TypeName = table.Column<string>(type: "text", nullable: false, comment: "Тип роли"),
                    NameRu = table.Column<string>(type: "text", nullable: false),
                    NameKk = table.Column<string>(type: "text", nullable: true),
                    IsDel = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ref_Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameRu = table.Column<string>(type: "text", nullable: false),
                    NameKk = table.Column<string>(type: "text", nullable: true),
                    IsDel = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResponseCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameCode = table.Column<string>(type: "text", nullable: false),
                    DescriptionCode = table.Column<string>(type: "text", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeloForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusOpor = table.Column<bool>(type: "boolean", nullable: true, comment: "Статус села опорное"),
                    StatusSput = table.Column<bool>(type: "boolean", nullable: true, comment: "Статус села спутниковое"),
                    StatusProch = table.Column<string>(type: "text", nullable: true, comment: "Статус села прочие"),
                    StatusPrigran = table.Column<bool>(type: "boolean", nullable: true, comment: "Статус села приграничные"),
                    ObshKolSelNasPun = table.Column<int>(type: "integer", nullable: true, comment: "Общее количество сельских населенных пунктов в области(единиц)"),
                    ObshKolChelNasPun = table.Column<int>(type: "integer", nullable: true, comment: "Общая численность населения в сельских населенных пунктах (человек)"),
                    ObshKolDomHoz = table.Column<int>(type: "integer", nullable: true, comment: "Общее количество домохозяйств (квартир, ИЖД)"),
                    YearSystVodoSnab = table.Column<string>(type: "text", nullable: true, comment: "Год постройки системы водоснабжения"),
                    ObslPredpId = table.Column<Guid>(type: "uuid", nullable: true, comment: "Обслуживающее предприятие"),
                    SobstId = table.Column<Guid>(type: "uuid", nullable: true, comment: "в чьей собственности находится"),
                    DosVodoSnabKolPunk = table.Column<int>(type: "integer", nullable: true, comment: "Доступ населения к услугам водоснабжения Количество сельских населенных пунктов (единиц)"),
                    DosVodoSnabKolChel = table.Column<int>(type: "integer", nullable: true, comment: "Доступ населения к услугам водоснабжения Численность населения, проживающего в данных сельских населенных пунктах (человек)"),
                    DosVodoSnabPercent = table.Column<decimal>(type: "numeric", nullable: true, comment: "Доступ населения к услугам водоснабжения, %"),
                    CentrVodoSnabKolNasPun = table.Column<int>(type: "integer", nullable: true, comment: "Кол-во сельских населенных пунктов (единиц)"),
                    CentrVodoSnabKolChel = table.Column<int>(type: "integer", nullable: true, comment: "Численность населения, проживающего в данных сельских населенных пунктах (человек)"),
                    CentrVodoSnabObesKolNasPunk = table.Column<decimal>(type: "numeric", nullable: true, comment: "Обеспеченность централизованным водоснабжением по количеству сельских населенных пунктов, % гр.19/гр.8 *100"),
                    CentrVodoSnabObesKolChel = table.Column<decimal>(type: "numeric", nullable: true, comment: "Обеспеченность централизованным водоснабжением по численности населения, % гр.20/гр.9 *100"),
                    CentrVodoSnabKolAbon = table.Column<int>(type: "integer", nullable: true, comment: "Кол-во абонентов, охваченных централизованным водоснабжением (единиц)"),
                    CentrVodoSnabFizLic = table.Column<int>(type: "integer", nullable: true, comment: "в том числе физических лиц/население (единиц)"),
                    CentrVodoSnabYriLic = table.Column<int>(type: "integer", nullable: true, comment: "в том числе юридических лиц (единиц)"),
                    CentrVodoSnabBudzhOrg = table.Column<int>(type: "integer", nullable: true, comment: "в том числе бюджетных организаций (единиц)"),
                    CentrVodoIndivPriborUchVodyVsego = table.Column<int>(type: "integer", nullable: true, comment: "Всего установлено индивидуальных приборов учета воды по состоянию на конец отчетного года (единиц)"),
                    CentrVodoIndivPriborUchVodyASYE = table.Column<int>(type: "integer", nullable: true, comment: "в том числе с дистанционной передачей данных в АСУЭ обслуживающего предприятия (единиц)"),
                    CentrVodoIndivPriborUchVodyOhvat = table.Column<decimal>(type: "numeric", nullable: true, comment: "Охват индивидуальными приборами учета воды, % гр.27/гр. 23*100"),
                    NeCtentrVodoKolSelsNasPunk = table.Column<int>(type: "integer", nullable: true, comment: "Нецентрализованное водоснабжение Количество сельских населенных пунктов (единиц)"),
                    KbmKolSelsNasPunk = table.Column<int>(type: "integer", nullable: true, comment: "Нецентрализованное водоснабжение КБМ Количество сельских населенных пунктов, где установлено КБМ"),
                    KbmKolChel = table.Column<int>(type: "integer", nullable: true, comment: "Нецентрализованное водоснабжение КБМ Численность населения, проживающего в сельских населенных пунктах, где установлены КБМ (человек)"),
                    KbmObespNasel = table.Column<decimal>(type: "numeric", nullable: true, comment: "Нецентрализованное водоснабжение КБМ Обеспеченность населения услугами КБМ, % гр.32/гр.9*100"),
                    PrvKolSelsNasPunk = table.Column<int>(type: "integer", nullable: true, comment: "Нецентрализованное водоснабжение ПРВ Количество сельских населенных пунктов, где установлено ПРВ"),
                    PrvKolChel = table.Column<int>(type: "integer", nullable: true, comment: "Нецентрализованное водоснабжение ПРВ Численность населения, проживающего в сельских населенных пунктах, где установлены ПРВ (человек)"),
                    PrvObespNasel = table.Column<decimal>(type: "numeric", nullable: true, comment: "Нецентрализованное водоснабжение ПРВ Обеспеченность населения услугами  ПРВ, % гр.35/гр.9*100"),
                    PrivVodaKolSelsNasPunk = table.Column<int>(type: "integer", nullable: true, comment: "Нецентрализованное водоснабжение Привозная вода Количество сельских населенных пунктов, жители которых используют привозную воду"),
                    PrivVodaKolChel = table.Column<int>(type: "integer", nullable: true, comment: "Нецентрализованное водоснабжение Привозная вода Численность населения, проживающего в сельских населенных пунктах, где используют привозную воду"),
                    PrivVodaObespNasel = table.Column<decimal>(type: "numeric", nullable: true, comment: "Нецентрализованное водоснабжение Привозная вода Обеспеченность населения привозной водой, % гр.38/гр.9*100"),
                    SkvazhKolSelsNasPunk = table.Column<int>(type: "integer", nullable: true, comment: "Нецентрализованное водоснабжение Скважины, колодцы Количество сельских населенных пунктов, жители которых используют воду из скважин и колодцов"),
                    SkvazhKolChel = table.Column<int>(type: "integer", nullable: true, comment: "Нецентрализованное водоснабжение Скважины, колодцы Численность населения, проживающего в сельских населенных пунктах, где используют  воду из скважин и колодцев"),
                    SkvazhObespNasel = table.Column<decimal>(type: "numeric", nullable: true, comment: "Нецентрализованное водоснабжение Скважины, колодцы Обеспеченность  привозной водой, % гр.41/гр.9*100"),
                    SkvazhKolSelsNasPunkOtkaz = table.Column<int>(type: "integer", nullable: true, comment: "Нецентрализованное водоснабжение Скважины, колодцы Количество сельских населенных пунктов, жители которых отказались от строительства ЦВ, установки КБМ и ПРВ  (наличие протоколов  отказа)"),
                    SkvazhKolChelOtkaz = table.Column<int>(type: "integer", nullable: true, comment: "Нецентрализованное водоснабжение Скважины, колодцы Численность населения, жители которых отказались от строительства ЦВ, установки КБМ и ПРВ  (наличие протоколов  отказа)"),
                    SkvazhDolyaNaselOtkaz = table.Column<decimal>(type: "numeric", nullable: true, comment: "Нецентрализованное водоснабжение Скважины, колодцы Доля населения, жители которых отказались от строительства ЦВ, установки КБМ и ПРВ  (наличие протоколов  отказа), гр.44/гр.9*100"),
                    SkvazhDolyaSelOtkaz = table.Column<decimal>(type: "numeric", nullable: true, comment: "Нецентрализованное водоснабжение Скважины, колодцы Доля сел, жители которых отказались от  строительства ЦВ, установки КБМ и ПРВ, %, гр.43/гр.8*100"),
                    CentrVodOtvedKolSelsNasPunk = table.Column<int>(type: "integer", nullable: true, comment: "Централизованное водоотведение Кол-во сельских населенных пунктов (единиц)"),
                    CentrVodOtvedKolChel = table.Column<int>(type: "integer", nullable: true, comment: "Централизованное водоотведение Численность населения, проживающего в данных сельских населенных пунктах (человек)"),
                    CentrVodOtvedKolAbonent = table.Column<int>(type: "integer", nullable: true, comment: "Централизованное водоотведение Кол-во абонентов, проживающих в данных сельских населенных пунктах (единиц)"),
                    CentrVodOtvedFizLic = table.Column<int>(type: "integer", nullable: true, comment: "Централизованное водоотведение в том числе физических лиц/население (единиц)"),
                    CentrVodOtvedYriLic = table.Column<int>(type: "integer", nullable: true, comment: "Централизованное водоотведение в том числе юридических лиц (единиц)"),
                    CentrVodOtvedBydzhOrg = table.Column<int>(type: "integer", nullable: true, comment: "Централизованное водоотведение в том числе бюджетных организаций (единиц)"),
                    CentrVodOtvedDostypKolNasPunk = table.Column<decimal>(type: "numeric", nullable: true, comment: "Централизованное водоотведение Доступ к централизованному водоотведению по количеству сельских населенных пунктов, в % гр.47/гр.8 *100"),
                    CentrVodOtvedDostypKolChel = table.Column<decimal>(type: "numeric", nullable: true, comment: "Централизованное водоотведение Доступ к централизованному водоотведению по численности населения, в % гр.48/гр.9 *100"),
                    CentrVodOtvedNalich = table.Column<int>(type: "integer", nullable: true, comment: "Централизованное водоотведение Наличие канализационно- очистных сооружений (единиц)"),
                    CentrVodOtvedNalichMechan = table.Column<int>(type: "integer", nullable: true, comment: "Централизованное водоотведение в том числе только с механичес-кой очисткой (еди-ниц)"),
                    CentrVodOtvedNalichMechanBiolog = table.Column<int>(type: "integer", nullable: true, comment: "Централизованное водоотведение в том числе с механической и биологической очист-кой (еди-ниц)"),
                    CentrVodOtvedProizvod = table.Column<int>(type: "integer", nullable: true, comment: "Централизованное водоотведение Производительность канализационно-очистных сооружений (проектная)"),
                    CentrVodOtvedIznos = table.Column<decimal>(type: "numeric", nullable: true, comment: "Централизованное водоотведение Износ канализационно- очистных сооружений, в %"),
                    CentrVodOtvedOhvatKolChel = table.Column<int>(type: "integer", nullable: true, comment: "Централизованное водоотведение Числен-ность населе-ния, охваченного действующими канализационно- очистными сооружениями (человек)"),
                    CentrVodOtvedOhvatNasel = table.Column<decimal>(type: "numeric", nullable: true, comment: "Централизованное водоотведение Охват населения очисткой сточных вод, в % гр.60/гр.9*100"),
                    CentrVodOtvedFactPostypStochVod = table.Column<int>(type: "integer", nullable: true, comment: "Централизованное водоотведение Фактически поступило сточных вод в канализационно-очистные сооружения (тыс.м3)"),
                    CentrVodOtvedFactPostypStochVod1 = table.Column<int>(type: "integer", nullable: true, comment: "Централизованное водоотведение В том числе За I квартал (тыс.м3)"),
                    CentrVodOtvedFactPostypStochVod2 = table.Column<int>(type: "integer", nullable: true, comment: "Централизованное водоотведение В том числе За II квартал (тыс.м3)"),
                    CentrVodOtvedFactPostypStochVod3 = table.Column<int>(type: "integer", nullable: true, comment: "Централизованное водоотведение В том числе За III квартал (тыс.м3)"),
                    CentrVodOtvedFactPostypStochVod4 = table.Column<int>(type: "integer", nullable: true, comment: "Централизованное водоотведение В том числе За IV квартал (тыс.м3)"),
                    CentrVodOtvedObiemStochVod = table.Column<int>(type: "integer", nullable: true, comment: "Централизованное водоотведение Объем сточных вод, соответствующей нормативной очистке по собственному лабораторному мониторингу за отчетный период (тыс.м3)"),
                    CentrVodOtvedUrovenNorm = table.Column<decimal>(type: "numeric", nullable: true, comment: "Централизованное водоотведение Уровень нормативно- очищенной воды, % гр.67/гр.62 * 100"),
                    DecentrVodoOtvedKolSelsNasPunk = table.Column<int>(type: "integer", nullable: true, comment: "Децентрализованное водоотведение Кол-во сельских населенных пунктов (единиц)"),
                    DecentrVodoOtvedKolChel = table.Column<int>(type: "integer", nullable: true, comment: "Децентрализованное водоотведение Численность населения, проживающего в данных сельских населенных пунктах (человек)"),
                    TarifVodoSnabUsred = table.Column<int>(type: "integer", nullable: true, comment: "Уровень тарифов водоснабжение усредненный, тенге/м3"),
                    TarifVodoSnabFizL = table.Column<int>(type: "integer", nullable: true, comment: "Уровень тарифов водоснабжение физическим лицам/населению, тенге/м3"),
                    TarifVodoSnabYriL = table.Column<int>(type: "integer", nullable: true, comment: "Уровень тарифов водоснабжение юридическим лицам, тенге/м3"),
                    TarifVodoSnabBudzh = table.Column<int>(type: "integer", nullable: true, comment: "Уровень тарифов водоснабжение бюджетным организациям, тенге/м3"),
                    TarifVodoOtvedUsred = table.Column<int>(type: "integer", nullable: true, comment: "Уровень тарифов водоотведение усредненный, тенге/м3"),
                    TarifVodoOtvedFizL = table.Column<int>(type: "integer", nullable: true, comment: "Уровень тарифов водоотведение физическим лицам/населению, тенге/м3"),
                    TarifVodoOtvedYriL = table.Column<int>(type: "integer", nullable: true, comment: "Уровень тарифов водоотведение юридическим лицам, тенге/м3"),
                    TarifVodoOtvedBudzh = table.Column<int>(type: "integer", nullable: true, comment: "Уровень тарифов водоотведение бюджетным организациям, тенге/м3"),
                    ProtyzhVodoSeteyObsh = table.Column<int>(type: "integer", nullable: true, comment: "Протяженность водопроводных сетей, км (по состоянию на конец отчетного года) общая, км"),
                    ProtyzhVodoSeteyVtomIznos = table.Column<int>(type: "integer", nullable: true, comment: "Протяженность водопроводных сетей, км (по состоянию на конец отчетного года) в том числе изношенных, км"),
                    ProtyzhVodoSeteyIznos = table.Column<decimal>(type: "numeric", nullable: true, comment: "Протяженность водопроводных сетей, км (по состоянию на конец отчетного года) Износ, % гр.80/гр.79"),
                    ProtyzhKanalSeteyObsh = table.Column<int>(type: "integer", nullable: true, comment: "Протяженность канализационных сетей, км (по состоянию на конец отчетного года) общая, км"),
                    ProtyzhKanalSeteyVtomIznos = table.Column<int>(type: "integer", nullable: true, comment: "Протяженность канализационных сетей, км (по состоянию на конец отчетного года) в том числе изношенных, км"),
                    ProtyzhKanalSeteyIznos = table.Column<decimal>(type: "numeric", nullable: true, comment: "Протяженность канализационных сетей, км (по состоянию на конец отчетного года) Износ, % гр.83/гр.82"),
                    ProtyzhNewSeteyVodoSnab = table.Column<int>(type: "integer", nullable: true, comment: "Общая протяженность построенных (новых) сетей в отчетном году, км - водоснабжения, км"),
                    ProtyzhNewSeteyVodoOtved = table.Column<int>(type: "integer", nullable: true, comment: "Общая протяженность построенных (новых) сетей в отчетном году, км - водоотведения, км"),
                    ProtyzhRekonSeteyVodoSnab = table.Column<int>(type: "integer", nullable: true, comment: "Общая протяженность реконструированных (замененных) сетей в отчетном году, км - водоснабжения, км"),
                    ProtyzhRekonSeteyVodoOtved = table.Column<int>(type: "integer", nullable: true, comment: "Общая протяженность реконструированных (замененных) сетей в отчетном году, км - водоотведения, км"),
                    ProtyzhRemontSeteyVodoSnab = table.Column<int>(type: "integer", nullable: true, comment: "Общая протяженность отремонтированных (текущий/капитальный ремонт) сетей в отчетном году, км - водоснабжения, км"),
                    ProtyzhRemontSeteyVodoOtved = table.Column<int>(type: "integer", nullable: true, comment: "Общая протяженность отремонтированных (текущий/капитальный ремонт) сетей в отчетном году, км - водоотведения, км")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeloForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettingsValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingsValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Universal_Refferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true, comment: "ключ на ИД (своего типа или стороннего)"),
                    Code = table.Column<string>(type: "text", nullable: false, comment: "Код*"),
                    Type = table.Column<string>(type: "text", nullable: false, comment: "Тип*"),
                    BusinessDecription = table.Column<string>(type: "text", nullable: true, comment: "Бизнес описание"),
                    NameKk = table.Column<string>(type: "text", nullable: true, comment: "Наименование на каз"),
                    NameRu = table.Column<string>(type: "text", nullable: true, comment: "Наименование на рус"),
                    DescriptionKk = table.Column<string>(type: "text", nullable: true, comment: "Пояснение на каз"),
                    DescriptionRu = table.Column<string>(type: "text", nullable: true, comment: "Пояснение на рус"),
                    IsDel = table.Column<bool>(type: "boolean", nullable: false, comment: "Удален")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universal_Refferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActionLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    FormId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionLogs_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApprovedFormItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApprovedFormId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false, comment: "Сервис. 0 - водоснабжение, 1- водоотведение, 2- водопровод"),
                    Title = table.Column<string>(type: "text", nullable: false, comment: "Заголовок Формы (короткий)"),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false, comment: "Порядок отображения"),
                    IsVillage = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак села"),
                    IsDel = table.Column<bool>(type: "boolean", nullable: false, comment: "Идентификатор удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedFormItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovedFormItems_ApprovedForms_ApprovedFormId",
                        column: x => x.ApprovedFormId,
                        principalTable: "ApprovedForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CityDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CityFormId = table.Column<Guid>(type: "uuid", nullable: true, comment: "Главная форма город"),
                    KodNaselPunk = table.Column<string>(type: "text", nullable: false, comment: "Код населенного пункта (КАТО)"),
                    KodOblast = table.Column<string>(type: "text", nullable: true, comment: "Код обалсти (КАТО)"),
                    KodRaiona = table.Column<string>(type: "text", nullable: true, comment: "Код района (КАТО)"),
                    Login = table.Column<string>(type: "text", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: false, comment: "За какой год данные")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityDocuments_CityForms_CityFormId",
                        column: x => x.CityFormId,
                        principalTable: "CityForms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ref_Streets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RefKatoId = table.Column<int>(type: "integer", nullable: false),
                    NameRu = table.Column<string>(type: "text", nullable: false),
                    NameKk = table.Column<string>(type: "text", nullable: true),
                    IsDel = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_Streets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ref_Streets_Ref_Katos_RefKatoId",
                        column: x => x.RefKatoId,
                        principalTable: "Ref_Katos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Bin = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    KatoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_Ref_Katos_KatoId",
                        column: x => x.KatoId,
                        principalTable: "Ref_Katos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDel = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false, comment: "Примечания")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_Roles_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Account_Roles_Ref_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Ref_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ref_Role_Access",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false, comment: "Айди roles"),
                    AccessId = table.Column<int>(type: "integer", nullable: false),
                    NameRu = table.Column<string>(type: "text", nullable: false),
                    NameKk = table.Column<string>(type: "text", nullable: true),
                    IsDel = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_Role_Access", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ref_Role_Access_Ref_Access_AccessId",
                        column: x => x.AccessId,
                        principalTable: "Ref_Access",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ref_Role_Access_Ref_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Ref_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report_Forms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApprovedFormId = table.Column<Guid>(type: "uuid", nullable: false),
                    RefKatoId = table.Column<int>(type: "integer", nullable: false),
                    ReportYearId = table.Column<int>(type: "integer", nullable: false),
                    ReportMonthId = table.Column<int>(type: "integer", nullable: false),
                    RefStatusId = table.Column<int>(type: "integer", nullable: false),
                    HasStreets = table.Column<bool>(type: "boolean", nullable: false, comment: "Наличие улиц в селе"),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDel = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false, comment: "Примечания")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report_Forms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_Forms_ApprovedForms_ApprovedFormId",
                        column: x => x.ApprovedFormId,
                        principalTable: "ApprovedForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Report_Forms_Ref_Katos_RefKatoId",
                        column: x => x.RefKatoId,
                        principalTable: "Ref_Katos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Report_Forms_Ref_Statuses_RefStatusId",
                        column: x => x.RefStatusId,
                        principalTable: "Ref_Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeloDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SeloFormId = table.Column<Guid>(type: "uuid", nullable: true, comment: "Главная форма город"),
                    KodNaselPunk = table.Column<string>(type: "text", nullable: true, comment: "Код населенного пункта (КАТО)"),
                    KodOblast = table.Column<string>(type: "text", nullable: true, comment: "Код обалсти (КАТО)"),
                    KodRaiona = table.Column<string>(type: "text", nullable: true, comment: "Код района (КАТО)"),
                    Login = table.Column<string>(type: "text", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: false, comment: "За какой год данные")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeloDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeloDocuments_SeloForms_SeloFormId",
                        column: x => x.SeloFormId,
                        principalTable: "SeloForms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApprovedFormItemColumns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApprovedFormItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataType = table.Column<int>(type: "integer", nullable: false, comment: "Тип хранимых данных: Label(Просто отображение), IntegerType, DecimalType, StringType, BooleanType, DateType, CalcType"),
                    Length = table.Column<int>(type: "integer", nullable: false),
                    Nullable = table.Column<bool>(type: "boolean", nullable: false),
                    NameKk = table.Column<string>(type: "text", nullable: false, comment: "Заголовок столбца на казахском"),
                    NameRu = table.Column<string>(type: "text", nullable: false, comment: "Заголовок столбца на русский"),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false, comment: "Порядок отображения"),
                    ReportCode = table.Column<string>(type: "text", nullable: true, comment: "уникальный код для отчета внутри формы, может дублироваться в других формах")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedFormItemColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovedFormItemColumns_ApprovedFormItems_ApprovedFormItemId",
                        column: x => x.ApprovedFormItemId,
                        principalTable: "ApprovedFormItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ref_Buildings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RefStreetId = table.Column<int>(type: "integer", nullable: false),
                    Building = table.Column<string>(type: "text", nullable: false),
                    NameRu = table.Column<string>(type: "text", nullable: false),
                    NameKk = table.Column<string>(type: "text", nullable: true),
                    IsDel = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_Buildings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ref_Buildings_Ref_Streets_RefStreetId",
                        column: x => x.RefStreetId,
                        principalTable: "Ref_Streets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consumers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    Ref_KatoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consumers_Ref_Katos_Ref_KatoId",
                        column: x => x.Ref_KatoId,
                        principalTable: "Ref_Katos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Consumers_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pipelines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalPipelineLength = table.Column<decimal>(type: "numeric", nullable: false, comment: "Протяженность водопроводных сетей, км (по состоянию на конец отчетного года),общая, км"),
                    WornPipelineLength = table.Column<decimal>(type: "numeric", nullable: false, comment: "в том числе изношенных, км"),
                    TotalSewerNetworkLength = table.Column<decimal>(type: "numeric", nullable: false, comment: "Протяженность канализационных сетей, км (по состоянию на конец отчетного года),общая, км"),
                    WornSewerNetworkLength = table.Column<decimal>(type: "numeric", nullable: false, comment: "в том числе изношенных, км"),
                    NewWaterSupplyNetworkLength = table.Column<decimal>(type: "numeric", nullable: false, comment: "Общая протяженность построенных (новых) сетей в отчетном году, км, водоснабжения, км"),
                    NewWastewaterNetworkLength = table.Column<decimal>(type: "numeric", nullable: false, comment: "водоотведения, км"),
                    ReconstructedNetworkLength = table.Column<decimal>(type: "numeric", nullable: false, comment: "Общая протяженность реконструированных (замененных) сетей в отчетном году, км, водоснабжения, км"),
                    ReconstructedWastewaterNetworkLength = table.Column<decimal>(type: "numeric", nullable: false, comment: "водоотведения, км"),
                    RepairedWaterSupplyNetworkLength = table.Column<decimal>(type: "numeric", nullable: false, comment: "Общая протяженность отремонтированных (текущий/капитальный ремонт) сетей в отчетном году, км, водоснабжения, км"),
                    RepairedWastewaterNetworkLength = table.Column<decimal>(type: "numeric", nullable: false, comment: "водоотведения, км"),
                    TotalPopulation = table.Column<decimal>(type: "numeric", nullable: false, comment: "численность населения (вся)"),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDel = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false, comment: "Примечания")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pipelines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pipelines_Report_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Report_Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportSuppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Report_FormId = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDel = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false, comment: "Примечания")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportSuppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportSuppliers_Report_Forms_Report_FormId",
                        column: x => x.Report_FormId,
                        principalTable: "Report_Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportSuppliers_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tariff_Level",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormId = table.Column<Guid>(type: "uuid", nullable: false),
                    TariffAverage = table.Column<decimal>(type: "numeric", nullable: false, comment: "усредненный, тенге/м3"),
                    TariffIndividual = table.Column<decimal>(type: "numeric", nullable: false, comment: "физическим лицам/населению, тенге/м3"),
                    TariffLegal = table.Column<decimal>(type: "numeric", nullable: false, comment: "юридическим лицам, тенге/м3"),
                    TariffBudget = table.Column<decimal>(type: "numeric", nullable: false, comment: "бюджетным организациям, тенге/м3"),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDel = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false, comment: "Примечания")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariff_Level", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tariff_Level_Report_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Report_Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColumnLayouts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApprovedFormItemColumnId = table.Column<Guid>(type: "uuid", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: true),
                    Width = table.Column<int>(type: "integer", nullable: true),
                    Position = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnLayouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColumnLayouts_ApprovedFormItemColumns_ApprovedFormItemColum~",
                        column: x => x.ApprovedFormItemColumnId,
                        principalTable: "ApprovedFormItemColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Roles_AccountId",
                table: "Account_Roles",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Roles_RoleId",
                table: "Account_Roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionLogs_AccountId",
                table: "ActionLogs",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedFormItemColumns_ApprovedFormItemId",
                table: "ApprovedFormItemColumns",
                column: "ApprovedFormItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedFormItems_ApprovedFormId",
                table: "ApprovedFormItems",
                column: "ApprovedFormId");

            migrationBuilder.CreateIndex(
                name: "IX_CityDocuments_CityFormId",
                table: "CityDocuments",
                column: "CityFormId");

            migrationBuilder.CreateIndex(
                name: "IX_ColumnLayouts_ApprovedFormItemColumnId",
                table: "ColumnLayouts",
                column: "ApprovedFormItemColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_Ref_KatoId",
                table: "Consumers",
                column: "Ref_KatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_SupplierId",
                table: "Consumers",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Pipelines_FormId",
                table: "Pipelines",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_Ref_Buildings_RefStreetId",
                table: "Ref_Buildings",
                column: "RefStreetId");

            migrationBuilder.CreateIndex(
                name: "IX_Ref_Role_Access_AccessId",
                table: "Ref_Role_Access",
                column: "AccessId");

            migrationBuilder.CreateIndex(
                name: "IX_Ref_Role_Access_RoleId",
                table: "Ref_Role_Access",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Ref_Streets_RefKatoId",
                table: "Ref_Streets",
                column: "RefKatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_Forms_ApprovedFormId",
                table: "Report_Forms",
                column: "ApprovedFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_Forms_RefKatoId",
                table: "Report_Forms",
                column: "RefKatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_Forms_RefStatusId",
                table: "Report_Forms",
                column: "RefStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportSuppliers_Report_FormId",
                table: "ReportSuppliers",
                column: "Report_FormId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportSuppliers_SupplierId",
                table: "ReportSuppliers",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SeloDocuments_SeloFormId",
                table: "SeloDocuments",
                column: "SeloFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_KatoId",
                table: "Suppliers",
                column: "KatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tariff_Level_FormId",
                table: "Tariff_Level",
                column: "FormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account_Roles");

            migrationBuilder.DropTable(
                name: "ActionLogs");

            migrationBuilder.DropTable(
                name: "Business_Dictionary");

            migrationBuilder.DropTable(
                name: "CityDocuments");

            migrationBuilder.DropTable(
                name: "ColumnLayouts");

            migrationBuilder.DropTable(
                name: "Consumers");

            migrationBuilder.DropTable(
                name: "Data");

            migrationBuilder.DropTable(
                name: "Pipelines");

            migrationBuilder.DropTable(
                name: "Ref_Buildings");

            migrationBuilder.DropTable(
                name: "Ref_Role_Access");

            migrationBuilder.DropTable(
                name: "ReportSuppliers");

            migrationBuilder.DropTable(
                name: "ResponseCodes");

            migrationBuilder.DropTable(
                name: "SeloDocuments");

            migrationBuilder.DropTable(
                name: "SettingsValues");

            migrationBuilder.DropTable(
                name: "Tariff_Level");

            migrationBuilder.DropTable(
                name: "Universal_Refferences");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "CityForms");

            migrationBuilder.DropTable(
                name: "ApprovedFormItemColumns");

            migrationBuilder.DropTable(
                name: "Ref_Streets");

            migrationBuilder.DropTable(
                name: "Ref_Access");

            migrationBuilder.DropTable(
                name: "Ref_Roles");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "SeloForms");

            migrationBuilder.DropTable(
                name: "Report_Forms");

            migrationBuilder.DropTable(
                name: "ApprovedFormItems");

            migrationBuilder.DropTable(
                name: "Ref_Katos");

            migrationBuilder.DropTable(
                name: "Ref_Statuses");

            migrationBuilder.DropTable(
                name: "ApprovedForms");
        }
    }
}
