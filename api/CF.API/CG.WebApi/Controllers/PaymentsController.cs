using App.Core.Commands.Payments;
using App.Core.Commands.Payments.ReferralPairs;
using App.Core.Commands.Payments.TaskRequests;
using App.Core.Requests.Transactions;
using App.Core.ViewModels.TaskTakeRequests;
using App.Interfaces.Core;
using App.Web.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PaymentsController : BaseController
{
    private readonly IDispatcher _dispatcher;

    public PaymentsController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost]
    [Route("take-requests/create")]
    [ProducesResponseType(typeof(TaskTakeRequestView[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateTaskRequestsPayments([FromBody] CreateTransactionRequestsCommand requests,
        CancellationToken cancellationToken)
    {
        return Ok(await _dispatcher.Send(requests, cancellationToken));
    }


    [HttpPost]
    [Route("referrals/create")]
    [ProducesResponseType(typeof(TaskTakeRequestView[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateReferralPairsPayments([FromBody] CreateTransactionReferralsCommand referral,
        CancellationToken cancellationToken)
    {
        return Ok(await _dispatcher.Send(referral, cancellationToken));
    }

    [HttpGet]
    [Route("transactions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult>
        GetTransactionPayments([FromQuery] GetTransactionsRequest request, CancellationToken cancellationToken)
    {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    [HttpPut]
    [Route("transactions/{id:guid}/block-chain/check")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTransactionPayments([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        return Ok(await _dispatcher.Send(new CheckExistTransactionInBlockChainCommand(id), cancellationToken));
    }
}