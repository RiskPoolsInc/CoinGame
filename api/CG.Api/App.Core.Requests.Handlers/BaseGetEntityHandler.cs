using App.Common.Helpers;
using App.Interfaces.Data.Entities;
using App.Interfaces.Repositories.Core;
using App.Interfaces.RequestsParams;

namespace App.Core.Requests.Handlers;

public abstract class BaseGetEntityHandler<TRequest, TResultView, TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TRequest : IBaseGetEntityFilter<TKey> {
    private readonly IRepository<TEntity, TKey> _repository;

    public BaseGetEntityHandler(IRepository<TEntity, TKey> repository) {
        _repository = repository;
    }

    public virtual async Task<TResultView> Handle(TRequest request, CancellationToken cancellationToken) {
        return await _repository.Get(request.TaskId).SingleAsync<TEntity, TResultView>(cancellationToken);
    }
}