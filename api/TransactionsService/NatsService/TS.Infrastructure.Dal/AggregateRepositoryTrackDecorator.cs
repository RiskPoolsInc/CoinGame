using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using SilentNotary.Cqrs.Domain;
using SilentNotary.Specifications;

namespace TS.Infrastructure.Dal
{
    public class AggregateRepositoryTrackDecorator<TAggregate, TId>
         : AggregateRepository<TAggregate, TId>
         where TAggregate : Aggregate<TId>
    {
        private readonly AggregateRepository<TAggregate, TId> _repository;
        private readonly IAggregateTracker _tracker;

        public AggregateRepositoryTrackDecorator(AggregateRepository<TAggregate, TId> repository,
            IAggregateTracker tracker)
        {
            _repository = repository;
            _tracker = tracker;
        }

        public override void Add(TAggregate aggregate)
        {
            _repository.Add(aggregate);
            _tracker.Track(aggregate);
        }

        public override Task<bool> AnyAsync<TAggregateState>(Specification<TAggregateState> specification = null)
        {
            return _repository.AnyAsync(specification);
        }

        public override async Task<IEnumerable<TAggregate>> GetAllAsync<TAggregateState>(
            Specification<TAggregateState> specification = null)
        {
            var aggregates = (await _repository.GetAllAsync(specification)).ToArray();
            foreach (var aggregate in aggregates)
                _tracker.Track(aggregate);
            return aggregates;
        }

        public override async Task<Result<TAggregate>> GetByIdAsync(TId id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result.IsSuccess)
                _tracker.Track(result.Value);
            return result;

        }

        public override async Task<Result<TAggregate>> GetSingleAsync<TAggregateState>(
            Specification<TAggregateState> specification = null)
        {
            var result = await _repository.GetSingleAsync(specification);
            if (result.IsSuccess)
                _tracker.Track(result.Value);
            return result;
        }

        public override void Update(TAggregate aggregate, params string[] updateProperties)
        {
            _repository.Update(aggregate, updateProperties);
        }
    }
}