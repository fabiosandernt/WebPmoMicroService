namespace ONS.WEBPMO.Api.Controllers
{
    public class HomeController : Controller
    {


        public HomeController()
        {

        }

        public ActionResult Index()
        {
            ViewBag.Message = "Olá SGIPMO!";
            return View();
        }
    }
}
