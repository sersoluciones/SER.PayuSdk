using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk.Models.Request
{
    public class ThreeDomainSecure
    {
        [JsonPropertyName("embedded")]
        public bool Embedded { get; set; }

        [JsonPropertyName("eci")]
        public string Eci { get; set; }

        [JsonPropertyName("cavv")]
        public string Cavv { get; set; }

        [JsonPropertyName("xid")]
        public string Xid { get; set; }

        [JsonPropertyName("directoryServerTransactionId")]
        public string DirectoryServerTransactionId { get; set; }
    }
}
