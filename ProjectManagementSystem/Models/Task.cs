namespace ProjectManagementSystem.Models
{
    public class ActivityLog
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public string? OldStatus { get; set; }

        public string? NewStatus { get; set; }

        public DateTime ChangedAt { get; set; }

        public Task? Task { get; set; }
    }
}