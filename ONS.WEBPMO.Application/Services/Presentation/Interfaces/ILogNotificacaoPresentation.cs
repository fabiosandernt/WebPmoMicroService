using ONS.WEBPMO.Application.DTO;

namespace ONS.WEBPMO.Domain.Presentations
{
    
    public interface ILogNotificacaoPresentation 
    {
        
        
        LogNotificacaoDTO ObterDadosPesquisaLogNotificacao(int? idSemanaOperativa = null, bool isMonitorar = false, bool? ordernarListagens = true);
    }
}
