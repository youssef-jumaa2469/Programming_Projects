using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Controllers
{
    public class KanbanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KanbanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // عرض لوحة Kanban
        public async Task<IActionResult> Index()
        {
            var tasks = await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.ActivityLogs)
                .AsNoTracking()
                .ToListAsync();

            foreach (var task in tasks)
            {
                if (string.IsNullOrWhiteSpace(task.Status))
                {
                    task.Status = "New";
                    continue;
                }

                var status = task.Status.Trim();

                if (status.Equals("New", StringComparison.OrdinalIgnoreCase))
                {
                    task.Status = "New";
                }
                else if (status.Equals("In Progress", StringComparison.OrdinalIgnoreCase))
                {
                    task.Status = "In Progress";
                }
                else if (status.Equals("Done", StringComparison.OrdinalIgnoreCase))
                {
                    task.Status = "Done";
                }
            }

            return View(tasks);
        }

        // تغيير حالة المهمة وتسجيل الحركة
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string newStatus)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            var oldStatus = task.Status?.Trim();

            if (!string.Equals(oldStatus, newStatus, StringComparison.OrdinalIgnoreCase))
            {
                task.Status = newStatus;

                var activityLog = new ActivityLog
                {
                    TaskId = task.Id,
                    OldStatus = oldStatus,
                    NewStatus = newStatus,
                    ChangedAt = DateTime.Now
                };

                _context.ActivityLogs.Add(activityLog);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}