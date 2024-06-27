namespace App.Interfaces.Core;

public interface IRejectingSettings
{
    public bool Enable { get; set; }
    public int CountForBlock { get; set; }
}