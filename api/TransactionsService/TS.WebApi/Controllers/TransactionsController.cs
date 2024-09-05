using App.Core.Commands.Transactions;
using App.Core.Commands.Wallets;
using App.Interfaces.Core;
using App.Web.Core;
using App.Web.Core.Errors;

using Microsoft.AspNetCore.Mvc;

namespace TS.WebApi.Controllers {
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/transactions"), Produces(SupportedMimeTypes.Json)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
    public class TransactionsController : BaseController {
        private readonly IDispatcher _dispatcher;

        public TransactionsController(IDispatcher dispatcher) {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Create transaction
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns created wallet</returns>
        /// <response code="200">The user games</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpPost("generate")]
        public async Task<IActionResult> CreateWalletAsync([FromBody] GenerateTransactionCommand request,CancellationToken cancellationToken) {
            return Ok(await _dispatcher.Send(new CreateWalletCommand(), cancellationToken));
        }
    }
}