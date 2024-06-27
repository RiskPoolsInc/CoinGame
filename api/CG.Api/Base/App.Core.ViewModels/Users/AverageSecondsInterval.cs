namespace App.Core.ViewModels.Users;

public class AverageSecondsInterval {
    public AverageSecondsInterval() {
    }

    public AverageSecondsInterval(int count, int minValue, int maxValue, int value) {
        this.count = count;
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.value = value;
    }

    public int count { get; set; }
    public int minValue { get; set; }
    public int maxValue { get; set; }
    public int value { get; set; }
}