﻿using App.Core.Commands.Games;
using App.Core.Commands.Transactions;
using App.Core.Requests.Games;
using App.Interfaces.Core;
using App.Web.Core;
using App.Web.Core.Errors;

using Microsoft.AspNetCore.Mvc;

namespace CG.WebApi.Controllers {
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/games"), Produces(SupportedMimeTypes.Json)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
    public class GamesController : BaseController {
        private readonly IDispatcher _dispatcher;

        public GamesController(IDispatcher dispatcher) {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Get Games
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns the user games</returns>
        /// <response code="200">The user games</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpGet]
        public async Task<IActionResult> GetGamesAsync([FromQuery] GetGamesRequest request, CancellationToken cancellationToken) {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Get Game
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns the user game</returns>
        /// <response code="200">The user game</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetGameAsync([FromRoute] Guid id, CancellationToken cancellationToken) {
            var request = new GetGameRequest(id);
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Get Current Game
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns the user games</returns>
        /// <response code="200">The user games</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentGameAsync([FromQuery] GetCurrentGameRequest request,
                                                             CancellationToken                 cancellationToken) {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Create new Game
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns the user game</returns>
        /// <response code="200">The user games</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpPut("new")]
        public async Task<IActionResult> CreateGameAsync([FromBody] CreateGameCommand request, CancellationToken cancellationToken) {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Run current Game
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns the user game</returns>
        /// <response code="200">The user games</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpPut("run")]
        public async Task<IActionResult> RunGameAsync([FromBody] RunGameCommand request, CancellationToken cancellationToken) {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Check game deposit transaction
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns the user game</returns>
        /// <response code="200">The user games</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpGet("deposit/check")]
        public async Task<IActionResult> CheckGameDepositTransactionAsync([FromQuery] CheckGameDepositTransactionCommand request,
                                                                          CancellationToken cancellationToken) {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }
    }
}