using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int totalTasks = await _context.Tasks.CountAsync();

            int completedTasks = await _context.Tasks
                .CountAsync(t => t.Status == "منتهي");

            double completionPercentage = totalTasks == 0
                ? 0
                : (double)completedTasks / totalTasks * 100;

            var model = new ReportViewModel
            {
                TotalTasks = totalTasks,
                CompletedTasks = completedTasks,
                RemainingTasks = totalTasks - completedTasks,
                CompletionPercentage = completionPercentage
            };

            return View(model);
        }
    }
}