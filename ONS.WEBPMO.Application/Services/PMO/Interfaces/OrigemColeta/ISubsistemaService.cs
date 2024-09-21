using ONS.Common.Services;
using ONS.Common.Wcf;
using ONS.SGIPMO.Domain.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta
{
    [ServiceContract]
    public interface ISubsistemaService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        Subsistema ObterSubsistemaPorChave(int chave);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Subsistema> ConsultarSubsistemasPorChaves(params int[] chaves);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Subsistema> ConsultarSubsistemasPorNome(string nome);
    }
}
