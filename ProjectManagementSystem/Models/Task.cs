using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان المهمة مطلوب")]
        [Display(Name = "عنوان المهمة")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "الوصف")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "الأولوية مطلوبة")]
        [Display(Name = "الأولوية")]
        public string? Priority { get; set; }

        [Required(ErrorMessage = "الحالة مطلوبة")]
        [Display(Name = "الحالة")]
        public string? Status { get; set; }

        [Required(ErrorMessage = "المشروع مطلوب")]
        [Display(Name = "المشروع")]
        [Range(1, int.MaxValue, ErrorMessage = "المشروع مطلوب")]
        public int ProjectId { get; set; }

        [Display(Name = "المشروع")]
        public Project? Project { get; set; }

        public int? UserId { get; set; }

        public int? ParentTaskId { get; set; }
    }
}
