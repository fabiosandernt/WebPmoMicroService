using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

using ONS.Common.IoC;
using ONS.SGIPMO.Domain.Entities;
using ONS.SGIPMO.Domain.Services;

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
