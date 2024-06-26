using App.Core.Commands.Attachments;
using App.Core.Requests.Attachments;
using App.Core.ViewModels;
using App.Core.ViewModels.Attachments;
using App.Interfaces.Core;
using App.Web.Core;
using App.Web.Core.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers; 

[ApiController]
[ApiVersion("1.0")]
[Authorize]
[Route("api/v{version:apiVersion}/attachments")]
[Produces(SupportedMimeTypes.Json)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
public class AttachmentsController : BaseController {
    private readonly IDispatcher _dispatcher;

    public AttachmentsController(IDispatcher dispatcher) {
        _dispatcher = dispatcher;
    }

    /// <summary>
    ///     Get Attachment
    /// </summary>
    /// <remarks>Method returns the attachment by unique Id</remarks>
    /// <param name="id">The unique identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Attachment</returns>
    /// <response code="200">Attachment</response>
    /// <response code="401">Unauthorized access, no access token provided by a client</response>
    /// <response code="403">Resource forbidden.</response>
    /// <response code="404">Requested attachment is not found</response>
    /// <response code="500">Unhandled server error</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AttachmentView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new GetAttachmentRequest(id), cancellationToken));
    }

    /// <summary>
    ///     Get Attachment
    /// </summary>
    /// <remarks>Method returns the attachment by unique object Id</remarks>
    /// <param name="id">The unique object identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Attachment</returns>
    /// <response code="200">Attachment</response>
    /// <response code="401">Unauthorized access, no access token provided by a client</response>
    /// <response code="403">Resource forbidden.</response>
    /// <response code="404">Requested attachment is not found</response>
    /// <response code="500">Unhandled server error</response>
    [HttpGet("object/{id:guid}")]
    [ProducesResponseType(typeof(AttachmentView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByObjectIdAsync(Guid id, CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new GetObjectAttachmentsRequest(id), cancellationToken));
    }

    /// <summary>
    ///     Get SAS Link
    /// </summary>
    /// <remarks>Method returns the temporary attachment link</remarks>
    /// <param name="id">The unique identifier</param>
    /// <param name="request">TTL</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Attachment link</returns>
    /// <response code="200">Attachment link</response>
    /// <response code="401">Unauthorized access, no access token provided by a client</response>
    /// <response code="403">Resource forbidden.</response>
    /// <response code="404">Requested attachment is not found</response>
    /// <response code="500">Unhandled server error</response>
    [HttpGet("{id:guid}/link")]
    [ProducesResponseType(typeof(ExternalLink), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLinkAsync(Guid id, [FromQuery] AttachmentLinkParams request, CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new GetAttachmentLinkRequest(id, request), cancellationToken));
    }

    /// <summary>
    ///     Get Blob
    /// </summary>
    /// <remarks>Method returns attachment blob</remarks>
    /// <param name="id">The unique identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Attachment BLOB</returns>
    /// <response code="200">Attachment BLOB</response>
    /// <response code="401">Unauthorized access, no access token provided by a client</response>
    /// <response code="403">Resource forbidden.</response>
    /// <response code="404">Requested attachment is not found</response>
    /// <response code="500">Unhandled server error</response>
    [HttpGet("{id:guid}/download")]
    [ProducesResponseType(typeof(ExternalLink), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDownloadAsync(Guid id, CancellationToken cancellationToken) {
        var blob = await _dispatcher.Send(new GetAttachmentBlobRequest(id), cancellationToken);
        return File(blob.File, blob.ContentType, blob.DisplayName);
    }

    /// <summary>
    ///     Delete Attachment
    /// </summary>
    /// <remarks>Method deletes attachments by its unique Id</remarks>
    /// <param name="id">Attachment Unique Id</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Deleted Attachment</returns>
    /// <response code="200">Deleted Attachment</response>
    /// <response code="401">Unauthorized access, no access token provided by a client</response>
    /// <response code="403">Resource forbidden.</response>
    /// <response code="404">Requested attachment is not found</response>
    /// <response code="500">Unhandled server error</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(AttachmentView), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new DeleteAttachmentCommand(id), cancellationToken));
    }

    /// <summary>
    ///     Create file directly to blob storage for testing purposes
    /// </summary>
    /// <remarks>Method creates incoming attachment by file</remarks>
    /// <param name="model">uploaded file</param>
    /// <param name="version"></param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Created Incoming Attachment</returns>
    /// <response code="201">Incoming Attachment</response>
    /// <response code="400">Payload is invalid</response>
    /// <response code="401">Unauthorized access, no access token provided by a client</response>
    /// <response code="403">Resource forbidden.</response>
    /// <response code="500">Unhandled server error</response>
    [HttpPost("direct")]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
    [Consumes(SupportedMimeTypes.FormData)]
    public async Task<IActionResult> CreateFileDirectlyAsync([FromForm] DirectFileCommand model, ApiVersion version,
                                                             CancellationToken            cancellationToken) {
        var response = await _dispatcher.Send(model, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    ///     Get SAS Link to direct filepath
    /// </summary>
    /// <remarks>Method returns the temporary attachment link</remarks>
    /// <param name="link">The link to direct filepath</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Attachment link</returns>
    /// <response code="200">Attachment link</response>
    /// <response code="401">Unauthorized access, no access token provided by a client</response>
    /// <response code="403">Resource forbidden.</response>
    /// <response code="404">Requested attachment is not found</response>
    /// <response code="500">Unhandled server error</response>
    [HttpGet("directlink")]
    [ProducesResponseType(typeof(ExternalLink), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDirectLinkAsync([FromQuery] string link, CancellationToken cancellationToken) {
        return Ok(await _dispatcher.Send(new GetDirectLinkRequest(link), cancellationToken));
    }
}