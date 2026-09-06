using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Priority { get; set; }

        public string? Status { get; set; }

        public int ProjectId { get; set; }

        public Project? Project { get; set; }

        public int? UserId { get; set; }

        public int? ParentTaskId { get; set; }
    }
}