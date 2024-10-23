using SilentNotary.Cqrs.Domain.Interfaces;

namespace TS.Infrastructure.Dal
{
    public interface IAggregateTracker
    {
        void Track(IAggregateRoot aggregate);
    }
}
