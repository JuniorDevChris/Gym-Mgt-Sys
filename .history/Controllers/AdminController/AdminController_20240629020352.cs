using Microsoft.AspNetCore.Mvc;

namespace Gym_Clt_Mgt_System.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminDashboard()
        {
            return View();
        }

        public IActionResult Coaches()
        {
            return View();
        }
    }
}
