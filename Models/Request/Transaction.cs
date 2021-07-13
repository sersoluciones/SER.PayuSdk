using SER.PayuSdk.Models.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk.Models.Request
{
    public enum PaymentMethod
    {
        PSE,

        // TC
        VISA,
        DINERS,
        MASTERCARD,
        AMEX,

        // EFECTIVO
        BALOTO,
        EFECTY,
        OTHERS_CASH
    }

    public enum TransactionType
    {
        AUTHORIZATION_AND_CAPTURE
    }

    public class Transaction
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("order")]
        public Order Order { get; set; }

        [JsonPropertyName("payer")]
        public Payer Payer { get; set; }

        [JsonPropertyName("creditCardTokenId")]
        public string CreditCardTokenId { get; set; }

        [JsonPropertyName("creditCard")]
        public CreditCard CreditCard { get; set; }

        [JsonPropertyName("extraParameters")]
        public ExtraParameters ExtraParameters { get; set; }

        [JsonPropertyName("type")]
        public string Type { get => TransactionType.ToString(); }

        [JsonIgnore]
        [Required]
        public TransactionType TransactionType { get; set; }

        [JsonPropertyName("paymentMethod")]
        public string PaymentMethod { get => PaymentMethodEnum.ToString(); }

        [JsonIgnore]
        [Required]
        public PaymentMethod PaymentMethodEnum { get; set; }

        [JsonPropertyName("paymentCountry")]
        public string PaymentCountry { get; set; } = "CO";

        /// <summary>]
        /// Obligatorio para pagos en efectivo
        /// Ej.: "2017-05-10T00:00:00"
        /// </summary>
        [JsonPropertyName("expirationDate")]
        public string ExpirationDate { get; set; }


        [JsonPropertyName("ipAddress")]
        public string IpAddress { get; set; }

        [JsonPropertyName("transactionResponse")]
        public TransactionResponse TransactionResponse { get; set; }

        //[JsonPropertyName("deviceSessionId")]
        //public string DeviceSessionId { get; set; }

        //[JsonPropertyName("cookie")]
        //public string Cookie { get; set; }

        //[JsonPropertyName("userAgent")]
        //public string UserAgent { get; set; }

        //[JsonPropertyName("threeDomainSecure")]
        //public ThreeDomainSecure ThreeDomainSecure { get; set; }
    }
}
