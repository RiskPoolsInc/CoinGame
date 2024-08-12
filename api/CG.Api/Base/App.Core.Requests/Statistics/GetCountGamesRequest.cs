namespace App.Core.Requests.Statistics;

public class GetCountGamesRequest : IRequest<int> {
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}