using CliTaskStatus = TaskCli.Enums.TaskStatus;

namespace TaskCli.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = "";
        public CliTaskStatus Status { get; set; } = CliTaskStatus.Todo;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public TaskItem() { }

        public TaskItem(Guid id, string description)
        {
            Id = id;
            Description = description;
            Status = CliTaskStatus.Todo;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateTask(string description)
        {
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateStatus(CliTaskStatus status)
        {
            Status = status;
            UpdatedAt = DateTime.UtcNow;
        }

        public override string ToString()
            => $"{Id} | {Description} | {Status} | Created: {CreatedAt:u}";
    }
}
