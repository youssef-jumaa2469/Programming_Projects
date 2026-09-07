using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الاسم مطلوب")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صالح")]
        public string Email { get; set; } = string.Empty;
    }
}