using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using System.Reflection;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class SharePointService : ISharePointService
    {
        public void EnviarArquivosSharePoint(ISet<ArquivoDadoNaoEstruturadoDTO> arquivos, string subPasta)
        {
            throw new NotImplementedException();
        }

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


    }
}
