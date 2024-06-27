using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Criterias.Core.Helpers;
using App.Data.Criterias.Core.Interfaces;
using App.Data.Entities.Tasks;
using App.Interfaces.RequestsParams;

namespace App.Data.Criterias.Tasks
{
    public class TaskFilter : PagedCriteria<TaskEntity>, IKeyWordFilter
    {
        public Guid[]? CreatedById { get; set; }
        public Guid?[]? AssigneeId { get; set; }

        public bool FilterByManaged { get; set; }
        public Guid[]? ManagedByUserIds { get; set; }
        public int[]? StateGroupId { get; set; }
        public int[]? StateId { get; set; }

        public string[]? ExclusiveProperties { get; set; }

        public KeyWordFilter[]? KeyWords { get; set; }

        public override Expression<Func<TaskEntity, bool>> Build()
        {
            var criteria = True;
            ExclusiveProperties = ExclusiveProperties ?? Array.Empty<string>();

            if (CreatedById != null && CreatedById.Length > 0)
                criteria = criteria.And(new TaskByCreatedId(CreatedById, ExclusiveProperties.Contains("createdById")));
            if (AssigneeId != null && AssigneeId.Length > 0)
                criteria = criteria.And(new TaskByAssigneeId(AssigneeId, ExclusiveProperties.Contains("assigneeId")));
            if (StateGroupId != null && StateGroupId.Length > 0)
                criteria = criteria.And(new TaskByStateGroup(StateGroupId, ExclusiveProperties.Contains("stateGroupId")));
            if (StateId != null && StateId.Length > 0)
                criteria = criteria.And(new TaskByStateId(StateId, ExclusiveProperties.Contains("stateId")));
            if (FilterByManaged)
                criteria = criteria.And(GetManagedCriteria());

            if (!string.IsNullOrEmpty(KeyWords?.FirstOrDefault()?.KeyWord))
                criteria = criteria.And(new TaskByKeyWord(KeyWords));

            if (String.IsNullOrEmpty(Sort))
                SetSortBy(a => a.CreatedOn);

            return criteria.Build();
        }

        private ICriteria<TaskEntity> GetManagedCriteria()
        {
            return new TaskByAssigneeIds(ManagedByUserIds);
        }


        public override IQueryable<TaskEntity> OrderBy(IQueryable<TaskEntity> source)
        {
            switch (Sort)
            {
                case "createdBy":
                    return OrderByDirection(source, s => s.CreatedBy.FirstName, s => s.CreatedBy.LastName);

                case "assignedTo":
                    return OrderByDirection(source, s => s.AssignedTo.FirstName, s => s.AssignedTo.LastName);

                case "state":
                    return OrderByDirection(source, s => s.State.Name);
            }

            return base.OrderBy(source);
        }
    }
}