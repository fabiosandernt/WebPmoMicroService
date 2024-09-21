using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using ONS.Common.Services;
using ONS.Common.Wcf;
using ONS.SGIPMO.Domain.Entities;
using ONS.SGIPMO.Domain.Entities.DTO;
using ONS.SGIPMO.Domain.Entities.Filters;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    public interface IDadoColetaNaoEstruturadoService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        DadoColetaNaoEstruturado ObterPorChave(int chave);
    }
}
