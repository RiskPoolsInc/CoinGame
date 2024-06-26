using App.Core.Commands.Notes;
using App.Core.Requests.Tasks;
using App.Core.ViewModels;
using App.Interfaces.Core;
using App.Web.Core;
using App.Web.Core.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers {
    [ApiController, ApiVersion("1.0"), Authorize, Route("api/v{version:apiVersion}/tasks/{taskId}/notes"), Produces(SupportedMimeTypes.Json)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
    public class TaskNotesController : BaseController {
        public TaskNotesController(IDispatcher dispatcher) {
            _dispatcher = dispatcher;
        }
        private readonly IDispatcher _dispatcher;

        /// <summary>
        /// Get Task Notes
        /// </summary>
        /// <remarks>This method returns the list of task's notes</remarks>
        /// <param name="taskId">Task identifier</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of task's notes</returns>
        /// <response code="200">The list of task's notes</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(NoteView[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNotesAsync(Guid taskId, CancellationToken cancellationToken) {
            return Ok(await _dispatcher.Send(new GetTaskNotesRequest(taskId), cancellationToken));
        }

        /// <summary>
        /// Get Task Note
        /// </summary>
        /// <remarks>This method returns task note</remarks>
        /// <param name="id">Note identifier</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Task note</returns>
        /// <response code="200">Task note</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="404">Requested note is not found</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("{id}", Name = RouteNames.Tasks.Notes.GetById)]
        [ProducesResponseType(typeof(NoteView), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNoteAsync(Guid id, CancellationToken cancellationToken) {
            return Ok(await _dispatcher.Send(new GetTaskNoteRequest(id), cancellationToken));
        }

        /// <summary>
        /// Create Task Note
        /// </summary>
        /// <remarks>Method creates task's note by passed payload to the body</remarks>
        /// <param name="request">Payload</param>
        /// <param name="version"></param>
        /// <param name="taskId"></param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Created Task's Note</returns>
        /// <response code="201">task's Note</response>
        /// <response code="400">Payload is invalid</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost]
        [ProducesResponseType(typeof(NoteView), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTaskNoteCommand request, ApiVersion version, Guid taskId, CancellationToken cancellationToken) {
            var response = await _dispatcher.Send(request, cancellationToken);
            return CreatedAtRoute(RouteNames.Tasks.Notes.GetById, new {
                id = response.Id,
                taskId,
                version = $"{version}"
            }, response);
        }

        /// <summary>
        /// Update Task Note
        /// </summary>
        /// <remarks>Method updates task's note by passed payload to the body</remarks>
        /// <param name="request">Payload</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Updated Task Note</returns>
        /// <response code="200">Task Note</response>
        /// <response code="400">Payload is invalid</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="404">Requested note is not found</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPut]
        [ProducesResponseType(typeof(NoteView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateTaskNoteCommand request, CancellationToken cancellationToken) {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Delete Task Note
        /// </summary>
        /// <remarks>Method deletes task note by its unique Id</remarks>
        /// <param name="id">Task note Unique Id</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Empty Response</returns>
        /// <response code="200">Empty Response</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="404">Requested note is not found</response>
        /// <response code="500">Unhandled server error</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) {
            return Ok(await _dispatcher.Send(new DeleteTaskNoteCommand(id), cancellationToken));
        }
    }
}
