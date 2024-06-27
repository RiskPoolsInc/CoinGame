namespace App.Interfaces.Data.Entities;

public interface IBaseDictionaryEntity<T> : IEntity<T> {
    string Name { get; set; }
    string Code { get; set; }
}