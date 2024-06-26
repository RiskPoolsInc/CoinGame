using App.Interfaces.Core.Requests;

namespace App.Core.ViewModels;

public class PagedList<T> : IPagedList<T> {
    public PagedList() {
    }

    public PagedList(T[] items, int count) : this(items, count, 1, 20) {
    }

    public PagedList(T[] items, int count, int pageNumber, int pageSize) {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        Items = items;
    }

    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public bool HasPrevious => CurrentPage > 1;

    public bool HasNext => CurrentPage < TotalPages;

    public T[] Items { get; }
}