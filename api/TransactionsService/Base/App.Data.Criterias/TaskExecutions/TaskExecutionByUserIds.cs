using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.TaskExecutions;

namespace App.Data.Criterias.TaskExecutions;

public class TaskExecutionByUserIds : ACriteria<TaskExecution>
{
    private readonly Guid?[] _userIds;

    public TaskExecutionByUserIds(Guid?[] userIds)
    {
        _userIds = userIds;
    }

    public override Expression<Func<TaskExecution, bool>> Build()
    {
        return _userIds?.Length switch
        {
            1 => a => _userIds[0] == a.UserId,
            > 0 => a => _userIds.Contains(a.UserId),
            _ => a => true
        };
    }
}