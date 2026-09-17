using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Helpers;
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

        public async Task<IActionResult> Index(int? projectId)
        {
            var query = _context.Tasks.AsNoTracking().AsQueryable();

            if (projectId.HasValue)
            {
                query = query.Where(t => t.ProjectId == projectId.Value);
            }

            var tasks = await query.ToListAsync();
            var completed = tasks.Count(t => WorkItemLabels.IsDone(t.Status));
            var inProgress = tasks.Count(t => WorkItemLabels.IsInProgress(t.Status));
            var newly = tasks.Count(t => WorkItemLabels.IsNew(t.Status));
            var total = tasks.Count;

            ViewBag.Projects = new SelectList(
                await _context.Projects.OrderBy(p => p.Name).ToListAsync(),
                "Id",
                "Name",
                projectId);

            var model = new ReportViewModel
            {
                ProjectId = projectId,
                ProjectName = projectId.HasValue
                    ? await _context.Projects
                        .AsNoTracking()
                        .Where(p => p.Id == projectId.Value)
                        .Select(p => p.Name)
                        .FirstOrDefaultAsync()
                    : null,
                TotalTasks = total,
                NewTasks = newly,
                InProgressTasks = inProgress,
                CompletedTasks = completed,
                RemainingTasks = total - completed,
                CompletionPercentage = total == 0 ? 0 : (double)completed / total * 100
            };

            return View(model);
        }
    }
}
