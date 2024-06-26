using App.Core.Requests.Statistics;
using App.Interfaces.Core;
using App.Web.Core;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/admin-stats")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiVersion("1.0")]
[Produces(SupportedMimeTypes.Json)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
public class AdminStatsController : BaseController {
    private readonly IDispatcher _dispatcher;

    public AdminStatsController(IDispatcher dispatcher) {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    [Route("payments")]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> GetUserPaymentsStats([FromQuery] GetUserPaymentsStatsRequest statsRequest,
                                                          CancellationToken                       cancellationToken) {
        return Ok(await _dispatcher.Send(statsRequest, cancellationToken));
    }

    [HttpGet]
    [Route("referrals-invited")]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> GetUserReferralsStats([FromQuery] GetInvitedReferralStatsRequest request,
                                                           CancellationToken                          cancellationToken) {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    [HttpGet]
    [Route("task-payments")]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> GetTasksStats([FromQuery] GetUserTasksStatsRequest request,
                                                   CancellationToken                    cancellationToken) {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    [HttpGet]
    [Route("referrals-registrations")]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> GetReferralsRegistrations([FromQuery] GetReferralsRegistrations request,
                                                               CancellationToken                     cancellationToken) {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    [HttpGet]
    [Route("users-registrations")]
    [Consumes(SupportedMimeTypes.Json)]
    public async Task<IActionResult> GetUsersRegistrations([FromQuery] GetUsersRegistrations request,
                                                           CancellationToken                 cancellationToken) {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    [HttpGet]
    [Route("commission")]
    public async Task<IActionResult> GetCommissionStats([FromQuery] GetCommissionStatistics request,
                                                        CancellationToken                   cancellationToken) {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }
}