namespace App.Core.Requests.Handlers.Helpers;

public static class FilterRequests {
    public static Guid FirstOrDefaultFromGuidArray(this Guid[] arr) {
        if (arr?.Length > 0)
            return arr.FirstOrDefault();

        return Guid.Empty;
    }
}