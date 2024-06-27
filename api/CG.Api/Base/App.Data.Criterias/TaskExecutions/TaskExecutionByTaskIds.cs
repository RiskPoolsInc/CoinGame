using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.TaskExecutions;

namespace App.Data.Criterias.TaskExecutions;

public class TaskExecutionByTaskIds : ACriteria<TaskExecution>
{
    private readonly Guid[]? _taskIds;

    public TaskExecutionByTaskIds(Guid[]? taskIds)
    {
        _taskIds = taskIds;
    }

    public override Expression<Func<TaskExecution, bool>> Build()
    {
        return _taskIds?.Length switch
        {
            1 => a => a.TaskId == _taskIds[0],
            > 0 => a => _taskIds.Contains(a.TaskId),
            _ => a => true
        };
    }
}