namespace App.Services.AutoPayment;

public interface IAutoPaymentServiceOptions {
    public int TimeRepeatAutoPaymentMilliseconds { get; set; }
    public string AccessKey { get; set; }
    public bool RunAfterBuild { get; set; }

}