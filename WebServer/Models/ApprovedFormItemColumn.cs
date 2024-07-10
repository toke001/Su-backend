using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WebServer.Emuns.Enums;

namespace WebServer.Models
{
    public class ApprovedFormItemColumn
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid ApprovedFormItemId { get; set; }
        public ApprovedFormItem? ApprovedFormItem { get; set; }

        [Comment("Тип хранимых данных: Label(Просто отображение), IntegerType, DecimalType, StringType, BooleanType, DateType, CalcType")]
        public DataTypeEnum DataType { get; set; }
        public int Length { get; set; }
        public bool Nullable { get; set; }

        [Comment("Заголовок столбца на казахском")]
        public string NameKk { get; set; } = "";
        [Comment("Заголовок столбца на русский")]
        public string NameRu { get; set; } = "";
        [Comment("Порядок отображения")]
        public int DisplayOrder { get; set; } = 1;
        
        [Comment("уникальный код для отчета внутри формы, может дублироваться в других формах")]
        public string? ReportCode { get; set; }


    }

    public class ColumnLayout
    {
        public Guid Id { get; set; }
        public Guid ApprovedFormItemColumnId { get; set; }
        public ApprovedFormItemColumn? ApprovedFormItemColumn { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public string? Position { get; set; }
    }
}
