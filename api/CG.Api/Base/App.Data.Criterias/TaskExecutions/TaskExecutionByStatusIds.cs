using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.TaskExecutions;

namespace App.Data.Criterias.TaskExecutions;

public class TaskExecutionByStatusIds : ACriteria<TaskExecution>
{
    private readonly int[] _statusIds;

    public TaskExecutionByStatusIds(int[] statusIds)
    {
        _statusIds = statusIds;
    }

    public override Expression<Func<TaskExecution, bool>> Build()
    {
        return _statusIds?.Length switch
        {
            1 => a => _statusIds[0] == a.StatusId,
            > 0 => a => _statusIds.Contains(a.StatusId),
            _ => a => true
        };
    }
}