using App.Core.Commands.Notifications;
using App.Core.Requests.Notifications;
using App.Core.ViewModels;
using App.Core.ViewModels.Notifications;
using App.Interfaces.Core;
using App.Web.Core;
using App.Web.Core.Errors;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers; 

[ApiController]
[ApiVersion("1.0")]
[Authorize]
[Route("api/v{version:apiVersion}/notifications")]
[Produces(SupportedMimeTypes.Json)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
[ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
public class NotificationsController : BaseController {
    private readonly IDispatcher _dispatcher;

    public NotificationsController(IDispatcher dispatcher) {
        _dispatcher = dispatcher;
    }

    /// <summary>
    ///     Get Notifications
    /// </summary>
    /// <remarks>This method returns the list of notifications</remarks>
    /// <param name="request">request parameters</param>
    /// <param name="cancellationToken">cancellation token</param>
    /// <returns>The list of notifications</returns>
    /// <response code="200">The list of campaigns</response>
    /// <response code="401">Unauthorized access, no access token provided by a client</response>
    /// <response code="403">Resource forbidden.</response>
    /// <response code="500">Unhandled server error</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedList<NotificationView>), StatusCodes.Status200OK)]
    public async Task<IActionResult> IndexAsync([FromQuery] GetNotificationsRequest request, CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    /// <summary>
    ///     Dismiss All Notifications
    /// </summary>
    /// <remarks>Method dismises all notifications for user</remarks>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Empty Response</returns>
    /// <response code="200">Empty Response</response>
    /// <response code="401">Unauthorized access, no access token provided by a client</response>
    /// <response code="403">Resource forbidden.</response>
    /// <response code="500">Unhandled server error</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DismisAllAsync(CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new DeleteNotificationsCommand(), cancellationToken));
    }

    /// <summary>
    ///     Dismiss Notification
    /// </summary>
    /// <remarks>Method dismises notification by its Id</remarks>
    /// <param name="id">Notification Unique Id</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Empty Response</returns>
    /// <response code="200">Empty Response</response>
    /// <response code="401">Unauthorized access, no access token provided by a client</response>
    /// <response code="403">Resource forbidden.</response>
    /// <response code="404">Requested notification is not found</response>
    /// <response code="500">Unhandled server error</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DismisAsync(Guid id, CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new DeleteNotificationCommand(id), cancellationToken));
    }
}