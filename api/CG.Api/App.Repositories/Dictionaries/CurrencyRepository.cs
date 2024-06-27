namespace App.Repositories.Dictionaries {

public class CurrencyRepository : DictionaryRepository<Currency>, ICurrencyRepository
{
    public CurrencyRepository(IAppDbContext context) : base(context) { }
}
}