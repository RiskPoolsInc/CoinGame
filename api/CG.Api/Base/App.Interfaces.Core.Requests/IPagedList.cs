namespace App.Interfaces.Core.Requests;

public interface IPagedList<out T> {
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasPrevious { get; }
    public bool HasNext { get; }
    public T[] Items { get; }
}