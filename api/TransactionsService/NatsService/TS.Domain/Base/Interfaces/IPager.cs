namespace TS.Domain.Base.Interfaces
{
    public interface IPager
    {
        int? PageNumber { get; }
        int? PageSize { get; }
    }
}