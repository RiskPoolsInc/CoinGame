using App.Core.ViewModels.Verifications;

namespace App.Core.Commands.Verifications;

public class GenerateVerificationCodeCommand : IRequest<VerificationCodeView> {
    public int ExpirationTimeMinutes { get; set; }
    public int Type { get; set; } = 1;
    public int Length { get; set; } = 4;
}