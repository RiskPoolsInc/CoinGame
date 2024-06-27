using App.Data.Entities.Core;

namespace App.Data.Entities.Dictionaries;

public class Country : DictionaryEntity {
    public string Iso { get; set; }
    public virtual ICollection<CountryState> States { get; set; } = new List<CountryState>();
}