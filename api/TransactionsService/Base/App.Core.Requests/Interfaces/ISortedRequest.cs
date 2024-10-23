namespace App.Core.Requests.Interfaces {
    public interface ISortedRequest {
        string Sort { get; }
        int Direction { get; }
    }
}
