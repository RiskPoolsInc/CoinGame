namespace App.Interfaces.Core.Configurations;

public interface IConfigFactory {
    T Create<T>() where T : class, IConfig;
}