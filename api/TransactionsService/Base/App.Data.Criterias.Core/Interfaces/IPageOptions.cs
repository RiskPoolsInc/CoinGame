namespace App.Data.Criterias.Core.Interfaces;

public interface IPageOptions
{
    public int? Size { get; }
    public int? Page { get; }
    public int? Skip { get; }
    public bool? GetAll { get; }
    public bool? WithDeleted { get; }
}