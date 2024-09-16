using App.Core.Commands.Wallets;
using App.Core.Requests.Wallets;
using App.Interfaces.Core;
using App.Web.Core;
using App.Web.Core.Errors;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

namespace TS.WebApi.Controllers;

[ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/wallets"), Produces(SupportedMimeTypes.Json)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
public class WalletsController : BaseController {
    private readonly IDispatcher _dispatcher;
    private readonly IMapper _mapper;

    public WalletsController(IDispatcher dispatcher, IMapper mapper) {
        _dispatcher = dispatcher;
        _mapper = mapper;
    }

    /// <summary>
    /// Generate new wallet
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns generated wallet</returns>
    /// <response code="200">The generated wallet</response>
    /// <response code="403">Access Denied</response>
    /// <response code="500">Unexpected server error</response>
    [HttpPost("generate")]
    public async Task<IActionResult> GenerateWalletAsync(CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new GenerateWalletCommand(), cancellationToken));
    }

    /// <summary>
    /// Import wallet
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns imported wallet</returns>
    /// <response code="200">The generated wallet</response>
    /// <response code="403">Access Denied</response>
    /// <response code="500">Unexpected server error</response>
    [HttpPost("import")]
    public async Task<IActionResult> ImportWalletAsync([FromBody] ImportWalletCommand request, CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(_mapper.Map<CreateImportedWalletCommand>(request), cancellationToken));
    }

    /// <summary>
    /// Get wallet balance by Wallet Id
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns created wallet</returns>
    /// <response code="200">The user games</response>
    /// <response code="403">Access Denied</response>
    /// <response code="500">Unexpected server error</response>
    [HttpGet("{id:guid}/balance")]
    public async Task<IActionResult> GetWalletBalanceAsync([FromRoute] Guid id, CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new GetWalletBalanceRequest(id), cancellationToken));
    }

    /// <summary>
    /// Refund coins
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns transaction refund</returns>
    /// <response code="200">The transaction refund</response>
    /// <response code="403">Access Denied</response>
    /// <response code="500">Unexpected server error</response>
    [HttpPost("{id:guid}/refund")]
    public async Task<IActionResult> RefundCoinsAsync([FromRoute] Guid id, CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new RefundCoinsCommand(id), cancellationToken));
    }
}