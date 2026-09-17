using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Helpers;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var tasks = await _context.Tasks.AsNoTracking().ToListAsync();

        var model = new DashboardViewModel
        {
            ProjectsCount = await _context.Projects.CountAsync(),
            TasksCount = tasks.Count,
            InProgressTasksCount = tasks.Count(t => WorkItemLabels.IsInProgress(t.Status)),
            DoneTasksCount = tasks.Count(t => WorkItemLabels.IsDone(t.Status))
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
