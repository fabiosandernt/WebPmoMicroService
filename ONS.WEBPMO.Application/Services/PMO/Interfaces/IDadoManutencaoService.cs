
using ONS.Infra.Core.Pagination;
using ONS.WEBPMO.Domain.DTO;
using ONS.WEBPMO.Domain.Entities.Base;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    //[ServiceContract]
    public interface IDadoColetaManutencaoService 
    {
        
        //[UseNetDataContractSerializer("ColetaInsumo")]
        DadoColetaManutencao ObterPorChave(int chave);

        
        
        PagedResult<DadoColetaManutencaoDTO> ConsultarDadoColetaManutencaoPorColetaInsumo(
            DadoColetaInsumoFilter filter);


        
        //[UseNetDataContractSerializer("ColetaInsumo")]
        DadoColetaManutencao ObterPorColetaInsumoId(int idColetaInsumo);

        void IncluirDadoColeta(DadoColetaManutencao dadoColeta);
        void Excluir(DadoColetaManutencao dadoColeta);
        void AlterarDadoColeta(DadoColetaManutencao dadoColeta);
        void IncluirDadoColetaSeNaoExiste(IList<DadoColetaManutencao> dadoColetaList);
    }
}
