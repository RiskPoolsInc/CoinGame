namespace App.Repositories.Dictionaries {

public class MultiFactorAuthTypeRepository : DictionaryRepository<MultiFactorAuthType>, IMultiFactorAuthTypeRepository
{
    public MultiFactorAuthTypeRepository(IAppDbContext context) : base(context)
    {
    }
}
}