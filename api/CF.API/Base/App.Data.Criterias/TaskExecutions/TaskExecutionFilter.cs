using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Criterias.Core.Helpers;
using App.Data.Entities.TaskExecutions;
using App.Interfaces.RequestParams.TaskExecutions;

namespace App.Data.Criterias.TaskExecutions;

public class TaskExecutionFilter : OrderCriteria<TaskExecution>, ITaskExecutionsRequestParams
{
    public Guid[]? TaskIds { get; set; }
    public int[]? StatusIds { get; set; }
    public int[]? StateIds { get; set; }
    public Guid?[]? UserIds { get; set; }

    public override Expression<Func<TaskExecution, bool>> Build()
    {
        var criteria = True;
        if (TaskIds?.Length > 0)
            criteria = criteria.And(new TaskExecutionByTaskIds(TaskIds));

        if (StateIds?.Length > 0)
            criteria = criteria.And(new TaskExecutionByStateIds(StateIds));

        if (StatusIds?.Length > 0)
            criteria = criteria.And(new TaskExecutionByStatusIds(StatusIds));

        if (UserIds?.Length > 0)
            criteria = criteria.And(new TaskExecutionByUserIds(UserIds));

        SetSortBy(a => a.UpdatedOn);

        return criteria.Build();
    }
}