namespace App.Core.Requests.Statistics;

public class GetUbistakeEarnedCoinsRequest : IRequest<decimal> {
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}