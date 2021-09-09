using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SER.PayuSdk.Models.Request;
using SER.PayuSdk.Models.Request.Tokenization;
using SER.PayuSdk.Models.Response;
using SER.PayuSdk.Models.Response.Details;
using SER.PayuSdk.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SER.PayuSdk
{
    public class Payu
    {
        #region Atributes
        private Consume _consume;
        private string _apiKey = string.Empty;
        private string _merchantId = string.Empty;
        private string _accountId = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;

        #endregion

        public Payu(ILoggerFactory logger, IHttpContextAccessor contextAccessor, string apiLogin = "", string apiKey = "", string merchantId = "",
            string accountId = "", bool sandBox = false)
        {
            _logger = logger.CreateLogger("Payu");
            _apiKey = apiKey;
            _merchantId = merchantId;
            _accountId = accountId;
            _httpContextAccessor = contextAccessor;
            _consume = new Consume(logger, apiLogin, apiKey, sandBox: sandBox);
        }

        /// <summary>
        /// Ping
        /// </summary>
        /// <returns></returns>
        public async Task<BaseResponse> Ping()
        {
            return await _consume.MakePostClientRequest<BaseResponse, BaseRequest>(endPoint: Constants.PAYMENT_ENDPOINT, model: new BaseRequest
            {
                Type = TypeRequest.PING
            });
        }


        /// <summary>
        /// Transaccion para tarjetas de credito y debito / PSE / Efectivo
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseResponse> GeneralTransaction(Transaction model)
        {
            var ping = await Ping();
            if (ping == null || ping.Code != "SUCCESS") return null;
            var transaction = new GeneralTransaction
            {
                Transaction = model,
                Type = TypeRequest.SUBMIT_TRANSACTION
            };
            transaction.Transaction.IpAddress = string.Format("{0}", _httpContextAccessor.HttpContext.Connection.RemoteIpAddress);
            transaction.Transaction.UserAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"];
            //transaction.Transaction.DeviceSessionId = GetDeviceSessionId(transaction.Transaction.UserAgent);
            transaction.Transaction.Order.AccountId = _accountId;
            // transaction.Transaction.Order.Signature = GenerateSignature(transaction.Transaction.Order, _apiKey, _merchantId);
            return await _consume.MakePostClientRequest<BaseResponse, GeneralTransaction>(endPoint: Constants.PAYMENT_ENDPOINT, model: transaction);
        }

        /// <summary>
        /// Tokenizacion de tarjeta
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseResponse> CreateTokenCard(BaseTokenization model)
        {
            return await _consume.MakePostClientRequest<BaseResponse, BaseTokenization>(endPoint: Constants.PAYMENT_ENDPOINT, model: model);
        }


        /// <summary>
        /// Obtener la lista de instituciones financieras
        /// </summary>
        /// <returns></returns>
        public async Task<BaseResponse> GetPSEFinancialInstitutions()
        {
            return await _consume.MakePostClientRequest<BaseResponse, BaseRequest>(endPoint: Constants.PAYMENT_ENDPOINT, model: new BaseRequest
            {
                Type = TypeRequest.GET_PAYMENT_METHODS
            });
        }

        /// <summary>
        /// Consultar el estado de cualquier transaccion
        /// </summary>
        /// <param name="idTransaction"></param>
        /// <returns></returns>
        public async Task<BaseResponse> CheckTransaction(long orderId)
        {
            return await _consume.MakePostClientRequest<BaseResponse, DetailsTransaction>(endPoint: Constants.CONSULT_ENDPOINT, model: new DetailsTransaction
            {
                Type = TypeRequest.ORDER_DETAIL,
                Details = new Details
                {
                    OrderId = orderId
                }
            });
        }

        /// <summary>
        /// Consultar el estado de cualquier transaccion con la referencia
        /// </summary>
        /// <param name="referenceCode"></param>
        /// <returns></returns>
        public async Task<BaseResponse> CheckTransaction(string referenceCode)
        {
            return await _consume.MakePostClientRequest<BaseResponse, DetailsTransaction>(endPoint: Constants.CONSULT_ENDPOINT, model: new DetailsTransaction
            {
                Type = TypeRequest.ORDER_DETAIL_BY_REFERENCE_CODE,
                Details = new Details
                {
                    ReferenceCode = referenceCode
                }
            });
        }


        /// <summary>
        /// Generar firma para cada transaccion
        /// </summary>
        /// <returns></returns>
        public static string GenerateSignature(Payload payload, string apiKey, string merchantId, string value, string state)
        {
            var valueToEvaluate = new string[] { apiKey, merchantId, payload.ReferenceCode,
                value, payload.AdditionalValues.TxValue.Currency, state };
            var toEvaluate = string.Join("~", valueToEvaluate);
            //Console.WriteLine($"-------------- toEvaluate {toEvaluate}");
            return ComputeMd5Hash(toEvaluate);
        }

        private static string ComputeMd5Hash(string rawData)
        {
            var md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(Encoding.ASCII.GetBytes(rawData));

            //get hash result after compute it  
            byte[] bytes = md5.Hash;

            // Convert byte array to a string   
            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }


        private string GetDeviceSessionId(string userAgent)
        {
            var os = new ClientOS(userAgent);
            return ComputeMd5Hash(string.Format("{0}{1}{2}", os.Name, os.Version, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds().ToString()));
        }
    }
}
