namespace TS.Domain.Base.Interfaces
{
    public interface ISortedCriterion
    {
        string Sort { get; }
        int Direction { get; }
    }
}