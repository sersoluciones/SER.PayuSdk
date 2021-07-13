﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk.Models.Response
{
    public class TransactionResponse
    {
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }

        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; }

        /// <summary>
        /// PENDING
        /// DECLINED
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("paymentNetworkResponseCode")]
        public string PaymentNetworkResponseCode { get; set; }

        [JsonPropertyName("paymentNetworkResponseErrorMessage")]
        public string PaymentNetworkResponseErrorMessage { get; set; }

        [JsonPropertyName("trazabilityCode")]
        public string TrazabilityCode { get; set; }

        [JsonPropertyName("authorizationCode")]
        public string AuthorizationCode { get; set; }

        [JsonPropertyName("pendingReason")]
        public string PendingReason { get; set; }

        [JsonPropertyName("responseCode")]
        public string ResponseCode { get; set; }

        [JsonPropertyName("errorCode")]
        public string ErrorCode { get; set; }

        [JsonPropertyName("responseMessage")]
        public string ResponseMessage { get; set; }

        [JsonPropertyName("transactionDate")]
        public string TransactionDate { get; set; }

        [JsonPropertyName("transactionTime")]
        public string TransactionTime { get; set; }

        [JsonPropertyName("operationDate")]
        public string OperationDate { get; set; }

        [JsonPropertyName("referenceQuestionnaire")]
        public object ReferenceQuestionnaire { get; set; }

        [JsonPropertyName("extraParameters")]
        public ExtraParameters ExtraParameters { get; set; }
    }

    public partial class ExtraParameters
    {
        [JsonPropertyName("BANK_URL")]
        public string BankUrl { get; set; }


        /**
         * EFECTIVO
         */
        [JsonPropertyName("EXPIRATION_DATE")]
        public long ExpirationDate { get; set; }

        [JsonPropertyName("URL_PAYMENT_RECEIPT_PDF")]
        public string UrlPaymentReceiptPdf { get; set; }

        [JsonPropertyName("REFERENCE")]
        public long Reference { get; set; }

        [JsonPropertyName("CHECKOUT_VERSION")]
        public string CheckoutVersion { get; set; }

        [JsonPropertyName("URL_PAYMENT_RECEIPT_HTML")]
        public string UrlPaymentReceiptHtml { get; set; }


        /**
         * RESPONSE DETAILS
         */
        [JsonPropertyName("travelAgencyAuthorizationCode")]
        public string TravelAgencyAuthorizationCode { get; set; }
    }

}
