using App.Core.Requests.Dictionaries.Housekeeping;
using App.Core.Requests.UsersProfiles;
using App.Core.ViewModels.Dictionaries;
using App.Interfaces.Core;
using App.Web.Core;
using App.Web.Core.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers
{
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/housekeeping"),
     Produces(SupportedMimeTypes.Json)]
    [ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
    public class HousekeepingController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public HousekeepingController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Get Locales
        /// </summary>
        /// <remarks>This method returns the list of all locales</remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of locales</returns>
        /// <response code="200">The list of locales</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("locales")]
        [ProducesResponseType(typeof(LocaleView[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLocalesAsync([FromQuery] GetLocalesRequest request,
            CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Get Country States
        /// </summary>
        /// <remarks>Method returns country states by country id and state name</remarks>
        /// <param name="request">The unique country identifier</param>
        /// <param name="countryId">The unique country identifier</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of country states</returns>
        /// <response code="200">Country states</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("countries/{countryId:int}/states")]
        [ProducesResponseType(typeof(CountryStateView[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountryStatesAsync(int countryId, [FromQuery] GetStatesRequest request,
            CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new GetCountryStatesRequest(countryId, request?.StateName),
                cancellationToken));
        }

        /// <summary>
        /// Get Countries
        /// </summary>
        /// <remarks>This method returns the list of all countries</remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of countries</returns>
        /// <response code="200">The list of countries</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("countries")]
        [ProducesResponseType(typeof(CountryView[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountriesAsync([FromQuery] GetCountriesRequest request,
            CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Get Currencies
        /// </summary>
        /// <remarks>This method returns the list of all currencies</remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of currencies</returns>
        /// <response code="200">The list of currencies</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("currencies")]
        [ProducesResponseType(typeof(CurrencyView[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrenciesAsync([FromQuery] GetCurrenciesRequest request,
            CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Get User Profile types
        /// </summary>
        /// <remarks>This method returns the list of all User Profile types</remarks>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of currencies</returns>
        /// <response code="200">The list of User Profile Types</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("user-types")]
        [ProducesResponseType(typeof(UserTypeView[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserTypes(CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new GetUserProfileTypesRequest(), cancellationToken));
        }
    }
}