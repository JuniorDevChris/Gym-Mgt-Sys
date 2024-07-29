using Microsoft.AspNetCore.Mvc;

namespace Gym_Mgt_System.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Login", "Account");
        }
    }
}
