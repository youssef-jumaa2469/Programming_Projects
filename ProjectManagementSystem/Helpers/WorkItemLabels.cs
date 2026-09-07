namespace ProjectManagementSystem.Helpers
{
    public static class WorkItemLabels
    {
        public static string Priority(string? value) => value switch
        {
            "Low" or "منخفضة" => "منخفضة",
            "Medium" or "متوسطة" => "متوسطة",
            "High" or "عالية" => "عالية",
            _ => string.IsNullOrWhiteSpace(value) ? "—" : value
        };

        public static string Status(string? value) => value switch
        {
            "New" or "جديدة" => "جديدة",
            "In Progress" or "قيد التنفيذ" => "قيد التنفيذ",
            "Done" or "منجزة" => "منجزة",
            _ => string.IsNullOrWhiteSpace(value) ? "—" : value
        };

        public static string PriorityClass(string? value) => Priority(value) switch
        {
            "منخفضة" => "badge-priority badge-priority--low",
            "متوسطة" => "badge-priority badge-priority--medium",
            "عالية" => "badge-priority badge-priority--high",
            _ => "badge-priority"
        };

        public static string StatusClass(string? value) => Status(value) switch
        {
            "جديدة" => "badge-status badge-status--new",
            "قيد التنفيذ" => "badge-status badge-status--progress",
            "منجزة" => "badge-status badge-status--done",
            _ => "badge-status"
        };
    }
}
