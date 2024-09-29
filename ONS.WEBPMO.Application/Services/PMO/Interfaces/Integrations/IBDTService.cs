using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces.Integrations
{
    
    public interface IBDTService 
    {
        /// <summary>
        /// Consulta UGEs sem parametro
        /// </summary>
        /// <returns>Lista de UGEs</returns>
        
        
        IList<UnidadeGeradora> ConsultarUnidadesGeradoras();

        /// <summary>
        /// Consulta de reservatorios
        /// Caso seja informado o parâmetro nomeExibicaoContem, será aplicado o filtro de LIKE (%nomeExibicaoContem%);
        /// Caso seja informado o parâmetro quantidadeMaxima, será retornado apenas a quantidade de registros informado;
        /// </summary>
        /// <param name="nomeExibicaoContem">Nome de exibição para filtrar</param>
        /// <returns>Lista de Reservatorios</returns>
        
        
        IList<Reservatorio> ConsultarReservatorioPorNomeExibicao(string nomeExibicaoContem = "");

        /// <summary>
        /// Consulta reservatorios onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Reservatorios</returns>
        
        
        IList<Reservatorio> ConsultarReservatorioPorChaves(params string[] chaves);

        /// <summary>
        /// Consulta usinas
        /// Caso seja informado o parâmetro nomeExibicaoContem, será aplicado o filtro de LIKE (%nomeExibicaoContem%)
        /// </summary>
        /// <param name="nomeExibicaoContem">Nome de exibição para filtrar</param>
        /// <returns>Lista de Usinas</returns>
        
        
        IList<Usina> ConsultarUsinaPorNomeExibicao(string nomeExibicaoContem = "");

        /// <summary>
        /// Consulta usinas onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Usinas</returns>
        
        
        IList<Usina> ConsultarUsinaPorChaves(params string[] chaves);

        /// <summary>
        /// Consulta usinas sem parametro
        /// </summary>
        /// <returns>Lista de Usinas</returns>
        
        
        IList<Usina> ConsultarUsinas();

        /// <summary>
        /// Consulta unidades geradoras onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Unidades Geradoras</returns>
        
        
        IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorChaves(params string[] chaves);

        /// <summary>
        /// Consulta unidades geradoras de uma Usina especifica
        /// </summary>
        /// <param name="chaveUsina">Nome de exibição para filtrar</param>
        /// <returns>Lista de Unidades Geradoras</returns>
        
        
        IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorUsina(string chaveUsina);

        /// <summary>
        /// Consulta todos os subsistemas na BDT
        /// </summary>
        /// <returns>Lista de subsistemas</returns>
        
        
        IList<Subsistema> ConsultarTodosSubsistemas();

        /// <summary>
        /// Consulta todos os subsistemas na BDT
        /// </summary>
        /// <returns>Lista de subsistemas</returns>
        
        
        IList<Subsistema> ConsultarSubsistemasAtivos();

        /// <summary>
        /// Consulta todos agentes que contém valor parcial (like) passado pelo parametro <paramref name="nomeParcial"/>
        /// </summary>
        /// <param name="nomeParcial">Valor parcial do nome do agente</param>
        /// <returns>Lista de Agentes</returns>
        
        
        IList<Agente> ConsultarAgentesPorNome(string nomeParcial, int top = int.MaxValue);


        /// <summary>
        /// Consulta todos agentes com as chaves informadas
        /// </summary>
        /// <param name="chaves">Chaves dos Agentes</param>
        /// <returns>Lista de Agentes</returns>
        
        
        IList<Agente> ConsultarAgentesPorChaves(params int[] chaves);

        /// <summary>
        /// Consulta todos os submercados
        /// </summary>
        /// <returns>Lista de Submercados</returns>
        
        
        IList<SubmercadoPMO> ConsultarSubmercados();

        /// <summary>
        /// Consulta todos os submercados
        /// </summary>
        /// <returns>Lista de Submercados</returns>
        
        
        IList<UsinaPEM> ConsultarDadosUsinasVisaoPEM();

        /// <summary>
        /// Consulta dados de todos os REEs ativos
        /// </summary>
        /// <returns>Lista de todos os REEs ativos</returns>
        
        
        IList<ReservatorioEE> ConsultarReservatoriosEquivalentesDeEnergiaAtivos();

    }
}
