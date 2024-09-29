namespace ONS.WEBPMO.Application.Services.PMO.Interfaces.OrigemColeta
{
    //[ServiceContract]
    public interface IUsinaService 
    {
        
        
        Usina ObterUsinaPorChave(int chave);

        
        
        IList<Usina> ConsultarUsinasPorChaves(params int[] chaves);

        
        
        IList<Usina> ConsultarUsinasPorNome(string nome);
    }
}
