using System;

using TS.Domain.Base.Interfaces;

namespace TS.Domain.Base
{
    public class PagedList<T>: IPagedList<T> {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < TotalPages;
        public PagedList() { }
        public PagedList(T[] items, int count) : this(items, count, 1, 20) { }
        public PagedList(T[] items, int count, int pageNumber, int pageSize) {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }

        public T[] Items { get; }
    }
}