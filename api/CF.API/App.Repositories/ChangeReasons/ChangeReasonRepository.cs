using App.Data.Entities.ChangeReasons;
using App.Interfaces.Repositories.ChangeReasons;

namespace App.Repositories.ChangeReasons;

public class ChangeReasonRepository: Repository<ChangeReason>, IChangeReasonRepository
{
    public ChangeReasonRepository(IAppDbContext context) : base(context)
    {
    }
}