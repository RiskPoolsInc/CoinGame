namespace App.Repozitories.Pbz.Dictionaries {

public class CountryRepository : DictionaryRepository<Country>, ICountryRepository
{
    public CountryRepository(IPbzDbContext context) : base(context)
    {
    }
}
}