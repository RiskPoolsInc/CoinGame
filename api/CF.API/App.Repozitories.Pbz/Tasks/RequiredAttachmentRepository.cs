namespace App.Repozitories.Pbz.Tasks;

public class RequiredAttachmentRepository : Repository<RequiredAttachment>, IRequiredAttachmentRepository {
    public RequiredAttachmentRepository(IPbzDbContext context) : base(context) {
    }
}