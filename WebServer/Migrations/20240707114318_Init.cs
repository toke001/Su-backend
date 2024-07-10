using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebServer.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
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
                name: "SettingsValues");

            migrationBuilder.DropTable(
                name: "Tariff_Level");

            migrationBuilder.DropTable(
                name: "Universal_Refferences");

            migrationBuilder.DropTable(
                name: "Accounts");

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
