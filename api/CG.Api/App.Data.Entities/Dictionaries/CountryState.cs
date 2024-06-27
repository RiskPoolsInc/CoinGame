using App.Data.Entities.Core;

namespace App.Data.Entities.Dictionaries;

public class CountryState : DictionaryEntity {
    public int CountryId { get; set; }
    public virtual Country Country { get; set; }
}