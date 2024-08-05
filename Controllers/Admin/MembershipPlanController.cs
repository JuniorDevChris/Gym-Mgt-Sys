using Microsoft.AspNetCore.Mvc;
using Gym_Mgt_System.Models;
using System.Linq;
using Gym_Mgt_System.Data;
using System.Collections.Generic;

namespace Gym_Mgt_System.Controllers
{
    public class MembershipPlanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MembershipPlanController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Add some dummy data
            var membershipPlans = new List<MembershipPlan>
            {
                new MembershipPlan
                {
                    Id = 1,
                    Name = "Standard Plan",
                    Cost = 29.99m,
                    PlanType = "Subscription"
                },
                new MembershipPlan
                {
                    Id = 2,
                    Name = "Premium Plan",
                    Cost = 49.99m,
                    PlanType = "Subscription"
                },
                new MembershipPlan
                {
                    Id = 3,
                    Name = "Pay-As-You-Go Plan",
                    Cost = 9.99m,
                    PlanType = "PayAsYouGo"
                }
            };

            ViewBag.MembershipPlans = membershipPlans;
            return View("~/Views/Admin/MembershipPlan.cshtml");
        }

        [HttpPost]
        public IActionResult CreateOrDeleteMembershipPlan(MembershipPlan membershipPlan, string action)
        {
            if (action == "Add" || action == "Save")
            {
                if (ModelState.IsValid)
                {
                    if (membershipPlan.Id == 0)
                    {
                        _context.MembershipPlans.Add(membershipPlan);
                    }
                    else
                    {
                        _context.MembershipPlans.Update(membershipPlan);
                    }
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            else if (action == "Delete")
            {
                var existingPlan = _context.MembershipPlans.Find(membershipPlan.Id);
                if (existingPlan != null)
                {
                    _context.MembershipPlans.Remove(existingPlan);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View("~/Views/Admin/MembershipPlan.cshtml", _context.MembershipPlans.ToList());
        }
    }
}
