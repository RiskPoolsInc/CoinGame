namespace App.Common.Helpers;

public static class DateTimeHelper {
    private static readonly DateTime _unixStartTime = new(1970, 1, 1, 0,
                                                          0, 0, DateTimeKind.Utc);

    public static double ToEpoch(this DateTime datetime) {
        return (datetime - _unixStartTime).TotalSeconds;
    }

    public static DateTime ToDate(this double epoch) {
        return _unixStartTime.AddSeconds(epoch);
    }
}