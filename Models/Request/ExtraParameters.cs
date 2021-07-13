using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk.Models.Request
{
    public class ExtraParameters
    {
        [JsonPropertyName("INSTALLMENTS_NUMBER")]
        public int InstallmentsNumber { get; set; }

        [JsonPropertyName("RESPONSE_URL")]
        public string ResponseUrl { get; set; }

        [JsonPropertyName("PSE_REFERENCE1")]
        public string PseReference1 { get; set; }

        [JsonPropertyName("FINANCIAL_INSTITUTION_CODE")]
        public string FinancialInstitutionCode { get; set; }

        [JsonPropertyName("USER_TYPE")]
        public string UserType { get; set; }

        [JsonPropertyName("PSE_REFERENCE2")]
        public string PseReference2 { get; set; }

        [JsonPropertyName("PSE_REFERENCE3")]
        public string PseReference3 { get; set; }
    }
}
