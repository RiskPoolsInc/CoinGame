using App.Core.Commands.Database;
using App.Core.Requests.Database;
using App.Interfaces.Core;
using App.Web.Core;
using App.Web.Core.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers
{
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/database"), Produces(SupportedMimeTypes.Json)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
    public class DatabaseController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public DatabaseController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Get Migrations List
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns the dictionary with all pending migrations split by the context</returns>
        /// <response code="200">Dictionary with all pending migrations split by the context</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpGet]
        public async Task<IActionResult> IndexAsync(CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new GetMigrationsRequest(), cancellationToken));
        }

        /// <summary>
        /// Apply Migrations
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns the dictionary with all pending migrations split by the context</returns>
        /// <response code="200">Dictionary with all pending migrations split by the context</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpPost]
        public async Task<IActionResult> PostAsync(CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new MigrateDatabaseCommand(), cancellationToken));
        }
    }
}