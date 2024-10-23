using SilentNotary.Common.Query;

namespace TS.Domain
{
    public class MetaQueryResult<T> : MultipleQueryResult<T>
    {
        public Metadata Meta { get; set; }
    }

    public class Metadata
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalItems { get; set; }
    }
}
