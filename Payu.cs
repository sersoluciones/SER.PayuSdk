using Microsoft.Extensions.Logging;
using SER.PayuSdk.Models.Request;
using SER.PayuSdk.Models.Request.Tokenization;
using SER.PayuSdk.Models.Response;
using SER.PayuSdk.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SER.PayuSdk
{
    public class Payu
    {

        private Consume _consume;

        public Payu(ILoggerFactory logger, string apiLogin, string apiKey, bool sandBox = false)
        {
            //_logger = logger.CreateLogger("Payu");
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
        public async Task<BaseResponse> GeneralTransaction(GeneralTransaction model)
        {
            return await _consume.ExecuteAsync<BaseResponse>(_consume.MakePostRequest(endPoint: Constants.PAYMENT_ENDPOINT, model: model));
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
    }
}
