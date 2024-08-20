﻿using App.Core.Requests.Statistics;
using App.Interfaces.Core;
using App.Web.Core;
using App.Web.Core.Errors;

using Microsoft.AspNetCore.Mvc;

namespace CG.WebApi.Controllers {
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/statistics"), Produces(SupportedMimeTypes.Json)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
    public class StatisticsController : BaseController {
        private readonly IDispatcher _dispatcher;

        public StatisticsController(IDispatcher dispatcher) {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Count Games
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns the games count</returns>
        /// <response code="200">The Count Games</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpGet("games/count")]
        public async Task<IActionResult> GetGamesCountAsync([FromQuery] GetCountGamesRequest request, CancellationToken cancellationToken) {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Sum of Games Rates
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns the sum games rates</returns>
        /// <response code="200">The Sum of Games Rates</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpGet("games/rates/sum")]
        public async Task<IActionResult> GetGamesRatesSumAsync([FromQuery] GetGamesRatesSumRequest request,
                                                               CancellationToken                   cancellationToken) {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Sum of Games Rewards
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns the sum games rewards</returns>
        /// <response code="200">The GamesRewardsSum</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpGet("games/rewards/sum")]
        public async Task<IActionResult> GetGamesRewardsSumAsync([FromQuery] GetGamesRewardsSumRequest request,
                                                                 CancellationToken                     cancellationToken) {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Sum of earned coins (2% from transactions)
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns the sum of earned coins (2% from transactions)</returns>
        /// <response code="200">The earned coins</response>
        /// <response code="403">Access Denied</response>s
        /// <response code="500">Unexpected server error</response>
        [HttpGet("coins/earned")]
        public async Task<IActionResult> GetCounsEarnedAsync([FromQuery] GetEarnedCoinsRequest request,
                                                             CancellationToken                 cancellationToken) {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }
        
        /// <summary>
        /// Wallet profit balance
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns the balance of wallet for profit (2% from transactions)</returns>
        /// <response code="200">v</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpGet("profit/balance")]
        public async Task<IActionResult> GetWalletProfitBalanceAsync(CancellationToken                 cancellationToken) {
            return Ok(await _dispatcher.Send(new GetWalletProfitBalanceRequest(), cancellationToken));
        }

        /// <summary>
        /// Sum of Ubistake Earned Games
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns the sum Ubistake earned coins</returns>
        /// <response code="200">The Sum of Ubistake Earned Games</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpGet("coins/ubistake/earned")]
        public async Task<IActionResult> GetCounsUbistakeEarnedAsync([FromQuery] GetUbistakeEarnedCoinsRequest request,
                                                                     CancellationToken                         cancellationToken) {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }
    }
}