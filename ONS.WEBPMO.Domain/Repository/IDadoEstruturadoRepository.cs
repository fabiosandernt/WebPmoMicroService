
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository
{
    public interface IDadoColetaEstruturadoRepository : IRepository<DadoColetaEstruturado>
    {
        IList<DadoColetaEstruturado> ConsultarPorFiltro(DadoColetaInsumoFilter filter);
        IList<DadoColetaEstruturado> ConsultarDadosComInsumoEGrandezaParticipaBlocoGNL(int idSemanaOperativa);
        IList<DadoColetaEstruturado> ConsultarDadosComInsumoEGrandezaParticipaBloco(int idSemanaOperativa);

        void DeletarPorIdGabarito(IList<int> idsGabarito);

        int ContarQuantidadeLinhasDadosEstruturados(int idColetaInsumo);
    }
}
