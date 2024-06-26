using App.Data.Entities.Pbz.ChangeReasons;
using App.Interfaces.Repositories.Pbz.ChangeReasons;

namespace App.Repozitories.Pbz.ChangeReasons;

public class ChangeReasonRepository: Repository<ChangeReason>, IChangeReasonRepository
{
    public ChangeReasonRepository(IPbzDbContext context) : base(context)
    {
    }
}