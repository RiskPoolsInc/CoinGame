namespace App.Core.ViewModels.Log.Properties;

public class PropertyHistoryView : BasePropertyHistoryView
{
    public int? ValueInt { get; set; }
    public bool? ValueBoolean { get; set; }
    public string? ValueString { get; set; }
    public double? ValueDouble { get; set; }
    public Guid? ValueGuid { get; set; }
}