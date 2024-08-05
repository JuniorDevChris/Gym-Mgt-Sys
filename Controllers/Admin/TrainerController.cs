using Microsoft.AspNetCore.Mvc;
using Gym_Mgt_System.Models;
using System.Linq;
using Gym_Mgt_System.Data;

namespace Gym_Mgt_System.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var trainers = new List<Trainer>
            {
                new Trainer { Id = 1, Name = "John Doe", PhoneNumber = "123-456-7890", Email = "john@doe.com" },
                new Trainer { Id = 2, Name = "Jane Smith", PhoneNumber = "234-567-8901", Email = "jane@doe.com" }
            };

            ViewBag.Trainers = trainers;
            return View("~/Views/Admin/Trainers.cshtml");
        }

        [HttpPost]
        public IActionResult CreateOrDeleteTrainer(Trainer trainer, string action)
        {
            if (action == "Add")
            {
                if (ModelState.IsValid)
                {
                    if (trainer.Id == 0)
                    {
                        _context.Trainers.Add(trainer);
                    }
                    else
                    {
                        _context.Trainers.Update(trainer);
                    }
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            else if (action == "Delete")
            {
                var existingTrainer = _context.Trainers.Find(trainer.Id);
                if (existingTrainer != null)
                {
                    _context.Trainers.Remove(existingTrainer);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View("~/Views/Admin/Trainer.cshtml", _context.Trainers.ToList());
        }
    }
}
