namespace App.Interfaces.Core;

public interface IEntityViewLocalizationService {
    public string GetEntityNameView { get; }
    public string GetEntityNameViewPlural { get; }
}

public interface IEntityViewLocalizationService<T> : IEntityViewLocalizationService {
}