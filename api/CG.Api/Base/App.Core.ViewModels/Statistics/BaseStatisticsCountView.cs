namespace App.Core.ViewModels.Statistics;

public class BaseStatisticsCountView {
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int Count { get; set; }
}