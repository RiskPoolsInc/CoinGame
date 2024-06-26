namespace App.Repozitories.Pbz.Dictionaries {

public class MultiFactorAuthTypeRepository : DictionaryRepository<MultiFactorAuthType>, IMultiFactorAuthTypeRepository
{
    public MultiFactorAuthTypeRepository(IPbzDbContext context) : base(context)
    {
    }
}
}