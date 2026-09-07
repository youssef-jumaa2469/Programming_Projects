using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Models;
using TaskModel = ProjectManagementSystem.Models.Task;

namespace ProjectManagementSystem.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? projectId)
        {
            var query = _context.Tasks
                .Include(t => t.Project)
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

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        public IActionResult Create(int? projectId)
        {
            PopulateProjects(projectId);

            if (!_context.Projects.Any())
            {
                TempData["Notice"] = "أضف مشروعاً أولاً قبل إنشاء المهام.";
                return RedirectToAction("Create", "Projects");
            }

            return View(new TaskModel
            {
                ProjectId = projectId ?? 0,
                Priority = "متوسطة",
                Status = "جديدة"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Title,Description,Priority,Status,ProjectId,UserId,ParentTaskId")] TaskModel task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
            }

            PopulateProjects(task.ProjectId);
            return View(task);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            PopulateProjects(task.ProjectId);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Title,Description,Priority,Status,ProjectId,UserId,ParentTaskId")] TaskModel task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
            }

            PopulateProjects(task.ProjectId);
            return View(task);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateProjects(int? selectedId)
        {
            ViewData["ProjectId"] = new SelectList(
                _context.Projects.OrderBy(p => p.Name),
                "Id",
                "Name",
                selectedId);
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
