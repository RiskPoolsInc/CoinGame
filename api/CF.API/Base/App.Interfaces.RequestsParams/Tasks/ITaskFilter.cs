using App.Data.Criterias.Core.Interfaces;
using App.Data.Entities.Tasks;

namespace App.Interfaces.RequestsParams.Tasks;

public interface ITaskFilter : ITaskRequest, ICriteria<TaskEntity>
{ 
}