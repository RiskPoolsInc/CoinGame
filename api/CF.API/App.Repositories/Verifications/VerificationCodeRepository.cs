using App.Data.Entities.Verification;
using App.Interfaces.Repositories;

namespace App.Repositories.Verifications;

public class VerificationCodeRepository : AuditableRepository<VerificationCode>, IVerificationCodeRepository {
    public VerificationCodeRepository(IAppDbContext context) : base(context) {
    }
}