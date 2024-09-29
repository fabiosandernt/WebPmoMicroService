namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    //[ServiceContract]
    public interface IGeracaoBlocosService 
    {
        /// <summary>
        /// Gera os arquivos de blocos e os mantem na base de dados do SGIPMO.
        /// </summary>
        /// <param name="idSemanaOperativa">Id da semana operativa (Estudo)</param>
        /// <param name="versao">Versão da semana operativa para controle de concorrência</param>
        
        
        
        void GerarBlocos(int idSemanaOperativa, byte[] versao, bool somenteAprovados);

    }
}
