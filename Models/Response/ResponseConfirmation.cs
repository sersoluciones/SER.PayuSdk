using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk.Models.Response
{
    public class ResponseConfirmation
    {
        [JsonPropertyName("transaction_id")]
        public string TransactionId { get; set; }

        [JsonPropertyName("state_pol")]
        public string StatePol { get; set; }

        [JsonPropertyName("response_message_pol")]
        public string ResponseMessagePol { get; set; }

        [JsonPropertyName("reference_sale")]
        public string ReferenceSale { get; set; }

        [JsonPropertyName("response_code_pol")]
        public string ResponseCodePol { get; set; }

        [JsonPropertyName("merchant_id")]
        public long MerchantId { get; set; } 
        
        [JsonPropertyName("payment_method_name")]
        public string PaymentMethodName { get; set; }

        [JsonPropertyName("payment_method_type")]
        public int PaymentMethodType { get; set; }

        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("sign")]
        public string Sign { get; set; }

    }
}
