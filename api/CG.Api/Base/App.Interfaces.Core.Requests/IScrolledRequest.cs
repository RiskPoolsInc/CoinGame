namespace App.Interfaces.Core.Requests; 

public interface IScrolledRequest : ISortedRequest {
    string Token { get; }
    int? Size { get; }
}