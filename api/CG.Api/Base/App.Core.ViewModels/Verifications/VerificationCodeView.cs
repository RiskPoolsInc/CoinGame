namespace App.Core.ViewModels.Verifications;

public class VerificationCodeView : BaseView {
    public string Value { get; set; }
    public bool Confirmed { get; set; }
    public int ExpirationTime { get; set; }
}