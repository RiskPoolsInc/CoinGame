namespace TS.Domain.Base.Interfaces
{ 
    public interface IPagedCriterion : ISortedCriterion {
        int? Page { get; }
        int? Size { get; }
        public bool? GetAll { get; set; }
        public bool? WithDeleted { get; set; }
        int? Skip { get; }
    }
    
    public interface IPagedCriterion<T> : IPagedCriterion {
    }
}