using App.Core.Commands.ReferralPairs;
using App.Core.Requests.ReferralPairs;
using App.Core.ViewModels.ReferralPairs;
using App.Data.Entities.Dictionaries;
using App.Interfaces.Core;
using App.Interfaces.Core.Requests;
using App.Web.Core;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers; 

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ReferralPairsController : BaseController {
    private readonly IDispatcher _dispatcher;

    public ReferralPairsController(IDispatcher dispatcher) {
        _dispatcher = dispatcher;
    }

    [HttpPost]
    [ProducesResponseType(typeof(IPagedList<ReferralPairView>), StatusCodes.Status200OK)]
    public async Task<IActionResult>
        GetReferralPairsAsync([FromBody] GetReferralPairsRequest request, CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    [HttpGet("types")]
    [ProducesResponseType(typeof(ReferralType[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetReferralTypesAsync(CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new GetReferralTypesRequest(), cancellationToken));
    }

    [HttpPost("complete")]
    [ProducesResponseType(typeof(ReferralPairView), StatusCodes.Status200OK)]
    public async Task<IActionResult> CompleteReferralPairsAsync([FromBody] CompleteReferralCommand request,
                                                                CancellationToken                  cancellationToken) {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }
}