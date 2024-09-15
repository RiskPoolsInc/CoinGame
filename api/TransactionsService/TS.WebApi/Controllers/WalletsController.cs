using App.Core.Commands.Wallets;
using App.Core.Requests.Wallets;
using App.Interfaces.Core;
using App.Web.Core;
using App.Web.Core.Errors;

using Microsoft.AspNetCore.Mvc;

namespace TS.WebApi.Controllers {
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/wallets"), Produces(SupportedMimeTypes.Json)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
    public class WalletsController : BaseController {
        private readonly IDispatcher _dispatcher;

        public WalletsController(IDispatcher dispatcher) {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Generate new wallet
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns generated wallet</returns>
        /// <response code="200">The generated wallet</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpPut("generate")]
        public async Task<IActionResult> CreateWalletAsync(CancellationToken cancellationToken) {
            return Ok(await _dispatcher.Send(new GenerateWalletCommand(), cancellationToken));
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
        /// Get wallet balance by address
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns wallet balance</returns>
        /// <response code="200">The wallet balance</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpGet("{address:string}/balance")]
        public async Task<IActionResult> GetWalletBalanceByAddressAsync([FromRoute] string address, CancellationToken cancellationToken) {
            return Ok(await _dispatcher.Send(new GetBalanceByAddressRequest(address), cancellationToken));
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
        public async Task<IActionResult> RefundCoinsAsync(Guid id, CancellationToken cancellationToken) {
            return Ok(await _dispatcher.Send(new RefundCoinsCommand(id), cancellationToken));
        }
    }
}