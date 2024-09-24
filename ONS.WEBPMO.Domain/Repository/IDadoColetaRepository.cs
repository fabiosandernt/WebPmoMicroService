
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository
{
    public interface IDadoColetaRepository : IRepository<DadoColeta>
    {
        void DeletarPorIdGabarito(IList<int> idsGabarito);

        /// <summary>
        /// [UC1003] Consulta todos os DadosColeta (Estruturados e Manutenção) que participam da montagem do arquivo para o montador
        /// </summary>
        /// <param name="idSemanaOperativa">Identificador da semana operativa</param>
        /// <returns>Lista de DadosColeta que participam da geração de blocos</returns>
        IList<DadoColeta> ConsutarParaGeracaoBlocos(int idSemanaOperativa);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idSemanaOperativa"></param>
        /// <returns></returns>
        IList<DadoColeta> ConsultarDadosComInsumoParticipaBlocoUH(int idSemanaOperativa);
    }
}
