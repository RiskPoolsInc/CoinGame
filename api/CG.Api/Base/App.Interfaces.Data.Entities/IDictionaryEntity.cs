namespace App.Interfaces.Data.Entities;

public interface IDictionaryEntity<T> : IEntity<T> {
    string Name { get; set; }
    string Code { get; set; }
}