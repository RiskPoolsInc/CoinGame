using App.Security.Annotation;

namespace App.Core.Commands.Notifications; 

[Access]
public class DeleteNotificationsCommand : IRequest<bool> {
}