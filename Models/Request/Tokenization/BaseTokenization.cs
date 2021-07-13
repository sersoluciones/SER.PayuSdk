using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk.Models.Request.Tokenization
{
    public class BaseTokenization : BaseRequest
    {
        public new TypeRequest Type { get; set; } = TypeRequest.CREATE_TOKEN;

        [JsonPropertyName("creditCardToken")]
        public CreditCardToken CreditCardToken { get; set; }
    }
}
