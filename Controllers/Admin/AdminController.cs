using Gym_Mgt_System.Data;
using Gym_Mgt_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym_Mgt_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;            
            _userManager = userManager;
        }
        
        [HttpGet]
        public async Task<IActionResult> AdminDashboard()
        {
            return View();
        }
    }
}
