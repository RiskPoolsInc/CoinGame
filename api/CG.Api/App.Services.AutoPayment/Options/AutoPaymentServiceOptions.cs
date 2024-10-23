using App.Core.Configurations.Annotaions;
using App.Interfaces.Core.Configurations;

namespace App.Services.AutoPayment.Options;

[ConfigurationName("AutoPaymentServiceOptions")]
public class AutoPaymentServiceOptions : IAutoPaymentServiceOptions, IConfig {
    public const string SECTION_NAME = "AutoPaymentServiceOptions";
    public int TimeRepeatAutoPaymentMilliseconds { get; set; }
    public string AccessKey { get; set; }
    public bool RunAfterBuild { get; set; }
}