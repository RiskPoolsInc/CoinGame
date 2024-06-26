using App.Core.Commands.Telegram;
using App.Core.Requests.Telegram;
using App.Interfaces.Core;
using App.Interfaces.Security;
using App.Web.Core;
using App.Web.Core.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Authorize]
[Route("api/v{version:apiVersion}/telegram")]
[Produces(SupportedMimeTypes.Json)]
[ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
public class TelegramController : BaseController
{
    private readonly IContextProvider _contextProvider;
    private readonly IDispatcher _dispatcher;

    public TelegramController(IDispatcher dispatcher, IContextProvider contextProvider)
    {
        _dispatcher = dispatcher;
        _contextProvider = contextProvider;
    }

    /// <summary>
    ///     Send message to telegram user
    /// </summary>
    /// <remarks>Method to send message to telegram user</remarks>
    /// <param name="request">Telegram message data</param>
    /// <param name="cancellationToken">cancellation token</param>
    /// <returns>Telegram response</returns>
    /// <response code="200"></response>
    /// <response code="400">Payload is invalid</response>
    /// <response code="500">Unhandled server error</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendTelegramMessageAsync([FromBody] TelegramMessageCommand request,
        CancellationToken cancellationToken)
    {
        return Ok(await _dispatcher.Send(new SendTelegramMessageCommand(
            _contextProvider.Context.UserProfileId.ToString("D"),
            request), cancellationToken));
    }

    [HttpPost("task-published")]
    [AllowAnonymous]
    public async Task<IActionResult> GenerateTelegramTaskMessage([FromBody] SendTgmTaskPublishedCommand command)
    {
        return Ok(await _dispatcher.Send(command));
    }

    /// <summary>
    ///     Check message from account
    /// </summary>
    /// <remarks>Method to send message to telegram user</remarks>
    /// <param name="request">Telegram message data</param>
    /// <param name="cancellationToken">cancellation token</param>
    /// <returns>Telegram response</returns>
    /// <response code="200"></response>
    /// <response code="400">Payload is invalid</response>
    /// <response code="500">Unhandled server error</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckTelegramMessageAsync([FromQuery] GetTelegramMessageRequest request,
        CancellationToken cancellationToken)
    {
        var command = new TelegramMessageCommand
        {
            Message = Guid.NewGuid().ToString("N")[..4],
            Receiver = request.Receiver
        };

        await _dispatcher.Send(new SendTelegramMessageCommand(_contextProvider.Context.UserProfileId.ToString("D"),
            command), cancellationToken);

        return Ok(await _dispatcher.Send(new CheckTelegramMessageCommand
        {
            Message = command.Message,
            Receiver = request.Receiver,
            Sender = _contextProvider.Context?.UserProfileId.ToString("D") ?? ""
        }, cancellationToken));
    }
}