using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk.Models.Request
{
    public class Details
    {
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }

        [JsonPropertyName("referenceCode")]
        public string ReferenceCode { get; set; }

        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; }
    }
}
