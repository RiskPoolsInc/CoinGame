using App.Core.Commands.Payments;
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
        [HttpPost("send")]
        public async Task<IActionResult> GenerateTransactionsync([FromBody] GenerateTransactionCommand request,
                                                                 CancellationToken                     cancellationToken) {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Check completing transaction
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns result of completeing blockahin</returns>
        /// <response code="200">The result of check transaction in completeds</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpPost("completed")]
        public async Task<IActionResult> CheckCompletingAsync([FromBody] CheckTransactionStateCommand request,
                                                              CancellationToken                       cancellationToken) {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Get fee for transaction
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns result of calculating fee for transaction</returns>
        /// <response code="200">The result of calculating fee for transaction</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpPost("fee")]
        public async Task<IActionResult> GetFeeAsync([FromBody] CalculateTransactionFeeCommand request,
                                                     CancellationToken                         cancellationToken) {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }
    }
}