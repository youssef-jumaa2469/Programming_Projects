using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم المشروع مطلوب")]
        [Display(Name = "اسم المشروع")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "الوصف")]
        public string? Description { get; set; }

        [Display(Name = "تاريخ البداية")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "تاريخ النهاية")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}