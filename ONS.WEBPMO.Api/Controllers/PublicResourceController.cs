using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ONS.Common.Util.Files;

using ONS.SGIPMO.Domain.Services;

namespace ONS.WEBPMO.Api.Controllers
{
    public class PublicResourceController : ControllerBase
    {
        private readonly IArquivoService arquivoService;

        public PublicResourceController(
            IArquivoService arquivoService)
        {
            this.arquivoService = arquivoService;
        }

        protected override ResponseDownload DownloadFilesDatabase(RequestDownload request)
        {
            return arquivoService.ObterArquivosCompactados(request);
        }
    }
}
