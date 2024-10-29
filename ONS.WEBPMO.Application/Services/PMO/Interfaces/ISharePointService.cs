
using ONS.WEBPMO.Application.DTO;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{

    public interface ISharePointService
    {


        byte[] ObterArquivo(string caminhoArquivo, int? numeroVersao);

        /// <summary>
        /// Envia arquivos para a biblioteca do SharePoint 2007 especificada no arquivo de configurações.
        /// </summary>
        /// <param name="arquivos"></param>
        /// <param name="subPasta"></param>
        void EnviarArquivosSharePoint(ISet<ArquivoDadoNaoEstruturadoDTO> arquivos, string subPasta);
    }
}
