using App.Interfaces.Core;

namespace App.Common;

public class RejectingSettings: IRejectingSettings
{
    public bool Enable { get; set; }
    public int CountForBlock { get; set; }
}