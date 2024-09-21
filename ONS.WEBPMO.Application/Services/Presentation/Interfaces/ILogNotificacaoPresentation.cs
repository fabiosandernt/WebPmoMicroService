using System.Collections.Generic;
using System.ServiceModel;
using ONS.Common.Services;
using ONS.Common.Wcf;
using ONS.SGIPMO.Domain.Entities;
using ONS.SGIPMO.Domain.Entities.DTO;

namespace ONS.WEBPMO.Domain.Presentations
{
    [ServiceContract]
    public interface ILogNotificacaoPresentation : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        LogNotificacaoDTO ObterDadosPesquisaLogNotificacao(int? idSemanaOperativa = null, bool isMonitorar = false, bool? ordernarListagens = true);
    }
}
