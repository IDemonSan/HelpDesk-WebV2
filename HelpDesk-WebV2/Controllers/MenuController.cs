using Microsoft.AspNetCore.Mvc;

namespace HelpDesk_WebV2.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
