namespace App.Repozitories.Pbz.Dictionaries {

public class LocaleCodeRepository : DictionaryRepository<LocaleCode>, ILocaleCodeRepository
{
    public LocaleCodeRepository(IPbzDbContext context) : base(context) { }
}
}