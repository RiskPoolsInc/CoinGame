namespace App.Repozitories.Pbz.Dictionaries {

public class CurrencyRepository : DictionaryRepository<Currency>, ICurrencyRepository
{
    public CurrencyRepository(IPbzDbContext context) : base(context) { }
}
}