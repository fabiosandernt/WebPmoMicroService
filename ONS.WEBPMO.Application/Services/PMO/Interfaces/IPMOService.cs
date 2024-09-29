using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Domain.Entities.Filters;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    
    public interface IPMOService 
    {
        /// <summary>
        /// Obtém o PMO pela chave.
        /// </summary>
        /// <param name="chave">Chave do PMO.</param>
        /// <returns>ONS.WEBPMO.Domain.Entities.PMO.PMO</returns>

        //[UseNetDataContractSerializer("SemanasOperativas.Situacao")]
        ONS.WEBPMO.Domain.Entities.PMO.PMO ObterPMOPorChave(int chave);

        /// <summary>
        /// Gera o PMO para o ano e mês indicado.
        /// </summary>
        /// <param name="ano">Ano do PMO.</param>
        /// <param name="mes">Mês do PMO.</param>
        /// <returns>PMO</returns>
        
        //[UseNetDataContractSerializer("SemanasOperativas.Situacao")]
        
        ONS.WEBPMO.Domain.Entities.PMO.PMO GerarPMO(int ano, int mes);

        /// <summary>
        /// Obtém o PMO de acordo com o filtro passado.
        /// </summary>
        /// <param name="filtro">Filtro para consulta do PMO></param>
        /// <returns>PMO.</returns>

        //[UseNetDataContractSerializer("SemanasOperativas.Situacao")]
        ONS.WEBPMO.Domain.Entities.PMO.PMO ObterPMOPorFiltro(PMOFilter filtro);

        /// <summary>
        /// Obtém o PMO de acordo com o filtro passado por uma chamada externa.
        /// </summary>
        /// <param name="filtro">Filtro para consulta do PMO></param>
        /// <returns>PMO.</returns>

        //[UseNetDataContractSerializer("SemanasOperativas.Situacao")]
        ONS.WEBPMO.Domain.Entities.PMO.PMO ObterPMOPorFiltroExterno(PMOFilter filtro);

        /// <summary>
        /// Incluir SemanaOperativa no PMO.
        /// </summary>
        /// <param name="dto">Propriedades: "IdPMO" indica o Id do PMO; "IsInicioPMO" indica se a SemanaOperativa vai ser incluída no início do PMO.</param>
        
        
        void IncluirSemanaOperativa(InclusaoSemanaOperativaDTO dto);

        /// <summary>
        /// Excluir SemanaOperativa do PMO.
        /// </summary>
        /// <param name="idPMO">Id do PMO.</param>
        /// <param name="versaoPMO">Identificação da versão do PMO.</param>
        
        
        void ExcluirUltimaSemanaOperativa(int idPMO, byte[] versaoPMO);

        /// <summary>
        /// Atualiza a quantidade de meses a frente para estudo.
        /// </summary>
        /// <param name="idPMO">Id do PMO.</param>
        /// <param name="mesesAdiante">Meses adiante.</param>
        /// <param name="versao">Versão para controle de concorrência.</param>
        
        
        void AtualizarMesesAdiantePMO(int idPMO, int? mesesAdiante, byte[] versao);

        /// <summary>
        /// Exclui o PMO
        /// </summary>
        /// <param name="idPMO">Id do PMO.</param>
        
        
        void ExcluirPMO(DadosPMODTO dto);
    }
}
