using Microsoft.AspNetCore.Mvc;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;

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

        //protected override ResponseDownload DownloadFilesDatabase(RequestDownload request)
        //{
        //    return arquivoService.ObterArquivosCompactados(request);
        //}
    }
}
