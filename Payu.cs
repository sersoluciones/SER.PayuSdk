using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SER.PayuSdk.Models.Request;
using SER.PayuSdk.Models.Request.Tokenization;
using SER.PayuSdk.Models.Response;
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

        #endregion

        public Payu(ILoggerFactory logger, IHttpContextAccessor contextAccessor, string apiLogin, string apiKey, string merchantId, string accountId, bool sandBox = false)
        {
            //_logger = logger.CreateLogger("Payu");
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
            return await _consume.ExecuteAsync<BaseResponse>(_consume.MakePostRequest(endPoint: Constants.PAYMENT_ENDPOINT, model: new BaseRequest
            {
                Type = TypeRequest.PING
            }));
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
                Transaction = model
            };
            transaction.Transaction.IpAddress = string.Format("{0}", _httpContextAccessor.HttpContext.Connection.RemoteIpAddress);
            transaction.Transaction.UserAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"];
            //transaction.Transaction.DeviceSessionId = GetDeviceSessionId(transaction.Transaction.UserAgent);
            transaction.Transaction.Order.AccountId = _accountId;
            transaction.Transaction.Order.Signature = GenerateSignature(transaction.Transaction.Order, _apiKey, _merchantId);
            return await _consume.ExecuteAsync<BaseResponse>(_consume.MakePostRequest(endPoint: Constants.PAYMENT_ENDPOINT, model: transaction));
        }

        /// <summary>
        /// Tokenizacion de tarjeta
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseResponse> CreateTokenCard(BaseTokenization model)
        {
            return await _consume.ExecuteAsync<BaseResponse>(_consume.MakePostRequest(endPoint: Constants.PAYMENT_ENDPOINT, model: model));
        }


        /// <summary>
        /// Obtener la lista de instituciones financieras
        /// </summary>
        /// <returns></returns>
        public async Task<BaseResponse> GetPSEFinancialInstitutions()
        {
            return await _consume.ExecuteAsync<BaseResponse>(_consume.MakePostRequest(endPoint: Constants.PAYMENT_ENDPOINT, model: new BaseRequest
            {
                Type = TypeRequest.GET_PAYMENT_METHODS
            }));
        }

        /// <summary>
        /// Consultar el estado de cualquier transaccion
        /// </summary>
        /// <param name="idTransaction"></param>
        /// <returns></returns>
        public async Task<BaseResponse> CheckTransaction(long orderId)
        {
            return await _consume.ExecuteAsync<BaseResponse>(_consume.MakePostRequest(endPoint: Constants.CONSULT_ENDPOINT, model: new DetailsTransaction
            {
                Type = TypeRequest.ORDER_DETAIL,
                Details = new Details
                {
                    OrderId = orderId
                }
            }));
        }

        /// <summary>
        /// Consultar el estado de cualquier transaccion con la referencia
        /// </summary>
        /// <param name="referenceCode"></param>
        /// <returns></returns>
        public async Task<BaseResponse> CheckTransaction(string referenceCode)
        {
            return await _consume.ExecuteAsync<BaseResponse>(_consume.MakePostRequest(endPoint: Constants.CONSULT_ENDPOINT, model: new DetailsTransaction
            {
                Type = TypeRequest.ORDER_DETAIL_BY_REFERENCE_CODE,
                Details = new Details
                {
                    ReferenceCode = referenceCode
                }
            }));
        }


        /// <summary>
        /// Generar firma para cada transaccion
        /// </summary>
        /// <returns></returns>
        public static string GenerateSignature(Order order, string apiKey, string merchantId)
        {
            var valueToEvaluate = new string[] { apiKey, merchantId, order.ReferenceCode, 
                Math.Round(order.AdditionalValues.TxValue.Value, 2).ToString(),
                order.AdditionalValues.TxValue.Currency };
            return ComputeSha256Hash(string.Join("~", valueToEvaluate));
        }

        private static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using SHA256 sha256Hash = SHA256.Create();
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

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
            return ComputeSha256Hash(string.Format("{0}{1}{2}", os.Name, os.Version, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds().ToString()));
        }
    }
}
