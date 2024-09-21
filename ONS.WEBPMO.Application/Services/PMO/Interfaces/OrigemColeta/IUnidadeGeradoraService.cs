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
    public interface IUnidadeGeradoraService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        UnidadeGeradora ObterUnidadeGeradoraPorChave(int chave);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<UnidadeGeradora> ConsultarUnidadesGeradorasPorChaves(params int[] chaves);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<UnidadeGeradora> ConsultarUnidadesGeradorasPorNome(string nome);
    }
}
