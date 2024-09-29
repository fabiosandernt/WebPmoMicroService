using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONS.WEBPMO.Application.DTO
{
    public abstract class ResponseDownload
    {
        public string Filename { get; set; }

        public abstract void GenerateReponseDownload(byte[] content, string filename);

        #region Métodos estáticos

        public static String MontarIdentificacaoUnica(string identificacaoArquivo)
        {
            return (String.IsNullOrEmpty(identificacaoArquivo) ? "Arquivos" : identificacaoArquivo) + "_" +
                   String.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + "_" + (new Random().Next());
        }

        #endregion

    }
}
