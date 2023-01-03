using Microsoft.AspNetCore.Mvc;

namespace NASSOne.Platform.Authorization.API.Controllers
{
    public class RolesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
