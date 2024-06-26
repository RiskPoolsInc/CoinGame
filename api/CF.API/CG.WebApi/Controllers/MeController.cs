using App.Core.Commands.Images;
using App.Core.Commands.Users;
using App.Core.Requests.Images;
using App.Core.Requests.Users;
using App.Core.ViewModels.Images;
using App.Core.ViewModels.Security;
using App.Interfaces.Core;
using App.Web.Core;
using App.Web.Core.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers
{
    [ApiController, ApiVersion("1.0"), Authorize, Route("api/v{version:apiVersion}/me"), Produces(SupportedMimeTypes.Json)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
    public class  MeController : BaseController
    {
        public MeController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        private readonly IDispatcher _dispatcher;


        /// <summary>
        /// Change Current Password
        /// </summary>
        /// <remarks>Methods changes current password for the authenticated user</remarks>
        /// <param name="request">Request payload</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Profile Data</returns>
        /// <response code="204"></response>
        /// <response code="400">Payload is invalid</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangeMyPasswordCommand request, CancellationToken cancellationToken)
        {
            await _dispatcher.Send(request, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Get Profile data
        /// </summary>
        /// <remarks>Methods returns all profile data, related to the authenticated user</remarks>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Profile Data</returns>
        /// <response code="200">Profile Data</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("profile")]
        [ProducesResponseType(typeof(UserView), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProfileAsync(CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new GetUserProfileRequest(), cancellationToken));
        }
         
        /// <summary>
        /// Get Profile data from client context
        /// </summary>
        /// <remarks>Methods returns all profile data from client context, related to the authenticated user</remarks>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Profile Data</returns>
        /// <response code="200">Profile Data</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("context-profile")]
        [ProducesResponseType(typeof(UserView), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserProfileContextData(CancellationToken cancellationToken) {
            return Ok(await _dispatcher.Send(new GetUserProfileContextDataRequest(), cancellationToken));
        }
        
         /// <summary>
        /// Create Profile Image
        /// </summary>
        /// <remarks>Method creates user image by file</remarks>
        /// <param name="model">uploaded file</param>
        /// <param name="version"></param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Created User Image Link</returns>
        /// <response code="201">User Image Link</response>
        /// <response code="400">Payload is invalid</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("image")]
        [ProducesResponseType(typeof(ImageView), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
        [Consumes(SupportedMimeTypes.FormData)]
        public async Task<IActionResult> CreateImageAsync([FromForm] ImageFileModel model, ApiVersion version, CancellationToken cancellationToken) {
            var response = await _dispatcher.Send(new CreateProfileImageCommand(model), cancellationToken);
            return CreatedAtRoute(RouteNames.Profile.Image.GetById, new { id = response.Id, version = $"{version}" }, response);
        }

        /// <summary>
        /// Get Profile Image View
        /// </summary>
        /// <remarks>Method returns the temporary link to image and thumbnail</remarks>
        /// <param name="request">TTL</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Image View</returns>
        /// <response code="200">Image link</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="404">Requested attachment is not found</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("image", Name = RouteNames.Profile.Image.GetById)]
        [ProducesResponseType(typeof(ImageView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetImageLinkAsync([FromQuery] ImageLinkParams request, CancellationToken cancellationToken) {
            return Ok(await _dispatcher.Send(new GetProfileImageRequest(request), cancellationToken));
        }
    }
}