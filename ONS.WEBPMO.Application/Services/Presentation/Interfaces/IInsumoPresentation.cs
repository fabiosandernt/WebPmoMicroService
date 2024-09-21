using System.Collections.Generic;
using System.ServiceModel;
using ONS.Common.Services;
using ONS.Common.Wcf;
using ONS.SGIPMO.Domain.Entities;
using ONS.SGIPMO.Domain.Entities.DTO;

namespace ONS.WEBPMO.Domain.Presentations
{
    [ServiceContract]
    public interface IInsumoPresentation : IService
    {
        /// <summary>
        /// Obter os dados para inclusão de um insumo estruturado
        /// </summary>
        /// <param name="idInsumo"></param>
        /// <returns></returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        DadosManutencaoInsumoEstruturado ObterDadosManutencaoInsumoEstruturado(int? idInsumo);

        /// <summary>
        /// Obter dados para a consulta de um insumo, tela inicial do insumo
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        DadosInsumoConsultaDTO ObterDadosInsumoConsulta();
    }
}
