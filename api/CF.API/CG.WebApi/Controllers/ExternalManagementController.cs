using App.Core.Commands.External;
using App.Core.Commands.TaskExecutions;
using App.Core.Commands.Tasks;
using App.Core.Requests.TaskExecutions;
using App.Core.ViewModels.TaskExecutions;
using App.Core.ViewModels.Tasks;
using App.Interfaces.Core;
using App.Interfaces.Core.Requests;
using App.Web.Core;
using App.Web.Core.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CF.WebApi.Controllers;

[ApiController, ApiVersion("1.0"), Authorize, Route("api/v{version:apiVersion}/external-management"),
 Produces(SupportedMimeTypes.Json)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
public class ExternalManagementController : BaseController
{
    public ExternalManagementController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    private readonly IDispatcher _dispatcher;

    /// <summary>
    /// Create Task
    /// </summary>
    /// <remarks>Method creates task by passed payload to the body</remarks>
    /// <param name="request">Payload</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Created task</returns>
    /// <response code="200">task</response>
    /// <response code="400">Payload is invalid</response>
    /// <response code="401">Unauthorized access, no access token provided by a client</response>
    /// <response code="403">Resource forbidden.</response>
    /// <response code="500">Unhandled server error</response>
    [HttpPost("tasks/create")]
    [ProducesResponseType(typeof(TaskView), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var response = await _dispatcher.Send(request, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Publish Task
    /// </summary>
    /// <remarks>Method send task to crowfeeding</remarks>
    /// <param name="taskId"></param>
    /// <param name="version"></param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Sent task</returns>
    /// <response code="201">task</response>
    /// <response code="400">Payload is invalid</response>
    /// <response code="401">Unauthorized access, no access token provided by a client</response>
    /// <response code="403">Resource forbidden.</response>
    /// <response code="500">Unhandled server error</response>
    [HttpPost("tasks/{taskId:guid}/publish")]
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
    [HttpPost("tasks/{taskId:guid}/complete")]
    [ProducesResponseType(typeof(TaskExecutionView[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> CompleteTaskAsync([FromRoute] Guid taskId,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _dispatcher.Send(new CompleteTaskCommand() {TaskId = taskId}, cancellationToken));
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
    [HttpPost("tasks/{taskId:guid}/executions")]
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
    [HttpPost("tasks/{taskId:guid}/confirm-execution")]
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
    /// Reject executions
    /// </summary>
    /// <remarks>Method reject executions</remarks>
    /// <param name="request">Payload</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Created wallet</returns>
    /// <response code="204"></response>
    /// <response code="400">Payload is invalid</response>
    /// <response code="401">Unauthorized access, no access token provided by a client</response>
    /// <response code="403">Resource forbidden.</response>
    /// <response code="500">Unhandled server error</response>
    [HttpPost("tasks/reject-executions")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RejectExecutions([FromBody] RejectExecutionsCommand request,
        CancellationToken cancellationToken
    )
    {
        await _dispatcher.Send(request, cancellationToken);
        return NoContent();
    }
}