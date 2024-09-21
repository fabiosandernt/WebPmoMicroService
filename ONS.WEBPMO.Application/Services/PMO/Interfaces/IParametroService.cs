using ONS.Common.Services;
using ONS.Common.Wcf;
using ONS.SGIPMO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    public interface IParametroService : IService
    {
        /// <summary>
        /// Obtém o parâmetro pelo nome.
        /// </summary>
        /// <param name="paramento">Nome do parâmetro</param>
        /// <returns>Parametro</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        Parametro ObterParametro(ParametroEnum paramento);
    }
}
