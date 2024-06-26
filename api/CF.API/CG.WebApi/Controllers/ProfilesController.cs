using App.Core.Commands.UserProfiles;
using App.Core.Requests.UsersProfiles;
using App.Core.ViewModels.Users;
using App.Data.Entities.UserProfiles;
using App.Interfaces.Core;
using App.Web.Core;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers; 

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProfilesController : BaseController {
    private readonly IDispatcher _dispatcher;

    public ProfilesController(IDispatcher dispatcher) {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    [Route("ubikiri-users/{ubikiriUserId:guid}")]
    [ProducesResponseType(typeof(UserProfile), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserProfileByUbikiriUserIdAsync(Guid              ubikiriUserId,
                                                                        CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new GetUserProfileByUbikiriIdRequest(ubikiriUserId), cancellationToken));
    }

    [HttpGet]
    [Route("me")]
    [ProducesResponseType(typeof(UserProfileView), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrentUserProfile(CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new GetCurrentUserProfileRequest(), cancellationToken));
    }

    [HttpGet]
    [Route("me/count-requests")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrentUserCountRequests(CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new GetUserRequestsCountRequest(), cancellationToken));
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(typeof(UserProfileView), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserProfileCommand request,
                                                 CancellationToken                   cancellationToken) {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    [HttpPut]
    [Route("referral")]
    [ProducesResponseType(typeof(UserProfileView), StatusCodes.Status200OK)]
    public async Task<IActionResult> GenerateReferralAsync(CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new GenerateReferralIdCommand(), cancellationToken));
    }

    [HttpPut]
    [Route("email")]
    [ProducesResponseType(typeof(UserProfileView), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateUserEmailAsync([FromBody] UpdateUserProfileEmailCommand request,
                                                          CancellationToken                        cancellationToken) {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    [HttpPut]
    [Route("update/social-properties")]
    [ProducesResponseType(typeof(UserProfileView), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateUserTelegramTwitterAsync([FromBody] UpdateProfileTwitterTelegramCommand request,
                                                                    CancellationToken                              cancellationToken) {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }

    [HttpGet]
    [Route("referral")]
    [ProducesResponseType(typeof(UserProfileView), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCountInvitedAsync(CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new GetCountInvitedRequest(), cancellationToken));
    }

    [HttpPost]
    [Route("add-wallet-to-profile")]
    [ProducesResponseType(typeof(UserProfile), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddWalletToProfile([FromBody] UpdateUserProfileWalletCommand request,
                                                        CancellationToken                         cancellationToken) {
        return Ok(await _dispatcher.Send(request, cancellationToken));
    }
}