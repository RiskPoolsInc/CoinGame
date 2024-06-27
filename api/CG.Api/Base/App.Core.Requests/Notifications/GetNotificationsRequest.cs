using App.Core.ViewModels.Notifications;

namespace App.Core.Requests.Notifications; 

public class GetNotificationsRequest : IRequest<PagedList<NotificationView>> {
    public GetNotificationsRequest() {
        Take = 30;
        Skip = 0;
    }

    public int? Take { get; set; }
    public int? Skip { get; set; }
}