using SER.PayuSdk.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk.Models.Request
{
    public enum TypeRequest
    {
        GET_PAYMENT_METHODS,
        SUBMIT_TRANSACTION,
        PING,
        CREATE_TOKEN,
        ORDER_DETAIL,
        ORDER_DETAIL_BY_REFERENCE_CODE,
        TRANSACTION_RESPONSE_DETAIL
    }

    public class BaseRequest
    {
        [JsonPropertyName("language")]
        public string Language { get; set; } = "es";

        [JsonIgnore]
        public TypeRequest Type { get; set; }

        [JsonPropertyName("command")]
        public string Command { get => Type.ToString(); }

        [Required]
        [JsonPropertyName("merchant")]
        public Merchant Merchant { get; set; }      


        [JsonPropertyName("test")]
        public bool Test { get; set; } = false;
    }
}
