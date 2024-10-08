
using ONS.WEBPMO.Domain.DTO;

namespace ONS.WEBPMO.Domain.Presentations
{
    
    public interface IInsumoPresentation  
    {
        /// <summary>
        /// Obter os dados para inclusão de um insumo estruturado
        /// </summary>
        /// <param name="idInsumo"></param>
        /// <returns></returns>
        
        
        DadosManutencaoInsumoEstruturado ObterDadosManutencaoInsumoEstruturado(int? idInsumo);

        /// <summary>
        /// Obter dados para a consulta de um insumo, tela inicial do insumo
        /// </summary>
        /// <returns></returns>
        
        
        DadosInsumoConsultaDTO ObterDadosInsumoConsulta();
    }
}
