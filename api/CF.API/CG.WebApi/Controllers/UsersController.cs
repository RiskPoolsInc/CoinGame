using App.Core.Commands.ExternalSystems.Users;
using App.Core.Commands.Images;
using App.Core.Commands.Notifications;
using App.Core.Commands.UserProfiles;
using App.Core.Commands.Users;
using App.Core.Requests.Dictionaries;
using App.Core.Requests.Images;
using App.Core.Requests.ReferralPairs;
using App.Core.Requests.TaskExecutions;
using App.Core.Requests.Tasks;
using App.Core.Requests.Users;
using App.Core.Requests.UsersProfiles;
using App.Core.ViewModels;
using App.Core.ViewModels.ChangeReasons;
using App.Core.ViewModels.Dictionaries;
using App.Core.ViewModels.Notifications;
using App.Core.ViewModels.ReferralPairs;
using App.Core.ViewModels.Security;
using App.Core.ViewModels.Users;
using App.Interfaces.Core;
using App.Interfaces.Core.Requests;
using App.Web.Core;
using App.Web.Core.Errors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiVersion("1.0"), Produces(SupportedMimeTypes.Json)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class UsersController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public UsersController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Get Users
        /// </summary>
        /// <remarks>This method returns the list of users</remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of users</returns>
        /// <response code="200">The list of users</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedList<UserProfileView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> IndexAsync([FromQuery] GetUserProfilesRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Get User
        /// </summary>
        /// <remarks>Method returns the user by unique Id</remarks>
        /// <param name="id">The unique identifier</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>User</returns>
        /// <response code="200">User</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="404">Request user is not found</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("{id:guid}", Name = RouteNames.Users.GetById)]
        [ProducesResponseType(typeof(UserView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new GetUserRequest(id), cancellationToken));
        }

        /// <summary>
        /// Get last reason block
        /// </summary>
        /// <remarks>Method returns the reason Users block by unique user Id</remarks>
        /// <param name="id">The unique identifier</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>User</returns>
        /// <response code="200">User</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="404">Request user is not found</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("{id:guid}/last-block")]
        [ProducesResponseType(typeof(ChangeReasonView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLastReasonBlockAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _dispatcher.Send(new GetUserBlockHistoryRequest(id), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get user rejected executions
        /// </summary>
        /// <remarks>Method returns the rejected execution for User by unique user Id</remarks>
        /// <param name="id">The unique identifier</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>User</returns>
        /// <response code="200">User</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="404">Request user is not found</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("{id:guid}/rejected-executions")]
        [ProducesResponseType(typeof(ChangeReasonView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRejectedExecutionsAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _dispatcher.Send(new GetUserRejectedExecutions(id), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Reset User Password
        /// </summary>
        /// <remarks>Method set a new password to user login</remarks>
        /// <param name="id">The unique user identifier</param>
        /// <param name="request">New Password</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Success</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Payload is invalid</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("{id:guid}/new-password")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetNewPasswordAsync(Guid id,
            [FromBody] UserNewPasswordCommand request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <remarks>Method creates user by passed payload to the body</remarks>
        /// <param name="request">Payload</param>
        /// <param name="version"></param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Created User</returns>
        /// <response code="201">v</response>
        /// <response code="400">Payload is invalid</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("create")]
        [ProducesResponseType(typeof(UserView), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserCommand request,
            ApiVersion version,
            CancellationToken cancellationToken
        )
        {
            var created = await _dispatcher.Send(request, cancellationToken);
            return CreatedAtRoute(RouteNames.Users.GetById, new {id = created.Id, version = $"{version}"}, created);
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <remarks>Method updates user by passed payload to the body</remarks>
        /// <param name="request">Payload</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Updated User</returns>
        /// <response code="200">User</response>
        /// <response code="400">Payload is invalid</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="404">Requested user is not found</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPut]
        [ProducesResponseType(typeof(UserView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserCommand request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Get MFA Types
        /// </summary>
        /// <remarks>Methods provides MFA types</remarks>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>MFA Types</returns>
        /// <response code="200">The list of MFA types</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("mfatypes")]
        [ProducesResponseType(typeof(MultiFactorAuthTypeView[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMFATypesAsync(CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new GetMFATypesRequest(), cancellationToken));
        }

        /// <summary>
        /// Create User Image
        /// </summary>
        /// <remarks>Method creates user image by file</remarks>
        /// <param name="id">user unique identifier</param>
        /// <param name="model">uploaded file</param>
        /// <param name="version"></param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Created User Image Link</returns>
        /// <response code="201">User Image Link</response>
        /// <response code="400">Payload is invalid</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("{id:guid}/image")]
        [ProducesResponseType(typeof(ExternalLink), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
        [Consumes(SupportedMimeTypes.FormData)]
        public async Task<IActionResult> CreateImageAsync(Guid id,
            [FromForm] ImageFileModel model,
            ApiVersion version,
            CancellationToken cancellationToken
        )
        {
            var response = await _dispatcher.Send(new CreateUserImageCommand(id, model), cancellationToken);
            return CreatedAtRoute(RouteNames.Users.Image.GetById, new {id = response.Id, version = $"{version}"}, response);
        }

        /// <summary>
        /// Get Image Link
        /// </summary>
        /// <remarks>Method returns the temporary image link</remarks>
        /// <param name="id">The unique user identifier</param>
        /// <param name="request">TTL</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Image link</returns>
        /// <response code="200">Image link</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="404">Requested attachment is not found</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("{id:guid}/image", Name = RouteNames.Users.Image.GetById)]
        [ProducesResponseType(typeof(ExternalLink), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetImageLinkAsync(Guid id,
            [FromQuery] ImageLinkParams request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(new GetUserImageLinkRequest(id, request), cancellationToken));
        }

        /// <summary>
        /// Follow User
        /// </summary>
        /// <remarks>Method adds user to the followers</remarks>
        /// <param name="id">The unique user identifier</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Follower record</returns>
        /// <response code="200">Follower record</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("{id:guid}/follow")]
        [ProducesResponseType(typeof(FollowView), StatusCodes.Status200OK)]
        public async Task<IActionResult> FollowAsync(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new FollowUserCommand(id), cancellationToken));
        }

        /// <summary>
        /// Unfollow User
        /// </summary>
        /// <remarks>Method removes user from the followers</remarks>
        /// <param name="id">The unique user identifier</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Follower record</returns>
        /// <response code="200">Follower record</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpDelete("{id:guid}/follow")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ForgetAsync(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new ForgetUserCommand(id), cancellationToken));
        }

        /// <summary>
        /// Get Tasks Id list By User
        /// </summary>
        /// <remarks>This method returns the list of task ids. Used to get all tasks w/o traffic overhead</remarks>
        /// <param name="id">User Id</param>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of tasks ids</returns>
        /// <response code="200">The list of tasks ids</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("{id:guid}/tasks/ids")]
        [ProducesResponseType(typeof(Guid[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaskIdsAsync(Guid id,
            [FromQuery] GetTaskIdsRequest request,
            CancellationToken cancellationToken
        )
        {
            request.AssigneeId = new[] {id};
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Block users
        /// </summary>
        /// <remarks>This method set IsActive false and IsBlock true</remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of tasks ids</returns>
        /// <response code="204"></response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("block")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> BlockUsers([FromBody] BlockUsersByExecutionsCommand request,
            CancellationToken cancellationToken
        )
        {
            await _dispatcher.Send(request, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Get Blocked users
        /// </summary>
        /// <remarks>This method return paged list with blocked users</remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of tasks ids</returns>
        /// <response code="204"></response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("list/blocked")]
        [ProducesResponseType(typeof(PagedList<UserProfileView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> BlockUsers([FromBody] GetUserProfilesRequest request, CancellationToken cancellationToken)
        {
            request.IsBlocked = true;
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Restore blocked user
        /// </summary>
        /// <remarks>This method return paged list with blocked users</remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of tasks ids</returns>
        /// <response code="204"></response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPut("{id:guid}/restore-blocked")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RestoreBlockedUser([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await _dispatcher.Send(new RestoreBlockedUserCommand(id), cancellationToken);
            return NoContent();
        }

        /// <summary>
        ///  ...
        /// </summary>
        /// <remarks>This method return ... </remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>...</returns>
        /// <response code="200">...</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost]
        [ProducesResponseType(typeof(IPagedList<UserProfileView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPagedListAsync([FromBody] GetUserProfilesRequest request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        ///  Update user
        /// </summary>
        /// <remarks>This method return updated User Profile </remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>User Profile</returns>
        /// <response code="200">User Profile</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("update")]
        [ProducesResponseType(typeof(UserProfileView), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPagedListAsync([FromBody] UpdateUserProfileCommand request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        ///  ...
        /// </summary>
        /// <remarks>This method return ... </remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>...</returns>
        /// <response code="200">...</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("verifications/telegram/send")]
        [ProducesResponseType(typeof(UserProfileView), StatusCodes.Status200OK)]
        public async Task<IActionResult> SendUserTgmCodeAsync([FromBody] SendUserTelegramCodeCommand request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        ///  ...
        /// </summary>
        /// <remarks>This method return ... </remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>...</returns>
        /// <response code="200">...</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("verifications/telegram/check")]
        [ProducesResponseType(typeof(UserProfileView), StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckUserTgmCodeAsync([FromBody] CheckUserTgmCodeCommand request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        ///  ...
        /// </summary>
        /// <remarks>This method return ... </remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>...</returns>
        /// <response code="200">...</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("types")]
        [ProducesResponseType(typeof(IPagedList<UserProfileView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTypesAsync(CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new GetUserProfileTypesRequest(), cancellationToken));
        }

        /// <summary>
        ///  ...
        /// </summary>
        /// <remarks>This method return ... </remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>...</returns>
        /// <response code="200">...</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("check-bots")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckBotsAsync([FromBody] CheckBotsCommand request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        ///  ...
        /// </summary>
        /// <remarks>This method return ... </remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>...</returns>
        /// <response code="200">...</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("ref-info")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [Consumes(SupportedMimeTypes.Json)]
        public async Task<IActionResult> RefInfoAsync([FromBody] GetUsersReferralsInfo request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        ///  ...
        /// </summary>
        /// <remarks>This method return ... </remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>...</returns>
        /// <response code="200">...</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("invited-referrals")]
        [ProducesResponseType(typeof(IPagedList<InvitedReferralView>), StatusCodes.Status200OK)]
        [Consumes(SupportedMimeTypes.Json)]
        public async Task<IActionResult> InvitedReferralsAsync([FromBody] GetInvitedReferralsRequest request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        ///  ...
        /// </summary>
        /// <remarks>This method return ... </remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>...</returns>
        /// <response code="200">...</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("bot-owner/{id:guid}")]
        [ProducesResponseType(typeof(IPagedList<InvitedReferralView>), StatusCodes.Status200OK)]
        [Consumes(SupportedMimeTypes.Json)]
        public async Task<IActionResult> InvitedReferralsAsync(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new SetUserAsBotOwnerCommand(id), cancellationToken));
        }
    }
}