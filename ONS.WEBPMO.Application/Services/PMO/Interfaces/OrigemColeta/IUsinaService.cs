using ONS.Common.Services;
using ONS.Common.Wcf;
using ONS.SGIPMO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta
{
    [ServiceContract]
    public interface IUsinaService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        Usina ObterUsinaPorChave(int chave);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Usina> ConsultarUsinasPorChaves(params int[] chaves);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Usina> ConsultarUsinasPorNome(string nome);
    }
}
