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
        /// Generate wallet
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
        /// Get wallet info
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns created wallet</returns>
        /// <response code="200">The user games</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetWalletInfoAsync([FromRoute] Guid id, CancellationToken cancellationToken) {
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
        [HttpPut("refund")]
        public async Task<IActionResult> RefundCoinsAsync([FromBody] RefundCoinsCommand request,
                                                          CancellationToken             cancellationToken) {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }
    }
}