using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServer.Models
{
    public class Data : Base
    {
        public Guid ApprovedFormId { get; set; }
        public Guid ApprovedFormItemId { get; set; }
        public Guid ApproverFormColumnId { get; set; }
        public Guid ReportFormId { get; set; }
        public int ValueType { get; set; }

        [NotMapped]
        public dynamic Value
        {
            get
            {
                switch (ValueType)
                {
                    case 0:
                        return JsonConvert.DeserializeObject<string>(ValueJson);
                    case 1:
                        return JsonConvert.DeserializeObject<int>(ValueJson);
                    case 2:
                        return JsonConvert.DeserializeObject<decimal>(ValueJson);
                    case 3:
                        return JsonConvert.DeserializeObject<string>(ValueJson);
                    case 4:
                        return JsonConvert.DeserializeObject<bool>(ValueJson);
                    case 5:
                        return JsonConvert.DeserializeObject<DateTime>(ValueJson).Date;
                    default:
                        throw new InvalidOperationException("Invalid ValueType");
                }
            }
            set
            {
                switch (ValueType)
                {
                    case 0:
                        ValueJson = JsonConvert.SerializeObject((string)value);
                        break;
                    case 1:
                        ValueJson = JsonConvert.SerializeObject((int)value);
                        break;
                    case 2:
                        ValueJson = JsonConvert.SerializeObject((decimal)value);
                        break;
                    case 3:
                        ValueJson = JsonConvert.SerializeObject((string)value);
                        break;
                    case 4:
                        ValueJson = JsonConvert.SerializeObject((bool)value);
                        break;
                    case 5:
                        ValueJson = JsonConvert.SerializeObject(((DateTime)value).Date);
                        break;

                    default:
                        throw new InvalidOperationException("Invalid ValueType");
                }
            }
        }
        public string ValueJson { get; set; } = string.Empty;
    }
}
