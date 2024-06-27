using System.Text;

using App.Interfaces.Repositories.Generic;

using Microsoft.Extensions.DependencyInjection;

namespace App.Web.Core.ParameterFilters; 

public class CustomParameterBase {
    protected readonly IServiceProvider _serviceProvider;

    public CustomParameterBase(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
    }

    protected Dictionary<string, string> GetDictionary(Type type) {
        var genericType = typeof(IGenericDictionaryRepository<>);
        var specificType = genericType.MakeGenericType(type);
        dynamic instance = _serviceProvider.GetRequiredService(specificType);
        return instance.GetDictionary() ?? new Dictionary<string, string>();
    }

    protected string GetDescriptionValues(Dictionary<string, string> dictionary) {
        if (dictionary.Count == 0)
            return "";
        var description = new StringBuilder("Members: <ul>");

        foreach (var entry in dictionary)
            description.Append($"<li>{entry.Key} = {entry.Value}</li>");
        description.Append("</ul>");
        return description.ToString();
    }
}