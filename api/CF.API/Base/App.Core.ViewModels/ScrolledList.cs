namespace App.Core.ViewModels;

public class ScrolledList<T> {
    public T[] Items { get; set; }
    public string Token { get; set; }
    public int? TotalCount { get; set; }
}