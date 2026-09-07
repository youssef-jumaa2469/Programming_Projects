namespace ProjectManagementSystem.Models
{
    public class ReportViewModel
    {
        public int TotalTasks { get; set; }

        public int CompletedTasks { get; set; }

        public int RemainingTasks { get; set; }

        public double CompletionPercentage { get; set; }
    }
}