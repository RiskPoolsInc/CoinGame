using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.TaskExecutions;

namespace App.Data.Criterias.TaskExecutions;

public class TaskExecutionByStateIds : ACriteria<TaskExecution>
{
    private readonly int[] _stateIds;

    public TaskExecutionByStateIds(int[] stateIds)
    {
        _stateIds = stateIds;
    }

    public override Expression<Func<TaskExecution, bool>> Build()
    {
        return _stateIds?.Length switch
        {
            1 => a => _stateIds[0] == a.StateId,
            > 0 => a => _stateIds.Contains(a.StateId),
            _ => a => true
        };
    }
}