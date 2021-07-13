using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk.Models.Request
{
    public class GeneralTransaction : BaseRequest
    {
        /// <summary>
        /// OPTIONS:
        /// bankListInformation
        /// transaction
        /// null
        /// </summary>
        [JsonPropertyName("transaction")]
        public Transaction Transaction { get; set; }

        public new TypeRequest Type { get; set; } = TypeRequest.SUBMIT_TRANSACTION;
    }
}
