using SER.PayuSdk.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk.Models.Response.Details
{
    public class ResTransaction
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("order")]
        public object Order { get; set; }

        [JsonPropertyName("creditCard")]
        public CreditCard CreditCard { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("parentTransactionId")]
        public object ParentTransactionId { get; set; }

        [JsonPropertyName("paymentMethod")]
        public string PaymentMethod { get; set; }

        [JsonPropertyName("source")]
        public object Source { get; set; }

        [JsonPropertyName("paymentCountry")]
        public string PaymentCountry { get; set; }

        [JsonPropertyName("transactionResponse")]
        public TransactionResponse TransactionResponse { get; set; }

        [JsonPropertyName("deviceSessionId")]
        public object DeviceSessionId { get; set; }

        [JsonPropertyName("ipAddress")]
        public string IpAddress { get; set; }

        [JsonPropertyName("cookie")]
        public string Cookie { get; set; }

        [JsonPropertyName("userAgent")]
        public string UserAgent { get; set; }

        [JsonPropertyName("expirationDate")]
        public object ExpirationDate { get; set; }

        [JsonPropertyName("payer")]
        public Payer Payer { get; set; }

        [JsonPropertyName("additionalValues")]
        public AdditionalValues AdditionalValues { get; set; }

        [JsonPropertyName("extraParameters")]
        public TransactionExtraParameters ExtraParameters { get; set; }
    }

    public partial class TransactionExtraParameters
    {
        [JsonPropertyName("RESPONSE_URL")]
        public string ResponseUrl { get; set; }

        [JsonPropertyName("INSTALLMENTS_NUMBER")]
        public string InstallmentsNumber { get; set; }
    }
}
