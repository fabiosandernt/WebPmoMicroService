using ONS.WEBPMO.Domain.DTO;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Presentations
{
    
    public interface IColetaInsumoPresentation 
    {
        
        //[UseNetDataContractSerializer("SemanasOperativas", "Agentes", "Insumos", "SituacoesColeta")]
        DadosPesquisaColetaInsumoDTO ObterDadosPesquisaColetaInsumo(
            int? idSemanaOperativa = null, bool isMonitorar = false, bool? ordernarListagens = true);

        
        
        DadosInclusaoDadoColetaManutencaoDTO ObterDadosInclusaoDadoColetaManutencao(int idColetaInsumo);

        
        
        DadosAlteracaoDadoColetaManutencaoDTO ObterDadosAlteracaoDadoColetaManutencao(int idDadoColeta);

        
        //[UseNetDataContractSerializer("ColetaInsumo", "UnidadeGeradora", "UnidadeGeradora.Usina")]
        IList<DadoColetaManutencao> ConsultarManutencaoSGI(int idColetaInsumo);

        
        
        DadosPesquisaGeracaoBlocosDTO ObterDadosPesquisaGeracaoBloco(int idSemanaOperativa);

        
        
        void ImportarCronogramaManutencaoHidraulicaTermica(int idSemanaOperativa, IList<int> idsInsumo);
    }
}
