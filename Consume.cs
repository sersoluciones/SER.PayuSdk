using Microsoft.Extensions.Logging;
using RestSharp;
using SER.PayuSdk.Models.Request;
using SER.PayuSdk.Utils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SER.PayuSdk
{
    public class Consume
    {
        #region Atributes
        private string _baseUrl = Constants.BASE_URL;
        private readonly ILogger _logger;
        private readonly RestClient _client;
        private string _apiLogin = string.Empty;
        private string _apiKey = string.Empty;
        private bool _sandBox = false;

        #endregion

        public Consume(ILoggerFactory logger, string apiLogin, string apiKey, bool sandBox = false)
        {
            _apiKey = apiKey;
            _apiLogin = apiLogin;
            _baseUrl = !sandBox ? Constants.BASE_URL : Constants.BASE_URL_SANDBOX;
            _client = new RestClient(_baseUrl);
            _logger = logger.CreateLogger("Payu");
            _sandBox = sandBox;
        }

        public async Task<Out> MakePostClientRequest<Out, T>(string endPoint = "", T model = null) where T : BaseRequest where Out : class
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            model.Test = _sandBox;
            model.Merchant = new Merchant
            {
                ApiKey = _apiKey,
                ApiLogin = _apiLogin
            };
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            var jsonString = JsonSerializer.Serialize(model, options);

            _logger.LogInformation($"Request:\n{jsonString}");
            _logger.LogInformation(_baseUrl + endPoint);

            using var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add(
                "User-Agent",
                "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");

            var response = await client.PostAsJsonAsync(endPoint, model, options);
            try
            {
                _logger.LogInformation($" ------------- StatusCode {response.StatusCode}");

                var jsonSerializerOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };
                jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

                if (response.Content != null && (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created))
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    _logger.LogInformation($"Response:\n{content}");
                    return JsonSerializer.Deserialize<Out>(content, jsonSerializerOptions);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Response: {response?.Content?.ReadAsStringAsync()?.Result}");
                _logger.LogError($"Failed - {ex.Message}");

                var innerException = ex.InnerException;

                while (innerException != null)
                {
                    _logger.LogError($"Inner - {innerException.Message}");
                    innerException = innerException.InnerException;
                }


                return null;
            }
        }

        private HttpClientHandler GetHttpClientHandler()
        {
            var httpHandler = new HttpClientHandler
            {
                // Return `true` to allow certificates that are untrusted/invalid
                //ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
                ClientCertificateOptions = ClientCertificateOption.Manual,
                //specify to use TLS 1.2 as default connection
                SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12,
                ServerCertificateCustomValidationCallback = delegate { return true; },
                //ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate, chain, sslPolicyErrors) => true
            };

            var certificatePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "");

            _logger.LogInformation($"------------------ certificatePath: {certificatePath}");
            X509Certificate2 certificate = new(certificatePath + @"certificate.pfx", "Sersol0110");

            httpHandler.ClientCertificates.Add(certificate);
            return httpHandler;
        }

        public RestRequest MakePostRequest<T>(string endPoint = "", T model = null) where T : BaseRequest
        {
            _logger.LogInformation($"------------------ ENDPOINT: {endPoint}");
            var request = new RestRequest(endPoint, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            model.Test = _sandBox;
            model.Merchant = new Merchant
            {
                ApiKey = _apiKey,
                ApiLogin = _apiLogin
            };
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            var jsonString = JsonSerializer.Serialize(model, options);
            _logger.LogInformation($"Request:\n{jsonString}");
            request.AddJsonBody(jsonString);

            return request;
        }

        public async Task<T> ExecuteAsync<T>(RestRequest request) where T : class
        {
            _logger.LogInformation($"------------------ BASE URL: {_baseUrl}");
            var response = await _client.ExecuteAsync(request);

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                _logger.LogInformation(response.StatusCode + " ---- " + response.Content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return JsonSerializer.Deserialize<T>(response.Content, options);
                }
                return JsonSerializer.Deserialize<T>(response.Content, options);
            }
            catch (Exception e)
            {
                _logger.LogError($"Response: {response?.Content}");
                _logger.LogError(e.ToString());
                return null;
            }
        }



    }
}
