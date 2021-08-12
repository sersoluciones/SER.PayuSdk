using Microsoft.Extensions.Logging;
using RestSharp;
using SER.PayuSdk.Models.Request;
using SER.PayuSdk.Utils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
                //_logger.LogError($"Response: {response.Content}");
                _logger.LogError(e.ToString());
                throw;
            }
        }



    }
}
