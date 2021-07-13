using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk.Models.Request
{
    public class Buyer
    {
        [JsonPropertyName("merchantBuyerId")]
        public string MerchantBuyerId { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [JsonPropertyName("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonPropertyName("contactPhone")]
        public string ContactPhone { get; set; }

        [JsonPropertyName("dniNumber")]
        public string DniNumber { get; set; }

        [JsonPropertyName("shippingAddress")]
        public IngAddress ShippingAddress { get; set; }
    }
}
