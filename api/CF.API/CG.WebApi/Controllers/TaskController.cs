using App.Core.Commands.Attachments;
using App.Core.Commands.External;
using App.Core.Commands.ExternalSystems.Tasks;
using App.Core.Commands.TaskExecutions;
using App.Core.Commands.Tasks;
using App.Core.Enums;
using App.Core.Requests.Attachments;
using App.Core.Requests.TaskExecutions;
using App.Core.Requests.Tasks;
using App.Core.ViewModels.Attachments;
using App.Core.ViewModels.TaskExecutions;
using App.Core.ViewModels.Tasks;
using App.Core.ViewModels.TaskTakeRequests;
using App.Interfaces.Core;
using App.Interfaces.Core.Requests;
using App.Interfaces.Security;
using App.Web.Core;
using App.Web.Core.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers
{
    [ApiController, ApiVersion("1.0"), Authorize, Route("api/v{version:apiVersion}/tasks/{taskId:guid}"),
     Produces(SupportedMimeTypes.Json)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
    public class TaskController : BaseController
    {
        private readonly IDispatcher _dispatcher;
        private readonly IContextProvider _contextProvider;

        public TaskController(IDispatcher dispatcher, IContextProvider contextProvider)
        {
            _dispatcher = dispatcher;
            _contextProvider = contextProvider;
        }

        /// <summary>
        /// Get Task Responders
        /// </summary>
        /// <remarks>This method returns the list of task responders</remarks>
        /// <param name="taskId">task id</param>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of tasks</returns>
        /// <response code="200">The list of tasks</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost(RouteNames.Tasks.Executions)]
        [ProducesResponseType(typeof(IPagedList<TaskExecutionView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaskExecutionsAsync([FromRoute] Guid taskId,
            [FromBody] GetTaskExecutionsRequest request,
            CancellationToken cancellationToken
        )
        {
            request.TaskIds = new[] {taskId};
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Send Task a Deposit
        /// </summary>
        /// <remarks>This method returns the answer from Crowfeeding with result of start Task for selected users </remarks>
        /// <param name="taskId">task id</param>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of tasks</returns>
        /// <response code="200">The list of tasks</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("send-deposit", Name = RouteNames.Tasks.SendDeposit)]
        [ProducesResponseType(typeof(TaskExecutionView[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> SendTaskDepositAsync([FromRoute] Guid taskId,
            [FromBody] SendTaskDepositCommand request,
            CancellationToken cancellationToken
        )
        {
            request.TaskId = taskId;
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Confirm Execution Task 
        /// </summary>
        /// <remarks>This method returns the answer from Crowfeeding with result of confirm execution Task for selected users </remarks>
        /// <param name="taskId">task id</param>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of tasks</returns>
        /// <response code="200">The list of tasks</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("confirm-execution", Name = RouteNames.Tasks.ConfirmExecution)]
        [ProducesResponseType(typeof(TaskExecutionView[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfirmExecutionAsync([FromRoute] Guid taskId,
            [FromBody] ConfirmTaskExecutionCommand request,
            CancellationToken cancellationToken
        )
        {
            request.TaskId = taskId;
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }


        /// <summary>
        /// Cancel Execution Task 
        /// </summary>
        /// <remarks>This method returns result of cancel execution Task for selected users </remarks>
        /// <param name="taskId">task id</param>
        /// <param name="id">Execution id</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of tasks</returns>
        /// <response code="200">The list of tasks</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpDelete("executions/{id:guid}/cancel", Name = RouteNames.Tasks.CancelExecution)]
        [ProducesResponseType(typeof(TaskExecutionView), StatusCodes.Status200OK)]
        public async Task<IActionResult> CancelExecutionAsync(
            [FromRoute] Guid taskId,
            [FromRoute] Guid id,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(new CancelTaskExecutionCommand()
            {
                TaskId = taskId,
                TaskExecutionId = id
            }, cancellationToken));
        }

        /// <summary>
        /// Complete Task 
        /// </summary>
        /// <remarks>This method returns the answer from Crowfeeding with result of complete Task for selected users </remarks>
        /// <param name="taskId">task id</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of tasks</returns>
        /// <response code="200">The list of tasks</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("complete", Name = RouteNames.Tasks.Complete)]
        [ProducesResponseType(typeof(TaskExecutionView[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> CompleteTaskAsync([FromRoute] Guid taskId,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(new CompleteTaskCommand() {TaskId = taskId}, cancellationToken));
        }

        /// <summary>
        /// Delete Task 
        /// </summary>
        /// <remarks>This method returns the answer from Crowfeeding with result of cancel Task</remarks>
        /// <param name="taskId">task id</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of tasks</returns>
        /// <response code="200">The canceled task</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpDelete(Name = RouteNames.Tasks.Cancel)]
        [ProducesResponseType(typeof(TaskView), StatusCodes.Status200OK)]
        public async Task<IActionResult> CancelTaskAsync([FromRoute] Guid taskId,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(new CancelTaskCommand() {TaskId = taskId}, cancellationToken));
        }


        /// <summary>
        /// Add Task Note 
        /// </summary>
        /// <remarks>This method returns added the Task note</remarks>
        /// <param name="taskId">Task Id</param>
        /// <param name="id">Task Execution id</param>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of tasks</returns>
        /// <response code="200">The list of tasks</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("executions/{id:guid}/add-note", Name = RouteNames.TaskExecutions.Notes.Add)]
        [ProducesResponseType(typeof(TaskExecutionView), StatusCodes.Status200OK)]
        [Consumes(SupportedMimeTypes.FormData)]
        public async Task<IActionResult> TaskExecutionAddNoteAsync(
            [FromRoute] Guid taskId,
            [FromRoute] Guid id,
            [FromForm] TaskExecutionAddNoteCommand request,
            CancellationToken cancellationToken
        )
        {
            request.TaskId = taskId;
            request.TaskExecutionId = id;
            request.Attachments = request.FileAttachments?.Select(a => new AttachFileModel() {Auto = false, File = a})
                .ToArray();
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Get Task Execution Notes 
        /// </summary>
        /// <remarks>This method returns added the Task note</remarks>
        /// <param name="taskId">Task Id</param>
        /// <param name="id">Task Execution id</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of tasks</returns>
        /// <response code="200">The list of tasks</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("executions/{id:guid}/notes", Name = RouteNames.TaskExecutions.Notes.Get)]
        [ProducesResponseType(typeof(TaskExecutionNoteView[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaskExecutionNotesAsync([FromRoute] Guid taskId,
            [FromRoute] Guid id,
            CancellationToken cancellationToken
        )
        {
            var request = new GetTaskExecutionNotesRequest
            {
                TaskId = taskId,
                TaskExecutionId = id
            };
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }


        /// <summary>
        /// Set Task state is Ready
        /// </summary>
        /// <remarks>This method returns  the Task Execution</remarks>
        /// <param name="taskId">Task Id</param>
        /// <param name="id">Task Execution id</param>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of tasks</returns>
        /// <response code="200">The list of tasks</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("executions/{id:guid}/ready", Name = RouteNames.TaskExecutions.Ready)]
        [ProducesResponseType(typeof(TaskExecutionView[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> TaskReadyAsync(
            Guid taskId,
            Guid id,
            [FromBody] TaskExecutionReadyCommand request,
            CancellationToken cancellationToken
        )
        {
            request.TaskId = taskId;
            request.TaskExecutionId = id;
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Create Task Attachments
        /// </summary>
        /// <remarks>Method creates task attachments by passed payload to the body</remarks>
        /// <param name="taskId">Task Id</param>
        /// <param name="request">Payload</param>
        /// <param name="version"></param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Updated task</returns>
        /// <response code="201">task</response>
        /// <response code="400">Payload is invalid</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("attachments")]
        [ProducesResponseType(typeof(TaskView), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
        [Consumes(SupportedMimeTypes.FormData)]
        public async Task<IActionResult> CreateAttachmentsAsync(Guid taskId,
            [FromForm] CreateTaskAttachmentsCommand request,
            ApiVersion version,
            CancellationToken cancellationToken
        )
        {
            request.Id = taskId;
            var response = await _dispatcher.Send(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Publish Task
        /// </summary>
        /// <remarks>Method send task to crowfeeding</remarks>
        /// <param name="version"></param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Sent task</returns>
        /// <response code="201">task</response>
        /// <response code="400">Payload is invalid</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("publish")]
        [ProducesResponseType(typeof(TaskView), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PublishAsync([FromRoute] Guid taskId,
            ApiVersion version,
            CancellationToken cancellationToken
        )
        {
            var response = await _dispatcher.Send(new PublishTaskCommand() {Id = taskId}, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Publish Task as Registration
        /// </summary>
        /// <remarks>Method send task to crowfeeding</remarks>
        /// <param name="version"></param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Sent task</returns>
        /// <response code="201">task</response>
        /// <response code="400">Payload is invalid</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("publish-registration")]
        [ProducesResponseType(typeof(TaskView), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PublishAsRegistrationTaskAsync([FromRoute] Guid taskId,
            ApiVersion version,
            CancellationToken cancellationToken
        )
        {
            var response = await _dispatcher.Send(new PublishTaskCommand()
            {
                Id = taskId,
                TypeId = (int) TaskTypes.FirstRegistration
            }, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Patch Task
        /// </summary>
        /// <remarks>Method updates task by passed payload to the body</remarks>
        /// <param name="taskId">Unqiue Id</param>
        /// <param name="request">Payload</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Updated task</returns>
        /// <response code="200">Task</response>
        /// <response code="400">Payload is invalid</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="404">Requested task is not found</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPatch]
        [ProducesResponseType(typeof(TaskView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchAsync(Guid taskId,
            [FromBody] JsonPatchDocument<PatchTaskCommand> request,
            CancellationToken cancellationToken
        )
        {
            var command = new PatchTaskCommand
            {
                Id = taskId
            };
            request.ApplyTo(command);
            return Ok(await _dispatcher.Send(command, cancellationToken));
        }


        /// <summary>
        /// Get Task Attachments
        /// </summary>
        /// <remarks>Method returns the task attachments by task Id</remarks>
        /// <param name="taskId">The unique task identifier</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of task v</returns>
        /// <response code="200">Task attachments</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("attachments")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AttachmentView[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaskAttachmentsAsync(Guid taskId, CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new GetTaskAttachmentsRequest(taskId), cancellationToken));
        }

        /// <summary>
        /// Create Task Attachment
        /// </summary>
        /// <remarks>Method creates incoming attachment by file</remarks>
        /// <param name="taskId">incoming unique identifier</param>
        /// <param name="model">uploaded file</param>
        /// <param name="version"></param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Created Incoming Attachment</returns>
        /// <response code="201">Incoming Attachment</response>
        /// <response code="400">Payload is invalid</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("attachment")]
        [ProducesResponseType(typeof(AttachmentView), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
        [Consumes(SupportedMimeTypes.FormData)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> CreateAttachmentAsync([FromRoute] Guid taskId,
            [FromForm] AttachFileModel model,
            ApiVersion version,
            CancellationToken cancellationToken
        )
        {
            var response = await _dispatcher.Send(new CreateTaskAttachCommand(taskId, model), cancellationToken);
            return CreatedAtRoute(RouteNames.Attachments.GetById, new {id = response.Id, version = $"{version}"},
                response);
        }


        [HttpGet]
        [ProducesResponseType(typeof(TaskView), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTask(Guid taskId, CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new GetTaskRequest(taskId), cancellationToken));
        }


        [HttpGet]
        [Route("take-request")]
        [ProducesResponseType(typeof(TaskTakeRequestView), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaskTakeRequest(Guid taskId, CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new GetTaskTakeRequest(taskId), cancellationToken));
        }


        [HttpGet]
        [Route("take-requests")]
        [ProducesResponseType(typeof(TaskTakeRequestView), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRequestsByTask([FromRoute] Guid taskId, CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new GetRequestsByTaskRequest() {TaskId = taskId}, cancellationToken));
        }

        [HttpPut]
        [Route("take-request")]
        [ProducesResponseType(typeof(TaskTakeRequestView), StatusCodes.Status200OK)]
        public async Task<IActionResult> SendTaskTakeRequest(Guid taskId,
            [FromBody] SendTaskTakeRequestCommand requestCommand,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(new SendTaskTakeRequestCommand()
            {
                Email = requestCommand.Email,
                TaskId = taskId
            }, cancellationToken));
        }
    }
}