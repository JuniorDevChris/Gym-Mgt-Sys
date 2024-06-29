using Microsoft.AspNetCore.Mvc;

namespace Gym_Clien_Mgt_System.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminDashboard()
        {
            return View();
        }
    }
}
