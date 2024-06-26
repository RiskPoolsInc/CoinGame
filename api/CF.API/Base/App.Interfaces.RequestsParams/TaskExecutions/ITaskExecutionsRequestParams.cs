namespace App.Interfaces.RequestsParams.TaskExecutions;

public interface ITaskExecutionsRequestParams
{
    Guid[]? TaskIds { get; }
    int[]? StateIds { get; }
    int[]? StatusIds { get; }
    Guid[]? UserIds { get; }
}