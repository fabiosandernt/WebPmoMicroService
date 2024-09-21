using Microsoft.AspNetCore.Mvc;

namespace ONS.WEBPMO.Api.Controllers
{
    

    public class ErrorController : ControllerBase
    {
        
        public ActionResult Index()
        {
            return View("Error");
        }

    }
}
