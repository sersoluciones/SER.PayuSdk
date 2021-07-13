using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk.Models.Request
{
    public class CreditCard
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("securityCode")]
        public string SecurityCode { get; set; }

        [JsonPropertyName("expirationDate")]
        public string ExpirationDate { get; set; }

        [JsonPropertyName("issuerBank")]
        public string IssuerBank { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("processWithoutCvv2")]
        public string ProcessWithoutCvv2 { get; set; } = "false";
    }
}
