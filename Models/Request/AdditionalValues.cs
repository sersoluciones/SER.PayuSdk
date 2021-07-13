using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk.Models.Request
{
    public class AdditionalValues
    {
        [JsonPropertyName("TX_VALUE")]
        public Tx TxValue { get; set; }

        [JsonPropertyName("TX_TAX")]
        public Tx TxTax { get; set; }

        [JsonPropertyName("TX_TAX_RETURN_BASE")]
        public Tx TxTaxReturnBase { get; set; }
    }

    public class Tx
    {
        [JsonPropertyName("value")]
        public decimal Value { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; } = "COP";
    }
}
