namespace App.Core.ViewModels;

public class BaseChildrenCountView
{
    public Guid TaskId { get; set; }
    public int Id { get; set; }
    public DictionaryView? State { get; set; }
    public long CountById { get; set; }
}