using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Helpers;
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

        public async Task<IActionResult> Index(int? projectId)
        {
            var query = _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.ActivityLogs)
                .AsNoTracking()
                .AsQueryable();

            if (projectId.HasValue)
            {
                query = query.Where(t => t.ProjectId == projectId.Value);
            }

            ViewBag.ProjectId = projectId;
            ViewBag.Projects = new SelectList(
                await _context.Projects.OrderBy(p => p.Name).ToListAsync(),
                "Id",
                "Name",
                projectId);

            return View(await query.OrderByDescending(t => t.Id).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string newStatus, int? projectId)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            var normalizedStatus = WorkItemLabels.Status(newStatus);
            if (normalizedStatus == "—")
            {
                return RedirectToAction(nameof(Index), new { projectId });
            }

            var oldStatus = WorkItemLabels.Status(task.Status);
            if (!string.Equals(oldStatus, normalizedStatus, StringComparison.Ordinal))
            {
                task.Status = normalizedStatus;

                _context.ActivityLogs.Add(new ActivityLog
                {
                    TaskId = task.Id,
                    OldStatus = oldStatus == "—" ? task.Status : oldStatus,
                    NewStatus = normalizedStatus,
                    ChangedAt = DateTime.Now
                });

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index), new { projectId });
        }
    }
}
