namespace ProjectManagementSystem.Models
{
    public class ReportViewModel
    {
        public int? ProjectId { get; set; }

        public string? ProjectName { get; set; }

        public int TotalTasks { get; set; }

        public int NewTasks { get; set; }

        public int InProgressTasks { get; set; }

        public int CompletedTasks { get; set; }

        public int RemainingTasks { get; set; }

        public double CompletionPercentage { get; set; }
    }
}
