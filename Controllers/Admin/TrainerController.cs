using Microsoft.AspNetCore.Mvc;
using Gym_Mgt_System.Models;
using System.Linq;
using Gym_Mgt_System.Data;
using Gym_Mgt_System.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

namespace Gym_Mgt_System.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public TrainerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var trainers = await _context.Trainers.ToListAsync();

            var viewModel = new TrainerViewModel
            {
                Trainers = trainers
            };

            return View("~/Views/Admin/Trainers.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var existingTrainer = await _context.Trainers.FindAsync(viewModel.Id);
                if (existingTrainer == null)
                {
                    Trainer trainer = new Trainer
                    {
                        Name = viewModel.Name,
                        PhoneNumber = viewModel.PhoneNumber,
                        Email = viewModel.Email
                    };
                    await _context.Trainers.AddAsync(trainer);
                }
                else if (existingTrainer != null)
                {
                    // Updating an existing trainer
                    existingTrainer.Name = viewModel.Name;
                    existingTrainer.PhoneNumber = viewModel.PhoneNumber;
                    existingTrainer.Email = viewModel.Email;

                    _context.Trainers.Update(existingTrainer);
                }
                
                await _context.SaveChangesAsync();
            }
            return Redirect(nameof(Index));
        }
    }
}
