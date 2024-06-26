namespace App.Interfaces.RequestsParams.Tasks;

public interface ITaskRequest
{
    Guid[]? CreatedById { get; set; }
    int[]? StateId { get; set; }
    decimal? AwardMin { get; set; }
    decimal? AwardMax { get; set; }
    DateTime? DeadlineFrom { get; set; }
    DateTime? DeadlineTo { get; set; }
    DateTime? CreatedFrom { get; set; }
    DateTime? CreatedTo { get; set; }
    string[]? Tags { get; set; }
    string? Text { get; set; }
    string? Location { get; set; }
    string[]? ExclusiveProperties { get; set; }
}