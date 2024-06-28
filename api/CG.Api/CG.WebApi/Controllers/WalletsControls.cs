using App.Core.Commands.Database;
using App.Core.Commands.Transactions;
using App.Core.Commands.Wallets;
using App.Core.Requests.Database;
using App.Core.Requests.Games;
using App.Interfaces.Core;
using App.Web.Core;
using App.Web.Core.Errors;

using Microsoft.AspNetCore.Mvc;

namespace CG.WebApi.Controllers {
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
        /// Create wallet for games
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns created wallet</returns>
        /// <response code="200">The user games</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpPut("create")]
        public async Task<IActionResult> CreateWalletAsync(CreateWalletCommand request, CancellationToken cancellationToken) {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Refund coins
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns transaction refund</returns>
        /// <response code="200">The user games</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpPut("refund")]
        public async Task<IActionResult> RefundCoinsAsync([FromHeader] RefundCoinsCommand request,
                                                          CancellationToken                     cancellationToken) {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }
    }
}