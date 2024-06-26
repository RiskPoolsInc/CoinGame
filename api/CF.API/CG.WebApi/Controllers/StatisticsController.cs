using System.Net;
using System.Text.Json;

using App.Core.Commands.UserLogs;
using App.Core.Exceptions;
using App.Core.Requests.Statistics;
using App.Core.ViewModels.Statistics;
using App.Interfaces.Core;
using App.Web.Core;
using App.Web.Core.Errors;

using CF.WebApi.Configuration;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[AllowAnonymous]
[Produces(SupportedMimeTypes.Json)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
public class StatisticsController : BaseController {
    private readonly IDispatcher _dispatcher;
    private readonly ExternalSystemsApiKeys _externalSystemsApiKeys;

    public StatisticsController(IDispatcher dispatcher, ExternalSystemsApiKeys externalSystemsApiKeys) {
        _dispatcher = dispatcher;
        _externalSystemsApiKeys = externalSystemsApiKeys;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("referrals/open")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> CreateTaskAsync([FromBody] CreateReferralVisitCommand request,
                                                     CancellationToken                     cancellationToken) {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("referrals/check/complete")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> CreateTaskAsync([FromBody] CreateReferralCheckedCommand request,
                                                     CancellationToken                       cancellationToken) {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    [HttpPost]
    [Route("users/login")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Consumes(SupportedMimeTypes.Json)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateLoginAsync([FromBody] CreateLoginCommand request,
                                                      CancellationToken             cancellationToken) {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    [HttpGet]
    [Route("active-users")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetActiveUsers(CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new GetCountActiveUsersRequest(), cancellationToken));
    }

    [HttpGet]
    [Route("active-tasks")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetActiveTasks(CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new GetCountActiveTasksRequest(), cancellationToken));
    }

    [HttpGet]
    [Route("total-payments")]
    [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> GetTotalPayments(CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new GetSumAllPaymentsRequest(), cancellationToken));
    }

    /// <summary>
    /// Get Average Tasks Award
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [Route("analytics/tasks/payments/average")]
    [ProducesResponseType(typeof(StatisticDecimalResult), StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> GetAverageTaskPaymentAsync([FromBody] GetAverageTaskPaymentStats request,
                                                                CancellationToken                     cancellationToken) {
        CheckApiKey(request.ApiKey);
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    /// <summary>
    /// Get Average Completed Tasks By User
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [Route("analytics/users/completed-tasks/average")]
    [ProducesResponseType(typeof(StatisticIntResult), StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> GetAverageCompletedTasksByUserAsync([FromBody] GetAverageCompletedTasksByUserStats request,
                                                                         CancellationToken                              cancellationToken) {
        CheckApiKey(request.ApiKey);
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    /// <summary>
    /// Get Completed Tasks
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [Route("analytics/tasks/completed/count")]
    [ProducesResponseType(typeof(StatisticIntResult), StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> GetCompletedTasksAsync([FromBody] GetCompletedTasksStats request,
                                                            CancellationToken                 cancellationToken) {
        CheckApiKey(request.ApiKey);
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    /// <summary>
    /// Get New Tasks
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [Route("analytics/tasks/new/count")]
    [ProducesResponseType(typeof(StatisticIntResult), StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> GetNewTasksAsync([FromBody] GetNewTasksStats request,
                                                      CancellationToken           cancellationToken) {
        CheckApiKey(request.ApiKey);
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    /// <summary>
    /// Get New Users
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [Route("analytics/users/new/count")]
    [ProducesResponseType(typeof(StatisticIntResult), StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> GetNewUsersAsync([FromBody] GetNewUsersStats request,
                                                      CancellationToken           cancellationToken) {
        CheckApiKey(request.ApiKey);
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    /// <summary>
    /// Get All Analytics
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [Route("analytics/all")]
    [ProducesResponseType(typeof(AnalyticsResultView), StatusCodes.Status200OK)]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> GetAllAnalyticsAsync([FromBody] GetAllAnalyticsStats request,
                                                          CancellationToken               cancellationToken) {
        CheckApiKey(request.ApiKey);

        var result = Ok(new AnalyticsResultView {
            AverageTaskPayment = await _dispatcher.Send(new GetAverageTaskPaymentStats {
                FromDate = request.FromDate,
                ToDate = request.ToDate
            }, cancellationToken),
            AverageCompletedTasksByUser = await _dispatcher.Send(new GetAverageCompletedTasksByUserStats {
                FromDate = request.FromDate,
                ToDate = request.ToDate
            }, cancellationToken),
            CompletedTasks = await _dispatcher.Send(new GetCompletedTasksStats {
                FromDate = request.FromDate,
                ToDate = request.ToDate
            }, cancellationToken),
            NewTasks = await _dispatcher.Send(new GetNewTasksStats {
                FromDate = request.FromDate,
                ToDate = request.ToDate
            }, cancellationToken),
            NewUsers = await _dispatcher.Send(new GetNewUsersStats {
                FromDate = request.FromDate,
                ToDate = request.ToDate
            }, cancellationToken),
        });
        return result;
    }

    private void CheckApiKey(string key) {
        if (!_externalSystemsApiKeys.ApiKeys?.Any(s => s.Key == key) ?? false)
            throw new AccessDeniedException("Incorrect api key");
    }

    private class PricePagedList {
        public int totalPages { get; set; }
        public int totalRows { get; set; }
        public Price[] page { get; set; }
    }

    private class Price {
        public string symbol { get; set; }
        public decimal price { get; set; }
    }
}