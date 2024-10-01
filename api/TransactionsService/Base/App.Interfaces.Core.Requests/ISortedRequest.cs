namespace App.Interfaces.Core.Requests; 

public interface ISortedRequest {
    string Sort { get; }
    int Direction { get; }
}