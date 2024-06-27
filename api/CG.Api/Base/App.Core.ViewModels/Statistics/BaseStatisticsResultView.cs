namespace App.Core.ViewModels.Statistics;

public abstract class BaseStatisticsResultView
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    
    // Boolean = 1,
    // Integer = 2,
    // Double = 3,
    // String = 4,
    // Guid = 5,
    // Decimal = 6,
    
    public abstract int Type { get; }
    public decimal? DecimalResult { get; set; }
    public string? TextResult { get; set; }
    public int? IntResult { get; set; }
    public double? DoubleResult { get; set; }
}