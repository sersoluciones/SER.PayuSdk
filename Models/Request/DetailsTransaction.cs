using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk.Models.Request
{
    public class DetailsTransaction : BaseRequest
    {
        [JsonPropertyName("details")]
        public Details Details { get; set; }

        public new TypeRequest Type { get; set; } = TypeRequest.ORDER_DETAIL;
    }
}
