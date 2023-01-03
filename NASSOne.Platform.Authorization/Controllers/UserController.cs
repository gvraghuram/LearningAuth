using Microsoft.AspNetCore.Mvc;

namespace NASSOne.Platform.Authorization.API.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
