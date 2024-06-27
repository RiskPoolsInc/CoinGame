namespace App.Core.Enums; 

public enum SubscriptionStatusEnum {
    NotStarted = 0,
    PendingFulfillmentStart,
    Subscribed,
    Suspended,
    Unsubscribed
}