namespace App.Repositories.Dictionaries {

public class CountryRepository : DictionaryRepository<Country>, ICountryRepository
{
    public CountryRepository(IAppDbContext context) : base(context)
    {
    }
}
}