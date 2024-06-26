using App.Data.Entities.Tasks;
using App.Data.Sql.Core.Interfaces;
using App.Interfaces.Repositories.Tasks;

namespace App.Repositories.Tasks;

public class RequiredAttachmentRepository : Repository<RequiredAttachment>, IRequiredAttachmentRepository {
    public RequiredAttachmentRepository(IAppDbContext context) : base(context) {
    }
}