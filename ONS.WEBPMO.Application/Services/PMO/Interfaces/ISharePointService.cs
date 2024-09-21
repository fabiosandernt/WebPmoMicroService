namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    public interface ISharePointService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        byte[] ObterArquivo(string caminhoArquivo, int? numeroVersao);

        /// <summary>
        /// Envia arquivos para a biblioteca do SharePoint 2007 especificada no arquivo de configurações.
        /// </summary>
        /// <param name="arquivos"></param>
        /// <param name="subPasta"></param>
        void EnviarArquivosSharePoint(ISet<ArquivoDadoNaoEstruturadoDTO> arquivos, string subPasta);
    }
}
