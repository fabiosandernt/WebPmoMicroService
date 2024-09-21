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
    public interface IDadoColetaEstruturadoService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        DadoColetaEstruturado ObterPorChave(int chave);
    }
}
