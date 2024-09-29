using App.Core.Commands;
using App.Core.Commands.AutoPaymentServiceCommands;
using App.Core.Exceptions;
using App.Core.Requests.AutoPaymentServiceRequests;
using App.Interfaces.Core;
using App.Services.AutoPayment;
using App.Web.Core;
using App.Web.Core.Errors;

using Microsoft.AspNetCore.Mvc;

namespace CG.WebApi.Controllers {
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/services"), Produces(SupportedMimeTypes.Json)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
    public class ServicesController : BaseController {
        private readonly IDispatcher _dispatcher;
        private readonly IAutoPaymentServiceOptions _autoPaymentServiceOptions;

        public ServicesController(IDispatcher dispatcher, IAutoPaymentServiceOptions autoPaymentServiceOptions) {
            _dispatcher = dispatcher;
            _autoPaymentServiceOptions = autoPaymentServiceOptions;
        }

        /// <summary>
        /// Get service status
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns status for AutoPayment servcie</returns>
        /// <response code="200">The status of service</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpGet("status")]
        public async Task<IActionResult> GetWalletInfoAsync([FromHeader] string accessKey, CancellationToken cancellationToken) {
            CheckAccessKey(accessKey);
            return Ok(await _dispatcher.Send(new GetAutoPaymentServiceStatusRequest(), cancellationToken));
        }

        /// <summary>
        /// Run AutoPayment Service
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="accessKey">Access Key for manage service</param>
        /// <response code="200">The status of service</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpPost("run")]
        public async Task<IActionResult> RunAsync([FromHeader] string accessKey, CancellationToken cancellationToken) {
            CheckAccessKey(accessKey);
            await _dispatcher.Send(new AutoPaymentServiceRunCommand(), cancellationToken);
            return Ok(await _dispatcher.Send(new GetAutoPaymentServiceStatusRequest(), cancellationToken));
        }

        /// <summary>
        /// Stop AutoPayment Service
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="accessKey">Access Key for manage service</param>
        /// <response code="201">Service is stopped successful</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpPost("stop")]
        public async Task<IActionResult> StopAsync([FromHeader] string accessKey, CancellationToken cancellationToken) {
            CheckAccessKey(accessKey);
            await _dispatcher.Send(new AutoPaymentServiceStopCommand(), cancellationToken);
            return Ok(await _dispatcher.Send(new GetAutoPaymentServiceStatusRequest(), cancellationToken));
        }

        private void CheckAccessKey(string accessKey) {
            if (accessKey != _autoPaymentServiceOptions.AccessKey)
                throw new AccessDeniedException("Invalid access key");
        }
    }
}