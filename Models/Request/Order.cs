using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk.Models.Request
{
    public class Order
    {
        /// <summary>
        /// accountId de la cuenta Payu
        /// </summary>
        [Required]
        [JsonPropertyName("accountId")]
        public string AccountId { get; set; }

        [JsonPropertyName("referenceCode")]
        public string ReferenceCode { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; } = "es";

        [JsonPropertyName("signature")]
        public string Signature { get; set; }

        [JsonPropertyName("notifyUrl")]
        public string NotifyUrl { get; set; }

        [JsonPropertyName("additionalValues")]
        public AdditionalValues AdditionalValues { get; set; }

        [JsonPropertyName("buyer")]
        public Buyer Buyer { get; set; }

        [JsonPropertyName("shippingAddress")]
        public IngAddress ShippingAddress { get; set; }
    }
}
