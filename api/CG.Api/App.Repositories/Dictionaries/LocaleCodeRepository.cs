namespace App.Repositories.Dictionaries {

public class LocaleCodeRepository : DictionaryRepository<LocaleCode>, ILocaleCodeRepository
{
    public LocaleCodeRepository(IAppDbContext context) : base(context) { }
}
}