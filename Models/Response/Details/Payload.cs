using SER.PayuSdk.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk.Models.Response.Details
{
    public class ResponseDetails
    {
        [JsonPropertyName("payload")]
        public Payload Payload { get; set; }
    }

    public class Payload
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }

        [JsonPropertyName("status")]
        public TransactionState Status { get; set; }

        [JsonPropertyName("referenceCode")]
        public string ReferenceCode { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("airlineCode")]
        public object AirlineCode { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("notifyUrl")]
        public string NotifyUrl { get; set; }

        [JsonPropertyName("shippingAddress")]
        public ShippingAddress ShippingAddress { get; set; }

        [JsonPropertyName("buyer")]
        public Buyer Buyer { get; set; }

        [JsonPropertyName("transactions")]
        public ResTransaction[] Transactions { get; set; }

        [JsonPropertyName("additionalValues")]
        public AdditionalValues AdditionalValues { get; set; }



        [JsonPropertyName("state")]
        public TransactionState State { get; set; }

        [JsonPropertyName("paymentNetworkResponseCode")]
        public object PaymentNetworkResponseCode { get; set; }

        [JsonPropertyName("paymentNetworkResponseErrorMessage")]
        public object PaymentNetworkResponseErrorMessage { get; set; }

        [JsonPropertyName("trazabilityCode")]
        public object TrazabilityCode { get; set; }

        [JsonPropertyName("authorizationCode")]
        public object AuthorizationCode { get; set; }

        [JsonPropertyName("pendingReason")]
        public object PendingReason { get; set; }

        [JsonPropertyName("responseCode")]
        public string ResponseCode { get; set; }

        [JsonPropertyName("errorCode")]
        public object ErrorCode { get; set; }

        [JsonPropertyName("responseMessage")]
        public object ResponseMessage { get; set; }

        [JsonPropertyName("transactionDate")]
        public object TransactionDate { get; set; }

        [JsonPropertyName("transactionTime")]
        public object TransactionTime { get; set; }

        [JsonPropertyName("operationDate")]
        public object OperationDate { get; set; }

        [JsonPropertyName("extraParameters")]
        public ExtraParameters ExtraParameters { get; set; }
    }

    public class ShippingAddress
    {
        [JsonPropertyName("street1")]
        public string Street1 { get; set; }

        [JsonPropertyName("street2")]
        public string Street2 { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }
    }
}
