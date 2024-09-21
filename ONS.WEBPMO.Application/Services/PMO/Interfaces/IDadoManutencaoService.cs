using ONS.Common.Services;
using ONS.Common.Util.Pagination;
using ONS.Common.Wcf;
using ONS.SGIPMO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using ONS.SGIPMO.Domain.Entities.Filters;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    public interface IDadoColetaManutencaoService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer("ColetaInsumo")]
        DadoColetaManutencao ObterPorChave(int chave);

        [OperationContract]
        [UseNetDataContractSerializer]
        PagedResult<DadoColetaManutencaoDTO> ConsultarDadoColetaManutencaoPorColetaInsumo(
            DadoColetaInsumoFilter filter);


        [OperationContract]
        [UseNetDataContractSerializer("ColetaInsumo")]
        DadoColetaManutencao ObterPorColetaInsumoId(int idColetaInsumo);

        void IncluirDadoColeta(DadoColetaManutencao dadoColeta);
        void Excluir(DadoColetaManutencao dadoColeta);
        void AlterarDadoColeta(DadoColetaManutencao dadoColeta);
        void IncluirDadoColetaSeNaoExiste(IList<DadoColetaManutencao> dadoColetaList);
    }
}
