using ONS.Common.Exceptions;
using ONS.Common.Services.Impl;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.DTO;
using System.Reflection;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class SharePointService : Service, ISharePointService
    {
        [Obsolete("Fake")]
        public byte[] ObterArquivo(string caminhoArquivo, int? numeroVersao)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream filestream = assembly.GetManifestResourceStream("ONS.SGIPMO.Domain.Services.Impl.Resources.Documento.pdf"))
            {
                if (filestream == null) return null;
                byte[] bytes = new byte[filestream.Length];
                filestream.Read(bytes, 0, bytes.Length);
                return bytes;
            }
        }

        /// <summary>
        /// Envia arquivos para a biblioteca do SharePoint 2007 especificada no arquivo de configurações.
        /// </summary>
        /// <param name="arquivos"></param>
        /// <param name="subPasta"></param>
        public void EnviarArquivosSharePoint(ISet<ArquivoDadoNaoEstruturadoDTO> arquivos, string subPasta)
        {
            List<KeyValuePair<string, string>> arquivosEnvioSharepoint = new List<KeyValuePair<string, string>>();
            foreach (ArquivoDadoNaoEstruturadoDTO arquivo in arquivos)
            {
                if (arquivo.Id == Guid.Empty)
                {
                    arquivosEnvioSharepoint.Add(new KeyValuePair<string, string>(arquivo.CaminhoFisicoCompleto, arquivo.Nome));
                }
            }

            if (arquivosEnvioSharepoint.Any())
            {
                try
                {
                    SharePointServiceHelper.UploadFilesToSharePointSite2007(arquivosEnvioSharepoint, subPasta);
                }
                catch (Exception ex)
                {
                    throw new ONSBusinessException("Problemas ocorreram ao enviar o(s) arquivo(s) para o SharePoint. Entre em contato com o administrador do sistema.");
                }
            }
        }
    }
}
