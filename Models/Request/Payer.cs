using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk.Models.Request
{
    public class Payer
    {
        [JsonPropertyName("merchantPayerId")]
        public string MerchantPayerId { get; set; }

        [Required]
        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [Required]
        [JsonPropertyName("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonPropertyName("contactPhone")]
        public string ContactPhone { get; set; }

        [JsonPropertyName("dniNumber")]
        public string DniNumber { get; set; }

        /// <summary>
        /// para credito codensa es obligatorio
        /// CC	Cédula de ciudadanía.
        /// CE Cédula de extranjería.
        /// NIT Número de Identificación Tributario.
        /// TI Tarjeta de Identidad.
        /// PP Pasaporte.
        /// IDC Identificador único de cliente, para el caso de ID’s únicos de clientes/usuarios de servicios públicos.
        /// CEL En caso de identificarse a través de la línea del móvil.
        /// RC Registro civil de nacimiento.
        /// DE Documento de identificación extranjero.
        /// </summary>
        [JsonPropertyName("dniType")]
        public string DniType { get; set; }

        [JsonPropertyName("billingAddress")]
        public IngAddress BillingAddress { get; set; }
    }
}
