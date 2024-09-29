namespace ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta
{
    //[ServiceContract]
    public interface ISubsistemaService 
    {
        
        
        Subsistema ObterSubsistemaPorChave(int chave);

        
        
        IList<Subsistema> ConsultarSubsistemasPorChaves(params int[] chaves);

        
        
        IList<Subsistema> ConsultarSubsistemasPorNome(string nome);
    }
}
