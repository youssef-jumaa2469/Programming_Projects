using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Models;
using TaskModel = ProjectManagementSystem.Models.Task;

namespace ProjectManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       public DbSet<Project> Projects { get; set; }
public DbSet<TaskModel> Tasks { get; set; }
public DbSet<ActivityLog> ActivityLogs { get; set; }
    }
}