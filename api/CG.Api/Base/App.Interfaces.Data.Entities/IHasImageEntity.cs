namespace App.Interfaces.Data.Entities;

public interface IHasImageEntity : IEntity {
    string ImageFileName { get; set; }
}