using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk.Models.Request.Tokenization
{
    public class CreditCardToken
    {
        [JsonPropertyName("payerId")]
        public string PayerId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("identificationNumber")]
        public string IdentificationNumber { get; set; }

        [JsonPropertyName("paymentMethod")]
        public string PaymentMethod { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("expirationDate")]
        public string ExpirationDate { get; set; }



        [JsonPropertyName("creditCardTokenId")]
        public string CreditCardTokenId { get; set; }

        [JsonPropertyName("creationDate")]
        public string CreationDate { get; set; }

        [JsonPropertyName("maskedNumber")]
        public string MaskedNumber { get; set; }

        [JsonPropertyName("errorDescription")]
        public string ErrorDescription { get; set; }
    }
}
