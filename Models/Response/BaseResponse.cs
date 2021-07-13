using SER.PayuSdk.Models.Request.Tokenization;
using SER.PayuSdk.Models.Response.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk.Models.Response
{
    public class BaseResponse
    {
        /// <summary>
        /// ERROR
        /// SUCCESS
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("error")]
        public object Error { get; set; }

        [JsonPropertyName("transactionResponse")]
        public TransactionResponse TransactionResponse { get; set; }

        [JsonPropertyName("paymentMethods")]
        public PaymentMethods PaymentMethods { get; set; }

        [JsonPropertyName("creditCardToken")]
        public CreditCardToken CreditCardToken { get; set; }

        [JsonPropertyName("result")]
        public ResponseDetails ResponseDetails { get; set; }
    }

    public class PaymentMethods
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }
    }
}
