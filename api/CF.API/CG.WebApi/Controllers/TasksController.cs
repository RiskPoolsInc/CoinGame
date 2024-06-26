using App.Core.Commands.Notifications;
using App.Core.Commands.TaskExecutions;
using App.Core.Commands.Tasks;
using App.Core.Commands.TaskTakeRequests;
using App.Core.Requests.Dictionaries.Tasks;
using App.Core.Requests.TaskExecutions;
using App.Core.Requests.Tasks;
using App.Core.ViewModels;
using App.Core.ViewModels.Notifications;
using App.Core.ViewModels.TaskExecutions;
using App.Core.ViewModels.Tasks;
using App.Core.ViewModels.TaskTakeRequests;
using App.Core.ViewModels.Wallets;
using App.Interfaces.Core;
using App.Interfaces.Core.Requests;
using App.Web.Core;
using App.Web.Core.Errors;
using AutoMapper;
using ExternalApiServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CF.WebApi.Controllers
{
    [ApiController, ApiVersion("1.0"), Authorize, Route("api/v{version:apiVersion}/[controller]"),
     Produces(SupportedMimeTypes.Json)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(GenericErrorModel), StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public class TasksController : BaseController
    {
        private readonly IDispatcher _dispatcher;
        private readonly IMapper _mapper;

        public TasksController(IDispatcher dispatcher, IMapper mapper)
        {
            _dispatcher = dispatcher;
            _mapper = mapper;
        }

        /// <summary>
        /// Get Tasks
        /// </summary>
        /// <remarks>This method returns the list of tasks</remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of tasks</returns>
        /// <response code="200">The list of tasks</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost(RouteNames.Tasks.List)]
        [ProducesResponseType(typeof(PagedList<TaskView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync([FromBody] GetTasksRequest request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Get Customer Tasks
        /// </summary>
        /// <remarks>This method returns the list of tasks</remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of tasks</returns>
        /// <response code="200">The list of tasks</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost(RouteNames.Tasks.CustomerTasksList)]
        [ProducesResponseType(typeof(PagedList<TaskView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomerTasksAsync([FromBody] GetCustomerTasksRequest request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }


        /// <summary>
        /// Get Tasks with information
        /// </summary>
        /// <remarks>This method returns the list of tasks</remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of tasks</returns>
        /// <response code="200">The list of tasks</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost(RouteNames.Tasks.ListInfo)]
        [ProducesResponseType(typeof(PagedList<TaskView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPagedAsync([FromBody] GetTasksExecutedRequest request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Get Canceled Tasks
        /// </summary>
        /// <remarks>This method returns the list of tasks</remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of tasks</returns>
        /// <response code="200">The list of tasks</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost(RouteNames.Tasks.Canceled)]
        [ProducesResponseType(typeof(PagedList<TaskView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCanceledAsync([FromBody] GetCanceledTasksRequest request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Get Executions
        /// </summary>
        /// <remarks>This method returns the list of count tasks executions for task</remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of tasks</returns>
        /// <response code="200">The list of tasks</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost(RouteNames.Tasks.Executions)]
        [ProducesResponseType(typeof(IPagedList<TaskExecutionView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaskExecutionsAsync([FromBody] GetTaskExecutionsRequest request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Get Task Executions Paged Request
        /// </summary>
        /// <remarks>This method returns the list of count tasks executions for task</remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of tasks</returns>
        /// <response code="200">The list of tasks</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost(RouteNames.Tasks.ExecutionsPaged)]
        [ProducesResponseType(typeof(PagedList<TaskExecutionView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaskExecutionsPagedAsync([FromBody] GetTaskExecutionsPagedRequest request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Create Task
        /// </summary>
        /// <remarks>Method creates task by passed payload to the body</remarks>
        /// <param name="request">Payload</param>
        /// <param name="version"></param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Created task</returns>
        /// <response code="201">task</response>
        /// <response code="400">Payload is invalid</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("create")]
        [ProducesResponseType(typeof(TaskView), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var response = await _dispatcher.Send(request, cancellationToken);
            return Ok(response);
        }


        /// <summary>
        /// Update Task
        /// </summary>
        /// <remarks>Method updates task by passed payload to the body</remarks>
        /// <param name="request">Payload</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Updated task</returns>
        /// <response code="200">task</response>
        /// <response code="400">Payload is invalid</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="404">Requested task is not found</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPut]
        [ProducesResponseType(typeof(TaskView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateTaskCommand request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }


        /// <summary>
        /// Get Task Types
        /// </summary>
        /// <remarks>This method returns the list of task types</remarks>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of task types</returns>
        /// <response code="200">The list of task types</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("types")]
        [ProducesResponseType(typeof(DictionaryBaseView), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaskTypesAsync(CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new GetTaskTypesRequest(), cancellationToken));
        }

        /// <summary>
        /// Get Task State Types
        /// </summary>
        /// <remarks>This method returns the list of task state types</remarks>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of task state types</returns>
        /// <response code="200">The list of task state types</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("states")]
        [ProducesResponseType(typeof(DictionaryBaseView[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaskStateAsync(CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new GetTaskStateTypesRequest(), cancellationToken));
        }

        /// <summary>
        /// Get Task State Types
        /// </summary>
        /// <remarks>This method returns the list of task state group types</remarks>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of task state group types</returns>
        /// <response code="200">The list of task state group types</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("stategroups")]
        [ProducesResponseType(typeof(DictionaryBaseView[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaskStateGroupsAsync(CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new GetTaskStateGroupTypesRequest(), cancellationToken));
        }

        /// <summary>
        /// Get Task Actions
        /// </summary>
        /// <remarks>This method returns the list of task actions</remarks>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of task actions</returns>
        /// <response code="200">The list of task actions</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpGet("actions")]
        [ProducesResponseType(typeof(DictionaryBaseView), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaskActionsAsync(CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new GetTaskActionTypesRequest(), cancellationToken));
        }

        /// <summary>
        /// Follow Task
        /// </summary>
        /// <remarks>Method adds user to the followers</remarks>
        /// <param name="id">The unique task identifier</param>
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
            return Ok(await _dispatcher.Send(new FollowTaskCommand(id), cancellationToken));
        }

        /// <summary>
        /// Unfollow Task
        /// </summary>
        /// <remarks>Method removes user from the followers</remarks>
        /// <param name="id">The unique task identifier</param>
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
            return Ok(await _dispatcher.Send(new ForgetTaskCommand(id), cancellationToken));
        }

        /// <summary>
        /// Get tasks filter entities for searching by them
        /// </summary>
        /// <remarks>This method returns the list of filter entities</remarks>
        /// <param name="request">request parameters</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>The list of filter entities</returns>
        /// <response code="200">The list of filter entities</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("filters")]
        [ProducesResponseType(typeof(PagedList<FilterEntityView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFilterEntitiesAsync([FromBody] GetFilterEntityRequest request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        /// <summary>
        /// Check wallet balance
        /// </summary>
        /// <remarks>Method send to crowfeeding request to check wallet balance</remarks>
        /// <param name="request">Payload</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Created wallet</returns>
        /// <response code="201">Wallet</response>
        /// <response code="400">Payload is invalid</response>
        /// <response code="401">Unauthorized access, no access token provided by a client</response>
        /// <response code="403">Resource forbidden.</response>
        /// <response code="500">Unhandled server error</response>
        [HttpPost("{taskId:guid}/wallet/check")]
        [ProducesResponseType(typeof(WalletView), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CheckWalletBalanceAsync([FromBody] JObject request,
            CancellationToken cancellationToken
        )
        {
            return Ok();
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
        [HttpPost("reject-executions")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RejectExecutions([FromBody] RejectExecutionsCommand request,
            CancellationToken cancellationToken
        )
        {
            await _dispatcher.Send(request, cancellationToken);
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(typeof(IPagedList<TaskView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTasks([FromBody] GetTasksRequest request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        [HttpGet("preview")]
        [ProducesResponseType(typeof(IPagedList<TaskTinyView>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetTasksPreview([FromQuery] GetTasksPreviewRequest request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        [HttpPost("history")]
        [ProducesResponseType(typeof(IPagedList<TaskView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompletedTasks([FromBody] GetTasksCompletedRequest request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        [HttpGet]
        [Route("take-requests")]
        [ProducesResponseType(typeof(TaskTakeRequestView[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaskTakeRequests([FromQuery] GetTaskTakeRequestsRequest request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        [HttpPost]
        [Route("take-requests")]
        [ProducesResponseType(typeof(TaskTakeRequestView), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaskTakeRequestsPagedList([FromBody] GetTakeRequestsPagedRequest request,
            CancellationToken cancellationToken
        )
        {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }

        [HttpPut]
        [Route("take-request/{id:guid}/payed")]
        [ProducesResponseType(typeof(TaskTakeRequestView), StatusCodes.Status200OK)]
        public async Task<IActionResult> SetTaskRequestPayed(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _dispatcher.Send(new SetTaskRequestPayedCommand(id), cancellationToken));
        }


        [HttpGet]
        [Route("temp")]
        public void Temp()
        {
            //WalletService.CreateKeys();
            var ws = new WalletService();
            ws.CreateKeys();
        }
    }
}